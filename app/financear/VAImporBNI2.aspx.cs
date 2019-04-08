using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Data.OleDb;

namespace ISC064.FINANCEAR
{
    public partial class VAImporBNI2 : System.Web.UI.Page
    {
        protected DataTable rsTagihan = new DataTable();
        protected int index = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!IsPostBack)
            {
                div1.Visible = true;
            }

            Cek(FilePath);

            Js.Confirm(this, "Lanjutkan proses upload transaksi virtual account?");
        }

        private void Cek(string path)
        {
            string strSql = "SELECT * FROM [Sheet1$]";
            DataTable rs = new DataTable();

            try
            {
                rs = Db.xls(strSql, FilePath);
            }
            catch { }


            Up(path);

        }

        private void Up(string path)
        {
            if (File.Exists(path))
            {
                string strSql = "SELECT * FROM [Sheet1$]";
                DataTable rs = Db.xls(strSql, path);

                for (int i = 1; i < rs.Rows.Count; i++)
                {
                    if (!Response.IsClientConnected) break;

                    Save(rs, i);
                }
            }
        }

        private bool Save(DataTable rs, int i)
        {
            string NoVA = Cf.Str(rs.Rows[i][2]);
            //string NoStock = Cf.Str(rs.Rows[i][1]);
            DateTime Tgl = Convert.ToDateTime(rs.Rows[i][1]);
            decimal Nilai = Convert.ToDecimal(rs.Rows[i][6]);
            int x = i;

            int nova = Db.SingleInteger("SELECT COUNT(*) FROM REF_VA WHERE NoVA = '" + NoVA + "'");

            string Proj = (Project == "SEMUA") ? " AND a.Project IN(" + Act.ProjectListSql + ")" : " AND a.Project = '" + Project + "'";
            DataTable rs2 = Db.Rs("SELECT a.*, b.NoKontrak, b.NoUnit"
                    + ", (SELECT Nama FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER WHERE NoCustomer = b.NoCustomer) AS Cs"
                    + " FROM REF_VA a"
                    + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK b ON a.NoVA = b.NoVA"
                    + " WHERE a.NoVA = '" + NoVA + "' " + Proj + "");

            if (rs2.Rows.Count > 0)
            {
                FillTb(rs2.Rows[0]["NoKontrak"].ToString(), rs2.Rows[0]["Cs"].ToString(), rs2.Rows[0]["NoUnit"].ToString(),
                    NoVA, Tgl, Nilai, x);

                return true;
            }
            else
                return false;
        }

        protected void FillTb(string NoKontrak, string Cs, string NoUnit, string NoVA, DateTime Tgl, decimal Nilai, int n)
        {
            decimal x = Nilai;
            int j = 0;

            string strSql = "SELECT *, MS_TAGIHAN.NoUrut as Notag, MS_TAGIHAN.NamaTagihan as Namatag  "
                + ",(NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE NoTagihan = MS_TAGIHAN.NoUrut AND NoKontrak = '" + NoKontrak + "') ) AS SisaTagihan"
                + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN AS MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "'"
                + " AND (NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE NoTagihan = MS_TAGIHAN.NoUrut AND NoKontrak = '" + NoKontrak + "') ) > 0"
                + " ORDER BY TglJT, NoUrut";
            DataTable rs = Db.Rs(strSql);
            rsTagihan.Merge(rs);


            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                if (i == 0) j = index;

                Label l;
                TextBox t;

                l = new Label();
                string s = "";
                if (i == 0)
                    s = "<td align=center>" + n + "</td>"
                        + "<td>" + NoVA + "</td>"
                        + "<td>" + Cf.Day(Tgl) + "</td>"
                        + "<td>" + NoKontrak + "</td>"
                        + "<td>" + Cs + "</td>"
                        + "<td>" + NoUnit + "</td>"
                        + "<td align=right>" + Cf.Num(Nilai) + "</td>";
                else
                    s = "<td colspan=7 />";
                l.Text = "<tr valign=top>"
                    + s
                    + "<td>" + rs.Rows[i]["NamaTagihan"] + "</td>"
                    + "<td>" + rs.Rows[i]["Tipe"] + "</td>"
                    + "<td style='white-space:nowrap'>" + Cf.Day(rs.Rows[i]["TglJT"]) + "</td>"
                    + "<td align=right>" + Cf.Num(rs.Rows[i]["SisaTagihan"]) + "</td>"
                    + "<td>"
                    ;
                ph.Controls.Add(l);


                t = new TextBox();
                t.ID = "lunas_" + index;
                t.Width = 100;
                t.CssClass = "txt_num";
                t.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
                t.Attributes["onkeyup"] = "CalcType(this,tempnum);";
                t.Attributes["onblur"] = "CalcBlur(this);";
                ph.Controls.Add(t);


                l = new Label();
                l.Text = "</td>"
                    + "<td>";
                ph.Controls.Add(l);

                t = new TextBox();
                t.ID = "pembulatan_" + index;
                t.Width = 100;
                t.CssClass = "txt_num";
                t.Text = "0";
                t.AutoPostBack = true;
                t.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
                t.Attributes["onkeyup"] = "CalcType(this,tempnum);";
                t.Attributes["onblur"] = "CalcBlur(this);";
                t.Attributes.Add("OnChange", "validate();");
                ph.Controls.Add(t);

                l = new Label();
                l.Text = "</td>"
                    + "<td>";
                ph.Controls.Add(l);

                t = new TextBox();
                t.ID = "lebihbayar_" + index;
                t.Width = 100;
                t.Text = "0";
                t.AutoPostBack = true;
                t.CssClass = "txt_num";
                t.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
                t.Attributes["onkeyup"] = "CalcType(this,tempnum);";
                t.Attributes["onblur"] = "CalcBlur(this);";
                t.Attributes.Add("OnChange", "validate();");
                ph.Controls.Add(t);

                l = new Label();
                l.Text = "</td>";
                ph.Controls.Add(l);

                t = new TextBox();
                t.ID = "va_" + index;
                t.Visible = false;
                t.Text = NoVA;
                ph.Controls.Add(t);

                t = new TextBox();
                t.ID = "tgl_" + index;
                t.Visible = false;
                t.Text = Cf.Day(Tgl);
                ph.Controls.Add(t);

                t = new TextBox();
                t.ID = "notag_" + index;
                t.Visible = false;
                t.Text = rs.Rows[i]["Notag"].ToString();
                ph.Controls.Add(t);

                t = new TextBox();
                t.ID = "namatag_" + index;
                t.Visible = false;
                t.Text = rs.Rows[i]["Namatag"].ToString();
                ph.Controls.Add(t);

                t = new TextBox();
                t.ID = "ref_" + index;
                t.Visible = false;
                t.Text = NoKontrak;
                ph.Controls.Add(t);

                t = new TextBox();
                t.ID = "cs_" + index;
                t.Visible = false;
                t.Text = Cs;
                ph.Controls.Add(t);

                t = new TextBox();
                t.ID = "unit_" + index;
                t.Visible = false;
                t.Text = NoUnit;
                ph.Controls.Add(t);

                t = new TextBox();
                t.ID = "tag_" + index;
                t.Visible = false;
                t.Text = rs.Rows[i]["NoUrut"]
                    + ";" + rs.Rows[i]["NamaTagihan"]
                    + ";" + rs.Rows[i]["Tipe"];
                ph.Controls.Add(t);

                l = new Label();
                l.Text = "</td>"
                    + "<td><input type='checkbox' onclick=\"tagihan('" + index + "','" + Cf.Num(rs.Rows[i]["SisaTagihan"]) + "', this)\"></td>"
                    + "</tr>";
                ph.Controls.Add(l);


                index++;
            }

            //Alokasi
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TextBox lunas = (TextBox)ph.FindControl("lunas_" + j);
                decimal SisaTagihan = (decimal)rs.Rows[i]["SisaTagihan"];

                if (i == rs.Rows.Count - 1)
                    lunas.Text = Cf.Num(x);
                else
                {
                    if (SisaTagihan >= x)
                    {
                        lunas.Text = Cf.Num(x);
                        break;
                    }
                    else
                        lunas.Text = Cf.Num(SisaTagihan);
                }
                x -= SisaTagihan;
                j++;
            }
        }
        protected void validate()
        {
            for (int i = 0; i < rsTagihan.Rows.Count; i++)
            {
                TextBox bulat = (TextBox)ph.FindControl("pembulatan_" + i);
                TextBox lebihbayar = (TextBox)ph.FindControl("lebihbayar_" + i);

                if (bulat.Text != "0")
                {
                    lebihbayar.ReadOnly = true;
                    lebihbayar.Enabled = false;
                }
                else if (lebihbayar.Text != "0")
                {
                    bulat.ReadOnly = true;
                    bulat.Enabled = false;
                }
            }
        }
        protected bool datavalid()
        {
            bool x = true;
            string s = "";

            bool adasatu = false;
            for (int i = 0; i < rsTagihan.Rows.Count; i++)
            {
                TextBox lunas = (TextBox)ph.FindControl("lunas_" + i);
                if (lunas.Text != "")
                {
                    adasatu = true;
                    try
                    {
                        decimal z = Convert.ToDecimal(lunas.Text);
                    }
                    catch
                    {
                        x = false;
                        if (s == "") s = lunas.ID;
                    }
                }
            }

            if (!adasatu) x = false;

            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Pembayaran harus berupa angka dan minimal untuk satu tagihan.\\n"
                    , "document.getElementById('" + s + "').focus();"
                    );

            return x;
        }
        protected void save_Click(object sender, System.EventArgs e)
        {
            string ID = "";
            if (datavalid())
            {
                for (int i = 0; i < rsTagihan.Rows.Count; i++)
                {
                    TextBox lunas = (TextBox)ph.FindControl("lunas_" + i);
                    TextBox bulat = (TextBox)ph.FindControl("pembulatan_" + i);
                    TextBox lebihbayar = (TextBox)ph.FindControl("lebihbayar_" + i);
                    TextBox va = (TextBox)ph.FindControl("va_" + i);
                    TextBox tgl = (TextBox)ph.FindControl("tgl_" + i);
                    TextBox nokontrak = (TextBox)ph.FindControl("ref_" + i);
                    TextBox cs = (TextBox)ph.FindControl("cs_" + i);
                    TextBox unit = (TextBox)ph.FindControl("unit_" + i);
                    TextBox tag = (TextBox)ph.FindControl("tag_" + i);
                    TextBox notag = (TextBox)ph.FindControl("notag_" + i);
                    TextBox namatag = (TextBox)ph.FindControl("namatag_" + i);

                    if (lunas.Text != "")
                    {
                        #region
                        Db.Execute("EXEC spTTSRegistrasi"
                            + " '" + Convert.ToDateTime(tgl.Text) + "'"
                            + ",'" + Act.UserID + "'"
                            + ",'" + Act.IP + "'"
                            + ",'JUAL'"
                            + ",'" + Cf.Str(nokontrak.Text) + "'"
                            + ",'" + Cf.Str(unit.Text) + "'"
                            + ",'" + Cf.Str(cs.Text) + "'"
                            + ",'TR'"
                            + ",'VA: " + Cf.Str(va.Text) + "'"
                            );

                        int NoTTS = Db.SingleInteger("SELECT TOP 1 NoTTS FROM MS_TTS ORDER BY NoTTS DESC");
                        Db.Execute("UPDATE MS_TTS"
                            + " SET Acc = '" + Bank + "'"
                            + " WHERE NoTTS = " + NoTTS
                            );

                        string[] arr = tag.Text.Split(';');
                        Db.Execute("EXEC spTTSAlokasi "
                            + "  " + NoTTS
                            + ", " + arr[0]
                            + ", " + Convert.ToDecimal(lunas.Text)
                            );

                        Db.Execute("EXEC spPostingTTS " + NoTTS + ", '" + Convert.ToDateTime(tgl.Text) + "'");
                        decimal LB = Convert.ToDecimal(lebihbayar.Text);
                        decimal B = Convert.ToDecimal(bulat.Text);
                        Db.Execute("UPDATE MS_TTS"
                            + " SET ManualBKM = ManualTTS"
                            + ", TanggalUangDiterima = TglBKM"
                            + ", LB = " + LB
                            + ", LebihBayar = " + B
                            + " WHERE NoTTS = " + NoTTS);

                        //Ambil Stok No. FP
                        DataTable fp = Db.Rs("SELECT * FROM REF_FP WHERE Status = 0");
                        if (fp.Rows.Count > 0)
                        {
                            Db.Execute("UPDATE MS_TTS SET"
                                + " NoFPS = '" + fp.Rows[0]["NoFPS"].ToString() + "'"
                                + " WHERE NoTTS = " + NoTTS);

                            Db.Execute("UPDATE REF_FP SET"
                                + " Status = 1"
                                + " WHERE NoFPS = '" + fp.Rows[0]["NoFPS"].ToString() + "'");
                        }

                        int NoBKM = Db.SingleInteger("SELECT NoBKM FROM MS_TTS WHERE NoTTS = " + NoTTS);

                        DataTable rsLog = Db.Rs("SELECT "
                            + " CONVERT(varchar, TglTTS, 106) AS [Tanggal]"
                            + ",Tipe"
                            + ",Ref AS [Ref.]"
                            + ",Unit"
                            + ",Customer"
                            + ",CaraBayar AS [Cara Bayar]"
                            + ",Ket AS [Keterangan]"
                            + ",Total"
                            + ",NoBG AS [No. BG]"
                            + ",CONVERT(varchar, TglBG, 106) AS [Tanggal BG]"
                            + ", Acc AS [Rekening Bank]"
                            + " FROM MS_TTS WHERE NoTTS = " + NoTTS);

                        string KetLog = Cf.LogCapture(rsLog)
                            + "<br>***ALOKASI PEMBAYARAN:<br>"
                            + arr[1] + " (" + arr[2] + ")";

                        Db.Execute("EXEC spLogTTS"
                            + " 'VA'"
                            + ",'" + Act.UserID + "'"
                            + ",'" + Act.IP + "'"
                            + ",'" + KetLog + "'"
                            + ",'" + NoTTS.ToString().PadLeft(7, '0') + "'"
                            );

                        decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_TTS_LOG ORDER BY LogID DESC");
                        string Project = Db.SingleString("SELECT Project FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = (SELECT Ref FROM MS_TTS WHERE NoTTS = '" + NoTTS + "')");
                        Db.Execute("UPDATE MS_TTS_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                        //PEMBULATAN
                        decimal pembulatan = Convert.ToDecimal(bulat.Text);
                        if (pembulatan > 0)
                        {
                            Db.Execute("EXEC spMEMORegistrasi"
                                  + " '" + Convert.ToDateTime(tgl.Text) + "'"
                                  + ",'" + Act.UserID + "'"
                                  + ",'" + Act.IP + "'"
                                  + ",'JUAL'"
                                  + ",'" + Cf.Str(nokontrak.Text) + "'"
                                  + ",'" + Cf.Str(unit.Text) + "'"
                                  + ",'" + Cf.Str(cs.Text) + "'"
                                  + ",'PP'"
                                  + ",''"
                                  + "," + NoTTS
                                  + ",0"
                                  );

                            int NoMEMO = 0;
                            if (Db.SingleInteger("SELECT COUNT(*) FROM MS_MEMO") > 0)
                                NoMEMO = Db.SingleInteger("SELECT TOP 1 NoMEMO FROM MS_MEMO ORDER BY NoMEMO DESC");
                            System.Text.StringBuilder alokasiM = new System.Text.StringBuilder();

                            decimal NilaiTagihan = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiTagihan),0) FROM ISC064_MARKETINGJUAL..MS_TAGIHAN WHERE NoUrut = " + notag.Text + " AND NoKontrak = '" + nokontrak.Text + "'");
                            decimal Pelunasan = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan),0) FROM ISC064_MARKETINGJUAL..MS_PELUNASAN WHERE NoTagihan = " + notag.Text + " AND NoKontrak = '" + nokontrak.Text + "'");
                            decimal SisaTag = NilaiTagihan - Pelunasan;
                            decimal n = 0;

                            if (SisaTag > 0)
                            {
                                n = SisaTag < pembulatan ? SisaTag : pembulatan;

                                Db.Execute("EXEC spMEMOAlokasi "
                                    + "  " + NoMEMO
                                    + ", " + notag.Text
                                    + ", " + n
                                    + ",0"
                                    );

                                Db.Execute("UPDATE ISC064_MARKETINGJUAL..MS_PELUNASAN"
                                     + " SET"
                                     + " TglPelunasan ='" + Convert.ToDateTime(tgl.Text) + "'"
                                     + ", SudahCair='1'"
                                     + " WHERE NoKontrak='" + nokontrak.Text + "' AND NoMemo='" + NoMEMO + "' AND NoTagihan='" + notag.Text + "'"
                                    );
                                Db.Execute("UPDATE MS_MEMO SET Status='POST' WHERE NoMemo='" + NoMEMO + "'");

                                alokasiM.Append(namatag.Text + "    " + Cf.Num(lunas.Text) + "<br>");

                                DataTable rsM = Db.Rs("SELECT "
                                            + " CONVERT(varchar, TglMEMO, 106) AS [Tanggal]"
                                            + ",Tipe"
                                            + ",Ref AS [Ref.]"
                                            + ",Unit"
                                            + ",Customer"
                                            + ",CaraBayar AS [Cara Bayar]"
                                            + ",Ket AS [Keterangan]"
                                            + ",Total"
                                            + ",NoBG AS [No. BG]"
                                            + ",CONVERT(varchar, TglBG, 106) AS [Tanggal BG]"
                                            + ", Acc AS [Rekening Bank]"
                                            + " FROM MS_MEMO WHERE NoMEMO = " + NoMEMO);

                                string KetLogM = Cf.LogCapture(rsM)
                                    + "<br>***ALOKASI PEMBAYARAN:<br>"
                                    + alokasiM.ToString();

                                Db.Execute("EXEC spLogMEMO"
                                    + " 'REGIS'"
                                    + ",'" + Act.UserID + "'"
                                    + ",'" + Act.IP + "'"
                                    + ",'" + KetLogM + "'"
                                    + ",'" + NoMEMO.ToString().PadLeft(7, '0') + "'"
                                    );

                                decimal LogID2 = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_MEMO_LOG ORDER BY LogID DESC");
                                string Project2 = Db.SingleString("SELECT Project FROM MS_MEMO WHERE NoMEMO = '" + NoMEMO + "'");
                                Db.Execute("UPDATE MS_MEMO_LOG SET Project = '" + Project2 + "' WHERE LogID  = " + LogID2);

                                Db.Execute("EXEC ISC064_MARKETINGJUAL..spProsentasePelunasan '" + nokontrak.Text + "'");
                                Db.Execute("UPDATE ISC064_MARKETINGJUAL..MS_KONTRAK SET FlagMemo=1 WHERE NoKontrak='" + nokontrak.Text + "'");
                            }
                        }
                        #endregion

                        ID += NoTTS + ";";
                    }
                }

                //div1.Visible = false;

                //feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                //    + "Upload Berhasil.."
                //    ;

                Dfc.DeleteFile(FilePath);

                string url = "VAImporBNI3.aspx?id=" + ID;
                Response.Redirect(url);
            }
        }
        protected string FilePath
        {
            get
            {
                return Request.QueryString["path"];
            }
        }
        protected string Bank
        {
            get
            {
                return Request.QueryString["bank"];
            }
        }
        private string Project
        {
            get
            {
                return Cf.Pk(Request.QueryString["project"]);
            }
        }
    }
}

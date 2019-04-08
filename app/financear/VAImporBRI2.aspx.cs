using System.IO;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Globalization;

namespace ISC064.FINANCEAR
{
    public partial class VAImporBRI2 : System.Web.UI.Page
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
                div2.Visible = false;
            }

            Init();

            Js.Confirm(this, "Lanjutkan proses upload transaksi virtual account?");
        }
        public int baris;
        protected void Init()
        {
            if (File.Exists(FilePath))
            {
                string strSql = "SELECT * FROM [Sheet1$]";
                DataTable rs2 = Db.xls(strSql, FilePath);

                baris = 0;
                for (int i = 0; i < rs2.Rows.Count; i++)
                {
                    if (!Response.IsClientConnected) break;

                    string NoVA = Cf.Str(rs2.Rows[i][2].ToString()) + Cf.Str(rs2.Rows[i][3].ToString());
                    string ket = Cf.Str(rs2.Rows[i][5]);
                    DateTime Tgl = DateTime.Now;//DateTime.ParseExact(rs2.Rows[i][1].ToString(), "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture);
                    decimal Nilai = Convert.ToDecimal(rs2.Rows[i][4].ToString());

                    DataTable rs = Db.Rs("SELECT a.*, b.NoKontrak, b.NoUnit"
                                + ", (SELECT Nama FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER WHERE NoCustomer = b.NoCustomer) AS Cs"
                                + " FROM REF_VA a"
                                + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK b ON a.NoVA = b.NoVA"
                                + " WHERE a.NoVA = '" + NoVA + "'");
                    if (rs.Rows.Count > 0)
                    {
                        FillTb(rs.Rows[0]["NoKontrak"].ToString(), rs.Rows[0]["Cs"].ToString(), rs.Rows[0]["NoUnit"].ToString(),
                            NoVA, Tgl, Nilai, ket, baris);
                        baris++;
                    }

                }


            }
        }
        protected void FillTb(string NoKontrak, string Cs, string NoUnit, string NoVA, DateTime Tgl, decimal Nilai, string Ket, int n)
        {
            decimal x = Nilai;
            int j = 0;

            string strSql = "SELECT * "
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
                    s = "<td align=center>" + (n+1) + "</td>"
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
                t.ID = "lunas_" + n + "_" + index;
                t.Width = 100;
                t.CssClass = "txt_num";
                t.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
                t.Attributes["onkeyup"] = "CalcType(this,tempnum);";
                t.Attributes["onblur"] = "CalcBlur(this);";
                ph.Controls.Add(t);

                l = new Label();
                l.Text = "</td>"
                    + "<td><input type='checkbox' onclick=\"tagihan('" + index + "','" + Cf.Num(rs.Rows[i]["SisaTagihan"]) + "', this)\"></td>"
                    + "</tr>";
                ph.Controls.Add(l);

                t = new TextBox();
                t.ID = "tag_" + n + "_" + index;
                t.Visible = false;
                t.Text = rs.Rows[i]["NoUrut"]
                    + ";" + rs.Rows[i]["NamaTagihan"]
                    + ";" + rs.Rows[i]["Tipe"];
                ph.Controls.Add(t);

                if (i == 0)
                {
                    t = new TextBox();
                    t.ID = "va_" + n;
                    t.Visible = false;
                    t.Text = NoVA;
                    ph.Controls.Add(t);

                    t = new TextBox();
                    t.ID = "tgl_" + n;
                    t.Visible = false;
                    t.Text = Cf.Day(Tgl);
                    ph.Controls.Add(t);

                    t = new TextBox();
                    t.ID = "ref_" + n;
                    t.Visible = false;
                    t.Text = NoKontrak;
                    ph.Controls.Add(t);

                    t = new TextBox();
                    t.ID = "cs_" + n;
                    t.Visible = false;
                    t.Text = Cs;
                    ph.Controls.Add(t);

                    t = new TextBox();
                    t.ID = "unit_" + n;
                    t.Visible = false;
                    t.Text = NoUnit;
                    ph.Controls.Add(t);

                    t = new TextBox();
                    t.ID = "ket_" + n;
                    t.Visible = false;
                    t.Text = Ket;
                    ph.Controls.Add(t);

                }

                index++;
            }

            //Alokasi
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TextBox lunas = (TextBox)ph.FindControl("lunas_" + n + "_" + j);
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
        protected bool datavalid()
        {
            bool x = true;
            string s = "";

            bool adasatu = false;

            for (int j = 0; j < baris; j++)
            {
                for (int i = 0; i < rsTagihan.Rows.Count; i++)
                {

                    TextBox lunas = (TextBox)ph.FindControl("lunas_" + j + "_" + i);

                    if (lunas != null && lunas.Text != "")
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
            }

            if (!adasatu) x = false;

            if (!x)
                Js.Alert(
                    this
                    , "Input tidak valid.\\n\\n"
                    + "Ketentuan:\\n"
                    + "1. Transaksi harus ada minimal satu." + baris + " \\n"
                    , "document.getElementById('" + s + "').focus();"
                    );

            return x;
        }

        protected void save_Click2(object sender, System.EventArgs e)
        {
            if (datavalid())
            {
                Js.Alert(
                   this
                   , "Input valid.\\n\\n"
                   , ""
                   );
            }
        }
        protected void save_Click(object sender, System.EventArgs e)
        {
            if (datavalid())
            {
                for (int j = 0; j < baris; j++)
                {
                    TextBox va = (TextBox)ph.FindControl("va_" + j);
                    TextBox tgl = (TextBox)ph.FindControl("tgl_" + j);
                    TextBox nokontrak = (TextBox)ph.FindControl("ref_" + j);
                    TextBox cs = (TextBox)ph.FindControl("cs_" + j);
                    TextBox unit = (TextBox)ph.FindControl("unit_" + j);
                    TextBox ket = (TextBox)ph.FindControl("ket_" + j);

                    DateTime TglBKM = Convert.ToDateTime(tgl.Text);
                    DateTime TglTTS = Convert.ToDateTime(tgl.Text);
                    string KetTag = "";



                    # region NoTTS yg pake format
                    //NoTTS
                    string formatMonth = Cf.Roman(TglTTS.Month);
                    string formatTahun = TglTTS.Year.ToString().Substring(2, 2);
                    string NoTTS2 = "";

                    bool hasfound = false;
                    while (!hasfound)
                    {
                        if (!Response.IsClientConnected) break;

                        int num = Db.SingleInteger("SELECT COUNT(NoTTS2) FROM MS_TTS WHERE MONTH(TglTTS)='" + TglTTS.Month + "' AND YEAR(TglTTS)='" + TglTTS.Year + "'");
                        if (num == 0)
                        {
                            //TTS Pertama
                            int increment = num + 1;
                            string no = increment.ToString().PadLeft(7, '0');
                            NoTTS2 = "TTS/" + formatTahun + "/" + formatMonth + "/" + no;

                        }
                        else
                        {
                            //TTS Berikutnya
                            string terakhir = Db.SingleString("SELECT TOP 1 NoTTS2 FROM MS_TTS WHERE MONTH(TglTTS)='" + TglTTS.Month + "' AND YEAR(TglTTS)='" + TglTTS.Year + "' ORDER BY NoTTS2 DESC");
                            string temp = terakhir.Substring(terakhir.Length - 7);
                            int temp2 = Convert.ToInt32(temp) + 1;
                            string no = temp2.ToString().PadLeft(7, '0');
                            NoTTS2 = "TTS/" + formatTahun + "/" + formatMonth + "/" + no;
                        }

                        if (isUniqueTTS(NoTTS2)) hasfound = true;
                    }
                    #endregion

                    Db.Execute("EXEC spTTSRegistrasi"
                        + " '" + Convert.ToDateTime(tgl.Text) + "'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'JUAL'"
                        + ",'" + nokontrak.Text + "'"
                        + ",'" + unit.Text + "'"
                        + ",'" + cs.Text + "'"
                        + ",'TR'"
                        + ",'VA: " + Cf.Str(va.Text) + " " + ket.Text + "'"
                        );

                    string[] x = Cf.SplitByString(Bank, ";");

                    int NoTTS = Db.SingleInteger("SELECT TOP 1 NoTTS FROM MS_TTS ORDER BY NoTTS DESC");

                    Db.Execute("UPDATE MS_TTS"
                          + " SET Acc = '" + x[0] + "'"
                          + ", SubID='" + x[1] + "'"
                          + " WHERE NoTTS = '" + NoTTS + "'"
                           );
                    for (int i = 0; i < rsTagihan.Rows.Count; i++)
                    {
                        TextBox lunas = (TextBox)ph.FindControl("lunas_" + j + "_" + i);
                        TextBox tag = (TextBox)ph.FindControl("tag_" + j + "_" + i);


                        if (lunas != null && tag != null && lunas.Text != "")
                        {
                            string[] arr = tag.Text.Split(';');
                            Db.Execute("EXEC spTTSAlokasi "
                                + "  '" + NoTTS + "'"
                                + ", " + arr[0]
                                + ", " + Convert.ToDecimal(lunas.Text)
                                );

                            KetTag = "<br>***ALOKASI PEMBAYARAN:<br>"
                                   + arr[1] + " (" + arr[2] + ")";
                        }

                    }
                    //==========================================
                    //Update NoBKM di MS_TTS
                    # region no bkm2
                    //NoBK
                    string formatMonth2 = Cf.Roman(TglBKM.Month);
                    string formatTahun2 = TglBKM.Year.ToString().Substring(2, 2);
                    string NoBKM2 = "";

                    bool hasfound2 = false;
                    while (!hasfound2)
                    {
                        if (!Response.IsClientConnected) break;

                        int num = Db.SingleInteger("SELECT COUNT(NoBKM2) FROM MS_TTS WHERE Status='POST' AND MONTH(TglBKM)='" + TglBKM.Month + "' AND YEAR(TglBKM)='" + TglBKM.Year + "'");
                        if (num == 0)
                        {
                            //BKM Pertama
                            int increment = num + 1;
                            string no = increment.ToString().PadLeft(7, '0');
                            NoBKM2 = "KW/" + formatTahun2 + "/" + formatMonth2 + "/" + no;

                        }
                        else
                        {
                            //NoBKM Terakhir
                            string terakhir = Db.SingleString("SELECT TOP 1 NoBKM2 FROM MS_TTS WHERE Status='POST' AND MONTH(TglBKM)='" + TglBKM.Month + "' AND YEAR(TglBKM)='" + TglBKM.Year + "' ORDER BY NoBKM2 DESC");
                            string temp = terakhir.Substring(terakhir.Length - 7);
                            int temp2 = Convert.ToInt32(temp) + 1;
                            string no = temp2.ToString().PadLeft(7, '0');
                            NoBKM2 = "KW/" + formatTahun2 + "/" + formatMonth2 + "/" + no;
                        }

                        if (isUniqueBKM(NoBKM2)) hasfound2 = true;
                    }

                    #endregion
                    //=======================================

                    Db.Execute("EXEC spPostingTTS " + NoTTS + ",'" + TglBKM + "'");
                    Db.Execute("UPDATE MS_TTS SET ManualBKM = ManualTTS, NoBKM2='" + NoBKM2 + "', TglFP = '" + TglBKM + "', NoTTS2='" + NoTTS2 + "' WHERE NoTTS = " + NoTTS);
                    Db.Execute("UPDATE ISC064_MARKETINGJUAL..MS_PELUNASAN SET NoBKM2='" + NoBKM2 + "' WHERE NoTTS = " + NoTTS);


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
                        + " FROM MS_TTS WHERE NoTTS = '" + NoTTS + "'");

                    string KetLog = Cf.LogCapture(rsLog)
                                   + KetTag;

                    Db.Execute("EXEC spLogTTS"
                        + " 'VA'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + KetLog + "'"
                        + ",'" + NoTTS + "'"
                        );

                    decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_TTS_LOG ORDER BY LogID DESC");
                    string Project = Db.SingleString("SELECT Project FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = (SELECT Ref FROM MS_TTS WHERE NoTTS = '" + NoTTS + "')");
                    Db.Execute("UPDATE MS_TTS_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                    //Result
                    TableRow r = new TableRow();
                    TableCell c;

                    c = new TableCell();
                    c.Text = va.Text;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Cf.Day(tgl.Text);
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = "<a href=\"javascript:call('" + NoTTS + "')\">"
                        + NoTTS2 + "</a>"
                        + "<br /><i>POST</i>"
                        + "<br />BKM: " + NoBKM2;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = "JUAL No. " + nokontrak.Text
                        + "<br />" + unit.Text
                        + "<br />" + cs.Text;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Cf.Num(Db.SingleDecimal("SELECT ISNULL(Total,0) FROM MS_TTS WHERE NoTTS=" + NoTTS + " AND Ref='" + nokontrak.Text + "'"));
                    c.HorizontalAlign = HorizontalAlign.Right;
                    r.Cells.Add(c);

                    Rpt.Border(r);
                    rpt.Rows.Add(r);

                }

                div1.Visible = false;
                div2.Visible = true;
                feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                    + "Import berhasil.."
                    ;


                Dfc.DeleteFile(FilePath);
            }

        }

        private bool isUniqueTTS(string kodebaru)
        {
            bool x = true;

            int c = Db.SingleInteger("SELECT COUNT(*) FROM MS_TTS WHERE NoTTS2 = '" + kodebaru + "'");
            if (c != 0)
                x = false;

            return x;
        }
        private bool isUniqueBKM(string kodebaru)
        {
            bool x = true;

            int c = Db.SingleInteger("SELECT COUNT(*) FROM MS_TTS WHERE NoBKM2 = '" + kodebaru + "'");
            if (c != 0)
                x = false;

            return x;
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
    }
}

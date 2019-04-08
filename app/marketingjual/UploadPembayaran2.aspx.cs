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

namespace ISC064.marketingjual
{
    public partial class UploadPembayaran2 : System.Web.UI.Page
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
                Init();
            }

            Js.Confirm(this, "Lanjutkan proses upload pembayaran?");
        }
        protected void Init()
        {
            if (File.Exists(FilePath))
            {
                //decimal Nilai = 0;

                string strSql = "SELECT * FROM [Pembayaran$]";
                DataTable rs = Db.xls(strSql, FilePath);

                int x = 1;
                for (int i = 0; i < rs.Rows.Count; i++)
                {
                    if (!Response.IsClientConnected) break;

                    string NoKontrak = Cf.Str(rs.Rows[i][0]);
                    string NoTTS = Cf.Str(rs.Rows[i][1]);
                    string NoTTSManual = Cf.Str(rs.Rows[i][2]);
                    DateTime TglTTS = Convert.ToDateTime(rs.Rows[i][3]);
                    string NoBKM = Cf.Str(rs.Rows[i][4]);
                    DateTime TglBKM = Convert.ToDateTime(rs.Rows[i][5]);
                    string CB = Cf.Str(rs.Rows[i][6]);
                    decimal NilaiPelunasan = Convert.ToDecimal(rs.Rows[i][7]);
                    string NamaTagihan = Cf.Str(rs.Rows[i][8]);
                    string Rekening = Cf.Str(rs.Rows[i][9]);

                    FillTb(NoKontrak, NoTTS, NoTTSManual, TglTTS, NoBKM, TglBKM, CB, NilaiPelunasan, NamaTagihan, Rekening, x);

                    x++;
                }

            }

        }
        protected void FillTb(string NoKontrak, string NoTTS, string NoTTSManual, DateTime TglTTS, string NoBKM, DateTime TglBKM, string CB, decimal NilaiPelunasan
            , string NamaTagihan, string Rekening, int n)
        {
            decimal x = NilaiPelunasan;

            Label l;
            TextBox t;

            l = new Label();
            string s = "";

            s = "<td align=center>" + n + "</td>"
                + "<td>" + NoKontrak + " " + Db.SingleInteger("Select count(*) from ms_customer a inner join ms_kontrak b on a.nocustomer = b.nocustomer where b.nokontrak = '" + NoKontrak + "'  ") + "</td>"
                + "<td>" + NoTTS + "</td>"
                + "<td>" + NoTTSManual + "</td>"
                + "<td>" + Cf.Day(TglTTS) + "</td>"
                + "<td>" + NoBKM + "</td>"
                + "<td>" + Cf.Day(TglBKM) + "</td>"
                + "<td align=right>" + CB + "</td>";

            l.Text = "<tr valign=top>"
                + s
                + "<td>" + NamaTagihan + "</td>"
                + "<td>" + Rekening + "</td>"
                + "<td>"
                ;
            ph.Controls.Add(l);

            t = new TextBox();
            t.ID = "lunas_" + index;
            t.Width = 100;
            t.Text = Cf.Num(x);
            t.CssClass = "txt_num";
            t.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            t.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            t.Attributes["onblur"] = "CalcBlur(this);";
            ph.Controls.Add(t);

            t = new TextBox();
            t.ID = "nokontrak_" + index;
            t.Visible = false;

            t.Text = NoKontrak;
            ph.Controls.Add(t);

            t = new TextBox();
            t.ID = "notts_" + index;
            t.Visible = false;
            t.Text = NoTTS;
            ph.Controls.Add(t);

            t = new TextBox();
            t.ID = "nottsmanual_" + index;
            t.Visible = false;
            t.Text = NoTTS;
            ph.Controls.Add(t);

            t = new TextBox();
            t.ID = "tgltts_" + index;
            t.Visible = false;
            t.Text = Cf.Day(TglTTS);
            ph.Controls.Add(t);

            t = new TextBox();
            t.ID = "nobkm_" + index;
            t.Visible = false;
            t.Text = NoBKM;
            ph.Controls.Add(t);

            t = new TextBox();
            t.ID = "tglbkm_" + index;
            t.Visible = false;
            t.Text = Cf.Day(TglBKM);
            ph.Controls.Add(t);

            t = new TextBox();
            t.ID = "nmtagihan_" + index;
            t.Visible = false;
            t.Text = NamaTagihan;
            ph.Controls.Add(t);

            t = new TextBox();
            t.ID = "cb_" + index;
            t.Visible = false;
            t.Text = CB;
            ph.Controls.Add(t);

            t = new TextBox();
            t.ID = "rek_" + index;
            t.Visible = false;
            t.Text = Rekening;
            ph.Controls.Add(t);


        }

        protected bool datavalid()
        {
            bool x = true;
            //string s = "";

            //bool adasatu = false;
            //for (int i = 0; i < rsTagihan.Rows.Count; i++)
            //{
            //    TextBox lunas = (TextBox)ph.FindControl("lunas_" + i);
            //    if (lunas.Text != "")
            //    {
            //        adasatu = true;
            //        try
            //        {
            //            decimal z = Convert.ToDecimal(lunas.Text);
            //        }
            //        catch
            //        {
            //            x = false;
            //            if (s == "") s = lunas.ID;
            //        }
            //    }
            //}

            //if (!adasatu) x = false;

            //if (!x)
            //    Js.Alert(
            //        this
            //        , "Input Not Valid.\\n\\n"
            //        + "Process Rule:\\n"
            //        + "1. Payment must be numeric and for at least one bill.\\n"
            //        , "document.getElementById('" + s + "').focus();"
            //        );

            return x;
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            //if (datavalid())
            //{
            string strSql = "SELECT * FROM MIGRATE_PEMBAYARAN";
            DataTable rs = Db.Rs(strSql);

            for (int i = 0; i < rs.Rows.Count; i++)
            {

                if (!Response.IsClientConnected) break;

                string NoKontrak = Cf.Str(rs.Rows[i]["NoKontrak"]);
                string NoTTS = Cf.Str(rs.Rows[i]["NoTTS"]);
                string NoTTSManual = Cf.Str(rs.Rows[i]["NoTTSManual"]);
                DateTime TglTTS = Convert.ToDateTime(rs.Rows[i]["TglTTS"]);
                string NoBKM = Cf.Str(rs.Rows[i]["NoBKM"]);
                DateTime TglBKM = Convert.ToDateTime(rs.Rows[i]["TglBKM"]);
                string CB = Cf.Str(rs.Rows[i]["CaraBayar"]);
                decimal NilaiPelunasan = (decimal)rs.Rows[i]["Nilai"];
                string NamaTagihan = Cf.Str(rs.Rows[i]["NamaTagihan"]);
                string Rekening = Cf.Str(rs.Rows[i]["Rekening"]);

                DataTable rskontrak = Db.Rs("SELECT * FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");

                string NoUnit = Db.SingleString("SELECT NoUnit from ms_kontrak where NoKontrak = '" + NoKontrak + "'");//rskontrak.Rows[0]["NoUnit"].ToString();
                string Customer = Db.SingleString("SELECT Nama FROM MS_CUSTOMER WHERE NoCustomer = '" + rskontrak.Rows[0]["NoCustomer"].ToString() + "'");
                string Ket = NoTTSManual; //edit

                string strSql1 = "SELECT * "
                            + ",(NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE NoTagihan = MS_TAGIHAN.NoUrut AND NoKontrak = '" + NoKontrak + "') ) AS SisaTagihan"
                            + " FROM MS_TAGIHAN AS MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "'"
                            + " AND (NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM MS_PELUNASAN WHERE NoTagihan = MS_TAGIHAN.NoUrut AND NoKontrak = '" + NoKontrak + "') ) > 0"
                            + " ORDER BY TglJT, NoUrut";
                DataTable rs1 = Db.Rs(strSql1);
                System.Text.StringBuilder alokasi = new System.Text.StringBuilder();

                Db.Execute("EXEC " + Mi.DbPrefix + "FINANCEAR..spTTSRegistrasiMigrate"
                    + " " + NoTTS
                    + ",'" + TglTTS + "'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'JUAL'"
                    + ",'" + NoKontrak + "'"
                    + ",'" + NoUnit + "'"
                    + ",'" + Customer.Replace("'", "") + "'"
                    + ",'" + CB + "'"
                    + ",'" + Ket + "'"
                    );
                string NoTTS2 = "TTS-" + TglTTS.ToString("yy") + TglTTS.ToString("MM") + NoTTS.ToString().PadLeft(7, '0');
                string NoBKM2 = "KWT-" + TglBKM.ToString("yy") + TglBKM.ToString("MM") + NoTTS.ToString().PadLeft(7, '0');
                Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_TTS SET NoTTS2 = '" + NoTTS2 + "', NoBKM2 = '" + NoBKM2 + "', tglbkm = '" + TglBKM + "' WHERE NoTTS = " + NoTTS);

                for (int j = 0; j < rs1.Rows.Count; j++)
                {
                    decimal sisatagihan = (decimal)rs1.Rows[j]["SisaTagihan"];
                    int nourut = (int)rs1.Rows[j]["NoUrut"];
                    string namatagihan = rs1.Rows[j]["NamaTagihan"].ToString();

                    if (j == rs1.Rows.Count - 1)
                    {
                        //last row
                        if (CB == "BG")
                        {
                            Db.Execute("EXEC ISC064_FINANCEAR..spTTSRegistrasiBG"
                                + " " + NoTTS
                                + ",''"
                                + ",'" + TglBKM + "'"
                                + ",''"
                                );
                        }

                        Db.Execute("EXEC " + Mi.DbPrefix + "FINANCEAR..spTTSAlokasi "
                                    + " " + NoTTS
                                    + ", " + nourut
                                    + ", " + NilaiPelunasan
                                    );

                        alokasi.Append(namatagihan + "    " + Cf.Num(NilaiPelunasan) + "<br>");

                        Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_TTS"
                            + " SET Acc = '" + Rekening + "'"
                            + ", SumberBayar = 0"
                            //+ ", Ket='" + NoTTSManual + "' "
                            //+ ", AdminBank='" + AdminBank + "' "
                            + ", Total2 = '" + NilaiPelunasan + "'"
                            //+ ", LebihBayar = '" + LebihBayar + "'"
                            + " WHERE NoTTS = '" + NoTTS + "'");

                        //BKM
                        if (NoBKM != "")
                        {
                            Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_TTS SET ManualBKM = ManualTTS, NoBKM = '" + NoTTS + "',NoBKMManual = '" + NoBKM + "' WHERE NoTTS = '" + NoTTS + "'");
                            Db.Execute("UPDATE MS_PELUNASAN SET NoBKM = '" + NoTTS + "' WHERE NoTTS = '" + NoTTS + "'");

                            //Db.Execute("EXEC " + Mi.DbPrefix + "FINANCEAR..spPostingTTS '" + NoTTS + "','" + TglBKM + "'");

                        }

                        DataTable rs2 = Db.Rs("SELECT "
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
                            + " FROM " + Mi.DbPrefix + "FINANCEAR.. MS_TTS WHERE NoTTS = '" + NoTTS + "'");

                        string KetLog = Cf.LogCapture(rs)
                            + "<br>***ALOKASI PEMBAYARAN:<br>"
                            + alokasi.ToString();

                        Db.Execute("EXEC " + Mi.DbPrefix + "FINANCEAR..spLogTTS"
                            + " 'REGIS'"
                            + ",'" + Act.UserID + "'"
                            + ",'" + Act.IP + "'"
                            + ",'" + KetLog + "'"
                            + ",'" + NoTTS + "'"
                            );

                        decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS_LOG ORDER BY LogID DESC");
                        string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = (SELECT Ref FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS WHERE NoTTS = '" + NoTTS + "')");
                        Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_TTS_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                        //Db.Execute("UPDATE ISC064_FINANCEAR..MS_TTS SET Status = 'POST' WHERE Ref = '" + NoKontrak + "' AND NoTTS = " + NoTTS);
                        Db.Execute("EXEC ISC064_MARKETINGJUAL..spProsentasePelunasan '" + NoKontrak + "'");
                        //lunas.Text = Cf.Num(NilaiPelunasan);
                    }
                    else
                    {
                        if (sisatagihan >= NilaiPelunasan)
                        {
                            //break, soalnya total udah abis
                            if (CB == "BG")
                            {
                                Db.Execute("EXEC ISC064_FINANCEAR..spTTSRegistrasiBG"
                                    + " " + NoTTS
                                    + ",''"
                                    + ",'" + TglBKM + "'"
                                    + ",''"
                                    );
                            }

                            Db.Execute("EXEC " + Mi.DbPrefix + "FINANCEAR..spTTSAlokasi "
                                        + " " + NoTTS
                                        + ", " + nourut
                                        + ", " + NilaiPelunasan
                                        );

                            alokasi.Append(namatagihan + "    " + Cf.Num(NilaiPelunasan) + "<br>");

                            Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_TTS"
                                + " SET Acc = '" + Rekening + "'"
                                + ", SumberBayar = 0"
                                //+ ", AdminBank='" + AdminBank + "' "
                                + ", Total2 = '" + NilaiPelunasan + "'"
                                //+ ", LebihBayar = '" + LebihBayar + "'"
                                + " WHERE NoTTS = '" + NoTTS + "'");

                            //BKM
                            if (NoBKM != "")
                            {
                                Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_TTS SET ManualBKM = ManualTTS, NoBKM = '" + NoTTS + "', NoBKMManual = '" + NoBKM + "' WHERE NoTTS = '" + NoTTS + "'");
                                Db.Execute("UPDATE MS_PELUNASAN SET NoBKM = '" + NoTTS + "' WHERE NoTTS = '" + NoTTS + "'");

                                //Db.Execute("EXEC " + Mi.DbPrefix + "FINANCEAR..spPostingTTS '" + NoTTS + "','" + TglBKM + "'");

                            }

                            DataTable rs2 = Db.Rs("SELECT "
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
                                + " FROM " + Mi.DbPrefix + "FINANCEAR.. MS_TTS WHERE NoTTS = '" + NoTTS + "'");

                            string KetLog = Cf.LogCapture(rs)
                                + "<br>***ALOKASI PEMBAYARAN:<br>"
                                + alokasi.ToString();

                            Db.Execute("EXEC " + Mi.DbPrefix + "FINANCEAR..spLogTTS"
                                + " 'REGIS'"
                                + ",'" + Act.UserID + "'"
                                + ",'" + Act.IP + "'"
                                + ",'" + KetLog + "'"
                                + ",'" + NoTTS + "'"
                                );

                            decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS_LOG ORDER BY LogID DESC");
                            string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = (SELECT Ref FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS WHERE NoTTS = '" + NoTTS + "')");
                            Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_TTS_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                            //Db.Execute("UPDATE ISC064_FINANCEAR..MS_TTS SET Status = 'POST' WHERE Ref = '" + NoKontrak + "' AND NoTTS = " + NoTTS);
                            Db.Execute("EXEC ISC064_MARKETINGJUAL..spProsentasePelunasan '" + NoKontrak + "'");
                            break;
                        }
                        else
                        {
                            if (CB == "BG")
                            {
                                Db.Execute("EXEC ISC064_FINANCEAR..spTTSRegistrasiBG"
                                    + " " + NoTTS
                                    + ",''"
                                    + ",'" + TglBKM + "'"
                                    + ",''"
                                    );
                            }

                            Db.Execute("EXEC " + Mi.DbPrefix + "FINANCEAR..spTTSAlokasi "
                                        + " " + NoTTS
                                        + ", " + nourut
                                        + ", " + sisatagihan
                                        );

                            alokasi.Append(namatagihan + "    " + Cf.Num(NilaiPelunasan) + "<br>");

                            Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_TTS"
                                + " SET Acc = '" + Rekening + "'"
                                + ", SumberBayar = 0"
                                //+ ", AdminBank='" + AdminBank + "' "
                                + ", Total2 = '" + sisatagihan + "'"
                                //+ ", LebihBayar = '" + LebihBayar + "'"
                                + " WHERE NoTTS = '" + NoTTS + "'");

                            //BKM
                            if (NoBKM != "")
                            {
                                Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_TTS SET ManualBKM = ManualTTS, NoBKM = '" + NoTTS + "' WHERE NoTTS = '" + NoTTS + "'");
                                Db.Execute("UPDATE MS_PELUNASAN SET NoBKM = '" + NoTTS + "' WHERE NoTTS = '" + NoTTS + "'");
                                // Db.Execute("EXEC " + Mi.DbPrefix + "FINANCEAR..spPostingTTS '" + NoTTS + "','" + TglBKM + "'");

                            }

                            DataTable rs2 = Db.Rs("SELECT "
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
                                + " FROM " + Mi.DbPrefix + "FINANCEAR.. MS_TTS WHERE NoTTS = '" + NoTTS + "'");

                            string KetLog = Cf.LogCapture(rs)
                                + "<br>***ALOKASI PEMBAYARAN:<br>"
                                + alokasi.ToString();

                            Db.Execute("EXEC " + Mi.DbPrefix + "FINANCEAR..spLogTTS"
                                + " 'REGIS'"
                                + ",'" + Act.UserID + "'"
                                + ",'" + Act.IP + "'"
                                + ",'" + KetLog + "'"
                                + ",'" + NoTTS + "'"
                                );

                            decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS_LOG ORDER BY LogID DESC");
                            string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = (SELECT Ref FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS WHERE NoTTS = '" + NoTTS + "')");
                            Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_TTS_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);



                            Db.Execute("EXEC ISC064_MARKETINGJUAL..spProsentasePelunasan '" + NoKontrak + "'");
                        }

                    }
                    NilaiPelunasan = NilaiPelunasan - sisatagihan;
                }
                Db.Execute("UPDATE ISC064_FINANCEAR..MS_TTS SET Status = 'POST' WHERE Ref = '" + NoKontrak + "' AND NoTTS = " + NoTTS);
                Db.Execute("UPDATE MS_PELUNASAN SET SudahCair = '1'");
            }

            div2.Visible = false;
            div1.Visible = false;
            feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                + "Upload Success.. " + rs.Rows.Count + " File"
                ;
            //}
        }
        protected string FilePath
        {
            get
            {
                return Request.QueryString["path"];
            }
        }

    }
}

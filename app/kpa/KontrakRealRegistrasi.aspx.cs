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

namespace ISC064.KPA
{
    public partial class KontrakRealRegistrasi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            fillAcc();
            Fill();

        }
        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                {
                    transaksi.Visible = false;

                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "<a href=\"javascript:popEditPengajuan('" + Request.QueryString["done"] + "')\">"
                        + "Registrasi Berhasil.."
                        + "</a>"
                        ;
                }
            }
        }
        private string Ref
        {
            get
            {
                return Cf.Pk(Request.QueryString["id"]);
            }
        }
        protected void Fill()
        {
            DataTable rs2 = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "FINANCEAR..MS_PENGAJUAN_KPA_DETIL WHERE NoPengajuan=" + Ref);

            for (int i = 0; i < rs2.Rows.Count; i++)
            {
                HtmlTableCell c;
                HtmlTableRow r = new HtmlTableRow();
                TextBox tb;
                Label l;

                c = new HtmlTableCell();
                l = new Label();
                l.ID = "nokontrak_" + i;
                l.Text = rs2.Rows[i]["NoKontrak"].ToString();
                c.Controls.Add(l);
                r.Cells.Add(c);

                c = new HtmlTableCell();
                l = new Label();
                l.ID = "notagihan_" + i;
                l.Text = rs2.Rows[i]["NoTagihan"].ToString();
                c.Controls.Add(l);
                r.Cells.Add(c);

                c = new HtmlTableCell();
                l = new Label();
                l.ID = "namatagihan_" + i;
                l.Text = rs2.Rows[i]["NamaTagihan"].ToString();
                c.Controls.Add(l);
                r.Cells.Add(c);

                c = new HtmlTableCell();
                l = new Label();
                l.ID = "nama_" + i;
                l.Text = rs2.Rows[i]["Nama"].ToString();
                c.Controls.Add(l);
                r.Cells.Add(c);

                c = new HtmlTableCell();
                l = new Label();
                l.ID = "nounit_" + i;
                l.Text = rs2.Rows[i]["NoUnit"].ToString();
                c.Controls.Add(l);
                r.Cells.Add(c);

                c = new HtmlTableCell();
                c.Align = "right";
                c.InnerText = Cf.Num(rs2.Rows[i]["Nilai"]);
                r.Cells.Add(c);

                c = new HtmlTableCell();
                tb = new TextBox();
                tb.ID = "lunas_" + i;
                tb.Width = 150;
                tb.CssClass = "txt_num";
                tb.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
                tb.Attributes["onkeyup"] = "CalcType(this,tempnum);";
                tb.Attributes["onblur"] = "CalcBlur(this);hitunggt();";
                c.Controls.Add(tb);
                r.Cells.Add(c);

                c = new HtmlTableCell();
                l = new Label();
                l.Text = "<input type='checkbox' onclick=\"tagihan('" + i + "','" + Cf.Num(rs2.Rows[i]["Nilai"]) + "',this)\"></td>";
                c.Controls.Add(l);
                r.Cells.Add(c);

                list.Controls.Add(r);

            }
        }
        private bool valid()
        {
            bool x = true;
            if (tgl.Text == "")
            {
                x = false;
                tglc.Text = "Kosong";
            }
            else if (!Cf.isTgl(tgl))
            {
                x = false;
                tglc.Text = "Format Tanggal";
            }
            else
            {
                tglc.Text = "";
            }

            if (total.Text == "" || total.Text == "0")
            {
                x = false;
                err.Text = "Belum ada pengajuan yang di realisasikan";
            }
            else
            {
                err.Text = "";
            }

            string s = "";
            if (ddlAcc.SelectedIndex == 0)
            {
                x = false;

                if (s == "")
                    s = ddlAcc.ID;

                ddlAccErr.Text = "Harus dipilih";
            }
            else
                ddlAccErr.Text = "";

            if (carabayar.SelectedValue == "")
            {
                x = false;
                Js.Alert(
                    this
                    , "Cara Bayar Tidak Valid.\\n"
                    + "Silakan pilih salah satu cara bayar yang tersedia."
                    , ""
                    );

            }
            if (carabayar.SelectedValue == "BG")
            {
                nobg.Text = Cf.Pk(nobg.Text);
                if (Cf.isEmpty(nobg))
                {
                    x = false;
                    if (s == "") s = nobg.ID;
                    nobgc.Text = "Kosong";
                }
                else
                    nobgc.Text = "";

                if (!Cf.isTgl(tglbg))
                {
                    x = false;
                    if (s == "") s = tglbg.ID;
                    tglbgc.Text = "Tanggal";
                }
                else
                    tglbgc.Text = "";
            }
            return x;
        }
        private void fillAcc()
        {
            string Project = Db.SingleString("SELECT c.Project FROM " + Mi.DbPrefix + "FINANCEAR..MS_PENGAJUAN_KPA a"
                          + " INNER JOIN " + Mi.DbPrefix + "FINANCEAR..MS_PENGAJUAN_KPA_DETIL b ON a.NoPengajuan = b.NoPengajuan"
                          + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK c ON c.NoKontrak = b.NoKontrak WHERE a.NoPengajuan = '" + Ref + "'");

            DataTable rs = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "FINANCEAR..REF_ACC WHERE Project = '" + Project + "'");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v2 = rs.Rows[i]["Acc"].ToString() + ";" + rs.Rows[i]["SubID"].ToString();
                string v = rs.Rows[i]["Acc"].ToString();
                string t = v + " : " + rs.Rows[i]["Bank"] + " " + rs.Rows[i]["Rekening"];
                ddlAcc.Items.Add(new ListItem(t, v2));
            }
        }
        protected void save_Click(object sender, EventArgs e)
        {
            if (valid())
            {

                Db.Execute("EXEC " + Mi.DbPrefix + "FINANCEAR..spRealisasiKPA"
                        + " '" + Convert.ToDateTime(tgl.Text) + "'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + ket.Text + "'"
                        + "," + Convert.ToDecimal(total.Text)
                        + "," + Ref
                    );

                //detil
                System.Text.StringBuilder alokasi = new System.Text.StringBuilder();
                int NoReal = Db.SingleInteger("SELECT MAX(NoReal) FROM " + Mi.DbPrefix + "FINANCEAR..MS_REAL_KPA");

                int index = 0;
                foreach (Control r in list.Controls)
                {
                    Label nokontrak = (Label)list.FindControl("nokontrak_" + index);
                    Label nama = (Label)list.FindControl("nama_" + index);
                    Label nounit = (Label)list.FindControl("nounit_" + index);
                    Label notagihan = (Label)list.FindControl("notagihan_" + index);
                    Label namatagihan = (Label)list.FindControl("namatagihan_" + index);
                    TextBox lunas = (TextBox)list.FindControl("lunas_" + index);

                    if (lunas.Text != "" && lunas.Text != "0")
                    {
                        //alokasi dan pelunasan kpa

                        alokasi.Append(nokontrak.Text + "." + notagihan.Text + "    " + namatagihan.Text + "    " + Cf.Num(Convert.ToDecimal(lunas.Text)) + "<br>");

                        SaveTTS(nokontrak.Text, nounit.Text, Convert.ToDecimal(lunas.Text), nama.Text, NoReal, notagihan.Text.ToString(), namatagihan.Text);
                        Db.Execute("UPDATE MS_TAGIHAN_KPA SET FlagPengajuanKPA=0 WHERE NoKontrak='" + nokontrak.Text + "' AND NoUrut='" + notagihan.Text.ToString() + "'");

                    }

                    index++;
                }


                Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_PENGAJUAN_KPA SET STATUS='POST' WHERE NoPengajuan=" + Ref);

                //log
                DataTable rs = Db.Rs("SELECT "
                    + " CONVERT(varchar, TglInput, 106) AS [Tanggal]"
                    + ",NoPengajuan"
                    + ",CONVERT(varchar, TglReal, 106) AS [Tanggal Realisasi]"
                    + ",Total"
                    + ",Ket"
                    + " FROM ISC064_FINANCEAR..MS_REAL_KPA WHERE NoReal = " + NoReal);

                string KetLog = Cf.LogCapture(rs)
                    + "<br>***REALISASI TAGIHAN KPA:<br>"
                    + alokasi.ToString();

                Db.Execute("EXEC ISC064_FINANCEAR..spLogRealKPA"
                    + " 'REGIS'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + KetLog + "'"
                    + ",'" + NoReal + "'"
                    );

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM " + Mi.DbPrefix + "FINANCEAR..MS_REAL_KPA_LOG ORDER BY LogID DESC");
                string Project = Db.SingleString("SELECT Project FROM " + Mi.DbPrefix + "FINANCEAR..MS_PENGAJUAN_KPA WHERE NoPengajuan = (SELECT NoPengajuan FROM " + Mi.DbPrefix + "FINANCEAR..MS_REAL_KPA WHERE NoReal = '" + NoReal + "')");
                Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_REAL_KPA_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                Response.Redirect("KontrakRealRegistrasi2.aspx?done=" + NoReal);


            }
        }

        protected void SaveTTS(string NoKontrak, string NoUnit, decimal lunas, string customer, int NoReal, string notag, string NamaTag)
        {
            DateTime TglTTS = Convert.ToDateTime(tgl.Text);
            string Unit = Cf.Str(NoUnit);
            string Customer = Cf.Str(customer);
            string CaraBayar = carabayar.SelectedValue;
            string Ket = NamaTag;

            # region NoTTS yg pake format
            //NoTTS
            string formatMonth = Cf.Roman(TglTTS.Month);
            string formatTahun = TglTTS.Year.ToString().Substring(2, 2);
            string NoTTS2 = "";

            bool hasfound = false;
            int num = Db.SingleInteger("SELECT COUNT(NoTTS2) FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS WHERE MONTH(TglTTS)='" + TglTTS.Month + "' AND YEAR(TglTTS)='" + TglTTS.Year + "'");
            while (!hasfound)
            {
                if (!Response.IsClientConnected) break;

                num++;
                string no = num.ToString().PadLeft(7, '0');
                NoTTS2 = "TTS/" + formatTahun + "/" + formatMonth + "/" + no;

                if (isUnique(NoTTS2))
                {
                    hasfound = true;
                }
            }
            #endregion


            Db.Execute("EXEC " + Mi.DbPrefix + "FINANCEAR..spTTSRegistrasi"
                + " '" + TglTTS + "'"
                + ",'" + Act.UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'JUAL'"
                + ",'" + NoKontrak + "'"
                + ",'" + Unit + "'"
                + ",'" + Customer + "'"
                + ",'" + CaraBayar + "'"
                + ",'" + Ket + "'"
                );
            string[] x = Cf.SplitByString(ddlAcc.SelectedValue, ";");

            int noTTS = Db.SingleInteger("SELECT TOP 1 NoTTS FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS ORDER BY NoTTS DESC");
            //Pelunasan di kpa
            Db.Execute("EXEC spPelunasanKPA"
                      + "'" + NoKontrak + "'"
                      + ", '" + notag + "'"
                      + ", " + lunas
                      + ", " + NoReal
                      + ", " + noTTS
                      + ", '" + carabayar.SelectedValue.ToString() + "'"
            );

            Db.Execute("UPDATE MS_TAGIHAN_KPA SET FlagPengajuanKPA=0 WHERE NoKontrak='" + NoKontrak + "' AND NoUrut='" + notag + "'");

            Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_TTS SET NoTTS2 = '" + NoTTS2 + "' WHERE NoTTS ='" + noTTS + "'");
            Db.Execute("UPDATE MS_PELUNASAN SET NoTTS2='" + NoTTS2 + "' WHERE NoTTS ='" + noTTS + "'");

            Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_TTS"
                + " SET Acc = '" + x[0] + "'"
                + ", SubID='" + x[1] + "'"
                + ", TTSKPA = 1"
                + " WHERE NoTTS = " + noTTS);

            if (carabayar.SelectedValue == "BG")
            {
                string NoBG = Cf.Pk(nobg.Text);
                DateTime TglBG = Convert.ToDateTime(tglbg.Text);

                Db.Execute("EXEC " + Mi.DbPrefix + "FINANCEAR..spTTSRegistrasiBG"
                    + " '" + noTTS + "'"
                    + ",'" + NoBG + "'"
                    + ",'" + TglBG + "'"
                    );

                Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_TTS SET "
                    + " BankBG= '" + Cf.Str(bankbg.Text) + "'"
                    + " WHERE NoTTS = '" + noTTS + "'"
                    );
            }

            if (carabayar.SelectedValue == "KK")
            {
                string NoKK = Cf.Pk(nokk.Text);
                string BankKK = Cf.Pk(bankkk.Text);

                Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_TTS SET "
                    + " NoKK = '" + NoKK + "'"
                    + ",BankKK = '" + BankKK + "'"
                    + " WHERE NoTTS = '" + noTTS + "'"
                    );
            }

            System.Text.StringBuilder alokasi = new System.Text.StringBuilder();

            DataTable rsTagihan = Db.Rs("SELECT TOP 1 * FROM MS_TAGIHAN WHERE KPR = 1 AND NoKontrak='" + NoKontrak + "'");
            string NamaTagihan = Cf.Str(rsTagihan.Rows[0]["NamaTagihan"])
                            + " (" + rsTagihan.Rows[0]["Tipe"] + ")";
            decimal Nilai = lunas;
            int NoTagihan = Convert.ToInt32(rsTagihan.Rows[0]["NoUrut"].ToString());

            Db.Execute("EXEC " + Mi.DbPrefix + "FINANCEAR..spTTSAlokasi "
                + "  " + noTTS
                + ", " + NoTagihan
                + ", " + Nilai
                );

            Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_TTS SET Total2 = '" + Nilai + "' WHERE NoTTS = '" + noTTS + "' ");

            alokasi.Append(NamaTagihan + "    " + Cf.Num(Nilai) + "<br>");

            DataTable rs = Db.Rs("SELECT "
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
                    + " FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS WHERE NoTTS = " + noTTS);

            string KetLog = Cf.LogCapture(rs)
                + "<br>***ALOKASI PEMBAYARAN:<br>"
                + alokasi.ToString();

            Db.Execute("EXEC " + Mi.DbPrefix + "FINANCEAR..spLogTTS"
                + " 'REGIS'"
                + ",'" + Act.UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'" + KetLog + "'"
                + ",'" + noTTS.ToString().PadLeft(7, '0') + "'"
                );

            decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS_LOG ORDER BY LogID DESC");
            string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = (SELECT Ref FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS WHERE NoTTS = '" + noTTS + "')");
            Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_TTS_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

            PostingTTS(noTTS, TglTTS);
        }
        protected void PostingTTS(int NoTTS, DateTime TglBKM)
        {
            DataTable rs = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS WHERE NoTTS = " + NoTTS + " AND Status = 'BARU' AND Acc <> '' AND Acc <> '0'");

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                DataTable rsHeader = Db.Rs("SELECT "
                    + " CONVERT(varchar, TglTTS, 106) AS [Tanggal]"
                    + ",Tipe"
                    + ",Ref AS [Ref.]"
                    + ",Unit"
                    + ",Customer"
                    + ",CaraBayar AS [Cara Bayar]"
                    + ",Ket AS [Keterangan]"
                    + ",NoSlip AS [Slip Setoran]"
                    + ",NoBG AS [No. BG]"
                    + ",CONVERT(varchar, TglBG, 106) AS [Tanggal BG]"
                    + ",Titip AS [Pengelola BG]"
                    + ",Total"
                    + ",NoFPS AS [No. Faktur Pajak]"
                    + " FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS WHERE NoTTS = " + NoTTS);

                string StatusLama = rs.Rows[0]["Status"].ToString();

                #region logfile
                string Tipe = Db.SingleString("SELECT Tipe FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS WHERE NoTTS = " + NoTTS);

                string strSql = "";
                if (Tipe != "TENANT")
                {
                    strSql = "SELECT "
                        + " CASE NoTagihan"
                        + "		WHEN 0 THEN 'UNALLOCATED    ' + CONVERT(varchar,NilaiPelunasan,1)"
                        + "		ELSE (SELECT NamaTagihan FROM MS_TAGIHAN WHERE NoUrut = l.NoTagihan AND NoKontrak = l.NoKontrak)"
                        + "          + '    ' + CONVERT(varchar,NilaiPelunasan,1)"
                        + " END AS NamaTagihan"
                        + " FROM MS_PELUNASAN AS l "
                        + " WHERE NoTTS = " + NoTTS;
                }
                else
                {
                    strSql = "SELECT "
                        + " NamaTagihan + '    ' + CONVERT(varchar,NilaiTagihan,1) "
                        + " FROM MS_TAGIHAN AS l "
                        + " WHERE NoTTS = " + NoTTS;
                }
                #endregion

                # region no bkm2
                //NoBK
                string formatMonth = Cf.Roman(TglBKM.Month);
                string formatTahun = TglBKM.Year.ToString().Substring(2, 2);
                string NoBKM2 = "";

                bool hasfound = false;
                int num = Db.SingleInteger("SELECT COUNT(NoBKM2) FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS WHERE Status='POST' AND MONTH(TglBKM)='" + TglBKM.Month + "' AND YEAR(TglBKM)='" + TglBKM.Year + "'");
                while (!hasfound)
                {
                    if (!Response.IsClientConnected) break;

                    num++;
                    string no = num.ToString().PadLeft(7, '0');
                    NoBKM2 = "KW/" + formatTahun + "/" + formatMonth + "/" + no;

                    if (isUniqueBKM(NoBKM2))
                    {
                        hasfound = true;
                    }
                }

                #endregion

                DataTable rsDetil = Db.Rs(strSql);

                Db.Execute("EXEC " + Mi.DbPrefix + "FINANCEAR..spPostingTTS " + NoTTS + ",'" + TglBKM + "'");
                Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_TTS SET ManualBKM = ManualTTS, NoBKM2='" + NoBKM2 + "', TglFP = '" + TglBKM + "' WHERE NoTTS = " + NoTTS);
                Db.Execute("UPDATE MS_PELUNASAN SET NoBKM2='" + NoBKM2 + "' WHERE NoTTS = " + NoTTS);


                string KetLog = Cf.LogCapture(rsHeader)
                    + Cf.LogList(rsDetil, "ALOKASI PELUNASAN")
                    ;

                Db.Execute("EXEC " + Mi.DbPrefix + "FINANCEAR..spLogTTS"
                    + " 'POST'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'***BUKA KWITANSI***<br>" + KetLog + "'"
                    + ",'" + NoTTS.ToString().PadLeft(7, '0') + "'"
                    );

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS_LOG ORDER BY LogID DESC");
                string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = (SELECT Ref FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS WHERE NoTTS = '" + NoTTS + "')");
                Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_TTS_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

            }
        }

        private bool isUnique(string kodebaru)
        {
            bool x = true;

            int c = Db.SingleInteger("SELECT COUNT(*) FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS WHERE NoTTS2 = '" + kodebaru + "'");
            if (c != 0)
                x = false;

            return x;
        }
        private bool isUniqueBKM(string kodebaru)
        {
            bool x = true;

            int c = Db.SingleInteger("SELECT COUNT(*) FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS WHERE NoBKM2 = '" + kodebaru + "'");
            if (c != 0)
                x = false;

            return x;
        }

    }
}

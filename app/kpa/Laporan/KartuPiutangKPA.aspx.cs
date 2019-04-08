using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Diagnostics;

namespace ISC064.KPA.Laporan
{
    public partial class KartuPiutangKPA : System.Web.UI.Page
{
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();

            param.Visible = false;
            report.Visible = true;
            int pdf = Convert.ToInt16(Request.QueryString["pdf"]);
            int excel = Convert.ToInt16(Request.QueryString["Excel"]);
            if (excel == 1)
            {
                Report();
                //Rpt.ToExcel(this, report);

                rp.Controls.Add(report);
                rp.Controls.Add(list);
                rp.Controls.Add(rpt);
                Rpt.ToExcel(this, rp);
            }
            else if (pdf == 1)
            {
                Report();
                Process p = new System.Diagnostics.Process();

                string Nama = "Laporan Kartu Piutang";
                string Link = "";
                DateTime TglGenerate = DateTime.Now;
                string FileName = "";
                string FileType = "application/pdf";
                string UserID = Act.UserID;
                string IP = Act.IP;

                Db.Execute("EXEC spLapPDFDaftar"

                        + " '" + Nama + "'"
                        + ",'" + Link + "'"
                        + ",'" + TglGenerate + "'"
                        + ",'" + IP + "'"
                        + ",'" + UserID + "'"
                        + ",'" + FileName + "'"
                        + ",'" + FileType + "'"
                        + ",'" + null + "'"
                        + ",'" + null + "'"
                        );

                //get nomor customer terbaru
                int NoAttachment = Db.SingleInteger(
                    "SELECT TOP 1 AttachmentID FROM LapPDF ORDER BY AttachmentID DESC");

                string strSql = "SELECT * FROM ISC064_FINANCEAR..LapPDF WHERE AttachmentID  = '" + NoAttachment + "'";
                DataTable rs = Db.Rs(strSql);

                string nfilename = "KartuPiutang" + NoAttachment + ".pdf";

                //update filename
                Db.Execute("UPDATE ISC064_FINANCEAR..LapPDF SET FileName= '" + nfilename + "' WHERE AttachmentID = " + NoAttachment);


                //folder untuk menyimpan file pdf
                string save = Mi.PathFilePDFReport + "KartuPiutang" + rs.Rows[0]["AttachmentID"] + ".pdf";

                string NoKontrak = Convert.ToString(Request.QueryString["NoKontrak"]);

                //link untuk download pdf
                //string link = Mi.PathAlamatWeb + "kpa/LaporanPDF/PDFLaporanKartuPiutang.aspx?NoKontrak=" + NoKontrak +""
                string link = Mi.PathAlamatWeb + "kpa/LaporanPDF/PDFLaporanKartuPiutang.aspx?id=" + rs.Rows[0]["AttachmentID"] + "";
                ;

                //update link
                Db.Execute("UPDATE ISC064_FINANCEAR..LapPDF SET Link= '" + link + "' WHERE AttachmentID = " + NoAttachment);

                //format page
                p.StartInfo.Arguments = "--orientation landscape --page-width 8.5in --page-height 11in --margin-left 0 --margin-right 0 --margin-top 0.25cm --margin-bottom 0 " + link + " " + save;

                //panggil aplikasi untuk mengconvert pdf
                p.StartInfo.FileName = Mi.PathWkhtmlPDFReport;
                p.Start();

                //60000 -> waktu jeda lama convert pdf
                p.WaitForExit(30000);

                string Src = Mi.PathFilePDFReport + nfilename;
                Mi.DownloadPDF(this, Src, (rs.Rows[0]["FileName"]).ToString(), rs.Rows[0]["FileType"].ToString());
            }
            else
            {
                Report();
            }

            if (!Page.IsPostBack)
            {
                Js.Focus(this, scr);
                if (!Act.Sec("DownloadExcel")) xls.Enabled = false;
            }
        }
        private bool valid()
        {
            string s = "";
            bool x = true;

            if (Cf.isEmpty(dari))
            {
                x = false;
                daric.Text = "Kosong";
                s = dari.ID;
            }
            else
            {
                daric.Text = "";
            }

            if (!x && s != "")
            {
                RegisterStartupScript("err"
                    , "<script type='text/javascript'>document.getElementById('" + s + "').select()</script>");
            }

            return x;
        }

        private void Report()
        {
            param.Visible = false;
            report.Visible = true;

            //lblHeader.Text = Mi.Pt
                //+ "<br />"
                //+ "KPA Report"
                //;

            System.Text.StringBuilder x = new System.Text.StringBuilder();
            x.Append("<br /><span style='font-weight: normal;'>This Report created on  : " + Cf.EnWeek(DateTime.Today)
                + ", " + Cf.Date(DateTime.Now)
                + " from workstation : " + Act.IP
                + " and username : " + Act.UserID
                + "</span>"
                );

            //lblSubHeader.Text = x.ToString();
            Fill();
        }
        protected int UserAgent()
        {
            int a = Db.SingleInteger("SELECT NoAgent FROM " + Mi.DbPrefix + "SECURITY..USERNAME WHERE UserID = '" + Act.UserID + "'");

            return a;
        }

        private void Fill()
        {
            string NoKontrak = Convert.ToString(Request.QueryString["NoKontrak"]);

            string aa = "";
            if (UserAgent() > 0)
                aa = " AND a.NoAgent = " + UserAgent();

            string strSql = "SELECT a.*, b.*, c.Nama AS NamaAgent, d.LuasSG"
                + " FROM ISC064_MARKETINGJUAL..MS_KONTRAK a"
                + " INNER JOIN ISC064_MARKETINGJUAL..MS_CUSTOMER b ON a.NoCustomer = b.NoCustomer"
                + " INNER JOIN ISC064_MARKETINGJUAL..MS_AGENT c ON a.NoAgent = c.NoAgent"
                + " INNER JOIN ISC064_MARKETINGJUAL..MS_UNIT d ON a.NoStock = d.NoStock"
                + " WHERE NoKontrak = '" + NoKontrak + "' "
                + aa
                ;
            DataTable rs = Db.Rs(strSql);

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected)
                {
                    break;
                }

                Label l;

                l = new Label();
                l.Text = "<table width='500px' class=''>";
                report.Controls.Add(l);

                //HEADER
                l = new Label();
                l.Text = "<tr><td colspan='8'>" + Mi.Pt + "<h1 class='title'>KARTU PIUTANG KPR<br /></h1></td></tr>";
                report.Controls.Add(l);

                //TYPE
                l = new Label();
                l.Text = "<tr><td width='80px'>TYPE</td><td>:</td><td>" + rs.Rows[i]["Jenis"].ToString() + "</td>";
                report.Controls.Add(l);

                //LUAS
                l = new Label();
                l.Text = "<td width='150px'>LUAS UNIT SG</td><td>:</td><td>" + Cf.Num(rs.Rows[i]["LuasSG"]) + " m<sup>2</sup></td></tr>";
                report.Controls.Add(l);

                //UNIT
                l = new Label();
                l.Text = "<tr><td style='color: red; font-weight: bold;'>NO UNIT</td><td style='color: red; font-weight: bold;'>:</td><td style='color: red; font-weight: bold;'>" + Cf.Pk(rs.Rows[i]["NoUnit"]) + "</td>";
                report.Controls.Add(l);

                //HARGA JUAL
                l = new Label();
                l.Text = "<td style='color: red; font-weight: bold;' width='150px'>NILAI KONTRAK</td><td style='color: red; font-weight: bold;'>:</td><td style='color: red; font-weight: bold;' align='right'>" + Cf.Num(rs.Rows[i]["NilaiKontrak"]) + "</td></tr>";
                report.Controls.Add(l);

                //NAMA
                l = new Label();
                l.Text = "<tr><td>NAMA</td><td>:</td><td>" + Cf.Str(rs.Rows[0]["Nama"]) + "</td>";
                report.Controls.Add(l);

                //TAGIHAN
                l = new Label();
                decimal Tagihan = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiTagihan),0) FROM ISC064_MARKETINGJUAL..MS_TAGIHAN_KPA WHERE NoKontrak = '" + Cf.Str(NoKontrak) + "' AND Tipe IN ('BF','DP','ANG')");
                l.Text = "<td>TAGIHAN</td><td>:</td><td align='right'>" + Cf.Num(Tagihan) + "</td>";
                report.Controls.Add(l);

                //ALAMAT
                l = new Label();
                l.Text = "<tr><td valign='top'>ALAMAT</td><td valign='top'>:</td><td>" + Cf.Str(rs.Rows[i]["KTP1"]) + " " + Cf.Str(rs.Rows[i]["KTP2"]) + "<br />" + Cf.Str(rs.Rows[i]["KTP3"]) + "</td>";
                report.Controls.Add(l);

                //BIAYA
                l = new Label();
                decimal Biaya = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiTagihan),0) FROM ISC064_MARKETINGJUAL..MS_TAGIHAN_KPA WHERE NoKontrak = '" + Cf.Str(NoKontrak) + "' AND Tipe IN ('ADM')");
                l.Text = "<td>BIAYA</td><td>:</td><td align='right'>" + Cf.Num(Biaya) + "</td></tr>";
                report.Controls.Add(l);

                //MARKETING
                l = new Label();
                l.Text = "<tr><td>MARKETING</td><td>:</td><td>" + Cf.Str(rs.Rows[i]["NamaAgent"]) + "</td>";
                report.Controls.Add(l);

                //TAGIHAN BIAYA
                l = new Label();
                decimal TagihanBiaya = Tagihan + Biaya;
                l.Text = "<td>TAGIHAN + BIAYA</td><td>:</td><td align='right'>" + Cf.Num(TagihanBiaya) + "</td></tr>";
                report.Controls.Add(l);

                l = new Label();
                l.Text = "<tr><td colspan='3'>";
                report.Controls.Add(l);

                //Cara Bayar
                l = new Label();
                l.Text = "<tr><td>CARA BAYAR</td><td>:</td><td>" + Cf.Str(rs.Rows[i]["CaraBayar"]) + "</td>";
                report.Controls.Add(l);

                //PEMBAYARAN
                l = new Label();
                decimal Pembayaran = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan),0) FROM ISC064_MARKETINGJUAL..MS_PELUNASAN WHERE NoKontrak = '" + Cf.Str(NoKontrak) + "' AND NoTagihan <> 0");
                l.Text = "<td>PEMBAYARAN</td><td>:</td><td align='right'>" + Cf.Num(Pembayaran) + "</td></tr>";
                report.Controls.Add(l);

                //DPP
                l = new Label();
                l.Text = "<tr><td>DPP</td><td>:</td><td>" + Cf.Num(rs.Rows[i]["NilaiDPP"]) + "</td>";
                report.Controls.Add(l);

                //PELUNASAN
                l = new Label();
                decimal Pelunasan = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan),0) FROM ISC064_MARKETINGJUAL..MS_PELUNASAN WHERE NoKontrak = '" + Cf.Str(NoKontrak) + "' AND NoTagihan <> 0 AND SudahCair = 1");
                decimal PersenLunas = Db.SingleDecimal("SELECT PersenLunas FROM ISC064_MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = '" + Cf.Str(NoKontrak) + "'");
                l.Text = "<td>PERSENTASE</td><td>:</td><td align='right'>" + Cf.Num(PersenLunas) + "%</td></tr>";
                report.Controls.Add(l);

                //PPN
                l = new Label();
                l.Text = "<tr><td>PPN</td><td>:</td><td>" + Cf.Num(rs.Rows[i]["NilaiPPN"]) + "</td>";
                report.Controls.Add(l);

                //Diskon
                l = new Label();
                l.Text = "<td>Diskon</td><td>:</td><td align='right'>" + Cf.Num(rs.Rows[i]["DiskonRupiah"]) + "</td></tr>";
                report.Controls.Add(l);

                //Status
                l = new Label();
                string statdb = Db.SingleString("SELECT status FROM ISC064_MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = '" + Cf.Str(NoKontrak) + "'");
                string status = "";
                if (statdb == "A")
                {
                    status = "Aktif";
                }
                else
                {
                    status = "Batal";
                }
                l.Text = "<tr><td>Status</td><td>:</td><td>" + status + "</td>";
                report.Controls.Add(l);

                l = new Label();
                l.Text = "</table><br />";
                report.Controls.Add(l);

                FillTagihan(Cf.Pk(rs.Rows[i]["NoKontrak"]));
                FillBottom(Cf.Pk(rs.Rows[i]["NoKontrak"]));
            }

        }

        protected void FillTagihan(string NoKontrak)
        {
            string strSql = "("
                + "SELECT *"
                + " FROM ISC064_MARKETINGJUAL..MS_TAGIHAN_KPA"
                + " WHERE NoKontrak = '" + Cf.Pk(NoKontrak) + "'"
                + " AND NilaiTagihan > 0"
                + ")"
                ;
            strSql += " UNION "
                + "("
                + "SELECT * FROM ISC064_MARKETINGJUAL..MS_TAGIHAN_KPA a"
                + " WHERE NoKontrak = '" + Cf.Pk(NoKontrak) + "'"
                + " AND NilaiTagihan < 0"
                + " AND (SELECT ISNULL(SUM(NilaiPelunasan), 0) FROM ISC064_MARKETINGJUAL..MS_PELUNASAN WHERE NoKontrak = '" + Cf.Pk(NoKontrak) + "'"
                + " AND NoTagihan = a.NoUrut) < 0"
                + ")"
                ;


            DataTable rs = Db.Rs(strSql);

            decimal nilaiPelunasan;



            decimal Total = Db.SingleDecimal("SELECT NilaiKontrak FROM ISC064_MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = '" + Cf.Pk(NoKontrak) + "'");
            decimal Sisa = Total;

            Label l;

            l = new Label();
            System.Text.StringBuilder x = new System.Text.StringBuilder();

            x.Append("<table cellspacing='1' style='border: 1px;' class='tb blue-skin'>");
            x.Append("<tr>");
            x.Append("<th rowspan='2' style='border-right: 1px ; border-bottom: 1px; background-color:#1E90FF; color:white'>NO.</th>");
            x.Append("<th rowspan='2' style='border-right: 1px ; border-bottom: 1px; background-color:#1E90FF; color:white'>KETERANGAN</th>");
            x.Append("<th colspan='2' style='border-right: 1px ; border-bottom: 1px; background-color:#1E90FF; color:white'>PIUTANG</th>");
            x.Append("<th colspan='3' style='border-right: 1px ; border-bottom: 1px; background-color:#1E90FF; color:white'>PEMBAYARAN</th>");
            x.Append("<th colspan='2' style='border-bottom: 1px ; background-color:#1E90FF; color:white'>DENDA</th>");
            x.Append("</tr>");
            x.Append("<tr>");
            x.Append("<th style='border-right: 1px ; border-bottom: 1px; background-color:#1E90FF; color:white'>TGL.JT</th>");
            x.Append("<th style='border-right: 1px ; border-bottom: 1px; background-color:#1E90FF; color:white'>NILAI</th>");
            x.Append("<th style='border-right: 1px ; border-bottom: 1px; background-color:#1E90FF; color:white'>TGL.BAYAR</th>");
            x.Append("<th style='border-right: 1px ; border-bottom: 1px; background-color:#1E90FF; color:white'>NO.KUITANSI</th>");
            x.Append("<th style='border-right: 1px ; border-bottom: 1px; background-color:#1E90FF; color:white'>NILAI</th>");
            x.Append("<th style='border-right: 1px ; border-bottom: 1px; background-color:#1E90FF; color:white'>HARI</th>");
            x.Append("<th style='border-bottom: 1px ; background-color:#1E90FF; color:white'>NILAI</th>");
            x.Append("</tr>");

            decimal t1 = 0, t2 = 0;
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected)
                    break;

                x.Append("<tr>");
                x.Append("<td align='center' style='border-right: 1px ;' nowrap='true'>" + (i + 1) + "</td>");
                x.Append("<td style='border-right: 1px ;' nowrap='true'>" + rs.Rows[i]["NamaTagihan"] + "</td>");
                x.Append("<td style='border-right: 1px ;' nowrap='true'>"
                    + ((Convert.ToDecimal(rs.Rows[i]["NilaiTagihan"]) > 0) ? Cf.Day(rs.Rows[i]["TglJT"]) : "&nbsp;")
                    + "</td>");
                x.Append("<td align='right' style='border-right: 1px ;' nowrap='true'>"
                    + ((Convert.ToDecimal(rs.Rows[i]["NilaiTagihan"]) > 0) ? Cf.Num(rs.Rows[i]["NilaiTagihan"]) : "&nbsp;")
                    + "</td>");
                x.Append("<td style='border-right: 1px ;' nowrap='true'>" + Lunas(NoKontrak, rs.Rows[i]["NoUrut"].ToString(), "b.TglBKM", "b.TglMEMO") + "</td>");
                x.Append("<td style='border-right: 1px ;' align='center' nowrap='true'>" + Lunas(NoKontrak, rs.Rows[i]["NoUrut"].ToString(), "b.ManualBKM", "a.NoMEMO") + "</td>");
                x.Append("<td align='right' style='border-right: 1px ;' nowrap='true'>" + Lunas(NoKontrak, rs.Rows[i]["NoUrut"].ToString(), "ISNULL(a.NilaiPelunasan,0)", "ISNULL(a.NilaiPelunasan,0)") + "</td>");
                x.Append("<td align='right' style='border-right: 1px ;' nowrap='true'>" + Denda(NoKontrak, rs.Rows[i]["NoUrut"].ToString(), "HARI") + "</td>");
                x.Append("<td nowrap='true'>" + Denda(NoKontrak, rs.Rows[i]["NoUrut"].ToString(), "NILAI") + "</td>");
                x.Append("</tr>");

                if (i == (rs.Rows.Count - 1))
                {
                    decimal JumlahKPR = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiTagihan), 0) FROM ISC064_MARKETINGJUAL..MS_TAGIHAN_KPA WHERE NoKontrak = '" + NoKontrak + "' AND KPR = 1");

                    decimal PelunasanKPR = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan), 0) FROM ISC064_MARKETINGJUAL..MS_PELUNASAN a"
                      + " INNER JOIN ISC064_MARKETINGJUAL..MS_TAGIHAN_KPA b ON a.NoTagihan = b.NoUrut AND a.NoKontrak = b.NoKontrak"
                      + " WHERE a.NoKontrak = '" + NoKontrak + "' AND (a.NoTTS <> '0' OR a.NoMemo <> '0') "
                      + " AND b.KPR = 1"
                      );

                    t1 = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiTagihan), 0) FROM ISC064_MARKETINGJUAL..MS_TAGIHAN_KPA"
                        + " WHERE NoKontrak = '" + NoKontrak + "'"
                        + " AND KPR = 0 AND NilaiTagihan > 0"
                        );

                    t2 = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan), 0) FROM ISC064_MARKETINGJUAL..MS_PELUNASAN a"
                      + " INNER JOIN ISC064_MARKETINGJUAL..MS_TAGIHAN_KPA b ON a.NoTagihan = b.NoUrut AND a.NoKontrak = b.NoKontrak"
                      + " WHERE a.NoKontrak = '" + NoKontrak + "' AND (a.NoTTS <> '0' OR a.NoMemo <> '0') "
                      + " AND b.KPR = 0"
                      );

                    x.Append("<tr>");
                    x.Append("<td style='border-right: 1px ;'>&nbsp;</td>");
                    x.Append("<td style='border-right: 1px ;'><b>JUMLAH PIUTANG UM</b></td>");
                    x.Append("<td style='border-right: 1px ;'>&nbsp;</td>");
                    x.Append("<td align='right' style='border-top: 1px ; border-right: 1px ;'><b>" + Cf.Num(t1) + "</b></td>");
                    x.Append("<td style='border-right: 1px ;' align='center'>&nbsp;</td>");
                    x.Append("<td style='border-right: 1px ;'>&nbsp;</td>");
                    x.Append("<td style='border-top: 1px ; border-right: 1px ;'>&nbsp;</td>");
                    x.Append("<td style='border-right: 1px ;'>&nbsp;</td>");
                    x.Append("<td>&nbsp;</td>");
                    x.Append("</tr>");


                    x.Append("<tr>");
                    x.Append("<td style='border-right: 1px ;'>&nbsp;</td>");
                    x.Append("<td style='border-right: 1px ;'><b>JUMLAH PEMBAYARAN UM</b></td>");
                    x.Append("<td style='border-right: 1px ;'>&nbsp;</td>");
                    x.Append("<td align='right' style='border-right: 1px ;'><b>" + Cf.Num(t2) + "</b></td>");
                    x.Append("<td style='border-right: 1px ;' align='center'>&nbsp;</td>");
                    x.Append("<td style='border-right: 1px ;'>&nbsp;</td>");
                    x.Append("<td style='border-top: 1px ; border-right: 1px ;'>&nbsp;</td>");
                    x.Append("<td style='border-right: 1px ;'>&nbsp;</td>");
                    x.Append("<td>&nbsp;</td>");
                    x.Append("</tr>");

                    decimal t = (t1 - t2);
                    if (t < 0)
                    {
                        t = 0;
                    }
                    x.Append("<tr>");
                    x.Append("<td style='border-right: 1px ;'>&nbsp;</td>");
                    x.Append("<td style='border-right: 1px ;'><b>SALDO PIUTANG UM</b></td>");
                    x.Append("<td style='border-right: 1px ;'>&nbsp;</td>");
                    x.Append("<td align='right' style='border-top: 1px ; border-right: 1px ;'><b>" + Cf.Num(Math.Round(t, 0)) + "</b></td>");
                    x.Append("<td style='border-right: 1px ;' align='center'>&nbsp;</td>");
                    x.Append("<td style='border-right: 1px ;'>&nbsp;</td>");
                    x.Append("<td style='border-right: 1px ;'>&nbsp;</td>");
                    x.Append("<td style='border-right: 1px ;'>&nbsp;</td>");
                    x.Append("<td>&nbsp;</td>");
                    x.Append("</tr>");


                    x.Append("<tr>");
                    x.Append("<td style='border-right: 1px ;'>&nbsp;</td>");
                    x.Append("<td style='border-right: 1px ;'><b>JUMLAH PIUTANG KPR</b></td>");
                    x.Append("<td style='border-right: 1px ;'>&nbsp;</td>");
                    x.Append("<td align='right' style='border-top: 1px ; border-right: 1px ;'><b>" + Cf.Num(JumlahKPR) + "</b></td>");
                    x.Append("<td style='border-right: 1px ;' align='center'>&nbsp;</td>");
                    x.Append("<td style='border-right: 1px ;'>&nbsp;</td>");
                    x.Append("<td style='border-top: 1px ; border-right: 1px ;'>&nbsp;</td>");
                    x.Append("<td style='border-right: 1px ;'>&nbsp;</td>");
                    x.Append("<td>&nbsp;</td>");
                    x.Append("</tr>");


                    x.Append("<tr>");
                    x.Append("<td style='border-right: 1px ;'>&nbsp;</td>");
                    x.Append("<td style='border-right: 1px ;'><b>JUMLAH PEMBAYARAN KPR</b></td>");
                    x.Append("<td style='border-right: 1px ;'>&nbsp;</td>");
                    x.Append("<td align='right' style='border-right: 1px ;'><b>" + Cf.Num(PelunasanKPR) + "</b></td>");
                    x.Append("<td style='border-right: 1px ;' align='center'>&nbsp;</td>");
                    x.Append("<td style='border-right: 1px ;'>&nbsp;</td>");
                    x.Append("<td style='border-top: 1px ; border-right: 1px ;'>&nbsp;</td>");
                    x.Append("<td style='border-right: 1px ;'>&nbsp;</td>");
                    x.Append("<td>&nbsp;</td>");
                    x.Append("</tr>");

                    decimal SisaKPR = JumlahKPR - PelunasanKPR;
                    if (SisaKPR < 0)
                    {
                        SisaKPR = 0;
                    }
                    x.Append("<tr>");
                    x.Append("<td style='border-right: 1px ;'>&nbsp;</td>");
                    x.Append("<td style='border-right: 1px ;'><b>SALDO PIUTANG KPR</b></td>");
                    x.Append("<td style='border-right: 1px ;'>&nbsp;</td>");
                    x.Append("<td align='right' style='border-top: 1px ; border-right: 1px ;'><b>" + Cf.Num(Math.Round(SisaKPR, 0)) + "</b></td>");
                    x.Append("<td style='border-right: 1px ;' align='center'>&nbsp;</td>");
                    x.Append("<td style='border-right: 1px  ;'>&nbsp;</td>");
                    x.Append("<td style='border-right: 1px  ;'>&nbsp;</td>");
                    x.Append("<td style='border-right: 1px ;'>&nbsp;</td>");
                    x.Append("<td>&nbsp;</td>");
                    x.Append("</tr>");

                    x.Append("<tr>");
                    x.Append("<td style='border-right: 1px ;'>&nbsp;</td>");
                    x.Append("<td style='border-right: 1px ;'><b>SALDO PIUTANG UM + KPR</b></td>");
                    x.Append("<td style='border-right: 1px  ;'>&nbsp;</td>");
                    x.Append("<td align='right' style='border-bottom: 1px ; border-top: 1px ; border-right: 1px ;'><b>" + Cf.Num(t + SisaKPR) + "</b></td>");
                    x.Append("<td style='border-right: 1px ;' align='center'>&nbsp;</td>");
                    x.Append("<td style='border-right: 1px ;'>&nbsp;</td>");
                    x.Append("<td style='border-top: 1px ; border-right: 1px ;'>&nbsp;</td>");
                    x.Append("<td style='border-right: 1px ;'>&nbsp;</td>");
                    x.Append("<td>&nbsp;</td>");
                    x.Append("</tr>");


                    x.Append("<tr>");
                    x.Append("<td style='border-right: 1px ;'>&nbsp;</td>");
                    x.Append("<td style='border-right: 1px ;'>&nbsp;</td>");
                    x.Append("<td style='border-right: 1px ;'>&nbsp;</td>");
                    x.Append("<td style='border-right: 1px ;'>&nbsp;</td>");
                    x.Append("<td style='border-right: 1px ;' align='center'>&nbsp;</td>");
                    x.Append("<td style='border-right: 1px ;'>&nbsp;</td>");
                    x.Append("<td style='border-right: 1px ;'>&nbsp;</td>");
                    x.Append("<td style='border-right: 1px ;'>&nbsp;</td>");
                    x.Append("<td>&nbsp;</td>");
                    x.Append("</tr>");
                }
            }

            x.Append("</table>");

            Label ll;

            ll = new Label();
            ll.Text = x.ToString();
            list.Controls.Add(ll);
        }


        protected void FillBottom(string NoKontrak)
        {
            DataTable rs = Db.Rs("SELECT *"
                + " FROM ISC064_MARKETINGJUAL..MS_KONTRAK"
                + " WHERE NoKontrak = '" + NoKontrak + "'"
                );

            System.Text.StringBuilder x = new System.Text.StringBuilder();

            x.Append("<br /><table class='ph'>");

            x.Append("<tr>");
            x.Append("<td style='font-size: 12pt;' colspan='7'><b><u>DATA STATUS</u></b></td>");
            //x.Append("<td style='font-size: 12pt;' colspan='3'><b><u>DATA HARGA JUAL</u></b></td>");
            x.Append("</tr>");

            x.Append("<tr>");
            x.Append("<td style='width: 50px;'>BERKAS</td>");
            x.Append("<td style='width: 10px;'>:</td>");
            string StatBer = "";
            if (!rs.Rows[0]["StatusBerkas"].Equals(System.DBNull.Value))
                if (rs.Rows[0]["StatusBerkas"].ToString().ToUpper() == "TRUE")
                    StatBer = "SUDAH";
                else
                    StatBer = "BELUM";
            //x.Append("<td style='width: 100px;'>" + (Convert.ToBoolean(rs.Rows[0]["StatusBerkas"].Equals(System.DBNull.Value)) ? "SUDAH" : "BELUM") + "</td>");
            x.Append("<td style='width: 100px;'>" + StatBer + "</td>");
            x.Append("<td style='width: 20px;'>&nbsp;</td>");
            x.Append("<td style='width: 30px;'>TGL.</td>");
            x.Append("<td style='width: 10px;'>:</td>");
            if (Cf.Day(rs.Rows[0]["TglSelesaiBerkas"]) == "")
            {
                DateTime berkas = Convert.ToDateTime(rs.Rows[0]["TglKontrak"]);
                DateTime berkas2 = berkas.AddDays(14);
                //x.Append("<td style='width: 200px;'>" + Cf.Day(berkas2) + "</td>");
            }
            else
                x.Append("<td style='width: 200px;'>" + Cf.Day(rs.Rows[0]["TglSelesaiBerkas"]) + "</td>");
            //x.Append("<td>HARGA JUAL AWAL</td><td>:</td><td>&nbsp;</td><td align='right'>" + Cf.Num(rs.Rows[0]["Gross"]) + "</td><td>&nbsp;</td>");
            x.Append("</tr>");

            x.Append("<tr>");
            x.Append("<td style='width: 50px;'>WAWANCARA</td>");
            x.Append("<td style='width: 10px;'>:</td>");
            x.Append("<td style='width: 100px;'>" + ((rs.Rows[0]["StatusWawancara"].ToString() == "") ? "BELUM" : rs.Rows[0]["StatusWawancara"].ToString()) + "</td>");
            x.Append("<td style='width: 20px;'>&nbsp;</td>");
            x.Append("<td style='width: 30px;'>TGL.</td>");
            x.Append("<td style='width: 10px;'>:</td>");
            if (Cf.Day(rs.Rows[0]["TglWawancara"]) == "")
            {
                DateTime wawancara = Convert.ToDateTime(rs.Rows[0]["TglKontrak"]);
                DateTime wawancara2 = wawancara.AddDays(21);
                //x.Append("<td style='width: 200px;'>" + Cf.Day(wawancara2) + "</td>");
            }
            else
                x.Append("<td style='width: 200px;'>" + Cf.Day(rs.Rows[0]["TglWawancara"]) + "</td>");
            //x.Append("<td>DISKON HARGA JUAL</td><td>:</td><td>(</td><td align='right' style='border-bottom: 1px solid Black;'>" + Cf.Num(rs.Rows[0]["DiskonRupiah"]) + "</td><td>)</td>");
            x.Append("</tr>");

            x.Append("<tr>");
            x.Append("<td style='width: 50px;'>RAKOMDIT</td>");
            x.Append("<td style='width: 10px;'>:</td>");
            x.Append("<td style='width: 100px;'>" + ((rs.Rows[0]["StatusOTS"].ToString() == "") ? "BELUM" : rs.Rows[0]["StatusOTS"].ToString()) + "</td>");
            x.Append("<td style='width: 20px;'>&nbsp;</td>");
            x.Append("<td style='width: 30px;'>TGL.</td>");
            x.Append("<td style='width: 10px;'>:</td>");
            if (Cf.Day(rs.Rows[0]["TglOTS"]) == "")
            {
                DateTime rakomdit = Convert.ToDateTime(rs.Rows[0]["TglKontrak"]);
                DateTime rakomdit2 = rakomdit.AddDays(28);
                //x.Append("<td style='width: 200px;'>" + Cf.Day(rakomdit2) + "</td>");
            }
            else
                x.Append("<td style='width: 200px;'>" + Cf.Day(rs.Rows[0]["TglOTS"]) + "</td>");
            decimal AfterPPN = Convert.ToDecimal(rs.Rows[0]["Gross"]) - Convert.ToDecimal(rs.Rows[0]["DiskonRupiah"]);
            //x.Append("<td colspan='2'>&nbsp;</td><td>&nbsp;</td><td align='right'>" + Cf.Num(AfterPPN) + "</td><td>&nbsp;</td>");
            x.Append("</tr>");

            x.Append("<tr>");
            x.Append("<td style='width: 50px;'>AKAD</td>");
            x.Append("<td style='width: 10px;'>:</td>");
            x.Append("<td style='width: 100px;'>" + ((rs.Rows[0]["StatusAkad"].ToString() == "") ? "BELUM" : rs.Rows[0]["StatusAkad"].ToString()) + "</td>");
            x.Append("<td style='width: 20px;'>&nbsp;</td>");
            x.Append("<td style='width: 30px;'>TGL.</td>");
            x.Append("<td style='width: 10px;'>:</td>");
            if (Cf.Day(rs.Rows[0]["TglAkad"]) == "")
            {
                DateTime akad = Convert.ToDateTime(rs.Rows[0]["TglKontrak"]);
                DateTime akad2 = akad.AddDays(42);
                //x.Append("<td style='width: 200px;'>" + Cf.Day(akad2) + "</td>");
            }
            else
                x.Append("<td style='width: 200px;'>" + Cf.Day(rs.Rows[0]["TglAkad"]) + "</td>");
            //x.Append("<td>PPN</td><td>:</td><td>&nbsp;</td><td align='right'>" + Cf.Num(rs.Rows[0]["PPnNominal"]) + "</td><td>&nbsp;</td>");
            x.Append("</tr>");

            x.Append("</table>");

            Label l;
            l = new Label();
            l.Text = x.ToString();// +"<div style='page-break-after: always;'></div>";
            list.Controls.Add(l);
        }

        protected string Lunas(string NoKontrak, string NoTagihan, string s, string t)
        {
            System.Text.StringBuilder x = new System.Text.StringBuilder();
            string strSql = "";
            int bayar = Db.SingleInteger("SELECT COUNT(*) FROM ISC064_MARKETINGJUAL..MS_PELUNASAN WHERE NoKontrak = '" + NoKontrak + "' AND NoTagihan = " + NoTagihan);
            if (bayar != 0)
            {
                int tts = Db.SingleInteger("SELECT NoTTS FROM ISC064_MARKETINGJUAL..MS_PELUNASAN WHERE NoKontrak = '" + NoKontrak + "' AND NoTagihan = " + NoTagihan);
                if (tts == 0)
                {

                    strSql = "SELECT " + t
                        + " FROM ISC064_MARKETINGJUAL..MS_PELUNASAN a"
                        + " INNER JOIN ISC064_FINANCEAR..MS_MEMO b ON a.NoMEMO = b.NoMEMO"
                        + " WHERE NoKontrak = '" + Cf.Pk(NoKontrak) + "'"
                        + " AND NoTagihan = " + NoTagihan
                        ;
                }
                else
                {
                    strSql = "SELECT " + s
                        + " FROM ISC064_MARKETINGJUAL..MS_PELUNASAN a"
                        + " INNER JOIN ISC064_FINANCEAR..MS_TTS b ON a.NoTTS = b.NoTTS"
                        + " WHERE NoKontrak = '" + Cf.Pk(NoKontrak) + "'"
                        + " AND NoTagihan = " + NoTagihan
                        + " AND b.Status <> 'VOID'"
                        ;
                }

                DataTable rs = Db.Rs(strSql);
                for (int i = 0; i < rs.Rows.Count; i++)
                {
                    if (rs.Rows[i][0].GetType().ToString() == "System.DateTime")
                        x.Append(Cf.Day(rs.Rows[i][0]));
                    else if (rs.Rows[i][0].GetType().ToString() == "System.Decimal")
                        x.Append(Cf.Num(Math.Round(Convert.ToDecimal(rs.Rows[i][0]), 0)));
                    else
                        x.Append(rs.Rows[i][0].ToString());

                    if (i < (rs.Rows.Count - 1))
                        x.Append("<br />");
                }
            }
            if (x.ToString() == "")
                return "&nbsp;";
            else
                return x.ToString();
        }

        protected string Denda(string NoKontrak, string NoTagihan, string s)
        {
            string x = "&nbsp;";


            if (s == "HARI")
            {
                DateTime TglJT = Db.SingleTime("SELECT TglJT FROM ISC064_MARKETINGJUAL..MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' AND NoUrut = " + NoTagihan);
                //cek di pelunasan
                int jml = Db.SingleInteger("SELECT count(*) FROM ISC064_MARKETINGJUAL..MS_PELUNASAN WHERE NoKontrak = '" + NoKontrak + "' AND NoTagihan = " + NoTagihan);
                TimeSpan ts = DateTime.Now.Subtract(TglJT);
                if (jml > 0)
                {
                    DateTime TglBayar = Db.SingleTime("SELECT TOP 1 TglPelunasan FROM ISC064_MARKETINGJUAL..MS_PELUNASAN WHERE NoKontrak = '" + NoKontrak + "' AND NoTagihan = " + NoTagihan);
                    ts = TglBayar.Subtract(TglJT);
                }
                else
                    ts = DateTime.Now.Subtract(TglJT);

                if (ts.Days > 0)
                    x = ts.Days.ToString();
            }
            else if (s == "NILAI")
            {
                DateTime TglJT = Db.SingleTime("SELECT TglJT FROM ISC064_MARKETINGJUAL..MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' AND NoUrut = " + NoTagihan);
                decimal Denda = Db.SingleDecimal("SELECT (Denda) as Denda FROM ISC064_MARKETINGJUAL..MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' AND NoUrut = " + NoTagihan);
                //cek di pelunasan
                int jml = Db.SingleInteger("SELECT count(*) FROM ISC064_MARKETINGJUAL..MS_PELUNASAN WHERE NoKontrak = '" + NoKontrak + "' AND NoUrut = " + NoTagihan);
                TimeSpan ts = DateTime.Now.Subtract(TglJT);
                if (jml > 0)
                {
                    DateTime TglBayar = Db.SingleTime("SELECT TglPelunasan FROM ISC064_MARKETINGJUAL..MS_PELUNASAN WHERE NoKontrak = '" + NoKontrak + "' AND NoUrut = " + NoTagihan);
                    ts = TglBayar.Subtract(TglJT);

                    if (ts.Days == 0) Denda = 0;
                }
                if (Denda > 0)
                    x = Cf.Num(Math.Round(Denda, 0));
            }
            return x;
        }

        private decimal NilaiPelunasan(string NoKontrak, string NoTagihan)
        {
            string strSql = "SELECT ISNULL(SUM(NilaiPelunasan), 0) AS NilaiPelunasan"
                + " FROM ISC064_MARKETINGJUAL..MS_PELUNASAN"
                + " WHERE NoKontrak = '" + Cf.Pk(NoKontrak) + "'"
                + " AND NoTagihan = " + NoTagihan
                ;
            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count > 0)
            {
                return Convert.ToDecimal(rs.Rows[0]["NilaiPelunasan"]);
            }
            else
            {
                return (decimal)0.00;
            }
        }

        private string CaraBayar(string NoKontrak, string NoTagihan)
        {
            string SudahCair = "";
            string strSql = "SELECT CaraBayar, Ket, SudahCair"
                + " FROM ISC064_MARKETINGJUAL..MS_PELUNASAN"
                + " WHERE NoKontrak = '" + Cf.Pk(NoKontrak) + "'"
                + " AND NoTagihan = " + NoTagihan
                ;
            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count > 1)
            {
                System.Text.StringBuilder x = new System.Text.StringBuilder();

                for (int i = 0; i < rs.Rows.Count; i++)
                {
                    if (rs.Rows[i]["SudahCair"].ToString().ToUpper() == "TRUE")
                    {
                        SudahCair = "CAIR";
                    }
                    else
                    {
                        SudahCair = "BELUM CAIR";
                    }

                    x.Append(Cf.Str(rs.Rows[i]["CaraBayar"]) + " " + Cf.Str(rs.Rows[i]["Ket"]) + " (" + SudahCair + ")<br />");
                }

                return x.ToString();
            }
            else if (rs.Rows.Count == 1)
            {
                if (rs.Rows[0]["SudahCair"].ToString().ToUpper() == "TRUE")
                {
                    SudahCair = "CAIR";
                }
                else
                {
                    SudahCair = "BELUM CAIR";
                }

                return Cf.Str(rs.Rows[0]["CaraBayar"]) + " " + Cf.Str(rs.Rows[0]["Ket"]) + " (" + SudahCair + ")";
            }
            else
            {
                return "";
            }
        }

        protected void scr_Click(object sender, EventArgs e)
        {
            if (valid())
            {
                Report();
            }
        }
        protected void xls_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                //Report();
                //Rpt.ToExcel(this, rpt);
                Report();
                rp.Controls.Add(report);
                rp.Controls.Add(list);
                rp.Controls.Add(rpt);
                Rpt.ToExcel(this, rp);
            }
        }
    }
}
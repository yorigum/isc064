using System;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using System.Drawing;
using System.Diagnostics;

namespace ISC064.FINANCEAR.Laporan
{
    public partial class LaporanDetilPembayaran : System.Web.UI.Page
    {
        protected System.Web.UI.WebControls.CheckBoxList tipe;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();

            if (!Page.IsPostBack)
            {
                Act.ProjectList(project);
                Act.PersList(pers);
                comp.InnerHtml = Mi.Pt;
                rpt.Visible = false;
                Js.Focus(this, scr);
                init();
                if (!Act.Sec("DownloadExcel")) xls.Enabled = false;
            }
        }

        protected int UserAgent()
        {
            int a = Db.SingleInteger("SELECT NoAgent FROM " + Mi.DbPrefix + "SECURITY..USERNAME WHERE UserID = '" + Act.UserID + "'");

            return a;
        }

        private void init()
        {
            dari.Text = Cf.Day(DateTime.Today);
            sampai.Text = Cf.Day(DateTime.Today);


            string strSql1 = "Project IN (" + Act.ProjectListSql + ")";
            if (project.SelectedValue != "SEMUA")
                strSql1 = " Project = '" + project.SelectedValue + "' ";

            DataTable rs1 = Db.Rs("SELECT DISTINCT Lokasi FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT WHERE " + strSql1 + " ORDER BY LOKASI ASC ");
            for (int i = 0; i < rs1.Rows.Count; i++)
            {
                lokasi.Items.Add(new ListItem(rs1.Rows[i]["Lokasi"].ToString()));
            }
            lokasi.SelectedIndex = 0;

        }

        private bool valid()
        {
            string s = "";
            bool x = true;

            if (!Cf.isTgl(dari))
            {
                x = false;
                if (s == "") s = dari.ID;
                daric.Text = "Tanggal";
            }
            else
                daric.Text = "";

            if (!Cf.isTgl(sampai))
            {
                x = false;
                if (s == "") s = sampai.ID;
                sampaic.Text = "Tanggal";
            }
            else
                sampaic.Text = "";

            if (!x && s != "")
            {
                RegisterStartupScript("err"
                    , "<script type='text/javascript'>document.getElementById('" + s + "').select()</script>");
            }

            return x;
        }

        protected void scr_Click(object sender, System.EventArgs e)
        {
            if (valid())
                Report();
        }
        protected void xls_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                Report();
                Rpt.ToExcel(this, headReport, rpt);
            }
        }

        protected void pdf_Click(object sender, System.EventArgs e)
        {

            Process p = new System.Diagnostics.Process();

            string Nama = "Laporan Detil Pembayaran";
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
                    + ",'" + Convert.ToDateTime(dari.Text) + "'"
                    + ",'" + Convert.ToDateTime(sampai.Text) + "'"
                    );

            //get nomor customer terbaru
            int NoAttachment = Db.SingleInteger(
                "SELECT TOP 1 AttachmentID FROM LapPDF ORDER BY AttachmentID DESC");

            string strSql = "SELECT * FROM ISC064_FINANCEAR..LapPDF WHERE AttachmentID  = '" + NoAttachment + "'";
            DataTable rs = Db.Rs(strSql);

            string nfilename = "DetilPembayaran" + NoAttachment + ".pdf";

            //update filename
            Db.Execute("UPDATE ISC064_FINANCEAR..LapPDF SET FileName= '" + nfilename + "' WHERE AttachmentID = " + NoAttachment);

            //folder untuk menyimpan file pdf
            string save = Mi.PathFilePDFReport + "DetilPembayaran" + rs.Rows[0]["AttachmentID"] + ".pdf";

            string Project = "";
            if (project.SelectedIndex == 0)
                Project = Act.ProjectListSql.Replace("'", "");
            else
                Project = project.SelectedValue;

            //link untuk download pdf
            string link = Mi.PathAlamatWeb + "financear/LaporanPDF/PDFLaporanDetilPembayaran.aspx?id=" + rs.Rows[0]["AttachmentID"]
                + "&userid=" + UserID
                + "&pers=" + pers.SelectedValue
                + "&project=" + Project
                + "&lokasi=" + lokasi.SelectedValue
                ;

            //update link
            Db.Execute("UPDATE ISC064_FINANCEAR..LapPDF SET Link= '" + link + "' WHERE AttachmentID = " + NoAttachment);

            //format page
            p.StartInfo.Arguments = "--orientation landscape --page-width 8.5in --page-height 20in --margin-left 0 --margin-right 0 --margin-top 0.25cm --margin-bottom 0 " + link + " " + save;

            //panggil aplikasi untuk mengconvert pdf
            p.StartInfo.FileName = Mi.PathWkhtmlPDFReport;
            p.Start();

            //60000 -> waktu jeda lama convert pdf
            p.WaitForExit(30000);

            string Src = Mi.PathFilePDFReport + nfilename;
            Mi.DownloadPDF(this, Src, (rs.Rows[0]["FileName"]).ToString(), rs.Rows[0]["FileType"].ToString());
        }
        private void Report()
        {
            param.Visible = false;
            rpt.Visible = true;

            Header();
            Fill();
        }

        private void Header()
        {
            System.Text.StringBuilder x = new System.Text.StringBuilder();

            Rpt.Judul(x, comp, judul);

            string tgl = "";
            if (tglinput.Checked) tgl = tglinput.Text;

            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);
            Rpt.SubJudul(x
                , tgl + " : " + Cf.Day(dari.Text) + " s/d " + Cf.Day(sampai.Text)
                );
            Rpt.SubJudul(x
                , "Lokasi : " + lokasi.SelectedItem.Text
                );
            Rpt.SubJudul(x
                , "Project : " + project.SelectedValue
                );
            Rpt.SubJudul(x
                , "Perusahaan : " + pers.SelectedItem.Text
                );

            string legend = "";
            //Rpt.Header(rpt, x);
            Rpt.HeaderReport(headReport, legend, x);
        }

        private void Fill()
        {
            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);
            if (Dari > Sampai)
            {
                DateTime x = Sampai;
                Sampai = Dari;
                Dari = x;
            }

            string Project = " AND a.Project IN (" + Act.ProjectListSql + ")";
            if (project.SelectedValue != "SEMUA") Project = " AND a.Project = '" + project.SelectedValue + "'";
            string Perusahaan = "";
            if (pers.SelectedValue != "SEMUA") Perusahaan = " AND a.Pers = '" + pers.SelectedValue + "'";
            string Lokasi = "";
            if (lokasi.SelectedValue != "SEMUA") Lokasi = " AND a.Lokasi = '" + lokasi.SelectedValue + "'";


            decimal t1 = 0;
            decimal t2 = 0;
            decimal t3 = 0;
            decimal t4 = 0;
            decimal t5 = 0;
            decimal t6 = 0;
            decimal t7 = 0;
            decimal t8 = 0;
            decimal t9 = 0;
            decimal t10 = 0;

            string tgl = "";
            string tgl2 = "";
            if (tglinput.Checked)
            {
                tgl = "a.TglTTS";
                tgl2 = "a.TglMemo";
            }

            string agent = "";
            if (UserAgent() > 0)
                agent = " AND a.NoAgent = " + UserAgent();

            //string strSql = "SELECT a.*, b.Nama AS Cust"
            //    + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK a"
            //    + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER b ON a.NoCustomer = b.NoCustomer"
            //    + " WHERE 1=1 "
            //    + Project
            //    + Perusahaan
            //    + Lokasi
            //    + agent
            //    + " ORDER BY NoKontrak";
            string strSql = "SELECT DISTINCT(b.NoKontrak),b.NoUnit,b.NilaiKontrak,c.Nama AS Cust FROM ms_tts a join " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK b on a.Ref = b.NoKontrak"
                + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER c ON b.NoCustomer = c.NoCustomer"
                + " WHERE 1=1 "
                + " AND CONVERT(VARCHAR, " + tgl + ", 112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND CONVERT(VARCHAR, " + tgl + ", 112) <= '" + Cf.Tgl112(Sampai) + "'"
                + Project
                + Perusahaan
                + Lokasi
                + agent
                + " ORDER BY NoKontrak";
            
            DataTable rs = Db.Rs(strSql);

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                r.VerticalAlign = VerticalAlign.Top;
                r.Attributes["ondblclick"] = "popEditKontrak('" + rs.Rows[i]["NoKontrak"] + "')";

                c = new TableCell();
                c.Text = (i + 1).ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoKontrak"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoUnit"].ToString();
                c.Wrap = false;
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Cust"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                decimal BF = Db.SingleDecimal("SELECT ISNULL(SUM(b.NilaiPelunasan),0) FROM MS_TTS a"
                    + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN b ON a.NoTTS = b.NoTTS"
                    + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN c ON b.NoTagihan = c.NoUrut AND b.NoKontrak = c.NoKontrak"
                    + " WHERE b.NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "'"
                    + " AND CONVERT(VARCHAR, " + tgl + ", 112) >= '" + Cf.Tgl112(Dari) + "'"
                    + " AND CONVERT(VARCHAR, " + tgl + ", 112) <= '" + Cf.Tgl112(Sampai) + "'"
                    + " AND c.Tipe = 'BF'");
                string BFAcc = Db.SingleString("SELECT TOP 1 a.Acc FROM MS_TTS a"
                    + " INNER JOIN ISC064_MARKETINGJUAL..MS_PELUNASAN b ON a.NoTTS = b.NoTTS"
                    + " INNER JOIN ISC064_MARKETINGJUAL..MS_TAGIHAN c ON b.NoTagihan = c.NoUrut AND b.NoKontrak = c.NoKontrak"
                    + " WHERE b.NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "'"
                    + " AND c.Tipe = 'BF'"
                    + " AND CONVERT(VARCHAR, " + tgl + ", 112) >= '" + Cf.Tgl112(Dari) + "'"
                    + " AND CONVERT(VARCHAR, " + tgl + ", 112) <= '" + Cf.Tgl112(Sampai) + "'"
                    + " ORDER BY a.TglBKM DESC");
                string BFNamaBank = Db.SingleString("SELECT Bank FROM REF_ACC WHERE Acc = '" + BFAcc + "'");

                c = new TableCell();
                c.Text = Cf.Num(BF);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = BFNamaBank;
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                decimal DP = Db.SingleDecimal("SELECT ISNULL(SUM(b.NilaiPelunasan),0) FROM MS_TTS a"
                    + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN b ON a.NoTTS = b.NoTTS"
                    + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN c ON b.NoTagihan = c.NoUrut AND b.NoKontrak = c.NoKontrak"
                    + " WHERE b.NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "'"
                    + " AND CONVERT(VARCHAR, " + tgl + ", 112) >= '" + Cf.Tgl112(Dari) + "'"
                    + " AND CONVERT(VARCHAR, " + tgl + ", 112) <= '" + Cf.Tgl112(Sampai) + "'"
                    + " AND c.Tipe = 'DP'");
                string DPAcc = Db.SingleString("SELECT TOP 1 a.Acc FROM MS_TTS a"
                    + " INNER JOIN ISC064_MARKETINGJUAL..MS_PELUNASAN b ON a.NoTTS = b.NoTTS"
                    + " INNER JOIN ISC064_MARKETINGJUAL..MS_TAGIHAN c ON b.NoTagihan = c.NoUrut AND b.NoKontrak = c.NoKontrak"
                    + " WHERE b.NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "'"
                    + " AND c.Tipe = 'DP'"
                    + " AND CONVERT(VARCHAR, " + tgl + ", 112) >= '" + Cf.Tgl112(Dari) + "'"
                    + " AND CONVERT(VARCHAR, " + tgl + ", 112) <= '" + Cf.Tgl112(Sampai) + "'"
                    + " ORDER BY a.TglBKM DESC");
                string DPNamaBank = Db.SingleString("SELECT Bank FROM REF_ACC WHERE Acc = '" + DPAcc + "'");

                c = new TableCell();
                c.Text = Cf.Num(DP);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = DPNamaBank;
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                decimal ANG = Db.SingleDecimal("SELECT ISNULL(SUM(b.NilaiPelunasan),0) FROM MS_TTS a"
                    + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN b ON a.NoTTS = b.NoTTS"
                    + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN c ON b.NoTagihan = c.NoUrut AND b.NoKontrak = c.NoKontrak"
                    + " WHERE b.NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "'"
                    + " AND CONVERT(VARCHAR, " + tgl + ", 112) >= '" + Cf.Tgl112(Dari) + "'"
                    + " AND CONVERT(VARCHAR, " + tgl + ", 112) <= '" + Cf.Tgl112(Sampai) + "'"
                    + " AND c.Tipe = 'ANG'");
                string ANGAcc = Db.SingleString("SELECT TOP 1 a.Acc FROM MS_TTS a"
                    + " INNER JOIN ISC064_MARKETINGJUAL..MS_PELUNASAN b ON a.NoTTS = b.NoTTS"
                    + " INNER JOIN ISC064_MARKETINGJUAL..MS_TAGIHAN c ON b.NoTagihan = c.NoUrut AND b.NoKontrak = c.NoKontrak"
                    + " WHERE b.NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "'"
                    + " AND c.Tipe = 'ANG'"
                    + " AND CONVERT(VARCHAR, " + tgl + ", 112) >= '" + Cf.Tgl112(Dari) + "'"
                    + " AND CONVERT(VARCHAR, " + tgl + ", 112) <= '" + Cf.Tgl112(Sampai) + "'"
                    + " ORDER BY a.TglBKM DESC");
                string ANGNamaBank = Db.SingleString("SELECT Bank FROM REF_ACC WHERE Acc = '" + ANGAcc + "'");


                c = new TableCell();
                c.Text = Cf.Num(ANG);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = ANGNamaBank;
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                decimal ADM = Db.SingleDecimal("SELECT ISNULL(SUM(b.NilaiPelunasan),0) FROM MS_TTS a"
                    + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN b ON a.NoTTS = b.NoTTS"
                    + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN c ON b.NoTagihan = c.NoUrut AND b.NoKontrak = c.NoKontrak"
                    + " WHERE b.NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "'"
                    + " AND CONVERT(VARCHAR, " + tgl + ", 112) >= '" + Cf.Tgl112(Dari) + "'"
                    + " AND CONVERT(VARCHAR, " + tgl + ", 112) <= '" + Cf.Tgl112(Sampai) + "'"
                    + " AND c.Tipe = 'ADM'");
                string ADMAcc = Db.SingleString("SELECT TOP 1 a.Acc FROM MS_TTS a"
                    + " INNER JOIN ISC064_MARKETINGJUAL..MS_PELUNASAN b ON a.NoTTS = b.NoTTS"
                    + " INNER JOIN ISC064_MARKETINGJUAL..MS_TAGIHAN c ON b.NoTagihan = c.NoUrut AND b.NoKontrak = c.NoKontrak"
                    + " WHERE b.NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "'"
                    + " AND c.Tipe = 'ADM' "
                    + " AND CONVERT(VARCHAR, " + tgl + ", 112) >= '" + Cf.Tgl112(Dari) + "'"
                    + " AND CONVERT(VARCHAR, " + tgl + ", 112) <= '" + Cf.Tgl112(Sampai) + "'"
                    + " ORDER BY a.TglBKM DESC");
                string ADMNamaBank = Db.SingleString("SELECT Bank FROM REF_ACC WHERE Acc = '" + ADMAcc + "'");


                c = new TableCell();
                c.Text = Cf.Num(ADM);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = ADMNamaBank;
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                decimal SaldoAwal = Db.SingleDecimal("SELECT ISNULL(SUM(b.NilaiPelunasan),0) FROM MS_MEMO a"
                   + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN b ON a.NoMEMO = b.NoMEMO"
                   + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN c ON b.NoTagihan = c.NoUrut AND b.NoKontrak = c.NoKontrak"
                   + " WHERE b.NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "'"
                   + " AND CONVERT(VARCHAR, " + tgl2 + ", 112) >= '" + Cf.Tgl112(Dari) + "'"
                   + " AND CONVERT(VARCHAR, " + tgl2 + ", 112) <= '" + Cf.Tgl112(Sampai) + "'"
                   + " AND b.CaraBayar='SA'");

                c = new TableCell();
                c.Text = Cf.Num(SaldoAwal);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                decimal MemoBiasa = Db.SingleDecimal("SELECT ISNULL(SUM(b.NilaiPelunasan),0) FROM MS_MEMO a"
                   + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN b ON a.NoMEMO = b.NoMEMO"
                   + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN c ON b.NoTagihan = c.NoUrut AND b.NoKontrak = c.NoKontrak"
                   + " WHERE b.NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "'"
                   + " AND CONVERT(VARCHAR, " + tgl2 + ", 112) >= '" + Cf.Tgl112(Dari) + "'"
                   + " AND CONVERT(VARCHAR, " + tgl2 + ", 112) <= '" + Cf.Tgl112(Sampai) + "'"
                   + " AND b.CaraBayar!='SA'");

                c = new TableCell();
                c.Text = Cf.Num(MemoBiasa);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                decimal Total = Db.SingleDecimal("SELECT ISNULL(SUM(b.NilaiPelunasan),0) FROM MS_TTS a"
                    + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN b ON a.NoTTS = b.NoTTS"
                    + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN c ON b.NoTagihan = c.NoUrut AND b.NoKontrak = c.NoKontrak"
                    + " WHERE b.NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "'"
                    + " AND CONVERT(VARCHAR, " + tgl + ", 112) <= '" + Cf.Tgl112(Sampai) + "'");

                c = new TableCell();
                c.Text = Cf.Num(Total);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);


                decimal TotalSaldoAwal = Db.SingleDecimal("SELECT ISNULL(SUM(b.NilaiPelunasan),0) FROM MS_MEMO a"
                   + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN b ON a.NoMEMO = b.NoMEMO"
                   + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN c ON b.NoTagihan = c.NoUrut AND b.NoKontrak = c.NoKontrak"
                   + " WHERE b.NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "'"
                   + " AND CONVERT(VARCHAR, " + tgl2 + ", 112) <= '" + Cf.Tgl112(Sampai) + "'"
                   + " AND b.CaraBayar='SA'");

                c = new TableCell();
                c.Text = Cf.Num(TotalSaldoAwal);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                decimal TotalMemoBiasa = Db.SingleDecimal("SELECT ISNULL(SUM(b.NilaiPelunasan),0) FROM MS_MEMO a"
                   + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN b ON a.NoMEMO = b.NoMEMO"
                   + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN c ON b.NoTagihan = c.NoUrut AND b.NoKontrak = c.NoKontrak"
                   + " WHERE b.NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "'"
                   + " AND CONVERT(VARCHAR, " + tgl2 + ", 112) <= '" + Cf.Tgl112(Sampai) + "'"
                   + " AND b.CaraBayar!='SA'");

                c = new TableCell();
                c.Text = Cf.Num(TotalMemoBiasa);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                decimal NilaiKontrak = Convert.ToDecimal(rs.Rows[i]["NilaiKontrak"]);

                c = new TableCell();
                c.Text = Cf.Num(NilaiKontrak);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                decimal persentase = 0;

                if (NilaiKontrak != 0)
                {
                    persentase = (Total + TotalSaldoAwal + TotalMemoBiasa) / NilaiKontrak * 100;
                }

                c = new TableCell();
                c.Text = Cf.Num(Math.Round(persentase, 2)) + "%";
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                rpt.Rows.Add(r);

                t1 += BF;
                t2 += DP;
                t3 += ANG;
                t4 += ADM;
                t5 += Total;
                t6 += NilaiKontrak;
                t7 += SaldoAwal;
                t8 += MemoBiasa;
                t9 += TotalSaldoAwal;
                t10 += TotalMemoBiasa;

                if (i == rs.Rows.Count - 1)
                {
                    SubTotal("GRAND TOTAL", t1, t2, t3, t4, t5, t6, t7, t8, t9, t10);
                }
            }
        }

        private void SubTotal(string txt, decimal t1, decimal t2, decimal t3, decimal t4, decimal t5, decimal t6, decimal t7, decimal t8, decimal t9, decimal t10)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = Rpt.Foot();
            c.Text = txt;
            c.ColumnSpan = 4;
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t1);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = "&nbsp;";
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t2);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = "&nbsp;";
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t3);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = "&nbsp;";
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t4);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = "&nbsp;";
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t7);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t8);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t5);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t9);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t10);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t6);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = "&nbsp;";
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            rpt.Rows.Add(r);
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion

        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            lokasi.Items.Clear();
            lokasi.Items.Add(new ListItem("SEMUA"));
            init();
        }

        protected void pers_SelectedIndexChanged(object sender, EventArgs e)
        {
            project.Items.Clear();
            lokasi.Items.Clear();
            lokasi.Items.Add(new ListItem("SEMUA"));
            if (pers.SelectedIndex == 0)
            {
                project.Items.Add(new ListItem("SEMUA"));
                Act.ProjectList(project);
            }
            else
                Act.ProjectList(project, pers.SelectedValue);

            init();
        }
    }
}

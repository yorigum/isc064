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

namespace ISC064.COLLECTION.Laporan
{
    public partial class Col : System.Web.UI.Page
    {
        protected System.Web.UI.WebControls.ListBox lokasi;
        protected System.Web.UI.WebControls.ListBox agent;
        protected System.Web.UI.WebControls.CheckBox jenisCheck;
        protected System.Web.UI.WebControls.Label jenisc;
        protected System.Web.UI.WebControls.CheckBoxList jenis;
        protected System.Web.UI.WebControls.RadioButton bfS;
        protected System.Web.UI.WebControls.RadioButton bf1;
        protected System.Web.UI.WebControls.RadioButton bf2;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();

            if (!Page.IsPostBack)
            {
                comp.InnerHtml = Mi.Pt;
                rpt.Visible = false;
                headJudul.Visible = false;
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
            dari.Text = Cf.Day(Cf.AwalBulan());
            sampai.Text = Cf.Day(Cf.AkhirBulan());
            Act.ProjectList(project);
            Act.PersList(pers);
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
            {
                Report();
            }
        }
        protected void xls_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                Report();
                rp.Controls.Add(headJudul);
                rp.Controls.Add(rpt);
                Rpt.ToExcel(this, rp);
            }
        }

        protected void pdf_Click(object sender, System.EventArgs e)
        {

            Process p = new System.Diagnostics.Process();

            string Nama = "Laporan Proyeksi Penerimaan";
            string Link = "";
            DateTime TglGenerate = DateTime.Now;
            string FileName = "";
            string FileType = "application/pdf";
            string UserID = Act.UserID;
            string IP = Act.IP;

            Db.Execute("EXEC ISC064_FINANCEAR..spLapPDFDaftar"

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
                "SELECT TOP 1 AttachmentID FROM ISC064_FINANCEAR..LapPDF ORDER BY AttachmentID DESC");

            string strSql = "SELECT * FROM ISC064_FINANCEAR..LapPDF WHERE AttachmentID  = '" + NoAttachment + "'";
            DataTable rs = Db.Rs(strSql);

            string nfilename = "ProyeksiPenerimaan" + NoAttachment + ".pdf";

            //update filename
            Db.Execute("UPDATE ISC064_FINANCEAR..LapPDF SET FileName= '" + nfilename + "' WHERE AttachmentID = " + NoAttachment);


            //folder untuk menyimpan file pdf
            string save = Mi.PathFilePDFReport + "ProyeksiPenerimaan" + rs.Rows[0]["AttachmentID"] + ".pdf";

            //parameter

            string nStatusS = "";
            string nStatusA = "";
            string nStatusB = "";
            if (statusS.Checked == true)
                nStatusS = statusS.Text;
            else
                nStatusS = "";
            if (statusA.Checked == true)
                nStatusA = statusA.Text;
            else
                nStatusA = "";
            if (statusB.Checked == true)
                nStatusB = statusB.Text;
            else
                nStatusB = "";

            string Project = "";
            if (project.SelectedIndex == 0)
            {
                Project = Act.ProjectListSql.Replace("'", "");
            }
            else
            {
                Project = project.SelectedValue;
            }

            //link untuk download pdf
            string link = Mi.PathAlamatWeb + "collection/LaporanPDF/PDFCol.aspx?id=" + rs.Rows[0]["AttachmentID"]
                + "&status_a=" + nStatusA
                + "&status_b=" + nStatusB
                + "&status_s=" + nStatusS
                + "&userid=" + UserID
                + "&pers=" + pers.SelectedValue
                + "&project=" + Project
                ;

            //update link
            Db.Execute("UPDATE ISC064_FINANCEAR..LapPDF SET Link= '" + link + "' WHERE AttachmentID = " + NoAttachment);

            //format page
            p.StartInfo.Arguments = "--orientation landscape --page-width 11in --page-height 16.5in --margin-left 0 --margin-right 0 --margin-top 0.25cm --margin-bottom 0 " + link + " " + save;

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
            headJudul.Visible = true;

            newHeader();
            //Header();
            HeaderBayar();
            HeaderBayar2();
            Fill();
        }

        private void newHeader()
        {
            string Status = "Semua";
            if (statusA.Checked) Status = "Aktif";
            else if (statusB.Checked) Status = "Batal";
            string header = "<p>" + Mi.Pt + "</p>";
            header += "<h1 class='title'>LAPORAN PROYEKSI PENERIMAAN</h1>";
            header += "Periode : " + Cf.Day(dari.Text) + " s/d " + Cf.Day(sampai.Text) + "<br>";
            header += "Status : " + Status + "<br>";
            header += "Perusahaan : " + pers.SelectedItem.Text + "<br>";
            header += "Project : " + project.SelectedValue;
            header += "<br/> Laporan dihasilkan pada hari " + Cf.IndoWeek(DateTime.Today);
            header += ", " + Cf.Date(DateTime.Now) + " dari workstation " + Act.IP + " oleh user " + Act.UserID + "<br /><br />";
            headJudul.Text = header;
        }

        private void Header()
        {
            System.Text.StringBuilder x = new System.Text.StringBuilder();

            Rpt.Judul(x, comp, judul);

            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);
            string Status = "Semua";
            if (statusA.Checked) Status = "Aktif";
            else if (statusB.Checked) Status = "Batal";
            Rpt.SubJudul(x
                , "Status: " + Status
                );
            Rpt.SubJudul(x
                , "Tgl. Jatuh Tempo : " + Cf.Day(dari.Text) + " s/d " + Cf.Day(sampai.Text)
                );
            Rpt.SubJudul(x
                , "Perusahaan : " + pers.SelectedItem.Text
                );
            Rpt.SubJudul(x
                , "Project : " + project.SelectedValue
                );

            Rpt.Header(rpt, x);
        }

        private void HeaderBayar()
        {
            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);
            if (Dari > Sampai)
            {
                DateTime x = Sampai;
                Sampai = Dari;
                Dari = x;
            }

            int m1 = Dari.Month;
            int m2 = Sampai.Month;
            int y1 = Dari.Year;
            int y2 = Sampai.Year;

            int th = y2 - y1;
            int bln = (m2 - m1) + 1;

            int jum = 0;
            if (th > 0)
            {
                jum = (((th - 1) * 12) + (12 - m1) + m2) + 1;
            }
            else
            {
                jum = bln;
            }

            rpt.Rows[0].Cells[8].ColumnSpan = jum * 5;

            TableRow r = new TableRow();
            r.BackColor = Color.FromArgb(30, 144, 255); ;
            TableCell c;

            //c = new TableHeaderCell();
            //c.ColumnSpan = 8;
            //c.RowSpan = 2;
            //r.Cells.Add(c);

            for (int j = 1; j <= jum; j++)
            {
                c = new TableHeaderCell();
                c.Text = Cf.Monthname(Dari.AddMonths(j - 1).Month) + " " + Dari.AddMonths(j - 1).Year.ToString();
                c.ForeColor = Color.White;
                c.ColumnSpan = 5;
                r.Cells.Add(c);
            }

            //c = new TableHeaderCell();
            //c.ColumnSpan = 2;
            //c.RowSpan = 2;
            //r.Cells.Add(c);

            rpt.Rows.Add(r);
        }

        private void HeaderBayar2()
        {
            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);
            if (Dari > Sampai)
            {
                DateTime x = Sampai;
                Sampai = Dari;
                Dari = x;
            }

            int m1 = Dari.Month;
            int m2 = Sampai.Month;
            int y1 = Dari.Year;
            int y2 = Sampai.Year;

            int th = y2 - y1;
            int bln = (m2 - m1) + 1;

            int jum = 0;
            if (th > 0)
            {
                jum = (((th - 1) * 12) + (12 - m1) + m2) + 1;
            }
            else
            {
                jum = bln;
            }

            TableRow r = new TableRow();
            r.BackColor = Color.FromArgb(30, 144, 255);
            r.ForeColor = Color.White;
            TableCell c;

            for (int j = 1; j <= jum; j++)
            {
                c = new TableHeaderCell();
                c.Text = "Saldo Awal";
                c.ForeColor = Color.White;
                r.Cells.Add(c);

                c = new TableHeaderCell();
                c.Text = "Perencanaan";
                c.ForeColor = Color.White;
                r.Cells.Add(c);

                c = new TableHeaderCell();
                c.Text = "Realisasi";
                c.ForeColor = Color.White;
                r.Cells.Add(c);

                c = new TableHeaderCell();
                c.Text = "Tgl.";
                c.ForeColor = Color.White;
                r.Cells.Add(c);

                c = new TableHeaderCell();
                c.Text = "Saldo Akhir";
                c.ForeColor = Color.White;
                r.Cells.Add(c);
            }

            rpt.Rows.Add(r);
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

            int m1 = Dari.Month;
            int m2 = Sampai.Month;
            int y1 = Dari.Year;
            int y2 = Sampai.Year;

            int th = y2 - y1;
            int bln = (m2 - m1) + 1;

            int jum = 0;
            if (th > 0)
            {
                jum = (((th - 1) * 12) + (12 - m1) + m2) + 1;
            }
            else
            {
                jum = bln;
            }

            decimal[] sawal = new decimal[jum];
            decimal[] t = new decimal[jum];
            decimal[] re = new decimal[jum];
            decimal[] sakhir = new decimal[jum];

            decimal t1 = 0, t2 = 0, t3 = 0, t4 = 0, t5 = 0, t6 = 0;

            string Status = "";
            if (statusA.Checked) Status = " AND a.Status = 'A'";
            if (statusB.Checked) Status = " AND a.Status = 'B'";

            string agent = "";
            if (UserAgent() > 0)
                agent = " AND a.NoAgent = " + UserAgent();

            string Project = " AND a.Project IN (" + Act.ProjectListSql + ")";
            if (project.SelectedValue != "SEMUA") Project = " AND a.Project = '" + project.SelectedValue + "'";
            string Perusahaan = "";
            if (pers.SelectedValue != "SEMUA") Perusahaan = " AND a.Pers = '" + pers.SelectedValue + "'";

            string strSql = "SELECT a.*, b.Nama, b.NoTelp, b.NoHP"
                + " FROM ISC064_MARKETINGJUAL..MS_KONTRAK a"
                + " INNER JOIN ISC064_MARKETINGJUAL..MS_CUSTOMER b ON a.NoCustomer = b.NoCustomer"
                + " WHERE 1=1"
                + Project
                + Perusahaan
                + Status
                + agent
                ;

            DataTable rs = Db.Rs(strSql);            

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected)
                    break;

                bool display = false;
                int tag = Db.SingleInteger("SELECT COUNT(*) FROM ISC064_MARKETINGJUAL..MS_TAGIHAN"
                    + " WHERE CONVERT(VARCHAR,TglJT,112) >= '" + Cf.Tgl112(Dari) + "'"
                    + " AND CONVERT(VARCHAR,TglJT,112) <= '" + Cf.Tgl112(Sampai) + "'"
                    );
                if (tag > 0) display = true;

                if (display)
                {
                    TableRow r = new TableRow();
                    r.Attributes["ondblclick"] = "javascript:popEditKontrak('" + rs.Rows[i]["NoKontrak"] + "');";
                    r.VerticalAlign = VerticalAlign.Top;
                    TableCell c;

                    c = new TableCell();
                    c.Text = (i + 1).ToString();
                    c.HorizontalAlign = HorizontalAlign.Center;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = rs.Rows[i]["Nama"].ToString();
                    c.HorizontalAlign = HorizontalAlign.Left;
                    c.Wrap = false;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = rs.Rows[i]["NoUnit"].ToString();
                    c.HorizontalAlign = HorizontalAlign.Left;
                    c.Wrap = false;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Cf.Num(rs.Rows[i]["NilaiDPP"]);
                    c.HorizontalAlign = HorizontalAlign.Left;
                    c.Wrap = false;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Cf.Num(rs.Rows[i]["NilaiPPN"]);
                    c.HorizontalAlign = HorizontalAlign.Right;
                    c.Wrap = false;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Cf.Num(rs.Rows[i]["NilaiKontrak"]);
                    c.HorizontalAlign = HorizontalAlign.Left;
                    c.Wrap = false;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = rs.Rows[i]["Skema"].ToString();
                    c.HorizontalAlign = HorizontalAlign.Left;
                    c.Wrap = false;
                    r.Cells.Add(c);

                    decimal dp = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiTagihan),0) FROM ISC064_MARKETINGJUAL..MS_TAGIHAN WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' AND Tipe = 'DP'");

                    c = new TableCell();
                    c.Text = Cf.Num(dp);
                    c.HorizontalAlign = HorizontalAlign.Right;
                    c.Wrap = false;
                    r.Cells.Add(c);

                    decimal byr = 0;
                    for (int j = 1; j <= jum; j++)
                    {
                        int m = Dari.AddMonths(j - 1).Month;
                        int y = Dari.AddMonths(j - 1).Year;
                        decimal tagihan = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiTagihan),0) FROM ISC064_MARKETINGJUAL..MS_TAGIHAN WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' AND MONTH(TglJT) = " + m + " AND YEAR(TglJT) = " + y);

                        DateTime TglLalu = new DateTime(y, m, 1);
                        decimal tagihan_lalu = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiTagihan),0) FROM ISC064_MARKETINGJUAL..MS_TAGIHAN WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' AND CONVERT(VARCHAR,TglJT,112) < '" + Cf.Tgl112(TglLalu) + "'");
                        decimal realisasi_lalu = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan),0)"
                            + " FROM ISC064_MARKETINGJUAL..MS_PELUNASAN a"
                            + " INNER JOIN MS_TTS b ON a.NoTTS = b.NoTTS"
                            + " INNER JOIN ISC064_MARKETINGJUAL..MS_TAGIHAN c ON a.NoTagihan = c.NoUrut AND a.NoKontrak = c.NoKontrak"
                            + " WHERE c.NoKontrak = '" + Cf.Pk(rs.Rows[i]["NoKontrak"]) + "'"
                            + " AND b.Status <> 'VOID'"
                            + " AND CONVERT(VARCHAR,c.TglJT,112) < '" + Cf.Tgl112(TglLalu) + "'");
                        decimal SaldoAwal = tagihan_lalu - realisasi_lalu;

                        c = new TableCell();
                        c.Text = Cf.Num(SaldoAwal);
                        c.HorizontalAlign = HorizontalAlign.Right;
                        c.Wrap = false;
                        r.Cells.Add(c);

                        c = new TableCell();
                        c.Text = Cf.Num(tagihan);
                        c.HorizontalAlign = HorizontalAlign.Right;
                        c.Wrap = false;
                        r.Cells.Add(c);

                        decimal realisasi = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan),0)"
                            + " FROM ISC064_MARKETINGJUAL..MS_PELUNASAN a"
                            + " INNER JOIN MS_TTS b ON a.NoTTS = b.NoTTS"
                            + " INNER JOIN ISC064_MARKETINGJUAL..MS_TAGIHAN c ON a.NoTagihan = c.NoUrut AND a.NoKontrak = c.NoKontrak"
                            + " WHERE c.NoKontrak = '" + Cf.Pk(rs.Rows[i]["NoKontrak"]) + "'"
                            + " AND b.Status <> 'VOID'"
                            + " AND MONTH(c.TglJT) = " + m + " AND YEAR(c.TglJT) = " + y);
                        
                        c = new TableCell();
                        c.Text = Cf.Num(realisasi);
                        c.HorizontalAlign = HorizontalAlign.Right;
                        c.Wrap = false;
                        r.Cells.Add(c);

                        string tglrealisasi = "";
                        DataTable aa = Db.Rs("SELECT a.TglPelunasan"
                            + " FROM ISC064_MARKETINGJUAL..MS_PELUNASAN a"
                            + " INNER JOIN MS_TTS b ON a.NoTTS = b.NoTTS"
                            + " INNER JOIN ISC064_MARKETINGJUAL..MS_TAGIHAN c ON a.NoTagihan = c.NoUrut AND a.NoKontrak = c.NoKontrak"
                            + " WHERE c.NoKontrak = '" + Cf.Pk(rs.Rows[i]["NoKontrak"]) + "'"
                            + " AND b.Status <> 'VOID'"
                            + " AND MONTH(c.TglJT) = " + m + " AND YEAR(c.TglJT) = " + y);
                        for (int k = 0; k < aa.Rows.Count; k++)
                        {
                            if (k > 0) tglrealisasi += ", ";
                            tglrealisasi += Cf.Day(aa.Rows[k]["TglPelunasan"]);
                        }

                        c = new TableCell();
                        c.Text = tglrealisasi;
                        c.HorizontalAlign = HorizontalAlign.Left;
                        c.Wrap = false;
                        r.Cells.Add(c);

                        decimal SaldoAkhir = (tagihan - realisasi) + SaldoAwal;

                        c = new TableCell();
                        c.Text = Cf.Num(SaldoAkhir);
                        c.HorizontalAlign = HorizontalAlign.Right;
                        c.Wrap = false;
                        r.Cells.Add(c);

                        sawal[j - 1] += SaldoAwal;
                        t[j - 1] += tagihan;
                        re[j - 1] += realisasi;
                        sakhir[j - 1] += SaldoAkhir;

                        byr += realisasi;
                    }

                    c = new TableCell();
                    c.Text = Cf.Num(byr); ;
                    c.HorizontalAlign = HorizontalAlign.Right;
                    c.Wrap = false;
                    r.Cells.Add(c);

                    decimal sisa = Convert.ToDecimal(rs.Rows[i]["NilaiKontrak"]) - byr;

                    c = new TableCell();
                    c.Text = Cf.Num(sisa); ;
                    c.HorizontalAlign = HorizontalAlign.Right;
                    c.Wrap = false;
                    r.Cells.Add(c);

                    rpt.Rows.Add(r);

                    t1 += Convert.ToDecimal(rs.Rows[i]["NilaiKontrak"]);
                    t2 += Convert.ToDecimal(dp);
                    t3 += byr;
                    t4 += sisa;

                    if (i == rs.Rows.Count - 1)
                        SubTotal(t1, t2, t3, t4, sawal, t, re, sakhir);
                }
            }
        }

        protected void SubTotal(decimal t1, decimal t2, decimal t3, decimal t4, decimal[] sawal, decimal[] t, decimal[] re, decimal[] sakhir)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = Rpt.Foot();
            c.Text = "<strong>TOTAL</strong>";
            c.ColumnSpan = 5;
            c.HorizontalAlign = HorizontalAlign.Center;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t1);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = "&nbsp;";
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t2);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            for (int i = 0; i < t.Length; i++)
            {
                c = Rpt.Foot();
                c.Text = Cf.Num(sawal[i]);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = Rpt.Foot();
                c.Text = Cf.Num(t[i]);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = Rpt.Foot();
                c.Text = Cf.Num(re[i]);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = Rpt.Foot();
                c.Text = "&nbsp;";
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = Rpt.Foot();
                c.Text = Cf.Num(sakhir[i]);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);
            }

            c = Rpt.Foot();
            c.Text = Cf.Num(t3);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t4);
            c.HorizontalAlign = HorizontalAlign.Right;
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



        protected void pers_SelectedIndexChanged(object sender, EventArgs e)
        {
            project.Items.Clear();
            if (pers.SelectedIndex == 0)
            {
                project.Items.Add(new ListItem("SEMUA"));
                Act.ProjectList(project);
            }
            else
                Act.ProjectList(project, pers.SelectedValue);
        }
    }
}

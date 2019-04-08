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
    public partial class Col2 : System.Web.UI.Page
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
            Cf.BindBulan(daribln);
            Cf.BindBulan(sampaibln);
            Cf.BindTahun(thn);
            Act.ProjectList(project);
            Act.PersList(pers);
            thn.SelectedValue = DateTime.Now.Year.ToString();
        }

        private bool valid()
        {
            string s = "";
            bool x = true;

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

            string Nama = "Laporan Collection";
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
                    + ",'" + null + "'"
                    + ",'" + null + "'"
                    );

            //get nomor customer terbaru
            int NoAttachment = Db.SingleInteger(
                "SELECT TOP 1 AttachmentID FROM ISC064_FINANCEAR..LapPDF ORDER BY AttachmentID DESC");

            string strSql = "SELECT * FROM ISC064_FINANCEAR..LapPDF WHERE AttachmentID  = '" + NoAttachment + "'";
            DataTable rs = Db.Rs(strSql);

            string nfilename = "LaporanCollection" + NoAttachment + ".pdf";

            //update filename
            Db.Execute("UPDATE ISC064_FINANCEAR..LapPDF SET FileName= '" + nfilename + "' WHERE AttachmentID = " + NoAttachment);


            //folder untuk menyimpan file pdf
            string save = Mi.PathFilePDFReport + "LaporanCollection" + rs.Rows[0]["AttachmentID"] + ".pdf";

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

            string blndari = daribln.SelectedValue;
            string blnsampai = sampaibln.SelectedValue;
            string Tahun = thn.SelectedValue;

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
            string link = Mi.PathAlamatWeb + "collection/LaporanPDF/PDFCol2.aspx?id=" + rs.Rows[0]["AttachmentID"]
                + "&status_a=" + nStatusA
                + "&status_b=" + nStatusB
                + "&status_s=" + nStatusS
                + "&blndari=" + blndari
                + "&blnsampai=" + blnsampai
                + "&thn=" + Tahun
                + "&userid=" + UserID
                + "&pers=" + pers.SelectedValue
                + "&project=" + Project
                ;

            //update link
            Db.Execute("UPDATE ISC064_FINANCEAR..LapPDF SET Link= '" + link + "' WHERE AttachmentID = " + NoAttachment);

            //format page
            p.StartInfo.Arguments = "--orientation landscape --page-width 16.5in --page-height 23.4in --margin-left 0 --margin-right 0 --margin-top 0.25cm --margin-bottom 0 " + link + " " + save;

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
            Fill();
        }

        private void newHeader()
        {
            string header = "<p>" + Mi.Pt + "</p>";
            header += "<h1 class='title'>LAPORAN COLLECTION</h1>";
            header += "Periode : " + Cf.Monthname(Convert.ToInt32(daribln.SelectedValue)) + " s/d " + Cf.Monthname(Convert.ToInt32(sampaibln.SelectedValue)) + " " + thn.SelectedValue;
            header += "<br>Project : " + project.SelectedItem.Text;
            header += "<br>Perusahaan : " + pers.SelectedItem.Text;
            header += "<br/> Laporan dihasilkan pada hari " + Cf.IndoWeek(DateTime.Today);
            header += ", " + Cf.Date(DateTime.Now) + " dari workstation " + Act.IP + " oleh user " + Act.UserID + "<br /><br />";
            headJudul.Text = header;
        }

        private void Header()
        {
            System.Text.StringBuilder x = new System.Text.StringBuilder();

            Rpt.Judul(x, comp, judul);

            DateTime Dari = Convert.ToDateTime(daribln.Text);
            DateTime Sampai = Convert.ToDateTime(sampaibln.Text);
            Rpt.SubJudul(x
                , "Periode : " + Cf.Monthname(Convert.ToInt32(daribln.SelectedValue)) + " s/d " + Cf.Monthname(Convert.ToInt32(sampaibln.SelectedValue)) + " " + thn.SelectedValue
                );
            Rpt.SubJudul(x
                , "Project : " + project.SelectedItem.Text
                );
            Rpt.SubJudul(x
                , "Perusahaan : " + pers.SelectedItem.Text
                );

            Rpt.Header(rpt, x);
        }

        private void HeaderBayar()
        {
            DateTime Dari = Cf.AwalBulan(Convert.ToInt32(daribln.SelectedValue), Convert.ToInt32(thn.SelectedValue));
            DateTime Sampai = Cf.AwalBulan(Convert.ToInt32(sampaibln.SelectedValue), Convert.ToInt32(thn.SelectedValue));
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
            TableCell c;

            c = new TableHeaderCell();
            c.Text = "NO";
            c.BackColor = Color.FromArgb(30, 144, 255);
            c.ForeColor = Color.White;
            c.RowSpan = 2;
            r.Cells.Add(c);

            c = new TableHeaderCell();
            c.Text = "Sales";
            c.BackColor = Color.FromArgb(30, 144, 255);
            c.ForeColor = Color.White;
            c.RowSpan = 2;
            r.Cells.Add(c);

            c = new TableHeaderCell();
            c.Text = "UNIT";
            c.BackColor = Color.FromArgb(30, 144, 255);
            c.ForeColor = Color.White;
            c.RowSpan = 2;
            r.Cells.Add(c);

            c = new TableHeaderCell();
            c.Text = "Luas";
            c.BackColor = Color.FromArgb(30, 144, 255);
            c.ForeColor = Color.White;
            c.ColumnSpan = 2;
            r.Cells.Add(c);

            c = new TableHeaderCell();
            c.Text = "Harga exc PPN";
            c.BackColor = Color.FromArgb(30, 144, 255);
            c.ForeColor = Color.White;
            c.RowSpan = 2;
            r.Cells.Add(c);

            c = new TableHeaderCell();
            c.Text = "Harga Jual inc PPN";
            c.BackColor = Color.FromArgb(30, 144, 255);
            c.ForeColor = Color.White;
            c.RowSpan = 2;
            r.Cells.Add(c);

            c = new TableHeaderCell();
            c.Text = "CUSTOMER";
            c.BackColor = Color.FromArgb(30, 144, 255);
            c.ForeColor = Color.White;
            c.RowSpan = 2;
            r.Cells.Add(c);

            c = new TableHeaderCell();
            c.Text = "NPWP";
            c.BackColor = Color.FromArgb(30, 144, 255);
            c.ForeColor = Color.White;
            c.RowSpan = 2;
            r.Cells.Add(c);

            c = new TableHeaderCell();
            c.Text = "Alamat NPWP";
            c.BackColor = Color.FromArgb(30, 144, 255);
            c.ForeColor = Color.White;
            c.RowSpan = 2;
            r.Cells.Add(c);

            c = new TableHeaderCell();
            c.Text = "Project";
            c.BackColor = Color.FromArgb(30, 144, 255);
            c.ForeColor = Color.White;
            c.RowSpan = 2;
            r.Cells.Add(c);

            for (int j = 1; j <= jum; j++)
            {
                c = new TableHeaderCell();
                c.Text = Cf.Monthname(Dari.AddMonths(j - 1).Month) + " " + Dari.AddMonths(j - 1).Year.ToString();
                c.BackColor = Color.FromArgb(30, 144, 255);
                c.ForeColor = Color.White;
                c.RowSpan = 2;
                r.Cells.Add(c);

                c = new TableHeaderCell();
                c.Text = "Saldo Awal " + Cf.Monthname(Dari.AddMonths(j - 1).Month) + " " + Dari.AddMonths(j - 1).Year.ToString();
                c.BackColor = Color.FromArgb(30, 144, 255);
                c.ForeColor = Color.White;
                c.RowSpan = 2;
                r.Cells.Add(c);

                c = new TableHeaderCell();
                c.Text = "Memo " + Cf.Monthname(Dari.AddMonths(j - 1).Month) + " " + Dari.AddMonths(j - 1).Year.ToString();
                c.BackColor = Color.FromArgb(30, 144, 255);
                c.ForeColor = Color.White;
                c.RowSpan = 2;
                r.Cells.Add(c);
            }

            c = new TableHeaderCell();
            c.Text = "TOTAL " + thn.SelectedValue;
            c.BackColor = Color.FromArgb(30, 144, 255);
            c.ForeColor = Color.White;
            c.RowSpan = 2;
            r.Cells.Add(c);

            c = new TableHeaderCell();
            c.Text = "TOTAL SALDO AWAL " + thn.SelectedValue;
            c.RowSpan = 2;
            c.BackColor = Color.FromArgb(30, 144, 255);
            c.ForeColor = Color.White;
            r.Cells.Add(c);

            c = new TableHeaderCell();
            c.Text = "TOTAL MEMO " + thn.SelectedValue;
            c.BackColor = Color.FromArgb(30, 144, 255);
            c.ForeColor = Color.White;
            c.RowSpan = 2;
            r.Cells.Add(c);

            c = new TableHeaderCell();
            c.BackColor = Color.FromArgb(30, 144, 255);
            c.ForeColor = Color.White;
            c.Text = "Akumulasi<br/>Pembayaran";
            c.RowSpan = 2;
            r.Cells.Add(c);

            c = new TableHeaderCell();
            c.Text = "Akumulasi<br/>Pembayaran (Saldo Awal)";
            c.BackColor = Color.FromArgb(30, 144, 255);
            c.ForeColor = Color.White;
            c.RowSpan = 2;
            r.Cells.Add(c);

            c = new TableHeaderCell();
            c.Text = "Akumulasi<br/>Pembayaran (Memo)";
            c.BackColor = Color.FromArgb(30, 144, 255);
            c.ForeColor = Color.White;
            c.RowSpan = 2;
            r.Cells.Add(c);

            c = new TableHeaderCell();
            c.Text = "%";
            c.BackColor = Color.FromArgb(30, 144, 255);
            c.ForeColor = Color.White;
            c.RowSpan = 2;
            r.Cells.Add(c);

            rpt.Rows.Add(r);

            r = new TableRow();
            r.BackColor = Color.LightGray;

            c = new TableHeaderCell();
            c.Text = "Nett";
            c.BackColor = Color.FromArgb(30, 144, 255);
            c.ForeColor = Color.White;
            r.Cells.Add(c);

            c = new TableHeaderCell();
            c.Text = "Gross";
            c.BackColor = Color.FromArgb(30, 144, 255);
            c.ForeColor = Color.White;
            r.Cells.Add(c);

            rpt.Rows.Add(r);
        }

        private void Fill()
        {
            DateTime Dari = Cf.AwalBulan(Convert.ToInt32(daribln.SelectedValue), Convert.ToInt32(thn.SelectedValue));
            DateTime Sampai = Cf.AkhirBulan(Convert.ToInt32(sampaibln.SelectedValue), Convert.ToInt32(thn.SelectedValue));
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

            decimal[] t = new decimal[jum];
            decimal[] re = new decimal[jum];
            decimal[] reSA = new decimal[jum];
            decimal[] reMO = new decimal[jum];

            decimal t1 = 0, t2 = 0, t3 = 0, t4 = 0, t5 = 0, t6 = 0, t7 = 0, t8 = 0, t9 = 0, t10 = 0, t11 = 0;

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

            string strSql = "SELECT a.*, b.*"
                + " FROM ISC064_MARKETINGJUAL..MS_KONTRAK a"
                + " INNER JOIN ISC064_MARKETINGJUAL..MS_CUSTOMER b ON a.NoCustomer = b.NoCustomer"
                + " WHERE 1=1"                
                + Project
                + Perusahaan
                + Status
                + agent
                + " ORDER BY a.PersenLunas DESC"
                ;

            DataTable rs = Db.Rs(strSql);

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected)
                    break;

                TableRow r = new TableRow();
                r.Attributes["ondblclick"] = "javascript:popEditKontrak('" + rs.Rows[i]["NoKontrak"] + "');";
                r.VerticalAlign = VerticalAlign.Top;
                TableCell c;

                c = new TableCell();
                c.Text = (i + 1).ToString();
                c.HorizontalAlign = HorizontalAlign.Center;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                string Nama = Db.SingleString("SELECT Nama FROM "+Mi.DbPrefix+"MARKETINGJUAL..MS_AGENT WHERE NoAgent  = '" + rs.Rows[i]["NoAgent"] + "'");
                //c.Text = Cf.Monthname(Convert.ToDateTime(rs.Rows[i]["TglKontrak"]).Month) + " " + Convert.ToDateTime(rs.Rows[i]["TglKontrak"]).Year;
                c.Text = Nama;
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoUnit"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                decimal LuasNett = Db.SingleDecimal("SELECT LuasNett FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT WHERE NoStock = '" + rs.Rows[i]["NoStock"] + "'");
                decimal Luas = Db.SingleDecimal("SELECT Luas FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT WHERE NoStock = '" + rs.Rows[i]["NoStock"] + "'");

                c = new TableCell();
                c.Text = Cf.Num(LuasNett);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Luas);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["NilaiDPP"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["NilaiKontrak"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Nama"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NPWP"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                string alamatnpwp = rs.Rows[i]["NPWPAlamat1"].ToString() + "<br/>" + rs.Rows[i]["NPWPAlamat2"].ToString() + "<br/>" + rs.Rows[i]["NPWPAlamat3"].ToString();
                c = new TableCell();
                c.Text = alamatnpwp;
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + rs.Rows[i]["Project"].ToString() + "'");
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                decimal byr = 0;
                for (int j = 1; j <= jum; j++)
                {
                    int m = Dari.AddMonths(j - 1).Month;
                    int y = Dari.AddMonths(j - 1).Year;

                    decimal realisasi = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan),0)"
                        + " FROM ISC064_MARKETINGJUAL..MS_PELUNASAN a"
                        + " INNER JOIN MS_TTS b ON a.NoTTS = b.NoTTS"
                        + " WHERE a.NoKontrak = '" + Cf.Pk(rs.Rows[i]["NoKontrak"]) + "'"
                        + " AND MONTH(b.TglTTS) = " + m + " AND YEAR(b.TglTTS) = " + y);

                    c = new TableCell();
                    c.Text = Cf.Num(realisasi);
                    c.HorizontalAlign = HorizontalAlign.Right;
                    c.Wrap = false;
                    r.Cells.Add(c);

                    decimal saldoawal = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan),0)"
                       + " FROM ISC064_MARKETINGJUAL..MS_PELUNASAN a"
                       + " INNER JOIN MS_MEMO b ON a.NoMEMO = b.NoMEMO"
                       + " WHERE b.CaraBayar = 'SA' AND a.NoKontrak = '" + Cf.Pk(rs.Rows[i]["NoKontrak"]) + "'"
                       + " AND MONTH(b.TglMEMO) = " + m + " AND YEAR(b.TgLMEMO) = " + y);

                    c = new TableCell();
                    c.Text = Cf.Num(saldoawal);
                    c.HorizontalAlign = HorizontalAlign.Right;
                    c.Wrap = false;
                    r.Cells.Add(c);

                    decimal memo = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan),0)"
                       + " FROM ISC064_MARKETINGJUAL..MS_PELUNASAN a"
                       + " INNER JOIN MS_MEMO b ON a.NoMEMO = b.NoMEMO"
                       + " WHERE a.NoKontrak = '" + Cf.Pk(rs.Rows[i]["NoKontrak"]) + "'"
                       + " AND MONTH(b.TglMEMO) = " + m + " AND YEAR(b.TglMEMO) = " + y);

                    c = new TableCell();
                    c.Text = Cf.Num(memo);
                    c.HorizontalAlign = HorizontalAlign.Right;
                    c.Wrap = false;
                    r.Cells.Add(c);


                    re[j - 1] += realisasi;
                    reSA[j - 1] += saldoawal;
                    reMO[j - 1] += memo;
                }

                decimal tot = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan),0)"
                    + " FROM ISC064_MARKETINGJUAL..MS_PELUNASAN a"
                    + " INNER JOIN MS_TTS b ON a.NoTTS = b.NoTTS"
                    + " WHERE a.NoKontrak = '" + Cf.Pk(rs.Rows[i]["NoKontrak"]) + "'"
                    + " AND YEAR(b.TglTTS) = " + Convert.ToInt32(thn.SelectedValue));

                c = new TableCell();
                c.Text = Cf.Num(tot); ;
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                decimal totSA = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan),0)"
                    + " FROM ISC064_MARKETINGJUAL..MS_PELUNASAN a"
                    + " INNER JOIN MS_MEMO b ON a.NoMemo = b.NoMemo"
                    + " WHERE b.CaraBayar='SA' AND a.NoKontrak = '" + Cf.Pk(rs.Rows[i]["NoKontrak"]) + "'"
                    + " AND YEAR(b.TglMEMO) = " + Convert.ToInt32(thn.SelectedValue));

                c = new TableCell();
                c.Text = Cf.Num(totSA); ;
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                decimal totMO = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan),0)"
                    + " FROM ISC064_MARKETINGJUAL..MS_PELUNASAN a"
                    + " INNER JOIN MS_MEMO b ON a.NoMemo = b.NoMemo"
                    + " WHERE a.NoKontrak = '" + Cf.Pk(rs.Rows[i]["NoKontrak"]) + "'"
                    + " AND YEAR(b.TglMEMO) = " + Convert.ToInt32(thn.SelectedValue));

                c = new TableCell();
                c.Text = Cf.Num(totMO); ;
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);


                decimal akum = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan),0)"
                    + " FROM ISC064_MARKETINGJUAL..MS_PELUNASAN a"
                    + " INNER JOIN MS_TTS b ON a.NoTTS = b.NoTTS"
                    + " WHERE a.NoKontrak = '" + Cf.Pk(rs.Rows[i]["NoKontrak"]) + "'"
                    + " AND CONVERT(VARCHAR,b.TglTTS,112) <= '" + Cf.Tgl112(Sampai) + "'");

                c = new TableCell();
                c.Text = Cf.Num(akum); ;
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);


                decimal akumSA = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan),0)"
                    + " FROM ISC064_MARKETINGJUAL..MS_PELUNASAN a"
                    + " INNER JOIN MS_MEMO b ON a.NoMEMO = b.NoMEMO"
                    + " WHERE b.CaraBayar = 'SA' AND a.NoKontrak = '" + Cf.Pk(rs.Rows[i]["NoKontrak"]) + "'"
                    + " AND CONVERT(VARCHAR,b.TglMEMO,112) <= '" + Cf.Tgl112(Sampai) + "'");

                c = new TableCell();
                c.Text = Cf.Num(akumSA); ;
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                decimal akumMO = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan),0)"
                    + " FROM ISC064_MARKETINGJUAL..MS_PELUNASAN a"
                    + " INNER JOIN MS_MEMO b ON a.NoMEMO = b.NoMEMO"
                    + " WHERE a.NoKontrak = '" + Cf.Pk(rs.Rows[i]["NoKontrak"]) + "'"
                    + " AND CONVERT(VARCHAR,b.TglMEMO,112) <= '" + Cf.Tgl112(Sampai) + "'");

                c = new TableCell();
                c.Text = Cf.Num(akumMO); ;
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                decimal a = akum + akumSA + akumMO;
                decimal persen = 0;
                if(a > 0)
                {
                   persen = Math.Round(((akum + akumSA + akumMO) / Convert.ToDecimal(rs.Rows[i]["NilaiKontrak"]) * 100), 2);
                }

                c = new TableCell();
                c.Text = Cf.Num(persen);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                rpt.Rows.Add(r);

                t1 += Convert.ToDecimal(LuasNett);
                t2 += Convert.ToDecimal(Luas);
                t3 += Convert.ToDecimal(rs.Rows[i]["NilaiDPP"]);
                t4 += Convert.ToDecimal(rs.Rows[i]["NilaiKontrak"]);
                t5 += tot;
                t7 += totSA;
                t8 += totMO;
                t6 += akum;
                t9 += akumSA;
                t10 += akumMO;
                t11 += persen;

                if (i == rs.Rows.Count - 1)
                    SubTotal(t1, t2, t3, t4, re, t5, t6, t7, t8, t9, t10, reSA, reMO,t11);
            }
        }

        protected void SubTotal(decimal t1, decimal t2, decimal t3, decimal t4, decimal[] re, decimal t5, decimal t6, decimal t7, decimal t8, decimal t9, decimal t10, decimal[] reSA, decimal[] reMO, decimal t11)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = Rpt.Foot();
            c.Text = "<strong>GRAND TOTAL</strong>";
            c.ColumnSpan = 3;
            c.HorizontalAlign = HorizontalAlign.Center;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t1);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t2);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t3);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t4);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = "&nbsp;";
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = "&nbsp;";
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = "&nbsp;";
            c.ColumnSpan = 2;
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            for (int i = 0; i < re.Length; i++)
            {
                c = Rpt.Foot();
                c.Text = Cf.Num(re[i]);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = Rpt.Foot();
                c.Text = Cf.Num(reSA[i]);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = Rpt.Foot();
                c.Text = Cf.Num(reMO[i]);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);
            }

            c = Rpt.Foot();
            c.Text = Cf.Num(t5);
            c.HorizontalAlign = HorizontalAlign.Right;
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
            c.Text = Cf.Num(t6);
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
            c.Text = "&nbsp;";
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

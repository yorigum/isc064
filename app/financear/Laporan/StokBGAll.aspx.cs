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
    public partial class StokBGAll : System.Web.UI.Page
    {

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
            Cf.BindTahun(daric);
            Cf.BindTahun(sampaic);
            daric.SelectedValue = DateTime.Today.Year.ToString();
            sampaic.SelectedValue = DateTime.Today.Year.ToString();
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
                Rpt.ToExcel(this, headReport, rpt);
            }
        }
        protected void pdf_Click(object sender, System.EventArgs e)
        {

            Process p = new System.Diagnostics.Process();

            string Nama = "Laporan Stok Cek Giro Tahunan";
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

            string nfilename = "StokCekGiroTahunan" + NoAttachment + ".pdf";

            //update filename
            Db.Execute("UPDATE ISC064_FINANCEAR..LapPDF SET FileName= '" + nfilename + "' WHERE AttachmentID = " + NoAttachment);


            //folder untuk menyimpan file pdf
            string save = Mi.PathFilePDFReport + "StokCekGiroTahunan" + rs.Rows[0]["AttachmentID"] + ".pdf";

            //parameter
            string Status = metode.SelectedValue;
            string Perhitungan = "";
            if (kuantitas.Checked)
                Perhitungan = "KUANTITAS";
            else if (rupiah.Checked)
                Perhitungan = "RUPIAH";

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
            string link = Mi.PathAlamatWeb + "financear/LaporanPDF/PDFStokBGAll.aspx?id=" + rs.Rows[0]["AttachmentID"]
                + "&userid=" + UserID
                + "&perhitungan=" + Perhitungan
                + "&dari=" + daric.SelectedValue
                + "&sampai=" + sampaic.SelectedValue
                + "&metode=" + metode.SelectedValue
                + "&project=" + Project
                + "&pers=" + pers.SelectedValue
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
        private void Report()
        {
            param.Visible = false;
            rpt.Visible = true;

            Header();
            Fill();
        }

        private void Header()
        {
            int Dari = Convert.ToInt32(daric.SelectedValue);
            int Sampai = Convert.ToInt32(sampaic.SelectedValue);

            if (Dari > Sampai)
            {
                int y = Dari;
                Dari = Sampai;
                Sampai = y;
            }

            /* TABLE HEADER */
            TableRow r = new TableRow();
            TableCell c;

            //c = new TableCell();
            //c.ColumnSpan = Sampai - Dari + 3;
            //c.Attributes["style"] = "font-size: 8pt;";
            //r.Cells.Add(c);

            rpt.Rows.Add(r);

            r = new TableRow();
            TableHeaderCell hc;

            hc = new TableHeaderCell();
            hc.Text = "Bulan";
            hc.ForeColor = Color.White;
            hc.Attributes["style"] = "background-color:#1E90FF";
            r.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.Text = "Total";
            hc.ForeColor = Color.White;
            hc.Attributes["style"] = "background-color:#1E90FF";
            r.Cells.Add(hc);

            for (int i = Dari; i <= Sampai; i++)
            {
                hc = new TableHeaderCell();
                hc.Text = i.ToString();
                hc.ForeColor = Color.White;
                hc.Attributes["style"] = "background-color:#1E90FF";
                r.Cells.Add(hc);
            }

            rpt.Rows.Add(r);
            /*****************/

            System.Text.StringBuilder x = new System.Text.StringBuilder();

            Rpt.Judul(x, comp, judul);

            Rpt.SubJudul(x
                , "Status : " + metode.SelectedItem.Text
                );

            Rpt.SubJudul(x
                , "Dari : " + daric.SelectedItem.Text
                );

            Rpt.SubJudul(x
                , "Sampai : " + sampaic.SelectedItem.Text
                );
            Rpt.SubJudul(x
                , "Project : " + project.SelectedItem.Text
                );
            Rpt.SubJudul(x
                , "Perusahaan : " + pers.SelectedValue
                );

            string hitung = "";
            if (kuantitas.Checked) hitung = kuantitas.Text;
            if (rupiah.Checked) hitung = rupiah.Text;
            Rpt.SubJudul(x
                , "Perhitungan : " + hitung);

            string legend = "";
            //Rpt.Header(rpt, x);
            Rpt.HeaderReport(headReport, legend, x);
        }

        private void Fill()
        {
            decimal GrandTotal = 0;
            int Dari = Convert.ToInt32(daric.SelectedValue);
            int Sampai = Convert.ToInt32(sampaic.SelectedValue);

            if (Dari > Sampai)
            {
                int x = Dari;
                Dari = Sampai;
                Sampai = x;
            }

            for (int bln = 1; bln <= 12; bln++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                r.VerticalAlign = VerticalAlign.Top;

                c = new TableCell();
                c.Text = Cf.Monthname(bln);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                int t = 0;
                int gt = 0;
                decimal rp = 0;
                decimal grp = 0;

                c = new TableCell();
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Font.Bold = true;
                r.Cells.Add(c);

                for (int thn = Dari; thn <= Sampai; thn++)
                {
                    c = new TableCell();

                    if (kuantitas.Checked)
                    {
                        t = sum1(thn, bln);
                        gt += t;
                        if (t != 0)
                            c.Text = t.ToString();
                    }

                    if (rupiah.Checked)
                    {
                        rp = sum2(thn, bln);
                        grp += rp;
                        if (rp != 0)
                            c.Text = Cf.Num(rp);
                    }

                    c.HorizontalAlign = HorizontalAlign.Right;
                    r.Cells.Add(c);
                }

                c = r.Cells[1];
                if (kuantitas.Checked)
                    c.Text = gt.ToString();
                if (rupiah.Checked)
                    c.Text = Cf.Num(grp);

                rpt.Rows.Add(r);

                if (kuantitas.Checked)
                    GrandTotal += Convert.ToInt32(gt);
                else if (rupiah.Checked)
                    GrandTotal += grp;

                if (bln == 12)
                    SubTotal(GrandTotal);
            }
        }

        private void SubTotal(decimal GrandTotal)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = Rpt.Foot();
            c.Text = "Grand Total";
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(GrandTotal);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            rpt.Rows.Add(r);
        }

        private int sum1(int year, int bln)
        {
            string addq = "";
            if (metode.SelectedIndex == 1)
                addq = " AND StatusBG = 'OK'";
            else if (metode.SelectedIndex == 2)
                addq = " AND StatusBG = 'OK' AND a.Status = 'BARU'";
            else if (metode.SelectedIndex == 3)
                addq = " AND StatusBG = 'OK' AND a.Status = 'POST'";
            else if (metode.SelectedIndex == 4)
                addq = " AND StatusBG = 'BAD'";

            string Project = " AND b.Project IN (" + Act.ProjectListSql + ")";
            if (project.SelectedValue != "SEMUA") Project = " AND b.Project = '" + project.SelectedValue + "'";
            string Perusahaan = "";
            if (pers.SelectedValue != "SEMUA") Perusahaan = " AND b.Pers = '" + pers.SelectedValue + "'";

            string agent = "";
            if (UserAgent() > 0)
                agent = " AND (SELECT NoAgent FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = MS_TTS.Ref) = " + UserAgent();

            return Db.SingleInteger("SELECT COUNT(DISTINCT NoBG)"
                + " FROM MS_TTS a"
                + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK b ON a.Ref = b.NoKontrak"
                + " WHERE a.CaraBayar = 'BG' "
                + " AND MONTH(TglBG) = " + bln + " AND YEAR(TglBG) = " + year
                + Project
                + Perusahaan
                + addq
                + agent
                );
        }

        private decimal sum2(int year, int bln)
        {
            string addq = "";
            if (metode.SelectedIndex == 1)
                addq = " AND StatusBG = 'OK'";
            else if (metode.SelectedIndex == 2)
                addq = " AND StatusBG = 'OK' AND a.Status = 'BARU'";
            else if (metode.SelectedIndex == 3)
                addq = " AND StatusBG = 'OK' AND a.Status = 'POST'";
            else if (metode.SelectedIndex == 4)
                addq = " AND StatusBG = 'BAD'";

            string Project = " AND b.Project IN (" + Act.ProjectListSql + ")";
            if (project.SelectedValue != "SEMUA") Project = " AND b.Project = '" + project.SelectedValue + "'";
            string Perusahaan = "";
            if (pers.SelectedValue != "SEMUA") Perusahaan = " AND b.Pers = '" + pers.SelectedValue + "'";

            string agent = "";
            if (UserAgent() > 0)
                agent = " AND (SELECT NoAgent FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = MS_TTS.Ref) = " + UserAgent();

            return Db.SingleDecimal("SELECT ISNULL(SUM(Total),0)"
                + " FROM MS_TTS a"
                + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK b ON a.Ref = b.NoKontrak"
                + " WHERE a.CaraBayar = 'BG' "
                + " AND MONTH(TglBG) = " + bln + " AND YEAR(TglBG) = " + year
                + Project
                + Perusahaan
                + addq
                + agent
                );
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

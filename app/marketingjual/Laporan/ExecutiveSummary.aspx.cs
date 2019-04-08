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

namespace ISC064.MARKETINGJUAL.Laporan
{
    public partial class ExecutiveSummary : System.Web.UI.Page
    {
        protected System.Web.UI.WebControls.PlaceHolder list;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            if (!Page.IsPostBack)
            {
                Bind();
                comp.InnerHtml = Mi.Pt;
                Js.Focus(this, scr);
                if (!Act.Sec("DownloadExcel")) xls.Enabled = false;
            }
        }
        protected void Bind()
        {
            project.Items.Clear();
            project.Items.Add("SEMUA");
            Act.ProjectList(project);
            //Act.PersList(pers);
        }
        protected void Report()
        {
            param.Visible = false;
            rpt.Visible = true;

            Fill();
        }
        protected void pdf_Click(object sender, System.EventArgs e)
        {

            Process p = new System.Diagnostics.Process();

            string Nama = "Laporan Executive Summary";
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

            string strSql = "SELECT * FROM ISC064_MARKETINGJUAL..LapPDF WHERE AttachmentID  = '" + NoAttachment + "'";
            DataTable rs = Db.Rs(strSql);

            string nfilename = "ExecutiveSummary" + NoAttachment + ".pdf";

            //update filename
            Db.Execute("UPDATE ISC064_MARKETINGJUAL..LapPDF SET FileName= '" + nfilename + "' WHERE AttachmentID = " + NoAttachment);


            //folder untuk menyimpan file pdf
            string save = Mi.PathFilePDFReport + "ExecutiveSummary" + rs.Rows[0]["AttachmentID"] + ".pdf";

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
            string link = Mi.PathAlamatWeb + "marketingjual/LaporanPDF/PDFExecutiveSummary.aspx?project=" + Project + "&pers=" + pers.SelectedValue;

            //update link
            Db.Execute("UPDATE ISC064_MARKETINGJUAL..LapPDF SET Link= '" + link + "' WHERE AttachmentID = " + NoAttachment);

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
        protected void Fill()
        {
            Header();
            Marketing1();

            Label l = new Label();
            l.Text = "<br />";
            rpt.Controls.Add(l);

            Penjualan();

            Label a = new Label();
            a.Text = "<br />";
            rpt.Controls.Add(a);

            Collection();

            Label b = new Label();
            b.Text = "<br />";
            rpt.Controls.Add(b);

            CashIn();
        }

        protected void Header()
        {
            Label l;

            l = new Label();
            l.Text = Mi.Pt;
            l.Text += "<h1 class='title'>Management Report</h1>";
            l.Text += "<h2>per " + Cf.Day(DateTime.Today) + "</h2>";
            l.Text += "Laporan dihasilkan pada hari : " + Cf.IndoWeek(DateTime.Today)
                + ", " + Cf.Date(DateTime.Now)
                + " dari workstation : " + Act.IP
                + " dan username : " + Act.UserID
                + "<br /><br />";
            rpt.Controls.Add(l);
        }

        protected void Marketing1()
        {
            Table tb;
            TableRow tr;
            TableHeaderCell hc;
            TableCell c;

            tb = new Table();
            tb.CssClass = "tb blue-skin";
            tb.CellSpacing = 1;

            tr = new TableRow();
            tb.Rows.Add(tr);

            c = new TableCell();
            c.Text = "<b>A. SUMMARY STOCK</b>";
            c.ColumnSpan = 10;
            tr.Cells.Add(c);

            tr = new TableRow();
            tb.Rows.Add(tr);

            hc = new TableHeaderCell();
            hc.Text = "Tower";
            hc.RowSpan = 2;
            hc.ForeColor = Color.White;
            hc.Attributes["style"] = "background-color:#1E90FF";
            hc.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.Text = "Tipe";
            hc.RowSpan = 2;
            hc.ForeColor = Color.White;
            hc.Attributes["style"] = "background-color:#1E90FF";
            hc.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.Text = "Available";
            hc.ColumnSpan = 2;
            hc.ForeColor = Color.White;
            hc.Attributes["style"] = "background-color:#1E90FF";
            hc.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.Text = "Sold";
            hc.ColumnSpan = 2;
            hc.ForeColor = Color.White;
            hc.Attributes["style"] = "background-color:#1E90FF";
            hc.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.Text = "Hold Internal";
            hc.ColumnSpan = 2;
            hc.ForeColor = Color.White;
            hc.Attributes["style"] = "background-color:#1E90FF";
            hc.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.Text = "Total";
            hc.ColumnSpan = 2;
            hc.ForeColor = Color.White;
            hc.Attributes["style"] = "background-color:#1E90FF";
            hc.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(hc);

            tr = new TableRow();
            tb.Rows.Add(tr);

            hc = new TableHeaderCell();
            hc.Text = "Unit";
            hc.ForeColor = Color.White;
            hc.Attributes["style"] = "background-color:#1E90FF";
            hc.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.Text = "%";
            hc.ForeColor = Color.White;
            hc.Attributes["style"] = "background-color:#1E90FF";
            hc.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.Text = "Unit";
            hc.ForeColor = Color.White;
            hc.Attributes["style"] = "background-color:#1E90FF";
            hc.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.Text = "%";
            hc.ForeColor = Color.White;
            hc.Attributes["style"] = "background-color:#1E90FF";
            hc.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.Text = "Unit";
            hc.ForeColor = Color.White;
            hc.Attributes["style"] = "background-color:#1E90FF";
            hc.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.Text = "%";
            hc.ForeColor = Color.White;
            hc.Attributes["style"] = "background-color:#1E90FF";
            hc.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.Text = "Unit";
            hc.HorizontalAlign = HorizontalAlign.Center;
            hc.ForeColor = Color.White;
            hc.Attributes["style"] = "background-color:#1E90FF";
            tr.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.Text = "%";
            hc.ForeColor = Color.White;
            hc.Attributes["style"] = "background-color:#1E90FF";
            hc.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(hc);

            rpt.Controls.Add(tb);

            string Project = "WHERE Project IN (" + Act.ProjectListSql + ")";
            if (project.SelectedValue != "SEMUA") Project = "WHERE Project = '" + project.SelectedValue + "'";
            DataTable rs = Db.Rs("SELECT DISTINCT Lokasi FROM MS_UNIT " + Project + "");

            decimal t1 = 0, t2 = 0, t3 = 0, t4 = 0, t5 = 0, t6 = 0, t7 = 0, t8 = 0;


            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected)
                    break;

                tr = new TableRow();
                tb.Rows.Add(tr);

                c = new TableCell();
                c.Text = rs.Rows[i]["Lokasi"].ToString();
                c.HorizontalAlign = HorizontalAlign.Center;
                tr.Cells.Add(c);

                DataTable r = Db.Rs("SELECT DISTINCT(JENIS) From MS_UNIT WHERE Lokasi = '" + rs.Rows[i]["Lokasi"].ToString() + "' " + Project.Replace("WHERE", "AND") + "");

                for (int j = 0; j < r.Rows.Count; j++)
                {
                    if (!Response.IsClientConnected) break;

                    if (j > 0)
                    {
                        tr = new TableRow();
                        tb.Rows.Add(tr);

                        c = new TableCell();
                        c.Text = "";
                        c.HorizontalAlign = HorizontalAlign.Center;
                        tr.Cells.Add(c);
                    }
                    c = new TableCell();
                    c.Text = r.Rows[j]["Jenis"].ToString();
                    c.HorizontalAlign = HorizontalAlign.Center;
                    tr.Cells.Add(c);

                    c = new TableCell();
                    decimal b = Nilai(rs.Rows[i]["Lokasi"].ToString(), r.Rows[j]["Jenis"].ToString(), "AVAILABLE", project.SelectedValue);
                    c.Text = Cf.Num(b);
                    c.HorizontalAlign = HorizontalAlign.Center;
                    tr.Cells.Add(c);

                    c = new TableCell();
                    decimal d = Persen(rs.Rows[i]["Lokasi"].ToString(), r.Rows[j]["Jenis"].ToString(), "AVAILABLE", project.SelectedValue);
                    c.Text = Cf.Num(Math.Round(d, 2)) + " %";
                    c.HorizontalAlign = HorizontalAlign.Center;
                    tr.Cells.Add(c);

                    c = new TableCell();
                    decimal e = Nilai(rs.Rows[i]["Lokasi"].ToString(), r.Rows[j]["Jenis"].ToString(), "SOLD", project.SelectedValue);
                    c.Text = Cf.Num(e);
                    c.HorizontalAlign = HorizontalAlign.Center;
                    tr.Cells.Add(c);

                    c = new TableCell();
                    decimal f = Persen(rs.Rows[i]["Lokasi"].ToString(), r.Rows[j]["Jenis"].ToString(), "SOLD", project.SelectedValue);
                    c.Text = Cf.Num(Math.Round(f, 2)) + " %";
                    c.HorizontalAlign = HorizontalAlign.Center;
                    tr.Cells.Add(c);

                    c = new TableCell();
                    decimal g = Nilai(rs.Rows[i]["Lokasi"].ToString(), r.Rows[j]["Jenis"].ToString(), "HOLD", project.SelectedValue);
                    c.Text = Cf.Num(g);
                    c.HorizontalAlign = HorizontalAlign.Center;
                    tr.Cells.Add(c);

                    c = new TableCell();
                    decimal h = Persen(rs.Rows[i]["Lokasi"].ToString(), r.Rows[j]["Jenis"].ToString(), "HOLD", project.SelectedValue);
                    c.Text = Cf.Num(Math.Round(h, 2)) + " %";
                    c.HorizontalAlign = HorizontalAlign.Center;
                    tr.Cells.Add(c);

                    c = new TableCell();
                    decimal k = Nilai(rs.Rows[i]["Lokasi"].ToString(), r.Rows[j]["Jenis"].ToString(), "TOTAL", project.SelectedValue);
                    c.Text = Cf.Num(k);
                    c.HorizontalAlign = HorizontalAlign.Center;
                    tr.Cells.Add(c);

                    c = new TableCell();
                    decimal l = Persen(rs.Rows[i]["Lokasi"].ToString(), r.Rows[j]["Jenis"].ToString(), "TOTAL", project.SelectedValue);
                    c.Text = Cf.Num(Math.Round(l, 2)) + " %";
                    c.HorizontalAlign = HorizontalAlign.Center;
                    tr.Cells.Add(c);

                    t7 += k;
                    t1 += b;
                    t2 = t1 / t7 * 100;
                    t3 += e;
                    t4 = t3 / t7 * 100;
                    t5 += g;
                    t6 = t5 / t7 * 100;
                    t8 = t2 + t4 + t6;
                }
            }

            tr = new TableRow();
            tb.Rows.Add(tr);

            c = Rpt.Foot();
            c.Text = "Total";
            c.ColumnSpan = 2;
            c.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = t1.ToString();
            c.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(Math.Round(t2, 2)) + " %";
            c.HorizontalAlign = HorizontalAlign.Right;
            tr.Cells.Add(c);
            c = Rpt.Foot();
            c.Text = t3.ToString();
            c.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(Math.Round(t4, 2)) + " %";
            c.HorizontalAlign = HorizontalAlign.Right;
            tr.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = t5.ToString();
            c.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(Math.Round(t6, 2)) + " %";
            c.HorizontalAlign = HorizontalAlign.Right;
            tr.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = t7.ToString();
            c.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(Math.Round(t8, 2)) + " %";
            c.HorizontalAlign = HorizontalAlign.Right;
            tr.Cells.Add(c);
        }

        protected void Penjualan()
        {
            Table tb;
            TableRow tr;
            TableHeaderCell hc;
            TableCell c;

            tb = new Table();
            tb.CssClass = "tb blue-skin";
            tb.CellSpacing = 1;

            tr = new TableRow();
            tb.Rows.Add(tr);

            c = new TableCell();
            c.Text = "<b>B. PENJUALAN</b>";
            c.ColumnSpan = 9;
            tr.Cells.Add(c);

            tr = new TableRow();
            tb.Rows.Add(tr);

            hc = new TableHeaderCell();
            hc.Text = "Tower";
            hc.ForeColor = Color.White;
            hc.Attributes["style"] = "background-color:#1E90FF";
            hc.RowSpan = 2;
            hc.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.Text = "Tipe";
            hc.RowSpan = 2;
            hc.ForeColor = Color.White;
            hc.Attributes["style"] = "background-color:#1E90FF";
            hc.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.Text = "Available";
            hc.ForeColor = Color.White;
            hc.Attributes["style"] = "background-color:#1E90FF";
            hc.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.Text = "Penjualan";
            hc.ForeColor = Color.White;
            hc.Attributes["style"] = "background-color:#1E90FF";
            hc.ColumnSpan = 2;
            hc.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.Text = "Batal";
            hc.ColumnSpan = 2;
            hc.ForeColor = Color.White;
            hc.Attributes["style"] = "background-color:#1E90FF";
            hc.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.Text = "Penjualan Bersih";
            hc.ColumnSpan = 2;
            hc.ForeColor = Color.White;
            hc.Attributes["style"] = "background-color:#1E90FF";
            hc.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(hc);
            tr = new TableRow();
            tb.Rows.Add(tr);

            hc = new TableHeaderCell();
            hc.Text = "Unit";
            hc.ForeColor = Color.White;
            hc.Attributes["style"] = "background-color:#1E90FF";
            hc.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.Text = "Unit";
            hc.ForeColor = Color.White;
            hc.Attributes["style"] = "background-color:#1E90FF";
            hc.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.Text = "Nilai Kontrak";
            hc.ForeColor = Color.White;
            hc.Attributes["style"] = "background-color:#1E90FF";
            hc.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.Text = "Unit";
            hc.ForeColor = Color.White;
            hc.Attributes["style"] = "background-color:#1E90FF";
            hc.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.Text = "Nilai Kontrak";
            hc.ForeColor = Color.White;
            hc.Attributes["style"] = "background-color:#1E90FF";
            hc.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.Text = "Unit";
            hc.ForeColor = Color.White;
            hc.Attributes["style"] = "background-color:#1E90FF";
            hc.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.Text = "Nilai Kontrak";
            hc.ForeColor = Color.White;
            hc.Attributes["style"] = "background-color:#1E90FF";
            hc.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(hc);
            rpt.Controls.Add(tb);

            string Project = "WHERE Project IN (" + Act.ProjectListSql + ")";
            if (project.SelectedValue != "SEMUA") Project = "WHERE Project = '" + project.SelectedValue + "'";
            string Perusahaan = "";
            if (pers.SelectedValue != "SEMUA") Perusahaan = "AND Project = '" + project.SelectedValue + "'";
            DataTable rs = Db.Rs("SELECT DISTINCT Lokasi FROM MS_UNIT " + Project + "");

            decimal t1 = 0, t2 = 0, t3 = 0, t4 = 0, t5 = 0, t6 = 0, t7 = 0;

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected)
                    break;

                tr = new TableRow();
                tb.Rows.Add(tr);

                c = new TableCell();
                c.Text = rs.Rows[i]["Lokasi"].ToString();
                c.HorizontalAlign = HorizontalAlign.Center;
                tr.Cells.Add(c);

                DataTable r = Db.Rs("SELECT DISTINCT(JENIS) From MS_UNIT WHERE Lokasi = '" + rs.Rows[i]["Lokasi"].ToString() + "' " + Project.Replace("WHERE", "AND") + "");

                for (int j = 0; j < r.Rows.Count; j++)
                {
                    if (!Response.IsClientConnected) break;

                    if (j > 0)
                    {
                        tr = new TableRow();
                        tb.Rows.Add(tr);
                        c = new TableCell();
                        c.Text = rs.Rows[i]["Lokasi"].ToString();
                        c.HorizontalAlign = HorizontalAlign.Center;
                        tr.Cells.Add(c);
                    }
                    c = new TableCell();
                    c.Text = r.Rows[j]["Jenis"].ToString();
                    c.HorizontalAlign = HorizontalAlign.Center;
                    tr.Cells.Add(c);

                    c = new TableCell();
                    decimal b = Nilai(rs.Rows[i]["Lokasi"].ToString(), r.Rows[j]["Jenis"].ToString(), "AVAILABLE", project.SelectedValue);
                    c.Text = Cf.Num(b);
                    c.HorizontalAlign = HorizontalAlign.Center;
                    tr.Cells.Add(c);

                    c = new TableCell();
                    decimal e = Nilai(rs.Rows[i]["Lokasi"].ToString(), r.Rows[j]["Jenis"].ToString(), "SOLD", project.SelectedValue);
                    c.Text = Cf.Num(e);
                    c.HorizontalAlign = HorizontalAlign.Center;
                    tr.Cells.Add(c);

                    c = new TableCell();
                    decimal g = NilaiKontrak(rs.Rows[i]["Lokasi"].ToString(), r.Rows[j]["Jenis"].ToString(), "SOLD", project.SelectedValue, pers.SelectedValue);
                    c.Text = Cf.Num(Math.Round(g));
                    c.HorizontalAlign = HorizontalAlign.Center;
                    tr.Cells.Add(c);

                    c = new TableCell();
                    decimal h = Nilai(rs.Rows[i]["Lokasi"].ToString(), r.Rows[j]["Jenis"].ToString(), "BATAL", project.SelectedValue);
                    c.Text = Cf.Num(h);
                    c.HorizontalAlign = HorizontalAlign.Center;
                    tr.Cells.Add(c);

                    c = new TableCell();
                    decimal k = NilaiKontrak(rs.Rows[i]["Lokasi"].ToString(), r.Rows[j]["Jenis"].ToString(), "BATAL", project.SelectedValue, pers.SelectedValue);
                    c.Text = Cf.Num(Math.Round(k));
                    c.HorizontalAlign = HorizontalAlign.Center;
                    tr.Cells.Add(c);

                    c = new TableCell();
                    decimal l = e - h;
                    c.Text = Cf.Num(Math.Round(l));
                    c.HorizontalAlign = HorizontalAlign.Center;
                    tr.Cells.Add(c);

                    c = new TableCell();
                    decimal m = g - k;
                    c.Text = Cf.Num(Math.Round(m));
                    c.HorizontalAlign = HorizontalAlign.Center;
                    tr.Cells.Add(c);

                    t1 += b;
                    t2 += e;
                    t3 += g;
                    t4 += h;
                    t5 += k;
                    t6 += l;
                    t7 += m;
                }
            }

            tr = new TableRow();
            tb.Rows.Add(tr);

            c = Rpt.Foot();
            c.Text = "Total";
            c.ColumnSpan = 2;
            c.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = t1.ToString();
            c.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = t2.ToString();
            c.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(Math.Round(t3));
            c.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = t4.ToString();
            c.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(Math.Round(t5));
            c.HorizontalAlign = HorizontalAlign.Right;
            tr.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(Math.Round(t6));
            c.HorizontalAlign = HorizontalAlign.Right;
            tr.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(Math.Round(t7));
            c.HorizontalAlign = HorizontalAlign.Right;
            tr.Cells.Add(c);
        }

        protected void Collection()
        {
            string Project = " AND b.Project IN (" + Act.ProjectListSql + ")";
            if (project.SelectedValue != "SEMUA") Project = " AND b.Project = '" + project.SelectedValue + "'";

            string Perusahaan = "";
            if (pers.SelectedValue != "SEMUA") Perusahaan = " AND b.Project = '" + project.SelectedValue + "'";

            Table tb;
            TableRow tr;
            TableHeaderCell hc;
            TableCell c;

            tb = new Table();
            tb.CssClass = "tb blue-skin";
            tb.CellSpacing = 1;

            tr = new TableRow();
            tb.Rows.Add(tr);

            c = new TableCell();
            c.Text = "<b>C. COLLECTION</b>";
            c.ColumnSpan = 6;
            tr.Cells.Add(c);

            tr = new TableRow();
            tb.Rows.Add(tr);

            hc = new TableHeaderCell();
            hc.Text = "Nilai Kontrak";
            hc.ForeColor = Color.White;
            hc.Attributes["style"] = "background-color:#1E90FF";
            hc.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.Text = "Belum Jatuh Tempo";
            hc.ForeColor = Color.White;
            hc.Attributes["style"] = "background-color:#1E90FF";
            hc.HorizontalAlign = HorizontalAlign.Right;
            tr.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.Text = "Jatuh Tempo";
            hc.ForeColor = Color.White;
            hc.Attributes["style"] = "background-color:#1E90FF";
            hc.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.Text = "Tunggakan";
            hc.ForeColor = Color.White;
            hc.Attributes["style"] = "background-color:#1E90FF";
            hc.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.Text = "Pelunasan Piutang";
            hc.ForeColor = Color.White;
            hc.Attributes["style"] = "background-color:#1E90FF";
            hc.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.Text = "Sisa Tagihan";
            hc.ForeColor = Color.White;
            hc.Attributes["style"] = "background-color:#1E90FF";
            hc.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(hc);

            tr = new TableRow();
            tb.Rows.Add(tr);

            decimal NilaiKontrak = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiKontrak),0) FROM MS_KONTRAK b WHERE Status = 'A'" + Project + Perusahaan + "");
            c = new TableCell();
            c.Text = Cf.Num(Math.Round(NilaiKontrak));
            c.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(c);

            decimal blmJT = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiTagihan),0) FROM MS_TAGIHAN a INNER JOIN MS_KONTRAK b ON a.NoKontrak = b.NoKontrak WHERE b.Status = 'A' AND a.Tipe != 'ADM' AND CONVERT(VARCHAR,a.TglJT,112) > '" + Cf.Tgl112(DateTime.Today) + "'" + Project + Perusahaan + "");
            decimal JT = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiTagihan),0) FROM MS_TAGIHAN a INNER JOIN MS_KONTRAK b ON a.NoKontrak = b.NoKontrak WHERE b.Status = 'A' AND a.Tipe != 'ADM' AND CONVERT(VARCHAR,a.TglJT,112) = '" + Cf.Tgl112(DateTime.Today) + "'" + Project + Perusahaan + "");
            decimal Tunggakan = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiTagihan),0) FROM MS_TAGIHAN a INNER JOIN MS_KONTRAK b ON a.NoKontrak = b.NoKontrak WHERE b.Status = 'A' AND a.Tipe != 'ADM' AND CONVERT(VARCHAR,a.TglJT,112) < '" + Cf.Tgl112(DateTime.Today) + "'" + Project + Perusahaan + "");
            c = new TableCell();

            c.Text = Cf.Num(blmJT);
            c.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(c);

            c = new TableCell();
            c.Text = Cf.Num(JT);
            c.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(c);

            c = new TableCell();
            c.Text = Cf.Num(Tunggakan);
            c.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(c);

            decimal Pembayaran = Db.SingleDecimal("SELECT ISNULL(SUM(a.NilaiPelunasan),0) FROM MS_PELUNASAN a INNER JOIN ISC064_Financear..MS_TTS c ON a.NoTTS = c.NoTTS INNER JOIN MS_KONTRAK b ON a.NoKontrak = b.NoKontrak WHERE c.Status = 'POST' AND b.Status = 'A'" + Project + Perusahaan + "");

            c = new TableCell();
            c.Text = Cf.Num(Pembayaran);
            c.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(c);

            decimal SisaTagihan = NilaiKontrak - Pembayaran;

            c = new TableCell();
            c.Text = Cf.Num(Math.Round(SisaTagihan));
            c.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(c);

            rpt.Controls.Add(tb);
        }

        protected void CashIn()
        {
            string Project = " AND b.Project IN (" + Act.ProjectListSql + ")";
            if (project.SelectedValue != "SEMUA") Project = " AND b.Project = '" + project.SelectedValue + "'";

            string Perusahaan = "";
            if (pers.SelectedValue != "SEMUA") Perusahaan = " AND b.Project = '" + project.SelectedValue + "'";

            Table tb;
            TableRow tr;
            TableHeaderCell hc;
            TableCell c;

            tb = new Table();
            tb.CssClass = "tb blue-skin";
            tb.CellSpacing = 1;

            tr = new TableRow();
            tb.Rows.Add(tr);

            c = new TableCell();
            c.Text = "<b>D. CASH IN</b>";
            c.ColumnSpan = 4;
            tr.Cells.Add(c);

            tr = new TableRow();
            tb.Rows.Add(tr);

            hc = new TableHeaderCell();
            hc.Text = "Piutang Pokok";
            hc.ForeColor = Color.White;
            hc.Attributes["style"] = "background-color:#1E90FF";
            hc.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.Text = "Piutang ADM";
            hc.ForeColor = Color.White;
            hc.Attributes["style"] = "background-color:#1E90FF";
            hc.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.Text = "Pelunasan<br/>(Kuitansi & Memo)";
            hc.HorizontalAlign = HorizontalAlign.Center;
            hc.ForeColor = Color.White;
            hc.Attributes["style"] = "background-color:#1E90FF";
            tr.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.Text = "Cash In<br/>(Kuitansi)";
            hc.ForeColor = Color.White;
            hc.Attributes["style"] = "background-color:#1E90FF";
            hc.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(hc);

            tr = new TableRow();
            tb.Rows.Add(tr);

            decimal Piutang = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiTagihan),0) FROM MS_TAGIHAN a INNER JOIN MS_KONTRAK b ON a.NoKontrak = b.NoKontrak WHERE b.Status = 'A' AND a.Tipe != 'ADM'" + Project + Perusahaan + "");
            c = new TableCell();
            c.Text = Cf.Num(Piutang);
            c.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(c);

            decimal PiutangADM = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiTagihan),0) FROM MS_TAGIHAN a INNER JOIN MS_KONTRAK b ON a.NoKontrak = b.NoKontrak WHERE b.Status = 'A' AND a.Tipe = 'ADM'" + Project + Perusahaan + "");
            c = new TableCell();
            c.Text = Cf.Num(PiutangADM);
            c.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(c);

            decimal Pembayaran = Db.SingleDecimal("SELECT ISNULL(SUM(a.NilaiPelunasan),0) FROM MS_PELUNASAN a INNER JOIN " + Mi.DbPrefix + "FINANCEAR..MS_TTS c ON a.NoTTS = c.NoTTS INNER JOIN MS_KONTRAK b ON a.NoKontrak = b.NoKontrak WHERE c.Status = 'POST' AND b.Status = 'A'" + Project + Perusahaan + "");
            c = new TableCell();
            c.Text = Cf.Num(Pembayaran);
            c.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(c);

            decimal CashIn = Db.SingleDecimal("SELECT ISNULL(SUM(a.NilaiPelunasan),0) FROM MS_PELUNASAN a INNER JOIN " + Mi.DbPrefix + "FINANCEAR..MS_TTS c ON a.NoTTS = c.NoTTS INNER JOIN MS_KONTRAK b ON a.NoKontrak = b.NoKontrak WHERE c.Status = 'POST' AND b.Status = 'A'" + Project + Perusahaan + "");
            CashIn -= Db.SingleDecimal("SELECT ISNULL(SUM(a.Total),0) FROM " + Mi.DbPrefix + "FINANCEAR..MS_MEMO a INNER JOIN MS_KONTRAK b ON a.Ref = b.NoKontrak WHERE a.Status = 'POST' AND b.Status = 'A'" + Project + Perusahaan + "");
            c = new TableCell();
            c.Text = Cf.Num(CashIn);
            c.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(c);

            rpt.Controls.Add(tb);
        }


        protected decimal NilaiKontrak(string Lokasi, string Jenis, string Tipe, string Project, string Perusahaan)
        {
            decimal t = 0;

            string nProject = " AND Project IN (" + Act.ProjectListSql + ")";
            if (Project != "SEMUA") nProject = " AND Project = '" + Project + "'";

            string nPerusahaan = "";
            if (Perusahaan != "SEMUA") nPerusahaan = " AND Pers = '" + Perusahaan + "'";

            if (Tipe == "SOLD")
            {
                t = Db.SingleDecimal(
                    "SELECT ISNULL(SUM(NilaiKontrak), 0) FROM MS_KONTRAK WHERE Lokasi = '" + Lokasi + "'"
                    + " AND Jenis = '" + Jenis + "'" + nProject + nPerusahaan + " AND Status = 'A'");
            }
            else if (Tipe == "BATAL")
            {
                t = Db.SingleDecimal(
                    "SELECT ISNULL(SUM(NilaiKontrak), 0) FROM MS_KONTRAK WHERE Lokasi = '" + Lokasi + "'"
                    + " AND Jenis = '" + Jenis + "'" + nProject + nPerusahaan + " AND Status = 'B'");
            }
            return t;
        }

        protected decimal TotalLunas(string Lokasi)
        {
            decimal d = Db.SingleDecimal(
                "SELECT ISNULL(SUM(NilaiPelunasan), 0) FROM MS_PELUNASAN a"
                + " INNER JOIN MS_KONTRAK b ON a.NoKontrak = b.NoKontrak"
                + " WHERE b.Lokasi = '" + Lokasi + "' AND Project = '" + project.SelectedValue + "' AND b.Status = 'A'"
                );

            return d;
        }

        protected decimal TotalLunasCair(string Lokasi)
        {
            decimal d = Db.SingleDecimal(
                "SELECT ISNULL(SUM(NilaiPelunasan), 0) FROM MS_PELUNASAN a"
                + " INNER JOIN MS_KONTRAK b ON a.NoKontrak = b.NoKontrak"
                + " WHERE b.Lokasi = '" + Lokasi + "' AND Project = '" + project.SelectedValue + "' AND b.Status = 'A'"
                + " AND a.SudahCair = 1"
                );

            return d;
        }

        protected decimal NilaiReservasi(string Lokasi)
        {
            decimal d = Db.SingleDecimal(
                "SELECT ISNULL(SUM(PriceList), 0) FROM MS_UNIT WHERE NoStock IN ("
                + "SELECT NoStock FROM MS_RESERVASI WHERE Lokasi = '" + Lokasi + "' AND Project = '" + project.SelectedValue + "' AND Status = 'A')");

            return d;
        }
        protected decimal Nilai(string Lokasi, string Jenis, string Tipe, string Project)
        {
            decimal t = 0;
            string nProject = " AND Project IN (" + Act.ProjectListSql + ")";
            if (Project != "SEMUA") nProject = " AND Project = '" + Project + "'";
            if (Tipe == "AVAILABLE")
            {
                t = Db.SingleDecimal(
                    "SELECT COUNT(*)"
                    + " FROM MS_UNIT"
                    + " WHERE Jenis = '" + Jenis + "'"
                    + " AND Lokasi = '" + Lokasi + "'"
                    + nProject
                    + " AND Status = 'A'"
                    );
            }
            else if (Tipe == "SOLD")
            {
                t = Db.SingleDecimal(
                    "SELECT COUNT(*)"
                    + " FROM MS_UNIT"
                    + " WHERE Jenis = '" + Jenis + "'"
                    + " AND Lokasi = '" + Lokasi + "'"
                    + nProject
                    + " AND Status = 'B'"
                    + " AND (SELECT COUNT(*) FROM MS_KONTRAK WHERE NoUnit = MS_UNIT.NoUnit AND Status = 'A') > 0"
                    );
            }
            else if (Tipe == "HOLD")
            {
                t = Db.SingleDecimal(
                    "SELECT COUNT(*)"
                    + " FROM MS_UNIT"
                    + " WHERE Jenis = '" + Jenis + "'"
                    + " AND Lokasi = '" + Lokasi + "'"
                    + nProject
                    + " AND Status = 'B'"
                    + " AND (SELECT COUNT(*) FROM MS_KONTRAK WHERE NoUnit = MS_UNIT.NoUnit AND Status = 'A') = 0"
                    );
            }
            else if (Tipe == "TOTAL")
            {
                t = Db.SingleDecimal(
                    "SELECT COUNT(*)"
                    + " FROM MS_UNIT"
                    + " WHERE Jenis = '" + Jenis + "'"
                    + " AND Lokasi = '" + Lokasi + "'"
                    + nProject
                    );
            }

            else if (Tipe == "TOTALJUAL")
            {
                t = Db.SingleDecimal(
                    "SELECT COUNT(*)"
                    + " FROM MS_UNIT"
                    + " WHERE Jenis = '" + Jenis + "'"
                    + " AND Lokasi = '" + Lokasi + "'"
                    + nProject
                    + " AND Status = 'B'"
                    + " AND (SELECT COUNT(*) FROM MS_KONTRAK WHERE NoUnit = MS_UNIT.NoUnit) = 0"
                    );
            }

            else if (Tipe == "BATAL")
            {
                t = Db.SingleDecimal(
                    "SELECT COUNT(*)"
                    + " FROM MS_UNIT"
                    + " WHERE Jenis = '" + Jenis + "'"
                    + " AND Lokasi = '" + Lokasi + "'"
                    + nProject
                    + " AND Status = 'A'"
                    + " AND (SELECT COUNT(*) FROM MS_KONTRAK WHERE NoUnit = MS_UNIT.NoUnit AND Status = 'B') > 0"
                    );
            }

            return t;
        }

        protected decimal Persen(string Lokasi, string Jenis, string Tipe, string Project)
        {
            decimal t = 0;
            string nProject = " AND Project IN (" + Act.ProjectListSql + ")";
            if (Project != "SEMUA") nProject = " AND Project = '" + Project + "'";
            decimal tot = Db.SingleDecimal(
                    "SELECT COUNT(*)"
                    + " FROM MS_UNIT"
                    + " WHERE Lokasi = '" + Lokasi + "'"
                    + " AND Jenis = '" + Jenis + "'"
                    + nProject
                    );

            if (Tipe == "AVAILABLE")
            {
                t = Db.SingleDecimal(
                    "SELECT COUNT(*)"
                    + " FROM MS_UNIT"
                    + " WHERE Jenis = '" + Jenis + "'"
                    + " AND Lokasi = '" + Lokasi + "'"
                    + nProject
                    + " AND Status = 'A'"
                    ) / tot * 100;
            }
            else if (Tipe == "SOLD")
            {
                t = Db.SingleDecimal(
                    "SELECT COUNT(*)"
                    + " FROM MS_UNIT"
                    + " WHERE Jenis = '" + Jenis + "'"
                    + " AND Lokasi = '" + Lokasi + "'"
                    + nProject
                    + " AND Status = 'B'"
                    + " AND (SELECT COUNT(*) FROM MS_KONTRAK WHERE NoUnit = MS_UNIT.NoUnit AND Status = 'A') > 0"
                    ) / tot * 100;
            }
            else if (Tipe == "HOLD")
            {
                t = Db.SingleDecimal(
                    "SELECT COUNT(*)"
                    + " FROM MS_UNIT"
                    + " WHERE Jenis = '" + Jenis + "'"
                    + " AND Lokasi = '" + Lokasi + "'"
                    + nProject
                    + " AND Status = 'B'"
                    + " AND (SELECT COUNT(*) FROM MS_KONTRAK WHERE NoUnit = MS_UNIT.NoUnit AND Status = 'A') = 0"
                    ) / tot * 100;
            }
            else if (Tipe == "TOTAL")
            {
                t = Db.SingleDecimal(
                    "SELECT COUNT(*)"
                    + " FROM MS_UNIT"
                    + " WHERE Jenis = '" + Jenis + "'"
                    + nProject
                    + " AND Lokasi = '" + Lokasi + "'"
                    ) / tot * 100;
            }
            else if (Tipe == "TITIPJUAL")
            {
                t = Db.SingleDecimal(
                    "SELECT COUNT(*)"
                    + " FROM MS_UNIT"
                    + " WHERE Jenis = '" + Jenis + "'"
                    + " AND Lokasi = '" + Lokasi + "'"
                    + nProject
                    + " AND Status = 'B'"
                    + " AND (SELECT COUNT(*) FROM MS_KONTRAK WHERE NoUnit = MS_UNIT.NoUnit AND Status = 'A' AND TitipJual = 1) > 0"
                    ) / tot * 100;
            }

            return t;
        }
        protected void scr_Click(object sender, System.EventArgs e)
        {
            Report();
        }

        protected void xls_Click(object sender, System.EventArgs e)
        {
            Report();
            Rpt.ToExcel(this, rpt);
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
            pers.Items.Clear();
            pers.Items.Add("SEMUA");
            if (project.SelectedValue != "SEMUA")
            {
                string strSql = "SELECT * FROM ISC064_SECURITY..PTSec A "
                + "INNER JOIN ISC064_SECURITY..REF_PERS B ON A.Pers = B.Pers "
                + "INNER JOIN ISC064_SECURITY..REF_PROJECT C ON A.Pers = C.Pers "
                + " WHERE A.UserID='" + Act.UserID + "' AND C.Project ='" + project.SelectedValue + "'  AND A.Granted = 1";

                DataTable rs = Db.Rs(strSql);
                for (int i = 0; i < rs.Rows.Count; i++)
                {
                    string t = rs.Rows[i]["Nama"].ToString();
                    string v = rs.Rows[i]["Pers"].ToString();
                    pers.Items.Add(new ListItem(t, v));
                }
            }
        }
    }
}

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
    public partial class DendaCustomer : System.Web.UI.Page
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
            tgl.Text = Cf.Day(DateTime.Now);
            string Project = project.SelectedValue == "SEMUA" ? "Project IN (" + Act.ProjectListSql + ")" : "Project = '" + project.SelectedValue + "'";
            DataTable rs;
            rs = Db.Rs("SELECT DISTINCT Lokasi FROM ISC064_MARKETINGJUAL..MS_KONTRAK WHERE " + Project + " ORDER BY Lokasi");
            for (int i = 0; i < rs.Rows.Count; i++)
                lokasi.Items.Add(new ListItem(rs.Rows[i][0].ToString()));

            lokasi.SelectedIndex = 0;
        }

        private bool valid()
        {
            string s = "";
            bool x = true;

            if (!Cf.isTgl(tgl))
            {
                x = false;
                if (s == "") s = tgl.ID;
                tglc.Text = "Tanggal";
            }
            else
                tglc.Text = "";

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
                //Rpt.ToExcel(this,rpt);
                Rpt.ToExcel(this, headReport, rpt);
            }
        }
        protected void pdf_Click(object sender, System.EventArgs e)
        {
            Process p = new System.Diagnostics.Process();

            string Nama = "Laporan Denda Customer";
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
                    + ",'" + Convert.ToDateTime(tgl.Text) + "'"
                    + ",'" + null + "'"
                    );

            //get nomor customer terbaru
            int NoAttachment = Db.SingleInteger(
                "SELECT TOP 1 AttachmentID FROM ISC064_FINANCEAR..LapPDF ORDER BY AttachmentID DESC");

            string strSql = "SELECT * FROM ISC064_FINANCEAR..LapPDF WHERE AttachmentID  = '" + NoAttachment + "'";
            DataTable rs = Db.Rs(strSql);

            string nfilename = "DendaCustomer" + NoAttachment + ".pdf";

            //update filename
            Db.Execute("UPDATE ISC064_FINANCEAR..LapPDF SET FileName= '" + nfilename + "' WHERE AttachmentID = " + NoAttachment);


            //folder untuk menyimpan file pdf
            string save = Mi.PathFilePDFReport + "DendaCustomer" + rs.Rows[0]["AttachmentID"] + ".pdf";

            //parameter                 
            string TglAsOf = tgl.Text;

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
            string link = Mi.PathAlamatWeb + "collection/LaporanPDF/PDFDendaCustomer.aspx?id=" + rs.Rows[0]["AttachmentID"]
                + "&lokasi=" + lokasi.SelectedValue
                + "&userid=" + UserID
                + "&project=" + Project
                + "&pers=" + pers.SelectedValue
                + "&asof=" + Cf.Tgl112(Convert.ToDateTime(tgl.Text))
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

            System.Text.StringBuilder x = new System.Text.StringBuilder();

            Rpt.Judul(x, comp, judul);
            if (lokasi.SelectedIndex > 0)
                Rpt.SubJudul(x, " Lokasi <b style='padding-left:38px'>:</b> " + lokasi.SelectedValue);
            else
                Rpt.SubJudul(x, " Lokasi <b style='padding-left:38px'>:</b> SEMUA");
            Rpt.SubJudul(x, " As Of <b style='padding-left:44px'>:</b> " + tgl.Text);
            Rpt.SubJudul(x, " Perusahaan <b style='padding-left:4px'>:</b> " + pers.SelectedItem.Text);
            Rpt.SubJudul(x, " Project <b style='padding-left:30px'>:</b> " + project.SelectedValue);

            string legend = "<br />Status : A = Aktif / B = Batal.<br />";
            Rpt.HeaderReport(headReport, legend, x);

            Fill();
        }


        private void Fill()
        {
            decimal total = 0;
            decimal t2 = 0;
            decimal t1 = 0;
            decimal t3 = 0;
            decimal t4 = 0;
            decimal t5 = 0;

            DateTime tanggal = Convert.ToDateTime(tgl.Text);

            string Lokasi = "";
            if (lokasi.SelectedIndex != 0)
            {
                Lokasi = " AND ISC064_MARKETINGJUAL..MS_KONTRAK.Lokasi = '" + Cf.Str(lokasi.SelectedValue) + "'";
            }

            string Project = " AND ISC064_MARKETINGJUAL..MS_KONTRAK.Project IN (" + Act.ProjectListSql + ")";
            if (project.SelectedValue != "SEMUA") Project = " AND ISC064_MARKETINGJUAL..MS_KONTRAK.Project = '" + project.SelectedValue + "'";
            string Perusahaan = "";
            if (pers.SelectedValue != "SEMUA") Perusahaan = " AND ISC064_MARKETINGJUAL..MS_KONTRAK.Pers = '" + pers.SelectedValue + "'";

            string strSql = "SELECT "
                + " ISC064_MARKETINGJUAL..MS_KONTRAK.NoKontrak"
                + ",ISC064_MARKETINGJUAL..MS_KONTRAK.NilaiKontrak"
                + ",ISC064_MARKETINGJUAL..MS_KONTRAK.NoUnit"
                + ",ISC064_MARKETINGJUAL..MS_CUSTOMER.Nama AS Cs"
                + ",DATEDIFF(day,convert(datetime,ISC064_MARKETINGJUAL..MS_TAGIHAN.TglJT,112),'" + Cf.Tgl112(tanggal) + "') as Telat"
                + ",ISC064_MARKETINGJUAL..MS_TAGIHAN.NilaiTagihan AS NilaiTagihan"
                + ",ISC064_MARKETINGJUAL..MS_TAGIHAN.Denda AS Denda"
                + ",ISC064_MARKETINGJUAL..MS_TAGIHAN.DendaReal AS DendaReal"
                + ",ISC064_MARKETINGJUAL..MS_TAGIHAN.NilaiPutihDenda AS PutihDenda"
                + ",ISC064_MARKETINGJUAL..MS_TAGIHAN.AlokasiBenefit AS AlokasiBenefit"
                + ",ISC064_MARKETINGJUAL..MS_TAGIHAN.Benefit AS Benefit"
                + ",ISC064_MARKETINGJUAL..MS_TAGIHAN.BenefitReal AS BenefitReal"
                + " FROM ISC064_MARKETINGJUAL..MS_KONTRAK INNER JOIN ISC064_MARKETINGJUAL..MS_CUSTOMER ON ISC064_MARKETINGJUAL..MS_KONTRAK.NoCustomer = ISC064_MARKETINGJUAL..MS_CUSTOMER.NoCustomer"
                + " INNER JOIN ISC064_MARKETINGJUAL..MS_TAGIHAN ON ISC064_MARKETINGJUAL..MS_KONTRAK.NoKontrak = ISC064_MARKETINGJUAL..MS_TAGIHAN.NoKontrak"
                + " WHERE 1=1 "
                + " AND ISC064_MARKETINGJUAL..MS_TAGIHAN.TglJT <= '" + Cf.AwalBulan1(tanggal.Month, tanggal.Year, tanggal.Day) + "'"
                + " AND (ISC064_MARKETINGJUAL..MS_TAGIHAN.Denda > 0 OR ISC064_MARKETINGJUAL..MS_TAGIHAN.Benefit > 0)"
                + Project
                + Perusahaan
                + Lokasi
                + " ORDER BY ISC064_MARKETINGJUAL..MS_CUSTOMER.Nama ASC";
            
            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                int no = i + 1;

                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                c = new TableCell();
                c.Text = Cf.Str(no);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoKontrak"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Cs"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoUnit"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["NilaiKontrak"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["NilaiTagihan"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Telat"].ToString();
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Math.Round(Convert.ToDecimal(rs.Rows[i]["Denda"])));
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Math.Round(Convert.ToDecimal(rs.Rows[i]["Benefit"])));
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Math.Round(Convert.ToDecimal(rs.Rows[i]["BenefitReal"])));
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Math.Round(Convert.ToDecimal(rs.Rows[i]["DendaReal"])));
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["PutihDenda"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                decimal SaldoDenda = Math.Round(Convert.ToDecimal(rs.Rows[i]["Denda"]) - Convert.ToDecimal(rs.Rows[i]["DendaReal"]) - Convert.ToDecimal(rs.Rows[i]["PutihDenda"]) - Convert.ToDecimal(rs.Rows[i]["AlokasiBenefit"]));
                c.Text = Cf.Num(SaldoDenda);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                rpt.Rows.Add(r);

                total = total + (decimal)rs.Rows[i]["Denda"];
                t2 = t2 + (decimal)rs.Rows[i]["DendaReal"];
                t1 = t1 + (decimal)rs.Rows[i]["PutihDenda"];
                t3 = t3 + (decimal)rs.Rows[i]["Benefit"];
                t4 = t4 + (decimal)rs.Rows[i]["BenefitReal"];
                t5 = t5 + SaldoDenda;

                if (i == rs.Rows.Count - 1)
                    SubTotal("GRAND TOTAL", total, t2, t1, t3, t4, t5);
            }
        }

        private void SubTotal(string txt, decimal total, decimal t2, decimal t1, decimal t3, decimal t4, decimal t5)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = Rpt.Foot();
            c.Text = txt;
            c.ColumnSpan = 7;
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(Math.Round(total));
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(Math.Round(t3));
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(Math.Round(t4));
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(Math.Round(t2));
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(Math.Round(t1));
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(Math.Round(t5));
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

        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            lokasi.Items.Clear();
            lokasi.Items.Add(new ListItem("SEMUA"));
            init();
        }
    }
}

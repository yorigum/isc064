using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Diagnostics;

namespace ISC064.KPA.Laporan
{
    public partial class LaporanLPA : System.Web.UI.Page
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

        private void init()
        {
            dari.Text = Cf.Day(Cf.AwalBulan());
            sampai.Text = Cf.Day(Cf.AkhirBulan());
            string Project = project.SelectedValue == "SEMUA" ? "Project IN (" + Act.ProjectListSql + ")" : "Project = '" + project.SelectedValue + "'";
            DataTable rs = Db.Rs("SELECT DISTINCT Lokasi FROM MS_KONTRAK WHERE " + Project + " ORDER BY Lokasi");
            for (int i = 0; i < rs.Rows.Count; i++)
                lokasi.Items.Add(new ListItem(rs.Rows[i][0].ToString()));

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

            Rpt.SubJudul(x
                , "Lokasi : " + lokasi.SelectedItem.Text
                );

            string tgl = "";
            if (tbTarget.Checked) tgl = tbTarget.Text;
            if (tbTgl.Checked) tgl = tbTgl.Text;

            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);
            Rpt.SubJudul(x
                , tgl + " : " + Cf.Day(dari.Text) + " s/d " + Cf.Day(sampai.Text)
                );

            Rpt.SubJudul(
                x, "Status LPA : " + ddlStatus.SelectedItem.Text
                );

            Rpt.SubJudul(
                x, "Project : " + project.SelectedValue
                );

            Rpt.SubJudul(
                x, "Perusahaan : " + pers.SelectedItem.Text
                );

            //Rpt.Header(rpt, x);
            Rpt.HeaderReport(headReport, "", x);
        }

        private void Fill()
        {
            string Project = " AND a.Project IN (" + Act.ProjectListSql + ")";
            if (project.SelectedValue != "SEMUA") Project = " AND a.Project = '" + project.SelectedValue + "'";
            string Perusahaan = "";
            if (pers.SelectedValue != "SEMUA") Perusahaan = " AND a.Pers = '" + pers.SelectedValue + "'";

            string Lokasi = "";
            if (lokasi.SelectedIndex > 0)
                Lokasi += " AND Lokasi = '" + lokasi.SelectedValue + "'";

            string tgl = "";
            if (tbTarget.Checked)
                tgl = "TargetLPA";

            if (tbTgl.Checked)
                tgl = "TglLPA";

            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);
            if (Dari > Sampai)
            {
                DateTime x = Sampai;
                Sampai = Dari;
                Dari = x;
            }

            string Status = "";
            if (ddlStatus.SelectedIndex != 0)
            {
                if (ddlStatus.SelectedItem.Text == "BELUM DITENTUKAN")
                    Status = " AND StatusLPA = ''";
                else
                    Status = " AND StatusLPA = '" + ddlStatus.SelectedItem.Text + "'";
            }

            string Tanggal = "";
            if (ddlStatus.SelectedItem.Text != "BELUM DITENTUKAN" && ddlStatus.SelectedItem.Text != "TIDAK PERLU" && ddlStatus.SelectedIndex != 0)
            {
                Tanggal = " AND CONVERT(varchar," + tgl + ",112) >= '" + Cf.Tgl112(Dari) + "'"
                    + " AND CONVERT(varchar," + tgl + ",112) <= '" + Cf.Tgl112(Sampai) + "'";
            }

            string strSql = "SELECT a.*, b.Nama AS NamaCustomer"
                + " FROM MS_KONTRAK a"
                + " INNER JOIN MS_CUSTOMER b ON a.NoCustomer = b.NoCustomer"
                + " WHERE a.Status = 'A'"
                + " AND CaraBayar='KPR'"
                + Project
                + Perusahaan
                + Lokasi
                + Tanggal
                + Status
                ;
            DataTable rs = Db.Rs(strSql);

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected)
                    break;

                TableRow r = new TableRow();
                r.Attributes["ondblclick"] = "popEditProsesKPR('" + Cf.Pk(rs.Rows[i]["NoKontrak"]) + "');";
                TableCell c;

                c = new TableCell();
                c.Text = (i + 1).ToString();
                c.HorizontalAlign = HorizontalAlign.Center;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoKontrak"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NamaCustomer"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoUnit"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["BankKPR"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["StatusLPA"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TargetLPA"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglLPA"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoLPA"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                rpt.Rows.Add(r);
            }
        }

        protected void SubTotal(decimal t)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = Rpt.Foot();
            c.Text = "<strong>GRAND TOTAL</strong>";
            c.ColumnSpan = 9;
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = "<strong>" + Cf.Num(t) + "</strong>";
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            rpt.Rows.Add(r);
        }
        protected void pdf_Click(object sender, System.EventArgs e)
        {

            Process p = new System.Diagnostics.Process();

            string Nama = "Laporan LPA";
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

            string strSql = "SELECT * FROM ISC064_MARKETINGJUAL..LapPDF WHERE AttachmentID  = '" + NoAttachment + "'";
            DataTable rs = Db.Rs(strSql);

            string nfilename = "LapLPA" + NoAttachment + ".pdf";

            //update filename
            Db.Execute("UPDATE ISC064_MARKETINGJUAL..LapPDF SET FileName= '" + nfilename + "' WHERE AttachmentID = " + NoAttachment);


            //folder untuk menyimpan file pdf
            string save = Mi.PathFilePDFReport + "LapLPA" + rs.Rows[0]["AttachmentID"] + ".pdf";

            //declare parameter
            string Lokasi = lokasi.SelectedValue;

            string tgl = "";
            if (tbTarget.Checked)
                tgl = "TargetLPA";
            if (tbTgl.Checked)
                tgl = "TglLPA";

            string status = ddlStatus.SelectedValue;

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
            string link = Mi.PathAlamatWeb + "kpa/LaporanPDF/PDFLPA.aspx?id=" + rs.Rows[0]["AttachmentID"] + "&tgl=" + tgl + "&status=" + status + "&lokasi=" + Lokasi + "&project=" + Project + "&pers=" + pers.SelectedValue + "";

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

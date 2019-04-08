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
    public partial class LaporanTransferAnonim : System.Web.UI.Page
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

        private void init()
        {
            dari.Text = Cf.Day(DateTime.Today);
            sampai.Text = Cf.Day(DateTime.Today);
            string Project = project.SelectedValue == "SEMUA" ? "Project IN (" + Act.ProjectListSql + ")" : "Project = '" + project.SelectedValue + "'";

            DataTable rs;

            rs = Db.Rs("SELECT * FROM REF_ACC WHERE " + Project);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["Acc"].ToString();
                string t = v + " : " + rs.Rows[i]["Bank"] + " " + rs.Rows[i]["Rekening"];
                rek.Items.Add(new ListItem(t, v));
            }

            rek.SelectedIndex = 0;
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
                    , "<script language='javascript'>document.getElementById('" + s + "').select()</script>");
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

            string Nama = "Laporan Transfer Anonim";
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

            string nfilename = "TransferAnonim" + NoAttachment + ".pdf";

            //update filename
            Db.Execute("UPDATE ISC064_FINANCEAR..LapPDF SET FileName= '" + nfilename + "' WHERE AttachmentID = " + NoAttachment);

            //folder untuk menyimpan file pdf
            string save = Mi.PathFilePDFReport + "TransferAnonim" + rs.Rows[0]["AttachmentID"] + ".pdf";

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
            string link = Mi.PathAlamatWeb + "financear/LaporanPDF/PDFLaporanTransferAnonim.aspx?id=" + rs.Rows[0]["AttachmentID"]
                + "&userid=" + UserID
                + "&status_a=" + "SEMUA"
                + "&status_b=" + "BARU"
                + "&status_id=" + "IDENTIFIKASI"
                + "&status_s=" + "SOLVE"
                + "&project=" + Project
                + "&pers=" + pers.SelectedValue
                + "&rek=" + rek.SelectedValue.Replace(" ", "%")
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

            //Header();
            Header2();
            FillHeader();
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
                , "Rekening : " + rek.SelectedItem.Text
                );

            Rpt.Header(rpt, x);
        }

        private void Header2()
        {
            System.Text.StringBuilder x = new System.Text.StringBuilder();

            x.Append("<h2 style='margin:0;font:bold 10pt trebuchet ms'>"
                + comp.InnerHtml
                + "</h2>");

            x.Append("<h1 class='title' style='margin:0;font:bold 20pt'>"
                + judul.InnerHtml
                + "</h1>");

            string tgl = "";
            if (tglinput.Checked) tgl = tglinput.Text;

            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);

            x.Append("<h2 style='margin:0;font:bold 10pt trebuchet ms'>"
                + tgl + " : " + Cf.Day(dari.Text) + " s/d " + Cf.Day(sampai.Text)
                + "</h2>");

            x.Append("<h2 style='margin:0;font:bold 10pt trebuchet ms'>"
                + "Rekening : " + rek.SelectedItem.Text
                + "</h2>");
            x.Append("<h2 style='margin:0;font:bold 10pt trebuchet ms'>"
                + "Project : " + project.SelectedValue
                + "</h2>");

            x.Append("<h2 style='margin:0;font:bold 10pt trebuchet ms'>"
                + "Perusahaan : " + pers.SelectedItem.Text
                + "</h2>");

            if (statusB.Checked)
                x.Append("<h2 style='margin:0;font:bold 10pt trebuchet ms'>"
                + "Status : " + statusB.Text
                + "<h2><br />");
            else if (statusID.Checked)
                x.Append("<h2 style='margin:0;font:bold 10pt trebuchet ms'>"
                + "Status : " + statusID.Text
                + "</h2><br />");
            else if (statusS.Checked)
                x.Append("<h2 style='margin:0;font:bold 10pt trebuchet ms'>"
                + "Status : " + statusS.Text
                + "</h2><br />");
            else
                x.Append("<h2 style='margin:0;font:bold 10pt trebuchet ms'>"
                + "Status : " + statusA.Text
                + "</h2><br />");


            //x.Append("Laporan dihasilkan pada hari : " + Cf.IndoWeek(DateTime.Today)
            //    + ", " + Cf.Date(DateTime.Now)
            //    + " dari workstation : " + Act.IP
            //    + " dan username : " + Act.UserID);

            //TableRow r = new TableRow();
            //TableCell c;

            //c = new TableCell();
            //c.Text = x.ToString();
            //c.ColumnSpan = 11;
            //r.Cells.Add(c);

            string legend = "";
            //rpt.Rows.Add(r);
            Rpt.HeaderReport(headReport, legend, x);
        }

        private void FillHeader()
        {
            TableRow r = new TableRow();
            TableHeaderCell c;

            c = new TableHeaderCell();
            c.Text = "TITIPAN";
            c.ColumnSpan = 6;
            c.ForeColor = Color.White;
            c.Attributes["style"] = "background-color:#1E90FF";
            r.Cells.Add(c);

            c = new TableHeaderCell();
            c.Text = "REALISASI";
            c.ColumnSpan = 5;
            c.ForeColor = Color.White;
            c.Attributes["style"] = "background-color:#1E90FF";
            r.Cells.Add(c);

            rpt.Rows.Add(r);

            r = new TableRow();

            c = new TableHeaderCell();
            c.Text = "NO.";
            c.ForeColor = Color.White;
            c.Attributes["style"] = "background-color:#1E90FF";
            r.Cells.Add(c);

            c = new TableHeaderCell();
            c.Text = "NO. ANONIM";
            c.ForeColor = Color.White;
            c.Attributes["style"] = "background-color:#1E90FF";
            r.Cells.Add(c);

            c = new TableHeaderCell();
            c.Text = "TGL. MASUK";
            c.ForeColor = Color.White;
            c.Attributes["style"] = "background-color:#1E90FF";
            r.Cells.Add(c);

            c = new TableHeaderCell();
            c.Text = "BANK";
            c.ForeColor = Color.White;
            c.Attributes["style"] = "background-color:#1E90FF";
            r.Cells.Add(c);

            c = new TableHeaderCell();
            c.Text = "KETERANGAN";
            c.ForeColor = Color.White;
            c.Attributes["style"] = "background-color:#1E90FF";
            r.Cells.Add(c);

            c = new TableHeaderCell();
            c.Text = "NILAI";
            c.ForeColor = Color.White;
            c.Attributes["style"] = "background-color:#1E90FF";
            r.Cells.Add(c);

            c = new TableHeaderCell();
            c.Text = "TGL. KWT";
            c.ForeColor = Color.White;
            c.Attributes["style"] = "background-color:#1E90FF";
            r.Cells.Add(c);

            c = new TableHeaderCell();
            c.Text = "NO. KWT";
            c.ForeColor = Color.White;
            c.Attributes["style"] = "background-color:#1E90FF";
            r.Cells.Add(c);

            c = new TableHeaderCell();
            c.Text = "UNIT";
            c.ForeColor = Color.White;
            c.Attributes["style"] = "background-color:#1E90FF";
            r.Cells.Add(c);

            c = new TableHeaderCell();
            c.Text = "NAMA";
            c.ForeColor = Color.White;
            c.Attributes["style"] = "background-color:#1E90FF";
            r.Cells.Add(c);

            c = new TableHeaderCell();
            c.Text = "NILAI";
            c.ForeColor = Color.White;
            c.Attributes["style"] = "background-color:#1E90FF";
            r.Cells.Add(c);

            rpt.Rows.Add(r);
        }

        private void Fill()
        {
            string Project = " AND c.Project In (" + Act.ProjectListSql + ")";
            if (project.SelectedValue != "SEMUA") Project = " AND c.Project = '" + project.SelectedValue + "'";
            string Perusahaan = "";
            if (pers.SelectedValue != "SEMUA") Perusahaan = " AND c.Pers = '" + pers.SelectedValue + "'";

            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);
            if (Dari > Sampai)
            {
                DateTime x = Sampai;
                Sampai = Dari;
                Dari = x;
            }

            decimal t1 = 0;
            decimal t2 = 0;
            decimal t3 = 0;

            string Rek = "";
            if (rek.SelectedIndex != 0)
                Rek = " AND a.Bank = '" + rek.SelectedValue + "'";

            string Status = "";
            if (statusB.Checked) Status = " AND a.Status = 'BARU'";
            if (statusID.Checked) Status = " AND a.Status = 'ID'";
            if (statusS.Checked) Status = " AND a.Status = 'S'";

            string tgl = "";
            if (tglinput.Checked) tgl = "Tgl";

            string strSql = "SELECT * "
                    + " FROM MS_ANONIM a LEFT JOIN MS_TTS b ON a.NoAnonim =  b.NoAnonim"
                    + " LEFT JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK c ON b.Ref = c.NoKontrak"
                    + " WHERE 1=1 "
                    + " AND CONVERT(varchar," + tgl + ",112) >= '" + Cf.Tgl112(Dari) + "'"
                    + " AND CONVERT(varchar," + tgl + ",112) <= '" + Cf.Tgl112(Sampai) + "'"
                    + Project
                    + Perusahaan
                    + Rek
                    + Status
                    + " ORDER BY a.NoAnonim";

            DataTable rs = Db.Rs(strSql);

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                r.VerticalAlign = VerticalAlign.Top;
                r.Attributes["ondblclick"] = "popEditTA('" + rs.Rows[i]["NoAnonim"] + "')";

                c = new TableCell();
                c.Text = (i + 1) + ".";
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoAnonim"].ToString().PadLeft(7, '0');
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["Tgl"]);
                c.Wrap = false;
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                string NamaBank = Db.SingleString("SELECT Bank FROM REF_ACC WHERE Acc = '" + rs.Rows[i]["Bank"] + "'");
                string RekBank = Db.SingleString("SELECT Rekening FROM REF_ACC WHERE Acc = '" + rs.Rows[i]["Bank"] + "'");

                c = new TableCell();
                c.Text = NamaBank;// +" " + RekBank;
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.StrKet(rs.Rows[i]["Ket"].ToString());
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["Nilai"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                DataTable kw = Db.Rs("SELECT * FROM MS_TTS WHERE Status = 'POST' AND NoAnonim = '" + rs.Rows[i]["NoAnonim"].ToString() + "'");

                c = new TableCell();
                if (kw.Rows.Count > 0)
                    c.Text = Cf.Day(kw.Rows[0]["TglBKM"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                if (kw.Rows.Count > 0)
                    c.Text = kw.Rows[0]["ManualBKM"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                if (kw.Rows.Count > 0)
                    c.Text = kw.Rows[0]["Unit"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                if (kw.Rows.Count > 0)
                    c.Text = kw.Rows[0]["Customer"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                if (kw.Rows.Count > 0)
                    c.Text = Cf.Num(kw.Rows[0]["Total"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                rpt.Rows.Add(r);

                t1 += Convert.ToDecimal(rs.Rows[i]["Nilai"]);
                if (kw.Rows.Count > 0)
                    t2 += Convert.ToDecimal(kw.Rows[0]["Total"]);

                if (i == rs.Rows.Count - 1)
                {
                    SubTotal("GRAND TOTAL", t1, t2);
                }
            }
        }

        private void SubTotal(string txt, decimal t1, decimal t2)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = Rpt.Foot();
            c.Text = txt;
            c.ColumnSpan = 5;
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t1);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = "&nbsp;";
            c.ColumnSpan = 4;
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t2);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            rpt.Rows.Add(r);

            r = new TableRow();

            c = new TableCell();
            c.Text = "<b>SISA TITIPAN : " + Cf.Num(t1 - t2) + "</b>";
            c.ColumnSpan = 11;
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

        protected void pers_SelectedIndexChanged(object sender, EventArgs e)
        {
            project.Items.Clear();
            rek.Items.Clear();
            rek.Items.Add(new ListItem("SEMUA"));
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
            rek.Items.Clear();
            rek.Items.Add(new ListItem("SEMUA"));
            init();
        }
    }
}

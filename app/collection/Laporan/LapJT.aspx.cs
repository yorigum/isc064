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
    public partial class LapJT : System.Web.UI.Page
    {
        protected System.Web.UI.WebControls.PlaceHolder list;
        protected System.Web.UI.WebControls.Label total1;
        protected System.Web.UI.WebControls.Label total2;
        protected System.Web.UI.WebControls.Label total3;
        protected System.Web.UI.WebControls.Label total4;
        protected System.Web.UI.WebControls.Label total5;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();

            if (!Page.IsPostBack)
            {
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
            dari.Text = Cf.Day(DateTime.Now);
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

            string Nama = "Laporan Jatuh Tempo";
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
                    + ",'" + null + "'"
                    );

            //get nomor customer terbaru
            int NoAttachment = Db.SingleInteger(
                "SELECT TOP 1 AttachmentID FROM LapPDF ORDER BY AttachmentID DESC");

            string strSql = "SELECT * FROM ISC064_FINANCEAR..LapPDF WHERE AttachmentID  = '" + NoAttachment + "'";
            DataTable rs = Db.Rs(strSql);

            string nfilename = "LaporanJatuhTempo" + NoAttachment + ".pdf";

            //update filename
            Db.Execute("UPDATE ISC064_FINANCEAR..LapPDF SET FileName= '" + nfilename + "' WHERE AttachmentID = " + NoAttachment);


            //folder untuk menyimpan file pdf
            string save = Mi.PathFilePDFReport + "LaporanJatuhTempo" + rs.Rows[0]["AttachmentID"] + ".pdf";

            string strcarabayar = string.Empty;
            try
            {

                foreach (ListItem item in carabayar.Items)
                {
                    if (item.Selected == true)
                    {
                        strcarabayar += item.Value.Replace(" ", "+") + "-";
                    }
                }
            }
            catch (Exception)
            {
            }

            string StatusKPA = "";
            if (kpa1.Checked)
                StatusKPA = "kpa1";
            else if (kpa2.Checked)
                StatusKPA = "kpa2";

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
            string link = Mi.PathAlamatWeb + "collection/LaporanPDF/PDFLapJT.aspx?id=" + rs.Rows[0]["AttachmentID"]
                + "&carabayar=" + strcarabayar
                + "&statuskpa=" + StatusKPA
                + "&userid=" + UserID
                + "&pers=" + pers.SelectedValue
                + "&project=" + Project
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
            System.Text.StringBuilder x = new System.Text.StringBuilder();

            Rpt.Judul(x, comp, judul);

            DateTime Dari = Convert.ToDateTime(dari.Text);

            if (kpa1.Checked)
                Rpt.SubJudul(x, "Status KPR <b style='padding-left:5px'>:</b> " + kpa1.Text);
            else
                Rpt.SubJudul(x, "Status KPR <b style='padding-left:5px'>:</b> " + kpa2.Text);

            Rpt.SubJudul(x, " As Of <b style='padding-left:40px'>:</b> " + dari.Text);
            Rpt.SubJudul(x, " Cara Bayar <b style='padding-left:5px'>:</b> " + Rpt.inSql(carabayar).Replace("'", ""));
            Rpt.SubJudul(x, " Perusahaan : " + pers.SelectedValue);
            Rpt.SubJudul(x, " Project <b style='padding-left:28px'>:</b> " + project.SelectedValue);
            //Rpt.Header(rpt, x);
            string legend = "";
            Rpt.HeaderReport(headReport, legend, x);
        }

        private void Fill()
        {
            DateTime Dari = Convert.ToDateTime(dari.Text);
            decimal t1 = 0, t2 = 0, t3 = 0;

            string KPR = "";
            if (kpa1.Checked)
            {
                KPR = " ";
            }
            else if (kpa2.Checked)
            {
                KPR = " AND a.KPR != '1' ";
            }

            string Project = " AND b.Project IN (" + Act.ProjectListSql + ")";
            if (project.SelectedValue != "SEMUA") Project = " AND b.Project = '" + project.SelectedValue + "'";
            string Perusahaan = "";
            if (pers.SelectedValue != "SEMUA") Perusahaan = " AND b.Pers = '" + pers.SelectedValue + "'";

            string aa = "";
            if (UserAgent() > 0)
                aa = " AND b.NoAgent = " + UserAgent();

            string strSql = "SELECT a.*, b.NoCustomer, b.NoUnit,b.CaraBayar"
                + " FROM ISC064_MARKETINGJUAL..MS_TAGIHAN a"
                + " INNER JOIN ISC064_MARKETINGJUAL..MS_KONTRAK b ON a.NoKontrak = b.NoKontrak"
                + " WHERE DATEDIFF(DAY, '" + Cf.Tgl112(Dari) + "', CONVERT(DATETIME, TglJT, 112)) BETWEEN 0 AND 14"
                + " AND b.CaraBayar IN(" + Rpt.inSql(carabayar) + ")"
                + " AND b.STATUS != 'B'"
                + Project
                + Perusahaan
                + KPR
                + aa
                ;

            DataTable rs = Db.Rs(strSql);

            TableRow r = new TableRow();
            rpt.Rows.Add(r);

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                r = new TableRow();
                TableCell c;

                c = new TableCell();
                c.Text = (i + 1).ToString();
                c.HorizontalAlign = HorizontalAlign.Center;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoKontrak"].ToString() + "." + rs.Rows[i]["NoUrut"];
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Db.SingleString("SELECT Nama FROM ISC064_MARKETINGJUAL..MS_CUSTOMER WHERE NoCustomer = " + rs.Rows[i]["NoCustomer"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Db.SingleString("SELECT NoTelp FROM ISC064_MARKETINGJUAL..MS_CUSTOMER WHERE NoCustomer = " + rs.Rows[i]["NoCustomer"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Db.SingleString("SELECT NoHP FROM ISC064_MARKETINGJUAL..MS_CUSTOMER WHERE NoCustomer = " + rs.Rows[i]["NoCustomer"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoUnit"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NamaTagihan"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglJt"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["NilaiTagihan"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                decimal NilaiPelunasan = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan),0) FROM ISC064_MARKETINGJUAL..MS_PELUNASAN WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' AND NoTagihan = '" + rs.Rows[i]["NoUrut"] + "'");
                c = new TableCell();
                c.Text = Cf.Num(NilaiPelunasan);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                decimal SisaTagihan = Convert.ToDecimal(rs.Rows[i]["NilaiTagihan"]) - NilaiPelunasan;
                c = new TableCell();
                c.Text = Cf.Num(SisaTagihan);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                rpt.Rows.Add(r);

                t1 = t1 + (decimal)rs.Rows[i]["NilaiTagihan"];
                t2 = t2 + NilaiPelunasan;
                t3 = t3 + SisaTagihan;

                if (i == (rs.Rows.Count - 1))
                {
                    SubTotal("GRAND TOTAL", t1, t2, t3);
                }
            }
        }

        private void SubTotal(string txt, decimal t1, decimal t2, decimal t3)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = Rpt.Foot();
            c.Text = txt;
            c.ColumnSpan = 8;
            c.HorizontalAlign = HorizontalAlign.Left;
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
        protected void carabayarCheck_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < carabayar.Items.Count; i++)
            {
                carabayar.Items[i].Selected = carabayarCheck.Checked;
            }

            Js.Focus(this, carabayarCheck);
            carabayarc.Text = "";
        }

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

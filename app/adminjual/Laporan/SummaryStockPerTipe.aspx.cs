using System;
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

namespace ISC064.ADMINJUAL.Laporan
{
    /// <summary>
    /// Summary description for SummaryStockPerTipe.
    /// </summary>
    public partial class SummaryStockPerTipe : System.Web.UI.Page
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
            DataTable rs;
            string Project = project.SelectedIndex == 0 ? "Project IN (" + Act.ProjectListSql + ")" : "Project = '" + project.SelectedValue + "'";
            rs = Db.Rs("SELECT * FROM REF_JENIS WHERE " + Project + " ORDER BY SN");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["Jenis"].ToString();
                string t = v + " - " + rs.Rows[i]["Nama"].ToString();
                jenis.Items.Add(new ListItem(t, v));
                jenis.Items[i].Selected = true;
            }

            rs = Db.Rs("SELECT * FROM REF_LOKASI WHERE " + Project + " ORDER BY Lokasi");
            for (int i = 0; i < rs.Rows.Count; i++)
                lokasi.Items.Add(new ListItem(rs.Rows[i][0].ToString()));

            lokasi.SelectedIndex = 0;
        }

        private bool valid()
        {
            string s = "";
            bool x = true;

            if (!Cf.isPilih(jenis))
            {
                x = false;
                jenisc.Text = " Pilih Minimum Satu";
            }
            else
                jenisc.Text = "";

            if (!x && s != "")
            {
                ClientScript.RegisterStartupScript(GetType(), "err"
                    , "<script type='text/javascript'>document.getElementById('" + s + "').select()</script>");
            }

            return x;
        }

        private void Report()
        {
            param.Visible = false;
            rpt.Visible = true;

            System.Text.StringBuilder x = new System.Text.StringBuilder();

            Rpt.Judul(x, comp, judul);

            x.Append(
                "<br/>Jenis : " + Rpt.inSql(jenis).Replace("'", "")
                );

            x.Append(
                "<br/>Lokasi : " + lokasi.SelectedItem.Text
                );

            x.Append(
                "<br/>Project : " + project.SelectedValue
                );

            lblHeader.Text = Mi.Pt
                + "<br />"
                + "PER " + Cf.Day(DateTime.Today)
                + x
                ;

            string legend = "";

            Rpt.HeaderReport(headReport, legend, x);

            Fill();
        }

        private void Fill()
        {
            string Lokasi = "";
            if (lokasi.SelectedIndex != 0)
            {
                Lokasi = " AND Lokasi = '" + Cf.Str(lokasi.SelectedValue) + "'";
            }

            string Project = " AND Project IN (" + Act.ProjectListSql + ")";
            if (project.SelectedValue != "SEMUA")
            {
                Project = " AND Project = '" + project.SelectedValue + "'";
            }

            DataTable aa = Db.Rs("SELECT DISTINCT(Lokasi) FROM MS_UNIT WHERE 1=1" + Lokasi + Project);
            for (int j = 0; j < aa.Rows.Count; j++)
            {
                if (!Response.IsClientConnected)
                    break;

                TableRow r = new TableRow();
                TableCell c;

                c = new TableCell();
                c.Text = aa.Rows[j]["Lokasi"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                string strSql = "SELECT DISTINCT(JENIS)"
                    + " FROM MS_UNIT"
                    + " WHERE Lokasi = '" + aa.Rows[j]["Lokasi"] + "'"
                    + " AND Jenis IN (" + Rpt.inSql(jenis) + ")"
                    + Project
                    ;
                DataTable rs = Db.Rs(strSql);

                decimal TotalAvailable = 0, LuasAvailable = 0;
                decimal TotalSold = 0, LuasSold = 0;
                decimal TotalHold = 0, LuasHold = 0;
                decimal Total = 0, LuasTotal = 0;
                decimal TotalTitip = 0;//, LuasTitip = 0;
                decimal t1 = 0, t2 = 0, t3 = 0, t4 = 0, t5 = 0, t6 = 0, t7 = 0, t8 = 0, t9 = 0, t10 = 0;

                for (int i = 0; i < rs.Rows.Count; i++)
                {
                    if (!Response.IsClientConnected)
                        break;

                    if (i > 0)
                    {
                        r = new TableRow();

                        c = new TableCell();
                        c.Text = "";
                        c.HorizontalAlign = HorizontalAlign.Left;
                        r.Cells.Add(c);
                    }

                    c = new TableCell();
                    c.Text = rs.Rows[i]["Jenis"].ToString();
                    c.HorizontalAlign = HorizontalAlign.Left;
                    r.Cells.Add(c);

                    c = new TableCell();
                    TotalAvailable = Nilai(rs.Rows[i]["Jenis"].ToString(), aa.Rows[j]["Lokasi"].ToString(), "AVAILABLE", project.SelectedValue);
                    c.Text = Cf.Num(TotalAvailable);
                    c.HorizontalAlign = HorizontalAlign.Right;
                    r.Cells.Add(c);

                    c = new TableCell();
                    LuasAvailable = Math.Round(Persen(rs.Rows[i]["Jenis"].ToString(), aa.Rows[j]["Lokasi"].ToString(), "AVAILABLE", project.SelectedValue), 2);
                    c.Text = Cf.Num(Math.Round(LuasAvailable, 2)) + " %";
                    c.HorizontalAlign = HorizontalAlign.Right;
                    r.Cells.Add(c);

                    c = new TableCell();
                    TotalSold = Nilai(rs.Rows[i]["Jenis"].ToString(), aa.Rows[j]["Lokasi"].ToString(), "SOLD", project.SelectedValue);
                    c.Text = Cf.Num(TotalSold);
                    c.HorizontalAlign = HorizontalAlign.Right;
                    r.Cells.Add(c);

                    c = new TableCell();
                    LuasSold = Math.Round(Persen(rs.Rows[i]["Jenis"].ToString(), aa.Rows[j]["Lokasi"].ToString(), "SOLD", project.SelectedValue), 2);
                    c.Text = Cf.Num(LuasSold) + " %";
                    c.HorizontalAlign = HorizontalAlign.Right;
                    r.Cells.Add(c);

                    c = new TableCell();
                    TotalHold = Nilai(rs.Rows[i]["Jenis"].ToString(), aa.Rows[j]["Lokasi"].ToString(), "HOLD", project.SelectedValue);
                    c.Text = Cf.Num(TotalHold);
                    c.HorizontalAlign = HorizontalAlign.Right;
                    r.Cells.Add(c);

                    c = new TableCell();
                    LuasHold = Math.Round(Persen(rs.Rows[i]["Jenis"].ToString(), aa.Rows[j]["Lokasi"].ToString(), "HOLD", project.SelectedValue), 2);
                    c.Text = Cf.Num(LuasHold) + " %";
                    c.HorizontalAlign = HorizontalAlign.Right;
                    r.Cells.Add(c);

                    c = new TableCell();
                    Total = Nilai(rs.Rows[i]["Jenis"].ToString(), aa.Rows[j]["Lokasi"].ToString(), "TOTAL", project.SelectedValue);
                    c.Text = Cf.Num(Total);
                    c.HorizontalAlign = HorizontalAlign.Right;
                    r.Cells.Add(c);

                    c = new TableCell();
                    LuasTotal = Math.Round(Persen(rs.Rows[i]["Jenis"].ToString(), aa.Rows[j]["Lokasi"].ToString(), "TOTAL", project.SelectedValue), 2);
                    c.Text = Cf.Num(LuasTotal) + " %";
                    c.HorizontalAlign = HorizontalAlign.Right;
                    r.Cells.Add(c);

                    t5 += Total;
                    t1 += TotalSold;
                    t2 = t1 / t5 * 100;
                    t3 += TotalAvailable;
                    t4 = t3 / t5 * 100;
                    t7 += TotalHold;
                    t8 = t7 / t5 * 100;
                    t9 += TotalTitip;
                    t10 = t9 / t5 * 100;
                    t6 = t2 + t4 + t8 + t10;

                    rpt.Rows.Add(r);

                    if (i == (rs.Rows.Count - 1))
                        SubTotal(t1, t2, t3, t4, t7, t8, t5, t6, t9, t10);
                }
            }
        }

        protected decimal Nilai(string Jenis, string Lokasi, string Tipe, string Project)
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
            return t;
        }

        protected decimal Persen(string Jenis, string Lokasi, string Tipe, string Project)
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

            decimal tot2 = Db.SingleDecimal(
                    "SELECT COUNT(*)"
                    + " FROM MS_UNIT"
                    + " WHERE Lokasi = '" + Lokasi + "'"
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
                    ) / tot
                    * 100;
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
                    ) / tot
                    * 100;
            }
            else if (Tipe == "TOTAL")
            {
                t = Db.SingleDecimal(
                    "SELECT COUNT(*)"
                    + " FROM MS_UNIT"
                    + " WHERE Jenis = '" + Jenis + "'"
                    + " AND Lokasi = '" + Lokasi + "'"
                    + nProject
                    ) / tot * 100;
            }
            return t;
        }

        private void SubTotal(decimal t1, decimal t2, decimal t3, decimal t4, decimal t7, decimal t8, decimal t5, decimal t6, decimal t9, decimal t10)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = Rpt.Foot();
            c.Text = "TOTAL";
            c.ColumnSpan = 2;
            c.HorizontalAlign = HorizontalAlign.Left;
            c.VerticalAlign = VerticalAlign.Top;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t3)
                //+ "<br />"
                //+ Cf.Num(t3 / t5 * 100) + "%"
                ;
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(Math.Round(t4, 2)) + " %"
                //+ "<br />"
                //+ Cf.Num(t4 / t6 * 100) + "%"
                ;
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t1)
                //+ "<br />"
                //+ Cf.Num(t1 / t5 * 100) + "%"
                ;
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(Math.Round(t2, 2)) + " %"
                //+ "<br />"
                //+ Cf.Num(t2 / t6 * 100) + "%"
                ;
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t7)
                //+ "<br />"
                //+ Cf.Num(t7 / t5 * 100) + "%"
                ;
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(Math.Round(t8, 2)) + " %"
                //+ "<br />"
                //+ Cf.Num(t8 / t6 * 100) + "%"
                ;
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t5)
                //+ "<br />100%"
                ;
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(Math.Round(t6)) + " %"
                //+ "<br />100%"
                ;
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            rpt.Rows.Add(r);
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
                //Rpt.ToExcel(this,rpt);
                Rpt.ToExcel(this, headReport, rpt);
            }
        }

        protected void pdf_Click(object sender, System.EventArgs e)
        {

            Process p = new System.Diagnostics.Process();

            string Nama = "Laporan Summary Stock Per Tipe";
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

            string nfilename = "SummaryStockPerTipe" + NoAttachment + ".pdf";

            //update filename
            Db.Execute("UPDATE ISC064_MARKETINGJUAL..LapPDF SET FileName= '" + nfilename + "' WHERE AttachmentID = " + NoAttachment);


            //folder untuk menyimpan file pdf
            string save = Mi.PathFilePDFReport + "SummaryStockPerTipe" + rs.Rows[0]["AttachmentID"] + ".pdf";

            //declare parameter
            string Lokasi = lokasi.SelectedValue;
            string Input = "";

            string Project = "";
            if (project.SelectedValue == "SEMUA")
            {
                Project = Act.ProjectListSql.Replace("'", "");
            }
            else
            {
                Project = project.SelectedValue;
            }

            string nm = string.Empty;
            string nm2 = string.Empty;
            try
            {
                foreach (ListItem item in jenis.Items)
                {
                    if (item.Selected == true)
                    {
                        nm += item.Value.Replace(" ", "%") + "-";
                    }
                }
            }
            catch (Exception)
            {
            }
            //link untuk download pdf
            string link = Mi.PathAlamatWeb + "adminjual/LaporanPDF/PDFSummaryStockPerTipe.aspx?id=" + rs.Rows[0]["AttachmentID"] + "&jenis=" + nm + "&input=" + Input + "&lokasi=" + Lokasi + "&project=" + Project + "";

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


        protected void jenisCheck_CheckedChanged(object sender, System.EventArgs e)
        {
            for (int i = 0; i < jenis.Items.Count; i++)
            {
                jenis.Items[i].Selected = jenisCheck.Checked;
            }

            Js.Focus(this, jenisCheck);
            jenisc.Text = "";
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
            jenis.Items.Clear();
            lokasi.Items.Clear();
            init();
        }
    }
}

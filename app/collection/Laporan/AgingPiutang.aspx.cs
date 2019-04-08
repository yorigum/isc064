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
    public partial class AgingPiutang : System.Web.UI.Page
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

            string aa = "";
            if (UserAgent() > 0)
                aa = " AND NoAgent = " + UserAgent();
            string Project = project.SelectedValue == "SEMUA" ? "Project IN (" + Act.ProjectListSql + ")" : "Project = '" + project.SelectedValue + "'";

            string strSql = "SELECT NoAgent, Nama FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_AGENT WHERE " + Project + aa;
            DataTable rs = Db.Rs(strSql);

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                ddlAgent.Items.Add(new ListItem(rs.Rows[i]["Nama"].ToString(), Cf.Pk(rs.Rows[i]["NoAgent"])));
            }

            rs = Db.Rs("SELECT DISTINCT Lokasi FROM ISC064_MARKETINGJUAL..MS_KONTRAK WHERE " + Project + " ORDER BY Lokasi ");
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

        private void Report()
        {
            param.Visible = false;
            rpt.Visible = true;

            //lblHeader.Text = Mi.Pt
            //	+ "<br />"
            //	+ "LAPORAN AGING PIUTANG"
            //	+ "<br />"
            //	+ "PER " + Cf.Day(tgl.Text)
            //	;

            System.Text.StringBuilder x = new System.Text.StringBuilder();
            Rpt.Judul(x, comp, judul);

            if (lokasi.SelectedIndex > 0)
                x.Append("Lokasi: " + lokasi.SelectedItem.Text + "<br />");

            if (ddlAgent.SelectedIndex != 0)
                x.Append("Sales: " + ddlAgent.SelectedItem.Text + "<br>");
            else
                x.Append("Sales: SEMUA <br>");
            x.Append("Perusahaan : " + pers.SelectedItem.Text + "<br />");
            x.Append("Project : " + project.SelectedValue + "<br />");

            string strPrincipal = "SEMUA";
            System.Text.StringBuilder z = new System.Text.StringBuilder();
            bool isFirst = true;
            string legend = "";
            Rpt.HeaderReport(headReport, legend, x);

            Fill();
        }
        protected void pdf_Click(object sender, System.EventArgs e)
        {

            Process p = new System.Diagnostics.Process();

            string Nama = "Laporan Aging Tagihan";
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

            string nfilename = "AgingPiutang" + NoAttachment + ".pdf";

            //update filename
            Db.Execute("UPDATE ISC064_FINANCEAR..LapPDF SET FileName= '" + nfilename + "' WHERE AttachmentID = " + NoAttachment);


            //folder untuk menyimpan file pdf
            string save = Mi.PathFilePDFReport + "AgingPiutang" + rs.Rows[0]["AttachmentID"] + ".pdf";

            //parameter

            string StatusKPA = "";
            if (kpa1.Checked)
                StatusKPA = "kpa1";
            else if (kpa2.Checked)
                StatusKPA = "kpa2";

            string CbPrincipal = "";

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
            string link = Mi.PathAlamatWeb + "collection/LaporanPDF/PDFAgingPiutang.aspx?id=" + rs.Rows[0]["AttachmentID"]
                + "&statuskpa=" + StatusKPA
                + "&lokasi=" + lokasi.SelectedValue.Replace(" ", "%")
                + "&userid=" + UserID
                + "&sales=" + ddlAgent.SelectedValue.Replace(" ", "%")
                + "&project=" + Project
                + "&pers=" + pers.SelectedValue
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
        private void Fill()
        {
            string strAdd = "";

            if (lokasi.SelectedIndex > 0)
                strAdd += " AND a.Lokasi = '" + lokasi.SelectedValue + "'";

            if (ddlAgent.SelectedIndex != 0)
                strAdd += " AND a.NoAgent = " + Cf.Pk(ddlAgent.SelectedValue);
            else
            {
                if (UserAgent() > 0)
                    strAdd += " AND a.NoAgent = " + UserAgent();
            }

            string Project = " AND a.Project IN (" + Act.ProjectListSql + ")";
            if (project.SelectedValue != "SEMUA") Project = " AND a.Project = '" + project.SelectedValue + "'";
            string Perusahaan = "";
            if (pers.SelectedValue != "SEMUA") Perusahaan = " AND a.Pers = '" + pers.SelectedValue + "'";

            System.Text.StringBuilder z = new System.Text.StringBuilder();
            bool isFirst = true;

            if (z.ToString() != "")
                strAdd += " AND c.Principal IN (" + z.ToString() + ")";

            string strSql = "SELECT *, b.Nama AS NamaCustomer, c.Nama AS NamaAgent, c.Principal"
                + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK a "
                + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER b "
                + " ON a.NoCustomer = b.NoCustomer "
                + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_AGENT c"
                + " ON a.NoAgent = c.NoAgent "
                + " WHERE 1=1 "
                + " AND a.Status = 'A' "
                + Project
                + Perusahaan
                + strAdd
                + " ORDER BY a.NoUnit"
                ;
            DataTable rs = Db.Rs(strSql);

            decimal t1 = 0, t2 = 0, t3 = 0, t4 = 0, t5 = 0, t6 = 0;
            int index = 1;

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                decimal st1 = 0, st2 = 0, st3 = 0, st4 = 0, st5 = 0;

                TableRow tr = new TableRow();
                TableCell c;

                c = new TableCell();
                c.Text = index.ToString();
                c.HorizontalAlign = HorizontalAlign.Center;
                c.Wrap = false;
                tr.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoKontrak"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                tr.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoUnit"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                tr.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NamaCustomer"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                tr.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NamaAgent"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                tr.Cells.Add(c);

                c = new TableCell();
                decimal Total = TotalOutstanding(rs.Rows[i]["NoKontrak"].ToString());
                c.Text = Cf.Num(Total);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                tr.Cells.Add(c);

                FillOutstanding(rs.Rows[i]["NoKontrak"].ToString(), ref t2, ref t3, ref t4, ref t5, ref t6,
                    ref st1, ref st2, ref st3, ref st4, ref st5, ref index, tr);

                t1 += Total;

            }
            GrandTotal(t1, t2, t3, t4, t5, t6);
        }

        protected decimal TotalOutstanding(string NoKontrak)
        {
            decimal Nilai = 0;

            string KPR = "";
            if (kpa1.Checked)
            {
                KPR = " ";
            }
            else if (kpa2.Checked)
            {
                KPR = " AND a.KPR != '1' ";
            }

            DataTable rs = Db.Rs("SELECT "
                + "NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan), 0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN"
                + " WHERE NoKontrak = '" + NoKontrak + "' AND NoTagihan = a.NoUrut"
                + ") AS Sisa"
                + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN a"
                + " WHERE NoKontrak = '" + NoKontrak + "'"
                + " AND DATEDIFF(DAY, TglJT, '" + Cf.Tgl112(Convert.ToDateTime(tgl.Text)) + "') >= 1"
                + " AND (SELECT ISNULL(SUM(NilaiPelunasan), 0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN"
                + " WHERE NoKontrak = '" + NoKontrak + "' AND NoTagihan = a.NoUrut"
                + ") < NilaiTagihan "
                + KPR
                );

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                Nilai += Convert.ToDecimal(rs.Rows[i]["Sisa"]);
            }

            return Nilai;
        }

        protected void FillOutstanding(string NoKontrak, ref decimal t2, ref decimal t3, ref decimal t4, ref decimal t5, ref decimal t6,
            ref decimal st1, ref decimal st2, ref decimal st3, ref decimal st4, ref decimal st5, ref int index, TableRow tr)
        {
            string KPR = "";
            if (kpa1.Checked)
            {
                KPR = " ";
            }
            else if (kpa2.Checked)
            {
                KPR = " AND a.KPR != '1' ";
            }

            DateTime AsOf = Convert.ToDateTime(tgl.Text);
            DataTable rs = Db.Rs("SELECT *"
                + ", DATEDIFF(DAY, TglJT, '" + Cf.Tgl112(AsOf) + "') AS Telat"
                + ", NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan), 0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN"
                + " WHERE NoKontrak = '" + NoKontrak + "' AND NoTagihan = a.NoUrut"
                + ") AS Sisa"
                + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN a"
                + " WHERE NoKontrak = '" + NoKontrak + "'"
                + " AND DATEDIFF(DAY, TglJT, '" + Cf.Tgl112(AsOf) + "') >= 1"
                + " AND (SELECT ISNULL(SUM(NilaiPelunasan), 0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN"
                + " WHERE NoKontrak = '" + NoKontrak + "' AND NoTagihan = a.NoUrut"
                + ") < NilaiTagihan"
                + KPR
                + " ORDER BY NoUrut"
                );

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                int Telat = Convert.ToInt32(rs.Rows[i]["Telat"]);
                decimal Sisa = Convert.ToDecimal(rs.Rows[i]["Sisa"]);

                TableCell c;

                if (i > 0)
                {
                    tr = new TableRow();
                    c = new TableCell();
                    c.ColumnSpan = 6;
                    tr.Cells.Add(c);
                }

                c = new TableCell();
                c.Text = rs.Rows[i]["NoKontrak"] + "." + rs.Rows[i]["NoUrut"] + " " + rs.Rows[i]["NamaTagihan"].ToString();
                c.Wrap = false;
                tr.Cells.Add(c);

                if (Telat >= 0 && Telat <= 30)
                {
                    t2 += Sisa;
                    st1 += Sisa;

                    c = new TableCell();
                    c.Text = Cf.Num(Sisa);
                    c.HorizontalAlign = HorizontalAlign.Right;
                    c.Wrap = false;
                    tr.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Telat.ToString();
                    c.HorizontalAlign = HorizontalAlign.Center;
                    c.Wrap = false;
                    tr.Cells.Add(c);
                }
                else
                {
                    c = new TableCell();
                    c.ColumnSpan = 2;
                    tr.Cells.Add(c);
                }

                if (Telat >= 31 && Telat <= 60)
                {
                    t3 += Sisa;
                    st2 += Sisa;

                    c = new TableCell();
                    c.Text = Cf.Num(Sisa);
                    c.HorizontalAlign = HorizontalAlign.Right;
                    c.Wrap = false;
                    tr.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Telat.ToString();
                    c.HorizontalAlign = HorizontalAlign.Center;
                    c.Wrap = false;
                    tr.Cells.Add(c);
                }
                else
                {
                    c = new TableCell();
                    c.ColumnSpan = 2;
                    tr.Cells.Add(c);
                }

                if (Telat >= 61 && Telat <= 90)
                {
                    t4 += Sisa;
                    st3 += Sisa;

                    c = new TableCell();
                    c.Text = Cf.Num(Sisa);
                    c.HorizontalAlign = HorizontalAlign.Right;
                    c.Wrap = false;
                    tr.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Telat.ToString();
                    c.HorizontalAlign = HorizontalAlign.Center;
                    c.Wrap = false;
                    tr.Cells.Add(c);
                }
                else
                {
                    c = new TableCell();
                    c.ColumnSpan = 2;
                    tr.Cells.Add(c);
                }

                if (Telat > 90)
                {
                    t5 += Sisa;
                    st4 += Sisa;

                    c = new TableCell();
                    c.Text = Cf.Num(Sisa);
                    c.HorizontalAlign = HorizontalAlign.Right;
                    c.Wrap = false;
                    tr.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Telat.ToString();
                    c.HorizontalAlign = HorizontalAlign.Center;
                    c.Wrap = false;
                    tr.Cells.Add(c);
                }
                else
                {
                    c = new TableCell();
                    c.ColumnSpan = 2;
                    tr.Cells.Add(c);
                }

                //decimal den = Db.SingleDecimal("SELECT Denda FROM ISC064_MARKETINGJUAL..MS_TAGIHAN WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' AND NoUrut = '" + rs.Rows[i]["NoUrut"] + "' ORDER BY NoUrut");

                //c = new TableCell();
                //c.Text = Cf.Num(den);
                //c.HorizontalAlign = HorizontalAlign.Right;
                //tr.Cells.Add(c);

                if (i == 0)
                {

                    string InfoTerakhir = Db.SingleString("SELECT Ket FROM ISC064_MARKETINGJUAL..MS_FOLLOWUP WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' "
                                + " AND NoTagihan = '" + rs.Rows[i]["NoUrut"].ToString() + "' AND NoFU = (SELECT TOP 1 NoFU FROM ISC064_MARKETINGJUAL..MS_FOLLOWUP WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' AND NoTagihan = '" + rs.Rows[i]["NoUrut"].ToString() + "' ORDER BY NoFU DESC)");

                    string Ket = Db.SingleString("SELECT NamaGrouping FROM ISC064_MARKETINGJUAL..MS_FOLLOWUP WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' "
                                + " AND NoTagihan = '" + rs.Rows[i]["NoUrut"].ToString() + "' AND NoFU = (SELECT TOP 1 NoFU FROM ISC064_MARKETINGJUAL..MS_FOLLOWUP WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' AND NoTagihan = '" + rs.Rows[i]["NoUrut"].ToString() + "' ORDER BY NoFU DESC)");

                    c = new TableCell();
                    c.Text = Ket;
                    c.HorizontalAlign = HorizontalAlign.Left;
                    c.Wrap = false;
                    tr.Cells.Add(c);

                    c = new TableCell();
                    c.Text = InfoTerakhir;
                    c.HorizontalAlign = HorizontalAlign.Left;
                    c.Wrap = false;
                    tr.Cells.Add(c);
                }

                rpt.Rows.Add(tr);

                //t6 += Convert.ToDecimal(den);
                //st5 += Convert.ToDecimal(den);
            }

            if (rs.Rows.Count > 0)
            {
                index++;
                SubTotal(st1, st2, st3, st4, st5);
            }
        }

        protected void SubTotal(decimal st1, decimal st2, decimal st3, decimal st4, decimal st5)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = Rpt.Foot();
            c.Text = "SUB TOTAL";
            c.ColumnSpan = 7;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(st1);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = "&nbsp;";
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(st2);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = "&nbsp;";
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(st3);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = "&nbsp;";
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(st4);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = "&nbsp;";
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = "&nbsp;";
            c.ColumnSpan = 2;
            r.Cells.Add(c);

            rpt.Rows.Add(r);
        }

        protected void GrandTotal(decimal t1, decimal t2, decimal t3, decimal t4, decimal t5, decimal t6)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = Rpt.Foot();
            c.Text = "GRAND TOTAL";
            c.ColumnSpan = 5;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t1);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = "&nbsp;";
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t2);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = "&nbsp;";
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t3);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = "&nbsp;";
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t4);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = "&nbsp;";
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t5);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = "&nbsp;";
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = "&nbsp;";
            c.ColumnSpan = 2;
            r.Cells.Add(c);

            //c = Rpt.Foot();
            //c.Text = Cf.Num(String.Format("{0:0.00}",t6));
            //c.HorizontalAlign = HorizontalAlign.Right;
            //r.Cells.Add(c);

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
            ddlAgent.Items.Clear();
            ddlAgent.Items.Add(new ListItem("SEMUA"));

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
            ddlAgent.Items.Clear();
            ddlAgent.Items.Add(new ListItem("SEMUA"));
            init();
        }
    }
}

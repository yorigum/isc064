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
    public partial class LapTunggakan : System.Web.UI.Page
    {


        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();

            if (!Page.IsPostBack)
            {
                Act.PersList(pers);
                Act.ProjectList(project);
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
            dari.Text = Cf.Day(DateTime.Today);
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
                Report(false);
            }
        }
        protected void xls_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                Report(true);
                //Rpt.ToExcel(this,rpt);
                Rpt.ToExcel(this, headReport, rpt);
            }
        }

        protected void pdf_Click(object sender, System.EventArgs e)
        {

            Process p = new System.Diagnostics.Process();

            string Nama = "Laporan Tunggakan";
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
                    + ",'" + null + "'"
                    );

            //get nomor customer terbaru
            int NoAttachment = Db.SingleInteger(
                "SELECT TOP 1 AttachmentID FROM ISC064_FINANCEAR..LapPDF ORDER BY AttachmentID DESC");

            string strSql = "SELECT * FROM ISC064_FINANCEAR..LapPDF WHERE AttachmentID  = '" + NoAttachment + "'";
            DataTable rs = Db.Rs(strSql);

            string nfilename = "LaporanTunggakan" + NoAttachment + ".pdf";

            //update filename
            Db.Execute("UPDATE ISC064_FINANCEAR..LapPDF SET FileName= '" + nfilename + "' WHERE AttachmentID = " + NoAttachment);


            //folder untuk menyimpan file pdf
            string save = Mi.PathFilePDFReport + "LaporanTunggakan" + rs.Rows[0]["AttachmentID"] + ".pdf";

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

            //link untuk download pdf
            string link = Mi.PathAlamatWeb + "collection/LaporanPDF/PDFLapTunggakan.aspx?id=" + rs.Rows[0]["AttachmentID"]
                + "&carabayar=" + strcarabayar
                + "&status_a=" + nStatusA
                + "&status_b=" + nStatusB
                + "&status_s=" + nStatusS
                + "&statuskpa=" + StatusKPA
                + "&lokasi=" + lokasi.SelectedValue.Replace(" ", "%")
                + "&userid=" + UserID
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
        private void Report(bool x)
        {
            param.Visible = false;
            rpt.Visible = true;
            if (x == true)
            {
                Fill(x);
            }
            else
            {
                Fill(false);
            }
            //Fill(false);
            Header();
        }

        private void Header()
        {
            System.Text.StringBuilder x = new System.Text.StringBuilder();

            Rpt.Judul(x, comp, judul);

            if (statusA.Checked)
                Rpt.SubJudul(x, "Status <b style='padding-left:35px'>:</b> " + statusA.Text);
            else if (statusB.Checked)
                Rpt.SubJudul(x, "Status <b style='padding-left:35px'>:</b> " + statusB.Text);
            else
                Rpt.SubJudul(x, "Status <b style='padding-left:35px'>:</b> " + statusS.Text);

            DateTime Dari = Convert.ToDateTime(dari.Text);
            Rpt.SubJudul(x
                , "Tanggal <b style='padding-left:25px'>:</b> " + Cf.Day(dari.Text)
                );
            Rpt.SubJudul(x
                , "Cara Bayar <b style='padding-left:6px'>:</b> " + Rpt.inSql(carabayar).Replace("'", "")
                );
            Rpt.SubJudul(x
                , "Lokasi <b style='padding-left:35px'>:</b> " + lokasi.SelectedValue
                );
            Rpt.SubJudul(x
                , "Perusahaan : " + pers.SelectedValue
                );
            Rpt.SubJudul(x
                , "Project <b style='padding-left:28px'>:</b> " + project.SelectedValue
                );

            string legend = "<br />Status: A = Aktif / S = Settled / U = Upgraded.";
            //Rpt.Header(rpt, x);
            Rpt.HeaderReport(headReport, legend, x);
        }

        private void Fill(bool excel)
        {
            string Status = "";
            if (statusA.Checked) Status = " AND a.Status = 'A'";
            if (statusB.Checked) Status = " AND a.Status = 'B'";

            string KPR = "";
            if (kpa1.Checked)
            {
                KPR = " ";
            }
            else if (kpa2.Checked)
            {
                KPR = " AND b.KPR != '1' ";
            }

            DateTime Dari = Convert.ToDateTime(dari.Text);

            string Lokasi = "";
            if (lokasi.SelectedIndex != 0)
            {
                Lokasi = " AND a.Lokasi = '" + Cf.Str(lokasi.SelectedValue) + "'";
            }

            string Project = " AND a.Project IN (" + Act.ProjectListSql + ")";
            if (project.SelectedValue != "SEMUA") Project = " AND a.Project = '" + project.SelectedValue + "'";
            string Perusahaan = "";
            if (pers.SelectedValue != "SEMUA") Perusahaan = " AND a.Pers = '" + pers.SelectedValue + "'";

            string aa = "";
            if (UserAgent() > 0)
                aa = " AND a.NoAgent = " + UserAgent();

            string strSql = "SELECT"
                + " a.NoKontrak"
                + ",a.CaraBayar"
                + ",NamaTagihan"
                + ",TglJT"
                + ",NilaiTagihan"
                + ",NoUrut"
                + ",datediff(day,convert(datetime,TglJT,112),'" + Cf.Tgl112(Dari) + "') as telat"
                + ", a.NoCustomer"
                + ", a.NoUnit"
                + ", b.KPR as KPR"
                + " FROM ISC064_MARKETINGJUAL..MS_KONTRAK a INNER JOIN ISC064_MARKETINGJUAL..MS_TAGIHAN b"
                + "		ON a.NoKontrak = b.NoKontrak"
                + " WHERE 1=1"
                + " AND ((SELECT ISNULL(SUM(NilaiPelunasan),0) as pelunasan FROM ISC064_MARKETINGJUAL..MS_PELUNASAN"
                + " WHERE NoKontrak = a.NoKontrak AND NoTagihan = b.NoUrut) < NilaiTagihan)"
                + " AND TglJT < '" + Dari + "' "
                + " AND a.CaraBayar IN(" + Rpt.inSql(carabayar) + ")"
                + Project
                + Perusahaan
                + Lokasi
                + KPR
                + Status
                + aa
                + " ORDER BY a.NoKontrak ASC";

            decimal a1 = 0;
            decimal a2 = 0;
            decimal a3 = 0;

            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                r.VerticalAlign = VerticalAlign.Top;

                c = new TableCell();
                c.Text = (i + 1).ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoKontrak"].ToString() + "." + rs.Rows[i]["NoUrut"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Db.SingleString("SELECT Nama FROM ISC064_MARKETINGJUAL..MS_CUSTOMER WHERE NoCustomer = " + rs.Rows[i]["NoCustomer"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                if (excel == false)
                {
                    c.Text = Db.SingleString("SELECT NoTelp FROM ISC064_MARKETINGJUAL..MS_CUSTOMER WHERE NoCustomer = " + rs.Rows[i]["NoCustomer"]);
                }
                else
                {
                    string petik = "'";
                    c.Text = petik + Db.SingleString("SELECT NoTelp FROM ISC064_MARKETINGJUAL..MS_CUSTOMER WHERE NoCustomer = " + rs.Rows[i]["NoCustomer"]);
                }
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();

                if (excel == false)
                {
                    c.Text = Db.SingleString("SELECT NoHP FROM ISC064_MARKETINGJUAL..MS_CUSTOMER WHERE NoCustomer = " + rs.Rows[i]["NoCustomer"]);
                }
                else
                {
                    string petik = "'";
                    c.Text = petik + Db.SingleString("SELECT NoHP FROM ISC064_MARKETINGJUAL..MS_CUSTOMER WHERE NoCustomer = " + rs.Rows[i]["NoCustomer"]);
                }
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
                c.Text = Cf.Day(rs.Rows[i]["TglJT"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["telat"].ToString();
                c.HorizontalAlign = HorizontalAlign.Center;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["NilaiTagihan"].ToString());
                a1 = a1 + Convert.ToDecimal(c.Text);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan),0) FROM ISC064_MARKETINGJUAL..MS_PELUNASAN WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' AND NoTagihan = '" + rs.Rows[i]["NoUrut"].ToString() + "'"));
                a2 = a2 + Convert.ToDecimal(c.Text);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                decimal sisa = 0;
                decimal n1 = (decimal)rs.Rows[i]["NilaiTagihan"];
                decimal n2 = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan),0) FROM ISC064_MARKETINGJUAL..MS_PELUNASAN WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' AND NoTagihan = '" + rs.Rows[i]["NoUrut"].ToString() + "'");
                string txt = "";
                sisa = n1 - n2;

                c = new TableCell();
                c.Text = Cf.Num(sisa);
                a3 = a3 + Convert.ToDecimal(c.Text);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                rpt.Rows.Add(r);
                if (i == rs.Rows.Count - 1)
                {
                    SubTotal(txt, a1, a2, a3);
                }
            }
        }
        private void SubTotal(string txt, decimal a1, decimal a2, decimal a3)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = Rpt.Foot();
            c.Text = txt;
            c.ColumnSpan = 9;
            c.Text = "<b>GRAND TOTAL</b>";
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(a1);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(a2);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(a3);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            rpt.Rows.Add(r);
        }

        protected void carabayarCheck_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < carabayar.Items.Count; i++)
            {
                carabayar.Items[i].Selected = carabayarCheck.Checked;
            }

            Js.Focus(this, carabayarCheck);
            carabayarc.Text = "";
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

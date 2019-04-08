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
    public partial class PenerimaanCustomer : System.Web.UI.Page
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

            string Nama = "Laporan Penerimaan Customer";
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
                    + ",'" + Convert.ToDateTime(tgl.Text) + "'"
                    + ",'" + null + "'"
                    );

            //get nomor customer terbaru
            int NoAttachment = Db.SingleInteger(
                "SELECT TOP 1 AttachmentID FROM LapPDF ORDER BY AttachmentID DESC");

            string strSql = "SELECT * FROM ISC064_FINANCEAR..LapPDF WHERE AttachmentID  = '" + NoAttachment + "'";
            DataTable rs = Db.Rs(strSql);

            string nfilename = "PenerimaanCustomer" + NoAttachment + ".pdf";

            //update filename
            Db.Execute("UPDATE ISC064_FINANCEAR..LapPDF SET FileName= '" + nfilename + "' WHERE AttachmentID = " + NoAttachment);


            //folder untuk menyimpan file pdf
            string save = Mi.PathFilePDFReport + "PenerimaanCustomer" + rs.Rows[0]["AttachmentID"] + ".pdf";

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

            string TglAsOf = tgl.Text;


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
            string link = Mi.PathAlamatWeb + "collection/LaporanPDF/PDFPenerimaanCustomer.aspx?id=" + rs.Rows[0]["AttachmentID"]
                + "&status_a=" + nStatusA
                + "&status_b=" + nStatusB
                + "&status_s=" + nStatusS
                + "&lokasi=" + lokasi.SelectedValue
                + "&statuskpa=" + StatusKPA
                + "&userid=" + UserID
                + "&pers=" + pers.SelectedValue
                + "&project=" + Project
                ;

            //update link
            Db.Execute("UPDATE ISC064_FINANCEAR..LapPDF SET Link= '" + link + "' WHERE AttachmentID = " + NoAttachment);

            //format page
            p.StartInfo.Arguments = "--orientation landscape --page-width 8.5in --page-height 16.5in --margin-left 0 --margin-right 0 --margin-top 0.25cm --margin-bottom 0 " + link + " " + save;

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

            if (statusA.Checked)
                x.Append("Status : " + statusA.Text);
            else if (statusB.Checked)
                x.Append("Status : " + statusB.Text);
            else
                x.Append("Status : " + statusS.Text);

            x.Append("<br />As of : " + Cf.Day(tgl.Text));
            x.Append("<br />Lokasi : " + lokasi.SelectedValue);
            x.Append("<br />Perusahaan : " + pers.SelectedValue);
            x.Append("<br />Project : " + project.SelectedValue);

            string legend = "<br />Status : A = Aktif / B = Batal.<br />";
            Rpt.HeaderReport(headReport, legend, x);

            Fill();
        }

        private void Fill()
        {
            string Status = "";
            if (statusA.Checked) Status = " AND ISC064_MARKETINGJUAL..MS_KONTRAK.Status = 'A'";
            if (statusB.Checked) Status = " AND ISC064_MARKETINGJUAL..MS_KONTRAK.Status = 'B'";

            decimal total = 0;
            decimal t2 = 0;
            decimal t1 = 0;
            decimal t3 = 0;
            decimal t4 = 0;
            decimal t5 = 0;
            decimal t6 = 0;

            DateTime tanggal = Convert.ToDateTime(tgl.Text);

            string Lokasi = "";
            if (lokasi.SelectedIndex != 0)
            {
                Lokasi = " AND ISC064_MARKETINGJUAL..MS_KONTRAK.Lokasi = '" + Cf.Str(lokasi.SelectedValue) + "'";
            }

            string KPR = "";
            if (kpa1.Checked)
            {
                KPR = " ";
            }
            else if (kpa2.Checked)
            {
                KPR = " AND MS_KONTRAK.NoKontrak IN (SELECT NoKontrak FROM ISC064_MARKETINGJUAL..MS_TAGIHAN WHERE Nokontrak = MS_KONTRAK.NoKontrak AND KPR != 1)";//" AND ISC064_MARKETINGJUAL..MS_TAGIHAN.KPR = '1' ";
            }

            string Project = " AND ISC064_MARKETINGJUAL..MS_KONTRAK.Project IN (" + Act.ProjectListSql + ")";
            if (project.SelectedValue != "SEMUA") Project = " AND ISC064_MARKETINGJUAL..MS_KONTRAK.Project = '" + project.SelectedValue + "'";
            string Perusahaan = "";
            if (pers.SelectedValue != "SEMUA") Perusahaan = " AND ISC064_MARKETINGJUAL..MS_KONTRAK.Pers = '" + pers.SelectedValue + "'";

            string aa = "";
            if (UserAgent() > 0)
                aa = " AND " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK.NoAgent = " + UserAgent();

            string strSql = "SELECT "
                + " ISC064_MARKETINGJUAL..MS_KONTRAK.TglKontrak"
                + ",ISC064_MARKETINGJUAL..MS_KONTRAK.NoKontrak"
                + ",ISC064_MARKETINGJUAL..MS_KONTRAK.NoUnit"
                + ",ISC064_MARKETINGJUAL..MS_KONTRAK.NilaiKontrak"
                + ",ISC064_MARKETINGJUAL..MS_CUSTOMER.Nama AS Cs"
                + ",ISC064_MARKETINGJUAL..MS_AGENT.Nama AS Agent"
                + ",(SELECT ISNULL(SUM(NilaiPelunasan),0) FROM ISC064_MARKETINGJUAL..MS_PELUNASAN a join ISC064_marketingjual..ms_tagihan b on a.NoKontrak = b.NoKontrak WHERE a.NoKontrak = ISC064_MARKETINGJUAL..MS_KONTRAK.NoKontrak AND a.TglPelunasan < '" + Cf.AwalBulan(tanggal.Month, tanggal.Year) + "' AND b.Tipe <> 'ADM' and a.NoTagihan = b.NoUrut) AS Lalu"
                + ",(SELECT ISNULL(SUM(NilaiPelunasan),0) FROM ISC064_MARKETINGJUAL..MS_PELUNASAN a join ISC064_marketingjual..ms_tagihan b on a.NoKontrak = b.NoKontrak WHERE a.NoKontrak = ISC064_MARKETINGJUAL..MS_KONTRAK.NoKontrak AND a.TglPelunasan >= '" + Cf.AwalBulan(tanggal.Month, tanggal.Year) + "' AND a.TglPelunasan <= '" + Cf.AwalBulan1(tanggal.Month, tanggal.Year, tanggal.Day) + "' AND b.Tipe <> 'ADM' and a.NoTagihan = b.NoUrut) AS Berjalan"
                + " FROM ISC064_MARKETINGJUAL..MS_KONTRAK INNER JOIN ISC064_MARKETINGJUAL..MS_CUSTOMER ON ISC064_MARKETINGJUAL..MS_KONTRAK.NoCustomer = ISC064_MARKETINGJUAL..MS_CUSTOMER.NoCustomer"
                + " INNER JOIN ISC064_MARKETINGJUAL..MS_AGENT ON ISC064_MARKETINGJUAL..MS_KONTRAK.NoAgent = ISC064_MARKETINGJUAL..MS_AGENT.NoAgent"
                + " WHERE 1=1 "
                + Perusahaan
                + Project
                + KPR
                + Lokasi
                + Status
                + aa;

            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                int no = i + 1;

                decimal KPALalu = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan),0) FROM "
                + " ISC064_MARKETINGJUAL..MS_PELUNASAN A INNER JOIN "
                + " ISC064_MARKETINGJUAL..MS_TAGIHAN B on A.NoTagihan = B.NoUrut AND A.NoKontrak = B.NoKontrak "
                + " WHERE TglPelunasan < '" + Cf.AwalBulan(tanggal.Month, tanggal.Year) + "' "
                + " AND B.KPR = '1' "
                + " AND B.TIPE <> 'ADM' "
                + " AND A.NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' ");

                decimal BerjalanLalu = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan),0) FROM "
                + " ISC064_MARKETINGJUAL..MS_PELUNASAN A INNER JOIN "
                + " ISC064_MARKETINGJUAL..MS_TAGIHAN B on A.NoTagihan = B.NoUrut AND A.NoKontrak = B.NoKontrak "
                + " WHERE TglPelunasan >= '" + Cf.AwalBulan(tanggal.Month, tanggal.Year) + "' "
                + " AND TglPelunasan <= '" + Cf.AwalBulan1(tanggal.Month, tanggal.Year, tanggal.Day) + "' "
                + " AND B.KPR = '1' "
                + " AND B.TIPE <> 'ADM' "
                + " AND A.NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' ");

                decimal NilaiLalu = Convert.ToDecimal(rs.Rows[i]["Lalu"]);
                decimal NilaiBerjalan = Convert.ToDecimal(rs.Rows[i]["Berjalan"]);

                if (kpa2.Checked)
                {
                    NilaiLalu -= KPALalu;
                    NilaiBerjalan -= BerjalanLalu;
                }

                decimal sekarang = NilaiLalu + NilaiBerjalan;
                decimal saldo = (decimal)rs.Rows[i]["NilaiKontrak"] - sekarang;

                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                c = new TableCell();
                c.Text = Cf.Str(no);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglKontrak"]);
                c.HorizontalAlign = HorizontalAlign.Left;
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
                c.Text = rs.Rows[i]["Agent"].ToString();
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
                c.Text = Cf.Num(NilaiLalu);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(NilaiBerjalan);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(sekarang);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(saldo);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                decimal adm = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiTagihan),0)FROM ISC064_MARKETINGJUAL..MS_TAGIHAN WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' AND Tipe = 'ADM' ");
                c.Text = Cf.Num(adm.ToString());
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                decimal admterima = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan),0)FROM ISC064_MARKETINGJUAL..MS_PELUNASAN a join ISC064_MARKETINGJUAL..MS_TAGIHAN b ON a.NoKontrak = b.NoKontrak WHERE a.NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' AND Tipe = 'ADM' AND a.NoTagihan = b.NoUrut AND a.TglPelunasan <= '" + Cf.AwalBulan1(tanggal.Month, tanggal.Year, tanggal.Day) + "'");
                c.Text = Cf.Num(admterima.ToString());
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                rpt.Rows.Add(r);

                total = total + (decimal)rs.Rows[i]["NilaiKontrak"];
                t2 = t2 + (decimal)rs.Rows[i]["NilaiKontrak"];
                t1 = t1 + NilaiLalu;
                t3 = t3 + NilaiBerjalan;
                t4 = t4 + sekarang;
                t5 = t5 + adm;
                t6 = t6 + admterima;

                if (i == rs.Rows.Count - 1)
                    SubTotal("GRAND TOTAL", total, t2, t1, t3, t4, t5, t6);
            }
        }

        private string DetilLunas(string NoKontrak, int NoTagihan)
        {
            System.Text.StringBuilder x = new System.Text.StringBuilder();

            DataTable rs = Db.Rs("SELECT CaraBayar, TglPelunasan, Ket FROM ISC064_MARKETINGJUAL..MS_PELUNASAN"
                + " WHERE NoKontrak = '" + NoKontrak + "' AND NoTagihan = " + NoTagihan
                );
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (x.Length != 0) x.Append("<br>");
                x.Append(rs.Rows[i]["CaraBayar"] + ", " + Cf.Day(rs.Rows[i]["TglPelunasan"]) + " "
                    + rs.Rows[i]["Ket"]);
            }

            return x.ToString();
        }

        private void SubTotal(string txt, decimal total, decimal t2, decimal t1, decimal t3, decimal t4, decimal t5, decimal t6)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = Rpt.Foot();
            c.Text = txt;
            c.ColumnSpan = 6;
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(total);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t1);
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
            c.Text = Cf.Num(Math.Round(t2 - t4));
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t5);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t6);
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

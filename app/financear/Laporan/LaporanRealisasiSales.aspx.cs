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
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
namespace ISC064.FINANCEAR.Laporan
{
    public partial class LaporanRealisasiSales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            comp.InnerHtml = Mi.Pt;
            rpt.Visible = false;
            Js.Focus(this, scr);
            rpt.Style["border-collapse"] = "collapse";
            Cf.BindTahun2(tahundari);
            Cf.BindTahun2(tahunsampai);

            if (!Page.IsPostBack)
            {
                Act.ProjectList(project);
                Act.PersList(pers);
                initLokasi();
                initLantai();
            }



        }
        private void initLokasi()
        {
            tower.Items.Clear();
            //lt.Items.Clear();
            //dari.Text = Cf.Day(DateTime.Today);            
            DataTable rs = Db.Rs("SELECT * FROM ISC064_MARKETINGJUAL..REF_LOKASI WHERE Project = '" + project.SelectedValue + "'");
            tower.Items.Add(new ListItem("SEMUA", "0"));
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["Lokasi"].ToString();
                string t = rs.Rows[i]["Nama"].ToString();
                tower.Items.Add(new ListItem(t, v));
            }


        }

        private void initLantai()
        {
            lt.Items.Clear();
            DataTable rs = Db.Rs("SELECT DISTINCT(Lantai) FROM ISC064_MARKETINGJUAL..MS_UNIT WHERE Project = '" + project.SelectedValue + "' AND Lokasi = '" + tower.SelectedValue + "'");

            lt.Items.Add(new ListItem("SEMUA", "0"));
            //string[] Lt = Param.Lantai.Split(',');
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                lt.Items.Add(new ListItem(rs.Rows[i]["Lantai"].ToString(), rs.Rows[i]["Lantai"].ToString()));
            }

        }
        protected void scr_Click(object sender, System.EventArgs e)
        {

            Report();
        }
        protected void xls_Click(object sender, System.EventArgs e)
        {

            Report();
            Rpt.ToExcel(this, headReport, rpt);

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

            x.Append("<br>Laporan dihasilkan pada hari : " + Cf.IndoWeek(DateTime.Today)
               + ", " + Cf.Date(DateTime.Now)
               + " dari workstation : " + Act.IP
               + " dan username : " + Act.UserID);

            lblHeader.Text = "<h3>" + Mi.Pt + "</h3>"
                + "<h1 class='title'>LAPORAN REALISASI SALES & CASH IN</h1>"
                + "Periode " + Cf.Monthname(Convert.ToInt32(bulandari.SelectedValue)) + " " + Convert.ToInt32(tahundari.SelectedValue)
                + " s/d " + Cf.Monthname(Convert.ToInt32(bulansampai.SelectedValue)) + " " + Convert.ToInt32(tahunsampai.SelectedValue)
                + "<br />"
                + "Project : " + project.SelectedValue
                + "<br>Perusahaan : " + pers.SelectedItem.Text
                + "<br>Lokasi : " + tower.SelectedItem.Text
                + "<br>Lantai : " + lt.SelectedItem.Text
                + x
                + "<br />"
                + "<br />"

                ;
        }
        protected void pdf_Click(object sender, System.EventArgs e)
        {

            Process p = new System.Diagnostics.Process();

            string Nama = "Laporan Realisasi Sales";
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

            string nfilename = "RealisasiSales" + NoAttachment + ".pdf";

            //update filename
            Db.Execute("UPDATE ISC064_FINANCEAR..LapPDF SET FileName= '" + nfilename + "' WHERE AttachmentID = " + NoAttachment);

            //folder untuk menyimpan file pdf
            string save = Mi.PathFilePDFReport + "RealisasiSales" + rs.Rows[0]["AttachmentID"] + ".pdf";

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
            string link = Mi.PathAlamatWeb + "financear/LaporanPDF/PDFLaporanRealisasiSales.aspx?id=" + rs.Rows[0]["AttachmentID"]
                + "&userid=" + UserID
                + "&dari=" + bulandari.SelectedValue
                + "&sampai=" + bulansampai.SelectedValue
                + "&thndari=" + tahundari.SelectedValue
                + "&thnsampai=" + tahunsampai.SelectedValue
                + "&tower=" + tower.SelectedValue.Replace(" ", "%")
                + "&lantai=" + lt.SelectedValue
                + "&project=" + Project
                + "&pers=" + pers.SelectedValue
                ;

            //update link
            Db.Execute("UPDATE ISC064_FINANCEAR..LapPDF SET Link= '" + link + "' WHERE AttachmentID = " + NoAttachment);

            //format page
            p.StartInfo.Arguments = "--orientation landscape --page-width 8.5in --page-height 20in --margin-left 0 --margin-right 0 --margin-top 0.25cm --margin-bottom 0 " + link + " " + save;

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
            //DateTime Dari = Convert.ToDateTime(dari.Text);
            string Project = " AND a.Project IN (" + Act.ProjectListSql + ")";
            if (project.SelectedValue != "SEMUA") Project = " AND a.Project = '" + project.SelectedValue + "'";
            string Perusahaan = "";
            if (pers.SelectedValue != "SEMUA") Perusahaan = " AND a.Pers = '" + pers.SelectedValue + "'";

            decimal t1 = 0;
            decimal t2 = 0;
            decimal t3 = 0;
            decimal t4 = 0;
            decimal t5 = 0;
            decimal t6 = 0;
            string lantai = "";
            string to = "";

            if (lt.SelectedValue != "0")
            {
                lantai = " AND LEFT(c.NoUnit,7) like '%" + lt.SelectedValue + "%'";
            }

            if (tower.SelectedValue != "0")
            {
                to = " AND c.Lokasi ='" + tower.SelectedValue + "'";
            }

            string strSql = "SELECT a.*, a.Status, b.Nama AS Cust"
                + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK a"
                + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER b ON a.NoCustomer = b.NoCustomer"
                + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT c ON a.NoUnit = c.NoUnit"
                + " WHERE 1=1 "
                + Project
                + Perusahaan
                + lantai
                + to
                + " ORDER BY a.Status, a.TglKontrak";

            DataTable rs = Db.Rs(strSql);

            TableHeaderRow trow = new TableHeaderRow();
            TableHeaderCell tc;

            //trow.BackColor = Color.LightGray;
            trow.HorizontalAlign = HorizontalAlign.Center;

            tc = new TableHeaderCell();
            tc.Text = "NO.";
            tc.RowSpan = 2;
            tc.Wrap = false;
            tc.ForeColor = Color.White;
            tc.Attributes["style"] = "background-color:#1E90FF";
            trow.Cells.Add(tc);

            tc = new TableHeaderCell();
            tc.Text = "CUSTOMER";
            tc.RowSpan = 2;
            tc.Wrap = false;
            tc.ForeColor = Color.White;
            tc.Attributes["style"] = "background-color:#1E90FF";
            trow.Cells.Add(tc);

            tc = new TableHeaderCell();
            tc.Text = "STATUS";
            tc.RowSpan = 2;
            tc.Wrap = false;
            tc.ForeColor = Color.White;
            tc.Attributes["style"] = "background-color:#1E90FF";
            trow.Cells.Add(tc);

            tc = new TableHeaderCell();
            tc.Text = "TGL BF";
            tc.RowSpan = 2;
            tc.Wrap = false;
            tc.ForeColor = Color.White;
            tc.Attributes["style"] = "background-color:#1E90FF";
            trow.Cells.Add(tc);

            tc = new TableHeaderCell();
            tc.Text = "TYPE";
            tc.ColumnSpan = 4;
            tc.Wrap = false;
            tc.ForeColor = Color.White;
            tc.Attributes["style"] = "background-color:#1E90FF";
            trow.Cells.Add(tc);

            tc = new TableHeaderCell();
            tc.Text = "HARGA";
            tc.RowSpan = 2;
            tc.Wrap = false;
            tc.ForeColor = Color.White;
            tc.Attributes["style"] = "background-color:#1E90FF";
            trow.Cells.Add(tc);

            tc = new TableHeaderCell();
            tc.Text = "PPN";
            tc.RowSpan = 2;
            tc.Wrap = false;
            tc.ForeColor = Color.White;
            tc.Attributes["style"] = "background-color:#1E90FF";
            trow.Cells.Add(tc);

            tc = new TableHeaderCell();
            tc.Text = "TOTAL HARGA";
            tc.RowSpan = 2;
            tc.Wrap = false;
            tc.ForeColor = Color.White;
            tc.Attributes["style"] = "background-color:#1E90FF";
            trow.Cells.Add(tc);

            tc = new TableHeaderCell();
            tc.Text = "TYPE OF PAYMENT";
            tc.RowSpan = 2;
            tc.Wrap = false;
            tc.ForeColor = Color.White;
            tc.Attributes["style"] = "background-color:#1E90FF";
            trow.Cells.Add(tc);

            tc = new TableHeaderCell();
            tc.Text = "Akumulasi Sebelum " + Cf.Monthname(Convert.ToInt32(bulandari.SelectedValue)) + " " + Convert.ToInt32(tahundari.SelectedValue);
            tc.RowSpan = 2;
            tc.Wrap = false;
            tc.ForeColor = Color.White;
            tc.Attributes["style"] = "background-color:#1E90FF";
            trow.Cells.Add(tc);

            DateTime awal = new DateTime(Convert.ToInt32(tahundari.SelectedValue), Convert.ToInt32(bulandari.SelectedValue), 1);
            DateTime akhir = Cf.AkhirBulan(Convert.ToInt32(bulansampai.SelectedValue), Convert.ToInt32(tahunsampai.SelectedValue));

            var listOfMonths = new List<string>();
            var list = new List<string>();

            while (awal <= akhir)
            {
                listOfMonths.Add(Cf.Monthname(awal.ToString("MMMM")) + " " + awal.ToString("yyyy"));
                list.Add(awal.ToString("MM-yyyy"));
                awal = awal.AddMonths(1);
            }

            foreach (var r in listOfMonths)
            {
                tc = new TableHeaderCell();
                tc.Text = r.ToString();
                tc.ColumnSpan = 4;
                tc.Wrap = false;
                tc.ForeColor = Color.White;
                tc.Attributes["style"] = "background-color:#1E90FF";
                trow.Cells.Add(tc);
            }
            tc = new TableHeaderCell();
            tc.Text = "Total Penerimaan Hingga " + Cf.Monthname(Convert.ToInt32(bulansampai.SelectedValue)) + " " + Convert.ToInt32(tahunsampai.Text) + " (Angsuran dan Saldo Awal)";
            tc.RowSpan = 2;
            tc.Wrap = false;
            tc.ForeColor = Color.White;
            tc.Attributes["style"] = "background-color:#1E90FF";
            trow.Cells.Add(tc);

            tc = new TableHeaderCell();
            tc.Text = "Total Penerimaan Lain-Lain Hingga " + Cf.Monthname(Convert.ToInt32(bulansampai.SelectedValue)) + " " + Convert.ToInt32(tahunsampai.Text) + " (Admin dan Memo Selain Saldo Awal)";
            tc.RowSpan = 2;
            tc.Wrap = false;
            tc.ForeColor = Color.White;
            tc.Attributes["style"] = "background-color:#1E90FF";
            trow.Cells.Add(tc);

            tc = new TableHeaderCell();
            tc.Text = "Persentase Penerimaan";
            tc.RowSpan = 2;
            tc.Wrap = false;
            tc.ForeColor = Color.White;
            tc.Attributes["style"] = "background-color:#1E90FF";
            trow.Cells.Add(tc);

            tc = new TableHeaderCell();
            tc.Text = "Sisa Angsuran";
            tc.RowSpan = 2;
            tc.Wrap = false;
            tc.ForeColor = Color.White;
            tc.Attributes["style"] = "background-color:#1E90FF";
            trow.Cells.Add(tc);


            rpt.Rows.Add(trow);

            TableHeaderRow trow1 = new TableHeaderRow();
            TableHeaderCell tc1;

            //trow1.BackColor = Color.LightGray;
            trow1.HorizontalAlign = HorizontalAlign.Center;

            tc1 = new TableHeaderCell();
            tc1.Text = "NO UNIT";
            tc1.ForeColor = Color.White;
            tc1.Attributes["style"] = "background-color:#1E90FF";
            trow1.Cells.Add(tc1);

            tc1 = new TableHeaderCell();
            tc1.Text = "TOWER";
            tc1.ForeColor = Color.White;
            tc1.Attributes["style"] = "background-color:#1E90FF";
            trow1.Cells.Add(tc1);

            tc1 = new TableHeaderCell();
            tc1.Text = "LANTAI";
            tc1.ForeColor = Color.White;
            tc1.Attributes["style"] = "background-color:#1E90FF";
            trow1.Cells.Add(tc1);

            tc1 = new TableHeaderCell();
            tc1.Text = "LUAS";
            tc1.ForeColor = Color.White;
            tc1.Attributes["style"] = "background-color:#1E90FF";
            trow1.Cells.Add(tc1);

            foreach (var r in list)
            {
                string[] a = Cf.SplitByString(r.ToString(), "-");
                tc1 = new TableHeaderCell();
                tc1.Text = "1  s/d  7 ";
                tc1.ForeColor = Color.White;
                tc1.Attributes["style"] = "background-color:#1E90FF";
                trow1.Cells.Add(tc1);

                tc1 = new TableHeaderCell();
                tc1.Text = "8  s/d  14 ";
                tc1.ForeColor = Color.White;
                tc1.Attributes["style"] = "background-color:#1E90FF";
                trow1.Cells.Add(tc1);

                tc1 = new TableHeaderCell();
                tc1.Text = "15  s/d  21 ";
                tc1.ForeColor = Color.White;
                tc1.Attributes["style"] = "background-color:#1E90FF";
                trow1.Cells.Add(tc1);

                tc1 = new TableHeaderCell();
                tc1.Text = "22  s/d  " + Cf.AkhirBulan(Convert.ToInt32(a[0]), Convert.ToInt32(a[1])).Day;
                tc1.ForeColor = Color.White;
                tc1.Attributes["style"] = "background-color:#1E90FF";
                trow1.Cells.Add(tc1);
            }

            rpt.Rows.Add(trow1);



            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                c = new TableCell();
                c.Text = (i + 1).ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Cust"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                if (rs.Rows[i]["Status"].ToString() == "A")
                {
                    c.Text = "<b>Aktif</b>";
                }
                else
                {
                    c.Text = "Batal";
                }
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglKontrak"]);
                c.Wrap = false;
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoUnit"].ToString();
                c.Wrap = false;
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Lokasi"].ToString();
                c.Wrap = false;
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                string nProject = Db.SingleString("SELECT Project FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT WHERE NoUnit = '" + rs.Rows[i]["NoUnit"] + "'");
                string ParamID = "FormatUnit" + nProject;
                string pemisah = Db.SingleString("SELECT Value FROM " + Mi.DbPrefix + "SECURITY..REF_PARAM WHERE ParamID = '" + ParamID + "'");
                string[] x = Cf.SplitByString(rs.Rows[i]["NoUnit"].ToString(), pemisah);
                c = new TableCell();
                c.Text = x[1];
                c.Wrap = false;
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["Luas"]) + "m<sup>2</sup>";
                c.Wrap = false;
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Convert.ToDecimal(rs.Rows[i]["NilaiDPP"])).ToString();
                c.Wrap = false;
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Convert.ToDecimal(rs.Rows[i]["NilaiPPN"])).ToString();
                c.Wrap = false;
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Convert.ToDecimal(rs.Rows[i]["NilaiKontrak"])).ToString();
                c.Wrap = false;
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Skema"].ToString();
                c.Wrap = false;
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);


                DateTime awala = new DateTime(Convert.ToInt32(tahundari.SelectedValue), Convert.ToInt32(bulandari.SelectedValue), 1);
                c = new TableCell();
                c.Text = Cf.Num(Db.SingleDecimal("SELECT ISNULL(SUM(NILAIPELUNASAN),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE TglPelunasan < '" + Cf.Tgl112(awala) + "' AND NoKontrak='" + rs.Rows[i]["NoKontrak"].ToString() + "' AND CaraBayar!='PPA'")).ToString();
                c.Wrap = false;
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);


                foreach (var u in list)
                {
                    string[] a = Cf.SplitByString(u.ToString(), "-");
                    DateTime week1a = new DateTime(Convert.ToInt32(a[1]), Convert.ToInt32(a[0]), 1);
                    DateTime week1b = new DateTime(Convert.ToInt32(a[1]), Convert.ToInt32(a[0]), 7);
                    DateTime week2a = new DateTime(Convert.ToInt32(a[1]), Convert.ToInt32(a[0]), 8);
                    DateTime week2b = new DateTime(Convert.ToInt32(a[1]), Convert.ToInt32(a[0]), 14);
                    DateTime week3a = new DateTime(Convert.ToInt32(a[1]), Convert.ToInt32(a[0]), 15);
                    DateTime week3b = new DateTime(Convert.ToInt32(a[1]), Convert.ToInt32(a[0]), 21);
                    DateTime week4a = new DateTime(Convert.ToInt32(a[1]), Convert.ToInt32(a[0]), 22);
                    DateTime week4b = Cf.AkhirBulan(Convert.ToInt32(a[0]), Convert.ToInt32(a[1]));


                    c = new TableCell();
                    c.Text = Cf.Num(Db.SingleDecimal("SELECT ISNULL(SUM(NILAIPELUNASAN),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE TglPelunasan <= '" + Cf.Tgl112(week1b) + "' AND TglPelunasan >= '" + Cf.Tgl112(week1a) + "' AND NoKontrak='" + rs.Rows[i]["NoKontrak"].ToString() + "' AND CaraBayar!='PPA'")).ToString();
                    c.Wrap = false;
                    c.HorizontalAlign = HorizontalAlign.Left;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Cf.Num(Db.SingleDecimal("SELECT ISNULL(SUM(NILAIPELUNASAN),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE TglPelunasan <= '" + Cf.Tgl112(week2b) + "' AND TglPelunasan >= '" + Cf.Tgl112(week2a) + "' AND NoKontrak='" + rs.Rows[i]["NoKontrak"].ToString() + "' AND CaraBayar!='PPA'")).ToString();
                    c.Wrap = false;
                    c.HorizontalAlign = HorizontalAlign.Left;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Cf.Num(Db.SingleDecimal("SELECT ISNULL(SUM(NILAIPELUNASAN),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE TglPelunasan <= '" + Cf.Tgl112(week3b) + "' AND TglPelunasan >= '" + Cf.Tgl112(week3a) + "' AND NoKontrak='" + rs.Rows[i]["NoKontrak"].ToString() + "' AND CaraBayar!='PPA'")).ToString();
                    c.Wrap = false;
                    c.HorizontalAlign = HorizontalAlign.Left;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Cf.Num(Db.SingleDecimal("SELECT ISNULL(SUM(NILAIPELUNASAN),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE TglPelunasan <= '" + Cf.Tgl112(week4b) + "' AND TglPelunasan >= '" + Cf.Tgl112(week4a) + "' AND NoKontrak='" + rs.Rows[i]["NoKontrak"].ToString() + "'")).ToString();
                    c.Wrap = false;
                    c.HorizontalAlign = HorizontalAlign.Left;
                    r.Cells.Add(c);



                }
                c = new TableCell();
                c.Text = Cf.Num(Ang(rs.Rows[i]["NoKontrak"].ToString(), akhir));
                c.Wrap = false;
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(LainLain(rs.Rows[i]["NoKontrak"].ToString(), akhir));
                c.Wrap = false;
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);


                c = new TableCell();
                c.Text = Cf.Num(Convert.ToDecimal(rs.Rows[i]["PersenLunas"])).ToString() + "%";
                c.Wrap = false;
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);


                decimal tagihan = Db.SingleDecimal("SELECT ISNULL(SUM(NILAITAGIHAN),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN WHERE NoKontrak='" + rs.Rows[i]["NoKontrak"].ToString() + "'");
                decimal pelunasan = Db.SingleDecimal("SELECT ISNULL(SUM(NILAIPELUNASAN),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE NoKontrak='" + rs.Rows[i]["NoKontrak"].ToString() + "' AND CaraBayar!='PPA'");
                decimal sisa = tagihan - pelunasan;

                c = new TableCell();
                c.Text = Cf.Num(sisa).ToString();
                c.Wrap = false;
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);


                rpt.Rows.Add(r);

            }
        }

        private decimal Ang(string NoKontrak, DateTime akhir)
        {
            decimal Hasil = 0;
            decimal TTS = 0;
            decimal Memo = 0;

            DataTable rs = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN WHERE Tipe!='ADM' AND NoKontrak='" + NoKontrak + "'");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                TTS += Db.SingleDecimal("SELECT ISNULL(SUM(NILAIPELUNASAN),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE TglPelunasan < '" + Cf.Tgl112(akhir) + "' AND NoKontrak='" + NoKontrak + "' AND NoTTS != 0 AND NoTagihan=" + rs.Rows[i]["NoUrut"]);
            }

            DataTable rs2 = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN WHERE NoKontrak='" + NoKontrak + "'");
            for (int i = 0; i < rs2.Rows.Count; i++)
            {
                Memo += Db.SingleDecimal("SELECT ISNULL(SUM(NILAIPELUNASAN),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE TglPelunasan < '" + Cf.Tgl112(akhir) + "' AND NoKontrak='" + NoKontrak + "' AND NoMemo != 0 AND CaraBayar='SA' AND NoTagihan=" + rs2.Rows[i]["NoUrut"]);
            }

            Hasil = Math.Round(TTS + Memo);

            return Hasil;
        }

        private decimal LainLain(string NoKontrak, DateTime akhir)
        {
            decimal Hasil = 0;
            decimal TTS = 0;
            decimal Memo = 0;

            DataTable rs = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN WHERE Tipe='ADM' AND NoKontrak='" + NoKontrak + "'");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                TTS += Db.SingleDecimal("SELECT ISNULL(SUM(NILAIPELUNASAN),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE TglPelunasan < '" + Cf.Tgl112(akhir) + "' AND NoKontrak='" + NoKontrak + "' AND NoTTS != 0 AND NoTagihan=" + rs.Rows[i]["NoUrut"]);
            }

            DataTable rs2 = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN WHERE NoKontrak='" + NoKontrak + "'");
            for (int i = 0; i < rs2.Rows.Count; i++)
            {
                Memo += Db.SingleDecimal("SELECT ISNULL(SUM(NILAIPELUNASAN),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE TglPelunasan < '" + Cf.Tgl112(akhir) + "' AND NoKontrak='" + NoKontrak + "' AND NoMemo != 0 AND CaraBayar!='SA' AND NoTagihan=" + rs2.Rows[i]["NoUrut"]);
            }

            Hasil = Math.Round(TTS + Memo);

            return Hasil;
        }

        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            initLokasi();
        }

        protected void tower_SelectedIndexChanged(object sender, EventArgs e)
        {
            initLantai();
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
            
            initLokasi();
            initLantai();
        }
    }
}

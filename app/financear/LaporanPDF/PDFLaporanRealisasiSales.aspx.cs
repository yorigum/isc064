using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Globalization;

namespace ISC064.FINANCEAR.Laporan
{
    public partial class LaporanRealisasiSales : System.Web.UI.Page
    {
        private string UserID { get { return (Request.QueryString["userid"]); } }
        private string AttachmentID { get { return (Request.QueryString["id"]); } }
        private string Lt { get { return (Request.QueryString["lantai"]); } }
        private string Tower { get { return (Request.QueryString["tower"]); } }
        private string Project { get { return (Request.QueryString["project"]); } }
        private string Perusahaan { get { return (Request.QueryString["pers"]); } }
        private int BlnDari { get { return Convert.ToInt32((Request.QueryString["dari"])); } }
        private int BlnSampai { get { return Convert.ToInt32((Request.QueryString["sampai"])); } }
        private int ThnDari { get { return Convert.ToInt32((Request.QueryString["thndari"])); } }
        private int ThnSampai { get { return Convert.ToInt32((Request.QueryString["thnsampai"])); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            Report();
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

            x.Append("Laporan dihasilkan pada hari : " + Cf.IndoWeek(DateTime.Today)
               + ", " + Cf.Date(DateTime.Now)
               + " dari workstation : " + Act.IP
               + " dan username : " + Act.UserID);

            lblHeader.Text = "<h3>" + Mi.Pt + "</h3>"
                + "<h1 class='title'>LAPORAN REALISASI SALES & CASH IN</h1>"
                + "Periode " + Cf.Monthname(Convert.ToInt32(BlnDari)) + " " + Convert.ToInt32(ThnDari)
                + " s/d " + Cf.Monthname(Convert.ToInt32(BlnSampai)) + " " + Convert.ToInt32(ThnSampai)
                + "<br />"
                + x
                + "<br />"
                + "<br />"

                ;
        }

        private void Fill()
        {
            //DateTime Dari = Convert.ToDateTime(dari.Text);
            string nProject = "";
            if (Project != "SEMUA") nProject = " AND a.Project IN('" + Project.Replace(",","','") + "')";
            string nPerusahaan = "";
            if (Perusahaan != "SEMUA") nPerusahaan = " AND a.Pers = '" + Perusahaan + "'";

            decimal t1 = 0;
            decimal t2 = 0;
            decimal t3 = 0;
            decimal t4 = 0;
            decimal t5 = 0;
            decimal t6 = 0;
            string lantai = "";
            string to = "";

            if (Lt != "0")
            {
                lantai = " AND LEFT(c.NoUnit,7) like '%" + Lt + "%'";
            }

            if (Tower != "0")
            {
                to = " AND c.Lokasi ='" + Tower.Replace("%"," ") + "'";
            }

            string strSql = "SELECT a.*, a.Status, b.Nama AS Cust"
                + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK a"
                + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER b ON a.NoCustomer = b.NoCustomer"
                + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT c ON a.NoUnit = c.NoUnit"
                + " WHERE 1=1 "
                + nProject
                + nPerusahaan
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
            tc.Wrap = false; tc.Attributes["style"] = "margin:1px;";
            trow.Cells.Add(tc);

            tc = new TableHeaderCell();
            tc.Text = "CUSTOMER";
            tc.RowSpan = 2;
            tc.Wrap = false;
            trow.Cells.Add(tc);

            tc = new TableHeaderCell();
            tc.Text = "STATUS";
            tc.RowSpan = 2;
            tc.Wrap = false;
            trow.Cells.Add(tc);

            tc = new TableHeaderCell();
            tc.Text = "TGL BF";
            tc.RowSpan = 2;
            tc.Wrap = false;
            trow.Cells.Add(tc);

            tc = new TableHeaderCell();
            tc.Text = "TYPE";
            tc.ColumnSpan = 4;
            tc.Wrap = false;
            trow.Cells.Add(tc);

            tc = new TableHeaderCell();
            tc.Text = "HARGA";
            tc.RowSpan = 2;
            tc.Wrap = false;
            //tc.Attributes["style"] = "padding:100px; margin:100px";
            trow.Cells.Add(tc);

            tc = new TableHeaderCell();
            tc.Text = "PPN";
            tc.RowSpan = 2;
            tc.Wrap = false;
            trow.Cells.Add(tc);

            tc = new TableHeaderCell();
            tc.Text = "TOTAL HARGA";
            tc.RowSpan = 2;
            tc.Wrap = false;
            trow.Cells.Add(tc);

            tc = new TableHeaderCell();
            tc.Text = "TYPE OF PAYMENT";
            tc.RowSpan = 2;
            tc.Wrap = false;
            trow.Cells.Add(tc);

            tc = new TableHeaderCell();
            tc.Text = "Akumulasi Sebelum " + Cf.Monthname(Convert.ToInt32(BlnDari)) + " " + Convert.ToInt32(ThnDari);
            tc.RowSpan = 2;
            tc.Wrap = false;
            trow.Cells.Add(tc);

            DateTime awal = new DateTime(Convert.ToInt32(ThnDari), Convert.ToInt32(BlnDari), 1);
            DateTime akhir = Cf.AkhirBulan(Convert.ToInt32(BlnSampai), Convert.ToInt32(ThnSampai));

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
                trow.Cells.Add(tc);
            }
            tc = new TableHeaderCell();
            tc.Text = "Total Penerimaan Hingga " + Cf.Monthname(Convert.ToInt32(BlnSampai)) + " " + Convert.ToInt32(ThnSampai) + " (Angsuran dan Saldo Awal)";
            tc.RowSpan = 2;
            tc.Wrap = false;
            trow.Cells.Add(tc);

            tc = new TableHeaderCell();
            tc.Text = "Total Penerimaan Lain-Lain Hingga " + Cf.Monthname(Convert.ToInt32(BlnSampai)) + " " + Convert.ToInt32(ThnSampai) + " (Admin dan Memo Selain Saldo Awal)";
            tc.RowSpan = 2;
            tc.Wrap = false;
            trow.Cells.Add(tc);

            tc = new TableHeaderCell();
            tc.Text = "Persentase Penerimaan";
            tc.RowSpan = 2;
            tc.Wrap = false;
            trow.Cells.Add(tc);

            tc = new TableHeaderCell();
            tc.Text = "Sisa Angsuran";
            tc.RowSpan = 2;
            tc.Wrap = false;
            trow.Cells.Add(tc);


            rpt.Rows.Add(trow);

            TableHeaderRow trow1 = new TableHeaderRow();
            TableHeaderCell tc1;

            //trow1.BackColor = Color.LightGray;
            trow1.HorizontalAlign = HorizontalAlign.Center;

            tc1 = new TableHeaderCell();
            tc1.Text = "NO UNIT";

            trow1.Cells.Add(tc1);

            tc1 = new TableHeaderCell();
            tc1.Text = "TOWER";

            trow1.Cells.Add(tc1);

            tc1 = new TableHeaderCell();
            tc1.Text = "LANTAI";

            trow1.Cells.Add(tc1);

            tc1 = new TableHeaderCell();
            tc1.Text = "LUAS";

            trow1.Cells.Add(tc1);

            foreach (var r in list)
            {
                string[] a = Cf.SplitByString(r.ToString(), "-");
                tc1 = new TableHeaderCell();
                tc1.Text = "1  s/d  7 ";
                trow1.Cells.Add(tc1);

                tc1 = new TableHeaderCell();
                tc1.Text = "8  s/d  14 ";
                trow1.Cells.Add(tc1);

                tc1 = new TableHeaderCell();
                tc1.Text = "15  s/d  21 ";
                trow1.Cells.Add(tc1);

                tc1 = new TableHeaderCell();
                tc1.Text = "22  s/d  " + Cf.AkhirBulan(Convert.ToInt32(a[0]), Convert.ToInt32(a[1])).Day;
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

                string mProject = Db.SingleString("SELECT Project FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT WHERE NoUnit = '" + rs.Rows[i]["NoUnit"] + "'");
                string ParamID = "FormatUnit" + mProject;
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


                DateTime awala = new DateTime(Convert.ToInt32(ThnDari), Convert.ToInt32(BlnDari), 1);
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
    }
}

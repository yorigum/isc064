using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Globalization;

namespace ISC064.MARKETINGJUAL.Laporan
{
    public partial class LaporanRealisasiSales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            comp.InnerHtml = Mi.Pt;
            rpt.Visible = false;
            Js.Focus(this, scr);
            init();
            rpt.Style["border-collapse"] = "collapse";
            
        }
        private void init()
        {
            //dari.Text = Cf.Day(DateTime.Today);
        }
         protected void scr_Click(object sender, System.EventArgs e)
        {
            
                Report();
        }
        protected void xls_Click(object sender, System.EventArgs e)
        {
            
                Report();
                Rpt.ToExcel(this, rpt);
            
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
          

            lblHeader.Text = "<H3>" +Mi.Pt+ "</H3>"
                + "<H1>LAPORAN REALISASI SALES & CASH IN</H1>"
                + "AS OF " + Cf.Monthname(Convert.ToInt32(bulansampai.SelectedValue)) + " " + Convert.ToInt32(tahunsampai.Text)
                + "<br />"
                + "<br />"
                + x
                ;
        }

        private void Fill()
        {
            //DateTime Dari = Convert.ToDateTime(dari.Text);

            decimal t1 = 0;
            decimal t2 = 0;
            decimal t3 = 0;
            decimal t4 = 0;
            decimal t5 = 0;
            decimal t6 = 0;

            string strSql = "SELECT a.*, a.Status, b.Nama AS Cust"
                + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK a"
                + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER b ON a.NoCustomer = b.NoCustomer"
                + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT c ON a.NoUnit = c.NoUnit"
                + " WHERE 1=1 "    
                + " ORDER BY a.TGLKONTRAK";

            DataTable rs = Db.Rs(strSql);

            TableRow trow = new TableRow();
            TableCell tc;

            trow.BackColor = Color.Gray;
            trow.HorizontalAlign = HorizontalAlign.Center;

            tc = new TableCell();
            tc.Text = "NO.";
            tc.RowSpan = 2;
            trow.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = "CUSTOMER";
            tc.RowSpan = 2;
            trow.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = "TGL BF";
            tc.RowSpan = 2;
            trow.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = "TYPE";
            tc.ColumnSpan = 4;
            trow.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = "HARGA";
            tc.RowSpan = 2;
            trow.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = "PPN";
            tc.RowSpan = 2;
            trow.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = "TOTAL HARGA";
            tc.RowSpan = 2;
            trow.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = "TYPE OF PAYMENT";
            tc.RowSpan = 2;
            trow.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = "Akumulasi Sebelum " + Cf.Monthname(Convert.ToInt32(bulandari.SelectedValue)) + " " + Convert.ToInt32(tahundari.Text);
            tc.RowSpan = 2;
            trow.Cells.Add(tc);

            DateTime awal = new DateTime(Convert.ToInt32(tahundari.Text), Convert.ToInt32(bulandari.SelectedValue), 1);
            DateTime akhir = Cf.AkhirBulan(Convert.ToInt32(bulansampai.SelectedValue),Convert.ToInt32(tahunsampai.Text));

            var listOfMonths = new List<string>();
            var list = new List<string>();

            while (awal <= akhir)
            {
                listOfMonths.Add(Cf.Monthname(awal.ToString("MMMM")) + " " + awal.ToString("yyyy"));
                list.Add(awal.ToString("MM-yyyy"));
                awal = awal.AddMonths(1);
            }

            foreach ( var r in listOfMonths)
            {
                tc = new TableCell();
                tc.Text = r.ToString();
                tc.ColumnSpan = 4;
                trow.Cells.Add(tc);
            }

            tc = new TableCell();
            tc.Text = "Total Penerimaan Hingga " + Cf.Monthname(Convert.ToInt32(bulansampai.SelectedValue)) + " " + Convert.ToInt32(tahunsampai.Text);
            tc.RowSpan = 2;
            trow.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = "";
            tc.RowSpan = 2;
            trow.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = "Persentase Pelunasan";
            tc.RowSpan = 2;
            trow.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = "Sisa Angsuran";
            tc.RowSpan = 2;
            trow.Cells.Add(tc);


            rpt.Rows.Add(trow);

            TableRow trow1 = new TableRow();
            TableCell tc1;

            trow1.BackColor = Color.Gray;
            trow1.HorizontalAlign = HorizontalAlign.Center;

            tc1 = new TableCell();
            tc1.Text = "NO UNIT";
           
            trow1.Cells.Add(tc1);

            tc1 = new TableCell();
            tc1.Text = "TOWER";
            
            trow1.Cells.Add(tc1);

            tc1 = new TableCell();
            tc1.Text = "LANTAI";
            
            trow1.Cells.Add(tc1);

            tc1 = new TableCell();
            tc1.Text = "LUAS";
            
            trow1.Cells.Add(tc1);

            foreach (var r in listOfMonths)
            {
                tc1 = new TableCell();
                tc1.Text = "1 - 7";
                trow1.Cells.Add(tc1);

                tc1 = new TableCell();
                tc1.Text = "8 - 14";
                trow1.Cells.Add(tc1);

                tc1 = new TableCell();
                tc1.Text = "15 - 21";
                trow1.Cells.Add(tc1);

                tc1 = new TableCell();
                tc1.Text = " >= 22";
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
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglKontrak"]);
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

                string[] x = Cf.SplitByString(rs.Rows[i]["NoUnit"].ToString(), "/");
                c = new TableCell();
                c.Text = x[1];
                c.Wrap = false;
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["Luas"]);
                c.Wrap = false;
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Math.Round(Convert.ToDecimal(rs.Rows[i]["NilaiDPP"])).ToString();
                c.Wrap = false;
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Math.Round(Convert.ToDecimal(rs.Rows[i]["NilaiPPN"])).ToString();
                c.Wrap = false;
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Math.Round(Convert.ToDecimal(rs.Rows[i]["NilaiKontrak"])).ToString();
                c.Wrap = false;
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Skema"].ToString();
                c.Wrap = false;
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);


                DateTime awala = new DateTime(Convert.ToInt32(tahundari.Text),Convert.ToInt32(bulandari.SelectedValue), 1 );
                c = new TableCell();
                c.Text = Math.Round(Db.SingleDecimal("SELECT ISNULL(SUM(NILAIPELUNASAN),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE TglPelunasan < '" + Cf.Tgl112(awala) + "' AND NoKontrak='" + rs.Rows[i]["NoKontrak"].ToString() + "'")).ToString();
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
                    c.Text = Math.Round(Db.SingleDecimal("SELECT ISNULL(SUM(NILAIPELUNASAN),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE TglPelunasan <= '" + Cf.Tgl112(week1b) + "' AND TglPelunasan >= '" + Cf.Tgl112(week1a) + "' AND NoKontrak='" + rs.Rows[i]["NoKontrak"].ToString() + "'")).ToString();
                    c.Wrap = false;
                    c.HorizontalAlign = HorizontalAlign.Left;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Math.Round(Db.SingleDecimal("SELECT ISNULL(SUM(NILAIPELUNASAN),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE TglPelunasan <= '" + Cf.Tgl112(week2b) + "' AND TglPelunasan >= '" + Cf.Tgl112(week2a) + "' AND NoKontrak='" + rs.Rows[i]["NoKontrak"].ToString() + "'")).ToString();
                    c.Wrap = false;
                    c.HorizontalAlign = HorizontalAlign.Left;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Math.Round(Db.SingleDecimal("SELECT ISNULL(SUM(NILAIPELUNASAN),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE TglPelunasan <= '" + Cf.Tgl112(week3b) + "' AND TglPelunasan >= '" + Cf.Tgl112(week3a) + "' AND NoKontrak='" + rs.Rows[i]["NoKontrak"].ToString() + "'")).ToString();
                    c.Wrap = false;
                    c.HorizontalAlign = HorizontalAlign.Left;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Math.Round(Db.SingleDecimal("SELECT ISNULL(SUM(NILAIPELUNASAN),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE TglPelunasan <= '" + Cf.Tgl112(week4b) + "' AND TglPelunasan >= '" + Cf.Tgl112(week4a) + "' AND NoKontrak='" + rs.Rows[i]["NoKontrak"].ToString() + "'")).ToString();
                    c.Wrap = false;
                    c.HorizontalAlign = HorizontalAlign.Left;
                    r.Cells.Add(c);



                }
                c = new TableCell();
                c.Text = Math.Round(Db.SingleDecimal("SELECT ISNULL(SUM(NILAIPELUNASAN),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE TglPelunasan < '" + Cf.Tgl112(akhir) + "' AND NoKontrak='" + rs.Rows[i]["NoKontrak"].ToString() + "'")).ToString();
                c.Wrap = false;
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);


                c = new TableCell();
                c.Text = "";
                c.Wrap = false;
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Math.Round(Convert.ToDecimal(rs.Rows[i]["PersenLunas"])).ToString() + "%";
                c.Wrap = false;
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                decimal tagihan = Db.SingleDecimal("SELECT ISNULL(SUM(NILAITAGIHAN),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN WHERE NoKontrak='" + rs.Rows[i]["NoKontrak"].ToString() + "'");
                decimal pelunasan = Db.SingleDecimal("SELECT ISNULL(SUM(NILAIPELUNASAN),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE NoKontrak='" + rs.Rows[i]["NoKontrak"].ToString() + "'");
                decimal sisa = tagihan - pelunasan;

                c = new TableCell();
                c.Text = Math.Round(sisa).ToString();
                c.Wrap = false;
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);


                rpt.Rows.Add(r);

            }
        }
    }
}

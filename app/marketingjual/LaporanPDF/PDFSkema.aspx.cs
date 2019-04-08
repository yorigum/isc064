using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Linq;

namespace ISC064.MARKETINGJUAL
{
	public partial class PDFSkema : System.Web.UI.Page
	{
        private string UserID { get { return (Request.QueryString["userid"]); } }
        private string AttachmentID { get { return (Request.QueryString["id"]); } }
        private string CaraBayar { get { return (Request.QueryString["carabayar"]); } }
        private string NoStok { get { return (Request.QueryString["nostok"]); } }
        private string Nilai { get { return (Request.QueryString["nilai"]); } }
        private string Metode { get { return (Request.QueryString["metode"]); } }
        private string Diskon { get { return (Request.QueryString["diskon"]); } }
        private string PresentValue { get { return (Request.QueryString["presenvalue"]); } }
        private string DPP { get { return (Request.QueryString["dpp"]); } }
        private string PPN { get { return (Request.QueryString["ppn"]); } }
        private string bfkali { get { return (Request.QueryString["bfkali"]); } }
        private string bflama1 { get { return (Request.QueryString["bflama1"]); } }
        private string bflama2 { get { return (Request.QueryString["bflama2"]); } }
        private string bfhari1 { get { return (Request.QueryString["bfhari1"]); } }
        private string bfhari2 { get { return (Request.QueryString["bfhari2"]); } }
        private string bfjumlah { get { return (Request.QueryString["bfjumlah"]); } }
        private string bfpersen { get { return (Request.QueryString["bfpersen"]); } }
        private string dpkali { get { return (Request.QueryString["dpkali"]); } }
        private string dplama1 { get { return (Request.QueryString["dplama1"]); } }
        private string dplama2 { get { return (Request.QueryString["dplama2"]); } }
        private string dphari1 { get { return (Request.QueryString["dphari1"]); } }
        private string dphari2 { get { return (Request.QueryString["dphari2"]); } }
        private string dpjumlah { get { return (Request.QueryString["dpjumlah"]); } }
        private string dppersen { get { return (Request.QueryString["dppersen"]); } }
        private string angkali { get { return (Request.QueryString["angkali"]); } }
        private string anglama1 { get { return (Request.QueryString["anglama1"]); } }
        private string anglama2 { get { return (Request.QueryString["anglama2"]); } }
        private string anghari1 { get { return (Request.QueryString["anghari1"]); } }
        private string anghari2 { get { return (Request.QueryString["anghari2"]); } }
        private string angjumlah { get { return (Request.QueryString["angjumlah"]); } }
        private string angpersen { get { return (Request.QueryString["angpersen"]); } }
        private string potong { get { return (Request.QueryString["potong"]); } }

        protected void Page_Load(object sender, System.EventArgs e)
		{
            if (Metode == "0")
            {
                Header();
                Jadwal();
            }
            else if (Metode == "1")
            {
                Header();
                Isi(true);
            }
        }
        private void Isi(bool potongbf)
        {
            //Func.KontrakHeader(NoKontrak, nokontrakdetail, unitdetail, namadetail, agent);


            SetBaris(bfkali, "BF", "BOOKING FEE", bflama1, bflama2, bfhari1, bfhari2, bfjumlah, bfpersen);
            SetBaris(dpkali, "DP", "DP", dplama1, dplama2, dphari1, dphari2, dpjumlah, dppersen);
            SetBaris(angkali, "ANG", "ANGSURAN", anglama1, anglama2, anghari1, anghari2, angjumlah, angpersen);


            //potong booking fee harus dikontrol pada saat POSTBACK
            if (potongbf)
                PotongBF();

            decimal Sum = 0;
            for (int i = 1; i < rpt.Rows.Count; i++)
            {
                Sum = Sum + Convert.ToDecimal(rpt.Rows[i].Cells[3].Text);
            }

            TableRow r = new TableRow();
            TableCell c;

            r = new TableRow();

            c = new TableCell();
            c.Text = "Grand Total";
            c.ColumnSpan = 2;
            c.Font.Bold = true;
            c.Font.Size = 10;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = Cf.Num(Sum);
            c.ColumnSpan = 2;
            c.HorizontalAlign = HorizontalAlign.Right;
            c.Font.Bold = true;
            c.Font.Size = 10;
            r.Cells.Add(c);

            rpt.Rows.Add(r);


        }
        private void SetBaris(string Kali, string Tipe, string Nama
            , string interval1, string interval2
            , string hari1, string hari2
            , string nominal, string persen
            )
        {
            int count = Convert.ToInt32(Kali);
            int index = rpt.Rows.Count - 1;
            DateTime Tgl = Db.SingleTime("SELECT FilterDari FROM LapPdf WHERE AttachmentID = '" + AttachmentID + "'");

            DateTime Tgl2 = Convert.ToDateTime(Tgl); //tanggal kontrak
            try
            {
                if (Tipe == "BF")
                {
                    Tgl2 = Convert.ToDateTime(Tgl); /*= Convert.ToDateTime(bftgl.Text);*/
                }
                else if (Tipe == "DP")
                {
                    Tgl2 = Convert.ToDateTime(Tgl);
                }
                else if (Tipe == "ANG")
                {
                    Tgl2 = Convert.ToDateTime(Tgl);
                }
                else
                {
                    Tgl2 = Convert.ToDateTime(rpt.Rows[rpt.Rows.Count - 1].Cells[3].Text);
                }
            }
            catch { }

            for (int i = 0; i < count; i++)
            {
                if (!Response.IsClientConnected) break;

                index++;

                TableRow r = new TableRow();
                TableCell c;

                c = new TableCell();
                c.Text = index.ToString() + ".";
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Nama + " " + (i + 1);
                r.Cells.Add(c);

                if (i == 0)
                {
                    int interval = Convert.ToInt32(interval2);
                    if (Tipe == "BF")
                    {
                        interval = 0;
                    }
                    else if (Tipe == "DP")
                    {
                        interval = 0;
                    }
                    else if (Tipe == "ANG")
                    {
                        interval = 0;
                    }
                    else
                    {
                        //pertama
                        if (hari2 != "")
                            Tgl2 = Tgl2.AddDays(Convert.ToInt32(interval));
                        else
                            Tgl2 = Tgl2.AddMonths(interval);
                    }
                }
                else
                {
                    int interval = Convert.ToInt32(interval1);
                    //pertama
                    if (hari1 != "")
                        Tgl2 = Tgl2.AddDays(interval);
                    else
                    {
                        int h = Tgl2.Day;
                        //if (Tipe == "BF" && bftgl.Text != "")
                        //{
                        //    h = Convert.ToDateTime(bftgl.Text).Day;
                        //}
                        //else if (Tipe == "DP" && dptgl.Text != "")
                        //{
                        //    h = Convert.ToDateTime(dptgl.Text).Day;
                        //}
                        //else if (Tipe == "ANG" && angtgl.Text != "")
                        //{
                        //    h = Convert.ToDateTime(angtgl.Text).Day;
                        //}
                        Tgl2 = Tgl2.AddMonths(interval);
                        if (h != Tgl2.Day && h <= DateTime.DaysInMonth(Tgl2.Year, Tgl2.Month))
                            Tgl2 = new DateTime(Tgl2.Year, Tgl2.Month, h);
                    }
                }

                c = new TableCell();
                c.Text = Cf.Day(Tgl);
                r.Cells.Add(c);

                decimal Nominal = Convert.ToDecimal(nominal);
                decimal Netto = Convert.ToDecimal(Nilai);

                if (persen == "%") Nominal = Netto * (Nominal / 100);

                //if (rounding.Checked)
                //{
                decimal rounded = 0;
                Nominal = RoundThousand(Nominal / count);
                //}
                //else
                //{
                //    Nominal = Math.Round(Nominal / count);
                //}

                c = new TableCell();
                c.Text = Cf.Num(Nominal);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "";
                if (potong == "DP1" && i == 0 && Tipe == "DP")
                    c.Text = "MIN";
                if (potong == "ANG1" && i == 0 && Tipe == "ANG")
                    c.Text = "MIN";
                if (potong == "DPS" && Tipe == "DP")
                    c.Text = "MIN";
                if (potong == "ANGS" && Tipe == "ANG")
                    c.Text = "MIN";

                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Tipe;
                c.Visible = false;
                r.Cells.Add(c);

                Rpt.Border(r);
                rpt.Rows.Add(r);
            }
        }


        private void PotongBF()
        {
            decimal totalbf = 0;
            int minbf = 0;

            for (int i = 1; i < rpt.Rows.Count; i++)
            {
                string Tipe = rpt.Rows[i].Cells[5].Text;
                string Min = rpt.Rows[i].Cells[4].Text;

                if (Tipe == "BF")
                    totalbf = totalbf + Convert.ToDecimal(rpt.Rows[i].Cells[3].Text);

                if (Min == "MIN")
                    minbf++;
            }

            decimal bfsatuan = Math.Round(totalbf / minbf);

            for (int i = 1; i < rpt.Rows.Count; i++)
            {
                string Min = rpt.Rows[i].Cells[4].Text;
                if (Min == "MIN")
                {
                    rpt.Rows[i].Cells[3].Text = Cf.Num(
                        Convert.ToDecimal(rpt.Rows[i].Cells[3].Text) - bfsatuan);
                }
            }
        }


        private void Header()
        {
            Table tb;
            TableRow tr;
            TableHeaderCell hc;
            TableCell c;

            tb = new Table();
            tb.CellSpacing = 1;

            tr = new TableRow();
            tb.Rows.Add(tr);

            c = new TableCell();
            c.Text = "Tanggal Perhitungan";
            c.Font.Size = 12;
            c.HorizontalAlign = HorizontalAlign.Left;
            tr.Cells.Add(c);

            c = new TableCell();
            c.Text = ":";
            c.Font.Size = 12;
            c.HorizontalAlign = HorizontalAlign.Left;
            tr.Cells.Add(c);

            c = new TableCell();
            DateTime tgl = Db.SingleTime("SELECT FilterDari From LapPdf WHERE AttachmentID='" + AttachmentID + "'");
            c.Text = Cf.Day(tgl);
            c.Font.Size = 12;
            c.HorizontalAlign = HorizontalAlign.Left;
            tr.Cells.Add(c);

            tr = new TableRow();
            tb.Rows.Add(tr);

            c = new TableCell();
            c.Text = "No. Unit";
            c.Font.Size = 12;
            c.HorizontalAlign = HorizontalAlign.Left;
            tr.Cells.Add(c);

            c = new TableCell();
            c.Text = ":";
            c.Font.Size = 12;
            c.HorizontalAlign = HorizontalAlign.Left;
            tr.Cells.Add(c);

            c = new TableCell();
            string unit = Db.SingleString("SELECT NoUnit FROM MS_UNIT WHERE NoStock = '" + NoStok + "'");
            c.Text = Cf.Str(unit);
            c.Font.Size = 12;
            c.HorizontalAlign = HorizontalAlign.Left;
            tr.Cells.Add(c);

            tr = new TableRow();
            tb.Rows.Add(tr);

            c = new TableCell();
            c.Text = "Cara Bayar";
            c.Font.Size = 12;
            c.HorizontalAlign = HorizontalAlign.Left;
            tr.Cells.Add(c);

            c = new TableCell();
            c.Text = ":";
            c.Font.Size = 12;
            c.HorizontalAlign = HorizontalAlign.Left;
            tr.Cells.Add(c);

            c = new TableCell();
            if(Metode == "0")
            {
                string carabayar = Db.SingleString("SELECT Nama FROM REF_SKEMA WHERE Nomor = '"+CaraBayar+"'");
                c.Text = Cf.Str(carabayar);
            }
            else
            {
                c.Text = "Customize";
            }
            c.Font.Size = 12;
            c.HorizontalAlign = HorizontalAlign.Left;
            tr.Cells.Add(c);

            tr = new TableRow();
            tb.Rows.Add(tr);

            c = new TableCell();
            c.Text = "Price List";
            c.Font.Size = 12;
            c.HorizontalAlign = HorizontalAlign.Left;
            tr.Cells.Add(c);

            c = new TableCell();
            c.Font.Size = 12;
            c.Text = ":";
            c.HorizontalAlign = HorizontalAlign.Left;
            tr.Cells.Add(c);

            c = new TableCell();
            decimal Pl = Db.SingleDecimal("SELECT PriceList FROM MS_UNIT WHERE NoStock = '" + NoStok + "'");
            c.Font.Size = 12;
            c.Text = Cf.Num(Pl);
            c.HorizontalAlign = HorizontalAlign.Left;
            tr.Cells.Add(c);

            tr = new TableRow();
            tb.Rows.Add(tr);

            c = new TableCell();
            c.Font.Size = 12;
            c.Text = "Diskon";
            c.HorizontalAlign = HorizontalAlign.Left;
            tr.Cells.Add(c);

            c = new TableCell();
            c.Font.Size = 12;
            c.Text = ":";
            c.HorizontalAlign = HorizontalAlign.Left;
            tr.Cells.Add(c);

            c = new TableCell();
            c.Font.Size = 12;
            c.Text = Cf.Num(Diskon);
            c.HorizontalAlign = HorizontalAlign.Left;
            tr.Cells.Add(c);

            tr = new TableRow();
            tb.Rows.Add(tr);

            c = new TableCell();
            c.Font.Size = 12;
            c.Text = "Present Value";
            c.HorizontalAlign = HorizontalAlign.Left;
            tr.Cells.Add(c);

            c = new TableCell();
            c.Font.Size = 12;
            c.Text = ":";
            c.HorizontalAlign = HorizontalAlign.Left;
            tr.Cells.Add(c);

            c = new TableCell();
            c.Font.Size = 12;
            c.Text = Cf.Num(PresentValue);
            c.HorizontalAlign = HorizontalAlign.Left;
            tr.Cells.Add(c);

            tr = new TableRow();
            tb.Rows.Add(tr);

            c = new TableCell();
            c.Font.Size = 12;
            c.Text = "<br>";
            c.HorizontalAlign = HorizontalAlign.Left;
            tr.Cells.Add(c);

            tr = new TableRow();
            tb.Rows.Add(tr);

            c = new TableCell();
            c.Font.Size = 12;
            c.Text = "DPP";
            c.HorizontalAlign = HorizontalAlign.Left;
            tr.Cells.Add(c);

            c = new TableCell();
            c.Font.Size = 12;
            c.Text = ":";
            c.HorizontalAlign = HorizontalAlign.Left;
            tr.Cells.Add(c);

            c = new TableCell();
            c.Font.Size = 12;
            c.Text = Cf.Num(DPP);
            c.HorizontalAlign = HorizontalAlign.Left;
            tr.Cells.Add(c);
            tr = new TableRow();
            tb.Rows.Add(tr);

            c = new TableCell();
            c.Font.Size = 12;
            c.Text = "PPN";
            c.HorizontalAlign = HorizontalAlign.Left;
            tr.Cells.Add(c);

            c = new TableCell();
            c.Text = ":";
            c.Font.Size = 12;
            c.HorizontalAlign = HorizontalAlign.Left;
            tr.Cells.Add(c);

            c = new TableCell();
            c.Font.Size = 12;
            c.Text = Cf.Num(PPN);
            c.HorizontalAlign = HorizontalAlign.Left;
            tr.Cells.Add(c);

            tr = new TableRow();
            tb.Rows.Add(tr);

            c = new TableCell();
            c.Font.Size = 12;
            c.Text = "Nilai Kontrak";
            c.HorizontalAlign = HorizontalAlign.Left;
            tr.Cells.Add(c);

            c = new TableCell();
            c.Text = ":";
            c.Font.Size = 12;
            c.HorizontalAlign = HorizontalAlign.Left;
            tr.Cells.Add(c);

            c = new TableCell();
            c.Text = Cf.Num(Nilai);
            c.Font.Size = 12;
            c.HorizontalAlign = HorizontalAlign.Left;
            tr.Cells.Add(c);

            c = new TableCell();
            c.Font.Size = 12;
            c.Text = "<br>";
            c.HorizontalAlign = HorizontalAlign.Left;
            tr.Cells.Add(c);
            list.Controls.Add(tb);
        }

        private void Jadwal()
        {            
            DateTime tgl = Db.SingleTime("SELECT FilterDari FROM LapPdf WHERE AttachmentID = '"+AttachmentID+"'");
            decimal netto = Convert.ToDecimal(Nilai);

            string[,] x = Func.Breakdown(
                Convert.ToInt32(CaraBayar), netto, tgl);

            decimal t = 0;
            //for (int i = 0; i <= x.GetUpperBound(0); i++)
            //{
            //    if (!Response.IsClientConnected) break;
            int index = 0;
            var d = Func.ListTagihan(CaraBayar, netto, tgl);
            foreach (var rb in d)
            {

                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;
                

                c = new TableCell();
                c.Text = (index + 1) + ".";
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rb.NamaTagihan;
                r.Cells.Add(c);

                //jadwal
                c = new TableCell();
                c.Text = Cf.Day(rb.TglJt);
                r.Cells.Add(c);

                //nominal
                c = new TableCell();
                c.Text = Cf.Num(rb.NilaiTagihan);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "";
                if (rb.PotongBF)
                    c.Text = "MIN";

                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rb.TipeTagihan;
                c.Visible = false;
                r.Cells.Add(c);

                t = t + Convert.ToDecimal(rb.NilaiTagihan);
                index++;
                Rpt.Border(r);
                rpt.Rows.Add(r);
                int CountJum = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM REF_SKEMA_DETAIL WHERE Nomor='" + CaraBayar + "'");
                if (index == CountJum)
                {
                    r = new TableRow();

                    c = new TableCell();
                    c.Text = "Grand Total";
                    c.ColumnSpan = 2;
                    c.Font.Bold = true;
                    c.Font.Size = 10;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Cf.Num(d.Sum(p => p.NilaiTagihan));
                    c.ColumnSpan = 2;
                    c.HorizontalAlign = HorizontalAlign.Right;
                    c.Font.Bold = true;
                    c.Font.Size = 10;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = ""; 
                    r.Cells.Add(c);
                    rpt.Rows.Add(r);
                }
            }
        }
        private static decimal RoundUp(decimal input)
        {
            string x = input.ToString();
            string[] arr = x.Split(new char[] { '.' });

            if (arr.Length > 1)
            {
                if (decimal.Parse(arr[1]) > 0)
                {
                    decimal dc = decimal.Parse(arr[0]) + 1;
                    return dc;
                }
                else
                {
                    return decimal.Parse(arr[0]);
                }
            }
            else
            {
                return input;
            }
        }

        private static decimal RoundThousand(decimal input)
        {
            if (input < 1000)
            {
                return 0;
            }
            else
            {
                input = RoundUp(input);
                if ((input % 1000) > 0)
                {
                    input = (input - (input % 1000)) + 0;
                }
                return input;
            }
        }
    }
}

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;

namespace ISC064.MARKETINGJUAL.Laporan
{
    /// <summary>
    /// Summary description for LaporanSalesPerformance.
    /// </summary>
    public partial class LaporanSalesPerformance : System.Web.UI.Page
    {
        private string nAgent { get { return (Request.QueryString["agent"]); } }
        private string AttachmentID { get { return (Request.QueryString["id"]); } }
        private string FilterTgl { get { return (Request.QueryString["filtertgl"]); } }
        private string UserID { get { return (Request.QueryString["userid"]); } }
        private string Project { get { return (Request.QueryString["project"]); } }
        private string StatusSales { get { return (Request.QueryString["statussales"]); } }
        private string TglAsOf { get { return (Request.QueryString["tglasof"]); } }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Report();
        }

        protected int UserAgent()
        {
            int a = Db.SingleInteger("SELECT NoAgent FROM " + Mi.DbPrefix + "SECURITY..USERNAME WHERE UserID = '" + UserID + "'");

            return a;
        }

        private void Report()
        {
            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");

            param.Visible = false;
            rpt.Visible = true;

            lblHeader.Text = "<p>" + Mi.Pt + "</p>"
                + "<h1 class='title'>Laporan Sales Performance</h1>"
                ;

            System.Text.StringBuilder x = new System.Text.StringBuilder();

            x.Append("Sales : " + nAgent);
            string nstatussales = String.Empty;
            if (StatusSales == "I")
                nstatussales = "Inaktif";
            else if (StatusSales == "A")
                nstatussales = "Aktif";
            else
                nstatussales = "Semua";

            x.Append("<br />Status Agent : " + nstatussales);
            if (FilterTgl == "0")
            {
                x.Append("<br />Tgl Kontrak : " + Cf.Day(Dari) + " s/d " + Cf.Day(Sampai));
            }
            else
            {
                x.Append("<br />Tgl Kontrak : As Of : " + Cf.Day(TglAsOf));
            }
            x.Append("<br />Project : " + Project);

            x.Append("<br /><br /><span style='font-weight: normal;'>Laporan dihasilkan pada hari : " + Cf.IndoWeek(DateTime.Today)
                + ", " + Cf.Date(DateTime.Now)
                + " dari workstation : " + Act.IP
                + " dan username : " + Act.UserID
                + "</span>"
                );

            lblSubHeader.Text = x.ToString();
            Fill();
        }
        private void newHeader()
        {
            string header = "<h2>" + Mi.Pt + "</h2>";
            header += "<h1 class='title'>LAPORAN PEMBATALAN</h1>";
            //header += "Periode : " + Cf.Day(dari.Text) + " s/d " + Cf.Day(sampai.Text);
            header += "<br/> Laporan dihasilkan pada hari " + Cf.IndoWeek(DateTime.Today);
            header += ", " + Cf.Date(DateTime.Now) + " dari workstation " + Act.IP + " oleh user " + Act.UserID + "<br /><br />";
            //headJudul.Text = header;
        }
        private void Fill()
        {

            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");

            DateTime AsOf = Convert.ToDateTime(TglAsOf);

            decimal TotalLalu = 0;
            decimal totalBatal = 0;
            decimal TotalJan = 0, TotalFeb = 0, TotalMar = 0, TotalApr = 0, TotalMay = 0, TotalJun = 0, TotalJul = 0, TotalAug = 0, TotalSep = 0, TotalOct = 0, TotalNov = 0, TotalDec = 0;
            decimal TargetJan = 0, TargetFeb = 0, TargetMar = 0, TargetApr = 0, TargetMay = 0, TargetJun = 0, TargetJul = 0, TargetAug = 0, TargetSep = 0, TargetOct = 0, TargetNov = 0, TargetDec = 0;
            decimal Total = 0;
            decimal GT = 0;

            decimal TotalLalua = 0;
            decimal totalBatala = 0;
            decimal TotalJana = 0, TotalFeba = 0, TotalMara = 0, TotalApra = 0, TotalMaya = 0, TotalJuna = 0, TotalJula = 0, TotalAuga = 0, TotalSepa = 0, TotalOcta = 0, TotalNova = 0, TotalDeca = 0;
            decimal TargetJana = 0, TargetFeba = 0, TargetMara = 0, TargetApra = 0, TargetMaya = 0, TargetJuna = 0, TargetJula = 0, TargetAuga = 0, TargetSepa = 0, TargetOcta = 0, TargetNova = 0, TargetDeca = 0;
            decimal Totala = 0;
            decimal GTa = 0;

            string Agent = "";
            if (nAgent != "SEMUA")
                Agent += " AND NoAgent = " + nAgent;
            else
            {
                if (UserAgent() > 0)
                    Agent += " AND NoAgent = " + UserAgent();
            }

           
            
            if (StatusSales == "A")
            {
                if (Agent == "")
                {
                    Agent += " AND Status='A'";
                }
                else
                {
                    Agent += " AND Status='A'";
                }
            }
            else if (StatusSales == "I")
            {
                if (Agent == "")
                {
                    Agent += " AND Status='I'";
                }
                else
                {
                    Agent += " AND Status='I'";
                }
            }


            //Response.Write(nAgent);
            string nProject = "";
            if (Project != "")  nProject = " WHERE Project IN ('" + Project.Replace(",", "','") + "')";
            string strSql = "SELECT * FROM MS_AGENT "
                + nProject
                + Agent;

            DataTable rs = Db.Rs(strSql);

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                r.VerticalAlign = VerticalAlign.Top;

                c = new TableCell();
                c.Text = Cf.Str(rs.Rows[i]["NoAgent"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Str(rs.Rows[i]["Nama"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Str(rs.Rows[i]["Principal"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + rs.Rows[i]["Project"].ToString() + "'");
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                decimal Lalu = 0;
                decimal Lalua = 0;
                //if(kuantitas.Checked)
                if (FilterTgl == "0")
                {
                    Lalu = Db.SingleDecimal("SELECT ISNULL(COUNT(NoKontrak),0) FROM MS_KONTRAK WHERE YEAR(TglKontrak) > '" + Dari.Year + "' AND YEAR(TglKontrak) < '" + Sampai.Year + "' AND NoAgent = '" + rs.Rows[i]["NoAgent"] + "'");
                    Lalua = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiDPP),0) FROM MS_KONTRAK WHERE YEAR(TglKontrak) > '" + Dari.Year + "' AND YEAR(TglKontrak) < '" + Sampai.Year + "' AND NoAgent = '" + rs.Rows[i]["NoAgent"] + "'");
                }
                else
                {
                    Lalu = Db.SingleDecimal("SELECT ISNULL(COUNT(NoKontrak),0) FROM MS_KONTRAK WHERE YEAR(TglKontrak) < '" + AsOf.Year + "' AND NoAgent = '" + rs.Rows[i]["NoAgent"] + "'");
                    Lalua = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiDPP),0) FROM MS_KONTRAK WHERE YEAR(TglKontrak) < '" + AsOf.Year + "' AND NoAgent = '" + rs.Rows[i]["NoAgent"] + "'");
                }
                //if(rupiah.Checked)

                decimal b1 = 0, b2 = 0, b3 = 0, b4 = 0, b5 = 0, b6 = 0, b7 = 0, b8 = 0, b9 = 0, b10 = 0, b11 = 0, b12 = 0;
                decimal t1 = 0, t2 = 0, t3 = 0, t4 = 0, t5 = 0, t6 = 0, t7 = 0, t8 = 0, t9 = 0, t10 = 0, t11 = 0, t12 = 0;
                decimal b1a = 0, b2a = 0, b3a = 0, b4a = 0, b5a = 0, b6a = 0, b7a = 0, b8a = 0, b9a = 0, b10a = 0, b11a = 0, b12a = 0;
                decimal t1a = 0, t2a = 0, t3a = 0, t4a = 0, t5a = 0, t6a = 0, t7a = 0, t8a = 0, t9a = 0, t10a = 0, t11a = 0, t12a = 0;
                decimal Batas = 0;
                if (FilterTgl == "0")
                {
                    Batas = (decimal)Sampai.Month;
                }
                else
                {
                    Batas = (decimal)AsOf.Month;
                }

                for (int j = 1; j <= Batas; j++)
                {
                    decimal Bulan = 0;
                    decimal Bulana = 0;

                    if (FilterTgl == "0")
                    {
                        Bulan = Db.SingleDecimal("SELECT ISNULL(COUNT(NoKontrak),0) FROM MS_KONTRAK WHERE MONTH(TglKontrak) = '" + j + "' AND YEAR(TglKontrak) = '" + Dari.Year + "' AND TglKontrak <= '" + Sampai + "' AND TglKontrak >= '" + Dari + "' AND NoAgent = '" + rs.Rows[i]["NoAgent"] + "'");
                        Bulana = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiDPP),0) FROM MS_KONTRAK WHERE MONTH(TglKontrak) = '" + j + "' AND YEAR(TglKontrak) = '" + Dari.Year + "' AND TglKontrak <= '" + Sampai + "' AND TglKontrak >= '" + Dari + "' AND NoAgent = '" + rs.Rows[i]["NoAgent"] + "'");
                    }
                    else
                    {
                        Bulan = Db.SingleDecimal("SELECT ISNULL(COUNT(NoKontrak),0) FROM MS_KONTRAK WHERE MONTH(TglKontrak) = '" + j + "' AND TglKontrak <= '" + AsOf + "' AND NoAgent = '" + rs.Rows[i]["NoAgent"] + "'");
                        Bulana = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiDPP),0) FROM MS_KONTRAK WHERE MONTH(TglKontrak) = '" + j + "' AND TglKontrak <= '" + AsOf + "' AND NoAgent = '" + rs.Rows[i]["NoAgent"] + "'");
                    }

                    //target
                    c = new TableCell();
                    decimal tgt = 0;
                    tgt = Db.SingleDecimal("SELECT ISNULL(SUM(Target),0) FROM MS_AGENT_TARGET WHERE NoAgent = '" + rs.Rows[i]["NoAgent"] + "' AND Bulan = '" + j + "' AND Tahun = '" + Dari.Year + "'");
                    c.Text = Cf.Num(tgt);
                    c.HorizontalAlign = HorizontalAlign.Right;
                    c.Wrap = false;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Cf.Num(Bulan);
                    c.HorizontalAlign = HorizontalAlign.Right;
                    c.Wrap = false;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Cf.Num(Bulana);
                    c.HorizontalAlign = HorizontalAlign.Right;
                    c.Wrap = false;
                    r.Cells.Add(c);

                    decimal persentase = 0;
                    if (Bulana != 0 && tgt != 0)
                    {
                        persentase = Bulana / tgt * 100;
                    }
                    c = new TableCell();
                    c.Text = Cf.Num(persentase) + "%";
                    c.HorizontalAlign = HorizontalAlign.Right;
                    c.Wrap = false;
                    r.Cells.Add(c);

                    if (j == 1)
                    {
                        b1 = b1 + (decimal)Bulan;
                        t1 = (decimal)tgt;
                    }
                    else if (j == 2)
                    {
                        b2 = b2 + (decimal)Bulan;
                        t2 += (decimal)tgt;
                    }
                    else if (j == 3)
                    {
                        b3 = b3 + (decimal)Bulan;
                        t3 += (decimal)tgt;
                    }
                    else if (j == 4)
                    {
                        b4 = b4 + (decimal)Bulan;
                        t4 += (decimal)tgt;
                    }
                    else if (j == 5)
                    {
                        b5 = b5 + (decimal)Bulan;
                        t5 += (decimal)tgt;
                    }
                    else if (j == 6)
                    {
                        b6 = b6 + (decimal)Bulan;
                        t6 += (decimal)tgt;
                    }
                    else if (j == 7)
                    {
                        b7 = b7 + (decimal)Bulan;
                        t7 += (decimal)tgt;
                    }
                    else if (j == 8)
                    {
                        b8 = b8 + (decimal)Bulan;
                        t8 += (decimal)tgt;
                    }
                    else if (j == 9)
                    {
                        b9 = b9 + (decimal)Bulan;
                        t9 += (decimal)tgt;
                    }
                    else if (j == 10)
                    {
                        b10 = b10 + (decimal)Bulan;
                        t10 += (decimal)tgt;
                    }
                    else if (j == 11)
                    {
                        b11 = b11 + (decimal)Bulan;
                        t11 += (decimal)tgt;
                    }
                    else if (j == 12)
                    {
                        b12 = b12 + (decimal)Bulan;
                        t12 += (decimal)tgt;
                    }




                    if (j == 1)
                    {
                        b1a = b1a + (decimal)Bulana;
                        t1a = (decimal)tgt;
                    }
                    else if (j == 2)
                    {
                        b2a = b2a + (decimal)Bulana;
                        t2a += (decimal)tgt;
                    }
                    else if (j == 3)
                    {
                        b3a = b3a + (decimal)Bulana;
                        t3a += (decimal)tgt;
                    }
                    else if (j == 4)
                    {
                        b4a = b4a + (decimal)Bulana;
                        t4a += (decimal)tgt;
                    }
                    else if (j == 5)
                    {
                        b5a = b5a + (decimal)Bulana;
                        t5a += (decimal)tgt;
                    }
                    else if (j == 6)
                    {
                        b6a = b6a + (decimal)Bulana;
                        t6a += (decimal)tgt;
                    }
                    else if (j == 7)
                    {
                        b7a = b7a + (decimal)Bulana;
                        t7a += (decimal)tgt;
                    }
                    else if (j == 8)
                    {
                        b8a = b8a + (decimal)Bulana;
                        t8a += (decimal)tgt;
                    }
                    else if (j == 9)
                    {
                        b9a = b9a + (decimal)Bulana;
                        t9a += (decimal)tgt;
                    }
                    else if (j == 10)
                    {
                        b10a = b10a + (decimal)Bulana;
                        t10a += (decimal)tgt;
                    }
                    else if (j == 11)
                    {
                        b11a = b11a + (decimal)Bulana;
                        t11a += (decimal)tgt;
                    }
                    else if (j == 12)
                    {
                        b12a = b12a + (decimal)Bulana;
                        t12a += (decimal)tgt;
                    }
                }

                decimal Sisa = 48 - (Batas * 4);
                if (Sisa > 0)
                {
                    for (int k = 1; k <= Sisa; k++)
                    {
                        c = new TableCell();
                        c.Text = Cf.Num(0);
                        c.HorizontalAlign = HorizontalAlign.Right;
                        r.Cells.Add(c);
                    }

                }

                decimal TotalBulan = b1 + b2 + b3 + b4 + b5 + b6 + b7 + b8 + b9 + b10 + b11 + b12;
                decimal TotalBulana = b1a + b2a + b3a + b4a + b5a + b6a + b7a + b8a + b9a + b10a + b11a + b12a;

                c = new TableCell();
                c.Text = Cf.Num(TotalBulan);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(TotalBulana);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                //batal per tahun ini
                decimal btl = 0;
                decimal btla = 0;

                if (FilterTgl == "0")
                {
                    btl = Db.SingleDecimal("SELECT ISNULL(COUNT(*),0) FROM MS_KONTRAK WHERE Status = 'B' AND YEAR(TglKontrak) like '" + Dari.Year + "' AND TglKontrak <= '" + Sampai + "' AND TglKontrak >= '" + Dari + "' AND NoAgent = '" + rs.Rows[i]["NoAgent"] + "'");
                    btla = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiDPP),0) FROM MS_KONTRAK WHERE Status = 'B' AND YEAR(TglKontrak) like '" + Dari.Year + "' AND TglKontrak <= '" + Sampai + "' AND TglKontrak >= '" + Dari + "' AND NoAgent = '" + rs.Rows[i]["NoAgent"] + "'");
                }
                else
                {
                    btl = Db.SingleDecimal("SELECT ISNULL(COUNT(*),0) FROM MS_KONTRAK WHERE Status = 'B' AND  TglKontrak <= '" + AsOf + "' AND NoAgent = '" + rs.Rows[i]["NoAgent"] + "'");
                    btla = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiDPP),0) FROM MS_KONTRAK WHERE Status = 'B' AND TglKontrak <= '" + AsOf + "' AND NoAgent = '" + rs.Rows[i]["NoAgent"] + "'");
                }

                c = new TableCell();
                c.Text = Cf.Num(btl);//Cf.Num(Lalu);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(btla);//Cf.Num(Lalu);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                decimal GrandTotal = Lalu + TotalBulan;
                decimal g1 = TotalBulan - btl;
                c = new TableCell();
                c.Text = Cf.Num(g1);//Cf.Num(GrandTotal);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                decimal GrandTotala = Lalua + TotalBulana;
                decimal g1a = TotalBulana - btla;
                c = new TableCell();
                c.Text = Cf.Num(g1a);//Cf.Num(GrandTotal);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                rpt.Rows.Add(r);

                TotalLalu = TotalLalu + Lalu;
                TotalJan = TotalJan + b1;
                TotalFeb = TotalFeb + b2;
                TotalMar = TotalMar + b3;
                TotalApr = TotalApr + b4;
                TotalMay = TotalMay + b5;
                TotalJun = TotalJun + b6;
                TotalJul = TotalJul + b7;
                TotalAug = TotalAug + b8;
                TotalSep = TotalSep + b9;
                TotalOct = TotalOct + b10;
                TotalNov = TotalNov + b11;
                TotalDec = TotalDec + b12;
                Total = Total + TotalBulan;
                totalBatal = totalBatal + btl;
                GT = GT + GrandTotal;

                TargetJan += t1;
                TargetFeb += t2;
                TargetMar += t3;
                TargetApr += t4;
                TargetMay += t5;
                TargetJun += t6;
                TargetJul += t7;
                TargetAug += t8;
                TargetSep += t9;
                TargetOct += t10;
                TargetNov += t11;
                TargetDec += t12;


                TotalLalua = TotalLalua + Lalua;
                TotalJana = TotalJana + b1a;
                TotalFeba = TotalFeba + b2a;
                TotalMara = TotalMara + b3a;
                TotalApra = TotalApra + b4a;
                TotalMaya = TotalMaya + b5a;
                TotalJuna = TotalJuna + b6a;
                TotalJula = TotalJula + b7a;
                TotalAuga = TotalAuga + b8a;
                TotalSepa = TotalSepa + b9a;
                TotalOcta = TotalOcta + b10a;
                TotalNova = TotalNova + b11a;
                TotalDeca = TotalDeca + b12a;
                Totala = Totala + TotalBulana;
                totalBatala = totalBatala + btla;
                GTa = GTa + GrandTotala;

                TargetJana += t1a;
                TargetFeba += t2a;
                TargetMara += t3a;
                TargetApra += t4a;
                TargetMaya += t5a;
                TargetJuna += t6a;
                TargetJula += t7a;
                TargetAuga += t8a;
                TargetSepa += t9a;
                TargetOcta += t10a;
                TargetNova += t11a;
                TargetDeca += t12a;

                if (i == rs.Rows.Count - 1)
                    SubTotal("JUMLAH", TotalLalu, TotalJan, TotalFeb, TotalMar, TotalApr, TotalMay, TotalJun, TotalJul, TotalAug, TotalSep, TotalOct, TotalNov, TotalDec,
                        TargetJan, TargetFeb, TargetMar, TargetApr, TargetMay, TargetJun, TargetJul, TargetAug, TargetSep, TargetOct, TargetNov, TargetDec,
                        Total, GT, totalBatal, TotalLalua, TotalJana, TotalFeba, TotalMara, TotalApra, TotalMaya, TotalJuna, TotalJula, TotalAuga, TotalSepa, TotalOcta, TotalNova, TotalDeca,
                        TargetJana, TargetFeba, TargetMara, TargetApra, TargetMaya, TargetJuna, TargetJula, TargetAuga, TargetSepa, TargetOcta, TargetNova, TargetDeca,
                        Totala, GTa, totalBatala);
            }

            //Chart1.Series.Clear();

            //Chart1.Width = 750;
            //Chart1.Height = 300;

            // Data arrays
            string[] seriesArray = { "Januari","Februari","Maret","April","Mei","Juni","Juli","Agustus","September","Oktober","November","Desember" };
            decimal[] pointsArray = { TotalJana,TotalFeba,TotalMara,TotalApra,TotalMaya,TotalJuna,TotalJula,TotalAuga,TotalSepa,TotalOcta,TotalNova,TotalDeca};
            // Set palette
            //Chart1.Palette = ChartColorPalette.BrightPastel;

            // Set title
            //Chart1.Titles.Add("Realisasi dalam Rupiah");
            

            // Add series.
            for (int i = 0; i < seriesArray.Length; i++)
            {
                //Series series = Chart1.Series.Add(seriesArray[i]);
                //series.Label = seriesArray[i];
                //series.XValueType = ChartValueType.String;
                //series["PixelPointWidth"] = "500";
                //series.YAxisType = AxisType.Primary;
                //series.AxisLabel = "Bulan";
                //series.Points.Add(Convert.ToDouble(pointsArray[i]));
            }
        }

        private void SubTotal(string txt, decimal TotalLalu, decimal TotalJan, decimal TotalFeb, decimal TotalMar, decimal TotalApr, decimal TotalMay, decimal TotalJun, decimal TotalJul, decimal TotalAug, decimal TotalSep, decimal TotalOct, decimal TotalNov, decimal TotalDec,
            decimal TargetJan, decimal TargetFeb, decimal TargetMar, decimal TargetApr, decimal TargetMay, decimal TargetJun, decimal TargetJul, decimal TargetAug, decimal TargetSep, decimal TargetOct, decimal TargetNov, decimal TargetDec,
            decimal Total, decimal GT, decimal totalBatal, decimal TotalLalua, decimal TotalJana, decimal TotalFeba, decimal TotalMara, decimal TotalApra, decimal TotalMaya, decimal TotalJuna, decimal TotalJula, decimal TotalAuga, decimal TotalSepa, decimal TotalOcta, decimal TotalNova, decimal TotalDeca,
            decimal TargetJana, decimal TargetFeba, decimal TargetMara, decimal TargetApra, decimal TargetMaya, decimal TargetJuna, decimal TargetJula, decimal TargetAuga, decimal TargetSepa, decimal TargetOcta, decimal TargetNova, decimal TargetDeca,
            decimal Totala, decimal GTa, decimal totalBatala)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = Rpt.Foot();
            c.Text = txt;
            c.ColumnSpan = 4;
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(TargetJan);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(TotalJan);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(TotalJana);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            if (TotalJana != 0 && TargetJan != 0)
            {
                c.Text = Cf.Num(TotalJana / TargetJan * 100) + "%";
            }
            else
            {
                c.Text = Cf.Num(0) + "%";
            }
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(TargetFeb);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(TotalFeb);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(TotalFeba);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            if (TotalFeba != 0 && TargetFeb != 0)
            {
                c.Text = Cf.Num(TotalFeba / TargetFeb * 100) + "%";
            }
            else
            {
                c.Text = Cf.Num(0) + "%";
            }
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(TargetMar);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);


            c = Rpt.Foot();
            c.Text = Cf.Num(TotalMar);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(TotalMara);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);


            c = Rpt.Foot();
            if (TotalMara != 0 && TargetMar != 0)
            {
                c.Text = Cf.Num(TotalMara / TargetMar * 100) + "%";
            }
            else
            {
                c.Text = Cf.Num(0) + "%";
            }
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(TargetApr);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);


            c = Rpt.Foot();
            c.Text = Cf.Num(TotalApr);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(TotalApra);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            if (TotalApra != 0 && TargetApr != 0)
            {
                c.Text = Cf.Num(TotalApra / TargetApr * 100) + "%";
            }
            else
            {
                c.Text = Cf.Num(0) + "%";
            }
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(TargetMay);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);


            c = Rpt.Foot();
            c.Text = Cf.Num(TotalMay);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(TotalMaya);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            if (TotalMaya != 0 && TargetMay != 0)
            {
                c.Text = Cf.Num(TotalMaya / TargetMay * 100) + "%";
            }
            else
            {
                c.Text = Cf.Num(0) + "%";
            }
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(TargetJun);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(TotalJun);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(TotalJuna);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            if (TotalJuna != 0 && TargetJun != 0)
            {
                c.Text = Cf.Num(TotalJuna / TargetJun * 100) + "%";
            }
            else
            {
                c.Text = Cf.Num(0) + "%";
            }
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(TargetJul);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(TotalJul);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(TotalJula);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            if (TotalJula != 0 && TargetJul != 0)
            {
                c.Text = Cf.Num(TotalJula / TargetJul * 100) + "%";
            }
            else
            {
                c.Text = Cf.Num(0) + "%";
            }
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(TargetAug);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(TotalAug);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(TotalAuga);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            if (TotalAuga != 0 && TargetAug != 0)
            {
                c.Text = Cf.Num(TotalAuga / TargetAug * 100) + "%";
            }
            else
            {
                c.Text = Cf.Num(0) + "%";
            }
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(TargetSep);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(TotalSep);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(TotalSepa);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            if (TotalSepa != 0 && TargetSep != 0)
            {
                c.Text = Cf.Num(TotalSepa / TargetSep * 100) + "%";
            }
            else
            {
                c.Text = Cf.Num(0) + "%";
            }
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(TargetOct);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(TotalOct);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(TotalOcta);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            if (TotalOcta != 0 && TargetOct != 0)
            {
                c.Text = Cf.Num(TotalOcta / TargetOct * 100) + "%";
            }
            else
            {
                c.Text = Cf.Num(0) + "%";
            }
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(TargetNov);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(TotalNov);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(TotalNova);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            if (TotalNova != 0 && TargetNov != 0)
            {
                c.Text = Cf.Num(TotalNova / TargetNov * 100) + "%";
            }
            else
            {
                c.Text = Cf.Num(0) + "%";
            }
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(TargetDec);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(TotalDec);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(TotalDeca);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            if (TotalDeca != 0 && TargetDec != 0)
            {
                c.Text = Cf.Num(TotalDeca / TargetDec * 100) + "%";
            }
            else
            {
                c.Text = Cf.Num(0) + "%";
            }
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(Total);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(Totala);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);


            c = Rpt.Foot();
            c.Text = Cf.Num(totalBatal);//Cf.Num(TotalLalu);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);


            c = Rpt.Foot();
            c.Text = Cf.Num(totalBatala);//Cf.Num(TotalLalu);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            decimal g = Total - totalBatal;
            c = Rpt.Foot();
            c.Text = Cf.Num(g);//Cf.Num(GT);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            decimal ga = Totala - totalBatala;
            c = Rpt.Foot();
            c.Text = Cf.Num(ga);//Cf.Num(GT);
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
     
}
}

using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL.Laporan
{
    public partial class Laporan_LaporanAkad : System.Web.UI.Page
    {
        private string UserID { get { return (Request.QueryString["userid"]); } }
        private string Lokasi { get { return (Request.QueryString["lokasi"]); } }
        private string Rekening { get { return (Request.QueryString["rek"]); } }
        private string Project { get { return (Request.QueryString["project"]); } }
        private string Perusahaan { get { return (Request.QueryString["pers"]); } }
        private string AttachmentID { get { return (Request.QueryString["id"]); } }
        protected void Page_Load(object sender, EventArgs e)
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
           

            Header();
        }
        private void Header()
        {
            System.Text.StringBuilder x = new System.Text.StringBuilder();

           // Rpt.Judul(x, comp, judul);

            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");

            Rpt.SubJudul(x
                , "Tanggal Akad" + " : " + Cf.Day(Dari) + " s/d " + Cf.Day(Sampai)
                );


            if (Rekening != "SEMUA")
            {
                Rpt.SubJudul(x, "Rekening Bank: SEMUA");
                if (Lokasi != "SEMUA")
                {
                    Rpt.SubJudul(x, "Lokasi / Tower: SEMUA");
                    //Rpt.Header(rpt, x);
                    Rpt.HeaderReport(headReport, "", x);
                    Fill();
                }
                else
                {
                    Rpt.SubJudul(x, "Lokasi / Tower: " + Lokasi);
                    //Rpt.Header(rpt, x);
                    Rpt.HeaderReport(headReport, "", x);
                    tower.Visible = false;
                    Fill2();
                }

            }
            else
            {
                Rpt.SubJudul(x, "Rekening Bank: " + Rekening);
                if (Lokasi != "SEMUA")
                {
                    Rpt.SubJudul(x, "Lokasi / Tower: SEMUA");
                    //Rpt.Header(rpt, x);
                    Rpt.HeaderReport(headReport, "", x);
                    Fill();
                }
                else
                {
                    Rpt.SubJudul(x, "Lokasi / Tower: " + Lokasi);
                    //Rpt.Header(rpt, x);
                    Rpt.HeaderReport(headReport, "", x);
                    tower.Visible = false;
                    Fill2();
                }
            }



        }
        private void Fill()
        {
            string nLokasi = "";
            if (Lokasi != "SEMUA")
                nLokasi += " AND c.Lokasi = '" + Lokasi + "'";

            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");

            string Tanggal = "";
            Tanggal = " AND CONVERT(varchar," + "TglAkad" + ",112) >= '" + Cf.Tgl112(Dari) + "'"
                    + " AND CONVERT(varchar," + "TglAkad" + ",112) <= '" + Cf.Tgl112(Sampai) + "'";

            string BankKPR = "";
            if (Rekening != "SEMUA")
                BankKPR = " AND BankKPR = '" + Rekening + "'";

            string nProject = "";
            if (Project != "SEMUA") nProject = " AND a.Project IN ('" + Project.Replace(",", "','") + "')";

            string nPerusahaan = "";
            if (Perusahaan != "SEMUA") nPerusahaan = " AND a.Pers = '" + Perusahaan + "'";

            string aa = "";
            if (UserAgent() > 0)
                aa = " AND a.NoAgent = " + UserAgent();

            string strSql = "SELECT a.*, b.*, C.Lokasi AS L, c.LuasSG"
                + " FROM MS_KONTRAK a"
                + " INNER JOIN MS_CUSTOMER b ON a.NoCustomer = b.NoCustomer"
                + " INNER JOIN MS_UNIT c ON a.NoStock = c.NoStock"
                + " WHERE a.Status = 'A'"
                + " AND StatusAkad = 'SELESAI'"
                + nProject
                + nPerusahaan
                + nLokasi
                + Tanggal
                + BankKPR
                + aa
                + "ORDER BY TglAkad"
                ;
            DataTable rs = Db.Rs(strSql);

            decimal t1 = 0, t2 = 0, t3 = 0, t4 = 0, t5 = 0, t6 = 0;
            decimal PotensiKPR = 0;
            decimal RealisasiAkad = 0;

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected)
                    break;

                TableRow r = new TableRow();
                TableCell c;

                c = new TableCell();
                c.Text = (i + 1).ToString();
                c.HorizontalAlign = HorizontalAlign.Center;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoAkad"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglAkad"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoKontrak"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Nama"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NPWP"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);


                c = new TableCell();
                c.Text = rs.Rows[i]["KTP1"].ToString()
                    + "</br>" + rs.Rows[i]["KTP2"].ToString()
                    + "</br>" + rs.Rows[i]["KTP3"].ToString()
                    + "</br>" + rs.Rows[i]["KTP4"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Db.SingleString("SELECT Nama FROM MS_AGENT WHERE NoAgent = '" + rs.Rows[i]["NoAgent"].ToString() + "' ");
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoUnit"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Convert.ToDecimal(rs.Rows[i]["LuasSG"]));
                c.HorizontalAlign = HorizontalAlign.Right;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["L"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["BankKPR"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["Gross"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Math.Round(Convert.ToDecimal(rs.Rows[i]["DiskonRupiah"]), 0));
                c.HorizontalAlign = HorizontalAlign.Right;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["NilaiKontrak"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                PotensiKPR = Convert.ToDecimal(rs.Rows[i]["NilaiPengajuan"]);
                c.Text = Cf.Num(PotensiKPR);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                RealisasiAkad = Convert.ToDecimal(rs.Rows[i]["ApprovalKPR"]);
                c.Text = Cf.Num(RealisasiAkad);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                rpt.Rows.Add(r);

                t1 += Convert.ToDecimal(rs.Rows[i]["Gross"]);
                t2 += Convert.ToDecimal(rs.Rows[i]["DiskonRupiah"]);
                t3 += Convert.ToDecimal(rs.Rows[i]["NilaiKontrak"]);
                t4 += PotensiKPR;
                t5 += RealisasiAkad;
                t6 += Convert.ToDecimal(rs.Rows[i]["LuasSG"]);
            }
            SubTotal(t1, t2, t3, t4, t5, t6);
        }

        private void Fill2()
        {
            string nLokasi = "";
            if (Lokasi != "SEMUA")
                nLokasi += " AND c.Lokasi = '" + Lokasi + "'";

            string tgl = "TglAkad";

            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");


            string Tanggal = "";
            Tanggal = " AND CONVERT(varchar," + tgl + ",112) >= '" + Cf.Tgl112(Dari) + "'"
                    + " AND CONVERT(varchar," + tgl + ",112) <= '" + Cf.Tgl112(Sampai) + "'";


            string BankKPR = "";
            if (Rekening != "SEMUA")
                BankKPR = " AND BankKPR = '" + Rekening + "'";

            string nProject = "";
            if (Project != "SEMUA") nProject = " AND a.Project IN ('" + Project.Replace(",", "','") + "')";

            string nPerusahaan = "";
            if (Perusahaan != "SEMUA") nPerusahaan = " AND a.Pers = '" + Perusahaan + "'";

            string aa = "";
            if (UserAgent() > 0)
                aa = " AND a.NoAgent = " + UserAgent();

            string strSql = "SELECT a.*, b.*, C.Lokasi AS L"
                + " FROM MS_KONTRAK a"
                + " INNER JOIN MS_CUSTOMER b ON a.NoCustomer = b.NoCustomer"
                + " INNER JOIN MS_UNIT c ON a.NoStock = c.NoStock"
                + " WHERE a.Status = 'A'"
                + " AND StatusAkad = 'SELESAI'"
                + nProject
                + nPerusahaan
                + nLokasi
                + Tanggal
                + BankKPR
                + aa
                + "ORDER BY TglAkad"
                ;
            DataTable rs = Db.Rs(strSql);

            decimal t1 = 0, t2 = 0, t3 = 0, t4 = 0, t5 = 0, t6 = 0;
            decimal PotensiKPR = 0;
            decimal RealisasiAkad = 0;

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected)
                    break;

                TableRow r = new TableRow();
                TableCell c;

                c = new TableCell();
                c.Text = (i + 1).ToString();
                c.HorizontalAlign = HorizontalAlign.Center;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoAkad"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglAkad"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoKontrak"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Nama"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NPWP"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);


                c = new TableCell();
                c.Text = rs.Rows[i]["KTP1"].ToString()
                    + "</br>" + rs.Rows[i]["KTP2"].ToString()
                    + "</br>" + rs.Rows[i]["KTP3"].ToString()
                    + "</br>" + rs.Rows[i]["KTP4"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Db.SingleString("SELECT Nama FROM MS_AGENT WHERE NoAgent = '" + rs.Rows[i]["NoAgent"].ToString() + "' ");
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoUnit"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Math.Round(Convert.ToDecimal(rs.Rows[i]["Luas"]), 2));
                c.HorizontalAlign = HorizontalAlign.Right;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                //c = new TableCell();
                //c.Text = rs.Rows[i]["L"].ToString();
                //c.HorizontalAlign = HorizontalAlign.Left;
                //c.VerticalAlign = VerticalAlign.Top;
                //r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["BankKPR"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["Gross"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Math.Round(Convert.ToDecimal(rs.Rows[i]["DiskonRupiah"]), 0));
                c.HorizontalAlign = HorizontalAlign.Right;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["NilaiKontrak"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                PotensiKPR = Convert.ToDecimal(rs.Rows[i]["NilaiPengajuan"]);
                c.Text = Cf.Num(PotensiKPR);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                RealisasiAkad = Convert.ToDecimal(rs.Rows[i]["ApprovalKPR"]);
                c.Text = Cf.Num(RealisasiAkad);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                rpt.Rows.Add(r);

                t1 += Convert.ToDecimal(rs.Rows[i]["Gross"]);
                t2 += Convert.ToDecimal(rs.Rows[i]["DiskonRupiah"]);
                t3 += Convert.ToDecimal(rs.Rows[i]["NilaiKontrak"]);
                t4 += PotensiKPR;
                t5 += RealisasiAkad;
                t6 += Convert.ToDecimal(rs.Rows[i]["Luas"]);
            }
            SubTotal(t1, t2, t3, t4, t5, t6);
        }

        protected void SubTotal(decimal t1, decimal t2, decimal t3, decimal t4, decimal t5
            , decimal t6)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = Rpt.Foot();
            c.Text = "<strong>GRAND TOTAL</strong>";
            c.ColumnSpan = 9;
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(Math.Round(t6, 2));
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = "&nbsp;";
            if (Lokasi != "SEMUA")
                c.ColumnSpan = 2;
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t1);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(Math.Round(t2, 0));
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
            c.Text = Cf.Num(t5);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            rpt.Rows.Add(r);
        }



    }
}
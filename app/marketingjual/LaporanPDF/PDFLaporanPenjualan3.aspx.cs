using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL.Laporan
{
    public partial class LaporanPenjualan3 : System.Web.UI.Page
    {
        private string TipePro { get { return (Request.QueryString["tipepro"]); } }
        private string Tipe { get { return (Request.QueryString["tipe"]); } }
        private string AttachmentID { get { return (Request.QueryString["id"]); } }
        private string Input { get { return Request.QueryString["input"]; } }
        private string Lokasi { get { return (Request.QueryString["lokasi"]); } }
        private string TitipJual { get { return (Request.QueryString["titipjual"]); } }
        private string Agent { get { return (Request.QueryString["agent"]); } }
        private string Thn1 { get { return (Request.QueryString["thn1"]); } }
        private string Thn2 { get { return (Request.QueryString["thn2"]); } }
        private string Principal { get { return (Request.QueryString["principal"]); } }
        private string UserID { get { return (Request.QueryString["userid"]); } }
        private string CaraBayar { get { return (Request.QueryString["carabayar"]); } }
        private string Project { get { return (Request.QueryString["project"]); } }
        private string Perusahaan { get { return (Request.QueryString["pers"]); } }
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
           
            rpt.Visible = true;
            headJudul.Visible = true;

            newHeader();
            Fill();
        }

        private void newHeader()
        {
            string tipe = "";
            tipe = Tipe.Replace("-", ",");
            string header = "<h2>" + Mi.Pt + "</h2>";
            header += "<h1 class='title'>LAPORAN PENJUALAN TAHUNAN</h1>";
            header += "Periode : " + Thn1 + " s/d " + Thn2;
            header += "<br/>Jenis : " + tipe.Replace("%", " ");
            header += "<br/>Lokasi : " + Lokasi;
            header += "<br/>Principal : " + Agent;
            header += "<br/>Perusahaan : " + Perusahaan;
            header += "<br/>Project : " + Project;
            header += "<br/> Laporan dihasilkan pada hari " + Cf.IndoWeek(DateTime.Today);
            header += ", " + Cf.Date(DateTime.Now) + " dari workstation " + Act.IP + " oleh user " + Act.UserID + "<br /><br />";
            headJudul.Text = header;
        }



        private void Fill()
        {
            string nLokasi = "";
            if (Lokasi != "SEMUA")
            {
                nLokasi = " AND Lokasi = '" + Cf.Str(Lokasi) + "'";
            }

            string nAgent = "";
            if (Agent != "SEMUA")
            {
                nAgent = " AND Principal = '" + Cf.Str(Agent) + "'";
            }

            string nProject = "";
            if (Project != "SEMUA") nProject = " AND MS_KONTRAK.Project IN ('" + Project.Replace(",","','") + "')";
            string nPerusahaan = "";
            if (Perusahaan != "SEMUA") nPerusahaan = " AND MS_KONTRAK.Pers = '" + Perusahaan + "'";

            //change parameter tipe
            string cb = String.Empty;
            cb = CaraBayar.Replace("-", ",").TrimEnd(',');
            cb = cb.Replace("+", " ");
            cb = cb.Replace(",", "','");
            cb = "'" + cb + "'";

            string tipe = String.Empty;
            tipe = Tipe.Replace("-", ",").TrimEnd(',');
            tipe = tipe.Replace("%", " ");
            tipe = tipe.Replace(",", "','");
            tipe = "'" + tipe + "'";


            string carabayar = "";
            if (cb != "")
                carabayar = " AND MS_KONTRAK.CaraBayar IN (" + cb + ")";

            string nTipe = "";
            if (tipe != "")
                nTipe = " AND MS_KONTRAK.Jenis IN (" + tipe + ")";

            string aa = "";
            if (UserAgent() > 0)
                aa = " AND NoAgent = " + UserAgent();

            int Tahun1 = Convert.ToInt32(Thn1);
            int Tahun2 = Convert.ToInt32(Thn2);

            decimal t1 = 0, t2 = 0, t3 = 0, t4 = 0, t5 = 0, t6 = 0, t7 = 0, t8 = 0, t9 = 0, t10 = 0, t11 = 0, t12 = 0;

            for (int i = Tahun1; i <= Tahun2; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                r.VerticalAlign = VerticalAlign.Top;

                c = new TableCell();
                c.Text = i.ToString();
                c.HorizontalAlign = HorizontalAlign.Center;
                r.Cells.Add(c);

                int Unit1 = Db.SingleInteger("SELECT COUNT(*) FROM MS_KONTRAK WHERE Status = 'A' AND YEAR(TglKontrak) = " + i + nLokasi + carabayar + nProject + nPerusahaan + aa + nTipe);                          
                decimal Net1 = Db.SingleDecimal("SELECT ISNULL(SUM(LuasNett),0) FROM MS_UNIT WHERE NoStock IN (SELECT NoStock FROM MS_KONTRAK WHERE Status = 'A' AND YEAR(TglKontrak) = " + i + nLokasi + carabayar + nProject + nPerusahaan + aa + nTipe + ")");
                decimal SGA1 = Db.SingleDecimal("SELECT ISNULL(SUM(LuasSG),0) FROM MS_UNIT WHERE NoStock IN (SELECT NoStock FROM MS_KONTRAK WHERE Status = 'A' AND YEAR(TglKontrak) = " + i + nLokasi + carabayar + nProject + nPerusahaan + aa + nTipe + ")");
                decimal Nilai1 = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiKontrak),0) FROM MS_KONTRAK WHERE Status = 'A' AND YEAR(TglKontrak) = " + i + nLokasi + carabayar + nProject + nPerusahaan + aa + nTipe);

                c = new TableCell();
                c.Text = Unit1.ToString();
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Net1);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(SGA1);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Nilai1);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "&nbsp;";
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                int Unit2 = Db.SingleInteger("SELECT COUNT(*) FROM MS_KONTRAK WHERE Status = 'B' AND YEAR(TglKontrak) = " + i + nLokasi + carabayar + nProject + nPerusahaan + aa + nTipe);
                decimal Net2 = Db.SingleDecimal("SELECT ISNULL(SUM(LuasNett),0) FROM MS_UNIT WHERE NoStock IN (SELECT NoStock FROM MS_KONTRAK WHERE Status = 'B' AND YEAR(TglKontrak) = " + i + nLokasi + carabayar + nProject + nPerusahaan + aa + nTipe + ")");
                decimal SGA2 = Db.SingleDecimal("SELECT ISNULL(SUM(LuasSG),0) FROM MS_UNIT WHERE NoStock IN (SELECT NoStock FROM MS_KONTRAK WHERE Status = 'B' AND YEAR(TglKontrak) = " + i + nLokasi + carabayar + nProject + nPerusahaan + aa + nTipe + ")");
                decimal Nilai2 = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiKontrak),0) FROM MS_KONTRAK WHERE Status = 'B' AND YEAR(TglKontrak) = " + i + nLokasi + carabayar + nProject + nPerusahaan + aa + nTipe);

                c = new TableCell();
                c.Text = Unit2.ToString();
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Net2);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(SGA2);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Nilai2);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "&nbsp;";
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                int Unit3 = Unit1 + Unit2;
                decimal Net3 = Net1 + Net2;
                decimal SGA3 = SGA1 + SGA2;
                decimal Nilai3 = Nilai1 + Nilai2;

                c = new TableCell();
                c.Text = Unit3.ToString();
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Net3);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(SGA3);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Nilai3);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                rpt.Rows.Add(r);

                t1 += Unit1;
                t2 += Net1;
                t3 += SGA1;
                t4 += Nilai1;
                t5 += Unit2;
                t6 += Net2;
                t7 += SGA2;
                t8 += Nilai2;
                t9 += Unit3;
                t10 += Net3;
                t11 += SGA3;
                t12 += Nilai3;

                if (i == Tahun2)
                    SubTotal("TOTAL", t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12);
            }
        }

        private void SubTotal(string txt, decimal t1, decimal t2, decimal t3, decimal t4, decimal t5, decimal t6, decimal t7, decimal t8, decimal t9, decimal t10, decimal t11, decimal t12)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = Rpt.Foot();
            c.Text = txt;
            c.HorizontalAlign = HorizontalAlign.Center;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t1);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t2);
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
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t5);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t6);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t7);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t8);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t9);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t10);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t11);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t12);
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

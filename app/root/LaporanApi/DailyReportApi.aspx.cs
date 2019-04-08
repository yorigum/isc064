using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LaporanApi
{
    public partial class DailyReportApi : System.Web.UI.Page
    {
        protected System.Web.UI.WebControls.PlaceHolder list;
        protected DateTime Tgl { get { return Convert.ToDateTime(Request.QueryString["Tgl"]); } }
        protected string UserID { get { return Request.QueryString["UserID"]; } }
        protected string Project { get { return Request.QueryString["Project"]; } }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Report();
            }
        }

        protected void Report()
        {
            rpt.Visible = true;

            Fill();
        }

        protected void Fill()
        {
            Header();
            StockUnit();

            Label l = new Label();
            l.Text = "<br />";
            rpt.Controls.Add(l);

            Sales();

            Label l2 = new Label();
            l2.Text = "<br />";
            rpt.Controls.Add(l2);

            AktivitasCustomer();

            Label l3 = new Label();
            l3.Text = "<br />";
            rpt.Controls.Add(l3);

            Finance();
        }

        protected void Header()
        {
            Label l;

            l = new Label();
            l.Text = Mi.Pt;
            l.Text += "<h1 class='title'>Daily Report</h1>";
            l.Text += "<h2>per " + Cf.Day(Tgl) + "</h2>";
            l.Text += "Laporan dihasilkan pada hari : " + Cf.IndoWeek(DateTime.Today)
                + ", " + Cf.Date(DateTime.Now)
                + " dari workstation : eProperty Mobile"
                + " dan username : " + UserID
                + "<br /><br />";
            rpt.Controls.Add(l);
        }

        protected void StockUnit()
        {
            Table tb;
            TableRow tr;
            TableHeaderCell hc;
            TableCell c;

            tb = new Table();
            tb.CssClass = "tb blue-skin";
            tb.CellSpacing = 1;

            tr = new TableRow();
            tb.Rows.Add(tr);

            c = new TableCell();
            c.Text = "<b>Stock Unit</b>";
            c.ColumnSpan = 5;
            tr.Cells.Add(c);

            tr = new TableRow();
            tb.Rows.Add(tr);

            hc = new TableHeaderCell();
            hc.Text = "Lokasi";
            hc.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.Text = "Available";
            hc.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.Text = "Booking";
            hc.HorizontalAlign = HorizontalAlign.Right;
            tr.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.Text = "Hold";
            hc.HorizontalAlign = HorizontalAlign.Right;
            tr.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.Text = "Sold";
            hc.HorizontalAlign = HorizontalAlign.Right;
            tr.Cells.Add(hc);

            rpt.Controls.Add(tb);

            DataTable rs = Db.Rs("SELECT DISTINCT Lokasi FROM ISC064_MARKETINGJUAL..MS_UNIT WHERE Project = '" + Project + "'");

            int tavailable = 0, tbooking = 0, thold = 0, tsold = 0;

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected)
                    break;

                tr = new TableRow();
                tb.Rows.Add(tr);

                c = new TableCell();
                c.Text = rs.Rows[i]["Lokasi"].ToString();
                c.HorizontalAlign = HorizontalAlign.Center;
                tr.Cells.Add(c);

                //Available
                c = new TableCell();
                int available = AvailableUnit(rs.Rows[i]["Lokasi"].ToString()) - BookedUnit(rs.Rows[i]["Lokasi"].ToString());
                c.Text = available.ToString();
                c.HorizontalAlign = HorizontalAlign.Center;
                tr.Cells.Add(c);

                //Booking
                c = new TableCell();
                int booking = BookedUnit(rs.Rows[i]["Lokasi"].ToString());
                c.Text = booking.ToString();
                c.HorizontalAlign = HorizontalAlign.Center;
                tr.Cells.Add(c);

                //Hold
                c = new TableCell();
                int hold = HoldUnit(rs.Rows[i]["Lokasi"].ToString());
                c.Text = hold.ToString();
                c.HorizontalAlign = HorizontalAlign.Center;
                tr.Cells.Add(c);

                //Sold
                c = new TableCell();
                int sold = SoldUnit(rs.Rows[i]["Lokasi"].ToString());
                c.Text = sold.ToString();
                c.HorizontalAlign = HorizontalAlign.Center;
                tr.Cells.Add(c);

                tavailable += available;
                tbooking += booking;
                thold += hold;
                tsold += sold;
            }

            tr = new TableRow();
            tb.Rows.Add(tr);

            c = Rpt.Foot();
            c.Text = "Total";
            c.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = tavailable.ToString();
            c.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = tbooking.ToString();
            c.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = thold.ToString();
            c.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = tsold.ToString();
            c.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(c);
        }

        protected void Sales()
        {
            Table tb;
            TableRow tr;
            TableHeaderCell hc;
            TableCell c;

            tb = new Table();
            tb.CssClass = "tb blue-skin";
            tb.CellSpacing = 1;

            tr = new TableRow();
            tb.Rows.Add(tr);

            c = new TableCell();
            c.Text = "<b>Sales</b>";
            c.ColumnSpan = 4;
            tr.Cells.Add(c);

            tr = new TableRow();
            tb.Rows.Add(tr);

            hc = new TableHeaderCell();
            hc.Text = "Tipe Unit";
            hc.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.Text = "Jumlah Unit";
            hc.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.Text = "Nilai Kontrak (Rp.)";
            hc.HorizontalAlign = HorizontalAlign.Right;
            tr.Cells.Add(hc);

            rpt.Controls.Add(tb);

            int tunitjenis = 0;
            decimal tnilaijenis = 0;

            DataTable rs = Db.Rs("SELECT DISTINCT Jenis FROM ISC064_MARKETINGJUAL..MS_KONTRAK WHERE TglKontrak = '" + Cf.Tgl112(Tgl) + "' AND Status = 'A' AND Project = '" + Project + "'");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected)
                    break;

                tr = new TableRow();
                tb.Rows.Add(tr);

                c = new TableCell();
                c.Text = rs.Rows[i]["Jenis"].ToString();
                c.HorizontalAlign = HorizontalAlign.Center;
                tr.Cells.Add(c);


                c = new TableCell();
                int unitjenis = UnitJenis(rs.Rows[i]["Jenis"].ToString());
                c.Text = unitjenis.ToString();
                c.HorizontalAlign = HorizontalAlign.Center;
                tr.Cells.Add(c);

                c = new TableCell();
                decimal nilaijenis = NilaiJenis(rs.Rows[i]["Jenis"].ToString());
                c.Text = Cf.Num(nilaijenis);
                c.HorizontalAlign = HorizontalAlign.Right;
                tr.Cells.Add(c);

                tunitjenis += unitjenis;
                tnilaijenis += nilaijenis;
            }

            tr = new TableRow();
            tb.Rows.Add(tr);

            c = Rpt.Foot();
            c.Text = "Total";
            c.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = tunitjenis.ToString();
            c.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(tnilaijenis);
            c.HorizontalAlign = HorizontalAlign.Right;
            tr.Cells.Add(c);
        }

        protected void AktivitasCustomer()
        {
            Table tb;
            TableRow tr;
            TableHeaderCell hc;
            TableCell c;

            tb = new Table();
            tb.CssClass = "tb blue-skin";
            tb.CellSpacing = 1;

            tr = new TableRow();
            tb.Rows.Add(tr);

            c = new TableCell();
            c.Text = "<b>Aktivitas Customer</b>";
            c.ColumnSpan = 2;
            tr.Cells.Add(c);

            tr = new TableRow();
            tb.Rows.Add(tr);

            hc = new TableHeaderCell();
            hc.Text = "Aktivitas";
            hc.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.Text = "Jumlah";
            hc.HorizontalAlign = HorizontalAlign.Right;
            tr.Cells.Add(hc);

            tr = new TableRow();
            tb.Rows.Add(tr);

            c = new TableCell();
            c.Text = "Batal";
            tr.Cells.Add(c);

            c = new TableCell();
            int batal = Db.SingleInteger("SELECT COUNT(*) FROM ISC064_MARKETINGJUAL..MS_KONTRAK WHERE TglBatal = '" + Cf.Tgl112(Tgl) + "' AND Status = 'B' AND Project = '" + Project + "'");
            c.Text = batal.ToString();
            c.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(c);

            tr = new TableRow();
            tb.Rows.Add(tr);

            c = new TableCell();
            c.Text = "Pengalihan Hak";
            tr.Cells.Add(c);

            c = new TableCell();
            int pengalihanhak = Db.SingleInteger("SELECT COUNT(*) FROM ISC064_MARKETINGJUAL..MS_KONTRAK_LOG WHERE Tgl = '" + Cf.Tgl112(Tgl) + "' AND Aktivitas = 'GN'");
            c.Text = pengalihanhak.ToString();
            c.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(c);

            tr = new TableRow();
            tb.Rows.Add(tr);

            c = new TableCell();
            c.Text = "Pindah Unit";
            tr.Cells.Add(c);

            c = new TableCell();
            int pindahunit = Db.SingleInteger("SELECT COUNT(*) FROM ISC064_MARKETINGJUAL..MS_KONTRAK_LOG WHERE Tgl = '" + Cf.Tgl112(Tgl) + "' AND Aktivitas = 'GU'");
            c.Text = pindahunit.ToString();
            c.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(c);

            rpt.Controls.Add(tb);
        }

        protected void Finance()
        {
            Table tb;
            TableRow tr;
            TableHeaderCell hc;
            TableCell c;

            tb = new Table();
            tb.CssClass = "tb blue-skin";
            tb.CellSpacing = 1;

            tr = new TableRow();
            tb.Rows.Add(tr);

            c = new TableCell();
            c.Text = "<b>Finance</b>";
            c.ColumnSpan = 4;
            tr.Cells.Add(c);

            tr = new TableRow();
            tb.Rows.Add(tr);

            hc = new TableHeaderCell();
            hc.Text = "Payment";
            hc.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.Text = "Nilai (Rp.)";
            hc.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(hc);

            tr = new TableRow();
            tb.Rows.Add(tr);

            c = new TableCell();
            c.Text = "Cash";
            tr.Cells.Add(c);

            c = new TableCell();
            decimal cash = Db.SingleDecimal("SELECT ISNULL(SUM(Total),0) FROM ISC064_FINANCEAR..MS_TTS WHERE TglTTS = '" + Cf.Tgl112(Tgl) + "' AND CaraBayar = 'TN' AND Project = '" + Project + "'");
            c.Text = Cf.Num(cash);
            c.HorizontalAlign = HorizontalAlign.Right;
            tr.Cells.Add(c);

            tr = new TableRow();
            tb.Rows.Add(tr);

            c = new TableCell();
            c.Text = "Credit Card";
            tr.Cells.Add(c);

            c = new TableCell();
            decimal creditcard = Db.SingleDecimal("SELECT ISNULL(SUM(Total),0) FROM ISC064_FINANCEAR..MS_TTS WHERE TglTTS = '" + Cf.Tgl112(Tgl) + "' AND CaraBayar = 'KK' AND Project = '" + Project + "'");
            c.Text = Cf.Num(creditcard);
            c.HorizontalAlign = HorizontalAlign.Right;
            tr.Cells.Add(c);

            tr = new TableRow();
            tb.Rows.Add(tr);

            c = new TableCell();
            c.Text = "Debit Card";
            tr.Cells.Add(c);

            c = new TableCell();
            decimal debitcard = Db.SingleDecimal("SELECT ISNULL(SUM(Total),0) FROM ISC064_FINANCEAR..MS_TTS WHERE TglTTS = '" + Cf.Tgl112(Tgl) + "' AND CaraBayar = 'KD' AND Project = '" + Project + "'");
            c.Text = Cf.Num(debitcard);
            c.HorizontalAlign = HorizontalAlign.Right;
            tr.Cells.Add(c);

            tr = new TableRow();
            tb.Rows.Add(tr);

            c = new TableCell();
            c.Text = "Giro";
            tr.Cells.Add(c);

            c = new TableCell();
            decimal giro = Db.SingleDecimal("SELECT ISNULL(SUM(Total),0) FROM ISC064_FINANCEAR..MS_TTS WHERE TglTTS = '" + Cf.Tgl112(Tgl) + "' AND CaraBayar = 'BG' AND Project = '" + Project + "'");
            c.Text = Cf.Num(giro);
            c.HorizontalAlign = HorizontalAlign.Right;
            tr.Cells.Add(c);

            tr = new TableRow();
            tb.Rows.Add(tr);

            c = new TableCell();
            c.Text = "Transfer";
            tr.Cells.Add(c);

            c = new TableCell();
            decimal transfer = Db.SingleDecimal("SELECT ISNULL(SUM(Total),0) FROM ISC064_FINANCEAR..MS_TTS WHERE TglTTS = '" + Cf.Tgl112(Tgl) + "' AND CaraBayar = 'TR' AND Project = '" + Project + "'");
            c.Text = Cf.Num(transfer);
            c.HorizontalAlign = HorizontalAlign.Right;
            tr.Cells.Add(c);

            tr = new TableRow();
            tb.Rows.Add(tr);

            c = Rpt.Foot();
            c.Text = "Payment Received";
            tr.Cells.Add(c);

            c = Rpt.Foot();
            decimal total = Db.SingleDecimal("SELECT ISNULL(SUM(Total),0) FROM ISC064_FINANCEAR..MS_TTS WHERE TglTTS = '" + Cf.Tgl112(Tgl) + "' AND Project = '" + Project + "'");
            c.Text = Cf.Num(total);
            c.HorizontalAlign = HorizontalAlign.Right;
            tr.Cells.Add(c);

            rpt.Controls.Add(tb);
        }

        protected int SoldUnit(string Lokasi)
        {
            int c = Db.SingleInteger(
                "SELECT COUNT(*) FROM ISC064_MARKETINGJUAL..MS_UNIT"
                + " WHERE NoStock IN (SELECT NoStock FROM ISC064_MARKETINGJUAL..MS_KONTRAK WHERE Status = 'A' AND Project = '" + Project + "')"
                + " AND Lokasi = '" + Lokasi + "' AND Project = '" + Project + "'");

            return c;
        }

        protected int BookedUnit(string Lokasi)
        {
            int c = Db.SingleInteger(
                "SELECT COUNT(*) FROM ISC064_MARKETINGJUAL..MS_UNIT"
                + " WHERE NoStock IN (SELECT NoStock FROM ISC064_MARKETINGJUAL..MS_RESERVASI WHERE Status = 'A')"
                + " AND Lokasi = '" + Lokasi + "' AND Project = '" + Project + "'");

            return c;
        }

        protected int AvailableUnit(string Lokasi)
        {
            int c = Db.SingleInteger(
                "SELECT COUNT(*) FROM ISC064_MARKETINGJUAL..MS_UNIT"
                + " WHERE NoStock NOT IN (SELECT NoStock FROM ISC064_MARKETINGJUAL..MS_KONTRAK WHERE Status = 'A' AND Project = '" + Project + "')"
                + " AND Lokasi = '" + Lokasi + "' AND Project = '" + Project + "'");

            return c;
        }

        protected int HoldUnit(string Lokasi)
        {
            int c = Db.SingleInteger(
                "SELECT COUNT(*) FROM ISC064_MARKETINGJUAL..MS_UNIT WHERE Status = 'H' AND Lokasi = '" + Lokasi + "' AND Project = '" + Project + "'"
                );

            return c;
        }

        protected int UnitJenis(string Jenis)
        {
            int c = Db.SingleInteger(
                "SELECT COUNT(*) FROM ISC064_MARKETINGJUAL..MS_KONTRAK WHERE TglKontrak = '" + Cf.Tgl112(Tgl) + "' "
                + " AND Status = 'A' AND Jenis = '" + Jenis + "' AND Project = '" + Project + "'");

            return c;
        }

        protected decimal NilaiJenis(string Jenis)
        {
            decimal d = Db.SingleDecimal(
                "SELECT ISNULL(SUM(NilaiKontrak), 0) FROM ISC064_MARKETINGJUAL..MS_KONTRAK WHERE TglKontrak = '" + Cf.Tgl112(Tgl) + "'"
                + " AND Status = 'A' AND Jenis = '" + Jenis + "' AND Project = '" + Project + "'");

            return d;
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

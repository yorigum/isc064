using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.COLLECTION.Laporan
{
    public partial class Col : System.Web.UI.Page
    {
        protected System.Web.UI.WebControls.ListBox lokasi;
        protected System.Web.UI.WebControls.ListBox agent;
        protected System.Web.UI.WebControls.CheckBox jenisCheck;
        protected System.Web.UI.WebControls.Label jenisc;
        protected System.Web.UI.WebControls.CheckBoxList jenis;
        protected System.Web.UI.WebControls.RadioButton bfS;
        protected System.Web.UI.WebControls.RadioButton bf1;
        protected System.Web.UI.WebControls.RadioButton bf2;
        private string UserID { get { return (Request.QueryString["userid"]); } }
        private string AttachmentID { get { return (Request.QueryString["id"]); } }
        private string Project { get { return (Request.QueryString["project"]); } }
        private string Perusahaan { get { return (Request.QueryString["pers"]); } }
        private string StatusA { get { return (Request.QueryString["status_a"]); } }
        private string StatusS { get { return (Request.QueryString["status_s"]); } }
        private string StatusB { get { return (Request.QueryString["status_b"]); } }
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
            param.Visible = false;
            rpt.Visible = true;
            headJudul.Visible = true;

            newHeader();
            //Header();
            HeaderBayar();
            HeaderBayar2();
            Fill();
        }

        private void newHeader()
        {
            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");

            string header = "<p>" + Mi.Pt + "</p>";
            header += "<h1 class='title'>LAPORAN PROYEKSI PENERIMAAN</h1>";
            header += "Periode : " + Cf.Day(Dari) + " s/d " + Cf.Day(Sampai);
            header += "<br/> Laporan dihasilkan pada hari " + Cf.IndoWeek(DateTime.Today);
            header += ", " + Cf.Date(DateTime.Now) + " dari workstation " + Act.IP + " oleh user " + Act.UserID + "<br /><br />";
            headJudul.Text = header;
        }

        private void Header()
        {
            System.Text.StringBuilder x = new System.Text.StringBuilder();

            Rpt.Judul(x, comp, judul);

            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");

            Rpt.SubJudul(x
                , "Tgl. Jatuh Tempo: " + Cf.Day(Dari) + " s/d " + Cf.Day(Sampai)
                );

            Rpt.Header(rpt, x);
        }

        private void HeaderBayar()
        {
            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");


            int m1 = Dari.Month;
            int m2 = Sampai.Month;
            int y1 = Dari.Year;
            int y2 = Sampai.Year;

            int th = y2 - y1;
            int bln = (m2 - m1) + 1;

            int jum = 0;
            if (th > 0)
            {
                jum = (((th - 1) * 12) + (12 - m1) + m2) + 1;
            }
            else
            {
                jum = bln;
            }

            rpt.Rows[0].Cells[8].ColumnSpan = jum * 5;

            TableRow r = new TableRow();
            r.BackColor = Color.LightGray;
            TableCell c;

            //c = new TableHeaderCell();
            //c.ColumnSpan = 8;
            //c.RowSpan = 2;
            //r.Cells.Add(c);

            for (int j = 1; j <= jum; j++)
            {
                c = new TableHeaderCell();
                c.Text = Cf.Monthname(Dari.AddMonths(j - 1).Month) + " " + Dari.AddMonths(j - 1).Year.ToString();
                c.ColumnSpan = 5;
                r.Cells.Add(c);
            }

            //c = new TableHeaderCell();
            //c.ColumnSpan = 2;
            //c.RowSpan = 2;
            //r.Cells.Add(c);

            rpt.Rows.Add(r);
        }

        private void HeaderBayar2()
        {
            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");


            int m1 = Dari.Month;
            int m2 = Sampai.Month;
            int y1 = Dari.Year;
            int y2 = Sampai.Year;

            int th = y2 - y1;
            int bln = (m2 - m1) + 1;

            int jum = 0;
            if (th > 0)
            {
                jum = (((th - 1) * 12) + (12 - m1) + m2) + 1;
            }
            else
            {
                jum = bln;
            }

            TableRow r = new TableRow();
            r.BackColor = Color.LightGray;
            TableCell c;

            for (int j = 1; j <= jum; j++)
            {
                c = new TableHeaderCell();
                c.Text = "Saldo Awal";
                r.Cells.Add(c);

                c = new TableHeaderCell();
                c.Text = "Perencanaan";
                r.Cells.Add(c);

                c = new TableHeaderCell();
                c.Text = "Realisasi";
                r.Cells.Add(c);

                c = new TableHeaderCell();
                c.Text = "Tgl.";
                r.Cells.Add(c);

                c = new TableHeaderCell();
                c.Text = "Saldo Akhir";
                r.Cells.Add(c);
            }

            rpt.Rows.Add(r);
        }

        private void Fill()
        {
            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");


            int m1 = Dari.Month;
            int m2 = Sampai.Month;
            int y1 = Dari.Year;
            int y2 = Sampai.Year;

            int th = y2 - y1;
            int bln = (m2 - m1) + 1;

            int jum = 0;
            if (th > 0)
            {
                jum = (((th - 1) * 12) + (12 - m1) + m2) + 1;
            }
            else
            {
                jum = bln;
            }

            decimal[] sawal = new decimal[jum];
            decimal[] t = new decimal[jum];
            decimal[] re = new decimal[jum];
            decimal[] sakhir = new decimal[jum];

            decimal t1 = 0, t2 = 0, t3 = 0, t4 = 0, t5 = 0, t6 = 0;

            string Status = "";
           if(StatusA != "") Status = "A";
            else if (StatusB != "") Status = "B";


            string agent = "";
            if (UserAgent() > 0)
                agent = " AND a.Status = " + 'A';

            string nstatus = "";
            if (StatusA != "")
                nstatus = " AND a.Status = 'A' ";           
            else if (StatusB != "")
                nstatus = " AND a.Status = 'B' ";            

            string nProject = "";
            if (Project != "SEMUA") nProject = " AND a.Project IN ('" + Project.Replace(",","','") + "')";
            string nPerusahaan = "";
            if (Perusahaan != "SEMUA") nPerusahaan = " AND a.Pers = '" + Perusahaan + "'";

            string strSql = "SELECT a.*, b.Nama, b.NoTelp, b.NoHP, a.Status"
                + " FROM ISC064_MARKETINGJUAL..MS_KONTRAK a"
                + " INNER JOIN ISC064_MARKETINGJUAL..MS_CUSTOMER b ON a.NoCustomer = b.NoCustomer"
                + " WHERE 1=1"
                + nProject
                + nPerusahaan
                + nstatus
                + agent
                ;

            DataTable rs = Db.Rs(strSql);            

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected)
                    break;

                bool display = false;
                int tag = Db.SingleInteger("SELECT COUNT(*) FROM ISC064_MARKETINGJUAL..MS_TAGIHAN"
                    + " WHERE CONVERT(VARCHAR,TglJT,112) >= '" + Cf.Tgl112(Dari) + "'"
                    + " AND CONVERT(VARCHAR,TglJT,112) <= '" + Cf.Tgl112(Sampai) + "'"
                    );
                if (tag > 0) display = true;

                if (display)
                {
                    TableRow r = new TableRow();
                    r.Attributes["ondblclick"] = "javascript:popEditKontrak('" + rs.Rows[i]["NoKontrak"] + "');";
                    r.VerticalAlign = VerticalAlign.Top;
                    TableCell c;

                    c = new TableCell();
                    c.Text = (i + 1).ToString();
                    c.HorizontalAlign = HorizontalAlign.Center;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = rs.Rows[i]["Nama"].ToString();
                    c.HorizontalAlign = HorizontalAlign.Left;
                    c.Wrap = false;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = rs.Rows[i]["NoUnit"].ToString();
                    c.HorizontalAlign = HorizontalAlign.Left;
                    c.Wrap = false;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Cf.Num(rs.Rows[i]["NilaiDPP"]);
                    c.HorizontalAlign = HorizontalAlign.Left;
                    c.Wrap = false;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Cf.Num(rs.Rows[i]["NilaiPPN"]);
                    c.HorizontalAlign = HorizontalAlign.Right;
                    c.Wrap = false;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Cf.Num(rs.Rows[i]["NilaiKontrak"]);
                    c.HorizontalAlign = HorizontalAlign.Left;
                    c.Wrap = false;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = rs.Rows[i]["Skema"].ToString();
                    c.HorizontalAlign = HorizontalAlign.Left;
                    c.Wrap = false;
                    r.Cells.Add(c);

                    decimal dp = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiTagihan),0) FROM ISC064_MARKETINGJUAL..MS_TAGIHAN WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' AND Tipe = 'DP'");

                    c = new TableCell();
                    c.Text = Cf.Num(dp);
                    c.HorizontalAlign = HorizontalAlign.Right;
                    c.Wrap = false;
                    r.Cells.Add(c);

                    decimal byr = 0;
                    for (int j = 1; j <= jum; j++)
                    {
                        int m = Dari.AddMonths(j - 1).Month;
                        int y = Dari.AddMonths(j - 1).Year;
                        decimal tagihan = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiTagihan),0) FROM ISC064_MARKETINGJUAL..MS_TAGIHAN WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' AND MONTH(TglJT) = " + m + " AND YEAR(TglJT) = " + y);

                        DateTime TglLalu = new DateTime(y, m, 1);
                        decimal tagihan_lalu = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiTagihan),0) FROM ISC064_MARKETINGJUAL..MS_TAGIHAN WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' AND CONVERT(VARCHAR,TglJT,112) < '" + Cf.Tgl112(TglLalu) + "'");
                        decimal realisasi_lalu = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan),0)"
                            + " FROM ISC064_MARKETINGJUAL..MS_PELUNASAN a"
                            + " INNER JOIN MS_TTS b ON a.NoTTS = b.NoTTS"
                            + " INNER JOIN ISC064_MARKETINGJUAL..MS_TAGIHAN c ON a.NoTagihan = c.NoUrut AND a.NoKontrak = c.NoKontrak"
                            + " WHERE c.NoKontrak = '" + Cf.Pk(rs.Rows[i]["NoKontrak"]) + "'"
                            + " AND b.Status <> 'VOID'"
                            + " AND CONVERT(VARCHAR,c.TglJT,112) < '" + Cf.Tgl112(TglLalu) + "'");
                        decimal SaldoAwal = tagihan_lalu - realisasi_lalu;

                        c = new TableCell();
                        c.Text = Cf.Num(SaldoAwal);
                        c.HorizontalAlign = HorizontalAlign.Right;
                        c.Wrap = false;
                        r.Cells.Add(c);

                        c = new TableCell();
                        c.Text = Cf.Num(tagihan);
                        c.HorizontalAlign = HorizontalAlign.Right;
                        c.Wrap = false;
                        r.Cells.Add(c);

                        decimal realisasi = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan),0)"
                            + " FROM ISC064_MARKETINGJUAL..MS_PELUNASAN a"
                            + " INNER JOIN MS_TTS b ON a.NoTTS = b.NoTTS"
                            + " INNER JOIN ISC064_MARKETINGJUAL..MS_TAGIHAN c ON a.NoTagihan = c.NoUrut AND a.NoKontrak = c.NoKontrak"
                            + " WHERE c.NoKontrak = '" + Cf.Pk(rs.Rows[i]["NoKontrak"]) + "'"
                            + " AND b.Status <> 'VOID'"
                            + " AND MONTH(c.TglJT) = " + m + " AND YEAR(c.TglJT) = " + y);

                        c = new TableCell();
                        c.Text = Cf.Num(realisasi);
                        c.HorizontalAlign = HorizontalAlign.Right;
                        c.Wrap = false;
                        r.Cells.Add(c);

                        string tglrealisasi = "";
                        DataTable aa = Db.Rs("SELECT a.TglPelunasan"
                            + " FROM ISC064_MARKETINGJUAL..MS_PELUNASAN a"
                            + " INNER JOIN MS_TTS b ON a.NoTTS = b.NoTTS"
                            + " INNER JOIN ISC064_MARKETINGJUAL..MS_TAGIHAN c ON a.NoTagihan = c.NoUrut AND a.NoKontrak = c.NoKontrak"
                            + " WHERE c.NoKontrak = '" + Cf.Pk(rs.Rows[i]["NoKontrak"]) + "'"
                            + " AND b.Status <> 'VOID'"
                            + " AND MONTH(c.TglJT) = " + m + " AND YEAR(c.TglJT) = " + y);
                        for (int k = 0; k < aa.Rows.Count; k++)
                        {
                            if (k > 0) tglrealisasi += ", ";
                            tglrealisasi += Cf.Day(aa.Rows[k]["TglPelunasan"]);
                        }

                        c = new TableCell();
                        c.Text = tglrealisasi;
                        c.HorizontalAlign = HorizontalAlign.Left;
                        c.Wrap = false;
                        r.Cells.Add(c);

                        decimal SaldoAkhir = (tagihan - realisasi) + SaldoAwal;

                        c = new TableCell();
                        c.Text = Cf.Num(SaldoAkhir);
                        c.HorizontalAlign = HorizontalAlign.Right;
                        c.Wrap = false;
                        r.Cells.Add(c);

                        sawal[j - 1] += SaldoAwal;
                        t[j-1] += tagihan;
                        re[j-1] += realisasi;
                        sakhir[j - 1] += SaldoAkhir;

                        byr += realisasi;
                    }
                    
                    c = new TableCell();
                    c.Text = Cf.Num(byr); ;
                    c.HorizontalAlign = HorizontalAlign.Right;
                    c.Wrap = false;
                    r.Cells.Add(c);

                    decimal sisa = Convert.ToDecimal(rs.Rows[i]["NilaiKontrak"]) - byr;

                    c = new TableCell();
                    c.Text = Cf.Num(sisa); ;
                    c.HorizontalAlign = HorizontalAlign.Right;
                    c.Wrap = false;
                    r.Cells.Add(c);

                    rpt.Rows.Add(r);

                    t1 += Convert.ToDecimal(rs.Rows[i]["NilaiKontrak"]);
                    t2 += Convert.ToDecimal(dp);
                    t3 += byr;
                    t4 += sisa;

                    if (i == rs.Rows.Count - 1)
                        SubTotal(t1, t2, t3, t4, sawal, t, re, sakhir);
                }
            }
        }

        protected void SubTotal(decimal t1, decimal t2, decimal t3, decimal t4, decimal[] sawal, decimal[] t, decimal[] re, decimal[] sakhir)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = Rpt.Foot();
            c.Text = "<strong>TOTAL</strong>";
            c.ColumnSpan = 5;
            c.HorizontalAlign = HorizontalAlign.Center;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t1);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = "&nbsp;";
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t2);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            for (int i = 0; i < t.Length; i++)
            {
                c = Rpt.Foot();
                c.Text = Cf.Num(sawal[i]);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = Rpt.Foot();
                c.Text = Cf.Num(t[i]);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = Rpt.Foot();
                c.Text = Cf.Num(re[i]);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = Rpt.Foot();
                c.Text = "&nbsp;";
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = Rpt.Foot();
                c.Text = Cf.Num(sakhir[i]);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);
            }

            c = Rpt.Foot();
            c.Text = Cf.Num(t3);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t4);
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

using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.COLLECTION.Laporan
{
    public partial class Col2 : System.Web.UI.Page
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
        private int Dari { get { return Convert.ToInt32((Request.QueryString["blndari"])); } }
        private int Sampai { get { return Convert.ToInt32((Request.QueryString["blnsampai"])); } }
        private int Tahun { get { return Convert.ToInt32((Request.QueryString["thn"])); } }
        private string StatusA { get { return (Request.QueryString["status_a"]); } }
        private string StatusS { get { return (Request.QueryString["status_s"]); } }
        private string StatusB { get { return (Request.QueryString["status_b"]); } }
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
            param.Visible = false;
            rpt.Visible = true;
            headJudul.Visible = true;

            newHeader();
            //Header();
            HeaderBayar();
            Fill();
        }

        private void newHeader()
        {
            string header = "<p>" + Mi.Pt + "</p>";
            header += "<h1 class='title'>LAPORAN COLLECTION</h1>";
            header += "Periode : " + Cf.Monthname(Convert.ToInt32(Dari)) + " s/d " + Cf.Monthname(Convert.ToInt32(Sampai)) + " " + Tahun;
            header += "Project : " + Project;
            header += "Perusahaan : " + Perusahaan;
            header += "<br/> Laporan dihasilkan pada hari " + Cf.IndoWeek(DateTime.Today);
            header += ", " + Cf.Date(DateTime.Now) + " dari workstation " + Act.IP + " oleh user " + Act.UserID + "<br /><br />";
            headJudul.Text = header;
        }

        private void Header()
        {
            System.Text.StringBuilder x = new System.Text.StringBuilder();

            Rpt.Judul(x, comp, judul);

            //DateTime Dari = Convert.ToDateTime(Dari);
            //DateTime Sampai = Convert.ToDateTime(Sampai);
            Rpt.SubJudul(x
                , "Periode : " + Cf.Monthname(Convert.ToInt32(Dari)) + " s/d " + Cf.Monthname(Convert.ToInt32(Sampai)) + " " + Tahun
                );

            Rpt.Header(rpt, x);
        }

        private void HeaderBayar()
        {
            DateTime nDari = Cf.AwalBulan(Convert.ToInt32(Dari), Convert.ToInt32(Tahun));
            DateTime nSampai = Cf.AwalBulan(Convert.ToInt32(Sampai), Convert.ToInt32(Tahun));
            if (nDari > nSampai)
            {
                DateTime x = nSampai;
                nSampai = nDari;
                nDari = x;
            }

            int m1 = nDari.Month;
            int m2 = nSampai.Month;
            int y1 = nDari.Year;
            int y2 = nSampai.Year;

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

            c = new TableHeaderCell();
            c.Text = "NO";
            c.RowSpan = 2;
            r.Cells.Add(c);

            c = new TableHeaderCell();
            c.Text = "Sales";
            c.RowSpan = 2;
            r.Cells.Add(c);

            c = new TableHeaderCell();
            c.Text = "UNIT";
            c.RowSpan = 2;
            r.Cells.Add(c);

            c = new TableHeaderCell();
            c.Text = "Luas";
            c.ColumnSpan = 2;
            r.Cells.Add(c);

            c = new TableHeaderCell();
            c.Text = "Harga exc PPN";
            c.RowSpan = 2;
            r.Cells.Add(c);

            c = new TableHeaderCell();
            c.Text = "Harga Jual inc PPN";
            c.RowSpan = 2;
            r.Cells.Add(c);

            c = new TableHeaderCell();
            c.Text = "CUSTOMER";
            c.RowSpan = 2;
            r.Cells.Add(c);

            c = new TableHeaderCell();
            c.Text = "NPWP";
            c.RowSpan = 2;
            r.Cells.Add(c);

            c = new TableHeaderCell();
            c.Text = "Alamat NPWP";
            c.RowSpan = 2;
            r.Cells.Add(c);

            c = new TableHeaderCell();
            c.Text = "Project";
            c.RowSpan = 2;
            r.Cells.Add(c);

            for (int j = 1; j <= jum; j++)
            {
                c = new TableHeaderCell();
                c.Text = Cf.Monthname(nDari.AddMonths(j - 1).Month) + " " + nDari.AddMonths(j - 1).Year.ToString();
                c.RowSpan = 2;
                r.Cells.Add(c);

                c = new TableHeaderCell();
                c.Text = "Saldo Awal " + Cf.Monthname(nDari.AddMonths(j - 1).Month) + " " + nDari.AddMonths(j - 1).Year.ToString();
                c.RowSpan = 2;
                r.Cells.Add(c);

                c = new TableHeaderCell();
                c.Text = "Memo " + Cf.Monthname(nDari.AddMonths(j - 1).Month) + " " + nDari.AddMonths(j - 1).Year.ToString();
                c.RowSpan = 2;
                r.Cells.Add(c);
            }

            c = new TableHeaderCell();
            c.Text = "TOTAL " + Tahun;
            c.RowSpan = 2;
            r.Cells.Add(c);

            c = new TableHeaderCell();
            c.Text = "TOTAL SALDO AWAL " + Tahun;
            c.RowSpan = 2;
            r.Cells.Add(c);

            c = new TableHeaderCell();
            c.Text = "TOTAL MEMO " + Tahun;
            c.RowSpan = 2;
            r.Cells.Add(c);

            c = new TableHeaderCell();
            c.Text = "Akumulasi<br/>Pembayaran";
            c.RowSpan = 2;
            r.Cells.Add(c);

            c = new TableHeaderCell();
            c.Text = "Akumulasi<br/>Pembayaran (Saldo Awal)";
            c.RowSpan = 2;
            r.Cells.Add(c);

            c = new TableHeaderCell();
            c.Text = "Akumulasi<br/>Pembayaran (Memo)";
            c.RowSpan = 2;
            r.Cells.Add(c);

            c = new TableHeaderCell();
            c.Text = "%";
            c.RowSpan = 2;
            r.Cells.Add(c);

            rpt.Rows.Add(r);

            r = new TableRow();
            r.BackColor = Color.LightGray;

            c = new TableHeaderCell();
            c.Text = "Nett";
            r.Cells.Add(c);

            c = new TableHeaderCell();
            c.Text = "Gross";
            r.Cells.Add(c);

            rpt.Rows.Add(r);
        }

        private void Fill()
        {
            DateTime nDari = Cf.AwalBulan(Convert.ToInt32(Dari), Convert.ToInt32(Tahun));
            DateTime nSampai = Cf.AwalBulan(Convert.ToInt32(Sampai), Convert.ToInt32(Tahun));
            if (nDari > nSampai)
            {
                DateTime x = nSampai;
                nSampai = nDari;
                nDari = x;
            }

            int m1 = nDari.Month;
            int m2 = nSampai.Month;
            int y1 = nDari.Year;
            int y2 = nSampai.Year;

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

            decimal[] t = new decimal[jum];
            decimal[] re = new decimal[jum];
            decimal[] reSA = new decimal[jum];
            decimal[] reMO = new decimal[jum];

            decimal t1 = 0, t2 = 0, t3 = 0, t4 = 0, t5 = 0, t6 = 0, t7 = 0, t8 = 0, t9 = 0, t10 = 0, t11 = 0;

            string Status = "";
            if (StatusA != "") Status = " AND a.Status = 'A'";
            if (StatusB != "") Status = " AND a.Status = 'B'";

            string agent = "";
            if (UserAgent() > 0)
                agent = " AND a.NoAgent = " + UserAgent();

            string nProject = "";
            if (Project != "SEMUA") nProject = " AND a.Project IN ('" + Project.Replace(",","','") + "')";
            string nPerusahaan = "";
            if (Perusahaan != "SEMUA") nPerusahaan = " AND a.Pers = '" + Perusahaan + "'";

            string strSql = "SELECT a.*, b.*"
                + " FROM ISC064_MARKETINGJUAL..MS_KONTRAK a"
                + " INNER JOIN ISC064_MARKETINGJUAL..MS_CUSTOMER b ON a.NoCustomer = b.NoCustomer"
                + " WHERE 1=1"
                + nProject
                + nPerusahaan
                + Status//" AND a.Status ='A'"
                + agent
                + " ORDER BY a.PersenLunas DESC"
                ;

            DataTable rs = Db.Rs(strSql);

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected)
                    break;

                TableRow r = new TableRow();
                r.Attributes["ondblclick"] = "javascript:popEditKontrak('" + rs.Rows[i]["NoKontrak"] + "');";
                r.VerticalAlign = VerticalAlign.Top;
                TableCell c;

                c = new TableCell();
                c.Text = (i + 1).ToString();
                c.HorizontalAlign = HorizontalAlign.Center;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                string Nama = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_AGENT WHERE NoAgent  = '" + rs.Rows[i]["NoAgent"] + "'");
                //c.Text = Cf.Monthname(Convert.ToDateTime(rs.Rows[i]["TglKontrak"]).Month) + " " + Convert.ToDateTime(rs.Rows[i]["TglKontrak"]).Year;
                c.Text = Nama;
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoUnit"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                decimal LuasNett = Db.SingleDecimal("SELECT LuasNett FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT WHERE NoStock = '" + rs.Rows[i]["NoStock"] + "'");
                decimal Luas = Db.SingleDecimal("SELECT Luas FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT WHERE NoStock = '" + rs.Rows[i]["NoStock"] + "'");

                c = new TableCell();
                c.Text = Cf.Num(LuasNett);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Luas);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["NilaiDPP"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["NilaiKontrak"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Nama"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NPWP"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                string alamatnpwp = rs.Rows[i]["NPWPAlamat1"].ToString() + "<br/>" + rs.Rows[i]["NPWPAlamat2"].ToString() + "<br/>" + rs.Rows[i]["NPWPAlamat3"].ToString();
                c = new TableCell();
                c.Text = alamatnpwp;
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + rs.Rows[i]["Project"].ToString() + "'");
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                decimal byr = 0;
                for (int j = 1; j <= jum; j++)
                {
                    int m = nDari.AddMonths(j - 1).Month;
                    int y = nDari.AddMonths(j - 1).Year;

                    decimal realisasi = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan),0)"
                        + " FROM ISC064_MARKETINGJUAL..MS_PELUNASAN a"
                        + " INNER JOIN MS_TTS b ON a.NoTTS = b.NoTTS"
                        + " WHERE a.NoKontrak = '" + Cf.Pk(rs.Rows[i]["NoKontrak"]) + "'"
                        + " AND MONTH(b.TglTTS) = " + m + " AND YEAR(b.TglTTS) = " + y);

                    c = new TableCell();
                    c.Text = Cf.Num(realisasi);
                    c.HorizontalAlign = HorizontalAlign.Right;
                    c.Wrap = false;
                    r.Cells.Add(c);

                    decimal saldoawal = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan),0)"
                       + " FROM ISC064_MARKETINGJUAL..MS_PELUNASAN a"
                       + " INNER JOIN MS_MEMO b ON a.NoMEMO = b.NoMEMO"
                       + " WHERE b.CaraBayar = 'SA' AND a.NoKontrak = '" + Cf.Pk(rs.Rows[i]["NoKontrak"]) + "'"
                       + " AND MONTH(b.TglMEMO) = " + m + " AND YEAR(b.TgLMEMO) = " + y);

                    c = new TableCell();
                    c.Text = Cf.Num(saldoawal);
                    c.HorizontalAlign = HorizontalAlign.Right;
                    c.Wrap = false;
                    r.Cells.Add(c);

                    decimal memo = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan),0)"
                       + " FROM ISC064_MARKETINGJUAL..MS_PELUNASAN a"
                       + " INNER JOIN MS_MEMO b ON a.NoMEMO = b.NoMEMO"
                       + " WHERE a.NoKontrak = '" + Cf.Pk(rs.Rows[i]["NoKontrak"]) + "'"
                       + " AND MONTH(b.TglMEMO) = " + m + " AND YEAR(b.TglMEMO) = " + y);

                    c = new TableCell();
                    c.Text = Cf.Num(memo);
                    c.HorizontalAlign = HorizontalAlign.Right;
                    c.Wrap = false;
                    r.Cells.Add(c);


                    re[j - 1] += realisasi;
                    reSA[j - 1] += saldoawal;
                    reMO[j - 1] += memo;
                }

                decimal tot = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan),0)"
                    + " FROM ISC064_MARKETINGJUAL..MS_PELUNASAN a"
                    + " INNER JOIN MS_TTS b ON a.NoTTS = b.NoTTS"
                    + " WHERE a.NoKontrak = '" + Cf.Pk(rs.Rows[i]["NoKontrak"]) + "'"
                    + " AND YEAR(b.TglTTS) = " + Convert.ToInt32(Tahun));

                c = new TableCell();
                c.Text = Cf.Num(tot); ;
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                decimal totSA = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan),0)"
                    + " FROM ISC064_MARKETINGJUAL..MS_PELUNASAN a"
                    + " INNER JOIN MS_MEMO b ON a.NoMemo = b.NoMemo"
                    + " WHERE b.CaraBayar='SA' AND a.NoKontrak = '" + Cf.Pk(rs.Rows[i]["NoKontrak"]) + "'"
                    + " AND YEAR(b.TglMEMO) = " + Convert.ToInt32(Tahun));

                c = new TableCell();
                c.Text = Cf.Num(totSA); ;
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                decimal totMO = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan),0)"
                    + " FROM ISC064_MARKETINGJUAL..MS_PELUNASAN a"
                    + " INNER JOIN MS_MEMO b ON a.NoMemo = b.NoMemo"
                    + " WHERE a.NoKontrak = '" + Cf.Pk(rs.Rows[i]["NoKontrak"]) + "'"
                    + " AND YEAR(b.TglMEMO) = " + Convert.ToInt32(Tahun));

                c = new TableCell();
                c.Text = Cf.Num(totMO); ;
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);


                decimal akum = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan),0)"
                    + " FROM ISC064_MARKETINGJUAL..MS_PELUNASAN a"
                    + " INNER JOIN MS_TTS b ON a.NoTTS = b.NoTTS"
                    + " WHERE a.NoKontrak = '" + Cf.Pk(rs.Rows[i]["NoKontrak"]) + "'"
                    + " AND CONVERT(VARCHAR,b.TglTTS,112) <= '" + Cf.Tgl112(nSampai) + "'");

                c = new TableCell();
                c.Text = Cf.Num(akum); ;
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);


                decimal akumSA = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan),0)"
                    + " FROM ISC064_MARKETINGJUAL..MS_PELUNASAN a"
                    + " INNER JOIN MS_MEMO b ON a.NoMEMO = b.NoMEMO"
                    + " WHERE b.CaraBayar = 'SA' AND a.NoKontrak = '" + Cf.Pk(rs.Rows[i]["NoKontrak"]) + "'"
                    + " AND CONVERT(VARCHAR,b.TglMEMO,112) <= '" + Cf.Tgl112(nSampai) + "'");

                c = new TableCell();
                c.Text = Cf.Num(akumSA); ;
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                decimal akumMO = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan),0)"
                    + " FROM ISC064_MARKETINGJUAL..MS_PELUNASAN a"
                    + " INNER JOIN MS_MEMO b ON a.NoMEMO = b.NoMEMO"
                    + " WHERE a.NoKontrak = '" + Cf.Pk(rs.Rows[i]["NoKontrak"]) + "'"
                    + " AND CONVERT(VARCHAR,b.TglMEMO,112) <= '" + Cf.Tgl112(nSampai) + "'");

                c = new TableCell();
                c.Text = Cf.Num(akumMO); ;
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                decimal persen = Math.Round(((akum + akumSA + akumMO) / Convert.ToDecimal(rs.Rows[i]["NilaiKontrak"]) * 100), 2);

                c = new TableCell();
                c.Text = Cf.Num(persen);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                rpt.Rows.Add(r);

                t1 += Convert.ToDecimal(LuasNett);
                t2 += Convert.ToDecimal(Luas);
                t3 += Convert.ToDecimal(rs.Rows[i]["NilaiDPP"]);
                t4 += Convert.ToDecimal(rs.Rows[i]["NilaiKontrak"]);
                t5 += tot;
                t7 += totSA;
                t8 += totMO;
                t6 += akum;
                t9 += akumSA;
                t10 += akumMO;

                if (i == rs.Rows.Count - 1)
                    SubTotal(t1, t2, t3, t4, re, t5, t6, t7, t8, t9, t10, reSA, reMO);
            }
        }

        protected void SubTotal(decimal t1, decimal t2, decimal t3, decimal t4, decimal[] re, decimal t5, decimal t6, decimal t7, decimal t8, decimal t9, decimal t10, decimal[] reSA, decimal[] reMO)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = Rpt.Foot();
            c.Text = "<strong>GRAND TOTAL</strong>";
            c.ColumnSpan = 3;
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
            c.Text = "&nbsp;";
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = "&nbsp;";
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = "&nbsp;";
            c.ColumnSpan = 2;
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            for (int i = 0; i < re.Length; i++)
            {
                c = Rpt.Foot();
                c.Text = Cf.Num(re[i]);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = Rpt.Foot();
                c.Text = Cf.Num(reSA[i]);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = Rpt.Foot();
                c.Text = Cf.Num(reMO[i]);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);
            }

            c = Rpt.Foot();
            c.Text = Cf.Num(t5);
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
            c.Text = Cf.Num(t6);
            c.HorizontalAlign = HorizontalAlign.Right;
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
            c.Text = "&nbsp;";
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

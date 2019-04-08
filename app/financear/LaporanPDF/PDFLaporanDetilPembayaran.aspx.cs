using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.FINANCEAR.Laporan
{
    public partial class LaporanDetilPembayaran : System.Web.UI.Page
    {
        private string UserID { get { return (Request.QueryString["userid"]); } }
        private string AttachmentID { get { return (Request.QueryString["id"]); } }
        private string Project { get { return (Request.QueryString["project"]); } }
        private string Perusahaan { get { return (Request.QueryString["pers"]); } }
        private string Lokasi { get { return (Request.QueryString["lokasi"]); } }

        protected System.Web.UI.WebControls.CheckBoxList tipe;

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

            Header();
            Fill();
        }

        private void Header()
        {
            System.Text.StringBuilder x = new System.Text.StringBuilder();

            Rpt.Judul(x, comp, judul);

            string tgl = "Tanggal Kuitansi";

            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");

            Rpt.SubJudul(x
                , tgl + " : " + Cf.Day(Dari) + " s/d " + Cf.Day(Sampai)
                );
            Rpt.SubJudul(x
                , "Lokasi : " + Lokasi
                );
            Rpt.SubJudul(x
                , "Project : " + Project
                );
            string pers = (Perusahaan == "SEMUA") ? "SEMUA" : Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PERS WHERE Pers ='" + Perusahaan + "'");
            Rpt.SubJudul(x
                , "Perusahaan : " + pers
                );

            string legend = "";
            //Rpt.Header(rpt, x);
            Rpt.HeaderReport(headReport, legend, x);
        }

        private void Fill()
        {
            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");

            string nProject = "";
            if (Project != "SEMUA") nProject = " AND a.Project IN('" + Project.Replace(",", "','") + "')";
            string nPerusahaan = "";
            if (Perusahaan != "SEMUA") nPerusahaan = " AND a.Pers = '" + Perusahaan + "'";
            string nLokasi = (Lokasi == "SEMUA") ? "" : " AND a.Lokasi = '" + Lokasi + "'";

            decimal t1 = 0;
            decimal t2 = 0;
            decimal t3 = 0;
            decimal t4 = 0;
            decimal t5 = 0;
            decimal t6 = 0;
            decimal t7 = 0;
            decimal t8 = 0;
            decimal t9 = 0;
            decimal t10 = 0;

            string tgl = "";
            string tgl2 = "";

            tgl = "a.TglBKM";
            tgl2 = "a.TglMemo";


            string agent = "";
            if (UserAgent() > 0)
                agent = " AND a.NoAgent = " + UserAgent();

            string strSql = "SELECT a.*, b.Nama AS Cust"
                + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK a"
                + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER b ON a.NoCustomer = b.NoCustomer"
                + " WHERE 1=1 "
                + nProject
                + nPerusahaan
                + nLokasi
                + agent
                + " ORDER BY NoKontrak";

            DataTable rs = Db.Rs(strSql);

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                r.VerticalAlign = VerticalAlign.Top;
                r.Attributes["ondblclick"] = "popEditKontrak('" + rs.Rows[i]["NoKontrak"] + "')";

                c = new TableCell();
                c.Text = (i + 1).ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoKontrak"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoUnit"].ToString();
                c.Wrap = false;
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Cust"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                decimal BF = Db.SingleDecimal("SELECT ISNULL(SUM(b.NilaiPelunasan),0) FROM MS_TTS a"
                    + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN b ON a.NoTTS = b.NoTTS"
                    + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN c ON b.NoTagihan = c.NoUrut AND b.NoKontrak = c.NoKontrak"
                    + " WHERE b.NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "'"
                    + " AND CONVERT(VARCHAR, " + tgl + ", 112) >= '" + Cf.Tgl112(Dari) + "'"
                    + " AND CONVERT(VARCHAR, " + tgl + ", 112) <= '" + Cf.Tgl112(Sampai) + "'"
                    + " AND c.Tipe = 'BF'");
                string BFAcc = Db.SingleString("SELECT TOP 1 a.Acc FROM MS_TTS a"
                    + " INNER JOIN ISC064_MARKETINGJUAL..MS_PELUNASAN b ON a.NoTTS = b.NoTTS"
                    + " INNER JOIN ISC064_MARKETINGJUAL..MS_TAGIHAN c ON b.NoTagihan = c.NoUrut AND b.NoKontrak = c.NoKontrak"
                    + " WHERE b.NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "'"
                    + " AND c.Tipe = 'BF'"
                    + " AND CONVERT(VARCHAR, " + tgl + ", 112) >= '" + Cf.Tgl112(Dari) + "'"
                    + " AND CONVERT(VARCHAR, " + tgl + ", 112) <= '" + Cf.Tgl112(Sampai) + "'"
                    + " ORDER BY a.TglBKM DESC");
                string BFNamaBank = Db.SingleString("SELECT Bank FROM REF_ACC WHERE Acc = '" + BFAcc + "'");

                c = new TableCell();
                c.Text = Cf.Num(BF);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = BFNamaBank;
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                decimal DP = Db.SingleDecimal("SELECT ISNULL(SUM(b.NilaiPelunasan),0) FROM MS_TTS a"
                    + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN b ON a.NoTTS = b.NoTTS"
                    + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN c ON b.NoTagihan = c.NoUrut AND b.NoKontrak = c.NoKontrak"
                    + " WHERE b.NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "'"
                    + " AND CONVERT(VARCHAR, " + tgl + ", 112) >= '" + Cf.Tgl112(Dari) + "'"
                    + " AND CONVERT(VARCHAR, " + tgl + ", 112) <= '" + Cf.Tgl112(Sampai) + "'"
                    + " AND c.Tipe = 'DP'");
                string DPAcc = Db.SingleString("SELECT TOP 1 a.Acc FROM MS_TTS a"
                    + " INNER JOIN ISC064_MARKETINGJUAL..MS_PELUNASAN b ON a.NoTTS = b.NoTTS"
                    + " INNER JOIN ISC064_MARKETINGJUAL..MS_TAGIHAN c ON b.NoTagihan = c.NoUrut AND b.NoKontrak = c.NoKontrak"
                    + " WHERE b.NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "'"
                    + " AND c.Tipe = 'DP'"
                    + " AND CONVERT(VARCHAR, " + tgl + ", 112) >= '" + Cf.Tgl112(Dari) + "'"
                    + " AND CONVERT(VARCHAR, " + tgl + ", 112) <= '" + Cf.Tgl112(Sampai) + "'"
                    + " ORDER BY a.TglBKM DESC");
                string DPNamaBank = Db.SingleString("SELECT Bank FROM REF_ACC WHERE Acc = '" + DPAcc + "'");

                c = new TableCell();
                c.Text = Cf.Num(DP);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = DPNamaBank;
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                decimal ANG = Db.SingleDecimal("SELECT ISNULL(SUM(b.NilaiPelunasan),0) FROM MS_TTS a"
                    + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN b ON a.NoTTS = b.NoTTS"
                    + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN c ON b.NoTagihan = c.NoUrut AND b.NoKontrak = c.NoKontrak"
                    + " WHERE b.NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "'"
                    + " AND CONVERT(VARCHAR, " + tgl + ", 112) >= '" + Cf.Tgl112(Dari) + "'"
                    + " AND CONVERT(VARCHAR, " + tgl + ", 112) <= '" + Cf.Tgl112(Sampai) + "'"
                    + " AND c.Tipe = 'ANG'");
                string ANGAcc = Db.SingleString("SELECT TOP 1 a.Acc FROM MS_TTS a"
                    + " INNER JOIN ISC064_MARKETINGJUAL..MS_PELUNASAN b ON a.NoTTS = b.NoTTS"
                    + " INNER JOIN ISC064_MARKETINGJUAL..MS_TAGIHAN c ON b.NoTagihan = c.NoUrut AND b.NoKontrak = c.NoKontrak"
                    + " WHERE b.NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "'"
                    + " AND c.Tipe = 'ANG'"
                    + " AND CONVERT(VARCHAR, " + tgl + ", 112) >= '" + Cf.Tgl112(Dari) + "'"
                    + " AND CONVERT(VARCHAR, " + tgl + ", 112) <= '" + Cf.Tgl112(Sampai) + "'"
                    + " ORDER BY a.TglBKM DESC");
                string ANGNamaBank = Db.SingleString("SELECT Bank FROM REF_ACC WHERE Acc = '" + ANGAcc + "'");


                c = new TableCell();
                c.Text = Cf.Num(ANG);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = ANGNamaBank;
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                decimal ADM = Db.SingleDecimal("SELECT ISNULL(SUM(b.NilaiPelunasan),0) FROM MS_TTS a"
                    + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN b ON a.NoTTS = b.NoTTS"
                    + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN c ON b.NoTagihan = c.NoUrut AND b.NoKontrak = c.NoKontrak"
                    + " WHERE b.NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "'"
                    + " AND CONVERT(VARCHAR, " + tgl + ", 112) >= '" + Cf.Tgl112(Dari) + "'"
                    + " AND CONVERT(VARCHAR, " + tgl + ", 112) <= '" + Cf.Tgl112(Sampai) + "'"
                    + " AND c.Tipe = 'ADM'");
                string ADMAcc = Db.SingleString("SELECT TOP 1 a.Acc FROM MS_TTS a"
                    + " INNER JOIN ISC064_MARKETINGJUAL..MS_PELUNASAN b ON a.NoTTS = b.NoTTS"
                    + " INNER JOIN ISC064_MARKETINGJUAL..MS_TAGIHAN c ON b.NoTagihan = c.NoUrut AND b.NoKontrak = c.NoKontrak"
                    + " WHERE b.NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "'"
                    + " AND c.Tipe = 'ADM' "
                    + " AND CONVERT(VARCHAR, " + tgl + ", 112) >= '" + Cf.Tgl112(Dari) + "'"
                    + " AND CONVERT(VARCHAR, " + tgl + ", 112) <= '" + Cf.Tgl112(Sampai) + "'"
                    + " ORDER BY a.TglBKM DESC");
                string ADMNamaBank = Db.SingleString("SELECT Bank FROM REF_ACC WHERE Acc = '" + ADMAcc + "'");


                c = new TableCell();
                c.Text = Cf.Num(ADM);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = ADMNamaBank;
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                decimal SaldoAwal = Db.SingleDecimal("SELECT ISNULL(SUM(b.NilaiPelunasan),0) FROM MS_MEMO a"
                   + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN b ON a.NoMEMO = b.NoMEMO"
                   + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN c ON b.NoTagihan = c.NoUrut AND b.NoKontrak = c.NoKontrak"
                   + " WHERE b.NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "'"
                   + " AND CONVERT(VARCHAR, " + tgl2 + ", 112) >= '" + Cf.Tgl112(Dari) + "'"
                   + " AND CONVERT(VARCHAR, " + tgl2 + ", 112) <= '" + Cf.Tgl112(Sampai) + "'"
                   + " AND b.CaraBayar='SA'");

                c = new TableCell();
                c.Text = Cf.Num(SaldoAwal);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                decimal MemoBiasa = Db.SingleDecimal("SELECT ISNULL(SUM(b.NilaiPelunasan),0) FROM MS_MEMO a"
                   + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN b ON a.NoMEMO = b.NoMEMO"
                   + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN c ON b.NoTagihan = c.NoUrut AND b.NoKontrak = c.NoKontrak"
                   + " WHERE b.NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "'"
                   + " AND CONVERT(VARCHAR, " + tgl2 + ", 112) >= '" + Cf.Tgl112(Dari) + "'"
                   + " AND CONVERT(VARCHAR, " + tgl2 + ", 112) <= '" + Cf.Tgl112(Sampai) + "'"
                   + " AND b.CaraBayar!='SA'");

                c = new TableCell();
                c.Text = Cf.Num(MemoBiasa);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                decimal Total = Db.SingleDecimal("SELECT ISNULL(SUM(b.NilaiPelunasan),0) FROM MS_TTS a"
                    + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN b ON a.NoTTS = b.NoTTS"
                    + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN c ON b.NoTagihan = c.NoUrut AND b.NoKontrak = c.NoKontrak"
                    + " WHERE b.NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "'"
                    + " AND CONVERT(VARCHAR, " + tgl + ", 112) <= '" + Cf.Tgl112(Sampai) + "'");

                c = new TableCell();
                c.Text = Cf.Num(Total);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);


                decimal TotalSaldoAwal = Db.SingleDecimal("SELECT ISNULL(SUM(b.NilaiPelunasan),0) FROM MS_MEMO a"
                   + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN b ON a.NoMEMO = b.NoMEMO"
                   + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN c ON b.NoTagihan = c.NoUrut AND b.NoKontrak = c.NoKontrak"
                   + " WHERE b.NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "'"
                   + " AND CONVERT(VARCHAR, " + tgl2 + ", 112) <= '" + Cf.Tgl112(Sampai) + "'"
                   + " AND b.CaraBayar='SA'");

                c = new TableCell();
                c.Text = Cf.Num(TotalSaldoAwal);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                decimal TotalMemoBiasa = Db.SingleDecimal("SELECT ISNULL(SUM(b.NilaiPelunasan),0) FROM MS_MEMO a"
                   + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN b ON a.NoMEMO = b.NoMEMO"
                   + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN c ON b.NoTagihan = c.NoUrut AND b.NoKontrak = c.NoKontrak"
                   + " WHERE b.NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "'"
                   + " AND CONVERT(VARCHAR, " + tgl2 + ", 112) <= '" + Cf.Tgl112(Sampai) + "'"
                   + " AND b.CaraBayar!='SA'");

                c = new TableCell();
                c.Text = Cf.Num(TotalMemoBiasa);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                decimal NilaiKontrak = Convert.ToDecimal(rs.Rows[i]["NilaiKontrak"]);

                c = new TableCell();
                c.Text = Cf.Num(NilaiKontrak);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                decimal persentase = 0;

                if (NilaiKontrak != 0)
                {
                    persentase = (Total + TotalSaldoAwal + TotalMemoBiasa) / NilaiKontrak * 100;
                }

                c = new TableCell();
                c.Text = Cf.Num(Math.Round(persentase, 2)) + "%";
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                rpt.Rows.Add(r);

                t1 += BF;
                t2 += DP;
                t3 += ANG;
                t4 += ADM;
                t5 += Total;
                t6 += NilaiKontrak;
                t7 += SaldoAwal;
                t8 += MemoBiasa;
                t9 += TotalSaldoAwal;
                t10 += TotalMemoBiasa;

                if (i == rs.Rows.Count - 1)
                {
                    SubTotal("GRAND TOTAL", t1, t2, t3, t4, t5, t6, t7, t8, t9, t10);
                }
            }
        }

        private void SubTotal(string txt, decimal t1, decimal t2, decimal t3, decimal t4, decimal t5, decimal t6, decimal t7, decimal t8, decimal t9, decimal t10)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = Rpt.Foot();
            c.Text = txt;
            c.ColumnSpan = 4;
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t1);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = "&nbsp;";
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t2);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = "&nbsp;";
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t3);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = "&nbsp;";
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t4);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = "&nbsp;";
            c.HorizontalAlign = HorizontalAlign.Left;
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
            c.Text = Cf.Num(t5);
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
            c.HorizontalAlign = HorizontalAlign.Left;
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

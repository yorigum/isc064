namespace ISC064.LAUNCHING
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Web.UI.WebControls;
    using System.Drawing.Printing;

    public partial class PrintBKMTemplate : System.Web.UI.UserControl
    {
        protected System.Web.UI.WebControls.Label lblblok;
        protected System.Web.UI.WebControls.Label lblunitno;
        protected System.Web.UI.WebControls.Label lblhrg;
        protected System.Web.UI.WebControls.Label cash;
        protected System.Web.UI.WebControls.Label ccard;
        protected System.Web.UI.WebControls.Label noccard;
        protected System.Web.UI.WebControls.Label cekgiro;
        protected System.Web.UI.WebControls.Label nocekgiro;
        protected System.Web.UI.WebControls.Label bankcekgiro;
        protected System.Web.UI.WebControls.Label transfer;
        protected System.Web.UI.WebControls.Label norek;
        protected System.Web.UI.WebControls.Label bankrek;
        protected System.Web.UI.WebControls.Label tgltr;
        protected System.Web.UI.WebControls.Label tglsrt;
        protected System.Web.UI.WebControls.Label jenis;

        //Passing parameter
        public string nomor;
        public string NoTTS
        {
            set { nomor = value; }
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Fill();

            //PrintDocument document = new PrintDocument();
            //document.DefaultPageSettings.PaperSize = new PaperSize("Custom size", (int) (3.75 * 96), (int) (3.75 * 96)); //10cm
            //document.PrinterSettings.Copies = 1;
            //document.Print();
        }

        private void Fill()
        {
            string strSql = "SELECT *"
                + ",CASE CaraBayar"
                + "		WHEN 'TN' THEN 'TUNAI'"
                + "		WHEN 'KK' THEN 'KARTU KREDIT'"
                + "		WHEN 'KD' THEN 'KARTU DEBIT'"
                + "		WHEN 'TR' THEN 'TRANSFER BANK'"
                + "		WHEN 'BG' THEN 'CEK GIRO'"
                + "		WHEN 'UJ' THEN 'UANG JAMINAN'"
                + "		WHEN 'DN' THEN 'DISKON'"
                + " END AS CaraBayar2"
                + " FROM ISC064_FINANCEAR..MS_TTS  WHERE NoTTS = " + nomor;

            DataTable rs = Db.Rs(strSql);
            if (rs.Rows.Count != 0)
            {
                notts.Text = rs.Rows[0]["NoBKM"].ToString().PadLeft(5, '0');
                nama.Text = rs.Rows[0]["Customer"].ToString();
                cb.Text = rs.Rows[0]["CaraBayar2"].ToString();
                if (rs.Rows[0]["CaraBayar2"].ToString() == "TRANSFER BANK")
                    cb.Text += " / " + Db.SingleString("SELECT b.Bank FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS a INNER JOIN " + Mi.DbPrefix + "FINANCEAR..REF_ACC b ON a.Acc = b.Acc WHERE a.NoTTS = " + nomor);
                tglbkm.Text = Cf.Tgl114(Convert.ToDateTime(rs.Rows[0]["TglBKM"]));
                tgljt.Text = Cf.Tgl114(Convert.ToDateTime(rs.Rows[0]["TglTTS"]));

                FillTable(rs.Rows[0]["Tipe"].ToString());

            }
        }

        private void FillTable(string Tipe)
        {

            string nobkm = Db.SingleString("SELECT NoBKM FROM ISC064_FINANCEAR..MS_TTS WHERE NoTTS = " + nomor);
            decimal t = 0;

            string Tb = Sc.MktTb(Tipe);
            string strSql = "SELECT "
                + " NilaiPelunasan AS Nilai"
                + ",CONVERT(VARCHAR,NoTagihan) AS RefTagihan"
                + ",CASE NoTagihan"
                + "		WHEN 0 THEN 'UNALLOCATED'"
                + "		ELSE (SELECT NamaTagihan FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN WHERE NoUrut = l.NoTagihan AND NoKontrak = l.NoKontrak)"
                + " END AS NamaTagihan"
                + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN AS l "
                + " WHERE NoTTS IN (SELECT NoTTS FROM ISC064_FINANCEAR..MS_TTS WHERE NoBKM = '" + nobkm + "')";

            System.Text.StringBuilder x = new System.Text.StringBuilder();

            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                //x.Append(rs.Rows[i]["NamaTagihan"] + " ("+Cf.Num(rs.Rows[i]["Nilai"])+")");
                //if(i >= 0 && (i < rs.Rows.Count - 1))
                //    x.Append(", ");

                x.Append(rs.Rows[i]["NamaTagihan"]);
                if (i >= 0 && (i < rs.Rows.Count - 1))
                    x.Append(", ");

                //namatagihan.Text = x.ToString().ToUpper();

                t = t + Convert.ToDecimal(rs.Rows[i]["Nilai"]);
            }

            //decimal LebihBayar = Db.SingleDecimal("SELECT LebihBayar FROM MS_TTS WHERE NoTTS = '" + nomor + "' ");
            //if (LebihBayar > 0)
            //{
            //    x.Append(", Pembulatan (" + Cf.Num(LebihBayar) + ")");
            //    t = t + LebihBayar;
            //}

            string NoUnit = Db.SingleString("SELECT Unit FROM ISC064_FINANCEAR..MS_TTS WHERE NoTTS = " + nomor);


            //string cetakUnit = jUnit + " " + tUn[1] + " " + noUn; //tUn[2];
            nounit.Text = NoUnit;


            string NoKontrak = Db.SingleString("SELECT Ref FROM ISC064_FINANCEAR..MS_TTS WHERE NoTTS = " + nomor);


            string clus = Db.SingleString("SELECT Lokasi FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
            Cluster.Text = clus;
            nilai.Text = Cf.Num(t);
            terbilang.Text = Money.Str(t) + " RUPIAH";
            sign.Text = Cf.Str(Db.SingleString("SELECT Nama FROM ISC064_SECURITY..REF_SIGN WHERE Dokumen = 'Kwitansi' AND SN = 1"));
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
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion
    }
}

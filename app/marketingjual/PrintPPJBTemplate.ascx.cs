namespace ISC064.MARKETINGJUAL
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Web.UI.WebControls;

    public partial class PrintPPJBTemplate : System.Web.UI.UserControl
    {
        protected System.Web.UI.WebControls.Label lbllantai;
        protected System.Web.UI.WebControls.Label lblnomor;
        protected string Halaman { get { return "PPJB"; } }

        //Passing parameter
        public string nomor;
        public string proj;
        public string NoKontrak
        {
            set { nomor = value; }
        }
        public string Project
        {
            set { proj = value; }
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            //Fill();
            FillHTML();
        }
        private void FillHTML()
        {
            DataTable rsKontrak = Db.Rs("SELECT NoKontrak FROM MS_KONTRAK WHERE NoKontrak = '" + nomor + "'");
            if (rsKontrak.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");

            string Html = He.Template(Halaman, nomor, proj);

            content.InnerHtml = Html;
        }

        //private void Fill()
        //{
        //    DataTable rs = Db.Rs("Select B.KTP1 + ' ' + B.KTP2 + ' ' + B.KTP3 + ' ' + B.KTP4 + ' ' + B.KTP5 As Alamat"
        //        + ", B.Nama, B.NoHp, B.Email, B.NoKTP "
        //        + ", C.NoPPJB, C.TglTTDPPJB "
        //        + ", D.NoUnit, D.Lokasi + ' / ' + D.Jenis As Tipe, D.NamaJalan, D.LuasSG, D.LuasNett, D.Nomor "
        //        + ", A.NamaProject, A.NilaiPPN, A.NilaiKontrak, A.CaraBayar, A.TargetST, A.NoKontrak, A.HargaTanah "
        //        + " From MS_KONTRAk As A "
        //        + " Inner Join MS_CUSTOMER B On B.NoCustomer = A.NoCustomer "
        //        + " Inner Join MS_PPJB C On C.NoKontrak = A.NoKontrak "
        //        + " Inner join MS_UNIT D On D.Nostock = A.NoStock "
        //        + " Where A.NoKontrak = '" + nomor + "'");

        //    if (rs.Rows.Count > 0)
        //    {
        //        var a = rs.Rows[0];

        //        pers.Text = pers_eng.Text = Mi.Pt;
        //        noppjb.Text = noppjb_eng.Text = a["NoPPJB"].ToString();

        //        //Halaman 1 Indo
        //        lbltempatid.Text = lbltempatid2.Text = lbltempatid3.Text = a["NamaProject"].ToString();
        //        lblhariid.Text = Cf.IndoWeek(Convert.ToDateTime(a["TglTTDPPJB"]));
        //        lbltanggalid.Text = Cf.DayIndo(Convert.ToDateTime(a["TglTTDPPJB"]));

        //        //Halaman 1 Eng
        //        lbltempaten.Text = lbltempaten2.Text = lbltempaten3.Text = a["NamaProject"].ToString();
        //        lblharien.Text = Cf.EnWeek(Convert.ToDateTime(a["TglTTDPPJB"]));
        //        lbltanggalen.Text = Cf.DayEng(Convert.ToDateTime(a["TglTTDPPJB"]));

        //        //Halaman 2 Indo & Eng, Halaman 61 TTD
        //        lblnamaid.Text = lblnamaen.Text = lblttdnama.Text = a["Nama"].ToString();
        //        lblnikid.Text = lblniken.Text = a["NoKTP"].ToString();
        //        lblalamatid.Text = lblalamaten.Text = a["Alamat"].ToString();
                
        //        //Halaman 7 Indo & Eng
        //        lbltipeid.Text = lbltipeen.Text = a["Tipe"].ToString();
        //        lbljalanid.Text = lbljalanen.Text = a["NamaJalan"].ToString();
        //        lblnounitid.Text = lblnouniten.Text = a["Nomor"].ToString();
        //        lblluastanahid.Text = lblluastanahen.Text = Cf.NumBulat(a["LuasSG"]) + " M<sup>2</sup>";
        //        lblluasbangunanid.Text = lblluasbangunanen.Text = Cf.NumBulat(a["LuasNett"]) + " M<sup>2</sup>";

        //        //Halaman 8 Indo & Eng
        //        lblhargaid.Text = lblhargaen.Text = Cf.Num(Convert.ToDecimal(a["HargaTanah"]));

        //        //Halaman 11 Indo & Eng
        //        lblhargatanahid.Text = lblhargatanahen.Text = Cf.Num(Convert.ToDecimal(a["HargaTanah"]));
        //        lblppnid.Text = lblppnen.Text = Cf.Num(Convert.ToDecimal(a["NilaiPPN"]));
        //        lbltotalid.Text = lbltotalen.Text = Cf.Num(Convert.ToDecimal(a["NilaiKontrak"]));
        //        lblterbilangid.Text = lblterbilangen.Text = Money.Str(Convert.ToDecimal(rs.Rows[0]["NilaiKontrak"])) + " RUPIAH";
        //        lblcarabayarid.Text = lblcarabayaren.Text = a["CaraBayar"].ToString();

        //        int NoTagihan1 = Db.SingleInteger("SELECT TOP 1 ISNULL(NoUrut, 0) from MS_TAGIHAN where NoKontrak = '" + a["NoKontrak"].ToString() + "' and Tipe != 'BF' and Tipe != 'ADM'");
        //        NoTagihan1 = NoTagihan1 + 1;
        //        decimal BookingFee = Db.SingleDecimal("SELECT TOP 1 ISNULL(NilaiTagihan, 0) from MS_TAGIHAN where NoKontrak = '" + a["NoKontrak"].ToString() + "' and Tipe = 'BF'");
        //        decimal Tagihan1 = Db.SingleDecimal("SELECT TOP 1 ISNULL(NilaiTagihan, 0) from MS_TAGIHAN where NoKontrak = '" + a["NoKontrak"].ToString() + "' and Tipe != 'BF' and Tipe != 'ADM'");
        //        decimal Tagihan2 = Db.SingleDecimal("SELECT TOP 1 ISNULL(NilaiTagihan, 0) from MS_TAGIHAN where NoKontrak = '" + a["NoKontrak"].ToString() + "' and Tipe != 'BF' and Tipe != 'ADM' and NoUrut = '" + NoTagihan1 + "'");
        //        decimal TagihanAkhir = Db.SingleDecimal("SELECT TOP 1 ISNULL(NilaiTagihan, 0) from MS_TAGIHAN where NoKontrak = '" + a["NoKontrak"].ToString() + "' and Tipe != 'BF' and Tipe != 'ADM' ORDER BY NoUrut desc");

        //        lblbfid.Text = lblbfen.Text = Cf.Num(BookingFee);
        //        lbldp1id.Text = lbldp1en.Text = Cf.Num(Tagihan1);
        //        lbldp2id.Text = lbldp2en.Text = Cf.Num(Tagihan2);
        //        lblpelid.Text = lblpelen.Text = Cf.Num(TagihanAkhir);

        //        //Halaman 21 Indo
        //        lbltglserahterimaid.Text = Cf.DayIndo(Convert.ToDateTime(a["TargetST"]));

        //        //Halaman 21 Eng
        //        lblserahterimaen.Text = Cf.DayEng(Convert.ToDateTime(a["TargetST"]));

        //        //Halaman 56 Indo
        //        lblalamat2id.Text = lblalamat2en.Text = a["Alamat"].ToString();
        //        lblemail2id.Text = lblemail2en.Text = a["Email"].ToString();
        //        lbltelepon2id.Text = lbltelepon2en.Text = a["NoHp"].ToString();
        //        lblup2id.Text = lblup2en.Text = a["Nama"].ToString();

        //    }
        //}

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

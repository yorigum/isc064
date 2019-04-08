namespace ISC064.LEGAL
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
        //    DateTime tgl = DateTime.Today;
        //    string strSql = "SELECT *, A.Lokasi AS Locate FROM MS_KONTRAK A INNER JOIN MS_PPJB B ON A.NoKontrak = B.NoKontrak " 
        //        + " INNER JOIN MS_CUSTOMER C ON A.NoCustomer = C.NoCustomer "
        //        + " INNER JOIN MS_UNIT D ON A.NoUnit = D.NoUnit"
        //        + " WHERE A.NoKontrak = '" + nomor + "'";
        //    DataTable rs = Db.Rs(strSql);
        //    if (rs.Rows.Count != 0)
        //    {
        //        //if (Convert.ToDecimal(rs.Rows[0]["Revisi"]) > 0)
        //        //{
        //        //    if (rs.Rows[0]["nokused"].ToString() == "0")
        //        //    {
        //        //        noskpu.Text = rs.Rows[0]["NoKontrak"].ToString() + "&nbsp;" + "REV-" + rs.Rows[0]["Revisi"].ToString();
        //        //    }
        //        //    else
        //        //    {
        //        //        noskpu.Text = rs.Rows[0]["NoKontrakManual"].ToString() + "&nbsp;" + "REV-" + rs.Rows[0]["Revisi"].ToString();
        //        //    }
        //        //}
        //        //else
        //        //{
        //        //    if (rs.Rows[0]["nokused"].ToString() == "0")
        //        //    {
        //        //        noskpu.Text = rs.Rows[0]["NoKontrak"].ToString();
        //        //    }
        //        //    else
        //        //    {
        //        //        noskpu.Text = rs.Rows[0]["NoKontrakManual"].ToString();
        //        //    }
        //        //}
        //        //nounit.Text = rs.Rows[0]["NoUnit"].ToString();
        //        //DateTime bf = Db.SingleTime("SELECT TOP 1 TglJT FROM MS_TAGIHAN WHERE NOKONTRAK='" + rs.Rows[0]["NoKontrak"].ToString() + "' ORDER BY TglJT");
        //        //DateTime tglkontrak = Convert.ToDateTime(rs.Rows[0]["TglKontrak"]);
        //        //tglbf.Text = tglkontrak.ToString("dd") + " " + Cf.Monthname(tglkontrak.Month) + " " + tglkontrak.Year;
        //        //lbltgl.Text = Money.Str(Convert.ToDecimal(Convert.ToDateTime(rs.Rows[0]["TglPPJB"]).Day)).ToLowerInvariant();
        //        //hari.Text = Cf.IndoWeek(Convert.ToDateTime(rs.Rows[0]["TglPPJB"]));
        //        //lblbulan.Text = Cf.Monthname(Convert.ToDateTime(rs.Rows[0]["TglPPJB"]).Month);
        //        //Tgl.Text = Convert.ToDateTime(rs.Rows[0]["TglPPJB"]).Day.ToString();
        //        //bln.Text = Convert.ToDateTime(rs.Rows[0]["TglPPJB"]).Month.ToString();
        //        //thn.Text = Convert.ToDateTime(rs.Rows[0]["TglPPJB"]).Year.ToString();
        //        //terbiltahun.Text = Money.Str(Convert.ToDecimal(Convert.ToDateTime(rs.Rows[0]["TglPPJB"]).Year)).ToLowerInvariant();
        //        //if (rs.Rows[0]["Locate"].ToString() == "AST")
        //        //{
        //        //    tipe.Text = "South";
        //        //}
        //        //else if (rs.Rows[0]["Locate"].ToString() == "ANT")
        //        //{
        //        //    tipe.Text = "North";
        //        //}
        //        //luassg.Text = Cf.Num(rs.Rows[0]["LuasSG"]);
        //        //noktp.Text = noktp2.Text = rs.Rows[0]["NoKTP"].ToString();
        //        //namacs.Text = namacs2.Text = namacs3.Text = namacs4.Text = up.Text = rs.Rows[0]["Nama"].ToString();
        //        //alamat.Text = alamat2.Text = alamat4.Text = rs.Rows[0]["KTP1"].ToString() + "&nbsp;" + rs.Rows[0]["KTP2"].ToString() + "&nbsp;" + rs.Rows[0]["KTP3"].ToString() + "&nbsp;" + rs.Rows[0]["KTP4"].ToString();
        //        //if (rs.Rows[0]["PPJBu"].ToString() == "0")
        //        //{
        //        //    noppjbm.Text = rs.Rows[0]["NoPPJB"].ToString();
        //        //}
        //        //else
        //        //{
        //        //    noppjbm.Text = rs.Rows[0]["NoPPJBm"].ToString();
        //        //}
        //        //kotala.Text = rs.Rows[0]["TempatLahir"].ToString();
        //        //ttl.Text = Convert.ToDateTime(rs.Rows[0]["TglLahir"]).ToString("dd") + " " + Cf.Monthname(Convert.ToDateTime(rs.Rows[0]["TglLahir"]).Month) + " " + Convert.ToDateTime(rs.Rows[0]["TglLahir"]).Year;
        //        //// Cf.Num(rs.Rows[0]["NilaiKontrak"]);
        //        ////Money.Str(Convert.ToDecimal(rs.Rows[0]["NilaiKontrak"]));
        //        //nilaikontrak.Text = harga.Text = Cf.Num(rs.Rows[0]["NilaiDPP"]);
        //        //nkterbil.Text = terbilharga.Text = Money.Str(Convert.ToDecimal(rs.Rows[0]["NilaiDPP"]));
        //        //ppn.Text = Cf.Num(rs.Rows[0]["NilaiPPN"]);
        //        //terbilppn.Text = Money.Str(Convert.ToDecimal(rs.Rows[0]["NilaiPPN"]));
        //        //notelp.Text = rs.Rows[0]["NoTelp"].ToString();
        //        //if (Convert.ToBoolean(rs.Rows[0]["KTPSeumurHidup"]) == false)
        //        //{
        //        //    ktpberlaku.Text = Convert.ToDateTime(rs.Rows[0]["TglKTP"]).ToString("dd") + " " + Cf.Monthname(Convert.ToDateTime(rs.Rows[0]["TglKTP"]).Month) + " " + Convert.ToDateTime(rs.Rows[0]["TglKTP"]).Year;
        //        //}
        //        //else
        //        //{
        //        //    ktpberlaku.Text = "Seumur Hidup";
        //        //}
        //        //string CaraBayar = Db.SingleString("SELECT CaraBayar FROM MS_KONTRAK WHERE NoKONTRAK='" + nomor + "'");
        //                //switch (CaraBayar)
        //                //{
        //                //    case "CASH KERAS": tunai.Text = "&#10003;"; break;
        //                //    case "CASH BERTAHAP": angsuran.Text = "&#10003;"; break;
        //                //    case "KPA": kpa.Text = "&#10003;"; break;

        //                //}
        //        //if (CaraBayar == "CASH KERAS")
        //        //{
        //        //    tunai.Text = "&#10003;";
        //        //    tunai2.Text = " - ";
        //        //    angsuran.Text = " - ";
        //        //    kpa.Text = " - ";
        //        //}
        //        //else if (CaraBayar == "CASH BERTAHAP")
        //        //{
        //        //    tunai.Text = " - ";
        //        //    tunai2.Text = " - ";
        //        //    angsuran.Text = "&#10003;";
        //        //    kpa.Text = " - ";
        //        //}
        //        //else if (CaraBayar == "KPA")
        //        //{
        //        //    tunai.Text = " - ";
        //        //    tunai2.Text = " - ";
        //        //    angsuran.Text = " - ";
        //        //    kpa.Text = "&#10003;";
        //        //}
        //        //DataTable refsign = Db.Rs("SELECT * FROM ISC064_SECURITY..REF_SIGN WHERE Dokumen='PPJB'");
        //        //if (refsign.Rows.Count != 0)
        //        //{
        //        //    ttdppjb.Text = dirut.Text = refsign.Rows[0]["Nama"].ToString();
        //        //    jbttdppjb.Text = refsign.Rows[0]["Jabatan"].ToString();
        //        //}

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

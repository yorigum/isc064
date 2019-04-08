namespace ISC064.MARKETINGJUAL
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Web.UI.WebControls;

    public partial class PrintTTSTemplate2 : System.Web.UI.UserControl
    {

        //Passing parameter
        public string nomor;
        public string pro;

        public string NoTTS
        {
            set { nomor = value; }
        }

        public string Project
        {
            set { pro = value; }
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Fill();
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
                + " FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS WHERE NoTTS = " + nomor;

            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count != 0)
            {
                namacs.Text = Cf.Str(rs.Rows[0]["Customer"]);
                //req nomor panahome (reset perbulan)
                nobkm.Text = rs.Rows[0]["NoTTS2"].ToString();
                tglbkm.Text = Cf.DayIndo(rs.Rows[0]["Tgltts"]);
                nosp.Text = rs.Rows[0]["Ref"].ToString();
                nilainup.Text = Cf.Num(rs.Rows[0]["Total"]);
                terbilangnilainup.Text = Money.Str(Convert.ToDecimal(rs.Rows[0]["Total"])) + " RUPIAH";

                if (rs.Rows[0]["Acc"].ToString() != "-")
                {
                    bankacc.Text = Db.SingleString("select ISNULL(Bank, '') from " + Mi.DbPrefix + "FINANCEAR..REF_ACC where Acc = '" + rs.Rows[0]["Acc"].ToString() + "'") + " (" + rs.Rows[0]["CaraBayar2"].ToString() + ")";
                }
                else
                    bankacc.Text = "Cash";

                tglttd.Text = Cf.DayIndo(rs.Rows[0]["TglTTS"]);
                tglbankacc.Text = Cf.DayIndo(rs.Rows[0]["TglTTS"]);

                decimal PpnNUP = Convert.ToDecimal(rs.Rows[0]["Total"]) / (decimal)1.1;
                decimal DppNUP = Convert.ToDecimal(rs.Rows[0]["Total"]) - PpnNUP;

                dppnup.Text = Cf.NumBulat(PpnNUP); //tebalik kah??
                ppnnup.Text = Cf.NumBulat(DppNUP); //tebalik kah??
                baya.Text = rs.Rows[0]["ket"].ToString();

                //fill data customer
                int CountNUP = Db.SingleInteger("select count(*) from MS_NUP where NoNUP = '" + rs.Rows[0]["NoNUP"] + "'");
                if(CountNUP != 0)
                {
                    string strSqlNUP = "";
                    strSqlNUP = "SELECT "
                        + " b.KTP1"
                        + " ,b.KTP2"
                        + " ,b.KTP3"
                        + " ,b.KTP4"
                        + " FROM MS_NUP a INNER JOIN MS_CUSTOMER b ON a.NoCustomer = b.NoCustomer WHERE a.NoNUP = '" + rs.Rows[0]["NoNUP"] + "'";
                    DataTable rsNUP = Db.Rs(strSqlNUP);
                    for (int j = 0; j < rsNUP.Rows.Count; j++)
                    {
                        alamat1.Text = rsNUP.Rows[j]["KTP1"].ToString();
                        alamat2.Text = rsNUP.Rows[j]["KTP2"].ToString() + " " + rsNUP.Rows[j]["KTP3"].ToString();
                        alamat3.Text = rsNUP.Rows[j]["KTP4"].ToString();
                    }
                }
                else
                {
                    int CountRSV = Db.SingleInteger("select count(*) from MS_RESERVASI where NoReservasi = '" + rs.Rows[0]["NoReservasi"] + "'");
                    if (CountRSV != 0)
                    {
                        string strSqlRsv = "";
                        strSqlRsv = "SELECT "
                            + " b.KTP1"
                            + " ,b.KTP2"
                            + " ,b.KTP3"
                            + " ,b.KTP4"
                            + " FROM MS_RESERVASI a INNER JOIN MS_CUSTOMER b ON a.NoCustomer = b.NoCustomer WHERE a.NoReservasi = '" + rs.Rows[0]["NoReservasi"] + "'";
                        DataTable rsRSV = Db.Rs(strSqlRsv);
                        for (int j = 0; j < rsRSV.Rows.Count; j++)
                        {
                            alamat1.Text = rsRSV.Rows[j]["KTP1"].ToString();
                            alamat2.Text = rsRSV.Rows[j]["KTP2"].ToString() + " " + rsRSV.Rows[j]["KTP3"].ToString();
                            alamat3.Text = rsRSV.Rows[j]["KTP4"].ToString();
                        }
                    }
                    else
                    {
                        string strSqlKon = "";
                        strSqlKon = "SELECT "
                            + " b.KTP1"
                            + " ,b.KTP2"
                            + " ,b.KTP3"
                            + " ,b.KTP4"
                            + " FROM MS_KONTRAK a INNER JOIN MS_CUSTOMER b ON a.NoCustomer = b.NoCustomer WHERE a.NoKontrak = '" + rs.Rows[0]["Ref"] + "'";
                        DataTable rsKon = Db.Rs(strSqlKon);
                        for (int j = 0; j < rsKon.Rows.Count; j++)
                        {
                            alamat1.Text = rsKon.Rows[j]["KTP1"].ToString();
                            alamat2.Text = rsKon.Rows[j]["KTP2"].ToString() + " " + rsKon.Rows[j]["KTP3"].ToString();
                            alamat3.Text = rsKon.Rows[j]["KTP4"].ToString();
                        }
                    }
                }

                //fill data unit
                int CountUnit = Db.SingleInteger("select count(*) from MS_UNIT where NoUnit = '" + rs.Rows[0]["Unit"] + "'");
                if (CountUnit != 0)
                {
                    string strSqlUnit = "";
                    strSqlUnit = "SELECT * "
                        + " FROM MS_UNIT WHERE NoUnit = '" + rs.Rows[0]["Unit"] + "'";
                    DataTable rsUnit = Db.Rs(strSqlUnit);
                    for (int k = 0; k < rsUnit.Rows.Count; k++)
                    {
                        nounit.Text = rsUnit.Rows[k]["Nomor"].ToString();
                        jalan.Text = rsUnit.Rows[k]["NamaJalan"].ToString();
                        cluster.Text = rsUnit.Rows[k]["Lokasi"].ToString();
                    }
                }
                else
                {
                    hide1.Visible = false;
                    hide2.Visible = false;
                    hide3.Visible = false;
                }
            }
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

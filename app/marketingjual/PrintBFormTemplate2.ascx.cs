namespace ISC064.MARKETINGJUAL
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Web.UI.WebControls;

    public partial class PrintBFormTemplate2 : System.Web.UI.UserControl
    {
        //Passing parameter
        public string nomor;
        public string pro;

        public string NoReservasi
        {
            set { nomor = value; }
        }

        public string Project
        {
            set { pro = value; }
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            string strsql = "SELECT * FROM MS_RESERVASI INNER JOIN MS_CUSTOMER ON MS_Reservasi.NoCustomer = MS_CUSTOMER.NoCustomer INNER JOIN MS_UNIT ON MS_RESERVASI.NoStock = MS_UNIT.NoStock WHERE NoReservasi='" + nomor + "'";
            DataTable rs = Db.Rs(strsql);
            if (rs.Rows.Count != 0)
            {
                pers.Text = Mi.Pt;
                namacs.Text = rs.Rows[0]["Nama"].ToString();

                string JenisProperti = rs.Rows[0]["JenisProperti"].ToString();

                string CaraBayar = Db.SingleString("SELECT Jenis FROM REF_SKEMA WHERE Nomor='" + rs.Rows[0]["RefSkema"] + "'");
                
                string ktp = "";
                if (rs.Rows[0]["KTP1"].ToString() != "")
                {
                    ktp += rs.Rows[0]["KTP1"];
                    if (rs.Rows[0]["KTP2"].ToString() != "") ktp += ",&nbsp;";
                }
                if (rs.Rows[0]["KTP2"].ToString() != "")
                {
                    ktp += rs.Rows[0]["KTP2"];
                    if (rs.Rows[0]["KTP3"].ToString() != "") ktp += ",&nbsp;";
                }
                if (rs.Rows[0]["KTP3"].ToString() != "")
                {
                    ktp += rs.Rows[0]["KTP3"];
                    if (rs.Rows[0]["KTP4"].ToString() != "") ktp += ",&nbsp;";
                }
                if (rs.Rows[0]["KTP4"].ToString() != "")
                {
                    ktp += rs.Rows[0]["KTP4"];
                }
                alamatktp.Text = ktp != "" ? ktp : "&nbsp;";
                
                telphpfax.Text = rs.Rows[0]["NoTelp"].ToString() + "/" + rs.Rows[0]["NoHP"].ToString();
                alasan1.Text = rs.Rows[0]["Alasan"].ToString();
                
                string persendiskon = Db.SingleString("SELECT Diskon FROM REF_SKEMA WHERE Nomor='" + rs.Rows[0]["RefSkema"] + "'");
                decimal Gross = Convert.ToDecimal(rs.Rows[0]["Gross"]);
                decimal Diskon = 0;

                decimal HargaSetelahDiskon = Gross - Diskon;
                decimal PPN = HargaSetelahDiskon * (decimal)0.1;

                harganett.Text = Cf.Num(HargaSetelahDiskon);
                decimal BF = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiTagihan),0) FROM MS_RESERVASI_TAGIHAN WHERE NoReservasi='" + nomor + "' AND Tipe='BF'");

                tglres.Text = tgl.Text = Convert.ToDateTime(rs.Rows[0]["Tgl"]).ToString("dd") + " " + Cf.Monthname(Convert.ToDateTime(rs.Rows[0]["Tgl"]).Month) + " " + Convert.ToDateTime(rs.Rows[0]["Tgl"]).Year;
                tgltarget.Text = Convert.ToDateTime(rs.Rows[0]["TglExpire"]).ToString("dd") + " " + Cf.Monthname(Convert.ToDateTime(rs.Rows[0]["TglExpire"]).Month) + " " + Convert.ToDateTime(rs.Rows[0]["TglExpire"]).Year;

                if (rs.Rows[0]["Acc"].ToString() != "-")
                {
                    bankacc.Text = Db.SingleString("select ISNULL(Bank, '') from " + Mi.DbPrefix + "FINANCEAR..REF_ACC where Acc = '" + rs.Rows[0]["Acc"].ToString() + "'");
                }
                else
                {
                    bankacc.Text = "Cash";
                }

                jalan.Text = rs.Rows[0]["NamaJalan"].ToString();
                nounit1.Text = rs.Rows[0]["Nomor"].ToString();
                jenis.Text = rs.Rows[0]["Jenis"].ToString();

                //string strSqlUnit = "";
                //strSqlUnit = "SELECT * FROM MS_UNIT WHERE NoStock = '" + rs.Rows[0]["NoStock"] + "'";
                //DataTable rsNUP = Db.Rs(strSqlUnit);
                //for (int j = 0; j < rsNUP.Rows.Count; j++)
                //{
                //    jalan.Text = "lt";//x[2];
                //    nounit1.Text = "un";//x[1];
                //    jenis.Text = "toew";//x[0];
                //}
            }
        }
    }
}
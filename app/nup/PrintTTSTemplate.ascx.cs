namespace ISC064.NUP
{
	using System;
	using System.Data;
	using System.Data.SqlClient;
	using System.Web.UI.WebControls;

	public partial class PrintTTSTemplate : System.Web.UI.UserControl
	{
		
		//Passing parameter
		public string nomor;
		public string NoTTS
		{
			set{nomor = value;}
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
            site.Visible = false;
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
                + " FROM ISC064_FINANCEAR..MS_TTS WHERE NoTTS = " + nomor;

            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count != 0)
            {
                string Tipe = "";
                if (rs.Rows[0]["NoNUP"].ToString() == "")
                {
                    Tipe = Db.SingleString("Select Jenis From ISC064_MARKETINGJUAL..MS_KONTRAK Where NoKontrak = '" + rs.Rows[0]["Ref"].ToString() + "'");
                }
                else
                {
                    string NoStock = Db.SingleString("SELECT NoStock FROM ISC064_MARKETINGJUAL..MS_NUP_PRIORITY WHERE NoNUP = '" + rs.Rows[0]["NoNUP"].ToString() + "'");
                    Tipe = Db.SingleString("SELECT Jenis FROM ISC064_MARKETINGJUAL..MS_UNIT WHERE NoStock = '" + NoStock + "'");
                }
                string KTP1 = Db.SingleString("Select KTP1 From ISC064_MARKETINGJUAL..MS_CUSTOMER Where Nama = '" + rs.Rows[0]["Customer"].ToString() + "'");
                string KTP2 = Db.SingleString("Select KTP2 From ISC064_MARKETINGJUAL..MS_CUSTOMER Where Nama = '" + rs.Rows[0]["Customer"].ToString() + "'");
                string KTP3 = Db.SingleString("Select KTP3 From ISC064_MARKETINGJUAL..MS_CUSTOMER Where Nama = '" + rs.Rows[0]["Customer"].ToString() + "'");
                string KTP4 = Db.SingleString("Select KTP4 From ISC064_MARKETINGJUAL..MS_CUSTOMER Where Nama = '" + rs.Rows[0]["Customer"].ToString() + "'");
                string Email = Db.SingleString("Select Email From ISC064_MARKETINGJUAL..MS_CUSTOMER Where Nama = '" + rs.Rows[0]["Customer"].ToString() + "'");
                string NoHP = Db.SingleString("Select NoHp From ISC064_MARKETINGJUAL..MS_CUSTOMER Where Nama = '" + rs.Rows[0]["Customer"].ToString() + "'");

                typ.Text = Tipe;
                alamat.Text = KTP1 + "," + KTP2 + "," + KTP3 + "," + KTP4;
                email.Text = Email;
                nohp.Text = NoHP;
                unit.Text = rs.Rows[0]["Unit"].ToString();
                typ.Text = rs.Rows[0]["Jenis"].ToString();
                nokwi.Text = rs.Rows[0]["NoTTS"].ToString().PadLeft(6, '0');
                tglreal.Text = Cf.DayIndo(rs.Rows[0]["TglTTS"]);
                kasir.Text = Db.SingleString("SELECT Nama FROM ISC064_SECURITY..USERNAME WHERE UserID = '" + rs.Rows[0]["UserID"].ToString() + "'");

                tanggal.Text = Cf.DayIndo(rs.Rows[0]["TglTTS"]);

                namacustomer.Text = Cf.Str(rs.Rows[0]["Customer"]);
                total.Text = Money.Str(Convert.ToDecimal(rs.Rows[0]["Total"])) + " RUPIAH";
                nilaitagihan.Text = NTot.Text = Cf.Num(rs.Rows[0]["Total"]);

                if (rs.Rows[0]["Ket"].ToString() == "PEMBAYARAN NUP KEDUA" && rs.Rows[0]["NoNUP"].ToString() != "")
                {
                    NBF.Text = Cf.Num(rs.Rows[0]["Total"]);
                }
                else if (rs.Rows[0]["Ket"].ToString() == "PEMBAYARAN NUP" && rs.Rows[0]["NoNUP"].ToString() != "")
                {
                    NNUP.Text = Cf.Num(rs.Rows[0]["Total"]);
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

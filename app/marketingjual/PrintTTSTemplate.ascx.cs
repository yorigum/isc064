namespace ISC064.MARKETINGJUAL
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
			if(rs.Rows.Count!=0)
			{
				nomorl.Text = rs.Rows[0]["NoTTS"].ToString().PadLeft(7,'0');
				tgl.Text = Cf.Day(rs.Rows[0]["TglTTS"]);

				cs.Text = rs.Rows[0]["Customer"].ToString();
				
				decimal Total = Convert.ToDecimal(rs.Rows[0]["Total"]);
				if((bool)rs.Rows[0]["Pph"]) Total = Total * (decimal)1.1;

				jumlah.Text = Cf.Num(Total);
				terbilang.Text = Money.Str(Total);
				
				carabayar.Text = rs.Rows[0]["CaraBayar2"].ToString();
				if(rs.Rows[0]["CaraBayar"].ToString()=="BG")
				{
					carabayar.Text = carabayar.Text
						+ " / " + rs.Rows[0]["NoBG"] + " Tgl. " + Cf.Day(rs.Rows[0]["TglBG"]);
				}
				if(rs.Rows[0]["Ket"].ToString()!="")
					carabayar.Text = carabayar.Text + ", " + rs.Rows[0]["Ket"].ToString();

				noref.Text = rs.Rows[0]["Ref"].ToString()
					+ " ("+rs.Rows[0]["Unit"]+")";
				
				FillTable(rs.Rows[0]["Tipe"].ToString());
			}
		}

		private void FillTable(string Tipe)
		{
			string Tb = Sc.MktTb(Tipe);
			string strSql = "SELECT "
				+ " NilaiPelunasan AS Nilai"
				+ ",CONVERT(VARCHAR,NoTagihan) AS RefTagihan"
				+ ",CASE NoTagihan"
				+ "		WHEN 0 THEN 'UNALLOCATED'"
				+ "		ELSE (SELECT NamaTagihan FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN WHERE NoUrut = l.NoTagihan AND NoKontrak = l.NoKontrak)"
				+ " END AS NamaTagihan"
				+ " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN AS l "
				+ " WHERE NoTTS = " + nomor;

			System.Text.StringBuilder x = new System.Text.StringBuilder();

			DataTable rs = Db.Rs(strSql);
			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				x.Append(rs.Rows[i]["NamaTagihan"] + " ("+Cf.Num(rs.Rows[i]["Nilai"])+")");
				x.Append("<br>");
			}

			pembayaran.Text = x.ToString();
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

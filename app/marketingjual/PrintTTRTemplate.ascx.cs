namespace ISC064.MARKETINGJUAL
{
	using System;
	using System.Data;
	using System.Data.SqlClient;
	using System.Web.UI.WebControls;

	public partial class PrintTTRTemplate : System.Web.UI.UserControl
	{
		
		
		//Passing parameter
		public string nomor;
		public string NoTTR
		{
			set{nomor = value;}
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			Fill();
		}

		private void Fill()
		{
			//comp.InnerHtml = Mi.Pt;

			string strSql = "SELECT *"
				+ ",CASE CaraBayar"
				+ "		WHEN 'TN' THEN 'TUNAI'"
				+ "		WHEN 'KK' THEN 'KARTU KREDIT'"
				+ "		WHEN 'KD' THEN 'KARTU DEBIT'"
				+ "		WHEN 'TR' THEN 'TRANSFER BANK'"
				+ "		WHEN 'BG' THEN 'CEK GIRO'"
				+ " END AS CaraBayar2"
				+ " FROM MS_TTR WHERE NoTTR = '" + nomor + "'";
			DataTable rs = Db.Rs(strSql);
			if(rs.Rows.Count!=0)
			{
				//nomorl.Text = rs.Rows[0]["NoTTR"].ToString();
				//tgl.Text = Cf.Day(rs.Rows[0]["TglTTR"]);
                tgl2.Text = Cf.Day(DateTime.Now);

				cs.Text = rs.Rows[0]["Customer"].ToString();
				
				decimal Total = Convert.ToDecimal(rs.Rows[0]["Total"]);

				jumlah.Text = Cf.Num(Total);
				terbilang.Text = Money.Str(Total);
				
				carabayar.Text = rs.Rows[0]["CaraBayar2"].ToString();
				if(rs.Rows[0]["CaraBayar"].ToString()=="BG")
				{
					carabayar.Text = carabayar.Text
						+ " / " + rs.Rows[0]["NoBG"] + " Tgl. " + Cf.Day(rs.Rows[0]["TglBG"]);
				}

                ketbayar.Text = rs.Rows[0]["Ket"].ToString();

				noref.Text = rs.Rows[0]["NoReservasi"].ToString()
					+ " (" + rs.Rows[0]["Unit"] + ")";

				unit.Text = rs.Rows[0]["Unit"].ToString();

				//pt.Text = Mi.Pt;
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

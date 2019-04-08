namespace ISC064.MARKETINGJUAL
{
	using System;
	using System.Data;
	using System.Data.SqlClient;
	using System.Web.UI.WebControls;


	public partial class PrintKUPTemplate : System.Web.UI.UserControl
	{

		//Passing parameter
		public string nomor;
		public string NoKontrak
		{
			set{nomor = value;}
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			Fill();
		}
		
		private void Fill()
		{
			string strSql = " SELECT * FROM MS_KONTRAK INNER JOIN MS_CUSTOMER ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
				+ " WHERE NoKontrak = '" + nomor + "'";
			DataTable rs = Db.Rs(strSql);
			if(rs.Rows.Count!=0)
			{
				namacust.Text = rs.Rows[0]["Nama"].ToString();
				alamat.Text = rs.Rows[0]["Alamat1"]
					+ "<br />" + rs.Rows[0]["Alamat2"]
					+ "<br />" + rs.Rows[0]["Alamat3"];
				notelp.Text = rs.Rows[0]["NoTelp"].ToString();
				nohp.Text = rs.Rows[0]["NoHP"].ToString();
				noktp.Text = rs.Rows[0]["NoKTP"].ToString();
				jnsusaha.Text = rs.Rows[0]["JenisBisnis"].ToString();
				sumber.Text = rs.Rows[0]["SumberData"].ToString();
				lokasi.Text = rs.Rows[0]["Lokasi"].ToString();
				unit.Text = rs.Rows[0]["NoUnit"].ToString();
				tipe.Text = Db.SingleString("SELECT Nama FROM REF_JENIS WHERE Jenis = '"+rs.Rows[0]["Jenis"]+"'");
				luas.Text = Cf.Num(rs.Rows[0]["Luas"]);		
				harga.Text = Cf.Num(Convert.ToDecimal(rs.Rows[0]["NilaiKontrak"]));

				DataTable rsLunas = Db.Rs("SELECT TOP 1 *"
					+ ",CASE CaraBayar"
					+ "		WHEN 'TN' THEN 'TUNAI'"
					+ "		WHEN 'KK' THEN 'KARTU KREDIT'"
					+ "		WHEN 'KD' THEN 'KARTU DEBIT'"
					+ "		WHEN 'TR' THEN 'TRANSFER BANK'"
					+ "		WHEN 'BG' THEN 'CEK GIRO'"
					+ "		WHEN 'UJ' THEN 'UANG JAMINAN'"
					+ "		WHEN 'DN' THEN 'DISKON'"
					+ " END AS CaraBayar2"
					+ " FROM MS_PELUNASAN WHERE NoKontrak = '"+nomor+"' AND NoTagihan = 1 AND NilaiPelunasan <> 0");
				if(rsLunas.Rows.Count!=0)
				{
					bf.Text = Cf.Num(Convert.ToDecimal(rsLunas.Rows[0]["NilaiPelunasan"]));
					bf2.Text = Money.Str(Convert.ToDecimal(rsLunas.Rows[0]["NilaiPelunasan"]));
//					carabayar.Text = rsLunas.Rows[0]["CaraBayar2"].ToString()
//						+ " " + rsLunas.Rows[0]["Ket"].ToString();
				}
				DataTable rsag = Db.Rs("SELECT * FROM MS_AGENT WHERE NoAgent = "+Cf.Pk(rs.Rows[0]["NoAgent"]));
				if(rsag.Rows.Count!=0)
				{
					agent.Text = rsag.Rows[0]["Nama"].ToString();
//					agid.Text = rsag.Rows[0]["NoAgent"].ToString().PadLeft(5,'0');
//					agtelp.Text = rsag.Rows[0]["Kontak"].ToString();
//					principal.Text = rsag.Rows[0]["Principal"].ToString();
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

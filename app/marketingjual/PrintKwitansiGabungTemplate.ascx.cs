namespace ISC064.MARKETINGJUAL
{
	using System;
	using System.Data;
	using System.Data.SqlClient;
	using System.Web.UI.WebControls;

	public partial class PrintKwitansiGabungTemplate : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label carabayar;
		protected System.Web.UI.WebControls.Label tts;

		
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
			string strSQL = " SELECT A.NoKwitansiGabung, B.* "
							+ "		FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS B"
							+ "		INNER JOIN MS_KONTRAK A ON A.NoKontrak = B.REF"
							+ "		WHERE B.Status = 'POST' AND B.Ref = '" + nomor + "'"
							+ " ORDER BY B.NoBKM ";

			DataTable rs = Db.Rs(strSQL);
			if(rs.Rows.Count!=0)
			{
				nomorl.Text = rs.Rows[0]["NoKwitansiGabung"].ToString().PadLeft(7,'0');
				tgl.Text = Cf.Day(DateTime.Now);
				cs.Text = rs.Rows[0]["Customer"].ToString();
				
				decimal Total = Convert.ToDecimal(rs.Rows[0]["Total"]);
				if((bool)rs.Rows[0]["Pph"]) Total = Total * (decimal)1.1;
				
				noref.Text = rs.Rows[0]["Ref"].ToString()
					+ " ("+rs.Rows[0]["Unit"]+")";

				FillTable(rs.Rows[0]["Tipe"].ToString());

			}
		}
		private void FillTable(string Tipe)
		{
			decimal t = 0;
			
			string Tb = Sc.MktTb(Tipe);
			string strSql = "SELECT "
				+ " NoBKM, NilaiPelunasan AS Nilai"
				+ ",CONVERT(VARCHAR,NoTagihan) AS RefTagihan"
				+ ",CASE NoTagihan"
				+ "		WHEN 0 THEN 'UNALLOCATED'"
				+ "		ELSE (SELECT NamaTagihan FROM MS_TAGIHAN WHERE NoUrut = l.NoTagihan AND NoKontrak = l.NoKontrak)"
				+ " END AS NamaTagihan"
				+ ",Tipe"
				+ " FROM MS_PELUNASAN AS l "
				+ " INNER JOIN MS_TAGIHAN b ON l.NoTagihan = b.NoUrut AND l.NoKontrak = b.NoKontrak"
				+ " WHERE l.NoKontrak = " + nomor
				;
				
			System.Text.StringBuilder x = new System.Text.StringBuilder();

			DataTable rs = Db.Rs(strSql);
			
			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				string Skema = Db.SingleString("SELECT Skema FROM MS_KONTRAK WHERE NoKontrak = '" + nomor + "'");
				if(Skema.StartsWith("KPR"))
				{
					if(rs.Rows[i]["Tipe"].ToString() != "ANG")
					{
						x.Append(rs.Rows[i]["NamaTagihan"] + "(" +Cf.Num(rs.Rows[i]["Nilai"])+ ")" + "-" + rs.Rows[i]["NoBKM"].ToString().PadLeft(7,'0'));
						x.Append("<br>");
					}
				}
				else
				{
					x.Append(rs.Rows[i]["NamaTagihan"] + " ("+Cf.Num(rs.Rows[i]["Nilai"])+")");
					x.Append("<br>");
				}
				
				
				t = t + Convert.ToDecimal(rs.Rows[i]["Nilai"]);
			}

			pembayaran.Text = x.ToString();

			jumlah.Text = Cf.Num(t);
			terbilang.Text = Money.Str(t);
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

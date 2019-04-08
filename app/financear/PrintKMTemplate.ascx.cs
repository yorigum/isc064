namespace ISC064.FINANCEAR
{
	using System;
	using System.Data;
	using System.Data.SqlClient;
	using System.Web.UI.WebControls;

	public partial class PrintKMTemplate : System.Web.UI.UserControl
	{
		
		//Passing parameter
		public string nomor;
		public string NoVoucher
		{
			set{nomor = value;}
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			Fill();
		}

		private void Fill()
		{
			string strSql = "SELECT * FROM MS_KASMASUK WHERE NoVoucher = " + nomor;
			DataTable rs = Db.Rs(strSql);
			if(rs.Rows.Count!=0)
			{
				nomorl.Text = rs.Rows[0]["NoVoucher"].ToString().PadLeft(5,'0');
				tgl.Text = Cf.Day(rs.Rows[0]["Tgl"]);
				acc.Text = Db.SingleString("SELECT"
					+ " Bank + ' ' + Rekening + ' ('+Acc+')' FROM REF_ACC "
					+ " WHERE Acc = '" + rs.Rows[0]["Acc"] + "'");
				keterangan.Text = rs.Rows[0]["Keterangan"].ToString();
				diterimadari.Text = rs.Rows[0]["DiterimaDari"].ToString();
				nilai.Text = Cf.Num(rs.Rows[0]["Nilai"]);
				terbilang.Text = Money.Str((decimal)rs.Rows[0]["Nilai"]) + " RUPIAH";
				carabayar.Text = rs.Rows[0]["CaraBayar"].ToString();
				alatbayar.Text = rs.Rows[0]["AlatBayar"].ToString();
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

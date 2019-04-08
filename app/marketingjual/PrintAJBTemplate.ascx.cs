namespace ISC064.MARKETINGJUAL
{
	using System;
	using System.Data;
	using System.Data.SqlClient;
	using System.Web.UI.WebControls;

	public partial class PrintAJBTemplate : System.Web.UI.UserControl
	{
		
		//Passing parameter
		public string nomor;
        public string proj;
        public string NoKontrak
		{
			set{nomor = value;}
		}

        public string Project
        {
            set { proj = value; }
        }
        protected void Page_Load(object sender, System.EventArgs e)
		{
			Fill();
		}

		private void Fill()
		{
			string strSql = "SELECT * FROM MS_KONTRAK INNER JOIN MS_CUSTOMER ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
				+ " WHERE NoKontrak = '" + nomor + "'";
			DataTable rs = Db.Rs(strSql);
			if(rs.Rows.Count!=0)
			{
				nomorl.Text = rs.Rows[0]["NoAJB"].ToString();
				tgl.Text = Cf.Day(rs.Rows[0]["TglAJB"]);
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

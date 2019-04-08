namespace ISC064.MARKETINGJUAL
{
	using System;
	using System.Data;
	using System.Data.SqlClient;
	using System.Web.UI.WebControls;

	public partial class PrintWLTemplate : System.Web.UI.UserControl
	{
		
		//Passing parameter
		public string nomor;
		public string NoReservasi
		{
			set{nomor = value;}
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			Fill();
		}

		private void Fill()
		{
			string strSql = "SELECT * FROM MS_RESERVASI WHERE NoReservasi = " + nomor;
			DataTable rs = Db.Rs(strSql);
			if(rs.Rows.Count!=0)
			{	
				nama.Text = Db.SingleString("SELECT Nama FROM MS_CUSTOMER WHERE NoCustomer = " + Cf.Pk(rs.Rows[0]["NoCustomer"]));
				alamat.Text = Db.SingleString("SELECT Alamat1 FROM MS_CUSTOMER WHERE NoCustomer = " + Cf.Pk(rs.Rows[0]["NoCustomer"]))
					+ "<br />"
					+ Db.SingleString("SELECT Alamat2 FROM MS_CUSTOMER WHERE NoCustomer = " + Cf.Pk(rs.Rows[0]["NoCustomer"]))
					+ "<br />"
					+ Db.SingleString("SELECT Alamat3 FROM MS_CUSTOMER WHERE NoCustomer = " + Cf.Pk(rs.Rows[0]["NoCustomer"]))
					;
				telp.Text = Db.SingleString("SELECT NoTelp FROM MS_CUSTOMER WHERE NoCustomer = " + Cf.Pk(rs.Rows[0]["NoCustomer"]));
				
				nounit.Text = rs.Rows[0]["NoUnit"].ToString();
				nourut.Text = rs.Rows[0]["NoUrut"].ToString();
				harga.Text = "Rp. " + Cf.Num(rs.Rows[0]["NilaiReservasi"]);
				terbilang.Text = Money.Str(Convert.ToDecimal(rs.Rows[0]["NilaiReservasi"]));
				carabyr.Text = rs.Rows[0]["Skema"].ToString();
				
				tgl.Text = Convert.ToDateTime(rs.Rows[0]["Tgl"]).ToString("dd")
					+ "&nbsp;"
					+ Cf.Monthname(Convert.ToDateTime(rs.Rows[0]["Tgl"]).Month)
					+ "&nbsp;"
					+ Convert.ToDateTime(rs.Rows[0]["Tgl"]).Year
					;
				
				namacust.Text = Db.SingleString("SELECT Nama FROM MS_CUSTOMER WHERE NoCustomer = " + Cf.Pk(rs.Rows[0]["NoCustomer"]));
				namaagent.Text = Db.SingleString("SELECT Nama FROM MS_AGENT WHERE NoAgent = " + Cf.Pk(rs.Rows[0]["NoAgent"]));
			
				nup.InnerHtml = rs.Rows[0]["NoQueue"].ToString();
				batas.InnerHtml = Cf.Date(rs.Rows[0]["TglExpire"]);
				masuk.InnerHtml = Cf.Date(rs.Rows[0]["TglInput"]);
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

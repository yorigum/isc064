namespace ISC064.MARKETINGJUAL
{
	using System;
	using System.Data;
	using System.Data.SqlClient;
	using System.Web.UI.WebControls;

	public partial class PrintTKOMTemplate : System.Web.UI.UserControl
	{

		//Passing parameter
		public string nomor1;
		public string nomor2;
        public string nomor3;

		public string NoKontrak
		{
			set{ nomor1 = value; }
		}
		public string NoUrut
		{
			set{ nomor2 = value; }
		}

        public string Baris
        {
            set { nomor3 = value; }
        }

		protected void Page_Load(object sender, System.EventArgs e)
		{
			Fill();
		}

		private void Fill()
		{
            string strSql = "SELECT a.NoNota,a.tglBayar,a.NilaiBayar,a.NilaiKomisi,a.PrintTKOM,b.NoCustomer,b.NoUnit,c.NamaPenerima,c.NamaKomisi,b.PersenLunas "
                + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KOMISI_DETAIL a "
                + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK b ON a.NoKontrak = b.NoKontrak"
                + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KOMISI c ON b.NoKontrak = c.NoKontrak"
                + " WHERE a.NoKontrak = '" + nomor1 + "' "
                + " AND a.Baris = '" + nomor2 + "' "
                + " AND a.BarisTermin = '" + nomor3 + "' "
                ;

			DataTable rs = Db.Rs(strSql);
			if(rs.Rows.Count!=0)
			{
                nomorl.Text = Cf.Num(rs.Rows[0]["NoNota"].ToString());
                tglbayar.Text = Cf.Day(rs.Rows[0]["TglBayar"]);

				cs.Text = Db.SingleString("SELECT Nama FROM MS_CUSTOMER WHERE NoCustomer = '" + rs.Rows[0]["NoCustomer"] + "' ");
				nilai.Text = Cf.Num(Math.Round(Convert.ToDecimal(rs.Rows[0]["NilaiBayar"].ToString())));
				nilai2.Text = Cf.Num(Math.Round(Convert.ToDecimal(rs.Rows[0]["NilaiKomisi"].ToString())));
                unit.Text = rs.Rows[0]["NoUnit"].ToString();
                
                agent.Text = rs.Rows[0]["NamaPenerima"].ToString();
				komisi.Text = rs.Rows[0]["NamaKomisi"].ToString();
                persenlunas.Text = Cf.Num(rs.Rows[0]["PersenLunas"]) + " %";
                cetak.Text = rs.Rows[0]["PrintTKOM"].ToString();
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

		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion
	}
}

namespace ISC064.LEGAL
{
	using System;
	using System.Data;
	using System.Data.SqlClient;
	using System.Web.UI.WebControls;

	public partial class PrintBASTTemplate : System.Web.UI.UserControl
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
            string strSql = "SELECT * FROM MS_BAST A INNER JOIN MS_KONTRAK B ON A.NoKontrak = B.NoKontrak "
                + "INNER JOIN MS_CUSTOMER C ON B.NoCustomer = C.NoCustomer"
                + " WHERE A.NoKontrak = '" + nomor + "'";
            DataTable rs = Db.Rs(strSql);
			if(rs.Rows.Count!=0)
			{
                string[] SplitDate = Cf.DaySlash1(rs.Rows[0]["TglST"]).ToString().Split('/');
                string Thns = SplitDate[2];
                string Blns = SplitDate[1];

				nomorl.Text = "BAST-" + Thns + Blns + rs.Rows[0]["NoST"].ToString();
				//tgl.Text = Cf.Day(rs.Rows[0]["TglST"]);
                harist.Text = Cf.IndoWeek(Convert.ToDateTime(rs.Rows[0]["TglST"]));
                tglst.Text = Cf.Day(rs.Rows[0]["TglST"]);

                string[] Times = Cf.Time(rs.Rows[0]["TglST"]).ToString().Split(':');
                string Jam = Times[0];
                string Menit = Times[1];
                jamst.Text = Jam + ":" + Menit;

                Namacs.Text = rs.Rows[0]["Nama"].ToString();
                Alamatcs.Text = rs.Rows[0]["KTP1"].ToString();
                ktpcs.Text = rs.Rows[0]["NoKTP"].ToString();
                
                DataTable r = Db.Rs("SELECT * FROM MS_UNIT WHERE NoUnit = '" + rs.Rows[0]["NoUnit"].ToString() + "'");
                if(r.Rows.Count!=0)
                {
                    nounit1.Text = r.Rows[0]["NoUnit"].ToString();


                    lantai1.Text = r.Rows[0]["Lantai"].ToString();
                    nounit1.Text = r.Rows[0]["Nomor"].ToString();
                    tipe1.Text = r.Rows[0]["Jenis"].ToString();
                    luas1.Text = Cf.Num(r.Rows[0]["LuasSG"]).ToString();
                    semigross1.Text = Cf.Num(r.Rows[0]["LuasNett"]).ToString();
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

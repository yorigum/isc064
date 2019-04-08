using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.SECURIY
{
	public partial class SecLevelDel : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			if(!Page.IsPostBack)
			{
				Js.Focus(this, ket);
				Js.Confirm(this,
					"Apakah anda ingin menghapus security level : "+Kode+" ?\\n"
					+ "Perhatian bahwa data akan dihapus secara PERMANEN."
					);
			}
		}

		protected void delbtn_Click(object sender, System.EventArgs e)
		{
			DataTable rs = Db.Rs("SELECT * FROM SECLEVEL WHERE Kode = '" + Kode + "'");

			if(rs.Rows.Count==0)
				Response.Redirect("/CustomError/Deleted.html");
			else
			{
				DataTable rsDetil = Db.Rs("SELECT PAGE.Modul + ' ' + PAGE.Nama + ' ' + PAGE.Halaman"
					+ " FROM PAGESEC INNER JOIN PAGE ON PAGESEC.Halaman = PAGE.Halaman "
					+ " WHERE Kode = '"+Kode+"' ORDER BY Modul,Nama");

				string Ket = "***Alasan Delete :<br>" + Cf.Str(ket.Text)
					+ "<br><br>***Data Sebelum Delete :<br>"
					+ Cf.LogCapture(rs)
					+ Cf.LogList(rsDetil, "KONFIGURASI SECURITY");

				Db.Execute("EXEC spSecLevelDel '" + Kode + "'");

				int c = Db.SingleInteger(
					"SELECT COUNT(*) FROM SECLEVEL WHERE Kode = '" + Kode + "'");

				if(c==0)
				{
					//Log
					Db.Execute("EXEC spLogSeclevel "
						+ " 'DEL'"
						+ ",'" + Act.UserID + "'"
						+ ",'" + Act.IP + "'"
						+ ",'" + Ket + "'"
						+ ",'" + Kode + "'"
						);

					Js.Close(this);
				}
				else
				{
					//Tidak bisa dihapus
					frm.Visible = false;
					nodel.Visible = true;
				}
			}
		}

		private string Kode
		{
			get
			{
				return Cf.Pk(Request.QueryString["Kode"]);
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion
	}
}

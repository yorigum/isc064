using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.ADMINJUAL
{
	public partial class SkomDelBaris : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			Act.CekInt("Nomor");
			Act.CekInt("Baris");

			DataTable rs = Db.Rs(
				"SELECT * FROM REF_SKOM_DETAIL WHERE Nomor = " + Nomor + " AND Baris = " + Baris);

			if(rs.Rows.Count==0)
				Response.Redirect("/CustomError/Deleted.html");
			else
			{
                //DataTable rsHeader = Db.Rs("SELECT "
                //    + " Nomor"
                //    + ",Nama"
                //    + ",Status"
                //    + " FROM REF_SKOM "
                //    + " WHERE Nomor = " + Nomor);

                //DataTable rsBef = Db.Rs("SELECT "
                //    + " CONVERT(VARCHAR, Baris) "
                //    + " + '.  ('+Tipe+') ' + Nama + '  ' "
                //    + " + TipeNominal + CONVERT(VARCHAR, Nominal, 1) + '  ' "
                //    + " + TipeJadwal + '(' + CONVERT(VARCHAR, IntJadwal) + ')' + "
                //    + " ISNULL(CONVERT(VARCHAR, TglFix, 106), 'NULL') + '  ' "
                //    + " + 'TERM:' + CONVERT(VARCHAR, TermCair, 1)"
                //    + " FROM REF_SKOM_DETAIL WHERE Nomor = " + Nomor);
				
				Db.Execute("EXEC spSkomDelBaris "
					+ Nomor + "," + Baris
					);

                //DataTable rsAft = Db.Rs("SELECT "
                //    + " CONVERT(VARCHAR, Baris) "
                //    + " + '.  ('+Tipe+') ' + Nama + '  ' "
                //    + " + TipeNominal + CONVERT(VARCHAR, Nominal, 1) + '  ' "
                //    + " + TipeJadwal + '(' + CONVERT(VARCHAR, IntJadwal) + ')' + "
                //    + " ISNULL(CONVERT(VARCHAR, TglFix, 106), 'NULL') + '  ' "
                //    + " + 'TERM:' + CONVERT(VARCHAR, TermCair, 1)"
                //    + " FROM REF_SKOM_DETAIL WHERE Nomor = " + Nomor);

                //string Ket = Cf.LogCapture(rsHeader)
                //    + "<br>---DELETE RUMUS---<br>"
                //    + Cf.LogList(rsBef, rsAft, "RUMUS");

                //Db.Execute("EXEC spLogSkom"
                //    + " 'EDIT'"
                //    + ",'" + Act.UserID + "'"
                //    + ",'" + Act.IP + "'"
                //    + ",'" + Ket + "'"
                //    + ",'" + Nomor.PadLeft(3,'0') + "'"
                //    );

				Response.Redirect("SkomEdit.aspx?Nomor=" + Nomor + "&done=1");
			}
		}

		private string Nomor
		{
			get
			{
				return Cf.Pk(Request.QueryString["Nomor"]);
			}
		}

		private string Baris
		{
			get
			{
				return Cf.Pk(Request.QueryString["Baris"]);
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

using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
	public partial class KomisiSolve : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			string strSql = "SELECT * FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'";
			DataTable rs = Db.Rs(strSql);

			if(rs.Rows.Count == 0)
				Response.Redirect("/CustomError/Deleted.html");
			else
			{
				DataTable rsBef = Db.Rs("SELECT "
					+ " NoKontrak AS [No. Kontrak]"
					+ ",CONVERT(varchar,TglKontrak,106) AS [Tanggal Kontrak]"
					+ ",MS_AGENT.Nama + ' ' + MS_AGENT.Principal AS [Agent]"
					+ ",CONVERT(varchar,MS_KONTRAK.TargetST,106) AS [Jadwal Serah Terima]"
					+ ",FlagKomisi AS [Flag Komisi]"
					+ " FROM MS_KONTRAK INNER JOIN MS_AGENT ON MS_KONTRAK.NoAgent = MS_AGENT.NoAgent"
					+ " WHERE NoKontrak = '" + NoKontrak + "'"
					);

				Db.Execute("UPDATE MS_KONTRAK SET FlagKomisi = 1 WHERE NoKontrak = '" + NoKontrak + "'");

				DataTable rsAft = Db.Rs("SELECT "
					+ " NoKontrak AS [No. Kontrak]"
					+ ",CONVERT(varchar,TglKontrak,106) AS [Tanggal Kontrak]"
					+ ",MS_AGENT.Nama + ' ' + MS_AGENT.Principal AS [Agent]"
					+ ",CONVERT(varchar,MS_KONTRAK.TargetST,106) AS [Jadwal Serah Terima]"
					+ ",FlagKomisi AS [Flag Komisi]"
					+ " FROM MS_KONTRAK INNER JOIN MS_AGENT ON MS_KONTRAK.NoAgent = MS_AGENT.NoAgent"
					+ " WHERE NoKontrak = '" + NoKontrak + "'"
					);

				//Logfile
				string Ket = Cf.LogCompare(rsBef,rsAft);

				Db.Execute("EXEC spLogKontrak"
					+ " 'SK'"
					+ ",'" + Act.UserID + "'"
					+ ",'" + Act.IP + "'"
					+ ",'" + Ket + "'"
					+ ",'" + NoKontrak + "'"
					);

				Response.Redirect("KontrakJadwalKomisi.aspx?NoKontrak=" + NoKontrak + "&done=3");
			}
		}

		private string NoKontrak
		{
			get
			{
				return Request.QueryString["NoKontrak"];
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

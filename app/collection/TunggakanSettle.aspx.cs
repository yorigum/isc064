using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.COLLECTION
{
	public partial class TunggakanSettle : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			Act.CekInt("NoTunggakan");

			DataTable rs = Db.Rs("SELECT * FROM MS_TUNGGAKAN WHERE NoTunggakan = " + NoTunggakan);

			if(rs.Rows.Count==0)
				Response.Redirect("/CustomError/Deleted.html");
			else
			{
				DataTable rsHeader = Db.Rs("SELECT "
					+ " CONVERT(varchar, TglTunggakan, 106) AS [Tanggal]"
					+ ",Tipe"
					+ ",Ref AS [Ref.]"
					+ ",Unit"
					+ ",Customer"
					+ ",Total"
					+ ",LevelTunggakan AS [Level]"
					+ " FROM MS_TUNGGAKAN WHERE NoTunggakan = " + NoTunggakan);

				string StatusLama = rs.Rows[0]["Status"].ToString();
				
				Db.Execute("EXEC spTunggakanSettle " + NoTunggakan);

				string StatusBaru = Db.SingleString(
					"SELECT Status FROM MS_Tunggakan WHERE NoTunggakan = " + NoTunggakan);

				if(StatusLama!="S" && StatusBaru=="S")
				{
					Db.Execute("EXEC spLogTunggakan"
						+ " 'SETTLE'"
						+ ",'" + Act.UserID + "'"
						+ ",'" + Act.IP + "'"
						+ ",'" + Cf.LogCapture(rsHeader) + "'"
						+ ",'" + NoTunggakan.ToString().PadLeft(7,'0') + "'"
						);

                    decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_TUNGGAKAN_LOG ORDER BY LogID DESC");
                    string Project = Db.SingleString("SELECT Project FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = (SELECT Ref FROM MS_TUNGGAKAN WHERE NoTunggakan = '" + NoTunggakan + "') ");
                    Db.Execute("UPDATE MS_TUNGGAKAN_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                    Response.Redirect("TunggakanEdit.aspx?NoTunggakan="+NoTunggakan+"&done=1");
				}
				else
				{
					//Tidak bisa dihapus
					nodel.Visible = true;
				}
			}
		}

		private string NoTunggakan
		{
			get
			{
				return Cf.Pk(Request.QueryString["NoTunggakan"]);
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

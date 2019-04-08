using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
	public partial class TTRVoid : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			DataTable rs = Db.Rs("SELECT * FROM MS_TTR WHERE NoTTR = '" + NoTTR + "'");

			if(rs.Rows.Count==0)
				Response.Redirect("/CustomError/Deleted.html");
			else
			{
				DataTable rsHeader = Db.Rs("SELECT "
					+ " CONVERT(varchar, TglTTR, 106) AS [Tanggal]"
					+ ",NoReservasi AS [No. Reservasi]"
					+ ",Unit"
					+ ",Customer"
					+ ",CaraBayar AS [Cara Bayar]"
					+ ",Ket AS [Keterangan]"
					+ ",NoBG AS [No. BG]"
					+ ",CONVERT(varchar, TglBG, 106) AS [Tanggal BG]"
					+ ",Total"
					+ " FROM MS_TTR WHERE NoTTR = '" + NoTTR + "'");

				string StatusLama = rs.Rows[0]["Status"].ToString();
				decimal NilaiKembali = Convert.ToDecimal(rs.Rows[0]["Total"]);

				Db.Execute("EXEC spTTRVoid '" + NoTTR + "'");

				string StatusBaru = Db.SingleString(
					"SELECT Status FROM MS_TTR WHERE NoTTR = '" + NoTTR + "'");

				if(StatusLama!="VOID" && StatusBaru=="VOID")
				{
					string logr = "";
					if(Request.QueryString["r"]!=null)
					{
						logr = "REIMBURSE<br>";
						Db.Execute("UPDATE MS_TTR SET NilaiKembali = " + NilaiKembali + " WHERE NoTTR = '" + NoTTR + "'");
					}

					//Log
					string KetLog = logr
						+ Cf.LogCapture(rsHeader)
						;

					Db.Execute("EXEC spLogTTR"
						+ " 'VOID'"
						+ ",'" + Act.UserID + "'"
						+ ",'" + Act.IP + "'"
						+ ",'" + KetLog + "'"
						+ ",'" + NoTTR + "'"
						);

                    decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_TTR_LOG ORDER BY LogID DESC");
                    string Project = Db.SingleString("SELECT Project FROM MS_UNIT WHERE NoUnit = (SELECT NoUnit FROM MS_TTR WHERE NoTTR = '" + NoTTR + "')");
                    Db.Execute("UPDATE MS_TTR_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                    Response.Redirect("TTREdit.aspx?NoTTR=" + NoTTR + "&done=1");
				}
				else
				{
					//Tidak bisa dihapus
					nodel.Visible = true;
				}
			}
		}

		private string NoTTR
		{
			get
			{
				return Cf.Pk(Request.QueryString["NoTTR"]);
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

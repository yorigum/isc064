using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
	public partial class ReservasiDel : System.Web.UI.Page
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			Act.CekInt("NoReservasi");

			if(!Page.IsPostBack)
			{
				Js.Focus(this, ket);
				Js.Confirm(this,
					"Apakah anda ingin menghapus reservasi : "+NoReservasi+" ?\\n"
					+ "Perhatian bahwa data akan dihapus secara PERMANEN."
					);
			}
		}

		protected void delbtn_Click(object sender, System.EventArgs e)
		{
			DataTable rs = Db.Rs(
				"SELECT * FROM MS_RESERVASI WHERE NoReservasi = " + NoReservasi);

			if(rs.Rows.Count==0)
				Response.Redirect("/CustomError/Deleted.html");
			else
			{
				string Ket = "***Alasan Delete :<br>" + Cf.Str(ket.Text)
					+ "<br><br>***Data Sebelum Delete :<br>"
					+ Cf.LogCapture(rs);

				Db.Execute("EXEC spReservasiDel " + NoReservasi);

				int c = Db.SingleInteger(
					"SELECT COUNT(*) FROM MS_RESERVASI WHERE NoReservasi = " + NoReservasi);

				if(c==0)
				{
					//Log
					Db.Execute("EXEC spLogReservasi "
						+ " 'DELETE'"
						+ ",'" + Act.UserID + "'"
						+ ",'" + Act.IP + "'"
						+ ",'" + Ket + "'"
						+ ",'" + NoReservasi.ToString().PadLeft(5,'0') + "'"
						);

                    decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_RESERVASI_LOG ORDER BY LogID DESC");
                    string Project = Db.SingleString("SELECT Project FROM MS_UNIT WHERE NoStock = (SELECT NoStock FROM MS_RESERVASI WHERE NoReservasi = '" + NoReservasi + "')");
                    Db.Execute("UPDATE MS_RESERVASI_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

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

		private string NoReservasi
		{
			get
			{
				return Cf.Pk(Request.QueryString["NoReservasi"]);
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

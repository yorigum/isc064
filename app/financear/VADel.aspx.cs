using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.FINANCEAR {
	public partial class VADel : System.Web.UI.Page {

		protected void Page_Load(object sender, System.EventArgs e) {
			Act.Pass();
			Act.NoCache();

			if (!Page.IsPostBack) {
				Js.Focus(this, ket);
				Js.Confirm(this,
					"Apakah anda ingin menghapus virtual account : " + NoVA + " ?\\n"
					+ "Perhatian bahwa data akan dihapus secara PERMANEN."
					);
			}
		}

		protected void delbtn_Click(object sender, System.EventArgs e) {
			DataTable rs = Db.Rs("SELECT * FROM REF_VA WHERE NoVA = '" + NoVA + "'");

			if (rs.Rows.Count == 0)
				Response.Redirect("/CustomError/Deleted.html");
			else {
				string Ket = "***Alasan Delete :<br>" + Cf.Str(ket.Text)
					+ "<br><br>***Data Sebelum Delete :<br>"
					+ Cf.LogCapture(rs);

				Db.Execute("EXEC spVADel '" + NoVA + "'");

				int c = Db.SingleInteger(
					"SELECT COUNT(*) FROM REF_VA WHERE NoVA = '" + NoVA + "'");

				if (c == 0) {
					//Log
					Db.Execute("EXEC spLogVA "
						+ " 'DELETE'"
						+ ",'" + Act.UserID + "'"
						+ ",'" + Act.IP + "'"
						+ ",'" + Ket + "'"
						+ ",'" + NoVA + "'"
						);

					Js.Close(this);
				}
				else {
					//Tidak bisa dihapus
					frm.Visible = false;
					nodel.Visible = true;
				}
			}
		}

		private string NoVA {
			get {
				return Cf.Pk(Request.QueryString["NoVA"]);
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e) {
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
		private void InitializeComponent() {

		}
		#endregion
	}
}

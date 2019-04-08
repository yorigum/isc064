using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.FINANCEAR {
	public partial class VAEdit : System.Web.UI.Page {

		protected void Page_Load(object sender, System.EventArgs e) {
			Act.Pass();
			Act.NoCache();

			if (!Act.Sec("ED:" + Request.PhysicalPath)) {
				ok.Enabled = false;
				save.Enabled = false;
			}

			if (!Page.IsPostBack) {
				Fill();
			}

			FeedBack();
		}

		private void FeedBack() {
			feed.Text = "";
			if (!Page.IsPostBack) {
				if (Request.QueryString["done"] != null)
					feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
						+ "Edit Berhasil...";
			}
		}

		private void Fill() {
			btnlog.Attributes["onclick"] = "location.href='LogPk.aspx?Tb=REF_VA_LOG&Pk=" + NoVA + "'";
			btndel.Attributes["onclick"] = "location.href='VADel.aspx?NoVA=" + NoVA + "'";

			DataTable rs = Db.Rs("SELECT * FROM REF_VA WHERE NoVA = '" + NoVA + "'");
			if (rs.Rows.Count == 0)
				Response.Redirect("/CustomError/Deleted.html");
			else {
				bank.Text = rs.Rows[0]["Bank"].ToString();
			}
		}
		private bool valid() {
			bool x = true;
			string s = "";

			//acc
			if (Cf.isEmpty(bank)) {
				x = false;
				if (s == "") s = bank.ID;
				bankc.Text = "Kosong";
			}
			else
				bankc.Text = "";

			if (!x) {
				Js.Alert(
					this
					, "Input Tidak Valid.\\n\\n"
					+ "Aturan Proses :\\n"
					+ "1. Bank tidak boleh kosong.\\n"
					, "document.getElementById('" + s + "').focus();"
					+ "document.getElementById('" + s + "').select();"
					);
			}

			return x;
		}

		private bool Save() {
			if (valid()) {
				string Bank = Cf.Str(bank.Text);

				DataTable rsBef = Db.Rs("SELECT "
					+ " NoVA AS [No. VA]"
					+ ",Bank"
					+ " FROM REF_VA"
					+ " WHERE NoVA = '" + NoVA + "'");

				Db.Execute("UPDATE REF_VA"
					+ " SET Bank = '" + Cf.Str(bank.Text) + "'"
					+ " WHERE NoVA = '" + NoVA + "'"
					);

				DataTable rsAft = Db.Rs("SELECT "
					+ " NoVA AS [No. VA]"
					+ ",Bank"
					+ " FROM REF_VA"
					+ " WHERE NoVA = '" + NoVA + "'");

				string KetLog = Cf.LogCompare(rsBef, rsAft);

				Db.Execute("EXEC spLogVA"
					+ " 'EDIT'"
					+ ",'" + Act.UserID + "'"
					+ ",'" + Act.IP + "'"
					+ ",'" + KetLog + "'"
					+ ",'" + NoVA + "'"
					);

				return true;
			}
			else
				return false;
		}

		protected void ok_Click(object sender, System.EventArgs e) {
			if (Save()) Js.Close(this);
		}

		protected void save_Click(object sender, System.EventArgs e) {
			if (Save()) Response.Redirect("VAEdit.aspx?NoVA=" + NoVA + "&done=1");
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

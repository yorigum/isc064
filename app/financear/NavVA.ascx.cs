namespace ISC064.FINANCEAR {
	using System;
	using System.Drawing;
	using System.Data;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	public partial class NavVA : System.Web.UI.UserControl {

		public string aktif;
		public string Aktif {
			set { aktif = value; }
		}

		protected void Page_Load(object sender, System.EventArgs e) {
			if (!Page.IsPostBack) {
				edit.HRef = "VAEdit.aspx?NoTTS=" + NoVA;

				switch (aktif) {
					case "1":
						div1.Attributes["class"] = "tabaktif";
						break;
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
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {

		}
		#endregion
	}
}

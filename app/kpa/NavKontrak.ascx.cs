namespace ISC064.KPA
{
	using System;
	using System.Drawing;
	using System.Data;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	public partial class NavKontrak : System.Web.UI.UserControl
	{

        public string aktif;
        public string Aktif
        {
            set { aktif = value; }
        }

        protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!Page.IsPostBack)
			{
                detil.HRef = "KontrakProses.aspx?NoKontrak=" + NoKontrak;
                tagihan.HRef = "KontrakJadwalTagihan.aspx?NoKontrak=" + NoKontrak;
                switch (aktif)
                {
                    case "1":
                        detilkpa.Attributes["class"] = "tabaktif";
                        break;
                    case "2":
                        tagihankpa.Attributes["class"] = "tabaktif";
                        tagihankpa.Attributes["style"] = "left:123";
                        break;
                }
            }
		}

		private string NoKontrak
		{
			get
			{
				return Cf.Pk(Request.QueryString["NoKontrak"]);
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

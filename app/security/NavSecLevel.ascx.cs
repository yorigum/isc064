namespace ISC064.SECURITY
{
	using System;
	using System.Drawing;
	using System.Data;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	public partial class NavSecLevel : System.Web.UI.UserControl
	{
		
		public string aktif;
		public string Aktif
		{
			set{aktif = value;}
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!Page.IsPostBack)
			{
				edit.HRef = "SecLevelEdit.aspx?Kode="+Kode;
				config.HRef = "SecLevelConfig.aspx?Kode="+Kode;
				akses.HRef = "SecLevelAkses.aspx?Kode="+Kode;

				switch(aktif)
				{
					case "1":
						div1.Attributes["class"] = "tabaktif";
						break;
					case "2":
						div2.Attributes["class"] = "tabaktif";
						div2.Attributes["style"] = "left:123";
						break;
					case "3":
						div3.Attributes["class"] = "tabaktif";
						div3.Attributes["style"] = "left:243";
						break;
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
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion
	}
}

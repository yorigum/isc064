namespace ISC064.COLLLECTION
{
	using System;
	using System.Drawing;
	using System.Data;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	public partial class NavCIF : System.Web.UI.UserControl
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
				tagihan.HRef = "CustomerInfo.aspx?Tipe="+Tipe+"&Ref="+Ref;
				pjt.HRef = "CustomerPJT.aspx?Tipe="+Tipe+"&Ref="+Ref;
				tunggakan.HRef = "CustomerTunggakan.aspx?Tipe="+Tipe+"&Ref="+Ref;

				switch(aktif)
				{
					case "1":
						div1.Attributes["class"] = "tabaktif";
						break;
					case "2":
						div2.Attributes["class"] = "tabaktif";
						div2.Attributes["style"] = "left:123; width:120px;";
						break;
					case "3":
						div3.Attributes["class"] = "tabaktif";
						div3.Attributes["style"] = "left:243; width:120px;";
						break;
				}
			}
		}

		private string Tipe
		{
			get
			{
				return Cf.Pk(Request.QueryString["Tipe"]);
			}
		}

		private string Ref
		{
			get
			{
				return Cf.Pk(Request.QueryString["Ref"]);
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

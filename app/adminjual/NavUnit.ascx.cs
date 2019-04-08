namespace ISC064.ADMINJUAL
{
	using System;
	using System.Drawing;
	using System.Data;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	public partial class NavUnit : System.Web.UI.UserControl
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
				edit.HRef = "UnitEdit.aspx?NoStock="+NoStock;
				spek.HRef = "UnitEditSpek.aspx?NoStock="+NoStock;
				histori.HRef = "UnitHistori.aspx?NoStock="+NoStock;
                //diskon.HRef = "UnitDiskon.aspx?NoStock=" + NoStock;

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
                    //case "4":
                    //    div4.Attributes["class"] = "tabaktif";
                    //    div4.Attributes["style"] = "left:363; width:120px;";
                    //    break;
				}
			}
		}

		private string NoStock
		{
			get
			{
				return Cf.Pk(Request.QueryString["NoStock"]);
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

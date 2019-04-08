namespace ISC064.SETTINGS
{
	using System;
	using System.Drawing;
	using System.Data;
	using System.Web.UI.WebControls;


	public partial class NavAgent : System.Web.UI.UserControl
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
				edit.HRef = "TipeSalesEdit.aspx?NoTipe="+ NoTipe;
				switch(aktif)
				{
					case "1":
						div1.Attributes["class"] = "tabaktif";
						break;
				}
			}
		}

		private string NoTipe
		{
			get
			{
				return Cf.Pk(Request.QueryString["NoTipe"]);
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

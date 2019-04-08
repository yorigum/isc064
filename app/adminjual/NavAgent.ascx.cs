namespace ISC064.ADMINJUAL
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
				edit.HRef = "AgentEdit.aspx?NoAgent="+NoAgent;
				histori.HRef = "AgentHistori.aspx?NoAgent="+NoAgent;
				foto.HRef = "AgentFoto.aspx?NoAgent="+NoAgent;
                komisigen.HRef = "AgentKomisiGen.aspx?NoAgent=" + NoAgent;
                jadkomisi.HRef = "AgentJadwalKomisi.aspx?NoAgent=" + NoAgent;

				switch(aktif)
				{
					case "1":
						div1.Attributes["class"] = "tabaktif";
						break;
					case "2":
						div2.Attributes["class"] = "tabaktif";
						div2.Attributes["style"] = "left:123; width:120px";
						break;
					case "3":
						div3.Attributes["class"] = "tabaktif";
						div3.Attributes["style"] = "left:243;";
						break;
                    case "4":
                        div4.Attributes["class"] = "tabaktif";
                        div4.Attributes["style"] = "left:363";
                        break;
                    case "5":
                        div5.Attributes["class"] = "tabaktif";
                        div5.Attributes["style"] = "left:483";
                        break;

				}
			}
		}

		private string NoAgent
		{
			get
			{
				return Cf.Pk(Request.QueryString["NoAgent"]);
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

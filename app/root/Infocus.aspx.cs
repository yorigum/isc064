using System;
using System.Drawing;
using System.IO;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064
{
	public partial class Infocus : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["fpindex"]==null)
				Session["fpindex"]="1";

			fp.ImageUrl = "/marketingjual/_img/"+Convert.ToInt32(Session["fpindex"]).ToString().PadLeft(2,'0')+".jpg";

			Session["fpindex"] = Convert.ToInt32(Session["fpindex"]) + 1;
			if(Session["fpindex"].ToString()=="22") Session["fpindex"] = "1";
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

using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064
{
	public partial class SignOut : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			Db.Execute("EXEC spAppSignOut "
				+ " '" + Act.UserID + "'"
				+ ",'" + Act.IP + "'"
				);


            Session.Abandon();

			if(Request.QueryString["close"]==null)
			{
				if(Request.QueryString["pass"]=="1")
					//Dari halaman ganti password : salah 3x
					Response.Redirect("/CustomError/Restricted.html");
				else
					Response.Redirect("/");
			}
			else  //close browser
				Js.Close(this);
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

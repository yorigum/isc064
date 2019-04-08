using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LAUNCHING
{
	public partial class _Default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.NoCache();

//			if(!Mi.Licensed())
//				Response.Redirect("/CustomError/Licensed.html");
//
//			if(Request.QueryString["SID"]==null||Request.QueryString["UserID"]==null)
//				Response.Redirect("/Gateway.aspx");

			string SID = Request.QueryString["SID"].Replace("'","''");
			string UserID = Request.QueryString["UserID"].Replace("'","''");

			//Cek apakah user sudah log-in di aplikasi gateway
//			int c = Db.SingleInteger("SELECT COUNT(*) FROM " + Mi.DbPrefix + "SECURITY..LOGIN"
//				+ " WHERE "
//				+ " UserID = '" + UserID + "'"
//				+ " AND SessionID = '" + SID + "'"
//				+ " AND Status = 'A'");
//
//			if(c==0)
//				Response.Redirect("/CustomError/Restricted.html");
//			else
			{
				string SecLevel = Db.SingleString(
					"SELECT SecLevel FROM " + Mi.DbPrefix + "SECURITY..USERNAME WHERE UserID = '" + UserID + "'");

				//Cek security level
				Act.UserID = UserID;
				Act.SecLevel = SecLevel;

				Response.Redirect("Index.aspx");
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
		}
		#endregion
	}
}

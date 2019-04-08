namespace ISC064.LAUNCHING
{
	using System;
	using System.Drawing;
	using System.Data;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	public partial class HeadUnit : System.Web.UI.UserControl
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!Page.IsPostBack)
			{
				if(Request.QueryString["p"]!=null)
					this.Page.RegisterStartupScript(
						"focusScript"
						, "<script language='javascript'>"
						+ " document.getElementById('"+this.ID+"_"+prev.ID+"').focus();"
						+ "</script>"
						);
				else if(Request.QueryString["n"]!=null)
					this.Page.RegisterStartupScript(
						"focusScript"
						, "<script language='javascript'>"
						+ " document.getElementById('"+this.ID+"_"+next.ID+"').focus();"
						+ "</script>"
						);
				string p = Db.SingleString("SELECT ISNULL((SELECT TOP 1 NoStock FROM MS_UNIT WHERE NoStock < '"+NoStock+"' ORDER BY NoStock DESC),'')");
				string n = Db.SingleString("SELECT ISNULL((SELECT TOP 1 NoStock FROM MS_UNIT WHERE NoStock > '"+NoStock+"' ORDER BY NoStock ASC),'')");
				if(p!="") prev.HRef = "?p=1&NoStock="+p; else prev.InnerHtml = "<img src='/Media/icon_prev_d.gif'>";
				if(n!="") next.HRef = "?n=1&NoStock="+n; else next.InnerHtml = "<img src='/Media/icon_next_d.gif'>";
				
				string strSql = "SELECT NoStock,NoUnit FROM MS_UNIT WHERE NoStock = '" + NoStock + "'";
				DataTable rs = Db.Rs(strSql);

				if(rs.Rows.Count==0)
					Response.Redirect("/CustomError/Deleted.html");
				else
				{
					nostock.Text = rs.Rows[0]["NoStock"].ToString();
					unit.Text = rs.Rows[0]["NoUnit"].ToString();
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

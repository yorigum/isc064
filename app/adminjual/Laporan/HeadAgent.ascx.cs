namespace ISC064.ADMINJUAL
{
	using System;
	using System.Drawing;
	using System.Data;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	public partial class HeadAgent : System.Web.UI.UserControl
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!Page.IsPostBack)
			{
				if(Request.QueryString["p"]!=null)
                    this.Page.ClientScript.RegisterStartupScript(GetType(),    
                            "focusScript"
						, "<script language='javascript'>"
						+ " document.getElementById('"+this.ID+"_"+prev.ID+"').focus();"
						+ "</script>"
						);
				else if(Request.QueryString["n"]!=null)
                    this.Page.ClientScript.RegisterStartupScript(GetType(),
                        "focusScript"
						, "<script language='javascript'>"
						+ " document.getElementById('"+this.ID+"_"+next.ID+"').focus();"
						+ "</script>"
						);
				
				int p = Db.SingleInteger("SELECT ISNULL((SELECT TOP 1 NoAgent FROM MS_AGENT WHERE NoAgent < "+NoAgent+" ORDER BY NoAgent DESC),0)");
				int n = Db.SingleInteger("SELECT ISNULL((SELECT TOP 1 NoAgent FROM MS_AGENT WHERE NoAgent > "+NoAgent+" ORDER BY NoAgent ASC),0)");
				if(p!=0) prev.HRef = "?p=1&NoAgent="+p; else prev.InnerHtml = "<img src='/Media/icon_prev_d.gif'>";
				if(n!=0) next.HRef = "?n=1&NoAgent="+n; else next.InnerHtml = "<img src='/Media/icon_next_d.gif'>";

				string strSql = "SELECT NoAgent, Nama, Principal FROM MS_AGENT WHERE NoAgent = " + NoAgent;
				DataTable rs = Db.Rs(strSql);

				if(rs.Rows.Count==0)
					Response.Redirect("/CustomError/Deleted.html");
				else
				{
					noagent.Text = rs.Rows[0]["NoAgent"].ToString().PadLeft(5,'0');
					nama.Text = rs.Rows[0]["Nama"].ToString();
					principal.Text = rs.Rows[0]["Principal"].ToString();
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

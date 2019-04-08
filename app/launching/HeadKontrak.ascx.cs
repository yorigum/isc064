namespace ISC064.LAUNCHING   
{
	using System;
	using System.Drawing;
	using System.Data;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	public partial class HeadKontrak : System.Web.UI.UserControl
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
				
				string p = Db.SingleString("SELECT ISNULL((SELECT TOP 1 NoKontrak FROM MS_KONTRAK WHERE NoKontrak < '"+NoKontrak+"' ORDER BY NoKontrak DESC),'')");
				string n = Db.SingleString("SELECT ISNULL((SELECT TOP 1 NoKontrak FROM MS_KONTRAK WHERE NoKontrak > '"+NoKontrak+"' ORDER BY NoKontrak ASC),'')");
				if(p!="") prev.HRef = "?p=1&NoKontrak="+p; else prev.InnerHtml = "<img src='/Media/icon_prev_d.gif'>";
				if(n!="") next.HRef = "?n=1&NoKontrak="+n; else next.InnerHtml = "<img src='/Media/icon_next_d.gif'>";

				string strSql = "SELECT "
					+ " MS_KONTRAK.NoKontrak"
					+ ",MS_KONTRAK.NoStock"
					+ ",MS_KONTRAK.NoUnit"
					+ ",MS_CUSTOMER.NoCustomer"
					+ ",MS_CUSTOMER.Nama AS Cs"
					+ ",MS_AGENT.NoAgent"
					+ ",MS_AGENT.Nama + ' ' + MS_AGENT.Principal AS Ag"
					+ " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
					+ " INNER JOIN MS_AGENT ON MS_KONTRAK.NoAgent = MS_AGENT.NoAgent"
					+ " WHERE MS_KONTRAK.NoKontrak = '" + NoKontrak + "'";
				DataTable rs = Db.Rs(strSql);

				if(rs.Rows.Count==0)
					Response.Redirect("/CustomError/Deleted.html");
				else
				{
					nokontrak.Text = rs.Rows[0]["NoKontrak"].ToString();
					unit.Text = "<a href='UnitInfo.aspx?NoStock="+rs.Rows[0]["NoStock"]+"'>"
						+ rs.Rows[0]["NoUnit"] + "</a>";
					customer.Text = "<a href='CustomerEdit.aspx?NoCustomer="+rs.Rows[0]["NoCustomer"]+"'>"
						+ rs.Rows[0]["Cs"] + "</a>";
					agent.Text = rs.Rows[0]["Ag"].ToString();
				}
			}
		}

		private string NoKontrak
		{
			get
			{
				return Cf.Pk(Request.QueryString["NoKontrak"]);
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

namespace ISC064.MARKETINGJUAL
{
	using System;
	using System.Drawing;
	using System.Data;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	public partial class HeadCustomer : System.Web.UI.UserControl
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!Page.IsPostBack)
			{
				if(Request.QueryString["p"]!=null)
					this.Page.RegisterStartupScript(
						"focusScript"
						, "<script type='text/javascript'>"
						+ " document.getElementById('"+this.ID+"_"+prev.ID+"').focus();"
						+ "</script>"
						);
				else if(Request.QueryString["n"]!=null)
					this.Page.RegisterStartupScript(
						"focusScript"
						, "<script type='text/javascript'>"
						+ " document.getElementById('"+this.ID+"_"+next.ID+"').focus();"
						+ "</script>"
						);
				
				int p = Db.SingleInteger("SELECT ISNULL((SELECT TOP 1 NoCustomer FROM MS_CUSTOMER WHERE NoCustomer < "+NoCustomer+" ORDER BY NoCustomer DESC),0)");
				int n = Db.SingleInteger("SELECT ISNULL((SELECT TOP 1 NoCustomer FROM MS_CUSTOMER WHERE NoCustomer > "+NoCustomer+" ORDER BY NoCustomer ASC),0)");
				if(p!=0) prev.HRef = "?p=1&NoCustomer="+p; else prev.InnerHtml = "<i class='fa fa-long-arrow-left btn-disabled'></i> <b class='btn-disabled'>Prev</b>";
				if(n!=0) next.HRef = "?n=1&NoCustomer="+n; else next.InnerHtml = "<b class='btn-disabled'>Next</b> <i class='fa fa-long-arrow-right btn-disabled'></i>";

				string strSql = "SELECT NoCustomer,Nama,AgentInput FROM MS_CUSTOMER WHERE NoCustomer = " + NoCustomer;
				DataTable rs = Db.Rs(strSql);

				if(rs.Rows.Count==0)
					Response.Redirect("/CustomError/Deleted.html");
				else
				{
					nocustomer.Text = rs.Rows[0]["NoCustomer"].ToString().PadLeft(5,'0');
					nama.Text = rs.Rows[0]["Nama"].ToString();
					
					agent.Text = rs.Rows[0]["AgentInput"].ToString();

					string NamaAgent = Db.SingleString(
						"SELECT ISNULL((SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..USERNAME WHERE UserID = '"+rs.Rows[0]["AgentInput"]+"'),'')");
					if(NamaAgent!="")
						agent.Text = agent.Text + " ("+NamaAgent+")";
				}
			}
		}

		private string NoCustomer
		{
			get
			{
				return Cf.Pk(Request.QueryString["NoCustomer"]);
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

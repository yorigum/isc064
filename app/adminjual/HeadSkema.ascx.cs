namespace ISC064.ADMINJUAL
{
	using System;
	using System.Drawing;
	using System.Data;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	public partial class HeadSkema : System.Web.UI.UserControl
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
				
				int p = Db.SingleInteger("SELECT ISNULL((SELECT TOP 1 Nomor FROM REF_SKEMA WHERE Nomor < "+Nomor+" ORDER BY Nomor DESC),0)");
				int n = Db.SingleInteger("SELECT ISNULL((SELECT TOP 1 Nomor FROM REF_SKEMA WHERE Nomor > "+Nomor+" ORDER BY Nomor ASC),0)");
				if(p!=0) prev.HRef = "?p=1&Nomor="+p; else prev.InnerHtml = "<i class='fa fa-long-arrow-left btn-disabled'></i> <b class='btn-disabled'>Prev</b>";
                if (n!=0) next.HRef = "?n=1&Nomor="+n; else next.InnerHtml = "<b class='btn-disabled'>Next</b> <i class='fa fa-long-arrow-right btn-disabled'></i>";

                string strSql = "SELECT Nomor,Nama FROM REF_SKEMA WHERE Nomor = " + Nomor;
				DataTable rs = Db.Rs(strSql);

				if(rs.Rows.Count==0)
					Response.Redirect("/CustomError/Deleted.html");
				else
				{
					nomor.Text = rs.Rows[0]["Nomor"].ToString().PadLeft(3,'0');
					nama.Text = rs.Rows[0]["Nama"].ToString();
				}
			}
		}

		private string Nomor
		{
			get
			{
				return Cf.Pk(Request.QueryString["Nomor"]);
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

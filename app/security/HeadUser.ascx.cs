namespace ISC064.SECURITY
{
	using System;
	using System.Drawing;
	using System.Data;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	public partial class HeadUser : System.Web.UI.UserControl
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
				
				//modul
				string mod = "";
				if(Request.QueryString["Modul"]!=null)
					mod = "&Modul="+Request.QueryString["Modul"];
				
				string p = Db.SingleString("SELECT ISNULL((SELECT TOP 1 UserID FROM USERNAME WHERE UserID < '"+UserID+"' ORDER BY UserID DESC)"+ ",'')");
				string n = Db.SingleString("SELECT ISNULL((SELECT TOP 1 UserID FROM USERNAME WHERE UserID > '"+UserID+"' ORDER BY UserID ASC)"+ ",'')");
				if(p!="") prev.HRef = "?p=1&UserID="+p+mod; else prev.InnerHtml = "<i class='fa fa-long-arrow-left btn-disabled'></i> <b class='btn-disabled'>Prev</b>";
				if(n!="") next.HRef = "?n=1&UserID="+n+mod; else next.InnerHtml = "<b class='btn-disabled'>Next</b> <i class='fa fa-long-arrow-right btn-disabled'></i>";

				string strSql = "SELECT Nama,SecLevel FROM USERNAME WHERE UserID = '" + UserID + "'";
				DataTable rs = Db.Rs(strSql);

				if(rs.Rows.Count==0)
					Response.Redirect("/CustomError/Deleted.html");
				else
				{
					nama.Text = rs.Rows[0]["Nama"].ToString();
					seclevel.Text = "<a href='SecLevelEdit.aspx?Kode="+rs.Rows[0]["SecLevel"]+"'>" + rs.Rows[0]["SecLevel"] + "</a>";
				}
			}
		}

		private string UserID
		{
			get
			{
				return Cf.Pk(Request.QueryString["UserID"]);
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

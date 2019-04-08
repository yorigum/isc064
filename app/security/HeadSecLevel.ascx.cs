namespace ISC064.SECURITY
{
	using System;
	using System.Drawing;
	using System.Data;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	public partial class HeadSecLevel : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label seclevel;
	
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

				string p = Db.SingleString("SELECT ISNULL((SELECT TOP 1 Kode FROM SECLEVEL WHERE Kode < '"+Kode+"' ORDER BY Kode DESC),'')");
				string n = Db.SingleString("SELECT ISNULL((SELECT TOP 1 Kode FROM SECLEVEL WHERE Kode > '"+Kode+"' ORDER BY Kode ASC),'')");
				if(p!="") prev.HRef = "?p=1&Kode="+p+mod; else prev.InnerHtml = "<i class='fa fa-long-arrow-left btn-disabled'></i> <b class='btn-disabled'>Prev</b>";
                if (n!="") next.HRef = "?n=1&Kode="+n+mod; else next.InnerHtml = "<b class='btn-disabled'>Next</b> <i class='fa fa-long-arrow-right btn-disabled'></i>";

                string strSql = "SELECT Kode,Nama FROM SECLEVEL WHERE Kode = '" + Kode + "'";
				DataTable rs = Db.Rs(strSql);

				if(rs.Rows.Count==0)
					Response.Redirect("/CustomError/Deleted.html");
				else
				{
					kode.Text = rs.Rows[0]["Kode"].ToString();
					nama.Text = rs.Rows[0]["Nama"].ToString();
				}
			}
		}

		private string Kode
		{
			get
			{
				return Cf.Pk(Request.QueryString["Kode"]);
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

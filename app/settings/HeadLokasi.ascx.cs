namespace ISC064.SETTINGS
{
	using System;
	using System.Drawing;
	using System.Data;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	public partial class HeadLokasi : System.Web.UI.UserControl
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!Page.IsPostBack)
			{
				if(Request.QueryString["p"]!=null)
					this.Page.ClientScript.RegisterStartupScript(
                        GetType()
						,"focusScript"
						, "<script type='text/javascript'>"
						+ " document.getElementById('"+this.ID+"_"+prev.ID+"').focus();"
						+ "</script>",
                        true
						);
				else if(Request.QueryString["n"]!=null)
					this.Page.ClientScript.RegisterStartupScript(
                        GetType()
						,"focusScript"
						, "<script type='text/javascript'>"
						+ " document.getElementById('"+this.ID+"_"+next.ID+"').focus();"
						+ "</script>"
                        ,true
						);
				
				int p = Db.SingleInteger("SELECT ISNULL((SELECT TOP 1 SN FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_LOKASI WHERE SN < '" + NoLokasi + "' ORDER BY SN DESC),'')");
				int n = Db.SingleInteger("SELECT ISNULL((SELECT TOP 1 SN FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_LOKASI WHERE SN > '" + NoLokasi + "' ORDER BY SN ASC),'')");
				if(p!=0) prev.HRef = "?NoLokasi=" + (Convert.ToInt32(NoLokasi) - 1); else prev.InnerHtml = "<i class='fa fa-long-arrow-left btn-disabled'></i> <b class='btn-disabled'>Prev</b>";
                if (n!=0) next.HRef = "?NoLokasi=" + (Convert.ToInt32(NoLokasi) + 1); else next.InnerHtml = "<b class='btn-disabled'>Next</b> <i class='fa fa-long-arrow-right btn-disabled'></i>";
                
                string strSql = "SELECT * FROM ISC064_MARKETINGJUAL..REF_LOKASI WHERE SN = '" + NoLokasi + "'";
				DataTable rs = Db.Rs(strSql);

				if(rs.Rows.Count==0)
					Response.Redirect("/CustomError/Deleted.html");
				else
				{
					nostock.Text = rs.Rows[0]["SN"].ToString();
					nama.Text = rs.Rows[0]["Nama"].ToString();
                    lokasi.Text = rs.Rows[0]["Lokasi"].ToString();
				}
			}
		}

		private string NoLokasi
        {
			get
			{
				return Cf.Pk(Request.QueryString["NoLokasi"]);
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

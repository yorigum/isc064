namespace ISC064.COLLECTION
{
	using System;
	using System.Drawing;
	using System.Data;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	public partial class HeadPJT : System.Web.UI.UserControl
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!Page.IsPostBack)
			{
                if (Request.QueryString["p"] != null)
                    this.Page.RegisterStartupScript(
                        "focusScript"
                        , "<script type='text/javascript'>"
                        + " document.getElementById('" + this.ID + "_" + prev.ID + "').focus();"
                        + "</script>"
                        );
                else if (Request.QueryString["n"] != null)
                    this.Page.RegisterStartupScript(
                        "focusScript"
                        , "<script type='text/javascript'>"
                        + " document.getElementById('" + this.ID + "_" + next.ID + "').focus();"
                        + "</script>"
                        );

                string p = Db.SingleString("SELECT ISNULL((SELECT TOP 1 NoPJT FROM MS_PJT a"
                                        + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK b"
                                        + " ON a.Ref = b.NoKontrak"
                                        + " WHERE NoPJT < '" + NoPJT + "'"
                                        + " AND b.Project IN (" + Act.ProjectListSql + ")"
                                        + " ORDER BY NoPJT DESC),'')");
                string n = Db.SingleString("SELECT ISNULL((SELECT TOP 1 NoPJT FROM MS_PJT a"
                                        + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK b"
                                        + " ON a.Ref = b.NoKontrak"
                                        + " WHERE NoPJT > '" + NoPJT + "'"
                                        + " AND b.Project IN (" + Act.ProjectListSql + ")"
                                        + " ORDER BY NoPJT DESC),'')");
                if (p != "") prev.HRef = "?p=1&NoPJT=" + p; else prev.InnerHtml = "<i class='fa fa-long-arrow-left btn-disabled'></i> <b class='btn-disabled'>Prev</b>";
                if (n != "") next.HRef = "?n=1&NoPJT=" + n; else next.InnerHtml = "<b class='btn-disabled'>Next</b> <i class='fa fa-long-arrow-right btn-disabled'></i>";

                string strSql = "SELECT NoPJT,Tipe,Ref,Unit,Customer FROM MS_PJT WHERE NoPJT = '" + NoPJT + "'";
                DataTable rs = Db.Rs(strSql);

                if (rs.Rows.Count == 0)
                    Response.Redirect("/CustomError/Deleted.html");
                else
                {
                    nopjt.Text = rs.Rows[0]["NoPJT"].ToString();
                    tipe.Text = rs.Rows[0]["Tipe"].ToString();
                    referensi.Text = rs.Rows[0]["Ref"].ToString();
                    unit.Text = rs.Rows[0]["Unit"].ToString();
                    customer.Text = "<a href='CustomerInfo.aspx?Tipe=" + rs.Rows[0]["Tipe"] + "&Ref=" + rs.Rows[0]["Ref"] + "'>"
                        + rs.Rows[0]["Customer"] + "</a>";
                }
			}
		}

		private string NoPJT
		{
			get
			{
				return Cf.Pk(Request.QueryString["NoPJT"]);
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

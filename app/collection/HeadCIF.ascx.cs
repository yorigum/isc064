namespace ISC064.COLLECTION
{
	using System;
	using System.Drawing;
	using System.Data;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

    public partial class HeadCIF : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
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

                string p = "";
                string n = "";
                if (Tipe != "TENANT")
                {
                    p = Db.SingleString("SELECT ISNULL((SELECT TOP 1 NoKontrak FROM " + Tb + "..MS_KONTRAK AS MS_KONTRAK  WHERE NoKontrak < '" + Ref + "' AND Project IN(" + Act.ProjectListSql + ") ORDER BY NoKontrak DESC),'')");
                    n = Db.SingleString("SELECT ISNULL((SELECT TOP 1 NoKontrak FROM " + Tb + "..MS_KONTRAK AS MS_KONTRAK  WHERE NoKontrak > '" + Ref + "' AND Project IN(" + Act.ProjectListSql + ") ORDER BY NoKontrak ASC),'')");
                }
                else
                {
                    p = Db.SingleString("SELECT ISNULL((SELECT TOP 1 NoPenghuni FROM " + Tb + "..MS_PENGHUNI AS MS_PENGHUNI  WHERE NoPenghuni < '" + Ref + "' ORDER BY NoPenghuni DESC),'')");
                    n = Db.SingleString("SELECT ISNULL((SELECT TOP 1 NoPenghuni FROM " + Tb + "..MS_PENGHUNI AS MS_PENGHUNI  WHERE NoPenghuni > '" + Ref + "' ORDER BY NoPenghuni ASC),'')");
                }
                if (p != "") prev.HRef = "?p=1&Tipe=" + Tipe + "&Ref=" + p; else prev.InnerHtml = "<i class='fa fa-long-arrow-left btn-disabled'></i> <b class='btn-disabled'>Prev</b>";
                if (n != "") next.HRef = "?n=1&Tipe=" + Tipe + "&Ref=" + n; else next.InnerHtml = "<b class='btn-disabled'>Next</b> <i class='fa fa-long-arrow-right btn-disabled'></i>";

				string strSql = "";
				if(Tipe!="TENANT")
				{
					strSql = "SELECT "
						+ " MS_KONTRAK.NoKontrak AS NoRef"
						+ ",MS_KONTRAK.NoUnit"
						+ ",MS_CUSTOMER.Nama AS Cs"
						+ " FROM "+Tb+"..MS_KONTRAK AS MS_KONTRAK INNER JOIN "+Tb+"..MS_CUSTOMER AS MS_CUSTOMER"
						+ " ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
						+ " WHERE MS_KONTRAK.NoKontrak = '" + Ref + "'";
				}
				else
				{
					strSql = "SELECT "
						+ " MS_PENGHUNI.NoPenghuni AS NoRef"
						+ ",MS_PENGHUNI.NoUnit"
						+ ",MS_PENGHUNI.Nama AS Cs"
						+ " FROM "+Tb+"..MS_PENGHUNI AS MS_PENGHUNI WHERE MS_PENGHUNI.NoPenghuni = '" + Ref + "'";
				}
				DataTable rs = Db.Rs(strSql);

				if(rs.Rows.Count==0)
					Response.Redirect("/CustomError/Deleted.html");
				else
				{
					tipe.Text = Tipe;
					referensi.Text = rs.Rows[0]["NoRef"].ToString();
					unit.Text = rs.Rows[0]["NoUnit"].ToString();
					customer.Text = rs.Rows[0]["Cs"].ToString();
				}
			}
		}

		private string Tb
		{
			get
			{
				return Sc.MktTb(Tipe);
			}
		}

		private string Tipe
		{
			get
			{
				return Cf.Pk(Request.QueryString["Tipe"]);
			}
		}

		private string Ref
		{
			get
			{
				return Cf.Pk(Request.QueryString["Ref"]);
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

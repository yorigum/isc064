namespace ISC064.FINANCEAR
{
    using System;
    using System.Drawing;
    using System.Data;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;

    public partial class HeadVA : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["p"] != null)
                    this.Page.RegisterStartupScript(
                        "focusScript"
                        , "<script language='javascript'>"
                        + " document.getElementById('" + this.ID + "_" + prev.ID + "').focus();"
                        + "</script>"
                        );
                else if (Request.QueryString["n"] != null)
                    this.Page.RegisterStartupScript(
                        "focusScript"
                        , "<script language='javascript'>"
                        + " document.getElementById('" + this.ID + "_" + next.ID + "').focus();"
                        + "</script>"
                        );

                string p = Db.SingleString("SELECT ISNULL((SELECT TOP 1 NoVA FROM REF_VA WHERE NoVA < '" + NoVA + "' AND Project IN (" + Act.ProjectListSql + ") ORDER BY NoVA DESC), '')");
                string n = Db.SingleString("SELECT ISNULL((SELECT TOP 1 NoVA FROM REF_VA WHERE NoVA > '" + NoVA + "' AND Project IN (" + Act.ProjectListSql + ") ORDER BY NoVA ASC), '')");
                if (p != "") prev.HRef = "?p=1&NoVA=" + p; else prev.InnerHtml = "<i class='fa fa-long-arrow-left btn-disabled'></i> <b class='btn-disabled'>Prev</b>";
                if (n != "") next.HRef = "?n=1&NoVA=" + n; else next.InnerHtml = "<b class='btn-disabled'>Next</b> <i class='fa fa-long-arrow-right btn-disabled'></i>";

                string strSql = "SELECT a.*, b.NoKontrak, b.NoUnit"
                    + ", (SELECT Nama FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER WHERE NoCustomer = b.NoCustomer) AS Cs"
                    + " FROM REF_VA a"
                    + " LEFT JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK b ON a.NoVA = b.NoVA"
                    + " WHERE a.NoVA = '" + NoVA + "'";
                DataTable rs = Db.Rs(strSql);

                if (rs.Rows.Count == 0)
                    Response.Redirect("/CustomError/Deleted.html");
                else
                {
                    nova.Text = rs.Rows[0]["NoVA"].ToString();
                    referensi.Text = (!(rs.Rows[0]["NoKontrak"] is DBNull)) ? rs.Rows[0]["NoKontrak"].ToString() : "";
                    unit.Text = (!(rs.Rows[0]["NoUnit"] is DBNull)) ? rs.Rows[0]["NoUnit"].ToString() : "";
                    customer.Text = (!(rs.Rows[0]["Cs"] is DBNull)) ? rs.Rows[0]["Cs"].ToString() : "";
                }
            }
        }

        private string NoVA
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoVA"]);
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

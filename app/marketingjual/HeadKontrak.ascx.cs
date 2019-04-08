namespace ISC064.MARKETINGJUAL
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

                string p = Db.SingleString("SELECT ISNULL((SELECT TOP 1 NoKontrak FROM MS_KONTRAK WHERE NoKontrak < '" + NoKontrak + "' AND Project IN (" + Act.ProjectListSql + ") ORDER BY NoKontrak DESC),'')");
                string n = Db.SingleString("SELECT ISNULL((SELECT TOP 1 NoKontrak FROM MS_KONTRAK WHERE NoKontrak > '" + NoKontrak + "' AND Project IN (" + Act.ProjectListSql + ") ORDER BY NoKontrak ASC),'')");                  
                if (p != "") prev.HRef = "?p=1&NoKontrak=" + p; else prev.InnerHtml = "<i class='fa fa-long-arrow-left btn-disabled'></i> <b class='btn-disabled'>Prev</b>";
                if (n != "") next.HRef = "?n=1&NoKontrak=" + n; else next.InnerHtml = "<b class='btn-disabled'>Next</b> <i class='fa fa-long-arrow-right btn-disabled'></i>";

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

                if (rs.Rows.Count == 0)
                    Response.Redirect("/CustomError/Deleted.html");
                else
                {
                    nokontrak.Text = rs.Rows[0]["NoKontrak"].ToString();
                    unit.Text = "<a href='UnitInfo.aspx?NoStock=" + rs.Rows[0]["NoStock"] + "'>"
                        + rs.Rows[0]["NoUnit"] + "</a>";
                    customer.Text = "<a href='CustomerEdit.aspx?NoCustomer=" + rs.Rows[0]["NoCustomer"] + "'>"
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

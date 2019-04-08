namespace ISC064.LEGAL
{
    using System;
    using System.Drawing;
    using System.Data;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;

    public partial class HeadAJB : System.Web.UI.UserControl
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

                string p = Db.SingleString("SELECT ISNULL((SELECT TOP 1 NoKontrak FROM MS_AJB WHERE NoKontrak < '" + NoKontrak + "' AND (SELECT COUNT(Project) FROM MS_KONTRAK WHERE NoKontrak = MS_AJB.NoKontrak AND Project IN(" + Act.ProjectListSql + ")) > 0 ORDER BY NoKontrak DESC),'')");
                string n = Db.SingleString("SELECT ISNULL((SELECT TOP 1 NoKontrak FROM MS_AJB WHERE NoKontrak > '" + NoKontrak + "' AND (SELECT COUNT(Project) FROM MS_KONTRAK WHERE NoKontrak = MS_AJB.NoKontrak AND Project IN(" + Act.ProjectListSql + ")) > 0 ORDER BY NoKontrak ASC),'')");
                if (p != "") prev.HRef = "?p=1&NoKontrak=" + p; else prev.InnerHtml = "<i class='fa fa-long-arrow-left btn-disabled'></i> <b class='btn-disabled'>Prev</b>";
                if (n != "") next.HRef = "?n=1&NoKontrak=" + n; else next.InnerHtml = "<b class='btn-disabled'>Next</b> <i class='fa fa-long-arrow-right btn-disabled'></i>";

                //string strSql = "SELECT "
                //    + " MS_KONTRAK.NoKontrak"
                //    + ",MS_KONTRAK.NoStock"
                //    + ",MS_KONTRAK.NoUnit"
                //    + ",MS_CUSTOMER.NoCustomer"
                //    + ",MS_CUSTOMER.Nama AS Cs"
                //    + ",MS_AGENT.NoAgent"
                //    + ",MS_AGENT.Nama + ' ' + MS_AGENT.Principal AS Ag"
                //    + " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
                //    + " INNER JOIN MS_AGENT ON MS_KONTRAK.NoAgent = MS_AGENT.NoAgent"
                //    + " WHERE MS_KONTRAK.NoKontrak = '" + NoKontrak + "'";
                //DataTable rs = Db.Rs(strSql);
                string strSql = "SELECT "
                + " A.NoKontrak"
                + ",A.NoStock"
                + ",A.Status"
                + ",A.NoUnit"
                + ",C.NoCustomer"
                + ",C.Nama AS Cs"
                + ",D.NoAgent"
                + ",D.Nama + ' ' + D.Principal AS Ag"
                //+ ",B.ST"
                //+ ",A.NoPPJB"
                //+ ",B.NoAJB"
                + " FROM MS_KONTRAK A LEFT JOIN MS_AJB B ON A.NoKontrak = B.NoKontrak"
                + " INNER JOIN MS_CUSTOMER C ON A.NoCustomer = C.NoCustomer "
                + " INNER JOIN MS_AGENT D ON A.NoAgent = D.NoAgent"
                + " WHERE A.NoKontrak = '" + NoKontrak + "'";
                DataTable rs = Db.Rs(strSql);

                //if (rs.Rows.Count == 0)
                    //Response.Redirect("/CustomError/Deleted.html");
                //else
                if(rs.Rows.Count > 0)
                {
                    nokontrak.Text = rs.Rows[0]["NoKontrak"].ToString();
                    unit.Text = rs.Rows[0]["NoUnit"].ToString();
                    customer.Text = rs.Rows[0]["Cs"].ToString();
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
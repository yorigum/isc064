namespace ISC064.SETTINGS
{
    using System;
    using System.Drawing;
    using System.Data;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;

    public partial class HeadUnit : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["p"] != null)
                    this.Page.ClientScript.RegisterStartupScript(
                        GetType()
                        , "focusScript"
                        , "<script type='text/javascript'>"
                        + " document.getElementById('" + this.ID + "_" + prev.ID + "').focus();"
                        + "</script>",
                        true
                        );
                else if (Request.QueryString["n"] != null)
                    this.Page.ClientScript.RegisterStartupScript(
                        GetType()
                        , "focusScript"
                        , "<script type='text/javascript'>"
                        + " document.getElementById('" + this.ID + "_" + next.ID + "').focus();"
                        + "</script>"
                        , true
                        );

                int p = Db.SingleInteger("SELECT ISNULL((SELECT TOP 1 SN FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_JENIS WHERE SN < '" + NoJenis + "' AND Project IN (" + Act.ProjectListSql + ") ORDER BY SN DESC),'')");
                int n = Db.SingleInteger("SELECT ISNULL((SELECT TOP 1 SN FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_JENIS WHERE SN > '" + NoJenis + "' AND Project IN (" + Act.ProjectListSql + ") ORDER BY SN ASC),'')");
                if (p != 0) prev.HRef = "?NoJenis=" + (Convert.ToInt32(NoJenis) - 1); else prev.InnerHtml = "<i class='fa fa-long-arrow-left btn-disabled'></i> <b class='btn-disabled'>Prev</b>";
                if (n != 0) next.HRef = "?NoJenis=" + (Convert.ToInt32(NoJenis) + 1); else next.InnerHtml = "<b class='btn-disabled'>Next</b> <i class='fa fa-long-arrow-right btn-disabled'></i>";

                string strSql = "SELECT * FROM ISC064_MARKETINGJUAL..REF_JENIS WHERE SN = '" + NoJenis + "'";
                DataTable rs = Db.Rs(strSql);

                if (rs.Rows.Count == 0)
                    Response.Redirect("/CustomError/Deleted.html");
                else
                {
                    nostock.Text = rs.Rows[0]["SN"].ToString();
                    nama.Text = rs.Rows[0]["Nama"].ToString();
                }
            }
        }

        private string NoJenis
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoJenis"]);
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

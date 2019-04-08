namespace ISC064.SETTINGS
{
    using System;
    using System.Drawing;
    using System.Data;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;

    public partial class HeadLevel : System.Web.UI.UserControl
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
                        + "</script>"
                        );
                else if (Request.QueryString["n"] != null)
                    this.Page.ClientScript.RegisterStartupScript(
                        GetType()
                        , "focusScript"
                        , "<script type='text/javascript'>"
                        + " document.getElementById('" + this.ID + "_" + next.ID + "').focus();"
                        + "</script>"
                        );

                int p = Db.SingleInteger("SELECT ISNULL((SELECT TOP 1 LevelID FROM ISC064_MARKETINGJUAL..ref_agent_level WHERE LevelID < " + NoLevel + " AND Project IN (" + Act.ProjectListSql + ") ORDER BY LevelID DESC),0)");
                int n = Db.SingleInteger("SELECT ISNULL((SELECT TOP 1 LevelID FROM ISC064_MARKETINGJUAL..ref_agent_level WHERE LevelID > " + NoLevel + " AND Project IN (" + Act.ProjectListSql + ") ORDER BY LevelID ASC),0)");
                if (p != 0) prev.HRef = "?NoLevel=" + (Convert.ToInt32(NoLevel) - 1); else prev.InnerHtml = "<i class='fa fa-long-arrow-left btn-disabled'></i> <b class='btn-disabled'>Prev</b>";
                if (n != 0) next.HRef = "?NoLevel=" + (Convert.ToInt32(NoLevel) + 1); else next.InnerHtml = "<b class='btn-disabled'>Next</b> <i class='fa fa-long-arrow-right btn-disabled'></i>";

                string strSql = "SELECT * FROM ISC064_MARKETINGJUAL..ref_agent_level WHERE LevelID = " + NoLevel;
                DataTable rs = Db.Rs(strSql);

                if (rs.Rows.Count == 0)
                    Response.Redirect("/CustomError/Deleted.html");
                else
                {
                    notipe.Text = rs.Rows[0]["LevelID"].ToString().PadLeft(5, '0');
                    nama.Text = rs.Rows[0]["Nama"].ToString();
                    tipe.Text = rs.Rows[0]["Tipe"].ToString();
                    parent.Text = rs.Rows[0]["ParentID"].ToString();
                }
            }
        }

        private string NoLevel
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoLevel"]);
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

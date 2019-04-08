namespace ISC064.FINANCEAR
{
    using System;
    using System.Drawing;
    using System.Data;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;

    public partial class HeadTTS : System.Web.UI.UserControl
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

                int p = Db.SingleInteger("SELECT ISNULL((SELECT TOP 1 NoTTS FROM MS_TTS WHERE NoTTS < " + NoTTS + " AND Project IN (" + Act.ProjectListSql + ") ORDER BY NoTTS DESC),0)");
                int n = Db.SingleInteger("SELECT ISNULL((SELECT TOP 1 NoTTS FROM MS_TTS WHERE NoTTS > " + NoTTS + " AND Project IN (" + Act.ProjectListSql + ") ORDER BY NoTTS ASC),0)");
                if (p != 0) prev.HRef = "?p=1&NoTTS=" + p; else prev.InnerHtml = "<i class='fa fa-long-arrow-left btn-disabled'></i> <b class='btn-disabled'>Prev</b>";
                if (n != 0) next.HRef = "?n=1&NoTTS=" + n; else next.InnerHtml = "<b class='btn-disabled'>Next</b> <i class='fa fa-long-arrow-right btn-disabled'></i>";

                string strSql = "SELECT NoTTS,NoTTS2,Tipe,Ref,Customer,Unit FROM MS_TTS WHERE NoTTS = " + NoTTS;
                DataTable rs = Db.Rs(strSql);

                if (rs.Rows.Count == 0)
                    Response.Redirect("/CustomError/Deleted.html");
                else
                {
                    notts.Text = rs.Rows[0]["NoTTS2"].ToString().PadLeft(7, '0');

                    tipe.Text = rs.Rows[0]["Tipe"].ToString();
                    referensi.Text = rs.Rows[0]["Ref"].ToString();
                    unit.Text = rs.Rows[0]["Unit"].ToString();
                    customer.Text = "<a href='CustomerInfo.aspx?Tipe=" + rs.Rows[0]["Tipe"] + "&Ref=" + rs.Rows[0]["Ref"] + "'>"
                        + rs.Rows[0]["Customer"] + "</a>";
                }
            }
        }

        private string NoTTS
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoTTS"]);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ISC064.COLLECTION
{
    public partial class HeadSKL : System.Web.UI.UserControl
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

                string p = Db.SingleString("SELECT ISNULL((SELECT TOP 1 NoSKL FROM MS_SKL a"
                                        + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK b"
                                        + " ON a.Ref = b.NoKontrak"
                                        + " WHERE NoSKL < '" + NoSKL + "'"
                                        + " AND b.Project IN (" + Act.ProjectListSql + ")"
                                        + " ORDER BY NoSKL DESC),'')");
                string n = Db.SingleString("SELECT ISNULL((SELECT TOP 1 NoSKL FROM MS_SKL a"
                                        + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK b"
                                        + " ON a.Ref = b.NoKontrak"
                                        + " WHERE NoSKL > '" + NoSKL + "'"
                                        + " AND b.Project IN (" + Act.ProjectListSql + ")"
                                        + " ORDER BY NoSKL ASC),'')");
                if (p != "") prev.HRef = "?p=1&NoSKL=" + p; else prev.InnerHtml = "<i class='fa fa-long-arrow-left btn-disabled'></i> <b class='btn-disabled'>Prev</b>";
                if (n != "") next.HRef = "?n=1&NoSKL=" + n; else next.InnerHtml = "<b class='btn-disabled'>Next</b> <i class='fa fa-long-arrow-right btn-disabled'></i>";

                string strSql = "SELECT NoSKL,Ref FROM MS_SKL WHERE NoSKL = '" + NoSKL + "'";
                DataTable rs = Db.Rs(strSql);

                if (rs.Rows.Count == 0)
                    Response.Redirect("/CustomError/Deleted.html");
                else
                {
                    noskl.Text = rs.Rows[0]["NoSKL"].ToString();
                    referensi.Text = rs.Rows[0]["Ref"].ToString();
                }
            }
        }

        private string NoSKL
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoSKL"]);
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

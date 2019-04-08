namespace ISC064.ADMINJUAL
{
    using System;
    using System.Drawing;
    using System.Data;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;


    public partial class HeadCF : System.Web.UI.UserControl
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

                string p = Db.SingleString("SELECT ISNULL((SELECT TOP 1 Lvl FROM REF_KOMISI_CF WHERE Lvl < '" + Lvl + "' ORDER BY Lvl DESC),'')");
                string n = Db.SingleString("SELECT ISNULL((SELECT TOP 1 Lvl FROM REF_KOMISI_CF WHERE Lvl > '" + Lvl + "' ORDER BY Lvl ASC),'')");
                if (p != "") prev.HRef = "?p=1&Lvl=" + p; else prev.InnerHtml = "<i class='fa fa-long-arrow-left btn-disabled'></i> <b class='btn-disabled'>Prev</b>";
                if (n != "") next.HRef = "?n=1&Lvl=" + n; else next.InnerHtml = "<b class='btn-disabled'>Next</b> <i class='fa fa-long-arrow-right btn-disabled'></i>";

                string strSql = "SELECT * FROM REF_KOMISI_CF WHERE Lvl = '" + Lvl + "'";
                DataTable rs = Db.Rs(strSql);

                if (rs.Rows.Count == 0)
                    Response.Redirect("/CustomError/Deleted.html");
                else
                {
                    lvl.Text = rs.Rows[0]["Lvl"].ToString();
                    ket.Text = rs.Rows[0]["Keterangan"].ToString();
                }
            }
        }

        private string Lvl
        {
            get
            {
                return Cf.Pk(Request.QueryString["Lvl"]);
            }
        }
    }
}

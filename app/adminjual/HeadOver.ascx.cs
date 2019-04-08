namespace ISC064.ADMINJUAL
{
    using System;
    using System.Drawing;
    using System.Data;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;


    public partial class HeadOver : System.Web.UI.UserControl
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

                int p = Db.SingleInteger("SELECT ISNULL((SELECT TOP 1 SN FROM REF_KOMISI_OVER WHERE SN < " + SN + " ORDER BY SN DESC),0)");
                int n = Db.SingleInteger("SELECT ISNULL((SELECT TOP 1 SN FROM REF_KOMISI_OVER WHERE SN > " + SN + " ORDER BY SN ASC),0)");
                if (p != 0) prev.HRef = "?p=1&SN=" + p; else prev.InnerHtml = "<i class='fa fa-long-arrow-left btn-disabled'></i> <b class='btn-disabled'>Prev</b>";
                if (n != 0) next.HRef = "?n=1&SN=" + n; else next.InnerHtml = "<b class='btn-disabled'>Next</b> <i class='fa fa-long-arrow-right btn-disabled'></i>";

                string strSql = "SELECT * FROM REF_KOMISI_OVER WHERE SN = " + SN;
                DataTable rs = Db.Rs(strSql);

                if (rs.Rows.Count == 0)
                    Response.Redirect("/CustomError/Deleted.html");
                else
                {
                    noSN.Text = rs.Rows[0]["SN"].ToString().PadLeft(5, '0');
                    jabatan.Text = rs.Rows[0]["Jabatan"].ToString();
                }
            }
        }

        private string SN
        {
            get
            {
                return Cf.Pk(Request.QueryString["SN"]);
            }
        }
    }
}

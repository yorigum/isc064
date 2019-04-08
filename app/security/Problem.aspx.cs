using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ISC064.SECURITY
{
    public partial class Problem : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Cf.SetGrid(tb);
            if (!Page.IsPostBack)
                fill();
        }

        protected void fill()
        {
            string nav = "'<a href=\"javascript:call('''+ CONVERT(VARCHAR(10),ProblemID)+''')\"> Open </a>'";
            DataTable rs = Db.Rs("SELECT TOP 100 "
                + nav
                + " AS ID"
                + ",Url AS Halaman"
                + ",CONVERT(VARCHAR,Tgl,106) AS Tanggal"
                + " FROM PROBLEM ORDER BY ProblemID DESC"); //Ambil 100 aja biar ga lemot

            tb.DataSource = rs;
            tb.DataBind();
        }

        protected void tb_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            tb.PageIndex = e.NewPageIndex;
            fill();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ISC064.SECURITY
{
    public partial class ProblemFile : System.Web.UI.Page
    {
        protected int ProblemID { get { return Convert.ToInt16(Request.QueryString["id"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                fill();
        }

        protected void fill()
        {
            var rs = Db.Rs("SELECT * FROM PROBLEM WHERE ProblemID = " + ProblemID);

            if (rs == null)
                Response.Redirect("/CustomError/Deleted.html");

            var r = rs.Rows[0];

            halaman.Text = r["Url"].ToString();
            keterangan.Text = r["Ket"].ToString();
        }
    }
}
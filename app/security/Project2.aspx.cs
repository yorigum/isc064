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
    public partial class Project2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Fill();
        }

        protected void Fill()
        {
            DataTable rs = Db.Rs("SELECT * FROM REF_PROJECT WHERE Project = '" + KodeProject + "'");
            project.Text = rs.Rows[0]["Project"].ToString();
            nama.Text = rs.Rows[0]["Nama"].ToString();
        }
        private string KodeProject
        {
            get
            {
                return Cf.Pk(Request.QueryString["project"]);
            }
        }
    }
}
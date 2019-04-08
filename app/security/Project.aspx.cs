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
    public partial class Project : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Fill();
        }

        protected void Fill()
        {
            DataTable rs = Db.Rs("SELECT * FROM REF_PROJECT");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                c = new TableCell();
                c.Text = "<a href=\"javascript:popProject('" + rs.Rows[i]["Project"].ToString() + "')\"> Open </a>";
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Project"].ToString();
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Nama"].ToString();
                r.Cells.Add(c);
                Rpt.Border(r);
                rpt.Rows.Add(r);
            }
        }
    }
}
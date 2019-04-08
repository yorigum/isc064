using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ISC064.SETTINGS
{

    public partial class SmsFormat1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();
            if (!Page.IsPostBack)
            {
                fill();
            }
        }
        protected void fill()
        {
            string[] map = LibSMS.Tipe;
            for (int i = 0; i <= map.GetUpperBound(0); i++)
            {
                string[] m = map[i].Split(';');

                TableRow tr = new TableRow();
                tb.Rows.Add(tr);

                TableCell c;

                c = new TableCell();
                c.Text = "<ul class=\"float\">"
                    + "<li><a href=\"SmsFormat2.aspx?id=" + m[0] + "\" style='text-decoration:none;'>" + m[1] + "</a></li>"
                    + "</ul>";
                c.CssClass = "link";
                tr.Cells.Add(c);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ISC064.SETTINGS
{
    public partial class HtmlEditor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();

            if (!Page.IsPostBack)
            {
                Bind();
                fill();
            }
        }

        protected void Bind()
        {
            Act.ProjectList(project);
        }

        protected void fill()
        {
            string sql = "SELECT Halaman, Modul, Nama FROM HTMLEDITOR WHERE Modul = '" + modul.SelectedValue + "' AND Project = '" + project.SelectedValue + "'";

            DataTable rs = Db.Rs(sql);

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow tr = new TableRow();
                TableCell c;
                HtmlAnchor a;

                c = new TableCell();
                a = new HtmlAnchor();

                a.InnerHtml = rs.Rows[i]["Nama"].ToString();
                a.HRef = "javascript:PopHtmlEditor('" + rs.Rows[i]["Halaman"] + "','" + project.SelectedValue + "');";
                c.Controls.Add(a);

                tr.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Modul"].ToString();
                tr.Cells.Add(c);

                list.Rows.Add(tr);
            }
        }

        protected void modul_SelectedIndexChanged(object sender, EventArgs e)
        {
            fill();
        }

        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            fill();
        }
    }
}
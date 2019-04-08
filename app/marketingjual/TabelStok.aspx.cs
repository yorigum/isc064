using System;
using System.Drawing;
using System.IO;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
    public partial class TabelStok : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack) Act.ProjectList(project);

            DataTable rs = Db.Rs("SELECT * FROM REF_LOKASI WHERE Project = '" + project.SelectedValue + "'");


            Rpt.NoData(rpt, rs, "Log file tidak tersedia.");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                c = new TableCell();
                if(rs.Rows[i]["Lokasi"].ToString()=="A")
                    c.Text = "<a href=\"TabelStokA.aspx\">"
                            + "Tower A"
                            + "</a>";
                else if (rs.Rows[i]["Lokasi"].ToString() == "B")
                    c.Text = "<a href=\"TabelStokB.aspx\">"
                      + "Tower B"
                      + "</a>";
                else if (rs.Rows[i]["Lokasi"].ToString() == "C")
                    c.Text = "<a href=\"TabelStokC.aspx\">"
                        + "Tower C"
                        + "</a>";
                else if (rs.Rows[i]["Lokasi"].ToString() == "S")
                    c.Text = "<a href=\"TabelStokS.aspx\">"
                        + "Shop House"
                        + "</a>";
                //else
                //    c.Text = "<a href=\"TabelStokView.aspx?Nama=" + rs.Rows[i]["Nama"] + "&Lokasi=" + rs.Rows[i]["Lokasi"] + "&Project='" + project.SelectedValue + "'\">"
                //      + rs.Rows[i]["Nama"].ToString()
                //      + "</a>";
                r.Cells.Add(c);

                Rpt.Border(r);
                rpt.Rows.Add(r);

            }

        }

    }

}

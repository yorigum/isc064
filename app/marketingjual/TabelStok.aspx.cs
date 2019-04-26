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

            for (int i = 1; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;
                c = new TableCell();

                //TableRow r2 = new TableRow();
                //TableCell c2;
                //c2 = new TableCell();

                //TableRow r3 = new TableRow();
                //TableCell c3;
                //c3 = new TableCell();

                //    c.Text = "<a href=\"TabelStokA.aspx\">"
                //            + "Ground Floor Plan"
                //            + "</a>";
                //    c2.Text = "<a href=\"TabelStokB.aspx\">"
                //      + "2nd Floor Plan"
                //      + "</a>";
                //    c3.Text = "<a href=\"TabelStokC.aspx\">"
                //        + "3rd Floor Plan"
                //        + "</a>";

                ////else
                c.Text = "<a href=\"TabelStokView.aspx?Nama=" + rs.Rows[i]["Nama"] + "&Lokasi=" + rs.Rows[i]["Lokasi"] + "&Project='" + project.SelectedValue + "'\">"
                  + rs.Rows[i]["Nama"].ToString()
                  + "</a>";
                r.Cells.Add(c);

                Rpt.Border(r);
                rpt.Rows.Add(r);

                //r2.Cells.Add(c2);

                //Rpt.Border(r2);
                //rpt.Rows.Add(r2);


                //r3.Cells.Add(c3);

                //Rpt.Border(r3);
                //rpt.Rows.Add(r3);


            }

        }
        
    }

}

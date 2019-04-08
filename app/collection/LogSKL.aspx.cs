using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ISC064.COLLECTION
{
    public partial class LogSKL : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                Fill();
            }
        }
        protected void Fill()
        {
          string strSql = "SELECT"
                        + " LogID"
                        + ",Tgl"
                        + ",Aktivitas"
                        + ",UserID"
                        + ",IP"
                        + ",(SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..USERNAME WHERE UserID = MS_SKL_LOG.UserID) AS Nama"
                        + ",Pk"
                        + " FROM MS_SKL_LOG"
                        + " WHERE Pk = '" + Pk + "' ORDER BY LogID";

            DataTable rs = Db.Rs(strSql);
            Rpt.NoData(rpt, rs, "Log file tidak tersedia.");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["Tgl"]);
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Time(rs.Rows[i]["Tgl"]);
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["UserID"].ToString();
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Nama"].ToString();
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "<a href=\"javascript:popLog('" + rs.Rows[i]["LogID"] + "','MS_SKL_LOG','MS_SKL','" + Pk + "')\">"
                    + rs.Rows[i]["Aktivitas"].ToString()
                    + "</a>";
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Pk"].ToString();
                c.Wrap = false;
                r.Cells.Add(c);

                Rpt.Border(r);
                rpt.Rows.Add(r);
            }
        }
        private string Pk
        {
            get
            {
                return Cf.Pk(Request.QueryString["Pk"]);
            }
        }
    }
}

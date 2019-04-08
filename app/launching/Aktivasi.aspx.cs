using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;

namespace ISC064.LAUNCHING
{
    public partial class Aktivasi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable rs = Db.Rs("SELECT * FROM MS_NUP WHERE Status >= 1");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                TableCell c;
                TableRow r = new TableRow();

                c = new TableCell();
                c.Text = rs.Rows[i]["NoNUP"].ToString();
                c.Attributes["style"] = "font-size:32pt;";
                r.Controls.Add(c);

                string St = "";
                if (Convert.ToInt16(rs.Rows[i]["Status"]) >= 1)
                {
                    St = "Sudah Aktivasi";
                }
                else
                    St = "Belum Aktivasi";
                c = new TableCell();
                c.Text = St;
                c.Attributes["style"] = "font-size:32pt;";
                r.Controls.Add(c);

                c = new TableCell();
                c.Text = Cf.Date(rs.Rows[i]["TglAktivasi"]);
                c.Attributes["style"] = "font-size:32pt;";
                r.Controls.Add(c);

                rpt.Controls.Add(r);
            }
        }
    }
}
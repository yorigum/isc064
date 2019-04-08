using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace ISC064
{
    public class LibMkt
    {
        public static void ListTipeSales(DropDownList container, string Project)
        {
            DataTable rs;

            rs = Db.Rs("SELECT * FROM REF_AGENT_TIPE WHERE Project = '" + Project + "'");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                container.Items.Add(new ListItem(rs.Rows[i]["Tipe"].ToString(), rs.Rows[i]["ID"].ToString()));
            }
        }
        public static void ListLvlSales(DropDownList container, int Tipe, string Project)
        {
            DataTable rs;

            rs = Db.Rs("SELECT * FROM REF_AGENT_LEVEL WHERE Tipe = " + Tipe + "AND Project = '" + Project + "'");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                container.Items.Add(new ListItem(rs.Rows[i]["Nama"].ToString(), rs.Rows[i]["LevelID"].ToString()));
            }
        }
    }
}

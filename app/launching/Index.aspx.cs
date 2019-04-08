using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ISC064.LAUNCHING
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //DataTable rs = Db.Rs("SELECT Nama FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_SITEPLAN WHERE ParentID = 0");
            //for (int i = 0; i < rs.Rows.Count; i++)
            //{
            //    //gambar.Text += "<img src='/Media/icon_tower.png' style='width: 80px; height: 80px; '>"+rs.Rows[i]["Nama"].ToString()+"</a>";
            //    //tower.Text += "<a href='/TabelStokView2.aspx?Nama=TOWER&Lokasi=A' style='text - align: center; vertical - align: middle; font - size: x - large; '>";
            //}
            //DataTable a = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_LOKASI WHERE Project IN (" + Act.ProjectListSql + ")");

            //for (int j = 0; j < a.Rows.Count; j++)
            //{
            //    TableRow r = new TableRow();
            //    TableCell c;

            //    c = new TableCell();
            //    c.Text = "<a href=TabelStokView2.aspx?Nama=Tower&Project=" + a.Rows[j]["Project"] + "&Lokasi=" + a.Rows[j]["Lokasi"] + "><img src='/Media/icon_tower.png' style='width: 80px; height: 80px; '>"
            //              + a.Rows[j]["Nama"] + "</a>";
            //    r.Cells.Add(c);
            //    tower1.Rows.Add(r);
            //    //tower2.HRef += "TabelStokView2.aspx?Nama=Tower&Lokasi="+a.Rows[j]["Lokasi"]+"";
            //    //towerlbl.InnerText += a.Rows[j]["Nama"].ToString();
            //}
        }
    }
}
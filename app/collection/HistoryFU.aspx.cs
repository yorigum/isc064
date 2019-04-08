using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.COLLECTION
{
    public partial class HistoryFU : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();
            Filltb();
        }
        private void Filltb()
        {
            string Strsql = "SELECT * FROM ISC064_MARKETINGJUAL..MS_FOLLOWUP WHERE NoKontrak = '" + Ref + "' AND NoTagihan = '" + NoUrut + "'";
            DataTable rs = Db.Rs(Strsql);

            for(int i=0; i<rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                Label l;
                TextBox t;
                string Collector = Db.SingleString("SELECT Nama FROM ISC064_SECURITY..USERNAME Where UserID = '" + rs.Rows[i]["Collector"] + "'");

                l = new Label();
                l.Text = "<tr valign=top>"
                    + "<td>" + (i + 1) + "</td>"
                    + "<td style='white-space:nowrap'>" + Cf.Day(rs.Rows[i]["TglFU"]) + "</td>"
                    + "<td>" + rs.Rows[i]["NamaGrouping"] + "</td>"
                    + "<td>" + rs.Rows[i]["Ket"] + "</td>"
                    + "<td style='white-space:nowrap'>" + Cf.Day(rs.Rows[i]["TglJanjiBayar"]) + "</td>"
                    +"<td>" + Collector + "</td>";
                list.Controls.Add(l);
            }
        }
        private string Ref
        {
            get
            {
                return Cf.Pk(Request.QueryString["Ref"]);
            }
        }

        private string NoUrut
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoUrut"]);
            }
        }

    }
}
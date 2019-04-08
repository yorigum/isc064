using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace ISC064.FINANCEAR
{
    public partial class VAEksporBNI : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            Fill();
        }
        protected void Fill()
        {
            string StrSql = "SELECT a.NoVA, b.Nama, a.NoUnit,b.NoCustomer FROM ISC064_MARKETINGJUAL..MS_KONTRAK a INNER JOIN ISC064_MARKETINGJUAL..MS_CUSTOMER b ON"
                          + "  a.NoCustomer = b.NoCustomer WHERE a.NoVA!='' AND a.STATUS='A'"
                          + " AND a.NoVA IN (SELECT NOVA FROM REF_VA WHERE Bank='BNI')";

            DataTable rs = Db.Rs(StrSql);

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                c = new TableCell();
                c.Text = (i+ 1).ToString();
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Nama"].ToString();
                c.Wrap = false;
                r.Cells.Add(c);

                string kdcust = rs.Rows[i]["NoCustomer"].ToString();
                string briva = rs.Rows[i]["NoVA"].ToString();

                c = new TableCell();
                c.Text = kdcust;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = briva;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoUnit"].ToString() + " - " + Mi.Pt;
                c.Wrap = false;
                r.Cells.Add(c);


                Rpt.Border(r);
                rpt.Rows.Add(r);
            }
        }
        protected void excel_Click(object sender, EventArgs e)
        {
            Rpt.ToExcel(this, rpt);
        }
    }
}

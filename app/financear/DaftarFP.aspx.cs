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
    public partial class DaftarFP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
                Fill();
                        
            Js.ConfirmKeyword(this, keyword);
        }
        protected void search_Click(object sender, System.EventArgs e)
        {
            Fill();
            
        }
        private void Fill()
        {
            string Project = Request.QueryString["project"];

            string query = "SELECT * FROM REF_FP WHERE STATUS = 0 AND Convert(varchar,TglTerimaFP,112) <= '" + Request.QueryString["tgl"] + "' AND Project = '" + Project + "'  ORDER BY NoFPS, NoUrut";
            DataTable rs = Db.Rs(query);

            Rpt.NoData(rpt, rs, "Tidak ditemukan nomor faktur pajak dengan keyword diatas.");
            
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                c = new TableCell();
                c.Text = "<a href=\"javascript:call('" + rs.Rows[i]["NoFPS"] + "','" + Request.QueryString["ctrl1"] + "','" + Request.QueryString["ctrl2"] + "')\">"
                        + rs.Rows[i]["NoFPS"] + "</a>"
                        ;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglTerimaFP"]);
                r.Cells.Add(c);

                Rpt.Border(r);
                rpt.Rows.Add(r);

            }
        }
    }
}

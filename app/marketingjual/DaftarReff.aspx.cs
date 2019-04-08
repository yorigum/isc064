using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{

    public partial class DaftarReff : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            //Js.ConfirmKeyword(this, keyword);

            if (!Page.IsPostBack)
            {
            }
        }
        protected void search_Click(object sender, System.EventArgs e)
        {
            Fill();
        }
        protected void Fill() 
        {
            string strSql = "SELECT NOCUSTOMER, NAMA FROM MS_CUSTOMER WHERE REFFERATOR=1 AND STATUS ='A' AND NAMA LIKE '%" + Cf.Str(keyword.Text) + "%'";

			DataTable rs = Db.Rs(strSql);

            string strSqla = "SELECT NOAGENT, NAMA FROM MS_AGENT WHERE TIPE='REFFERATOR' AND NAMA LIKE '%" + Cf.Str(keyword.Text) + "%'";


            DataTable rsa = Db.Rs(strSqla);
            if (rs.Rows.Count == 0)
            {
                Rpt.NoData(rptx, rs, ("Tidak ditemukan data refferator dengan keyword diatas."));
            }
            if (rsa.Rows.Count == 0)
            {
                Rpt.NoData(rpt, rsa, ("Tidak ditemukan data refferator dengan keyword diatas."));
                   
            }
            

			for (int i = 0; i < rs.Rows.Count; i++) {
				if (!Response.IsClientConnected) break;

				TableRow r = new TableRow();
				TableCell c;

				c = new TableCell();
				c.Text = "<a href=\"javascript:call('" + rs.Rows[i]["NoCustomer"] + "','Customer')\">"
					+ rs.Rows[i]["Nocustomer"].ToString()
					+ "</a>"
					;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Nama"].ToString();
				r.Cells.Add(c);

				Rpt.Border(r);
				rptx.Rows.Add(r);
			}


            for (int i = 0; i < rsa.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                c = new TableCell();
                c.Text = "<a href=\"javascript:call2('" + rsa.Rows[i]["NoAgent"] + "','Agent')\">"
                    + rsa.Rows[i]["NoAgent"].ToString()
                    + "</a>"
                    ;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rsa.Rows[i]["Nama"].ToString();
                r.Cells.Add(c);

                Rpt.Border(r);
                rpt.Rows.Add(r);

            }

          
            }
        }
    }

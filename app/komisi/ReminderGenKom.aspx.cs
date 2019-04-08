using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.KOMISI
{
	public partial class ReminderGenKom : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();
			Fill();
            ok.HRef = "Reminder.aspx?project=" + Project;
        }
        private void Fill()
        {
            string strSql = "SELECT "
            + " NoUnit AS Unit"
            + ",MS_KONTRAK.NoKontrak AS Kontrak"
            + ",MS_CUSTOMER.Nama AS Customer"
            + ",MS_AGENT.Nama + ' ' + MS_AGENT.Principal AS Sales"
            + ",MS_AGENT.NoAgent + ' ' + MS_AGENT.Principal AS KodeSales"
            + ",TglKontrak"
            + ",NilaiKontrak AS Nilai"
            + " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER"
            + " ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
            + " INNER JOIN MS_AGENT"
            + " ON MS_KONTRAK.NoAgent = MS_AGENT.NoAgent"
            + " WHERE 1=1"
            + " AND MS_KONTRAK.Status = 'A' AND MS_KONTRAK.KomisiID = '' AND CFID != '' "
            + " AND MS_KONTRAK.Project = '" + Project + "'"
            + " ORDER BY MS_KONTRAK.NoKontrak";

            DataTable rs = Db.Rs(strSql);
            Rpt.NoData(rpt, rs, "Tidak ada logfile untuk periode tanggal tersebut.");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                DateTime Dari = Convert.ToDateTime(rs.Rows[i]["TglKontrak"]);
                DateTime Sampai = Convert.ToDateTime(rs.Rows[i]["TglKontrak"]);

                c = new TableCell();
                c.Text = "<a href=\"KomisiRegis2.aspx?Dari=" + Dari + "&Sampai=" + Sampai + "&Sales=" + rs.Rows[i]["KodeSales"].ToString()+"&Project=BTV1&NoKontrak='''+NoKontrak+'''\">" + rs.Rows[i]["Kontrak"].ToString() + "</a>";
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Str(rs.Rows[i]["Unit"]);
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Customer"].ToString();
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Sales"].ToString();
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["Nilai"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                Rpt.Border(r);
                rpt.Rows.Add(r);
            }
            if (rpt.Rows.Count == 0) kosong.InnerText = "Reminder untuk topik diatas masih kosong.";
        }
        private string Project
        {
            get
            {
                return Cf.Pk(Request.QueryString["project"]);
            }
        }

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
        #endregion
    }
}

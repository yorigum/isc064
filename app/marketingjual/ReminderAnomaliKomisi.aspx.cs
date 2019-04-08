using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
	public partial class ReminderAnomaliKomisi : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			Fill();

			if(Request.QueryString["done"]!=null)
			{
				feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
					+ "<a href=\"javascript:popJadwalKomisi('"+Request.QueryString["done"]+"')\">"
					+ "Generate Komisi Berhasil..."
					+ "</a>";
			}
		}

		private void Fill()
		{
			string strSql = "SELECT"
				+ " NoKontrak"
				+ ",NoUnit"
				+ ",MS_CUSTOMER.Nama AS Cs"
				+ ",MS_AGENT.Nama + ' ' + MS_AGENT.Principal AS Ag"
				+ ",NilaiKontrak"
				+ " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER"
				+ " ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
				+ " INNER JOIN MS_AGENT"
				+ " ON MS_KONTRAK.NoAgent = MS_AGENT.NoAgent"
				+ " WHERE MS_KONTRAK.FlagKomisi = 2"
				+ " ORDER BY MS_KONTRAK.NoKontrak";
			
			DataTable rs = Db.Rs(strSql);
			Rpt.NoData(rpt, rs, "Reminder untuk topik diatas masih kosong.");

			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				TableRow r = new TableRow();
				TableCell c;

				c = new TableCell();
				c.Text = "<a href=\"javascript:popJadwalKomisi('"+rs.Rows[i]["NoKontrak"]+"');\">"
					+ rs.Rows[i]["NoKontrak"].ToString() + "</a>";
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["NoUnit"].ToString();
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Cs"].ToString();
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Ag"].ToString();
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Num(rs.Rows[i]["NilaiKontrak"]);
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				Rpt.Border(r);
				rpt.Rows.Add(r);
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

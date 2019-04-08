using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
	public partial class ReminderPPJB : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			Fill();

			if(Request.QueryString["done"]!=null)
			{
				feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
					+ "<a href=\"javascript:popEditKontrak('"+Request.QueryString["done"]+"')\">"
					+ "PPJB Berhasil..."
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
				+ ",PersenLunas, Skema"
				+ ",(SELECT ISNULL(SUM(NilaiPelunasan),0) FROM MS_PELUNASAN WHERE NoKontrak = MS_KONTRAK.NoKontrak) AS Nilai"
				+ " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
				+ " INNER JOIN MS_AGENT"
				+ " ON MS_KONTRAK.NoAgent = MS_AGENT.NoAgent"
				+ " WHERE 1=1"
				+ " AND MS_KONTRAK.PPJB = 'B' AND PersenLunas >= 30 AND MS_KONTRAK.Status = 'A'"
				+ " ORDER BY MS_KONTRAK.NoKontrak";
			
			DataTable rs = Db.Rs(strSql);
			Rpt.NoData(rpt, rs, "Reminder untuk topik diatas masih kosong.");

			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				TableRow r = new TableRow();
				TableCell c;

				c = new TableCell();
				c.Text = "<a href=\"KontrakPPJB.aspx?NoKontrak="+rs.Rows[i]["NoKontrak"]+"\">"
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
				c.Text = Cf.Num(rs.Rows[i]["PersenLunas"]) + "%";
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Num(rs.Rows[i]["Nilai"]);
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Skema"].ToString();
				r.Cells.Add(c);

				// int NoTagihan = Db.SingleInteger("SELECT NoTagihan FROM MS_PELUNASAN WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"].ToString() + "' ORDER BY NoTagihan DESC");
				// string sSQL = "SELECT TglJT FROM MS_TAGIHAN WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"].ToString() + "' AND NoUrut = " + NoTagihan + "";
				
				//DataTable rsJT = Db.Rs(sSQL);
				//if (rsJT.Rows.Count > 0)
				//{
				//	DateTime tglJT = Convert.ToDateTime(rsJT.Rows[i]["TglJT"]);
				

				//	c = new TableCell();
				//	c.Text =Cf.Day(tglJT);
				//	r.Cells.Add(c);
				//}

				
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

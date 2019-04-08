using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
	public partial class ReminderObs : System.Web.UI.Page
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			Fill();
		}

		private void Fill()
		{
			string strSql = "SELECT *"
				+ " FROM MS_RESERVASI_OBS"
				+ " WHERE Reminder = 0"
				+ " ORDER BY NoObs";
			
			DataTable rs = Db.Rs(strSql);
			Rpt.NoData(rpt, rs, "Reminder untuk topik diatas masih kosong.");

			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				TableRow r = new TableRow();
				TableCell c;

				c = new TableCell();
				c.Text = rs.Rows[i]["NoObs"].ToString().PadLeft(5,'0');
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["NoUnit"].ToString()
					+ "<br /><i>NUP: " + rs.Rows[i]["NoQueue"].ToString() + "</i>"
					+ "<br /><strong><font style='font-size: 15pt;'>" + rs.Rows[i]["NoUrut"].ToString() + "</font></strong>"
					;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Str(rs.Rows[i]["Customer"])
					+ "<br />" + Cf.Str(rs.Rows[i]["Agent"])
					;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Date(rs.Rows[i]["Tgl"])
					+ "<br />" + Cf.Date(rs.Rows[i]["TglExpire"])
					;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Str(rs.Rows[i]["Skema"])
					+ "<br />" + Cf.Num(rs.Rows[i]["Netto"])
					;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = "<a href='ReminderObsProses.aspx?NoObs=" + Cf.Pk(rs.Rows[i]["NoObs"]) + "' onclick=\"javascript:return confirm('Anda yakin ingin follow up?');\" />Follow up...</a>";
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

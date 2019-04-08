using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.SETTINGS
{
	public partial class DaftarUserAktif : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			Fill();
		}

		private void Fill()
		{
			string strSql = "SELECT"
				+ " Nama"
				+ ",UserID"
				+ ",SecLevel"
				+ " FROM USERNAME WHERE Status = 'A'"
				+ " ORDER BY Nama, UserID";

			DataTable rs = Db.Rs(strSql);
			Rpt.NoData(rpt, rs, "Tidak ada username dengan status AKTIF.");

			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				TableRow r = new TableRow();
				TableCell c;
				
				c = new TableCell();
				c.Text = "<a style='font-weight:bold;' href=\"javascript:call('"+rs.Rows[i]["UserID"]+"')\">"
					+ rs.Rows[i]["Nama"] + "</a>";
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["UserID"].ToString();
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["SecLevel"].ToString();
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

using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
	public partial class DaftarReservasi : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			Js.ConfirmKeyword(this, keyword);

			if(!Page.IsPostBack)
			{
				if(Request.QueryString["status"]==null)
					metode.SelectedIndex = 0;
				else if(Request.QueryString["status"]=="a")
					metode.SelectedIndex = 1;
				else if(Request.QueryString["status"]=="e")
					metode.SelectedIndex = 2;

				if(metode.SelectedIndex!=0) metode.Enabled = false;
			}
		}

		protected void search_Click(object sender, System.EventArgs e)
		{
			Fill();
		}

		private void Fill()
		{
			string addq = "";
			if(metode.SelectedIndex==1)
				addq = " AND MS_RESERVASI.Status = 'A'";
			else if(metode.SelectedIndex==2)
				addq = " AND MS_RESERVASI.Status = 'E'";

			string strSql = "SELECT"
				+ " NoReservasi"
				+ ",NoUrut"
				+ ",Tgl"
				+ ",TglExpire"
				+ ",NoUnit"
				+ ",MS_RESERVASI.Status"
				+ ",MS_CUSTOMER.Nama AS Cs"
				+ ",MS_AGENT.Nama + ' ' + MS_AGENT.Principal AS Ag"
				+ " FROM MS_RESERVASI INNER JOIN MS_CUSTOMER ON MS_RESERVASI.NoCustomer = MS_CUSTOMER.NoCustomer"
				+ " INNER JOIN MS_AGENT ON MS_RESERVASI.NoAgent = MS_AGENT.NoAgent"	
				+ " WHERE CONVERT(varchar,NoReservasi) + NoUnit + MS_CUSTOMER.Nama + MS_AGENT.Nama + MS_AGENT.Principal "
				+ " LIKE '%" + Cf.Str(keyword.Text) +"%'"
				+ addq
				+ " ORDER BY NoReservasi";

			DataTable rs = Db.Rs(strSql);
			Rpt.NoData(rpt, rs, "Tidak ditemukan data reservasi dengan keyword diatas.");

			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				TableRow r = new TableRow();
				TableCell c;

				c = new TableCell();
				c.Text = "<a href=\"javascript:call('" + rs.Rows[i]["NoReservasi"] + "')\">"
					+ rs.Rows[i]["NoReservasi"].ToString().PadLeft(5,'0') + "</a>";
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["NoUnit"].ToString();
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["NoUrut"].ToString();
				c.Font.Size = 15;
				c.Font.Bold = true;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Day(rs.Rows[i]["Tgl"]);
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Cs"].ToString() + "<br>"
					+ rs.Rows[i]["Ag"].ToString();
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Date(rs.Rows[i]["TglExpire"]);
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

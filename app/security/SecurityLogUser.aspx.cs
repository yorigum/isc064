using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.SECURITY
{
	public partial class SecurityLogUser : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			if(!Page.IsPostBack)
			{
				Cf.BindTahun(tahun);
				Cf.BindBulan(bulan);

				tahun.SelectedValue = DateTime.Today.Year.ToString();
				bulan.SelectedValue = DateTime.Today.Month.ToString();
			}
		}

		protected void display_Click(object sender, System.EventArgs e)
		{
			//SetHeader();
			FillTb();
		}

		private void SetHeader()
		{
			//Header
			TableRow r = new TableRow();
			TableCell c = new TableCell();
			c.Text = tahun.SelectedItem.Text + " " + bulan.SelectedItem.Text;
			c.Font.Size = 15;
			c.Font.Bold = true;
			c.ColumnSpan = 4;
			r.Cells.Add(c);
			rpt.Rows.Add(r);
		}

		private void FillTb()
		{
			string strSql = "SELECT "
				+ " Tgl"
				+ ",CASE Aktivitas"
				+ "		WHEN 'L' THEN 'Log-In Normal'"
				+ "		WHEN 'S' THEN 'Sign-Out Normal'"
				+ "		WHEN 'DL' THEN 'Double Login'"
				+ "		WHEN 'SP' THEN 'Salah Password'"
				+ "		WHEN 'B' THEN 'Blokir'"
				+ "		WHEN 'A' THEN 'Aktivasi'"
				+ "		WHEN 'GP' THEN 'Ganti Password'"
				+ " END AS Aktivitas"
				+ ",IP"
				+ " FROM SECURITY_LOG"
				+ " WHERE MONTH(Tgl) = " + bulan.SelectedValue
				+ " AND YEAR(Tgl) = " + tahun.SelectedValue
				+ " AND UserID = '" + UserID + "'"
				+ " ORDER BY LogID";
			
			DataTable rs = Db.Rs(strSql);
			Rpt.NoData(rpt, rs, "Tidak ada security log untuk periode diatas.");

			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				TableRow r = new TableRow();
				TableCell c;

				DateTime d = Convert.ToDateTime(rs.Rows[i]["Tgl"]);
				
				c = new TableCell();
				c.Text = d.Day.ToString();
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Time(d);
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Aktivitas"].ToString();
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["IP"].ToString();
				r.Cells.Add(c);

				Rpt.Border(r);
				rpt.Rows.Add(r);
			}
		}

		private string UserID
		{
			get
			{
				return Cf.Pk(Request.QueryString["UserID"]);
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

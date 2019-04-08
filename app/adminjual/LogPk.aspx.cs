using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.ADMINJUAL
{
	public partial class LogPk : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			if(!Page.IsPostBack)
			{
				tb.SelectedValue = Tb;
				Fill();
			}
		}

		private void Fill()
		{
            string strSql = "SELECT"
                + " a.LogID"
                + ",a.Tgl"
                + ",a.Aktivitas"
                + ",a.UserID"
                + ",a.IP"
                + ",b.Nama"
                + ",a.Pk"
                + ",a.Approve"
                + " FROM " + Tb + " a JOIN "+Mi.DbPrefix+"SECURITY..USERNAME b ON a.UserID = b.UserID WHERE a.Pk = '"+ Pk +"' ORDER BY LogID";
            
			DataTable rs = Db.Rs(strSql);
			Rpt.NoData(rpt, rs, "Log file tidak tersedia.");

			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				TableRow r = new TableRow();
				TableCell c;

				c = new TableCell();
				c.Text = Cf.Day(rs.Rows[i]["Tgl"]);
				c.Wrap = false;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Time(rs.Rows[i]["Tgl"]);
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["UserID"].ToString();
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Nama"].ToString();
				c.Wrap = false;
				r.Cells.Add(c);

				c = new TableCell();
                c.Text = "<a href=\"javascript:popLog('" + rs.Rows[i]["LogID"] + "','" + Tb + "','" + Tb + "','" + Pk + "')\">"
					+ rs.Rows[i]["Aktivitas"].ToString()
					+ "</a>";
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Pk"].ToString();
				c.Wrap = false;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Approve"].ToString();
				c.Wrap = false;
				r.Cells.Add(c);

				Rpt.Border(r);
				rpt.Rows.Add(r);
			}
		}

		private string Tb
		{
			get
			{
				return Cf.Pk(Request.QueryString["tb"]);
			}
		}

		private string Pk
		{
			get
			{
				return Cf.Pk(Request.QueryString["Pk"]);
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

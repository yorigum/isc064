using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.ADMINJUAL
{
	public partial class AgentHistori : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			Act.CekInt("NoAgent");

			if(!Page.IsPostBack)
			{
				FillRsv(); //reservasi
				FillTb(); //kontrak
			}
		}

		private void FillRsv()
		{	
			string strSql = "SELECT "
				+ " NoUrut"
				+ ",Tgl"
				+ ",MS_CUSTOMER.Nama AS Cs"
				+ ",NoUnit"
				+ ",MS_RESERVASI.Status"
				+ ",NoReservasi"
				+ ",MS_RESERVASI.TglExpire"
				+ " FROM MS_RESERVASI INNER JOIN MS_CUSTOMER ON MS_RESERVASI.NoCustomer = MS_CUSTOMER.NoCustomer"
				+ " WHERE NoAgent = " + NoAgent
				+ " ORDER BY NoUrut";


			DataTable rs = Db.Rs(strSql);
			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				TableRow r = new TableRow();
				TableCell c;

				c = new TableCell();
				c.Text = rs.Rows[i]["NoReservasi"].ToString().PadLeft(5,'0');
                c.Wrap = false;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Status"].ToString();
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Day(rs.Rows[i]["Tgl"]);
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Cs"].ToString();
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["NoUnit"].ToString();
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = "W.List : " + rs.Rows[i]["NoUrut"]
					+ ", " + Cf.Date(rs.Rows[i]["TglExpire"]);
				c.Wrap = false;
				r.Cells.Add(c);

				Rpt.Border(r);
				rpt.Rows.Add(r);
			}
		}

		private void FillTb()
		{	
			string strSql = "SELECT "
				+ " NoKontrak"
				+ ",TglKontrak"
				+ ",MS_CUSTOMER.Nama AS Cs"
				+ ",NoUnit"
				+ ",MS_KONTRAK.Status"
				+ " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
				+ " WHERE NoAgent = " + NoAgent
				+ " ORDER BY NoKontrak";
			DataTable rs = Db.Rs(strSql);

			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				TableRow r = new TableRow();
				TableCell c;

				c = new TableCell();
				c.Text = rs.Rows[i]["NoKontrak"].ToString();
                c.Wrap = false;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Status"].ToString();
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Day(rs.Rows[i]["TglKontrak"]);
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Cs"].ToString();
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["NoUnit"].ToString();
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = "";
				c.Wrap = false;
				r.Cells.Add(c);

				Rpt.Border(r);
				rpt.Rows.Add(r);
			}
		}

		private string NoAgent
		{
			get
			{
				return Cf.Pk(Request.QueryString["NoAgent"]);
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

using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
	public partial class CustomerHistori : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			Act.CekInt("NoCustomer");

			if(!Page.IsPostBack)
			{
				Func.CustomerPassword(NoCustomer); //Custom SECURITY
				FillRsv();
				FillKontrak();
			}
		}

		private void FillRsv()
		{	
			string strSql = "SELECT "
				+ " NoUrut"
				+ ",Tgl"
				+ ",NoUnit"
				+ ",MS_AGENT.Nama + ' ' + MS_AGENT.Principal AS Ag"
				+ ",MS_RESERVASI.Status"
				+ ",MS_RESERVASI.NoReservasi"
				+ ",MS_RESERVASI.Netto"
				+ ",MS_RESERVASI.TglExpire"
				+ " FROM MS_RESERVASI INNER JOIN MS_AGENT ON MS_RESERVASI.NoAgent = MS_AGENT.NoAgent"
				+ " WHERE NoCustomer = " + NoCustomer
				+ " ORDER BY NoReservasi";
			
			DataTable rs = Db.Rs(strSql);
			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				TableRow r = new TableRow();
				TableCell c;

				c = new TableCell();
				c.Text = "<a href='ReservasiEdit.aspx?NoReservasi="+rs.Rows[i]["NoReservasi"]+"'>"
					+ rs.Rows[i]["NoReservasi"].ToString().PadLeft(5,'0')
					+ "</a>"
					;
                c.Wrap = false;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Status"].ToString();
                c.Wrap = false;
                r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Day(rs.Rows[i]["Tgl"]);
                c.Wrap = false;
                r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["NoUnit"].ToString();
                c.Wrap = false;
                r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Ag"].ToString();
                c.Wrap = false;
                r.Cells.Add(c);

				c = new TableCell();
				c.Text = "W.List : " + rs.Rows[i]["NoUrut"]
					+ ", " + Cf.Date(rs.Rows[i]["TglExpire"]);
				c.Wrap = false;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Num(rs.Rows[i]["Netto"]);
				c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

				Rpt.Border(r);
				rpt.Rows.Add(r);
			}
		}

		private void FillKontrak()
		{
			string strSql = "SELECT "
				+ " NoKontrak"
				+ ",TglKontrak"
				+ ",NoUnit"
				+ ",NilaiKontrak"
				+ ",Luas"
				+ ",MS_KONTRAK.Status"
				+ ",MS_AGENT.Nama + ' ' + MS_AGENT.Principal AS Ag"
				+ " FROM MS_KONTRAK INNER JOIN MS_AGENT ON MS_KONTRAK.NoAgent = MS_AGENT.NoAgent"
				+ " WHERE NoCustomer = " + NoCustomer
				+ " ORDER BY NoKontrak";
			
			DataTable rs = Db.Rs(strSql);
			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				TableRow r = new TableRow();
				TableCell c;

				c = new TableCell();
				c.Text = "<a href='KontrakEdit.aspx?NoKontrak="+rs.Rows[i]["NoKontrak"]+"'>"
					+ rs.Rows[i]["NoKontrak"].ToString() + "</a>";
                c.Wrap = false;
                r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Status"].ToString();
                c.Wrap = false;
                r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Day(rs.Rows[i]["TglKontrak"]);
				c.Wrap = false;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["NoUnit"] + " ("+Cf.Num(rs.Rows[i]["Luas"])+" m2)";
                c.Wrap = false;
                r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Ag"].ToString();
                c.Wrap = false;
                r.Cells.Add(c);

				c = new TableCell();
				c.Text = "";
				c.Wrap = false;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Num(rs.Rows[i]["NilaiKontrak"]);
				c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

				Rpt.Border(r);
				rpt.Rows.Add(r);
			}
		}

		private string NoCustomer
		{
			get
			{
				return Cf.Pk(Request.QueryString["NoCustomer"]);
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

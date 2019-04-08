using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
	public partial class UnitWL : System.Web.UI.Page
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			if(!Page.IsPostBack)
			{
				FillTb();
			}
		}

		private void FillTb()
		{
			string strSql = "SELECT "
				+ " NoUrut"
				+ ",Tgl"
				+ ",TglExpire"
				+ ",MS_CUSTOMER.Nama AS Cs"
				+ ",MS_AGENT.Nama + ' ' + MS_AGENT.Principal AS Ag"
				+ ",MS_RESERVASI.NoReservasi"
				+ ",MS_RESERVASI.Netto"
				+ ",MS_RESERVASI.Skema"
				+ ",MS_RESERVASI.Status"
				+ " FROM MS_RESERVASI INNER JOIN MS_CUSTOMER ON MS_RESERVASI.NoCustomer = MS_CUSTOMER.NoCustomer"
				+ " INNER JOIN MS_AGENT ON MS_RESERVASI.NoAgent = MS_AGENT.NoAgent"
				+ " WHERE NoStock = '" + NoStock + "'"
				+ " ORDER BY NoUrut";
			
			DataTable rs = Db.Rs(strSql);
			Rpt.NoData(rpt, rs, "Unit tidak memiliki waiting list.");

			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				if((int)rs.Rows[i]["NoUrut"]==1)
					win.Text = rs.Rows[i]["Cs"].ToString();

				TableRow r = new TableRow();
				TableCell c;

				c = new TableCell();
				c.Text = rs.Rows[i]["NoUrut"].ToString();
				c.Font.Size = 15;
				c.Font.Bold = true;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = "<input type='button' class='btn btn-blue' value='Promote'"
					+ "onclick=\"promote('"+rs.Rows[i]["NoReservasi"]+"')\">";
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = "<a href='ReservasiEdit.aspx?NoReservasi="+rs.Rows[i]["NoReservasi"]+"'>"
					+ rs.Rows[i]["NoReservasi"].ToString().PadLeft(5,'0') + "</a>";
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Day(rs.Rows[i]["Tgl"]);
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Date(rs.Rows[i]["TglExpire"]);
				if((string)rs.Rows[i]["Status"]=="E")
					c.Text = c.Text + "<br><b class=err>Expire</b>";
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Cs"].ToString()
					+ "<br>" + rs.Rows[i]["Ag"]
					;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Skema"].ToString()
					+ "<br>" + Cf.Num(rs.Rows[i]["Netto"])
					;
				r.Cells.Add(c);

				Rpt.Border(r);
				rpt.Rows.Add(r);
			}
		}

		private string NoStock
		{
			get
			{
				return Cf.Pk(Request.QueryString["NoStock"]);
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

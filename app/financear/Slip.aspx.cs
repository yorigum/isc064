using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.FINANCEAR
{
	public partial class Slip : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			if(!IsPostBack)
				Fill();
		}

		protected void Fill()
		{
			string strSql = "SELECT DISTINCT NoSlip"
				+ " FROM MS_TTS"
				+ " WHERE NoSlip <> 0"
				;
			DataTable rs = Db.Rs(strSql);

			Rpt.NoData(tb, rs, "Tidak ada slip setoran.");

			for(int i = 0; i < rs.Rows.Count; i++)
			{
				if(!Response.IsClientConnected)
					break;

				TableRow r = new TableRow();
				TableCell c;

				c = new TableCell();
				c.Text = "<a href='SlipLaporan.aspx?NoSlip=" + rs.Rows[i]["NoSlip"].ToString() + "'>"
					+ rs.Rows[i]["NoSlip"].ToString().PadLeft(7, '0')
					+ "</a>"
					;
				c.HorizontalAlign = HorizontalAlign.Center;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Db.SingleInteger(
					"SELECT COUNT(NoTTS)"
					+ " FROM MS_TTS"
					+ " WHERE NoSlip = " + rs.Rows[i]["NoSlip"].ToString()
					).ToString();
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Db.SingleInteger(
					"SELECT COUNT(DISTINCT NoBG)"
					+ " FROM MS_TTS"
					+ " WHERE NoSlip = " + rs.Rows[i]["NoSlip"].ToString()
					).ToString();
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Num(Db.SingleDecimal("SELECT ISNULL(SUM(Total), 0) FROM MS_TTS WHERE NoSlip = " + rs.Rows[i]["NoSlip"].ToString()));
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				tb.Rows.Add(r);
				Rpt.Border(r);
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

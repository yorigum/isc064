using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.COLLECTION
{
	public partial class TunggakanTagihan : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			Act.CekInt("NoTunggakan");

			if(!Page.IsPostBack)
			{
				FillTable();
			}
		}

		private void FillTable()
		{
			decimal t = 0;

			DataTable rs = Db.Rs("SELECT * FROM MS_TUNGGAKAN_DETIL WHERE NoTunggakan = " + NoTunggakan);
			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				TableRow r = new TableRow();
				TableCell c;

				c = new TableCell();
				c.Text = Cf.Day(rs.Rows[i]["TglJT"]);
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Telat"] + " Hari";
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["NamaTagihan"].ToString();
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Num(rs.Rows[i]["Nilai"]);
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				t = t + (decimal)rs.Rows[i]["Nilai"];

				Rpt.Border(r);
				rpt.Rows.Add(r);

				if(i==rs.Rows.Count-1)
					SubTotal(t);
			}
		}

		private void SubTotal(decimal t)
		{
			TableRow r = new TableRow();
			TableCell c;

			c = new TableCell();
			c.Text = "GRAND TOTAL";
			c.ColumnSpan = 3;
			c.Font.Bold = true;
			r.Cells.Add(c);

			c = new TableCell();
			c.Text = Cf.Num(t);
			c.HorizontalAlign = HorizontalAlign.Right;
			c.Font.Bold = true;
			r.Cells.Add(c);

			rpt.Rows.Add(r);
		}

		private string NoTunggakan
		{
			get
			{
				return Cf.Pk(Request.QueryString["NoTunggakan"]);
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

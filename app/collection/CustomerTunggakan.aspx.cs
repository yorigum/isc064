using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.COLLECTION
{
	public partial class CustomerTunggakan : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			if(!Page.IsPostBack)
			{
				FillTable();
			}
		}

		private void FillTable()
		{
			string strSql = "SELECT *"
				+ ",CASE Status "
				+ "		WHEN 'A' THEN 'AKTIF' "
				+ "		WHEN 'S' THEN 'SETTLED' "
				+ "		WHEN 'U' THEN 'UPGRADED' "
				+ " END AS Status1"
				+ " FROM MS_TUNGGAKAN WHERE Tipe = '"+Tipe+"' AND Ref = '"+Ref+"'";
			DataTable rs = Db.Rs(strSql);

			decimal t1 = 0;

			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				TableRow r = new TableRow();
				TableCell c;

				c = new TableCell();
				c.Text = "<a href='TunggakanEdit.aspx?NoTunggakan="+rs.Rows[i]["NoTunggakan"]+"'>"
					+ rs.Rows[i]["ManualTunggakan"].ToString().PadLeft(7,'0') + "</a>";
                c.Wrap = false;
                r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Day(rs.Rows[i]["TglTunggakan"]);
                c.Wrap = false;
                r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["LevelTunggakan"] + ", " + rs.Rows[i]["Status1"];
                c.Wrap = false;
                r.Cells.Add(c);

				c = new TableCell();
				c.Text = Ket((int)rs.Rows[i]["NoTunggakan"]);
                c.Wrap = false;
                r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Num(rs.Rows[i]["Total"]);
				c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

				Rpt.Border(r);
				rpt.Rows.Add(r);

				t1 = t1 + (decimal)rs.Rows[i]["Total"];
				
				if(i==rs.Rows.Count-1)
					SubTotal(t1);
			}
		}

		private string Ket(int NoTunggakan)
		{
			System.Text.StringBuilder x = new System.Text.StringBuilder();

			string strSql = "SELECT * FROM MS_TUNGGAKAN_DETIL WHERE NoTunggakan = "+NoTunggakan;
			DataTable rs = Db.Rs(strSql);

			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				x.Append("<p>"+rs.Rows[i]["NamaTagihan"]+"&nbsp;&nbsp;"
					+ Cf.Day(rs.Rows[i]["TglJT"])
					+ "&nbsp;&nbsp; ("+Cf.Num(rs.Rows[i]["Nilai"])+")</p>");
			}

			return x.ToString();
		}

		private void SubTotal(decimal t1)
		{
			TableRow r = new TableRow();
			TableCell c;

			c = new TableCell();
			c.ColumnSpan = 4;
			c.Text = "<b>GRAND TOTAL</b>";
			r.Cells.Add(c);

			c = new TableCell();
			c.Font.Bold = true;
			c.Text = Cf.Num(t1);
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			rpt.Rows.Add(r);
		}

		private string Tipe
		{
			get
			{
				return Cf.Pk(Request.QueryString["Tipe"]);
			}
		}

		private string Ref
		{
			get
			{
				return Cf.Pk(Request.QueryString["Ref"]);
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

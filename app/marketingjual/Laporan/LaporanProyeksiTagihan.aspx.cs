using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL.Laporan
{
	public partial class LaporanProyeksiTagihan : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();

			if(!Page.IsPostBack)
			{
				comp.InnerHtml = Mi.Pt;
				rpt.Visible = false;
				Js.Focus(this,scr);
				Cf.BindTahun(thn);
				thn.SelectedValue = DateTime.Today.Year.ToString();

				if(!Act.Sec("DownloadExcel"))
					xls.Enabled = false;
			}
		}

		protected void Report()
		{
			param.Visible = false;
			rpt.Visible = true;

			Header();
			Fill();
		}

		protected void Header()
		{
			System.Text.StringBuilder x = new System.Text.StringBuilder();

			Rpt.Judul(x, comp, judul);
			
			Rpt.SubJudul(x
				, "Tahun : " + thn.SelectedValue
				);

			Rpt.Header(rpt, x);
		}

		protected void Fill()
		{
			decimal t = 0, Total = 0;
			
			for(int i = 1; i <= 12; i++)
			{
				if(!Response.IsClientConnected)
					break;

				TableRow r = new TableRow();
				TableCell c;

				c = new TableCell();
				c.Text = Cf.Monthname(i);
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				t = Proyeksi(i);
				c.Text = Cf.Num(t);
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				rpt.Rows.Add(r);

				Total += t;

				if(i == 12)
					SubTotal(Total);
			}
		}

		protected decimal Proyeksi(int i)
		{
			string strSql = "SELECT ISNULL(SUM(NilaiTagihan), 0)"
				+ " FROM MS_TAGIHAN a"
				+ " INNER JOIN MS_KONTRAK b ON a.NoKontrak = b.NoKontrak"
				+ " WHERE MONTH(TglJT) = " + i
				+ " AND YEAR(TglJT) = " + thn.SelectedValue
				+ " AND Status = 'A'"
				;
			decimal x = Db.SingleDecimal(strSql);

			return x;
		}

		protected void SubTotal(decimal t)
		{
			TableRow r = new TableRow();
			TableCell c;

			c = Rpt.Foot();
			c.Text = "<strong>TOTAL</strong>";
			c.HorizontalAlign = HorizontalAlign.Left;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = "<strong>" + Cf.Num(t) + "</strong>";
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			rpt.Rows.Add(r);
		}

		protected void scr_Click(object sender, System.EventArgs e)
		{
			Report();
		}

		protected void xls_Click(object sender, System.EventArgs e)
		{
			Report();
			Rpt.ToExcel(this, rpt);
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

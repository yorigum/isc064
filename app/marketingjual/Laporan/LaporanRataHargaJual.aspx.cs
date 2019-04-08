using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL.Laporan
{
	public partial class LaporanRataHargaJual : System.Web.UI.Page
	{
		
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();

			if(!Page.IsPostBack)
			{
				comp.InnerHtml = Mi.Pt;
				rpt.Visible = false;
				Js.Focus(this,scr);
				if(!Act.Sec("DownloadExcel")) xls.Enabled = false;
			}
		}

		private bool valid()
		{
			string s = "";
			bool x = true;

			if(!x && s!="")
			{
				RegisterStartupScript("err"
					,"<script language='javascript'>document.getElementById('"+s+"').select()</script>");
			}

			return x;
		}

		private void Report()
		{
			param.Visible = false;
			rpt.Visible = true;

			lblHeader.Text = Mi.Pt
				+ "<br />"
				+ "LAPORAN RATA-RATA HARGA JUAL PER TOWER"
				+ "<br />"
				+ "PER " + Cf.Day(DateTime.Today)
				;
			
			Fill();
		}

		private void Fill()
		{
			string strSql = "SELECT DISTINCT(Lokasi)"
				+ " FROM MS_UNIT"
				;
			DataTable rs = Db.Rs(strSql);

			decimal NilaiSold = 0, LuasSold = 0, RataSold = 0;
			decimal NilaiTotal = 0, LuasTotal = 0, RataTotal = 0;
			decimal t1 = 0, t2 = 0, t3 = 0, t4 = 0, t5 = 0, t6 = 0;

			for(int i = 0; i < rs.Rows.Count; i++)
			{
				if(!Response.IsClientConnected)
					break;

				TableRow r = new TableRow();
				TableCell c;

				c = new TableCell();
				c.Text = rs.Rows[i]["Lokasi"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				NilaiTotal = Nilai(rs.Rows[i]["Lokasi"].ToString(), "TOTAL");
				c.Text = Cf.Num(NilaiTotal);
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				c = new TableCell();
				LuasTotal = Luas(rs.Rows[i]["Lokasi"].ToString(), "TOTAL");
				c.Text = Cf.Num(LuasTotal);
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				c = new TableCell();
				RataTotal = Rata(rs.Rows[i]["Lokasi"].ToString(), NilaiTotal, LuasTotal, "TOTAL");
				c.Text = Cf.Num(RataTotal);
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				c = new TableCell();
				NilaiSold = Nilai(rs.Rows[i]["Lokasi"].ToString(), "SOLD"); 
				c.Text = Cf.Num(NilaiSold);
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				c = new TableCell();
				LuasSold = Luas(rs.Rows[i]["Lokasi"].ToString(), "SOLD");
				c.Text = Cf.Num(LuasSold);
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				c = new TableCell();
				RataSold = Rata(rs.Rows[i]["Lokasi"].ToString(), NilaiSold, LuasSold, "SOLD");
				c.Text = Cf.Num(RataSold);
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				t1 += NilaiTotal;
				t2 += LuasTotal;
				t3 += RataTotal;
				t4 += NilaiSold;
				t5 += LuasSold;
				t6 += RataSold;

				rpt.Rows.Add(r);

				if(i == (rs.Rows.Count - 1))
					SubTotal(t1, t2, t3, t4, t5, t6);
			}
		}

		protected decimal Nilai(string Lokasi, string Tipe)
		{
			decimal t = 0;
			
			if(Tipe == "SOLD")
			{
				t = Db.SingleDecimal(
					"SELECT ISNULL(SUM(NilaiKontrak), 0)"
					+ " FROM MS_KONTRAK"
					+ " WHERE Lokasi = '" + Lokasi + "'"
					+ " AND Status = 'A'"
					);
			}
			else if(Tipe == "TOTAL")
			{
				t = Db.SingleDecimal(
					"SELECT ISNULL(SUM(PriceList), 0)"
					+ " FROM MS_UNIT"
					+ " WHERE Lokasi = '" + Lokasi + "'"
					);
			}

			return t;
		}

		protected decimal Luas(string Lokasi, string Tipe)
		{
			decimal t = 0;

			if(Tipe == "SOLD")
			{
				t = Db.SingleDecimal(
					"SELECT ISNULL(SUM(Luas), 0)"
					+ " FROM MS_KONTRAK"
					+ " WHERE Lokasi = '" + Lokasi + "'"
					+ " AND Status = 'A'"
					);
			}
			else if(Tipe == "TOTAL")
			{
				t = Db.SingleDecimal(
					"SELECT ISNULL(SUM(Luas), 0)"
					+ " FROM MS_UNIT"
					+ " WHERE Lokasi = '" + Lokasi + "'"
					);
			}

			return t;
		}

		protected decimal Rata(string Lokasi, decimal Nilai, decimal Luas, string Tipe)
		{
			decimal t = 0;
			
			if(Luas > 0)
				t = Nilai / Luas;

			return t;
		}

		private void SubTotal(decimal t1, decimal t2, decimal t3, decimal t4, decimal t5, decimal t6)
		{
			TableRow r = new TableRow();
			TableCell c;

			c = Rpt.Foot();
			c.Text = "TOTAL";
			c.HorizontalAlign = HorizontalAlign.Left;
			c.VerticalAlign = VerticalAlign.Top;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(t1);
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(t2);
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(t3);
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(t4);
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(t5);
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(t6);
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			rpt.Rows.Add(r);
		}

		protected void scr_Click(object sender, System.EventArgs e)
		{
			if(valid())
				Report();
		}

		protected void xls_Click(object sender, System.EventArgs e)
		{
			if(valid())
			{
				Report();
				Rpt.ToExcel(this, rpt);
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

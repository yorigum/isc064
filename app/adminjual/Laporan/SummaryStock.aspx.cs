using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.ADMINJUAL.Laporan
{
	public partial class SummaryStock : System.Web.UI.Page
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
                ClientScript.RegisterStartupScript(GetType(), "err"
                    , "<script language='javascript'>document.getElementById('"+s+"').select()</script>");
			}

			return x;
		}

		private void Report()
		{
			param.Visible = false;
			rpt.Visible = true;

			lblHeader.Text = Mi.Pt
				+ "<br />"
				+ "LAPORAN SUMMARY STOCK"
				+ "<br />"
				+ "PER " + Cf.Day(DateTime.Today)
				;
			
			Fill();
		}

		private void Fill()
		{
			decimal TotalLuas = 0, TotalLuasAvailable = 0, TotalLuasSold = 0, TotalLuasHold = 0, TotalLuasBooked = 0;
			decimal TotalJumlah = 0, TotalJumlahAvailable = 0, TotalJumlahSold = 0, TotalJumlahHold = 0, TotalJumlahBooked = 0;

			TableRow r;
			TableCell c;

			//TOTAL
			r = new TableRow();

			c = new TableCell();
			c.Text = "<strong>TOTAL</strong>";
			c.VerticalAlign = VerticalAlign.Top;
			r.Cells.Add(c);

			c = new TableCell();
			c.Text = "Semua unit dan semua status";
			c.VerticalAlign = VerticalAlign.Top;
			r.Cells.Add(c);

			c = new TableCell();
			TotalLuas = Unit("TOTAL", "LUAS");
			TotalJumlah = Unit("TOTAL", "JUMLAH");
			c.Text = Cf.Num(TotalLuas)
				+ "<br />"
				+ Cf.Num(TotalJumlah) + " UNIT"
				;
			c.HorizontalAlign = HorizontalAlign.Right;
			c.VerticalAlign = VerticalAlign.Top;
			r.Cells.Add(c);

			c = new TableCell();
			c.Text = "100%"
				+ "<br />100%"
				;
			c.HorizontalAlign = HorizontalAlign.Right;
			c.VerticalAlign = VerticalAlign.Top;
			r.Cells.Add(c);

			rpt.Rows.Add(r);

			//TOTAL AVAILABLE
			r = new TableRow();

			c = new TableCell();
			c.Text = "<strong>TOTAL AVAILABLE</strong>";
			c.VerticalAlign = VerticalAlign.Top;
			r.Cells.Add(c);

			c = new TableCell();
			c.Text = "Unit dengan status tersedia saja";
			c.VerticalAlign = VerticalAlign.Top;
			r.Cells.Add(c);

			c = new TableCell();
			TotalLuasAvailable = Unit("AVAILABLE", "LUAS");
			TotalJumlahAvailable = Unit("AVAILABLE", "JUMLAH");
			c.Text = Cf.Num(TotalLuasAvailable)
				+ "<br />"
				+ Cf.Num(TotalJumlahAvailable) + " UNIT"
				;
			c.HorizontalAlign = HorizontalAlign.Right;
			c.VerticalAlign = VerticalAlign.Top;
			r.Cells.Add(c);

			c = new TableCell();
			c.Text = Cf.Num(TotalLuasAvailable / TotalLuas * 100) + "%"
				+ "<br />"
				+ Cf.Num(TotalJumlahAvailable / TotalJumlah * 100) + "%"
				;
			c.HorizontalAlign = HorizontalAlign.Right;
			c.VerticalAlign = VerticalAlign.Top;
			r.Cells.Add(c);

			rpt.Rows.Add(r);

			//TOTAL SOLD
			r = new TableRow();

			c = new TableCell();
			c.Text = "<strong>TOTAL SOLD</strong>";
			c.VerticalAlign = VerticalAlign.Top;
			r.Cells.Add(c);

			c = new TableCell();
			c.Text = "Unit dengan status terjual saja";
			c.VerticalAlign = VerticalAlign.Top;
			r.Cells.Add(c);

			c = new TableCell();
			TotalLuasSold = Unit("SOLD", "LUAS");
			TotalJumlahSold = Unit("SOLD", "JUMLAH");
			c.Text = Cf.Num(TotalLuasSold)
				+ "<br />"
				+ Cf.Num(TotalJumlahSold) + " UNIT"
				;
			c.HorizontalAlign = HorizontalAlign.Right;
			c.VerticalAlign = VerticalAlign.Top;
			r.Cells.Add(c);

			c = new TableCell();
			c.Text = Cf.Num(TotalLuasSold / TotalLuas * 100) + "%"
				+ "<br />"
				+ Cf.Num(TotalJumlahSold / TotalJumlah * 100) + "%"
				;
			c.HorizontalAlign = HorizontalAlign.Right;
			c.VerticalAlign = VerticalAlign.Top;
			r.Cells.Add(c);

			rpt.Rows.Add(r);

			//HOLD
			r = new TableRow();

			c = new TableCell();
			c.Text = "<strong>TOTAL HOLD</strong>";
			c.VerticalAlign = VerticalAlign.Top;
			r.Cells.Add(c);

			c = new TableCell();
			c.Text = "Unit dengan status hold internal saja";
			c.VerticalAlign = VerticalAlign.Top;
			r.Cells.Add(c);

			c = new TableCell();
			TotalLuasHold = Unit("HOLD", "LUAS");
			TotalJumlahHold = Unit("HOLD", "JUMLAH");
			c.Text = Cf.Num(TotalLuasHold)
				+ "<br />"
				+ Cf.Num(TotalJumlahHold) + " UNIT"
				;
			c.HorizontalAlign = HorizontalAlign.Right;
			c.VerticalAlign = VerticalAlign.Top;
			r.Cells.Add(c);

			c = new TableCell();
			c.Text = Cf.Num(TotalLuasHold / TotalLuas * 100) + "%"
				+ "<br />"
				+ Cf.Num(TotalJumlahHold / TotalJumlah * 100) + "%"
				;
			c.HorizontalAlign = HorizontalAlign.Right;
			c.VerticalAlign = VerticalAlign.Top;
			r.Cells.Add(c);

			rpt.Rows.Add(r);

			//BOOKED
			r = new TableRow();
			
			c = new TableCell();
			c.Text = "<strong>TOTAL BOOKED</strong>";
			c.VerticalAlign = VerticalAlign.Top;
			r.Cells.Add(c);

			c = new TableCell();
			c.Text = "Unit dengan status telah dipesan saja";
			c.VerticalAlign = VerticalAlign.Top;
			r.Cells.Add(c);

			c = new TableCell();
			TotalLuasBooked = Unit("BOOKED", "LUAS");
			TotalJumlahBooked = Unit("BOOKED", "JUMLAH");
			c.Text = Cf.Num(TotalLuasBooked)
				+ "<br />"
				+ Cf.Num(TotalJumlahBooked) + " UNIT"
				;
			c.HorizontalAlign = HorizontalAlign.Right;
			c.VerticalAlign = VerticalAlign.Top;
			r.Cells.Add(c);

			c = new TableCell();
			c.Text = Cf.Num(TotalLuasBooked / TotalLuas * 100) + "%"
				+ "<br />"
				+ Cf.Num(TotalJumlahBooked / TotalJumlah * 100) + "%"
				;
			c.HorizontalAlign = HorizontalAlign.Right;
			c.VerticalAlign = VerticalAlign.Top;
			r.Cells.Add(c);

			rpt.Rows.Add(r);
		}

		protected decimal Unit(string Tipe, string x)
		{
			decimal t = 0;

			if(Tipe == "TOTAL")
			{
				if(x == "LUAS")
				{
					t = Db.SingleDecimal(
						"SELECT ISNULL(SUM(Luas), 0)"
						+ " FROM MS_UNIT"
						);
				}
				else if(x == "JUMLAH")
				{
					t = Db.SingleInteger(
						"SELECT COUNT(*)"
						+ " FROM MS_UNIT"
						);
				}
			}
			else if(Tipe == "AVAILABLE")
			{
				if(x == "LUAS")
				{
					t = Db.SingleDecimal(
						"SELECT ISNULL(SUM(Luas), 0)"
						+ " FROM MS_UNIT"
						+ " WHERE Status = 'A'"
						);
				}
				else if(x == "JUMLAH")
				{
					t = Db.SingleInteger(
						"SELECT COUNT(*)"
						+ " FROM MS_UNIT"
						+ " WHERE Status = 'A'"
						);
				}
			}
			else if(Tipe == "SOLD")
			{
				if(x == "LUAS")
				{
					t = Db.SingleDecimal(
						"SELECT ISNULL(SUM(Luas), 0)"
						+ " FROM MS_KONTRAK"
						+ " WHERE Status = 'A'"
						);
				}
				else if(x == "JUMLAH")
				{
					t = Db.SingleInteger(
						"SELECT COUNT(*)"
						+ " FROM MS_KONTRAK"
						+ " WHERE Status = 'A'"
						);
				}
			}
			else if(Tipe == "HOLD")
			{
				if(x == "LUAS")
				{
					t = Db.SingleDecimal(
						"SELECT ISNULL(SUM(Luas), 0)"
						+ " FROM MS_UNIT a"
						+ " WHERE Status = 'B'"
						+ " AND (SELECT COUNT(*) FROM MS_KONTRAK WHERE NoStock = a.NoStock) = 0"
						);
				}
				else if(x == "JUMLAH")
				{
					t = Db.SingleInteger(
						"SELECT COUNT(*)"
						+ " FROM MS_UNIT a"
						+ " WHERE Status = 'B'"
						+ " AND (SELECT COUNT(*) FROM MS_KONTRAK WHERE NoStock = a.NoStock) = 0"
						);
				}
			}
			else if(Tipe == "BOOKED")
			{
				if(x == "LUAS")
				{
					t = Db.SingleDecimal(
						"SELECT ISNULL(SUM(Luas), 0)"
						+ " FROM MS_UNIT a"
						+ " INNER JOIN MS_RESERVASI b ON a.NoStock = b.NoStock"
						+ " WHERE a.Status = 'A'"
						);
				}
				else if(x == "JUMLAH")
				{
					t = Db.SingleInteger(
						"SELECT COUNT(*)"
						+ " FROM MS_UNIT a"
						+ " INNER JOIN MS_RESERVASI b ON a.NoStock = b.NoStock"
						+ " WHERE a.Status = 'A'"
						);
				}
			}

			return t;
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

using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LaporanApi
{
	public partial class LapManagementReportApi : System.Web.UI.Page
	{
        protected DateTime Tgl { get { return Convert.ToDateTime(Request.QueryString["Tgl"]); } }
        protected string UserID { get { return Request.QueryString["UserID"]; } }
        protected string Project { get { return Request.QueryString["Project"]; } }

        protected void Page_Load(object sender, System.EventArgs e)
		{
            Report();
		}

		protected void Report()
		{
			rpt.Visible = true;

			Fill();
		}

		protected void Fill()
		{
			Header();
			Marketing1();
			Marketing2();

			Label l = new Label();
			l.Text = "<br />";
			rpt.Controls.Add(l);

			Finance1();
			Finance2();
		}

		protected void Header()
		{
			Label l;

			l = new Label();
			l.Text = Mi.Pt;
			l.Text += "<h1 class='title'>Management Report</h1>";
			l.Text += "<h2>per " + Cf.Day(Tgl) + "</h2>";
			l.Text += "Laporan dihasilkan pada hari : " + Cf.IndoWeek(DateTime.Today)
				+ ", " + Cf.Date(DateTime.Now)
				+ " dari workstation : server"
				+ " dan username : " + UserID
				+ "<br /><br />";
			rpt.Controls.Add(l);
		}

		protected void Marketing1()
		{
			Table tb;
			TableRow tr;
			TableHeaderCell hc;
			TableCell c;

			tb = new Table();
			tb.CssClass = "tb blue-skin";
			tb.CellSpacing = 1;

			tr = new TableRow();
			tb.Rows.Add(tr);

			c = new TableCell();
			c.Text = "<b>A. MARKETING</b>";
			c.ColumnSpan = 3;
			tr.Cells.Add(c);

			tr = new TableRow();
			tb.Rows.Add(tr);

			c = new TableCell();
			c.Text = "<b>1. UNIT STOCK</b>";
			c.ColumnSpan = 4;
			tr.Cells.Add(c);

			tr = new TableRow();
			tb.Rows.Add(tr);

			hc = new TableHeaderCell();
			hc.Text = "Lantai";
			// hc.BackColor = Color.Gray;
			// hc.ForeColor = Color.White;
			hc.HorizontalAlign = HorizontalAlign.Center;
			tr.Cells.Add(hc);

			hc = new TableHeaderCell();
			hc.Text = "Stock Tersedia";
			// hc.BackColor = Color.Gray;
			// hc.ForeColor = Color.White;
			hc.HorizontalAlign = HorizontalAlign.Center;
			tr.Cells.Add(hc);

			hc = new TableHeaderCell();
			hc.Text = "Price List";
			// hc.BackColor = Color.Gray;
			// hc.ForeColor = Color.White;
			hc.HorizontalAlign = HorizontalAlign.Right;
			tr.Cells.Add(hc);

			rpt.Controls.Add(tb);

			DataTable rs = Db.Rs("SELECT DISTINCT Lokasi FROM ISC064_MARKETINGJUAL..MS_UNIT WHERE Project = '" + Project + "'");

			int t1 = 0;
			decimal t2 = 0;
			
			for(int i = 0; i < rs.Rows.Count; i++)
			{
				if(!Response.IsClientConnected)
					break;

				tr = new TableRow();
				tb.Rows.Add(tr);

				c = new TableCell();
				c.Text = rs.Rows[i]["Lokasi"].ToString();
				c.HorizontalAlign = HorizontalAlign.Center;
				tr.Cells.Add(c);

				c = new TableCell();
				int a = TotalUnit(rs.Rows[i]["Lokasi"].ToString());
				c.Text = a.ToString();
				c.HorizontalAlign = HorizontalAlign.Center;
				tr.Cells.Add(c);

				c = new TableCell();
				decimal b = PriceList(rs.Rows[i]["Lokasi"].ToString());
				c.Text = Cf.Num(b);
				c.HorizontalAlign = HorizontalAlign.Right;
				tr.Cells.Add(c);

				t1 += a;
				t2 += b;
			}

			tr = new TableRow();
			tb.Rows.Add(tr);

			c = Rpt.Foot();
			c.Text = "Total";
			c.HorizontalAlign = HorizontalAlign.Center;
			tr.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = t1.ToString();
			c.HorizontalAlign = HorizontalAlign.Center;
			tr.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(t2);
			c.HorizontalAlign = HorizontalAlign.Right;
			tr.Cells.Add(c);
		}

		protected void Marketing2()
		{
			Table tb;
			TableRow tr;
			TableHeaderCell hc;
			TableCell c;

			tb = new Table();
			tb.CssClass = "tb blue-skin";
			tb.CellSpacing = 1;

			tr = new TableRow();
			tb.Rows.Add(tr);

			c = new TableCell();
			c.Text = "<b>2. SALES / PENJUALAN</b>";
			c.ColumnSpan = 7;
			tr.Cells.Add(c);
			
			tr = new TableRow();
			tb.Rows.Add(tr);

			hc = new TableHeaderCell();
			hc.Text = "Lantai";
			// hc.BackColor = Color.Gray;
			// hc.ForeColor = Color.White;
			hc.RowSpan = 2;
			hc.HorizontalAlign = HorizontalAlign.Center;
			tr.Cells.Add(hc);

			hc = new TableHeaderCell();
			hc.Text = "Unit";
			// hc.BackColor = Color.Gray;
			// hc.ForeColor = Color.White;
			hc.ColumnSpan = 4;
			hc.HorizontalAlign = HorizontalAlign.Center;
			tr.Cells.Add(hc);

			hc = new TableHeaderCell();
			hc.Text = "Nilai Kontrak";
			// hc.BackColor = Color.Gray;
			// hc.ForeColor = Color.White;
			hc.RowSpan = 2;
			hc.HorizontalAlign = HorizontalAlign.Right;
			tr.Cells.Add(hc);

			hc = new TableHeaderCell();
			hc.Text = "Jumlah Dasar Booking";
			// hc.BackColor = Color.Gray;
			// hc.ForeColor = Color.White;
			hc.RowSpan = 2;
			hc.HorizontalAlign = HorizontalAlign.Right;
			tr.Cells.Add(hc);

			tr = new TableRow();
			tb.Rows.Add(tr);

			hc = new TableHeaderCell();
			hc.Text = "Stock Tersedia";
			// hc.BackColor = Color.Gray;
			// hc.ForeColor = Color.White;
			hc.HorizontalAlign = HorizontalAlign.Center;
			tr.Cells.Add(hc);

			hc = new TableHeaderCell();
			hc.Text = "Penjualan Dasar SP";
			// hc.BackColor = Color.Gray;
			// hc.ForeColor = Color.White;
			hc.HorizontalAlign = HorizontalAlign.Center;
			tr.Cells.Add(hc);

			hc = new TableHeaderCell();
			hc.Text = "Stock Booking";
			// hc.BackColor = Color.Gray;
			// hc.ForeColor = Color.White;
			hc.HorizontalAlign = HorizontalAlign.Center;
			tr.Cells.Add(hc);

			hc = new TableHeaderCell();
			hc.Text = "Stock Available";
			// hc.BackColor = Color.Gray;
			// hc.ForeColor = Color.White;
			hc.HorizontalAlign = HorizontalAlign.Center;
			tr.Cells.Add(hc);

			rpt.Controls.Add(tb);

			int t1 = 0, t2 = 0, t3 = 0, t4 = 0;
			decimal t5 = 0, t6 = 0;

			DataTable rs = Db.Rs("SELECT DISTINCT Lokasi FROM ISC064_MARKETINGJUAL..MS_UNIT WHERE Project = '" + Project + "' ORDER BY Lokasi");
			for(int i = 0; i < rs.Rows.Count; i++)
			{
				if(!Response.IsClientConnected)
					break;

				tr = new TableRow();
				tb.Rows.Add(tr);

				c = new TableCell();
				c.Text = rs.Rows[i]["Lokasi"].ToString();
				c.HorizontalAlign = HorizontalAlign.Center;
				tr.Cells.Add(c);

				c = new TableCell();
				int a = TotalUnit(rs.Rows[i]["Lokasi"].ToString());
				c.Text = a.ToString();
				c.HorizontalAlign = HorizontalAlign.Center;
				tr.Cells.Add(c);

				c = new TableCell();
				int b = SoldUnit(rs.Rows[i]["Lokasi"].ToString());
				c.Text = b.ToString();
				c.HorizontalAlign = HorizontalAlign.Center;
				tr.Cells.Add(c);

				c = new TableCell();
				int x = BookedUnit(rs.Rows[i]["Lokasi"].ToString());
				c.Text = x.ToString();
				c.HorizontalAlign = HorizontalAlign.Center;
				tr.Cells.Add(c);

				c = new TableCell();
				int y = AvailableUnit(rs.Rows[i]["Lokasi"].ToString());
				c.Text = y.ToString();
				c.HorizontalAlign = HorizontalAlign.Center;
				tr.Cells.Add(c);

				c = new TableCell();
				decimal d = NilaiKontrak(rs.Rows[i]["Lokasi"].ToString());
				c.Text = Cf.Num(d);
				c.HorizontalAlign = HorizontalAlign.Right;
				tr.Cells.Add(c);

				c = new TableCell();
				decimal e = NilaiReservasi(rs.Rows[i]["Lokasi"].ToString());
				c.Text = Cf.Num(e);
				c.HorizontalAlign = HorizontalAlign.Right;
				tr.Cells.Add(c);

				t1 += a;
				t2 += b;
				t3 += x;
				t4 += y;
				t5 += d;
				t6 += e;
			}

			tr = new TableRow();
			tb.Rows.Add(tr);

			c = Rpt.Foot();
			c.Text = "Total";
			c.HorizontalAlign = HorizontalAlign.Center;
			tr.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = t1.ToString();
			c.HorizontalAlign = HorizontalAlign.Center;
			tr.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = t2.ToString();
			c.HorizontalAlign = HorizontalAlign.Center;
			tr.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = t3.ToString();
			c.HorizontalAlign = HorizontalAlign.Center;
			tr.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = t4.ToString();
			c.HorizontalAlign = HorizontalAlign.Center;
			tr.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(t5);
			c.HorizontalAlign = HorizontalAlign.Right;
			tr.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(t6);
			c.HorizontalAlign = HorizontalAlign.Right;
			tr.Cells.Add(c);
		}

		protected void Finance1()
		{
			Table tb;
			TableRow tr;
			TableHeaderCell hc;
			TableCell c;

			tb = new Table();
			tb.CssClass = "tb blue-skin";
			tb.CellSpacing = 1;

			tr = new TableRow();
			tb.Rows.Add(tr);

			c = new TableCell();
			c.Text = "<b>B. FINANCE</b>";
			c.ColumnSpan = 4;
			tr.Cells.Add(c);

			tr = new TableRow();
			tb.Rows.Add(tr);

			c = new TableCell();
			c.Text = "<b>1. COLLECTION</b>";
			c.ColumnSpan = 4;
			tr.Cells.Add(c);
			
			tr = new TableRow();
			tb.Rows.Add(tr);

			hc = new TableHeaderCell();
			hc.Text = "Lantai";
			// hc.BackColor = Color.Gray;
			// hc.ForeColor = Color.White;
			hc.HorizontalAlign = HorizontalAlign.Center;
			tr.Cells.Add(hc);

			hc = new TableHeaderCell();
			hc.Text = "Nilai Kontrak";
			// hc.BackColor = Color.Gray;
			// hc.ForeColor = Color.White;
			hc.HorizontalAlign = HorizontalAlign.Right;
			tr.Cells.Add(hc);

			hc = new TableHeaderCell();
			hc.Text = "Collection<br />Dasar \"Voucher PB\"";
			// hc.BackColor = Color.Gray;
			// hc.ForeColor = Color.White;
			hc.HorizontalAlign = HorizontalAlign.Center;
			tr.Cells.Add(hc);

			hc = new TableHeaderCell();
			hc.Text = "Saldo<br />Sisa \"SP\"";
			// hc.BackColor = Color.Gray;
			// hc.ForeColor = Color.White;
			hc.HorizontalAlign = HorizontalAlign.Center;
			tr.Cells.Add(hc);

			decimal t1 = 0, t2 = 0, t3 = 0;

			DataTable rs = Db.Rs("SELECT DISTINCT Lokasi FROM ISC064_MARKETINGJUAL..MS_UNIT WHERE Project = '" + Project + "' ORDER BY Lokasi");
			for(int i = 0; i < rs.Rows.Count; i++)
			{
				if(!Response.IsClientConnected)
					break;

				tr = new TableRow();
				tb.Rows.Add(tr);

				c = new TableCell();
				c.Text = rs.Rows[i]["Lokasi"].ToString();
				c.HorizontalAlign = HorizontalAlign.Center;
				tr.Cells.Add(c);

				c = new TableCell();
				decimal a = NilaiKontrak(rs.Rows[i]["Lokasi"].ToString());
				c.Text = Cf.Num(a);
				c.HorizontalAlign = HorizontalAlign.Right;
				tr.Cells.Add(c);

				c = new TableCell();
				decimal b = TotalLunas(rs.Rows[i]["Lokasi"].ToString());
				c.Text = Cf.Num(b);
				c.HorizontalAlign = HorizontalAlign.Right;
				tr.Cells.Add(c);

				c = new TableCell();
				decimal x = a - b;
				c.Text = Cf.Num(x);
				c.HorizontalAlign = HorizontalAlign.Right;
				tr.Cells.Add(c);

				t1 += a;
				t2 += b;
				t3 += x;
			}

			tr = new TableRow();
			tb.Rows.Add(tr);

			c = Rpt.Foot();
			c.Text = "Total";
			c.HorizontalAlign = HorizontalAlign.Center;
			tr.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(t1);
			c.HorizontalAlign = HorizontalAlign.Right;
			tr.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(t2);
			c.HorizontalAlign = HorizontalAlign.Right;
			tr.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(t3);
			c.HorizontalAlign = HorizontalAlign.Right;
			tr.Cells.Add(c);

			rpt.Controls.Add(tb);
		}

		protected void Finance2()
		{
			Table tb;
			TableRow tr;
			TableHeaderCell hc;
			TableCell c;

			tb = new Table();
			tb.CssClass = "tb blue-skin";
			tb.CellSpacing = 1;

			tr = new TableRow();
			tb.Rows.Add(tr);

			c = new TableCell();
			c.Text = "<b>2. CLEARING BANK</b>";
			c.ColumnSpan = 4;
			tr.Cells.Add(c);
			
			tr = new TableRow();
			tb.Rows.Add(tr);

			hc = new TableHeaderCell();
			hc.Text = "Lantai";
			// hc.BackColor = Color.Gray;
			// hc.ForeColor = Color.White;
			hc.HorizontalAlign = HorizontalAlign.Center;
			tr.Cells.Add(hc);

			hc = new TableHeaderCell();
			hc.Text = "Collection<br />Dasar \"Voucher PB\"";
			// hc.BackColor = Color.Gray;
			// hc.ForeColor = Color.White;
			hc.HorizontalAlign = HorizontalAlign.Center;
			tr.Cells.Add(hc);

			hc = new TableHeaderCell();
			hc.Text = "Clearing Bank<br />Dasar \"Kwitansi\"";
			// hc.BackColor = Color.Gray;
			// hc.ForeColor = Color.White;
			hc.HorizontalAlign = HorizontalAlign.Center;
			tr.Cells.Add(hc);

			hc = new TableHeaderCell();
			hc.Text = "Saldo<br />Sisa";
			// hc.BackColor = Color.Gray;
			// hc.ForeColor = Color.White;
			hc.HorizontalAlign = HorizontalAlign.Center;
			tr.Cells.Add(hc);

			decimal t1 = 0, t2 = 0, t3 = 0;

			DataTable rs = Db.Rs("SELECT DISTINCT Lokasi FROM ISC064_MARKETINGJUAL..MS_UNIT WHERE Project = '" + Project + "' ORDER BY Lokasi");
			for(int i = 0; i < rs.Rows.Count; i++)
			{
				if(!Response.IsClientConnected)
					break;

				tr = new TableRow();
				tb.Rows.Add(tr);

				c = new TableCell();
				c.Text = rs.Rows[i]["Lokasi"].ToString();
				c.HorizontalAlign = HorizontalAlign.Center;
				tr.Cells.Add(c);

				c = new TableCell();
				decimal a = TotalLunas(rs.Rows[i]["Lokasi"].ToString());
				c.Text = Cf.Num(a);
				c.HorizontalAlign = HorizontalAlign.Right;
				tr.Cells.Add(c);

				c = new TableCell();
				decimal b = TotalLunasCair(rs.Rows[i]["Lokasi"].ToString());
				c.Text = Cf.Num(b);
				c.HorizontalAlign = HorizontalAlign.Right;
				tr.Cells.Add(c);

				c = new TableCell();
				decimal x = a - b;
				c.Text = Cf.Num(x);
				c.HorizontalAlign = HorizontalAlign.Right;
				tr.Cells.Add(c);

				t1 += a;
				t2 += b;
				t3 += x;
			}

			tr = new TableRow();
			tb.Rows.Add(tr);

			c = Rpt.Foot();
			c.Text = "Total";
			c.HorizontalAlign = HorizontalAlign.Center;
			tr.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(t1);
			c.HorizontalAlign = HorizontalAlign.Right;
			tr.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(t2);
			c.HorizontalAlign = HorizontalAlign.Right;
			tr.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(t3);
			c.HorizontalAlign = HorizontalAlign.Right;
			tr.Cells.Add(c);

			rpt.Controls.Add(tb);
		}

		protected int TotalUnit(string Lokasi)
		{
			int c = Db.SingleInteger(
				"SELECT COUNT(*) FROM ISC064_MARKETINGJUAL..MS_UNIT WHERE Lokasi = '" + Lokasi + "' AND Project = '" + Project + "'"
				);

			return c;
		}

		protected decimal PriceList(string Lokasi)
		{
			decimal c = Db.SingleDecimal(
				"SELECT ISNULL(SUM(PriceList), 0) FROM ISC064_MARKETINGJUAL..MS_UNIT"
				+ " WHERE Lokasi = '" + Lokasi + "' AND AND Project = '" + Project + "'"
                );

			return c;
		}

		protected int SoldUnit(string Lokasi)
		{
			int c = Db.SingleInteger(
				"SELECT COUNT(*) FROM ISC064_MARKETINGJUAL..MS_UNIT"
				+ " WHERE NoStock IN (SELECT NoStock FROM ISC064_MARKETINGJUAL..MS_KONTRAK WHERE Status = 'A' AND TglKontrak <= '" + Tgl + "' AND Project = '" + Project + "')"
                + " AND Lokasi = '" + Lokasi + "' AND Project = '" + Project + "'");

			return c;
		}

		protected int BookedUnit(string Lokasi)
		{
			int c = Db.SingleInteger(
				"SELECT COUNT(*) FROM ISC064_MARKETINGJUAL..MS_UNIT"
				+ " WHERE NoStock IN (SELECT NoStock FROM ISC064_MARKETINGJUAL..MS_RESERVASI WHERE Status = 'A' AND Tgl <= '" + Tgl + "')"
				+ " AND Lokasi = '" + Lokasi + "' AND Project = '" + Project + "'");

			return c;
		}

		protected int AvailableUnit(string Lokasi)
		{
			int c = Db.SingleInteger(
				"SELECT COUNT(*) FROM ISC064_MARKETINGJUAL..MS_UNIT"
				+ " WHERE NoStock NOT IN (SELECT NoStock FROM ISC064_MARKETINGJUAL..MS_KONTRAK WHERE Status = 'A' AND TglKontrak <= '" + Tgl + "' AND Project = '" + Project + "')"
                + " AND Lokasi = '" + Lokasi + "' AND Project = '" + Project + "'");

			return c;
		}

		protected decimal NilaiKontrak(string Lokasi)
		{
			decimal d = Db.SingleDecimal(
				"SELECT ISNULL(SUM(NilaiKontrak), 0) FROM ISC064_MARKETINGJUAL..MS_KONTRAK WHERE Lokasi = '" + Lokasi + "'"
				+ " AND Status = 'A' AND TglKontrak <= '" + Tgl + "' AND Project = '" + Project + "'");

			return d;
		}

		protected decimal TotalLunas(string Lokasi)
		{
			decimal d = Db.SingleDecimal(
				"SELECT ISNULL(SUM(NilaiPelunasan), 0) FROM ISC064_MARKETINGJUAL..MS_PELUNASAN a"
				+ " INNER JOIN ISC064_MARKETINGJUAL..MS_KONTRAK b ON a.NoKontrak = b.NoKontrak"
				+ " WHERE b.Lokasi = '" + Lokasi + "' AND b.Status = 'A' AND TglPelunasan <= '" + Tgl + "' AND b.Project = '" + Project + "'"
                );

			return d;
		}

		protected decimal TotalLunasCair(string Lokasi)
		{
			decimal d = Db.SingleDecimal(
				"SELECT ISNULL(SUM(NilaiPelunasan), 0) FROM ISC064_MARKETINGJUAL..MS_PELUNASAN a"
				+ " INNER JOIN ISC064_MARKETINGJUAL..MS_KONTRAK b ON a.NoKontrak = b.NoKontrak"
				+ " WHERE b.Lokasi = '" + Lokasi + "' AND b.Status = 'A' AND TglPelunasan <= '" + Tgl + "'"
                + " AND a.SudahCair = 1 AND b.Project = '" + Project + "'"
                );

			return d;
		}

		protected decimal NilaiReservasi(string Lokasi)
		{
			decimal d = Db.SingleDecimal(
                "SELECT ISNULL(SUM(PriceList), 0) FROM ISC064_MARKETINGJUAL..MS_UNIT WHERE Project = '" + Project + "' AND NoStock IN ("
                + "SELECT NoStock FROM ISC064_MARKETINGJUAL..MS_RESERVASI WHERE Lokasi = '" + Lokasi + "' AND Status = 'A' AND Tgl <= '" + Tgl + "')");

			return d;
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

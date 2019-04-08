using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL.Laporan
{
	public partial class LaporanCashFlow : System.Web.UI.Page
	{
		protected int Dari=0,Sampai=0;
		protected DateTime M1A, M1B, M2A, M2B, M3A, M3B, M4A, M4B;
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();

			if(!Page.IsPostBack)
			{
				comp.InnerHtml = Mi.Pt;
				rpt.Visible = false;
				Js.Focus(this, scr);
				init();
				if(!Act.Sec("DownloadExcel")) xls.Enabled = false;
			}
		}

		protected void init()
		{
			Cf.BindTahun(thn);
			Cf.BindBulan(dari);
			//Cf.BindBulan(sampai);

			thn.SelectedValue = DateTime.Today.Year.ToString();
			dari.SelectedValue  = DateTime.Today.Month.ToString();
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
			header.Text = Mi.Pt
				+ "<br />"
				+ "LAPORAN CASH FLOW"
				;

			System.Text.StringBuilder x = new System.Text.StringBuilder();
			
			Dari = Convert.ToInt32(dari.SelectedValue);
//			Sampai = Convert.ToInt32(sampai.SelectedValue);

//			if(Dari > Sampai)
//			{
				int y = Dari;
//				Dari = Sampai;
//				Sampai = y;
//			}
		
			x.Append("Periode: " + thn.SelectedValue);
			
			
			TableRow r;
			TableCell c;
			TableHeaderCell hc;
			
//			r = new TableRow();
//			c = new TableCell();
//			c.ColumnSpan = Dari - 5;
//			c.Attributes["style"] = "font-size: 8pt;";
//			r.Cells.Add(c);
//
//			rpt.Rows.Add(r);
			
			r = new TableRow();

			hc = new TableHeaderCell();
			hc.Text = "No. Kontrak";
			hc.Attributes["style"] = "background-color: gray; color: white;";
			hc.RowSpan = 2;
			hc.Width = 100;
			r.Cells.Add(hc);

			hc = new TableHeaderCell();
			hc.Text = "Status";
			hc.Attributes["style"] = "background-color: gray; color: white;";
			hc.RowSpan = 2;
			r.Cells.Add(hc);

			hc = new TableHeaderCell();
			hc.Text = "Customer";
			hc.Attributes["style"] = "background-color: gray; color: white;";
			hc.RowSpan = 2;
			hc.Width = 200;
			r.Cells.Add(hc);

			hc = new TableHeaderCell();
			hc.Text = "Unit";
			hc.Attributes["style"] = "background-color: gray; color: white;";
			hc.RowSpan = 2;
			r.Cells.Add(hc);
			int j = Dari;
//			for(int i = Dari; i <= Sampai; i++)
//			{
				hc = new TableHeaderCell();
				hc.Text = Cf.Monthname(j);
				hc.Attributes["style"] = "background-color: gray; color: white;";
				hc.ColumnSpan = 4;
				r.Cells.Add(hc);
//			}

			rpt.Rows.Add(r);

			r = new TableRow();

			int roman = 1;
			int temp =  1;
			for(int i = 0; i < temp * 4; i++)
			{
				hc = new TableHeaderCell();
				hc.Text = Cf.Roman(roman);
				hc.Attributes["style"] = "background-color: gray; color: white;";
				r.Cells.Add(hc);

				roman++;

				if(roman > 4)
					roman = 1;
			}

			rpt.Rows.Add(r);
		}

		protected decimal GetNilai(string NoKontrak, DateTime TglAwal, DateTime TglAkhir)
		{
			string strSQL = "";
			strSQL = "SELECT ISNULL(SUM(NilaiTagihan),0) FROM MS_TAGIHAN"
				+ " WHERE NoKontrak = '" + NoKontrak + "' AND Convert(varchar,TglJT,112) >= '" + TglAwal.ToString("yyyyMMdd") + "'"
				+ " AND Convert(varchar,TglJT,112) <= '" + TglAkhir.ToString("yyyyMMdd") + "'";
		
			return Db.SingleDecimal(strSQL);
		}

		protected void FillBulan(string NoKontrak, int Bulan)
		{
			
			TableRow r = new TableRow();
			TableCell c;
		  
			int tahun = Convert.ToInt32(thn.SelectedValue);
			int i = Bulan;
				M1A = new DateTime(tahun, i, 1);
				if (periodetetap.Checked)
				{
			
					M1B = M1A.AddDays(6);
					M2A = M1B.AddDays(1);
					M2B = M2A.AddDays(6);
					M3A = M2B.AddDays(1);
					M3B = M3A.AddDays(6);
					M4A = M3B.AddDays(1);
					int k = DateTime.DaysInMonth(tahun,i);
					M4B = new DateTime(tahun, i, k);
					

				}
				else if(periodecal.Checked)
				{
					switch(M1A.DayOfWeek)
					{
						case DayOfWeek.Monday:M1A=M1A;break;
						case DayOfWeek.Tuesday:M1A=M1A.AddDays(6);break;
						case DayOfWeek.Wednesday:M1A=M1A.AddDays(5);break;
						case DayOfWeek.Thursday:M1A=M1A.AddDays(4);break;
						case DayOfWeek.Friday:M1A=M1A.AddDays(3);break;
						case DayOfWeek.Saturday:M1A=M1A.AddDays(2);break;
						case DayOfWeek.Sunday:M1A=M1A.AddDays(1);break;
					}
					M1B = M1A.AddDays(6);
					M2A = M1B.AddDays(1);
					M2B = M2A.AddDays(6);
					M3A = M2B.AddDays(1);
					M3B = M3A.AddDays(6);
					M4A = M3B.AddDays(1);
					M4B = M4A.AddDays(6);

				}
			

//			c = new TableCell();
//			c.Text = Cf.Num(GetNilai(NoKontrak, M1A, M1B));
//			c.HorizontalAlign = HorizontalAlign.Right;
//			r.Cells.Add(c);
//
//			c = new TableCell();
//			c.Text = Cf.Num(GetNilai(NoKontrak, M2A, M2B));
//			c.HorizontalAlign = HorizontalAlign.Right;
//			r.Cells.Add(c);
//
//			c = new TableCell();
//			c.Text = Cf.Num(GetNilai(NoKontrak, M3A, M3B));
//			c.HorizontalAlign = HorizontalAlign.Right;
//			r.Cells.Add(c);
//
//			c = new TableCell();
//			c.Text = Cf.Num(GetNilai(NoKontrak, M4A, M4B));
//			c.HorizontalAlign = HorizontalAlign.Right;
//			r.Cells.Add(c);
//
//			rpt.Rows.Add(r);

		}

		protected void Fill()
		{
			string Status = "", NoKontrak = "";
			if(statusA.Checked) Status = " AND MS_KONTRAK.Status = 'A'";
			if(statusB.Checked) Status = " AND MS_KONTRAK.Status = 'B'";
			decimal t1 = 0, t2 = 0, t3 = 0, t4 = 0;

			string strSql = "SELECT MS_KONTRAK.*"
			+ ",MS_CUSTOMER.Nama AS Cs"
			+ " FROM MS_KONTRAK"
			+ " INNER JOIN MS_CUSTOMER ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
			+ " WHERE 1=1 "
			+ Status
			;
				
			DataTable rs = Db.Rs(strSql);

			for(int i = 0; i < rs.Rows.Count; i++)
			{
				if(!Response.IsClientConnected)
					break;

				TableRow r = new TableRow();
				TableCell c;

				NoKontrak = rs.Rows[i]["NoKontrak"].ToString();
				c = new TableCell();
				c.Text = NoKontrak;
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Status"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Cs"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["NoUnit"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);
				
				FillBulan(NoKontrak, Dari);

				c = new TableCell();
				c.Text = Cf.Num(GetNilai(NoKontrak, M1A, M1B));
				t1 += Convert.ToDecimal(Cf.Num(GetNilai(NoKontrak, M1A, M1B)));
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Num(GetNilai(NoKontrak, M2A, M2B));
				t2 += Convert.ToDecimal(Cf.Num(GetNilai(NoKontrak, M2A, M2B)));
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Num(GetNilai(NoKontrak, M3A, M3B));
				t3 += Convert.ToDecimal(Cf.Num(GetNilai(NoKontrak, M3A, M3B)));
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Num(GetNilai(NoKontrak, M4A, M4B));
				t4 += Convert.ToDecimal(Cf.Num(GetNilai(NoKontrak, M4A, M4B)));
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				rpt.Rows.Add(r);
				
//				for(int i = Dari; i <= Sampai; i++)
//				{
					
//				}
//				rpt.Rows.Add(r);
				if(i == rs.Rows.Count - 1)
					SubTotal(t1,t2,t3,t4);
			}
			
//			t1 += Convert.ToDecimal(rs.Rows[i]["NilaiKontrak"]);
//			t2 += Convert.ToDecimal(rs.Rows[i]["NilaiKlaim"]);
//			t3 += Convert.ToDecimal(rs.Rows[i]["TotalLunasBatal"]);
//			t4 += Convert.ToDecimal(rs.Rows[i]["NilaiPulang"]);
//
			
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

		private void SubTotal(decimal t1, decimal t2, decimal t3, decimal t4)
		{
			TableRow r = new TableRow();
			TableCell c;

			c = Rpt.Foot();
			c.Text = "";
			c.ColumnSpan = 4;
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


			rpt.Rows.Add(r);
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

using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL.Laporan
{
	public partial class LaporanPPNKPA : System.Web.UI.Page
	{
		protected decimal JumlahLalu = 0, NettoLalu = 0, PPNLalu = 0;

	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();

			if(!Page.IsPostBack)
			{
				comp.InnerHtml = Mi.Pt;
				rpt.Visible = false;
				Bind();
				Js.Focus(this, scr);
				if(!Act.Sec("DownloadExcel")) xls.Enabled = false;
			}
		}

		private void Bind()
		{
			dari.Text = Cf.Day(Cf.AwalBulan());
			sampai.Text = Cf.Day(Cf.AkhirBulan());
		}

		private bool valid()
		{
			string s = "";
			bool x = true;

			if(!Cf.isTgl(dari))
			{
				x = false;
				if(s=="") s = dari.ID;
				daric.Text = "Tanggal";
			}
			else
				daric.Text = "";

			if(!Cf.isTgl(sampai))
			{
				x = false;
				if(s=="") s = sampai.ID;
				sampaic.Text = "Tanggal";
			}
			else
				sampaic.Text = "";

			if(!x && s!="")
			{
				RegisterStartupScript("err"
					,"<script language='javascript'>document.getElementById('"+s+"').select()</script>");
			}

			return x;
		}

		private void Fill()
		{
			DateTime Dari = Convert.ToDateTime(dari.Text);
			DateTime Sampai = Convert.ToDateTime(sampai.Text);

			if(Dari > Sampai)
			{
				DateTime x = Sampai;
				Sampai = Dari;
				Dari = Sampai;
			}

			string PPNDitanggung = " AND c.JenisPPN = '" + JenisPPN.SelectedItem.Text + "'";

			string strSql = "SELECT a.*, b.NamaTagihan, b.Tipe, c.NoUnit"
				+ " FROM MS_PELUNASAN a"
				+ " INNER JOIN MS_TAGIHAN b ON a.NoTagihan = b.NoUrut AND a.NoKontrak = b.NoKontrak"
				+ " INNER JOIN MS_KONTRAK c ON a.NoKontrak = c.NoKontrak"
				+ " WHERE a.TglPelunasan >= '" + Dari + "'"
				+ " AND a.TglPelunasan <= '" + Sampai + "'"
				+ " AND b.NamaTagihan LIKE '%AKAD%'"
				+ " AND a.NoBKM > 0"
				+ PPNDitanggung
				;
			DataTable rs = Db.Rs(strSql);

			decimal t1 = 0, t2 = 0, t3 = 0, t4 = 0, PPN = 0, Netto = 0;

			PeriodeLalu(Dari, PPNDitanggung);
			
			for(int i = 0; i < rs.Rows.Count; i++)
			{
				if(!Response.IsClientConnected)
					break;

				TableRow r = new TableRow();
				TableCell c;

				c = new TableCell();
				c.Text = (i + 1).ToString();
				c.HorizontalAlign = HorizontalAlign.Center;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Day(rs.Rows[i]["TglPelunasan"]);
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

                string ManualBKM = Db.SingleString("SELECT ManualBKM FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS WHERE NoBKM = '" + rs.Rows[i]["NoBKM"].ToString() + "'");

				c = new TableCell();
                c.Text = ManualBKM; //rs.Rows[i]["NoBKM"].ToString().PadLeft(5, '0');
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["NoUnit"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["NamaTagihan"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Convert.ToDecimal(rs.Rows[i]["NilaiPelunasan"]).ToString("N0");
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				c = new TableCell();
				if(JenisPPN.SelectedItem.Text == "KONSUMEN")
					PPN = Convert.ToDecimal(rs.Rows[i]["NilaiPelunasan"]) / (decimal)1.1;
				else
					PPN = 0;
				c.Text = PPN.ToString("N0");
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = PPN.ToString("N0");
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				c = new TableCell();
				Netto = Convert.ToDecimal(rs.Rows[i]["NilaiPelunasan"]) - PPN;
				c.Text = Netto.ToString("N0");
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Db.SingleString("SELECT NoFPS"
					+ " FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS"
					+ " WHERE NoTTS = " + Cf.Pk(rs.Rows[i]["NoTTS"])
					);
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				t1 += Convert.ToDecimal(rs.Rows[i]["NilaiPelunasan"]);
				t2 += Convert.ToDecimal(PPN);
				t3 = t2;
				t4 += Convert.ToDecimal(Netto);

				rpt.Rows.Add(r);

				if(i == (rs.Rows.Count - 1))
					SubTotal(t1, t2, t3, t4);
			}

			PeriodeNow((JumlahLalu + t1), (PPNLalu + t2), (PPNLalu + t3), (NettoLalu + t4), Sampai);
		}

		protected void PeriodeLalu(DateTime Dari, string PPNDitanggung)
		{
			int BlnLalu = Dari.AddMonths(-1).Month;
			int Thn = Dari.Year;
			DateTime PeriodeLalu = Convert.ToDateTime(BlnLalu + "/" + DateTime.DaysInMonth(Thn, BlnLalu) + "/" + Thn);
			string strSql = "SELECT ISNULL(SUM(NilaiPelunasan), 0)"
				+ " FROM MS_PELUNASAN a"
				+ " INNER JOIN MS_TAGIHAN b ON a.NoTagihan = b.NoUrut AND a.NoKontrak = b.NoKontrak"
				+ " INNER JOIN MS_KONTRAK c ON a.NoKontrak = c.NoKontrak"
				+ " WHERE a.TglPelunasan <= '" + PeriodeLalu + "'"
				+ " AND b.NamaTagihan LIKE '%AKAD%'"
				+ " AND a.NoBKM > 0"
				+ PPNDitanggung
				;
			JumlahLalu = Db.SingleDecimal(strSql);
			
			if(JenisPPN.SelectedItem.Text == "KONSUMEN")
			{
				NettoLalu =(JumlahLalu / (decimal)1.1);
				PPNLalu = JumlahLalu - NettoLalu;
			}
			else if(JenisPPN.SelectedItem.Text == "PEMERINTAH")
			{
				NettoLalu = JumlahLalu;
				PPNLalu = 0;
			}				

			TableRow r = new TableRow();
			TableCell c;

			c = new TableCell();
			c.Text = "";
			c.ColumnSpan = 5;
			c.Text = "Jumlah s/d tanggal: " + Cf.Day(PeriodeLalu);
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			c = new TableCell();
			c.Text = JumlahLalu.ToString("N0");
			c.HorizontalAlign = HorizontalAlign.Right;
			c.Attributes["style"] = "border-bottom: 1px solid Black;";
			r.Cells.Add(c);

			c = new TableCell();
			c.Text = PPNLalu.ToString("N0");
			c.HorizontalAlign = HorizontalAlign.Right;
			c.Attributes["style"] = "border-bottom: 1px solid Black;";
			r.Cells.Add(c);

			c = new TableCell();
			c.Text = PPNLalu.ToString("N0");
			c.HorizontalAlign = HorizontalAlign.Right;
			c.Attributes["style"] = "border-bottom: 1px solid Black;";
			r.Cells.Add(c);

			c = new TableCell();
			c.Text = NettoLalu.ToString("N0");
			c.HorizontalAlign = HorizontalAlign.Right;
			c.Attributes["style"] = "border-bottom: 1px solid Black;";
			r.Cells.Add(c);

			rpt.Rows.Add(r);
		}

		protected void PeriodeNow(decimal t1, decimal t2, decimal t3, decimal t4, DateTime Sampai)
		{
			TableRow r = new TableRow();
			TableCell c;

			c = Rpt.Foot();
			c.Text = "Jumlah s/d tanggal: " + Cf.Day(Sampai);
			c.ColumnSpan = 5;
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = t1.ToString("N0");
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = t2.ToString("N0");
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = t3.ToString("N0");
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = t4.ToString("N0");
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);
			
			rpt.Rows.Add(r);
		}

		protected void SubTotal(decimal t1, decimal t2, decimal t3, decimal t4)
		{
			TableRow r = new TableRow();
			TableCell c;

			c = Rpt.Foot();
			c.Text = "Jumlah Periode Ini";
			c.ColumnSpan = 5;
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = t1.ToString("N0");
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = t2.ToString("N0");
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = t3.ToString("N0");
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = t4.ToString("N0");
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);
			
			rpt.Rows.Add(r);
		}

		private void Header()
		{
			System.Text.StringBuilder x = new System.Text.StringBuilder();

			Rpt.Judul(x, comp, judul);

			DateTime Dari = Convert.ToDateTime(dari.Text);
			DateTime Sampai = Convert.ToDateTime(sampai.Text);
			Rpt.SubJudul(x
				, "Tanggal: " + Cf.Day(dari.Text) + " s/d " + Cf.Day(sampai.Text)
				);

			Rpt.SubJudul(x
				, "Jenis PPN: DITANGGUNG " + JenisPPN.SelectedItem.Text
				);

			Rpt.Header(rpt, x);
		}

		private void Report()
		{
			param.Visible = false;
			rpt.Visible = true;

			Header();
			Fill();
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

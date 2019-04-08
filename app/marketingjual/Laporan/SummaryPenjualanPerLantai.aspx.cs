using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL.Laporan
{
	public partial class SummaryPenjualanPerLantai : System.Web.UI.Page
	{
		
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();

			if(!Page.IsPostBack)
			{
				comp.InnerHtml = Mi.Pt;
				rpt.Visible = false;
				Js.Focus(this,scr);
				init();
				if(!Act.Sec("DownloadExcel")) xls.Enabled = false;
			}
		}

		private void init()
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
				Rpt.ToExcel(this,rpt);
			}
		}

		private void Report()
		{
			param.Visible = false;
			rpt.Visible = true;

			Header();
			Fill();
		}

		private void Header()
		{
			System.Text.StringBuilder x = new System.Text.StringBuilder();

			Rpt.Judul(x, comp, judul);

			string tgl = "";
			if(tglkontrak.Checked) tgl = tglkontrak.Text;

			DateTime Dari = Convert.ToDateTime(dari.Text);
			DateTime Sampai = Convert.ToDateTime(sampai.Text);
			Rpt.SubJudul(x
				, tgl + " : " + Cf.Day(dari.Text) + " s/d " + Cf.Day(sampai.Text)
				);

			Rpt.Header(rpt, x);
		}

		private void Fill()
		{	
			DateTime Dari = Convert.ToDateTime(dari.Text);
			DateTime Sampai = Convert.ToDateTime(sampai.Text);
			if(Dari > Sampai)
			{
				DateTime x = Sampai;
				Sampai = Dari;
				Dari = x;
			}

			decimal Unit = 0, Nilai = 0, Luas = 0, Diskon = 0, TTS = 0, Cair = 0, Outstanding = 0;
			decimal t1 = 0, t2 = 0, t3 = 0, t4 = 0, t5 = 0, t6 = 0, t7 = 0;

			string strSql = "SELECT DISTINCT(Lokasi) FROM MS_UNIT";
			
			DataTable rs = Db.Rs(strSql);
			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				TableRow r = new TableRow();
				TableCell c;

				c = new TableCell();
				c.Text = rs.Rows[i]["Lokasi"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				Unit = Hitung(Dari, Sampai, rs.Rows[i]["Lokasi"].ToString(), "UNIT");
				c.Text = Cf.Num(Unit);
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				c = new TableCell();
				Nilai = Hitung(Dari, Sampai, rs.Rows[i]["Lokasi"].ToString(), "NILAI");
				c.Text = Cf.Num(Nilai);
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				c = new TableCell();
				Luas = Hitung(Dari, Sampai, rs.Rows[i]["Lokasi"].ToString(), "LUAS");
				c.Text = Cf.Num(Luas);
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				c = new TableCell();
				Diskon = Hitung(Dari, Sampai, rs.Rows[i]["Lokasi"].ToString(), "DISKON");
				c.Text = Cf.Num(Diskon);
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				c = new TableCell();
				TTS = Hitung(Dari, Sampai, rs.Rows[i]["Lokasi"].ToString(), "TTS");
				c.Text = Cf.Num(TTS);
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				c = new TableCell();
				Cair = Hitung(Dari, Sampai, rs.Rows[i]["Lokasi"].ToString(), "CAIR");
				c.Text = Cf.Num(Cair);
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				c = new TableCell();
				Outstanding = Nilai - Cair;
				c.Text = Cf.Num(Outstanding);
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				rpt.Rows.Add(r);

				t1 += Unit;
				t2 += Nilai;
				t3 += Luas;
				t4 += Diskon;
				t5 += TTS;
				t6 += Cair;
				t7 += Outstanding;

				if(i==rs.Rows.Count-1)
					SubTotal("", t1, t2, t3, t4, t5, t6, t7);
			}
		}

		protected decimal Hitung(DateTime Dari, DateTime Sampai, string Lokasi, string Tipe)
		{
			decimal t = 0;
			string strSql = "";

			if(Tipe == "UNIT")
			{
				strSql = "SELECT COUNT(*)"
					+ " FROM MS_KONTRAK"
					+ " WHERE TglKontrak >= '" + Dari + "'"
					+ " AND TglKontrak <= '" + Sampai + "'"
					+ " AND Lokasi = '" + Lokasi + "'"
					+ " AND Status = 'A'"
					;
			}
			else if(Tipe == "NILAI")
			{
				strSql = "SELECT ISNULL(SUM(NilaiKontrak), 0)"
					+ " FROM MS_KONTRAK"
					+ " WHERE TglKontrak >= '" + Dari + "'"
					+ " AND TglKontrak <= '" + Sampai + "'"
					+ " AND Lokasi = '" + Lokasi + "'"
					+ " AND Status = 'A'"
					;
			}
			else if(Tipe == "LUAS")
			{
				strSql = "SELECT ISNULL(SUM(Luas), 0)"
					+ " FROM MS_KONTRAK"
					+ " WHERE TglKontrak >= '" + Dari + "'"
					+ " AND TglKontrak <= '" + Sampai + "'"
					+ " AND Lokasi = '" + Lokasi + "'"
					+ " AND Status = 'A'"
					;
			}
			else if(Tipe == "DISKON")
			{
				strSql = "SELECT ISNULL(SUM(DiskonRupiah), 0)"
					+ " FROM MS_KONTRAK"
					+ " WHERE TglKontrak >= '" + Dari + "'"
					+ " AND TglKontrak <= '" + Sampai + "'"
					+ " AND Lokasi = '" + Lokasi + "'"
					+ " AND Status = 'A'"
					;
			}
			else if(Tipe == "TTS")
			{
				strSql = "SELECT ISNULL(SUM(NilaiPelunasan), 0)"
					+ " FROM MS_PELUNASAN a"
					+ " INNER JOIN MS_KONTRAK b ON a.NoKontrak = b.NoKontrak"
					+ " WHERE b.TglKontrak >= '" + Dari + "'"
					+ " AND b.TglKontrak <= '" + Sampai + "'"
					+ " AND b.Lokasi = '" + Lokasi + "'"
					+ " AND b.Status = 'A'"
					;
			}
			else if(Tipe == "CAIR")
			{
				strSql = "SELECT ISNULL(SUM(NilaiPelunasan), 0)"
					+ " FROM MS_PELUNASAN a"
					+ " INNER JOIN MS_KONTRAK b ON a.NoKontrak = b.NoKontrak"
					+ " WHERE b.TglKontrak >= '" + Dari + "'"
					+ " AND b.TglKontrak <= '" + Sampai + "'"
					+ " AND b.Lokasi = '" + Lokasi + "'"
					+ " AND b.Status = 'A'"
					+ " AND a.SudahCair = 1"
					;
			}
			
			t = Db.SingleDecimal(strSql);

			return t;
		}

		private void SubTotal(string txt, decimal t1, decimal t2, decimal t3, decimal t4, decimal t5, decimal t6, decimal t7)
		{
			TableRow r = new TableRow();
			TableCell c;

			c = Rpt.Foot();
			c.Text = txt;
			c.HorizontalAlign = HorizontalAlign.Left;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(t1) + " UNIT";
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

			c = Rpt.Foot();
			c.Text = Cf.Num(t7);
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

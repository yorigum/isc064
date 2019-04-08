using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.FINANCEAR.Laporan
{
	public partial class BukuBank : System.Web.UI.Page
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
			dari.Text = Cf.Day(DateTime.Today);
			sampai.Text = Cf.Day(DateTime.Today);

			DataTable rs = Db.Rs("SELECT * FROM REF_ACC");
			for(int i=0;i<rs.Rows.Count;i++) 
			{
				string v = rs.Rows[i]["Acc"].ToString();
				string t = v + " : " + rs.Rows[i]["Bank"] + " " + rs.Rows[i]["Rekening"];
				ddlAcc.Items.Add(new ListItem(t,v));
			}
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
					,"<script type='text/javascript'>document.getElementById('"+s+"').select()</script>");
			}

			return x;
		}

		protected void scr_Click(object sender, System.EventArgs e)
		{
			if(valid())
			{
				Report();
			}
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

			DateTime Dari = Convert.ToDateTime(dari.Text);
			DateTime Sampai = Convert.ToDateTime(sampai.Text);
			Rpt.SubJudul(x
				, "Tanggal : " + Cf.Day(dari.Text) + " s/d " + Cf.Day(sampai.Text)
				);

			Rpt.SubJudul(x
				, "Rekening Bank : " + ddlAcc.SelectedItem.Text
				);

			Rpt.Header(rpt, x);
		}

		private void Fill()
		{
			DateTime Dari = Convert.ToDateTime(dari.Text);
			DateTime Sampai = Convert.ToDateTime(sampai.Text);
			if(Dari>Sampai)
			{
				DateTime x = Sampai;
				Sampai = Dari;
				Dari = x;
			}

			string Rekening = Cf.Str(ddlAcc.SelectedValue);

			//Saldo Awal
			TableRow r = new TableRow();
			TableCell c;

			c = new TableCell();
			c.Text = "SALDO AWAL";
			c.ColumnSpan = 8;
			c.HorizontalAlign = HorizontalAlign.Left;
			r.Cells.Add(c);

			c = new TableCell();
			c.Text = Cf.Num(Func.SaldoBank(Rekening, Dari));
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			rpt.Rows.Add(r);

			do
			{
				KasMasuk(Rekening, Dari);
				KasKeluar(Rekening, Dari);
				
				Dari = Dari.AddDays(1);
			}while(Dari <= Sampai);

			//Saldo Akhir
			r = new TableRow();

			c = new TableCell();
			c.Text = "SALDO AKHIR";
			c.ColumnSpan = 8;
			c.HorizontalAlign = HorizontalAlign.Left;
			r.Cells.Add(c);

			c = new TableCell();
			c.Text = Cf.Num(Func.SaldoBank(Rekening, Sampai.AddDays(1)));
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			rpt.Rows.Add(r);
		}

		private void KasMasuk(string Rekening, DateTime Dari)
		{
			string strSql = "SELECT *"
				+ " FROM MS_KASMASUK"
				+ " WHERE Acc = '" + Rekening + "'"
				+ " AND Tgl = '" + Dari + "'"
				;
			DataTable rsMasuk = Db.Rs(strSql);

			for(int i = 0; i < rsMasuk.Rows.Count; i++)
			{
				TableRow r = new TableRow();
				TableCell c;

				c = new TableCell();
				c.Text = Cf.Day(rsMasuk.Rows[i]["Tgl"]);
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rsMasuk.Rows[i]["CaraBayar"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rsMasuk.Rows[i]["AlatBayar"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rsMasuk.Rows[i]["DiterimaDari"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = "";
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rsMasuk.Rows[i]["Keterangan"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Num(rsMasuk.Rows[i]["Nilai"]);
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = "";
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = "";
				r.Cells.Add(c);

				rpt.Rows.Add(r);
			}
		}

		private void KasKeluar(string Rekening, DateTime Dari)
		{
			string strSql = "SELECT *"
				+ " FROM MS_KASKELUAR"
				+ " WHERE Acc = '" + Rekening + "'"
				+ " AND Tgl = '" + Dari + "'"
				;
			DataTable rsKeluar = Db.Rs(strSql);

			for(int i = 0; i < rsKeluar.Rows.Count; i++)
			{
				TableRow r = new TableRow();
				TableCell c;

				c = new TableCell();
				c.Text = Cf.Day(rsKeluar.Rows[i]["Tgl"]);
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rsKeluar.Rows[i]["CaraBayar"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rsKeluar.Rows[i]["AlatBayar"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = "";
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rsKeluar.Rows[i]["DibayarKepada"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rsKeluar.Rows[i]["Keterangan"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = "";
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Num(rsKeluar.Rows[i]["Nilai"]);
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = "";
				r.Cells.Add(c);

				rpt.Rows.Add(r);
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

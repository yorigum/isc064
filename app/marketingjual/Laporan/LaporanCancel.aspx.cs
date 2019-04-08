using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL.Laporan
{
	public partial class LaporanCancel : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.RadioButton statusS;
		protected System.Web.UI.WebControls.RadioButton statusA;
		protected System.Web.UI.WebControls.RadioButton statusB;
		
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

			DataTable rs;

			rs = Db.Rs("SELECT * FROM REF_JENIS ORDER BY SN");
			for(int i=0;i<rs.Rows.Count;i++)
			{
				string v = rs.Rows[i]["Jenis"].ToString();
				string t = v + " - " + rs.Rows[i]["Nama"].ToString();
				jenis.Items.Add(new ListItem(t,v));
				jenis.Items[i].Selected = true;
			}

			rs = Db.Rs("SELECT DISTINCT Lokasi FROM MS_KONTRAK ORDER BY Lokasi");
			for(int i=0;i<rs.Rows.Count;i++)
				lokasi.Items.Add(new ListItem(rs.Rows[i][0].ToString()));

			rs = Db.Rs("SELECT DISTINCT Principal FROM MS_AGENT WHERE Status = 'A' ORDER BY Principal");
			for(int i=0;i<rs.Rows.Count;i++)
				agent.Items.Add(new ListItem(rs.Rows[i][0].ToString()));

			lokasi.SelectedIndex = 0;
			agent.SelectedIndex = 0;
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

			if(!Cf.isPilih(jenis))
			{
				x = false;
				jenisc.Text = " Pilih Minimum Satu";
			}
			else
				jenisc.Text = "";

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
			
			string tgl = "";
			if(tglkontrak.Checked) tgl = tglkontrak.Text;

			DateTime Dari = Convert.ToDateTime(dari.Text);
			DateTime Sampai = Convert.ToDateTime(sampai.Text);
			Rpt.SubJudul(x
				, tgl + " : " + Cf.Day(dari.Text) + " s/d " + Cf.Day(sampai.Text)
				);

			Rpt.SubJudul(x
				, "Jenis : " + Rpt.inSql(jenis).Replace("'","")
				);

			Rpt.SubJudul(x
				, "Lokasi : " + lokasi.SelectedItem.Text
				);

			Rpt.SubJudul(x
                , "Sales : " + agent.SelectedItem.Text
				);

			Rpt.Header(rpt, x);
		}

		private void Fill()
		{
			string tgl = "";
			string order = "";
			if(tglkontrak.Checked)
			{
				tgl = "TglKontrak";
				order = "NoKontrak";
			}
			
			DateTime Dari = Convert.ToDateTime(dari.Text);
			DateTime Sampai = Convert.ToDateTime(sampai.Text);
			if(Dari>Sampai)
			{
				DateTime x = Sampai;
				Sampai = Dari;
				Dari = x;
			}

			string Lokasi = "";
			if(lokasi.SelectedIndex != 0)
			{
				Lokasi = " AND Lokasi = '" + Cf.Str(lokasi.SelectedValue) + "'";
			}

			string Agent = "";
			if(agent.SelectedIndex != 0)
			{
				Agent = " AND Principal = '" + Cf.Str(agent.SelectedValue) + "'";
			}

			decimal t1 = 0, t2 = 0, t3 = 0, t4 = 0;;

			string strSql = "SELECT "
				+ " NoKontrak"
				+ ",TglKontrak"
				+ ",Jenis"
				+ ",Lokasi"
				+ ",NoUnit"
				+ ",MS_CUSTOMER.Nama AS Cs"
				+ ",MS_AGENT.Nama AS Ag"
				+ ",MS_AGENT.Principal"
				+ ",Luas"
				+ ",Gross"
				+ ",DiskonRupiah"
				+ ",NilaiKontrak"
				+ ",MS_KONTRAK.Status"
				+ ",NoST"
				+ ",PersenLunas"
				+ ", Skema"
				+ ", BatalMasuk"
				+ ", AlasanBatal"
				+ " FROM MS_KONTRAK"
				+ " INNER JOIN MS_CUSTOMER ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
				+ " INNER JOIN MS_AGENT ON MS_KONTRAK.NoAgent = MS_AGENT.NoAgent "
				+ " WHERE 1=1 "
				+ " AND CONVERT(varchar,"+tgl+",112) >= '" + Cf.Tgl112(Dari) + "'"
				+ " AND CONVERT(varchar,"+tgl+",112) <= '" + Cf.Tgl112(Sampai) + "'"
				+ " AND Jenis IN (" + Rpt.inSql(jenis) + ")"
				+ " AND MS_KONTRAK.Status = 'B'"
				+ Lokasi
				+ Agent
				+ " ORDER BY " + order;
			
			DataTable rs = Db.Rs(strSql);
			
			decimal[] ADM = new decimal[rs.Rows.Count];
			decimal[] UangKeluar = new decimal[rs.Rows.Count];

			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				TableRow r = new TableRow();
				TableCell c;

				r.VerticalAlign = VerticalAlign.Top;
				r.Attributes["ondblclick"] = "popEditKontrak('" + rs.Rows[i]["NoKontrak"] + "')";

				c = new TableCell();
				c.Text = Cf.Pk(rs.Rows[i]["NoKontrak"]);
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Day(rs.Rows[i]["TglKontrak"]);
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Str(rs.Rows[i]["Cs"]);
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Str.Lantai(Cf.Pk(rs.Rows[i]["NoUnit"]));
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Str.NoUnit(Cf.Pk(rs.Rows[i]["NoUnit"]));
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Str(rs.Rows[i]["Jenis"]);
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Num(rs.Rows[i]["NilaiKontrak"]);
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Num(rs.Rows[i]["BatalMasuk"]);
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Num(BiayaADM(Cf.Pk(rs.Rows[i]["NoKontrak"])));
				ADM[i] = BiayaADM(Cf.Pk(rs.Rows[i]["NoKontrak"]));
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Num(VoidReimburse(Cf.Pk(rs.Rows[i]["NoKontrak"])));
				UangKeluar[i] = VoidReimburse(Cf.Pk(rs.Rows[i]["NoKontrak"]));
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Str(rs.Rows[i]["Ag"]);
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["AlasanBatal"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				rpt.Rows.Add(r);

				t1 = t1 + (decimal) rs.Rows[i]["NilaiKontrak"];
				t2 = t2 + (decimal) rs.Rows[i]["BatalMasuk"];
				t3 = t3 + (decimal) ADM[i];
				t4 = t4 + (decimal) UangKeluar[i];

				if(i==rs.Rows.Count-1)
				{
					SubTotal(t1, t2, t3, t4);
				}
			}
		}

		private decimal BiayaADM(string NoKontrak)
		{
			string strSql = "SELECT ISNULL(SUM(NilaiTagihan), 0)"
				+ " FROM MS_TAGIHAN"
				+ " WHERE NoKontrak = '" + NoKontrak + "'"
				+ " AND Tipe = 'ADM'"
				+ " AND UPPER(NamaTagihan) = 'BIAYA ADM. PEMBATALAN'"
				;
			decimal x = Db.SingleDecimal(strSql);

			return x;
		}

		private decimal VoidReimburse(string NoKontrak)
		{
			string strSql = "SELECT ISNULL(SUM(NilaiKembali), 0)"
				+ " FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS"
				+ " WHERE Ref = '" + NoKontrak + "'"
				;
			decimal x = Db.SingleDecimal(strSql);

			return x;
		}

		private void SubTotal(decimal t1, decimal t2, decimal t3, decimal t4)
		{
			TableRow r = new TableRow();
			TableCell c;

			c = Rpt.Foot();
			c.Text = "";
			c.ColumnSpan = 6;
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
			c.Text = "";
			c.ColumnSpan = 2;
			r.Cells.Add(c);

			rpt.Rows.Add(r);
		}

		protected void jenisCheck_CheckedChanged(object sender, System.EventArgs e)
		{
			for(int i = 0; i < jenis.Items.Count; i++)
			{
				jenis.Items[i].Selected = jenisCheck.Checked;
			}

			Js.Focus(this, jenisCheck);
			jenisc.Text = "";
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

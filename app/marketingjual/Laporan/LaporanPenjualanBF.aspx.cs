using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL.Laporan
{
	public partial class LaporanPenjualanBF : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.PlaceHolder report;
		protected System.Web.UI.WebControls.DropDownList ddlSkema;
	
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

			string strSql = "SELECT NoAgent, Nama FROM MS_AGENT WHERE Status = 'A' ORDER BY Nama";
			DataTable rs = Db.Rs(strSql);

			for(int i = 0; i < rs.Rows.Count; i++)
			{
				ddlAgent.Items.Add(new ListItem(rs.Rows[i]["Nama"].ToString(), Cf.Pk(rs.Rows[i]["NoAgent"])));
			}

			strSql = "SELECT DISTINCT Principal FROM MS_AGENT";
			rs = Db.Rs(strSql);

			for(int i = 0; i < rs.Rows.Count; i++)
			{
				cblPrincipal.Items.Add(new ListItem(rs.Rows[i]["Principal"].ToString()));
				cblPrincipal.Items[i].Selected = true;
			}

			strSql = "SELECT Nomor, Nama FROM REF_SKEMA WHERE Status = 'A'";
			rs = Db.Rs(strSql);

			for(int i = 0; i < rs.Rows.Count; i++)
			{
				cblSkema.Items.Add(new ListItem(rs.Rows[i]["Nama"].ToString() + " (" + Cf.Pk(rs.Rows[i]["Nomor"]).PadLeft(3, '0') + ")", Cf.Pk(rs.Rows[i]["Nomor"])));
				cblSkema.Items[i].Selected = true;
			}

			strSql = "SELECT * FROM REF_JENIS ORDER BY SN";
			rs = Db.Rs(strSql);

			for(int i = 0; i < rs.Rows.Count; i++)
			{
				cblTipe.Items.Add(new ListItem(rs.Rows[i]["Jenis"].ToString() + " - " + rs.Rows[i]["Nama"].ToString(), Cf.Pk(rs.Rows[i]["Jenis"].ToString())));
				cblTipe.Items[i].Selected = true;
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

			if(!Cf.isPilih(cblPrincipal))
			{
				x = false;
				if(s == "")
					s = cblPrincipal.ID;

				lblPrincipal.Text = "Pilih minimum satu";
			}else
				lblPrincipal.Text = "";

			if(!Cf.isPilih(cblSkema))
			{
				x = false;
				if(s == "")
					s = cblSkema.ID;

				lblSkema.Text = "Pilih minimum satu";
			}
			else
				lblSkema.Text = "";

			if(!Cf.isPilih(cblTipe))
			{
				x = false;
				if(s == "")
					s = cblTipe.ID;

				lblTipe.Text = "Pilih minimum satu";
			}
			else
				lblTipe.Text = "";

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

			string strAdd = "";

			if(ddlAgent.SelectedIndex != 0)
				strAdd += " AND a.NoAgent = " + ddlAgent.SelectedValue;

			string Principal = "";
			System.Text.StringBuilder z = new System.Text.StringBuilder();
			bool isFirst = true;
			for(int i = 0; i < cblPrincipal.Items.Count; i++)
			{
				if(cblPrincipal.Items[i].Selected)
				{
					if(isFirst)
					{
						z.Append("'" + Cf.Str(cblPrincipal.Items[i].Text) + "'");
						isFirst = false;
					}
					else
						z.Append(",'" + Cf.Str(cblPrincipal.Items[i].Text) + "'");
				}
			}

			if(z.ToString() != "")
				Principal = " AND c.Principal IN (" + z.ToString() + ")";

			string Skema = "";
			z = new System.Text.StringBuilder();
			isFirst = true;
			for(int i = 0; i < cblSkema.Items.Count; i++)
			{
				if(cblSkema.Items[i].Selected)
				{
					if(isFirst)
					{
						z.Append("'" + Cf.Str(cblSkema.Items[i].Text) + "'");
						isFirst = false;
					}
					else
						z.Append(",'" + Cf.Str(cblSkema.Items[i].Text) + "'");
				}
			}

			if(z.ToString() != "")
				Skema = " AND a.Skema IN (" + z.ToString() + ")";

			string Tipe = "";
			z = new System.Text.StringBuilder();
			isFirst = true;
			for(int i = 0; i < cblTipe.Items.Count; i++)
			{
				if(cblTipe.Items[i].Selected)
				{
					if(isFirst)
					{
						z.Append("'" + Cf.Str(cblTipe.Items[i].Value) + "'");
						isFirst = false;
					}
					else
						z.Append(",'" + Cf.Str(cblTipe.Items[i].Value) + "'");
				}
			}

			if(z.ToString() != "")
				Tipe = " AND d.Jenis IN (" + z.ToString() + ")";

			string strSql = "SELECT a.*, b.Nama AS Customer, c.Nama AS Agent, c.Principal, d.Jenis, d.PriceList"
				+ " FROM MS_KONTRAK a"
				+ " INNER JOIN MS_CUSTOMER b ON a.NoCustomer = b.NoCustomer"
				+ " INNER JOIN MS_AGENT c ON a.NoAgent = c.NoAgent"
				+ " INNER JOIN MS_UNIT d ON a.NoStock = d.NoStock"
				+ " WHERE a.TglKontrak >= '" + Convert.ToDateTime(Dari) + "'"
				+ " AND a.TglKontrak <= '" + Convert.ToDateTime(Sampai) + "'"
				+ Principal
				+ Skema
				+ Tipe
				+ " AND "
				+ "("
				+ "SELECT COUNT(*) FROM MS_PELUNASAN"
				+ " INNER JOIN MS_TAGIHAN ON MS_PELUNASAN.NoKontrak = MS_TAGIHAN.NoKontrak AND MS_PELUNASAN.NoTagihan = MS_TAGIHAN.NoUrut"
				+ " WHERE MS_TAGIHAN.Tipe NOT IN ('BF')"
				+ " AND MS_PELUNASAN.NoKontrak = a.NoKontrak"
				+ ") = 0"
				;
			DataTable rs = Db.Rs(strSql);

			decimal decTotalKontrak = 0, decTotalPriceList = 0, decTotalNilaiKontrak = 0, decTotalDiskon = 0, decTotalBayar = 0;
			
			for(int i = 0; i < rs.Rows.Count; i++)
			{
				if(!Response.IsClientConnected)
					break;

				TableRow r = new TableRow();
				r.Attributes["ondblclick"] = "popJadwalTagihan('" + Cf.Pk(rs.Rows[i]["NoKontrak"]) + "');";
				TableCell c;

				decTotalKontrak += 1;

				c = new TableCell();
				c.Text = Cf.Day(rs.Rows[i]["TglKontrak"]);
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = (i + 1).ToString();
				c.HorizontalAlign = HorizontalAlign.Center;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["NoKontrak"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Customer"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Agent"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Principal"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["NoUnit"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Jenis"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Num(rs.Rows[i]["PriceList"]);
				decTotalPriceList += Convert.ToDecimal(rs.Rows[i]["PriceList"]);
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Num(rs.Rows[i]["NilaiKontrak"]);
				decTotalNilaiKontrak += Convert.ToDecimal(rs.Rows[i]["NilaiKontrak"]);
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Num(rs.Rows[i]["DiskonRupiah"]);
				decTotalDiskon += Convert.ToDecimal(rs.Rows[i]["DiskonRupiah"]);
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Num(Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan), 0) FROM MS_PELUNASAN WHERE NoKontrak = '" + Cf.Pk(rs.Rows[i]["NoKontrak"]) + "'"));
				decTotalBayar += Convert.ToDecimal(c.Text);
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Skema"].ToString();
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				rpt.Rows.Add(r);

				bool x = false;

				if(i < (rs.Rows.Count - 1))
				{
					if(Convert.ToDateTime(rs.Rows[i + 1]["TglKontrak"]) > Convert.ToDateTime(rs.Rows[i]["TglKontrak"]))
						x = true;
				}
				else if(i == (rs.Rows.Count - 1))
					x = true;

				if(x)
				{
					SubTotal(decTotalKontrak, decTotalPriceList, decTotalNilaiKontrak, decTotalDiskon, decTotalBayar, Convert.ToDateTime(rs.Rows[i]["TglKontrak"]));
					decTotalKontrak = 0;
					decTotalPriceList = 0;
					decTotalNilaiKontrak = 0;
					decTotalDiskon = 0;
					decTotalBayar = 0;
				}
			}
		}

		private void SubTotal(decimal decTotalKontrak, decimal decTotalPriceList, decimal decTotalNilaiKontrak, decimal decTotalDiskon, decimal decTotalBayar, DateTime TglKontrak)
		{
			TableRow r = new TableRow();
			TableCell c;

			c = Rpt.Foot();
			c.Text = "SUBTOTAL UNIT PENJUALAN " + Cf.Day(TglKontrak);
			c.ColumnSpan = 3;
			c.HorizontalAlign = HorizontalAlign.Center;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = decTotalKontrak.ToString();
			c.HorizontalAlign = HorizontalAlign.Center;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = "SUBTOTAL PENJUALAN " + Cf.Day(TglKontrak);
			c.ColumnSpan = 4;
			c.HorizontalAlign = HorizontalAlign.Center;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(decTotalPriceList);
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(decTotalNilaiKontrak);
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(decTotalDiskon);
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(decTotalBayar);
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = "&nbsp;";
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
				, "Tanggal : " + Cf.Day(dari.Text) + " s/d " + Cf.Day(sampai.Text)
				);

			string strAgent = "SEMUA";
			if(ddlAgent.SelectedIndex != 0)
				strAgent = ddlAgent.SelectedItem.Text;
			Rpt.SubJudul(x
                , "Sales : " + strAgent
				);

			string strPrincipal = "SEMUA";
			System.Text.StringBuilder z = new System.Text.StringBuilder();
			bool isFirst = true;
			for(int i = 0; i < cblPrincipal.Items.Count; i++)
			{
				if(cblPrincipal.Items[i].Selected)
				{
					if(isFirst)
					{
						z.Append(cblPrincipal.Items[i].Text);
						isFirst = false;
					}
					else
						z.Append("," + cblPrincipal.Items[i].Text);
				}
			}

			if(z.ToString() != "")
				strPrincipal = z.ToString();
			Rpt.SubJudul(x
				, "Principal : " + strPrincipal
				);

			string strSkema = "SEMUA";
			z = new System.Text.StringBuilder();
			isFirst = true;
			for(int i = 0; i < cblSkema.Items.Count; i++)
			{
				if(cblSkema.Items[i].Selected)
				{
					if(isFirst)
					{
						z.Append(cblSkema.Items[i].Text);
						isFirst = false;
					}
					else
						z.Append("," + cblSkema.Items[i].Text);
				}
			}

			if(z.ToString() != "")
				strSkema = z.ToString();
			Rpt.SubJudul(x
				, "Skema Bayar : " + strSkema
				);

			string strTipe = "SEMUA";
			z = new System.Text.StringBuilder();
			isFirst = true;
			for(int i = 0; i < cblTipe.Items.Count; i++)
			{
				if(cblTipe.Items[i].Selected)
				{
					if(isFirst)
					{
						z.Append(cblTipe.Items[i].Text);
						isFirst = false;
					}
					else
						z.Append("," + cblTipe.Items[i].Text);
				}
			}

			if(z.ToString() != "")
				strTipe = z.ToString();
			Rpt.SubJudul(x
				, "Tipe : " + strTipe
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

		protected void cbPrincipal_CheckedChanged(object sender, System.EventArgs e)
		{
			for(int i = 0; i < cblPrincipal.Items.Count; i++)
			{
				cblPrincipal.Items[i].Selected = cbPrincipal.Checked;
			}
		}

		protected void cbSkema_CheckedChanged(object sender, System.EventArgs e)
		{
			for(int i = 0; i < cblSkema.Items.Count; i++)
			{
				cblSkema.Items[i].Selected = cbSkema.Checked;
			}
		}

		protected void cbTipe_CheckedChanged(object sender, System.EventArgs e)
		{
			for(int i = 0; i < cblTipe.Items.Count; i++)
			{
				cblTipe.Items[i].Selected = cbTipe.Checked;
			}
		}
	}
}

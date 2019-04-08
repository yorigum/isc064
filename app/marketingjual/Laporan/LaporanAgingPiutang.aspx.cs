using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.COLLECTION.Laporan
{
	public partial class LaporanAgingPiutang: System.Web.UI.Page
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
			tgl.Text = Cf.Day(DateTime.Now);

			string strSql = "SELECT NoAgent, Nama FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_AGENT";
			DataTable rs = Db.Rs(strSql);

			for(int i = 0; i < rs.Rows.Count; i++)
			{
				ddlAgent.Items.Add(new ListItem(rs.Rows[i]["Nama"].ToString(), Cf.Pk(rs.Rows[i]["NoAgent"])));
			}

			strSql = "SELECT DISTINCT Principal FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_AGENT";
			rs = Db.Rs(strSql);

			for(int i = 0; i < rs.Rows.Count; i++)
			{
				cblPrincipal.Items.Add(new ListItem(rs.Rows[i]["Principal"].ToString()));
				cblPrincipal.Items[i].Selected = true;
			}

			rs = Db.Rs("SELECT DISTINCT Lokasi FROM MS_KONTRAK ORDER BY Lokasi");
			for(int i = 0; i < rs.Rows.Count; i++)
				lokasi.Items.Add(new ListItem(rs.Rows[i][0].ToString()));
		}

		private bool valid()
		{
			string s = "";
			bool x = true;

			if(!Cf.isTgl(tgl))
			{
				x = false;
				if(s=="") s = tgl.ID;
				tglc.Text = "Tanggal";
			}
			else
				tglc.Text = "";

			if(!Cf.isPilih(cblPrincipal))
			{
				x = false;
				if(s == "")
					s = cblPrincipal.ID;

				lblPrincipal.Text = "Pilih minimum satu";
			}
			else
				lblPrincipal.Text = "";

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
				+ "LAPORAN AGING PIUTANG"
				+ "<br />"
				+ "PER " + Cf.Day(tgl.Text)
				;
			
			System.Text.StringBuilder x = new System.Text.StringBuilder();

			if(lokasi.SelectedIndex > 0)
				x.Append("Lokasi: " + lokasi.SelectedItem.Text + "<br />");

			if(ddlAgent.SelectedIndex != 0)
                x.Append("Sales: " + ddlAgent.SelectedValue);
			else
                x.Append("Sales: SEMUA");

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
			x.Append("<br />Principal: " + strPrincipal);

			x.Append("<br /><span style='font-weight: normal;'>Laporan dihasilkan pada hari : " + Cf.IndoWeek(DateTime.Today)
				+ ", " + Cf.Date(DateTime.Now)
				+ " dari workstation : " + Act.IP
				+ " dan username : " + Act.UserID
				+ "</span>"
				);
			
			lblSubHeader.Text = x.ToString();
			Fill();
		}

		private void Fill()
		{
			string strAdd = "";

			if(lokasi.SelectedIndex > 0)
				strAdd += " AND a.Lokasi = '" + lokasi.SelectedValue + "'";

			if(ddlAgent.SelectedIndex != 0)
				strAdd = " AND a.NoAgent = " + Cf.Pk(ddlAgent.SelectedValue);

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
				strAdd = " AND c.Principal IN (" + z.ToString() + ")";
			
			string strSql = "SELECT *, b.Nama AS NamaCustomer, c.Nama AS NamaAgent, c.Principal"
				+ " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK a"
				+ " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER b"
				+ " ON a.NoCustomer = b.NoCustomer"
				+ " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_AGENT c"
				+ " ON a.NoAgent = c.NoAgent"
				+ " WHERE 1=1 "
				+ strAdd
				;
			DataTable rs = Db.Rs(strSql);

			decimal t1 = 0, t2 = 0, t3 = 0, t4 = 0, t5 = 0, t6 = 0;
			int index = 1;

			for(int i = 0; i < rs.Rows.Count; i++)
			{
				if(!Response.IsClientConnected) break;

				decimal st1 = 0, st2 = 0, st3 = 0, st4 = 0, st5 = 0;

				TableRow tr = new TableRow();
				TableCell c;

				c = new TableCell();
				c.Text = index.ToString();
				c.HorizontalAlign = HorizontalAlign.Center;
				tr.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["NoKontrak"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				tr.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["NamaCustomer"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				tr.Cells.Add(c);

				c = new TableCell();
				decimal Total = TotalOutstanding(rs.Rows[i]["NoKontrak"].ToString());
				c.Text = Cf.Num(Total);
				c.HorizontalAlign = HorizontalAlign.Right;
				tr.Cells.Add(c);

				FillOutstanding(rs.Rows[i]["NoKontrak"].ToString(), ref t2, ref t3, ref t4, ref t5, ref t6,
					ref st1, ref st2, ref st3, ref st4, ref st5, ref index, tr);

				t1 += Total;

			}
			GrandTotal(t1, t2, t3, t4, t5, t6);
		}
		
		protected decimal TotalOutstanding(string NoKontrak)
		{
			decimal Nilai = 0;

			DataTable rs = Db.Rs("SELECT "
				+ "NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan), 0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN"
				+ " WHERE NoKontrak = '" + NoKontrak + "' AND NoTagihan = a.NoUrut"
				+ ") AS Sisa"
				+ " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN a"
				+ " WHERE NoKontrak = '" + NoKontrak + "'"
				+ " AND DATEDIFF(DAY, TglJT, '" + Cf.Tgl112(Convert.ToDateTime(tgl.Text)) + "') >= 0"
				+ " AND (SELECT ISNULL(SUM(NilaiPelunasan), 0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN"
				+ " WHERE NoKontrak = '" + NoKontrak + "' AND NoTagihan = a.NoUrut"
				+ ") < NilaiTagihan"
				);
			
			for(int i = 0; i < rs.Rows.Count; i++)
			{
				if(!Response.IsClientConnected) break;

				Nilai += Convert.ToDecimal(rs.Rows[i]["Sisa"]);
			}

			return Nilai;
		}

		protected void FillOutstanding(string NoKontrak, ref decimal t2, ref decimal t3, ref decimal t4, ref decimal t5, ref decimal t6, 
			ref decimal st1, ref decimal st2, ref decimal st3, ref decimal st4, ref decimal st5, ref int index, TableRow tr)
		{
			DateTime AsOf = Convert.ToDateTime(tgl.Text);
			DataTable rs = Db.Rs("SELECT *"
				+ ", DATEDIFF(DAY, TglJT, '" + Cf.Tgl112(AsOf) + "') AS Telat"
				+ ", NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan), 0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN"
				+ " WHERE NoKontrak = '" + NoKontrak + "' AND NoTagihan = a.NoUrut"
				+ ") AS Sisa"
				+ " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN a"
				+ " WHERE NoKontrak = '" + NoKontrak + "'"
				+ " AND DATEDIFF(DAY, TglJT, '" + Cf.Tgl112(AsOf) + "') >= 0"
				+ " AND (SELECT ISNULL(SUM(NilaiPelunasan), 0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN"
				+ " WHERE NoKontrak = '" + NoKontrak + "' AND NoTagihan = a.NoUrut"
				+ ") < NilaiTagihan"
				+ " ORDER BY NoUrut"
				);

			for(int i = 0; i < rs.Rows.Count; i++)
			{
				if(!Response.IsClientConnected) break;

				int Telat = Convert.ToInt32(rs.Rows[i]["Telat"]);
				decimal Sisa = Convert.ToDecimal(rs.Rows[i]["Sisa"]);

				TableCell c;

				if(i > 0)
				{
					tr = new TableRow();
					c = new TableCell();
					c.ColumnSpan = 4;
					tr.Cells.Add(c);
				}

				c = new TableCell();
				c.Text = rs.Rows[i]["NoKontrak"] + "." + rs.Rows[i]["NoUrut"] + " " + rs.Rows[i]["NamaTagihan"].ToString();
				tr.Cells.Add(c);

				if(Telat >= 0 && Telat <= 30)
				{
					t2 += Sisa;
					st1 += Sisa;

					c = new TableCell();
					c.Text = Cf.Num(Sisa);
					c.HorizontalAlign = HorizontalAlign.Right;
					tr.Cells.Add(c);

					c = new TableCell();
					c.Text = Telat.ToString();
					c.HorizontalAlign = HorizontalAlign.Center;
					tr.Cells.Add(c);
				}
				else
				{
					c = new TableCell();
					c.ColumnSpan = 2;
					tr.Cells.Add(c);
				}

				if(Telat >= 31 && Telat <= 60)
				{
					t3 += Sisa;
					st2 += Sisa;

					c = new TableCell();
					c.Text = Cf.Num(Sisa);
					c.HorizontalAlign = HorizontalAlign.Right;
					tr.Cells.Add(c);

					c = new TableCell();
					c.Text = Telat.ToString();
					c.HorizontalAlign = HorizontalAlign.Center;
					tr.Cells.Add(c);
				}
				else
				{
					c = new TableCell();
					c.ColumnSpan = 2;
					tr.Cells.Add(c);
				}

				if(Telat >= 61 && Telat <= 90)
				{
					t4 += Sisa;
					st3 += Sisa;

					c = new TableCell();
					c.Text = Cf.Num(Sisa);
					c.HorizontalAlign = HorizontalAlign.Right;
					tr.Cells.Add(c);

					c = new TableCell();
					c.Text = Telat.ToString();
					c.HorizontalAlign = HorizontalAlign.Center;
					tr.Cells.Add(c);
				}
				else
				{
					c = new TableCell();
					c.ColumnSpan = 2;
					tr.Cells.Add(c);
				}

				if(Telat > 90)
				{
					t5 += Sisa;
					st4 += Sisa;

					c = new TableCell();
					c.Text = Cf.Num(Sisa);
					c.HorizontalAlign = HorizontalAlign.Right;
					tr.Cells.Add(c);

					c = new TableCell();
					c.Text = Telat.ToString();
					c.HorizontalAlign = HorizontalAlign.Center;
					tr.Cells.Add(c);
				}
				else
				{
					c = new TableCell();
					c.ColumnSpan = 2;
					tr.Cells.Add(c);
				}

				decimal den = Db.SingleDecimal("SELECT Denda FROM MS_TAGIHAN WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' AND KPR != 1 AND NoUrut = '" + rs.Rows[i]["NoUrut"] + "' ORDER BY NoUrut");

				c = new TableCell();
				c.Text = Cf.Num(den);
				c.HorizontalAlign = HorizontalAlign.Right;
				tr.Cells.Add(c);

				rpt.Rows.Add(tr);

				t6 += Convert.ToDecimal(den);
				st5 += Convert.ToDecimal(den);
			}

			if(rs.Rows.Count > 0)
			{
				index++;
				SubTotal(st1, st2, st3, st4, st5);
			}
		}

		protected void SubTotal(decimal st1, decimal st2, decimal st3, decimal st4, decimal st5)
		{
			TableRow r = new TableRow();
			TableCell c;

			c = Rpt.Foot();
			c.Text = "SUB TOTAL";
			c.ColumnSpan = 5;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(st1);
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = "&nbsp;";
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(st2);
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = "&nbsp;";
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(st3);
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = "&nbsp;";
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(st4);
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = "&nbsp;";
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(st5);
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			rpt.Rows.Add(r);
		}

		protected void GrandTotal(decimal t1, decimal t2, decimal t3, decimal t4, decimal t5, decimal t6)
		{
			TableRow r = new TableRow();
			TableCell c;

			c = Rpt.Foot();
			c.Text = "GRAND TOTAL";
			c.ColumnSpan = 3;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(t1);
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = "&nbsp;";
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(t2);
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = "&nbsp;";
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(t3);
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = "&nbsp;";
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(t4);
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = "&nbsp;";
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(t5);
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = "&nbsp;";
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

		protected void cbPrincipal_CheckedChanged(object sender, System.EventArgs e)
		{
			for(int i = 0; i < cblPrincipal.Items.Count; i++)
			{
				cblPrincipal.Items[i].Selected = cbPrincipal.Checked;
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

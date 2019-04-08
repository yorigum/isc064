using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL.Laporan
{
	public partial class OutstandingTagihan : System.Web.UI.Page
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
				+ "Laporan Outstanding Tagihan"
				+ "<br />"
				+ "Per " + Cf.Day(tgl.Text)
				;
			
			System.Text.StringBuilder x = new System.Text.StringBuilder();

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

		
			string order = "";
			if(toponly.Checked) order = "ORDER BY b.Nama, a.nokontrak";


			string strSql = "SELECT *, b.Nama AS NamaCustomer, c.Nama AS NamaAgent, c.Principal"
				+ " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK a"
				+ " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER b"
				+ " ON a.NoCustomer = b.NoCustomer"
				+ " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_AGENT c"
				+ " ON a.NoAgent = c.NoAgent"
				+ strAdd
				+ order
				;
			DataTable rs = Db.Rs(strSql);

			decimal t1 = 0, t2 = 0, t3 = 0, t4 = 0, t5 = 0, t6 = 0, t7 = 0, t8 = 0;

			for(int i = 0; i < rs.Rows.Count; i++)
			{
				if(!Response.IsClientConnected)
					break;

				decimal Total = 0;
				string Ket = "";

				TableRow r = new TableRow();
				TableCell c;

				c = new TableCell();
				c.HorizontalAlign = HorizontalAlign.Center;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["NoUnit"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["NoKontrak"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["NamaCustomer"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["NamaAgent"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Principal"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Outstanding(rs.Rows[i]["NoKontrak"].ToString(), 0, 3, "NOMINAL");
				t8 = t8 + Convert.ToDecimal(c.Text);
				Total = Total + Convert.ToDecimal(c.Text);
				if(c.Text == "0")
					c.Text = "";
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Outstanding(rs.Rows[i]["NoKontrak"].ToString(), 0, 3, "TELAT");
				Ket = Outstanding(rs.Rows[i]["NoKontrak"].ToString(), 0, 3, "KET");
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Outstanding(rs.Rows[i]["NoKontrak"].ToString(), 3, 7, "NOMINAL");
				t1 = t1 + Convert.ToDecimal(c.Text);
				Total = Total + Convert.ToDecimal(c.Text);
				if(c.Text == "0")
					c.Text = "";
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Outstanding(rs.Rows[i]["NoKontrak"].ToString(), 3, 7, "TELAT");
				Ket = Outstanding(rs.Rows[i]["NoKontrak"].ToString(), 3, 7, "KET");
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Outstanding(rs.Rows[i]["NoKontrak"].ToString(), 7, 15, "NOMINAL");
				t2 = t2 + Convert.ToDecimal(c.Text);
				Total = Total + Convert.ToDecimal(c.Text);
				if(c.Text == "0")
					c.Text = "";
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Outstanding(rs.Rows[i]["NoKontrak"].ToString(), 7, 15, "TELAT");
				if(c.Text != "" && Ket != "")
					Ket += ";";
				Ket += Outstanding(rs.Rows[i]["NoKontrak"].ToString(), 7, 15, "KET");
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Outstanding(rs.Rows[i]["NoKontrak"].ToString(), 15, 30, "NOMINAL");
				t3 = t3 + Convert.ToDecimal(c.Text);
				Total = Total + Convert.ToDecimal(c.Text);
				if(c.Text == "0")
					c.Text = "";
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Outstanding(rs.Rows[i]["NoKontrak"].ToString(), 15, 30, "TELAT");
				if(c.Text != "" && Ket != "")
					Ket += ";";
				Ket = Ket + Outstanding(rs.Rows[i]["NoKontrak"].ToString(), 15, 30, "KET");
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Outstanding(rs.Rows[i]["NoKontrak"].ToString(), 30, 60, "NOMINAL");
				t4 = t4 + Convert.ToDecimal(c.Text);
				Total = Total + Convert.ToDecimal(c.Text);
				if(c.Text == "0")
					c.Text = "";
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);
				
				c = new TableCell();
				c.Text = Outstanding(rs.Rows[i]["NoKontrak"].ToString(), 30, 60, "TELAT");		
				if(c.Text != "" && Ket != "")
					Ket += ";";
				Ket = Ket + Outstanding(rs.Rows[i]["NoKontrak"].ToString(), 30, 60, "KET");
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Outstanding(rs.Rows[i]["NoKontrak"].ToString(), 60, 90, "NOMINAL");
				t5 = t5 + Convert.ToDecimal(c.Text);
				Total = Total + Convert.ToDecimal(c.Text);
				if(c.Text == "0")
					c.Text = "";
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Outstanding(rs.Rows[i]["NoKontrak"].ToString(), 60, 90, "TELAT");
				if(c.Text != "" && Ket != "")
					Ket += ";";
				Ket = Ket + Outstanding(rs.Rows[i]["NoKontrak"].ToString(), 60, 90, "KET");
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Outstanding(rs.Rows[i]["NoKontrak"].ToString(), 90, 0, "NOMINAL");
				t6 = t6 + Convert.ToDecimal(c.Text);
				Total = Total + Convert.ToDecimal(c.Text);
				if(c.Text == "0")
					c.Text = "";
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Outstanding(rs.Rows[i]["NoKontrak"].ToString(), 90, 0, "TELAT");
				if(c.Text != "" && Ket != "")
					Ket += ";";
				Ket = Ket + Outstanding(rs.Rows[i]["NoKontrak"].ToString(), 90, 0, "KET");
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Num(Total);
				t7 = t7 + Convert.ToDecimal(c.Text);
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Ket;
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				if(Total > 0)
					rpt.Rows.Add(r);

				if(i == (rs.Rows.Count - 1))
					SubTotal(t1, t2, t3, t4, t5, t6, t7, t8);
			}

			int y = 0;
			for(int x = 4; x < (rpt.Rows.Count - 1); x++)
			{
				y++;
				rpt.Rows[x].Cells[0].Text = y.ToString();
			}
		}

		private string Outstanding(string NoKontrak, int Interval1, int Interval2, string Tipe)
		{
			System.Text.StringBuilder x = new System.Text.StringBuilder();
			string strSql = "SELECT *, DATEDIFF(day, TglJT, '" + Convert.ToDateTime(tgl.Text) + "') AS Telat"
				+ " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN"
				+ " WHERE NoKontrak = '" + NoKontrak + "'"
				+ " AND DATEDIFF(day, TglJT, '" + Convert.ToDateTime(tgl.Text) + "') > " + Interval1
				;

			if(Interval2 != 0)
				strSql += " AND DATEDIFF(day, TglJT, '" + Convert.ToDateTime(tgl.Text) + "') <= " + Interval2;

			DataTable rs = Db.Rs(strSql);

			decimal Nominal = 0;

			for(int i = 0; i < rs.Rows.Count; i++)
			{
				if(!Response.IsClientConnected)
					break;

				strSql = "SELECT ISNULL(SUM(NilaiPelunasan), 0)"
					+ " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN"
					+ " WHERE NoKontrak = '" + NoKontrak + "'"
					+ " AND NoTagihan = " + Cf.Pk(rs.Rows[i]["NoUrut"])
					;
				decimal TotalPelunasan = Db.SingleDecimal(strSql);
				decimal TotalTagihan = Convert.ToDecimal(rs.Rows[i]["NilaiTagihan"]);

				if(TotalPelunasan < TotalTagihan)
				{
					if(Tipe == "NOMINAL")
					{
						decimal SisaTagihan = TotalTagihan - TotalPelunasan;
						Nominal = Nominal + SisaTagihan;
					}
					else if(Tipe == "TELAT")
					{
						if(x.ToString() != "" && rs.Rows[i]["Telat"].ToString() != "")
							x.Append("/");
						x.Append(rs.Rows[i]["Telat"].ToString());
					}
					else if(Tipe == "KET")
					{
						if(x.ToString() != "" && rs.Rows[i]["NamaTagihan"].ToString() != "")
							x.Append("/");
						x.Append(rs.Rows[i]["NamaTagihan"].ToString());
					}
				}
			}

			if(Tipe == "NOMINAL")
				return Cf.Num(Nominal);
			else
				return x.ToString();
		}

		private void SubTotal(decimal t1, decimal t2, decimal t3, decimal t4, decimal t5, decimal t6, decimal t7, decimal t8)
		{
			TableRow r = new TableRow();
			TableCell c;

			c = Rpt.Foot();
			c.ColumnSpan = 6;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(t8);
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.ColumnSpan = 1;
			r.Cells.Add(c);
			
			c = Rpt.Foot();
			c.Text = Cf.Num(t1);
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.ColumnSpan = 1;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(t2);
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.ColumnSpan = 1;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(t3);
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.ColumnSpan = 1;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(t4);
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.ColumnSpan = 1;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(t5);
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.ColumnSpan = 1;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(t6);
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.ColumnSpan = 1;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(t7);
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

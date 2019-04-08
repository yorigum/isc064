using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL.Laporan
{
	public partial class MasterTagihan : System.Web.UI.Page
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

			if(!Cf.isPilih(tipe))
			{
				x = false;
				tipec.Text = " Pilih Minimum Satu";
			}
			else
				tipec.Text = "";

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

			if(statusA.Checked)
				Rpt.SubJudul(x, "Status : " + statusA.Text);
			else if(statusB.Checked)
				Rpt.SubJudul(x, "Status : " + statusB.Text);
			else
				Rpt.SubJudul(x, "Status : " + statusS.Text);

			string tgl = "";
			if(tglkontrak.Checked) tgl = tglkontrak.Text;
			if(tgljt.Checked) tgl = tgljt.Text;

			DateTime Dari = Convert.ToDateTime(dari.Text);
			DateTime Sampai = Convert.ToDateTime(sampai.Text);
			Rpt.SubJudul(x
				, tgl + " : " + Cf.Day(dari.Text) + " s/d " + Cf.Day(sampai.Text)
				);

			Rpt.SubJudul(x
				, "Tipe : " + Rpt.inSql(tipe).Replace("'","")
				);

			Rpt.Header(rpt, x);
		}

		private void Fill()
		{
			string Status = "";
			if(statusA.Checked) Status = " AND MS_KONTRAK.Status = 'A'";
			if(statusB.Checked) Status = " AND MS_KONTRAK.Status = 'B'";

			string tgl = "";
			string order = "";
			if(tglkontrak.Checked)
			{
				tgl = "MS_KONTRAK.TglKontrak";
				order = "MS_KONTRAK.NoKontrak, NoUrut";
			}
			if(tgljt.Checked)
			{
				tgl = "MS_TAGIHAN.TglJT";
				order = "MS_TAGIHAN.TglJT, MS_KONTRAK.NoKontrak";
			}

			DateTime Dari = Convert.ToDateTime(dari.Text);
			DateTime Sampai = Convert.ToDateTime(sampai.Text);
			if(Dari>Sampai)
			{
				DateTime x = Sampai;
				Sampai = Dari;
				Dari = x;
			}

			decimal total = 0;
			decimal t2 = 0;

			string strSql = "SELECT "
				+ " MS_TAGIHAN.NoKontrak"
				+ ",MS_TAGIHAN.NoUrut"
				+ ",MS_TAGIHAN.Tipe"
				+ ",MS_KONTRAK.TglKontrak"
				+ ",MS_KONTRAK.NoUnit"
				+ ",MS_CUSTOMER.Nama AS Cs"
				+ ",MS_TAGIHAN.NamaTagihan"
				+ ",MS_TAGIHAN.TglJT"
				+ ",MS_TAGIHAN.NilaiTagihan"
				+ ",MS_KONTRAK.Status"
				+ ",DATEDIFF(day,GETDATE(),TglJT) AS Diff"
				+ ",(SELECT ISNULL(SUM(NilaiPelunasan),0) FROM MS_PELUNASAN WHERE NoKontrak = MS_KONTRAK.NoKontrak AND NoTagihan = MS_TAGIHAN.NoUrut) AS TotalPelunasan"
				+ " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
				+ " INNER JOIN MS_TAGIHAN ON MS_KONTRAK.NoKontrak = MS_TAGIHAN.NoKontrak"
				+ " WHERE 1=1 "
				+ " AND CONVERT(varchar,"+tgl+",112) >= '" + Cf.Tgl112(Dari) + "'"
				+ " AND CONVERT(varchar,"+tgl+",112) <= '" + Cf.Tgl112(Sampai) + "'"
				+ " AND MS_TAGIHAN.Tipe IN (" + Rpt.inSql(tipe) + ")"
				+ Status
				+ " ORDER BY " + order;
			
			DataTable rs = Db.Rs(strSql);
			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				TableRow r = new TableRow();
				TableCell c;

				r.VerticalAlign = VerticalAlign.Top;
				r.Attributes["ondblclick"] = "popJadwalTagihan('"+rs.Rows[i]["NoKontrak"]+"')";

				string jt = "";
				if(Convert.ToInt32(rs.Rows[i]["Diff"])<=0)
					jt = " **";

				c = new TableCell();
				c.Text = rs.Rows[i]["NoKontrak"]+ "." + rs.Rows[i]["NoUrut"]
					+ jt
					;
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Status"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Day(rs.Rows[i]["TglKontrak"]);
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

				c = new TableCell();
				c.Text = rs.Rows[i]["Tipe"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["NamaTagihan"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Day(rs.Rows[i]["TglJT"]);
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Num(rs.Rows[i]["NilaiTagihan"]);
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = DetilLunas(rs.Rows[i]["NoKontrak"].ToString()
					,(int)rs.Rows[i]["NoUrut"]
					);
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Num((decimal)rs.Rows[i]["NilaiTagihan"]
					- (decimal)rs.Rows[i]["TotalPelunasan"]
					);
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				rpt.Rows.Add(r);

				total = total + (decimal)rs.Rows[i]["NilaiTagihan"];
				t2 = t2 + (decimal)rs.Rows[i]["TotalPelunasan"];

				if(i==rs.Rows.Count-1)
					SubTotal("GRAND TOTAL", total, t2);
			}
		}

		private string DetilLunas(string NoKontrak, int NoTagihan)
		{
			System.Text.StringBuilder x = new System.Text.StringBuilder();

			DataTable rs = Db.Rs("SELECT CaraBayar, TglPelunasan, Ket FROM MS_PELUNASAN"
				+ " WHERE NoKontrak = '"+NoKontrak+"' AND NoTagihan = " + NoTagihan
				);
			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(x.Length!=0) x.Append("<br>");
				x.Append(rs.Rows[i]["CaraBayar"] + ", " + Cf.Day(rs.Rows[i]["TglPelunasan"]) + " "
					+ rs.Rows[i]["Ket"]);
			}

			return x.ToString();
		}

		private void SubTotal(string txt, decimal total, decimal t2)
		{
			TableRow r = new TableRow();
			TableCell c;

			c = Rpt.Foot();
			c.Text = txt;
			c.ColumnSpan = 8;
			c.HorizontalAlign = HorizontalAlign.Left;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(total);
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = "&nbsp;";
			c.HorizontalAlign = HorizontalAlign.Left;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(total-t2);
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			rpt.Rows.Add(r);
		}

		protected void tipeCheck_CheckedChanged(object sender, System.EventArgs e)
		{
			for(int i = 0; i < tipe.Items.Count; i++)
			{
				tipe.Items[i].Selected = tipeCheck.Checked;
			}

			Js.Focus(this, tipeCheck);
			tipec.Text = "";
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

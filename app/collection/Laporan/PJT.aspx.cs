using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.COLLECTION.Laporan
{
	public partial class PJT : System.Web.UI.Page
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

			//tipe
			string[] x = Sc.MktCatalog();
			for(int i=0;i<=x.GetUpperBound(0);i++)
			{
				string[] xdetil = x[i].Split(';');
				tipe.Items.Add(new ListItem(xdetil[2],xdetil[1]));
				tipe.Items[i].Selected = true;
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

			Rpt.SubJudul(x
				, "Tipe : " + Rpt.inSql(tipe).Replace("'","")
				);

			DateTime Dari = Convert.ToDateTime(dari.Text);
			DateTime Sampai = Convert.ToDateTime(sampai.Text);
			Rpt.SubJudul(x
				, "Periode : " + Cf.Day(dari.Text) + " s/d " + Cf.Day(sampai.Text)
				);

			if(statusA.Checked)
				Rpt.SubJudul(x, "Status : " + statusA.Text);
			else if(statusB.Checked)
				Rpt.SubJudul(x, "Status : " + statusB.Text);
			else
				Rpt.SubJudul(x, "Status : " + statusS.Text);

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

			string Status = "";
			if(statusA.Checked) Status = " AND b.Status = 'A'";
			if(statusB.Checked) Status = " AND b.Status = 'B'";

			decimal t1 = 0;

			string strSql = "SELECT * "
				+ " FROM MS_PJT a"
				+ " INNER JOIN ISC064_MARKETINGJUAL..MS_KONTRAK b"
				+ " ON a.Ref = b.NoKontrak"
				+ " WHERE 1=1 "
				+ " AND CONVERT(varchar,TglPJT,112) >= '" + Cf.Tgl112(Dari) + "'"
				+ " AND CONVERT(varchar,TglPJT,112) <= '" + Cf.Tgl112(Sampai) + "'"
				+ " AND Tipe IN (" + Rpt.inSql(tipe) + ")"
				+ Status
				+ " ORDER BY NoPJT";
			
			DataTable rs = Db.Rs(strSql);
			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				TableRow r = new TableRow();
				TableCell c;

				r.VerticalAlign = VerticalAlign.Top;
				r.Attributes["ondblclick"] = "popEditPJT('"+rs.Rows[i]["NoPJT"]+"')";

				c = new TableCell();
				c.Text = rs.Rows[i]["NoPJT"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Day(rs.Rows[i]["TglPJT"]);
				c.Wrap = false;
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Tipe"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Ref"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Unit"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Customer"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Status"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				if(!detil.Checked)
					c.Text = Cf.Num(rs.Rows[i]["Total"]);
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				rpt.Rows.Add(r);

				if(detil.Checked)
					Detil(rs.Rows[i]["NoPJT"].ToString(),(decimal)rs.Rows[i]["Total"]);

				t1 = t1 + (decimal)rs.Rows[i]["Total"];

				if(i==rs.Rows.Count-1)
					SubTotal("GRAND TOTAL", t1);
			}
		}

		private void Detil(string NoPJT, decimal Total)
		{
			string strSql = "SELECT * "
				+ " FROM MS_PJT_DETIL"
				+ " WHERE NoPJT = '" + NoPJT + "'"
				+ " ORDER BY NoUrut";
			
			DataTable rs = Db.Rs(strSql);
			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				TableRow r = new TableRow();
				TableCell c;

				r.VerticalAlign = VerticalAlign.Top;

				c = new TableCell();
				c.ColumnSpan = 4;
				r.Cells.Add(c);

				c = new TableCell();
				c.ColumnSpan = 2;
				c.Text = rs.Rows[i]["NamaTagihan"].ToString()
					+ ", " + Cf.Day(rs.Rows[i]["TglJT"]);
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = "";
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Num(rs.Rows[i]["Nilai"]);
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				rpt.Rows.Add(r);

				if(i==rs.Rows.Count-1)
				{
					r = new TableRow();

					c = new TableCell();
					c.ColumnSpan = 7;
					c.Text = "Total :";
					c.HorizontalAlign = HorizontalAlign.Right;
					r.Cells.Add(c);

					c = new TableCell();
					c.Text = Cf.Num(Total);
					c.Font.Bold = true;
					c.Attributes["style"] = "border-top:1px solid black";
					c.HorizontalAlign = HorizontalAlign.Right;
					r.Cells.Add(c);

					rpt.Rows.Add(r);
				}
			}
		}

		private void SubTotal(string txt, decimal t1)
		{
			TableRow r = new TableRow();
			TableCell c;

			c = Rpt.Foot();
			c.Text = txt;
			c.ColumnSpan = 7;
			c.HorizontalAlign = HorizontalAlign.Left;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(t1);
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

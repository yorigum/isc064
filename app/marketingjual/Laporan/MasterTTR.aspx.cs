using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL.Laporan
{
	public partial class MasterTTR : System.Web.UI.Page
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

			DataTable rs;

			rs = Db.Rs("SELECT DISTINCT UserID FROM MS_TTR ORDER BY UserID");
			for(int i = 0; i < rs.Rows.Count; i++)
				kasir.Items.Add(new ListItem(
					rs.Rows[i][0].ToString()));

			kasir.SelectedIndex = 0;
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

			if(!Cf.isPilih(carabayar))
			{
				x = false;
				carabayarc.Text = " Pilih Minimum Satu";
			}
			else
				carabayarc.Text = "";

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
				, "Cara Bayar : " + Rpt.inSql(carabayar).Replace("'","")
				);

			string tgl = "";
			if(tglttr.Checked) tgl = tglttr.Text;
			if(tglinput.Checked) tgl = tglinput.Text;
			if(tglbg.Checked) tgl = tglbg.Text;

			DateTime Dari = Convert.ToDateTime(dari.Text);
			DateTime Sampai = Convert.ToDateTime(sampai.Text);
			Rpt.SubJudul(x
				, tgl + " : " + Cf.Day(dari.Text) + " s/d " + Cf.Day(sampai.Text)
				);

			Rpt.SubJudul(x
				, "Kasir : " + kasir.SelectedItem.Text
				);

			if(statusV.Checked)
				Rpt.SubJudul(x, "Status : " + statusV.Text);
			else if(statusP.Checked)
				Rpt.SubJudul(x, "Status : " + statusP.Text);
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
			if(Dari > Sampai)
			{
				DateTime x = Sampai;
				Sampai = Dari;
				Dari = x;
			}

			string UserID = "";
			if(kasir.SelectedIndex!=0)
				UserID = " AND UserID = '" + kasir.SelectedValue + "'";

			string Status = "";
			if(statusB.Checked) Status = " AND Status = 'BARU'";
			if(statusP.Checked) Status = " AND Status = 'POST'";
			if(statusV.Checked) Status = " AND Status = 'VOID'";

			decimal t1 = 0;
			decimal t2 = 0;

			string tgl = "";
			if(tglttr.Checked) tgl = "TglTTR";
			if(tglinput.Checked) tgl = "TglInput";
			if(tglbg.Checked) tgl = "TglBG";

			string strSql = "SELECT * "
				+ " FROM MS_TTR"
				+ " WHERE 1 = 1"
				+ " AND CONVERT(VARCHAR, " + tgl + ", 112) >= '" + Cf.Tgl112(Dari) + "'"
				+ " AND CONVERT(VARCHAR," + tgl + ", 112) <= '" + Cf.Tgl112(Sampai) + "'"
				+ " AND CaraBayar IN (" + Rpt.inSql(carabayar) + ")"
				+ UserID
				+ Status
				+ " ORDER BY NoTTR";
			
			DataTable rs = Db.Rs(strSql);

			DataTable rsGiro = Db.Rs(
				"SELECT DISTINCT NoBG"
				+ " FROM MS_TTR"
				+ " WHERE 1 = 1"
				+ " AND CONVERT(VARCHAR, " + tgl + ", 112) >= '" + Cf.Tgl112(Dari) + "'"
				+ " AND CONVERT(VARCHAR, " + tgl + ", 112) <= '" + Cf.Tgl112(Sampai) + "'"
				+ " AND CaraBayar IN (" + Rpt.inSql(carabayar) + ")"
				+ UserID
				+ Status
				+ " AND NoBG <> ''"
				);
			int LembarGiro = rsGiro.Rows.Count;

			for(int i = 0; i < rs.Rows.Count; i++)
			{
				if(!Response.IsClientConnected) break;

				TableRow r = new TableRow();
				TableCell c;

				r.VerticalAlign = VerticalAlign.Top;
				r.Attributes["ondblclick"] = "popEditTTR('"+rs.Rows[i]["NoTTR"]+"')";

				c = new TableCell();
				c.Text = rs.Rows[i]["NoTTR"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Status"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Day(rs.Rows[i]["TglTTR"]);
				c.Wrap = false;
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["UserID"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["IP"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["NoReservasi"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Unit"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				c.Wrap = false;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Customer"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				c.Wrap = false;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["CaraBayar"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Ket"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				c.Wrap = false;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["NoBG"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				c.Wrap = false;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Day(rs.Rows[i]["TglBG"]);
				c.HorizontalAlign = HorizontalAlign.Left;
				c.Wrap = false;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Num(rs.Rows[i]["Total"]);
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Num(rs.Rows[i]["NilaiKembali"]);
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				rpt.Rows.Add(r);

				t1 = t1 + (decimal)rs.Rows[i]["Total"];
				t2 = t2 + (decimal)rs.Rows[i]["NilaiKembali"];

				if(i==rs.Rows.Count-1)
				{
					SubTotal("GRAND TOTAL", t1, t2);
					Giro(LembarGiro);
				}
			}
		}

		private void Giro(int LembarGiro)
		{	
			TableRow r = new TableRow();
			TableCell c;

			c = new TableCell();
			c.ColumnSpan = 14;
			c.Text = "<strong>Lembar Giro: </strong>" + LembarGiro.ToString();
			r.Cells.Add(c);

			rpt.Rows.Add(r);
		}

		private void SubTotal(string txt, decimal t1, decimal t2)
		{
			TableRow r = new TableRow();
			TableCell c;

			c = Rpt.Foot();
			c.Text = txt;
			c.ColumnSpan = 12;
			c.HorizontalAlign = HorizontalAlign.Left;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(t1);
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(t2);
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			rpt.Rows.Add(r);
		}

		protected void carabayarCheck_CheckedChanged(object sender, System.EventArgs e)
		{
			for(int i = 0; i < carabayar.Items.Count; i++)
			{
				carabayar.Items[i].Selected = carabayarCheck.Checked;
			}

			Js.Focus(this, carabayarCheck);
			carabayarc.Text = "";
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

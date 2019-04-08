using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL.Laporan
{
	public partial class OutstandingTagihan2 : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();

			if(!Page.IsPostBack)
			{
				comp.InnerHtml = Mi.Pt;
				rpt.Visible = false;
				Js.Focus(this, scr);
				init();
				if(!Act.Sec("DownloadExcel")) xls.Enabled = false;
			}
		}

		private void init()
		{
			tgl.Text = Cf.Day(DateTime.Today);
			
			DataTable rsAg = Db.Rs("SELECT NoAgent, Nama FROM MS_AGENT");
			for(int i = 0; i < rsAg.Rows.Count; i++)
			{
				agent.Items.Add(new ListItem(rsAg.Rows[i]["Nama"].ToString(), rsAg.Rows[i]["NoAgent"].ToString()));
			}

			DataTable rsLok = Db.Rs("SELECT DISTINCT Lokasi FROM MS_UNIT");
			for(int i = 0; i < rsLok.Rows.Count; i++)
			{
				lokasi.Items.Add(new ListItem(rsLok.Rows[i]["Lokasi"].ToString(), rsLok.Rows[i]["Lokasi"].ToString()));
			}

			lokasi.SelectedIndex = 0;
			agent.SelectedIndex = 0;
		}

		protected void Report()
		{
			param.Visible = false;
			rpt.Visible = true;

			Header();
			Fill();
		}

		protected void Header()
		{
			System.Text.StringBuilder x = new System.Text.StringBuilder();

			Rpt.Judul(x, comp, judul);

			Rpt.SubJudul(x, "Kelompok Hari Keterlambatan: " + hari.SelectedItem.Text);
            Rpt.SubJudul(x, "Sales: " + agent.SelectedItem.Text);
			Rpt.SubJudul(x, "Lokasi: " + lokasi.SelectedValue);

			Rpt.Header(rpt, x);
		}

		protected void Fill()
		{
			string addSql = "", x = "";

			if(lokasi.SelectedIndex != 0)
				addSql += " AND Lokasi = '" + lokasi.SelectedValue + "'";

			if(agent.SelectedIndex != 0)
				addSql += " AND NoAgent = " + agent.SelectedValue;

			if(hari.SelectedValue == "3")
			{
				x = " WHERE DATEDIFF(DAY, TglJT, '" + Convert.ToDateTime(tgl.Text) + "') > 3"
					+ " AND DATEDIFF(DAY, TglJT, '" + Convert.ToDateTime(tgl.Text) + "') < 7"
					;
			}
			else if(hari.SelectedValue == "7")
			{
				x = " WHERE DATEDIFF(DAY, TglJT, '" + Convert.ToDateTime(tgl.Text) + "') > 7"
					+ " AND DATEDIFF(DAY, TglJT, '" + Convert.ToDateTime(tgl.Text) + "') < 15"
					;
			}
			else if(hari.SelectedValue == "15")
			{
				x = " WHERE DATEDIFF(DAY, TglJT, '" + Convert.ToDateTime(tgl.Text) + "') > 15"
					+ " AND DATEDIFF(DAY, TglJT, '" + Convert.ToDateTime(tgl.Text) + "') < 30"
					;
			}
			else if(hari.SelectedValue == "30")
			{
				x = " WHERE DATEDIFF(DAY, TglJT, '" + Convert.ToDateTime(tgl.Text) + "') > 30"
					+ " AND DATEDIFF(DAY, TglJT, '" + Convert.ToDateTime(tgl.Text) + "') < 60"
					;
			}
			else if(hari.SelectedValue == "60")
			{
				x = " WHERE DATEDIFF(DAY, TglJT, '" + Convert.ToDateTime(tgl.Text) + "') > 60"
					+ " AND DATEDIFF(DAY, TglJT, '" + Convert.ToDateTime(tgl.Text) + "') < 90"
					;
			}
			else if(hari.SelectedValue == "90")
				x = " WHERE DATEDIFF(DAY, TglJT, '" + Convert.ToDateTime(tgl.Text) + "') > 90";

			string strSql = "SELECT *"
				+ " FROM MS_KONTRAK"
				+ " WHERE NoKontrak IN ("
					+ " SELECT NoKontrak FROM MS_TAGIHAN"
					+ x
				+ ")"
				+ addSql
				;
			DataTable rs = Db.Rs(strSql);

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
				c.Text = rs.Rows[i]["NoKontrak"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Day(rs.Rows[i]["TglKontrak"]);
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Db.SingleString("SELECT Nama FROM MS_CUSTOMER WHERE NoCustomer = " + rs.Rows[i]["NoCustomer"]);
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Db.SingleString("SELECT Nama FROM MS_AGENT WHERE NoAgent = '" + rs.Rows[i]["NoKontrak"] + "'");
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Db.SingleString("SELECT Principal FROM MS_AGENT WHERE NoAgent = '" + rs.Rows[i]["NoKontrak"] + "'");
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["NoUnit"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Num(rs.Rows[i]["NilaiKontrak"]);
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				rpt.Rows.Add(r);
			}
		}

		protected bool valid()
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

			if(!x && s!="")
			{
				RegisterStartupScript("err"
					,"<script language='javascript'>document.getElementById('" + s + "').select()</script>");
			}

			return x;
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
			{
				Report();
			}
		}

		protected void xls_Click(object sender, System.EventArgs e)
		{
			if(valid())
			{
				Report();
				Rpt.ToExcel(this, rpt);
			}
		}
	}
}

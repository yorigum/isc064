using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL.Laporan
{
	public partial class LaporanTransaksiBulanan : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();

			if(!Page.IsPostBack)
			{
				Js.Focus(this, scr);
				if(!Act.Sec("DownloadExcel")) xls.Enabled = false;
			}
		}

		private bool valid()
		{
			string s = "";
			bool x = true;

			if(Cf.isEmpty(dari))
			{
				x = false;
				daric.Text = "Kosong";
			}
			else
			{
				daric.Text = "";
			}

			if(Cf.isEmpty(sampai))
			{
				x = false;
				sampaic.Text = "Kosong";
			}
			else
			{
				sampaic.Text = "";
			}
			
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

			Fill();
		}

		private void Fill()
		{
			string Dari = dari.Text;
			string Sampai = sampai.Text;
			
			if(Dari.CompareTo(Sampai) == 1)
			{
				string swap = Dari;
				Dari = Sampai;
				Sampai = swap;
			}

			string strSql = "SELECT "
				+ " MS_AGENT.NoAgent"
				+ ", MS_AGENT.Nama"
				+ " FROM MS_AGENT"
				+ " WHERE NoAgent BETWEEN " + Dari + " AND " + Sampai
				+ " AND "
				+ " (SELECT COUNT(*) FROM MS_KONTRAK WHERE NoAgent = MS_AGENT.NoAgent) > 0"
				;
			DataTable rs = Db.Rs(strSql);

			for(int i = 0; i < rs.Rows.Count; i++)
			{
				if(!Response.IsClientConnected)
					break;

				Label l;

				//HEADER
				l = new Label();
				l.Text = "<table><tr><td colspan='3'><h1>LAPORAN TRANSAKSI KOMISI BULANAN<br />" + Mi.Pt + "<br />PETRA TOWN SQUARE</h1></td></tr></table>";
				rpt.Controls.Add(l);
				
				l = new Label();
				l.Text = "<table>";
				rpt.Controls.Add(l);

				//NO. AGENT
				l = new Label();
                l.Text = "<tr><td>No. Sales</td><td>:</td><td>" + rs.Rows[i]["NoAgent"].ToString() + "</td></tr>";
				rpt.Controls.Add(l);
				
				//NAMA
				l = new Label();
				l.Text = "<tr><td>Nama</td><td>:</td><td>" + rs.Rows[i]["Nama"].ToString() + "</td></tr>";
				rpt.Controls.Add(l);

				l = new Label();
				l.Text = "</table><br />";
				rpt.Controls.Add(l);

				FillKomisi(Cf.Pk(rs.Rows[i]["NoAgent"]));
			}
		}

		private void FillKomisi(string NoAgent)
		{
			string strSql = "SELECT "
				+ " MS_KONTRAK.NoKontrak"
				+ ", MS_KONTRAK.Status"
				+ ", MS_KONTRAK.TglKontrak"
				+ ", MS_CUSTOMER.Nama AS Cs"
				+ ", MS_KONTRAK.NoUnit"
				+ ", MS_KONTRAK.SkemaKomisi"
				+ ", MS_KONTRAK.NilaiKontrak"
				+ ", (SELECT ISNULL(SUM(NilaiKomisi), 0) FROM MS_KOMISI WHERE NoKontrak = MS_KONTRAK.NoKontrak) AS TotalKomisi"
				+ " FROM MS_KONTRAK"
				+ " INNER JOIN MS_CUSTOMER ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
				+ " WHERE NoAgent = " + NoAgent
				;
			DataTable rs = Db.Rs(strSql);

			Label l = new Label();
			System.Text.StringBuilder x = new System.Text.StringBuilder();

			x.Append("<table border='0' class='tb' cellspacing='3'><tr>");
			x.Append("<th style='background-color: gray; color: white;'>No. Kontrak</th>");
			x.Append("<th style='background-color: gray; color: white;'>Status</th>");
			x.Append("<th style='background-color: gray; color: white;'>Tgl. Kontrak</th>");
			x.Append("<th style='background-color: gray; color: white;'>Customer</th>");
			x.Append("<th style='background-color: gray; color: white;'>Unit</th>");
			x.Append("<th style='background-color: gray; color: white;'>Skema Komisi</th>");
			x.Append("<th style='background-color: gray; color: white;'>Nilai Kontrak</th>");
			x.Append("<th style='background-color: gray; color: white;'>Total Komisi</th>");
			
			decimal t1 = 0, t2 = 0;

			for(int i = 0; i < rs.Rows.Count; i++)
			{
				if(!Response.IsClientConnected)
					break;

				x.Append("<tr>");
				x.Append("<td>" + rs.Rows[i]["NoKontrak"] + "</td>");
				x.Append("<td>" + rs.Rows[i]["Status"].ToString() + "</td>");
				x.Append("<td>" + Cf.Day(rs.Rows[i]["TglKontrak"]) + "</td>");
				x.Append("<td>" + rs.Rows[i]["Cs"].ToString() + "</td>");
				x.Append("<td>" + rs.Rows[i]["NoUnit"].ToString() + "</td>");
				x.Append("<td>" + rs.Rows[i]["SkemaKomisi"].ToString() + "</td>");
				x.Append("<td align='right'>" + Cf.Num(rs.Rows[i]["NilaiKontrak"]) + "</td>");
				x.Append("<td align='right'>" + Cf.Num(rs.Rows[i]["TotalKomisi"]) + "</td>");

				x.Append("</tr>");

				t1 = t1 + Convert.ToDecimal(rs.Rows[i]["NilaiKontrak"]);
				t2 = t2 + Convert.ToDecimal(rs.Rows[i]["TotalKomisi"]);

				if(i == (rs.Rows.Count - 1))
				{
					x.Append("<tr>");
					x.Append("<td colspan='6'>GRAND TOTAL</td>");
					x.Append("<td align='right' style='border-bottom: double black; font-weight: bold;'>" + Cf.Num(t1) + "</td>");
					x.Append("<td align='right' style='border-bottom: double black; font-weight: bold;'>" + Cf.Num(t2) + "</td>");
					x.Append("</tr>");
				}
			}

			x.Append("</tr>");

            x.Append("<tr><td colspan='7'></td><td align='center'><br /><br />Sales,<br /><br /><br /><br />(" + Db.SingleString("SELECT Nama FROM MS_AGENT WHERE NoAgent = " + NoAgent) + ")</td></tr>");

			x.Append("</table>");

			l = new Label();
			l.Text = x.ToString() + "<div style='page-break-after: always;'></div>";

			rpt.Controls.Add(l);
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

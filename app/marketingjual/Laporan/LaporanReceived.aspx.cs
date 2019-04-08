using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL.Laporan
{
	public partial class LaporanReceived : System.Web.UI.Page
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

			if(statusA.Checked)
				Rpt.SubJudul(x, "Status : " + statusA.Text);
			else if(statusB.Checked)
				Rpt.SubJudul(x, "Status : " + statusB.Text);
			else
				Rpt.SubJudul(x, "Status : " + statusS.Text);
			
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
			string Status = "";
			if(statusA.Checked) Status = " AND a.Status = 'A'";
			if(statusB.Checked) Status = " AND a.Status = 'B'";

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

			decimal t1 = 0, t2 = 0;

			string strSql = "SELECT a.*"
				+ ", b.Nama AS Cs"
				+ ", b.NoKTP"
				+ ", b.NoTelp"
				+ ", b.NoHP"
				+ ", c.Nama AS Ag"
				+ ", c.Principal"
				+ ", (SELECT ISNULL(SUM(NilaiPelunasan), 0) FROM MS_PELUNASAN WHERE NoKontrak = a.NoKontrak) AS TotalBFR"
				+ " FROM MS_KONTRAK a"
				+ " INNER JOIN MS_CUSTOMER b ON a.NoCustomer = b.NoCustomer"
				+ " INNER JOIN MS_AGENT c ON a.NoAgent = c.NoAgent"
				+ " WHERE a.NoKontrak NOT IN"
				+ "					("
				+ "						SELECT NoKontrak"
				+ "						FROM"
				+ "						("
				+ "							SELECT NoKontrak, SUM(NilaiPelunasan) AS Total"
				+ "							FROM MS_PELUNASAN"
				+ "							GROUP BY NoKontrak"
				+ "						)"
				+ "						AS table1 WHERE Total >= 10000000"
				+ "					)"
				+ " AND CONVERT(varchar, " + tgl + " ,112) >= '" + Cf.Tgl112(Dari) + "'"
				+ " AND CONVERT(varchar, " + tgl + ", 112) <= '" + Cf.Tgl112(Sampai) + "'"
				+ " AND Jenis IN (" + Rpt.inSql(jenis) + ")"
				+ Lokasi
				+ Status
				+ Agent
				+ " ORDER BY " + order
				;
			
			DataTable rs = Db.Rs(strSql);

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
				c.Text = Cf.IndoWeek(Convert.ToDateTime(rs.Rows[i]["TglKontrak"])) + "<br />" + Cf.Day(rs.Rows[i]["TglKontrak"]);
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Str(rs.Rows[i]["Cs"])
					+ "<br />"
					+ "KTP: " + rs.Rows[i]["NoKTP"].ToString()
					+ "<br />"
					+ rs.Rows[i]["NoTelp"].ToString() 
					+ "<br />" 
					+ rs.Rows[i]["NoHP"].ToString()
					;
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Str.Lantai(Cf.Pk(rs.Rows[i]["NoUnit"]));
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Str.Blok(Cf.Pk(rs.Rows[i]["NoUnit"]));
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
				c.Text = Cf.Str(rs.Rows[i]["Skema"]);
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = NoTTS(Cf.Pk(rs.Rows[i]["NoKontrak"]));
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = CaraBayar(Cf.Pk(rs.Rows[i]["NoKontrak"]));
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Num(rs.Rows[i]["TotalBFR"]);
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Str(rs.Rows[i]["Ag"]);
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				rpt.Rows.Add(r);

				t1 = t1 + (decimal) rs.Rows[i]["NilaiKontrak"];
				t2 = t2 + (decimal) rs.Rows[i]["TotalBFR"];

				if(i == (rs.Rows.Count - 1))
				{
					SubTotal(t1, t2);
				}
			}
		}

		private string NoTTS(string NoKontrak)
		{
			string strSql = "SELECT TOP 1 ManualTTS"
				+ " FROM MS_PELUNASAN"
				+ " WHERE NoKontrak = '" + NoKontrak + "'"
				+ " ORDER BY TglPelunasan DESC"
				;
			DataTable rs = Db.Rs(strSql);

			if(rs.Rows.Count > 0)
			{
				return rs.Rows[0]["ManualTTS"].ToString();
			}
			else
			{
				return "";
			}
		}

		private string CaraBayar(string NoKontrak)
		{
			string strSql = "SELECT TOP 1 CaraBayar, Ket"
				+ " FROM MS_PELUNASAN"
				+ " WHERE NoKontrak = '" + NoKontrak + "'"
				+ " ORDER BY TglPelunasan DESC"
				;
			DataTable rs = Db.Rs(strSql);

			if(rs.Rows.Count > 0)
			{
				string x = rs.Rows[0]["CaraBayar"].ToString() + "<br />" + rs.Rows[0]["Ket"].ToString();
				return x;
			}
			else
			{
				return "";
			}
		}

		private void SubTotal(decimal t1, decimal t2)
		{
			TableRow r = new TableRow();
			TableCell c;

			c = Rpt.Foot();
			c.Text = "";
			c.ColumnSpan = 7;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(t1);
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = "";
			c.ColumnSpan = 3;
			r.Cells.Add(c);
			
			c = Rpt.Foot();
			c.Text = Cf.Num(t2);
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = "";
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

using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL.Laporan
{
	public partial class LaporanPenjualan : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.RadioButton statusS;
		protected System.Web.UI.WebControls.RadioButton statusA;
		protected System.Web.UI.WebControls.RadioButton statusI;
		protected System.Web.UI.WebControls.CheckBox namaCheck;
		protected System.Web.UI.WebControls.Label namac;
		protected System.Web.UI.WebControls.CheckBoxList nama;
		protected System.Web.UI.WebControls.ListBox input;
		protected System.Web.UI.WebControls.ListBox lahir;
		protected System.Web.UI.WebControls.CheckBox agamaCheck;
		protected System.Web.UI.WebControls.Label agamac;
		protected System.Web.UI.WebControls.CheckBoxList agama;
	
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
		
		private void Report()
		{
			param.Visible = false;
			report.Visible = true;

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
			
			string strSql = "SELECT a.*, b.*, c.Nama AS NamaAgent"
				+ " FROM MS_KONTRAK a"
				+ " INNER JOIN MS_CUSTOMER b ON a.NoCustomer = b.NoCustomer"
				+ " INNER JOIN MS_AGENT c ON a.NoAgent = c.NoAgent"
				+ " WHERE NoKontrak BETWEEN '" + Dari + "' AND '" + Sampai + "'"
				;
			DataTable rs = Db.Rs(strSql);
			
			for(int i = 0; i < rs.Rows.Count; i++)
			{
				if(!Response.IsClientConnected)
				{
					break;
				}

				Label l;

				l = new Label();
				l.Text = "<table>";
				report.Controls.Add(l);

				//HEADER
				l = new Label();
				l.Text = "<tr><td colspan='8'><h1>KARTU PIUTANG</h1><h2>" + Mi.Pt + "</h2><h2>PETRA SQUARE</h2></td></tr>";
				report.Controls.Add(l);

				//TYPE
				l = new Label();
				l.Text = "<tr><td>Jenis</td><td>:</td><td>" + rs.Rows[i]["Jenis"].ToString() + "</td>";
				report.Controls.Add(l);
				
				//HARGA JUAL
				l = new Label();
				l.Text = "<td>Price List</td><td>:</td><td align='right'>" + Cf.Num(rs.Rows[i]["NilaiKontrak"]) + "</td></tr>";
				report.Controls.Add(l);

				//UNIT
				l = new Label();
				l.Text = "<tr><td>No. Unit</td><td>:</td><td>" + Cf.Pk(rs.Rows[i]["NoUnit"]) + "</td>";
				report.Controls.Add(l);

				//DP
				l = new Label();
				decimal DP = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiTagihan), 0)"
					+ " FROM MS_TAGIHAN"
					+ " WHERE NoKontrak = '" + Cf.Pk(rs.Rows[i]["NoKontrak"]) + "'"
					+ " AND Tipe IN ('BF', 'DP')"
					);
				l.Text = "<td>DP</td><td>:</td><td align='right'>" + Cf.Num(DP) + "</td>";
				report.Controls.Add(l);

				//NAMA
				l = new Label();
				string Nama = Db.SingleString("SELECT Nama FROM MS_CUSTOMER "
					+ " WHERE NoCustomer = '" + Cf.Pk(rs.Rows[i]["NoCustomer"]) + "'");
				l.Text = "<tr><td>Customer</td><td>:</td><td>" + Cf.Str(Nama) + "</td>";
				report.Controls.Add(l);

				//ANGSURAN
				l = new Label();
				decimal Angsuran = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiTagihan), 0)"
					+ " FROM MS_TAGIHAN"
					+ " WHERE NoKontrak = '" + Cf.Pk(rs.Rows[i]["NoKontrak"]) + "'"
					+ " AND Tipe = 'ANG'"
					);
				l.Text = "<td>Angsuran</td><td>:</td><td align='right'>" + Cf.Num(Angsuran) + "</td></tr>";
				report.Controls.Add(l);
			

				//ALAMAT
				l = new Label();
				l.Text = "<tr><td valign='top'>Alamat</td><td valign='top'>:</td><td>" + Cf.Str(rs.Rows[i]["KTP1"]) + "<br />" + Cf.Str(rs.Rows[i]["KTP2"]) + "<br />" + Cf.Str(rs.Rows[i]["KTP3"]) + "</td>";
				report.Controls.Add(l);

				//BIAYA ADM
				l = new Label();
				decimal Adm = Db.SingleDecimal("SELECT ISNULL(NilaiTagihan, 0)"
					+ " FROM MS_TAGIHAN"
					+ " WHERE NoKontrak = '" + Cf.Pk(rs.Rows[i]["NoKontrak"]) + "'"
					+ " AND Tipe = 'ADM'"
					);
				l.Text = "<td>Biaya Adm.</td><td style='font-weight: bold;'>:</td><td style='font-weight: bold;' align='right'>" + Cf.Num(Adm) + "</td></tr>";
				report.Controls.Add(l);

				//MARKETING
				l = new Label();
                l.Text = "<tr><td>Sales</td><td>:</td><td>" + Cf.Str(rs.Rows[i]["NamaAgent"]) + "</td>";
				report.Controls.Add(l);

				//STATUS
				l = new Label();
				string ket = "";
				switch(Cf.Str(rs.Rows[i]["Status"]))
				{
					case "A":
						ket = "AKTIF";
						break;
					case "B":
						ket = "BATAL";
						break;
					case "E":
						ket = "EXPIRE";
						break;
				}
				l.Text = "<td>Status</td><td>:</td><td>" + ket + "</td></tr>";
				report.Controls.Add(l);

				//KOMISI
				l = new Label();
				l.Text = "<tr><td>Skema Komisi</td><td>:</td><td>" + Cf.Str(rs.Rows[i]["SkemaKomisi"]) + "</td></tr>";
				report.Controls.Add(l);

				l = new Label();
				l.Text = "</table><br />";
				report.Controls.Add(l);

				FillTagihan(Cf.Pk(rs.Rows[i]["NoKontrak"]));
			}

		}

		private void FillTagihan(string NoKontrak)
		{
			string strSql = "SELECT *"
				+ " FROM MS_TAGIHAN"
				+ " WHERE NoKontrak = '" + Cf.Pk(NoKontrak) + "' AND NamaTagihan NOT LIKE '%BATAL%'"
				;
			
			DataTable rs = Db.Rs(strSql);

			decimal Total = Db.SingleDecimal("SELECT NilaiKontrak FROM MS_KONTRAK WHERE NoKontrak = '" + Cf.Pk(NoKontrak) + "'");

			Label l = new Label();
			System.Text.StringBuilder x = new System.Text.StringBuilder();

			x.Append("<table border='0' class='tb' cellspacing='3'><tr>");
			x.Append("<th rowspan='2' style='background-color: gray; color: white;'>NO.</th>");
			x.Append("<th colspan='2' style='background-color: gray; color: white;'>TANGGAL</th>");
			x.Append("<th rowspan='2' style='background-color: gray; color: white;'>NO. KUITANSI</th>");
			x.Append("<th rowspan='2' style='background-color: gray; color: white;'>KETERANGAN</th>");
			x.Append("<th colspan='3' style='background-color: gray; color: white;'>PEMBAYARAN</th>");
			x.Append("<th rowspan='2' style='background-color: gray; color: white;'>CARA BAYAR</th>");
			
			x.Append("<tr><th style='background-color: gray; color: white;'>J. TEMPO</th><th style='background-color: gray; color: white;'>BAYAR</th><th style='background-color: gray; color: white;'>JADWAL</th><th style='background-color: gray; color: white;'>REAL</th><th style='background-color: gray; color: white;'>SISA</th></tr>");

			for(int i = 0; i < rs.Rows.Count; i++)
			{
				if(!Response.IsClientConnected)
					break;

				decimal Sisa = Convert.ToDecimal(rs.Rows[i]["NilaiTagihan"]);

				x.Append("<tr>");
				x.Append("<td align='center'>" + (i + 1) + "</td>");
				x.Append("<td valign='top'>" + Cf.Day(rs.Rows[i]["TglJT"]) + "</td>");
				
				decimal a = Db.SingleDecimal("SELECT NilaiPelunasan FROM MS_PELUNASAN WHERE NoTagihan = '" + rs.Rows[i]["NoUrut"]+ "' AND NoKontrak = '" + rs.Rows[i]["NoKontrak"]+ "' ORDER BY TglPelunasan DESC");
				decimal b = Db.SingleDecimal("SELECT ISNULL (SUM(NilaiPelunasan),0) FROM MS_PELUNASAN WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"]+ "' ");
				decimal sisa = 0;
				sisa = Total - b;
				if (a == 0)
					x.Append("<td>&nbsp;</td>");
				else
					x.Append("<td>" + TglPelunasan(NoKontrak, rs.Rows[i]["NoUrut"].ToString()) + "</td>");

				x.Append("<td>" + Cf.Str(NoKuitansi(NoKontrak, rs.Rows[i]["NoUrut"].ToString())) + "</td>");
				x.Append("<td valign='top'>" + Cf.Str(rs.Rows[i]["NamaTagihan"]) + "</td>");
				
				if(i == (rs.Rows.Count - 1))
					x.Append("<td align='right'>" + Cf.Num(rs.Rows[i]["NilaiTagihan"]) + "</td>");
				else
					x.Append("<td align='right' valign='top' >" + Cf.Num(rs.Rows[i]["NilaiTagihan"]) + "</td>");
				
				x.Append("<td align='right'>" + NilaiPelunasan(NoKontrak, rs.Rows[i]["NoUrut"].ToString()) + "</td>");
				x.Append("<td align='right'>" + SisaPelunasan(NoKontrak, rs.Rows[i]["NoUrut"].ToString(), Sisa)  + "</td>");
				x.Append("<td>" + Cf.Str(CaraBayar(NoKontrak, rs.Rows[i]["NoUrut"].ToString())) + "</td>");
				
				x.Append("</tr>");

				if(i == (rs.Rows.Count - 1))
				{
					x.Append("<tr>");
					x.Append("<td colspan='4'>&nbsp;</td>");
					x.Append("<td>TOTAL</td>");
					x.Append("<td align='right' style='border-top: 1px double Black; border-bottom: double black; font-weight: bold;'>" + Cf.Num(Total) + "</td>");
					x.Append("<td align='right' style='border-top: 1px double Black; border-bottom: double black; font-weight: bold;'>" + Cf.Num(b) + "</td>");
					x.Append("<td align='right' style='border-top: 1px double Black; border-bottom: double black; font-weight: bold;'>" + Cf.Num(sisa) + "</td>");
					x.Append("<td colspan='8'>&nbsp;</td>");
					x.Append("</tr>");
				}
			}

			x.Append("</tr></table>");

			l = new Label();
			l.Text = x.ToString() + "<div style='page-break-after: always;'></div>";

			report.Controls.Add(l);
		}

		private string TglPelunasan(string NoKontrak, string NoTagihan)
		{
			System.Text.StringBuilder x = new System.Text.StringBuilder();
			string strSql = "SELECT TglPelunasan"
				+ " FROM MS_PELUNASAN"
				+ " WHERE NoKontrak = '" + Cf.Pk(NoKontrak) + "'"
				+ " AND NoTagihan = " + NoTagihan
				+ " ORDER BY TglPelunasan DESC"
				;
	
			DataTable rs = Db.Rs(strSql);

			if(rs.Rows.Count > 1)
			{   				
				for(int i = 0; i < rs.Rows.Count; i++)
				{
					x.Append(Cf.Day(rs.Rows[i]["TglPelunasan"]) + "<br/>");
				}
				return x.ToString();
			}
			else
			if(rs.Rows.Count == 1)
			{
				return Cf.Day(rs.Rows[0]["TglPelunasan"]);
			}
			else
			{
				return Cf.Day("1-1-1900");
			}
		}

		private string NilaiPelunasan(string NoKontrak, string NoTagihan)
		{
			System.Text.StringBuilder x = new System.Text.StringBuilder();
			string strSql = "SELECT ISNULL(NilaiPelunasan, 0) AS NilaiPelunasan"
				+ " FROM MS_PELUNASAN"
				+ " WHERE NoKontrak = '" + Cf.Pk(NoKontrak) + "'"
				+ " AND NoTagihan = " + NoTagihan
				+ " ORDER BY TglPelunasan DESC"
				;

			DataTable rs = Db.Rs(strSql);

			if(rs.Rows.Count > 1)
			{   				
				for(int i = 0; i < rs.Rows.Count; i++)
				{
					x.Append(Cf.Num(Convert.ToDecimal(rs.Rows[i]["NilaiPelunasan"])) + "<br/>");
				}
				return x.ToString();
			}
			else if(rs.Rows.Count == 1)
			{
				return Cf.Num(Convert.ToDecimal(rs.Rows[0]["NilaiPelunasan"]));
			}		
			else
			{
				return Cf.Num((decimal) 0.00);
			}
		}

		private string SisaPelunasan(string NoKontrak, string NoTagihan, decimal Sisa)
		{
			System.Text.StringBuilder x = new System.Text.StringBuilder();
			string strSql = "SELECT ISNULL(NilaiPelunasan, 0) AS NilaiPelunasan"
				+ " FROM MS_PELUNASAN"
				+ " WHERE NoKontrak = '" + Cf.Pk(NoKontrak) + "'"
				+ " AND NoTagihan = " + NoTagihan
				;

			DataTable rs = Db.Rs(strSql);

			if(rs.Rows.Count > 1)
			{
				for(int i = 0; i < rs.Rows.Count; i++)
				{
					Sisa = Sisa - Convert.ToDecimal(rs.Rows[i]["NilaiPelunasan"]);
					x.Append(Cf.Num(Sisa) + "<br />");
				}
				return x.ToString();
			}
			else if(rs.Rows.Count == 1)
			{
				Sisa = Sisa - Convert.ToDecimal(rs.Rows[0]["NilaiPelunasan"]);
				return Cf.Num(Sisa);
			}		
			else
			{
				return Cf.Num(Sisa);
			}

		}
		private string CaraBayar(string NoKontrak, string NoTagihan)
		{
			string SudahCair = "";
			string strSql = "SELECT CaraBayar, Ket, SudahCair"
				+ " FROM MS_PELUNASAN"
				+ " WHERE NoKontrak = '" + Cf.Pk(NoKontrak) + "'"
				+ " AND NoTagihan = " + NoTagihan
				;
			DataTable rs = Db.Rs(strSql);

			if(rs.Rows.Count > 1)
			{
				System.Text.StringBuilder x = new System.Text.StringBuilder();
				
				for(int i = 0; i < rs.Rows.Count; i++)
				{
					if(rs.Rows[i]["SudahCair"].ToString().ToUpper() == "TRUE")
					{
						SudahCair = "CAIR";
					}
					else
					{
						SudahCair = "BELUM CAIR";
					}

					x.Append(Cf.Str(rs.Rows[i]["CaraBayar"]) + " " + Cf.Str(rs.Rows[i]["Ket"]) + " (" + SudahCair + ")<br />");
				}

				return x.ToString();
			}
			else if(rs.Rows.Count == 1)
			{
				if(rs.Rows[0]["SudahCair"].ToString().ToUpper() == "TRUE")
				{
					SudahCair = "CAIR";
				}
				else
				{
					SudahCair = "BELUM CAIR";
				}
				
				return Cf.Str(rs.Rows[0]["CaraBayar"]) + " " + Cf.Str(rs.Rows[0]["Ket"]) + " (" + SudahCair + ")";
			}
			else
			{
				return "";
			}
		}
		
		private string NoKuitansi(string NoKontrak, string NoTagihan)
		{
			System.Text.StringBuilder x = new System.Text.StringBuilder();
			string strSql = "SELECT ManualTTS FROM MS_PELUNASAN " 
				+ " WHERE NoKontrak = '" + Cf.Pk(NoKontrak) + "'"
				+ " AND NoTagihan = " + NoTagihan
				+ " ORDER BY TglPelunasan DESC"
				;

			DataTable rs = Db.Rs(strSql);
			if(rs.Rows.Count > 1)
			{   
				
				for(int i = 0; i < rs.Rows.Count; i++)
				{
					x.Append(Cf.Str(rs.Rows[i]["ManualTTS"].ToString())+ "<br/>");
				}
				return x.ToString();
			}
			else if(rs.Rows.Count == 1)
			{
				return Cf.Str(rs.Rows[0]["ManualTTS"].ToString());
			}		
			else
			{
				return "";
			}
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
				Rpt.ToExcel(this, report);
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

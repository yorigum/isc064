using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL.Laporan
{
	
	public partial class DataCustomerSiapPPJB : System.Web.UI.Page
	{
		
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

		protected void Bind()
		{
			string strSql = "SELECT Nomor, Nama FROM REF_SKEMA WHERE Status = 'A'";
			DataTable rs = Db.Rs(strSql);

			for(int i = 0; i < rs.Rows.Count; i++)
			{
				skemalist.Items.Add(new ListItem(rs.Rows[i]["Nama"].ToString() + " (" + Cf.Pk(rs.Rows[i]["Nomor"]).PadLeft(3, '0') + ")", rs.Rows[i]["Nama"].ToString()));
				skemalist.Items[i].Selected = true;
			}
		}

		protected void Report()
		{
			param.Visible = false;
			rpt.Visible = true;

			header.Text = Mi.Pt
				+ "<br />"
				+ "LAPORAN DATA CUSTOMER SIAP PPJB"
				;

			System.Text.StringBuilder x = new System.Text.StringBuilder();

			x.Append("Urut Customer: " + urut.SelectedItem.Text + "<br />");
			x.Append("Skema Bayar: " + Rpt.inSql(skemalist).Replace("'", ""));
			x.Append("<br /><span style='font-weight: normal;'>Laporan dihasilkan pada hari : " + Cf.IndoWeek(DateTime.Today)
				+ ", " + Cf.Date(DateTime.Now)
				+ " dari workstation : " + Act.IP
				+ " dan username : " + Act.UserID
				+ "</span>"
				);

			subheader.Text = x.ToString();
			Fill();
		}

		protected void Fill()
		{
			string addSql = "";

			System.Text.StringBuilder x = new System.Text.StringBuilder();
			bool isFirst = true;
			for(int i = 0; i < skemalist.Items.Count; i++)
			{
				if(skemalist.Items[i].Selected)
				{
					if(isFirst)
					{
						x.Append("'" + Cf.Str(skemalist.Items[i].Text) + "'");
						isFirst = false;
					}
					else
						x.Append(",'" + Cf.Str(skemalist.Items[i].Text) + "'");
				}
			}

			if(x.ToString() != "")
				addSql += " AND a.Skema IN (" + x.ToString() + ")";

			addSql += " ORDER BY a.NoCustomer " + urut.SelectedValue;
			
			string strSql = "SELECT a.*, b.Nama"
				+ ", (SELECT ISNULL(SUM(NilaiPelunasan), 0) FROM MS_PELUNASAN WHERE NoKontrak = a.NoKontrak) AS SudahBayar"
				+ " FROM MS_KONTRAK a"
				+ " INNER JOIN MS_CUSTOMER b ON a.NoCustomer = b.NoCustomer"
				+ " WHERE a.Status = 'A'"
				+ " AND (SELECT ISNULL(SUM(NilaiPelunasan), 0) FROM MS_PELUNASAN WHERE NoKontrak = a.NoKontrak) >= (0.2 * a.NilaiKontrak)"
				+ addSql
				;
			DataTable rs = Db.Rs(strSql);

			for(int i = 0; i < rs.Rows.Count; i++)
			{
				if(!Response.IsClientConnected)
					break;

				TableRow r = new TableRow();
				r.Attributes["ondblclick"] = "popJadwalTagihan('" + rs.Rows[i]["NoKontrak"] + "')";
				TableCell c;

				c = new TableCell();
				c.Text = (i + 1).ToString();
				c.HorizontalAlign = HorizontalAlign.Center;
				c.VerticalAlign = VerticalAlign.Top;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Nama"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				c.VerticalAlign = VerticalAlign.Top;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["NoUnit"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				c.VerticalAlign = VerticalAlign.Top;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Jenis"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				c.VerticalAlign = VerticalAlign.Top;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Num(rs.Rows[i]["NilaiKontrak"]);
				c.HorizontalAlign = HorizontalAlign.Right;
				c.VerticalAlign = VerticalAlign.Top;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Num(rs.Rows[i]["SudahBayar"]);
				c.HorizontalAlign = HorizontalAlign.Right;
				c.VerticalAlign = VerticalAlign.Top;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = YAD(rs.Rows[i]["NoKontrak"].ToString(), "TglJT");
				c.HorizontalAlign = HorizontalAlign.Left;
				c.VerticalAlign = VerticalAlign.Top;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = YAD(rs.Rows[i]["NoKontrak"].ToString(), "NilaiTagihan");
				c.HorizontalAlign = HorizontalAlign.Right;
				c.VerticalAlign = VerticalAlign.Top;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = YAD(rs.Rows[i]["NoKontrak"].ToString(), "Denda");
				c.HorizontalAlign = HorizontalAlign.Right;
				c.VerticalAlign = VerticalAlign.Top;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Skema(rs.Rows[i]["Skema"].ToString(),rs.Rows[i]["JenisPPN"].ToString(), "CASH");
				c.HorizontalAlign = HorizontalAlign.Center;
				c.VerticalAlign = VerticalAlign.Top;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Skema(rs.Rows[i]["Skema"].ToString(),rs.Rows[i]["JenisPPN"].ToString(), "CREDIT");
				c.HorizontalAlign = HorizontalAlign.Center;
				c.VerticalAlign = VerticalAlign.Top;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Skema(rs.Rows[i]["Skema"].ToString(),rs.Rows[i]["JenisPPN"].ToString(), "KPANONS");
				c.HorizontalAlign = HorizontalAlign.Center;
				c.VerticalAlign = VerticalAlign.Top;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Skema(rs.Rows[i]["Skema"].ToString(),rs.Rows[i]["JenisPPN"].ToString(), "KPAS");
				c.HorizontalAlign = HorizontalAlign.Center;
				c.VerticalAlign = VerticalAlign.Top;
				r.Cells.Add(c);

				rpt.Rows.Add(r);
			}
		}

		protected string YAD(string NoKontrak, string x)
		{
			System.Text.StringBuilder z = new System.Text.StringBuilder();
			
			string strSql = "SELECT " + x
				+ " FROM MS_TAGIHAN"
				+ " WHERE NoKontrak = '" + NoKontrak + "'"
				+ " AND (NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan), 0) FROM MS_PELUNASAN"
				+ " WHERE NoKontrak = MS_TAGIHAN.NoKontrak AND NoTagihan = MS_TAGIHAN.NoUrut) > 0)"
				;
			DataTable rs = Db.Rs(strSql);

			for(int i = 0; i < rs.Rows.Count; i++)
			{
				if(!Response.IsClientConnected)
					break;

				if(x == "TglJT")
					z.Append(Cf.Day(rs.Rows[i][x]) + "<br />");
				if(x == "NilaiTagihan" || x == "Denda")
					z.Append(Cf.Num(rs.Rows[i][x]) + "<br />");
			}

			return z.ToString();
		}

		protected string Skema(string x, string y, string z)
		{
			int StatusBayar = 0;

			if(x.StartsWith("CASH"))
				StatusBayar = 1;
			else if(x.StartsWith("KREDIT"))
				StatusBayar = 2;
			else if(x.StartsWith("KPA"))
			{
				if(y == "PEMERINTAH")
					StatusBayar = 3;
				else if(y == "KONSUMEN")
					StatusBayar = 4;
			}
			
			if(z == "CASH" && StatusBayar == 1)
				return "X";
			else if(z == "CREDIT" && StatusBayar == 2)
				return "X";
			else if(z == "KPANONS" && StatusBayar == 4)
				return "X";
			else if(z == "KPAS" && StatusBayar == 3)
				return "X";
			else
				return "";
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

		protected void skema_CheckedChanged(object sender, System.EventArgs e)
		{
			for(int i = 0; i < skemalist.Items.Count; i++)
			{
				skemalist.Items[i].Selected = skema.Checked;
			}
		}

		protected void scr_Click(object sender, System.EventArgs e)
		{
			Report();
		}

		protected void xls_Click(object sender, System.EventArgs e)
		{
			Report();
			Rpt.ToExcel(this, rpt);
		}
	}
}

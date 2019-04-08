using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL.Laporan
{
	public partial class LaporanCollection : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.PlaceHolder list;
		
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
			rs = Db.Rs("SELECT DISTINCT Lokasi FROM MS_KONTRAK ORDER BY Lokasi");
			for(int i=0;i<rs.Rows.Count;i++)
				lokasi.Items.Add(new ListItem(rs.Rows[i][0].ToString()));

			lokasi.SelectedIndex = 0;
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

			DateTime Dari = Convert.ToDateTime(dari.Text);
			DateTime Sampai = Convert.ToDateTime(sampai.Text);
			Rpt.SubJudul(x
				, "As of : " + Cf.Day(dari.Text) + " s/d " + Cf.Day(sampai.Text)
				);

			Rpt.SubJudul(x
				, "Lokasi : " + lokasi.SelectedItem.Text
				);

			Rpt.Header(rpt, x);
		}

		private void Fill()
		{
			string Status = "";
			if(statusA.Checked) Status = " AND MS_KONTRAK.Status = 'A'";
			if(statusB.Checked) Status = " AND MS_KONTRAK.Status = 'B'";
			
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

			string strSql = "SELECT MS_KONTRAK.*"
				+ ",MS_CUSTOMER.Nama AS Cs"
				+ ",MS_AGENT.Nama AS Ag"
				+ ",MS_AGENT.Principal"
				+ ",(SELECT ISNULL(SUM(NilaiPelunasan),0) FROM MS_PELUNASAN WHERE NoKontrak = MS_KONTRAK.NoKontrak) AS NilaiTTS"
				+ " FROM MS_KONTRAK"
				+ " INNER JOIN MS_CUSTOMER ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
				+ " INNER JOIN MS_AGENT ON MS_KONTRAK.NoAgent = MS_AGENT.NoAgent "
				+ " WHERE 1=1 "
				+ Lokasi
				+ Status
				;
			
			DataTable rs = Db.Rs(strSql);

			TableRow r = new TableRow();
			TableHeaderCell hc;

			hc = new TableHeaderCell();
			hc.Text = "Overdue";
			hc.HorizontalAlign = HorizontalAlign.Right;
			hc.Attributes["style"] = "background-color:gray;color:white;";
			r.Cells.Add(hc);

			hc = new TableHeaderCell();
			hc.Text = "Actual";
			hc.HorizontalAlign = HorizontalAlign.Right;
			hc.Attributes["style"] = "background-color:gray;color:white;";
			r.Cells.Add(hc);

			hc = new TableHeaderCell();
			hc.Text = "Early";
			hc.HorizontalAlign = HorizontalAlign.Right;
			hc.Attributes["style"] = "background-color:gray;color:white;";
			r.Cells.Add(hc);

			hc = new TableHeaderCell();
			hc.Text = "A";
			hc.HorizontalAlign = HorizontalAlign.Right;
			hc.Attributes["style"] = "background-color:gray;color:white;";
			r.Cells.Add(hc);

			hc = new TableHeaderCell();
			hc.Text = "B";
			hc.HorizontalAlign = HorizontalAlign.Right;
			hc.Attributes["style"] = "background-color:gray;color:white;";
			r.Cells.Add(hc);

			rpt.Rows.Add(r);

			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				DataTable rs2 = Db.Rs("SELECT *"
					+ " ,(SELECT COUNT(*) FROM MS_PELUNASAN WHERE NoKontrak = MS_TAGIHAN.NoKontrak AND NoTagihan = MS_TAGIHAN.NoUrut) AS CountLunas"
					+ " ,(SELECT COUNT(*) FROM MS_PELUNASAN WHERE NoKontrak = MS_TAGIHAN.NoKontrak AND NoTagihan = MS_TAGIHAN.NoUrut AND (CONVERT(VARCHAR, TglPelunasan, 112) >= " + Cf.Tgl112(Dari) + " AND CONVERT(VARCHAR, TglPelunasan, 112) <= " + Cf.Tgl112(Sampai) + ")) AS CountLunas2"
					+ " FROM MS_TAGIHAN"
					+ " WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "'");

				string temp = Tagihan(rs2, Cf.Pk(rs.Rows[i]["NoKontrak"]), "NAMA", Dari, Sampai);
				if(temp!="")
				{
					r = new TableRow();
					TableCell c;

					r.VerticalAlign = VerticalAlign.Top;
					r.Attributes["ondblclick"] = "popEditKontrak('"+rs.Rows[i]["NoKontrak"]+"')";

					c = new TableCell();
					c.Text = rs.Rows[i]["NoKontrak"].ToString();
					c.HorizontalAlign = HorizontalAlign.Left;
					r.Cells.Add(c);

					c = new TableCell();
					c.Text = Cf.Day(rs.Rows[i]["TglKontrak"]);
					c.HorizontalAlign = HorizontalAlign.Left;
					r.Cells.Add(c);

					c = new TableCell();
					c.Text = rs.Rows[i]["NoUnit"].ToString();
					c.HorizontalAlign = HorizontalAlign.Left;
					r.Cells.Add(c);

					c = new TableCell();
					c.Text = Cf.Str(rs.Rows[i]["Cs"]);
					c.HorizontalAlign = HorizontalAlign.Left;
					r.Cells.Add(c);

					c = new TableCell();
					c.Text = Cf.Str(rs.Rows[i]["Ag"]);
					c.HorizontalAlign = HorizontalAlign.Left;
					r.Cells.Add(c);

					c = new TableCell();
					c.Text = Cf.Num(rs.Rows[i]["NilaiKontrak"]);
					c.HorizontalAlign = HorizontalAlign.Right;
					r.Cells.Add(c);

					c = new TableCell();
					c.Text = temp;
					c.HorizontalAlign = HorizontalAlign.Left;
					r.Cells.Add(c);

					c = new TableCell();
					c.Text = Tagihan(rs2, Cf.Pk(rs.Rows[i]["NoKontrak"]), "TGL", Dari, Sampai);
					c.HorizontalAlign = HorizontalAlign.Left;
					r.Cells.Add(c);

					c = new TableCell();
					c.Text = Tagihan(rs2, Cf.Pk(rs.Rows[i]["NoKontrak"]), "OVERDUE", Dari, Sampai);
					c.HorizontalAlign = HorizontalAlign.Right;
					r.Cells.Add(c);

					c = new TableCell();
					c.Text = Tagihan(rs2, Cf.Pk(rs.Rows[i]["NoKontrak"]), "ACTUAL", Dari, Sampai);
					c.HorizontalAlign = HorizontalAlign.Right;
					r.Cells.Add(c);

					c = new TableCell();
					c.Text = Tagihan(rs2, Cf.Pk(rs.Rows[i]["NoKontrak"]), "EARLY", Dari, Sampai);
					c.HorizontalAlign = HorizontalAlign.Right;
					r.Cells.Add(c);

					c = new TableCell();
					c.Text = Tagihan(rs2, Cf.Pk(rs.Rows[i]["NoKontrak"]), "TGLLUNAS", Dari, Sampai);
					c.HorizontalAlign = HorizontalAlign.Left;
					r.Cells.Add(c);

					c = new TableCell();
					c.Text = Tagihan(rs2, Cf.Pk(rs.Rows[i]["NoKontrak"]), "A", Dari, Sampai);
					c.HorizontalAlign = HorizontalAlign.Right;
					r.Cells.Add(c);

					c = new TableCell();
					c.Text = Tagihan(rs2, Cf.Pk(rs.Rows[i]["NoKontrak"]), "B", Dari, Sampai);
					c.HorizontalAlign = HorizontalAlign.Right;
					r.Cells.Add(c);

					rpt.Rows.Add(r);
				}

				if(i == (rs.Rows.Count - 1))
				{
					SubTotal();
				}
			}
		}

		private string Tagihan(DataTable rs, string NoKontrak, string Type, DateTime Dari, DateTime Sampai)
		{
			string strSql = "";
			bool s = false, Overdue = false, Actual = false, Early = false;
			System.Text.StringBuilder x = new System.Text.StringBuilder();
			
			for(int i = 0; i < rs.Rows.Count; i++)
			{
				if(!Response.IsClientConnected) break;
				
				if((Convert.ToDateTime(rs.Rows[i]["TglJT"]) >= Dari) && (Convert.ToDateTime(rs.Rows[i]["TglJT"]) <= Sampai))	// 1
				{
					s = true;
					Overdue = false;
					Actual = true;
					Early = false;
				}
				else if(Convert.ToDateTime(rs.Rows[i]["TglJT"]) <= Dari)
				{
					if(Convert.ToInt32(rs.Rows[i]["CountLunas"]) == 0)	// 2
						s = true;
					else if(Convert.ToInt32(rs.Rows[i]["CountLunas2"]) > 0)	// 3
						s = true;
					else
						s = false;

					Overdue = true;
					Actual = false;
					Early = false;
				}
				else if(Convert.ToDateTime(rs.Rows[i]["TglJT"]) > Sampai)
				{
					if(Convert.ToInt32(rs.Rows[i]["CountLunas2"]) > 0)	// 4
						s = true;
					else
						s = false;

					Overdue = false;
					Actual = false;
					Early = true;
				}
				else
				{
					s = false;
					Overdue = false;
					Actual = false;
					Early = false;
				}
				
				if(s)
				{
					if(Type == "NAMA")
						x.Append(rs.Rows[i]["NamaTagihan"].ToString() + "<br />");
					else if(Type == "TGL")
						x.Append(Cf.Day(rs.Rows[i]["TglJT"]) + "<br />");
					else if(Type == "OVERDUE")
					{
						if(Overdue)
						{
							x.Append(Cf.Num(rs.Rows[i]["NilaiTagihan"]) + "<br />");
							total1.Text = Cf.Num(Convert.ToDecimal(rs.Rows[i]["NilaiTagihan"])
								+ Convert.ToDecimal(total1.Text));
						}
						else
							x.Append("&nbsp;<br />");
					}
					else if(Type == "ACTUAL")
					{
						if(Actual)
						{
							x.Append(Cf.Num(rs.Rows[i]["NilaiTagihan"]) + "<br />");
							total2.Text = Cf.Num(Convert.ToDecimal(rs.Rows[i]["NilaiTagihan"])
								+ Convert.ToDecimal(total2.Text));
						}
						else
							x.Append("&nbsp;<br />");
					}
					else if(Type == "EARLY")
					{
						if(Early)
						{
							x.Append(Cf.Num(rs.Rows[i]["NilaiTagihan"]) + "<br />");
							total3.Text = Cf.Num(Convert.ToDecimal(rs.Rows[i]["NilaiTagihan"])
								+ Convert.ToDecimal(total3.Text));
						}
						else
							x.Append("&nbsp;<br />");
					}
					else if(Type == "TGLLUNAS")
					{
						strSql = "SELECT TOP 1 TglPelunasan"
							+ " FROM MS_PELUNASAN"
							+ " WHERE NoTagihan = " + Cf.Pk(rs.Rows[i]["NoUrut"])
							+ " AND NoKontrak = '" + NoKontrak + "'"
							+ " ORDER BY TglPelunasan DESC"
							;
						DataTable rs2 = Db.Rs(strSql);

						if(rs2.Rows.Count > 0)
							x.Append(Cf.Day(rs2.Rows[0]["TglPelunasan"]) + "<br />");
						else
							x.Append("&nbsp;<br />");
					}
					else if(Type == "A")
					{
						decimal t = 0;
						strSql = "SELECT ISNULL(SUM(NilaiPelunasan), 0)"
							+ " FROM MS_PELUNASAN"
							+ " WHERE NoTagihan = " + Cf.Pk(rs.Rows[i]["NoUrut"])
							+ " AND (CONVERT(varchar, TglPelunasan, 112) >= '" + Cf.Tgl112(Dari) + "'"
							+ " AND CONVERT(varchar, TglPelunasan, 112) <= '" + Cf.Tgl112(Sampai) + "')"
							+ " AND NoKontrak = '" + NoKontrak + "'"
							;
						t = Db.SingleDecimal(strSql);
						total4.Text = Cf.Num(t + Convert.ToDecimal(total4.Text));

						if(t != 0)
							x.Append(Cf.Num(t) + "<br />");
						else
							x.Append("&nbsp;<br />");
					}
					else if(Type == "B")
					{
						decimal t = 0;
						strSql = "SELECT ISNULL(SUM(NilaiPelunasan), 0)"
							+ " FROM MS_PELUNASAN"
							+ " WHERE NoTagihan = " + Cf.Pk(rs.Rows[i]["NoUrut"])
							+ " AND (CONVERT(varchar, TglPelunasan, 112) < '" + Cf.Tgl112(Dari) + "'"
							+ " OR CONVERT(varchar, TglPelunasan, 112) > '" + Cf.Tgl112(Sampai) + "')"
							+ " AND NoKontrak = '" + NoKontrak + "'"
							;
						t = Db.SingleDecimal(strSql);
						total5.Text = Cf.Num(t + Convert.ToDecimal(total5.Text));

						if(t != 0)
							x.Append(Cf.Num(t) + "<br />");
						else
							x.Append("&nbsp;<br />");
					}
				}
				
			}
			
			return x.ToString();
		}

		private void SubTotal()
		{
			TableRow r = new TableRow();
			TableCell c;

			c = Rpt.Foot();
			c.Text = "";
			c.ColumnSpan = 8;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = total1.Text;
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = total2.Text;
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = total3.Text;
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = "";
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = total4.Text;
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = total5.Text;
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			rpt.Rows.Add(r);
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

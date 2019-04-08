using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
	public partial class RealisasiDenda : System.Web.UI.Page
	{
		
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			if(!Page.IsPostBack)
			{
				Js.Focus(this,nokontrak);
				nokontrak.Attributes["ondblclick"] = "popDaftarKontrak('a');";
				frm.Visible = false;

				tagihandenda.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
				tagihandenda.Attributes["onkeyup"] = "CalcType(this,tempnum);";
				tagihandenda.Attributes["onblur"] = "CalcBlur(this);";
			}

			FeedBack();
			if(frm.Visible)
				Js.Confirm(this,"Lanjutkan proses Realisasi Denda?");
		}

		private void FeedBack()
		{
			feed.Text = "";
			if(!Page.IsPostBack)
			{
				if(Request.QueryString["done"]!=null)
					feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
						+ "<a href=\"javascript:popJadwalTagihan('"+Request.QueryString["done"]+"')\">"
						+ "Realisasi Denda Berhasil..."
						+ "</a>";
			}
		}

		private bool valid()
		{
			bool x = true;

			int c = Db.SingleInteger(
				"SELECT COUNT(*) FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "' AND Status = 'A'");

			if(c==0)
				x = false;

			if(!x)
				Js.Alert(
					this
					, "Kontrak Tidak Valid.\\n\\n"
					+ "Kemungkinan Sebab :\\n"
					+ "1. Kontrak tersebut tidak terdaftar.\\n"
					+ "2. Kontrak tersebut sudah dibatalkan.\\n"
					, "document.getElementById('nokontrak').focus();"
					+ "document.getElementById('nokontrak').select();"
					);

			return x;
		}

		protected void next_Click(object sender, System.EventArgs e)
		{
			if(valid())
			{
				pilih.Visible = false;
				frm.Visible = true;

				Fill();

				Js.Confirm(this,"Lanjutkan proses Realisasi Denda?");
			}
		}

		private void Fill()
		{
			Func.KontrakHeader(NoKontrak, nokontrakl, unit, customer, agent);

			string strSql = "SELECT * FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "'";
			DataTable rs = Db.Rs(strSql);

			decimal Denda = 0, RealDenda = 0, Sisa = 0, t = 0, TagihanDenda = 0;

			for(int i = 0; i < rs.Rows.Count; i++)
			{
				if(!Response.IsClientConnected)
					break;

				Denda += Convert.ToDecimal(rs.Rows[i]["Denda"]);
				RealDenda += Convert.ToDecimal(rs.Rows[i]["DendaReal"]);
				Sisa = Convert.ToDecimal(rs.Rows[i]["Denda"]) - Convert.ToDecimal(rs.Rows[i]["DendaReal"]);
				t += Sisa;

				TableRow r = new TableRow();
				TableCell c;

				c = new TableCell();
				c.Text = rs.Rows[i]["NoUrut"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["NamaTagihan"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Tipe"].ToString();
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
				c.Text = Cf.Num(rs.Rows[i]["Denda"]);
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Num(rs.Rows[i]["DendaReal"]);
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Num(Sisa);
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				rpt.Rows.Add(r);

				if(i == (rs.Rows.Count - 1))
					SubTotal(Denda, RealDenda, t);
			}

			TagihanDenda = Denda - RealDenda;

			tagihandenda.Text = Cf.Num(TagihanDenda);
			tgl.Text = Cf.Day(DateTime.Today);

			if(Func.CekAkunting(NoKontrak))
				warning.Text = "Transaksi sudah pernah diposting ke Akunting";
			else
				warning.Text = "";
		}

		protected void SubTotal(decimal t1, decimal t2, decimal t3)
		{
			TableRow r = new TableRow();
			TableCell c;

			c = new TableCell();
			c.Text = "<strong>Grand Total</strong>";
			c.HorizontalAlign = HorizontalAlign.Left;
			c.ColumnSpan = 5;
			r.Cells.Add(c);

			c = new TableCell();
			c.Text = "<strong>" + Cf.Num(t1) + "</strong>";
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			c = new TableCell();
			c.Text = "<strong>" + Cf.Num(t2) + "</strong>";
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			c = new TableCell();
			c.Text = "<strong>" + Cf.Num(t3) + "</strong>";
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			rpt.Rows.Add(r);
		}

		private bool datavalid()
		{
			bool x = true;
			string s = "";
			
			if(!Cf.isMoney(tagihandenda))
			{
				x = false;

				if(s == "")
					s = tagihandenda.ID;

				tagihandendac.Text = "Angka";
			}else
				tagihandendac.Text = "";

			if(!Cf.isTgl(tgl))
			{
				x = false;

				if(s == "")
					s = tgl.ID;

				tglc.Text = "Tanggal";
			}else
				tglc.Text = "";

			if(!x)
				Js.Alert(
					this
					, "Input Tidak Valid.\\n\\n"
					+ "Kemungkinan Sebab :\\n"
					+ "1. Tagihan Denda harus berupa angka.\\n"
					+ "2. Format Tanggal : Bulan / Tanggal / Tahun.\\n"
					, "document.getElementById('"+s+"').focus();"
					+ "document.getElementById('"+s+"').select();"
					);

			return x;
		}

		protected bool filevalid()
		{
			bool x = true;
			string s = "";

			if(file.PostedFile.FileName.Length!=0
				&& !file.PostedFile.FileName.EndsWith(".jpg"))
			{
				x = false;

				if(s == "")
					s = file.ID;
			}

			if(!x)
			{
				Js.Alert(
					this
					, "Proses Upload Gagal.\\n"
					+ "File yang boleh di-upload adalah file dengan extension .jpg saja."
					, "document.getElementById('" + s + "').focus();"
					);
			}

			return x;
		}

		protected void save_Click(object sender, System.EventArgs e)
		{	
			if(datavalid())
			{
				if(filevalid())
				{
					DateTime Tgl = Convert.ToDateTime(tgl.Text);
					decimal Nilai = Convert.ToDecimal(tagihandenda.Text);

					DataTable rsBef = Db.Rs("SELECT "
						+ "CONVERT(VARCHAR,NoUrut) + '.   ' + NamaTagihan + ' ('+Tipe+')   ' + CONVERT(VARCHAR,TglJT,106) + '   ' + CONVERT(VARCHAR,NilaiTagihan,1) "
						+ "FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' ORDER BY NoUrut");
				
					Db.Execute("UPDATE MS_TAGIHAN SET DendaReal = Denda WHERE NoKontrak = '" + NoKontrak + "'");

					Db.Execute("EXEC spTagihanDaftar"
						+ " '" + NoKontrak + "'"
						+ ", 'BIAYA DENDA'"
						+ ", '" + Tgl + "'"
						+ ", " + Nilai
						+ ", 'ADM'"
						);

					DataTable rsAft = Db.Rs("SELECT "
						+ "CONVERT(VARCHAR,NoUrut) + '.   ' + NamaTagihan + ' ('+Tipe+')   ' + CONVERT(VARCHAR,TglJT,106) + '   ' + CONVERT(VARCHAR,NilaiTagihan,1) "
						+ "FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' ORDER BY NoUrut");

					DataTable rs = Db.Rs("SELECT"
						+ " MS_KONTRAK.NoKontrak AS [No. Kontrak]"
						+ ",MS_KONTRAK.NoUnit AS [Unit]"
						+ ",MS_CUSTOMER.Nama AS [Customer]"
						+ ",MS_KONTRAK.NilaiKontrak AS [Nilai Kontrak]"
						+ ",MS_KONTRAK.Skema AS [Skema]"
						+ " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER"
						+ " ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
						+ " WHERE MS_KONTRAK.NoKontrak = '" + NoKontrak + "'");

					string Ket = Cf.LogCapture(rs)
						+ "<br>---REALISASI DENDA---<br>"
						+ Cf.LogList(rsBef, rsAft, "JADWAL TAGIHAN")
						;

					Db.Execute("EXEC spLogKontrak "
						+ " 'RD'"
						+ ",'" + Act.UserID + "'"
						+ ",'" + Act.IP + "'"
						+ ",'" + Ket + "'"
						+ ",'" + NoKontrak + "'"
						);

                    decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
                    string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                    Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                    /*Insert jurnal kontrak*/
                    string strKetJurnal = "KONTRAK REALISASI DENDA<br />" + Cf.Str(baru.Text);
			
					Db.Execute("EXEC spJurnalKontrak "
						+ " '" + Act.UserID + "'"
						+ ",'" + NoKontrak + "'"
						+ ",'" + strKetJurnal + "'"
						);

					if(file.PostedFile.FileName.Length!=0)
					{
						long JurnalID = Db.SingleLong("SELECT TOP 1 JurnalID FROM MS_KONTRAK_JURNAL ORDER BY JurnalID DESC");
						string path = Request.PhysicalApplicationPath
							+ "JurnalKontrak\\" + JurnalID + ".jpg";
						Dfc.UploadFile(".jpg",path,file);
					}
					/***********************/

					Response.Redirect("RealisasiDenda.aspx?done=" + NoKontrak);
				}
			}
		}

		private string NoKontrak
		{
			get
			{
				return Cf.Pk(nokontrak.Text);
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

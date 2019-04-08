using System;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using System.Drawing;
using System.Diagnostics;

namespace ISC064.MARKETINGJUAL.Laporan
{
	public partial class LaporanAktivitasCustomer : System.Web.UI.Page
	{
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();

			if(!Page.IsPostBack)
			{
				comp.InnerHtml = Mi.Pt;
				rptA.Visible = false;
				rptB.Visible = false;
				rpt.Visible = false;
				rptD.Visible = false;
				Js.Focus(this,scr);
				init();
				if(!Act.Sec("DownloadExcel")) xls.Enabled = false;
			}
		}

        protected int UserAgent()
        {
            int a = Db.SingleInteger("SELECT NoAgent FROM " + Mi.DbPrefix + "SECURITY..USERNAME WHERE UserID = '" + Act.UserID + "'");

            return a;
        }

        private void init()
        {
            dari.Text = Cf.Day(Cf.AwalBulan());
            sampai.Text = Cf.Day(Cf.AkhirBulan());
            project.Items.Clear();
            project.Items.Add("SEMUA");
            Act.ProjectList(project);
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
					,"<script type='text/javascript'>document.getElementById('"+s+"').select()</script>");
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
				//rpt.Controls.Add(rptC);
				rp.Controls.Add(headJudul);
				rp.Controls.Add(lblA);
				rp.Controls.Add(rptA);
				rp.Controls.Add(lblB);
				rp.Controls.Add(rptB);
				rp.Controls.Add(lblC);
				rp.Controls.Add(rpt);
				rp.Controls.Add(lblD);
				rp.Controls.Add(rptD);
				Rpt.ToExcel(this,rp);
			}
		}
        protected void pdf_Click(object sender, System.EventArgs e)
        {

            Process p = new System.Diagnostics.Process();

            string Nama = "Laporan Aktivitas Customer";
            string Link = "";
            DateTime TglGenerate = DateTime.Now;
            string FileName = "";
            string FileType = "application/pdf";
            string UserID = Act.UserID;
            string IP = Act.IP;

            Db.Execute("EXEC spLapPDFDaftar"

                    + " '" + Nama + "'"
                    + ",'" + Link + "'"
                    + ",'" + TglGenerate + "'"
                    + ",'" + IP + "'"
                    + ",'" + UserID + "'"
                    + ",'" + FileName + "'"
                    + ",'" + FileType + "'"
                    + ",'" + Cf.Date(dari.Text) + "'"
                    + ",'" + Cf.Date(sampai.Text) + "'"
                    );

            //get nomor customer terbaru
            int NoAttachment = Db.SingleInteger(
                "SELECT TOP 1 AttachmentID FROM LapPDF ORDER BY AttachmentID DESC");

            string strSql = "SELECT * FROM ISC064_MARKETINGJUAL..LapPDF WHERE AttachmentID  = '" + NoAttachment + "'";
            DataTable rs = Db.Rs(strSql);

            string nfilename = "AktivitasCustomer" + NoAttachment + ".pdf";

            //update filename
            Db.Execute("UPDATE ISC064_MARKETINGJUAL..LapPDF SET FileName= '" + nfilename + "' WHERE AttachmentID = " + NoAttachment);


            //folder untuk menyimpan file pdf
            string save = Mi.PathFilePDFReport + "AktivitasCustomer" + rs.Rows[0]["AttachmentID"] + ".pdf";

            string Project = "";
            if (project.SelectedIndex == 0)
            {
                Project = Act.ProjectListSql.Replace("'", "");
            }
            else
            {
                Project = project.SelectedValue;
            }


            //link untuk download pdf
            string link = Mi.PathAlamatWeb + "marketingjual/LaporanPDF/PDFLaporanAktivitasCustomer.aspx?id=" + rs.Rows[0]["AttachmentID"] + "&userid=" + UserID + "&project=" + Project + "&pers=" + pers.SelectedValue + "";

            //update link
            Db.Execute("UPDATE ISC064_MARKETINGJUAL..LapPDF SET Link= '" + link + "' WHERE AttachmentID = " + NoAttachment);

            //format page
            p.StartInfo.Arguments = "--orientation landscape --page-width 8.5in --page-height 11in --margin-left 0 --margin-right 0 --margin-top 0.25cm --margin-bottom 0 " + link + " " + save;

            //panggil aplikasi untuk mengconvert pdf
            p.StartInfo.FileName = Mi.PathWkhtmlPDFReport;
            p.Start();

            //60000 -> waktu jeda lama convert pdf
            p.WaitForExit(30000);

            string Src = Mi.PathFilePDFReport + nfilename;
            Mi.DownloadPDF(this, Src, (rs.Rows[0]["FileName"]).ToString(), rs.Rows[0]["FileType"].ToString());
        }

		private void Report()
		{
			param.Visible = false;

			lblA.Text = "<h3>A. Customer Reservasi</h3>";
			rptA.Visible = true;

			lblB.Text = "<h3>B. Customer Batal</h3>";
			rptB.Visible = true;

			lblC.Text = "<h3>C. Customer Pindah Unit</h3>";
			rpt.Visible = true;

			lblD.Text = "<h3>D. Customer Pengalihan Hak</h3>";
			rptD.Visible = true;
			//Header();

			newHeader();
			Fill();
		}

		private void Header()
		{
//			System.Text.StringBuilder x = new System.Text.StringBuilder();
//
//			Rpt.Judul(x, comp, judul);
//
//			DateTime Dari = Convert.ToDateTime(dari.Text);
//			DateTime Sampai = Convert.ToDateTime(sampai.Text);
//			Rpt.SubJudul(x
//				, "Tanggal : " + Cf.Day(dari.Text) + " s/d " + Cf.Day(sampai.Text)
//				);
//
//			Rpt.Header(rpt, x);
		}

		private void newHeader()
		{
			string header = "<h2>"+Mi.Pt+"</h2>";
			header += "<h1 class='title'>LAPORAN AKTIVITAS CUSTOMER</h1>";
            header += "<h4>Project : " + project.SelectedItem.Text + "</h4>";
            header += "Periode : " + Cf.Day(dari.Text) + " s/d " + Cf.Day(sampai.Text);
			header += "<br/> Laporan dihasilkan pada hari " + Cf.IndoWeek(DateTime.Today);
			header += ", " + Cf.Date(DateTime.Now)+" dari workstation "+Act.IP+" oleh user "+Act.UserID+"<br />";
			headJudul.Text = header;
		}

		private void fillReserv(DateTime Dari, DateTime Sampai)
		{
            string aa = "";
            if (UserAgent() > 0)
                aa = " AND a.NoAgent = " + UserAgent();

            string Project = "";
            if (project.SelectedValue != "SEMUA") Project = " AND b.Project = '" + project.SelectedValue + "'";

            string strSql = "SELECT a.*, b.LuasSG, CASE WHEN a.Status = 'A' THEN 'AKTIF' ELSE 'BLOKIR' END AS Status2, b.Project"
                + " FROM MS_RESERVASI a"
                + " INNER JOIN MS_UNIT b ON a.NoStock = b.NoStock"
                + " WHERE a.Tgl >= '" + Dari + "'"
                + " AND a.Tgl <= '" + Sampai + "'"
                + Project
                + aa
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
				c.Text = rs.Rows[i]["Status2"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Day(rs.Rows[i]["Tgl"]);
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				strSql = "SELECT b.Nama"
					+ " FROM MS_RESERVASI a"
					+ " INNER JOIN MS_CUSTOMER b"
					+ " ON a.NoCustomer = b.NoCustomer"
                    + " WHERE a.NoReservasi ='" + rs.Rows[i]["NoReservasi"].ToString() +"'"
					;
				//DataTable reserv = Db.Rs(strSql);
				c.Text = Db.SingleString(strSql);
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				string agentR = Db.SingleString("SELECT Nama FROM MS_AGENT WHERE NoAgent='"+rs.Rows[i]["NoAgent"]+"'");
				c.Text = agentR;
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["NoUnit"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["LuasSG"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["NoReservasi"].ToString();
				c.HorizontalAlign = HorizontalAlign.Center;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Day(rs.Rows[i]["TglExpire"]);
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["NoQueue"].ToString();
				c.HorizontalAlign = HorizontalAlign.Center;
				r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["Netto"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                strSql = "SELECT USERNAME.Nama" 
                       + " FROM ISC064_SECURITY..USERNAME" 
                       + " WHERE UserID in (select UserID from MS_RESERVASI_LOG" 
                       + " WHERE ket like" 
                       + " '%No. Reservasi : " + rs.Rows[i]["NoReservasi"].ToString() + "%')"
                        ;
                c.Text = Db.SingleString(strSql);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + rs.Rows[i]["Project"].ToString() + "'");
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                rptA.Rows.Add(r);
			}
			//return 0;
		}

		private void fillBatal(DateTime Dari, DateTime Sampai)
		{
            string aa = "";
            if (UserAgent() > 0)
                aa = " AND b.NoAgent = " + UserAgent();

            string Project = "";
            if (project.SelectedValue != "SEMUA") Project = " AND b.Project = '" + project.SelectedValue + "'";

            string Perusahaan = "";
            if (pers.SelectedValue != "SEMUA") Perusahaan = " AND b.Pers = '" + pers.SelectedValue + "'";

            string strSql = "SELECT a.*"
                + " FROM MS_KONTRAK_LOG a"
                + " INNER JOIN MS_KONTRAK b ON a.Pk = b.NoKontrak"
                + " WHERE a.Tgl >= '" + Dari + "'"
                + " AND a.Tgl <= '" + Sampai + "'"
                + " AND a.Aktivitas = 'BATAL'"
                + Project
                + Perusahaan
                + aa;
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
				c.Text = Cf.Day(rs.Rows[i]["Tgl"]);
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Pk"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				strSql = "SELECT b.Nama"
					+ " FROM MS_KONTRAK a"
					+ " INNER JOIN MS_CUSTOMER b"
					+ " ON a.NoCustomer = b.NoCustomer"
					+ " WHERE a.NoKontrak = '" + Cf.Pk(rs.Rows[i]["Pk"]) + "'"
					;
				c.Text = Db.SingleString(strSql);
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				strSql = "SELECT b.Nama"
					+ " FROM MS_KONTRAK a"
					+ " INNER JOIN MS_AGENT b"
					+ " ON a.NoAgent = b.NoAgent"
					+ " WHERE a.NoKontrak = '" + Cf.Pk(rs.Rows[i]["Pk"]) + "'"
					;
				c.Text = Db.SingleString(strSql);
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				strSql = "SELECT NoUnit"
					+ " FROM MS_KONTRAK"
					+ " WHERE NoKontrak = '" + Cf.Pk(rs.Rows[i]["Pk"]) + "'"
					;
				c.Text = Db.SingleString(strSql);
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

                c = new TableCell();
                strSql = "SELECT b.LuasSG"
                    + " FROM MS_KONTRAK a"
                    + " INNER JOIN MS_UNIT b ON a.NoStock = b.NoStock"
                    + " WHERE a.NoKontrak = '" + Cf.Pk(rs.Rows[i]["Pk"]) + "'"
                    ;
                c.Text = Cf.Num(Db.SingleDecimal(strSql));
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Day(rs.Rows[i]["Tgl"]);
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);
				
				string[] strTemp = Cf.SplitByString(rs.Rows[i]["Ket"].ToString(), "<br>");
				string Reason="";
				for(int j = 0; j < strTemp.Length; j++)
				{
					if(strTemp[j].StartsWith("Alasan"))
					 Reason = strTemp[j].ToString().Replace("Alasan Pembatalan : ", "");
				}
				c = new TableCell();
				c.Text = Reason;
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				strSql = "SELECT NilaiKontrak"
					+ " FROM MS_KONTRAK"
					+ " WHERE NoKontrak = '" + Cf.Pk(rs.Rows[i]["Pk"]) + "'"
					;
				c.Text = Convert.ToString(Cf.Num(Db.SingleDecimal(strSql)));
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				string biaya = "";
				string[] strTemp1 = Cf.SplitByString(rs.Rows[i]["Ket"].ToString(), "<br>");
				for(int j = 0; j < strTemp1.Length; j++)
				{
					if(strTemp1[j].StartsWith("Biaya"))
						biaya = strTemp1[j].ToString().Replace("Biaya Administrasi : ", "");
				}
				c = new TableCell();
				c.Text = biaya;
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

                c = new TableCell();
                strSql = "SELECT USERNAME.Nama"
                       + " FROM ISC064_SECURITY..USERNAME"
                       + " WHERE UserID in (select UserID from MS_KONTRAK_LOG"
                       + " WHERE Aktivitas = 'BATAL' AND "
                       + " Pk ='" + Cf.Pk(rs.Rows[i]["Pk"]) + "')"
                        ;
                c.Text = Db.SingleString(strSql);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + rs.Rows[i]["Project"].ToString() + "'");
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                rptB.Rows.Add(r);
			}
			//return 0;
		}
		
		private void fillGN(DateTime Dari, DateTime Sampai)
		{
            string aa = "";
            if (UserAgent() > 0)
                aa = " AND b.NoAgent = " + UserAgent();

            string Project = "";
            if (project.SelectedValue != "SEMUA") Project = " AND b.Project = '" + project.SelectedValue + "'";

            string Perusahaan = "";
            if (pers.SelectedValue != "SEMUA") Perusahaan = " AND b.Pers = '" + pers.SelectedValue + "'";

            string strSql = "SELECT a.*"
                + " FROM MS_KONTRAK_LOG a"
                + " INNER JOIN MS_KONTRAK b ON a.Pk = b.NoKontrak"
                + " WHERE a.Tgl >= '" + Dari + "'"
                + " AND a.Tgl <= '" + Sampai + "'"
                + " AND a.Aktivitas = 'GN'"
                + Project
                + Perusahaan
                + aa
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
				c.Text = Cf.Day(rs.Rows[i]["Tgl"]);
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Pk"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				strSql = "SELECT NoUnit"
					+ " FROM MS_KONTRAK "
					+ " WHERE NoKontrak = '" + Cf.Pk(rs.Rows[i]["Pk"]) + "'"
					;
				c.Text = Db.SingleString(strSql);
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

                c = new TableCell();
                strSql = "SELECT a.LuasSG"
                    + " FROM MS_UNIT a"
                    + " INNER JOIN MS_KONTRAK b ON a.NoStock = b.NoStock"
                    + " WHERE b.NoKontrak = '" + Cf.Pk(rs.Rows[i]["Pk"]) + "'"
                    ;
                c.Text = Cf.Num(Db.SingleDecimal(strSql));
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

				c = new TableCell();
				strSql = "SELECT b.Nama"
					+ " FROM MS_KONTRAK a"
					+ " INNER JOIN MS_AGENT b"
					+ " ON a.NoAgent = b.NoAgent"
					+ " WHERE a.NoKontrak = '" + Cf.Pk(rs.Rows[i]["Pk"]) + "'"
					;
				c.Text = Db.SingleString(strSql);
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);
				
				c = new TableCell();
				c.Text = Cf.Day(rs.Rows[i]["Tgl"]);
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				//Cek nama sebelum dan sesudah Pengalihan Hak
				string strBef = "", strAft = "";
				string[] strTemp = Cf.SplitByString(rs.Rows[i]["Ket"].ToString(), "<br>");
				bool isNext = false;

				for(int j = 0; j < strTemp.Length; j++)
				{
					if(!isNext)
					{
						if(strTemp[j].StartsWith("Nama Customer"))
						{
							strBef = strTemp[j].ToString().Replace("Nama Customer : ", "");
							isNext = true;
						}
					}
					else
					{
						if(strTemp[j].StartsWith("Nama Customer"))
						{
							strAft = strTemp[j].ToString().Replace("Nama Customer : ", "");
							break;
						}
					}
				}
				c = new TableCell();
				c.Text = strBef;
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				strSql = "SELECT b.Nama"
					+ " FROM MS_KONTRAK a"
					+ " INNER JOIN MS_CUSTOMER b"
					+ " ON a.NoCustomer = b.NoCustomer"
					+ " WHERE a.NoKontrak = '" + Cf.Pk(rs.Rows[i]["Pk"]) + "'"
					;
				c.Text = Db.SingleString(strSql);
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				strSql = "SELECT NilaiKontrak"
					+ " FROM MS_KONTRAK"
					+ " WHERE NoKontrak = '" + Cf.Pk(rs.Rows[i]["Pk"]) + "'"
					;
				c.Text = Cf.Num(Db.SingleDecimal(strSql));
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);
				
				string biaya = "";
				string[] strTemp1 = Cf.SplitByString(rs.Rows[i]["Ket"].ToString(), "<br>");
				for(int j = 0; j < strTemp1.Length; j++)
				{
					if(strTemp1[j].StartsWith("Biaya"))
						biaya = strTemp1[j].ToString().Replace("Biaya Administrasi : ", "");
				}
				c = new TableCell();
				c.Text = biaya;
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

                c = new TableCell();
                strSql = "SELECT USERNAME.Nama"
                       + " FROM ISC064_SECURITY..USERNAME"
                       + " WHERE UserID in (select UserID from MS_KONTRAK_LOG"
                       + " WHERE Aktivitas = 'GN' AND "
                       + " Pk ='" + Cf.Pk(rs.Rows[i]["Pk"]) + "')"
                        ;
                c.Text = Db.SingleString(strSql);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + rs.Rows[i]["Project"].ToString() + "'");
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                rptD.Rows.Add(r);
			}
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

			fillReserv(Dari, Sampai);
			fillBatal(Dari, Sampai);
			fillGN(Dari, Sampai);

            string aa = "";
            if (UserAgent() > 0)
                aa = " AND b.NoAgent = " + UserAgent();

            string Project = " AND b.Project IN (" + Act.ProjectListSql + ")";
            if (project.SelectedValue != "SEMUA") Project = " AND b.Project = '" + project.SelectedValue + "'";

            string Perusahaan = "";
            if (pers.SelectedValue != "SEMUA") Perusahaan = " AND b.Pers = '" + pers.SelectedValue + "'";

            string strSql = "SELECT a.*"
                + " FROM MS_KONTRAK_LOG a"
                + " INNER JOIN MS_KONTRAK b ON a.Pk = b.NoKontrak"
                + " WHERE a.Tgl >= '" + Dari + "'"
                + " AND a.Tgl <= '" + Sampai + "'"
                + " AND a.Aktivitas = 'GU'"
                + Project
                + Perusahaan
                + aa
				;
			DataTable rs = Db.Rs(strSql);

			for(int i = 0; i < rs.Rows.Count; i++)
			{
				if(!Response.IsClientConnected)
					break;

				string strBef = "", strAft = "", Reason = "";
				string[] strTemp = Cf.SplitByString(rs.Rows[i]["Ket"].ToString(), "<br>");
				bool isNext = false;

				for(int j = 0; j < strTemp.Length; j++)
				{
					if(!isNext)
					{
						if(strTemp[j].StartsWith("Unit"))
						{
							strBef = strTemp[j].ToString().Replace("Unit : ", "");
							isNext = true;
						}
					}
					else
					{
						if(strTemp[j].StartsWith("Unit"))
						{
							strAft = strTemp[j].ToString().Replace("Unit : ", "");
							break;
						}
					}
				}
				
				TableRow r = new TableRow();
				TableCell c;

				c = new TableCell();
				c.Text = (i + 1).ToString();
				c.HorizontalAlign = HorizontalAlign.Center;
				r.Cells.Add(c);
			
				c = new TableCell();
                strSql = "SELECT *"
                    + " FROM MS_KONTRAK"
                    + " WHERE NoKontrak = '" + rs.Rows[i]["Pk"].ToString() + "'"
                    ;
                DataTable tglK = Db.Rs(strSql);
                c.Text = Cf.Day(tglK.Rows[0]["TglKontrak"]);
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);
				
				c = new TableCell();
				c.Text = rs.Rows[i]["Pk"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				strSql = "SELECT b.Nama"
					+ " FROM MS_KONTRAK a"
					+ " INNER JOIN MS_CUSTOMER b"
					+ " ON a.NoCustomer = b.NoCustomer"
					+ " WHERE a.NoKontrak = '" + Cf.Pk(rs.Rows[i]["Pk"]) + "'"
					;
				c.Text = Db.SingleString(strSql);
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				strSql = "SELECT b.Nama"
					+ " FROM MS_KONTRAK a"
					+ " INNER JOIN MS_AGENT b"
					+ " ON a.NoAgent = b.NoAgent"
					+ " WHERE a.NoKontrak = '" + Cf.Pk(rs.Rows[i]["Pk"]) + "'"
					;
				c.Text = Db.SingleString(strSql);
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);
				
				c = new TableCell();
				c.Text = Cf.Day(rs.Rows[i]["Tgl"]);
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);
				
				c = new TableCell();
				c.Text = strBef;
				c.HorizontalAlign = HorizontalAlign.Center;
				r.Cells.Add(c);

                c = new TableCell();
                strSql = "SELECT LuasSG"
                    + " FROM MS_UNIT"
                    + " WHERE NoUnit = '" + strBef + "'"
                    ;
                c.Text = Cf.Num(Db.SingleDecimal(strSql));
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

				c = new TableCell();
				c.Text = strAft;
				c.HorizontalAlign = HorizontalAlign.Center;
				r.Cells.Add(c);

                c = new TableCell();
                strSql = "SELECT LuasSG"
                    + " FROM MS_UNIT"
                    + " WHERE NoUnit = '" + strAft + "'"
                    ;
                c.Text = Cf.Num(Db.SingleDecimal(strSql));
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

				c = new TableCell();
				strSql = "SELECT NilaiKontrak"
					+ " FROM MS_KONTRAK"
					+ " WHERE NoKontrak = '" + Cf.Pk(rs.Rows[i]["Pk"]) + "'"
					;
				c.Text = Cf.Num(Db.SingleDecimal(strSql));
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				string biaya = "";
				string[] strTemp1 = Cf.SplitByString(rs.Rows[i]["Ket"].ToString(), "<br>");
				for(int j = 0; j < strTemp1.Length; j++)
				{
					if(strTemp1[j].StartsWith("Biaya"))
						biaya = strTemp1[j].ToString().Replace("Biaya Administrasi : ", "");
				}
				c = new TableCell();
				c.Text = biaya;
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

                c = new TableCell();
                strSql = "SELECT USERNAME.Nama"
                       + " FROM ISC064_SECURITY..USERNAME"
                       + " WHERE UserID in (select UserID from MS_KONTRAK_LOG"
                       + " WHERE Aktivitas = 'GU' AND "
                       + " Pk ='" + Cf.Pk(rs.Rows[i]["Pk"]) + "')"
                        ;
                c.Text = Db.SingleString(strSql);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + rs.Rows[i]["Project"].ToString() + "'");
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                rpt.Rows.Add(r);
			}
		}
        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            pers.Items.Clear();
            pers.Items.Add("SEMUA");
            if (project.SelectedValue != "SEMUA")
            {
                string strSql = "SELECT * FROM ISC064_SECURITY..PTSec A "
                + "INNER JOIN ISC064_SECURITY..REF_PERS B ON A.Pers = B.Pers "
                + "INNER JOIN ISC064_SECURITY..REF_PROJECT C ON A.Pers = C.Pers "
                + " WHERE A.UserID='" + Act.UserID + "' AND C.Project ='" + project.SelectedValue + "'  AND A.Granted = 1";

                DataTable rs = Db.Rs(strSql);
                for (int i = 0; i < rs.Rows.Count; i++)
                {
                    string t = rs.Rows[i]["Nama"].ToString();
                    string v = rs.Rows[i]["Pers"].ToString();
                    pers.Items.Add(new ListItem(t, v));
                }
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

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
	public partial class LaporanBatal : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.ListBox lokasi;
		protected System.Web.UI.WebControls.ListBox agent;
		protected System.Web.UI.WebControls.RadioButton statusS;
		protected System.Web.UI.WebControls.RadioButton statusA;
		protected System.Web.UI.WebControls.RadioButton statusB;
		protected System.Web.UI.WebControls.CheckBox jenisCheck;
		protected System.Web.UI.WebControls.Label jenisc;
		protected System.Web.UI.WebControls.CheckBoxList jenis;
		protected System.Web.UI.WebControls.RadioButton bfS;
		protected System.Web.UI.WebControls.RadioButton bf1;
		protected System.Web.UI.WebControls.RadioButton bf2;
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();

			if(!Page.IsPostBack)
			{
				comp.InnerHtml = Mi.Pt;
				rpt.Visible = false;
				headJudul.Visible = false;
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
				rp.Controls.Add(headJudul);
				rp.Controls.Add(rpt);
				Rpt.ToExcel(this,rp);
			}
		}
        protected void pdf_Click(object sender, System.EventArgs e)
        {

            Process p = new System.Diagnostics.Process();

            string Nama = "Laporan Pembatalan";
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

            string nfilename = "LaporanPembatalan" + NoAttachment + ".pdf";

            //update filename
            Db.Execute("UPDATE ISC064_MARKETINGJUAL..LapPDF SET FileName= '" + nfilename + "' WHERE AttachmentID = " + NoAttachment);


            //folder untuk menyimpan file pdf
            string save = Mi.PathFilePDFReport + "LaporanPembatalan" + rs.Rows[0]["AttachmentID"] + ".pdf";

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
            string link = Mi.PathAlamatWeb + "marketingjual/LaporanPDF/PDFLaporanBatal.aspx?id=" + rs.Rows[0]["AttachmentID"] + "&userid=" + UserID + "&project=" + Project + "&pers=" + pers.SelectedValue + "";

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
			rpt.Visible = true;
			headJudul.Visible = true;
			
			newHeader();
			//Header();
			Fill();
		}

		private void newHeader()
		{
			string header = "<h2>"+Mi.Pt+"</h2>";
			header += "<h1 class='title'>Laporan Pembatalan</h1>";            
            header += "<h4>Project : " + project.SelectedItem.Text + "</h4>";
            header += "Periode : " + Cf.Day(dari.Text) + " s/d " + Cf.Day(sampai.Text);
			header += "<br/> Laporan dihasilkan pada hari " + Cf.IndoWeek(DateTime.Today);
			header += ", " + Cf.Date(DateTime.Now)+" dari workstation "+Act.IP+" oleh user "+Act.UserID+"<br /><br />";
			headJudul.Text = header;
		}

		private void Header()
		{
			System.Text.StringBuilder x = new System.Text.StringBuilder();

			Rpt.Judul(x, comp, judul);

			DateTime Dari = Convert.ToDateTime(dari.Text);
			DateTime Sampai = Convert.ToDateTime(sampai.Text);
			Rpt.SubJudul(x
				, "Tgl. Batal: " + Cf.Day(dari.Text) + " s/d " + Cf.Day(sampai.Text)
				);

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

            string Project = " AND a.Project IN (" + Act.ProjectListSql + ")";
            if (project.SelectedValue != "SEMUA") Project = " AND a.Project = '" + project.SelectedValue + "'";

            string Perusahaan = "";
            if (pers.SelectedValue != "SEMUA") Perusahaan = " AND a.Pers = '" + pers.SelectedValue + "'";

            string aa = "";
            if (UserAgent() > 0)
                aa = " AND a.NoAgent = " + UserAgent();

			decimal t1 = 0, t2 = 0, t3 = 0, t4 = 0, t5 = 0, t6 = 0, t7=0; 
			
			string strSql = "SELECT "
                + " a.TglKontrak"
                + " ,a.NoKontrak"
                + " ,a.Gross"
                + " ,a.DiskonRupiah"
                + " ,a.DiskonTambahan"
                + " ,a.BungaNominal"
                + " ,a.TglBatal"
                + " ,a.TotalLunasBatal"
                + " ,a.NilaiPulang"
                + " ,a.NilaiKontrak"
                + " ,a.Project"
                + " ,a.NilaiKlaim, a.NilaiPPN, a.NilaiDPP"
                + " ,b.NoUnit, b.Jenis, b.LuasSG"
                + " ,c.Nama, d.Nama AS Ag"
				+ " FROM MS_KONTRAK a"
                + " INNER JOIN MS_UNIT b ON a.NoStock= b.NoStock"
                + " INNER JOIN MS_CUSTOMER c ON a.NoCustomer = c.NoCustomer"
                + " INNER JOIN MS_AGENT d ON a.NoAgent = d.NoAgent"
                + " WHERE a.TglBatal >= '" + Dari + "' AND a.TglBatal <= '" + Sampai + "'"
                + " AND a.Status = 'B'"
                + Perusahaan
                + Project
                + aa
				;
            //Response.Write(strSql);
			DataTable rs = Db.Rs(strSql);

			for(int i = 0; i < rs.Rows.Count; i++)
			{
				if(!Response.IsClientConnected)
					break;

				TableRow r = new TableRow();
				r.Attributes["ondblclick"] = "javascript:popEditKontrak('" + rs.Rows[i]["NoKontrak"] + "');";
				TableCell c;

				c = new TableCell();
				c.Text = (i + 1).ToString();
				c.HorizontalAlign = HorizontalAlign.Center;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Day(rs.Rows[i]["TglKontrak"]);
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["NoKontrak"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

                string unit = rs.Rows[i]["NoUnit"].ToString();
                string[] nounit = Cf.SplitByString(unit, "/");
                //string Tower = unit.Substring(1, 1);
                //string Lantai = unit.Substring(3, 2);
                //string nounit = unit.Substring(6, 2);

                c = new TableCell();
                c.Text = unit;// "SP" + nounit[0].Substring(1) + nounit[1] + nounit[2];
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

				c = new TableCell();
                c.Text = rs.Rows[i]["Nama"].ToString();//Db.SingleString("SELECT Nama FROM MS_CUSTOMER WHERE NoCustomer = " + rs.Rows[i]["NoCustomer"]);
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
                c.Text = rs.Rows[i]["Ag"].ToString();//Db.SingleString("SELECT Nama FROM MS_AGENT a INNER JOIN MS_KONTRAK b ON a.NoAgent=b.NoAgent WHERE b.NoCustomer = " + rs.Rows[i]["NoCustomer"]);
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
                c.Text = rs.Rows[i]["NoUnit"].ToString();//Db.SingleString("SELECT NoUnit FROM MS_KONTRAK WHERE NoCustomer = " + rs.Rows[i]["NoCustomer"]);
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
                c.Text = rs.Rows[i]["Jenis"].ToString();//Db.SingleString("SELECT Jenis FROM MS_KONTRAK WHERE NoCustomer = " + rs.Rows[i]["NoCustomer"]);
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["LuasSG"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Num(rs.Rows[i]["Gross"]);
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Num(Math.Round((Convert.ToDecimal(rs.Rows[i]["DiskonRupiah"]) + Convert.ToDecimal(rs.Rows[i]["DiskonTambahan"]))));
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Math.Round(Convert.ToDecimal(rs.Rows[i]["BungaNominal"])));
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Day(rs.Rows[i]["TglBatal"]);
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

                c = new TableCell();
                c.Text = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + rs.Rows[i]["Project"].ToString() + "'");
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["NilaiKontrak"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Num(rs.Rows[i]["TotalLunasBatal"]);
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Num(rs.Rows[i]["NilaiPulang"]);
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Num(rs.Rows[i]["NilaiKlaim"]);
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				c = new TableCell();
				decimal DPP = Convert.ToDecimal(rs.Rows[i]["NilaiDPP"]);//(rs.Rows[i]["NilaiKontrak"]) - Convert.ToDecimal(rs.Rows[i]["NilaiPPN"]);//Math.Round(Convert.ToDecimal(rs.Rows[i]["NilaiKontrak"])/(decimal)1.1);
				c.Text = Cf.Num(Math.Round(DPP));
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				c = new TableCell();
				decimal PPN = Convert.ToDecimal(rs.Rows[i]["NilaiPPN"]);//Math.Round(DPP * (decimal)0.1);
				c.Text = Cf.Num(Math.Round(PPN));
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				rpt.Rows.Add(r);

				t1 += Convert.ToDecimal(rs.Rows[i]["NilaiKontrak"]);
				t2 += Convert.ToDecimal(rs.Rows[i]["NilaiKlaim"]);
				t3 += Convert.ToDecimal(rs.Rows[i]["TotalLunasBatal"]);
				t4 += Convert.ToDecimal(rs.Rows[i]["NilaiPulang"]);
				t5 += DPP;
                t6 += PPN;
                t7 += Convert.ToDecimal(rs.Rows[i]["BungaNominal"]);

				if(i == rs.Rows.Count - 1)
					SubTotal(t1, t2, t3, t4, t5, t6, t7);
			}
		}

		protected void SubTotal(decimal t1, decimal t2, decimal t3, decimal t4, decimal t5, decimal t6, decimal t7)
		{
			TableRow r = new TableRow();
			TableCell c;

			c = Rpt.Foot();
			c.Text = "<strong>TOTAL</strong>";
			c.ColumnSpan = 14;
			r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t1);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(t3);
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);
			
			c = Rpt.Foot();
			c.Text = Cf.Num(t4);
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);



			c = Rpt.Foot();
			c.Text = Cf.Num(t2);
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);
			
			c = Rpt.Foot();
			c.Text = Cf.Num(Math.Round(t5));
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(Math.Round(t6));
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);


			rpt.Rows.Add(r);
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

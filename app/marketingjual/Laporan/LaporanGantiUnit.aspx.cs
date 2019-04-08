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
	public partial class LaporanGantiUnit : System.Web.UI.Page
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
				rp.Controls.Add(headJudul);
				rp.Controls.Add(rpt);
				Rpt.ToExcel(this,rp);
			}
		}
        protected void pdf_Click(object sender, System.EventArgs e)
        {

            Process p = new System.Diagnostics.Process();

            string Nama = "Laporan Pindah Unit";
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

            string nfilename = "GantiUnit" + NoAttachment + ".pdf";

            //update filename
            Db.Execute("UPDATE ISC064_MARKETINGJUAL..LapPDF SET FileName= '" + nfilename + "' WHERE AttachmentID = " + NoAttachment);


            //folder untuk menyimpan file pdf
            string save = Mi.PathFilePDFReport + "GantiUnit" + rs.Rows[0]["AttachmentID"] + ".pdf";

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
            string link = Mi.PathAlamatWeb + "marketingjual/LaporanPDF/PDFLaporanGantiUnit.aspx?id=" + rs.Rows[0]["AttachmentID"] + "&userid=" + UserID + "&project=" + Project + "&pers=" + pers.SelectedValue + "";

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
			
			newHeader();
			//Header();
			Fill();
		}

		private void newHeader()
		{
			string header = "<h2>"+Mi.Pt+"</h2>";
			header += "<h1 class='title'>Laporan Pindah Unit</h1>";            
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
				, "Tanggal : " + Cf.Day(dari.Text) + " s/d " + Cf.Day(sampai.Text)
				);

            //Rpt.Header(rpt, x);
            string legend = "";
            Rpt.HeaderReport(headReport, legend, x);
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

            string Project = " AND b.Project IN (" + Act.ProjectListSql + ")";
            if (project.SelectedValue != "SEMUA") Project = " AND b.Project = '" + project.SelectedValue + "'";

            string Perusahaan = "";
            if (pers.SelectedValue != "SEMUA") Perusahaan = " AND b.Pers = '" + pers.SelectedValue + "'";

            string aa = "";
            if (UserAgent() > 0)
                aa = " AND b.NoAgent = " + UserAgent();

			string strSql = "SELECT a.*"
                + " FROM MS_KONTRAK_LOG a"
                + " INNER JOIN MS_KONTRAK b ON a.Pk = b.NoKontrak"
                + " WHERE a.Tgl >= '" + Dari + "'"
                + " AND a.Tgl <= '" + Sampai + "'"
                + " AND a.Aktivitas = 'GU'"
                + " AND b.Status = 'A'"
                + Project
                + Perusahaan
                + aa
				;
			DataTable rs = Db.Rs(strSql);

			for(int i = 0; i < rs.Rows.Count; i++)
			{
				if(!Response.IsClientConnected) break;

                string strBef = "", strAft = "", strTgl = "";
                string[] strTemp = Cf.SplitByString(rs.Rows[i]["Ket"].ToString(), "<br>");
                bool isNext = false;

                for (int j = 0; j < strTemp.Length; j++)
                {
                    if (!isNext)
                    {
                        if (strTemp[j].StartsWith("No. Stock"))
                        {
                            strBef = strTemp[j].ToString().Replace("No. Stock : ", "");
                            isNext = true;
                        }
                    }
                    else
                    {
                        if (strTemp[j].StartsWith("No. Stock"))
                        {
                            strAft = strTemp[j].ToString().Replace("No. Stock : ", "");
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
                c.Text = Cf.Day(Db.SingleTime("SELECT TglKontrak FROM MS_KONTRAK WHERE NoKontrak = '" + rs.Rows[i]["Pk"] + "'"));
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
                c.Text = strBef;
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Db.SingleString("SELECT Jenis FROM MS_UNIT WHERE NoStock = '" + strBef + "'");
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                strSql = "SELECT LuasSG"
                    + " FROM MS_UNIT"
                    + " WHERE NoStock = '" + strBef + "'"
                    ;
                c.Text = Cf.Num(Db.SingleDecimal(strSql));
                c.HorizontalAlign = HorizontalAlign.Left;                
                r.Cells.Add(c);

                c = new TableCell();
                decimal pl = Db.SingleDecimal("SELECT Pricelist FROM MS_UNIT WHERE NoStock = '" + strBef + "'");
                c.Text = Cf.Num(pl);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                decimal discrup = Db.SingleDecimal("SELECT DiskonRupiah FROM MS_Kontrak WHERE NoStock = '" + strAft + "'");
                decimal disctambah = Db.SingleDecimal("SELECT DiskonTambahan FROM MS_Kontrak WHERE NoStock = '" + strAft + "'");
                decimal bunga = Db.SingleDecimal("SELECT BungaNominal FROM MS_Kontrak WHERE NoStock = '" + strAft + "'");
                c.Text = Cf.Num(discrup + disctambah);                
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(pl + bunga - discrup - disctambah);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = strAft;
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Db.SingleString("SELECT Jenis FROM MS_UNIT WHERE NoStock = '" + strAft + "'");
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                strSql = "SELECT LuasSG"
                    + " FROM MS_UNIT"
                    + " WHERE NoStock = '" + strAft + "'"
                    ;
                c.Text = Cf.Num(Db.SingleDecimal(strSql));
                c.HorizontalAlign = HorizontalAlign.Left;                
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Db.SingleDecimal("SELECT Gross FROM MS_Kontrak WHERE NoStock = '" + strAft + "'"));
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(discrup + disctambah);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Db.SingleDecimal("SELECT NilaiKontrak FROM MS_Kontrak WHERE NoStock = '" + strAft + "'"));
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                for (int k = 0; k < strTemp.Length; k++)
                {
                    if (strTemp[k].StartsWith("Tgl Pindah Unit"))
                    {
                        strTgl = strTemp[k].ToString().Replace("Tgl Pindah Unit : ", "");
                    }
                }
                c = new TableCell();
                c.Text = strTgl;
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);
				rpt.Rows.Add(r);

                c = new TableCell();
                c.Text = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + rs.Rows[i]["Project"].ToString() + "'");
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

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

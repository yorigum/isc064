using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.FINANCEAR.Laporan
{
	public partial class StokBG : System.Web.UI.Page
	{
        private string UserID { get { return (Request.QueryString["userid"]); } }
        private string AttachmentID { get { return (Request.QueryString["id"]); } }
        private string TanggalInput { get { return (Request.QueryString["tglinput"]); } }
        private string Kasir { get { return (Request.QueryString["kasir"]); } }
        private string Metode { get { return (Request.QueryString["metode"]); } }
        private string Project { get { return (Request.QueryString["project"]); } }
        private string Perusahaan { get { return (Request.QueryString["pers"]); } }
        private string Perhitungan { get { return (Request.QueryString["perhitungan"]); } }
		protected void Page_Load(object sender, System.EventArgs e)
		{
            Report();
		}

        protected int UserAgent()
        {
            int a = Db.SingleInteger("SELECT NoAgent FROM " + Mi.DbPrefix + "SECURITY..USERNAME WHERE UserID = '" + UserID + "'");

            return a;
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

			Rpt.SubJudul(x
				, "Status : " + Metode
				);

            string Tahun = Db.SingleString("SELECT PeriodeThn FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
			Rpt.SubJudul(x
				, "Tahun : " + Tahun
				);

			string hitung = "";
            if (Perhitungan == "KUANTITAS") hitung = Perhitungan;
            if (Perhitungan == "RUPIAH") hitung = Perhitungan;
			Rpt.SubJudul(x
				,"Perhitungan : " + hitung);

            Rpt.SubJudul(x
                , "Project : " + Project
                );
            string pers = (Perusahaan == "SEMUA") ? "SEMUA" : Db.SingleString("SELECT Nama FROM "+Mi.DbPrefix+"SECURITY..REF_PERS WHERE Pers = '"+Perusahaan+"'");
            Rpt.SubJudul(x
                , "Perusahaan : " + pers
                );
            string legend = "";
            //Rpt.Header(rpt, x);
            Rpt.HeaderReport(headReport, legend, x);
		}

		private void Fill()
		{
			decimal GrandTotal = 0;

			for(int bln=1;bln<=12;bln++)
			{
				if(!Response.IsClientConnected) break;

				TableRow r = new TableRow();
				TableCell c;

				r.VerticalAlign = VerticalAlign.Top;

				c = new TableCell();
				c.Text = Cf.Monthname(bln);
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				int t = 0;
				int gt = 0;
				decimal rp = 0;
				decimal grp = 0;
				
				c = new TableCell();
				c.HorizontalAlign = HorizontalAlign.Right;
				c.Font.Bold = true;
				r.Cells.Add(c);
                string Tahun = Db.SingleString("SELECT PeriodeThn FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");

				int akhirbulan = DateTime.DaysInMonth(
					Convert.ToInt32(Tahun)
					,bln
					);

				for(int hari=1;hari<=31;hari++)
				{
					if(hari<=akhirbulan)
					{
						c = new TableCell();
						if(Perhitungan == "KUANTITAS")
						{
							t = sum1(hari,bln);
							gt = gt + t;
							if(t!=0)
								c.Text = t.ToString();
						}
						if(Perhitungan == "RUPIAH")
						{
							rp = sum2(hari,bln);
							grp = grp + rp;
							if(rp!=0)
								c.Text = Cf.Num(rp);
						}
						c.HorizontalAlign = HorizontalAlign.Right;
						r.Cells.Add(c);
					}
				}

				c = r.Cells[1];
				if(Perhitungan == "KUANTITAS")
					c.Text = gt.ToString();
				if(Perhitungan == "RUPIAH")
					c.Text = Cf.Num(grp);

				rpt.Rows.Add(r);

				if(Perhitungan == "KUANTITAS")
					GrandTotal += Convert.ToInt32(gt);
				else if(Perhitungan == "RUPIAH")
					GrandTotal += grp;

				if(bln == 12)
					SubTotal(GrandTotal);
			}
		}

		private void SubTotal(decimal GrandTotal)
		{
			TableRow r = new TableRow();
			TableCell c;

			c = Rpt.Foot();
			c.Text = "Grand Total";
			c.HorizontalAlign = HorizontalAlign.Left;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(GrandTotal);
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			rpt.Rows.Add(r);
		}

		private int sum1(int day, int bln)
		{
			string addq = "";
			if(Metode == "1")
				addq = " AND StatusBG = 'OK'";
			else if(Metode == "2")
				addq = " AND StatusBG = 'OK' AND a.Status = 'BARU'";
			else if(Metode == "3")
				addq = " AND StatusBG = 'OK' AND a.Status = 'POST'";
			else if(Metode == "4")
				addq = " AND StatusBG = 'BAD'";

            string Tahun = Db.SingleString("SELECT PeriodeThn FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");

            string nProject = "";
            if (Project != "SEMUA") nProject = " AND b.Project IN('" + Project.Replace(",","','") + "')";
            string nPerusahaan = "";
            if (Perusahaan != "SEMUA") nPerusahaan = " AND b.Pers = '" + Perusahaan + "'";

            DateTime d = Convert.ToDateTime(
				bln + "/" + day + "/" + Tahun
				);

            string agent = "";
            if (UserAgent() > 0)
                agent = " AND (SELECT NoAgent FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = MS_TTS.Ref) = " + UserAgent();

            return Db.SingleInteger("SELECT COUNT(DISTINCT NoBG)"
                + " FROM MS_TTS a"
                + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK b ON a.Ref = b.NoKontrak"
                + " WHERE a.CaraBayar = 'BG' "
                + " AND CONVERT(varchar,TglBG,112) = '" + Cf.Tgl112(d) + "'"
                + nProject
                + nPerusahaan
                + addq
                + agent
				);
		}

		private decimal sum2(int day, int bln)
		{
			string addq = "";
			if(Metode == "1")
				addq = " AND StatusBG = 'OK'";
			else if(Metode == "2")
				addq = " AND StatusBG = 'OK' AND a.Status = 'BARU'";
			else if(Metode == "3")
				addq = " AND StatusBG = 'OK' AND a.Status = 'POST'";
			else if(Metode == "4")
				addq = " AND StatusBG = 'BAD'";

            string Tahun = Db.SingleString("SELECT PeriodeThn FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");

            string nProject = "";
            if (Project != "SEMUA") nProject = " AND b.Project IN('" + Project.Replace(",", "','") + "')";
            string nPerusahaan = "";
            if (Perusahaan != "SEMUA") nPerusahaan = " AND b.Pers = '" + Perusahaan + "'";

            DateTime d = Convert.ToDateTime(
				bln + "/" + day + "/" + Tahun
				);

            string agent = "";
            if (UserAgent() > 0)
                agent = " AND (SELECT NoAgent FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = MS_TTS.Ref) = " + UserAgent();

            return Db.SingleDecimal("SELECT ISNULL(SUM(Total),0)"
                + " FROM MS_TTS a"
                + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK b ON a.Ref = b.NoKontrak"
                + " WHERE a.CaraBayar = 'BG' "
                + " AND CONVERT(varchar,TglBG,112) = '" + Cf.Tgl112(d) + "'"
                + nProject
                + nPerusahaan
                + addq
                + agent
				);
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

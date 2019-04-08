using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.KPA.Laporan
{
	public partial class MasterPiutangBank : System.Web.UI.Page
	{
        private string UserID { get { return (Request.QueryString["userid"]); } }
        private string StatusA { get { return (Request.QueryString["status_a"]); } }
        private string StatusS { get { return (Request.QueryString["status_s"]); } }
        private string StatusB { get { return (Request.QueryString["status_b"]); } }
        private string JenisTanggal { get { return (Request.QueryString["jenistgl"]); } }
        private string Project { get { return (Request.QueryString["project"]); } }
        private string Perusahaan { get { return (Request.QueryString["pers"]); } }
        private string AttachmentID { get { return (Request.QueryString["id"]); } }
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

			if(StatusA != "")
				Rpt.SubJudul(x, "Status : " + StatusA);
			else if(StatusB != "")
				Rpt.SubJudul(x, "Status : " + StatusB);
			else
				Rpt.SubJudul(x, "Status : " + StatusS);


            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");

            string tanggal = "";
            if (JenisTanggal == "TglKontrak")
                tanggal = "Tanggal Kontrak";
            else if (JenisTanggal == "TglJt")
                tanggal = "Tanggal Jatuh Tempo";

            Rpt.SubJudul(x
				, tanggal + " : " + Cf.Day(Dari) + " s/d " + Cf.Day(Sampai)
				);

            Rpt.SubJudul(
                x, "Project : " + Project
                );

            string pers = (Perusahaan == "SEMUA") ? "SEMUA" : Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PERS WHERE Pers = '" + Perusahaan + "'");
            Rpt.SubJudul(
                x, "Perusahaan : " + pers
                );

            string legend = "<br />Status: A = Aktif / B = Batal.<br />"
                          + "Tipe : BF = Booking Fee / DP = Downpayment / ANG = Angsuran / ADM = Biaya Administrasi.<br />"
                          + "Cara Bayar: TN = Tunai / KD = Kartu Debit / KK = Kartu Kredit / TR = Transfer Bank / BG = Cek Giro / DN = Diskon.<br />"
                          + "** = Jatuh Tempo.";

            Rpt.HeaderReport(headReport, legend, x);
        }

		private void Fill()
		{
            string nProject = "";
            if (Project != "SEMUA") nProject = " AND b.Project IN ('" + Project.Replace(",", "','") + "')";

            string nPerusahaan = "";
            if (Perusahaan != "SEMUA") nPerusahaan = " AND b.Pers = '" + Perusahaan + "'";

            string Status = "";
			if(StatusA != "") Status = " AND b.Status = 'A'";
			if(StatusB != "") Status = " AND b.Status = 'B'";

			string tgl = "";
			string order = "";
            if (JenisTanggal == "TglKontrak")
			{
				tgl = "b.TglKontrak";
				order = "b.TglKontrak, a.NoUrut";
			}
			if(JenisTanggal == "TglJt")
			{
                tgl = "a.TglJT";
                order = "a.TglJT, b.NoKontrak";
			}

            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");

            string aa = "";
            if (UserAgent() > 0)
                aa = " AND " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK.NoAgent = " + UserAgent();

            decimal total = 0;
            decimal t2 = 0;
            decimal t3 = 0;

            string strSql = "SELECT "
                + " a.NoKontrak"
                + ",a.NoUrut"
                + ",a.Tipe"
                + ",b.TglKontrak"
                + ",c.NoUnit"
                + ",d.Nama AS Cs"
                + ",d.NoTelp AS NoTelp"
                + ",d.NoHp AS NoHp"
                + ",a.NamaTagihan"
                + ",a.TglJT"
                + ",a.NilaiTagihan"
                + ",b.Status"
                + ",DATEDIFF(day,GETDATE(),TglJT) AS Diff"
                + " FROM ISC064_MARKETINGJUAL..MS_TAGIHAN_KPA a INNER JOIN ISC064_MARKETINGJUAL..MS_KONTRAK b ON a.NoKontrak = b.NoKontrak"
                + " INNER JOIN ISC064_MARKETINGJUAL..MS_CUSTOMER d ON b.NoCustomer = d.NoCustomer"
                + " INNER JOIN ISC064_MARKETINGJUAL..MS_UNIT c ON b.NoStock = c.NoStock"
                + " WHERE 1=1 "
                + " AND CONVERT(varchar," + tgl + ",112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND CONVERT(varchar," + tgl + ",112) <= '" + Cf.Tgl112(Sampai) + "'"
                + nProject
                + nPerusahaan
                + Status
                + aa
                + " ORDER BY NoKontrak";


            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableRow r2 = new TableRow();
                TableCell c;
                TableCell c2;

                r.VerticalAlign = VerticalAlign.Top;
                r.Attributes["ondblclick"] = "popJadwalTagihan('" + rs.Rows[i]["NoKontrak"] + "')";

                string jt = "";
                if (Convert.ToInt32(rs.Rows[i]["Diff"]) <= 0)
                    jt = " **";

                c = new TableCell();
                c.Text = rs.Rows[i]["NoKontrak"] + "." + rs.Rows[i]["NoUrut"]
                    + jt
                    ;
                c.Font.Size = 10;
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Font.Size = 10;
                c.Text = rs.Rows[i]["Status"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Font.Size = 10;
                c.Text = Cf.Day(rs.Rows[i]["TglKontrak"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Font.Size = 10;
                c.Text = rs.Rows[i]["Cs"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Font.Size = 10;
                c.Text = rs.Rows[i]["NoUnit"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Font.Size = 10;
                c.Text = rs.Rows[i]["Tipe"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Font.Size = 10;
                c.Text = rs.Rows[i]["NamaTagihan"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Font.Size = 10;
                c.Text = Cf.Day(rs.Rows[i]["TglJT"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Font.Size = 10;
                c.Text = Cf.Num(rs.Rows[i]["NilaiTagihan"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Font.Size = 10;
                decimal Pengajuan = Db.SingleDecimal("SELECT ISNULL(Total, 0) FROM ISC064_FINANCEAR..MS_PENGAJUAN_KPA a join ISC064_FINANCEAR..MS_PENGAJUAN_KPA_DETIL b ON a.NoPengajuan = b.NoPengajuan WHERE b.NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' AND b.Notagihan = '" + rs.Rows[i]["NoUrut"] + "'");
                c.Text = Cf.Num(Pengajuan);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Font.Size = 10;
                decimal Pel = Db.SingleDecimal("SELECT ISNULL (SUM(NilaiPelunasan),0) FROM MS_PELUNASAN_KPA WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' AND NoTagihan = " + rs.Rows[i]["NoUrut"]);
                c.Text = Cf.Num(Pel);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Font.Size = 10;
                c.Text = Cf.Num((decimal)rs.Rows[i]["NilaiTagihan"] - Pel);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                //BrisBru(r, c, rs.Rows[i]["NoKontrak"].ToString());

                rpt.Rows.Add(r);

                total = total + (decimal)rs.Rows[i]["NilaiTagihan"];
                t2 = t2 + Pengajuan;
                t3 = t3 + Pel;


                if (i==rs.Rows.Count-1)
					SubTotal("GRAND TOTAL", total, t2,t3);
			}
		}

		private string DetilLunas(string NoKontrak, int NoTagihan)
		{
			System.Text.StringBuilder x = new System.Text.StringBuilder();

			DataTable rs = Db.Rs("SELECT CaraBayar, TglPelunasan, Ket FROM ISC064_MARKETINGJUAL..MS_PELUNASAN"
				+ " WHERE NoKontrak = '"+NoKontrak+"' AND NoTagihan = " + NoTagihan
				);
			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(x.Length!=0) x.Append("<br>");
				x.Append(rs.Rows[i]["CaraBayar"] + ", " + Cf.Day(rs.Rows[i]["TglPelunasan"]) + " "
					+ rs.Rows[i]["Ket"]);
			}

			return x.ToString();
		}

		private void SubTotal(string txt, decimal total, decimal t2, decimal t3)
		{
			TableRow r = new TableRow();
			TableCell c;

			c = Rpt.Foot();
			c.Text = txt;
			c.ColumnSpan = 8;
			c.HorizontalAlign = HorizontalAlign.Left;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(total);
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(t2);
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t3);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
			c.Text = Cf.Num(total-t3);
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

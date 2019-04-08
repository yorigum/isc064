using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.KPA.Laporan
{
	public partial class Wawancara : System.Web.UI.Page
	{
        //private string NoCustomer { get { return (Request.QueryString["NoCustomer"]); } }
        private string Project { get { return (Request.QueryString["project"]); } }
        private string Perusahaan { get { return Request.QueryString["pers"]; } }
        private string Tanggal2 { get { return (Request.QueryString["tglwawancara"]); } }
        private string StatusWawancara { get { return (Request.QueryString["statuswawancara"]); } }
        private string Lokasi { get { return (Request.QueryString["lokasi"]); } }
        private string Rekening { get { return (Request.QueryString["rek"]); } }
        //private string StatusB { get { return (Request.QueryString["tbBerkas"]); } }
        //private string StatusK { get { return (Request.QueryString["tbKontrak"]); } }
        private string AttachmentID { get { return (Request.QueryString["id"]); } }

        protected void Page_Load(object sender, System.EventArgs e)
		{
            Report();
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

            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");

            Rpt.SubJudul(x
                , "Lokasi : " + Lokasi
                );

            Rpt.SubJudul(x
              , Tanggal2 + " : " + Cf.Day(Dari) + " s/d " + Cf.Day(Sampai)
              );
            Rpt.SubJudul(
                x, "Status Wawancara : " + StatusWawancara
                );
            if (Rekening == "SEMUA")
                Rpt.SubJudul(x, "Rekening Bank: SEMUA");
            else
                Rpt.SubJudul(x, "Rekening Bank: " + Rekening);

            Rpt.SubJudul(
                x, "Project : " + Project
                );

            string pers = (Perusahaan == "SEMUA") ? "SEMUA" : Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PERS WHERE Pers = '" + Perusahaan + "'");
            Rpt.SubJudul(
                x, "Perusahaan : " + pers
                );

            string legend = "";
            //Rpt.Header(rpt, x);
            Rpt.HeaderReport(headReport, legend, x);
		}

        private void Fill()
        {
            string nProject = "";
            if (Project != "SEMUA") nProject = " AND a.Project IN ('" + Project.Replace(",","','") + "')";
            string nPerusahaan = "";
            if (Perusahaan != "SEMUA") nPerusahaan = " AND a.Pers = '" + Perusahaan + "'";

            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");

            string tgl = "";
            if (Tanggal2 == "TargetWawancara")
                tgl = "TargetWawancara";

            if (Tanggal2 == "TglWawancara")
                tgl = "TglWawancara";

            string Status = "";
            if (StatusWawancara != "SEMUA")
            {
                if (StatusWawancara == "BELUM DITENTUKAN")
                    Status = " AND StatusWawancara = ''";
                else
                    Status = " AND StatusWawancara = '" + StatusWawancara + "'";
            }
            string Tanggal = "";
            Tanggal = " AND CONVERT(varchar," + tgl + ",112) >= '" + Cf.Tgl112(Dari) + "'"
                    + " AND CONVERT(varchar," + tgl + ",112) <= '" + Cf.Tgl112(Sampai) + "'";

            string nLokasi = "";
            if (Lokasi != "SEMUA")
            {
                nLokasi = " AND MS_UNIT.Lokasi = '" + Cf.Str(Lokasi) + "'";
            }

            string BankKPR = "";
            if (Rekening != "")
                BankKPR = " AND BankKPR = '" + Rekening + "'";

            string strSql = "SELECT a.*, b.Nama AS NamaCustomer"
                 + " FROM MS_KONTRAK a"
                 + " INNER JOIN MS_CUSTOMER b ON a.NoCustomer = b.NoCustomer"
                 + " WHERE a.Status = 'A'"
                 + nProject
                 + nPerusahaan
                 + nLokasi
                 + Tanggal
                 + Status
                 + BankKPR
                 + " AND CaraBayar = 'KPR' OR CaraBayar = 'KPA'"
                 ;
            DataTable rs = Db.Rs(strSql);

            decimal t = 0, PotensiKPR = 0;

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected)
                    break;

                TableRow r = new TableRow();
                TableCell c;

                c = new TableCell();
                c.Text = (i + 1).ToString();
                c.HorizontalAlign = HorizontalAlign.Center;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoKontrak"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NamaCustomer"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoUnit"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["BankKPR"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["StatusWawancara"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TargetWawancara"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglWawancara"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["LokasiWawancara"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                PotensiKPR = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiTagihan), 0) FROM MS_TAGIHAN WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' AND (Tipe = 'ANG' AND KPR = 1 OR NamaTagihan LIKE '%TAMBAHAN UANG MUKA%')");
                c.Text = Cf.Num(PotensiKPR);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                rpt.Rows.Add(r);

                t += PotensiKPR;

                if (i == (rs.Rows.Count - 1))
                    SubTotal(t);
            }
        }

        protected void SubTotal(decimal t)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = Rpt.Foot();
            c.Text = "<strong>GRAND TOTAL</strong>";
            c.ColumnSpan = 9;
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = "<strong>" + Cf.Num(t) + "</strong>";
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

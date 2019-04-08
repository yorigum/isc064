using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.KPA.Laporan
{
	public partial class CheckListBerkas : System.Web.UI.Page
	{
        //private string NoCustomer { get { return (Request.QueryString["NoCustomer"]); } }
        private string Project { get { return (Request.QueryString["project"]); } }
        private string Perusahaan { get { return Request.QueryString["pers"]; } }
        private string Tanggal2 { get { return (Request.QueryString["tanggalberkas"]); } }
        private string Tanggal { get { return (Request.QueryString["tanggal"]); } }
        private string StatusBerkas { get { return (Request.QueryString["statusberkas"]); } }
        private string Lokasi { get { return (Request.QueryString["lokasi"]); } }
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
                x, "Status Berkas : " + StatusBerkas
                );

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
            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");

            string tgl = "";
            if (Tanggal2 == "TglSelesaiBerkas")
                tgl = "TglSelesaiBerkas";

            if (Tanggal2 == "TglKontrak")
                tgl = "TglKontrak";

            string Status = "";
            if (StatusBerkas != "SEMUA")
            {
                if (StatusBerkas == "BELUM LENGKAP")
                    Status = " AND StatusBerkas = ''";
                else
                    Status = " AND StatusBerkas = '" + StatusBerkas + "'";
            }
            string Tanggal = "";
            Tanggal = " AND CONVERT(varchar," + tgl + ",112) >= '" + Cf.Tgl112(Dari) + "'"
                    + " AND CONVERT(varchar," + tgl + ",112) <= '" + Cf.Tgl112(Sampai) + "'";

            string nLokasi = "";
            if (Lokasi != "SEMUA")
            {
                nLokasi = " AND MS_UNIT.Lokasi = '" + Cf.Str(Lokasi) + "'";
            }

            string nProject = "";
            if (Project != "SEMUA") nProject = " AND a.Project IN ('" + Project.Replace(",","','") + "')";
            string nPerusahaan = "";
            if (Perusahaan != "SEMUA") nPerusahaan = " AND a.Pers = '" + Perusahaan + "'";

            string strSql = "SELECT a.*, b.Nama AS NamaCustomer"
                + " FROM MS_KONTRAK a"
                + " INNER JOIN MS_CUSTOMER b ON a.NoCustomer = b.NoCustomer"
                + " WHERE a.Status = 'A'"
                + " AND CaraBayar = 'KPR'"
                + nProject
                + nPerusahaan
                + nLokasi
                + Tanggal
                + Status
                ;
            DataTable rs = Db.Rs(strSql);

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected)
                    break;

                TableRow r = new TableRow();
                TableCell c;

                c = new TableCell();
                c.Text = (i + 1).ToString();
                c.VerticalAlign = VerticalAlign.Top;
                c.HorizontalAlign = HorizontalAlign.Center;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoKontrak"].ToString();
                c.VerticalAlign = VerticalAlign.Top;
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NamaCustomer"].ToString();
                c.VerticalAlign = VerticalAlign.Top;
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoUnit"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["BankKPR"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                string Berkas = "";
                if (Convert.ToBoolean(rs.Rows[i]["StatusBerkas"]) == false)
                    Berkas = "BELUM LENGKAP";
                else
                    Berkas = "SUDAH LENGKAP";
                c.Text = Berkas;
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglSelesaiBerkas"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["CheckListDokumen"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                rpt.Rows.Add(r);
            }
        }

        private void SubTotal(string txt, decimal t1, decimal t2)
		{
			TableRow r = new TableRow();
			TableCell c;

			c = Rpt.Foot();
			c.Text = txt;
			c.ColumnSpan = 6;
			c.HorizontalAlign = HorizontalAlign.Left;
			r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t2);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(t1);
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

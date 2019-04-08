using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.KPA.Laporan
{
	public partial class AKAD : System.Web.UI.Page
	{
        private string Tanggal2 { get { return (Request.QueryString["tgl"]); } }
        private string StatusS { get { return (Request.QueryString["status_s"]); } }
        private string StatusB { get { return (Request.QueryString["status_b"]); } }
        private string StatusA { get { return (Request.QueryString["status_a"]); } }
        private string S { get { return (Request.QueryString["S"]); } }
        private string B { get { return (Request.QueryString["B"]); } }
        private string D { get { return (Request.QueryString["D"]); } }
        private string Project { get { return (Request.QueryString["project"]); } }
        private string Perusahaan { get { return (Request.QueryString["pers"]); } }
        private string Lokasi { get { return (Request.QueryString["lokasi"]); } }
        private string Rekening { get { return (Request.QueryString["rek"]); } }
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
            if (Project != "SEMUA") nProject = " AND a.Project IN ('" + Project.Replace(",", "','") + "')";
            string nPerusahaan = "";
            if (Perusahaan != "SEMUA") nPerusahaan = " AND a.Pers = '" + Perusahaan + "'";

            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");

            string nStatus = "";
            if (StatusA != "") nStatus = " AND (a.FOBO1 = '1' OR a.FOBO2 = '1')";
            if (StatusB != "") nStatus = " AND (a.FOBO1 = '0' AND a.FOBO2 = '0')";

            string nAkad = "";
            string B = "";
            if (B != "") nAkad = " AND a.StatusAkad != 'SELESAI'";

            string D = "";
            if (D != "") nAkad = " AND a.StatusAkad = 'SELESAI'";

            string tgl = "";
            if (Tanggal2 == "TglAkad")
                tgl = "TglAkad";

            string Tanggal = "";
            if (D != "")
            {
                Tanggal = " AND CONVERT(varchar," + tgl + ",112) >= '" + Cf.Tgl112(Dari) + "'"
                        + " AND CONVERT(varchar," + tgl + ",112) <= '" + Cf.Tgl112(Sampai) + "'";
            }
            string nLokasi = "";
            if (Lokasi != "SEMUA")
            {
                nLokasi = " AND MS_UNIT.Lokasi = '" + Cf.Str(Lokasi) + "'";
            }

            string BankKPR = "";
            if (Rekening != "SEMUA")
                BankKPR = " AND BankKPR = '" + Rekening + "'";

            string strSql = "SELECT a.*, b.*"
                + " FROM MS_KONTRAK a"
                + " INNER JOIN MS_CUSTOMER b ON a.NoCustomer = b.NoCustomer"
                + " WHERE a.Status = 'A'"
                + " AND a.CaraBayar = 'KPR'"
                + nProject
                + nPerusahaan
                //+ " AND StatusAkad = 'SELESAI'"
                + nLokasi
                + Tanggal
                + BankKPR
                + nStatus
                + nAkad
                + "ORDER BY TglAkad"
                ;
            DataTable rs = Db.Rs(strSql);

            decimal t1 = 0, t2 = 0, t3 = 0, t4 = 0, t5 = 0, t6 = 0;
            decimal PotensiKPR = 0;
            decimal RealisasiAkad = 0;

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected)
                    break;

                TableRow r = new TableRow();
                TableCell c;

                c = new TableCell();
                c.Text = (i + 1).ToString();
                c.HorizontalAlign = HorizontalAlign.Center;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoAkad"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglAkad"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoKontrak"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Nama"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NPWP"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);


                c = new TableCell();
                c.Text = rs.Rows[i]["KTP1"].ToString()
                    + "</br>" + rs.Rows[i]["KTP2"].ToString()
                    + "</br>" + rs.Rows[i]["KTP3"].ToString()
                    + "</br>" + rs.Rows[i]["KTP4"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Db.SingleString("SELECT Nama FROM MS_AGENT WHERE NoAgent = '" + rs.Rows[i]["NoAgent"].ToString() + "' ");
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoUnit"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Math.Round(Convert.ToDecimal(rs.Rows[i]["Luas"]), 2));
                c.HorizontalAlign = HorizontalAlign.Right;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Jenis"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["BankKPR"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["Gross"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Math.Round(Convert.ToDecimal(rs.Rows[i]["DiskonRupiah"]), 0));
                c.HorizontalAlign = HorizontalAlign.Right;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["NilaiKontrak"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                PotensiKPR = Convert.ToDecimal(rs.Rows[i]["NilaiPengajuan"]);
                c.Text = Cf.Num(PotensiKPR);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                c = new TableCell();
                RealisasiAkad = Convert.ToDecimal(rs.Rows[i]["ApprovalKPR"]);
                c.Text = Cf.Num(RealisasiAkad);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                rpt.Rows.Add(r);

                t1 += Convert.ToDecimal(rs.Rows[i]["Gross"]);
                t2 += Convert.ToDecimal(rs.Rows[i]["DiskonRupiah"]);
                t3 += Convert.ToDecimal(rs.Rows[i]["NilaiKontrak"]);
                t4 += PotensiKPR;
                t5 += RealisasiAkad;
                t6 += Convert.ToDecimal(rs.Rows[i]["Luas"]);
            }
            SubTotal(t1, t2, t3, t4, t5, t6);
        }

    protected void SubTotal(decimal t1, decimal t2, decimal t3, decimal t4, decimal t5
        , decimal t6)
    {
        TableRow r = new TableRow();
        TableCell c;

        c = Rpt.Foot();
        c.Text = "<strong>GRAND TOTAL</strong>";
        c.ColumnSpan = 9;
        c.HorizontalAlign = HorizontalAlign.Left;
        r.Cells.Add(c);

        c = Rpt.Foot();
        c.Text = Cf.Num(Math.Round(t6, 2));
        c.HorizontalAlign = HorizontalAlign.Right;
        r.Cells.Add(c);

        c = Rpt.Foot();
        c.Text = "&nbsp;";
        c.ColumnSpan = 2;
        c.HorizontalAlign = HorizontalAlign.Right;
        r.Cells.Add(c);

        c = Rpt.Foot();
        c.Text = Cf.Num(t1);
        c.HorizontalAlign = HorizontalAlign.Right;
        r.Cells.Add(c);

        c = Rpt.Foot();
        c.Text = Cf.Num(Math.Round(t2, 0));
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
        c.Text = Cf.Num(t5);
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

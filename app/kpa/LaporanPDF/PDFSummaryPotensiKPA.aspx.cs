using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.KPA.Laporan
{
	public partial class SummaryPotensiKPA : System.Web.UI.Page
	{
        private string bulan { get { return (Request.QueryString["bln"]); } }
        private string tahun { get { return (Request.QueryString["thn"]); } }
        private string Project { get { return (Request.QueryString["project"]); } }
        private string Perusahaan { get { return (Request.QueryString["pers"]); } }
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

            Rpt.SubJudul(x, "Periode: " + Cf.NamaBln(bulan) + " " + tahun);

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

            int Bln = Convert.ToInt32(bulan);
            int Thn = Convert.ToInt32(tahun);

            string strSql = "SELECT DISTINCT BankKPR FROM MS_KONTRAK WHERE BankKPR <> ''";
            DataTable rs = Db.Rs(strSql);

            decimal t1 = 0, t2 = 0, t3 = 0, t4 = 0, t5 = 0, t6 = 0, t7 = 0, t8 = 0, t9 = 0, t10 = 0, t11 = 0, t12 = 0, t13 = 0, t14 = 0;

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected)
                    break;

                TableRow r = new TableRow();
                TableCell c;

                c = new TableCell();
                c.Text = (i + 1).ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["BankKPR"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                decimal ar = Hitung(rs.Rows[i]["BankKPR"].ToString(), Bln, Thn, "POTENSIKPR", "RP");
                c.Text = Cf.Num(ar);
                t1 += ar;
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                decimal au = Hitung(rs.Rows[i]["BankKPR"].ToString(), Bln, Thn, "POTENSIKPR", "UNIT");
                c.Text = Cf.Num(au);
                t2 += au;
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Hitung(rs.Rows[i]["BankKPR"].ToString(), Bln, Thn, "SP3K", "RP"));
                t3 += Convert.ToDecimal(c.Text);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Hitung(rs.Rows[i]["BankKPR"].ToString(), Bln, Thn, "SP3K", "UNIT"));
                t4 += Convert.ToDecimal(c.Text);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                decimal br = Hitung(rs.Rows[i]["BankKPR"].ToString(), Bln, Thn, "REALISASIAKAD", "RP");
                c.Text = Cf.Num(br);
                t5 += br;
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                decimal bu = Hitung(rs.Rows[i]["BankKPR"].ToString(), Bln, Thn, "REALISASIAKAD", "UNIT");
                c.Text = Cf.Num(bu);
                t6 += bu;
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Hitung(rs.Rows[i]["BankKPR"].ToString(), Bln, Thn, "SP3KBELUMAKAD", "RP"));
                t7 += Convert.ToDecimal(c.Text);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Hitung(rs.Rows[i]["BankKPR"].ToString(), Bln, Thn, "SP3KBELUMAKAD", "UNIT"));
                t8 += Convert.ToDecimal(c.Text);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                decimal cr = Hitung(rs.Rows[i]["BankKPR"].ToString(), Bln, Thn, "KPRTOLAK", "RP");
                c.Text = Cf.Num(cr);
                t9 += cr;
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                decimal cu = Hitung(rs.Rows[i]["BankKPR"].ToString(), Bln, Thn, "KPRTOLAK", "UNIT");
                c.Text = Cf.Num(cu);
                t10 += cu;
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                decimal dr = Hitung(rs.Rows[i]["BankKPR"].ToString(), Bln, Thn, "BATAL", "RP");
                c.Text = Cf.Num(dr);
                t11 += dr;
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                decimal du = Hitung(rs.Rows[i]["BankKPR"].ToString(), Bln, Thn, "BATAL", "UNIT");
                c.Text = Cf.Num(du);
                t12 += du;
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                decimal SisaRp = ar - br - cr - dr;
                c.Text = Cf.Num(SisaRp);
                t13 += SisaRp;
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                decimal SisaUnit = au - bu - cu - du;
                c.Text = Cf.Num(SisaUnit);
                t14 += SisaUnit;
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                rpt.Rows.Add(r);

                if (i == (rs.Rows.Count - 1))
                    SubTotal(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14);
            }
        }

        protected decimal Hitung(string BankKPR, int Bln, int Thn, string Tipe, string x)
        {
            string addq = "";
            string strSql = "";

            string nProject = "";
            if (Project != "SEMUA") nProject = " AND Project IN ('" + Project.Replace(",", "','") + "')";

            string nPerusahaan = "";
            if (Perusahaan != "SEMUA") nPerusahaan = " AND Pers = '" + Perusahaan + "'";

            if (Tipe == "SP3K")
            {
                addq += " AND StatusSP3K = 'SELESAI' AND HasilSP3K = 'SETUJU'";
            }

            if (Tipe == "REALISASIAKAD")
            {
                addq += " AND Status = 'A' AND StatusAkad = 'SELESAI'";
            }

            if (Tipe == "SP3KBELUMAKAD")
            {
                addq += " AND StatusSP3K = 'SELESAI' AND HasilSP3K = 'SETUJU' AND StatusAkad = 'DIJADWALKAN'";
            }

            if (Tipe == "KPRTOLAK")
            {
                addq += " AND Status = 'A' AND (KetWawancara LIKE '%TOLAK%' OR HasilOTS = 'TOLAK' OR HasilSP3K = 'TOLAK')";
            }

            if (Tipe == "BATAL")
            {
                addq += " AND Status = 'B'";
            }

            if (x == "RP")
            {
                if (Tipe == "POTENSIKPR")
                {
                    strSql = "SELECT ISNULL(SUM(NilaiPengajuan), 0)"
                        + " FROM MS_KONTRAK"
                        + " WHERE BankKPR = '" + BankKPR + "'"
                        + nProject
                        + nPerusahaan
                        + addq
                        ;
                }
                else
                {
                    strSql = "SELECT ISNULL(SUM(NilaiKontrak), 0)"
                        + " FROM MS_KONTRAK"
                        + " WHERE BankKPR = '" + BankKPR + "'"
                        + nProject
                        + nPerusahaan
                        + addq
                        ;
                }
            }
            else
            {
                strSql = "SELECT COUNT(*)"
                    + " FROM MS_KONTRAK"
                    + " WHERE BankKPR = '" + BankKPR + "'"
                    + nProject
                    + nPerusahaan
                    + addq
                    ;
            }

            decimal Nilai = Db.SingleDecimal(strSql);
            return Nilai;
        }

        protected void SubTotal(decimal t1, decimal t2, decimal t3, decimal t4, decimal t5, decimal t6, decimal t7, decimal t8, decimal t9, decimal t10, decimal t11, decimal t12, decimal t13, decimal t14)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = Rpt.Foot();
            c.Text = "<strong>TOTAL</strong>";
            c.ColumnSpan = 2;
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t1);
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
            c.Text = Cf.Num(t4);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t5);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t6);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t7);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t8);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t9);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t10);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t11);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t12);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t13);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t14);
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

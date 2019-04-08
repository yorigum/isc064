using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

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
        private string UserID { get { return (Request.QueryString["userid"]); } }
        private string Perusahaan { get { return (Request.QueryString["pers"]); } }
        private string Project { get { return (Request.QueryString["project"]); } }
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

			newHeader();
			Fill();
		}
        private void newHeader()
        {
            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");

            string header = "<h2>" + Mi.Pt + "</h2>";
            header += "<h1 class='title'>Laporan Pembatalan</h1>";
            header += "<h4>Project : " + Project + "</h4>";
            header += "Periode : " + Cf.Day(Dari) + " s/d " + Cf.Day(Sampai);
            header += "<br/> Laporan dihasilkan pada hari " + Cf.IndoWeek(DateTime.Today);
            header += ", " + Cf.Date(DateTime.Now) + " dari workstation " + Act.IP + " oleh user " + Act.UserID + "<br /><br />";
            headJudul.Text = header;
        }
        private void Header()
		{
			System.Text.StringBuilder x = new System.Text.StringBuilder();

			Rpt.Judul(x, comp, judul);
            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");

			Rpt.SubJudul(x
				, "Tanggal : " + Cf.Day(Dari) + " s/d " + Cf.Day(Sampai)
				);

            //Rpt.Header(rpt, x);
            string legend = "";
            Rpt.HeaderReport(headReport, legend, x);
		}

        private void Fill()
        {
            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");

            string nProject = "";
            if (Project != "SEMUA") nProject = " AND a.Project IN ('" + Project.Replace(",", "','") + "')";

            string nPerusahaan = "";
            if (Perusahaan != "SEMUA") nPerusahaan = " AND a.Pers = '" + Perusahaan + "'";

            string aa = "";
            if (UserAgent() > 0)
                aa = " AND a.NoAgent = " + UserAgent();

            decimal t1 = 0, t2 = 0, t3 = 0, t4 = 0, t5 = 0, t6 = 0, t7 = 0;

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
                + nPerusahaan
                + nProject
                + aa
                ;
            //Response.Write(strSql);
            DataTable rs = Db.Rs(strSql);

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected)
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

                if (i == rs.Rows.Count - 1)
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

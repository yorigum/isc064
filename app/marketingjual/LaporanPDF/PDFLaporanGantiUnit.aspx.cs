using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL.Laporan
{
	public partial class LaporanGantiNama : System.Web.UI.Page
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

			newHeader();
			Fill();
		}

        private void newHeader()
        {
            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");

            string header = "<h2>" + Mi.Pt + "</h2>";
            header += "<h1 class='title'>Laporan Pindah Unit</h1>";            
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

            if (Dari > Sampai)
            {
                DateTime x = Sampai;
                Sampai = Dari;
                Dari = x;
            }

            string nProject = "";
            if (Project != "SEMUA") nProject = " AND b.Project IN ('" + Project.Replace(",", "','") + "')";

            string nPerusahaan = "";
            if (Perusahaan != "SEMUA") nPerusahaan = " AND b.Pers = '" + Perusahaan + "'";

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
                + nProject
                + nPerusahaan
                + aa
                ;
            DataTable rs = Db.Rs(strSql);

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

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


                rpt.Rows.Add(r);
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

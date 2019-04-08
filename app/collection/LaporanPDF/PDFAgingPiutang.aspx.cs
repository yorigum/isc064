using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.COLLECTION.Laporan
{
	public partial class AgingPiutang: System.Web.UI.Page
	{
        private string Lokasi { get { return (Request.QueryString["lokasi"]); } }
        private string Sales { get { return (Request.QueryString["sales"]); } }
        private string KPAStatus { get { return (Request.QueryString["statuskpa"]); } }        
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

			
			
			System.Text.StringBuilder x = new System.Text.StringBuilder();
            Rpt.Judul(x, comp, judul);

			if(Lokasi != "SEMUA")
				x.Append("Lokasi: " + Lokasi + "<br />");

            if (Sales != "SEMUA")
            {
                string nama = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_AGENT WHERE NoAgent = '" + Sales + "'");
                x.Append("Sales: " + nama + "<br>");
            }
            else
                x.Append("Sales: SEMUA <br>");

            string namapers = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PERS WHERE Pers = '" + Perusahaan + "'");
            x.Append("Perusahaan : " + namapers + "<br />");
            x.Append("Project : " + Project + "<br />");
            string legend = "";
            Rpt.HeaderReport(headReport, legend, x);

			Fill();
		}

		private void Fill()
		{
			string strAdd = "";

			if(Lokasi != "SEMUA")
				strAdd += " AND a.Lokasi = '" + Lokasi.Replace("%"," ") + "'";

            if (Sales != "SEMUA")
                strAdd += " AND a.NoAgent = " + Cf.Pk(Sales);
            else
            {
                if (UserAgent() > 0)
                    strAdd += " AND a.NoAgent = " + UserAgent(); 
            }

            string nProject = "";
            if (Project != "SEMUA") nProject = " AND a.Project IN ('" + Project.Replace(",","','") + "')";
            string nPerusahaan = "";
            if (Perusahaan != "SEMUA") nPerusahaan = " AND a.Pers = '" + Perusahaan + "'";

			
			string strSql = "SELECT *, b.Nama AS NamaCustomer, c.Nama AS NamaAgent, c.Principal"
				+ " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK a "
				+ " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER b "
				+ " ON a.NoCustomer = b.NoCustomer "
				+ " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_AGENT c"
				+ " ON a.NoAgent = c.NoAgent "
				+ " WHERE 1=1 "
				+ " AND a.Status = 'A' "
                + nProject
                + nPerusahaan
				+ strAdd
                + " ORDER BY a.NoUnit"
				;
			DataTable rs = Db.Rs(strSql);

			decimal t1 = 0, t2 = 0, t3 = 0, t4 = 0, t5 = 0, t6 = 0;
			int index = 1;

			for(int i = 0; i < rs.Rows.Count; i++)
			{
				if(!Response.IsClientConnected) break;

				decimal st1 = 0, st2 = 0, st3 = 0, st4 = 0, st5 = 0;

				TableRow tr = new TableRow();
				TableCell c;

				c = new TableCell();
				c.Text = index.ToString();
				c.HorizontalAlign = HorizontalAlign.Center;
                c.Wrap = false;
				tr.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["NoKontrak"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                tr.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoUnit"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                tr.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["NamaCustomer"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                tr.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NamaAgent"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                tr.Cells.Add(c);

				c = new TableCell();
				decimal Total = TotalOutstanding(rs.Rows[i]["NoKontrak"].ToString());
				c.Text = Cf.Num(Total);
				c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                tr.Cells.Add(c);

				FillOutstanding(rs.Rows[i]["NoKontrak"].ToString(), ref t2, ref t3, ref t4, ref t5, ref t6,
					ref st1, ref st2, ref st3, ref st4, ref st5, ref index, tr);

				t1 += Total;

			}
			GrandTotal(t1, t2, t3, t4, t5, t6);
		}
		
		protected decimal TotalOutstanding(string NoKontrak)
		{
			decimal Nilai = 0;

            string KPR = "";
            if (KPAStatus == "kpa1")
            {
                KPR = " ";
            }
            else if (KPAStatus == "kpa2")
            {
                KPR = " AND KPR != '1' ";
            }

            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");

			DataTable rs = Db.Rs("SELECT "
				+ "NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan), 0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN"
				+ " WHERE NoKontrak = '" + NoKontrak + "' AND NoTagihan = a.NoUrut"
				+ ") AS Sisa"
				+ " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN a"
				+ " WHERE NoKontrak = '" + NoKontrak + "'"
				+ " AND DATEDIFF(DAY, TglJT, '" + Cf.Tgl112(Dari) + "') >= 1"
				+ " AND (SELECT ISNULL(SUM(NilaiPelunasan), 0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN"
				+ " WHERE NoKontrak = '" + NoKontrak + "' AND NoTagihan = a.NoUrut"
				+ ") < NilaiTagihan "
                + KPR
				);
			
			for(int i = 0; i < rs.Rows.Count; i++)
			{
				if(!Response.IsClientConnected) break;

				Nilai += Convert.ToDecimal(rs.Rows[i]["Sisa"]);
			}

			return Nilai;
		}

		protected void FillOutstanding(string NoKontrak, ref decimal t2, ref decimal t3, ref decimal t4, ref decimal t5, ref decimal t6, 
			ref decimal st1, ref decimal st2, ref decimal st3, ref decimal st4, ref decimal st5, ref int index, TableRow tr)
		{
            string KPR = "";
            if (KPAStatus == "kpa1")
            {
                KPR = " ";
            }
            else if (KPAStatus == "kpa2")
            {
                KPR = " AND KPR != '1' ";
            }

            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
			DataTable rs = Db.Rs("SELECT *"
				+ ", DATEDIFF(DAY, TglJT, '" + Cf.Tgl112(Dari) + "') AS Telat"
				+ ", NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan), 0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN"
				+ " WHERE NoKontrak = '" + NoKontrak + "' AND NoTagihan = a.NoUrut"
				+ ") AS Sisa"
				+ " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN a"
				+ " WHERE NoKontrak = '" + NoKontrak + "'"
				+ " AND DATEDIFF(DAY, TglJT, '" + Cf.Tgl112(Dari) + "') >= 1"
				+ " AND (SELECT ISNULL(SUM(NilaiPelunasan), 0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN"
				+ " WHERE NoKontrak = '" + NoKontrak + "' AND NoTagihan = a.NoUrut"
				+ ") < NilaiTagihan"
                + KPR
				+ " ORDER BY NoUrut"
				);

			for(int i = 0; i < rs.Rows.Count; i++)
			{
				if(!Response.IsClientConnected) break;

				int Telat = Convert.ToInt32(rs.Rows[i]["Telat"]);
				decimal Sisa = Convert.ToDecimal(rs.Rows[i]["Sisa"]);

				TableCell c;

				if(i > 0)
				{
					tr = new TableRow();
					c = new TableCell();
					c.ColumnSpan = 6;
					tr.Cells.Add(c);
				}

				c = new TableCell();
				c.Text = rs.Rows[i]["NoKontrak"] + "." + rs.Rows[i]["NoUrut"] + " " + rs.Rows[i]["NamaTagihan"].ToString();
                c.Wrap = false;
                tr.Cells.Add(c);

				if(Telat >= 0 && Telat <= 30)
				{
					t2 += Sisa;
					st1 += Sisa;

					c = new TableCell();
					c.Text = Cf.Num(Sisa);
					c.HorizontalAlign = HorizontalAlign.Right;
                    c.Wrap = false;
                    tr.Cells.Add(c);

					c = new TableCell();
					c.Text = Telat.ToString();
					c.HorizontalAlign = HorizontalAlign.Center;
                    c.Wrap = false;
                    tr.Cells.Add(c);
				}
				else
				{
					c = new TableCell();
					c.ColumnSpan = 2;
					tr.Cells.Add(c);
				}

				if(Telat >= 31 && Telat <= 60)
				{
					t3 += Sisa;
					st2 += Sisa;

					c = new TableCell();
					c.Text = Cf.Num(Sisa);
					c.HorizontalAlign = HorizontalAlign.Right;
                    c.Wrap = false;
                    tr.Cells.Add(c);

					c = new TableCell();
					c.Text = Telat.ToString();
					c.HorizontalAlign = HorizontalAlign.Center;
                    c.Wrap = false;
                    tr.Cells.Add(c);
				}
				else
				{
					c = new TableCell();
					c.ColumnSpan = 2;
					tr.Cells.Add(c);
				}

				if(Telat >= 61 && Telat <= 90)
				{
					t4 += Sisa;
					st3 += Sisa;

					c = new TableCell();
					c.Text = Cf.Num(Sisa);
					c.HorizontalAlign = HorizontalAlign.Right;
                    c.Wrap = false;
                    tr.Cells.Add(c);

					c = new TableCell();
					c.Text = Telat.ToString();
					c.HorizontalAlign = HorizontalAlign.Center;
                    c.Wrap = false;
                    tr.Cells.Add(c);
				}
				else
				{
					c = new TableCell();
					c.ColumnSpan = 2;
					tr.Cells.Add(c);
				}

				if(Telat > 90)
				{
					t5 += Sisa;
					st4 += Sisa;

					c = new TableCell();
					c.Text = Cf.Num(Sisa);
					c.HorizontalAlign = HorizontalAlign.Right;
                    c.Wrap = false;
                    tr.Cells.Add(c);

					c = new TableCell();
					c.Text = Telat.ToString();
					c.HorizontalAlign = HorizontalAlign.Center;
                    c.Wrap = false;
                    tr.Cells.Add(c);
				}
				else
				{
					c = new TableCell();
					c.ColumnSpan = 2;
					tr.Cells.Add(c);
				}
    
                //decimal den = Db.SingleDecimal("SELECT Denda FROM ISC064_MARKETINGJUAL..MS_TAGIHAN WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' AND NoUrut = '" + rs.Rows[i]["NoUrut"] + "' ORDER BY NoUrut");

                //c = new TableCell();
                //c.Text = Cf.Num(den);
                //c.HorizontalAlign = HorizontalAlign.Right;
                //tr.Cells.Add(c);

                if (i == 0)
                {

                    string InfoTerakhir = Db.SingleString("SELECT Ket FROM ISC064_MARKETINGJUAL..MS_FOLLOWUP WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' "
                                + " AND NoTagihan = '" + rs.Rows[i]["NoUrut"].ToString() + "' AND NoFU = (SELECT TOP 1 NoFU FROM ISC064_MARKETINGJUAL..MS_FOLLOWUP WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' AND NoTagihan = '" + rs.Rows[i]["NoUrut"].ToString() + "' ORDER BY NoFU DESC)");

                    string Ket = Db.SingleString("SELECT NamaGrouping FROM ISC064_MARKETINGJUAL..MS_FOLLOWUP WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' "
                                + " AND NoTagihan = '" + rs.Rows[i]["NoUrut"].ToString() + "' AND NoFU = (SELECT TOP 1 NoFU FROM ISC064_MARKETINGJUAL..MS_FOLLOWUP WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' AND NoTagihan = '" + rs.Rows[i]["NoUrut"].ToString() + "' ORDER BY NoFU DESC)");

                    c = new TableCell();
                    c.Text = Ket;
                    c.HorizontalAlign = HorizontalAlign.Left;
                    c.Wrap = false;
                    tr.Cells.Add(c);

                    c = new TableCell();
                    c.Text = InfoTerakhir;
                    c.HorizontalAlign = HorizontalAlign.Left;
                    c.Wrap = false;
                    tr.Cells.Add(c);
                }

				rpt.Rows.Add(tr);

                //t6 += Convert.ToDecimal(den);
                //st5 += Convert.ToDecimal(den);
			}

			if(rs.Rows.Count > 0)
			{
				index++;
				SubTotal(st1, st2, st3, st4, st5);
			}
		}

		protected void SubTotal(decimal st1, decimal st2, decimal st3, decimal st4, decimal st5)
		{
			TableRow r = new TableRow();
			TableCell c;

			c = Rpt.Foot();
			c.Text = "SUB TOTAL";
			c.ColumnSpan = 7;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(st1);
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = "&nbsp;";
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(st2);
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = "&nbsp;";
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(st3);
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = "&nbsp;";
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(st4);
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = "&nbsp;";
			r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = "&nbsp;";
            c.ColumnSpan = 2;
            r.Cells.Add(c);
            
			rpt.Rows.Add(r);
		}

		protected void GrandTotal(decimal t1, decimal t2, decimal t3, decimal t4, decimal t5, decimal t6)
		{
			TableRow r = new TableRow();
			TableCell c;

			c = Rpt.Foot();
			c.Text = "GRAND TOTAL";
			c.ColumnSpan = 5;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(t1);
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = "&nbsp;";
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(t2);
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = "&nbsp;";
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(t3);
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = "&nbsp;";
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(t4);
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = "&nbsp;";
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(t5);
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = "&nbsp;";
			r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = "&nbsp;";
            c.ColumnSpan = 2;
            r.Cells.Add(c);

            //c = Rpt.Foot();
            //c.Text = Cf.Num(String.Format("{0:0.00}",t6));
            //c.HorizontalAlign = HorizontalAlign.Right;
            //r.Cells.Add(c);

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

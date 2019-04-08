using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.COLLECTION.Laporan
{
	public partial class LapTunggakan : System.Web.UI.Page
	{

        private string UserID { get { return (Request.QueryString["userid"]); } }
        private string CaraBayar { get { return (Request.QueryString["carabayar"]); } }
        private string Lokasi { get { return (Request.QueryString["lokasi"]); } }
        private string StatusA { get { return (Request.QueryString["status_a"]); } }
        private string StatusS { get { return (Request.QueryString["status_s"]); } }
        private string StatusB { get { return (Request.QueryString["status_b"]); } }
        private string AttachmentID { get { return (Request.QueryString["id"]); } }
        private string Perusahaan { get { return (Request.QueryString["pers"]); } }
        private string Project { get { return (Request.QueryString["project"]); } }
        private string KPAStatus { get { return (Request.QueryString["statuskpa"]); } }
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
				Rpt.SubJudul(x, "Status  <b style='padding-left:35px'>:</b> " + StatusA);
			else if(StatusB != "")
				Rpt.SubJudul(x, "Status  <b style='padding-left:35px'>:</b> " + StatusB);
			else
				Rpt.SubJudul(x, "Status  <b style='padding-left:35px'>:</b> " + StatusS);

            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");

			Rpt.SubJudul(x
			    , "Tanggal  <b style='padding-left:25px'>:</b> " + Cf.Day(Dari)
				);
            Rpt.SubJudul(x
                , "Lokasi <b style='padding-left:35px'>:</b> " + Lokasi
                );
            Rpt.SubJudul(x
                , "Perusahaan : " + Perusahaan
                );
            Rpt.SubJudul(x
                , "Project <b style='padding-left:30px'>:</b> " + Project
                );

            string legend = "<br />Status: A = Aktif / S = Settled / U = Upgraded.";
            //Rpt.Header(rpt, x);
            Rpt.HeaderReport(headReport, legend, x);
		}

		private void Fill()
		{
			string Status = "";
			if(StatusA != "") Status = " AND a.Status = 'A'";
			if(StatusB != "") Status = " AND a.Status = 'B'";

            string KPR = "";
            if (KPAStatus == "kpa1")
            {
                KPR = " ";
            }
            else if (KPAStatus == "kpa2")
            {
                KPR = " AND b.KPR != '1' ";
            }

            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");

            string nProject = "";
            if (Project != "SEMUA") nProject = " AND a.Project IN ('" + Project.Replace(",","','") + "')";
            string nPerusahaan = "";
            if (Perusahaan != "SEMUA") nPerusahaan = " AND a.Pers = '" + Perusahaan + "'";

            string nLokasi = "";
            if (Lokasi != "SEMUA")
            {
                nLokasi = " AND a.Lokasi = '" + Cf.Str(Lokasi) + "'";
            }

            string aa = "";
            if (UserAgent() > 0)
                aa = " AND a.NoAgent = " + UserAgent();

            //Cara Bayar
            string akt = String.Empty;
            akt = CaraBayar.Replace("-", ",").TrimEnd(',');
            akt = akt.Replace("+", " ");
            akt = akt.Replace(",", "','");
            akt = "'" + akt + "'";

			string strSql = "SELECT"
				+ " a.NoKontrak"
				+ ",NamaTagihan"
				+ ",TglJT"
				+ ",NilaiTagihan"
				+ ",NoUrut"
				+ ",datediff(day,convert(datetime,TglJT,112),'" + Cf.Tgl112(Dari) + "') as telat"
				+ ", a.NoCustomer"
				+ ", a.NoUnit"
				+ " FROM ISC064_MARKETINGJUAL..MS_KONTRAK a INNER JOIN ISC064_MARKETINGJUAL..MS_TAGIHAN b"
				+ "		ON a.NoKontrak = b.NoKontrak"
				+ " WHERE 1=1"
				+ " AND ((SELECT ISNULL(SUM(NilaiPelunasan),0) as pelunasan FROM ISC064_MARKETINGJUAL..MS_PELUNASAN"
				+ " WHERE NoKontrak = a.NoKontrak AND NoTagihan = b.NoUrut) < NilaiTagihan)"
				+ " AND TglJT < '" + Dari + "' "
                + " AND a.CaraBayar IN(" + akt + ")"
                + nProject
                + nPerusahaan
                + nLokasi
                + KPR
				+ Status
                + aa
				+ " ORDER BY a.NoKontrak ASC";

			decimal a1 = 0;
			decimal a2 = 0;
			decimal a3 = 0;
			
			DataTable rs = Db.Rs(strSql);
			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				TableRow r = new TableRow();
				TableCell c;

				r.VerticalAlign = VerticalAlign.Top;
				
				c = new TableCell();
				c.Text = (i+1).ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["NoKontrak"].ToString()+ "." +rs.Rows[i]["NoUrut"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

				c = new TableCell();
				c.Text = Db.SingleString("SELECT Nama FROM ISC064_MARKETINGJUAL..MS_CUSTOMER WHERE NoCustomer = " + rs.Rows[i]["NoCustomer"]);
				c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);
				
				c = new TableCell();
				c.Text = Db.SingleString("SELECT NoTelp FROM ISC064_MARKETINGJUAL..MS_CUSTOMER WHERE NoCustomer = " + rs.Rows[i]["NoCustomer"]);
				c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);
				
				c = new TableCell();
				c.Text = Db.SingleString("SELECT NoHP FROM ISC064_MARKETINGJUAL..MS_CUSTOMER WHERE NoCustomer = " + rs.Rows[i]["NoCustomer"]);
				c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["NoUnit"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["NamaTagihan"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Day(rs.Rows[i]["TglJT"]);
				c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["telat"].ToString();
				c.HorizontalAlign = HorizontalAlign.Center;
                c.Wrap = false;
                r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Num(rs.Rows[i]["NilaiTagihan"].ToString());
				a1 = a1 + Convert.ToDecimal(c.Text);
				c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Num(Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan),0) FROM ISC064_MARKETINGJUAL..MS_PELUNASAN WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] +"' AND NoTagihan = '" + rs.Rows[i]["NoUrut"].ToString() + "'"));
				a2 = a2 + Convert.ToDecimal(c.Text);
				c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

				decimal sisa = 0;
				decimal n1 = (decimal)rs.Rows[i]["NilaiTagihan"];
				decimal n2 = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan),0) FROM ISC064_MARKETINGJUAL..MS_PELUNASAN WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] +"' AND NoTagihan = '" + rs.Rows[i]["NoUrut"].ToString() + "'");
				string txt = "";
				sisa = n1 - n2 ;
				
				c = new TableCell();
				c.Text = Cf.Num(sisa);
				a3 = a3 + Convert.ToDecimal(c.Text);
				c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

				rpt.Rows.Add(r);
				if (i == rs.Rows.Count-1)	
				{
					SubTotal(txt, a1, a2, a3);
				}
			}
		}
		private void SubTotal(string txt, decimal a1, decimal a2, decimal a3)
		{
			TableRow r = new TableRow();
			TableCell c;

			c = Rpt.Foot();
			c.Text = txt;
			c.ColumnSpan = 9;
			c.Text = "<b>GRAND TOTAL</b>";
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(a1);
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(a2);
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(a3);
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

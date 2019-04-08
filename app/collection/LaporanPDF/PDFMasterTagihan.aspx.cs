using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.COLLECTION.Laporan
{
	public partial class MasterTagihan : System.Web.UI.Page
	{
        private string UserID { get { return (Request.QueryString["userid"]); } }
        private string Tipe { get { return (Request.QueryString["tipe"]); } }
        private string StatusA { get { return (Request.QueryString["status_a"]); } }
        private string StatusS { get { return (Request.QueryString["status_s"]); } }
        private string StatusB { get { return (Request.QueryString["status_b"]); } }
        private string JenisTanggal { get { return (Request.QueryString["jenistgl"]); } }
        private string AttachmentID { get { return (Request.QueryString["id"]); } }
        private string Project { get { return (Request.QueryString["project"]); } }
        private string Perusahaan { get { return (Request.QueryString["pers"]); } }
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
				Rpt.SubJudul(x, "Status : " + StatusA);
			else if(StatusB != "")
				Rpt.SubJudul(x, "Status : " + StatusB);
			else
				Rpt.SubJudul(x, "Status : " + StatusS);


            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");

			Rpt.SubJudul(x
				, JenisTanggal + " : " + Cf.Day(Dari) + " s/d " + Cf.Day(Sampai)
				);

			Rpt.SubJudul(x
				, "Tipe : " + Tipe.Replace("'","")
				);

            Rpt.SubJudul(x
                , "Perusahaan : " + Perusahaan
                );

            Rpt.SubJudul(x
                , "Project : " + Project
                );

            //Rpt.Header(rpt, x);

            string legend = "<br />Status: A = Aktif / B = Batal.<br />"
                          + "Tipe : BF = Booking Fee / DP = Downpayment / ANG = Angsuran / ADM = Biaya Administrasi.<br />"
                          + "Cara Bayar: TN = Tunai / KD = Kartu Debit / KK = Kartu Kredit / TR = Transfer Bank / BG = Cek Giro / DN = Diskon.<br />"
                          + "** = Jatuh Tempo.";

            Rpt.HeaderReport(headReport, legend, x);
        }

		private void Fill()
		{
			string Status = "";
			if(StatusA != "") Status = " AND "+ Mi.DbPrefix +"MARKETINGJUAL..MS_KONTRAK.Status = 'A'";
			if(StatusB != "") Status = " AND ISC064_MARKETINGJUAL..MS_KONTRAK.Status = 'B'";

            string StatusKPA = "";
            if (KPAStatus == "includekpa") StatusKPA = "";
            if (KPAStatus == "excludekpa") StatusKPA = " AND ISC064_MARKETINGJUAL..MS_TAGIHAN.KPR = '0' ";

			string tgl = "";
			string order = "";
            if (JenisTanggal == "TglKontrak")
			{
				tgl = "ISC064_MARKETINGJUAL..MS_KONTRAK.TglKontrak";
				order = "ISC064_MARKETINGJUAL..MS_KONTRAK.NoKontrak, NoUrut";
			}
			if(JenisTanggal == "TglJt")
			{
                tgl = "ISC064_MARKETINGJUAL..MS_TAGIHAN.TglJT";
                order = "ISC064_MARKETINGJUAL..MS_TAGIHAN.TglJT, MS_KONTRAK.NoKontrak";
			}

            string nProject = "";
            if (Project != "SEMUA") nProject = " AND ISC064_MARKETINGJUAL..MS_KONTRAK.Project IN ('" + Project.Replace(",","','") + "')";

            string nPerusahaan = "";
            if (Perusahaan != "SEMUA") nPerusahaan = " AND ISC064_MARKETINGJUAL..MS_KONTRAK.Pers = '" + Perusahaan + "'";
            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");

            string aa = "";
            if (UserAgent() > 0)
                aa = " AND " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK.NoAgent = " + UserAgent();


            //Tipe
            //change parameter tipe
            string akt = String.Empty;
            akt = Tipe.Replace("-", ",").TrimEnd(',');
            akt = akt.Replace("%", " ");
            akt = akt.Replace(",", "','");
            akt = "'" + akt + "'";

			decimal total = 0;
			decimal t2 = 0;

            string strSql = "SELECT "
                + " ISC064_MARKETINGJUAL..MS_TAGIHAN.NoKontrak"
                + ",ISC064_MARKETINGJUAL..MS_TAGIHAN.NoUrut"
                + ",ISC064_MARKETINGJUAL..MS_TAGIHAN.Tipe"
                + ",ISC064_MARKETINGJUAL..MS_KONTRAK.TglKontrak"
                + ",ISC064_MARKETINGJUAL..MS_KONTRAK.NoUnit"
                + ",ISC064_MARKETINGJUAL..MS_CUSTOMER.Nama AS Cs"
                + ",ISC064_MARKETINGJUAL..MS_CUSTOMER.NoTelp AS NoTelp"
                + ",ISC064_MARKETINGJUAL..MS_CUSTOMER.NoHp AS NoHp"
                + ",ISC064_MARKETINGJUAL..MS_TAGIHAN.NamaTagihan"
                + ",ISC064_MARKETINGJUAL..MS_TAGIHAN.TglJT"
                + ",ISC064_MARKETINGJUAL..MS_TAGIHAN.NilaiTagihan"
                + ",ISC064_MARKETINGJUAL..MS_KONTRAK.Status"
                + ",DATEDIFF(day,GETDATE(),TglJT) AS Diff"
                + ",(SELECT ISNULL(SUM(NilaiPelunasan),0) FROM ISC064_MARKETINGJUAL..MS_PELUNASAN WHERE NoKontrak = ISC064_MARKETINGJUAL..MS_KONTRAK.NoKontrak AND NoTagihan = ISC064_MARKETINGJUAL..MS_TAGIHAN.NoUrut) AS TotalPelunasan"
                + " FROM ISC064_MARKETINGJUAL..MS_KONTRAK INNER JOIN ISC064_MARKETINGJUAL..MS_CUSTOMER ON ISC064_MARKETINGJUAL..MS_KONTRAK.NoCustomer = ISC064_MARKETINGJUAL..MS_CUSTOMER.NoCustomer"
                + " INNER JOIN ISC064_MARKETINGJUAL..MS_TAGIHAN ON ISC064_MARKETINGJUAL..MS_KONTRAK.NoKontrak = ISC064_MARKETINGJUAL..MS_TAGIHAN.NoKontrak"
                + " WHERE 1=1 "
                + " AND CONVERT(varchar," + tgl + ",112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND CONVERT(varchar," + tgl + ",112) <= '" + Cf.Tgl112(Sampai) + "'"
                + " AND ISC064_MARKETINGJUAL..MS_TAGIHAN.Tipe IN (" + akt + ")"
                + nProject
                + nPerusahaan
                + Status
                + StatusKPA
                + aa
				+ " ORDER BY " + order;

            
            DataTable rs = Db.Rs(strSql);
			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				TableRow r = new TableRow();
				TableCell c;

				r.VerticalAlign = VerticalAlign.Top;
				r.Attributes["ondblclick"] = "popJadwalTagihan('"+rs.Rows[i]["NoKontrak"]+"')";

				string jt = "";
				if(Convert.ToInt32(rs.Rows[i]["Diff"])<=0)
					jt = " **";

				c = new TableCell();
				c.Text = rs.Rows[i]["NoKontrak"]+ "." + rs.Rows[i]["NoUrut"]
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
                c.Text = rs.Rows[i]["NoTelp"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Font.Size = 10;
                c.Text = rs.Rows[i]["NoHp"].ToString();
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
                c.Text = Cf.Num(rs.Rows[i]["TotalPelunasan"]);
				c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

				c = new TableCell();
                c.Font.Size = 10;
                c.Text = Cf.Num((decimal)rs.Rows[i]["NilaiTagihan"]
					- (decimal)rs.Rows[i]["TotalPelunasan"]
					);
				c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

				rpt.Rows.Add(r);

				total = total + (decimal)rs.Rows[i]["NilaiTagihan"];
				t2 = t2 + (decimal)rs.Rows[i]["TotalPelunasan"];

				if(i==rs.Rows.Count-1)
					SubTotal("GRAND TOTAL", total, t2);
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

		private void SubTotal(string txt, decimal total, decimal t2)
		{
			TableRow r = new TableRow();
			TableCell c;

			c = Rpt.Foot();
			c.Text = txt;
			c.ColumnSpan = 10;
			c.HorizontalAlign = HorizontalAlign.Left;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(total);
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(t2);
			c.HorizontalAlign = HorizontalAlign.Left;
			r.Cells.Add(c);

			c = Rpt.Foot();
			c.Text = Cf.Num(total-t2);
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

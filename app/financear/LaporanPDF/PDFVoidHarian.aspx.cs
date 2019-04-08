using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.FINANCEAR.Laporan
{
	public partial class VoidHarian : System.Web.UI.Page
	{
        private string UserID { get { return (Request.QueryString["userid"]); } }
        private string AttachmentID { get { return (Request.QueryString["id"]); } }
        private string TanggalInput { get { return (Request.QueryString["tglinput"]); } }
        private string Kasir { get { return (Request.QueryString["kasir"]); } }
        private string Project { get { return (Request.QueryString["project"]); } }
        private string Perusahaan { get { return (Request.QueryString["pers"]); } }
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

            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");

			Rpt.SubJudul(x
				,"Tanggal Void : " + Cf.Day(Dari) + " s/d " + Cf.Day(Sampai)
				);

			Rpt.SubJudul(x
				, "Kasir : " + Kasir
				);

            Rpt.SubJudul(x
                , "Project : " + Project
                );

            string pers = (Perusahaan == "SEMUA") ? "SEMUA" : Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PERS WHERE Pers = '" + Perusahaan + "'");
            Rpt.SubJudul(x
                , "Perusahaan : " + pers
                );

            string legend = "";
            //Rpt.Header(rpt, x);
            Rpt.HeaderReport(headReport, legend, x);
		}

		private void Fill()
		{
            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");


			string UserID = "";
			if(Kasir != "SEMUA")
				UserID = " AND a.UserID = '" + Kasir + "'";

            string agent = "";
            if (UserAgent() > 0)
                agent = " AND (SELECT NoAgent FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = a.Ref) = " + UserAgent();

            string nProject = "";
            if (Project != "SEMUA") nProject = " AND c.Project IN ('" + Project.Replace(",","','") + "')";
            string nPerusahaan = "";
            if (Perusahaan != "SEMUA") nPerusahaan = " AND c.Pers = '" + Perusahaan + "'";

            decimal t = 0, t1 = 0, t2 = 0, t3 = 0;

			string strSql = "SELECT a.NoTTS AS NoTTS,a.NoTTS2,a.Project, b.Tgl AS Tgl, a.TglTTS AS TglTTS, a.UserID AS UserID, a.ManualTTS, "
				+ " a.IP as IP, a.Tipe as Tipe, a.Ref as Ref, a.Unit as Unit, a.Customer AS Customer, "
				+ " a.CaraBayar as CaraBayar, a.Ket as Ket, a.NoBG as NoBG, a.TglBG as TglBG, "
				+ " a.Titip as Titip, a.Tolak as Tolak, a.NoBKM as NoBKM, a.TglBKM as TglBKM, a.ManualBKM, "
				+ " a.NoSlip as NoSlip, a.LebihBayar, a.Total2 ,a.Acc as Acc, a.NilaiKembali as NilaiKembali, b.Ket as Keterangan"
				+ " FROM MS_TTS a"
				+ " INNER JOIN MS_TTS_LOG b ON a.NoTTS = CONVERT(INT, b.Pk)"
                + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK c ON a.Ref = c.NoKontrak"
                + " WHERE 1 = 1"
				+ " AND CONVERT(VARCHAR, b.Tgl, 112) >= '" + Cf.Tgl112(Dari) + "'"
				+ " AND CONVERT(VARCHAR, b.Tgl, 112) <= '" + Cf.Tgl112(Sampai) + "'"
				+ " AND b.Aktivitas = 'VOID'"
                + nProject
                + nPerusahaan
				+ UserID
                + agent
				+ " ORDER BY b.Tgl";

			DataTable rs = Db.Rs(strSql);

			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				TableRow r = new TableRow();
				TableCell c;

				r.VerticalAlign = VerticalAlign.Top;
				r.Attributes["ondblclick"] = "popEditTTS('"+rs.Rows[i]["NoTTS"]+"')";

				c = new TableCell();
                c.Text = rs.Rows[i]["NoTTS2"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Day(rs.Rows[i]["Tgl"]);
				c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Day(rs.Rows[i]["TglTTS"]);
				c.Wrap = false;
				c.HorizontalAlign = HorizontalAlign.Left;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["UserID"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Unit"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				c.Wrap = false;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Customer"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				c.Wrap = false;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["CaraBayar"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Ket"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				c.Wrap = false;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["NoBG"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
				c.Wrap = false;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Day(rs.Rows[i]["TglBG"]);
				c.HorizontalAlign = HorizontalAlign.Left;
				c.Wrap = false;
				r.Cells.Add(c);

				c = new TableCell();
                if ((int)rs.Rows[i]["NoBKM"] != 0)
                    c.Text = rs.Rows[i]["ManualBKM"].ToString().PadLeft(7, '0');
				c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Day(rs.Rows[i]["TglBKM"]);
				c.HorizontalAlign = HorizontalAlign.Left;
				c.Wrap = false;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Acc"].ToString();
				c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Db.SingleString("SELECT NAMA FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE PROJECT='" + rs.Rows[i]["Project"].ToString() + "'");
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                string sTotal = "";
                string[] sTemp = Cf.SplitByString(rs.Rows[i]["Keterangan"].ToString(),"<br>");

				for (int j = 0; j <sTemp.Length; j++)
				{
					if (sTemp[j].StartsWith("Total :"))
						sTotal = sTemp[j].ToString().Replace("Total :", "");
				}

				c = new TableCell();
				c.Text = Cf.Num(sTotal);
				c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["LebihBayar"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["Total2"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

				rpt.Rows.Add(r);


				

				t = t + (decimal)rs.Rows[i]["NilaiKembali"];
				t1 = t1 + (decimal)Convert.ToDecimal(sTotal);
                t2 = t2 + (decimal)Convert.ToDecimal(rs.Rows[i]["LebihBayar"]);
                t3 = t3 + (decimal)Convert.ToDecimal(rs.Rows[i]["Total2"]);



				if(i==rs.Rows.Count-1)
					SubTotal("GRAND TOTAL", t, t1, t2, t3);

			}
		}

		private void SubTotal(string txt, decimal t, decimal t1, decimal t2, decimal t3)
		{
			TableRow r = new TableRow();
			TableCell c;

			c = Rpt.Foot();
			c.Text = txt;
			c.ColumnSpan = 14;
			c.HorizontalAlign = HorizontalAlign.Left;
			r.Cells.Add(c);

            //c = Rpt.Foot();
            //c.Text = Cf.Num(t);
            //c.HorizontalAlign = HorizontalAlign.Right;
            //r.Cells.Add(c);

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

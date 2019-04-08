using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LAUNCHING.Laporan
{
	/// <summary>
	/// Summary description for LaporanSalesPerformance.
	/// </summary>
    public partial class SummaryNUP : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();

			if(!Page.IsPostBack)
			{
				comp.InnerHtml = Mi.Pt;
				rpt.Visible = false;
				Js.Focus(this,scr);
				if(!Act.Sec("DownloadExcel")) xls.Enabled = false;

                tglawal.Text = Cf.Day(DateTime.Now);
                tglakhir.Text = Cf.Day(DateTime.Now);
                fillAdmin();
                fillTipe();
			}
		}

        protected int UserAgent()
        {
            int a = Db.SingleInteger("SELECT NoAgent FROM " + Mi.DbPrefix + "SECURITY..USERNAME WHERE UserID = '" + Act.UserID + "'");

            return a;
        }

        private void fillAdmin()
        {
            //Sales
            DataTable rs = Db.Rs("SELECT * FROM AM132_SECURITY..USERNAME WHERE SecLevel='ADM SALES'");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["UserID"].ToString();
                string t = rs.Rows[i]["Nama"].ToString();

                admin.Items.Add(new ListItem(t, v));
            }
        }

        private void fillTipe()
        {
            //Sales
            DataTable rs = Db.Rs("SELECT * FROM AM132_MARKETINGJUAL..REF_TIPE ORDER BY Tipe DESC");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["Tipe"].ToString();
                string t = rs.Rows[i]["Tipe"].ToString();

                ntipe.Items.Add(new ListItem(t, v));
            }
        }

		private bool valid()
		{
			string s = "";
			bool x = true;

            if (!Cf.isTgl(tglawal) || !Cf.isTgl(tglakhir))
			{
				x = false;
				tglc.Text = "Format salah";
			}
			else
				tglc.Text = "";

            if (Cf.isEmpty(tglawal) || Cf.isEmpty(tglakhir))
            {
                x = false;
                tglc.Text = "Kosong";
            }
            else
                tglc.Text = "";

			if(!x && s!="")
			{
				RegisterStartupScript("err"
					,"<script language='javascript'>document.getElementById('"+s+"').select()</script>");
			}

			return x;
		}

		protected void scr_Click(object sender, System.EventArgs e)
		{
			if(valid())
			{
				Report();
			}
		}

		protected void xls_Click(object sender, System.EventArgs e)
		{
			if(valid())
			{
				Report();
				Rpt.ToExcel(this,rpt);
			}
		}

		private void Report()
		{
			param.Visible = false;
			rpt.Visible = true;

			lblHeader.Text = Mi.Pt
				+ "<br />"
				+ "Laporan NUP"
				;

			System.Text.StringBuilder x = new System.Text.StringBuilder();

            string addStr = "Semua Admin";
            string addStrTipe = "Semua Tipe";
            
            if (admin.SelectedIndex != 0)
                addStr = Db.SingleString("SELECT Nama FROM AM132_SECURITY..USERNAME WHERE UserID='" + admin.SelectedValue + "'");

            if (ntipe.SelectedIndex != 0)
                addStrTipe = Db.SingleString("SELECT Tipe FROM AM132_MARKETINGJUAL..REF_TIPE WHERE Tipe='" + ntipe.SelectedValue + "'");

            x.Append("<br />Untuk tanggal : " + Cf.Day(tglawal.Text) + " s/d " + Cf.Day(tglakhir.Text));
            x.Append("<br />Untuk Admin : " + Cf.Str(addStr));
            x.Append("<br />Untuk Tipe : " + Cf.Str(addStrTipe));

			x.Append("<br /><span style='font-weight: normal;'>Laporan dihasilkan pada hari : " + Cf.IndoWeek(DateTime.Today)
				+ ", " + Cf.Date(DateTime.Now)
				+ " dari workstation : " + Act.IP
				+ " dan username : " + Act.UserID
				+ "</span>"
				);
			
			lblSubHeader.Text = x.ToString();
			Fill();
		}

        private void Fill()
        {
            DateTime Tanggal1 = Convert.ToDateTime(tglawal.Text);
            DateTime Tanggal2 = Convert.ToDateTime(tglakhir.Text);

            string addSql = "";

            if (admin.SelectedIndex > 0)
                addSql += " AND UserInputID = '" + admin.SelectedValue + "'";

            if (ntipe.SelectedIndex > 0)
                addSql += " AND Tipe = '" + ntipe.SelectedValue + "'";

            string strSql = "SELECT * FROM MS_NUP WHERE 1=1"
                    + " AND CONVERT(DATETIME,TglDaftar,112) BETWEEN '" + Cf.Tgl112(Tanggal1) + "' AND '" + Cf.Tgl112(Tanggal2) + "'"
                    + addSql
                    + " ORDER BY Tipe DESC, NoNUP ASC";

            DataTable dtNUP = Db.Rs(strSql);
            Rpt.NoData(rpt, dtNUP, "Tidak ada data NUP yang terdaftar.");

            decimal Rm1 = 0, Rm2 = 0, Rm3 = 0, Rk = 0, Kv = 0, Kv2 = 0;

            int no = 0;
            for (int i = 0; i < dtNUP.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                r.VerticalAlign = VerticalAlign.Top;

                c = new TableCell();
                c.Text = (no+1).ToString();
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                //Tgl NUP
                c = new TableCell();
                c.Text = Cf.Day(dtNUP.Rows[i]["TglDaftar"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                //NoNUP
                c = new TableCell();
                string cetakn = dtNUP.Rows[i]["NoNUP"].ToString();

                if (Convert.ToInt32(dtNUP.Rows[i]["Revisi"].ToString()) > 0)
                    cetakn = cetakn + "R";
                c.Text = Cf.Str(cetakn);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                string namaCS = "";
                string noKTP = "";
                string alamatCS = "";
                string telpCS = "";
                string bank = "", nrek = "";
                DataTable dtCs = Db.Rs("SELECT * FROM MS_CUSTOMER WHERE NoCustomer=" + Convert.ToInt32(dtNUP.Rows[i]["NoCustomer"]));

                if (dtCs.Rows.Count > 0)
                {
                    namaCS = dtCs.Rows[0]["Nama"].ToString();
                    noKTP = dtCs.Rows[0]["NoKTP"].ToString();
                    telpCS = dtCs.Rows[0]["NoTelp"].ToString();
                    bank = dtCs.Rows[0]["RekBank"].ToString();
                    nrek = dtCs.Rows[0]["RekNo"].ToString();
                    alamatCS = dtCs.Rows[0]["KTP1"].ToString() + " " + dtCs.Rows[0]["KTP1"].ToString() + " " + dtCs.Rows[0]["KTP2"].ToString() + " " + dtCs.Rows[0]["KTP3"].ToString() + " " + dtCs.Rows[0]["KTP4"].ToString();
                }

                //Nama Customer
                c = new TableCell();
                c.Text = Cf.Str(namaCS);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                //Alamat Customer
                c = new TableCell();
                c.Text = Cf.Str(alamatCS);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                //Telpon Customer
                c = new TableCell();
                c.Text = Cf.Str(telpCS);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                string namaAG = "";
                string telpAG = "";
                DataTable dtAG = Db.Rs("SELECT * FROM MS_AGENT WHERE NoAgent=" + Convert.ToInt32(dtNUP.Rows[i]["NoAgent"]));

                if (dtAG.Rows.Count > 0)
                {
                    namaAG = dtAG.Rows[0]["Nama"].ToString();
                    telpAG = dtAG.Rows[0]["Kontak"].ToString();
                }

                //Agent
                c = new TableCell();
                c.Text = Cf.Str(namaAG);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                //telp Agent
                c = new TableCell();
                c.Text = Cf.Str(telpAG);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                //Unit
                c = new TableCell();
                string noUnit = Db.SingleString("SELECT ISNULL(NoUnit,' ') FROM MS_PRIORITY WHERE NoNUP='" + dtNUP.Rows[i]["NoNUP"].ToString() + "'");
                c.Text = noUnit;
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                //Tipe
                c = new TableCell();
                c.Text = Cf.Str(dtNUP.Rows[i]["Tipe"].ToString());
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);
                
                //bank
                c = new TableCell();
                c.Text = bank + " " + nrek;
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                //penginput
                c = new TableCell();
                c.Text = dtNUP.Rows[i]["UserInputNama"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                //Nilai Pembayaran
                c = new TableCell();
                decimal nBayar = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiBayar),0) FROM MS_NUP_PELUNASAN WHERE NoNUP='" + dtNUP.Rows[i]["NoNUP"].ToString() + "'");
                c.Text = Cf.Num(nBayar);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                rpt.Rows.Add(r);

                no++;

                if (dtNUP.Rows[i]["Tipe"].ToString() == "RUMAH 23/60")
                    Rm1 += 1;
                else if (dtNUP.Rows[i]["Tipe"].ToString() == "RUMAH 36/72")
                    Rm2 += 1;
                else if (dtNUP.Rows[i]["Tipe"].ToString() == "RUMAH 45/105")
                    Rm3 += 1;
                else if (dtNUP.Rows[i]["Tipe"].ToString() == "RUKO")
                    Rk += 1;
                else if (dtNUP.Rows[i]["Tipe"].ToString() == "KAVLING")
                    Kv += 1;
                else if (dtNUP.Rows[i]["Tipe"].ToString() == "KAVLING KOMERSIAL")
                    Kv2 += 1;
            }

            Detail(Rm1, Rm2, Rm3, Rk, Kv, Kv2);
        }

        private void Detail(decimal Rm1, decimal Rm2, decimal Rm3, decimal Rk, decimal Kv, decimal Kv2)
        {
            TableRow r;
            TableCell c;

            r = new TableRow();

            c = new TableCell();
            c.ColumnSpan = 15;
            c.Text = "<br /><br /><br /><strong>RUMAH 26/32: </strong>" + Cf.Num(Rm1)
                + "<br />"
                + "<strong>RUMAH 36/72: </strong>" + Cf.Num(Rm2)
                + "<br />"
                + "<strong>RUMAH 45/105: </strong>" + Cf.Num(Rm3)
                + "<br />"
                + "<strong>RUKO: </strong>" + Cf.Num(Rk)
                + "<br />"
                + "<strong>KAVLING: </strong>" + Cf.Num(Kv)
                + "<br />"
                + "<strong>KAVLING KOMERSIL: </strong>" + Cf.Num(Kv2)
                ;
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

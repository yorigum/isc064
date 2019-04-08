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

namespace ISC064.NUP.Laporan
{
    /// <summary>
    /// Summary description for LaporanSalesPerformance.
    /// </summary>
    public partial class NUPBelumBayar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();

            if (!Page.IsPostBack)
            {
                comp.InnerHtml = Mi.Pt;
                rpt.Visible = false;
                Js.Focus(this, scr);
                if (!Act.Sec("DownloadExcel")) xls.Enabled = false;

                tgl.Text = Cf.Day(DateTime.Now);
                fillAdmin();
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
            DataTable rs = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "SECURITY..USERNAME WHERE SecLevel='ADM SALES'");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["UserID"].ToString();
                string t = rs.Rows[i]["Nama"].ToString();

                admin.Items.Add(new ListItem(t, v));
            }
        }

        private bool valid()
        {
            string s = "";
            bool x = true;

            if (!Cf.isTgl(tgl))
            {
                x = false;
                if (s == "") s = tgl.ID;
                tglc.Text = "Format salah";
            }
            else
                tglc.Text = "";

            if (!x && s != "")
            {
                RegisterStartupScript("err"
                    , "<script language='javascript'>document.getElementById('" + s + "').select()</script>");
            }

            return x;
        }

        protected void scr_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                Report();
            }
        }

        protected void xls_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                Report();
                Rpt.ToExcel(this, rpt);
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
            if (admin.SelectedIndex != 0)
                addStr = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..USERNAME WHERE UserID='" + admin.SelectedValue + "'");

            x.Append("<br />As of : " + Cf.Day(tgl.Text));
            x.Append("<br />Untuk Admin : " + Cf.Str(addStr));

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
            DateTime Tanggal = Convert.ToDateTime(tgl.Text);

            string addSql = "";

            if (admin.SelectedIndex > 0)
                addSql = " AND UserInputID = '" + admin.SelectedValue + "'";

            string strSql = "SELECT * FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_NUP WHERE 1=1"
                    + " AND NoNUP NOT IN (SELECT DISTINCT(NoNUP) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_NUP_PELUNASAN)"
                    + " AND CONVERT(DATETIME,TglDaftar,112) <= '" + Cf.Tgl112(Tanggal) + "' "
                    + addSql;

            DataTable dtNUP = Db.Rs(strSql);
            Rpt.NoData(rpt, dtNUP, "Tidak ada data NUP yang terdaftar.");

            int no = 0;
            for (int i = 0; i < dtNUP.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                r.VerticalAlign = VerticalAlign.Top;

                c = new TableCell();
                c.Text = (no + 1).ToString();
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
                DataTable dtCs = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER WHERE NoCustomer=" + Convert.ToInt32(dtNUP.Rows[i]["NoCustomer"]));

                if (dtCs.Rows.Count > 0)
                {
                    namaCS = dtCs.Rows[0]["Nama"].ToString();
                    noKTP = dtCs.Rows[0]["NoKTP"].ToString();
                    telpCS = dtCs.Rows[0]["NoTelp"].ToString();
                    bank = dtCs.Rows[0]["RekBank"].ToString();
                    nrek = dtCs.Rows[0]["RekNo"].ToString();
                    alamatCS = dtCs.Rows[0]["KTP1"].ToString() + " " + dtCs.Rows[0]["KTP1"].ToString() + " " + dtCs.Rows[0]["KTP2"].ToString() + " " + dtCs.Rows[0]["KTP3"].ToString() + " " + dtCs.Rows[0]["KTP4"].ToString();
                }

                //Unit Customer
                string unit = "";
                DataTable rskontrak = Db.Rs("SELECT a.NoUnit FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK a INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_NUP b ON a.NUP=b.NoNUP WHERE b.NoNUP='" + dtNUP.Rows[i]["NoNUP"].ToString() + "'");
                if (rskontrak.Rows.Count > 0)
                {
                    unit = rskontrak.Rows[0]["NoUnit"].ToString();
                }
                c = new TableCell();
                c.Text = Cf.Str(unit);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

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
                DataTable dtAG = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_AGENT WHERE NoAgent=" + Convert.ToInt32(dtNUP.Rows[i]["NoAgent"]));

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

                //Tipe
                c = new TableCell();
                c.Text = Cf.Str(dtNUP.Rows[i]["Tipe"].ToString());
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                //telp Agent
                c = new TableCell();
                c.Text = bank + " " + nrek;
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                rpt.Rows.Add(r);

                no++;
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

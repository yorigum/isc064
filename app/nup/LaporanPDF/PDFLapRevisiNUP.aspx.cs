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
using System.Diagnostics;

namespace ISC064.NUP.Laporan
{
    /// <summary>
    /// Summary description for LaporanSalesPerformance.
    /// </summary>
    public partial class LapRevisiNUP : System.Web.UI.Page
    {
        private string UserID { get { return (Request.QueryString["userid"]); } }
        private string Project { get { return (Request.QueryString["project"]); } }
        private string AttachmentID { get { return (Request.QueryString["id"]); } }
        private string TanggalDari { get { return Cf.Day(Db.SingleTime("SELECT FilterDari FROM " + Mi.DbPrefix + "MARKETINGJUAL..LapPDF WHERE AttachmentID = '" + AttachmentID + "'")); } }
        private string TanggalSampai { get { return Cf.Day(Db.SingleTime("SELECT FilterSampai FROM " + Mi.DbPrefix + "MARKETINGJUAL..LapPDF WHERE AttachmentID = '" + AttachmentID + "'")); } }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Report();
        }

        protected void Report()
        {
            lblHeader.Text = Mi.Pt
                    + "<br />"
                    + "Laporan Revisi NUP"
                    ;

            System.Text.StringBuilder x = new System.Text.StringBuilder();            
            x.Append("<br />Untuk Project : " + Cf.Str(Project));

            x.Append("<br /><span style='font-weight: normal;'>Laporan dihasilkan pada hari : " + Cf.IndoWeek(DateTime.Today)
                + ", " + Cf.Date(DateTime.Now)
                + " dari workstation : " + Act.IP
                + " dan username : " + Act.UserID
                + "</span>"
                );

            lblSubHeader.Text = x.ToString();

            Fill();
        }

        protected int UserAgent()
        {
            int a = Db.SingleInteger("SELECT NoAgent FROM " + Mi.DbPrefix + "SECURITY..USERNAME WHERE UserID = '" + Act.UserID + "'");

            return a;
        }


        private void Fill()
        {
            DateTime Tanggal1 = Db.SingleTime("SELECT FilterDari FROM " + Mi.DbPrefix + "MARKETINGJUAL..LapPDF WHERE AttachmentID = '" + AttachmentID + "'");
            DateTime Tanggal2 = Db.SingleTime("SELECT FilterSampai FROM " + Mi.DbPrefix + "MARKETINGJUAL..LapPDF WHERE AttachmentID = '" + AttachmentID + "'");

            string nProject = "";
            if (Project != "SEMUA") nProject = " AND Project IN ('" + Project.Replace(",", "','") + "')";

            string strSql = "SELECT * FROM MS_NUP WHERE 1=1"
                    + " AND CONVERT(varchar,TglRevisi,112) BETWEEN '" + Cf.Tgl112(Tanggal1) + "' AND '" + Cf.Tgl112(Tanggal2) + "'"
                    + nProject
                    + " AND Revisi != 0"
                    + " ORDER BY NoNUP ASC";

            DataTable rs = Db.Rs(strSql);
            decimal Rm1 = 0, Rm2 = 0, Rm3 = 0, Rk = 0, Kv = 0, Kv2 = 0;

            int index = 1;
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                r.VerticalAlign = VerticalAlign.Top;

                c = new TableCell();
                c.Text = (index + i).ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglDaftar"].ToString());
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoNUP"].ToString().PadLeft(4, '0');
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglRevisi"]); ;
                c.Wrap = false;
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NamaBfr"].ToString();
                c.Wrap = false;
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Db.SingleString("SELECT Nama FROM MS_CUSTOMER WHERE NoCUSTOMER = " + rs.Rows[i]["NoCUstomer"]);
                c.Wrap = false;
                c.HorizontalAlign = HorizontalAlign.Left;
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

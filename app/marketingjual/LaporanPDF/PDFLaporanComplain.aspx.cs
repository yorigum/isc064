using System;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using System.Drawing;
using System.Diagnostics;

namespace ISC064.MARKETINGJUAL.Laporan
{
    public partial class LaporanComplain : System.Web.UI.Page
    {

        private string UserID { get { return (Request.QueryString["userid"]); } }
        private string Cust { get { return (Request.QueryString["customer"]); } }
        private string List { get { return (Request.QueryString["complain"]); } }
        private string Status { get { return (Request.QueryString["status"]); } }
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
            rpt.Visible = true;

            Header();
            Fill();
        }

        private void Header()
        {
            System.Text.StringBuilder x = new System.Text.StringBuilder();

            //Rpt.Judul(x, comp, judul);

            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");

            Rpt.SubJudul(x
                , "Tgl Complain : " + Cf.Day(Dari) + " s/d " + Cf.Day(Sampai)
                );
            Rpt.SubJudul(x
                , "Project : " + Project
                );
            Rpt.SubJudul(x
                , "Perusahaan : " + Perusahaan
                );

            //Rpt.Header(rpt, x);
            Rpt.HeaderReport(headReport, "", x);
        }
        public static void listcomplain(DropDownList container)
        {
            DataTable rs = Db.Rs("SELECT * FROM REF_COMPLAIN");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                container.Items.Add(new ListItem(rs.Rows[i]["Judul"].ToString(), rs.Rows[i]["NoComplain"].ToString()));
            }
        }
        private void Fill()
        {
            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");

            string Customer = "";
            if (Cust != "")
            {
                Customer = " AND b.NoCustomer='" + Cust + "'";
            }
            string Complain = "";
            if (List != "SEMUA")
            {
                Complain = " AND a.NoComplain=" + List ;
            }

            string nStatus = "";
            if (Status != "2")
            {
                nStatus = " AND a.Status=" + Status;
            }

            string nProject = "";
            if (Project != "SEMUA") nProject = " AND b.Project IN ('" + Project.Replace(",", "','") + "')";

            string nPerusahaan = "";
            if (Perusahaan != "SEMUA") nPerusahaan = " AND b.Pers = '" + Perusahaan + "'";

            string strSql = "SELECT a.*, c.Judul, c.PIC FROM MS_COMPLAIN a"
                          + " INNER JOIN MS_KONTRAK b ON a.NoKontrak = b.NoKontrak"
                          + " INNER JOIN REF_COMPLAIN c ON a.NoComplain = c.NoComplain"
                          + " AND CONVERT(varchar,a.TglComplain,112) >= '" + Cf.Tgl112(Dari) + "'"
                          + " AND CONVERT(varchar,a.TglComplain,112) <= '" + Cf.Tgl112(Sampai) + "'"
                          + nProject
                          + nPerusahaan
                          + Customer
                          + Complain
                          + nStatus
                          + " ORDER BY a.NoKontrak";

            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                r.VerticalAlign = VerticalAlign.Top;
                r.Attributes["ondblclick"] = "popEditKontrak('" + rs.Rows[i]["NoKontrak"] + "')";

                c = new TableCell();
                c.Text = Cf.Num(i + 1);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);


                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglComplain"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Judul"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["PIC"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoKontrak"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Db.SingleString("SELECT Nama FROM MS_CUSTOMER WHERE NOCUSTOMER ='" + rs.Rows[i]["NoCustomer"].ToString() + "'");
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Db.SingleString("SELECT NoUnit FROM MS_KONTRAK WHERE NOKONTRAK='" + rs.Rows[i]["NoKontrak"].ToString() + "'");
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Detil"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Solusi"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglSolved"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = (rs.Rows[i]["Status"].ToString() == "1") ? "Selesai" : "-";
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + rs.Rows[i]["Project"].ToString() + "'");
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

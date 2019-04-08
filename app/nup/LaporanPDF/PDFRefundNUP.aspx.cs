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
    public partial class RefundNUP : System.Web.UI.Page
    {
        private string UserID { get { return (Request.QueryString["userid"]); } }
        private string Project { get { return (Request.QueryString["project"]); } }
        private string AttachmentID { get { return (Request.QueryString["id"]); } }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Report();
        }

        protected void Report()
        {
            Header();
            Fill();
        }

        protected int UserAgent()
        {
            int a = Db.SingleInteger("SELECT NoAgent FROM " + Mi.DbPrefix + "SECURITY..USERNAME WHERE UserID = '" + Act.UserID + "'");

            return a;
        }

        private void Header()
        {
            System.Text.StringBuilder x = new System.Text.StringBuilder();

            Rpt.Judul(x, comp, judul);
            Rpt.SubJudul(x
                , "Project : " + Project
                );

            string legend = "";
            Rpt.HeaderReport(headReport, legend, x);
        }

        private void Fill()
        {
            string nProject = (Project != "SEMUA") ? " AND a.Project IN ('" + Project.Replace(",", "','") + "')" : "";
            string strSql = "SELECT "
                + " a.NoNUP"
                + ",a.NoCustomer"
                + ",a.NilaiBayar"
                + ",a.Status"
                + ",b.Nama AS Nama"
                + ",b.NoKTP AS NoKTP"
                + ",b.KTP1 AS KTP1"
                + ",b.KTP2 AS KTP2"
                + ",b.KTP3 AS KTP3"
                + ",b.KTP4 AS KTP4"
                + ",b.NoHP AS NoHP"
                + ",b.RekBank AS RekBank"
                + ",b.RekNo AS RekNo"
                + ",b.RekNama AS RekNama"
                + ",b.RekCabang AS RekCabang"
                + " FROM MS_NUP a inner join MS_CUSTOMER b on a.NoCustomer = b.NoCustomer"
                + " WHERE 1=1 AND a.Status = 5" //status Refund
                                                //+ Status
                + nProject
                + " ORDER BY a.NoNUP";

            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                r.VerticalAlign = VerticalAlign.Top;
                r.Attributes["ondblclick"] = "popEditNUP('" + rs.Rows[i]["NoNUP"] + "')";

                c = new TableCell();
                c.Text = (i + 1).ToString();
                c.HorizontalAlign = HorizontalAlign.Center;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Str(rs.Rows[i]["NoNUP"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Str(rs.Rows[i]["Nama"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Str(rs.Rows[i]["NoKTP"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["KTP1"].ToString() + " &nbsp; " + Cf.Str(rs.Rows[i]["KTP2"]); ;
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Str(rs.Rows[i]["KTP3"]) + " &nbsp; " + Cf.Str(rs.Rows[i]["KTP4"]); ;
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Str(rs.Rows[i]["NoHP"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "Rp. " + Cf.Num(rs.Rows[i]["NilaiBayar"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Str(rs.Rows[i]["RekBank"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Str(rs.Rows[i]["RekNo"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Str(rs.Rows[i]["RekNama"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Str(rs.Rows[i]["RekCabang"]);
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

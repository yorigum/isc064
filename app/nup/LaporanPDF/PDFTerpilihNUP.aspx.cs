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
    public partial class TerpilihNUP : System.Web.UI.Page
    {
        private string UserID { get { return (Request.QueryString["userid"]); } }
        private string Lokasi { get { return (Request.QueryString["lokasi"]); } }
        private string Jenis { get { return (Request.QueryString["jenis"]); } }
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

            Rpt.SubJudul(x, "Status : Terpilih Unit");

            //Rpt.SubJudul(x
            //    , "Periode : " + periode.SelectedItem.Text
            //    );

            Rpt.SubJudul(x
                , "Jenis : " + Jenis.Replace(";"," ")
                );

            Rpt.SubJudul(x
                , "Project : " + Project
                );

            Rpt.SubJudul(x
                , "Lokasi : " + Lokasi
                );

            Rpt.Header(rpt, x);
        }

        private void Fill()
        {
            string nLokasi = Lokasi != "SEMUA" ? " AND MS_UNIT.Lokasi = '" + Lokasi + "'" : "";
            string nProject = (Project != "SEMUA") ? " AND Project IN ('" + Project.Replace(",", "','") + "')" : "";

            string akt = String.Empty;
            akt = Jenis.Replace("-", ",").TrimEnd(',');
            akt = akt.Replace("+", " ");
            akt = akt.Replace(",", "','");
            akt = "'" + akt + "'";

            decimal t1 = 0;

            string strSql = "SELECT "
                + "	NoStock"
                + ",Jenis"
                + ",Lokasi"
                + ",NoUnit"
                + ",Luas"
                + ",PriceList"
                + ",PriceListMin"
                + ",TglInput"
                + ",Status"
                + " FROM MS_UNIT"
                + " WHERE 1=1"
                + " AND Jenis IN (" + akt + ")"
                + nProject
                + " AND Status = 'P'"
                + nLokasi
                //+ Periode
                + " AND NoStock NOT IN (SELECT NoStock FROM MS_KONTRAK)"
                + " ORDER BY NoStock";
            //Response.Write(strSql);
            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                r.VerticalAlign = VerticalAlign.Top;
                //r.Attributes["ondblclick"] = "popEditUnit('"+rs.Rows[i]["NoStock"]+"')";

                DateTime p = Convert.ToDateTime(rs.Rows[i]["TglInput"]);

                c = new TableCell();
                c.Text = (i + 1).ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Db.SingleString("SELECT NoKontrak FROM MS_KONTRAK WHERE NoStock = '" + rs.Rows[i]["NoStock"].ToString() + "'");
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglInput"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Lokasi"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Jenis"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoUnit"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Status"].ToString();
                c.HorizontalAlign = HorizontalAlign.Center;
                r.Cells.Add(c);

                DataTable dtNUP = Db.Rs("SELECT B.*, C.Nama AS NamaCS, D.Nama AS NamaAG FROM MS_PRIORITY A"
                        + " INNER JOIN MS_NUP B ON A.NoNUP = B.NoNUP"
                        + " INNER JOIN MS_CUSTOMER C ON B.NoCustomer = C.NoCustomer"
                        + " INNER JOIN MS_AGENT D ON B.NoAgent = D.NoAgent"
                        + " WHERE A.NoStock = '" + rs.Rows[i]["NoStock"].ToString() + "'"
                    );

                for (int j = 0; j < dtNUP.Rows.Count; j++)
                {
                    if (!Response.IsClientConnected) break;

                    c = new TableCell();
                    c.Text = dtNUP.Rows[j]["NoNUP"].ToString();
                    c.HorizontalAlign = HorizontalAlign.Left;
                    r.Cells.Add(c);

                    c = new TableCell();
                    decimal bayar = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiBayar),0) FROM MS_NUP_PELUNASAN WHERE NoNUP='" + dtNUP.Rows[j]["NoNUP"].ToString() + "'");
                    c.Text = Cf.Num(bayar);
                    c.HorizontalAlign = HorizontalAlign.Right;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = dtNUP.Rows[j]["NamaCS"].ToString();
                    c.HorizontalAlign = HorizontalAlign.Left;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = dtNUP.Rows[j]["NamaAG"].ToString();
                    c.HorizontalAlign = HorizontalAlign.Left;
                    r.Cells.Add(c);

                    t1 = t1 + bayar;
                }
                rpt.Rows.Add(r);

                if (i == rs.Rows.Count - 1)
                    SubTotal("TOTAL PEMBAYARAN", t1);
            }
        }

        private void SubTotal(string txt, decimal t1)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = Rpt.Foot();
            c.Text = txt;
            c.ColumnSpan = 8;
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t1);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = "&nbsp;";
            c.ColumnSpan = 4;
            c.HorizontalAlign = HorizontalAlign.Left;
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

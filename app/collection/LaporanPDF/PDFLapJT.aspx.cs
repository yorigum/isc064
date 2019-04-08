using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.COLLECTION.Laporan
{
    public partial class LapJT : System.Web.UI.Page
    {
        protected System.Web.UI.WebControls.PlaceHolder list;
        protected System.Web.UI.WebControls.Label total1;
        protected System.Web.UI.WebControls.Label total2;
        protected System.Web.UI.WebControls.Label total3;
        protected System.Web.UI.WebControls.Label total4;
        protected System.Web.UI.WebControls.Label total5;


        private string UserID { get { return (Request.QueryString["userid"]); } }
        private string AttachmentID { get { return (Request.QueryString["id"]); } }
        private string Perusahaan { get { return (Request.QueryString["pers"]); } }
        private string Project { get { return (Request.QueryString["project"]); } }
        private string CaraBayar { get { return (Request.QueryString["carabayar"]); } }
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

            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");

            if (KPAStatus == "kpa1")
                Rpt.SubJudul(x, "Status KPR <b style='padding-left:5px'>:</b> INCLUDE TAGIHAN KPR ");
            else
                Rpt.SubJudul(x, "Status KPR <b style='padding-left:5px'>:</b> EXCLUDE TAGIHAN KPR");
            Rpt.SubJudul(x
                , "As of <b style='padding-left:40px'>:</b> " + Cf.Day(Dari));
            Rpt.SubJudul(x, " Cara Bayar <b style='padding-left:5px'>:</b> " + CaraBayar.Replace('-',',').TrimEnd());
            Rpt.SubJudul(x, " Perusahaan : " + Perusahaan);
            Rpt.SubJudul(x, " Project <b style='padding-left:28px'>:</b> " + Project);

            //Rpt.Header(rpt, x);
            string legend = "";
            Rpt.HeaderReport(headReport, legend, x);
        }

        private void Fill()
        {
            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            decimal t1 = 0, t2 = 0, t3 = 0;

            string KPR = "";
            if (KPAStatus == "kpa1")
            {
                KPR = " ";
            }
            else if (KPAStatus == "kpa2")
            {
                KPR = " AND a.KPR != '1' ";
            }

            string nProject = "";
            if (Project != "SEMUA") nProject = " AND b.Project IN ('" + Project.Replace(",","','") + "')";
            string nPerusahaan = "";
            if (Perusahaan != "SEMUA") nPerusahaan = " AND b.Pers = '" + Perusahaan + "'";

            //Cara Bayar
            string akt = String.Empty;
            akt = CaraBayar.Replace("-", ",").TrimEnd(',');
            akt = akt.Replace("+", " ");
            akt = akt.Replace(",", "','");
            akt = "'" + akt + "'";

            string aa = "";
            if (UserAgent() > 0)
                aa = " AND b.NoAgent = " + UserAgent();

            string strSql = "SELECT a.*, b.NoCustomer, b.NoUnit"
                + " FROM ISC064_MARKETINGJUAL..MS_TAGIHAN a"
                + " INNER JOIN ISC064_MARKETINGJUAL..MS_KONTRAK b ON a.NoKontrak = b.NoKontrak"
                + " WHERE DATEDIFF(DAY, '" + Cf.Tgl112(Dari) + "', CONVERT(DATETIME, TglJT, 112)) BETWEEN 0 AND 14"
                + " AND b.CaraBayar IN(" + akt + ")"
                + " AND b.STATUS != 'B'"
                + nProject
                + nPerusahaan
                + KPR
                + aa
                ;

            DataTable rs = Db.Rs(strSql);

            TableRow r = new TableRow();
            rpt.Rows.Add(r);

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                r = new TableRow();
                TableCell c;

                c = new TableCell();
                c.Text = (i + 1).ToString();
                c.HorizontalAlign = HorizontalAlign.Center;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoKontrak"].ToString() + "." + rs.Rows[i]["NoUrut"];
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
                c.Text = Cf.Day(rs.Rows[i]["TglJt"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["NilaiTagihan"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                decimal NilaiPelunasan = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan),0) FROM ISC064_MARKETINGJUAL..MS_PELUNASAN WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' AND NoTagihan = '" + rs.Rows[i]["NoUrut"] + "'");
                c = new TableCell();
                c.Text = Cf.Num(NilaiPelunasan);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                decimal SisaTagihan = Convert.ToDecimal(rs.Rows[i]["NilaiTagihan"]) - NilaiPelunasan;
                c = new TableCell();
                c.Text = Cf.Num(SisaTagihan);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                rpt.Rows.Add(r);

                t1 = t1 + (decimal)rs.Rows[i]["NilaiTagihan"];
                t2 = t2 + NilaiPelunasan;
                t3 = t3 + SisaTagihan;

                if (i == (rs.Rows.Count - 1))
                {
                    SubTotal("GRAND TOTAL", t1, t2, t3);
                }
            }
        }

        private void SubTotal(string txt, decimal t1, decimal t2, decimal t3)
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

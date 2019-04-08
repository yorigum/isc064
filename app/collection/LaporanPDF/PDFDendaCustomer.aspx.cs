using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.COLLECTION.Laporan
{
    public partial class DendaCustomer : System.Web.UI.Page
    {
        protected System.Web.UI.WebControls.TextBox dari;
        protected System.Web.UI.WebControls.TextBox sampai;
        protected System.Web.UI.WebControls.Label daric;
        protected System.Web.UI.WebControls.Label sampaic;
        protected System.Web.UI.WebControls.CheckBox tipeCheck;
        protected System.Web.UI.WebControls.Label tipec;
        protected System.Web.UI.WebControls.CheckBoxList tipe;
        protected System.Web.UI.WebControls.RadioButton tglkontrak;
        protected System.Web.UI.WebControls.RadioButton tgljt;

        private string UserID { get { return (Request.QueryString["userid"]); } }
        private string AttachmentID { get { return (Request.QueryString["id"]); } }
        private string Project { get { return (Request.QueryString["project"]); } }
        private string Perusahaan { get { return (Request.QueryString["pers"]); } }
        private string Lokasi { get { return (Request.QueryString["lokasi"]); } }
        private string Asof { get { return (Request.QueryString["asof"]); } }

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

            System.Text.StringBuilder x = new System.Text.StringBuilder();

            Rpt.Judul(x, comp, judul);
            Rpt.SubJudul(x, " Lokasi <b style='padding-left:38px'>:</b>" + Lokasi);
            DateTime Tanggal = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            Rpt.SubJudul(x, " As Of <b style='padding-left:44px'>:</b> " + Tanggal);
            string Pers = "SEMUA";
            if(Perusahaan != "SEMUA")
            {
                Pers = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PERS WHERE Pers = '" + Perusahaan + "'");
            }
            Rpt.SubJudul(x, " Perusahaan <b style='padding-left:4px'>:</b> " + Pers);
            Rpt.SubJudul(x, " Project <b style='padding-left:30px'>:</b> " + Project);

            string legend = "<br />Status : A = Aktif / B = Batal.<br />";
            Rpt.HeaderReport(headReport, legend, x);

            Fill();
        }

        private void Fill()
        {
            decimal total = 0;
            decimal t2 = 0;
            decimal t1 = 0;
            decimal t3 = 0;
            decimal t4 = 0;
            decimal t5 = 0;

            DateTime Tanggal = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");

            string nLokasi = "";
            if (Lokasi != "SEMUA")
            {
                nLokasi = " AND ISC064_MARKETINGJUAL..MS_KONTRAK.Lokasi = '" + Cf.Str(Lokasi) + "'";
            }

            string nProject = "";
            if (Project != "SEMUA") nProject = " AND ISC064_MARKETINGJUAL..MS_KONTRAK.Project IN ('" + Project.Replace(",","','") + "')";
            string nPerusahaan = "";
            if (Perusahaan != "SEMUA") nPerusahaan = " AND ISC064_MARKETINGJUAL..MS_KONTRAK.Pers = '" + Perusahaan + "'";

            string strSql = "SELECT "
                + " ISC064_MARKETINGJUAL..MS_KONTRAK.NoKontrak"
                + ",ISC064_MARKETINGJUAL..MS_KONTRAK.NilaiKontrak"
                + ",ISC064_MARKETINGJUAL..MS_KONTRAK.NoUnit"
                + ",ISC064_MARKETINGJUAL..MS_CUSTOMER.Nama AS Cs"
                + ",DATEDIFF(day,convert(datetime,ISC064_MARKETINGJUAL..MS_TAGIHAN.TglJT,112),'" + Cf.Tgl112(Tanggal) + "') as Telat"
                + ",ISC064_MARKETINGJUAL..MS_TAGIHAN.NilaiTagihan AS NilaiTagihan"
                + ",ISC064_MARKETINGJUAL..MS_TAGIHAN.Denda AS Denda"
                + ",ISC064_MARKETINGJUAL..MS_TAGIHAN.DendaReal AS DendaReal"
                + ",ISC064_MARKETINGJUAL..MS_TAGIHAN.NilaiPutihDenda AS PutihDenda"
                + ",ISC064_MARKETINGJUAL..MS_TAGIHAN.AlokasiBenefit AS AlokasiBenefit"
                + ",ISC064_MARKETINGJUAL..MS_TAGIHAN.Benefit AS Benefit"
                + ",ISC064_MARKETINGJUAL..MS_TAGIHAN.BenefitReal AS BenefitReal"
                + " FROM ISC064_MARKETINGJUAL..MS_KONTRAK INNER JOIN ISC064_MARKETINGJUAL..MS_CUSTOMER ON ISC064_MARKETINGJUAL..MS_KONTRAK.NoCustomer = ISC064_MARKETINGJUAL..MS_CUSTOMER.NoCustomer"
                + " INNER JOIN ISC064_MARKETINGJUAL..MS_TAGIHAN ON ISC064_MARKETINGJUAL..MS_KONTRAK.NoKontrak = ISC064_MARKETINGJUAL..MS_TAGIHAN.NoKontrak"
                + " WHERE 1=1 "
                + " AND ISC064_MARKETINGJUAL..MS_TAGIHAN.TglJT <= '" + Cf.AwalBulan1(Tanggal.Month, Tanggal.Year, Tanggal.Day) + "'"
                + " AND ISC064_MARKETINGJUAL..MS_TAGIHAN.Denda > 0 OR ISC064_MARKETINGJUAL..MS_TAGIHAN.Benefit > 0"
                + nProject
                + nPerusahaan
                + nLokasi
                + " ORDER BY ISC064_MARKETINGJUAL..MS_CUSTOMER.Nama ASC";

            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                int no = i + 1;

                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                c = new TableCell();
                c.Text = Cf.Str(no);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoKontrak"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Cs"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoUnit"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["NilaiKontrak"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["NilaiTagihan"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Telat"].ToString();
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Math.Round(Convert.ToDecimal(rs.Rows[i]["Denda"])));
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Math.Round(Convert.ToDecimal(rs.Rows[i]["Benefit"])));
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Math.Round(Convert.ToDecimal(rs.Rows[i]["BenefitReal"])));
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Math.Round(Convert.ToDecimal(rs.Rows[i]["DendaReal"])));
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["PutihDenda"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                decimal SaldoDenda = Math.Round(Convert.ToDecimal(rs.Rows[i]["Denda"]) - Convert.ToDecimal(rs.Rows[i]["DendaReal"]) - Convert.ToDecimal(rs.Rows[i]["PutihDenda"]) - Convert.ToDecimal(rs.Rows[i]["AlokasiBenefit"]));
                c.Text = Cf.Num(SaldoDenda);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                rpt.Rows.Add(r);

                total = total + (decimal)rs.Rows[i]["Denda"];
                t2 = t2 + (decimal)rs.Rows[i]["DendaReal"];
                t1 = t1 + (decimal)rs.Rows[i]["PutihDenda"];
                t3 = t3 + (decimal)rs.Rows[i]["Benefit"];
                t4 = t4 + (decimal)rs.Rows[i]["BenefitReal"];
                t5 = t5 + SaldoDenda;

                if (i == rs.Rows.Count - 1)
                    SubTotal("GRAND TOTAL", total, t2, t1, t3, t4,t5);
            }
        }

        private void SubTotal(string txt, decimal total, decimal t2, decimal t1, decimal t3, decimal t4, decimal t5)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = Rpt.Foot();
            c.Text = txt;
            c.ColumnSpan = 7;
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(Math.Round(total));
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(Math.Round(t3));
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(Math.Round(t4));
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(Math.Round(t2));
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(Math.Round(t1));
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(Math.Round(t5));
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

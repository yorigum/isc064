using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.COLLECTION.Laporan
{
    public partial class PenerimaanCustomer : System.Web.UI.Page
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
        private string StatusA { get { return (Request.QueryString["status_a"]); } }
        private string StatusS { get { return (Request.QueryString["status_s"]); } }
        private string StatusB { get { return (Request.QueryString["status_b"]); } }
        //private string AsOF { get { return (Request.QueryString["asof"]); } }
        private string Lokasi { get { return (Request.QueryString["lokasi"]); } }
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

            System.Text.StringBuilder x = new System.Text.StringBuilder();

            Rpt.Judul(x, comp, judul);

            if (StatusA != "")
                x.Append("Status : " + StatusA);
            else if (StatusB != "")
                x.Append("Status : " + StatusB);
            else
                x.Append("Status : " + StatusS);


            DateTime Tanggal = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            x.Append("<br />As of : " + Cf.Day(Tanggal));
            x.Append("<br />Lokasi : " + Lokasi);
            x.Append("<br />Perusahaan : " + Perusahaan);
            x.Append("<br />Project : " + Project);

            string legend = "<br />Status : A = Aktif / B = Batal.<br />";
            Rpt.HeaderReport(headReport, legend, x);

            Fill();
        }

        private void Fill()
        {
            string nProject = "";
            if (Project != "SEMUA") nProject = " AND ISC064_MARKETINGJUAL..MS_KONTRAK.Project IN ('" + Project.Replace(",","','") + "')";
            string nPerusahaan = "";
            if (Perusahaan != "SEMUA") nPerusahaan = " AND ISC064_MARKETINGJUAL..MS_KONTRAK.Pers = '" + Perusahaan + "'";

            string Status = "";
            if (StatusA != "") Status = " AND ISC064_MARKETINGJUAL..MS_KONTRAK.Status = 'A'";
            if (StatusB != "") Status = " AND ISC064_MARKETINGJUAL..MS_KONTRAK.Status = 'B'";

            decimal total = 0;
            decimal t2 = 0;
            decimal t1 = 0;
            decimal t3 = 0;
            decimal t4 = 0;
            decimal t5 = 0;
            decimal t6 = 0;


            DateTime Tanggal = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            //DateTime tanggal = Convert.ToDateTime(AsOF);

            string nLokasi = "";
            if (Lokasi != "SEMUA")
            {
                nLokasi = " AND ISC064_MARKETINGJUAL..MS_KONTRAK.Lokasi = '" + Cf.Str(Lokasi) + "'";
            }

            string KPR = "";
            if (KPAStatus == "kpa1")
            {
                KPR = " ";
            }
            else if (KPAStatus == "kpa2")
            {
                KPR = " AND a.KPR != '1' ";
            }

            string aa = "";
            if (UserAgent() > 0)
                aa = " AND " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK.NoAgent = " + UserAgent();

       

            string strSql = "SELECT "
                + " ISC064_MARKETINGJUAL..MS_KONTRAK.TglKontrak"
                + ",ISC064_MARKETINGJUAL..MS_KONTRAK.NoKontrak"
                + ",ISC064_MARKETINGJUAL..MS_KONTRAK.NoUnit"
                + ",ISC064_MARKETINGJUAL..MS_KONTRAK.NilaiKontrak"
                + ",ISC064_MARKETINGJUAL..MS_CUSTOMER.Nama AS Cs"
                + ",ISC064_MARKETINGJUAL..MS_AGENT.Nama AS Agent"
                + ",(SELECT ISNULL(SUM(NilaiPelunasan),0) FROM ISC064_MARKETINGJUAL..MS_PELUNASAN a join ISC064_marketingjual..ms_tagihan b on a.NoKontrak = b.NoKontrak WHERE a.NoKontrak = ISC064_MARKETINGJUAL..MS_KONTRAK.NoKontrak AND a.TglPelunasan < '" + Cf.AwalBulan(Tanggal.Month, Tanggal.Year) + "' AND b.Tipe <> 'ADM' and a.NoTagihan = b.NoUrut) AS Lalu"
                + ",(SELECT ISNULL(SUM(NilaiPelunasan),0) FROM ISC064_MARKETINGJUAL..MS_PELUNASAN a join ISC064_marketingjual..ms_tagihan b on a.NoKontrak = b.NoKontrak WHERE a.NoKontrak = ISC064_MARKETINGJUAL..MS_KONTRAK.NoKontrak AND a.TglPelunasan >= '" + Cf.AwalBulan(Tanggal.Month, Tanggal.Year) + "' AND a.TglPelunasan <= '" + Cf.AwalBulan1(Tanggal.Month, Tanggal.Year, Tanggal.Day) + "' AND b.Tipe <> 'ADM' and a.NoTagihan = b.NoUrut) AS Berjalan"
                + " FROM ISC064_MARKETINGJUAL..MS_KONTRAK INNER JOIN ISC064_MARKETINGJUAL..MS_CUSTOMER ON ISC064_MARKETINGJUAL..MS_KONTRAK.NoCustomer = ISC064_MARKETINGJUAL..MS_CUSTOMER.NoCustomer"
                + " INNER JOIN ISC064_MARKETINGJUAL..MS_AGENT ON ISC064_MARKETINGJUAL..MS_KONTRAK.NoAgent = ISC064_MARKETINGJUAL..MS_AGENT.NoAgent"
                + " WHERE 1=1 "
                + nProject
                + nPerusahaan
                + nLokasi
                + Status
                + aa;

            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                int no = i + 1;

                decimal KPALalu = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan),0) FROM "
                + " ISC064_MARKETINGJUAL..MS_PELUNASAN A INNER JOIN "
                + " ISC064_MARKETINGJUAL..MS_TAGIHAN B on A.NoTagihan = B.NoUrut AND A.NoKontrak = B.NoKontrak "
                + " WHERE TglPelunasan < '" + Cf.AwalBulan(Tanggal.Month, Tanggal.Year) + "' "
                + " AND B.KPR = '1' "
                + " AND B.TIPE <> 'ADM' "
                + " AND A.NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' ");

                decimal BerjalanLalu = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan),0) FROM "
                + " ISC064_MARKETINGJUAL..MS_PELUNASAN A INNER JOIN "
                + " ISC064_MARKETINGJUAL..MS_TAGIHAN B on A.NoTagihan = B.NoUrut AND A.NoKontrak = B.NoKontrak "
                + " WHERE TglPelunasan >= '" + Cf.AwalBulan(Tanggal.Month, Tanggal.Year) + "' "
                + " AND TglPelunasan <= '" + Cf.AwalBulan1(Tanggal.Month, Tanggal.Year, Tanggal.Day) + "' "
                + " AND B.KPR = '1' "
                + " AND B.TIPE <> 'ADM' "
                + " AND A.NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' ");

                decimal NilaiLalu = Convert.ToDecimal(rs.Rows[i]["Lalu"]);
                decimal NilaiBerjalan = Convert.ToDecimal(rs.Rows[i]["Berjalan"]);

                if (KPAStatus == "kpa2")
                {
                    NilaiLalu -= KPALalu;
                    NilaiBerjalan -= BerjalanLalu;
                }

                decimal sekarang = NilaiLalu + NilaiBerjalan;
                decimal saldo = (decimal)rs.Rows[i]["NilaiKontrak"] - sekarang;

                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                c = new TableCell();
                c.Text = Cf.Str(no);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglKontrak"]);
                c.HorizontalAlign = HorizontalAlign.Left;
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
                c.Text = rs.Rows[i]["Agent"].ToString();
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
                c.Text = Cf.Num(NilaiLalu);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(NilaiBerjalan);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(sekarang);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(saldo);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                decimal adm = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiTagihan),0)FROM ISC064_MARKETINGJUAL..MS_TAGIHAN WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' AND Tipe = 'ADM' ");
                c.Text = Cf.Num(adm.ToString());
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                decimal admterima = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan),0)FROM ISC064_MARKETINGJUAL..MS_PELUNASAN a join ISC064_MARKETINGJUAL..MS_TAGIHAN b ON a.NoKontrak = b.NoKontrak WHERE a.NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' AND Tipe = 'ADM' AND a.NoTagihan = b.NoUrut AND a.TglPelunasan <= '" + Cf.AwalBulan1(Tanggal.Month, Tanggal.Year, Tanggal.Day) + "'");
                c.Text = Cf.Num(admterima.ToString());
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                rpt.Rows.Add(r);

                total = total + (decimal)rs.Rows[i]["NilaiKontrak"];
                t2 = t2 + (decimal)rs.Rows[i]["NilaiKontrak"];
                t1 = t1 + NilaiLalu;
                t3 = t3 + NilaiBerjalan;
                t4 = t4 + sekarang;
                t5 = t5 + adm;
                t6 = t6 + admterima;

                if (i == rs.Rows.Count - 1)
                    SubTotal("GRAND TOTAL", total, t2, t1, t3, t4, t5, t6);
            }
        }

        private string DetilLunas(string NoKontrak, int NoTagihan)
        {
            System.Text.StringBuilder x = new System.Text.StringBuilder();

            DataTable rs = Db.Rs("SELECT CaraBayar, TglPelunasan, Ket FROM ISC064_MARKETINGJUAL..MS_PELUNASAN"
                + " WHERE NoKontrak = '" + NoKontrak + "' AND NoTagihan = " + NoTagihan
                );
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (x.Length != 0) x.Append("<br>");
                x.Append(rs.Rows[i]["CaraBayar"] + ", " + Cf.Day(rs.Rows[i]["TglPelunasan"]) + " "
                    + rs.Rows[i]["Ket"]);
            }

            return x.ToString();
        }

        private void SubTotal(string txt, decimal total, decimal t2, decimal t1, decimal t3, decimal t4, decimal t5, decimal t6)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = Rpt.Foot();
            c.Text = txt;
            c.ColumnSpan = 6;
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(total);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t1);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t3);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t4);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(Math.Round(t2 - t4));
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t5);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t6);
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

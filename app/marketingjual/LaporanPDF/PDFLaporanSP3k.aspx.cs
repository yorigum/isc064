using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL.Laporan
{
    public partial class Laporan_LaporanSP3k : System.Web.UI.Page
    {
        private string UserID { get { return (Request.QueryString["userid"]); } }
        private string AttachmentID { get { return (Request.QueryString["id"]); } }
        private string Lokasi { get { return (Request.QueryString["lokasi"]); } }
        private string Tanggal2 { get { return (Request.QueryString["tanggal"]); } }
        private string Rekening { get { return (Request.QueryString["rek"]); } }
        private string Project { get { return (Request.QueryString["project"]); } }
        private string Perusahaan { get { return (Request.QueryString["pers"]); } }
        private string StatusSPK { get { return (Request.QueryString["statusspk"]); } }
        private string HasilSPK { get { return (Request.QueryString["hasilspk"]); } }

        protected void Page_Load(object sender, EventArgs e)
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
                , Tanggal2 + " : " + Cf.Day(Dari) + " s/d " + Cf.Day(Sampai)
                );

            Rpt.SubJudul(
                x, "Status SP3K : " + StatusSPK.Replace("-"," ")
                );

            Rpt.SubJudul(
                x, "Hasil SP3K : " + HasilSPK.Replace("-", " ")
                );

            Rpt.SubJudul(
                x, "Project : " + Project
                );

            Rpt.SubJudul(
                x, "Perusahaan : " + Perusahaan
                );

            if (Rekening == "SEMUA")
                Rpt.SubJudul(x, "Rekening Bank: SEMUA");
            else
                Rpt.SubJudul(x, "Rekening Bank: " + Rekening);

            //Rpt.Header(rpt, x);
            Rpt.HeaderReport(headReport, "", x);
        }
        private void Fill()
        {
            //string w = "";
            string nLokasi = "";
            if (Lokasi != "SEMUA")
                nLokasi += " AND a.Lokasi = '" + Lokasi + "'";

            string tgl = "";
            if (Tanggal2 == "TargetSP3K")
                tgl = "TargetSP3K";
            else if (Tanggal2 == "TglPengajuanSP3K")
                tgl = "TglPengajuanSP3K";
            else if (Tanggal2 == "TglHasilSP3K")
                tgl = "TglHasilSP3K";

            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");


            string Status = "";
            if (StatusSPK != "SEMUA")
            {
                if (StatusSPK == "BELUM DITENTUKAN")
                    Status = " AND StatusSP3K = ''";
                else
                    Status = " AND StatusSP3K = '" + StatusSPK.Replace("-"," ") + "'";
            }

            string Hasil = "";
            if (HasilSPK != "SEMUA")
            {
                Hasil = " AND HasilSP3K = '" + HasilSPK.Replace("-", " ") + "'";
            }

            string Tanggal = "";
            Tanggal = " AND CONVERT(varchar," + tgl + ",112) >= '" + Cf.Tgl112(Dari) + "'"
                    + " AND CONVERT(varchar," + tgl + ",112) <= '" + Cf.Tgl112(Sampai) + "'";


            string BankKPR = "";
            if (Rekening != "SEMUA")
                BankKPR = " AND BankKPR = '" + Rekening + "'";

            string nProject = "";
            if (Project != "SEMUA") nProject = " AND a.Project IN ('" + Project.Replace(",", "','") + "')";

            string nPerusahaan = "";
            if (Perusahaan != "SEMUA") nPerusahaan = " AND a.Pers = '" + Perusahaan + "'";

            string aa = "";
            if (UserAgent() > 0)
                aa = " AND a.NoAgent = " + UserAgent();


            string strSql = "SELECT a.*, b.Nama AS NamaCustomer, d.LuasSG"
                + " FROM MS_KONTRAK a"
                + " INNER JOIN MS_CUSTOMER b ON a.NoCustomer = b.NoCustomer"
                  + " INNER JOIN MS_UNIT d ON a.NoUnit = d.NoUnit "
                + " WHERE a.Status = 'A'"                
                + nPerusahaan
                + nProject
                + nLokasi
                + Tanggal
                + Status
                + Hasil
                + BankKPR
                + aa
                ;
            DataTable rs = Db.Rs(strSql);

            decimal t = 0, PotensiKPR = 0;

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected)
                    break;

                TableRow r = new TableRow();
                r.Attributes["ondblclick"] = "popEditProsesKPR('" + Cf.Pk(rs.Rows[i]["NoKontrak"]) + "');";
                TableCell c;

                c = new TableCell();
                c.Text = (i + 1).ToString();
                c.HorizontalAlign = HorizontalAlign.Center;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoKontrak"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NamaCustomer"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoUnit"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["LuasSG"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["BankKPR"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["StatusSP3K"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TargetSP3K"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglPengajuanSP3K"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglHasilSP3K"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoSP3K"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["HasilSP3K"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + rs.Rows[i]["Project"].ToString() + "'");
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);
                c = new TableCell();
                PotensiKPR = Convert.ToDecimal(rs.Rows[i]["ApprovalKPR"]);
                c.Text = Cf.Num(PotensiKPR);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);
                

                //c = new TableCell();
                //PotensiKPR = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiTagihan), 0) FROM MS_TAGIHAN WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' AND Tipe = 'ANG' AND (NamaTagihan LIKE '%KPR%' OR NamaTagihan LIKE '%AKAD%')");
                //c.Text = Cf.Num(PotensiKPR);
                //c.HorizontalAlign = HorizontalAlign.Right;
                //r.Cells.Add(c);

                rpt.Rows.Add(r);

                t += PotensiKPR;

                if (i == (rs.Rows.Count - 1))
                    SubTotal(t);
            }
        }
        protected void SubTotal(decimal t)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = Rpt.Foot();
            c.Text = "<strong>GRAND TOTAL</strong>";
            c.ColumnSpan = 13;
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = "<strong>" + Cf.Num(t) + "</strong>";
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            rpt.Rows.Add(r);
        }

}
}
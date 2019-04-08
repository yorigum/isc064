using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.KOMISI.Laporan
{

    public partial class MasterCF : System.Web.UI.Page
{
        private string StatusS { get { return (Request.QueryString["status_s"]); } }
        private string StatusA { get { return (Request.QueryString["status_a"]); } }
        private string StatusB { get { return (Request.QueryString["status_b"]); } }
        private string UserID { get { return (Request.QueryString["userid"]); } }
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
            MenuAtas();
            Fill();
        }

        private void Header()
        {
            System.Text.StringBuilder x = new System.Text.StringBuilder();

            //Rpt.Judul(x, comp, judul);

            if (StatusA != "")
                Rpt.SubJudul(x, "Status : " + StatusA);
            else if (StatusB != "")
                Rpt.SubJudul(x, "Status : " + StatusB);
            else
                Rpt.SubJudul(x, "Status : " + StatusS);

            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID=" + AttachmentID + "");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID=" + AttachmentID + "");

            Rpt.SubJudul(x
                , "Tanggal Kontrak" + " : " + Cf.Day(Dari) + " s/d " + Cf.Day(Sampai)
                );

            Rpt.SubJudul(x
                , "Project : " + Project
                );

            string pers = (Perusahaan == "SEMUA") ? "SEMUA" : Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + Project + "'");
            Rpt.SubJudul(x
                , "Perusahaan : " + pers
                );

            //Rpt.Header(rpt, x);
            string legend = "Status: A = Aktif / B = Batal.";
            Rpt.HeaderReport(headReport, legend, x);
        }

        private void MenuAtas()
        {
            TableRow r = new TableRow();
            TableRow r2 = new TableRow();
            TableRow r3 = new TableRow();
            TableRow r4 = new TableRow();
            TableCell c = new TableCell();
            TableCell c2 = new TableCell();
            TableCell c3 = new TableCell();
            TableCell c4 = new TableCell();

            c = new TableCell();
            c.Text = "No Kontrak";
            c.ForeColor = Color.White;
            c.Attributes["style"] = "background-color:#1E90FF";
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "No Unit";
            c.ForeColor = Color.White;
            c.Attributes["style"] = "background-color:#1E90FF";
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "Agent";
            c.ForeColor = Color.White;
            c.Attributes["style"] = "background-color:#1E90FF";
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "Nilai";
            c.ForeColor = Color.White;
            c.Attributes["style"] = "background-color:#1E90FF";
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "Status";
            c.ForeColor = Color.White;
            c.Attributes["style"] = "background-color:#1E90FF";
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            rpt.Rows.Add(r);
        }

        private void Fill()
        {
            string Status = "";
            if (StatusA != "") Status = " AND A.Status = 'A'";
            if (StatusB != "") Status = " AND A.Status = 'B'";

            string tgl = "";
            string order = "";

            tgl = "A.TglKontrak";
            order = ",A.NoKontrak";


            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");

            string nProject = "";
            if (Project != "SEMUA") nProject = " AND A.Project = '" + Project + "'";
            string nPerusahaan = "";
            if (Perusahaan != "SEMUA") nPerusahaan = " AND A.Pers = '" + Perusahaan + "'";

            int index = 1;

            int no = 1;
            decimal t1 = 0;

            string strSql = "SELECT "
                + "A.NoKontrak"
                + ",A.NoUnit"
                + ",C.Nilai"
                + ",B.NamaAgent"
                + ",A.Status"
                + ",A.PersenLunas"
                + ",A.NoAgent"
                + ",C.NoCF"
                + ",C.SN"
                + " FROM MS_KONTRAK A INNER JOIN MS_KOMISI_CF B ON A.NoKontrak = B.NoKontrak"
                + " INNER JOIN MS_KOMISI_CF_DETAIL C ON B.NoCF = C.NoCF"
                //+ " WHERE A.NoAgent= '" + sr.Rows[g]["NoAgent"] + "'"
                //+ " AND A.FlagKomisi = '1'"
                + Status
                + " AND CONVERT(varchar,A.TglKontrak,112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND CONVERT(varchar,A.TglKontrak,112) <= '" + Cf.Tgl112(Sampai) + "'"
                + nProject
                + nPerusahaan
                + " ORDER BY B.NamaAgent"
                + order;

            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;
                TableRow r2a;
                TableHeaderCell th2;
                Table tb;

                r.VerticalAlign = VerticalAlign.Top;
                //r.Attributes["ondblclick"] = "popJadwalKomisi('" + rs.Rows[i]["NoKontrak"] + "')";

                c = new TableCell();
                c.Text = rs.Rows[i]["NoKontrak"].ToString();
                string NoKontrak = rs.Rows[i]["NoKontrak"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoUnit"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NamaAgent"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                decimal NKom = (Convert.ToDecimal(rs.Rows[i]["Nilai"])); //Db.SingleDecimal("Select Nilai From MS_KOMISI_DETAIL Where NoKontrak ='" + rs.Rows[i]["NoKontrak"].ToString() + "'");
                c.Text = Cf.Num(Math.Round(NKom));
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                string StatusCF = "<label style='color:red;'>Belum Pengajuan</label>", NoRef = "";
                DataTable cfp = Db.Rs("SELECT * FROM MS_KOMISI_CFP_DETAIL WHERE NoCF = '" + rs.Rows[i]["NoCF"].ToString() + "' AND SN_NoCF = " + Convert.ToInt32(rs.Rows[i]["SN"]));
                if (cfp.Rows.Count > 0)
                {
                    NoRef = cfp.Rows[0]["NoCFP"].ToString();
                    StatusCF = "<label style='color:green;'>Pengajuan Pencairan</label>";

                    DataTable cfr = Db.Rs("SELECT * FROM MS_KOMISI_CFR_DETAIL WHERE NoCF = '" + rs.Rows[i]["NoCF"].ToString() + "' AND SN_NoCF = " + Convert.ToInt32(rs.Rows[i]["SN"]));
                    if (cfr.Rows.Count > 0)
                    {
                        NoRef = cfr.Rows[0]["NoCFR"].ToString();
                        StatusCF = "<label style='color:blue;'>Realisasi Pencairan</label>";
                    }
                }

                c = new TableCell();
                c.Text = StatusCF;
                r.Cells.Add(c);

                rpt.Rows.Add(r);
                no++;
            }
        }
        private void SubTotal(string txt, decimal t1)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = Rpt.Foot();
            c.Text = txt;
            c.ColumnSpan = 4;
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t1);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
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
using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Diagnostics;

namespace ISC064.MARKETINGJUAL.Laporan
{
    public partial class MasterReservasi : System.Web.UI.Page
    {
        private string UserID { get { return (Request.QueryString["userid"]); } }
        private string AttachmentID { get { return (Request.QueryString["id"]); } }
        private string StatusS { get { return (Request.QueryString["status_s"]); } }
        private string StatusA { get { return (Request.QueryString["status_a"]); } }
        private string StatusE { get { return (Request.QueryString["status_e"]); } }
        private string nAgent { get { return (Request.QueryString["agent"]); } }
        private string nNoUrut { get { return (Request.QueryString["nourut"]); } }
        private string nLokasi { get { return (Request.QueryString["lokasi"]); } }
        private string Project { get { return (Request.QueryString["project"]); } }
        private string Perusahaan { get { return (Request.QueryString["pers"]); } }
        private string Tipe { get { return (Request.QueryString["tipe"]); } }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Report();
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

            Rpt.Judul(x, comp, judul);

            if (StatusA != "")
                Rpt.SubJudul(x, "Status : " + StatusA);
            else if (StatusE != "")
                Rpt.SubJudul(x, "Status : " + StatusE);
            else
                Rpt.SubJudul(x, "Status : " + StatusS);

            //string tgl = "";
            //if (tglreservasi.Checked) tgl = tglreservasi.Text;
            //if (tglbatas.Checked) tgl = tglbatas.Text;

            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");

            Rpt.SubJudul(x
                , "Tanggal Reservasi" + " : " + Cf.Day(Dari) + " s/d " + Cf.Day(Sampai)
                );

            Rpt.SubJudul(x
               , "Jenis : " + Tipe.Replace("-", ",").Replace("%", " ").TrimEnd(',')
               );

            Rpt.SubJudul(x
                , "Lokasi : " + nLokasi
                );

            Rpt.SubJudul(x
                , "Principal : " + nAgent
                );

            Rpt.SubJudul(
                x, "Perusahaan : " + Perusahaan
                );

            Rpt.SubJudul(
                x, "Project : " + Project
                );

            string legend = "Status: A = Aktif / E = Expire.< br />"
                        + "Luas dalam meter persegi. Price List dalam rupiah per meter persegi per bulan.";

            Rpt.HeaderReport(headReport, legend, x);
        }

        private void Fill()
        {
            string nStatus = "";
            if (StatusA != "") nStatus = " AND MS_RESERVASI.Status = 'A'";
            else if (StatusE != "") nStatus = " AND MS_RESERVASI.Status = 'E'";

            string nNoUrut = "";
            if (nNoUrut != "")
            {
                nNoUrut = " AND NoUrut = 1";
            }

            string tgl = "";
            tgl = "Tgl";

            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");

            string Lokasi = "";
            if (nLokasi != "SEMUA")
            {
                Lokasi = " AND Lokasi = '" + Cf.Str(nLokasi) + "'";
            }

            string Agent = "";
            if (nAgent != "SEMUA")
            {
                Agent = " AND Principal = '" + Cf.Str(nAgent) + "'";
            }

            string nProject = "";
            if (Project != "SEMUA") nProject = " AND MS_KONTRAK.Project IN ('" + Project.Replace(",", "','") + "')";

            string nPerusahaan = "";
            if (Perusahaan != "SEMUA") nPerusahaan = " AND MS_KONTRAK.Pers = '" + Perusahaan + "'";

            string akt = String.Empty;
            akt = Tipe.Replace("-", ",").TrimEnd(',');
            akt = akt.Replace("%", " ");
            akt = akt.Replace(",", "','");
            akt = "'" + akt + "'";

            decimal t1 = 0;

            string strSql = "SELECT"
                + " NoReservasi"
                + ",NoUrut"
                + ",Tgl"
                + ",TglExpire"
                + ",MS_RESERVASI.Status"
                + ",MS_RESERVASI.NoUnit"
                + ",MS_CUSTOMER.Nama AS Cs"
                + ",MS_AGENT.Nama AS Ag"
                + ",MS_AGENT.Principal"
                + ",MS_RESERVASI.Skema"
                + ",NilaiReservasi"
                + ",NoQueue"
                + " FROM MS_RESERVASI INNER JOIN MS_CUSTOMER ON MS_RESERVASI.NoCustomer = MS_CUSTOMER.NoCustomer"
                + " INNER JOIN MS_AGENT ON MS_RESERVASI.NoAgent = MS_AGENT.NoAgent"
                + " INNER JOIN MS_KONTRAK ON MS_RESERVASI.NoStock = MS_KONTRAK.NoStock"
                + " WHERE 1=1"
                + " AND CONVERT(varchar," + tgl + ",112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND CONVERT(varchar," + tgl + ",112) <= '" + Cf.Tgl112(Sampai) + "'"
                //+ " AND Jenis IN (" + Rpt.inSql(jenis) + ")"
                + nProject
                + nPerusahaan
                + Lokasi
                + nNoUrut
                + nStatus
                + Agent
                + " ORDER BY NoReservasi";

            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                r.VerticalAlign = VerticalAlign.Top;
                r.Attributes["ondblclick"] = "popEditReservasi('" + rs.Rows[i]["NoReservasi"] + "')";

                c = new TableCell();
                c.Text = rs.Rows[i]["NoReservasi"].ToString().PadLeft(5, '0');
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Status"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["Tgl"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Cs"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Ag"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Principal"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoUnit"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoUrut"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Date(rs.Rows[i]["TglExpire"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoQueue"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Skema"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["NilaiReservasi"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                rpt.Rows.Add(r);

                t1 = t1 + (decimal)rs.Rows[i]["NilaiReservasi"];

                if (i == rs.Rows.Count - 1)
                    SubTotal("GRAND TOTAL", t1);
            }
        }

        private void SubTotal(string txt, decimal t1)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = Rpt.Foot();
            c.Text = txt;
            c.ColumnSpan = 11;
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t1);
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

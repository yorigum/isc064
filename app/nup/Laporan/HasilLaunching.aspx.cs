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

namespace ISC064.NUP.Laporan
{
    /// <summary>
    /// Summary description for LaporanSalesPerformance.
    /// </summary>
    public partial class HasilLaunching : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();

            if (!Page.IsPostBack)
            {
                comp.InnerHtml = Mi.Pt;
                rpt.Visible = false;
                rptTotal.Visible = false;
                Js.Focus(this, scr);
                if (!Act.Sec("DownloadExcel")) xls.Enabled = false;

                tglawal.Text = Cf.Day(Cf.AwalBulan(DateTime.Now.Month, DateTime.Now.Year));
                tglakhir.Text = Cf.Day(Cf.AkhirBulan(DateTime.Now.Month, DateTime.Now.Year));
            }

            Bind();
        }

        protected void Bind()
        {
            tipepro.Items.Add("SEMUA");
            DataTable rs = Db.Rs("SELECT Distinct Tipe FROM MS_NUP");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["Tipe"].ToString();
                tipepro.Items.Add(v);
            }
        }

        protected int UserAgent()
        {
            int a = Db.SingleInteger("SELECT NoAgent FROM " + Mi.DbPrefix + "SECURITY..USERNAME WHERE UserID = '" + Act.UserID + "'");

            return a;
        }

        private bool valid()
        {
            string s = "";
            bool x = true;

            if (!Cf.isTgl(tglawal) || !Cf.isTgl(tglakhir))
            {
                x = false;
                tglc.Text = "Format salah";
            }
            else
                tglc.Text = "";

            if (Cf.isEmpty(tglawal) || Cf.isEmpty(tglakhir))
            {
                x = false;
                tglc.Text = "Kosong";
            }
            else
                tglc.Text = "";

            if (!x && s != "")
            {
                RegisterStartupScript("err"
                    , "<script language='javascript'>document.getElementById('" + s + "').select()</script>");
            }

            return x;
        }

        protected void scr_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                Report();
            }
        }

        protected void xls_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                Report();
                Rpt.ToExcel(this, rpt);
            }
        }

        private void Report()
        {
            param.Visible = false;
            rpt.Visible = true;
            rptTotal.Visible = true;

            lblHeader.Text = Mi.Pt
                + "<br />"
                + "Laporan Hasil Launching NUP"
                ;

            System.Text.StringBuilder x = new System.Text.StringBuilder();

            string addStrAktivasi = "Semua";
            string addStrStatus = "Semua";
            string addStrProject = "Semua Project";

            //aktivasi
            if (aktivasi.SelectedIndex == 1)
            {
                addStrAktivasi = " Aktif";
            }
            else if (aktivasi.SelectedIndex == 2)
            {
                addStrAktivasi = " Non Aktif";
            }

            //status
            if (status.SelectedValue == "PilihUnit")
            {
                addStrStatus = " Pilih Unit";
            }
            else if (status.SelectedValue == "SudahClosing")
            {
                addStrStatus = " Sudah Closing";
            }
            else if (status.SelectedValue == "SudahBayar")
            {
                addStrStatus = " Sudah Bayar";
            }

            x.Append("<br />Tgl. Launching : " + Cf.Day(tglawal.Text) + " s/d " + Cf.Day(tglakhir.Text));
            x.Append("<br />Untuk Aktivasi : " + Cf.Str(addStrAktivasi));
            x.Append("<br />Untuk Status : " + Cf.Str(addStrStatus));
            x.Append("<br />Untuk Project : " + Cf.Str(addStrProject));

            x.Append("<br /><span style='font-weight: normal;'>Laporan dihasilkan pada hari : " + Cf.IndoWeek(DateTime.Today)
                + ", " + Cf.Date(DateTime.Now)
                + " dari workstation : " + Act.IP
                + " dan username : " + Act.UserID
                + "</span>"
                );

            lblSubHeader.Text = x.ToString();
            Fill();
        }

        private void Fill()
        {
            DateTime Tanggal1 = Convert.ToDateTime(tglawal.Text);
            DateTime Tanggal2 = Convert.ToDateTime(tglakhir.Text);

            string Aktivasi = "";
            string Status = "";
            string Tipe = "";

            decimal t1 = 0;
            decimal t2 = 0;
            decimal t3 = 0;
            decimal t4 = 0;
            decimal t5 = 0;

            //status
            if (status.SelectedValue == "PilihUnit")
            {
                Status = " AND Status = '3'";
            }
            else if (status.SelectedValue == "SudahClosing")
            {
                Status = " AND Status = '4' AND NoNUP IN (select NoNUP from MS_NUP_PRIORITY where NoKontrak != '')";
            }
            else if (status.SelectedValue == "SudahBayar")
            {
                Status = " AND Status = '4' AND NoNUP IN (select NoNUP from MS_NUP_PRIORITY where NoKontrak = '')";
            }

            //Aktivasi
            if (aktivasi.SelectedIndex == 1)
            {
                Aktivasi = " AND TglAktivasi is not null";
            }
            if (aktivasi.SelectedIndex == 2)
            {
                Aktivasi = " AND TglAktivasi is null";
            }

            //Tipe Properti
            if (tipepro.SelectedIndex > 0)
            {
                Tipe = " AND Tipe = '" + tipepro.SelectedValue + "'";
            }

            //Fill Table Total
            TableRow r2 = new TableRow();
            TableCell c2;

            r2.VerticalAlign = VerticalAlign.Top;

            //Nama Project
            c2 = new TableCell();
            c2.Text = Db.SingleString("select ISNULL(Nama, '') from " + Mi.DbPrefix + "SECURITY..REF_PROJECT where Project = 'SVS'"); //hardcode project -- Cf.Str(Mi.Pt);
            c2.HorizontalAlign = HorizontalAlign.Left;
            c2.Wrap = false;
            r2.Cells.Add(c2);

            //jumlah NUP
            c2 = new TableCell();
            c2.Text = Cf.Num(Db.SingleInteger("select count(*) from MS_NUP"));
            c2.HorizontalAlign = HorizontalAlign.Left;
            c2.Wrap = false;
            r2.Cells.Add(c2);

            //Nup Tidak Aktif
            c2 = new TableCell();
            c2.Text = Cf.Num(Db.SingleInteger("select count(*) from MS_NUP where Status = 0"));
            c2.HorizontalAlign = HorizontalAlign.Left;
            c2.Wrap = false;
            r2.Cells.Add(c2);

            //NUP Aktif
            c2 = new TableCell();
            c2.Text = Cf.Num(Db.SingleInteger("select count(*) from MS_NUP where Status != 0"));
            c2.HorizontalAlign = HorizontalAlign.Left;
            c2.Wrap = false;
            r2.Cells.Add(c2);

            //Tidak Pilih Unit
            c2 = new TableCell();
            c2.Text = Cf.Num(Db.SingleInteger("select COUNT(*) from MS_NUP where NoNUP NOT IN (SELECT NoNUP FROM MS_NUP_PRIORITY)"));
            c2.HorizontalAlign = HorizontalAlign.Left;
            c2.Wrap = false;
            r2.Cells.Add(c2);

            //Pilih Unit
            c2 = new TableCell();
            c2.Text = Cf.Num(Db.SingleInteger("select count(*) from MS_NUP_PRIORITY where NoStock != ''"));
            c2.HorizontalAlign = HorizontalAlign.Left;
            c2.Wrap = false;
            r2.Cells.Add(c2);

            //Nilai Pembayaran
            c2 = new TableCell();
            decimal nBayar2 = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiBayar),0) FROM MS_NUP_PELUNASAN");
            c2.Text = Cf.Num(nBayar2);
            c2.HorizontalAlign = HorizontalAlign.Right;
            c2.Wrap = false;
            r2.Cells.Add(c2);

            rptTotal.Rows.Add(r2);


            //fill table NUP lengkap
            string strSql = "SELECT * FROM MS_NUP WHERE 1=1"
                    + " AND CONVERT(DATETIME,TglDaftar,112) BETWEEN '" + Cf.Tgl112(Tanggal1) + "' AND '" + Cf.Tgl112(Tanggal2) + "'"
                    + Aktivasi
                    + Status
                    + Tipe
                    + " ORDER BY Tipe DESC, NoNUP ASC";

            DataTable dtNUP = Db.Rs(strSql);
            Rpt.NoData(rpt, dtNUP, "Tidak ada data NUP yang terdaftar.");

            for (int i = 0; i < dtNUP.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                r.VerticalAlign = VerticalAlign.Top;

                //NoNUP
                c = new TableCell();
                string cetakn = dtNUP.Rows[i]["NoNUP"].ToString();

                if (Convert.ToInt32(dtNUP.Rows[i]["Revisi"].ToString()) > 0)
                    cetakn = cetakn + "R";
                c.Text = Cf.Str(cetakn);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                //Nama Customer
                c = new TableCell();
                c.Text = Db.SingleString("select ISNULL(Nama,' ') from ms_customer where NoCustomer = '" + Convert.ToInt32(dtNUP.Rows[i]["NoCustomer"]) + "'");
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                //Tipe Agent
                int kodeTipeAgent = Db.SingleInteger("select ISNULL(SalesTipe, 0) from MS_AGENT WHERE NoAgent = '" + Convert.ToInt32(dtNUP.Rows[i]["NoAgent"]) + "'");
                c = new TableCell();
                c.Text = Db.SingleString("select ISNULL(Tipe, '') from REF_AGENT_TIPE WHERE ID = '" + kodeTipeAgent + "'");
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                //Nama Agent
                c = new TableCell();
                c.Text = Db.SingleString("select ISNULL(Nama,' ') from MS_AGENT WHERE NoAgent = '" + Convert.ToInt32(dtNUP.Rows[i]["NoAgent"]) + "'");
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                //Tipe
                c = new TableCell();
                c.Text = Cf.Str(dtNUP.Rows[i]["Tipe"].ToString());
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                //Tgl Aktivasi
                c = new TableCell();
                c.Text = Cf.DayIndo(dtNUP.Rows[i]["TglAktivasi"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                //// cek apakah ada si nup priority
                int adapriority = Db.SingleInteger("SELECT COUNT(*) FROM MS_NUP_PRIORITY WHERE NoNUP='" + dtNUP.Rows[i]["NoNUP"].ToString() + "' AND Tipe = '" + dtNUP.Rows[i]["Tipe"].ToString() + "'");

                //Unit
                c = new TableCell();
                string noUnit = Db.SingleString("SELECT ISNULL(NoStock,' ') FROM MS_NUP_PRIORITY WHERE NoNUP='" + dtNUP.Rows[i]["NoNUP"].ToString() + "' AND Tipe = '" + dtNUP.Rows[i]["Tipe"].ToString() + "'");
                c.Text = Db.SingleString("select NoUnit from MS_UNIT where NoStock = '" + noUnit + "'");
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                //No. Kontrak
                c = new TableCell();
                string noKontrak = Db.SingleString("SELECT ISNULL(NoKontrak,' ') FROM MS_NUP_PRIORITY WHERE NoNUP='" + dtNUP.Rows[i]["NoNUP"].ToString() + "' AND Tipe = '" + dtNUP.Rows[i]["Tipe"].ToString() + "'");
                c.Text = noKontrak;
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                //Cara Bayar
                c = new TableCell();
                string cb = "";
                if (adapriority > 0)
                {
                    int nomorskema = Db.SingleInteger("SELECT nomorskema FROM MS_NUP_PRIORITY WHERE NoNUP='" + dtNUP.Rows[i]["NoNUP"].ToString() + "' AND Tipe = '" + dtNUP.Rows[i]["Tipe"].ToString() + "'");

                    cb = Db.SingleString("SELECT NAMA FROM REF_SKEMA WHERE NOMOR = " + nomorskema);
                }                 
                c.Text = cb;
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                //Pricelist
                c = new TableCell();
                string pl = "";
                if (adapriority > 0)
                {
                    decimal pls = Db.SingleDecimal("SELECT ISNULL(Harga,' ') FROM MS_NUP_PRIORITY WHERE NoNUP='" + dtNUP.Rows[i]["NoNUP"].ToString() + "' AND Tipe = '" + dtNUP.Rows[i]["Tipe"].ToString() + "'");
                    pl = Cf.Num(pls);
                }                
                c.Text = pl;
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                //Nilai Pembayaran
                c = new TableCell();
                decimal nBayar = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiBayar),0) FROM MS_NUP_PELUNASAN WHERE NoNUP='" + dtNUP.Rows[i]["NoNUP"].ToString() + "' AND Tipe = '" + dtNUP.Rows[i]["Tipe"].ToString() + "'");
                c.Text = Cf.Num(nBayar);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                //Status
                c = new TableCell();
                if (Convert.ToInt32(dtNUP.Rows[i]["Status"]) == 0)
                {
                    c.Text = "Belum Aktivasi";
                }
                else if (Convert.ToInt32(dtNUP.Rows[i]["Status"]) == 1)
                {
                    c.Text = "Aktivasi";
                }
                else if (Convert.ToInt32(dtNUP.Rows[i]["Status"]) == 3)
                {
                    c.Text = "Pilih Unit";
                }
                else if (Convert.ToInt32(dtNUP.Rows[i]["Status"]) == 4)
                {
                    int countKontrak = Db.SingleInteger("select count(*) from ms_nup_priority where NoNUP = '" + Cf.Str(dtNUP.Rows[i]["NoNUP"]) + "' and NoKontrak != ''");
                    if (countKontrak != 0)
                    {
                        c.Text = "Sudah Closing";
                    }
                    else
                    {
                        c.Text = "Sudah Bayar";
                    }
                }
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                decimal totalNUP = Db.SingleInteger("select count(*) from ms_nup where NoNUP = '" + Cf.Str(dtNUP.Rows[i]["NoNUP"]) + "' AND Tipe = '" + Cf.Str(dtNUP.Rows[i]["Tipe"]) + "'");
                decimal totalAktivasi = Db.SingleInteger("select count(*) from ms_nup where NoNUP = '" + Cf.Str(dtNUP.Rows[i]["NoNUP"]) + "' AND Tipe = '" + Cf.Str(dtNUP.Rows[i]["Tipe"]) + "' and TglAktivasi is not null");
                decimal totalPilihUnit = Db.SingleInteger("select count(*) from ms_nup where NoNUP = '" + Cf.Str(dtNUP.Rows[i]["NoNUP"]) + "' AND Tipe = '" + Cf.Str(dtNUP.Rows[i]["Tipe"]) + "' and NoNUP IN (select NoNUP from MS_NUP_PRIORITY where NoKontrak = '')");
                decimal totalClosing = Db.SingleInteger("select count(*) from ms_nup where NoNUP = '" + Cf.Str(dtNUP.Rows[i]["NoNUP"]) + "' AND Tipe = '" + Cf.Str(dtNUP.Rows[i]["Tipe"]) + "' and NoNUP IN (select NoNUP from MS_NUP_PRIORITY where NoKontrak != '')");

                t1 = t1 + nBayar;
                t2 += totalNUP;
                t3 += totalAktivasi;
                t4 += totalPilihUnit;
                t5 += totalClosing;

                rpt.Rows.Add(r);

                if (i == dtNUP.Rows.Count - 1)
                {
                    SubTotal("TOTAL", t1, t2, t3, t4, t5);
                }
            }
        }

        private void SubTotal(string txt, decimal t1, decimal t2, decimal t3, decimal t4, decimal t5)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = Rpt.Foot();
            c.Text = txt;
            c.ColumnSpan = 4;
            c.HorizontalAlign = HorizontalAlign.Center;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t2);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t3);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t5);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = ""; /*Cf.Num(t4)*/
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = "&nbsp;";
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = "&nbsp;";
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t1);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = "&nbsp;";
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

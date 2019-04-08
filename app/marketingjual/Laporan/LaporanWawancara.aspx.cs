using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL.Laporan
{
    public partial class Laporan_LaporanWawancara : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();

            if (!Page.IsPostBack)
            {
                //comp.InnerHtml = Mi.Pt;
                rpt.Visible = false;
                Js.Focus(this, scr);
                init();
                if (!Act.Sec("DownloadExcel")) xls.Enabled = false;
            }
        }
        private void init()
        {
            dari.Text = Cf.Day(Cf.AwalBulan());
            sampai.Text = Cf.Day(Cf.AkhirBulan());

            DataTable rs = Db.Rs("SELECT DISTINCT BankKPR FROM MS_KONTRAK");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                rekening.Items.Add(new ListItem(rs.Rows[i]["BankKPR"].ToString()));
            }

            //rs = Db.Rs("SELECT DISTINCT MS_KONTRAK.Lokasi FROM MS_KONTRAK INNER JOIN MS_UNIT "
            //    + "ON MS_KONTRAK.NoUnit = MS_UNIT.NoUnit "
            //    + "WHERE MS_UNIT.PT in " + Act.PT + " "
            //    + "ORDER BY MS_KONTRAK.Lokasi");
            //if (rs.Rows.Count == 4)
            //{
            //    lokasi.Items.Add(new ListItem("SEMUA"));
            //}
            rs = Db.Rs("SELECT DISTINCT Lokasi FROM MS_KONTRAK ORDER BY Lokasi");
            for (int i = 0; i < rs.Rows.Count; i++)
                lokasi.Items.Add(new ListItem(rs.Rows[i][0].ToString()));
            //for (int i = 0; i < rs.Rows.Count; i++)
            //    lokasi.Items.Add(new ListItem(rs.Rows[i][0].ToString()));
            //lokasi.SelectedIndex = 0;

            //rs = Db.Rs("SELECT PT, Nama FROM REF_PT ORDER BY PT");
            //for (int i = 0; i < rs.Rows.Count; i++)
            //    pt.Items.Add(new ListItem(rs.Rows[i]["Nama"].ToString(), rs.Rows[i]["PT"].ToString()));

            //prj.Visible = false;
            //clsr.Visible = false;
        }
        private bool valid()
        {
            string s = "";
            bool x = true;

            if (!Cf.isTgl(dari))
            {
                x = false;
                if (s == "") s = dari.ID;
                daric.Text = "Tanggal";
            }
            else
                daric.Text = "";

            if (!Cf.isTgl(sampai))
            {
                x = false;
                if (s == "") s = sampai.ID;
                sampaic.Text = "Tanggal";
            }
            else
                sampaic.Text = "";

            if (!x && s != "")
            {
                RegisterStartupScript("err"
                    , "<script language='javascript'>document.getElementById('" + s + "').select()</script>");
            }

            return x;
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



            string tgl = "";
            if (tbTarget.Checked) tgl = tbTarget.Text;
            if (tbTgl.Checked) tgl = tbTgl.Text;

            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);
            Rpt.SubJudul(x
                , tgl + " : " + Cf.Day(dari.Text) + " s/d " + Cf.Day(sampai.Text)
                );

            Rpt.SubJudul(
                    x, "Status Wawancara : " + ddlStatus.SelectedItem.Text
                    );

            if (rekening.SelectedIndex == 0)
                Rpt.SubJudul(x, "Rekening Bank: SEMUA");
            else
                Rpt.SubJudul(x, "Rekening Bank: " + rekening.SelectedValue);

            Rpt.Header(rpt, x);
        }

        private void Fill()
        {
            //string w = "";

            string Lokasi = "";
            if (lokasi.SelectedValue != "SEMUA")
                Lokasi += " AND a.Lokasi = '" + lokasi.SelectedValue + "'";

            string tgl = "";
            if (tbTarget.Checked)
                tgl = "TargetWawancara";

            if (tbTgl.Checked)
                tgl = "TglWawancara";

            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);
            if (Dari > Sampai)
            {
                DateTime x = Sampai;
                Sampai = Dari;
                Dari = x;
            }

            string Status = "";
            if (ddlStatus.SelectedIndex != 0)
            {
                if (ddlStatus.SelectedItem.Text == "BELUM DITENTUKAN")
                    Status = " AND StatusWawancara = ''";
                else
                    Status = " AND StatusWawancara = '" + ddlStatus.SelectedItem.Text + "'";
            }

            string Tanggal = "";
            Tanggal = " AND CONVERT(varchar," + tgl + ",112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND CONVERT(varchar," + tgl + ",112) <= '" + Cf.Tgl112(Sampai) + "'";

            string BankKPR = "";
            if (rekening.SelectedIndex != 0)
                BankKPR = " AND BankKPR = '" + rekening.SelectedValue + "'";



            string strSql = "SELECT a.*, b.Nama AS NamaCustomer"
                    + " FROM MS_KONTRAK a"
                    + " INNER JOIN MS_CUSTOMER b ON a.NoCustomer = b.NoCustomer"
                    + " INNER JOIN MS_UNIT d ON a.NoUnit = d.NoUnit "
                    + " WHERE a.Status = 'A'"
                    + " AND CaraBayar = 'KPA'"
                    //+ w
                    + Lokasi
                    + Tanggal
                    + Status
                    + BankKPR
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
                c.Text = rs.Rows[i]["BankKPR"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["StatusWawancara"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TargetWawancara"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglWawancara"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["LokasiWawancara"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                PotensiKPR = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiTagihan), 0) FROM MS_TAGIHAN WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' AND Tipe = 'ANG' AND (NamaTagihan LIKE '%KPA%' OR NamaTagihan LIKE '%AKAD%')");
                c.Text = Cf.Num(PotensiKPR);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

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
            c.ColumnSpan = 9;
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = "<strong>" + Cf.Num(t) + "</strong>";
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            rpt.Rows.Add(r);
        }
        protected void scr_Click(object sender, EventArgs e)
        {
            if (valid())
            {
                Report();
            }
        }
        protected void xls_Click(object sender, EventArgs e)
        {
            if (valid())
            {
                Report();
                Rpt.ToExcel(this, rpt);
            }
        }
    }
}
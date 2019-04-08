using System;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using System.Drawing;
using System.Diagnostics;

namespace ISC064.COLLECTION.Laporan
{
    public partial class DaftarTagihan : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();

            if (!Page.IsPostBack)
            {
                Act.ProjectList(project);
                Act.PersList(pers);
                comp.InnerHtml = Mi.Pt;
                rpt.Visible = false;
                Js.Focus(this, scr);
                init();
                if (!Act.Sec("DownloadExcel")) xls.Enabled = false;
            }
        }

        protected int UserAgent()
        {
            int a = Db.SingleInteger("SELECT NoAgent FROM " + Mi.DbPrefix + "SECURITY..USERNAME WHERE UserID = '" + Act.UserID + "'");

            return a;
        }

        private void init()
        {
            tgl.Text = Cf.Day(DateTime.Now);
            string Project = project.SelectedValue == "SEMUA" ? "Project IN (" + Act.ProjectListSql + ")" : "Project = '" + project.SelectedValue + "'";
            DataTable rs = Db.Rs("SELECT DISTINCT Lokasi FROM ISC064_MARKETINGJUAL..MS_KONTRAK WHERE " + Project + " ORDER BY Lokasi ");
            for (int i = 0; i < rs.Rows.Count; i++)
                lokasi.Items.Add(new ListItem(rs.Rows[i][0].ToString()));

            lokasi.SelectedIndex = 0;
        }

        private bool valid()
        {
            string s = "";
            bool x = true;

            if (!Cf.isTgl(tgl))
            {
                x = false;
                if (s == "") s = tgl.ID;
                tglc.Text = "Tanggal";
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

        private void Report()
        {
            param.Visible = false;
            rpt.Visible = true;

            System.Text.StringBuilder x = new System.Text.StringBuilder();

            Rpt.Judul(x, comp, judul);

            if (lokasi.SelectedIndex > 0)
                Rpt.SubJudul(x, " Lokasi <b style='padding-left:38px'>:</b> " + lokasi.SelectedValue);
            else
                Rpt.SubJudul(x, " Lokasi <b style='padding-left:38px'>:</b> SEMUA");

            if (include.Checked)
                Rpt.SubJudul(x, " Biaya Admin : " + include.Text);
            else
                Rpt.SubJudul(x, " Biaya Admin : " + exclude.Text);
            Rpt.SubJudul(x, " As Of <b style='padding-left:44px'>:</b> " + tgl.Text);
            Rpt.SubJudul(x, " Perusahaan <b style='padding-left:4px'>:</b> " + pers.SelectedItem.Text);
            Rpt.SubJudul(x, " Project <b style='padding-left:30px'>:</b> " + project.SelectedValue);
            string legend = "<br />Status : A = Aktif / B = Batal.<br />";
            Rpt.HeaderReport(headReport, legend, x);

            Fill();
        }

        private void Fill()
        {
            string strAdd = "";

            if (lokasi.SelectedIndex > 0)
                strAdd += " AND a.Lokasi = '" + lokasi.SelectedValue + "'";

            string Project = " AND a.Project IN (" + Act.ProjectListSql + ")";
            if (project.SelectedValue != "SEMUA") Project = " AND a.Project = '" + project.SelectedValue + "'";
            string Perusahaan = "";
            if (pers.SelectedValue != "SEMUA") Perusahaan = " AND a.Pers = '" + pers.SelectedValue + "'";

            string strSql = "SELECT *, b.Nama AS NamaCustomer"
                + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK a "
                + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER b "
                + " ON a.NoCustomer = b.NoCustomer "
                + " WHERE 1=1 "
                + " AND a.Status = 'A' "
                + " AND CONVERT(varchar,TglKontrak,112) <= '" + Cf.Tgl112(Convert.ToDateTime(tgl.Text)) + "'"
                + Project
                + Perusahaan
                + strAdd
                + " ORDER BY a.NoUnit"
                ;
            DataTable rs = Db.Rs(strSql);

            decimal t1 = 0, t2 = 0, t3 = 0, t4 = 0, t5 = 0, t6 = 0, t7 = 0, t8 = 0, t9 = 0, t10 = 0, t11 = 0, t12 = 0, t13 = 0, t14 = 0;
            decimal tHutangJangkaPendek = 0;
            decimal tHutangJangkaPanjang = 0;
            int index = 1;

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow tr = new TableRow();
                TableCell c;

                rpt.Rows.Add(tr);

                c = new TableCell();
                c.Text = (i + 1).ToString() + ".";
                c.HorizontalAlign = HorizontalAlign.Center;
                tr.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NamaCustomer"].ToString();
                c.Wrap = false;
                c.HorizontalAlign = HorizontalAlign.Left;
                tr.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoUnit"].ToString();
                t1++;
                c.HorizontalAlign = HorizontalAlign.Left;
                tr.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Convert.ToDecimal(rs.Rows[i]["NilaiDPP"]));
                t2 += Convert.ToDecimal(rs.Rows[i]["NilaiDPP"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                tr.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Convert.ToDecimal(rs.Rows[i]["NilaiPPN"]));
                t3 += Convert.ToDecimal(rs.Rows[i]["NilaiPPN"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                tr.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Convert.ToDecimal(rs.Rows[i]["NilaiKontrak"]));
                t4 += Convert.ToDecimal(rs.Rows[i]["NilaiKontrak"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                tr.Cells.Add(c);

                decimal Pembayaran = 0;
                if (include.Checked)
                {
                    Pembayaran = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan),0) FROM "
                        + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN"
                        + " WHERE CONVERT(varchar,TglPelunasan,112) <= '" + Cf.Tgl112(Convert.ToDateTime(tgl.Text)) + "'"
                        + " AND NoKontrak='" + rs.Rows[i]["NoKontrak"] + "'");
                }
                if (exclude.Checked)
                {
                    Pembayaran = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan),0) FROM "
                        + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN a"
                        + " JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN b ON a.NoKontrak = b.NoKontrak AND a.NoTagihan = b.NoUrut "
                        + " WHERE CONVERT(varchar,TglPelunasan,112) <= '" + Cf.Tgl112(Convert.ToDateTime(tgl.Text)) + "'"
                        + " AND b.Tipe != 'ADM'"
                        + " AND a.NoKontrak='" + rs.Rows[i]["NoKontrak"] + "'");
                }

                c = new TableCell();
                c.Text = Cf.Num(Pembayaran);
                t5 += Pembayaran;
                c.HorizontalAlign = HorizontalAlign.Right;
                tr.Cells.Add(c);

                c = new TableCell();
                decimal Piutang = 0;
                if (include.Checked)
                {
                    DataTable rs2 = Db.Rs("SELECT (NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) "
                         + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE NoTagihan = MS_TAGIHAN.NoUrut "
                         + " AND NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "')) As SisaTagihan"
                         + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN AS MS_TAGIHAN WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "'"
                         + " AND (NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE NoTagihan = MS_TAGIHAN.NoUrut "
                         + " AND NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "') ) > 0 ");
                    for (int a = 0; a < rs2.Rows.Count; a++)
                    {
                        Piutang += Convert.ToDecimal(rs2.Rows[a]["SisaTagihan"]);
                    }
                }
                else
                {
                    DataTable rs2 = Db.Rs("SELECT (NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) "
                             + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE NoTagihan = MS_TAGIHAN.NoUrut "
                             + " AND NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "')) As SisaTagihan"
                             + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN AS MS_TAGIHAN WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "'"
                             + " AND Tipe != 'ADM'"
                             + " AND (NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE NoTagihan = MS_TAGIHAN.NoUrut "
                             + " AND NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "') ) > 0 ");
                    for (int a = 0; a < rs2.Rows.Count; a++)
                    {
                        Piutang += Convert.ToDecimal(rs2.Rows[a]["SisaTagihan"]);
                    }
                }

                c.Text = Cf.Num(Piutang);
                t6 += Piutang;
                c.HorizontalAlign = HorizontalAlign.Right;
                tr.Cells.Add(c);

                decimal BiayaAdmin = 0;
                if (include.Checked)
                {
                    tradmin.Visible = true;

                    BiayaAdmin = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiTagihan),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' AND Tipe = 'ADM'");

                    c = new TableCell();
                    c.Text = Cf.Num(BiayaAdmin);
                    c.HorizontalAlign = HorizontalAlign.Right;
                    tr.Cells.Add(c);
                }
                t14 += BiayaAdmin;

                c = new TableCell();
                decimal BelumJatuhTempo = 0;
                if (include.Checked)
                {
                    DataTable rs3 = Db.Rs("SELECT (NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE NoTagihan = MS_TAGIHAN.NoUrut AND NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "') ) AS SisaTagihan"
                        + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN AS MS_TAGIHAN WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "'"
                        + " AND (NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE NoTagihan = MS_TAGIHAN.NoUrut AND NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "') ) > 0 "
                        + " AND TglJT> '" + Cf.Tgl112(Convert.ToDateTime(tgl.Text)) + "' ORDER BY TglJT, NoUrut");

                    for (int a = 0; a < rs3.Rows.Count; a++)
                    {
                        BelumJatuhTempo += Convert.ToDecimal(rs3.Rows[a]["SisaTagihan"]);
                    }
                }
                else
                {
                    DataTable rs3 = Db.Rs("SELECT (NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE NoTagihan = MS_TAGIHAN.NoUrut AND NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "') ) AS SisaTagihan"
                        + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN AS MS_TAGIHAN WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' AND Tipe != 'ADM'"
                        + " AND (NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE NoTagihan = MS_TAGIHAN.NoUrut AND NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "') ) > 0 "
                        + " AND TglJT> '" + Cf.Tgl112(Convert.ToDateTime(tgl.Text)) + "' ORDER BY TglJT, NoUrut");

                    for (int a = 0; a < rs3.Rows.Count; a++)
                    {
                        BelumJatuhTempo += Convert.ToDecimal(rs3.Rows[a]["SisaTagihan"]);
                    }
                }

                t7 += BelumJatuhTempo;
                c.Text = Cf.Num(Math.Round(BelumJatuhTempo, 0));
                c.HorizontalAlign = HorizontalAlign.Right;
                tr.Cells.Add(c);

                c = new TableCell();
                decimal satu = 0;
                if (include.Checked)
                {
                    DataTable rs4 = Db.Rs("SELECT (NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE NoTagihan = MS_TAGIHAN.NoUrut AND NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "') ) AS SisaTagihan"
                        + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN AS MS_TAGIHAN WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' AND (NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE NoTagihan = MS_TAGIHAN.NoUrut AND NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "') ) > 0"
                        + " AND DATEDIFF(day,TglJT,'" + Cf.Tgl112(Convert.ToDateTime(tgl.Text)) + "') between 0 and 30 ORDER BY TglJT, NoUrut");
                    for (int a = 0; a < rs4.Rows.Count; a++)
                    {
                        satu += Convert.ToDecimal(rs4.Rows[a]["SisaTagihan"]);
                    }
                }
                else
                {
                    DataTable rs4 = Db.Rs("SELECT (NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE NoTagihan = MS_TAGIHAN.NoUrut AND NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "') ) AS SisaTagihan"
                        + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN AS MS_TAGIHAN WHERE Tipe != 'ADM' AND NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' AND (NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE NoTagihan = MS_TAGIHAN.NoUrut AND NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "') ) > 0"
                        + " AND DATEDIFF(day,TglJT,'" + Cf.Tgl112(Convert.ToDateTime(tgl.Text)) + "') between 0 and 30 ORDER BY TglJT, NoUrut");
                    for (int a = 0; a < rs4.Rows.Count; a++)
                    {
                        satu += Convert.ToDecimal(rs4.Rows[a]["SisaTagihan"]);
                    }
                }
                t8 += satu;
                c.Text = Cf.Num(Math.Round(satu, 0));
                c.HorizontalAlign = HorizontalAlign.Right;
                tr.Cells.Add(c);

                c = new TableCell();
                decimal dua = 0;
                if (include.Checked)
                {
                    DataTable rs5 = Db.Rs("SELECT (NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE NoTagihan = MS_TAGIHAN.NoUrut AND NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "') ) AS SisaTagihan"
                        + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN AS MS_TAGIHAN WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' AND (NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE NoTagihan = MS_TAGIHAN.NoUrut AND NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "') ) > 0"
                        + " AND DATEDIFF(day,TglJT,'" + Cf.Tgl112(Convert.ToDateTime(tgl.Text)) + "') between 31 and 60 ORDER BY TglJT, NoUrut");
                    for (int a = 0; a < rs5.Rows.Count; a++)
                    {
                        dua += Convert.ToDecimal(rs5.Rows[a]["SisaTagihan"]);
                    }
                }
                else
                {
                    DataTable rs5 = Db.Rs("SELECT (NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE NoTagihan = MS_TAGIHAN.NoUrut AND NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "') ) AS SisaTagihan"
                        + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN AS MS_TAGIHAN WHERE Tipe != 'ADM' AND NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' AND (NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE NoTagihan = MS_TAGIHAN.NoUrut AND NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "') ) > 0"
                        + " AND DATEDIFF(day,TglJT,'" + Cf.Tgl112(Convert.ToDateTime(tgl.Text)) + "') between 31 and 60 ORDER BY TglJT, NoUrut");
                    for (int a = 0; a < rs5.Rows.Count; a++)
                    {
                        dua += Convert.ToDecimal(rs5.Rows[a]["SisaTagihan"]);
                    }
                }
                t9 += dua;
                c.Text = Cf.Num(Math.Round(dua, 0));
                c.HorizontalAlign = HorizontalAlign.Right;
                tr.Cells.Add(c);

                c = new TableCell();
                decimal tiga = 0;
                if (include.Checked)
                {
                    DataTable rs6 = Db.Rs("SELECT (NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE NoTagihan = MS_TAGIHAN.NoUrut AND NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "') ) AS SisaTagihan"
                        + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN AS MS_TAGIHAN WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' AND (NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE NoTagihan = MS_TAGIHAN.NoUrut AND NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "') ) > 0"
                        + " AND DATEDIFF(day,TglJT,'" + Cf.Tgl112(Convert.ToDateTime(tgl.Text)) + "') between 61 and 90 ORDER BY TglJT, NoUrut");
                    for (int a = 0; a < rs6.Rows.Count; a++)
                    {
                        tiga += Convert.ToDecimal(rs6.Rows[a]["SisaTagihan"]);
                    }
                }
                else
                {
                    DataTable rs6 = Db.Rs("SELECT (NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE NoTagihan = MS_TAGIHAN.NoUrut AND NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "') ) AS SisaTagihan"
                        + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN AS MS_TAGIHAN WHERE Tipe != 'ADM' AND NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' AND (NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE NoTagihan = MS_TAGIHAN.NoUrut AND NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "') ) > 0"
                        + " AND DATEDIFF(day,TglJT,'" + Cf.Tgl112(Convert.ToDateTime(tgl.Text)) + "') between 61 and 90 ORDER BY TglJT, NoUrut");
                    for (int a = 0; a < rs6.Rows.Count; a++)
                    {
                        tiga += Convert.ToDecimal(rs6.Rows[a]["SisaTagihan"]);
                    }
                }
                t10 += tiga;
                c.Text = Cf.Num(Math.Round(tiga, 0));
                c.HorizontalAlign = HorizontalAlign.Right;
                tr.Cells.Add(c);

                c = new TableCell();
                decimal empat = 0;
                if (include.Checked)
                {
                    DataTable rs7 = Db.Rs("SELECT (NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE NoTagihan = MS_TAGIHAN.NoUrut AND NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "') ) AS SisaTagihan"
                        + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN AS MS_TAGIHAN WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' AND (NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE NoTagihan = MS_TAGIHAN.NoUrut AND NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "') ) > 0"
                        + " AND DATEDIFF(day,TglJT,'" + Cf.Tgl112(Convert.ToDateTime(tgl.Text)) + "') between 91 and 120 ORDER BY TglJT, NoUrut");
                    for (int a = 0; a < rs7.Rows.Count; a++)
                    {
                        empat += Convert.ToDecimal(rs7.Rows[a]["SisaTagihan"]);
                    }
                }
                else
                {
                    DataTable rs7 = Db.Rs("SELECT (NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE NoTagihan = MS_TAGIHAN.NoUrut AND NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "') ) AS SisaTagihan"
                        + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN AS MS_TAGIHAN WHERE Tipe != 'ADM' AND NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' AND (NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE NoTagihan = MS_TAGIHAN.NoUrut AND NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "') ) > 0"
                        + " AND DATEDIFF(day,TglJT,'" + Cf.Tgl112(Convert.ToDateTime(tgl.Text)) + "') between 91 and 120 ORDER BY TglJT, NoUrut");
                    for (int a = 0; a < rs7.Rows.Count; a++)
                    {
                        empat += Convert.ToDecimal(rs7.Rows[a]["SisaTagihan"]);
                    }
                }
                t11 += empat;
                c.Text = Cf.Num(Math.Round(empat, 0));
                c.HorizontalAlign = HorizontalAlign.Right;
                tr.Cells.Add(c);

                c = new TableCell();
                decimal lima = 0;
                if (include.Checked)
                {
                    DataTable rs8 = Db.Rs("SELECT (NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE NoTagihan = MS_TAGIHAN.NoUrut AND NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "') ) AS SisaTagihan"
                        + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN AS MS_TAGIHAN WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' AND (NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE NoTagihan = MS_TAGIHAN.NoUrut AND NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "') ) > 0"
                        + " AND DATEDIFF(day,TglJT,'" + Cf.Tgl112(Convert.ToDateTime(tgl.Text)) + "') between 121 and 180 ORDER BY TglJT, NoUrut");
                    for (int a = 0; a < rs8.Rows.Count; a++)
                    {
                        lima += Convert.ToDecimal(rs8.Rows[a]["SisaTagihan"]);
                    }
                }
                else
                {
                    DataTable rs8 = Db.Rs("SELECT (NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE NoTagihan = MS_TAGIHAN.NoUrut AND NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "') ) AS SisaTagihan"
                        + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN AS MS_TAGIHAN WHERE Tipe != 'ADM' AND NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' AND (NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE NoTagihan = MS_TAGIHAN.NoUrut AND NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "') ) > 0"
                        + " AND DATEDIFF(day,TglJT,'" + Cf.Tgl112(Convert.ToDateTime(tgl.Text)) + "') between 121 and 180 ORDER BY TglJT, NoUrut");
                    for (int a = 0; a < rs8.Rows.Count; a++)
                    {
                        lima += Convert.ToDecimal(rs8.Rows[a]["SisaTagihan"]);
                    }
                }
                t12 += lima;
                c.Text = Cf.Num(Math.Round(lima, 0));
                c.HorizontalAlign = HorizontalAlign.Right;
                tr.Cells.Add(c);

                c = new TableCell();
                decimal enam = 0;
                if (include.Checked)
                {
                    DataTable rs9 = Db.Rs("SELECT (NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE NoTagihan = MS_TAGIHAN.NoUrut AND NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "') ) AS SisaTagihan"
                        + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN AS MS_TAGIHAN WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' AND (NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE NoTagihan = MS_TAGIHAN.NoUrut AND NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "') ) > 0"
                        + " AND DATEDIFF(day,TglJT,'" + Cf.Tgl112(Convert.ToDateTime(tgl.Text)) + "')>=181 ORDER BY TglJT, NoUrut");
                    for (int a = 0; a < rs9.Rows.Count; a++)
                    {
                        enam += Convert.ToDecimal(rs9.Rows[a]["SisaTagihan"]);
                    }
                }
                else
                {
                    DataTable rs9 = Db.Rs("SELECT (NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE NoTagihan = MS_TAGIHAN.NoUrut AND NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "') ) AS SisaTagihan"
                      + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN AS MS_TAGIHAN WHERE Tipe != 'ADM' AND NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' AND (NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE NoTagihan = MS_TAGIHAN.NoUrut AND NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "') ) > 0"
                      + " AND DATEDIFF(day,TglJT,'" + Cf.Tgl112(Convert.ToDateTime(tgl.Text)) + "')>=181 ORDER BY TglJT, NoUrut");
                    for (int a = 0; a < rs9.Rows.Count; a++)
                    {
                        enam += Convert.ToDecimal(rs9.Rows[a]["SisaTagihan"]);
                    }
                }
                t13 += enam;
                c.Text = Cf.Num(Math.Round(enam, 0));
                c.HorizontalAlign = HorizontalAlign.Right;
                tr.Cells.Add(c);

                decimal HutangJangkaPendek = satu + dua + tiga + empat + lima + enam;
                tHutangJangkaPendek += HutangJangkaPendek;

                c = new TableCell();
                c.Text = Cf.Num(HutangJangkaPendek);
                c.HorizontalAlign = HorizontalAlign.Right;
                tr.Cells.Add(c);

                decimal HutangJangkaPanjang = BelumJatuhTempo;
                tHutangJangkaPanjang += HutangJangkaPanjang;

                c = new TableCell();
                c.Text = Cf.Num(Math.Round(HutangJangkaPanjang, 0));
                c.HorizontalAlign = HorizontalAlign.Right;
                tr.Cells.Add(c);

            }

            Total(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, tHutangJangkaPendek, tHutangJangkaPanjang);
        }
        protected void Total(decimal t1, decimal t2, decimal t3, decimal t4, decimal t5, decimal t6, decimal t7, decimal t8, decimal t9, decimal t10, decimal t11, decimal t12, decimal t13, decimal t14, decimal JangkaPendek, decimal JangkaPanjang)
        {
            TableRow r = new TableRow();
            TableCell c;


            c = new TableCell();
            c.Text = "Total";
            c.HorizontalAlign = HorizontalAlign.Right;
            c.Wrap = false;
            c.ColumnSpan = 2;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = Cf.Num(Math.Round(t1, 0));
            c.HorizontalAlign = HorizontalAlign.Right;
            c.Wrap = false;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = Cf.Num(Math.Round(t2, 0));
            c.HorizontalAlign = HorizontalAlign.Right;
            c.Wrap = false;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = Cf.Num(Math.Round(t3, 0));
            c.HorizontalAlign = HorizontalAlign.Right;
            c.Wrap = false;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = Cf.Num(Math.Round(t4, 0));
            c.HorizontalAlign = HorizontalAlign.Right;
            c.Wrap = false;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = Cf.Num(Math.Round(t5, 0));
            c.HorizontalAlign = HorizontalAlign.Right;
            c.Wrap = false;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = Cf.Num(Math.Round(t6, 0));
            c.HorizontalAlign = HorizontalAlign.Right;
            c.Wrap = false;
            r.Cells.Add(c);

            if (include.Checked)
            {
                c = new TableCell();
                c.Text = Cf.Num(Math.Round(t14, 0));
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);
            }

            c = new TableCell();
            c.Text = Cf.Num(Math.Round(t7, 0));
            c.HorizontalAlign = HorizontalAlign.Right;
            c.Wrap = false;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = Cf.Num(Math.Round(t8, 0));
            c.HorizontalAlign = HorizontalAlign.Right;
            c.Wrap = false;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = Cf.Num(Math.Round(t9, 0));
            c.HorizontalAlign = HorizontalAlign.Right;
            c.Wrap = false;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = Cf.Num(Math.Round(t10, 0));
            c.HorizontalAlign = HorizontalAlign.Right;
            c.Wrap = false;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = Cf.Num(Math.Round(t11, 0));
            c.HorizontalAlign = HorizontalAlign.Right;
            c.Wrap = false;
            r.Cells.Add(c);


            c = new TableCell();
            c.Text = Cf.Num(Math.Round(t12, 0));
            c.HorizontalAlign = HorizontalAlign.Right;
            c.Wrap = false;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = Cf.Num(Math.Round(t13, 0));
            c.HorizontalAlign = HorizontalAlign.Right;
            c.Wrap = false;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = Cf.Num(Math.Round(JangkaPendek, 0));
            c.HorizontalAlign = HorizontalAlign.Right;
            c.Wrap = false;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = Cf.Num(Math.Round(JangkaPanjang, 0));
            c.HorizontalAlign = HorizontalAlign.Right;
            c.Wrap = false;
            r.Cells.Add(c);

            rpt.Rows.Add(r);
        }

        protected void scr_Click(object sender, System.EventArgs e)
        {
            if (valid())
                Report();
        }

        protected void xls_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                Report();
                Rpt.ToExcel(this, headReport, rpt);
            }
        }
        protected void pdf_Click(object sender, System.EventArgs e)
        {
            Process p = new System.Diagnostics.Process();

            string Nama = "Laporan Daftar Tagihan";
            string Link = "";
            DateTime TglGenerate = DateTime.Now;
            string FileName = "";
            string FileType = "application/pdf";
            string UserID = Act.UserID;
            string IP = Act.IP;

            Db.Execute("EXEC ISC064_FINANCEAR..spLapPDFDaftar"

                    + " '" + Nama + "'"
                    + ",'" + Link + "'"
                    + ",'" + TglGenerate + "'"
                    + ",'" + IP + "'"
                    + ",'" + UserID + "'"
                    + ",'" + FileName + "'"
                    + ",'" + FileType + "'"
                    + ",'" + Convert.ToDateTime(tgl.Text) + "'"
                    + ",'" + null + "'"
                    );

            int NoAttachment = Db.SingleInteger(
                "SELECT TOP 1 AttachmentID FROM ISC064_FINANCEAR..LapPDF ORDER BY AttachmentID DESC");

            string strSql = "SELECT * FROM ISC064_FINANCEAR..LapPDF WHERE AttachmentID  = '" + NoAttachment + "'";
            DataTable rs = Db.Rs(strSql);

            string nfilename = "DaftarTagihan" + NoAttachment + ".pdf";

            //update filename
            Db.Execute("UPDATE ISC064_FINANCEAR..LapPDF SET FileName= '" + nfilename + "' WHERE AttachmentID = " + NoAttachment);


            //folder untuk menyimpan file pdf
            string save = Mi.PathFilePDFReport + "DaftarTagihan" + rs.Rows[0]["AttachmentID"] + ".pdf";

            //parameter
            string nbiayaInc = "";
            string nbiayaExc = "";
            if (include.Checked == true)
                nbiayaInc = include.Text;
            else
                nbiayaInc = "";

            if (exclude.Checked == true)
                nbiayaExc = exclude.Text;
            else
                nbiayaExc = "";

            DateTime tanggal = Convert.ToDateTime(tgl.Text);

            string Project = "";
            if (project.SelectedIndex == 0)
            {
                Project = Act.ProjectListSql.Replace("'", "");
            }
            else
            {
                Project = project.SelectedValue;
            }

            //link untuk download pdf
            string link = Mi.PathAlamatWeb + "collection/LaporanPDF/PDFDaftarTagihan.aspx?id=" + rs.Rows[0]["AttachmentID"]
                + "&lokasi=" + lokasi.SelectedValue
                + "&biayaExc=" + nbiayaExc
                + "&biayaInc=" + nbiayaInc
                + "&userid=" + UserID
                + "&project=" + Project
                + "&pers=" + pers.SelectedValue
                + "&asof=" + tgl.Text.Replace(' ', '-');
                ;

            //update link
            Db.Execute("UPDATE ISC064_FINANCEAR..LapPDF SET Link= '" + link + "' WHERE AttachmentID = " + NoAttachment);

            //format page
            p.StartInfo.Arguments = "--orientation landscape --page-width 11in --page-height 16.5in --margin-left 0 --margin-right 0 --margin-top 0.25cm --margin-bottom 0 " + link + " " + save;

            //panggil aplikasi untuk mengconvert pdf
            p.StartInfo.FileName = Mi.PathWkhtmlPDFReport;
            p.Start();

            //60000 -> waktu jeda lama convert pdf
            p.WaitForExit(30000);

            string Src = Mi.PathFilePDFReport + nfilename;
            Mi.DownloadPDF(this, Src, (rs.Rows[0]["FileName"]).ToString(), rs.Rows[0]["FileType"].ToString());

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


        protected void pers_SelectedIndexChanged(object sender, EventArgs e)
        {
            project.Items.Clear();
            lokasi.Items.Clear();
            lokasi.Items.Add(new ListItem("SEMUA"));
            if (pers.SelectedIndex == 0)
            {
                project.Items.Add(new ListItem("SEMUA"));
                Act.ProjectList(project);
            }
            else
                Act.ProjectList(project, pers.SelectedValue);

            init();
        }

        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            lokasi.Items.Clear();
            lokasi.Items.Add(new ListItem("SEMUA"));
            init();
        }
    }
}

using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL.Laporan
{
    public partial class LaporanPPJB : System.Web.UI.Page
    {
        private string UserID { get { return (Request.QueryString["userid"]); } }
        private string Project { get { return (Request.QueryString["project"]); } }
        private string Perusahaan { get { return (Request.QueryString["pers"]); } }
        private string AttachmentID { get { return (Request.QueryString["id"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            Report();
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
                , "Tanggal PPJB : " + Cf.Day(Dari) + " s/d " + Cf.Day(Sampai)
                );
            Rpt.SubJudul(x
                , "Project : " + Project
                );
            Rpt.HeaderReport(headReport, "", x);
        }
        private void Fill()
        {
            rpt.Style["border-collapse"] = "collapse";

            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");

            string nProject = "";
            if (Project != "SEMUA") nProject = " AND MS_KONTRAK.Project IN ('" + Project.Replace(",", "','") + "')";

            string nPerusahaan = "";
            if (Perusahaan != "SEMUA") nPerusahaan = " AND MS_KONTRAK.Pers = '" + Perusahaan + "'";

            string strSql = "SELECT * FROM MS_KONTRAK"
                          + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER  ON MS_CUSTOMER.NoCustomer = MS_KONTRAK.NoCustomer"
                          + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT ON MS_UNIT.NoUnit = MS_KONTRAK.NoUnit"
                          + " WHERE PPJB != 'B'"
                          + " AND CONVERT(varchar,TglPPJB,112) >= '" + Cf.Tgl112(Dari) + "'"
                          + " AND CONVERT(varchar,TglPPJB,112) <= '" + Cf.Tgl112(Sampai) + "'"
                          + nProject + nPerusahaan                        
                          + " ORDER BY MS_KONTRAK.NoPPJB";

            DataTable rs = Db.Rs(strSql);


            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;
                bool a = true;
                bool b = true;
                bool d = true;
                bool e = true;
                bool f = true;
                bool g = true;
                bool h = true;
                bool j = true;
                bool k = true;

                r.Attributes["ondblclick"] = "popEditKontrak('" + rs.Rows[i]["NoKontrak"] + "')";

                c = new TableCell();
                c.Text = (i + 1).ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                c.Text = rs.Rows[i]["Nama"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                string[] x = Cf.SplitByString(rs.Rows[i]["NoUnit"].ToString(), "/");

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                c.Text = rs.Rows[i]["Nomor"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                c.Text = rs.Rows[i]["Lokasi"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                c.Text = rs.Rows[i]["Lantai"].ToString();
                c.Wrap = false;
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                c.Text = Cf.Num(rs.Rows[i]["LuasSG"]) + "m<sup>2</sup>";
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                //    B = BELUM
                //    D = SUDAH REGIS
                //    T = PROSES TTD
                //    S = SELESAI

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                if (rs.Rows[i]["PPJB"].ToString() == "D")
                {
                    c.Text = "Teregister";
                }
                else if (rs.Rows[i]["PPJB"].ToString() == "T")
                {
                    c.Text = "Proses Tanda Tangan";
                }
                else if (rs.Rows[i]["PPJB"].ToString() == "S")
                {
                    c.Text = "Selesai";
                }
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);


                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                if (rs.Rows[i]["TglPPJB"] != DBNull.Value)
                {
                    c.Text = Cf.Day(Convert.ToDateTime(rs.Rows[i]["TglPPJB"]));
                }
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);


                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                if (rs.Rows[i]["TglCetakPPJB"] != DBNull.Value)
                {
                    c.Text = Cf.Day(Convert.ToDateTime(rs.Rows[i]["TglCetakPPJB"]));
                }
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                if (rs.Rows[i]["TglTTDPPJB"] != DBNull.Value)
                {
                    c.Text = Cf.Day(Convert.ToDateTime(rs.Rows[i]["TglTTDPPJB"]));
                }
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                c.Text = rs.Rows[i]["NoPPJB"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                c.Text = rs.Rows[i]["NoPPJBm"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                if (rs.Rows[i]["PPJBu"].ToString() == "1")
                {
                    c.Text = "Manual";
                }
                else
                {
                    c.Text = "System";
                }
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);


                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                c.Text = rs.Rows[i]["NoKTP"].ToString() + " ";
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                if (rs.Rows[i]["KTPMilik"].ToString() == "0")
                {
                    c.Text = "Tidak Ada";
                }
                else
                {
                    c.Text = "Ada";
                }
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                if (rs.Rows[i]["KTPIstri"].ToString() == "1")
                {
                    c.Text = "Ada";
                }
                else
                {
                    c.Text = "Tidak Ada";
                }
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);



                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                if (rs.Rows[i]["KK"].ToString() == "0")
                {
                    c.Text = "Tidak Ada";
                    a = false;
                }
                else if (rs.Rows[i]["KK"].ToString() == "1")
                {
                    c.Text = "Ada";
                }
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                r.Cells.Add(c);

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                if (rs.Rows[i]["SNKH"].ToString() == "0")
                {
                    c.Text = "Tidak Ada";
                    b = false;
                }
                else if (rs.Rows[i]["SNKH"].ToString() == "1")
                {
                    c.Text = "Ada";
                }
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                if (rs.Rows[i]["SKK"].ToString() == "0")
                {
                    c.Text = "Tidak Ada";
                    k = false;
                }
                else if (rs.Rows[i]["SKK"].ToString() == "1")
                {
                    c.Text = "Ada";
                }
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                if (rs.Rows[i]["RK"].ToString() == "0")
                {
                    c.Text = "Tidak Ada";
                    d = false;
                }
                else if (rs.Rows[i]["RK"].ToString() == "1")
                {
                    c.Text = "Ada";
                }
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                if (rs.Rows[i]["BT"].ToString() == "0")
                {
                    c.Text = "Tidak Ada";
                    e = false;
                }
                else if (rs.Rows[i]["BT"].ToString() == "1")
                {
                    c.Text = "Ada";
                }
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                if (rs.Rows[i]["KW"].ToString() == "0")
                {
                    c.Text = "Tidak Ada";
                    f = false;
                }
                else if (rs.Rows[i]["KW"].ToString() == "1")
                {
                    c.Text = "Ada";
                }
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                c.Text = rs.Rows[i]["NPWP"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                c.Text = rs.Rows[i]["NoKontrak"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);


                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                if (rs.Rows[i]["DU"].ToString() == "0")
                {
                    c.Text = "Tidak Ada";
                    g = false;
                }
                else if (rs.Rows[i]["DU"].ToString() == "1")
                {
                    c.Text = "Ada";
                }
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                if (rs.Rows[i]["DL"].ToString() == "0")
                {
                    c.Text = "Tidak Ada";
                    h = false;
                }
                else if (rs.Rows[i]["DL"].ToString() == "1")
                {
                    c.Text = "Ada";
                }
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                if (rs.Rows[i]["SM"].ToString() == "0")
                {
                    c.Text = "Tidak Ada";
                    j = false;
                }
                else if (rs.Rows[i]["SM"].ToString() == "1")
                {
                    c.Text = "Ada";
                }
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                bool z = a & b & d & e & f & g & h & j & k;
                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                if (z == false)
                {
                    c.Text = "Tidak Lengkap";
                }
                else if (z == true)
                {
                    c.Text = "Lengkap";
                }
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                string ker = "";
                if (a == false)
                {
                    ker += "Kartu Keluarga,";
                }
                if (b == false)
                {
                    ker += "Surat Nikah,";
                }
                if (k == false)
                {
                    ker += "SKK,";
                }
                if (d == false)
                {
                    ker += "RK,";
                }
                if (e == false)
                {
                    ker += "BT,";
                }
                if (f == false)
                {
                    ker += "KW,";
                }
                if (g == false)
                {
                    ker += "Denah Unit,";
                }
                if (h == false)
                {
                    ker += "Denah Lantai,";
                }
                if (j == false)
                {
                    ker += "Spesifikasi Material";
                }


                c = new TableCell();
                c.Text = ker;
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.VerticalAlign = VerticalAlign.Top;
                if (rs.Rows[i]["TglLengkapPPJB"] != DBNull.Value)
                {
                    c.Text = Cf.Day(Convert.ToDateTime(rs.Rows[i]["TglLengkapPPJB"]));
                }
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + rs.Rows[i]["Project"].ToString() + "'");
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                rpt.Rows.Add(r);
            }
        }
    }
}

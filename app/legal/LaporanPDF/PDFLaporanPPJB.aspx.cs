using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LEGAL.Laporan
{
    public partial class LaporanPPJB : System.Web.UI.Page
    {
        protected DataTable rs3;
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
            FillColumn();
        }
        private void Header()
        {
            System.Text.StringBuilder x = new System.Text.StringBuilder();
            Rpt.Judul(x, comp, judul);
            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            Rpt.SubJudul(x
                , "Tanggal : " + Cf.Day(Dari) + " s/d " + Cf.Day(Sampai)
                );
            Rpt.SubJudul(x
                , "Project : " + Project
                );
            string pers = (Perusahaan == "SEMUA") ? "SEMUA" : Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PERS WHERE Pers = '" + Perusahaan + "'");
            Rpt.SubJudul(x
                , "Perusahaan : " + pers
                );
            Rpt.HeaderReport(headReport, "", x);
        }
        protected void FillColumn()
        {
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            Label l;
            l = new Label();
            rs3 = Db.Rs("SELECT DISTINCT(Nama) FROM REF_BERKAS_PPJB WHERE Project IN ('" + Project.Replace(",", "','") + "')");
            for (int i = 0; i < rs3.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                l.ID = "nama_" + i;
                s.Append("<th vertical-align=\"middle\" rowspan=\"2\">" + rs3.Rows[i]["Nama"] + "</th>");
                l.Text = s.ToString();
            }
            col1.Controls.Add(l);
        }
        private void Fill()
        {
            //rpt.Style["border-collapse"] = "collapse";
            string nProject = "";
            if (Project != "SEMUA") nProject = " AND A.Project IN ('" + Project.Replace(",", "','") + "')";
            string nPerusahaan = "";
            if (Perusahaan != "SEMUA") nPerusahaan = " AND A.Pers = '" + Perusahaan + "'";

            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");


            //string strSql = "SELECT * FROM MS_KONTRAK"
            //              + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER  ON MS_CUSTOMER.NoCustomer = MS_KONTRAK.NoCustomer"
            //              + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT ON MS_UNIT.NoUnit = MS_KONTRAK.NoUnit"
            //              + " WHERE PPJB != 'B'"
            //              + " AND CONVERT(varchar,TglPPJB,112) >= '" + Cf.Tgl112(Dari) + "'"
            //              + " AND CONVERT(varchar,TglPPJB,112) <= '" + Cf.Tgl112(Sampai) + "'"
            //              + " ORDER BY MS_KONTRAK.NoPPJB";

            //DataTable rs = Db.Rs(strSql);

            string strSql = "SELECT A.*,B.*,C.*,D.*, D.NoPPJB AS NomorPPJB "
                          + ", D.NoPPJBm AS NomorPPJBm"
                          + " FROM MS_KONTRAK A "
                          + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER B  ON B.NoCustomer = A.NoCustomer"
                          + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT C ON C.NoUnit = A.NoUnit"
                          + " LEFT JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_PPJB D ON A.NoKontrak = D.NoKontrak"
                          + " WHERE D.PPJB != 'B'"
                          + " AND CONVERT(varchar,D.TglPPJB,112) >= '" + Cf.Tgl112(Dari) + "'"
                          + " AND CONVERT(varchar,D.TglPPJB,112) <= '" + Cf.Tgl112(Sampai) + "'"
                          + nProject
                          + nPerusahaan
                          + " ORDER BY D.NoPPJB";

            DataTable rs = Db.Rs(strSql);

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                Label l;
                Label l2;
                Label rb;
                TextBox t;
                TextBox tgl;
                HtmlInputButton btn;

                l = new Label();
                l2 = new Label();
                string berkas = "";
                string kurang = "";

                string PPJBu;
                if (rs.Rows[i]["PPJBu"].ToString() == "1")
                {
                    PPJBu = "Manual";
                }
                else
                {
                    PPJBu = "System";
                }

                l.Text = "<tr>"
                    + "<td>" + (i + 1).ToString() + "</td>"
                    + "<td align='right'>" + (rs.Rows[i]["Nama"]) + "</td>"
                    + "<td align='right'>" + (rs.Rows[i]["NoUnit"]) + "</td>"
                    + "<td align='right'>" + (rs.Rows[i]["Nomor"]) + "</td>"
                    + "<td align='right'>" + (rs.Rows[i]["Lantai"]) + "</td>"
                    + "<td align='right'>" + (rs.Rows[i]["LuasSG"]) + "</td>"
                    + "<td align='right'>" + (rs.Rows[i]["PPJB"]) + "</td>"
                    + "<td align='right'>" + (Cf.Day(rs.Rows[i]["TglPPJB"])) + "</td>"
                    + "<td align='right'>" + (rs.Rows[i]["TglCetakPPJB"]) + "</td>"
                    + "<td align='right'>" + (rs.Rows[i]["TglTTDPPJB"]) + "</td>"
                    + "<td align='right'>" + (rs.Rows[i]["NomorPPJB"]) + "</td>"
                    + "<td align='right'>" + (rs.Rows[i]["NomorPPJBm"]) + "</td>"
                    + "<td align='right'>" + PPJBu + "</td>";
                //+ "<td>";
                list.Controls.Add(l);

                string tgllengkap = "";
                DataTable rs3 = Db.Rs("SELECT DISTINCT(Nama) FROM REF_BERKAS_PPJB WHERE Project IN ('" + Project.Replace(",", "','") + "')");
                DataTable rs2 = Db.Rs("SELECT * FROM MS_BERKAS_PPJB WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"].ToString() + "'");                
                for (int j = 0; j < rs3.Rows.Count; j++)
                {
                    if (!Response.IsClientConnected) break;

                    l = new Label();
                    l.Text = "<td>";
                    list.Controls.Add(l);

                    string Status = "";                    
                    rb = new Label();
                    int ada = Db.SingleInteger("SELECT COUNT(*) FROM MS_BERKAS_PPJB WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"].ToString() + "' AND Nama = '" + rs3.Rows[j]["Nama"] + "'");
                    if (rs2.Rows.Count > 0)
                    {
                        rb.Width = 100;
                        //rb.Text = Db.SingleBool("Select ISNULL((SELECT CAST(ISNULL (Value,0) AS bit) From MS_BERKAS_PPJB WHERE NoKontrak =  '" + rs.Rows[i]["NoKontrak"] + "' AND NoBerkas='" + (i + 1) + "'),0)") ? 1 : 0;
                        int pl = Db.SingleBool("Select ISNULL((SELECT CAST(ISNULL (Value,0) AS bit) From MS_BERKAS_PPJB WHERE NoKontrak =  '" + rs.Rows[i]["NoKontrak"] + "' AND Nama='" + rs3.Rows[j]["Nama"] + "'),0)") ? 1 : 0;

                        if (pl == 0)
                            Status = "Tidak Ada";
                        else
                            Status = "Ada";

                        tgllengkap = Cf.Day(rs2.Rows[i]["TglLengkap"]);
                    }
                    else
                    {
                        Status = "Tidak Ada";
                    }
                    rb.Text = Status;
                    list.Controls.Add(rb);
                }

                for (int j = 0; j < rs2.Rows.Count; j++)
                {                    
                    int z = Db.SingleBool("Select ISNULL((SELECT CAST(ISNULL (Value,0) AS bit) From MS_BERKAS_PPJB WHERE NoKontrak =  '" + rs.Rows[i]["NoKontrak"] + "' AND NoBerkas='" + rs2.Rows[j]["NoBerkas"] + "'),0)") ? 0 : 1;
                    if (z == 0)
                    {
                        berkas = "Tidak Lengkap";
                    }
                    else
                    {
                        berkas = "Lengkap";
                    }
                    
                    int a = Db.SingleBool("Select ISNULL((SELECT CAST(ISNULL (Value,0) AS bit) From MS_BERKAS_PPJB WHERE NoKontrak =  '" + rs.Rows[i]["NoKontrak"] + "' AND NoBerkas='" + rs2.Rows[j]["NoBerkas"] + "' AND Nama = '" + rs2.Rows[j]["Nama"] + "'),0)") ? 1 : 0;
                    if (a == 0)
                    {
                        kurang += (rs2.Rows[j]["Nama"]).ToString() + ",";
                    }                    
                }

                if (rs2.Rows.Count == 0)
                {
                    berkas = "Tidak Lengkap";
                    DataTable rs4 = Db.Rs("SELECT Nama From REF_BERKAS_PPJB WHERE Project = (SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "')");
                    for (int m = 0; m < rs4.Rows.Count; m++)
                    {
                        kurang += (rs4.Rows[m]["Nama"]).ToString() + ",";
                    }
                    tgllengkap = "-";
                }

                l = new Label();
                l.Text = "</td>";
                l2.Text = "<td align='right'>" + (rs.Rows[i]["NPWP"]) + "</td>"
                    + "<td align='right'>" + (rs.Rows[i]["NoKontrak"]) + "</td>"
                    + "<td align='right'>" + berkas + "</td>"
                    + "<td align='right'>" + kurang + "</td>"
                    + "<td align='right'>" + tgllengkap + "</td>";
                list.Controls.Add(l2);

                l.Text = "</tr>";
                list.Controls.Add(l);

                //for (int i = 0; i < rs.Rows.Count; i++)
                //{
                //    if (!Response.IsClientConnected) break;

                //    TableRow r = new TableRow();
                //    TableCell c;
                //    bool a = true;
                //    bool b = true;
                //    bool d = true;
                //    bool e = true;
                //    bool f = true;
                //    bool g = true;
                //    bool h = true;
                //    bool j = true;
                //    bool k = true;

                //    r.Attributes["ondblclick"] = "popEditKontrak('" + rs.Rows[i]["NoKontrak"] + "')";

                //    c = new TableCell();
                //    c.Text = (i + 1).ToString();
                //    c.HorizontalAlign = HorizontalAlign.Left;
                //    c.VerticalAlign = VerticalAlign.Top;
                //    c.Wrap = false;
                //    r.Cells.Add(c);

                //    c = new TableCell();
                //    c.VerticalAlign = VerticalAlign.Top;
                //    c.Text = rs.Rows[i]["Nama"].ToString();
                //    c.HorizontalAlign = HorizontalAlign.Left;
                //    c.Wrap = false;
                //    r.Cells.Add(c);

                //    //string[] x = Cf.SplitByString(rs.Rows[i]["NoUnit"].ToString(), "/");

                //    c = new TableCell();
                //    c.VerticalAlign = VerticalAlign.Top;
                //    c.Text = rs.Rows[i]["NoUnit"].ToString();
                //    c.HorizontalAlign = HorizontalAlign.Left;
                //    c.Wrap = false;
                //    r.Cells.Add(c);

                //    c = new TableCell();
                //    c.VerticalAlign = VerticalAlign.Top;
                //    c.Text = rs.Rows[i]["Nomor"].ToString();
                //    c.HorizontalAlign = HorizontalAlign.Left;
                //    c.Wrap = false;
                //    r.Cells.Add(c);

                //    //c = new TableCell();
                //    //c.VerticalAlign = VerticalAlign.Top;
                //    //c.Text = rs.Rows[i]["Lokasi"].ToString();
                //    //c.HorizontalAlign = HorizontalAlign.Left;
                //    //c.Wrap = false;
                //    //r.Cells.Add(c);

                //    c = new TableCell();
                //    c.VerticalAlign = VerticalAlign.Top;
                //    c.Text = rs.Rows[i]["Lantai"].ToString();
                //    c.Wrap = false;
                //    c.HorizontalAlign = HorizontalAlign.Left;
                //    r.Cells.Add(c);

                //    c = new TableCell();
                //    c.VerticalAlign = VerticalAlign.Top;
                //    c.Text = Cf.Num(rs.Rows[i]["LuasSG"]) + "m<sup>2</sup>";
                //    c.HorizontalAlign = HorizontalAlign.Left;
                //    c.Wrap = false;
                //    r.Cells.Add(c);

                //    //    B = BELUM
                //    //    D = SUDAH REGIS
                //    //    T = PROSES TTD
                //    //    S = SELESAI

                //    c = new TableCell();
                //    c.VerticalAlign = VerticalAlign.Top;
                //    if (rs.Rows[i]["PPJB"].ToString() == "D")
                //    {
                //        c.Text = "Teregister";
                //    }
                //    else if (rs.Rows[i]["PPJB"].ToString() == "T")
                //    {
                //        c.Text = "Proses Tanda Tangan";
                //    }
                //    else if (rs.Rows[i]["PPJB"].ToString() == "S")
                //    {
                //        c.Text = "Selesai";
                //    }
                //    c.HorizontalAlign = HorizontalAlign.Left;
                //    c.Wrap = false;
                //    r.Cells.Add(c);


                //    c = new TableCell();
                //    c.VerticalAlign = VerticalAlign.Top;
                //    if (rs.Rows[i]["TglPPJB"] != DBNull.Value)
                //    {
                //        c.Text = Cf.Day(Convert.ToDateTime(rs.Rows[i]["TglPPJB"]));
                //    }
                //    c.HorizontalAlign = HorizontalAlign.Left;
                //    c.Wrap = false;
                //    r.Cells.Add(c);


                //    c = new TableCell();
                //    c.VerticalAlign = VerticalAlign.Top;
                //    if (rs.Rows[i]["TglCetakPPJB"] != DBNull.Value)
                //    {
                //        c.Text = Cf.Day(Convert.ToDateTime(rs.Rows[i]["TglCetakPPJB"]));
                //    }
                //    c.HorizontalAlign = HorizontalAlign.Left;
                //    c.Wrap = false;
                //    r.Cells.Add(c);

                //    c = new TableCell();
                //    c.VerticalAlign = VerticalAlign.Top;
                //    if (rs.Rows[i]["TglTTDPPJB"] != DBNull.Value)
                //    {
                //        c.Text = Cf.Day(Convert.ToDateTime(rs.Rows[i]["TglTTDPPJB"]));
                //    }
                //    c.HorizontalAlign = HorizontalAlign.Left;
                //    c.Wrap = false;
                //    r.Cells.Add(c);

                //    c = new TableCell();
                //    c.VerticalAlign = VerticalAlign.Top;
                //    c.Text = rs.Rows[i]["NoPPJB"].ToString();
                //    c.HorizontalAlign = HorizontalAlign.Left;
                //    c.Wrap = false;
                //    r.Cells.Add(c);

                //    c = new TableCell();
                //    c.VerticalAlign = VerticalAlign.Top;
                //    c.Text = rs.Rows[i]["NoPPJBm"].ToString();
                //    c.HorizontalAlign = HorizontalAlign.Left;
                //    c.Wrap = false;
                //    r.Cells.Add(c);

                //    c = new TableCell();
                //    c.VerticalAlign = VerticalAlign.Top;
                //    if (rs.Rows[i]["PPJBu"].ToString() == "1")
                //    {
                //        c.Text = "Manual";
                //    }
                //    else
                //    {
                //        c.Text = "System";
                //    }
                //    c.HorizontalAlign = HorizontalAlign.Left;
                //    c.Wrap = false;
                //    r.Cells.Add(c);


                //    c = new TableCell();
                //    c.VerticalAlign = VerticalAlign.Top;
                //    c.Text = rs.Rows[i]["NoKTP"].ToString() + " ";
                //    c.HorizontalAlign = HorizontalAlign.Left;
                //    c.Wrap = false;
                //    r.Cells.Add(c);

                //    c = new TableCell();
                //    c.VerticalAlign = VerticalAlign.Top;
                //    if (rs.Rows[i]["KTPMilik"].ToString() == "0")
                //    {
                //        c.Text = "Tidak Ada";
                //    }
                //    else
                //    {
                //        c.Text = "Ada";
                //    }
                //    c.HorizontalAlign = HorizontalAlign.Left;
                //    c.Wrap = false;
                //    r.Cells.Add(c);

                //    c = new TableCell();
                //    c.VerticalAlign = VerticalAlign.Top;
                //    if (rs.Rows[i]["KTPIstri"].ToString() == "1")
                //    {
                //        c.Text = "Ada";
                //    }
                //    else
                //    {
                //        c.Text = "Tidak Ada";
                //    }
                //    c.HorizontalAlign = HorizontalAlign.Left;
                //    c.Wrap = false;
                //    r.Cells.Add(c);



                //    c = new TableCell();
                //    c.VerticalAlign = VerticalAlign.Top;
                //    if (rs.Rows[i]["KK"].ToString() == "0")
                //    {
                //        c.Text = "Tidak Ada";
                //        a = false;
                //    }
                //    else if (rs.Rows[i]["KK"].ToString() == "1")
                //    {
                //        c.Text = "Ada";
                //    }
                //    c.HorizontalAlign = HorizontalAlign.Left;
                //    c.Wrap = false;
                //    r.Cells.Add(c);

                //    r.Cells.Add(c);

                //    c = new TableCell();
                //    c.VerticalAlign = VerticalAlign.Top;
                //    if (rs.Rows[i]["SNKH"].ToString() == "0")
                //    {
                //        c.Text = "Tidak Ada";
                //        b = false;
                //    }
                //    else if (rs.Rows[i]["SNKH"].ToString() == "1")
                //    {
                //        c.Text = "Ada";
                //    }
                //    c.HorizontalAlign = HorizontalAlign.Left;
                //    c.Wrap = false;
                //    r.Cells.Add(c);

                //    c = new TableCell();
                //    c.VerticalAlign = VerticalAlign.Top;
                //    if (rs.Rows[i]["SKK"].ToString() == "0")
                //    {
                //        c.Text = "Tidak Ada";
                //        k = false;
                //    }
                //    else if (rs.Rows[i]["SKK"].ToString() == "1")
                //    {
                //        c.Text = "Ada";
                //    }
                //    c.HorizontalAlign = HorizontalAlign.Left;
                //    c.Wrap = false;
                //    r.Cells.Add(c);

                //    c = new TableCell();
                //    c.VerticalAlign = VerticalAlign.Top;
                //    if (rs.Rows[i]["RK"].ToString() == "0")
                //    {
                //        c.Text = "Tidak Ada";
                //        d = false;
                //    }
                //    else if (rs.Rows[i]["RK"].ToString() == "1")
                //    {
                //        c.Text = "Ada";
                //    }
                //    c.HorizontalAlign = HorizontalAlign.Left;
                //    c.Wrap = false;
                //    r.Cells.Add(c);

                //    c = new TableCell();
                //    c.VerticalAlign = VerticalAlign.Top;
                //    if (rs.Rows[i]["BT"].ToString() == "0")
                //    {
                //        c.Text = "Tidak Ada";
                //        e = false;
                //    }
                //    else if (rs.Rows[i]["BT"].ToString() == "1")
                //    {
                //        c.Text = "Ada";
                //    }
                //    c.HorizontalAlign = HorizontalAlign.Left;
                //    c.Wrap = false;
                //    r.Cells.Add(c);

                //    c = new TableCell();
                //    c.VerticalAlign = VerticalAlign.Top;
                //    if (rs.Rows[i]["KW"].ToString() == "0")
                //    {
                //        c.Text = "Tidak Ada";
                //        f = false;
                //    }
                //    else if (rs.Rows[i]["KW"].ToString() == "1")
                //    {
                //        c.Text = "Ada";
                //    }
                //    c.HorizontalAlign = HorizontalAlign.Left;
                //    c.Wrap = false;
                //    r.Cells.Add(c);

                //    c = new TableCell();
                //    c.VerticalAlign = VerticalAlign.Top;
                //    c.Text = rs.Rows[i]["NPWP"].ToString();
                //    c.HorizontalAlign = HorizontalAlign.Left;
                //    c.Wrap = false;
                //    r.Cells.Add(c);

                //    c = new TableCell();
                //    c.VerticalAlign = VerticalAlign.Top;
                //    c.Text = rs.Rows[i]["NoKontrak"].ToString();
                //    c.HorizontalAlign = HorizontalAlign.Left;
                //    c.Wrap = false;
                //    r.Cells.Add(c);


                //    c = new TableCell();
                //    c.VerticalAlign = VerticalAlign.Top;
                //    if (rs.Rows[i]["DU"].ToString() == "0")
                //    {
                //        c.Text = "Tidak Ada";
                //        g = false;
                //    }
                //    else if (rs.Rows[i]["DU"].ToString() == "1")
                //    {
                //        c.Text = "Ada";
                //    }
                //    c.HorizontalAlign = HorizontalAlign.Left;
                //    c.Wrap = false;
                //    r.Cells.Add(c);

                //    c = new TableCell();
                //    c.VerticalAlign = VerticalAlign.Top;
                //    if (rs.Rows[i]["DL"].ToString() == "0")
                //    {
                //        c.Text = "Tidak Ada";
                //        h = false;
                //    }
                //    else if (rs.Rows[i]["DL"].ToString() == "1")
                //    {
                //        c.Text = "Ada";
                //    }
                //    c.HorizontalAlign = HorizontalAlign.Left;
                //    c.Wrap = false;
                //    r.Cells.Add(c);

                //    c = new TableCell();
                //    c.VerticalAlign = VerticalAlign.Top;
                //    if (rs.Rows[i]["SM"].ToString() == "0")
                //    {
                //        c.Text = "Tidak Ada";
                //        j = false;
                //    }
                //    else if (rs.Rows[i]["SM"].ToString() == "1")
                //    {
                //        c.Text = "Ada";
                //    }
                //    c.HorizontalAlign = HorizontalAlign.Left;
                //    c.Wrap = false;
                //    r.Cells.Add(c);

                //    bool z = a & b & d & e & f & g & h & j & k;
                //    c = new TableCell();
                //    c.VerticalAlign = VerticalAlign.Top;
                //    if (z == false)
                //    {
                //        c.Text = "Tidak Lengkap";
                //    }
                //    else if (z == true)
                //    {
                //        c.Text = "Lengkap";
                //    }
                //    c.HorizontalAlign = HorizontalAlign.Left;
                //    c.Wrap = false;
                //    r.Cells.Add(c);

                //    string ker = "";
                //    if (a == false)
                //    {
                //        ker += "Kartu Keluarga,";
                //    }
                //    if (b == false)
                //    {
                //        ker += "Surat Nikah,";
                //    }
                //    if (k == false)
                //    {
                //        ker += "SKK,";
                //    }
                //    if (d == false)
                //    {
                //        ker += "RK,";
                //    }
                //    if (e == false)
                //    {
                //        ker += "BT,";
                //    }
                //    if (f == false)
                //    {
                //        ker += "KW,";
                //    }
                //    if (g == false)
                //    {
                //        ker += "Denah Unit,";
                //    }
                //    if (h == false)
                //    {
                //        ker += "Denah Lantai,";
                //    }
                //    if (j == false)
                //    {
                //        ker += "Spesifikasi Material";
                //    }


                //    c = new TableCell();
                //    c.Text = ker;
                //    c.HorizontalAlign = HorizontalAlign.Left;
                //    c.Wrap = false;
                //    r.Cells.Add(c);

                //    c = new TableCell();
                //    c.VerticalAlign = VerticalAlign.Top;
                //    if (rs.Rows[i]["TglLengkapPPJB"] != DBNull.Value)
                //    {
                //        c.Text = Cf.Day(Convert.ToDateTime(rs.Rows[i]["TglLengkapPPJB"]));
                //    }
                //    c.HorizontalAlign = HorizontalAlign.Left;
                //    c.Wrap = false;
                //    r.Cells.Add(c);

                //    rpt.Rows.Add(r);
                //}
            }
        }
    }
}

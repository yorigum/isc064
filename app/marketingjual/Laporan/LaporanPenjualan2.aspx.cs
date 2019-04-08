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

namespace ISC064.MARKETINGJUAL.Laporan
{
    public partial class LaporanPenjualan2 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();

            if (!Page.IsPostBack)
            {
                Act.ProjectList(project);
                comp.InnerHtml = Mi.Pt;
                rptA.Visible = false;
                rptB.Visible = false;
                rptC.Visible = false;
                rptD.Visible = false;
                graph.Visible = false;
                summary.Visible = false;
                Bind();
                BindAgent();
                Js.Focus(this, scr);
                if (!Act.Sec("DownloadExcel")) xls.Enabled = false;
            }
        }

        protected int UserAgent()
        {
            int a = Db.SingleInteger("SELECT NoAgent FROM " + Mi.DbPrefix + "SECURITY..USERNAME WHERE UserID = '" + Act.UserID + "'");

            return a;
        }

        private void Bind()
        {
            ddlAgent.Items.Clear();
            ddlAgent.Items.Add("SEMUA");

            lokasi.Items.Clear();
            lokasi.Items.Add("SEMUA");

            cblPrincipal.Items.Clear();
            cblPrincipal.Items.Add("SEMUA");

            cblTipe.Items.Clear();

            dari.Text = Cf.Day(Cf.AwalBulan());
            sampai.Text = Cf.Day(Cf.AkhirBulan());

            string Project = project.SelectedIndex == 0 ? "Project IN (" + Act.ProjectListSql + ")" : "Project = '" + project.SelectedValue + "'";

            DataTable rs = Db.Rs("SELECT * FROM REF_LOKASI WHERE " + Project + " ORDER BY Lokasi");
            for (int i = 0; i < rs.Rows.Count; i++)
                lokasi.Items.Add(new ListItem(rs.Rows[i][0].ToString()));

            string strSql = "SELECT DISTINCT Principal FROM MS_AGENT";
            rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
                cblPrincipal.Items.Add(new ListItem(rs.Rows[i]["Principal"].ToString()));

            strSql = "SELECT * FROM REF_JENIS WHERE " + Project + "  ORDER BY SN";
            rs = Db.Rs(strSql);

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                cblTipe.Items.Add(new ListItem(rs.Rows[i]["Jenis"].ToString() + " - " + rs.Rows[i]["Nama"].ToString(), Cf.Pk(rs.Rows[i]["Jenis"].ToString())));
                cblTipe.Items[i].Selected = true;
            }
            lokasi.SelectedIndex = 0;

            strSql = "SELECT * FROM REF_AGENT_TIPE WHERE " + Project + "  ORDER BY ID";
            rs = Db.Rs(strSql);

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                tipesales.Items.Add(new ListItem(rs.Rows[i]["Tipe"].ToString()));
                //tipesales.Items[i].Selected = true;
            }
            tipesales.SelectedIndex = 0;

            strSql = "SELECT * FROM REF_JENISPROPERTI WHERE " + Project + " ORDER BY SN";
            rs = Db.Rs(strSql);

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                tipepro.Items.Add(new ListItem(rs.Rows[i]["Nama"].ToString()));
                //tipesales.Items[i].Selected = true;
            }
            tipepro.SelectedIndex = 0;
        }

        private void BindAgent()
        {
            ddlAgent.Items.Clear();
            ddlAgent.Items.Add("SEMUA");

            string BAgent = "";
            if (tipesales.SelectedItem.Text == "SEMUA")
            {
                BAgent = "";
            }
            else
            {
                BAgent = " AND Tipe = '" + tipesales.SelectedItem.Text + "'";
            }
            string Project = project.SelectedIndex == 0 ? "Project IN (" + Act.ProjectListSql + ")" : "Project = '" + project.SelectedValue + "'";
            string strSql = "SELECT NoAgent, Nama FROM MS_AGENT WHERE Status = 'A' " + BAgent + " AND " + Project + " ORDER BY Nama";
            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
                ddlAgent.Items.Add(new ListItem(rs.Rows[i]["Nama"].ToString(), Cf.Pk(rs.Rows[i]["NoAgent"])));

        }
        protected void pdf_Click(object sender, System.EventArgs e)
        {

            Process p = new System.Diagnostics.Process();

            string Nama = "Laporan Penjualan";
            string Link = "";
            DateTime TglGenerate = DateTime.Now;
            string FileName = "";
            string FileType = "application/pdf";
            string UserID = Act.UserID;
            string IP = Act.IP;

            Db.Execute("EXEC spLapPDFDaftar"

                    + " '" + Nama + "'"
                    + ",'" + Link + "'"
                    + ",'" + TglGenerate + "'"
                    + ",'" + IP + "'"
                    + ",'" + UserID + "'"
                    + ",'" + FileName + "'"
                    + ",'" + FileType + "'"
                    + ",'" + Cf.Date(dari.Text) + "'"
                    + ",'" + Cf.Date(sampai.Text) + "'"
                    );

            //get nomor customer terbaru
            int NoAttachment = Db.SingleInteger(
                "SELECT TOP 1 AttachmentID FROM LapPDF ORDER BY AttachmentID DESC");

            string strSql = "SELECT * FROM ISC064_MARKETINGJUAL..LapPDF WHERE AttachmentID  = '" + NoAttachment + "'";
            DataTable rs = Db.Rs(strSql);

            string nfilename = "LaporanPenjualan" + NoAttachment + ".pdf";

            //update filename
            Db.Execute("UPDATE ISC064_MARKETINGJUAL..LapPDF SET FileName= '" + nfilename + "' WHERE AttachmentID = " + NoAttachment);


            //folder untuk menyimpan file pdf
            string save = Mi.PathFilePDFReport + "LaporanPenjualan" + rs.Rows[0]["AttachmentID"] + ".pdf";

            //declare parameter
            string Lokasi = lokasi.SelectedValue.Replace(" ", "%");

            string TitipJual = "";
            if (titipjual.SelectedIndex != 0)
                TitipJual = " AND TitipJual=" + titipjual.SelectedValue.ToString();
            else TitipJual = "SEMUA";

            string TipePro = "";
            if (tipepro.SelectedIndex != 0)
                TipePro = " AND JenisProperti='" + tipepro.SelectedItem.ToString() + "'";

            else TipePro = "SEMUA";

            string Agent = ddlAgent.SelectedValue;

            string Project = "";
            if (project.SelectedIndex == 0)
            {
                Project = Act.ProjectListSql.Replace("'", "");
            }
            else
            {
                Project = project.SelectedValue;
            }

            string nm = string.Empty;
            string nm2 = string.Empty;
            try
            {
                foreach (ListItem item in cblTipe.Items)
                {
                    if (item.Selected == true)
                    {
                        nm += item.Value.Replace(" ", "%") + "-";
                    }
                }
            }
            catch (Exception)
            {
            }
            //link untuk download pdf
            string link = Mi.PathAlamatWeb + "marketingjual/LaporanPDF/PDFLaporanPenjualan2.aspx?id=" + rs.Rows[0]["AttachmentID"] + "&tipe=" + nm + "&lokasi=" + Lokasi + "&agent=" + Agent + "&titipjual=" + TitipJual + "&tipepro=" + TipePro + "&userid=" + UserID + "&project=" + Project + "&pers=" + pers.SelectedValue + "";

            //update link
            Db.Execute("UPDATE ISC064_MARKETINGJUAL..LapPDF SET Link= '" + link + "' WHERE AttachmentID = " + NoAttachment);

            //format page
            p.StartInfo.Arguments = "--orientation landscape --page-width 16.5in --page-height 11.5in --margin-left 0 --margin-right 0 --margin-top 0.25cm --margin-bottom 0 " + link + " " + save;

            //panggil aplikasi untuk mengconvert pdf
            p.StartInfo.FileName = Mi.PathWkhtmlPDFReport;
            p.Start();

            //60000 -> waktu jeda lama convert pdf
            p.WaitForExit(30000);

            string Src = Mi.PathFilePDFReport + nfilename;
            Mi.DownloadPDF(this, Src, (rs.Rows[0]["FileName"]).ToString(), rs.Rows[0]["FileType"].ToString());
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

            //if (!Cf.isPilih(cblPrincipal))
            //{
            //    x = false;
            //    if (s == "")
            //        s = cblPrincipal.ID;

            //    lblPrincipal.Text = "Pilih minimum satu";
            //}
            //else
            //    lblPrincipal.Text = "";

            //if (!Cf.isPilih(cblTipe))
            //{
            //    x = false;
            //    if (s == "")
            //        s = cblTipe.ID;

            //    lblTipe.Text = "Pilih minimum satu";
            //}
            //else
            //    lblTipe.Text = "";

            if (!x && s != "")
            {
                RegisterStartupScript("err"
                    , "<script type='text/javascript'>document.getElementById('" + s + "').select()</script>");
            }

            return x;
        }

        private Table fillAll(DateTime Dari, DateTime Sampai, string Tipe, string strAdd)
        {

            string Lokasi = "";
            if (lokasi.SelectedIndex != 0)
            {
                Lokasi = " AND a.Lokasi = '" + Cf.Str(lokasi.SelectedValue) + "'";
            }

            string TitipJual = "";
            if (titipjual.SelectedIndex != 0)
                TitipJual = " AND TitipJual=" + titipjual.SelectedValue.ToString();

            string TipePro = "";
            if (tipepro.SelectedIndex != 0)
            {
                TipePro = " AND JenisProperti='" + tipepro.SelectedItem.ToString() + "'";
            }

            string Project = " AND a.Project IN (" + Act.ProjectListSql + ")";
            if (project.SelectedValue != "SEMUA") Project = " AND a.Project = '" + project.SelectedValue + "'";

            string Perusahaan = "";
            if (pers.SelectedValue != "SEMUA") Perusahaan = " AND a.Pers = '" + pers.SelectedValue + "'";

            //query tabel all summary
            string strSql1 = "SELECT a.*, b.Nama AS Customer, c.Nama AS Agent, c.Principal, d.Jenis, d.LuasSG, d.LuasNett, d.JenisProperti, d.ArahHadap"
                + " FROM MS_KONTRAK a"
                + " INNER JOIN MS_CUSTOMER b ON a.NoCustomer = b.NoCustomer"
                + " INNER JOIN MS_AGENT c ON a.NoAgent = c.NoAgent"
                + " INNER JOIN MS_UNIT d ON a.NoStock = d.NoStock"
                + " WHERE a.TglKontrak >= '" + Convert.ToDateTime(Dari) + "'"
                + " AND a.TglKontrak <= '" + Convert.ToDateTime(Sampai) + "'"
                + Project
                + Perusahaan
                + Lokasi
                + Tipe
                + strAdd
                + TitipJual
                + TipePro
                + " ORDER BY TglKontrak, NoKontrak"
                ;
            DataTable rs1 = Db.Rs(strSql1);

            sA.Text = " <h3> ( " + rs1.Rows.Count.ToString() + " ) </h3>";

            decimal jumPlist = 0;
            decimal jumDisc = 0;
            decimal jumBunga = 0;
            decimal jumNilaiKontrakA = 0;
            decimal jumDPP = 0;
            decimal jumPPN = 0;
            decimal jumLuasSG = 0;
            decimal jumLuasNett = 0;

            decimal jumDiscTambahan = 0;
            decimal jumHargaGimmick = 0;
            decimal jumHargaLainLain = 0;

            for (int i = 0; i < rs1.Rows.Count; i++)
            {
                if (!Response.IsClientConnected)
                    break;

                TableRow r1 = new TableRow();
                r1.Attributes["ondblclick"] = "popEditKontrak('" + rs1.Rows[i]["NoKontrak"] + "')";

                TableCell c1;
                // tabel summary
                c1 = new TableCell();
                c1.Text = (i + 1).ToString();
                c1.HorizontalAlign = HorizontalAlign.Center;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                c1.Text = Cf.Day(rs1.Rows[i]["TglKontrak"]);
                c1.HorizontalAlign = HorizontalAlign.Left;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                c1.Text = rs1.Rows[i]["NoKontrak"].ToString();
                c1.HorizontalAlign = HorizontalAlign.Left;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                c1.Text = rs1.Rows[i]["Customer"].ToString();
                c1.HorizontalAlign = HorizontalAlign.Left;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                c1.Text = rs1.Rows[i]["Agent"].ToString();
                c1.HorizontalAlign = HorizontalAlign.Left;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                c1.Text = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + rs1.Rows[i]["Project"].ToString() + "'");
                c1.HorizontalAlign = HorizontalAlign.Left;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                c1.Text = rs1.Rows[i]["NoUnit"].ToString();
                c1.HorizontalAlign = HorizontalAlign.Left;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                c1.Text = rs1.Rows[i]["Jenis"].ToString();
                c1.HorizontalAlign = HorizontalAlign.Left;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                c1.Text = rs1.Rows[i]["JenisProperti"].ToString();
                c1.HorizontalAlign = HorizontalAlign.Left;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                c1.Text = Cf.Num(rs1.Rows[i]["LuasSG"]);
                jumLuasSG += Convert.ToDecimal(rs1.Rows[i]["LuasSG"]);
                c1.HorizontalAlign = HorizontalAlign.Left;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                c1.Text = Cf.Num(rs1.Rows[i]["LuasNett"]);
                jumLuasNett += Convert.ToDecimal(rs1.Rows[i]["LuasNett"]);
                c1.HorizontalAlign = HorizontalAlign.Left;
                r1.Cells.Add(c1);
                //
                c1 = new TableCell();
                c1.Text = rs1.Rows[i]["ArahHadap"].ToString();
                c1.HorizontalAlign = HorizontalAlign.Left;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                c1.Text = Cf.Num(rs1.Rows[i]["Gross"]);
                jumPlist += Convert.ToDecimal(rs1.Rows[i]["Gross"]);
                c1.HorizontalAlign = HorizontalAlign.Right;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                //decimal Disc1 = 0, AfterDisc1 = 0;
                c1.Text = Cf.Num(rs1.Rows[i]["DiskonRupiah"]);//Cf.Num(Disc1);
                jumDisc += Convert.ToDecimal(rs1.Rows[i]["DiskonRupiah"]);
                c1.HorizontalAlign = HorizontalAlign.Right;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                //decimal Disc1 = 0, AfterDisc1 = 0;
                c1.Text = Cf.Num(rs1.Rows[i]["DiskonTambahan"]);//Cf.Num(Disc1);
                jumDiscTambahan += Convert.ToDecimal(rs1.Rows[i]["DiskonTambahan"]);
                c1.HorizontalAlign = HorizontalAlign.Right;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                //decimal Disc1 = 0, AfterDisc1 = 0;
                c1.Text = Cf.Num(rs1.Rows[i]["HargaGimmick"]);//Cf.Num(Disc1);
                jumHargaGimmick += Convert.ToDecimal(rs1.Rows[i]["HargaGimmick"]);
                c1.HorizontalAlign = HorizontalAlign.Right;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                //decimal Disc1 = 0, AfterDisc1 = 0;
                c1.Text = Cf.Num(rs1.Rows[i]["HargaLainLain"]);//Cf.Num(Disc1);
                jumHargaLainLain += Convert.ToDecimal(rs1.Rows[i]["HargaLainLain"]);
                c1.HorizontalAlign = HorizontalAlign.Right;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                // decimal NominalBunga = Db.SingleDecimal("SELECT BungaNominal FROM MS_KONTRAK WHERE NoKontrak = '"++"' ");
                c1.Text = Cf.Num(rs1.Rows[i]["BungaNominal"]);//Cf.Num(Disc1);
                jumBunga += Convert.ToDecimal(rs1.Rows[i]["BungaNominal"]);
                c1.HorizontalAlign = HorizontalAlign.Right;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                decimal NilaiKontrak1 = Convert.ToDecimal(rs1.Rows[i]["NilaiKontrak"]);
                c1.Text = Cf.Num(NilaiKontrak1);
                jumNilaiKontrakA += NilaiKontrak1;
                c1.HorizontalAlign = HorizontalAlign.Right;
                r1.Cells.Add(c1);

                //hitung DPP + PPN
                decimal bn = 0;
                if (!rs1.Rows[i]["BungaNominal"].Equals(System.DBNull.Value))
                    bn = Convert.ToDecimal(rs1.Rows[i]["BungaNominal"]);
                decimal DPP1 = Convert.ToDecimal(rs1.Rows[i]["NilaiKontrak"]);
                DPP1 = Math.Round(DPP1 / (decimal)1.1);
                c1 = new TableCell();
                if (!Convert.ToBoolean(rs1.Rows[i]["PPN"]))
                {
                    DPP1 = Convert.ToDecimal(rs1.Rows[i]["Gross"]) - Convert.ToDecimal(rs1.Rows[i]["DiskonRupiah"]) + bn;
                }
                if (rs1.Rows[0]["JenisPPN"].ToString() == "PEMERINTAH")
                {
                    DPP1 = Convert.ToDecimal(rs1.Rows[i]["NilaiKontrak"]);
                }

                DPP1 = Convert.ToDecimal(rs1.Rows[i]["NilaiKontrak"]) - Convert.ToDecimal(rs1.Rows[i]["NilaiPPN"]);
                c1.Text = Cf.Num(DPP1);//Cf.Num(TotalBayar);
                jumDPP += DPP1;
                c1.HorizontalAlign = HorizontalAlign.Right;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                decimal PPN1 = 0;
                //				PPN1 = Math.Round(DPP1 * (decimal)0.1);
                PPN1 = Convert.ToDecimal(rs1.Rows[i]["NilaiPPN"]);
                jumPPN += PPN1;
                c1.Text = Cf.Num(PPN1);
                c1.HorizontalAlign = HorizontalAlign.Right;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                c1.Text = rs1.Rows[i]["Skema"].ToString();
                c1.HorizontalAlign = HorizontalAlign.Left;
                r1.Cells.Add(c1);


                rptA.Rows.Add(r1);

                if (i == rs1.Rows.Count - 1)
                {
                    TableRow r4 = new TableRow();

                    TableCell c4;

                    c4 = new TableCell();
                    c4.Text = "<strong>TOTAL</strong>";
                    c4.Attributes["style"] = "border-top:1px solid black";
                    c4.HorizontalAlign = HorizontalAlign.Left;
                    c4.ColumnSpan = 6;
                    r4.Cells.Add(c4);

                    c4 = new TableCell();
                    c4.Text = "<strong>" + rs1.Rows.Count + "</strong>";
                    c4.Attributes["style"] = "border-top:1px solid black";
                    c4.HorizontalAlign = HorizontalAlign.Center;
                    c4.ColumnSpan = 2;
                    r4.Cells.Add(c4);

                    c4 = new TableCell();
                    c4.Text = "<strong>&nbsp;</strong>";
                    c4.Attributes["style"] = "border-top:1px solid black";
                    c4.HorizontalAlign = HorizontalAlign.Center;
                    r4.Cells.Add(c4);

                    c4 = new TableCell();
                    c4.Text = "<strong>" + Cf.Num(jumLuasSG) + "</strong>";
                    c4.Attributes["style"] = "border-top:1px solid black; text-align:left;";
                    c4.HorizontalAlign = HorizontalAlign.Center;
                    r4.Cells.Add(c4);

                    c4 = new TableCell();
                    c4.Text = "<strong>" + Cf.Num(jumLuasNett) + "</strong>";
                    c4.Attributes["style"] = "border-top:1px solid black; text-align:left;";
                    c4.HorizontalAlign = HorizontalAlign.Center;
                    c4.ColumnSpan = 2;
                    r4.Cells.Add(c4);

                    c4 = new TableCell();
                    c4.Text = "<strong>" + Cf.Num(jumPlist) + "</strong>";
                    c4.Attributes["style"] = "border-top:1px solid black";
                    c4.HorizontalAlign = HorizontalAlign.Right;
                    r4.Cells.Add(c4);

                    c4 = new TableCell();
                    c4.Text = "<strong>" + Cf.Num(jumDisc) + "</strong>";
                    c4.Attributes["style"] = "border-top:1px solid black";
                    c4.HorizontalAlign = HorizontalAlign.Right;
                    r4.Cells.Add(c4);

                    c4 = new TableCell();
                    c4.Text = "<strong>" + Cf.Num(jumDiscTambahan) + "</strong>";
                    c4.Attributes["style"] = "border-top:1px solid black";
                    c4.HorizontalAlign = HorizontalAlign.Right;
                    r4.Cells.Add(c4);

                    c4 = new TableCell();
                    c4.Text = "<strong>" + Cf.Num(jumHargaGimmick) + "</strong>";
                    c4.Attributes["style"] = "border-top:1px solid black";
                    c4.HorizontalAlign = HorizontalAlign.Right;
                    r4.Cells.Add(c4);

                    c4 = new TableCell();
                    c4.Text = "<strong>" + Cf.Num(jumHargaLainLain) + "</strong>";
                    c4.Attributes["style"] = "border-top:1px solid black";
                    c4.HorizontalAlign = HorizontalAlign.Right;
                    r4.Cells.Add(c4);

                    c4 = new TableCell();
                    c4.Text = "<strong>" + Cf.Num(jumBunga) + "</strong>";
                    c4.Attributes["style"] = "border-top:1px solid black";
                    c4.HorizontalAlign = HorizontalAlign.Right;
                    r4.Cells.Add(c4);

                    c4 = new TableCell();
                    c4.Text = "<strong>" + Cf.Num(jumNilaiKontrakA) + "</strong>";
                    c4.Attributes["style"] = "border-top:1px solid black";
                    c4.HorizontalAlign = HorizontalAlign.Right;
                    r4.Cells.Add(c4);

                    c4 = new TableCell();
                    c4.Text = "<strong>" + Cf.Num(jumDPP) + "</strong>";
                    c4.Attributes["style"] = "border-top:1px solid black";
                    c4.HorizontalAlign = HorizontalAlign.Right;
                    r4.Cells.Add(c4);

                    c4 = new TableCell();
                    c4.Text = "<strong>" + Cf.Num(jumPPN) + "</strong>";
                    c4.Attributes["style"] = "border-top:1px solid black";
                    c4.HorizontalAlign = HorizontalAlign.Right;
                    r4.Cells.Add(c4);

                    c4 = new TableCell();
                    c4.Text = "";
                    r4.Cells.Add(c4);

                    rptA.Rows.Add(r4);
                }
            }

            sumall.Text = Convert.ToString(rs1.Rows.Count);

            TableRow r0 = new TableRow();
            rptA.Rows.Add(r0);

            return rptA;
        }

        private Table fillBatal(DateTime Dari, DateTime Sampai, string Tipe, string strAdd)
        {

            string Lokasi = "";
            if (lokasi.SelectedIndex != 0)
            {
                Lokasi = " AND a.Lokasi = '" + Cf.Str(lokasi.SelectedValue) + "'";
            }

            string TitipJual = "";
            if (titipjual.SelectedIndex != 0)
                TitipJual = " AND TitipJual=" + titipjual.SelectedValue.ToString();

            string TipePro = "";
            if (tipepro.SelectedIndex != 0)
            {
                TipePro = " AND JenisProperti='" + tipepro.SelectedItem.ToString() + "'";
            }

            string Project = " AND a.Project IN (" + Act.ProjectListSql + ")";
            if (project.SelectedValue != "SEMUA") Project = " AND a.Project = '" + project.SelectedValue + "'";

            string Perusahaan = "";
            if (pers.SelectedValue != "SEMUA") Perusahaan = " AND a.Pers = '" + pers.SelectedValue + "'";

            //query tabel batal
            string strSql2 = "SELECT a.*, b.Nama AS Customer, c.Nama AS Agent, c.Principal, d.Jenis, d.LuasSG, d.LuasNett, d.JenisProperti"
                + " FROM MS_KONTRAK a"
                + " INNER JOIN MS_CUSTOMER b ON a.NoCustomer = b.NoCustomer"
                + " INNER JOIN MS_AGENT c ON a.NoAgent = c.NoAgent"
                + " INNER JOIN MS_UNIT d ON a.NoStock = d.NoStock"
                + " WHERE a.TglBatal >= '" + Convert.ToDateTime(Dari) + "'"
                + " AND a.TglBatal <= '" + Convert.ToDateTime(Sampai) + "'"
                + " AND a.Status = 'B'"
                + Project
                + Perusahaan
                + Lokasi
                + Tipe
                + strAdd
                + TitipJual
                + TipePro
                + " AND ( a.TglBatal is not null )" //OR a.TglBatal '"+ Convert.ToDateTime(Sampai) +"') "//Status
                + " ORDER BY TglKontrak, NoKontrak"
                ;
                        
            DataTable rs2 = Db.Rs(strSql2);

            sumB.Text = " <h3> ( " + rs2.Rows.Count.ToString() + " ) </h3>";

            decimal jumNilaiKontrakB = 0;
            decimal jumAdmB = 0;
            decimal jumPembayaran = 0;
            decimal jumKembali = 0;
            decimal jumLuasSG = 0;
            decimal jumLuasNett = 0;
            //
            decimal jumDiskonRupiahB = 0;
            decimal jumDiskonTambahanB = 0;
            decimal jumHargaGimmickB = 0;
            decimal jumHargaLainLainB = 0;
            decimal jumNominalBungaB = 0;

            for (int i = 0; i < rs2.Rows.Count; i++)
            {
                if (!Response.IsClientConnected)
                    break;

                TableRow r2 = new TableRow();
                r2.Attributes["ondblclick"] = "popEditKontrak('" + rs2.Rows[i]["NoKontrak"] + "')";

                TableCell c2;
                // tabel batal
                c2 = new TableCell();
                c2.Text = (i + 1).ToString();
                c2.HorizontalAlign = HorizontalAlign.Center;
                r2.Cells.Add(c2);

                c2 = new TableCell();
                c2.Text = Cf.Day(rs2.Rows[i]["TglKontrak"]);
                c2.HorizontalAlign = HorizontalAlign.Left;
                r2.Cells.Add(c2);

                c2 = new TableCell();
                c2.Text = rs2.Rows[i]["NoKontrak"].ToString();
                c2.HorizontalAlign = HorizontalAlign.Left;
                r2.Cells.Add(c2);

                c2 = new TableCell();
                c2.Text = rs2.Rows[i]["Customer"].ToString();
                c2.HorizontalAlign = HorizontalAlign.Left;
                r2.Cells.Add(c2);

                c2 = new TableCell();
                c2.Text = rs2.Rows[i]["Agent"].ToString();
                c2.HorizontalAlign = HorizontalAlign.Left;
                r2.Cells.Add(c2);

                c2 = new TableCell();
                c2.Text = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + rs2.Rows[i]["Project"].ToString() + "'");
                c2.HorizontalAlign = HorizontalAlign.Left;
                r2.Cells.Add(c2);

                c2 = new TableCell();
                c2.Text = rs2.Rows[i]["NoUnit"].ToString();
                c2.HorizontalAlign = HorizontalAlign.Left;
                r2.Cells.Add(c2);

                c2 = new TableCell();
                c2.Text = rs2.Rows[i]["JenisProperti"].ToString();
                c2.HorizontalAlign = HorizontalAlign.Left;
                r2.Cells.Add(c2);

                c2 = new TableCell();
                c2.Text = Cf.Num(rs2.Rows[i]["LuasSG"]);
                jumLuasSG += Convert.ToDecimal(rs2.Rows[i]["LuasSG"]);
                c2.HorizontalAlign = HorizontalAlign.Left;
                r2.Cells.Add(c2);

                c2 = new TableCell();
                c2.Text = Cf.Num(rs2.Rows[i]["LuasNett"]);
                jumLuasNett += Convert.ToDecimal(rs2.Rows[i]["LuasNett"]);
                c2.HorizontalAlign = HorizontalAlign.Left;
                r2.Cells.Add(c2);

                c2 = new TableCell();
                c2.Text = Cf.Day(rs2.Rows[i]["TglBatal"]);
                c2.HorizontalAlign = HorizontalAlign.Left;
                r2.Cells.Add(c2);

                c2 = new TableCell();
                c2.Text = Cf.Num(Convert.ToDecimal(rs2.Rows[i]["DiskonRupiah"]));
                jumDiskonRupiahB += Convert.ToDecimal(rs2.Rows[i]["DiskonRupiah"]);
                c2.HorizontalAlign = HorizontalAlign.Right;
                r2.Cells.Add(c2);

                c2 = new TableCell();
                c2.Text = Cf.Num(Convert.ToDecimal(rs2.Rows[i]["DiskonTambahan"]));
                jumDiskonTambahanB += Convert.ToDecimal(rs2.Rows[i]["DiskonTambahan"]);
                c2.HorizontalAlign = HorizontalAlign.Right;
                r2.Cells.Add(c2);

                c2 = new TableCell();
                c2.Text = Cf.Num(Convert.ToDecimal(rs2.Rows[i]["HargaGimmick"]));
                jumHargaGimmickB += Convert.ToDecimal(rs2.Rows[i]["HargaGimmick"]);
                c2.HorizontalAlign = HorizontalAlign.Right;
                r2.Cells.Add(c2);

                c2 = new TableCell();
                c2.Text = Cf.Num(Convert.ToDecimal(rs2.Rows[i]["HargaLainLain"]));
                jumHargaLainLainB += Convert.ToDecimal(rs2.Rows[i]["HargaLainLain"]);
                c2.HorizontalAlign = HorizontalAlign.Right;
                r2.Cells.Add(c2);

                c2 = new TableCell();
                c2.Text = Cf.Num(Convert.ToDecimal(rs2.Rows[i]["BungaNominal"]));
                jumNominalBungaB += Convert.ToDecimal(rs2.Rows[i]["BungaNominal"]);
                c2.HorizontalAlign = HorizontalAlign.Right;
                r2.Cells.Add(c2);

                c2 = new TableCell();
                decimal NilaiKontrak2 = Convert.ToDecimal(rs2.Rows[i]["NilaiKontrak"]);
                c2.Text = Cf.Num(NilaiKontrak2);
                jumNilaiKontrakB += NilaiKontrak2;
                c2.HorizontalAlign = HorizontalAlign.Right;
                r2.Cells.Add(c2);

                // nilai pembatalan
                decimal nBatal = Db.SingleDecimal("SELECT NilaiTagihan FROM MS_TAGIHAN WHERE NamaTagihan = 'BIAYA ADM. PEMBATALAN' AND NoKontrak='" + rs2.Rows[i]["NoKontrak"] + "'");
                c2 = new TableCell();
                c2.Text = Cf.Num(nBatal);
                jumAdmB += nBatal;
                c2.HorizontalAlign = HorizontalAlign.Right;
                r2.Cells.Add(c2);

                //jumlah pembayaran
                c2 = new TableCell();
                decimal NilaiPembayaran = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan),0) AS TotalPembayaran FROM MS_PELUNASAN WHERE NoKontrak = '" + rs2.Rows[i]["NoKontrak"] + "'");
                c2.Text = Cf.Num(NilaiPembayaran);
                jumPembayaran += NilaiPembayaran;
                c2.HorizontalAlign = HorizontalAlign.Right;
                r2.Cells.Add(c2);

                c2 = new TableCell();
                c2.Text = Cf.Num(rs2.Rows[i]["NilaiPulang"]);//Cf.Num(TotalBayar);
                jumKembali += Convert.ToDecimal(rs2.Rows[i]["NilaiPulang"]);
                c2.HorizontalAlign = HorizontalAlign.Right;
                r2.Cells.Add(c2);

                rptB.Rows.Add(r2);

                if (i == rs2.Rows.Count - 1)
                {
                    TableRow r3 = new TableRow();

                    TableCell c3;

                    c3 = new TableCell();
                    c3.Text = "<strong>TOTAL</strong>";
                    c3.Attributes["style"] = "border-top:1px solid black";
                    c3.HorizontalAlign = HorizontalAlign.Left;
                    c3.ColumnSpan = 6;
                    r3.Cells.Add(c3);

                    c3 = new TableCell();
                    c3.Text = "<strong>" + rs2.Rows.Count + "</strong>";
                    c3.Attributes["style"] = "border-top:1px solid black";
                    c3.HorizontalAlign = HorizontalAlign.Center;
                    r3.Cells.Add(c3);

                    c3 = new TableCell();
                    c3.Attributes["style"] = "border-top:1px solid black";
                    c3.Text = "&nbsp;";
                    c3.HorizontalAlign = HorizontalAlign.Center;
                    r3.Cells.Add(c3);

                    c3 = new TableCell();
                    c3.Text = "<strong>" + Cf.Num(jumLuasSG) + "</strong>";
                    c3.Attributes["style"] = "border-top:1px solid black";
                    c3.HorizontalAlign = HorizontalAlign.Center;
                    r3.Cells.Add(c3);

                    c3 = new TableCell();
                    c3.Text = "<strong>" + Cf.Num(jumLuasNett) + "</strong>";
                    c3.Attributes["style"] = "border-top:1px solid black";
                    c3.HorizontalAlign = HorizontalAlign.Center;
                    r3.Cells.Add(c3);

                    c3 = new TableCell();
                    c3.Attributes["style"] = "border-top:1px solid black";
                    c3.Text = "&nbsp;";
                    c3.HorizontalAlign = HorizontalAlign.Center;
                    r3.Cells.Add(c3);

                    c3 = new TableCell();
                    c3.Text = "<strong>" + Cf.Num(jumDiskonRupiahB.ToString()) + "</strong>";
                    c3.Attributes["style"] = "border-top:1px solid black";
                    c3.HorizontalAlign = HorizontalAlign.Right;
                    r3.Cells.Add(c3);

                    c3 = new TableCell();
                    c3.Text = "<strong>" + Cf.Num(jumDiskonTambahanB.ToString()) + "</strong>";
                    c3.Attributes["style"] = "border-top:1px solid black";
                    c3.HorizontalAlign = HorizontalAlign.Right;
                    r3.Cells.Add(c3);

                    c3 = new TableCell();
                    c3.Text = "<strong>" + Cf.Num(jumHargaGimmickB.ToString()) + "</strong>";
                    c3.Attributes["style"] = "border-top:1px solid black";
                    c3.HorizontalAlign = HorizontalAlign.Right;
                    r3.Cells.Add(c3);

                    c3 = new TableCell();
                    c3.Text = "<strong>" + Cf.Num(jumHargaLainLainB.ToString()) + "</strong>";
                    c3.Attributes["style"] = "border-top:1px solid black";
                    c3.HorizontalAlign = HorizontalAlign.Right;
                    r3.Cells.Add(c3);

                    c3 = new TableCell();
                    c3.Text = "<strong>" + Cf.Num(jumNominalBungaB.ToString()) + "</strong>";
                    c3.Attributes["style"] = "border-top:1px solid black";
                    c3.HorizontalAlign = HorizontalAlign.Right;
                    r3.Cells.Add(c3);
                    c3 = new TableCell();
                    c3.Text = "<strong>" + Cf.Num(jumNilaiKontrakB.ToString()) + "</strong>";
                    c3.Attributes["style"] = "border-top:1px solid black";
                    c3.HorizontalAlign = HorizontalAlign.Right;
                    r3.Cells.Add(c3);

                    c3 = new TableCell();
                    c3.Text = "<strong>" + Cf.Num(jumAdmB.ToString()) + "</strong>";
                    c3.Attributes["style"] = "border-top:1px solid black";
                    c3.HorizontalAlign = HorizontalAlign.Right;
                    r3.Cells.Add(c3);

                    c3 = new TableCell();
                    c3.Text = "<strong>" + Cf.Num(jumPembayaran.ToString()) + "</strong>";
                    c3.Attributes["style"] = "border-top:1px solid black";
                    c3.HorizontalAlign = HorizontalAlign.Right;
                    r3.Cells.Add(c3);

                    c3 = new TableCell();
                    c3.Text = "<strong>" + Cf.Num(jumKembali.ToString()) + "</strong>";
                    c3.Attributes["style"] = "border-top:1px solid black";
                    c3.HorizontalAlign = HorizontalAlign.Right;
                    r3.Cells.Add(c3);

                    rptB.Rows.Add(r3);
                }
            }

            sumbatal.Text = Convert.ToString(rs2.Rows.Count);
            TableRow r0 = new TableRow();
            rptB.Rows.Add(r0);

            return rptB;
        }

        private Table fillTitipJual(DateTime Dari, DateTime Sampai, string Tipe, string strAdd)
        {

            string Lokasi = "";
            if (lokasi.SelectedIndex != 0)
            {
                Lokasi = " AND a.Lokasi = '" + Cf.Str(lokasi.SelectedValue) + "'";
            }

            string TitipJual = "";
            if (titipjual.SelectedIndex != 0)
            {
                TitipJual = " AND TitipJual=" + titipjual.SelectedValue.ToString();

            }
            else
            {
                TitipJual = " AND TitipJual=1";
            }

            string TipePro = "";
            if (tipepro.SelectedIndex != 0)
            {
                TipePro = " AND JenisProperti='" + tipepro.SelectedItem.ToString() + "'";
            }

            string Project = " AND a.Project IN (" + Act.ProjectListSql + ")";
            if (project.SelectedValue != "SEMUA") Project = " AND a.Project = '" + project.SelectedValue + "'";

            string Perusahaan = "";
            if (pers.SelectedValue != "SEMUA") Perusahaan = " AND a.Pers = '" + pers.SelectedValue + "'";

            //query tabel all summary
            string strSql1 = "SELECT a.*, b.Nama AS Customer, c.Nama AS Agent, c.Principal, d.Jenis, d.LuasSG, d.LuasNett, d.JenisProperti"
                + " FROM MS_KONTRAK a"
                + " INNER JOIN MS_CUSTOMER b ON a.NoCustomer = b.NoCustomer"
                + " INNER JOIN MS_AGENT c ON a.NoAgent = c.NoAgent"
                + " INNER JOIN MS_UNIT d ON a.NoStock = d.NoStock"
                + " WHERE a.TglKontrak >= '" + Convert.ToDateTime(Dari) + "'"
                + " AND a.TglKontrak <= '" + Convert.ToDateTime(Sampai) + "'"
                + Project
                + Perusahaan
                + Lokasi
                + Tipe
                + strAdd
                + TitipJual
                + TipePro
                + " ORDER BY TglKontrak, NoKontrak"
                ;
            DataTable rs1 = Db.Rs(strSql1);

            sumD.Text = " <h3> ( " + rs1.Rows.Count.ToString() + " ) </h3>";

            decimal jumPlist = 0;
            decimal jumDisc = 0;
            decimal jumBunga = 0;
            decimal jumNilaiKontrakA = 0;
            decimal jumDPP = 0;
            decimal jumPPN = 0;
            decimal jumLuasSG = 0;
            decimal jumLuasNett = 0;

            decimal jumDiscTambahan = 0;
            decimal jumHargaGimmick = 0;
            decimal jumHargaLainLain = 0;

            for (int i = 0; i < rs1.Rows.Count; i++)
            {
                if (!Response.IsClientConnected)
                    break;

                TableRow r1 = new TableRow();
                r1.Attributes["ondblclick"] = "popEditKontrak('" + rs1.Rows[i]["NoKontrak"] + "')";

                TableCell c1;
                // tabel summary
                c1 = new TableCell();
                c1.Text = (i + 1).ToString();
                c1.HorizontalAlign = HorizontalAlign.Center;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                c1.Text = Cf.Day(rs1.Rows[i]["TglKontrak"]);
                c1.HorizontalAlign = HorizontalAlign.Left;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                c1.Text = rs1.Rows[i]["NoKontrak"].ToString();
                c1.HorizontalAlign = HorizontalAlign.Left;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                c1.Text = rs1.Rows[i]["Customer"].ToString();
                c1.HorizontalAlign = HorizontalAlign.Left;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                c1.Text = rs1.Rows[i]["Agent"].ToString();
                c1.HorizontalAlign = HorizontalAlign.Left;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                c1.Text = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + rs1.Rows[i]["Project"].ToString() + "'");
                c1.HorizontalAlign = HorizontalAlign.Left;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                c1.Text = rs1.Rows[i]["NoUnit"].ToString();
                c1.HorizontalAlign = HorizontalAlign.Left;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                c1.Text = rs1.Rows[i]["Jenis"].ToString();
                c1.HorizontalAlign = HorizontalAlign.Left;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                c1.Text = rs1.Rows[i]["JenisProperti"].ToString();
                c1.HorizontalAlign = HorizontalAlign.Left;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                c1.Text = Cf.Num(rs1.Rows[i]["LuasSG"]);
                jumLuasSG += Convert.ToDecimal(rs1.Rows[i]["LuasSG"]);
                c1.HorizontalAlign = HorizontalAlign.Left;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                c1.Text = Cf.Num(rs1.Rows[i]["LuasNett"]);
                jumLuasNett += Convert.ToDecimal(rs1.Rows[i]["LuasNett"]);
                c1.HorizontalAlign = HorizontalAlign.Left;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                c1.Text = Cf.Num(rs1.Rows[i]["Gross"]);
                jumPlist += Convert.ToDecimal(rs1.Rows[i]["Gross"]);
                c1.HorizontalAlign = HorizontalAlign.Right;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                //decimal Disc1 = 0, AfterDisc1 = 0;
                c1.Text = Cf.Num(rs1.Rows[i]["DiskonRupiah"]);//Cf.Num(Disc1);
                jumDisc += Convert.ToDecimal(rs1.Rows[i]["DiskonRupiah"]);
                c1.HorizontalAlign = HorizontalAlign.Right;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                //decimal Disc1 = 0, AfterDisc1 = 0;
                c1.Text = Cf.Num(rs1.Rows[i]["DiskonTambahan"]);//Cf.Num(Disc1);
                jumDiscTambahan += Convert.ToDecimal(rs1.Rows[i]["DiskonTambahan"]);
                c1.HorizontalAlign = HorizontalAlign.Right;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                //decimal Disc1 = 0, AfterDisc1 = 0;
                c1.Text = Cf.Num(rs1.Rows[i]["HargaGimmick"]);//Cf.Num(Disc1);
                jumHargaGimmick += Convert.ToDecimal(rs1.Rows[i]["HargaGimmick"]);
                c1.HorizontalAlign = HorizontalAlign.Right;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                //decimal Disc1 = 0, AfterDisc1 = 0;
                c1.Text = Cf.Num(rs1.Rows[i]["HargaLainLain"]);//Cf.Num(Disc1);
                jumHargaLainLain += Convert.ToDecimal(rs1.Rows[i]["HargaLainLain"]);
                c1.HorizontalAlign = HorizontalAlign.Right;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                // decimal NominalBunga = Db.SingleDecimal("SELECT BungaNominal FROM MS_KONTRAK WHERE NoKontrak = '"++"' ");
                c1.Text = Cf.Num(rs1.Rows[i]["BungaNominal"]);//Cf.Num(Disc1);
                jumBunga += Convert.ToDecimal(rs1.Rows[i]["BungaNominal"]);
                c1.HorizontalAlign = HorizontalAlign.Right;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                decimal NilaiKontrak1 = Convert.ToDecimal(rs1.Rows[i]["NilaiKontrak"]);
                c1.Text = Cf.Num(NilaiKontrak1);
                jumNilaiKontrakA += NilaiKontrak1;
                c1.HorizontalAlign = HorizontalAlign.Right;
                r1.Cells.Add(c1);

                //hitung DPP + PPN
                decimal bn = 0;
                if (!rs1.Rows[i]["BungaNominal"].Equals(System.DBNull.Value))
                    bn = Convert.ToDecimal(rs1.Rows[i]["BungaNominal"]);
                decimal DPP1 = Convert.ToDecimal(rs1.Rows[i]["NilaiKontrak"]);
                DPP1 = Math.Round(DPP1 / (decimal)1.1);
                c1 = new TableCell();
                if (!Convert.ToBoolean(rs1.Rows[i]["PPN"]))
                {
                    DPP1 = Convert.ToDecimal(rs1.Rows[i]["Gross"]) - Convert.ToDecimal(rs1.Rows[i]["DiskonRupiah"]) + bn;
                }
                if (rs1.Rows[0]["JenisPPN"].ToString() == "PEMERINTAH")
                {
                    DPP1 = Convert.ToDecimal(rs1.Rows[i]["NilaiKontrak"]);
                }

                DPP1 = Convert.ToDecimal(rs1.Rows[i]["NilaiKontrak"]) - Convert.ToDecimal(rs1.Rows[i]["NilaiPPN"]);
                c1.Text = Cf.Num(DPP1);//Cf.Num(TotalBayar);
                jumDPP += DPP1;
                c1.HorizontalAlign = HorizontalAlign.Right;
                r1.Cells.Add(c1);

                c1 = new TableCell();
                decimal PPN1 = 0;
                //				PPN1 = Math.Round(DPP1 * (decimal)0.1);
                PPN1 = Convert.ToDecimal(rs1.Rows[i]["NilaiPPN"]);
                jumPPN += PPN1;
                c1.Text = Cf.Num(PPN1);
                c1.HorizontalAlign = HorizontalAlign.Right;
                r1.Cells.Add(c1);


                rptD.Rows.Add(r1);

                if (i == rs1.Rows.Count - 1)
                {
                    TableRow r4 = new TableRow();

                    TableCell c4;

                    c4 = new TableCell();
                    c4.Text = "<strong>TOTAL</strong>";
                    c4.Attributes["style"] = "border-top:1px solid black";
                    c4.HorizontalAlign = HorizontalAlign.Left;
                    c4.ColumnSpan = 7;
                    r4.Cells.Add(c4);

                    c4 = new TableCell();
                    c4.Text = "<strong>" + rs1.Rows.Count + "</strong>";
                    c4.Attributes["style"] = "border-top:1px solid black";
                    c4.HorizontalAlign = HorizontalAlign.Center;
                    r4.Cells.Add(c4);

                    c4 = new TableCell();
                    c4.Text = "<strong>&nbsp;</strong>";
                    c4.Attributes["style"] = "border-top:1px solid black";
                    c4.HorizontalAlign = HorizontalAlign.Center;
                    c4.ColumnSpan = 2;
                    r4.Cells.Add(c4);

                    c4 = new TableCell();
                    c4.Text = "<strong>" + Cf.Num(jumLuasSG) + "</strong>";
                    c4.Attributes["style"] = "border-top:1px solid black";
                    c4.HorizontalAlign = HorizontalAlign.Center;
                    r4.Cells.Add(c4);

                    c4 = new TableCell();
                    c4.Text = "<strong>" + Cf.Num(jumLuasNett) + "</strong>";
                    c4.Attributes["style"] = "border-top:1px solid black";
                    c4.HorizontalAlign = HorizontalAlign.Center;
                    r4.Cells.Add(c4);

                    c4 = new TableCell();
                    c4.Text = "<strong>" + Cf.Num(jumPlist) + "</strong>";
                    c4.Attributes["style"] = "border-top:1px solid black";
                    c4.HorizontalAlign = HorizontalAlign.Right;
                    r4.Cells.Add(c4);

                    c4 = new TableCell();
                    c4.Text = "<strong>" + Cf.Num(jumDisc) + "</strong>";
                    c4.Attributes["style"] = "border-top:1px solid black";
                    c4.HorizontalAlign = HorizontalAlign.Right;
                    r4.Cells.Add(c4);

                    c4 = new TableCell();
                    c4.Text = "<strong>" + Cf.Num(jumDiscTambahan) + "</strong>";
                    c4.Attributes["style"] = "border-top:1px solid black";
                    c4.HorizontalAlign = HorizontalAlign.Right;
                    r4.Cells.Add(c4);

                    c4 = new TableCell();
                    c4.Text = "<strong>" + Cf.Num(jumHargaGimmick) + "</strong>";
                    c4.Attributes["style"] = "border-top:1px solid black";
                    c4.HorizontalAlign = HorizontalAlign.Right;
                    r4.Cells.Add(c4);

                    c4 = new TableCell();
                    c4.Text = "<strong>" + Cf.Num(jumHargaLainLain) + "</strong>";
                    c4.Attributes["style"] = "border-top:1px solid black";
                    c4.HorizontalAlign = HorizontalAlign.Right;
                    r4.Cells.Add(c4);

                    c4 = new TableCell();
                    c4.Text = "<strong>" + Cf.Num(jumBunga) + "</strong>";
                    c4.Attributes["style"] = "border-top:1px solid black";
                    c4.HorizontalAlign = HorizontalAlign.Right;
                    r4.Cells.Add(c4);

                    c4 = new TableCell();
                    c4.Text = "<strong>" + Cf.Num(jumNilaiKontrakA) + "</strong>";
                    c4.Attributes["style"] = "border-top:1px solid black";
                    c4.HorizontalAlign = HorizontalAlign.Right;
                    r4.Cells.Add(c4);

                    c4 = new TableCell();
                    c4.Text = "<strong>" + Cf.Num(jumDPP) + "</strong>";
                    c4.Attributes["style"] = "border-top:1px solid black";
                    c4.HorizontalAlign = HorizontalAlign.Right;
                    r4.Cells.Add(c4);

                    c4 = new TableCell();
                    c4.Text = "<strong>" + Cf.Num(jumPPN) + "</strong>";
                    c4.Attributes["style"] = "border-top:1px solid black";
                    c4.HorizontalAlign = HorizontalAlign.Right;
                    r4.Cells.Add(c4);

                    rptD.Rows.Add(r4);
                }
            }

            sumD.Text = Convert.ToString(rs1.Rows.Count);

            TableRow r0 = new TableRow();
            rptD.Rows.Add(r0);

            return rptD;
        }

        private void Fill()
        {
            string Lokasi = "";
            if (lokasi.SelectedIndex != 0)
            {
                Lokasi = " AND a.Lokasi = '" + Cf.Str(lokasi.SelectedValue) + "'";
            }

            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);

            if (Dari > Sampai)
            {
                DateTime x = Sampai;
                Sampai = Dari;
                Dari = Sampai;
            }

            string strAdd = "";

            if (ddlAgent.SelectedIndex != 0)
                strAdd += " AND a.NoAgent = " + ddlAgent.SelectedValue;
            //else
            //{
            //    if (UserAgent() > 0)
            //        strAdd += " AND a.NoAgent = " + UserAgent();
            //}
            System.Text.StringBuilder z = new System.Text.StringBuilder();
            bool isFirst = true;

            string Tipe = "";
            z = new System.Text.StringBuilder();
            isFirst = true;
            for (int i = 0; i < cblTipe.Items.Count; i++)
            {
                if (cblTipe.Items[i].Selected)
                {
                    if (isFirst)
                    {
                        z.Append("'" + Cf.Str(cblTipe.Items[i].Value) + "'");
                        isFirst = false;
                    }
                    else
                        z.Append(",'" + Cf.Str(cblTipe.Items[i].Value) + "'");
                }
            }

            if (z.ToString() != "")
                Tipe = " AND a.Jenis IN (" + z.ToString() + ")";

            string TitipJual = "";
            if (titipjual.SelectedIndex != 0)
            {
                TitipJual = " AND TitipJual=" + titipjual.SelectedValue.ToString();
            }
            if (titipjual.SelectedValue.ToString() != "0")
            {
                fillTitipJual(Dari, Sampai, Tipe, strAdd);
            }
            else
            {
                lblD.Visible = rptD.Visible = false;
            }

            string Project = " AND a.Project IN (" + Act.ProjectListSql + ")";
            if (project.SelectedValue != "SEMUA") Project = " AND a.Project = '" + project.SelectedValue + "'";

            string Perusahaan = "";
            if (pers.SelectedValue != "SEMUA") Perusahaan = " AND a.Pers = '" + pers.SelectedValue + "'";

            string TipePro = "";
            if (tipepro.SelectedIndex != 0)
            {
                TipePro = " AND JenisProperti='" + tipepro.SelectedItem.ToString() + "'";
            }

            fillAll(Dari, Sampai, Tipe, strAdd);
            fillBatal(Dari, Sampai, Tipe, strAdd);

            string strSql = "SELECT a.*, b.Nama AS Customer, c.Nama AS Agent, c.Principal, d.Jenis, d.LuasSG, d.LuasNett, d.JenisProperti"
                + " FROM MS_KONTRAK a"
                + " INNER JOIN MS_CUSTOMER b ON a.NoCustomer = b.NoCustomer"
                + " INNER JOIN MS_AGENT c ON a.NoAgent = c.NoAgent"
                + " INNER JOIN MS_UNIT d ON a.NoStock = d.NoStock"
                + " WHERE a.TglKontrak >= '" + Convert.ToDateTime(Dari) + "'"
                + " AND a.TglKontrak <= '" + Convert.ToDateTime(Sampai) + "'"
                + " AND a.STATUS='A'"
                + Project
                + Perusahaan
                + Lokasi
                + Tipe
                + strAdd
                + TitipJual
                + TipePro
                + " ORDER BY TglKontrak, NoKontrak"
                ;
            DataTable rs = Db.Rs(strSql);            

            sumC.Text = " <h3> ( " + rs.Rows.Count.ToString() + " ) </h3>";

            decimal t1 = 0, t2 = 0, t3 = 0, t4 = 0, t5 = 0, t6 = 0, t7 = 0, tBunga = 0, tDiskonTambahan = 0, tHargaGimmick = 0, tHargaLainLain = 0, tLuasSG = 0, tLuasNett = 0;
            decimal gt1 = 0, gt2 = 0, gt3 = 0, gt4 = 0, gt5 = 0, gt6 = 0, gt7 = 0, gtBunga = 0, gtDiskonTambahan = 0, gtHargaGimmick = 0, gtHargaLainLain = 0, gtLuasSG = 0, gtLuasNett = 0;

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected)
                    break;

                TableRow r = new TableRow();
                r.Attributes["ondblclick"] = "popEditKontrak('" + rs.Rows[i]["NoKontrak"] + "')";
                TableCell c;

                t1 += 1;
                gt1 += 1;

                //tabel Netto
                c = new TableCell();
                c.Text = (i + 1).ToString();
                c.HorizontalAlign = HorizontalAlign.Center;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglKontrak"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoKontrak"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Customer"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Agent"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + rs.Rows[i]["Project"].ToString() + "'");
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoUnit"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Jenis"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["JenisProperti"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["LuasSG"]);
                tLuasSG += Convert.ToDecimal(rs.Rows[i]["LuasSG"]);
                gtLuasSG += Convert.ToDecimal(rs.Rows[i]["LuasSG"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["LuasNett"]);
                tLuasNett += Convert.ToDecimal(rs.Rows[i]["LuasNett"]);
                gtLuasNett += Convert.ToDecimal(rs.Rows[i]["LuasNett"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["CaraBayar"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["Gross"]);
                t2 += Convert.ToDecimal(rs.Rows[i]["Gross"]);
                gt2 += Convert.ToDecimal(rs.Rows[i]["Gross"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                decimal Disc = 0, AfterDisc = 0;
                if (rs.Rows[i]["JenisPPN"].ToString() == "KONSUMEN")
                    Disc = ((decimal)1.1 * Convert.ToDecimal(rs.Rows[i]["Gross"]) - Convert.ToDecimal(rs.Rows[i]["NilaiKontrak"])) / (decimal)1.1;
                else
                    Disc = Convert.ToDecimal(rs.Rows[i]["Gross"]) - Convert.ToDecimal(rs.Rows[i]["NilaiKontrak"]);
                AfterDisc = Convert.ToDecimal(rs.Rows[i]["Gross"]) - Disc;
                c.Text = Cf.Num(rs.Rows[i]["DiskonRupiah"]);//Cf.Num(Disc);
                t3 += Convert.ToDecimal(rs.Rows[i]["DiskonRupiah"]);//Disc;
                gt3 += Convert.ToDecimal(rs.Rows[i]["DiskonRupiah"]);//Disc;
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["DiskonTambahan"]);
                tDiskonTambahan += Convert.ToDecimal(rs.Rows[i]["DiskonTambahan"]);
                gtDiskonTambahan += Convert.ToDecimal(rs.Rows[i]["DiskonTambahan"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["HargaGimmick"]);
                tHargaGimmick += Convert.ToDecimal(rs.Rows[i]["HargaGimmick"]);
                gtHargaGimmick += Convert.ToDecimal(rs.Rows[i]["HargaGimmick"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["HargaLainLain"]);
                tHargaLainLain += Convert.ToDecimal(rs.Rows[i]["HargaLainLain"]);
                gtHargaLainLain += Convert.ToDecimal(rs.Rows[i]["HargaLainLain"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["BungaNominal"]);
                tBunga += Convert.ToDecimal(rs.Rows[i]["BungaNominal"]);
                gtBunga += Convert.ToDecimal(rs.Rows[i]["BungaNominal"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                decimal NilaiKontrak = Convert.ToDecimal(rs.Rows[i]["NilaiKontrak"]);
                c.Text = Cf.Num(NilaiKontrak);
                t5 += Convert.ToDecimal(rs.Rows[i]["NilaiKontrak"]);
                gt5 += Convert.ToDecimal(rs.Rows[i]["NilaiKontrak"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                //hitung DPP + PPN
                decimal DPP = Convert.ToDecimal(rs.Rows[i]["NilaiKontrak"]);
                DPP = Math.Round(DPP / (decimal)1.1);

                c = new TableCell();
                decimal bn = 0;
                if (!rs.Rows[i]["BungaNominal"].Equals(System.DBNull.Value))
                    bn = Convert.ToDecimal(rs.Rows[i]["BungaNominal"]);

                decimal TotalBayar = Db.SingleDecimal(
                    "SELECT ISNULL(SUM(NilaiPelunasan), 0) FROM MS_PELUNASAN"
                    + " WHERE NoKontrak = '" + Cf.Pk(rs.Rows[i]["NoKontrak"]) + "'");
                if (!Convert.ToBoolean(rs.Rows[i]["PPN"]))
                {
                    DPP = Convert.ToDecimal(rs.Rows[i]["Gross"]) - Convert.ToDecimal(rs.Rows[i]["DiskonRupiah"]) + bn;
                }
                if (rs.Rows[i]["JenisPPN"].ToString() == "PEMERINTAH")
                {
                    DPP = Convert.ToDecimal(rs.Rows[i]["NilaiKontrak"]);
                }
                DPP = Convert.ToDecimal(rs.Rows[i]["NilaiDPP"]);
                c.Text = Cf.Num(DPP);//Cf.Num(TotalBayar);
                t6 += DPP;//TotalBayar;
                gt6 += DPP;//TotalBayar;
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                decimal PPN = 0;
                PPN = Convert.ToDecimal(rs.Rows[i]["NilaiPPN"]);
                t4 += PPN;
                gt4 += PPN;
                c.Text = Cf.Num(PPN);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                rptC.Rows.Add(r);

                bool x = false;

                if (i < (rs.Rows.Count - 1))
                {
                    if (Convert.ToDateTime(rs.Rows[i + 1]["TglKontrak"]) > Convert.ToDateTime(rs.Rows[i]["TglKontrak"]))
                        x = true;
                }
                else if (i == (rs.Rows.Count - 1))
                    x = true;

                //				if(x)
                //				{
                //					SubTotal(t1, t2, t3, t4, t5, t6, t7, Convert.ToDateTime(rs.Rows[i]["TglKontrak"]));
                //					t1 = t2 = t3 = t4 = t5 = t6 = t7 = 0;
                //				}
                bool y = false;
                if (i == (rs.Rows.Count - 1))
                    y = true;

                sumnetto.Text = Convert.ToString(rs.Rows.Count);

                if (y)
                {
                    GrandTotal(gt1, gt2, gt3, gt4, gt5, gt6, gt7, gtBunga, gtDiskonTambahan, gtHargaGimmick, gtHargaLainLain, gtLuasSG, gtLuasNett, Convert.ToDateTime(rs.Rows[i]["TglKontrak"]));
                }

            }
            //grafik(dari.Text, sampai.Text, Principal, Tipe, strAdd);
        }

        private void SubTotal(decimal t1, decimal t2, decimal t3, decimal t4, decimal t5, decimal t6, decimal t7, DateTime TglKontrak)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = Rpt.Foot();
            c.Text = "SUBTOTAL UNIT PENJUALAN " + Cf.Day(TglKontrak);
            c.ColumnSpan = 6;
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = t1.ToString();
            c.HorizontalAlign = HorizontalAlign.Center;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = "&nbsp;";
            c.HorizontalAlign = HorizontalAlign.Right;
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
            c.Text = Cf.Num(t6);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t4);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            //			c = Rpt.Foot();
            //			c.Text = Cf.Num(t7);
            //			c.HorizontalAlign = HorizontalAlign.Right;
            //			r.Cells.Add(c);
            //
            //			c = Rpt.Foot();
            //			c.Text = "&nbsp;";
            //			r.Cells.Add(c);

            rptC.Rows.Add(r);
        }

        private void GrandTotal(decimal gt1, decimal gt2, decimal gt3, decimal gt4, decimal gt5, decimal gt6, decimal gt7, decimal gtBunga, decimal gtDiskonTambahan, decimal gtHargaGimmick, decimal gtHargaLainLain, decimal gtLuasSG, decimal gtLuasNett, DateTime TglKontrak)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = Rpt.Foot();
            c.Text = "TOTAL ";
            c.ColumnSpan = 6;
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = gt1.ToString();
            c.ColumnSpan = 2;
            c.HorizontalAlign = HorizontalAlign.Center;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = "&nbsp;";
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(gtLuasSG);
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(gtLuasNett);
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = "&nbsp;";
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(gt2);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(gt3);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(gtDiskonTambahan);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(gtHargaGimmick);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(gtHargaLainLain);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(gtBunga);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(gt5);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(gt6);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(gt4);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            rptC.Rows.Add(r);
        }

        private void grafik(string dari, string sampai)
        {
            string strAdd = "";

            if (ddlAgent.SelectedIndex != 0)
                strAdd += " AND NoAgent = " + ddlAgent.SelectedValue;
            else
            {
                if (UserAgent() > 0)
                    strAdd += " AND NoAgent = " + UserAgent();
            }

            string Lokasi = "";
            if (lokasi.SelectedIndex != 0)
            {
                Lokasi = " AND a.Lokasi = '" + Cf.Str(lokasi.SelectedValue) + "'";
            }

            string TitipJual = "";
            if (titipjual.SelectedIndex != 0)
                TitipJual = " AND a.TitipJual=" + titipjual.SelectedValue.ToString();

            string Project = " AND a.Project IN (" + Act.ProjectListSql + ")";
            if (project.SelectedValue != "SEMUA") Project = " AND a.Project = '" + project.SelectedValue + "'";

            string Perusahaan = "";
            if (pers.SelectedValue != "SEMUA") Perusahaan = " AND a.Pers = '" + pers.SelectedValue + "'";

            System.Text.StringBuilder z = new System.Text.StringBuilder();
            bool isFirst = true;

            string Tipe = "";
            z = new System.Text.StringBuilder();
            isFirst = true;
            for (int i = 0; i < cblTipe.Items.Count; i++)
            {
                if (cblTipe.Items[i].Selected)
                {
                    if (isFirst)
                    {
                        z.Append("'" + Cf.Str(cblTipe.Items[i].Value) + "'");
                        isFirst = false;
                    }
                    else
                        z.Append(",'" + Cf.Str(cblTipe.Items[i].Value) + "'");
                }
            }

            if (z.ToString() != "")
                Tipe = " AND a.Jenis IN (" + z.ToString() + ")";

            string TipePro = "";
            if (tipepro.SelectedIndex != 0)
            {
                TipePro = " AND JenisProperti='" + tipepro.SelectedItem.ToString() + "'";
            }

            //penjualan perbulan
            TableRow r = new TableRow();
            TableRow r1 = new TableRow();

            int counting = 0;

            //hitung jumlah bulan
            string dbulan = Convert.ToString(Convert.ToDateTime(dari).Month);
            string sbulan = Convert.ToString(Convert.ToDateTime(sampai).Month);
            string dtahun = Convert.ToString(Convert.ToDateTime(dari).Year);
            string stahun = Convert.ToString(Convert.ToDateTime(sampai).Year);

            int y = 0;
            int jumlahBulan = 0;
            if ((Convert.ToInt32(stahun) - Convert.ToInt32(dtahun)) >= 1)
            {
                y = (int)12 * ((Convert.ToInt32(stahun) - Convert.ToInt32(dtahun)) - (int)1);
                jumlahBulan = (12 - Convert.ToInt32(Convert.ToDateTime(dari).Month)) + y + Convert.ToInt32(Convert.ToDateTime(sampai).Month);
            }
            else
            {
                jumlahBulan = Convert.ToInt32(Convert.ToDateTime(sampai).Month) - Convert.ToInt32(Convert.ToDateTime(dari).Month);
            }

            int batasulang = Convert.ToInt32(Convert.ToDateTime(dari).Month) + jumlahBulan;
            //Response.Write(strAdd);
            int bl = Convert.ToInt32(Convert.ToDateTime(dari).Month);
            int thn = Convert.ToInt32(Convert.ToDateTime(dari).Year);

            for (int i = Convert.ToInt32(Convert.ToDateTime(dari).Month); i <= batasulang; i++)
            {
                TableCell c;

                if (bl > 12)
                {
                    thn++;
                    bl = 1;
                }
                string sampaitgl = bl
                                + "/"
                                + DateTime.DaysInMonth(thn, bl)
                                + "/"
                                + thn;
                //Response.Write(sampaitgl + "<br />");
                int d = Convert.ToDateTime(dari).Day;
                if (counting > 1)
                    d = 1;

                string daritgl = bl
                    + "/"
                    + d
                    + "/"
                    + thn;
                
                int qbatalBulan = Db.SingleInteger("SELECT ISNULL(COUNT(*),0)"
                    + " FROM MS_KONTRAK a INNER JOIN MS_UNIT u ON a.NoStock = u.NoStock"
                    + " WHERE TglBatal >= '" + Cf.Tgl(Convert.ToDateTime(daritgl)) + "'"
                    + " AND TglBatal <= '" + Cf.Tgl(Convert.ToDateTime(sampaitgl)) + "'"
                    + " AND a.Status = 'B'"
                    + Project
                    + Perusahaan
                    + strAdd
                    + Tipe
                    + Lokasi
                    + TitipJual
                    + TipePro
                    + " AND TglBatal is not null");               

                int qallBulan = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM MS_KONTRAK a INNER JOIN MS_UNIT u ON a.NoStock = u.NoStock"
                    + " WHERE TglKontrak >= '" + Convert.ToDateTime(daritgl) + "'"
                    + " AND TglKontrak <= '" + Convert.ToDateTime(sampaitgl) + "'"
                    + Project
                    + Perusahaan
                    + strAdd
                    + Tipe
                    + Lokasi
                    + TitipJual
                    + TipePro
                    );

                int qnetBulan = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM MS_KONTRAK a INNER JOIN MS_UNIT u ON a.NoStock = u.NoStock"
                    + " WHERE TglKontrak >= '" + Convert.ToDateTime(daritgl) + "'"
                    + " AND TglKontrak <= '" + Convert.ToDateTime(sampaitgl) + "'"
                    + " AND a.STATUS='A'"
                    + Project
                    + Perusahaan
                    + strAdd
                    + Tipe
                    + Lokasi
                    + TitipJual
                    + TipePro
                    );

                c = new TableCell();
                c.Text = "<p>" + qallBulan + "</p><img src='/Media/g2.jpg' height='" + qallBulan * (int)5 + "' width='15px' />";
                c.HorizontalAlign = HorizontalAlign.Center;
                c.VerticalAlign = VerticalAlign.Bottom;
                c.Attributes["style"] = "margin:0px; padding:0px; height:200px";
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "<p>" + qbatalBulan + "</p><img src='/Media/g1.jpg' height='" + qbatalBulan * (int)5 + "' width='15px' />";
                c.HorizontalAlign = HorizontalAlign.Center;
                c.VerticalAlign = VerticalAlign.Bottom;
                c.Attributes["style"] = "margin:0px; padding:0px";
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "<p>" + qnetBulan + "</p><img src='/Media/g3.jpg' height='" + qnetBulan * (int)5 + "' width='15px' />";
                c.HorizontalAlign = HorizontalAlign.Center;
                c.VerticalAlign = VerticalAlign.Bottom;
                c.Attributes["style"] = "margin:0px; padding:0px";
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "&nbsp;&nbsp;";
                //c.Attributes["style"] = "padding-right:20px";
                c.HorizontalAlign = HorizontalAlign.Center;
                c.Attributes["style"] = "margin:0px; padding:0px";
                r.Cells.Add(c);

                TableCell c1;

                c1 = new TableCell();
                c1.Text = Cf.Monthname(bl) + "<br />" + thn;
                c1.HorizontalAlign = HorizontalAlign.Center;
                c1.Attributes["style"] = "border-top:solid black 2px";
                c1.ColumnSpan = 4;

                r1.Cells.Add(c1);

                counting++;
                bl++;
            }
            graph.Rows.Add(r);
            graph.Rows.Add(r1);
        }

        private void Header()
        {
            System.Text.StringBuilder x = new System.Text.StringBuilder();

            Rpt.Judul(x, comp, judul);

            if (statusA.Checked)
                Rpt.SubJudul(x, "Status : " + statusA.Text);
            else if (statusB.Checked)
                Rpt.SubJudul(x, "Status : " + statusB.Text);
            else
                Rpt.SubJudul(x, "Status : " + statusS.Text);

            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);
            Rpt.SubJudul(x
                , "Tanggal : " + Cf.Day(dari.Text) + " s/d " + Cf.Day(sampai.Text)
                );

            string strAgent = "SEMUA";
            if (ddlAgent.SelectedIndex != 0)
                strAgent = ddlAgent.SelectedItem.Text;
            Rpt.SubJudul(x
                , "Sales : " + strAgent
                );
            Rpt.SubJudul(x
                , "Lokasi : " + lokasi.SelectedItem.Text
                );

            string strPrincipal = "SEMUA";
            System.Text.StringBuilder z = new System.Text.StringBuilder();
            bool isFirst = true;
            for (int i = 0; i < cblPrincipal.Items.Count; i++)
            {
                if (cblPrincipal.Items[i].Selected)
                {
                    if (isFirst)
                    {
                        z.Append(cblPrincipal.Items[i].Text);
                        isFirst = false;
                    }
                    else
                        z.Append("," + cblPrincipal.Items[i].Text);
                }
            }

            if (z.ToString() != "")
                strPrincipal = z.ToString();
            Rpt.SubJudul(x
                , "Principal : " + strPrincipal
                );

            string strTipe = "SEMUA";
            z = new System.Text.StringBuilder();
            isFirst = true;
            for (int i = 0; i < cblTipe.Items.Count; i++)
            {
                if (cblTipe.Items[i].Selected)
                {
                    if (isFirst)
                    {
                        z.Append(cblTipe.Items[i].Text);
                        isFirst = false;
                    }
                    else
                        z.Append("," + cblTipe.Items[i].Text);
                }
            }

            if (z.ToString() != "")
                strTipe = z.ToString();
            Rpt.SubJudul(x
                , "Tipe : " + strTipe
                );

            Rpt.Header(rptC, x);
        }

        private void newHeader()
        {
            string TitipJual = "";
            if (titipjual.SelectedIndex != 0)
                TitipJual = titipjual.SelectedItem.ToString();

            string header = "<h2>" + Mi.Pt + "</h2>";
            header += "<h1 class='title'>LAPORAN PENJUALAN</h1>";
            header += "Periode : " + Cf.Day(dari.Text) + " s/d " + Cf.Day(sampai.Text);
            if (titipjual.SelectedIndex != 0)
            {
                header += "<br/> Status Titip Jual : " + TitipJual;
            }
            else
            {
                header += "<br/> Status Titip Jual : SEMUA";
            }
            header += "<br/> Project : " + project.SelectedItem.Text;
            header += "<br/> Perusahaan : " + pers.SelectedItem.Text;
            header += "<br/> Laporan dihasilkan pada hari " + Cf.IndoWeek(DateTime.Today);
            header += ", " + Cf.Date(DateTime.Now) + " dari workstation " + Act.IP + " oleh user " + Act.UserID + "<br />";
            headJudul.Text = header;
            //Response.Write(header);
        }

        private void sumReport()
        {
            param.Visible = false;

            Fill();

            lblA.Text = "<h3>A. Summary Penjualan</h3>";

            lblB.Text = "<h3>B. Pembatalan Unit</h3>";

            lblC.Text = "<h3>C. Penjualan Netto</h3>";

            newHeader();
            //Header();
        }

        private void Report()
        {
            param.Visible = false;

            graph.Visible = true;
            summary.Visible = true;
            lblA.Text = "<h3>A. Summary Penjualan</h3>";
            rptA.Visible = true;

            lblB.Text = "<h3>B. Pembatalan Unit</h3>";
            rptB.Visible = true;

            lblC.Text = "<h3>C. Penjualan Netto</h3>";
            rptC.Visible = true;

            lblD.Text = "<h3>D. Titip Jual</h3>";
            rptD.Visible = true;

            newHeader();
            //Header();
            Fill();
            grafik(dari.Text, sampai.Text);
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

        protected void scr_Click(object sender, System.EventArgs e)
        {
            if (valid())
                //sumReport();
                Report();
        }

        protected void xls_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                Report();
                //Rpt.ToExcel(this, rptA);
                //Rpt.ToExcel(this, rptB);
                rpt.Controls.Add(headJudul);
                //rpt.Controls.Add(graph);
                rpt.Controls.Add(lblA);
                rpt.Controls.Add(rptA);
                rpt.Controls.Add(lblB);
                rpt.Controls.Add(rptB);
                rpt.Controls.Add(lblC);
                rpt.Controls.Add(rptC);
                rpt.Controls.Add(rptD);
                Rpt.ToExcel(this, rpt);


            }
        }

        protected void cbPrincipal_CheckedChanged(object sender, System.EventArgs e)
        {
            for (int i = 0; i < cblPrincipal.Items.Count; i++)
            {
                cblPrincipal.Items[i].Selected = cbPrincipal.Checked;
            }
        }

        protected void cbTipe_CheckedChanged(object sender, System.EventArgs e)
        {
            for (int i = 0; i < cblTipe.Items.Count; i++)
            {
                cblTipe.Items[i].Selected = cbTipe.Checked;
            }
        }

        protected void tipesales_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindAgent();
        }

        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            tipepro.Items.Clear();
            tipesales.Items.Clear();
            Bind();
            BindAgent();
        }
    }
}

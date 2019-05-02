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

namespace ISC064.MARKETINGJUAL.Laporan
{
    public partial class MasterKontrak : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();

            if (!Page.IsPostBack)
            {
                Act.ProjectList(project);
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
            dari.Text = Cf.Day(Cf.AwalBulan());
            sampai.Text = Cf.Day(Cf.AkhirBulan());

            lokasi.SelectedIndex = 0;

            DataTable rs;
            string Project = project.SelectedIndex == 0 ? "Project IN (" + Act.ProjectListSql + ")" : "Project = '" + project.SelectedValue + "'";
            rs = Db.Rs("SELECT * FROM REF_JENIS WHERE " + Project + " ORDER BY SN");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["Jenis"].ToString();
                string t = v + " - " + rs.Rows[i]["Nama"].ToString();
                jenis.Items.Add(new ListItem(t, v));
                jenis.Items[i].Selected = true;
            }

            //jenis.SelectedIndex = 0;

            //cblcarabayar.SelectedIndex = 0;

            cblcarabayar.Attributes["style"] = "margin-top:20px";
            cblcarabayar.Items.Add(new ListItem("Cash Keras", "CASH KERAS"));
            cblcarabayar.Items.Add(new ListItem("Cash Bertahap", "CASH BERTAHAP"));
            cblcarabayar.Items.Add(new ListItem("KPR", "KPR"));
            for (int i = 0; i < 3; i++)
            {
                cblcarabayar.Items[i].Selected = true;
            }

            rs = Db.Rs("SELECT * FROM REF_LOKASI WHERE " + Project + " ORDER BY Lokasi");
            for (int i = 0; i < rs.Rows.Count; i++)
                lokasi.Items.Add(new ListItem(rs.Rows[i][0].ToString()));
        }

        protected void pdf_Click(object sender, System.EventArgs e)
        {

            Process p = new System.Diagnostics.Process();

            string Nama = "Laporan Master Kontrak";
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

            string nfilename = "MasterKontrak" + NoAttachment + ".pdf";

            //update filename
            Db.Execute("UPDATE ISC064_MARKETINGJUAL..LapPDF SET FileName= '" + nfilename + "' WHERE AttachmentID = " + NoAttachment);


            //folder untuk menyimpan file pdf
            string save = Mi.PathFilePDFReport + "MasterKontrak" + rs.Rows[0]["AttachmentID"] + ".pdf";

            //declare parameter
            string Lokasi = lokasi.SelectedValue.Replace(" ", "%");
            string nStatusS = "";
            string nStatusA = "";
            string nStatusB = "";
            if (statusS.Checked == true)
                nStatusS = statusS.Text;
            else
                nStatusS = "";
            if (statusA.Checked == true)
                nStatusA = statusA.Text;
            else
                nStatusA = "";
            if (statusB.Checked == true)
                nStatusB = statusB.Text;
            else
                nStatusB = "";

            //string Agent = agent.SelectedValue;

            string SPelunasan = String.Empty;

            if (semua.Checked)
                SPelunasan = "SEMUA";
            else if (statusL0.Checked)
                SPelunasan = "statusL0";
            else if (statusL.Checked)
                SPelunasan = "statusL";
            else if (statusL1.Checked)
                SPelunasan = "statusL1";
            else if (statusL2.Checked)
                SPelunasan = "statusL2";

            string nm = string.Empty;
            string nm2 = string.Empty;
            try
            {
                foreach (ListItem item in jenis.Items)
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

            //Cara Bayar
            try
            {
                foreach (ListItem item in cblcarabayar.Items)
                {
                    if (item.Selected == true)
                    {
                        nm2 += item.Value.Replace(" ", "+") + "-";
                    }
                }
            }
            catch (Exception)
            {
            }
            string BF1 = "";
            string BF2 = "";
            string BF3 = "";

            if (bf1.Checked)
                BF1 = "bf1";
            else if (bf2.Checked)
                BF2 = "bf2";
            else
                BF3 = "bf3";

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
            string link = Mi.PathAlamatWeb + "marketingjual/LaporanPDF/PDFMasterKontrak.aspx?id=" + rs.Rows[0]["AttachmentID"]
                + "&status_s=" + nStatusS
                + "&status_b=" + nStatusB
                + "&status_a=" + nStatusA
                //+ "&agent=" + agent.SelectedValue
                + "&lokasi=" + lokasi.SelectedValue
                + "&bf1=" + BF1
                + "&bf2=" + BF2
                + "&bf3=" + BF3
                + "&tipe=" + nm //jenis
                + "&statuspelunasan=" + BF3
                + "&statuspelunasan=" + SPelunasan
                + "&carabayar=" + nm2
                + "&userid=" + UserID
                + "&project=" + Project
                + "&pers=" + pers.SelectedValue
                ;

            //update link
            Db.Execute("UPDATE ISC064_MARKETINGJUAL..LapPDF SET Link= '" + link + "' WHERE AttachmentID = " + NoAttachment);

            //format page
            p.StartInfo.Arguments = "--orientation landscape --page-width 45cm --page-height 80cm --margin-left 0 --margin-right 0 --margin-top 0.25cm --margin-bottom 0 " + link + " " + save;

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

            if (!Cf.isPilih(jenis))
            {
                x = false;
                jenisc.Text = " Pilih Minimum Satu";
            }
            else
                jenisc.Text = "";

            if (!x && s != "")
            {
                RegisterStartupScript("err"
                    , "<script type='text/javascript'>document.getElementById('" + s + "').select()</script>");
            }


            if (!Cf.isPilih(cblcarabayar))
            {
                x = false;
                if (s == "")
                    s = cblcarabayar.ID;

                errcarabayar.Text = "Pilih minimum satu";
            }
            else
                errcarabayar.Text = "";

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
                Rpt.ToExcel(this, headReport, rpt);
            }
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

            if (statusA.Checked)
                Rpt.SubJudul(x, "Status : " + statusA.Text);
            else if (statusB.Checked)
                Rpt.SubJudul(x, "Status : " + statusB.Text);
            else
                Rpt.SubJudul(x, "Status : " + statusS.Text);

            if (bf1.Checked)
                Rpt.SubJudul(x, "TTS : " + bf1.Text);
            else if (bf2.Checked)
                Rpt.SubJudul(x, "TTS : " + bf2.Text);
            else
                Rpt.SubJudul(x, "TTS : " + bfS.Text);

            string tgl = "";
            if (tglkontrak.Checked) tgl = tglkontrak.Text;

            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);
            Rpt.SubJudul(x
                , tgl + " : " + Cf.Day(dari.Text) + " s/d " + Cf.Day(sampai.Text)
                );

            Rpt.SubJudul(x
                , "Jenis : " + Rpt.inSql(jenis).Replace("'", "")
                );

            //Rpt.Header(rpt, x);
            string legend = "Status: A = Aktif / B = Batal.< br />"
                        + "Luas dalam meter persegi.Gross adalah harga sebelum diskon.";
            Rpt.HeaderReport(headReport, legend, x);
        }

        private void Fill()
        {
            string Status = "";
            if (statusA.Checked) Status = " AND MS_KONTRAK.Status = 'A'";
            if (statusB.Checked) Status = " AND MS_KONTRAK.Status = 'B'";

            string Lunas = "";
            if (semua.Checked) Lunas = "";
            if (statusL0.Checked) Lunas = "AND MS_KONTRAK.PersenLunas = '0'";
            if (statusL.Checked) Lunas = " AND MS_KONTRAK.PersenLunas > '0'";
            if (statusL1.Checked) Lunas = " AND MS_KONTRAK.PersenLunas >= '20'";
            if (statusL2.Checked) Lunas = " AND MS_KONTRAK.PersenLunas >= '100'";

            string Bf = "";
            if (bf1.Checked) Bf = " AND (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM MS_PELUNASAN WHERE NoKontrak = MS_KONTRAK.NoKontrak) < 10000000";
            if (bf2.Checked) Bf = " AND (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM MS_PELUNASAN WHERE NoKontrak = MS_KONTRAK.NoKontrak) >= 10000000";

            string tgl = "";
            string order = "";
            if (tglkontrak.Checked)
            {
                tgl = "TglKontrak";
                order = "NoKontrakManual";
            }

            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);
            if (Dari > Sampai)
            {
                DateTime x = Sampai;
                Sampai = Dari;
                Dari = x;
            }

            string Lokasi = "";
            if (lokasi.SelectedIndex != 0)
            {
                Lokasi = " AND Lokasi = '" + Cf.Str(lokasi.SelectedValue) + "'";
            }
            
            string aa = "";
            if (UserAgent() > 0)
            {
                aa = " AND MS_KONTRAK.NoAgent = " + UserAgent();
            }

            string carabayar = "";
            System.Text.StringBuilder z = new System.Text.StringBuilder();
            bool isFirst = true;
            for (int i = 0; i < cblcarabayar.Items.Count; i++)
            {
                if (cblcarabayar.Items[i].Selected)
                {
                    if (isFirst)
                    {
                        z.Append("'" + Cf.Str(cblcarabayar.Items[i].Text) + "'");
                        isFirst = false;
                    }
                    else
                        z.Append(",'" + Cf.Str(cblcarabayar.Items[i].Text) + "'");
                }
            }
            if (z.ToString() != "")
                carabayar = " AND MS_KONTRAK.CaraBayar IN (" + z.ToString() + ")";

            string Project = " AND MS_KONTRAK.Project IN (" + Act.ProjectListSql + ")";
            if (project.SelectedValue != "SEMUA") Project = " AND MS_KONTRAK.Project = '" + project.SelectedValue + "'";

            string Perusahaan = "";
            if (pers.SelectedValue != "SEMUA") Perusahaan = " AND MS_KONTRAK.Pers = '" + pers.SelectedValue + "'";

            decimal t1 = 0;
            decimal t2 = 0;
            decimal t3 = 0;
            decimal t4 = 0;
            decimal t5 = 0;
            decimal t6 = 0;
            decimal tBunga = 0;
            decimal t7 = 0, t8 = 0;

            decimal tDiskonTambahan = 0;
            decimal tHargaGimmick = 0;
            decimal tHargaLainLain = 0;

            string strSql = "SELECT "
                + " NoKontrak"
                + ",TglKontrak"
                + ",MS_KONTRAK.TglInput"
                + ",Jenis"
                + ",Lokasi"
                + ",NoUnit"
                + ",NUP"
                + ", Skema"
                + ",MS_CUSTOMER.Nama AS Cs"
                + ",MS_AGENT.Nama AS Ag"
                + ",MS_AGENT.Principal"
                + ",Luas"
                + ",Gross"
                + ",DiskonRupiah"
                + ",BungaNominal"
                + ",NilaiKontrak"
                + ",MS_KONTRAK.Status"
                + ",NoST"
                + ",PersenLunas"
                + ",TglST"
                + ",TglPPJB"
                + ",NoPPJB"
                + ",TglAJB"
                + ",NoAJB"
                + ",DiskonTambahan "
                + ",HargaGimmick "
                + ",HargaLainLain "
                + ",MS_KONTRAK.NoVA as nono"
                + ", CaraBayar "
                + ",(SELECT ISNULL(SUM(NilaiPelunasan),0) FROM MS_PELUNASAN WHERE NoKontrak = MS_KONTRAK.NoKontrak) AS NilaiTTS"
                + ", JenisPPN"
                + ",NoStock"
                + " FROM MS_KONTRAK"
                + " INNER JOIN MS_CUSTOMER ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
                + " INNER JOIN MS_AGENT ON MS_KONTRAK.NoAgent = MS_AGENT.NoAgent "
                + " WHERE 1=1 "
                + " AND CONVERT(varchar," + tgl + ",112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND CONVERT(varchar," + tgl + ",112) <= '" + Cf.Tgl112(Sampai) + "'"
                 + " AND Jenis IN (" + Rpt.inSql(jenis) + ")"
                 + Lokasi
                 + Perusahaan
                 + Project
                 + Lunas
                 //+ " AND MS_KONTRAK.PersenLunas >= '20'"
                 + Status
                 + Bf
                 + carabayar
                 + aa
                + " ORDER BY " + order;

            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                r.VerticalAlign = VerticalAlign.Top;
                r.Attributes["ondblclick"] = "popEditKontrak('" + rs.Rows[i]["NoKontrak"] + "')";

                c = new TableCell();
                c.Text = Cf.Num(i + 1);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoKontrak"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglKontrak"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglInput"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Status"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Cs"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Lokasi"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Jenis"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoUnit"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NUP"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                decimal LuasSG = Db.SingleDecimal("SELECT LuasSG FROM MS_UNIT WHERE NoStock = '" + rs.Rows[i]["NoStock"] + "'");
                c = new TableCell();
                c.Text = Cf.Num(LuasSG);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Skema"].ToString();
                //rs.Rows[i]["Skema"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["CaraBayar"].ToString();
                //rs.Rows[i]["Skema"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["nono"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["Gross"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                //				decimal Disc = 0, AfterDisc = 0;
                //				if(rs.Rows[0]["JenisPPN"].ToString() == "KONSUMEN")
                //					Disc = ((decimal)1.1 * Convert.ToDecimal(rs.Rows[i]["Gross"]) - Convert.ToDecimal(rs.Rows[i]["NilaiKontrak"])) / (decimal)1.1;
                //				else
                //					Disc = Convert.ToDecimal(rs.Rows[i]["Gross"]) - Convert.ToDecimal(rs.Rows[i]["NilaiKontrak"]);
                //				AfterDisc = Convert.ToDecimal(rs.Rows[i]["Gross"]) - Disc;
                c.Text = Cf.Num(rs.Rows[i]["DiskonRupiah"]);//Cf.Num(Disc);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["BungaNominal"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["DiskonTambahan"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                //c = new TableCell();
                //c.Text = Cf.Num(rs.Rows[i]["HargaGimmick"]);
                //c.HorizontalAlign = HorizontalAlign.Right;
                //r.Cells.Add(c);

                //c = new TableCell();
                //c.Text = Cf.Num(rs.Rows[i]["HargaLainLain"]);
                //c.HorizontalAlign = HorizontalAlign.Right;
                //r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["NilaiKontrak"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);


                decimal adm = Db.SingleDecimal("SELECT ISNULL(SUM(NILAITAGIHAN),0) FROM MS_TAGIHAN WHERE TIPE='ADM' AND NOKONTRAK='" + rs.Rows[i]["NoKontrak"] + "'");

                c = new TableCell();
                c.Text = Cf.Num(adm);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                decimal fo = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiTagihan),0) FROM MS_TAGIHAN WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"].ToString() + "' AND NamaTagihan LIKE '%FITTING OUT%'");
                c.Text = Cf.Num(fo);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["NilaiTTS"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                decimal Sisa = (Convert.ToDecimal(rs.Rows[i]["NilaiKontrak"]) + adm) - Convert.ToDecimal(rs.Rows[i]["NilaiTTS"]);
                c.Text = Cf.Num(Sisa);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["PersenLunas"].ToString()) + " %";
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                // if (Convert.ToDecimal(rs.Rows[i]["PersenLunas"]) == 100)
                // {
                // c.Text = Cf.Day(Db.SingleTime("SELECT TOP 1 TglPelunasan FROM MS_PELUNASAN WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"].ToString() + "' ORDER BY TglPelunasan DESC"));
                // }
                // else
                // {
                c.Text = "";
                // }
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
                c.Text = Cf.Day(rs.Rows[i]["TglPPJB"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoPPJB"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglST"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoST"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglAJB"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoAJB"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                rpt.Rows.Add(r);

                t1 = t1 + LuasSG;
                t2 = t2 + (decimal)rs.Rows[i]["Gross"];
                t3 += (decimal)rs.Rows[i]["DiskonRupiah"];
                t4 = t4 + (decimal)rs.Rows[i]["NilaiKontrak"];
                t5 = t5 + (decimal)rs.Rows[i]["NilaiTTS"];
                t6 = t6 + Sisa;
                t7 += fo;
                t8 = t8 + adm;
                tBunga += (decimal)rs.Rows[i]["BungaNominal"];

                tDiskonTambahan += (decimal)rs.Rows[i]["DiskonTambahan"];
                tHargaGimmick += (decimal)rs.Rows[i]["HargaGimmick"];
                tHargaLainLain += (decimal)rs.Rows[i]["HargaLainLain"];

                if (i == rs.Rows.Count - 1)
                    SubTotal("GRAND TOTAL", t1, t2, t3, t4, t5, t6, t7, tBunga, tDiskonTambahan, tHargaGimmick, tHargaLainLain, t8);
            }
        }

        private void SubTotal(string txt, decimal t1, decimal t2, decimal t3, decimal t4, decimal t5, decimal t6, decimal t7, decimal tBunga, decimal tDiskonTambahan, decimal tHargaGimmick, decimal tHargaLainLain, decimal t8)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = Rpt.Foot();
            c.Text = txt;
            c.ColumnSpan = 10;
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t1);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
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
            c.Text = Cf.Num(tBunga);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(tDiskonTambahan);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            //c = Rpt.Foot();
            //c.Text = Cf.Num(tHargaGimmick);
            //c.HorizontalAlign = HorizontalAlign.Right;
            //r.Cells.Add(c);

            //c = Rpt.Foot();
            //c.Text = Cf.Num(tHargaLainLain);
            //c.HorizontalAlign = HorizontalAlign.Right;
            //r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t4);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t8);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t7);
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
            c.Text = "";
            c.ColumnSpan = 10;
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);
            rpt.Rows.Add(r);
        }

        protected void jenisCheck_CheckedChanged(object sender, System.EventArgs e)
        {
            for (int i = 0; i < jenis.Items.Count; i++)
            {
                jenis.Items[i].Selected = jenisCheck.Checked;
            }

            Js.Focus(this, jenisCheck);
            jenisc.Text = "";
        }

        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            pers.Items.Clear();
            pers.Items.Add("SEMUA");
            if (project.SelectedValue != "SEMUA")
            {
                string strSql = "SELECT * FROM ISC064_SECURITY..PTSec A "
                + "INNER JOIN ISC064_SECURITY..REF_PERS B ON A.Pers = B.Pers "
                + "INNER JOIN ISC064_SECURITY..REF_PROJECT C ON A.Pers = C.Pers "
                + " WHERE A.UserID='" + Act.UserID + "' AND C.Project ='" + project.SelectedValue + "'  AND A.Granted = 1";

                DataTable rs = Db.Rs(strSql);
                for (int i = 0; i < rs.Rows.Count; i++)
                {
                    string t = rs.Rows[i]["Nama"].ToString();
                    string v = rs.Rows[i]["Pers"].ToString();
                    pers.Items.Add(new ListItem(t, v));
                }
            }
            cblcarabayar.Items.Clear();
            jenis.Items.Clear();
            lokasi.Items.Clear();
            //agent.Items.Clear();
            init();
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

        protected void cbcarabayar_CheckedChanged(object sender, System.EventArgs e)
        {
            for (int i = 0; i < 3; i++)
            {
                cblcarabayar.Items[i].Selected = cbcarabayar.Checked;
            }
        }
    }
}

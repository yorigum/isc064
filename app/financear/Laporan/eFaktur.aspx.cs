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

namespace ISC064.FINANCEAR.Laporan
{
    public partial class eFaktur : System.Web.UI.Page
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

        protected string BersihKoma(string kata)
        {
            if (kata != null)
            {
                if (kata.Contains(","))
                {
                    kata = kata.Replace(",", "");
                }
            }

            return kata;
        }

        private void init()
        {
            dari.Text = Cf.Day(Cf.AwalBulan());
            sampai.Text = Cf.Day(Cf.AkhirBulan());
            string Project = project.SelectedValue == "SEMUA" ? "Project IN (" + Act.ProjectListSql + ")" : "Project = '" + project.SelectedValue + "'";
            DataTable rs;

            rs = Db.Rs("SELECT DISTINCT UserID FROM MS_TTS WHERE " + Project + " ORDER BY UserID");
            for (int i = 0; i < rs.Rows.Count; i++)
                kasir.Items.Add(new ListItem(
                    rs.Rows[i][0].ToString()));

            kasir.SelectedIndex = 0;

            string strSql1 = "SELECT DISTINCT Lokasi FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT WHERE " + Project + " ORDER BY LOKASI ASC ";
            DataTable rs1 = Db.Rs(strSql1);

            for (int i = 0; i < rs1.Rows.Count; i++)
            {
                ddlLokasi.Items.Add(new ListItem(rs1.Rows[i]["Lokasi"].ToString()));
            }
            ddlLokasi.SelectedIndex = 0;

            //tipe
            string[] x = Sc.MktCatalog();
            for (int i = 0; i <= x.GetUpperBound(0); i++)
            {
                string[] xdetil = x[i].Split(';');
                tipe.Items.Add(new ListItem(xdetil[2], xdetil[1]));
                tipe.Items[i].Selected = true;
            }

            rs = Db.Rs("SELECT * FROM REF_ACC WHERE " + Project + "");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["Acc"].ToString();
                string t = v + " : " + rs.Rows[i]["Bank"] + " " + rs.Rows[i]["Rekening"];
                lbAcc.Items.Add(new ListItem(t, v));
            }

            lbAcc.SelectedIndex = 0;
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

            if (!Cf.isPilih(tipe))
            {
                x = false;
                tipec.Text = " Pilih Minimum Satu";
            }
            else
                tipec.Text = "";

            if (!Cf.isPilih(carabayar))
            {
                x = false;
                carabayarc.Text = " Pilih Minimum Satu";
            }
            else
                carabayarc.Text = "";

            if (!x && s != "")
            {
                RegisterStartupScript("err"
                    , "<script type='text/javascript'>document.getElementById('" + s + "').select()</script>");
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
                ReportExcel();
                Rpt.ToExcel(this, headReport, rpt);
            }
        }

        protected void pdf_Click(object sender, System.EventArgs e)
        {

            Process p = new System.Diagnostics.Process();

            string Nama = "Laporan eFaktur";
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
                    + ",'" + Convert.ToDateTime(dari.Text) + "'"
                    + ",'" + Convert.ToDateTime(sampai.Text) + "'"
                    );

            //get nomor customer terbaru
            int NoAttachment = Db.SingleInteger(
                "SELECT TOP 1 AttachmentID FROM LapPDF ORDER BY AttachmentID DESC");

            string strSql = "SELECT * FROM " + Mi.DbPrefix + "FINANCEAR..LapPDF WHERE AttachmentID  = '" + NoAttachment + "'";
            DataTable rs = Db.Rs(strSql);

            string nfilename = "eFaktur" + NoAttachment + ".pdf";

            //update filename
            Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..LapPDF SET FileName= '" + nfilename + "' WHERE AttachmentID = " + NoAttachment);


            //folder untuk menyimpan file pdf
            string save = Mi.PathFilePDFReport + "eFaktur" + rs.Rows[0]["AttachmentID"] + ".pdf";

            //parameter
            //Tipe
            string Tipe = tipe.SelectedValue;
            //Status
            string nStatusS = "";
            string nStatusB = "";
            string nStatusP = "";
            string nStatusV = "";

            if (statusS.Checked == true)
                nStatusS = statusS.Text;
            else
                nStatusS = "";
            if (statusB.Checked == true)
                nStatusB = statusB.Text;
            else
                nStatusB = "";
            if (statusP.Checked == true)
                nStatusP = statusP.Text;
            else
                nStatusP = "";
            if (statusV.Checked == true)
                nStatusV = statusV.Text;
            else
                nStatusV = "";

            //Tanggal
            string tgl = "";
            if (tgltts.Checked) tgl = "tgltts";
            if (tglinput.Checked) tgl = "tglinput";
            if (tglbkm.Checked) tgl = "tglbkm";
            if (tglbg.Checked) tgl = "tglbg.Text";

            //CaraBayar
            string crbayar = string.Empty;
            try
            {
                foreach (ListItem item in carabayar.Items)
                {
                    if (item.Selected == true)
                    {
                        crbayar += item.Value.Replace(" ", "%") + "-";
                    }
                }
            }
            catch (Exception)
            {
            }

            //Tipe
            string nmtipe = string.Empty;
            try
            {
                foreach (ListItem item in tipe.Items)
                {
                    if (item.Selected == true)
                    {
                        nmtipe += item.Value.Replace(" ", "%") + "-";
                    }
                }
            }
            catch (Exception)
            {
            }

            //Lokasi
            string Lokasi = ddlLokasi.SelectedValue.Replace(" ", "%");
            string Kasir = kasir.SelectedValue;
            string Rek = lbAcc.SelectedValue;

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
            string link = Mi.PathAlamatWeb + "financear/LaporanPDF/PDFeFaktur.aspx?id=" + rs.Rows[0]["AttachmentID"]
                + "&lokasi=" + Lokasi
                + "&kasir=" + Kasir
                + "&rek=" + Rek
                + "&status_p=" + nStatusP
                + "&status_b=" + nStatusB
                + "&status_s=" + nStatusS
                + "&status_v=" + nStatusV
                + "&tanggal=" + tgl
                + "&userid=" + UserID
                + "&detil=" + detil.Checked
                + "&carabayar=" + crbayar
                + "&tipe=" + nmtipe
                + "&project=" + Project
                + "&pers=" + pers.SelectedValue
                ;

            //update link
            Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..LapPDF SET Link= '" + link + "' WHERE AttachmentID = " + NoAttachment);

            //format page
            p.StartInfo.Arguments = "--orientation landscape --page-width 25cm --page-height 55cm --margin-left 0 --margin-right 0 --margin-top 0.25cm --margin-bottom 0 " + link + " " + save;

            //panggil aplikasi untuk mengconvert pdf
            p.StartInfo.FileName = Mi.PathWkhtmlPDFReport;
            p.Start();

            //60000 -> waktu jeda lama convert pdf
            p.WaitForExit(30000);

            string Src = Mi.PathFilePDFReport + nfilename;
            Mi.DownloadPDF(this, Src, (rs.Rows[0]["FileName"]).ToString(), rs.Rows[0]["FileType"].ToString());
        }


        private void Report()
        {
            param.Visible = false;
            rpt.Visible = true;

            Header();
            Fill(false);
        }

        private void ReportExcel()
        {
            rpt.Visible = true;
            Header();
            Fill(true);
        }

        private void Header()
        {
            System.Text.StringBuilder x = new System.Text.StringBuilder();

            Rpt.Judul(x, comp, judul);

            Rpt.SubJudul(x
                , "Tipe : " + Rpt.inSql(tipe).Replace("'", "")
                );

            Rpt.SubJudul(x
                , "Cara Bayar : " + Rpt.inSql(carabayar).Replace("'", "")
                );

            Rpt.SubJudul(x
                , "Project : " + project.SelectedValue
                );

            Rpt.SubJudul(x
                , "Perusahaan : " + pers.SelectedItem.Text
                );
            string tgl = "";
            if (tgltts.Checked) tgl = tgltts.Text;
            if (tglinput.Checked) tgl = tglinput.Text;
            if (tglbkm.Checked) tgl = tglbkm.Text;
            if (tglbg.Checked) tgl = tglbg.Text;

            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);
            Rpt.SubJudul(x
                , tgl + " : " + Cf.Day(dari.Text) + " s/d " + Cf.Day(sampai.Text)
                );

            Rpt.SubJudul(x
                , "Kasir : " + kasir.SelectedItem.Text
                );

            Rpt.SubJudul(x
                , "Rekening Bank : " + lbAcc.SelectedValue
                );
            Rpt.SubJudul(x
                , "Lokasi : " + ddlLokasi.SelectedValue
                );
            if (statusV.Checked)
                Rpt.SubJudul(x, "Status : " + statusV.Text);
            else if (statusP.Checked)
                Rpt.SubJudul(x, "Status : " + statusP.Text);
            else if (statusB.Checked)
                Rpt.SubJudul(x, "Status : " + statusB.Text);
            else
                Rpt.SubJudul(x, "Status : " + statusS.Text);


            //Rpt.Header(rpt, x);
            string legend = "";
            Rpt.HeaderReport(headReport, legend, x);
        }

        private void Fill(bool excel)
        {
            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);
            if (Dari > Sampai)
            {
                DateTime x = Sampai;
                Sampai = Dari;
                Dari = x;
            }

            string UserID = "";
            if (kasir.SelectedIndex != 0)
                UserID = " AND a.UserID = '" + kasir.SelectedValue + "'";

            string Status = "";
            if (statusB.Checked) Status = " AND a.Status = 'BARU'";
            if (statusP.Checked) Status = " AND a.Status = 'POST'";
            if (statusV.Checked) Status = " AND a.Status = 'VOID'";

            string Project = " AND a.Project IN (" + Act.ProjectListSql + ")";
            if (project.SelectedValue != "SEMUA") Project = " AND a.Project = '" + project.SelectedValue + "'";

            string Perusahaan = "";
            if (pers.SelectedValue != "SEMUA") Perusahaan = " AND a.REF IN (Select NoKontrak from " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK where Pers = '" + pers.SelectedValue + "')";
            
            string strAcc = "";
            if (lbAcc.SelectedIndex != 0)
                strAcc = " AND a.Acc = '" + Cf.Str(lbAcc.SelectedValue) + "'";

            string Lokasi = "";

            if (ddlLokasi.SelectedIndex != 0)// ;
                Lokasi = " AND b.Lokasi = '" + Cf.Str(ddlLokasi.SelectedValue) + "'";

            string agent = "";
            if (UserAgent() > 0)
                agent = " AND (SELECT NoAgent FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = a.Ref) = " + UserAgent();

            decimal t1 = 0;
            decimal t2 = 0;
            decimal t3 = 0;

            string tgl = "";
            if (tgltts.Checked) tgl = "a.TglTTS";
            if (tglinput.Checked) tgl = "a.TglInput";
            if (tglbkm.Checked) tgl = "a.TglBKM";
            if (tglbg.Checked) tgl = "TglBG";
            
            //string strSql = "SELECT a.*, b.Lokasi, b.Jenis "
            //    + " FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS a"
            //    + " INNER JOIN [" + Mi.DbPrefix + "MARKETINGJUAL].[dbo].[MS_UNIT] b ON a.Unit =  b.NoUnit "
            //    + " WHERE 1=1 "
            //    + " AND CONVERT(varchar," + tgl + ",112) >= '" + Cf.Tgl112(Dari) + "'"
            //    + " AND CONVERT(varchar," + tgl + ",112) <= '" + Cf.Tgl112(Sampai) + "'"
            //    + " AND a.Tipe IN (" + Rpt.inSql(tipe) + ")"
            //    + " AND CaraBayar IN (" + Rpt.inSql(carabayar) + ")"
            //    + UserID
            //    + Status
            //    + strAcc
            //    + Lokasi
            //    + agent
            //    + " ORDER BY ManualBKM";
            
            string strSql = "SELECT a.*, b.Lokasi, b.Jenis "
                + " FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS a"
                + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT b ON a.Unit =  b.NoUnit "
                //+ " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK c ON a.Ref = c.NoKontrak"
                + " WHERE 1=1 "
                //+ " AND a.NoFPS != ''"
                + " AND CONVERT(varchar," + tgl + ",112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND CONVERT(varchar," + tgl + ",112) <= '" + Cf.Tgl112(Sampai) + "'"
                + " AND a.Tipe IN (" + Rpt.inSql(tipe) + ")"
                + " AND a.CaraBayar IN (" + Rpt.inSql(carabayar) + ")"
                + Project
                + Perusahaan
                + UserID
                + Status
                + strAcc
                + Lokasi
                + agent
                + " ORDER BY NoTTS";
            //Response.Write(strSql);
            DataTable rs = Db.Rs(strSql);

            DataTable rsGiro = Db.Rs(
                "SELECT a.*,b.Lokasi "
                + " FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS a"
                + " INNER JOIN [" + Mi.DbPrefix + "MARKETINGJUAL].[dbo].[MS_UNIT] b ON a.Unit =  b.NoUnit "
                + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK c ON a.Ref = c.NoKontrak"
                + " WHERE 1=1"
                + " AND CONVERT(varchar," + tgl + ",112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND CONVERT(varchar," + tgl + ",112) <= '" + Cf.Tgl112(Sampai) + "'"
                + " AND a.Tipe IN (" + Rpt.inSql(tipe) + ")"
                + " AND a.CaraBayar IN (" + Rpt.inSql(carabayar) + ")"
                + Project
                + Perusahaan
                + UserID
                + Status
                + strAcc
                + Lokasi
                + " AND NoBG <> ''"
                );
            int LembarGiro = rsGiro.Rows.Count;

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                string strSqlCekTipeTagihan = "";
                strSqlCekTipeTagihan = "SELECT DISTINCT Tipe FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN"
                     + " WHERE NOURUT in (SELECT NOTAGIHAN FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE NOTTS='" + rs.Rows[i]["NoTTS"].ToString() + "')"
                     + " AND NoKontrak = '" + rs.Rows[i]["Ref"] + "'"
                     ;

                DataTable ListTipeTagihan = Db.Rs(strSqlCekTipeTagihan);
                bool AdaBiayaAdm = false;

                for (int iTag = 0; iTag < ListTipeTagihan.Rows.Count; iTag++)
                {
                    if (ListTipeTagihan.Rows[iTag]["Tipe"].ToString() == "ADM")
                        AdaBiayaAdm = true;
                }

                if (!AdaBiayaAdm)
                {

                    TableRow r = new TableRow();
                    TableCell c;

                    r.VerticalAlign = VerticalAlign.Top;
                    r.Attributes["ondblclick"] = "popEditTTS('" + rs.Rows[i]["NoTTS"] + "')";

                    c = new TableCell();
                    c.Text = "FK" + "<br />" + "FAPR" + "<br />" + "OF";
                    c.HorizontalAlign = HorizontalAlign.Left;
                    c.Wrap = false;
                    r.Cells.Add(c);

                    string strSql2 = "";
                    strSql2 = "SELECT DISTINCT a.NamaTagihan "
                         + ", (SELECT Count(*) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN b WHERE b.NoKontrak = a.NoKontrak AND b.Tipe != 'BF' AND b.KPR != '1') AS JumlahAngsuran"
                         + ", (SELECT Count(*) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN c WHERE c.NoKontrak = a.NoKontrak AND c.Tipe = 'DP') AS JumlahDP"
                         + ", (SELECT Count(*) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN d WHERE d.NoKontrak = a.NoKontrak AND d.Tipe = 'ANG') AS JumlahANG"
                         + ", (SELECT Tipe FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN e WHERE e.NoUrut = a.NoUrut AND e.NoKontrak = a.NoKontrak) AS TipeTagihan"
                         + ", (SELECT CaraBayar FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK f WHERE f.NoKontrak = a.NoKontrak) AS CaraBayar"
                         + ", (SELECT NoUnit FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK g WHERE g.NoKontrak = a.NoKontrak) AS NoUnit"
                         + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN a"
                         + " WHERE a.NOURUT in (SELECT NOTAGIHAN FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE NOTTS='" + rs.Rows[i]["NoTTS"].ToString() + "')"
                         + " AND a.Tipe != 'ADM' AND a.NoKontrak = '" + rs.Rows[i]["Ref"] + "'"
                         ;
                    DataTable rs12 = Db.Rs(strSql2);
                    string w2 = "";
                    string strTagihan = "";
                    string Kode_Objek = "";

                    //ini pake substring.. dan cuma 15 karakter
                    string Cluster = Db.SingleString("select SUBSTRING(Nama,9,15) from " + Mi.DbPrefix + "MARKETINGJUAL..ref_lokasi where Lokasi = '" + rs.Rows[i]["Lokasi"].ToString() + "'");

                    for (int j = 0; j < rs12.Rows.Count; j++)
                    {
                        if (rs12.Rows[j]["CaraBayar"].ToString() == "KPR")
                        {
                            if (rs12.Rows[j]["TipeTagihan"].ToString() != "BF")
                            {
                                w2 += rs12.Rows[j]["NamaTagihan"].ToString() + "-" + rs12.Rows[j]["JumlahDP"].ToString() + "/" + rs.Rows[i]["Jenis"].ToString() + "/" + rs.Rows[i]["Unit"].ToString() + "/" + Cluster + ";";
                            }
                            else
                            {
                                w2 += rs12.Rows[j]["NamaTagihan"].ToString() + "/" + rs.Rows[i]["Jenis"].ToString() + "/" + rs.Rows[i]["Unit"].ToString() + "/" + Cluster + ";";
                            }
                        }
                        else if (rs12.Rows[j]["CaraBayar"].ToString() == "CASH BERTAHAP")
                        {
                            if (rs12.Rows[j]["TipeTagihan"].ToString() != "BF")
                            {
                                w2 += rs12.Rows[j]["NamaTagihan"].ToString() + "-" + rs12.Rows[j]["JumlahANG"].ToString() + "/" + rs.Rows[i]["Jenis"].ToString() + "/" + rs.Rows[i]["Unit"].ToString() + "/" + Cluster + ";";
                            }
                            else
                            {
                                w2 += rs12.Rows[j]["NamaTagihan"].ToString() + "/" + rs.Rows[i]["Jenis"].ToString() + "/" + rs.Rows[i]["Unit"].ToString() + "/" + Cluster + ";";
                            }
                        }
                        else
                        {
                            w2 += rs12.Rows[j]["NamaTagihan"].ToString() + "/" + rs.Rows[i]["Jenis"].ToString() + "/" + rs.Rows[i]["Unit"].ToString() + "/" + Cluster + ";";
                        }

                        strTagihan = rs12.Rows[j]["NamaTagihan"].ToString();

                        Kode_Objek += Db.SingleString("SELECT ISNULL(KODE,' ') FROM REF_EFAKTUR WHERE Uraian='" + strTagihan + "'");
                    }

                    //string Kode_Objek = Db.SingleString("SELECT ISNULL(KODE,' ') FROM REF_EFAKTUR WHERE Uraian='" + w2 + "'");

                    string NamaNPWP = Db.SingleString("SELECT ISNULL(NPWPNama,' ') FROM " + Mi.DbPrefix + "SECURITY..REF_DATA where Project = '" + project.SelectedValue + "'");
                    string AlamatNPWP = Db.SingleString("SELECT ISNULL(AlamatNPWP,' ') FROM " + Mi.DbPrefix + "SECURITY..REF_DATA where Project = '" + project.SelectedValue + "'");
                    string NomorNPWP = Db.SingleString("SELECT ISNULL(NomorNPWP,' ') FROM " + Mi.DbPrefix + "SECURITY..REF_DATA where Project = '" + project.SelectedValue + "'");

                    //jenis transaksi
                    c = new TableCell();
                    c.Text = "01" + "<br />" + BersihKoma(NamaNPWP) + "<br />";
                    if (excel) c.Text = c.Text + rs.Rows[i]["Unit"].ToString(); //"'001";
                    else c.Text = c.Text + rs.Rows[i]["Unit"].ToString(); //"'001";
                    c.HorizontalAlign = HorizontalAlign.Left;
                    c.Wrap = false;
                    r.Cells.Add(c);

                    string NoFPS = rs.Rows[i]["NoFPS"].ToString();
                    string[] noFG = NoFPS.Split('.');
                    string printFG = "", printFP = "";

                    if (noFG.Length > 0)
                    {
                        //printFG = noFG.Length.ToString();
                        //printFP = noFG[1].ToString();

                        for (int count = 0; count <= noFG.Length - 1; count++)
                        {
                            if (count == 0)
                                printFG = noFG[count].ToString();

                            if (count > 0)
                            {
                                if (count == 1)
                                    printFP += noFG[count].ToString();
                                else
                                    printFP += "." + noFG[count].ToString();
                            }
                        }
                    }

                    c = new TableCell();
                    c.Text = BersihKoma(printFG) + "<br />" + BersihKoma(AlamatNPWP) + "<br />" + w2;
                    c.HorizontalAlign = HorizontalAlign.Left;
                    c.Wrap = false;
                    r.Cells.Add(c);

                    decimal NilaiBayar = Convert.ToDecimal(rs.Rows[i]["Total"]);
                    decimal DPP = NilaiBayar / (decimal)1.1;

                    c = new TableCell();
                    string Jalan = Db.SingleString("SELECT ISNULL(AlamatProject,' ') FROM " + Mi.DbPrefix + "SECURITY..REF_DATA where Project = '" + project.SelectedValue + "'");
                    printFP = printFP.Replace(".", "");
                    if (excel) printFP = "'" + printFP;
                    c.Text = BersihKoma(printFP) + "<br />" + BersihKoma(Jalan) + "<br />" + Math.Round(DPP).ToString();
                    c.HorizontalAlign = HorizontalAlign.Left;
                    c.Wrap = false;
                    r.Cells.Add(c);


                    DateTime TglKwitasi = Convert.ToDateTime(rs.Rows[i]["TglBKM"]);
                    int BulanKwitansi = TglKwitasi.Month;
                    int TahunKwitansi = TglKwitasi.Year;
                    string BlokNPWP = Db.SingleString("SELECT ISNULL(BlokNPWP,' ') FROM " + Mi.DbPrefix + "SECURITY..REF_DATA where Project = '" + project.SelectedValue + "'");
                    c = new TableCell();
                    c.Text = BulanKwitansi + "<br />" + BersihKoma(BlokNPWP) + "<br />" + "1";
                    c.HorizontalAlign = HorizontalAlign.Left;
                    c.Wrap = false;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = TahunKwitansi + "<br />" + " " + "<br />" + Math.Round(DPP).ToString();
                    c.HorizontalAlign = HorizontalAlign.Left;
                    c.Wrap = false;
                    r.Cells.Add(c);

                    string RTNPWP = Db.SingleString("SELECT ISNULL(RTNPWP,' ') FROM " + Mi.DbPrefix + "SECURITY..REF_DATA where Project = '" + project.SelectedValue + "'");
                    c = new TableCell();
                    c.Text = Cf.DaySlash(TglKwitasi) + "<br />" + BersihKoma(RTNPWP) + "<br />" + "0";
                    c.HorizontalAlign = HorizontalAlign.Left;
                    c.Wrap = false;
                    r.Cells.Add(c);

                    string QueryNamaCS = "SELECT ISNULL(NPWP,' ') FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK A"
                         + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER  B ON A.NoCustomer = B.NoCustomer"
                         + " WHERE NoKontrak = '" + rs.Rows[i]["Ref"] + "' where Project = '" + project.SelectedValue + "'";

                    string NPWPCS = Db.SingleString(QueryNamaCS);
                    string RWNPWP = Db.SingleString("SELECT ISNULL(RWNPWP,'') FROM " + Mi.DbPrefix + "SECURITY..REF_DATA where Project = '" + project.SelectedValue + "'");
                    c = new TableCell();
                    if (excel) NPWPCS = "'" + NPWPCS;
                    c.Text = BersihKoma(NPWPCS) + "<br />" + BersihKoma(RWNPWP) + "<br />" + Math.Round(DPP).ToString();
                    c.HorizontalAlign = HorizontalAlign.Left;
                    c.Wrap = false;
                    r.Cells.Add(c);

                    int Kode = Db.SingleInteger("SELECT NoCustomer FROM " + Mi.DbPrefix + "Marketingjual..ms_Kontrak where NoKontrak = '" + rs.Rows[i]["Ref"].ToString() + "' where Project = '" + project.SelectedValue + "'");
                    string Nama = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER WHERE NoCustomer = " + Kode);
                    string KecamatanNPWP = Db.SingleString("SELECT ISNULL(KecamatanNPWP,' ') FROM " + Mi.DbPrefix + "SECURITY..REF_DATA where Project = '" + project.SelectedValue + "'");
                    c = new TableCell();
                    c.Text = BersihKoma(Nama) + "<br />" + BersihKoma(KecamatanNPWP) + "<br />" + Math.Round(NilaiBayar - DPP).ToString();
                    c.HorizontalAlign = HorizontalAlign.Left;
                    c.Wrap = false;
                    r.Cells.Add(c);

                    // DATA CUSTOMER
                    decimal NoCustomer = Db.SingleDecimal("SELECT NoCustomer FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak='" + rs.Rows[i]["Ref"] + "'");
                    DataTable dtCustomer = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER WHERE NoCustomer =" + NoCustomer);
                    string Alm_NPWP1 = "", Alm_NPWP2 = "", Alm_NPWP3 = "";
                    string Alm_KTP1 = "", Alm_KTP2 = "", Alm_KTP3 = "", Alm_KTP4 = "";
                    string AlamatCS = " ";

                    if (dtCustomer.Rows.Count > 0)
                    {
                        Alm_NPWP1 = dtCustomer.Rows[0]["NPWPAlamat1"].ToString().Trim();
                        Alm_NPWP2 = dtCustomer.Rows[0]["NPWPAlamat2"].ToString().Trim();
                        Alm_NPWP3 = dtCustomer.Rows[0]["NPWPAlamat3"].ToString().Trim();
                        Alm_KTP1 = dtCustomer.Rows[0]["KTP1"].ToString().Trim();
                        Alm_KTP2 = dtCustomer.Rows[0]["KTP2"].ToString().Trim();
                        Alm_KTP3 = dtCustomer.Rows[0]["KTP3"].ToString().Trim();
                        Alm_KTP4 = dtCustomer.Rows[0]["KTP4"].ToString().Trim();

                        if (Alm_NPWP1 == "" || Alm_NPWP2 == "" || Alm_NPWP3 == "" || Alm_NPWP1 == "-" || Alm_NPWP2 == "-" || Alm_NPWP3 == "-")
                        {
                            if (Alm_KTP1 == "" || Alm_KTP2 == "" || Alm_KTP3 == "" || Alm_KTP4 == "" || Alm_KTP1 == "-" || Alm_KTP2 == "-" || Alm_KTP3 == "-" || Alm_KTP4 == "-")
                                AlamatCS = "";
                            else
                                AlamatCS = Alm_KTP1 + "" + Alm_KTP2 + "" + Alm_KTP3 + "" + Alm_KTP4;
                        }
                        else
                            AlamatCS = Alm_NPWP1 + "" + Alm_NPWP2 + "" + Alm_NPWP3;

                        AlamatCS = AlamatCS.Trim();

                        if (AlamatCS == "")
                            AlamatCS = "&nbsp;";
                    }

                    string KelurahanNPWP = Db.SingleString("SELECT ISNULL(KelurahanNPWP,' ') FROM " + Mi.DbPrefix + "SECURITY..REF_DATA where Project = '" + project.SelectedValue + "'");
                    c = new TableCell();
                    c.Text = BersihKoma(AlamatCS) + "<br />" + BersihKoma(KelurahanNPWP) + "<br />" + "0";
                    c.HorizontalAlign = HorizontalAlign.Left;
                    c.Wrap = false;
                    r.Cells.Add(c);

                    string KabupatenNPWP = Db.SingleString("SELECT ISNULL(KabupatenNPWP,' ') FROM " + Mi.DbPrefix + "SECURITY..REF_DATA where Project = '" + project.SelectedValue + "'");
                    c = new TableCell();
                    c.Text = Math.Round(DPP).ToString() + "<br />" + BersihKoma(KabupatenNPWP) + "<br />" + "0";
                    c.HorizontalAlign = HorizontalAlign.Left;
                    c.Wrap = false;
                    r.Cells.Add(c);

                    string PropinsiNPWP = Db.SingleString("SELECT ISNULL(PropinsiNPWP,' ') FROM " + Mi.DbPrefix + "SECURITY..REF_DATA where Project = '" + project.SelectedValue + "'");
                    c = new TableCell();
                    c.Text = Math.Round(NilaiBayar - DPP).ToString() + "<br />" + BersihKoma(PropinsiNPWP) + "<br />" + "0";
                    c.HorizontalAlign = HorizontalAlign.Left;
                    c.Wrap = false;
                    r.Cells.Add(c);

                    //JUMLAH_PPNBM  -  KODE_POS
                    string KodePOS = Db.SingleString("SELECT ISNULL(KodePosNPWP,' ') FROM " + Mi.DbPrefix + "SECURITY..REF_DATA where Project = '" + project.SelectedValue + "'");
                    c = new TableCell();
                    c.Text = "0" + "<br />" + BersihKoma(KodePOS) + "<br />" + " ";
                    c.HorizontalAlign = HorizontalAlign.Left;
                    c.Wrap = false;
                    r.Cells.Add(c);

                    //ID_KETERANGAN_TAMBAHAN   -   NOMOR_TELEPON   -   <<KOSONG>>
                    string NoTelp = Db.SingleString("SELECT ISNULL(NoTelp,' ') FROM " + Mi.DbPrefix + "SECURITY..REF_DATA where Project = '" + project.SelectedValue + "'");
                    c = new TableCell();
                    if (excel) NoTelp = "'" + NoTelp;
                    c.Text = "&nbsp;" + "<br />" + BersihKoma(NoTelp) + "<br />" + "&nbsp;";
                    c.HorizontalAlign = HorizontalAlign.Left;
                    c.Wrap = false;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = "0" + "<br />" + " " + "<br />" + " ";
                    c.HorizontalAlign = HorizontalAlign.Left;
                    c.Wrap = false;
                    r.Cells.Add(c);

                    //UANG_MUKA_DPP
                    c = new TableCell();
                    c.Text = "0" + "<br />" + " " + "<br />" + " ";
                    c.HorizontalAlign = HorizontalAlign.Left;
                    c.Wrap = false;
                    r.Cells.Add(c);

                    //UANG_MUKA_PPN
                    c = new TableCell();
                    c.Text = "0" + "<br />" + " " + "<br />" + " ";
                    c.HorizontalAlign = HorizontalAlign.Left;
                    c.Wrap = false;
                    r.Cells.Add(c);

                    //UANG_MUKA_PPNBM
                    c = new TableCell();
                    c.Text = "0" + "<br />" + " " + "<br />" + " ";
                    c.HorizontalAlign = HorizontalAlign.Left;
                    c.Wrap = false;
                    r.Cells.Add(c);

                    //REFERENSI
                    string sRef = w2 + " " + rs.Rows[0]["Jenis"].ToString() + " " + "SL-" + rs.Rows[0]["Unit"].ToString();
                    c = new TableCell();
                    c.Text = BersihKoma(sRef) + "<br />" + " " + "<br />" + " ";
                    c.HorizontalAlign = HorizontalAlign.Left;
                    c.Wrap = false;
                    r.Cells.Add(c);

                    rpt.Rows.Add(r);
                }
            }
        }

        protected void tipeCheck_CheckedChanged(object sender, System.EventArgs e)
        {
            for (int i = 0; i < tipe.Items.Count; i++)
            {
                tipe.Items[i].Selected = tipeCheck.Checked;
            }

            Js.Focus(this, tipeCheck);
            tipec.Text = "";
        }
        private void FillCsv()
        {
            string Project = "";
            if (project.SelectedValue != "SEMUA") Project = " AND a.Project = '" + project.SelectedValue + "'";
            string Perusahaan = "";
            if (pers.SelectedValue != "SEMUA") Perusahaan = " AND a.REF IN (Select NoKontrak from " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK where Pers = '" + pers.SelectedValue + "')";

            string csv = string.Empty;
            //FK	KD_JENIS_TRANSAKSI	FG_PENGGANTI	NOMOR_FAKTUR	MASA_PAJAK	TAHUN_PAJAK	TANGGAL_FAKTUR
            //NPWP	NAMA	ALAMAT_LENGKAP	JUMLAH_DPP	JUMLAH_PPN	JUMLAH_PPNBM	
            //ID_KETERANGAN_TAMBAHAN	FG_UANG_MUKA	UANG_MUKA_DPP	UANG_MUKA_PPN	
            //UANG_MUKA_PPNBM	REFERENSI
            csv += "FK,";
            csv += "KD_JENIS_TRANSAKSI,";
            csv += "FG_PENGGANTI,";
            csv += "NOMOR_FAKTUR,";
            csv += "MASA_PAJAK,";
            csv += "TAHUN_PAJAK,";
            csv += "TANGGAL_FAKTUR,";
            csv += "NPWP,";
            csv += "NAMA,";
            csv += "ALAMAT_LENGKAP,";
            csv += "JUMLAH_DPP,";
            csv += "JUMLAH_PPN,";
            csv += "JUMLAH_PPNBM,";
            csv += "ID_KETERANGAN_TAMBAHAN,";
            csv += "FG_UANG_MUKA,";
            csv += "UANG_MUKA_DPP,";
            csv += "UANG_MUKA_PPN,";
            csv += "UANG_MUKA_PPNBM,";
            csv += "REFERENSI";


            //Add new line.
            csv += "\r\n";

            //LT	NPWP	NAMA	JALAN	BLOK	NOMOR	RT	RW	KECAMATAN	
            //KELURAHAN	KABUPATEN	PROPINSI	KODE_POS	NOMOR_TELEPON					

            csv += "LT,";
            csv += "NPWP,";
            csv += "NAMA,";
            csv += "JALAN,";
            csv += "BLOK,";
            csv += "RT,";
            csv += "RW,";
            csv += "KECAMATAN,";
            csv += "KELURAHAN,";
            csv += "KABUPATEN,";
            csv += "PROPINSI,";
            csv += "KODE_POS,";
            csv += "NOMOR_TELEPON";

            csv += "\r\n";
            //OF	KODE_OBJEK	NAMA	HARGA_SATUAN	JUMLAH_BARANG	HARGA_TOTAL	DISKON	DPP	PPN	TARIF_PPNBM	PPNBM								
            csv += "OF,";
            csv += "KODE_OBJEK,";
            csv += "NAMA,";
            csv += "HARGA_SATUAN,";
            csv += "JUMLAH_BARANG,";
            csv += "HARGA_TOTAL,";
            csv += "DISKON,";
            csv += "DPP,";
            csv += "PPN,";
            csv += "TARIF_PPNBM,";
            csv += "PPNBM";
            csv += "\r\n";

            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);
            if (Dari > Sampai)
            {
                DateTime x = Sampai;
                Sampai = Dari;
                Dari = x;
            }

            string UserID = "";
            if (kasir.SelectedIndex != 0)
                UserID = " AND a.UserID = '" + kasir.SelectedValue + "'";

            string Status = "";
            if (statusB.Checked) Status = " AND a.Status = 'BARU'";
            if (statusP.Checked) Status = " AND a.Status = 'POST'";
            if (statusV.Checked) Status = " AND a.Status = 'VOID'";


            string strAcc = "";
            if (lbAcc.SelectedIndex != 0)
                strAcc = " AND a.Acc = '" + Cf.Str(lbAcc.SelectedValue) + "'";

            string Lokasi = "";

            if (ddlLokasi.SelectedIndex != 0)// ;
                Lokasi = " AND b.Lokasi = '" + Cf.Str(ddlLokasi.SelectedValue) + "'";

            string agent = "";
            if (UserAgent() > 0)
                agent = " AND (SELECT NoAgent FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = a.Ref) = " + UserAgent();

            decimal t1 = 0;
            decimal t2 = 0;
            decimal t3 = 0;

            string tgl = "";
            if (tgltts.Checked) tgl = "a.TglTTS";
            if (tglinput.Checked) tgl = "a.TglInput";
            if (tglbkm.Checked) tgl = "a.TglBKM";
            if (tglbg.Checked) tgl = "TglBG";

            string strSql = "SELECT a.*, b.Lokasi, b.Jenis "
                + " FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS a"
                + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT b ON a.Unit =  b.NoUnit "
                //+ " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK c ON a.Ref = c.NoKontrak"
                + " WHERE 1=1 "
                + " AND CONVERT(varchar," + tgl + ",112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND CONVERT(varchar," + tgl + ",112) <= '" + Cf.Tgl112(Sampai) + "'"
                + " AND a.Tipe IN (" + Rpt.inSql(tipe) + ")"
                + " AND a.CaraBayar IN (" + Rpt.inSql(carabayar) + ")"
                + Project
                + Perusahaan
                + UserID
                + Status
                + strAcc
                + Lokasi
                + agent
                + " ORDER BY a.NoTTS";

            DataTable rs = Db.Rs(strSql);

            string NPWPPT = Db.SingleString("SELECT ISNULL(NPWP,' ') FROM " + Mi.DbPrefix + "SECURITY..REF_DATA");
            string NamaNPWP = Db.SingleString("SELECT ISNULL(NPWPNama,' ') FROM " + Mi.DbPrefix + "SECURITY..REF_DATA");
            string AlamatNPWP = Db.SingleString("SELECT ISNULL(AlamatNPWP,' ') FROM " + Mi.DbPrefix + "SECURITY..REF_DATA");
            string NomorNPWP = Db.SingleString("SELECT ISNULL(NomorNPWP,' ') FROM " + Mi.DbPrefix + "SECURITY..REF_DATA");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                string strSqlCekTipeTagihan = "";
                strSqlCekTipeTagihan = "SELECT DISTINCT Tipe FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN"
                     + " WHERE NOURUT in (SELECT NOTAGIHAN FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE NOTTS='" + rs.Rows[i]["NoTTS"].ToString() + "')"
                     + " AND NoKontrak = '" + rs.Rows[i]["Ref"] + "'"
                     ;

                DataTable ListTipeTagihan = Db.Rs(strSqlCekTipeTagihan);
                bool AdaBiayaAdm = false;

                for (int iTag = 0; iTag < ListTipeTagihan.Rows.Count; iTag++)
                {
                    if (ListTipeTagihan.Rows[iTag]["Tipe"].ToString() == "ADM")
                        AdaBiayaAdm = true;
                }

                //Jika tidak ada Biaya Admin, tampilkan.
                if (!AdaBiayaAdm)
                {
                    string strSql2 = "";
                    strSql2 = "SELECT DISTINCT a.NamaTagihan"
                         + ", (SELECT Count(*) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN b WHERE b.NoKontrak = a.NoKontrak AND b.Tipe != 'BF' AND b.KPR != '1') AS JumlahAngsuran"
                         + ", (SELECT Count(*) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN c WHERE c.NoKontrak = a.NoKontrak AND c.Tipe = 'DP') AS JumlahDP"
                         + ", (SELECT Count(*) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN d WHERE d.NoKontrak = a.NoKontrak AND d.Tipe = 'ANG') AS JumlahANG"
                         + ", (SELECT Tipe FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN e WHERE e.NoUrut = a.NoUrut AND e.NoKontrak = a.NoKontrak) AS TipeTagihan"
                         + ", (SELECT CaraBayar FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK f WHERE f.NoKontrak = a.NoKontrak) AS CaraBayar"
                         + ", (SELECT NoUnit FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK g WHERE g.NoKontrak = a.NoKontrak) AS NoUnit"
                         + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN a"
                         + " WHERE a.NOURUT in (SELECT NOTAGIHAN FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE NoTTS='" + rs.Rows[i]["NoTTS"].ToString() + "')"
                         + " AND a.NoKontrak = '" + rs.Rows[i]["Ref"] + "'"
                         ;

                    DataTable rs12 = Db.Rs(strSql2);
                    string w2 = "";
                    string strTagihan = "";
                    string Kode_Objek = "";
                    string NoUnit = "";


                    //ini pake substring.. dan cuma 15 karakter
                    string Cluster = Db.SingleString("select SUBSTRING(Nama,9,15) from " + Mi.DbPrefix + "MARKETINGJUAL..ref_lokasi where Lokasi = '" + rs.Rows[i]["Lokasi"].ToString() + "'");

                    for (int j = 0; j < rs12.Rows.Count; j++)
                    {
                        if (rs12.Rows[j]["CaraBayar"].ToString() == "KPR")
                        {
                            if (rs12.Rows[j]["TipeTagihan"].ToString() != "BF")
                            {
                                w2 += rs12.Rows[j]["NamaTagihan"].ToString() + "-" + rs12.Rows[j]["JumlahDP"].ToString() + "/" + rs.Rows[i]["Jenis"].ToString() + "/" + rs.Rows[i]["Unit"].ToString() + "/" + Cluster + ";";
                            }
                            else
                            {
                                w2 += rs12.Rows[j]["NamaTagihan"].ToString() + "/" + rs.Rows[i]["Jenis"].ToString() + "/" + rs.Rows[i]["Unit"].ToString() + "/" + Cluster + ";";
                            }
                        }
                        else if (rs12.Rows[j]["CaraBayar"].ToString() == "CASH BERTAHAP")
                        {
                            if (rs12.Rows[j]["TipeTagihan"].ToString() != "BF")
                            {
                                w2 += rs12.Rows[j]["NamaTagihan"].ToString() + "-" + rs12.Rows[j]["JumlahANG"].ToString() + "/" + rs.Rows[i]["Jenis"].ToString() + "/" + rs.Rows[i]["Unit"].ToString() + "/" + Cluster + ";";
                            }
                            else
                            {
                                w2 += rs12.Rows[j]["NamaTagihan"].ToString() + "/" + rs.Rows[i]["Jenis"].ToString() + "/" + rs.Rows[i]["Unit"].ToString() + "/" + Cluster + ";";
                            }
                        }
                        else
                        {
                            w2 += rs12.Rows[j]["NamaTagihan"].ToString() + "/" + rs.Rows[i]["Jenis"].ToString() + "/" + rs.Rows[i]["Unit"].ToString() + "/" + Cluster + ";";
                        }

                        strTagihan = rs12.Rows[j]["NamaTagihan"].ToString();

                        Kode_Objek += Db.SingleString("SELECT ISNULL(KODE,' ') FROM REF_EFAKTUR WHERE Uraian='" + strTagihan + "'");
                        NoUnit = rs12.Rows[j]["NoUnit"].ToString();
                    }

                    //Baris 1
                    string NoFPS = rs.Rows[i]["NoFPS"].ToString();
                    string[] noFG = NoFPS.Split('.');
                    string printFG = "", printFP = "";

                    if (noFG.Length > 0)
                    {
                        //printFG = noFG.Length.ToString();
                        //printFP = noFG[1].ToString();

                        for (int count = 0; count <= noFG.Length - 1; count++)
                        {
                            if (count == 0)
                                printFG = noFG[count].ToString();

                            if (count > 0)
                            {
                                if (count == 1)
                                    printFP += noFG[count].ToString();
                                else
                                    printFP += "." + noFG[count].ToString();
                            }
                        }
                    }
                    else
                    {

                    }

                    DateTime TglKwitasi = Convert.ToDateTime(rs.Rows[i]["TglBKM"]);
                    int BulanKwitansi = TglKwitasi.Month;
                    int TahunKwitansi = TglKwitasi.Year;
                    string QueryNamaCS = "SELECT ISNULL(NPWP,' ') FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK A"
                      + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER  B ON A.NoCustomer = B.NoCustomer"
                      + " WHERE NoKontrak = '" + rs.Rows[i]["Ref"] + "'";

                    string NPWPCS = Db.SingleString(QueryNamaCS);
                    
                    decimal NoCustomer = Db.SingleDecimal("SELECT NoCustomer FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak='" + rs.Rows[i]["Ref"] + "'");
                    DataTable dtCustomer = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER WHERE NoCustomer = " + NoCustomer);
                    string Alm_NPWP1 = "", Alm_NPWP2 = "", Alm_NPWP3 = "";
                    string Alm_KTP1 = "", Alm_KTP2 = "", Alm_KTP3 = "", Alm_KTP4 = "";
                    string AlamatCS = " ";

                    if (dtCustomer.Rows.Count > 0)
                    {
                        Alm_NPWP1 = dtCustomer.Rows[0]["NPWPAlamat1"].ToString().Trim();
                        Alm_NPWP2 = dtCustomer.Rows[0]["NPWPAlamat2"].ToString().Trim();
                        Alm_NPWP3 = dtCustomer.Rows[0]["NPWPAlamat3"].ToString().Trim();
                        Alm_KTP1 = dtCustomer.Rows[0]["KTP1"].ToString().Trim();
                        Alm_KTP2 = dtCustomer.Rows[0]["KTP2"].ToString().Trim();
                        Alm_KTP3 = dtCustomer.Rows[0]["KTP3"].ToString().Trim();
                        Alm_KTP4 = dtCustomer.Rows[0]["KTP4"].ToString().Trim();

                        if (Alm_NPWP1 == "" || Alm_NPWP2 == "" || Alm_NPWP3 == "" || Alm_NPWP1 == "-" || Alm_NPWP2 == "-" || Alm_NPWP3 == "-")
                        {
                            if (Alm_KTP1 == "" || Alm_KTP2 == "" || Alm_KTP3 == "" || Alm_KTP4 == "" || Alm_KTP1 == "-" || Alm_KTP2 == "-" || Alm_KTP3 == "-" || Alm_KTP4 == "-")
                                AlamatCS = "";
                            else
                                AlamatCS = Alm_KTP1 + " " + Alm_KTP2 + " " + Alm_KTP3 + " " + Alm_KTP4;
                        }
                        else
                            AlamatCS = Alm_NPWP1 + " " + Alm_NPWP2 + " " + Alm_NPWP3;

                        AlamatCS = AlamatCS.Trim();

                        if (AlamatCS == "")
                            AlamatCS = "";
                    }

                    decimal NilaiBayar = Convert.ToDecimal(rs.Rows[i]["Total"]);
                    decimal DPP = NilaiBayar / (decimal)1.1;
                    //string sRef = w2 + " " + rs.Rows[0]["Jenis"].ToString() + " " + "SL-" + rs.Rows[0]["Unit"].ToString();
                    string NoKwi = rs.Rows[i]["NoBKM2"].ToString() + " " + Cf.Day(rs.Rows[i]["TglBKM"]);

                    string KelurahanNPWP = Db.SingleString("SELECT ISNULL(KelurahanNPWP,' ') FROM " + Mi.DbPrefix + "SECURITY..REF_DATA");
                    string KabupatenNPWP = Db.SingleString("SELECT ISNULL(KabupatenNPWP,' ') FROM " + Mi.DbPrefix + "SECURITY..REF_DATA");
                    string KodePOS = Db.SingleString("SELECT ISNULL(KodePosNPWP,' ') FROM " + Mi.DbPrefix + "SECURITY..REF_DATA");
                    string NoTelp = Db.SingleString("SELECT ISNULL(NoTelp,' ') FROM " + Mi.DbPrefix + "SECURITY..REF_DATA");
                    string PropinsiNPWP = Db.SingleString("SELECT ISNULL(PropinsiNPWP,' ') FROM " + Mi.DbPrefix + "SECURITY..REF_DATA");
                    string BlokNPWP = Db.SingleString("SELECT ISNULL(BlokNPWP,' ') FROM " + Mi.DbPrefix + "SECURITY..REF_DATA");
                    string RTNPWP = Db.SingleString("SELECT ISNULL(RTNPWP,' ') FROM " + Mi.DbPrefix + "SECURITY..REF_DATA");
                    string RWNPWP = Db.SingleString("SELECT ISNULL(RWNPWP,'') FROM " + Mi.DbPrefix + "SECURITY..REF_DATA");
                    string KecamatanNPWP = Db.SingleString("SELECT ISNULL(KecamatanNPWP,' ') FROM " + Mi.DbPrefix + "SECURITY..REF_DATA");

                    string JenisProperti = Db.SingleString("SELECT ISNULL(JenisProperti,' ') FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT where NoUnit = '" + NoUnit + "'");

                    csv += "FK".Replace(",", ";") + ',' + "01".Replace(",", ";") + ',' + "0".Replace(",", ";") + ',' + NoFPS.Replace(".", "").Replace(",", ";") + ',';
                    csv += BulanKwitansi.ToString().Replace(",", ";") + ',' + TahunKwitansi.ToString().Replace(",", ";") + ',' + Cf.DaySlash(TglKwitasi) + ',' + NPWPCS + ',';
                    csv += rs.Rows[i]["Customer"].ToString().Replace(",", ";") + ',' + AlamatCS.Replace(",", ";") + ',' + Math.Round(DPP).ToString().Replace(",", ";") + ',' + Math.Round(NilaiBayar - DPP).ToString().Replace(",", ";") + ',';
                    csv += "0".Replace(",", ";") + ',' + "".Replace(",", ";") + ',' + "0".Replace(",", ";") + ',' + "0".Replace(",", ";") + ',';
                    csv += "0".Replace(",", ";") + ',' + "0".Replace(",", ";") + ',' + NoKwi.Replace(",", ";");
                    csv += "\r\n";

                    //Baris 2
                    //csv += "FAPR".Replace(",", ";") + ',' + NPWPPT.Replace(",", ";") + ',' + NamaNPWP.Replace(",", ";") + ',' + AlamatNPWP.Replace(",", ";") + ',';
                    //csv += BlokNPWP.Replace(",", ";") + ',' + RTNPWP.Replace(",", ";") + ',' + RWNPWP.Replace(",", ";") + ',' + KecamatanNPWP.Replace(",", ";") + ',';
                    //csv += KelurahanNPWP.Replace(",", ";") + ',' + KabupatenNPWP.Replace(",", ";") + ',' + PropinsiNPWP.Replace(",", ";") + ',' + KodePOS.Replace(",", ";") + ',';
                    //csv += NoTelp.Replace(",", ";");
                    //csv += "\r\n";

                    //Baris 3
                    string KetBaris3 = strTagihan.ToString() + " " + JenisProperti + " Unit " + NoUnit;
                    //awalnya kayak gini  //csv += "OF".ToString().Replace(",", ";") + ',' + rs12.Rows[j]["NoUnit"].ToString() + ',' + w2.Replace(",", ";") + ',' + Math.Round(DPP).ToString().Replace(",", ";") + ',';
                    csv += "OF".ToString().Replace(",", ";") + ',' + NoUnit + ',' + KetBaris3 + ',' + Math.Round(DPP).ToString().Replace(",", ";") + ',';
                    csv += "1".Replace(",", ";") + ',' + Math.Round(DPP).ToString().Replace(",", ";") + ',' + "0".ToString().Replace(",", ";") + ',' + Math.Round(DPP).ToString().Replace(",", ";") + ',' + Math.Round(NilaiBayar - DPP).ToString().Replace(",", ";") + ',' + "0".ToString().Replace(",", ";") + ',' + "0".ToString().Replace(",", ";");
                    csv += "\r\n";
                } //END IF(!AdaBiayaAdm)
            }


            string NamaFileCsv = "";
            NamaFileCsv = "eFaktur" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year;

            //Download the CSV file.
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=" + NamaFileCsv + ".csv");
            Response.Charset = "";
            Response.ContentType = "application/text";
            Response.Output.Write(csv);
            Response.Flush();
            Response.End();
            
        }
        protected void carabayarCheck_CheckedChanged(object sender, System.EventArgs e)
        {
            for (int i = 0; i < carabayar.Items.Count; i++)
            {
                carabayar.Items[i].Selected = carabayarCheck.Checked;
            }

            Js.Focus(this, carabayarCheck);
            carabayarc.Text = "";
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
        protected void btncsv_Click(object sender, EventArgs e)
        {
            FillCsv();
        }

        protected void pers_SelectedIndexChanged(object sender, EventArgs e)
        {
            project.Items.Clear();
            lbAcc.Items.Clear();
            lbAcc.Items.Add(new ListItem("SEMUA"));
            kasir.Items.Clear();
            kasir.Items.Add(new ListItem("SEMUA"));
            ddlLokasi.Items.Clear();
            ddlLokasi.Items.Add(new ListItem("SEMUA"));
            tipe.Items.Clear();

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
            lbAcc.Items.Clear();
            lbAcc.Items.Add(new ListItem("SEMUA"));
            kasir.Items.Clear();
            kasir.Items.Add(new ListItem("SEMUA"));
            ddlLokasi.Items.Clear();
            ddlLokasi.Items.Add(new ListItem("SEMUA"));
            tipe.Items.Clear();
            init();
        }
    }
}


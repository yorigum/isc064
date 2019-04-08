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
    public partial class MasterTTS : System.Web.UI.Page
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
                //Js.Focus(this, scr);
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
            dari.Text = Cf.Day(DateTime.Today);
            sampai.Text = Cf.Day(DateTime.Today);
            string Project = project.SelectedValue == "SEMUA" ? "Project IN (" + Act.ProjectListSql + ")" : "Project = '" + project.SelectedValue + "'";
            DataTable rs;

            rs = Db.Rs("SELECT DISTINCT UserID FROM MS_TTS WHERE " + Project + " ORDER BY UserID");
            for (int i = 0; i < rs.Rows.Count; i++)
                kasir.Items.Add(new ListItem(
                    rs.Rows[i][0].ToString()));

            kasir.SelectedIndex = 0;

            DataTable rs1 = Db.Rs("SELECT DISTINCT Lokasi FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT WHERE " + Project + " ORDER BY LOKASI ASC ");

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

            rs = Db.Rs("SELECT * FROM REF_ACC WHERE " + Project);
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
                Report();
                Rpt.ToExcel(this, headReport, rpt);
            }
        }
        protected void pdf_Click(object sender, System.EventArgs e)
        {

            Process p = new System.Diagnostics.Process();

            string Nama = "Laporan Master TTS";
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

            string strSql = "SELECT * FROM ISC064_FINANCEAR..LapPDF WHERE AttachmentID  = '" + NoAttachment + "'";
            DataTable rs = Db.Rs(strSql);

            string nfilename = "MasterTTS" + NoAttachment + ".pdf";

            //update filename
            Db.Execute("UPDATE ISC064_FINANCEAR..LapPDF SET FileName= '" + nfilename + "' WHERE AttachmentID = " + NoAttachment);


            //folder untuk menyimpan file pdf
            string save = Mi.PathFilePDFReport + "MasterTTS" + rs.Rows[0]["AttachmentID"] + ".pdf";

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
            if (tglbg.Checked) tgl = "tglbg";

            string Project = "";
            if (project.SelectedIndex == 0)
            {
                Project = Act.ProjectListSql.Replace("'", "");
            }
            else
            {
                Project = project.SelectedValue;
            }

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

            //link untuk download pdf
            string link = Mi.PathAlamatWeb + "financear/LaporanPDF/PDFMasterTTS.aspx?id=" + rs.Rows[0]["AttachmentID"]
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
            Db.Execute("UPDATE ISC064_FINANCEAR..LapPDF SET Link= '" + link + "' WHERE AttachmentID = " + NoAttachment);

            //format page
            p.StartInfo.Arguments = "--orientation landscape --page-width 8.5in --page-height 15in --margin-left 0 --margin-right 0 --margin-top 0.25cm --margin-bottom 0 " + link + " " + save;

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
            Fill();
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
            Rpt.SubJudul(x
              , "Project : " + project.SelectedItem.Text
              );
            Rpt.SubJudul(x
              , "Perusahaan : " + pers.SelectedItem.Text
              );
            if (statusV.Checked)
                Rpt.SubJudul(x, "Status : " + statusV.Text);
            else if (statusP.Checked)
                Rpt.SubJudul(x, "Status : " + statusP.Text);
            else if (statusB.Checked)
                Rpt.SubJudul(x, "Status : " + statusB.Text);
            else
                Rpt.SubJudul(x, "Status : " + statusS.Text);

            string legend = "Cara Bayar : TN = Tunai / KD = Kartu Debit / KK = Kartu Kredit / TR = Transfer Bank / BG = Cek Giro / UJ = Uang Jaminan / DN = Diskon.";
            //Rpt.Header(rpt, x);
            Rpt.HeaderReport(headReport, legend, x);
        }

        private void Fill()
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
                UserID = " AND UserID = '" + kasir.SelectedValue + "'";

            string Status = "";
            if (statusB.Checked) Status = " AND a.Status = 'BARU'";
            if (statusP.Checked) Status = " AND a.Status = 'POST'";
            if (statusV.Checked) Status = " AND a.Status = 'VOID'";

            //ini dibuat karna ada kondisi tts terbentuk tanpa kontrak saat launching
            string Pembayaran = "";
            if (alokasiYes.Checked) Pembayaran = " AND Ref != ''";
            if (alokasiNo.Checked) Pembayaran = " AND Ref = ''";

            string strAcc = "";
            if (lbAcc.SelectedIndex != 0)
                strAcc = " AND Acc = '" + Cf.Str(lbAcc.SelectedValue) + "'";

            string Lokasi = "";

            if (ddlLokasi.SelectedIndex != 0)// ;
                Lokasi = " AND b.Lokasi = '" + Cf.Str(ddlLokasi.SelectedValue) + "'";

            string agent = "";
            if (UserAgent() > 0)
                agent = " AND (SELECT NoAgent FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = a.Ref) = " + UserAgent();

            decimal t1 = 0;
            decimal t2 = 0;
            decimal t3 = 0;

            string Project = " AND a.Project IN (" + Act.ProjectListSql + ")";
            if (project.SelectedValue != "0") Project = " AND a.Project = '" + project.SelectedValue + "'";

            //string Perusahaan = "";
            //if (pers.SelectedValue != "SEMUA") Perusahaan = " AND c.Pers = '" + pers.SelectedValue + "'";

            string tgl = "";
            if (tgltts.Checked) tgl = "TglTTS";
            if (tglinput.Checked) tgl = "a.TglInput";
            if (tglbkm.Checked) tgl = "TglBKM";
            if (tglbg.Checked) tgl = "TglBG";

            //string strSql = "SELECT a.*,b.Lokasi "
            //    + " FROM ISC064_FINANCEAR..MS_TTS a"
            //    + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT b ON a.Unit =  b.NoUnit "
            //    + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK c ON a.Ref = c.NoKontrak"
            //    + " WHERE 1=1 "
            //    + " AND CONVERT(varchar," + tgl + ",112) >= '" + Cf.Tgl112(Dari) + "'"
            //    + " AND CONVERT(varchar," + tgl + ",112) <= '" + Cf.Tgl112(Sampai) + "'"
            //    + " AND a.Tipe IN (" + Rpt.inSql(tipe) + ")"
            //    + " AND a.CaraBayar IN (" + Rpt.inSql(carabayar) + ")"
            //    + Project
            //    + Perusahaan
            //    + UserID
            //    + Status
            //    + strAcc
            //    + Lokasi
            //    + agent
            //    + " ORDER BY a.NoTTS";

            string strSql = "SELECT * "
                + " FROM ISC064_FINANCEAR..MS_TTS a"
                + " WHERE 1=1 "
                + " AND CONVERT(varchar," + tgl + ",112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND CONVERT(varchar," + tgl + ",112) <= '" + Cf.Tgl112(Sampai) + "'"
                + " AND a.Tipe IN (" + Rpt.inSql(tipe) + ")"
                + " AND a.CaraBayar IN (" + Rpt.inSql(carabayar) + ")"
                + Project
                + UserID
                + Status
                + Pembayaran
                + strAcc
                + Lokasi
                + agent
                + " ORDER BY a.NoTTS";

            DataTable rs = Db.Rs(strSql);

            //DataTable rsGiro = Db.Rs(
            //    "SELECT a.*,b.Lokasi "
            //    + " FROM ISC064_FINANCEAR..MS_TTS a"
            //    + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT b ON a.Unit =  b.NoUnit "
            //    + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK c ON a.Ref = c.NoKontrak"
            //    + " WHERE 1=1"
            //    + " AND CONVERT(varchar," + tgl + ",112) >= '" + Cf.Tgl112(Dari) + "'"
            //    + " AND CONVERT(varchar," + tgl + ",112) <= '" + Cf.Tgl112(Sampai) + "'"
            //    + " AND Tipe IN (" + Rpt.inSql(tipe) + ")"
            //    + " AND a.CaraBayar IN (" + Rpt.inSql(carabayar) + ")"
            //    + Project
            //    + Perusahaan
            //    + UserID
            //    + Status
            //    + strAcc
            //    + Lokasi
            //    + " AND NoBG <> ''"
            //    );

            DataTable rsGiro = Db.Rs(
                "SELECT * "
                + " FROM ISC064_FINANCEAR..MS_TTS a"
                + " WHERE 1=1"
                + " AND CONVERT(varchar," + tgl + ",112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND CONVERT(varchar," + tgl + ",112) <= '" + Cf.Tgl112(Sampai) + "'"
                + " AND Tipe IN (" + Rpt.inSql(tipe) + ")"
                + " AND a.CaraBayar IN (" + Rpt.inSql(carabayar) + ")"
                + Project
                + UserID
                + Status
                + Pembayaran
                + strAcc
                + Lokasi
                + " AND NoBG <> ''"
                );

            int LembarGiro = rsGiro.Rows.Count;

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                r.VerticalAlign = VerticalAlign.Top;
                r.Attributes["ondblclick"] = "popEditTTS('" + rs.Rows[i]["NoTTS"] + "')";

                c = new TableCell();
                c.Text = (i + 1).ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoTTS2"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                if ((int)rs.Rows[i]["NoBKM"] != 0)
                    c.Text = rs.Rows[i]["NoBKM2"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Status"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglTTS"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglBKM"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["UserID"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                strSql = "SELECT DISTINCT TIPE FROM ISC064_MARKETINGJUAL..MS_TAGIHAN"
                       + " WHERE NOURUT in (SELECT NOTAGIHAN FROM ISC064_MARKETINGJUAL..MS_PELUNASAN WHERE NOTTS='" + rs.Rows[i]["NoTTS"].ToString() + "')"
                       + " AND NoKontrak = '" + rs.Rows[i]["Ref"] + "'"
                       ;
                DataTable rs1 = Db.Rs(strSql);
                string w = "";
                if (rs1.Rows.Count > 1)
                {
                    for (int j = 0; j < rs1.Rows.Count; j++)
                    {
                        w += rs1.Rows[j]["Tipe"].ToString() + ",";
                    }
                }
                else
                {
                    for (int j = 0; j < rs1.Rows.Count; j++)
                    {
                        w += rs1.Rows[j]["Tipe"].ToString();
                    }
                }
                c.Text = w;
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Ref"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Unit"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Customer"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["CaraBayar"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Ket"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoBG"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglBG"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Acc"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                string Bank = Db.SingleString("SELECT Bank FROM REF_ACC WHERE Acc = '" + rs.Rows[i]["Acc"] + "' AND SubID = '" + rs.Rows[i]["SubID"] + "' ");
                c = new TableCell();
                c.Text = Bank;
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                if (!detil.Checked)
                    c.Text = Cf.Num(rs.Rows[i]["Total"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["LebihBayar"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                decimal t = Convert.ToDecimal(rs.Rows[i]["Total"]) + Convert.ToDecimal(rs.Rows[i]["LebihBayar"]);

                c = new TableCell();
                c.Text = Cf.Num(t);//Cf.Num(rs.Rows[i]["Total2"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                rpt.Rows.Add(r);

                if (detil.Checked)
                    Detil(
                         rs.Rows[i]["NoTTS"].ToString()
                        , rs.Rows[i]["Tipe"].ToString()
                        , (decimal)rs.Rows[i]["Total"]
                        );

                t1 = t1 + (decimal)rs.Rows[i]["Total"];
                t2 = t2 + (decimal)rs.Rows[i]["LebihBayar"];
                t3 = t3 + t;

                if (i == rs.Rows.Count - 1)
                {
                    SubTotal("GRAND TOTAL", t1, t2, t3);
                    Giro(LembarGiro);
                }
            }
        }

        private void Giro(int LembarGiro)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = new TableCell();
            c.ColumnSpan = 21;
            c.Text = "<strong>Lembar Giro: </strong>" + LembarGiro.ToString();
            r.Cells.Add(c);

            rpt.Rows.Add(r);
        }

        private void Detil(string NoTTS, string Tipe, decimal Total)
        {
            string Tb = Sc.MktTb(Tipe);
            string strSql = "";

            if (Tipe != "TENANT")
            {
                strSql = "SELECT "
                    + " NilaiPelunasan AS Nilai"
                    + ",CASE NoTagihan"
                    + "		WHEN 0 THEN 'UNALLOCATED'"
                    + "		ELSE (SELECT NamaTagihan FROM " + Tb + "..MS_TAGIHAN WHERE NoUrut = l.NoTagihan AND NoKontrak = l.NoKontrak)"
                    + " END AS NamaTagihan"
                    + " FROM " + Tb + "..MS_PELUNASAN AS l "
                    + " WHERE NoTTS = " + NoTTS;
            }
            else
            {
                strSql = "SELECT "
                    + " NilaiTagihan+LebihBayar AS Nilai"
                    + ",NamaTagihan"
                    + ",Tipe"
                    + " FROM " + Tb + "..MS_TAGIHAN AS l "
                    + " WHERE NoTTS = " + NoTTS;
            }

            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                r.VerticalAlign = VerticalAlign.Top;

                c = new TableCell();
                c.ColumnSpan = 8;
                r.Cells.Add(c);

                c = new TableCell();
                c.ColumnSpan = 10;
                c.Text = rs.Rows[i]["NamaTagihan"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["Nilai"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);


                rpt.Rows.Add(r);

                if (i == rs.Rows.Count - 1)
                {
                    r = new TableRow();

                    c = new TableCell();
                    c.ColumnSpan = 17;
                    c.Text = "Total :";
                    c.HorizontalAlign = HorizontalAlign.Right;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Cf.Num(Total);
                    c.Font.Bold = true;
                    c.Attributes["style"] = "border-top:1px solid black";
                    c.HorizontalAlign = HorizontalAlign.Right;
                    r.Cells.Add(c);

                    rpt.Rows.Add(r);
                }
            }
        }

        private void SubTotal(string txt, decimal t1, decimal t2, decimal t3)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = Rpt.Foot();
            c.Text = txt;
            c.ColumnSpan = 17;
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t1);
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

            //c = Rpt.Foot();
            //c.Text = "&nbsp;";
            //c.HorizontalAlign = HorizontalAlign.Right;
            //r.Cells.Add(c);

            rpt.Rows.Add(r);
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

        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlLokasi.Items.Clear();
            ddlLokasi.Items.Add(new ListItem("SEMUA"));
            lbAcc.Items.Clear();
            lbAcc.Items.Add(new ListItem("SEMUA"));
            kasir.Items.Clear();
            kasir.Items.Add(new ListItem("SEMUA"));
            tipe.Items.Clear();
            init();
        }

        protected void pers_SelectedIndexChanged(object sender, EventArgs e)
        {
            project.Items.Clear();
            ddlLokasi.Items.Clear();
            ddlLokasi.Items.Add(new ListItem("SEMUA"));
            lbAcc.Items.Clear();
            lbAcc.Items.Add(new ListItem("SEMUA"));
            kasir.Items.Clear();
            kasir.Items.Add(new ListItem("SEMUA"));
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
    }
}

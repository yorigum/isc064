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

namespace ISC064.KOMISI.Laporan
{
    public partial class MasterKomisi : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();

            if (!Page.IsPostBack)
            {
                comp.InnerHtml = Mi.Pt;
                rpt.Visible = false;
                Js.Focus(this, scr);
                init();
                if (!Act.Sec("DownloadExcel")) xls.Enabled = false;
            }
        }

        //protected int UserAgent()
        //{
        //    int a = Db.SingleInteger("SELECT NoAgent FROM " + Mi.DbPrefix + "SECURITY..USERNAME WHERE UserID = '" + Act.UserID + "'");

        //    return a;
        //}

        private void init()
        {
            dari.Text = Cf.Day(Cf.AwalBulan());
            sampai.Text = Cf.Day(Cf.AkhirBulan());

            //string aa = "";
            //if (UserAgent() > 0)
            //    aa = " AND NoAgent = " + UserAgent();

            //DataTable rs;
            //rs = Db.Rs("SELECT DISTINCT Nama, NoAgent FROM MS_AGENT WHERE Status = 'A'" + aa + " ORDER BY Nama");
            //for (int i = 0; i < rs.Rows.Count; i++)
            //    agent.Items.Add(new ListItem(rs.Rows[i]["Nama"].ToString(), rs.Rows[i]["NoAgent"].ToString()));

            project.Items.Clear();
            project.Items.Add("SEMUA");
            Act.ProjectList(project);
            //agent.SelectedIndex = 0;
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

            //if(!Cf.isPilih(tipe))
            //{
            //    x = false;
            //    tipec.Text = " Pilih Minimum Satu";
            //}
            //else
            //    tipec.Text = "";

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
                Rpt.ToExcel(this,headReport, rpt);
            }
        }

        protected void pdf_Click(object sender, System.EventArgs e)
        {

            Process p = new System.Diagnostics.Process();

            string Nama = "Laporan Master Komisi";
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

            string strSql = "SELECT * FROM ISC064_MARKETINGJUAL..LapPDF WHERE AttachmentID  = '" + NoAttachment + "'";
            DataTable rs = Db.Rs(strSql);

            string nfilename = "MasterKomisi" + NoAttachment + ".pdf";

            //update filename
            Db.Execute("UPDATE ISC064_MARKETINGJUAL..LapPDF SET FileName= '" + nfilename + "' WHERE AttachmentID = " + NoAttachment);


            //folder untuk menyimpan file pdf
            string save = Mi.PathFilePDFReport + "MasterKomisi" + rs.Rows[0]["AttachmentID"] + ".pdf";

            //declare parameter
            //string Sales = agent.SelectedValue;
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

           

            //link untuk download pdf
            string link = Mi.PathAlamatWeb + "komisi/LaporanPDF/PDFMasterKomisi.aspx?id=" + rs.Rows[0]["AttachmentID"]
                + "&status_s=" + nStatusS
                + "&status_b=" + nStatusB
                + "&status_a=" + nStatusA
                + "&userid=" + UserID
                + "&project=" + project.SelectedValue
                + "&pers=" + pers.SelectedValue
                ;

            //update link
            Db.Execute("UPDATE ISC064_MARKETINGJUAL..LapPDF SET Link= '" + link + "' WHERE AttachmentID = " + NoAttachment);

            //format page
            p.StartInfo.Arguments = "--orientation landscape --page-width 8.5in --page-height 11in --margin-left 0 --margin-right 0 --margin-top 0.25cm --margin-bottom 0 " + link + " " + save;

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
            MenuAtas();
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

            string tgl = "";
            if (tglkontrak.Checked) tgl = tglkontrak.Text;
            //if(tglbolehbayar.Checked) tgl = tglbolehbayar.Text;
            //if(tglbayar.Checked) tgl = tglbayar.Text;

            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);
            Rpt.SubJudul(x
                , tgl + " : " + Cf.Day(dari.Text) + " s/d " + Cf.Day(sampai.Text)
                );

            Rpt.SubJudul(x
                , "Project : " + project.SelectedValue
                );

            Rpt.SubJudul(x
                , "Perusahaan : " + pers.SelectedItem.Text
                );

            //Rpt.Header(rpt, x);
            string legend = "Status: A = Aktif / B = Batal. ";
            Rpt.HeaderReport(headReport, legend, x);
        }

        private void MenuAtas()
        {
            TableRow r = new TableRow();
            TableRow r2 = new TableRow();
            TableRow r3 = new TableRow();
            TableRow r4 = new TableRow();
            TableCell c = new TableCell();
            TableCell c2 = new TableCell();
            TableCell c3 = new TableCell();
            TableCell c4 = new TableCell();

            //c = new TableCell();
            //c.Text = "No";
            //c.ForeColor = Color.White;
            //c.Attributes["style"] = "background-color:#1E90FF";
            //c.HorizontalAlign = HorizontalAlign.Left;
            ////c.RowSpan = 3;
            //r.Cells.Add(c);
            
            c = new TableCell();
            c.Text = "No Kontrak";
            c.ForeColor = Color.White;
            c.Attributes["style"] = "background-color:#1E90FF";
            c.HorizontalAlign = HorizontalAlign.Left;
            //c.RowSpan = 3;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "No Unit";
            c.ForeColor = Color.White;
            c.Attributes["style"] = "background-color:#1E90FF";
            c.HorizontalAlign = HorizontalAlign.Left;
            //c.RowSpan = 3;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "Agent";
            c.ForeColor = Color.White;
            c.Attributes["style"] = "background-color:#1E90FF";
            c.HorizontalAlign = HorizontalAlign.Left;
            //c.RowSpan = 3;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "Nilai";
            c.ForeColor = Color.White;
            c.Attributes["style"] = "background-color:#1E90FF";
            c.HorizontalAlign = HorizontalAlign.Left;
            //c.RowSpan = 3;
            r.Cells.Add(c);

            //int no = Db.SingleInteger("Select Max(NoSkema) from ref_skom");
            //string hasil = "SELECT Nama FROM REF_SKOM Where NoSkema = '"+no+"'";
            //DataTable hr = Db.Rs(hasil);

            //if (hr.Rows.Count > 0)
            //{
            //    c = new TableCell();
            //    c.Text = "Komisi";
            //    c.ForeColor = Color.White;
            //    c.Attributes["style"] = "background-color:#1E90FF";
            //    c.HorizontalAlign = HorizontalAlign.Center;
            //    c.RowSpan = 1;
            //    c.ColumnSpan = hr.Rows.Count * 3; //jumlah rows tipe sales
            //    r.Cells.Add(c);

            //    c = new TableCell();
            //    c.Text = "Closing Fee";
            //    c.ForeColor = Color.White;
            //    c.Attributes["style"] = "background-color:#1E90FF";
            //    c.HorizontalAlign = HorizontalAlign.Center;
            //    c.RowSpan = 1;
            //    c.ColumnSpan = hr.Rows.Count * 3; //jumlah rows tipe sales
            //    r.Cells.Add(c);

            //    for (int j = 0; j < hr.Rows.Count; j++)
            //    {
            //        c2 = new TableCell();
            //        c2.Text = hr.Rows[j]["Nama"].ToString();
            //        c2.ForeColor = Color.White;
            //        c2.Attributes["style"] = "background-color:#1E90FF";
            //        c2.HorizontalAlign = HorizontalAlign.Center;
            //        c2.ColumnSpan = 3;
            //        r2.Cells.Add(c2);


            //        c3 = new TableCell();
            //        c3.Text = "Nilai Komisi";
            //        c3.ForeColor = Color.White;
            //        c3.Attributes["style"] = "background-color:#1E90FF";
            //        r3.Cells.Add(c3);

            //        c3 = new TableCell();
            //        c3.Text = "Nilai Bayar";
            //        c3.ForeColor = Color.White;
            //        c3.Attributes["style"] = "background-color:#1E90FF";
            //        r3.Cells.Add(c3);

            //        c3 = new TableCell();
            //        c3.Text = "Tanggal Bayar";
            //        c3.ForeColor = Color.White;
            //        c3.Attributes["style"] = "background-color:#1E90FF";
            //        r3.Cells.Add(c3);
            //    }

            //    for (int g = 0; g < hr.Rows.Count; g++)
            //    {
            //        c2 = new TableCell();
            //        c2.Text = hr.Rows[g]["Nama"].ToString();
            //        c2.ForeColor = Color.White;
            //        c2.Attributes["style"] = "background-color:#1E90FF";
            //        c2.HorizontalAlign = HorizontalAlign.Center;
            //        c2.ColumnSpan = 3;
            //        r2.Cells.Add(c2);

            //        c3 = new TableCell();
            //        c3.Text = "Nilai Closing Fee";
            //        c3.ForeColor = Color.White;
            //        c3.Attributes["style"] = "background-color:#1E90FF";
            //        r3.Cells.Add(c3);

            //        c3 = new TableCell();
            //        c3.Text = "Nilai Bayar";
            //        c3.ForeColor = Color.White;
            //        c3.Attributes["style"] = "background-color:#1E90FF";
            //        r3.Cells.Add(c3);

            //        c3 = new TableCell();
            //        c3.Text = "Tanggal Bayar";
            //        c3.ForeColor = Color.White;
            //        c3.Attributes["style"] = "background-color:#1E90FF";
            //        r3.Cells.Add(c3);

            //    }
            //}

            
            rpt.Rows.Add(r);
            //rpt.Rows.Add(r2);
            //rpt.Rows.Add(r3);
        }

        private void Fill()
        {
            string Status = "";
            if (statusA.Checked) Status = " AND A.Status = 'A'";
            if (statusB.Checked) Status = " AND A.Status = 'B'";

            string tgl = "";
            string order = "";
            if (tglkontrak.Checked)
            {
                tgl = "A.TglKontrak";
                order = ",A.NoKontrak";
            }

            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);
            if (Dari > Sampai)
            {
                DateTime x = Sampai;
                Sampai = Dari;
                Dari = x;
            }

            //string Agent = "", Agent2 = "";
            //if (agent.SelectedIndex != 0)
            //{
            //    Agent = " AND B.NoAgent = '" + agent.SelectedValue + "'";
            //    Agent2 = " AND A.NoAgent = '" + agent.SelectedValue + "'";
            //}
            //else
            //{
            //    if (UserAgent() > 0)
            //        Agent = " AND B.NoAgent = " + UserAgent();
            //}

            string Project = "";
            if (project.SelectedValue != "SEMUA") Project = " AND A.Project = '" + project.SelectedValue + "'";
            string Perusahaan = "";
            if (pers.SelectedValue != "SEMUA") Perusahaan = " AND A.Pers = '" + pers.SelectedValue + "'";
            int index = 1;

            int no = 1;

            //string sql = "SELECT DISTINCT (NoAgent) from MS_KONTRAK A"
            //    + " where (select ISNULL(count(*),0) from MS_KOMISI where NoKontrak = A.NoKontrak) > 0"  +Project + Perusahaan;
            //DataTable sr = Db.Rs(sql);
            //decimal t1 = 0, t2 = 0;
            //for (int g = 0; g < sr.Rows.Count; g++)
            //{
                //if (!Response.IsClientConnected) break;

                //string strSql = "SELECT "
                //    + "A.NoKontrak"
                //    + ",A.TglKontrak"
                //    + ",A.NilaiDPP"
                //    + ",A.NoUnit"
                //    + ",A.Skema"
                //    + ",A.NilaiKontrak"
                //    + ",B.Nama AS Ag"
                //    + ",B.Principal"
                //    + ",B.NPWP"
                //    + ",B.Rekening"
                //    + ",A.Status"
                //    + ",A.PersenLunas"
                //    + ",C.Nama as Customer"
                //    + ",A.NoAgent"
                //    + ",A.NoStock"
                //    + " FROM MS_KONTRAK A INNER JOIN MS_AGENT B ON A.NoAgent = B.NoAgent"
                //    + " INNER JOIN MS_CUSTOMER C ON A.NoCustomer = C.NoCustomer"
                //    + " WHERE A.NoAgent= '" + sr.Rows[g]["NoAgent"] + "'"
                //    + " AND A.FlagKomisi = '1'"
                //    + Status
                //    + " AND CONVERT(varchar,A.TglKontrak,112) >= '" + Cf.Tgl112(Dari) + "'"
                //    + " AND CONVERT(varchar,A.TglKontrak,112) <= '" + Cf.Tgl112(Sampai) + "'"
                //    //+ Agent
                //    + Project
                //    + Perusahaan
                //    + " ORDER BY B.Nama"
                //    + order;
                string strSql = "SELECT "
                    + "A.NoKontrak"
                    + ",A.NoUnit"
                    + ",C.Nilai"
                    + ",B.NamaAgent"
                    + ",A.Status"
                    + ",A.PersenLunas"
                    //+ ",C.Nama as Customer"
                    + ",A.NoAgent"
                    + " FROM MS_KONTRAK A INNER JOIN MS_KOMISI B ON A.NoKontrak = B.NoKontrak"
                    + " INNER JOIN MS_KOMISI_DETAIL C ON B.NoKomisi = C.NoKomisi"
                    + " INNER JOIN MS_KOMISI_TERM D ON C.NoKomisi = D.NoKomisi"
                    //+ " WHERE A.NoAgent= '" + sr.Rows[g]["NoAgent"] + "'"
                    //+ " AND A.FlagKomisi = '1'"
                    + Status
                    + " AND CONVERT(varchar,A.TglKontrak,112) >= '" + Cf.Tgl112(Dari) + "'"
                    + " AND CONVERT(varchar,A.TglKontrak,112) <= '" + Cf.Tgl112(Sampai) + "'"
                    //+ Agent
                    + Project
                    + Perusahaan
                    + " ORDER BY B.NamaAgent"
                    + order;



                DataTable rs = Db.Rs(strSql);
                for (int i = 0; i < rs.Rows.Count; i++)
                {
                    if (!Response.IsClientConnected) break;

                    TableRow r = new TableRow();
                    TableCell c;
                    TableRow r2a;
                    TableHeaderCell th2;
                    Table tb;

                    r.VerticalAlign = VerticalAlign.Top;
                    r.Attributes["ondblclick"] = "popJadwalKomisi('" + rs.Rows[i]["NoKontrak"] + "')";

                    //nambah no default
                    //c = new TableCell();
                    //c.Text = (no).ToString();
                    ////c.RowSpan = 4;
                    //c.HorizontalAlign = HorizontalAlign.Left;
                    //r.Cells.Add(c);
                    
                    c = new TableCell();
                    c.Text = rs.Rows[i]["NoKontrak"].ToString();
                    string NoKontrak = rs.Rows[i]["NoKontrak"].ToString();
                    //c.RowSpan = 4;
                    c.HorizontalAlign = HorizontalAlign.Left;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = rs.Rows[i]["NoUnit"].ToString();
                    //c.RowSpan = 4;
                    c.HorizontalAlign = HorizontalAlign.Left;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = rs.Rows[i]["NamaAgent"].ToString();
                    //c.RowSpan = 4;
                    c.HorizontalAlign = HorizontalAlign.Left;
                    r.Cells.Add(c);

                //string agen = Db.SingleString("Select Nama from ms_agent where NoAgent ='" + rs.Rows[i]["NoAgent"]+ "'");
                //c = new TableCell();
                //c.Text = agen;
                //c.RowSpan = 4;
                //c.HorizontalAlign = HorizontalAlign.Left;
                //r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Cf.Num(Convert.ToDecimal(rs.Rows[i]["Nilai"]));
                    //c.RowSpan = 4;
                    c.HorizontalAlign = HorizontalAlign.Left;
                    r.Cells.Add(c);

                    //c = new TableCell();
                    //decimal NKom = Db.SingleDecimal("Select NilaiKomisi From MS_KOMISI Where NoKontrak ='" + rs.Rows[i]["NoKontrak"].ToString() + "'"); 
                    //c.Text = Cf.Num(Math.Round(NKom));
                    //c.RowSpan = 4;
                    //c.HorizontalAlign = HorizontalAlign.Right;
                    //r.Cells.Add(c);
                    
                    rpt.Rows.Add(r);
                    no++;

                    //t1 += NKom;
                    //termin(NoKontrak);
                }

            //}
            //SubTotal("GRAND TOTAL", t1);
            
        }

        private void termin(string Nokontrak)
        {
            TableRow r2 = new TableRow();
            TableRow r3 = new TableRow();
            TableRow r4 = new TableRow();
            TableCell c2 = new TableCell();
            TableCell c3 = new TableCell();
            TableCell c4 = new TableCell();

            System.Text.StringBuilder ArrTermin = new System.Text.StringBuilder();

            int term = Db.SingleInteger("select count(distinct termin) from ms_komisi_detail where Nokontrak ='" + Nokontrak + "'");
            DataTable rj = Db.Rs("Select * from ms_komisi_detail where nokontrak ='"+Nokontrak+"' And Baris = 1");
            DataTable rj2 = Db.Rs("Select * from ms_komisi_detail where nokontrak ='" + Nokontrak + "' And Baris = 2");
            

                if (term == 1)
                {
                    c2 = new TableCell();
                    c2.Text = "TERMIN 1";
                    r2.Cells.Add(c2);

                    for(int g = 0; g < rj.Rows.Count; g++)
                    {
                        c2 = new TableCell();
                        c2.Text = Cf.Num(rj.Rows[g]["NilaiKomisi"]);
                        r2.Cells.Add(c2);

                        c2 = new TableCell();
                        c2.Text = Cf.Num(Math.Round(Convert.ToDecimal(rj.Rows[g]["NilaiBayar"])));
                        r2.Cells.Add(c2);

                        c2 = new TableCell();
                        c2.Text = Cf.Day(rj.Rows[g]["TglBayar"]);
                        r2.Cells.Add(c2);

                    }

                    for (int h = 0; h < rj.Rows.Count; h++)
                    {
                        c2 = new TableCell();
                        c2.Text = Cf.Num(rj.Rows[h]["ClosingFee"]);
                        r2.Cells.Add(c2);

                        c2 = new TableCell();
                        c2.Text = Cf.Num(Math.Round(Convert.ToDecimal(rj.Rows[h]["NilaiBayarCF"])));
                        r2.Cells.Add(c2);

                        c2 = new TableCell();
                        c2.Text = Cf.Day(rj.Rows[h]["TglBayarClosingfee"]);
                        r2.Cells.Add(c2);
                    }
                }

                else if (term == 2)
                {
                    c2 = new TableCell();
                    c2.Text = "TERMIN 1";
                    r2.Cells.Add(c2);

                    c3 = new TableCell();
                    c3.Text = "TERMIN 2";
                    r3.Cells.Add(c3);


                    for (int f = 0; f < rj.Rows.Count; f++)
                    {
                        c2 = new TableCell();
                        c2.Text = Cf.Num(rj.Rows[f]["NilaiKomisi"]);
                        r2.Cells.Add(c2);

                        c2 = new TableCell();
                        c2.Text = Cf.Num(Math.Round(Convert.ToDecimal(rj.Rows[f]["NilaiBayar"])));
                        r2.Cells.Add(c2);

                        c2 = new TableCell();
                        c2.Text = Cf.Day(rj.Rows[f]["TglBayar"]);
                        r2.Cells.Add(c2);

                    }

                    //closingfee
                    for (int h = 0; h < rj.Rows.Count; h++)
                    {
                        c2 = new TableCell();
                        c2.Text = Cf.Num(rj.Rows[h]["ClosingFee"]);
                        c2.RowSpan = 2;
                        r2.Cells.Add(c2);

                        c2 = new TableCell();
                        c2.Text = Cf.Num(Math.Round(Convert.ToDecimal(rj.Rows[h]["NilaiBayarCF"])));
                        c2.RowSpan = 2;
                        r2.Cells.Add(c2);

                        c2 = new TableCell();
                        c2.Text = Cf.Day(rj.Rows[h]["TglBayarClosingfee"]);
                        c2.RowSpan = 2;
                        r2.Cells.Add(c2);
                    }

                    for (int f2 = 0; f2 < rj2.Rows.Count; f2++)
                    {
                        c3 = new TableCell();
                        c3.Text = Cf.Num(rj2.Rows[f2]["NilaiKomisi"]);
                        r3.Cells.Add(c3);

                        c3 = new TableCell();
                        c3.Text = Cf.Num(Math.Round(Convert.ToDecimal(rj2.Rows[f2]["NilaiBayar"])));
                        r3.Cells.Add(c3);

                        c3 = new TableCell();
                        c3.Text = Cf.Day(rj2.Rows[f2]["TglBayar"]);
                        r3.Cells.Add(c3);

                    }

                }
                else if (term == 3)
                {
                    DataTable rj3 = Db.Rs("Select * from ms_komisi_detail where nokontrak ='" + Nokontrak + "' And Baris = 3");

                    c2 = new TableCell();
                    c2.Text = "TERMIN 1";
                    r2.Cells.Add(c2);

                    c3 = new TableCell();
                    c3.Text = "TERMIN 2";
                    r3.Cells.Add(c3);

                    c4 = new TableCell();
                    c4.Text = "TERMIN 3";
                    r4.Cells.Add(c4);

                    for (int j = 0; j < rj.Rows.Count; j++)
                    {
                        c2 = new TableCell();
                        c2.Text = Cf.Num(rj.Rows[j]["NilaiKomisi"]);
                        r2.Cells.Add(c2);

                        c2 = new TableCell();
                        c2.Text = Cf.Num(Math.Round(Convert.ToDecimal(rj.Rows[j]["NilaiBayar"])));
                        r2.Cells.Add(c2);

                        c2 = new TableCell();
                        c2.Text = Cf.Day(rj.Rows[j]["TglBayar"]);
                        r2.Cells.Add(c2);
                    }

                    //closingfee
                    for (int h = 0; h < rj.Rows.Count; h++)
                    {
                        c2 = new TableCell();
                        c2.Text = Cf.Num(rj.Rows[h]["ClosingFee"]);
                        c2.RowSpan = 3;
                        r2.Cells.Add(c2);

                        c2 = new TableCell();
                        c2.Text = Cf.Num(Math.Round(Convert.ToDecimal(rj.Rows[h]["NilaiBayarCF"])));
                        c2.RowSpan = 3;
                        r2.Cells.Add(c2);

                        c2 = new TableCell();
                        c2.Text = Cf.Day(rj.Rows[h]["TglBayarClosingfee"]);
                        c2.RowSpan = 3;
                        r2.Cells.Add(c2);
                    }

                    for (int j2 = 0; j2 < rj2.Rows.Count; j2++)
                    {
                        c3 = new TableCell();
                        c3.Text = Cf.Num(rj2.Rows[j2]["NilaiKomisi"]);
                        r3.Cells.Add(c3);

                        c3 = new TableCell();
                        c3.Text = Cf.Num(Math.Round(Convert.ToDecimal(rj2.Rows[j2]["NilaiBayar"])));
                        r3.Cells.Add(c3);

                        c3 = new TableCell();
                        c3.Text = Cf.Day(rj2.Rows[j2]["TglBayar"]);
                        r3.Cells.Add(c3);
                    }

                    for (int j3 = 0; j3 < rj3.Rows.Count; j3++)
                    {
                        c4 = new TableCell();
                        c4.Text = Cf.Num(rj3.Rows[j3]["NilaiKomisi"]);
                        r4.Cells.Add(c4);

                        c4 = new TableCell();
                        c4.Text = Cf.Num(Math.Round(Convert.ToDecimal(rj3.Rows[j3]["NilaiBayar"])));
                        r4.Cells.Add(c4);

                        c4 = new TableCell();
                        c4.Text = Cf.Day(rj3.Rows[j3]["TglBayar"]);
                        r4.Cells.Add(c4);
                    }
                }
                       
            rpt.Rows.Add(r2);
            rpt.Rows.Add(r3);
            rpt.Rows.Add(r4);   
        }
        private void SubTotal(string txt, decimal t1)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = Rpt.Foot();
            c.Text = txt;
            c.ColumnSpan = 8;
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t1);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            rpt.Rows.Add(r);
        }

        protected string TipeReff(string NoKontrak, string RefEm, string RefCust)
        {
            string Tipe = "";
            if (RefEm != "" && RefCust == "")
            {
                Tipe = "EMPLOYEE";
            }
            else if (RefEm == "" && RefCust != "")
            {
                Tipe = "BUYER";
            }

            return Tipe;
        }

        protected string NamaReff(string RefEm, string RefCust)
        {
            string Nama = "";
            if (RefEm != "" && RefCust == "")
            {
                Nama = Db.SingleString("SELECT Nama FROM MS_AGENT WHERE NoAgent = '" + RefEm + "'");
            }
            else if (RefEm == "" && RefCust != "")
            {
                Nama = Db.SingleString("SELECT Nama FROM MS_CUSTOMER WHERE NoCustomer = '" + RefCust + "'");
            }

            return Nama;
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

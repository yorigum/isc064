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
    public partial class MasterKomisiDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();

            if (!Page.IsPostBack)
            {
                comp.InnerHtml = Mi.Pt;
                rpt.Visible = false;
                Js.Focus(this, scr);
                init();
                //Fill();
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
            string aa = "";
            if (UserAgent() > 0)
                aa = " AND NoAgent = " + UserAgent();

            DataTable rs;
            rs = Db.Rs("SELECT DISTINCT(NoKontrak) FROM MS_KOMISI");
            string Customer = "";
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                Customer = Db.SingleString("SELECT Nama From MS_CUSTOMER Where NoCustomer = (Select NoCustomer From MS_KONTRAK Where NoKontrak = '"+ rs.Rows[i]["NoKontrak"].ToString() +"')");
                nokontrak.Items.Add(new ListItem(Customer + " - " + rs.Rows[i]["Nokontrak"].ToString(), rs.Rows[i]["Nokontrak"].ToString()));
            }
            nokontrak.SelectedIndex = 0;

        }

        private bool valid()
        {
            string s = "";
            bool x = true;

            
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

            string Nama = "Laporan Master Komisi Per - Kontrak";
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
                    + ",'" + null + "'"
                    + ",'" + null + "'"
                    );

            //get nomor customer terbaru
            int NoAttachment = Db.SingleInteger(
                "SELECT TOP 1 AttachmentID FROM LapPDF ORDER BY AttachmentID DESC");

            string strSql = "SELECT * FROM ISC064_MARKETINGJUAL..LapPDF WHERE AttachmentID  = '" + NoAttachment + "'";
            DataTable rs = Db.Rs(strSql);

            string nfilename = "MasterKomisiDetail" + NoAttachment + ".pdf";

            //update filename
            Db.Execute("UPDATE ISC064_MARKETINGJUAL..LapPDF SET FileName= '" + nfilename + "' WHERE AttachmentID = " + NoAttachment);


            //folder untuk menyimpan file pdf
            string save = Mi.PathFilePDFReport + "MasterKomisiDetail" + rs.Rows[0]["AttachmentID"] + ".pdf";

            string NoKontrak = nokontrak.SelectedValue;
            string Termin = listtermin.SelectedValue;

            //link untuk download pdf
            string link = Mi.PathAlamatWeb + "marketingjual/LaporanPDF/PDFMasterKomisiDetail.aspx?id=" + rs.Rows[0]["AttachmentID"]
                + "&nokontrak=" + NoKontrak 
                + "&termin=" + Termin
                + "&userid=" + UserID

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

            Rpt.SubJudul(x
                , "NoKontrak : " + nokontrak.SelectedItem.Text
                );

            string legend = "Status: A = Aktif / B = Batal.< br /> ";
            Rpt.HeaderReport(headReport, legend, x);
        }

        private void MenuAtas()
        {
            TableRow r = new TableRow();
            TableCell c = new TableCell();

            c = new TableCell();
            c.Text = "No Kontrak";
            c.ForeColor = Color.White;
            c.Attributes["style"] = "background-color:#1E90FF";
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "Nama Customer";
            c.ForeColor = Color.White;
            c.Attributes["style"] = "background-color:#1E90FF";
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "Nilai Kontrak";
            c.ForeColor = Color.White;
            c.Attributes["style"] = "background-color:#1E90FF";
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "Nilai DPP";
            c.ForeColor = Color.White;
            c.Attributes["style"] = "background-color:#1E90FF";
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "Nama";
            c.ForeColor = Color.White;
            c.Attributes["style"] = "background-color:#1E90FF";
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "Tipe";
            c.ForeColor = Color.White;
            c.Attributes["style"] = "background-color:#1E90FF";
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "NPWP";
            c.ForeColor = Color.White;
            c.Attributes["style"] = "background-color:#1E90FF";
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "Bank Account";
            c.ForeColor = Color.White;
            c.Attributes["style"] = "background-color:#1E90FF";
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "Gross Closing Fee";
            c.ForeColor = Color.White;
            c.Attributes["style"] = "background-color:#1E90FF";
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "Termin";
            c.ForeColor = Color.White;
            c.Attributes["style"] = "background-color:#1E90FF";
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "Gross Komisi (Rp)";
            c.ForeColor = Color.White;
            c.Attributes["style"] = "background-color:#1E90FF";
            r.Cells.Add(c);
           
            rpt.Rows.Add(r);
        }

        private void Fill()
        {
            int barisakhir = Db.SingleInteger("Select MAX(Baris) from ms_komisi_detail where nokontrak = '" + nokontrak.SelectedValue + "'");
            string sql1 = "";
            if (Convert.ToInt32(listtermin.SelectedValue) == 0)
            {
                sql1 = "SELECT distinct ClosingFee , BarisTermin, Tipe"
                    + " FROM MS_KOMISI_DETAIL"
                    + " WHERE NoKontrak = '" + nokontrak.SelectedValue + "' "
                    + " AND NilaiKomisi <> 0"
                    + " ORDER BY Baristermin ASC";
            }
            else if (Convert.ToInt32(listtermin.SelectedValue) <= Convert.ToInt32(barisakhir))
            {
                sql1 = "SELECT ClosingFee , BarisTermin, Tipe"
                    + " FROM MS_KOMISI_DETAIL"
                    + " WHERE NoKontrak = '" + nokontrak.SelectedValue + "' "
                    + " AND NilaiKomisi <> 0"
                    + " AND Baris = '" + barisakhir + "'"
                    + " ORDER BY Baristermin ASC";
            }
            else
            {
                sql1 = "SELECT distinct ClosingFee , BarisTermin, Tipe"
                    + " FROM MS_KOMISI_DETAIL"
                    + " WHERE NoKontrak = '" + nokontrak.SelectedValue + "' "
                    + " AND NilaiKomisi <> 0"
                    + " ORDER BY Baristermin ASC";
            }

            DataTable rc = Db.Rs(sql1);
            
            Rpt.NoData(rpt, rc, "Daftar komisi untuk kontrak tersebut masih kosong.");

            decimal t1 = 0, t2 = 0, cf1 = 0;
            TableRow r = new TableRow();
            string b = "";
            string n = "";
            string an = "";
            string bank = "";
            string rek = "";
            string tipe1 = "";
                 
            int noagent = Db.SingleInteger("Select NoAgent from ms_kontrak where nokontrak='" + nokontrak.SelectedValue + "'");
            int nocust = Db.SingleInteger("Select Nocustomer from ms_kontrak where nokontrak = '" +nokontrak.SelectedValue+ "'");
            string namacust = Db.SingleString("select nama from ms_customer where Nocustomer = '"+ nocust +"'");
            decimal nilaikontrak = Db.SingleDecimal("select NilaiKontrak from ms_kontrak where nokontrak = '"+nokontrak.SelectedValue+"'");
            decimal nilaidpp = Db.SingleDecimal("select NilaiDPP from ms_kontrak where nokontrak = '"+nokontrak.SelectedValue+"'");

            for (int k = 0; k < rc.Rows.Count; k++)
            {
                
                if (Convert.ToInt32(listtermin.SelectedValue) > Convert.ToInt32(barisakhir+1))
                {
                    TableRow rq = new TableRow();
                    TableCell cq = new TableCell();
                    rpt.Rows.Clear();
                    cq = new TableCell();
                    cq.ColumnSpan = 10;
                    cq.Text = "Termin yang dipilih tidak sampai 3 baris.";
                    rq.Cells.Add(cq);

                    rpt.Rows.Add(rq);
                }

                if (!Response.IsClientConnected) break;

                decimal nilaibgb = Db.SingleDecimal("Select SUM(ClosingFee) from ms_komisi_detail where nokontrak = '" + nokontrak.SelectedValue + "' AND BarisTermin = 8 "); //BGB
                decimal nilaireff = Db.SingleDecimal("Select SUM(ClosingFee) from ms_komisi_detail where nokontrak = '" + nokontrak.SelectedValue + "' AND BarisTermin = 2 "); //Reff

                if (Convert.ToInt32(rc.Rows[k]["BarisTermin"]) == 2)
                {
                    if(nilaibgb > 0)
                    {
                        b = Db.SingleString("Select Salesmanager from ms_agent where noagent ='" + noagent + "'");
                        n = Db.SingleString("Select NpwpSalesmanager from ms_agent where noagent ='" + noagent + "'");
                        an = Db.SingleString("Select AtasNamaSalesmanager from ms_agent where noagent ='" + noagent + "'");
                        bank = Db.SingleString("Select bankSalesmanager from ms_agent where noagent ='" + noagent + "'");
                        rek = Db.SingleString("Select rekSalesmanager from ms_agent where noagent ='" + noagent + "'");
                    }
                    else
                    {
                        b = Db.SingleString("Select reffcust from ms_kontrak where nokontrak = '" + nokontrak.SelectedValue + "'");
                        n = Db.SingleString("Select npwpreff from ms_kontrak where nokontrak = '" + nokontrak.SelectedValue + "'");
                        an = Db.SingleString("Select anreff from ms_kontrak where nokontrak = '" + nokontrak.SelectedValue + "'");
                        bank = Db.SingleString("Select bankreff from ms_kontrak where nokontrak = '" + nokontrak.SelectedValue + "'");
                        rek = Db.SingleString("Select norekreff from ms_kontrak where nokontrak = '" + nokontrak.SelectedValue + "'");
                    }
                    
                }
                else if (Convert.ToInt32(rc.Rows[k]["BarisTermin"]) == 5)
                {
                    b = Db.SingleString("Select Salesmanager from ms_agent where noagent ='" + noagent + "'");
                    n = Db.SingleString("Select NpwpSalesmanager from ms_agent where noagent ='" + noagent + "'");
                    an = Db.SingleString("Select AtasNamaSalesmanager from ms_agent where noagent ='" + noagent + "'");
                    bank = Db.SingleString("Select bankSalesmanager from ms_agent where noagent ='" + noagent + "'");
                    rek = Db.SingleString("Select rekSalesmanager from ms_agent where noagent ='" + noagent + "'");
                }
                else if (Convert.ToInt32(rc.Rows[k]["BarisTermin"]) == 6)
                {
                    b = Db.SingleString("Select GeneralManager from ms_agent where noagent ='" + noagent + "'");
                    n = Db.SingleString("Select NpwpGeneralManager from ms_agent where noagent ='" + noagent + "'");
                    an = Db.SingleString("Select AtasNamaGeneralManager from ms_agent where noagent ='" + noagent + "'");
                    bank = Db.SingleString("Select bankGeneralManager from ms_agent where noagent ='" + noagent + "'");
                    rek = Db.SingleString("Select rekGeneralManager from ms_agent where noagent ='" + noagent + "'");
                }
                else if (Convert.ToInt32(rc.Rows[k]["BarisTermin"]) == 7)
                {
                    b = Db.SingleString("Select MarketingSupport from ms_agent where noagent ='" + noagent + "'");
                    n = Db.SingleString("Select NpwpSupporting from ms_agent where noagent ='" + noagent + "'");
                    an = Db.SingleString("Select AtasNamaSupporting from ms_agent where noagent ='" + noagent + "'");
                    bank = Db.SingleString("Select bankSupporting from ms_agent where noagent ='" + noagent + "'");
                    rek = Db.SingleString("Select rekSupporting from ms_agent where noagent ='" + noagent + "'");
                }
                else if (Convert.ToInt32(rc.Rows[k]["BarisTermin"]) == 8)
                {
                    if (nilaireff > 0)
                    {
                        b = Db.SingleString("Select Salesmanager from ms_agent where noagent ='" + noagent + "'");
                        n = Db.SingleString("Select NpwpSalesmanager from ms_agent where noagent ='" + noagent + "'");
                        an = Db.SingleString("Select AtasNamaSalesmanager from ms_agent where noagent ='" + noagent + "'");
                        bank = Db.SingleString("Select bankSalesmanager from ms_agent where noagent ='" + noagent + "'");
                        rek = Db.SingleString("Select rekSalesmanager from ms_agent where noagent ='" + noagent + "'");
                    }
                    else
                    {
                        b = Db.SingleString("Select reffcust from ms_kontrak where nokontrak = '" + nokontrak.SelectedValue + "'");
                        n = Db.SingleString("Select npwpreff from ms_kontrak where nokontrak = '" + nokontrak.SelectedValue + "'");
                        an = Db.SingleString("Select anreff from ms_kontrak where nokontrak = '" + nokontrak.SelectedValue + "'");
                        bank = Db.SingleString("Select bankreff from ms_kontrak where nokontrak = '" + nokontrak.SelectedValue + "'");
                        rek = Db.SingleString("Select norekreff from ms_kontrak where nokontrak = '" + nokontrak.SelectedValue + "'");
                    }
                }
                else if (Convert.ToInt32(rc.Rows[k]["BarisTermin"]) == 9)
                {
                    b = Db.SingleString("Select GMMarketing from ms_agent where noagent ='" + noagent + "'");
                    n = Db.SingleString("Select NpwpGMMarketing from ms_agent where noagent ='" + noagent + "'");
                    an = Db.SingleString("Select AtasNamaGMMarketing from ms_agent where noagent ='" + noagent + "'");
                    bank = Db.SingleString("Select bankGMMarketing from ms_agent where noagent ='" + noagent + "'");
                    rek = Db.SingleString("Select rekGMMarketing from ms_agent where noagent ='" + noagent + "'");
                }
                else if (Convert.ToInt32(rc.Rows[k]["BarisTermin"]) == 10)
                {
                    b = Db.SingleString("Select Bod from ms_agent where noagent ='" + noagent + "'");
                    n = Db.SingleString("Select NpwpBod from ms_agent where noagent ='" + noagent + "'");
                    an = Db.SingleString("Select AtasNamaBod from ms_agent where noagent ='" + noagent + "'");
                    bank = Db.SingleString("Select bankBod from ms_agent where noagent ='" + noagent + "'");
                    rek = Db.SingleString("Select rekBod from ms_agent where noagent ='" + noagent + "'");
                }
                else if (Convert.ToInt32(rc.Rows[k]["BarisTermin"]) == 11)
                {
                    b = Db.SingleString("Select Wadir from ms_agent where noagent ='" + noagent + "'");
                    n = Db.SingleString("Select NpwpWadir from ms_agent where noagent ='" + noagent + "'");
                    an = Db.SingleString("Select AtasNamaWadir from ms_agent where noagent ='" + noagent + "'");
                    bank = Db.SingleString("Select bankWadir from ms_agent where noagent ='" + noagent + "'");
                    rek = Db.SingleString("Select rekWadir from ms_agent where noagent ='" + noagent + "'");
                }
                else if (Convert.ToInt32(rc.Rows[k]["BarisTermin"]) == 13)
                {
                    b = Db.SingleString("Select coagent from ms_agent where noagent ='" + noagent + "'");
                    n = Db.SingleString("Select Npwpcoagent from ms_agent where noagent ='" + noagent + "'");
                    an = Db.SingleString("Select AtasNamacoagent from ms_agent where noagent ='" + noagent + "'");
                    bank = Db.SingleString("Select bankcoagent from ms_agent where noagent ='" + noagent + "'");
                    rek = Db.SingleString("Select rekcoagent from ms_agent where noagent ='" + noagent + "'");
                }
                else
                {
                    b = Db.SingleString("Select Nama from ms_agent where noagent ='" + noagent + "'");
                    n = Db.SingleString("Select NPWP from ms_agent where noagent ='" + noagent + "'");
                    an = Db.SingleString("Select AtasNama from ms_agent where noagent ='" + noagent + "'");
                    bank = Db.SingleString("Select RekBank from ms_agent where noagent ='" + noagent + "'");
                    rek = Db.SingleString("Select Rekening from ms_agent where noagent ='" + noagent + "'");
                }
                tipe1 = rc.Rows[k]["Tipe"].ToString();
                cf1 = Convert.ToDecimal(rc.Rows[k]["ClosingFee"]);
                
                
                r = new TableRow();

                TableCell c;
                c = new TableCell();
                c.Text = nokontrak.SelectedValue;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = namacust;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(nilaikontrak);
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Math.Round(nilaidpp));
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = b;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = tipe1;
                r.Cells.Add(c);

                c = new TableCell();                
                c.Text = "'"+n;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = bank + " / " + an + " / " + rek;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(cf1);
                r.Cells.Add(c);

                //Rpt.Border(r);
                
                if (Convert.ToInt32(listtermin.SelectedValue) <= Convert.ToInt32(barisakhir))
                {
                    rpt.Rows.Add(r);
                    filldetail(nokontrak.Text, tipe1, c, r);
                }
                else
                {
                   //kosong
                }
                
            }

            string sql = "";
            decimal nilkom = 0;
            decimal nilCF = 0;

            if (Convert.ToInt32(listtermin.SelectedValue) == 0)
            {
                sql = "SELECT NilaiKomisi"
                        + " FROM MS_KOMISI_DETAIL"
                        + " WHERE NoKontrak = '" + nokontrak.SelectedValue + "' "
                        + " AND NilaiKomisi <> 0"
                        + " ORDER BY Baristermin ASC";

                nilCF = Db.SingleDecimal("SELECT SUM(ClosingFee) / " + barisakhir 
                    + " AS ClosingFee "
                   + " FROM MS_KOMISI_DETAIL"
                   + " WHERE NoKontrak = '" + nokontrak.SelectedValue + "' "
                   + " AND NilaiKomisi <> 0");

            }
            else if (Convert.ToInt32(listtermin.SelectedValue) <= barisakhir)
            {
                sql = "SELECT NilaiKomisi"
                        + " FROM MS_KOMISI_DETAIL"
                        + " WHERE NoKontrak = '" + nokontrak.SelectedValue + "' "
                        + " AND NilaiKomisi <> 0"
                        + " AND Baris = '" + listtermin.SelectedValue + "'"
                        + " ORDER BY Baristermin ASC";

                nilkom = Db.SingleDecimal("SELECT SUM(NilaiKomisi) "
                   + " FROM MS_KOMISI_DETAIL"
                   + " WHERE NoKontrak = '" + nokontrak.SelectedValue + "' "
                   + " AND NilaiKomisi <> 0"
                   + " AND Baris = '" + listtermin.SelectedValue + "'");

                nilCF = Db.SingleDecimal("SELECT SUM(ClosingFee) "
                   + " FROM MS_KOMISI_DETAIL"
                   + " WHERE NoKontrak = '" + nokontrak.SelectedValue + "' "
                   + " AND NilaiKomisi <> 0"
                   + " AND Baris = '" + listtermin.SelectedValue + "'");
            }
            else
            {
                sql = "SELECT NilaiKomisi"
                        + " FROM MS_KOMISI_DETAIL"
                        + " WHERE NoKontrak = '" + nokontrak.SelectedValue + "' "
                        + " AND NilaiKomisi <> 0"
                        + " ORDER BY Baristermin ASC";

                nilCF = Db.SingleDecimal("SELECT SUM(ClosingFee) "
                   + " FROM MS_KOMISI_DETAIL"
                   + " WHERE NoKontrak = '" + nokontrak.SelectedValue + "' "
                   + " AND NilaiKomisi <> 0");
            }

            DataTable rs = Db.Rs(sql);

            if (Convert.ToInt32(listtermin.SelectedValue) <= barisakhir)
            {
                t1 += nilkom;
                t2 = nilCF;
            }

            for (int q = 0; q < rs.Rows.Count; q++)
            {
                if (Convert.ToInt32(listtermin.SelectedValue) == 0)
                {
                    t1 += Convert.ToDecimal(rs.Rows[q]["NilaiKomisi"]);
                    t2 = nilCF;
                }
            }

            

            //for (int s = 0; s < rc.Rows.Count; s++)
            //{
            //    decimal potongnilaibgb = Db.SingleDecimal("Select ClosingFee from ms_komisi_detail where nokontrak = '" + nokontrak.SelectedValue + "' AND BarisTermin = 8 ");
            //    decimal potongnilai = Db.SingleDecimal("Select ClosingFee from ms_komisi_detail where nokontrak = '"+nokontrak.SelectedValue+"' AND BarisTermin = 2 ");
            //    if(potongnilai == 0)
            //    {
            //        t2 = nilCF;
            //    }
            //    t2 = nilCF;
            //}

            if (Convert.ToInt32(listtermin.SelectedValue) > Convert.ToInt32(barisakhir))
            {

            }
            else
            {
                SubTotal("TOTAL CLOSING FEE", " TOTAL KOMISI", t1, t2);
            }
            
        }

        private void filldetail(string nokontrak, string tipe, TableCell c, TableRow r)
        {
            string sql = "";
            if (Convert.ToInt32(listtermin.SelectedValue) == 0)
            {
                sql = "SELECT * "
                        + " FROM MS_KOMISI_DETAIL"
                        + " WHERE NoKontrak = '" + nokontrak + "' "
                        + " AND NilaiKomisi <> 0 "
                        + " AND Tipe = '" + tipe + "'"
                        + " ORDER BY Baristermin,nilaikomisi asc";
            }
            else
            {
                sql = "SELECT * "
                        + " FROM MS_KOMISI_DETAIL"
                        + " WHERE NoKontrak = '" + nokontrak + "' "
                        + " AND NilaiKomisi <> 0 "
                        + " AND Tipe = '" + tipe + "'"
                        + " AND Baris = '" + listtermin.SelectedValue + "'"
                        + " ORDER BY Baristermin,nilaikomisi asc";
            }

            DataTable rs = Db.Rs(sql);

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (i  == 0)
                {
                    c = new TableCell();
                    c.Text = rs.Rows[i]["Termin"].ToString();
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Cf.Num(Math.Round(Convert.ToDecimal(rs.Rows[i]["NilaiKomisi"])));
                    r.Cells.Add(c);

                    //Rpt.Border(r);
                    rpt.Rows.Add(r);
                }
                else
                {
                    TableRow r2 = new TableRow();

                    c = new TableCell();
                    c.Text = "";
                    c.ColumnSpan = 9;
                    r2.Cells.Add(c);

                    c = new TableCell();
                    c.Text = rs.Rows[i]["Termin"].ToString();
                    r2.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Cf.Num(Math.Round(Convert.ToDecimal(rs.Rows[i]["NilaiKomisi"])));
                    r2.Cells.Add(c);

                    //Rpt.Border(r2);
                    rpt.Rows.Add(r2);
                }
                
                
            }
        }

        private void SubTotal(string cf, string txt, decimal t1, decimal t2)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = Rpt.Foot();
            c.Text = cf;
            c.ColumnSpan = 8;
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(Math.Round(t2));
            c.HorizontalAlign = HorizontalAlign.Right;

            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = txt;
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(Math.Round(t1));
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
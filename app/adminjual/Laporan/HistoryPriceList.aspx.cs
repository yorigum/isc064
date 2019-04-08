using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Diagnostics;

namespace ISC064.ADMINJUAL.Laporan
{
    public partial class HistoryPriceList : System.Web.UI.Page
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

        private void init()
        {
            string Project = project.SelectedIndex == 0 ? "Project IN (" + Act.ProjectListSql + ")" : "Project = '" + project.SelectedValue + "'";

            DataTable rs;
            lokasi.Items.Clear();
            lokasi.Items.Add("SEMUA");
            periode.Items.Clear();
            periode.Items.Add("SEMUA");
            rs = Db.Rs("SELECT DISTINCT YEAR(Periode), MONTH(Periode) FROM MS_PRICELIST_HISTORY "
                + "  ORDER BY YEAR(Periode), MONTH(Periode)");
            for (int i = 0; i < rs.Rows.Count; i++)
                periode.Items.Add(new ListItem(
                    Cf.Monthname((int)rs.Rows[i][1]) + " " + rs.Rows[i][0].ToString()
                    , rs.Rows[i][0] + "," + rs.Rows[i][1]
                    ));

            rs = Db.Rs("SELECT * FROM REF_JENIS WHERE " + Project + " ORDER BY SN");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["Jenis"].ToString();
                string t = v + " - " + rs.Rows[i]["Nama"].ToString();
                jenis.Items.Add(new ListItem(t, v));
                jenis.Items[i].Selected = true;
            }

            rs = Db.Rs("SELECT DISTINCT Lokasi FROM MS_UNIT WHERE " + Project + " ORDER BY Lokasi");
            for (int i = 0; i < rs.Rows.Count; i++)
                lokasi.Items.Add(new ListItem(rs.Rows[i][0].ToString()));

     
        }

        private bool valid()
        {
            string s = "";
            bool x = true;

            if (!Cf.isPilih(jenis))
            {
                x = false;
                jenisc.Text = " Pilih Minimum Satu";
            }
            else
                jenisc.Text = "";

            if (!x && s != "")
            {
                ClientScript.RegisterStartupScript(GetType(), "err"
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
                //Rpt.ToExcel(this,rpt);
                Rpt.ToExcel(this, headReport, rpt);
            }
        }
        protected void pdf_Click(object sender, System.EventArgs e)
        {

            Process p = new System.Diagnostics.Process();

            string Nama = "Laporan History Price List";
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

            string nfilename = "LapHistoryPriceList" + NoAttachment + ".pdf";

            //update filename
            Db.Execute("UPDATE ISC064_MARKETINGJUAL..LapPDF SET FileName= '" + nfilename + "' WHERE AttachmentID = " + NoAttachment);


            //folder untuk menyimpan file pdf
            string save = Mi.PathFilePDFReport + "LapHistoryPriceList" + rs.Rows[0]["AttachmentID"] + ".pdf";


            string Input = "";
            if (periode.SelectedIndex != 0)
            {
                string[] z = periode.SelectedValue.Split(',');
                Input = z[0] + "-" + z[1];
            }
            else
                Input = periode.SelectedValue;

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

            string Project = "";
            if (project.SelectedValue == "SEMUA")
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
            //link untuk download pdf
            string link = Mi.PathAlamatWeb + "adminjual/LaporanPDF/PDFHistoryPriceList.aspx?id=" + rs.Rows[0]["AttachmentID"] + "&jenis=" + nm + "&input=" + Input + "&status_s=" + nStatusS + "&status_b=" + nStatusB + "&status_a=" + nStatusA + "&project=" + Project + "";

            //update link
            Db.Execute("UPDATE ISC064_MARKETINGJUAL..LapPDF SET Link= '" + link + "' WHERE AttachmentID = " + NoAttachment);

            //format page
            p.StartInfo.Arguments = "--orientation landscape --page-width 16.5in --page-height 40in --margin-left 0 --margin-right 0 --margin-top 0.25cm --margin-bottom 0 " + link + " " + save;

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

            FillHeader();
            Fill();
        }

        private void FillHeader()
        {
            TableHeaderCell c = new TableHeaderCell();

            DataTable rs = Db.Rs("SELECT DISTINCT(Periode) FROM MS_PRICELIST_HISTORY");
            int j = 0;
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                c = new TableHeaderCell();
                c.ForeColor = Color.White;
                c.Attributes["style"] = "background-color:'#1E90FF'";
                rpt.Rows[0].Cells.Add(c);
                rpt.Rows[0].Cells[3 + j].Text = "Periode";

                c = new TableHeaderCell();
                c.ForeColor = Color.White;
                c.Attributes["style"] = "background-color:'#1E90FF'";
                rpt.Rows[0].Cells.Add(c);
                rpt.Rows[0].Cells[4 + j].Text = "PriceList";

                j += 2;
            }
            //rpt.Rows[1].Cells[14].Text = "";

            c = new TableHeaderCell();
            c.ForeColor = Color.White;
            c.Attributes["style"] = "background-color:'#1E90FF'";
            rpt.Rows[0].Cells.Add(c);
            rpt.Rows[0].Cells[j + 3].Text = "No Kontrak";

            c = new TableHeaderCell();
            c.ForeColor = Color.White;
            c.Attributes["style"] = "background-color:'#1E90FF'";
            rpt.Rows[0].Cells.Add(c);
            rpt.Rows[0].Cells[j + 4].Text = "Tgl Kontrak";

            c = new TableHeaderCell();
            c.ForeColor = Color.White;
            c.Attributes["style"] = "background-color:'#1E90FF'";
            rpt.Rows[0].Cells.Add(c);
            rpt.Rows[0].Cells[j + 5].Text = "Customer";

            c = new TableHeaderCell();
            c.ForeColor = Color.White;
            c.Attributes["style"] = "background-color:'#1E90FF'";
            rpt.Rows[0].Cells.Add(c);
            rpt.Rows[0].Cells[j + 6].Text = "Nilai PriceList";

            //rpt.Rows[1].Cells[14 + 1].Text = "Tgl Kontrak";
            //rpt.Rows[1].Cells[16].Text = "Customer";
            //rpt.Rows[1].Cells[17].Text = "Nilai PriceList";


            System.Text.StringBuilder x = new System.Text.StringBuilder();

            Rpt.Judul(x, comp, judul);

            //if (statusA.Checked)
            //    Rpt.SubJudul(x, "Status : " + statusA.Text);
            //else if (statusB.Checked)
            //    Rpt.SubJudul(x, "Status : " + statusB.Text);
            //else
            //    Rpt.SubJudul(x, "Status : " + statusS.Text);

            Rpt.SubJudul(x
                , "Periode : " + periode.SelectedItem.Text
                );

            Rpt.SubJudul(x
                , "Project : " + project.SelectedItem.Text
                );

            //Rpt.SubJudul(x
            //    , "Jenis : " + Rpt.inSql(jenis).Replace("'", "")
            //    );

            //Rpt.SubJudul(x
            //    , "Lokasi : " + lokasi.SelectedItem.Text
            //    );

            string legend = "Status: A = Aktif / B = Blokir."
                            + "<br />"
                            + "Luas dalam meter persegi.Harga price list adalah dalam rupiah.";
            //Rpt.Header(rpt, x);
            Rpt.HeaderReport(headReport, legend, x);
        }

        private void Fill()
        {

            string Status = "";
            if (statusA.Checked) Status = " AND b.Status = 'A'";
            if (statusB.Checked) Status = " AND b.Status = 'B'";

            string Periode = "";
            if (periode.SelectedIndex != 0)
            {
                string[] z = periode.SelectedValue.Split(',');
                Periode = " AND YEAR(a.Periode) = " + z[0]
                    + " AND MONTH(a.Periode) = " + z[1];
            }

            string Lokasi = "";
            if (lokasi.SelectedIndex != 0)
            {
                Lokasi = " AND b.Lokasi = '" + Cf.Str(lokasi.SelectedValue) + "'";
            }

            string Project = " AND b.Project IN (" + Act.ProjectListSql + ")";
            if (project.SelectedValue != "SEMUA")
            {
                Project = " AND b.Project = '" + project.SelectedValue + "'";
            }
            decimal t1 = 0;

            string strSql = "SELECT a.NoStock "
                + " FROM MS_PRICELIST_HISTORY a"
                + " INNER JOIN MS_UNIT b ON a.NoStock = b.NoStock"
                + " WHERE 1=1 "
                + Status
                + Periode
                + " GROUP BY a.NoStock"
                + " ORDER BY a.NoStock";
            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                string strSql2 = "SELECT a.*, b.NoUnit"
                    + " FROM MS_PRICELIST_HISTORY a"
                    + " INNER JOIN MS_UNIT b ON a.NoStock = b.NoStock"
                    + " WHERE a.NoStock = '" + rs.Rows[i]["NoStock"] + "'"
                    + " ORDER BY a.NoStock";
                DataTable rs2 = Db.Rs(strSql2);
                if (rs2.Rows.Count > 0)
                {
                    TableRow r = new TableRow();
                    TableCell c;

                    r.VerticalAlign = VerticalAlign.Top;
                    r.Attributes["ondblclick"] = "popEditUnit('" + rs2.Rows[0]["NoStock"] + "')";

                    DateTime p = Convert.ToDateTime(rs2.Rows[0]["Periode"]);

                    c = new TableCell();
                    c.Text = (i + 1).ToString();
                    c.HorizontalAlign = HorizontalAlign.Left;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = rs2.Rows[0]["NoStock"].ToString();
                    c.HorizontalAlign = HorizontalAlign.Left;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = rs2.Rows[0]["NoUnit"].ToString();
                    c.HorizontalAlign = HorizontalAlign.Left;
                    r.Cells.Add(c);

                    DataTable aa = Db.Rs("SELECT DISTINCT(Periode) FROM MS_PRICELIST_HISTORY ORDER BY Periode");
                    for (int j = 0; j < aa.Rows.Count; j++)
                    {
                        string nilai = "";
                        DataTable bb = Db.Rs("SELECT TOP 1 PriceList FROM MS_PRICELIST_HISTORY WHERE NoStock = '" + rs2.Rows[0]["NoStock"] + "' AND Periode = '" + aa.Rows[j]["Periode"] + "' ORDER BY No DESC");
                        if (bb.Rows.Count > 0)
                            nilai = Cf.Num(bb.Rows[0]["PriceList"]);

                        c = new TableCell();
                        c.Text = nilai != "" ? Cf.Day(aa.Rows[j]["Periode"]) : "";
                        c.HorizontalAlign = HorizontalAlign.Right;
                        r.Cells.Add(c);

                        c = new TableCell();
                        c.Text = nilai;
                        c.HorizontalAlign = HorizontalAlign.Right;
                        r.Cells.Add(c);
                    }

                    string strSql3 = "SELECT b.*, c.Nama"
                    + " FROM MS_KONTRAK b"
                    + " INNER JOIN MS_CUSTOMER c ON b.NoCustomer = c.NoCustomer"
                    + " WHERE b.NoStock = '" + rs.Rows[i]["NoStock"] + "'"
                    + " ORDER BY b.NoStock";
                    DataTable rs3 = Db.Rs(strSql3);
                    if (rs3.Rows.Count > 0)
                    //for (int k = 0; k < rs3.Rows.Count; k++)
                    {
                        c = new TableCell();
                        c.Text = rs3.Rows[0]["NoKontrak"].ToString();
                        c.HorizontalAlign = HorizontalAlign.Left;
                        r.Cells.Add(c);

                        c = new TableCell();
                        c.Text = Cf.Day(rs3.Rows[0]["TglKontrak"]);
                        c.HorizontalAlign = HorizontalAlign.Left;
                        r.Cells.Add(c);

                        c = new TableCell();
                        c.Text = rs3.Rows[0]["Nama"].ToString();
                        c.HorizontalAlign = HorizontalAlign.Left;
                        r.Cells.Add(c);

                        c = new TableCell();
                        c.Text = Cf.Num(rs3.Rows[0]["Gross"]);
                        c.HorizontalAlign = HorizontalAlign.Left;
                        r.Cells.Add(c);

                    }
                    else
                    {
                        c = new TableCell();
                        c.Text = "";
                        c.HorizontalAlign = HorizontalAlign.Left;
                        r.Cells.Add(c);

                        c = new TableCell();
                        c.Text = "";
                        c.HorizontalAlign = HorizontalAlign.Left;
                        r.Cells.Add(c);

                        c = new TableCell();
                        c.Text = "";
                        c.HorizontalAlign = HorizontalAlign.Left;
                        r.Cells.Add(c);

                        c = new TableCell();
                        c.Text = "";
                        c.HorizontalAlign = HorizontalAlign.Left;
                        r.Cells.Add(c);
                    }

                    rpt.Rows.Add(r);

                    t1 += (decimal)rs2.Rows[0]["PriceList"];
                }

                //if(i==rs.Rows.Count-1)
                //    SubTotal("GRAND TOTAL", t1);
            }
        }

        private void SubTotal(string txt, decimal t1)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = Rpt.Foot();
            c.Text = txt;
            c.ColumnSpan = 3;
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);


            c = Rpt.Foot();
            c.Text = Cf.Num(t1);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = "&nbsp;";
            r.Cells.Add(c);

            rpt.Rows.Add(r);
        }

        private void SubTotal(string txt, decimal[] t1)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = Rpt.Foot();
            c.Text = txt;
            c.ColumnSpan = 3;
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            for (int i = 0; i < t1.Length; i++)
            {
                if (t1[i] != 0)
                {
                    c = Rpt.Foot();
                    c.Text = Cf.Num(t1[i]);
                    c.HorizontalAlign = HorizontalAlign.Right;
                    r.Cells.Add(c);

                    c = Rpt.Foot();
                    c.Text = "&nbsp;";
                    r.Cells.Add(c);
                }

                if (t1[i] == 0) //t1[i] == null || 
                    i = t1.Length;
            }
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
            periode.Items.Clear();
            jenis.Items.Clear();
            lokasi.Items.Clear();
            init();
        }
    }
}

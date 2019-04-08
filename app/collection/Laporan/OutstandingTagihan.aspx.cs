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
    public partial class LaporanOutstandingTagihan : System.Web.UI.Page
    {
        protected System.Web.UI.WebControls.PlaceHolder list;
        protected System.Web.UI.WebControls.TextBox tgl;
        protected System.Web.UI.WebControls.Label tglc;
        protected System.Web.UI.WebControls.DropDownList ddlAgent;
        protected System.Web.UI.WebControls.CheckBox cbPrincipal;
        protected System.Web.UI.WebControls.Label lblPrincipal;
        protected System.Web.UI.WebControls.CheckBoxList cblPrincipal;
        protected System.Web.UI.WebControls.CheckBox toponly;

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
            dari.Text = Cf.Day(Cf.AwalBulan());
            sampai.Text = Cf.Day(Cf.AkhirBulan());
            string Project = project.SelectedValue == "SEMUA" ? "Project IN (" + Act.ProjectListSql + ")" : "Project = '" + project.SelectedValue + "'";
            DataTable rs;
            rs = Db.Rs("SELECT DISTINCT Lokasi FROM ISC064_MARKETINGJUAL..MS_KONTRAK WHERE " + Project + " ORDER BY Lokasi");
            for (int i = 0; i < rs.Rows.Count; i++)
                lokasi.Items.Add(new ListItem(rs.Rows[i][0].ToString()));

            lokasi.SelectedIndex = 0;
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
                //Rpt.ToExcel(this,rpt);
                Rpt.ToExcel(this, headReport, rpt);
            }
        }
        protected void pdf_Click(object sender, System.EventArgs e)
        {

            Process p = new System.Diagnostics.Process();

            string Nama = "Laporan Outstanding Tagihan";
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
                    + ",'" + Convert.ToDateTime(dari.Text) + "'"
                    + ",'" + Convert.ToDateTime(sampai.Text) + "'"
                    );

            //get nomor customer terbaru
            int NoAttachment = Db.SingleInteger(
                "SELECT TOP 1 AttachmentID FROM ISC064_FINANCEAR..LapPDF ORDER BY AttachmentID DESC");

            string strSql = "SELECT * FROM ISC064_FINANCEAR..LapPDF WHERE AttachmentID  = '" + NoAttachment + "'";
            DataTable rs = Db.Rs(strSql);

            string nfilename = "LaporanOutstandingTagihan" + NoAttachment + ".pdf";

            //update filename
            Db.Execute("UPDATE ISC064_FINANCEAR..LapPDF SET FileName= '" + nfilename + "' WHERE AttachmentID = " + NoAttachment);


            //folder untuk menyimpan file pdf
            string save = Mi.PathFilePDFReport + "LaporanOutstandingTagihan" + rs.Rows[0]["AttachmentID"] + ".pdf";

            //parameter
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
            string link = Mi.PathAlamatWeb + "collection/LaporanPDF/PDFOutstandingTagihan.aspx?id=" + rs.Rows[0]["AttachmentID"]
                + "&status_a=" + nStatusA
                + "&status_b=" + nStatusB
                + "&status_s=" + nStatusS
                + "&lokasi=" + Lokasi
                + "&userid=" + UserID
                + "&pers=" + pers.SelectedValue
                + "&project=" + Project
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

            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);
            Rpt.SubJudul(x
                , "As of : " + Cf.Day(dari.Text) + " s/d " + Cf.Day(sampai.Text)
                );

            Rpt.SubJudul(x
                , "Lokasi : " + lokasi.SelectedItem.Text
                );
            Rpt.SubJudul(x
                , "Perusahaan : " + pers.SelectedValue
                );
            Rpt.SubJudul(x
                , "Project : " + project.SelectedValue
                );
            //Rpt.Header(rpt, x);
            string legend = "Status: A = Aktif / B = Batal.<br />"
                       + "Luas dalam meter persegi.Gross adalah harga sebelum diskon.";
            Rpt.HeaderReport(headReport, legend, x);
        }

        private void Fill()
        {
            string Status = "";
            if (statusA.Checked) Status = " AND ISC064_MARKETINGJUAL..MS_KONTRAK.Status = 'A'";
            if (statusB.Checked) Status = " AND ISC064_MARKETINGJUAL..MS_KONTRAK.Status = 'B'";

            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);
            if (Dari > Sampai)
            {
                DateTime x = Sampai;
                Sampai = Dari;
                Dari = x;
            }

            string Project = " AND ISC064_MARKETINGJUAL..MS_KONTRAK.Project IN (" + Act.ProjectListSql + ")";
            if (project.SelectedValue != "SEMUA") Project = " AND ISC064_MARKETINGJUAL..MS_KONTRAK.Project = '" + project.SelectedValue + "'";
            string Perusahaan = "";
            if (pers.SelectedValue != "SEMUA") Perusahaan = " AND ISC064_MARKETINGJUAL..MS_KONTRAK.Pers = '" + pers.SelectedValue + "'";

            string Lokasi = "";
            if (lokasi.SelectedIndex != 0)
            {
                Lokasi = " AND Lokasi = '" + Cf.Str(lokasi.SelectedValue) + "'";
            }

            string aa = "";
            if (UserAgent() > 0)
                aa = " AND " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK.NoAgent = " + UserAgent();

            string strSql = "SELECT ISC064_MARKETINGJUAL..MS_KONTRAK.*"
                + ",ISC064_MARKETINGJUAL..MS_CUSTOMER.Nama AS Cs"
                + ",ISC064_MARKETINGJUAL..MS_CUSTOMER.NoTelp AS NoTelp"
                + ",ISC064_MARKETINGJUAL..MS_CUSTOMER.NoHp AS NoHp"
                + ",ISC064_MARKETINGJUAL..MS_AGENT.Nama AS Ag"
                + ",ISC064_MARKETINGJUAL..MS_AGENT.Principal"
                + ",(SELECT ISNULL(SUM(NilaiPelunasan),0) FROM ISC064_MARKETINGJUAL..MS_PELUNASAN WHERE NoKontrak = ISC064_MARKETINGJUAL..MS_KONTRAK.NoKontrak) AS NilaiTTS"
                + " FROM ISC064_MARKETINGJUAL..MS_KONTRAK"
                + " INNER JOIN ISC064_MARKETINGJUAL..MS_CUSTOMER ON ISC064_MARKETINGJUAL..MS_KONTRAK.NoCustomer = ISC064_MARKETINGJUAL..MS_CUSTOMER.NoCustomer"
                + " INNER JOIN ISC064_MARKETINGJUAL..MS_AGENT ON ISC064_MARKETINGJUAL..MS_KONTRAK.NoAgent = ISC064_MARKETINGJUAL..MS_AGENT.NoAgent "
                + " WHERE 1=1 "
                + Perusahaan
                + Project
                + Lokasi
                + Status
                + aa
                ;

            DataTable rs = Db.Rs(strSql);

            TableRow r = new TableRow();
            TableHeaderCell hc;

            hc = new TableHeaderCell();
            hc.Text = "Overdue";
            hc.HorizontalAlign = HorizontalAlign.Center;
            //hc.Attributes["style"] = "background-color:gray;color:white;";
            hc.Wrap = false;
            r.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.Text = "Actual";
            hc.HorizontalAlign = HorizontalAlign.Center;
            //hc.Attributes["style"] = "background-color:gray;color:white;";
            hc.Wrap = false;
            r.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.Text = "Early";
            hc.HorizontalAlign = HorizontalAlign.Center;
            //hc.Attributes["style"] = "background-color:gray;color:white;";
            hc.Wrap = false;
            r.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.Text = "A";
            hc.HorizontalAlign = HorizontalAlign.Center;
            //hc.Attributes["style"] = "background-color:gray;color:white;";
            hc.Wrap = false;
            r.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.Text = "B";
            hc.HorizontalAlign = HorizontalAlign.Center;
            //hc.Attributes["style"] = "background-color:gray;color:white;";
            r.Cells.Add(hc);

            rpt.Rows.Add(r);

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                DataTable rs2 = Db.Rs("SELECT *"
                    + " ,(SELECT COUNT(*) FROM ISC064_MARKETINGJUAL..MS_PELUNASAN WHERE NoKontrak = ISC064_MARKETINGJUAL..MS_TAGIHAN.NoKontrak AND NoTagihan = ISC064_MARKETINGJUAL..MS_TAGIHAN.NoUrut) AS CountLunas"
                    + " ,(SELECT COUNT(*) FROM ISC064_MARKETINGJUAL..MS_PELUNASAN WHERE NoKontrak = ISC064_MARKETINGJUAL..MS_TAGIHAN.NoKontrak AND NoTagihan = ISC064_MARKETINGJUAL..MS_TAGIHAN.NoUrut AND (CONVERT(VARCHAR, TglPelunasan, 112) >= " + Cf.Tgl112(Dari) + " AND CONVERT(VARCHAR, TglPelunasan, 112) <= " + Cf.Tgl112(Sampai) + ")) AS CountLunas2"
                    + " FROM ISC064_MARKETINGJUAL..MS_TAGIHAN"
                    + " WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "'");

                string temp = Tagihan(rs2, Cf.Pk(rs.Rows[i]["NoKontrak"]), "NAMA", Dari, Sampai);
                if (temp != "")
                {
                    r = new TableRow();
                    TableCell c;

                    r.VerticalAlign = VerticalAlign.Top;
                    r.Attributes["ondblclick"] = "popEditKontrak('" + rs.Rows[i]["NoKontrak"] + "')";

                    c = new TableCell();
                    c.Text = rs.Rows[i]["NoUnit"].ToString();
                    c.HorizontalAlign = HorizontalAlign.Left;
                    c.Wrap = false;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = rs.Rows[i]["NoKontrak"].ToString();
                    c.HorizontalAlign = HorizontalAlign.Left;
                    c.Wrap = false;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Cf.Day(rs.Rows[i]["TglKontrak"]);
                    c.HorizontalAlign = HorizontalAlign.Left;
                    c.Wrap = false;
                    r.Cells.Add(c);


                    c = new TableCell();
                    c.Text = Cf.Str(rs.Rows[i]["Cs"]);
                    c.HorizontalAlign = HorizontalAlign.Left;
                    c.Wrap = false;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Cf.Str(rs.Rows[i]["NoTelp"]);
                    c.HorizontalAlign = HorizontalAlign.Left;
                    c.Wrap = false;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Cf.Str(rs.Rows[i]["NoHp"]);
                    c.HorizontalAlign = HorizontalAlign.Left;
                    c.Wrap = false;
                    r.Cells.Add(c);


                    c = new TableCell();
                    c.Text = Cf.Str(rs.Rows[i]["Ag"]);
                    c.HorizontalAlign = HorizontalAlign.Left;
                    c.Wrap = false;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Cf.Num(rs.Rows[i]["NilaiKontrak"]);
                    c.HorizontalAlign = HorizontalAlign.Right;
                    c.Wrap = false;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = temp;
                    c.HorizontalAlign = HorizontalAlign.Left;
                    c.Wrap = false;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Tagihan(rs2, Cf.Pk(rs.Rows[i]["NoKontrak"]), "TGL", Dari, Sampai);
                    c.HorizontalAlign = HorizontalAlign.Left;
                    c.Wrap = false;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Tagihan(rs2, Cf.Pk(rs.Rows[i]["NoKontrak"]), "OVERDUE", Dari, Sampai);
                    c.HorizontalAlign = HorizontalAlign.Right;
                    c.Wrap = false;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Tagihan(rs2, Cf.Pk(rs.Rows[i]["NoKontrak"]), "ACTUAL", Dari, Sampai);
                    c.HorizontalAlign = HorizontalAlign.Right;
                    c.Wrap = false;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Tagihan(rs2, Cf.Pk(rs.Rows[i]["NoKontrak"]), "EARLY", Dari, Sampai);
                    c.HorizontalAlign = HorizontalAlign.Right;
                    c.Wrap = false;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Tagihan(rs2, Cf.Pk(rs.Rows[i]["NoKontrak"]), "TGLLUNAS", Dari, Sampai);
                    c.HorizontalAlign = HorizontalAlign.Left;
                    c.Wrap = false;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Tagihan(rs2, Cf.Pk(rs.Rows[i]["NoKontrak"]), "A", Dari, Sampai);
                    c.HorizontalAlign = HorizontalAlign.Right;
                    c.Wrap = false;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Tagihan(rs2, Cf.Pk(rs.Rows[i]["NoKontrak"]), "B", Dari, Sampai);
                    c.HorizontalAlign = HorizontalAlign.Right;
                    c.Wrap = false;
                    r.Cells.Add(c);

                    rpt.Rows.Add(r);
                }

                if (i == (rs.Rows.Count - 1))
                {
                    SubTotal();
                }
            }
        }

        private string Tagihan(DataTable rs, string NoKontrak, string Type, DateTime Dari, DateTime Sampai)
        {
            string strSql = "";
            bool s = false, Overdue = false, Actual = false, Early = false;
            System.Text.StringBuilder x = new System.Text.StringBuilder();

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                if ((Convert.ToDateTime(rs.Rows[i]["TglJT"]) >= Dari) && (Convert.ToDateTime(rs.Rows[i]["TglJT"]) <= Sampai))   // 1
                {
                    s = true;
                    Overdue = false;
                    Actual = true;
                    Early = false;
                }
                else if (Convert.ToDateTime(rs.Rows[i]["TglJT"]) <= Dari)
                {
                    if (Convert.ToInt32(rs.Rows[i]["CountLunas"]) == 0) // 2
                        s = true;
                    else if (Convert.ToInt32(rs.Rows[i]["CountLunas2"]) > 0)    // 3
                        s = true;
                    else
                        s = false;

                    Overdue = true;
                    Actual = false;
                    Early = false;
                }
                else if (Convert.ToDateTime(rs.Rows[i]["TglJT"]) > Sampai)
                {
                    if (Convert.ToInt32(rs.Rows[i]["CountLunas2"]) > 0) // 4
                        s = true;
                    else
                        s = false;

                    Overdue = false;
                    Actual = false;
                    Early = true;
                }
                else
                {
                    s = false;
                    Overdue = false;
                    Actual = false;
                    Early = false;
                }

                if (s)
                {
                    if (Type == "NAMA")
                        x.Append(rs.Rows[i]["NamaTagihan"].ToString() + "<br />");
                    else if (Type == "TGL")
                        x.Append(Cf.Day(rs.Rows[i]["TglJT"]) + "<br />");
                    else if (Type == "OVERDUE")
                    {
                        if (Overdue)
                        {
                            x.Append(Cf.Num(rs.Rows[i]["NilaiTagihan"]) + "<br />");
                            total1.Text = Cf.Num(Convert.ToDecimal(rs.Rows[i]["NilaiTagihan"])
                                + Convert.ToDecimal(total1.Text));
                        }
                        else
                            x.Append("&nbsp;<br />");
                    }
                    else if (Type == "ACTUAL")
                    {
                        if (Actual)
                        {
                            x.Append(Cf.Num(rs.Rows[i]["NilaiTagihan"]) + "<br />");
                            total2.Text = Cf.Num(Convert.ToDecimal(rs.Rows[i]["NilaiTagihan"])
                                + Convert.ToDecimal(total2.Text));
                        }
                        else
                            x.Append("&nbsp;<br />");
                    }
                    else if (Type == "EARLY")
                    {
                        if (Early)
                        {
                            x.Append(Cf.Num(rs.Rows[i]["NilaiTagihan"]) + "<br />");
                            total3.Text = Cf.Num(Convert.ToDecimal(rs.Rows[i]["NilaiTagihan"])
                                + Convert.ToDecimal(total3.Text));
                        }
                        else
                            x.Append("&nbsp;<br />");
                    }
                    else if (Type == "TGLLUNAS")
                    {
                        strSql = "SELECT TOP 1 TglPelunasan"
                            + " FROM ISC064_MARKETINGJUAL..MS_PELUNASAN"
                            + " WHERE NoTagihan = " + Cf.Pk(rs.Rows[i]["NoUrut"])
                            + " AND NoKontrak = '" + NoKontrak + "'"
                            + " ORDER BY TglPelunasan DESC"
                            ;
                        DataTable rs2 = Db.Rs(strSql);

                        if (rs2.Rows.Count > 0)
                            x.Append(Cf.Day(rs2.Rows[0]["TglPelunasan"]) + "<br />");
                        else
                            x.Append("&nbsp;<br />");
                    }
                    else if (Type == "A")
                    {
                        decimal t = 0;
                        strSql = "SELECT ISNULL(SUM(NilaiPelunasan), 0)"
                            + " FROM ISC064_MARKETINGJUAL..MS_PELUNASAN"
                            + " WHERE NoTagihan = " + Cf.Pk(rs.Rows[i]["NoUrut"])
                            + " AND (CONVERT(varchar, TglPelunasan, 112) >= '" + Cf.Tgl112(Dari) + "'"
                            + " AND CONVERT(varchar, TglPelunasan, 112) <= '" + Cf.Tgl112(Sampai) + "')"
                            + " AND NoKontrak = '" + NoKontrak + "'"
                            ;
                        t = Db.SingleDecimal(strSql);
                        total4.Text = Cf.Num(t + Convert.ToDecimal(total4.Text));

                        if (t != 0)
                            x.Append(Cf.Num(t) + "<br />");
                        else
                            x.Append("&nbsp;<br />");
                    }
                    else if (Type == "B")
                    {
                        decimal t = 0;
                        strSql = "SELECT ISNULL(SUM(NilaiPelunasan), 0)"
                            + " FROM ISC064_MARKETINGJUAL..MS_PELUNASAN"
                            + " WHERE NoTagihan = " + Cf.Pk(rs.Rows[i]["NoUrut"])
                            + " AND (CONVERT(varchar, TglPelunasan, 112) < '" + Cf.Tgl112(Dari) + "'"
                            + " OR CONVERT(varchar, TglPelunasan, 112) > '" + Cf.Tgl112(Sampai) + "')"
                            + " AND NoKontrak = '" + NoKontrak + "'"
                            ;
                        t = Db.SingleDecimal(strSql);
                        total5.Text = Cf.Num(t + Convert.ToDecimal(total5.Text));

                        if (t != 0)
                            x.Append(Cf.Num(t) + "<br />");
                        else
                            x.Append("&nbsp;<br />");
                    }
                }

            }

            return x.ToString();
        }

        private void SubTotal()
        {
            TableRow r = new TableRow();
            TableCell c;

            c = Rpt.Foot();
            c.Text = "";
            c.ColumnSpan = 10;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = total1.Text;
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = total2.Text;
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = total3.Text;
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = "";
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = total4.Text;
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = total5.Text;
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            rpt.Rows.Add(r);
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

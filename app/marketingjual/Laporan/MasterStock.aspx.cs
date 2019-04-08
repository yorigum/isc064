using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;

namespace ISC064.MARKETINGJUAL.Laporan
{
    public partial class MasterStock : System.Web.UI.Page
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

        private void init()
        {
            DataTable rs;

            lokasi.Items.Clear();
            lokasi.Items.Add("SEMUA");
            lokasi.SelectedIndex = 0;
            string Project = project.SelectedIndex == 0 ? "Project IN (" + Act.ProjectListSql + ")" : "Project = '" + project.SelectedValue + "'";
            rs = Db.Rs("SELECT * FROM REF_LOKASI WHERE " + Project + " ORDER BY Lokasi");
            for (int i = 0; i < rs.Rows.Count; i++)
                lokasi.Items.Add(new ListItem(rs.Rows[i][0].ToString()));

            Act.ProjectList(project);;
        }

        protected void scr_Click(object sender, System.EventArgs e)
        {
            Report();
        }

        protected void xls_Click(object sender, System.EventArgs e)
        {
            Report();
            rp.Controls.Add(headJudul);
            rp.Controls.Add(rpt);
            Rpt.ToExcel(this, rp);
        }
        protected void pdf_Click(object sender, System.EventArgs e)
        {

            Process p = new System.Diagnostics.Process();

            string Nama = "Laporan Master Stock";
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

            string nfilename = "MasterStock" + NoAttachment + ".pdf";

            //update filename
            Db.Execute("UPDATE ISC064_MARKETINGJUAL..LapPDF SET FileName= '" + nfilename + "' WHERE AttachmentID = " + NoAttachment);


            //folder untuk menyimpan file pdf
            string save = Mi.PathFilePDFReport + "MasterStock" + rs.Rows[0]["AttachmentID"] + ".pdf";

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
            string link = Mi.PathAlamatWeb + "marketingjual/LaporanPDF/PDFMasterStock.aspx?id=" + rs.Rows[0]["AttachmentID"] + "&userid=" + UserID + "&lokasi=" + lokasi.SelectedValue + "&project=" + Project;

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

            newHeader();
            //Header();
            Fill();
        }

        private void newHeader()
        {
            string header = "<h2>" + Mi.Pt + "</h2>";
            header += "<h1 class='title'>LAPORAN MASTER STOCK PER LANTAI</h1>";
            header += "<h4>Lokasi : " + lokasi.SelectedItem.Text;
            header += "<br>Project : " + project.SelectedItem.Text + "</h4>";
            header += "Laporan dihasilkan pada hari " + Cf.IndoWeek(DateTime.Today);
            header += ", " + Cf.Date(DateTime.Now) + " dari workstation " + Act.IP + " oleh user " + Act.UserID + "<br /><br />";
            headJudul.Text = header;
        }

        private void Header()
        {
            System.Text.StringBuilder x = new System.Text.StringBuilder();

            Rpt.Judul(x, comp, judul);

            Rpt.SubJudul(x
                , "Lokasi : " + lokasi.SelectedItem.Text
                );

            Rpt.Header(rpt, x);
        }

        private void Fill()
        {
            string Lokasi = "";
            if (lokasi.SelectedValue == "SEMUA")
            {
                Lokasi = " AND a.Lokasi IN (SELECT Lokasi FROM REF_LOKASI WHERE Project IN (" + Act.ProjectListSql + "))";
            }
            else
            {
                Lokasi = " AND a.Lokasi = '" + Cf.Str(lokasi.SelectedValue) + "'";
            }

            string Project = " AND a.Project IN (" + Act.ProjectListSql + ")";
            if (project.SelectedValue != "SEMUA") Project = " AND a.Project = '" + project.SelectedValue + "'";

            string strSql = "SELECT *"
                + " FROM MS_UNIT a"
                + " WHERE 1 = 1"
                + Project
                + Lokasi
                + " ORDER BY NoUnit"
                ;
            DataTable rs = Db.Rs(strSql);

            DataTable lantai = Db.Rs("SELECT DISTINCT(Lantai) FROM MS_UNIT a WHERE 1 = 1 " + Project + Lokasi);

            //Show table per lantai
            int jumUnitA = 0, jumUnitB = 0, jumUnitD = 0, grandtotal = 0;
            for (int a = 0; a < lantai.Rows.Count; a++)
            {
                if (!Response.IsClientConnected)
                    break;

                TableRow r = new TableRow();
                TableCell c;

                int pembagiUnit = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM MS_UNIT a WHERE Lantai = '" + lantai.Rows[a]["Lantai"] + "' " + Lokasi);
                int jumA = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM MS_UNIT a WHERE Lantai = '" + lantai.Rows[a]["Lantai"] + "' AND Status = 'A' " + Lokasi);
                int jumB = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM MS_KONTRAK a INNER JOIN MS_UNIT b ON a.NoStock=b.NoStock WHERE a.Status = 'A' AND b.Lantai = '" + lantai.Rows[a]["Lantai"] + "' " + Lokasi);
                int jumD = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM MS_UNIT a LEFT JOIN MS_KONTRAK b ON a.NoUnit=b.NoUnit WHERE a.Lantai = '" + lantai.Rows[a]["Lantai"] + "' AND a.Status='H' AND b.NoUnit IS NULL " + Lokasi);
                r.VerticalAlign = VerticalAlign.Top;

                c = new TableCell();
                string tower = Db.SingleString("SELECT Lokasi FROM MS_UNIT a WHERE NoUnit LIKE '%" + lantai.Rows[a]["Lantai"] + "%' " + Lokasi);
                c.Text = tower;
                c.HorizontalAlign = HorizontalAlign.Center;
                r.Cells.Add(c);

                //lantai
                c = new TableCell();
                c.Text = lantai.Rows[a]["Lantai"].ToString();
                c.HorizontalAlign = HorizontalAlign.Center;
                r.Cells.Add(c);

                //project
                c = new TableCell();
                c.Text = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + rs.Rows[a]["Project"].ToString() + "'");
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                //jum Available/lantai
                c = new TableCell();
                c.Text = Convert.ToString(jumA);
                jumUnitA += jumA;
                c.HorizontalAlign = HorizontalAlign.Center;
                r.Cells.Add(c);

                //persenAvailable/lantai
                c = new TableCell();
                if (jumA > 0)
                {
                    c.Text = String.Format("{0:0.00}", (((decimal)jumA / (decimal)(jumA + jumB + jumD)) * (decimal)100)) + " %";
                }
                else
                    c.Text = "0";
                c.HorizontalAlign = HorizontalAlign.Center;
                r.Cells.Add(c);


                //jumSold/lantai

                c = new TableCell();
                //int jumB = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM MS_KONTRAK a INNER JOIN MS_UNIT b ON a.NoStock=b.NoStock WHERE a.Status = 'A' AND b.NoUnit LIKE '%"+ arCheck[a] +"/%' ");
                c.Text = Convert.ToString(jumB);
                jumUnitB += jumB;
                c.HorizontalAlign = HorizontalAlign.Center;
                r.Cells.Add(c);
                
                //persenSold/lantai
                c = new TableCell();
                if (jumB > 0)
                {
                    c.Text = String.Format("{0:0.00}", ((decimal)jumB / (decimal)(jumA + jumB + jumD)) * (decimal)100) + " %";
                }
                else
                    c.Text = "0";
                c.HorizontalAlign = HorizontalAlign.Center;
                r.Cells.Add(c);


                //jumHold/lantai
                c = new TableCell();
                //int jumD = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM MS_UNIT a LEFT JOIN MS_KONTRAK b ON a.NoUnit=b.NoUnit WHERE a.NoUnit LIKE '%"+ arCheck[a] +"/%' AND a.Status='B' AND b.NoUnit IS NULL ");
                c.Text = jumD.ToString();
                jumUnitD += jumD;
                c.HorizontalAlign = HorizontalAlign.Center;
                r.Cells.Add(c);


                //persenHold/lantai
                c = new TableCell();
                if (jumD > 0)
                {
                    c.Text = String.Format("{0:0.00}", ((decimal)jumD / (decimal)(jumA + jumB + jumD)) * (decimal)100) + " %";
                }
                else
                    c.Text = "0";
                c.HorizontalAlign = HorizontalAlign.Center;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Convert.ToString(jumA + jumB + jumD);
                grandtotal += (jumA + jumB + jumD);
                c.HorizontalAlign = HorizontalAlign.Center;
                r.Cells.Add(c);

                c = new TableCell();
                if ((jumA > 0 && jumB > 0 && jumD > 0) || (jumA > 0 || jumB > 0 || jumD > 0))
                {
                    c.Text = Cf.Num(((jumA + jumB + jumD) / (jumA + jumB + jumD)) * (decimal)100) + " %";
                }
                else
                    c.Text = "0";
                c.HorizontalAlign = HorizontalAlign.Center;
                r.Cells.Add(c);

                rpt.Rows.Add(r);

                // Grand Total
                if (a == lantai.Rows.Count - 1)
                {
                    TableRow r1 = new TableRow();
                    TableCell c1;

                    c1 = new TableCell();
                    c1.Text = "<strong>TOTAL</strong>";
                    c1.ColumnSpan = 3;
                    c1.Attributes["style"] = "border-top:1px solid black";
                    r1.Cells.Add(c1);

                    c1 = new TableCell();
                    c1.Text = "<strong>" + jumUnitA.ToString() + "</strong>";
                    c1.Attributes["style"] = "border-top:1px solid black";
                    c1.HorizontalAlign = HorizontalAlign.Center;
                    r1.Cells.Add(c1);

                    c1 = new TableCell();
                    c1.Text = "<strong>" + String.Format("{0:0.00}", ((decimal)jumUnitA / (decimal)(jumUnitA + jumUnitB + jumUnitD)) * (decimal)100) + "% </strong>";
                    c1.Attributes["style"] = "border-top:1px solid black";
                    c1.HorizontalAlign = HorizontalAlign.Center;
                    r1.Cells.Add(c1);

                    c1 = new TableCell();
                    c1.Text = "<strong>" + jumUnitB.ToString() + "</strong>";
                    c1.Attributes["style"] = "border-top:1px solid black";
                    c1.HorizontalAlign = HorizontalAlign.Center;
                    r1.Cells.Add(c1);

                    c1 = new TableCell();
                    c1.Text = "<strong>" + String.Format("{0:0.00}", ((decimal)jumUnitB / (decimal)(jumUnitA + jumUnitB + jumUnitD)) * (decimal)100) + "% </strong>";
                    c1.Attributes["style"] = "border-top:1px solid black";
                    c1.HorizontalAlign = HorizontalAlign.Center;
                    r1.Cells.Add(c1);

                    c1 = new TableCell();
                    c1.Text = "<strong>" + jumUnitD.ToString() + "</strong>";
                    c1.Attributes["style"] = "border-top:1px solid black";
                    c1.HorizontalAlign = HorizontalAlign.Center;
                    r1.Cells.Add(c1);

                    c1 = new TableCell();
                    c1.Text = "<strong>" + String.Format("{0:0.00}", ((decimal)jumUnitD / (decimal)(jumUnitA + jumUnitB + jumUnitD)) * (decimal)100) + "% </strong>";
                    c1.Attributes["style"] = "border-top:1px solid black";
                    c1.HorizontalAlign = HorizontalAlign.Center;
                    r1.Cells.Add(c1);

                    c1 = new TableCell();
                    c1.Text = "<strong>" + grandtotal.ToString() + "</strong>";
                    c1.Attributes["style"] = "border-top:1px solid black";
                    c1.HorizontalAlign = HorizontalAlign.Center;
                    r1.Cells.Add(c1);

                    c1 = new TableCell();
                    c1.Text = "<strong>" + String.Format("{0:0.00}", ((decimal)(jumUnitA + jumUnitB + jumUnitD) / (decimal)(jumUnitA + jumUnitB + jumUnitD)) * (decimal)100) + "% </strong>";
                    c1.Attributes["style"] = "border-top:1px solid black";
                    c1.HorizontalAlign = HorizontalAlign.Center;
                    r1.Cells.Add(c1);

                    rpt.Rows.Add(r1);
                }
            }
        }
        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            lokasi.Items.Clear();
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

    }
}

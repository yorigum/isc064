using System;
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

namespace ISC064.ADMINJUAL.Laporan
{
    public partial class MasterAgent : System.Web.UI.Page
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

        private void init()
        {
            DataTable rs;
            string Project = project.SelectedIndex == 0 ? "Project IN (" + Act.ProjectListSql + ")" : "Project = '" + project.SelectedValue + "'";

            rs = Db.Rs("SELECT DISTINCT YEAR(TglInput), MONTH(TglInput) FROM MS_AGENT "
                + " ORDER BY YEAR(TglInput), MONTH(TglInput)");
            for (int i = 0; i < rs.Rows.Count; i++)
                input.Items.Add(new ListItem(
                    Cf.Monthname((int)rs.Rows[i][1]) + " " + rs.Rows[i][0].ToString()
                    , rs.Rows[i][0] + "," + rs.Rows[i][1]
                    ));


            rs = Db.Rs("SELECT * FROM REF_AGENT_LEVEL WHERE " + Project + " ORDER BY LevelID");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["LevelID"].ToString();
                string t = v + " - " + rs.Rows[i]["Nama"].ToString();
                principal.Items.Add(new ListItem(t, v));
                principal.Items[i].Selected = true;
            }

            rs = Db.Rs("SELECT * FROM REF_AGENT_TIPE WHERE " + Project + " ORDER BY ID");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["ID"].ToString();
                string t = v + " - " + rs.Rows[i]["Tipe"].ToString();
                tipe.Items.Add(new ListItem(t, v));
                tipe.Items[i].Selected = true;
            }

            //rs = Db.Rs("SELECT DISTINCT SalesTipe FROM MS_AGENT ORDER BY SalesTipe");
            //for (int i = 0; i < rs.Rows.Count; i++)
            //    tipe.Items.Add(new ListItem(rs.Rows[i][0].ToString()));

            tipe.SelectedIndex = 0;
            principal.SelectedIndex = 0;
            input.SelectedIndex = 0;
        }

        private bool valid()
        {
            string s = "";
            bool x = true;

            if (!Cf.isPilih(nama))
            {
                x = false;
                namac.Text = "Pilih Minimum Satu";
            }
            else
                namac.Text = "";

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
                bool excel = false;
                Report(excel);
            }
        }
        protected void xls_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                bool excel = true;
                Report(excel);
                //Rpt.ToExcel(this,rpt);
                Rpt.ToExcel(this, headReport, rpt);
            }
        }
        protected void pdf_Click(object sender, System.EventArgs e)
        {

            Process p = new System.Diagnostics.Process();

            string Nama = "Laporan Master Agent";
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
                    //+ ",'" + Cf.Date(dari.Text) + "'"
                    //+ ",'" + Cf.Date(sampai.Text) + "'"
                    );

            //get nomor customer terbaru
            int NoAttachment = Db.SingleInteger(
                "SELECT TOP 1 AttachmentID FROM LapPDF ORDER BY AttachmentID DESC");

            string strSql = "SELECT * FROM ISC064_MARKETINGJUAL..LapPDF WHERE AttachmentID  = '" + NoAttachment + "'";
            DataTable rs = Db.Rs(strSql);

            string nfilename = "LapMasterAgent" + NoAttachment + ".pdf";

            //update filename
            Db.Execute("UPDATE ISC064_MARKETINGJUAL..LapPDF SET FileName= '" + nfilename + "' WHERE AttachmentID = " + NoAttachment);


            //folder untuk menyimpan file pdf
            string save = Mi.PathFilePDFReport + "LapMasterAgent" + rs.Rows[0]["AttachmentID"] + ".pdf";

            //declare parameter
            string Principal = principal.SelectedValue;

            string Input2 = "";
            if (input.SelectedIndex != 0)
            {
                string[] z = input.SelectedValue.Split(',');
                Input2 = z[0] + "-" + z[1];
            }
            else
                Input2 = input.SelectedValue;

            //declare parameter
            string Tipe = tipe.SelectedValue;
            string Input = "";
            if (input.SelectedIndex != 0)
            {
                string[] z = input.SelectedValue.Split(',');
                Input = z[0] + "-" + z[1];
            }
            else
                Input = input.SelectedValue;

            string nStatusS = "";
            string nStatusA = "";
            string nStatusI = "";
            if (statusS.Checked == true)
                nStatusS = statusS.Text;
            else
                nStatusS = "";
            if (statusA.Checked == true)
                nStatusA = statusA.Text;
            else
                nStatusA = "";
            if (statusI.Checked == true)
                nStatusI = statusI.Text;
            else
                nStatusI = "";

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
                foreach (ListItem item in nama.Items)
                {
                    if (item.Selected == true)
                    {
                        nm += item.Value + '-';
                    }
                }
            }
            catch (Exception)
            {
            }
            //link untuk download pdf
            string link = Mi.PathAlamatWeb + "adminjual/LaporanPDF/PDFMasterAgent.aspx?id=" + rs.Rows[0]["AttachmentID"] + "&nama=" + nm + "&input=" + Input + "&status_s=" + nStatusS + "&status_i=" + nStatusI + "&status_a=" + nStatusA + "&principal=" + Principal + "&tipe=" + Tipe + "&project=" + Project + "";

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
        private void Report(bool excel)
        {
            param.Visible = false;
            rpt.Visible = true;

            FillHeader();
            Fill(excel);
        }

        private void FillHeader()
        {
            System.Text.StringBuilder x = new System.Text.StringBuilder();

            Rpt.Judul(x, comp, judul);

            string status = "";
            if (statusS.Checked) status = statusS.Text;
            if (statusA.Checked) status = statusA.Text;
            if (statusI.Checked) status = statusI.Text;

            Rpt.SubJudul(x
                , "Status : " + status
                );

            Rpt.SubJudul(x
                , "Nama : " + Rpt.inSql(nama).Replace("'", "")
                );

            Rpt.SubJudul(x
                , "Periode Input : " + input.SelectedItem.Text
                );

            Rpt.SubJudul(x
                , "Principal : " + principal.SelectedItem.Text
                );

            Rpt.SubJudul(x
                , "Tipe : " + tipe.SelectedItem.Text
                );

            Rpt.SubJudul(x
                , "Project : " + project.SelectedItem.Text
                );

            string legend = "";
            //Rpt.Header(rpt, x);
            Rpt.HeaderReport(headReport, legend, x);
        }

        private void Fill(bool excel)
        {
            string Status = "";
            if (statusA.Checked) Status = " AND A.Status = 'A'";
            if (statusI.Checked) Status = " AND A.Status = 'I'";

            string aq = "";
            if (nama.Items[0].Selected)
                aq = aq + " LEFT(A.Nama,1) IN ('a','b','c','d') OR ";
            if (nama.Items[1].Selected)
                aq = aq + " LEFT(A.Nama,1) IN ('e','f','g','h') OR ";
            if (nama.Items[2].Selected)
                aq = aq + " LEFT(A.Nama,1) IN ('i','j','k','l') OR ";
            if (nama.Items[3].Selected)
                aq = aq + " LEFT(A.Nama,1) IN ('m','n','o','p') OR ";
            if (nama.Items[4].Selected)
                aq = aq + " LEFT(A.Nama,1) IN ('q','r','s','t') OR ";
            if (nama.Items[5].Selected)
                aq = aq + " LEFT(A.Nama,1) IN ('u','v','w','x') OR ";
            if (nama.Items[6].Selected)
                aq = aq + " LEFT(A.Nama,1) IN ('y','z','0','1','2','3','4','5','6','7','8','9') OR ";
            if (aq != "")
                aq = " AND (" + aq.Substring(0, aq.Length - 3) + ")";

            string Input = "";
            if (input.SelectedIndex != 0)
            {
                string[] z = input.SelectedValue.Split(',');
                Input = " AND YEAR(A.TglInput) = " + z[0]
                    + " AND MONTH(A.TglInput) = " + z[1];
            }

            string Principal = "";
            if (principal.SelectedIndex != 0)
            {
                Principal = " AND A.SalesLevel = '" + Cf.Str(principal.SelectedValue) + "'";
            }

            string Tipe = "";
            if (tipe.SelectedIndex != 0)
            {
                Tipe = " AND A.SalesTipe = '" + Cf.Str(tipe.SelectedValue) + "'";
            }

            string Project = " AND A.Project IN (" + Act.ProjectListSql + ")";
            if (project.SelectedValue != "SEMUA") Project = " AND A.Project = '" + project.SelectedValue + "'";

            string strSql = "SELECT "
                + " A.NoAgent"
                //+ ",A.Nama"
                + ",A.KodeSales AS [Kode Sales]"
                + ",A.Nama AS [Nama Lengkap]"
                + ",A.SalesTipe"
                + ",A.SalesLevel"
                + ",A.Atasan"
                + ",A.Alamat"
                + ",A.Kontak"
                + ",A.Handphone"
                + ",A.Whatsapp"
                + ",A.NPWP"
                + ",A.Email"
                + ",A.RekBank"
                + ",A.Rekening"
                + ",A.AtasNama"
                + ",A.TglInput"
                + ",A.Status"
                + ",A.Principal"
                + ",A.Status"
                + ",A.TglEdit"
                //+ ",Skema0"
                + ",B.Tipe AS [Tipe Agent]"
                + ",C.Nama AS [Nama Agent]"
                + " FROM MS_AGENT A INNER JOIN REF_AGENT_TIPE B ON A.SalesTipe = B.ID"
                + " INNER JOIN REF_AGENT_LEVEL C ON A.SalesLevel = C.LevelID"
                + " WHERE 1=1 "
                + Project
                + Status
                + aq
                + Input
                + Principal
                + Tipe
                + " ORDER BY A.Nama, A.NoAgent";

            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                r.VerticalAlign = VerticalAlign.Top;
                r.Attributes["ondblclick"] = "popEditAgent('" + rs.Rows[i]["NoAgent"] + "')";

                //string inaktif = "";
                //if (rs.Rows[i]["Status"].ToString() == "I")
                //    inaktif = " (Inaktif)";

                c = new TableCell();
                c.Text = (i + 1).ToString();
                c.HorizontalAlign = HorizontalAlign.Center;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Tipe Agent"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Nama Agent"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Db.SingleString("SELECT Nama FROM MS_AGENT WHERE NoAgent = '" + rs.Rows[i]["Atasan"].ToString() + "'");
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                if (rs.Rows[i]["Kode Sales"].ToString() == "0" || rs.Rows[i]["Kode Sales"].ToString() == "")
                {
                    c.Text = "";
                }
                else
                {
                    c.Text = rs.Rows[i]["Kode Sales"].ToString().PadLeft(5, '0');
                }
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Nama Lengkap"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Status"].ToString() == "A" ? "Aktif" : "Inaktif";
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text  = rs.Rows[i]["Status"].ToString() == "I" ? Cf.Day(rs.Rows[i]["TglEdit"]):"-";
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);


                c = new TableCell();
                c.Text = rs.Rows[i]["Alamat"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Email"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = excel ? "'" + rs.Rows[i]["Kontak"].ToString() : rs.Rows[i]["Kontak"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = excel ? "'" + rs.Rows[i]["Handphone"].ToString() : rs.Rows[i]["Handphone"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = excel ? "'" + rs.Rows[i]["Whatsapp"].ToString() : rs.Rows[i]["Whatsapp"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = excel ? "'" + rs.Rows[i]["NPWP"].ToString() : rs.Rows[i]["NPWP"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Rekening"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["RekBank"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["AtasNama"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                rpt.Rows.Add(r);
            }
        }

        protected void namaCheck_CheckedChanged(object sender, System.EventArgs e)
        {
            for (int i = 0; i < nama.Items.Count; i++)
            {
                nama.Items[i].Selected = namaCheck.Checked;
            }

            Js.Focus(this, namaCheck);
            namac.Text = "";
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
            tipe.Items.Clear();
            principal.Items.Clear();
            tipe.Items.Add("SEMUA");
            principal.Items.Add("SEMUA");
            input.Items.Clear();
            init();
        }
    }
}

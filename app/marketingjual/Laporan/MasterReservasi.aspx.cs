using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Diagnostics;

namespace ISC064.MARKETINGJUAL.Laporan
{
    public partial class MasterReservasi : System.Web.UI.Page
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
            dari.Text = Cf.Day(Cf.AwalBulan());
            sampai.Text = Cf.Day(Cf.AkhirBulan());

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

            //rs = Db.Rs("SELECT DISTINCT Lokasi FROM MS_RESERVASI WHERE " + Project + " ORDER BY Lokasi");
            rs = Db.Rs("SELECT * FROM REF_LOKASI WHERE " + Project + " ORDER BY Lokasi");
            for (int i = 0; i < rs.Rows.Count; i++)
                lokasi.Items.Add(new ListItem(rs.Rows[i][0].ToString()));

            rs = Db.Rs("SELECT DISTINCT Principal FROM MS_AGENT WHERE Status = 'A' ORDER BY Principal");
            for (int i = 0; i < rs.Rows.Count; i++)
                agent.Items.Add(new ListItem(rs.Rows[i][0].ToString()));

            lokasi.SelectedIndex = 0;
            agent.SelectedIndex = 0;
            jenis.SelectedIndex = 0;
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
                    , "<script language='javascript'>document.getElementById('" + s + "').select()</script>");
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

            string Nama = "Laporan Master Reservasi";
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

            string nfilename = "MasterReservasi" + NoAttachment + ".pdf";

            //update filename
            Db.Execute("UPDATE ISC064_MARKETINGJUAL..LapPDF SET FileName= '" + nfilename + "' WHERE AttachmentID = " + NoAttachment);


            //folder untuk menyimpan file pdf
            string save = Mi.PathFilePDFReport + "MasterReservasi" + rs.Rows[0]["AttachmentID"] + ".pdf";

            //declare parameter
            string Lokasi = lokasi.SelectedValue.Replace(" ", "%");
            string nStatusS = "";
            string nStatusA = "";
            string nStatusE = "";
            string nNoUrut = "";

            if (statusS.Checked == true)
                nStatusS = statusS.Text;
            else
                nStatusS = "";
            if (statusA.Checked == true)
                nStatusA = statusA.Text;
            else
                nStatusA = "";
            if (statusE.Checked == true)
                nStatusE = statusE.Text;
            else
                nStatusE = "";

            string Agent = agent.SelectedValue;

            
            if (toponly.Checked)
                nNoUrut = toponly.Text;

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
            string link = Mi.PathAlamatWeb + "marketingjual/LaporanPDF/PDFMasterReservasi.aspx?id=" + rs.Rows[0]["AttachmentID"]
                + "&status_s=" + nStatusS
                + "&status_b=" + nStatusE
                + "&status_a=" + nStatusA
                + "&agent=" + agent.SelectedValue
                + "&lokasi=" + lokasi.SelectedValue
                + "&tipe=" + nm //jenis
                + "&userid=" + UserID
                + "&project=" + Project
                + "&nourut=" + nNoUrut
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
            else if (statusE.Checked)
                Rpt.SubJudul(x, "Status : " + statusE.Text);
            else
                Rpt.SubJudul(x, "Status : " + statusS.Text);

            string tgl = "";
            if (tglreservasi.Checked) tgl = tglreservasi.Text;
            if (tglbatas.Checked) tgl = tglbatas.Text;

            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);
            Rpt.SubJudul(x
                , tgl + " : " + Cf.Day(dari.Text) + " s/d " + Cf.Day(sampai.Text)
                );

            Rpt.SubJudul(x
                , "Jenis : " + Rpt.inSql(jenis).Replace("'", "")
                );

            Rpt.SubJudul(x
                , "Lokasi : " + lokasi.SelectedItem.Text
                );

            Rpt.SubJudul(x
                , "Sales : " + agent.SelectedItem.Text
                );

            Rpt.SubJudul(
                x, "Perusahaan : " + pers.SelectedItem.Text
                );

            Rpt.SubJudul(
                x, "Project : " + project.SelectedItem.Text
                );

            //Rpt.Header(rpt, x);
            Rpt.HeaderReport(headReport, "", x);
        }

        private void Fill()
        {
            string Status = "";
            if (statusA.Checked) Status = " AND MS_RESERVASI.Status = 'A'";
            if (statusE.Checked) Status = " AND MS_RESERVASI.Status = 'E'";

            string tgl = "";
            string order = "";
            if (tglreservasi.Checked)
            {
                tgl = "Tgl";
                order = "NoReservasi";
            }
            if (tglbatas.Checked)
            {
                tgl = "TglExpire";
                order = "TglExpire,NoReservasi";
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

            string Agent = "";
            if (agent.SelectedIndex != 0)
            {
                Agent = " AND Principal = '" + Cf.Str(agent.SelectedValue) + "'";
            }

            string NoUrut = "";
            if (toponly.Checked)
            {
                NoUrut = " AND NoUrut = 1";
            }

            string Project = " AND MS_KONTRAK.Project IN (" + Act.ProjectListSql + ")";
            if (project.SelectedValue != "SEMUA") Project = " AND MS_KONTRAK.Project = '" + project.SelectedValue + "'";

            string Perusahaan = "";
            if (pers.SelectedValue != "SEMUA") Perusahaan = " AND MS_KONTRAK.Pers = '" + pers.SelectedValue + "'";

            decimal t1 = 0;

            string strSql = "SELECT"
                + " NoReservasi"
                + ",NoUrut"
                + ",Tgl"
                + ",TglExpire"
                + ",MS_RESERVASI.Status"
                + ",MS_RESERVASI.NoUnit"
                + ",MS_CUSTOMER.Nama AS Cs"
                + ",MS_AGENT.Nama AS Ag"
                + ",MS_AGENT.Principal"
                + ",MS_RESERVASI.Skema"
                + ",MS_KONTRAK.Project"
                + ",MS_KONTRAK.Pers"
                + ",NilaiReservasi"
                + ",NoQueue"
                + " FROM MS_RESERVASI INNER JOIN MS_CUSTOMER ON MS_RESERVASI.NoCustomer = MS_CUSTOMER.NoCustomer"
                + " INNER JOIN MS_AGENT ON MS_RESERVASI.NoAgent = MS_AGENT.NoAgent"
                + " INNER JOIN MS_KONTRAK ON MS_RESERVASI.NoStock = MS_KONTRAK.NoStock"
                + " WHERE 1=1"
                + " AND CONVERT(varchar," + tgl + ",112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND CONVERT(varchar," + tgl + ",112) <= '" + Cf.Tgl112(Sampai) + "'"
                + " AND MS_RESERVASI.Jenis IN (" + Rpt.inSql(jenis) + ")"
                + Project
                + Perusahaan
                + Status
                + Lokasi
                + Agent
                + NoUrut
                + " ORDER BY " + order;

            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                r.VerticalAlign = VerticalAlign.Top;
                r.Attributes["ondblclick"] = "popEditReservasi('" + rs.Rows[i]["NoReservasi"] + "')";

                c = new TableCell();
                c.Text = rs.Rows[i]["NoReservasi"].ToString().PadLeft(5, '0');
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Status"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["Tgl"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Cs"].ToString();
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
                c.Text = rs.Rows[i]["NoUnit"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoUrut"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Date(rs.Rows[i]["TglExpire"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoQueue"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Skema"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["NilaiReservasi"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                rpt.Rows.Add(r);

                t1 = t1 + (decimal)rs.Rows[i]["NilaiReservasi"];

                if (i == rs.Rows.Count - 1)
                    SubTotal("GRAND TOTAL", t1);
            }
        }

        private void SubTotal(string txt, decimal t1)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = Rpt.Foot();
            c.Text = txt;
            c.ColumnSpan = 11;
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t1);
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

        void BindProject()
        {
            project.Items.Clear();
            //Act.ProjectList(project);
            DataTable rs = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Pers='" + pers.SelectedValue + "'");
            project.Items.Add(new ListItem { Text = "Project :", Value = "0" });

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["Project"].ToString();
                string t = rs.Rows[i]["Nama"].ToString();
                project.Items.Add(new ListItem(t, v));
            }
        }

        protected void pers_SelectedIndexChanged(object sender, EventArgs e)
        {
            //BindProject();
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
            jenis.Items.Clear();
            jenis.Items.Add("SEMUA");
            lokasi.Items.Clear();
            lokasi.Items.Add("SEMUA");
            agent.Items.Clear();
            agent.Items.Add("SEMUA");
            init();
        }

        //protected void project_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //if (level.SelectedIndex == 0) //VALIDASI 2 SPV DIDALAM 1 TIPE
        //    //{
        //    //    trAtasan.Visible = false;
        //    //}
        //    //else if (level.SelectedIndex == 1)
        //    //{
        //    //    trAtasan.Visible = false;
        //    //    //BindAtasan();
        //    //    //trAtasanM.Visible = true;
        //    //}
        //    //else
        //    //{
        //    //    trAtasan.Visible = true;
        //    //    BindAtasan();
        //    //}
        //    ////BindGrade();
        //}

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

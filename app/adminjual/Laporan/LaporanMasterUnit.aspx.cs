using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Diagnostics;

namespace ISC064.ADMINJUAL.Laporan
{
    public partial class LaporanMasterUnit : System.Web.UI.Page
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
            {
                string v = rs.Rows[i]["Lokasi"].ToString();
                string t = v + " - " + Db.SingleString("SELECT Nama FROM REF_LOKASI WHERE Lokasi='" + rs.Rows[i]["Lokasi"].ToString() + "'");
                lokasi.Items.Add(new ListItem(t, v));
            }
            lokasi.SelectedIndex = 0;
        }

        private bool valid()
        {
            string s = "";
            bool x = true;

            if (!Cf.isPilih(jenis))
            {
                x = false;
                jenisc.Text = " Choose One";
            }
            else
                jenisc.Text = "";

            if (!x && s != "")
            {
                ClientScript.RegisterStartupScript(GetType(), "err"
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
                Rpt.ToExcel(this, headReport, rpt);
            }
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
            System.Text.StringBuilder x = new System.Text.StringBuilder();

            Rpt.Judul(x, comp, judul);

            if (statusA.Checked)
                Rpt.SubJudul(x, "Status : AVAILABLE");
            else if (statusB.Checked)
                Rpt.SubJudul(x, "Status : SOLD");
            else if (statusH.Checked)
                Rpt.SubJudul(x, "Status : HOLD");
            else
                Rpt.SubJudul(x, "Status : SEMUA");

            //Rpt.SubJudul(x
            //    , "Periode : " + periode.SelectedItem.Text
            //    );

            Rpt.SubJudul(x
                , "Tipe : " + Rpt.inSql(jenis).Replace("'", "")
                );

            Rpt.SubJudul(x
                , "Lokasi : " + lokasi.SelectedItem.Text
                );

            Rpt.SubJudul(x
                , "Project : " + project.SelectedValue
                );

            Rpt.SubJudul(x
                , "Perusahaan : " + pers.SelectedValue
                );
            string legend = "Status: A = Available / S = Sold / H = Hold."
                            + "<br />"
                            + "Luas dalam meter persegi.Harga price list adalah dalam rupiah.";
            //Rpt.Header(rpt, x);
            Rpt.HeaderReport(headReport, legend, x);
        }

        private void Fill()
        {
            string Status = "";
            if (statusA.Checked) Status = " AND Status = 'A'";
            if (statusB.Checked) Status = " AND Status = 'B'";
            if (statusH.Checked) Status = " AND Status = 'H'";

            //string Periode = "";
            //if(periode.SelectedIndex!=0)
            //{
            //    string[] z = periode.SelectedValue.Split(',');
            //    Periode = " AND YEAR(TglInput) = " + z[0]
            //        + " AND MONTH(TglInput) = " + z[1];
            //}

            string Lokasi = "";
            if (lokasi.SelectedIndex != 0)
            {
                Lokasi = " AND MS_UNIT.Lokasi = '" + Cf.Str(lokasi.SelectedValue) + "'";
            }
            string Project = " AND MS_UNIT.Project IN(" + Act.ProjectListSql + ")";
            if (project.SelectedValue != "SEMUA")
            {
                Project = " AND MS_UNIT.Project = '" + Cf.Str(project.SelectedValue) + "'";
            }
            string Perusahaan = "";
            if (pers.SelectedValue != "SEMUA")
            {
                Perusahaan = " AND MS_KONTRAK.Pers = '" + Cf.Str(pers.SelectedValue) + "'";
            }

            decimal t1 = 0;

            string strSql = "SELECT "
                + "	NoStock"
                + ",Jenis"
                + ",Lokasi"
                + ",NoUnit"
                + ",Luas"
                + ",PriceList"
                + ",PriceListMin"
                + ",TglInput"
                + ",Status"
                + ",Panorama"
                + ",LuasSG"
                + ",LuasNett"
                + ",JenisProperti"
                + ",HadapAtrium"
                + ",HadapEntrance"
                + ",HadapEskalator"
                + ",HadapLift"
                + ",HadapParkir"
                + ",HadapAxis"
                + " FROM MS_UNIT"
                + " WHERE Jenis IN (" + Rpt.inSql(jenis) + ")"
                + Project
                + Status
                + Lokasi
                //+ Periode
                + " ORDER BY NoStock";

            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                r.VerticalAlign = VerticalAlign.Top;
                r.Attributes["ondblclick"] = "popEditUnit('" + rs.Rows[i]["NoStock"] + "')";

                DateTime p = Convert.ToDateTime(rs.Rows[i]["TglInput"]);

                c = new TableCell();
                c.Text = (i + 1).ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoStock"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Jenis"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Db.SingleString("SELECT Nama FROM REF_LOKASI WHERE Lokasi = '" + rs.Rows[i]["Lokasi"].ToString() + "'");
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["JenisProperti"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoUnit"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                decimal luassg = Convert.ToDecimal(rs.Rows[i]["LuasSG"]);
                c.Text = Math.Round(luassg, 2).ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                decimal luasnet = Convert.ToDecimal(rs.Rows[i]["LuasNett"]);
                c.Text = Math.Round(luasnet, 2).ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                if (rs.Rows[i]["HadapAtrium"].ToString() == "1")
                {
                    c.Text = "Hadap Atrium";
                }
                else if (rs.Rows[i]["HadapEntrance"].ToString() == "1")
                {
                    c.Text = "Hadap Eskalator";
                }
                else if (rs.Rows[i]["HadapEskalator"].ToString() == "1")
                {
                    c.Text = "Hadap Eskalator";
                }

                else if (rs.Rows[i]["HadapLift"].ToString() == "1")
                {
                    c.Text = "Hadap Lift";
                }
                else if (rs.Rows[i]["HadapParkir"].ToString() == "1")
                {
                    c.Text = "Hadap Parkir";
                }
                else if (rs.Rows[i]["HadapAxis"].ToString() == "1")
                {
                    c.Text = "Hadap Axis";
                }
                else
                {
                    c.Text = "";
                }
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Panorama"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                if (rs.Rows[i]["Status"].ToString() == "B")
                {
                    c.Text = "S";
                }
                else if (rs.Rows[i]["Status"].ToString() == "A")
                {
                    c.Text = "A";
                }
                else
                {
                    c.Text = "H";
                }
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Db.SingleString("SELECT NoKontrak FROM MS_KONTRAK WHERE NoStock = '" + rs.Rows[i]["NoStock"].ToString() + "'" + Perusahaan + " AND Status = 'A'");
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                rpt.Rows.Add(r);

                t1 = t1 + (decimal)rs.Rows[i]["PriceList"];

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
            c.ColumnSpan = 12;
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            //c = Rpt.Foot();
            //c.Text = Cf.Num(t1);
            //c.HorizontalAlign = HorizontalAlign.Right;
            //r.Cells.Add(c);

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
        protected void pdf_Click(object sender, EventArgs e)
        {
            Process p = new System.Diagnostics.Process();

            string Nama = "Laporan Master Unit";
            string Link = "";
            DateTime TglGenerate = DateTime.Now;
            string FileName = "";
            string FileType = "application/pdf";
            string UserID = Act.UserID;
            string IP = Act.IP;

            Db.Execute("EXEC ISC064_MARKETINGJUAL..spLapPDFDaftar"

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
                "SELECT TOP 1 AttachmentID FROM ISC064_MARKETINGJUAL..LapPDF ORDER BY AttachmentID DESC");

            string strSql = "SELECT * FROM ISC064_MARKETINGJUAL..LapPDF WHERE AttachmentID  = '" + NoAttachment + "'";
            DataTable rs = Db.Rs(strSql);

            string nfilename = "MasterUnit" + NoAttachment + ".pdf";

            //update filename
            Db.Execute("UPDATE ISC064_MARKETINGJUAL..LapPDF SET FileName= '" + nfilename + "' WHERE AttachmentID = " + NoAttachment);


            //folder untuk menyimpan file pdf
            string save = Mi.PathFilePDFReport + "MasterUnit" + rs.Rows[0]["AttachmentID"] + ".pdf";

            //parameter

            string nStatusS = "";
            string nStatusA = "";
            string nStatusB = "";
            string nStatusH = "";
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
            if (statusH.Checked == true)
                nStatusH = statusH.Text;
            else
                nStatusH = "";

            string Project = "";
            if (project.SelectedValue == "SEMUA")
            {
                Project = Act.ProjectListSql.Replace("'", "");
            }
            else
            {
                Project = project.SelectedValue;
            }

            string jenis2 = string.Empty;
            string jenis3 = string.Empty;
            try
            {
                foreach (ListItem item in jenis.Items)
                {
                    if (item.Selected == true)
                    {
                        if (jenis.Items.IndexOf(item) == jenis.Items.Count - 1)
                        {
                            jenis2 += item.Value;
                        }
                        else
                            jenis2 += item.Value + '-';

                    }
                }
            }
            catch (Exception)
            {
            }

            //link untuk download pdf
            string link = Mi.PathAlamatWeb + "adminjual/LaporanPDF/PDFMasterUnit.aspx?id=" + rs.Rows[0]["AttachmentID"]
                + "&status_a=" + nStatusA
                + "&status_b=" + nStatusB
                + "&status_s=" + nStatusS
                + "&status_h=" + nStatusH
                + "&project=" + Project
                + "&pers=" + pers.SelectedValue
                + "&lokasi=" + lokasi.SelectedValue
                + "&jenis=" + jenis2.Replace(' ', '.')
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

        protected void project_SelectedIndexChanged1(object sender, EventArgs e)
        {
            jenis.Items.Clear();
            lokasi.Items.Clear();
            lokasi.Items.Add("SEMUA");
            init();
        }
    }
}

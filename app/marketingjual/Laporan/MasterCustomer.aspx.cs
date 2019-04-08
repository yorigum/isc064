using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;

namespace ISC064.MARKETINGJUAL.Laporan
{
    public partial class MasterCustomer : System.Web.UI.Page
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

            Cf.BindBulan(lahir);

            rs = Db.Rs("SELECT DISTINCT YEAR(TglInput), MONTH(TglInput) FROM MS_CUSTOMER "
                + " ORDER BY YEAR(TglInput), MONTH(TglInput)");
            for (int i = 0; i < rs.Rows.Count; i++)
                input.Items.Add(new ListItem(
                    Cf.Monthname((int)rs.Rows[i][1]) + " " + rs.Rows[i][0].ToString()
                    , rs.Rows[i][0] + "," + rs.Rows[i][1]
                    ));

            rs = Db.Rs("SELECT DISTINCT AgentInput FROM MS_CUSTOMER "
                + " ORDER BY AgentInput");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i][0].ToString();
                string t = v;

                string NamaAgent = Db.SingleString(
                    "SELECT ISNULL((SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..USERNAME WHERE UserID = '" + v + "'),'')");
                if (NamaAgent != "")
                    t = t + " (" + NamaAgent + ")";

                agentinput.Items.Add(new ListItem(t, v));
            }

            lahir.SelectedIndex = 0;
            input.SelectedIndex = 0;
            agentinput.SelectedIndex = 0;
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

            //if (!Cf.isPilih(agama))
            //{
            //    x = false;
            //    agamac.Text = "Pilih Minimum Satu";
            //}
            //else
            //    agamac.Text = "";

            if (!Cf.isPilih(sumberdata))
            {
                x = false;
                sumberdatac.Text = "Pilih Minimum Satu";
            }
            else
                sumberdatac.Text = "";

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
                Report(false);
            }
        }
        protected void xls_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                Report(true);
                Rpt.ToExcel(this, headReport, rpt);
            }
        }
        protected void pdf_Click(object sender, System.EventArgs e)
        {

            Process p = new System.Diagnostics.Process();

            string Nama = "Laporan Master Customer";
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

            string nfilename = "MasterCustomer" + NoAttachment + ".pdf";

            //update filename
            Db.Execute("UPDATE ISC064_MARKETINGJUAL..LapPDF SET FileName= '" + nfilename + "' WHERE AttachmentID = " + NoAttachment);


            //folder untuk menyimpan file pdf
            string save = Mi.PathFilePDFReport + "MasterCustomer" + rs.Rows[0]["AttachmentID"] + ".pdf";

            //declare parameter

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

            string Agent = agentinput.SelectedValue;

            string Sifat = String.Empty;

            if (sifatALL.Checked)
                Sifat = "sifatALL";
            else if (sifatBELUM.Checked)
                Sifat = "sifatBELUM";
            else if (sifatSUDAH.Checked)
                Sifat = "sifatSUDAH";


            string nm = string.Empty;
            string agm = string.Empty;
            string smb = string.Empty;
            try
            {
                foreach (ListItem item in nama.Items)
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

            //Agama
            try
            {
                foreach (ListItem item in agama.Items)
                {
                    if (item.Selected == true)
                    {
                        agm += item.Value.Replace(" ", "+") + "-";
                    }
                }
            }
            catch (Exception)
            {
            }

            //Sumber Data
            try
            {
                foreach (ListItem item in sumberdata.Items)
                {
                    if (item.Selected == true)
                    {
                        smb += item.Value.Replace(" ", "+") + "-";
                    }
                }
            }
            catch (Exception)
            {
            }

            string Input = "";
            if (input.SelectedIndex != 0)
            {
                string[] z = input.SelectedValue.Split(',');
                Input = z[0] + "-" + z[1];
            }
            else
                Input = input.SelectedValue;

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
            string link = Mi.PathAlamatWeb + "marketingjual/LaporanPDF/PDFMasterCustomer.aspx?id=" + rs.Rows[0]["AttachmentID"]
                + "&status_s=" + nStatusS
                + "&status_i=" + nStatusI
                + "&status_a=" + nStatusA
                + "&agentinput=" + Agent
                + "&sifat=" + Sifat
                + "&nama=" + nm
                + "&agama=" + agm
                + "&sumberdata=" + smb
                + "&input=" + Input
                + "&blnlahir=" + lahir.SelectedValue
                + "&userid=" + UserID
                + "&project=" + Project

                ;

            //update link
            Db.Execute("UPDATE ISC064_MARKETINGJUAL..LapPDF SET Link= '" + link + "' WHERE AttachmentID = " + NoAttachment);

            //format page
            p.StartInfo.Arguments = "--orientation landscape --page-width 16in --page-height 30in --margin-left 0 --margin-right 0 --margin-top 0.25cm --margin-bottom 0 " + link + " " + save;

            //panggil aplikasi untuk mengconvert pdf
            p.StartInfo.FileName = Mi.PathWkhtmlPDFReport;
            p.Start();

            //60000 -> waktu jeda lama convert pdf
            p.WaitForExit(30000);

            string Src = Mi.PathFilePDFReport + nfilename;
            Mi.DownloadPDF(this, Src, (rs.Rows[0]["FileName"]).ToString(), rs.Rows[0]["FileType"].ToString());
        }

        private void Report(bool Excel)
        {
            param.Visible = false;
            rpt.Visible = true;

            Header();
            Fill(Excel);
        }

        private void Header()
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

            string sifat = "";
            if (sifatALL.Checked) sifat = sifatALL.Text;
            if (sifatSUDAH.Checked) sifat = sifatSUDAH.Text;
            if (sifatBELUM.Checked) sifat = sifatBELUM.Text;

            Rpt.SubJudul(x
                , "Sifat : " + sifat
                );

            Rpt.SubJudul(x
                , "Nama : " + Rpt.inSql(nama).Replace("'", "")
                );

            Rpt.SubJudul(x
                , "Agama : " + Rpt.inSql(agama).Replace("'", "")
                );

            Rpt.SubJudul(x
                , "Sumber Data : " + Rpt.inSql(sumberdata).Replace("'", "")
                );

            Rpt.SubJudul(x
                , "Periode Input : " + input.SelectedItem.Text
                );

            Rpt.SubJudul(x
                , "Bulan Lahir : " + lahir.SelectedItem.Text
                );

            Rpt.SubJudul(x
                , "Sales Account : " + agentinput.SelectedItem.Text
                );

            Rpt.SubJudul(x
                , "Project : " + project.SelectedItem.Text
                );

            //Rpt.Header(rpt, x);
            string legend = "* * = Inaktif";
            Rpt.HeaderReport(headReport, legend, x);
        }

        private void Fill(bool Excel)
        {
            string order = "Nama, NoCustomer";

            string Status = "";
            if (statusA.Checked) Status = " AND Status = 'A'";
            if (statusI.Checked) Status = " AND Status = 'I'";

            string Sifat = "";
            if (sifatSUDAH.Checked) Sifat = " AND (SELECT COUNT(*) FROM MS_KONTRAK WHERE NoCustomer = MS_CUSTOMER.NoCustomer) <> 0";
            if (sifatBELUM.Checked) Sifat = " AND (SELECT COUNT(*) FROM MS_KONTRAK WHERE NoCustomer = MS_CUSTOMER.NoCustomer) = 0";

            string aq = "";
            if (nama.Items[0].Selected)
                aq = aq + " LEFT(Nama,1) IN ('a','b','c','d') OR ";
            if (nama.Items[1].Selected)
                aq = aq + " LEFT(Nama,1) IN ('e','f','g','h') OR ";
            if (nama.Items[2].Selected)
                aq = aq + " LEFT(Nama,1) IN ('i','j','k','l') OR ";
            if (nama.Items[3].Selected)
                aq = aq + " LEFT(Nama,1) IN ('m','n','o','p') OR ";
            if (nama.Items[4].Selected)
                aq = aq + " LEFT(Nama,1) IN ('q','r','s','t') OR ";
            if (nama.Items[5].Selected)
                aq = aq + " LEFT(Nama,1) IN ('u','v','w','x') OR ";
            if (nama.Items[6].Selected)
                aq = aq + " LEFT(Nama,1) IN ('y','z','0','1','2','3','4','5','6','7','8','9') OR ";
            if (aq != "")
                aq = " AND (" + aq.Substring(0, aq.Length - 3) + ")";

            string Input = "";
            if (input.SelectedIndex != 0)
            {
                string[] z = input.SelectedValue.Split(',');
                Input = " AND YEAR(TglInput) = " + z[0]
                    + " AND MONTH(TglInput) = " + z[1];
            }

            string Lahir = "";
            if (lahir.SelectedIndex != 0)
            {
                order = " DAY(TglLahir), Nama";
                Lahir = " AND MONTH(TglLahir) = " + lahir.SelectedValue;
            }

            string AgentInput = "";
            if (agentinput.SelectedIndex != 0)
            {
                AgentInput = " AND AgentInput = '" + agentinput.SelectedValue + "'";
            }

            string Project = " AND Project IN (" + Act.ProjectListSql + ")";
            if (project.SelectedIndex != 0)
            {
                Project = " AND Project = '" + project.SelectedValue + "'";
            }

            string strSql = "SELECT *"
                + ",CASE (SELECT COUNT(*) FROM MS_KONTRAK WHERE NoCustomer = MS_CUSTOMER.NoCustomer)"
                + "		WHEN 0 THEN 'BELUM BELI'"
                + "		ElSE 'SUDAH BELI'"
                + " END AS Sifat"
                + " FROM MS_CUSTOMER"
                + " WHERE "
                + " SumberData IN (" + Rpt.inSql(sumberdata) + ")"
                //+ " AND Agama IN (" + Rpt.inSql(agama) + ")"
                + Project
                + Status
                + aq
                + Input
                + Lahir
                //+ AgentInput
                + Sifat
                + " ORDER BY " + order;

            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                r.VerticalAlign = VerticalAlign.Top;
                r.Attributes["ondblclick"] = "popEditCustomer('" + rs.Rows[i]["NoCustomer"] + "')";

                c = new TableCell();
                c.Text = (i + 1).ToString() + ".";
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                string inaktif = "";
                if (rs.Rows[i]["Status"].ToString() == "I")
                    inaktif = " **";

                c = new TableCell();
                if (!Excel)
                {
                    c.Text = rs.Rows[i]["Nama"].ToString() + " ("
                        + rs.Rows[i]["NoCustomer"].ToString().PadLeft(5, '0') + ")"
                        + inaktif;
                }
                else
                {
                    c.Text = rs.Rows[i]["Nama"].ToString();
                }
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["TipeCs"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["SumberData"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                string unit = "SELECT NoUnit FROM MS_KONTRAK WHERE NoCustomer = '" + rs.Rows[i]["NoCustomer"].ToString() + "'";
                DataTable rsUnit = Db.Rs(unit);
                string addUnit = "";
                for (int j = 0; j < rsUnit.Rows.Count; j++)
                {
                    addUnit = rsUnit.Rows[j]["NoUnit"] + ", ";
                }
                if (addUnit != "")
                    addUnit = addUnit.Substring(0, addUnit.Length - 2);
                c.Text = addUnit;
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["AgentInput"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NamaBisnis"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["JenisBisnis"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["MerekBisnis"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Agama"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglLahir"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoTelp"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoHP"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoKantor"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoFax"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Email"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoKTP"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Alamat1"] + "&nbsp;"
                    + rs.Rows[i]["Alamat2"] + "&nbsp;"
                    + rs.Rows[i]["Alamat3"] + "&nbsp;"
                    ;
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["KTP1"] + "&nbsp;"
                    + rs.Rows[i]["KTP2"] + "&nbsp;"
                    + rs.Rows[i]["KTP3"] + "&nbsp;"
                    + rs.Rows[i]["KTP4"] + "&nbsp;"
                    ;
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NPWP"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NPWPAlamat1"] + "&nbsp;"
                    + rs.Rows[i]["NPWPAlamat2"] + "&nbsp;"
                    + rs.Rows[i]["NPWPAlamat3"] + "&nbsp;"
                    ;
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Sifat"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                if (rs.Rows[i]["Sifat"].ToString() == "SUDAH BELI")
                {
                    PrintSales((int)rs.Rows[i]["NoCustomer"], r);
                }
                else
                {
                    c = new TableCell();
                    c.Text = "";
                    c.ColumnSpan = 5;
                    c.Wrap = false;
                    r.Cells.Add(c);
                }

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglInput"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglTransaksi"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + rs.Rows[i]["Project"].ToString() + "'");
                c.HorizontalAlign = HorizontalAlign.Left;
                c.VerticalAlign = VerticalAlign.Top;
                r.Cells.Add(c);

                rpt.Rows.Add(r);
            }
        }

        private void PrintSales(int NoCustomer, TableRow r)
        {
            TableCell c;

            DataTable rs = Db.Rs("SELECT TOP 1 * FROM MS_KONTRAK WHERE NoCustomer = " + NoCustomer
                + " ORDER BY NoKontrak DESC");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                c = new TableCell();
                c.Text = rs.Rows[i]["NoUnit"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                decimal LuasSG = Db.SingleDecimal("SELECT LuasSG FROM MS_UNIT WHERE NoStock = '" + rs.Rows[i]["NoStock"] + "'");
                c = new TableCell();
                c.Text = Cf.Num(LuasSG);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["NilaiKontrak"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Skema"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglKontrak"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);
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

        protected void agamaCheck_CheckedChanged(object sender, System.EventArgs e)
        {
            for (int i = 0; i < agama.Items.Count; i++)
            {
                agama.Items[i].Selected = agamaCheck.Checked;
            }

            Js.Focus(this, agamaCheck);
            agamac.Text = "";
        }

        protected void sumberdataCheck_CheckedChanged(object sender, System.EventArgs e)
        {
            for (int i = 0; i < sumberdata.Items.Count; i++)
            {
                sumberdata.Items[i].Selected = sumberdataCheck.Checked;
            }

            Js.Focus(this, sumberdataCheck);
            sumberdatac.Text = "";
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

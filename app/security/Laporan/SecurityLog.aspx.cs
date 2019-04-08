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


namespace ISC064.SECURITY.Laporan
{
    public partial class SecurityLog : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();

            if (!Page.IsPostBack)
            {
                comp.InnerHtml = Mi.Pt;
                rpt.Visible = false;
                init();
                if (!Act.Sec("DownloadExcel")) xls.Enabled = false;
            }
        }

        private void init()
        {
            dari.Text = Cf.Day(Cf.AwalBulan());
            sampai.Text = Cf.Day(Cf.AkhirBulan());

            DataTable rs;

            rs = Db.Rs("SELECT DISTINCT UserID, Nama FROM SECURITY_LOG ORDER BY Nama, UserID");
            for (int i = 0; i < rs.Rows.Count; i++)
                user.Items.Add(new ListItem(
                    rs.Rows[i][1] + " (" + rs.Rows[i][0] + ")"
                    , rs.Rows[i][0].ToString()));

            rs = Db.Rs("SELECT DISTINCT SecLevel FROM SECURITY_LOG ORDER BY SecLevel");
            for (int i = 0; i < rs.Rows.Count; i++)
                seclevel.Items.Add(new ListItem(rs.Rows[i][0].ToString()));

            rs = Db.Rs("SELECT DISTINCT IP FROM SECURITY_LOG ORDER BY IP");
            for (int i = 0; i < rs.Rows.Count; i++)
                ip.Items.Add(new ListItem(rs.Rows[i][0].ToString()));

            user.SelectedIndex = 0;
            seclevel.SelectedIndex = 0;
            ip.SelectedIndex = 0;
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

            if (!Cf.isPilih(aktivitas))
            {
                x = false;
                aktivitasc.Text = "Pilih Minimum Satu";
            }
            else
                aktivitasc.Text = "";

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

        protected void pdf_Click(object sender, System.EventArgs e)
        {
            
            Process p = new System.Diagnostics.Process();

            string Nama = "Laporan Security Log";
            string Link = "";
            DateTime TglGenerate = DateTime.Now;
            string FileName = "";
            string FileType = "application/pdf";
            string UserID = Act.UserID;
            string IP = Act.IP;

            Db.Execute("EXEC spLapPDFDaftar"

                  //  + " '" + Convert.ToString(007) + "'"
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

            string strSql = "SELECT * FROM ISC064_SECURITY..LapPDF WHERE AttachmentID  = '" + NoAttachment + "'";
            DataTable rs = Db.Rs(strSql);

            string nfilename = "LapSecurityLog" + NoAttachment + ".pdf";

            //update filename
            Db.Execute("UPDATE ISC064_SECURITY..LapPDF SET FileName= '" + nfilename + "' WHERE AttachmentID = " + NoAttachment);


            //folder untuk menyimpan file pdf
            string save = Mi.PathFilePDFReport + "LapSecurityLog" + rs.Rows[0]["AttachmentID"] + ".pdf";

            //declare parameter
            string SecLevel = seclevel.SelectedValue;
            string akt = string.Empty;
            try
            {
                foreach (ListItem item in aktivitas.Items)
                {
                    if (item.Selected == true)
                    {
                        akt += item.Value + '-';
                    }
                }
                // retval.TrimEnd(',');
            }
            catch (Exception)
            {
            }


            //link untuk download pdf
            string link = Mi.PathAlamatWeb + "security/LaporanPDF/PDFSecurityLog.aspx?id=" + rs.Rows[0]["AttachmentID"] + "&user=" + user.SelectedValue + "&ip=" + ip.SelectedValue + "&aktivitas=" + akt + "&seclevel=" + SecLevel + "";

            //update link
            Db.Execute("UPDATE ISC064_SECURITY..LapPDF SET Link= '" + link + "' WHERE AttachmentID = " + NoAttachment);


            //format page
            p.StartInfo.Arguments = "--orientation landscape --page-width 8.5in --page-height 11in --margin-left 0 --margin-right 0 --margin-top 0.25cm --margin-bottom 0 " + link + " " + save;
          
            //panggil aplikasi untuk mengconvert pdf
            p.StartInfo.FileName = Mi.PathWkhtmlPDFReport;
            p.Start();

            //60000 -> waktu jeda lama convert pdf
            p.WaitForExit(60000);


            string Src = Mi.PathFilePDFReport + nfilename;
            Mi.DownloadPDF(this, Src, (rs.Rows[0]["FileName"]).ToString(), rs.Rows[0]["FileType"].ToString());
          
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

            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);
            Rpt.SubJudul(x
                , "Tanggal Log : " + Cf.Day(dari.Text) + " s/d " + Cf.Day(sampai.Text)
                );

            Rpt.SubJudul(x
                , "Aktivitas : " + Rpt.inSql(aktivitas).Replace("'", "")
                );

            Rpt.SubJudul(x
                , "User : " + user.SelectedItem.Text
                );

            Rpt.SubJudul(x
                , "Security Level : " + seclevel.SelectedItem.Text
                );

            Rpt.SubJudul(x
                , "IP Address : " + ip.SelectedItem.Text
                );

            string legend = "";
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
            if (user.SelectedIndex != 0)
                UserID = " AND UserID = '" + user.SelectedValue + "'";

            string SecLevel = "";
            if (seclevel.SelectedIndex != 0)
                SecLevel = " AND SecLevel = '" + seclevel.SelectedValue + "'";

            string IP = "";
            if (ip.SelectedIndex != 0)
                IP = " AND IP = '" + ip.SelectedValue + "'";

            string strSql = "SELECT "
                + " LogID"
                + ",Tgl"
                + ",CASE Aktivitas "
                + "		WHEN 'L' THEN 'Log-In Normal'"
                + "		WHEN 'S' THEN 'Sign-Out Normal'"
                + "		WHEN 'DL' THEN 'Double Login'"
                + "		WHEN 'SP' THEN 'Salah Password'"
                + "		WHEN 'B' THEN 'Blokir'"
                + "		WHEN 'A' THEN 'Aktivasi'"
                + "		WHEN 'GP' THEN 'Ganti Password'"
                + " END AS AktivitasDetil"
                + ",UserID"
                + ",Nama"
                + ",SecLevel"
                + ",IP"
                + " FROM SECURITY_LOG"
                + " WHERE 1=1 "
                + " AND CONVERT(varchar,Tgl,112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND CONVERT(varchar,Tgl,112) <= '" + Cf.Tgl112(Sampai) + "'"
                + " AND Aktivitas IN (" + Rpt.inSql(aktivitas) + ")"
                + UserID
                + SecLevel
                + IP
                + " ORDER BY LogID";

            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                r.VerticalAlign = VerticalAlign.Top;

                c = new TableCell();
                c.Text = rs.Rows[i]["LogID"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["Tgl"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Time(rs.Rows[i]["Tgl"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["AktivitasDetil"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["UserID"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Nama"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["SecLevel"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["IP"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                rpt.Rows.Add(r);
            }
        }

        protected void aktivitasCheck_CheckedChanged(object sender, System.EventArgs e)
        {
            for (int i = 0; i < aktivitas.Items.Count; i++)
            {
                aktivitas.Items[i].Selected = aktivitasCheck.Checked;
            }

            Js.Focus(this, aktivitasCheck);
            aktivitasc.Text = "";
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

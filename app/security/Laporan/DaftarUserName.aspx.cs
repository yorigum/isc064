using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Diagnostics;

namespace ISC064.SECURITY.Laporan
{
    public partial class DaftarUserName : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();

            if (!Page.IsPostBack)
            {
                comp.InnerHtml = Mi.Pt;
                rpt.Visible = false;
                Js.Focus(this, scr);
                if (!Act.Sec("DownloadExcel")) xls.Enabled = false;
            }
        }
        protected void scr_Click(object sender, EventArgs e)
        {
            Report();
        }

        protected void xls_Click(object sender, EventArgs e)
        {
            Report();
            Rpt.ToExcel(this,headReport, rpt);
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
            
            if (rblstatus.SelectedValue == "S")
            {
                Rpt.SubJudul(x
                    , "Status : SEMUA"
                    );
            }
            else if (rblstatus.SelectedValue == "A")
            {
                Rpt.SubJudul(x
                    , "Status : AKTIF"
                    );
            }
            if (rblstatus.SelectedValue == "B")
            {
                Rpt.SubJudul(x
                    , "Status : BLOKIR"
                    );
            }
            string legend = "";
            Rpt.HeaderReport(headReport, legend, x);

        }

        private void Fill()
        {
            string Status = "";
            if (rblstatus.SelectedIndex != 0)
                Status = " WHERE Status = '" + rblstatus.SelectedValue + "'";

            string strSql = "SELECT * FROM " + Mi.DbPrefix + "SECURITY..USERNAME "
                + Status; 
                
            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                r.VerticalAlign = VerticalAlign.Top;

                c = new TableCell();
                c.Text = (i + 1).ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["UserID"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Nama"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);
				
				c = new TableCell();
                c.Text = rs.Rows[i]["SecLevel"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Status"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                rpt.Rows.Add(r);
            }
        }


        protected void pdf_Click(object sender, EventArgs e)
        {
            Process p = new System.Diagnostics.Process();

            string Nama = "Laporan Daftar Username";
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

            string strSql = "SELECT * FROM LapPDF WHERE AttachmentID  = '" + NoAttachment + "'";
            DataTable rs = Db.Rs(strSql);

            string nfilename = "LapDaftarUser" + NoAttachment + ".pdf";

            //update filename
            Db.Execute("UPDATE LapPDF SET FileName= '" + nfilename + "' WHERE AttachmentID = " + NoAttachment);


            //folder untuk menyimpan file pdf
            string save = Mi.PathFilePDFReport + "LapDaftarUser" + rs.Rows[0]["AttachmentID"] + ".pdf";

            //declare parameter
            string nStatusS = "";
            string nStatusA = "";
            string nStatusB = "";
            if (rblstatus.SelectedValue == "S")
                nStatusS = "SEMUA";
            else
                nStatusS = "";
            if (rblstatus.SelectedValue == "A")
                nStatusA = "AKTIF";
            else
                nStatusA = "";
            if (rblstatus.SelectedValue == "B")
                nStatusB = "BLOKIR";
            else
                nStatusB = "";

            //link untuk download pdf
            string link = Mi.PathAlamatWeb + "security/LaporanPDF/PDFLapDaftarUser.aspx?id=" + rs.Rows[0]["AttachmentID"] + "&status_s=" + nStatusS + "&status_b=" + nStatusB + "&status_a=" + nStatusA +"";

            //update link
            Db.Execute("UPDATE LapPDF SET Link= '" + link + "' WHERE AttachmentID = " + NoAttachment);

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
    }
}

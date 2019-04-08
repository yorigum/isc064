using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.FINANCEAR.Laporan
{
    public partial class LaporanTransferAnonim : System.Web.UI.Page
    {

        private string UserID { get { return (Request.QueryString["userid"]); } }
        private string AttachmentID { get { return (Request.QueryString["id"]); } }
        private string Rekening { get { return (Request.QueryString["rek"]); } }
        private string StatusA { get { return (Request.QueryString["status_a"]); } }
        private string StatusS { get { return (Request.QueryString["status_s"]); } }
        private string Project { get { return (Request.QueryString["project"]); } }
        private string Perusahaan { get { return (Request.QueryString["pers"]); } }
        private string StatusB { get { return (Request.QueryString["status_b"]); } }
        private string StatusID { get { return (Request.QueryString["status_id"]); } }

        protected System.Web.UI.WebControls.CheckBoxList tipe;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Report();
        }

        private void Report()
        {
            param.Visible = false;
            rpt.Visible = true;

            //Header();
            Header2();
            FillHeader();
            Fill();
        }

        private void Header()
        {
            System.Text.StringBuilder x = new System.Text.StringBuilder();

            Rpt.Judul(x, comp, judul);

            string tgl = "Tanggal Anonim";

            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");

            Rpt.SubJudul(x
                , tgl + " : " + Cf.Day(Dari) + " s/d " + Cf.Day(Sampai)
                );

            Rpt.SubJudul(x
                , "Rekening : " + Rekening
                );

            Rpt.Header(rpt, x);
        }

        private void Header2()
        {
            System.Text.StringBuilder x = new System.Text.StringBuilder();

            x.Append("<h2 style='margin:0;font:bold 10pt trebuchet ms'>"
                + comp.InnerHtml
                + "</h2>");

            x.Append("<h1 class='title' style='margin:0;font:bold 20pt'>"
                + judul.InnerHtml
                + "</h1>");

            string tgl = "Tanggal Anonim";

            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");


            x.Append("<h2 style='margin:0;font:bold 10pt trebuchet ms'>"
                + tgl + " : " + Cf.Day(Dari) + " s/d " + Cf.Day(Sampai)
                + "</h2>");

            x.Append("<h2 style='margin:0;font:bold 10pt trebuchet ms'>"
                + "Rekening : " + Rekening
                + "</h2>");

            if (StatusB != "")
                x.Append("<h2 style='margin:0;font:bold 10pt trebuchet ms'>"
                + "Status : " + StatusB
                + "<h2><br />");
            else if (StatusID != "")
                x.Append("<h2 style='margin:0;font:bold 10pt trebuchet ms'>"
                + "Status : " + StatusID
                + "</h2><br />");
            else if (StatusS != "")
                x.Append("<h2 style='margin:0;font:bold 10pt trebuchet ms'>"
                + "Status : " + StatusS
                + "</h2><br />");
            else
                x.Append("<h2 style='margin:0;font:bold 10pt trebuchet ms'>"
                + "Status : " + StatusA
                + "</h2><br />");

            //TableRow r = new TableRow();
            //TableCell c;

            //c = new TableCell();
            //c.Text = x.ToString();
            //c.ColumnSpan = 11;
            //r.Cells.Add(c);

            string legend = "";
            //rpt.Rows.Add(r);
            Rpt.HeaderReport(headReport, legend, x);
        }

        private void FillHeader()
        {
            TableRow r = new TableRow();
            TableHeaderCell c;

            c = new TableHeaderCell();
            c.Text = "TITIPAN";
            c.ColumnSpan = 6;
            //c.Attributes["style"] = "background-color:gray;color:white;";
            r.Cells.Add(c);

            c = new TableHeaderCell();
            c.Text = "REALISASI";
            c.ColumnSpan = 5;
            //c.Attributes["style"] = "background-color:gray;color:white;";
            r.Cells.Add(c);

            rpt.Rows.Add(r);

            r = new TableRow();

            c = new TableHeaderCell();
            c.Text = "NO.";
            //c.Attributes["style"] = "background-color:gray;color:white;";
            r.Cells.Add(c);

            c = new TableHeaderCell();
            c.Text = "NO. ANONIM";
            //c.Attributes["style"] = "background-color:gray;color:white;";
            r.Cells.Add(c);

            c = new TableHeaderCell();
            c.Text = "TGL. MASUK";
            //c.Attributes["style"] = "background-color:gray;color:white;";
            r.Cells.Add(c);

            c = new TableHeaderCell();
            c.Text = "BANK";
            //c.Attributes["style"] = "background-color:gray;color:white;";
            r.Cells.Add(c);

            c = new TableHeaderCell();
            c.Text = "KETERANGAN";
            //c.Attributes["style"] = "background-color:gray;color:white;";
            r.Cells.Add(c);

            c = new TableHeaderCell();
            c.Text = "NILAI";
            //c.Attributes["style"] = "background-color:gray;color:white;";
            r.Cells.Add(c);

            c = new TableHeaderCell();
            c.Text = "TGL. KWT";
            //c.Attributes["style"] = "background-color:gray;color:white;";
            r.Cells.Add(c);

            c = new TableHeaderCell();
            c.Text = "NO. KWT";
            //c.Attributes["style"] = "background-color:gray;color:white;";
            r.Cells.Add(c);

            c = new TableHeaderCell();
            c.Text = "UNIT";
            //c.Attributes["style"] = "background-color:gray;color:white;";
            r.Cells.Add(c);

            c = new TableHeaderCell();
            c.Text = "NAMA";
            //c.Attributes["style"] = "background-color:gray;color:white;";
            r.Cells.Add(c);

            c = new TableHeaderCell();
            c.Text = "NILAI";
            //c.Attributes["style"] = "background-color:gray;color:white;";
            r.Cells.Add(c);

            rpt.Rows.Add(r);
        }

        private void Fill()
        {
            string nProject = "";
            if (Project != "SEMUA") nProject = " AND c.Project IN('" + Project.Replace(",","','") + "')";
            string nPerusahaan = "";
            if (Perusahaan != "SEMUA") nPerusahaan = " AND c.Pers = '" + Perusahaan + "'";

            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");


            decimal t1 = 0;
            decimal t2 = 0;
            decimal t3 = 0;

            string Rek = "";
            if (Rekening != "SEMUA")
                Rek = " AND a.Bank = '" + Rekening.Replace("%", " ") + "'";

            string Status = "";
            if (StatusB != "") Status = " AND a.Status = 'BARU'";
            if (StatusID != "") Status = " AND a.Status = 'ID'";
            if (StatusS != "") Status = " AND a.Status = 'S'";

            string tgl = "Tgl";

            string strSql = "SELECT * "
                    + " FROM MS_ANONIM a LEFT JOIN MS_TTS b ON a.NoAnonim =  b.NoAnonim"
                    + " LEFT JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK c ON b.Ref = c.NoKontrak"
                    + " WHERE 1=1 "
                    + " AND CONVERT(varchar," + tgl + ",112) >= '" + Cf.Tgl112(Dari) + "'"
                    + " AND CONVERT(varchar," + tgl + ",112) <= '" + Cf.Tgl112(Sampai) + "'"
                    + nProject
                    + nPerusahaan
                    + Rek
                    + Status
                    + " ORDER BY a.NoAnonim";

            DataTable rs = Db.Rs(strSql);

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                r.VerticalAlign = VerticalAlign.Top;
                r.Attributes["ondblclick"] = "popEditTA('" + rs.Rows[i]["NoAnonim"] + "')";

                c = new TableCell();
                c.Text = (i + 1) + ".";
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoAnonim"].ToString().PadLeft(7, '0');
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["Tgl"]);
                c.Wrap = false;
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                string NamaBank = Db.SingleString("SELECT Bank FROM REF_ACC WHERE Acc = '" + rs.Rows[i]["Bank"] + "'");
                string RekBank = Db.SingleString("SELECT Rekening FROM REF_ACC WHERE Acc = '" + rs.Rows[i]["Bank"] + "'");

                c = new TableCell();
                c.Text = NamaBank;// +" " + RekBank;
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.StrKet(rs.Rows[i]["Ket"].ToString());
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["Nilai"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                DataTable kw = Db.Rs("SELECT * FROM MS_TTS WHERE Status = 'POST' AND NoAnonim = '" + rs.Rows[i]["NoAnonim"].ToString() + "'");

                c = new TableCell();
                if(kw.Rows.Count > 0)
                    c.Text = Cf.Day(kw.Rows[0]["TglBKM"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                if (kw.Rows.Count > 0)
                    c.Text = kw.Rows[0]["ManualBKM"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                if (kw.Rows.Count > 0)
                    c.Text = kw.Rows[0]["Unit"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                if (kw.Rows.Count > 0)
                    c.Text = kw.Rows[0]["Customer"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                if (kw.Rows.Count > 0)
                    c.Text = Cf.Num(kw.Rows[0]["Total"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                rpt.Rows.Add(r);

                t1 += Convert.ToDecimal(rs.Rows[i]["Nilai"]);
                if (kw.Rows.Count > 0)
                    t2 += Convert.ToDecimal(kw.Rows[0]["Total"]);

                if (i == rs.Rows.Count - 1)
                {
                    SubTotal("GRAND TOTAL", t1, t2);
                }
            }
        }

        private void SubTotal(string txt, decimal t1, decimal t2)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = Rpt.Foot();
            c.Text = txt;
            c.ColumnSpan = 5;
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t1);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = "&nbsp;";
            c.ColumnSpan = 4;
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t2);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            rpt.Rows.Add(r);

            r = new TableRow();

            c = new TableCell();
            c.Text = "<b>SISA TITIPAN : " + Cf.Num(t1-t2) + "</b>";
            c.ColumnSpan = 11;
            c.HorizontalAlign = HorizontalAlign.Left;
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
    }
}

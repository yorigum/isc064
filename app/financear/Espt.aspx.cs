using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.FINANCEAR
{
    public partial class Espt : System.Web.UI.Page
    {
        protected System.Web.UI.WebControls.CheckBoxList tipe;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();

            if (!Page.IsPostBack)
            {
                //comp.InnerHtml = Mi.Pt;
                rpt.Visible = false;
                Js.Focus(this, scr);
                init();
                if (!Act.Sec("DownloadExcel")) xls.Enabled = false;
            }
        }

        private void init()
        {
            dari.Text = Cf.Day(DateTime.Today);
            sampai.Text = Cf.Day(DateTime.Today);

            //DataTable rs;

            //rs = Db.Rs("SELECT DISTINCT UserID FROM MS_TTS ORDER BY UserID");
            //for (int i = 0; i < rs.Rows.Count; i++)
            //    kasir.Items.Add(new ListItem(
            //        rs.Rows[i][0].ToString()));

            //kasir.SelectedIndex = 0;
        }

        private bool valid()
        {
            string s = "";
            bool x = true;

            //if (!Cf.isTgl(dari))
            //{
            //    x = false;
            //    if (s == "") s = dari.ID;
            //    daric.Text = "Tanggal";
            //}
            //else
            //    daric.Text = "";

            //if (!Cf.isTgl(sampai))
            //{
            //    x = false;
            //    if (s == "") s = sampai.ID;
            //    sampaic.Text = "Tanggal";
            //}
            //else
            //    sampaic.Text = "";

            //if (!x && s != "")
            //{
            //    RegisterStartupScript("err"
            //        , "<script language='javascript'>document.getElementById('" + s + "').select()</script>");
            //}

            return x;
        }

        protected void scr_Click(object sender, System.EventArgs e)
        {
            if (valid())
                Report();
        }
        protected void xls_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                Report();
                Rpt.ToExcel(this, rpt);
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

            //Rpt.Judul(x, comp, judul);

            //string tgl = "";
            //if (tglinput.Checked) tgl = tglinput.Text;

            //DateTime Dari = Convert.ToDateTime(dari.Text);
            //DateTime Sampai = Convert.ToDateTime(sampai.Text);
            //Rpt.SubJudul(x
            //    , tgl + " : " + Cf.Day(dari.Text) + " s/d " + Cf.Day(sampai.Text)
            //    );

            //Rpt.SubJudul(x
            //    , "Kasir : " + kasir.SelectedItem.Text
            //    );

            //Rpt.Header(rpt, x);
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

            //string UserID = " AND UserID = '" + Act.UserID + "'";

            decimal t1 = 0;
            decimal t2 = 0;
            decimal t3 = 0;

            string UserID = "";
            //if (kasir.SelectedIndex != 0)
            //    UserID = " AND UserID = '" + kasir.SelectedValue + "'";

            string tgl = "";

            string strSql = "SELECT * "
                + " FROM MS_TTS"
                + " WHERE 1=1 "
                + " AND CONVERT(varchar,TglTTS,112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND CONVERT(varchar,TglTTS,112) <= '" + Cf.Tgl112(Sampai) + "'"
                + " AND NoFPS <> ''"
                //+ UserID
                + " ORDER BY NoTTS";

            DataTable rs = Db.Rs(strSql);

            //DataTable rsGiro = Db.Rs(
            //    "SELECT DISTINCT NoBG"
            //    + " FROM MS_TTS"
            //    + " WHERE 1=1 "
            //    + " AND CONVERT(varchar," + tgl + ",112) >= '" + Cf.Tgl112(Dari) + "'"
            //    + " AND CONVERT(varchar," + tgl + ",112) <= '" + Cf.Tgl112(Sampai) + "'"
            //    + UserID
            //    + " AND NoBG != ''"
            //    );
            //int LembarGiro = rsGiro.Rows.Count;

            decimal TN = 0, KD = 0, KK = 0, TR = 0, BG = 0, UJ = 0, DN = 0;

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                r.VerticalAlign = VerticalAlign.Top;
                r.Attributes["ondblclick"] = "popEditTTS('" + rs.Rows[i]["NoTTS"] + "')";

                c = new TableCell();
                //c.Text = rs.Rows[i]["NoTTS"].ToString().PadLeft(7,'0');
                c.Text = "A";// rs.Rows[i]["NoTTS2"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "2";// Cf.Day(rs.Rows[i]["TglTTS"]);
                c.Wrap = false;
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                //c.Text = rs.Rows[i]["NoTTS"].ToString().PadLeft(7,'0');
                c.Text = "1";// rs.Rows[i]["NoBKM2"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "1";// Cf.Day(rs.Rows[i]["TglBKM"]);
                c.Wrap = false;
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "3";// rs.Rows[i]["Ref"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "";// rs.Rows[i]["Unit"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "";// rs.Rows[i]["Customer"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoFPS"].ToString();// rs.Rows[i]["CaraBayar"].ToString();
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

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglTTS"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "";
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "0101";
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Convert.ToDateTime(rs.Rows[i]["TglTTS"]).Year.ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "";
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);
                
                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["Total"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Convert.ToDecimal(rs.Rows[i]["Total"]) / (decimal)10);//rs.Rows[i]["LebihBayar"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "0";// Cf.Num(rs.Rows[i]["Total2"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                rpt.Rows.Add(r);

                t1 = t1 + (decimal)rs.Rows[i]["Total"];
                t2 = t2 + (decimal)rs.Rows[i]["LebihBayar"];
                t3 = t3 + (decimal)rs.Rows[i]["Total2"];

                if (rs.Rows[i]["CaraBayar"].ToString() == "TN")
                    TN += Convert.ToDecimal(rs.Rows[i]["Total"]);
                else if (rs.Rows[i]["CaraBayar"].ToString() == "KD")
                    KD += Convert.ToDecimal(rs.Rows[i]["Total"]);
                else if (rs.Rows[i]["CaraBayar"].ToString() == "KK")
                    KK += Convert.ToDecimal(rs.Rows[i]["Total"]);
                else if (rs.Rows[i]["CaraBayar"].ToString() == "TR")
                    TR += Convert.ToDecimal(rs.Rows[i]["Total"]);
                else if (rs.Rows[i]["CaraBayar"].ToString() == "BG")
                    BG += Convert.ToDecimal(rs.Rows[i]["Total"]);
                else if (rs.Rows[i]["CaraBayar"].ToString() == "UJ")
                    UJ += Convert.ToDecimal(rs.Rows[i]["Total"]);
                else if (rs.Rows[i]["CaraBayar"].ToString() == "DN")
                    DN += Convert.ToDecimal(rs.Rows[i]["Total"]);

                //if (i == rs.Rows.Count - 1)
                //{
                //    SubTotal("GRAND TOTAL", t1, t2, t3);
                //    Giro(LembarGiro);
                //    Detail(TN, KD, KK, TR, BG, UJ, DN);
                //}
            }
        }

        private void Giro(int LembarGiro)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = new TableCell();
            c.ColumnSpan = 15;
            c.Text = "<strong>Lembar Giro: </strong>" + LembarGiro.ToString();
            r.Cells.Add(c);

            rpt.Rows.Add(r);
        }

        private void Detail(decimal TN, decimal KD, decimal KK, decimal TR, decimal BG, decimal UJ, decimal DN)
        {
            TableRow r;
            TableCell c;

            r = new TableRow();

            c = new TableCell();
            c.ColumnSpan = 15;
            c.Text = "<strong>Jumlah Tunai (TN): </strong>" + Cf.Num(TN)
                + "<br />"
                + "<strong>Jumlah Kartu Debit (KD): </strong>" + Cf.Num(KD)
                + "<br />"
                + "<strong>Jumlah Kartu Kredit (KK): </strong>" + Cf.Num(KK)
                + "<br />"
                + "<strong>Jumlah Transfer Bank (TR): </strong>" + Cf.Num(TR)
                + "<br />"
                + "<strong>Jumlah Cek Giro (BG): </strong>" + Cf.Num(BG)
                + "<br />"
                + "<strong>Jumlah Uang Jaminan (UJ): </strong>" + Cf.Num(UJ)
                + "<br />"
                + "<strong>Jumlah Diskon (DN): </strong>" + Cf.Num(DN)
                ;
            r.Cells.Add(c);

            rpt.Rows.Add(r);

            //Ttd
            r = new TableRow();

            c = new TableCell();
            c.ColumnSpan = 13;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "Kasir:<br /><br /><br /><br /><br />(" + Act.UserID + ")";
            c.ColumnSpan = 2;
            c.HorizontalAlign = HorizontalAlign.Center;
            r.Cells.Add(c);

            rpt.Rows.Add(r);
        }

        private void SubTotal(string txt, decimal t1, decimal t2, decimal t3)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = Rpt.Foot();
            c.Text = txt;
            c.ColumnSpan = 13;
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t1);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t2);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t3);
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
    }
}

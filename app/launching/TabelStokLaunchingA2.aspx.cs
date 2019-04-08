using System;
using System.Drawing;
using System.IO;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LAUNCHING
{
    public partial class TabelStokLaunchingA2 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            PrintHeader();

            Print("25");
            Print("23");
            Print("22");
            Print("21");
            Print("19");
            Print("18");
            Print("17");
            Print("16");
            Print("15");
            Print("12");
            Print("11");
            Print("10");
            Print("09");
            Print("08");
            Print("07");
            Print("06");
            Print("05");
            Print("03");
            Print("02");
            Print("01");

            fillLegend();
        }


        private void PrintHeader()
        {
            TableRow r = new TableRow();
            TableCell c;

            c = new TableCell(); c.CssClass = "h"; c.Text = "Blk.";
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "D1";
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "D2";
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "D3";
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "D5";
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "D6";
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "D7";
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "D8";
            r.Cells.Add(c);  

            tb.Rows.Add(r);
        }


        private void Print(string Lantai)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = new TableCell();
            c.Text = Lantai;
            c.CssClass = "lt";
            r.Cells.Add(c);

            for (int i = 1; i < tb.Rows[0].Cells.Count; i++)
            {
                string nounit = tb.Rows[0].Cells[i].Text;

                c = new TableCell();

                string x = href(nounit + "/" + Lantai);
                if (x != "")
                {
                    //c.Text = "<a href='" + x + "'>" + nounit + "</a>";
                    c.Text = Lantai;
                }
                else
                {
                    c.Text = "";// nounit;
                }
                string noUnit = nounit + "/" + Lantai;
                string unit = Db.SingleString("SELECT NoUnit FROM MS_UNIT WHERE NoUnit='" + noUnit + "'");

                c.BackColor = status(nounit + "/" + Lantai);
                c.ToolTip = unit;
                r.Cells.Add(c);
            }

            tb.Rows.Add(r);
        }

        private void PrintKet(string Ket)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = new TableCell();
            c.Text = Ket;
            c.CssClass = "ket";
            c.ColumnSpan = 21;
            r.Cells.Add(c);

            tb.Rows.Add(r);
        }

        private void PrintKet2(string Lantai, string Ket)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = new TableCell();
            c.Text = Lantai;
            c.CssClass = "lt";
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = Ket;
            c.CssClass = "ket";
            c.ColumnSpan = 20;
            r.Cells.Add(c);

            tb.Rows.Add(r);
        }

        private string href(string unit)
        {
            string x = "";

            string strSql = "SELECT Status, NoStock FROM MS_UNIT WHERE NoUnit = '" + unit + "'";
            DataTable rs = Db.Rs(strSql);
            if (rs.Rows.Count != 0)
            {
                if (rs.Rows[0]["Status"].ToString() == "B")
                {
                    int c = Db.SingleInteger("SELECT COUNT(*) FROM MS_KONTRAK"
                        + " WHERE Status = 'A' AND NoUnit = '" + unit + "'");
                    if (c != 0)
                    {
                        string NoKontrak = Db.SingleString("SELECT TOP 1 NoKontrak FROM MS_KONTRAK WHERE Status = 'A' AND NoUnit = '" + unit + "'");

                        int NoTTS = 0;
                        DataTable tts = Db.Rs("SELECT TOP 1 NoTTS FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS WHERE Ref = '" + NoKontrak + "' AND Status <> 'VOID' ORDER BY NoTTS ASC");
                        if (tts.Rows.Count != 0)
                            NoTTS = Convert.ToInt32(tts.Rows[0][0]);

                        x = "TabelStok3.aspx?NoKontrak=" + NoKontrak + "&NoTTS=" + NoTTS; //sold
                    }
                    else
                        x = unit; //hold internal
                }
                else
                {
                    int c = Db.SingleInteger("SELECT COUNT(*) FROM MS_RESERVASI"
                        + " WHERE Status = 'A' AND NoUnit = '" + unit + "'");
                    if (c != 0)
                        x = "TabelStok2.aspx?NoUnit=" + unit;//"KontrakDaftar2.aspx?NoStock=" + rs.Rows[0]["NoStock"]; //booked
                    else
                    {
                        if (Act.SecLevel == "AG")
                            x = "javascript:popUnit(\"" + rs.Rows[0]["NoStock"] + "\")"; //available
                        else
                            x = "TabelStok2.aspx?NoUnit=" + unit; //available
                    }
                }
            }

            return x;
        }

        private Color status(string unit)
        {
            string strSql = "SELECT Status"
                + " FROM MS_UNIT "
                + " WHERE NoUnit = '" + unit + "'";
            DataTable rs = Db.Rs(strSql);
            if (rs.Rows.Count != 0)
            {
                Color color = new Color();

                if (rs.Rows[0]["Status"].ToString() == "B")
                {
                    int c = Db.SingleInteger("SELECT COUNT(*) FROM MS_KONTRAK"
                        + " WHERE Status = 'A' AND NoUnit = '" + unit + "'");
                    if (c != 0)
                    {
                        string NoKontrak = Db.SingleString("SELECT NoKontrak FROM "
                            + " MS_KONTRAK WHERE Status = 'A' AND NoUnit = '" + unit + "'");
                        DateTime TglInput = Db.SingleTime("SELECT TglInput FROM "
                            + " MS_KONTRAK WHERE Status = 'A' AND NoUnit = '" + unit + "'");
                        decimal t = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan),0) FROM "
                            + " MS_PELUNASAN WHERE NoKontrak = '" + NoKontrak + "'");
                        decimal r = Db.SingleDecimal("SELECT PersenLunas FROM "
                            + " MS_KONTRAK WHERE Status = 'A' AND NoUnit = '" + unit + "'");
                        if (r >= 30)
                            color = Color.Red; //sold
                        else
                            color = Color.Red; //sold under 30%

                        int FlagProsesBatal = Db.SingleInteger("SELECT FlagProsesBatal FROM MS_KONTRAK WHERE Status = 'A' AND NoUnit = '" + unit + "' ");
                        if (FlagProsesBatal == 1)
                        {
                            color = Color.Red; // Kontrak Sedang Dalam Proses Batal
                        }
                    }
                    else
                        color = Color.Red; //hold internal
                }
                else
                {
                    int c = Db.SingleInteger("SELECT COUNT(*) FROM MS_RESERVASI"
                        + " WHERE Status = 'A' AND NoUnit = '" + unit + "'");
                    if (c != 0)
                        color = Color.Green; //reserved
                    else
                        color = Color.Green; //available
                }

                return color;
            }
            else
                return Color.White;
        }

        protected void fillLegend()
        {
            legend.Text = "<table><tr>";

            addLegendColor("Red", "Sold");
            //addLegendColor("Yellow", "Sold Under 30%");
            //addLegendColor("Gray", "Proses Batal");
            //addLegendColor("Orange", "Hold Internal");
            //addLegendColor("Blue", "Reserved");
            addLegendColor("Green", "Available");

            legend.Text += "</tr><table>";
        }

        protected void addLegendColor(string code, string name)
        {

            bool isValidCode = true;

            code = code.Trim();
            name = name.Trim();

            //if (code.Substring(0, 1) != "#") { code = "#" + code; }
            //if (code.Length != 7) { isValidCode = false; }

            if (isValidCode)
            {
                legend.Text += "<td style='width:20px; padding:0 0 0 5px; border:solid 1px black; background-color:" + code + ";'>&nbsp;</td><td>&nbsp;:&nbsp;</td><td style='padding:0 5px 0 0;'>" + name + "</td>";
            }
        }

        private string tower
        {
            get
            {
                return "TA";
            }
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
using System;
using System.Drawing;
using System.IO;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LAUNCHING
{
    public partial class TabelStokC : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            PrintHeader();
            Print("3");
            Print("5");
            Print("6");
            Print("7");
            Print("8");
            Print("9");
            Print("10");
            Print("11");
            Print("12");
            Print("15");
            Print("16");
            Print("17");
            Print("18");
            Print("19");
            Print("20");
            Print("21");
            Print("23");

            fillLegend();
        }


        private void PrintHeader()
        {
            TableRow r = new TableRow();
            TableCell c;

            c = new TableCell(); c.CssClass = "h"; c.Text = "Lt.";
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "1";
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "2";
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "3";
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "5";
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "6";
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "7";
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "8";
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "9";
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "10";
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "11";
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "12";
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "15";
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "16";
            r.Cells.Add(c);


            c = new TableCell(); c.CssClass = "h"; c.Text = "17";
            r.Cells.Add(c);


            c = new TableCell(); c.CssClass = "h"; c.Text = "18";
            r.Cells.Add(c);


            c = new TableCell(); c.CssClass = "h"; c.Text = "19";
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

                string x = href(tower + "-" + Lantai.PadLeft(2, '0') + "" + nounit.PadLeft(2, '0'));
                if (x != "")
                {
                    c.Text = "<a href='" + x + "'>" + nounit + "</a>";
                }
                else
                {
                    c.Text = nounit;// nounit;
                }
                string noUnit = tower + "-" + Lantai.PadLeft(2, '0') + "" + nounit.PadLeft(2, '0');
                string unit = Db.SingleString("SELECT NoUnit FROM MS_UNIT WHERE NoUnit='" + noUnit + "'");

                c.BackColor = status(tower + "-" + Lantai.PadLeft(2, '0') + "" + nounit.PadLeft(2, '0'));
                c.ToolTip = unit;
                r.Cells.Add(c);
            }

            tb.Rows.Add(r);
        }

        private void PrintKet(string Ket1, int a1, string Ket2, int a2)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = new TableCell();
            c.Text = "&nbsp;";
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = Ket1;
            c.CssClass = "ket";
            c.ColumnSpan = a1;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = Ket2;
            c.CssClass = "ket";
            c.ColumnSpan = a2;
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

                        //x = "TabelStok3.aspx?NoKontrak=" + NoKontrak + "&NoTTS=" + NoTTS; //sold
                    }
                    else
                        x = ""; //hold internal
                }
                else
                {
                    int c = Db.SingleInteger("SELECT COUNT(*) FROM MS_RESERVASI"
                        + " WHERE Status = 'A' AND NoUnit = '" + unit + "'");

                    if (c != 0)
                        //x = "UnitPilih1.aspx?NoUnit=" + unit;//"KontrakDaftar2.aspx?NoStock=" + rs.Rows[0]["NoStock"]; //booked
                        x = "UnitPilih1.aspx?NoNUP=" + NoNUP + "&NoStock=" + rs.Rows[0]["NoStock"];
                    else
                    {
                        //if (Act.SecLevel == "AG")
                        //    x = "javascript:popUnit(\"" + rs.Rows[0]["NoStock"] + "\")"; //available
                        //else
                        //x = "UnitPilih1.aspx?NoUnit=" + unit; //available
                        x = "UnitPilih1.aspx?NoNUP=" + NoNUP + "&NoStock=" + rs.Rows[0]["NoStock"];
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

                            color = System.Drawing.ColorTranslator.FromHtml("#ed4e2a");
                        //color = Color.Red; //sold
                        else
                            color = System.Drawing.ColorTranslator.FromHtml("#ed4e2a");
                        //color = Color.Red; //sold under 30%

                        byte titipjual = Db.SingleByte("SELECT TitipJual FROM MS_KONTRAK WHERE NoUnit='" + unit + "' AND STATUS='A'");
                        if (titipjual == (byte)1)
                        {
                            color = Color.Pink;
                        }
                        int FlagProsesBatal = Db.SingleInteger("SELECT FlagProsesBatal FROM MS_KONTRAK WHERE Status = 'A' AND NoUnit = '" + unit + "' ");
                        if (FlagProsesBatal == 1)
                        {
                            color = System.Drawing.ColorTranslator.FromHtml("#999999");
                            //color = Color.Gray; // Kontrak Sedang Dalam Proses Batal
                        }
                    }
                    else
                        color = Color.Blue;
                    //color = Color.RED; //hold internal
                }
                else
                {
                    int c = Db.SingleInteger("SELECT COUNT(*) FROM MS_RESERVASI"
                        + " WHERE Status = 'A' AND NoUnit = '" + unit + "'");

                    if (c != 0)
                        color = System.Drawing.ColorTranslator.FromHtml("#57b5e3"); //#57b5e3
                    else
                        color = System.Drawing.ColorTranslator.FromHtml("#3cc051");
                    //color = Color.Green; //available
                }

                if (rs.Rows[0]["Status"].ToString() == "H")
                {
                    color = Color.Blue;
                }

                return color;
            }
            else
                return Color.White;
        }

        protected void fillLegend()
        {
            legend.Text = "<table><tr>";

            addLegendColor("#ed4e2a", "<b>Sold</b>"); //red
            //addLegendColor("#fce522", "<b>Sold Under 30%</b>"); //yellow
            //addLegendColor("#999999", "<b>Proses Batal</b>"); //gray
            addLegendColor("blue", "<b>Hold</b>"); //orange
            //addLegendColor("#57b5e3", "<b>Reserved</b>"); //blue
            addLegendColor("#3cc051", "<b>Available</b>"); //green
            addLegendColor("LightBlue", "<b>Reserved</b>");
            //addLegendColor("Pink", "<b>Titip Jual</b>");

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
                legend.Text += "<td style='width:40px; height:25px; padding:0 0 0 5px; background-color:" + code + ";'></td><td style='padding:0 5px 0 5px;'>" + name + "</td>";
            }
        }

        private string tower
        {
            get
            {
                return "WP";
            }
        }

        private string NoNUP
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoNUP"]);
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
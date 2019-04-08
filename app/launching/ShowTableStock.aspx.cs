using System;
using System.Drawing;
using System.IO;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LAUNCHING
{
    public partial class ShowTableStock : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            PrintHeader();
            PrintHeader2();
            PrintHeader3();
            PrintKet();

            string[] lt = { "16", "15", "12", "11", "10", "09", "08", "07", "06", "05", "03", "02" };
            for (int i = 0; i < lt.Length; i++)
            {
                Print(lt[i]);
            }
        }

        private void PrintHeader()
        {
            TableRow r = new TableRow();
            TableCell c;

            c = new TableCell(); c.CssClass = "h"; c.Style["text-align"] = "center";
            c.ColumnSpan = 39; c.Text = "TOWER A";
            c.Width = 500;
            r.Cells.Add(c);

            tb.Rows.Add(r);
        }

        private void PrintHeader2()
        {
            TableRow r = new TableRow();
            TableCell c;

            c = new TableCell(); c.CssClass = "h";
            c.Text = "FLOOR";
            c.Width = 75;
            r.Cells.Add(c);

            string[] z = { "01", "02", "03", "05", "06", "07", "08", "09", "10", "11", "12", "15", "16", "17", "18", "19", "20", "21", "22", "23", "25", "26", "27", "28", "29", "30", "31", "32", "33", "35", "36", "37", "38", "39", "40", "41", "42", "43" };
            for (int i = 0; i < z.Length; i++)
            {
                c = new TableCell();
                c.CssClass = "h";
                c.Text = z[i];
                c.Style["text-align"] = "center";
                c.Width = 100;
                r.Cells.Add(c);
            }
            tb.Rows.Add(r);

        }

        private void PrintHeader3()
        {
            TableRow r = new TableRow();
            TableCell c;

            c = new TableCell();
            c.Text = "Type";
            c.CssClass = "ket";
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "36";
            c.CssClass = "ket2";
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "21";
            c.CssClass = "ket2";
            c.ColumnSpan = 5;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "36";
            c.CssClass = "ket2";
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "21";
            c.ColumnSpan = 2;
            c.CssClass = "ket2";
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "36";
            c.ColumnSpan = 2;
            c.CssClass = "ket2";
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "21";
            c.ColumnSpan = 10;
            c.CssClass = "ket2";
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "36";
            c.ColumnSpan = 2;
            c.CssClass = "ket2";
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "21";
            c.ColumnSpan = 13;
            c.CssClass = "ket2";
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "36";
            c.ColumnSpan = 2;
            c.CssClass = "ket2";
            r.Cells.Add(c);

            tb.Rows.Add(r);
        }

        private void PrintNoUnit()
        {
            TableRow r = new TableRow();
            TableCell c;

            string[] z = { "28", "27", "26", "25", "23", "22", "21", "20", "19", "18", "17", "16", "15", "12", "11", "10", "09", "08", "07", "06", "05", "03", "02" };
            for (int i = 0; i < z.Length; i++)
            {
                c = new TableCell();
                c.CssClass = "h";
                c.Text = z[i];
                c.Style["text-align"] = "center";
                c.Width = 100;
                r.Cells.Add(c);
            }
            tb.Rows.Add(r);
        }

        private void PrintKet()
        {
            TableRow r = new TableRow();
            TableCell c;

            c = new TableCell();
            c.Text = "Lantai";
            c.CssClass = "ket";
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "Salak Mountain / City";
            c.CssClass = "ket";
            c.ColumnSpan = 10;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "City Cibubur";
            c.CssClass = "ket";
            c.BackColor = Color.SkyBlue;
            c.ColumnSpan = 12;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "Swimming Pool & Jakarta City";
            c.CssClass = "ket";
            c.ColumnSpan = 9;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "Swimming Pool";
            c.CssClass = "ket";
            c.BackColor = Color.SkyBlue;
            c.ColumnSpan = 10;
            r.Cells.Add(c);

            tb.Rows.Add(r);
        }

        private void Print(string Lantai)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = new TableCell();
            c.Text = Lantai;
            c.CssClass = "h";
            c.Width = 45;
            r.Cells.Add(c);

            string[] z = new string[] { "01", "02", "03", "05", "06", "07", "08", "09", "10", "11", "12", "15", "16", "17", "18", "19", "20", "21", "22", "23", "25", "26", "27", "28", "29", "30", "31", "32", "33", "35", "36", "37", "38", "39", "40", "41", "42", "43" };
            for (int i = 0; i < z.Length; i++)
            {
                string nounit = z[i];

                string noUnit = tower + Lantai + nounit;
                string unit = Db.SingleString("SELECT NoUnit FROM MS_UNIT WHERE NoUnit = '" + noUnit + "'");
                string jenis = Db.SingleString("SELECT Jenis FROM MS_UNIT WHERE NoUnit = '" + noUnit + "'");
                string price = Cf.Num(Db.SingleDecimal("SELECT PriceList FROM MS_UNIT WHERE NoUnit = '" + noUnit + "'"));
                int count = Db.SingleInteger("SELECT COUNT(*) FROM ISC064_MARKETINGJUAL..MS_UNIT WHERE NoUnit = '" + noUnit + "'");

                c = new TableCell();

                string x = tower + Lantai + nounit;//href(tower + Lantai + nounit);
                string Unit2 = tower + Lantai + nounit;
                if (count != 0)
                {
                    if (x != "")
                    {
                        if (
                            Unit2 == "A0828" || Unit2 == "A0829" || Unit2 == "A0830" || Unit2 == "A0831" || Unit2 == "A0832" || Unit2 == "A0835" || Unit2 == "A0836" || Unit2 == "A0837" ||
                        Unit2 == "A0928" || Unit2 == "A0929" || Unit2 == "A0930" || Unit2 == "A0931" || Unit2 == "A0932" || Unit2 == "A0935" || Unit2 == "A0936" || Unit2 == "A0937" ||
                        Unit2 == "A0728" || Unit2 == "A0529" || Unit2 == "A0530" || Unit2 == "A0537" || Unit2 == "A0537" || Unit2 == "A0329" || Unit2 == "A0330" || Unit2 == "A0331" ||
                        Unit2 == "A0332" || Unit2 == "A0333" || Unit2 == "A0335" || Unit2 == "A0336" || Unit2 == "A0337" || Unit2 == "A0338" || Unit2 == "A0339" || Unit2 == "A0340" ||
                        Unit2 == "A0341" || Unit2 == "A0342" || Unit2 == "A0228" || Unit2 == "A0229"
                           )
                        { c.Text = ""; }
                        else
                            c.Text = "<a>" + tower + Lantai + nounit + "</a>";
                    }
                    else
                    {
                        c.Text = tower + Lantai + nounit;
                    }
                }

                c.BackColor = status(tower + Lantai + nounit);
                //c.ToolTip = unit + " - " + jenis + "\nRp. " + price;
                c.Style["text-align"] = "center";
                c.Attributes["style"] = "1px dotted black";
                c.Width = 45;
                r.Cells.Add(c);
            }

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

                        x = "";// "TabelStok3.aspx?NoKontrak=" + NoKontrak + "&NoTTS=" + NoTTS; //sold
                    }
                    else
                        x = ""; //hold internal
                }
                else
                {
                    int c = Db.SingleInteger("SELECT COUNT(*) FROM MS_RESERVASI"
                         + " WHERE Status = 'A' AND NoUnit = '" + unit + "'");
                    if (c != 0)
                        x = "";//"KontrakDaftar2.aspx?NoStock=" + rs.Rows[0]["NoStock"]; //booked
                    else
                    {
                        x = "";// "UnitPilih1.aspx?NoNUP=" + NoNUP + "&NoStock=" + rs.Rows[0]["NoStock"] + "&NoPil=" + NoPilihan;
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
                        color = Color.Red; //Sold
                    }
                    else
                        color = Color.Yellow; //select unit
                }
                else
                {
                    color = Color.White; //available
                }

                return color;
            }
            else
                return Color.White;
        }

        private string tower
        {
            get
            {
                return "A";
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
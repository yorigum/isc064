using System;
using System.Drawing;
using System.IO;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
    public partial class TabelStokC : System.Web.UI.Page
    {
        private static string Conn = "server=.; uid=batavianet;pwd=iNDigo100; database=ISC064_MARKETINGJUAL";
        protected void Page_Load(object sender, System.EventArgs e)
        {
            PrintHeader();
            Print("08");
            PrintHeader2();

            PrintHeader4(); //Kosong

            Print2("07");
            Print2("06");
            Print2("05");
            Print2("03");
            Print2("02");
            PrintHeader3();

            PrintHeader4(); //Kosong

            PrintHeader5();
            Print3("01");
            PrintHeader6();

            fillLegend();
        }

        private void PrintHeader()
        {
            TableRow r = new TableRow();
            TableCell c;

            c = new TableCell(); c.CssClass = "h"; c.Text = "FLOOR";
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "POOL VIEW"; c.ColumnSpan = 4;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "CITY VIEW";
            c.ColumnSpan = 9;
            r.Cells.Add(c);


            tb.Rows.Add(r);
        }
        private void PrintHeader2()
        {
            TableRow r = new TableRow();
            TableCell c;

            c = new TableCell(); c.CssClass = "h"; c.Text = "&nbsp;";
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "PH A - 3BR";
            c.Attributes["style"] += "background-color:#00ccff;";
            c.ColumnSpan = 4;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "PH C -2BR";
            c.Attributes["style"] += "background-color:#00ffff;";
            c.ColumnSpan = 2;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "PH B -3BR";
            c.Attributes["style"] += "background-color:#9bc2e6;";
            c.ColumnSpan = 5;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "PH D - 1BR";
            c.Attributes["style"] += "background-color:#d0cece;";
            c.ColumnSpan = 2;
            r.Cells.Add(c);


            tb.Rows.Add(r);
        }
        private void PrintHeader3()
        {
            TableRow r = new TableRow();
            TableCell c;

            c = new TableCell(); c.CssClass = "h"; c.Text = "&nbsp;";
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "3BR";
            c.Attributes["style"] += "background-color:#33cccc;";
            c.ColumnSpan = 2;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "1BR-B";
            c.Attributes["style"] += "background-color:#a9d08e;width:80px;";
            c.ColumnSpan = 2;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "2BR";
            c.Attributes["style"] += "background-color:#fff2cc;width:80px;";
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "1BR-A";
            c.Attributes["style"] += "background-color:#e2efda;";
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "2BR";
            c.Attributes["style"] += "background-color:#fff2cc;width:80px;";
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "1BR-A";
            c.Attributes["style"] += "background-color:#e2efda;";
            c.ColumnSpan = 2;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "2BR";
            c.Attributes["style"] += "background-color:#fff2cc;width:80px;";
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "1BR-A";
            c.Attributes["style"] += "background-color:#e2efda;";
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "2BR";
            c.Attributes["style"] += "background-color:#fff2cc;width:80px;";
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "1BR-A";
            c.Attributes["style"] += "background-color:#e2efda;";
            r.Cells.Add(c);


            tb.Rows.Add(r);
        }
        private void PrintHeader4()
        {
            TableRow r = new TableRow();
            TableCell c;

            c = new TableCell(); c.CssClass = "h"; c.Text = "&nbsp;";
            c.ColumnSpan = 14;
            r.Cells.Add(c);

            tb.Rows.Add(r);
        }
        private void PrintHeader5()
        {
            TableRow r = new TableRow();
            TableCell c;

            c = new TableCell(); c.CssClass = "h"; c.Text = "&nbsp;";
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "POOL VIEW"; c.ColumnSpan = 6;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "POOL & CITY VIEW";
            c.ColumnSpan = 4;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "CITY VIEW";
            c.ColumnSpan = 3;
            r.Cells.Add(c);

            tb.Rows.Add(r);
        }
        private void PrintHeader6()
        {
            TableRow r = new TableRow();
            TableCell c;

            c = new TableCell(); c.CssClass = "h"; c.Text = "&nbsp;";
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "B1 - 3BR"; c.ColumnSpan = 6;
            c.Attributes["style"] += "background-color:#bfbfbf;";
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "A1 - 4BR";
            c.Attributes["style"] += "background-color:#808080;";
            c.ColumnSpan = 4;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "C - 3BR";
            c.Attributes["style"] += "background-color:#339966;";
            c.ColumnSpan = 3;
            r.Cells.Add(c);

            tb.Rows.Add(r);
        }

        private void Print(string Lantai)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = new TableCell();
            c.Text = "PH";
            c.CssClass = "lt";
            r.Cells.Add(c);

            string[] Nomor = { "02", "03", "05", "01", "07", "06"};

            for (int i = 0; i < Nomor.Length; i++)
            {
                int Kolom = 0;
                if (Nomor[i] == "01")
                {
                    Kolom = 3;
                }
                else
                {
                    Kolom = 2;
                }

                c = new TableCell();
                string x = Href(NoStock("C/" + Lantai + "/" + Nomor[i]));
                c.Text = "<a href='" + x + "'>" + "C/" + Lantai + "/" + Nomor[i] + "</a>";
                c.BackColor = status("C/" + Lantai + "/" + Nomor[i]);
                c.ColumnSpan = Kolom;
                r.Cells.Add(c);
            }

            tb.Rows.Add(r);
        }
        private void Print2(string Lantai)
        {
            TableRow r = new TableRow();
            TableCell c;

            string[] Nomor = { "05", "06", "07", "08", "09", "10", "03", "02", "01", "16", "15", "12", "11" };

            c = new TableCell();
            c.Text = Lantai;
            c.CssClass = "lt";
            r.Cells.Add(c);

            for (int i = 0; i < Nomor.Length; i++)
            {
                c = new TableCell();
                string x = Href(NoStock("C/" + Lantai + "/" + Nomor[i]));
                c.Text = "<a href='" + x + "'>" + "C/" + Lantai + "/" + Nomor[i] + "</a>";
                c.BackColor = status("C/" + Lantai + "/" + Nomor[i]);
                r.Cells.Add(c);
            }

            tb.Rows.Add(r);
        }
        private void Print3(string Lantai)
        {
            TableRow r = new TableRow();
            TableCell c;

            string[] Nomor = { "02", "03", "05", "01" };

            c = new TableCell();
            c.Text = "TV";
            c.CssClass = "lt";
            r.Cells.Add(c);

            for (int i = 0; i < Nomor.Length; i++)
            {
                int Kolom = 0;
                if (Nomor[i] == "05")
                {
                    Kolom = 4;
                }
                else
                {
                    Kolom = 3;
                }

                c = new TableCell();
                string x = Href(NoStock("C/" + Lantai + "/" + Nomor[i]));
                c.Text = "<a href='" + x + "'>" + "C/" + Lantai + "/" + Nomor[i] + "</a>";
                c.BackColor = status("C/" + Lantai + "/" + Nomor[i]);
                c.ColumnSpan = Kolom;
                r.Cells.Add(c);
            }

            tb.Rows.Add(r);
        }

        private string NoStock(string unit)
        {
            string x = "";
            x = Db.SingleString("SELECT ISNULL(NoStock, '') FROM MS_UNIT WHERE NoUnit = '" + unit + "'");
            return x;
        }

        private string Href(string NoStock)
        {
            string x = "";

            string strSql = "SELECT Status, NoUnit FROM ISC064_MARKETINGJUAL..MS_UNIT WHERE NoStock = '" + NoStock + "'";
            DataTable rs = Db.Rs(strSql, Conn);
            if (rs.Rows.Count != 0)
            {
                if (rs.Rows[0]["Status"].ToString() == "B")
                {
                    int c = Db.SingleInteger("SELECT COUNT(*) FROM ISC064_MARKETINGJUAL..MS_KONTRAK"
                        + " WHERE Status = 'A' AND NoStock = '" + NoStock + "'", Conn);
                    if (c != 0)
                    {
                        string NoKontrak = Db.SingleString("SELECT TOP 1 NoKontrak FROM ISC064_MARKETINGJUAL..MS_KONTRAK WHERE Status = 'A' AND NoStock = '" + NoStock + "'", Conn);

                        int NoTTS = 0;
                        DataTable tts = Db.Rs("SELECT TOP 1 NoTTS FROM ISC064_FINANCEAR..MS_TTS WHERE Ref = '" + NoKontrak + "' AND Status <> 'VOID' ORDER BY NoTTS ASC", Conn);
                        if (tts.Rows.Count != 0)
                            NoTTS = Convert.ToInt32(tts.Rows[0][0]);

                        x = "TabelStok3.aspx?NoKontrak=" + NoKontrak + "&NoTTS=" + NoTTS; //sold
                    }
                    else
                        x = "javascript:popUnit(\"" + NoStock + "\")"; //hold internal
                }
                else if (rs.Rows[0]["Status"].ToString() == "H")
                {
                    int c = Db.SingleInteger("SELECT COUNT(*) FROM ISC064_MARKETINGJUAL..MS_HOLD"
                         + " WHERE Status = 'A' AND NoStock = '" + NoStock + "'", Conn);
                    if (c != 0)
                    {
                        string NoHold = Db.SingleString("SELECT TOP 1 NoHOLD FROM ISC064_MARKETINGJUAL..MS_HOLD WHERE Status = 'A' AND NoStock = '" + NoStock + "'", Conn);
                        x = "HoldUnitDaftarDone.aspx?NoHold=" + NoHold + "";
                    }
                    else
                    {
                        x = " ";
                    }
                }
                else
                {
                    int c = Db.SingleInteger("SELECT COUNT(*) FROM ISC064_MARKETINGJUAL..MS_RESERVASI"
                        + " WHERE Status = 'A' AND NoStock = '" + NoStock + "'", Conn);
                    if (c != 0)
                        x = "TabelStok2.aspx?NoStock=" + NoStock;//"KontrakDaftar2.aspx?NoStock=" + rs.Rows[0]["NoStock"]; //booked
                    else
                    {
                        if (Act.SecLevel == "AG")
                            x = "javascript:popUnit(\"" + NoStock + "\")"; //available
                        else
                            x = "TabelStok2.aspx?NoStock=" + NoStock; //available
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
                            color = System.Drawing.ColorTranslator.FromHtml("#ff0000");
                        else
                            color = System.Drawing.ColorTranslator.FromHtml("#ff0000");

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
                    color = Color.Red;
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
            addLegendColor("#3cc051", "<b>Available</b>"); //green

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
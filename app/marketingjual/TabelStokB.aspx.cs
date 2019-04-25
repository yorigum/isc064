using System;
using System.Drawing;
using System.IO;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
    public partial class TabelStokB : System.Web.UI.Page
    {
        private static string Conn = "server=.; uid=batavianet;pwd=iNDigo100; database=ISC064_MARKETINGJUAL";
        protected void Page_Load(object sender, System.EventArgs e)
        {
            PrintHeader();
            Print("8");
            PrintHeader2();

            PrintHeader3();
            Print2("7");
            Print2("6");
            Print2("5");
            Print2("3");
            Print2("2");
            PrintHeader4();

            PrintHeader5();
            Print3("1");
            PrintHeader6();

            fillLegend();
        }
        private void PrintHeader()
        {
            TableRow r = new TableRow();
            TableCell c;

            c = new TableCell(); c.CssClass = "h"; c.Text = "FLOOR";
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "POOL & CITY VIEW"; c.ColumnSpan = 5;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "POOL VIEW"; c.ColumnSpan = 8;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "CITY VIEW"; c.ColumnSpan = 4;
            r.Cells.Add(c);

            tb.Rows.Add(r);
        }
        private void PrintHeader2()
        {
            TableRow r = new TableRow();
            TableCell c;

            c = new TableCell(); c.CssClass = "h"; c.Text = "&nbsp;";
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "PH F - 5BR";
            c.Attributes["style"] += "background-color:#2f75b5;";
            c.ColumnSpan = 5;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "PH A - 3BR";
            c.Attributes["style"] += "background-color:#bdd7ee;";
            c.ColumnSpan = 8;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "PH E - 1BR";
            c.Attributes["style"] += "background-color:#d9d9d9;";
            c.ColumnSpan = 4;
            r.Cells.Add(c);

            tb.Rows.Add(r);
        }
        private void PrintHeader3()
        {
            TableRow r = new TableRow();
            TableCell c;

            c = new TableCell(); c.CssClass = "h"; c.Text = "&nbsp;";
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "POOL VIEW";
            c.ColumnSpan = 5;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "CITY VIEW";
            c.ColumnSpan = 12;
            r.Cells.Add(c);

            tb.Rows.Add(r);
        }
        private void PrintHeader4()
        {
            TableRow r = new TableRow();
            TableCell c;

            c = new TableCell(); c.CssClass = "h"; c.Text = "&nbsp;";
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "2BR";
            c.Attributes["style"] += "background-color:#fce4d6;";
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "1BR-A";
            c.Attributes["style"] += "background-color:#e2efda;width:80px;";
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "3BR";
            c.Attributes["style"] += "background-color:#33cccc;";
            c.ColumnSpan = 3;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "1BR-A";
            c.Attributes["style"] += "background-color:#e2efda;width:80px;";
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "STUDIO";
            c.Attributes["style"] += "background-color:#ddebf7;";
            c.ColumnSpan = 6;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "1BR-A";
            c.Attributes["style"] += "background-color:#e2efda;width:80px;";
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "STUDIO";
            c.Attributes["style"] += "background-color:#ddebf7;";
            c.ColumnSpan = 3;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "1BR-A";
            c.Attributes["style"] += "background-color:#e2efda;width:80px;";
            r.Cells.Add(c);

            tb.Rows.Add(r);
        }
        private void PrintHeader5()
        {
            TableRow r = new TableRow();
            TableCell c;

            c = new TableCell(); c.CssClass = "h"; c.Text = "&nbsp;";
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "POOL VIEW"; c.ColumnSpan = 5;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "POOL & CITY VIEW"; c.ColumnSpan = 8;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "CITY VIEW"; c.ColumnSpan = 4;
            r.Cells.Add(c);

            tb.Rows.Add(r);
        }
        private void PrintHeader6()
        {
            TableRow r = new TableRow();
            TableCell c;

            c = new TableCell();
            c = new TableCell(); c.CssClass = "h"; c.Text = "&nbsp;";
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "B2-3BR"; c.ColumnSpan = 5;
            c.Attributes["style"] += "background-color:#c6e0b4;";
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "A2-4BR";
            c.Attributes["style"] += "background-color:#bfbfbf;";
            c.ColumnSpan = 8;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "D-2BR";
            c.Attributes["style"] += "background-color:#33cc33;";
            c.ColumnSpan = 4;
            r.Cells.Add(c);

            tb.Rows.Add(r);
        }

        private void Print(string Lantai)
        {
            TableRow r = new TableRow();
            TableCell c;

            string[] Nomor = { "06", "01", "02", "05", "03" };

            c = new TableCell();
            c.Text = "PH";
            c.CssClass = "lt";
            r.Cells.Add(c);

            for (int i = 0; i < Nomor.Length; i++)
            {
                int Kolom = 0;
                if (Nomor[i] == "06")
                {
                    Kolom = 5;
                }
                else if (Nomor[i] == "01" || Nomor[i] == "02")
                {
                    Kolom = 4;
                }
                else
                {
                    Kolom = 2;
                }

                c = new TableCell();
                string x = Href(NoStock("B/" + Lantai + "/" + Nomor[i]));
                c.Text = "<a href='" + x + "' style='text-decoration:none;color:black;'>" + "B/" + Lantai + "/" + Nomor[i] + "</a>";
                c.BackColor = status("B/" + Lantai + "/" + Nomor[i]);
                c.ColumnSpan = Kolom;
                r.Cells.Add(c);
            }

            tb.Rows.Add(r);
        }
        private void Print2(string Lantai)
        {
            TableRow r = new TableRow();
            TableCell c;

            string[] Nomor = { "19", "20", "01", "02", "03", "18", "17", "16", "15", "12", "11", "10", "09", "08", "07", "06", "05" };

            c = new TableCell();
            c.Text = Lantai;
            c.CssClass = "lt";
            r.Cells.Add(c);

            for (int i = 0; i < Nomor.Length; i++)
            {
                c = new TableCell();
                c.Text = Nomor[i];
                c.CssClass = "lt";
                string x = Href(NoStock("B/" + Lantai + "/" + Nomor[i]));
                c.Text = "<a href='" + x + "' style='text-decoration:none;color:black;'>" + "B/" + Lantai + "/" + Nomor[i] + "</a>";
                c.BackColor = status("B/" + Lantai + "/" + Nomor[i]);
                r.Cells.Add(c);
            }

            tb.Rows.Add(r);
        }
        private void Print3(string Lantai)
        {
            TableRow r = new TableRow();
            TableCell c;

            string[] Nomor = { "01", "02", "05", "03" };

            c = new TableCell();
            c.Text = "TV";
            c.CssClass = "lt";
            r.Cells.Add(c);

            for (int i = 0; i < Nomor.Length; i++)
            {
                int Kolom = 0;
                if (Nomor[i] == "01")
                {
                    Kolom = 2;
                }
                else if (Nomor[i] == "02")
                {
                    Kolom = 3;
                }
                else if (Nomor[i] == "05")
                {
                    Kolom = 8;
                }
                else
                {
                    Kolom = 4;
                }

                c = new TableCell();
                string x = Href(NoStock("B/" + Lantai + "/" + Nomor[i]));
                c.Text = "<a href='" + x + "' style='text-decoration:none;color:black;'>" + "B/" + Lantai + "/" + Nomor[i] + "</a>";
                c.BackColor = status("B/" + Lantai + "/" + Nomor[i]);
                c.ColumnSpan = Kolom;
                r.Cells.Add(c);
            }

            tb.Rows.Add(r);
        }
        private string NoStock(string unit)
        {
            string x = "";
            x = Db.SingleString("SELECT ISNULL(NoStock, '') FROM ISC064_MARKETINGJUAL..MS_UNIT WHERE NoUnit = '" + unit + "'");
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
            string strSql = "SELECT Status,Jenis"
                + " FROM ISC064_MARKETINGJUAL..MS_UNIT "
                + " WHERE NoUnit = '" + unit + "'";
            DataTable rs = Db.Rs(strSql);
            if (rs.Rows.Count != 0)
            {
                string Status = rs.Rows[0]["Status"].ToString();
                Color color = new Color();
                int Count = 0;
                Count = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM [NUP03]..CustomerNupDetail WHERE NoUnit= '" + unit + "'");
                int Count2 = 0;
                Count2 = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM [NUP03]..CustomerNUPDetail WHERE NoUnit='" + unit + "' ");
                if (Count == 1 || Count2 != 0)
                {
                    color = System.Drawing.ColorTranslator.FromHtml("#ff0000");
                }
                else
                {
                    if (Status == "A")
                    {
                        string Jenis = rs.Rows[0]["Jenis"].ToString();
                        //Lantai 8
                        if (Jenis == "C-2BR")
                            color = System.Drawing.ColorTranslator.FromHtml("#bdd7ee");
                        else if (Jenis == "LUX-5BR")
                            color = System.Drawing.ColorTranslator.FromHtml("#33cccc");
                        else if (Jenis == "D-1BR")
                            color = System.Drawing.ColorTranslator.FromHtml("#00b0f0");
                        else if (Jenis == "B-3BR")
                            color = System.Drawing.ColorTranslator.FromHtml("#9bc2e6");
                        else if (Jenis == "PH A - 3BR")
                            color = System.Drawing.ColorTranslator.FromHtml("#bdd7ee");
                        else if (Jenis == "PH E - 1BR")
                            color = System.Drawing.ColorTranslator.FromHtml("#d9d9d9");
                        else if (Jenis == "PH F - 5BR")
                            color = System.Drawing.ColorTranslator.FromHtml("#2f75b5");
                        //Lantai 8
                        //Lantai 7-2
                        else if (Jenis == "1BR-A")
                            color = System.Drawing.ColorTranslator.FromHtml("#e2efda");
                        else if (Jenis == "2BR")
                            color = System.Drawing.ColorTranslator.FromHtml("#fce4d6");
                        else if (Jenis == "1BR-B")
                            color = System.Drawing.ColorTranslator.FromHtml("#a9d08e");
                        else if (Jenis == "3BR")
                            color = System.Drawing.ColorTranslator.FromHtml("#33cccc");
                        else if (Jenis == "STD")
                            color = System.Drawing.ColorTranslator.FromHtml("#ddebf7");
                        //Lantai 7-2
                        //Lantai 1
                        else if (Jenis == "B1-3BR")
                            color = System.Drawing.ColorTranslator.FromHtml("#bfbfbf");
                        else if (Jenis == "C-3BR")
                            color = System.Drawing.ColorTranslator.FromHtml("#339966");
                        else if (Jenis == "A1-4BR")
                            color = System.Drawing.ColorTranslator.FromHtml("#808080");
                        else if (Jenis == "B2-3BR")
                            color = System.Drawing.ColorTranslator.FromHtml("#c6e0b4");
                        else if (Jenis == "A2-4BR")
                            color = System.Drawing.ColorTranslator.FromHtml("#bfbfbf");
                        else if (Jenis == "D-2BR")
                            color = System.Drawing.ColorTranslator.FromHtml("#33cc33");
                        else
                            color = System.Drawing.ColorTranslator.FromHtml("#000");
                    }
                    else if (Status == "H")
                        color = System.Drawing.ColorTranslator.FromHtml("#ff0000");
                    else if (Status == "B")
                        color = System.Drawing.ColorTranslator.FromHtml("#ff0000");
                    else
                        color = System.Drawing.ColorTranslator.FromHtml("#000");
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
                                                      //addLegendColor("#000", "<b>Hold</b>"); //black


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
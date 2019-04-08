using System;
using System.Drawing;
using System.IO;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
    public partial class TabelStokS : System.Web.UI.Page
    {
        private static string Conn = "server=.; uid=batavianet;pwd=iNDigo100; database=ISC064_MARKETINGJUAL";
        protected void Page_Load(object sender, System.EventArgs e)
        {
            PrintHeader();
            Print("A");
            PrintHeader();
            Print("B");
            fillLegend();
        }
        private void PrintHeader()
        {
            TableRow r = new TableRow();
            TableCell c;

            c = new TableCell(); c.CssClass = "h"; c.Text = "CORNER";
            c.Attributes["style"] += "background-color:#ff6699;";
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "&nbsp;"; c.ColumnSpan = 8;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "CORNER";
            c.Attributes["style"] += "background-color:#ff6699;";
            r.Cells.Add(c);


            tb.Rows.Add(r);
        }

        private void Print(string Lantai)
        {
            TableRow r = new TableRow();
            TableCell c;

            string[] Nomor = { "01", "02", "03", "05", "06", "07", "08", "09", "10", "11" };

            for (int i = 0; i < Nomor.Length; i++)
            {
                c = new TableCell();
                string x = Href(NoStock("S/" + Lantai + "/" + Nomor[i]));
                c.Text = "<a href='" + x + "' style='text-decoration:none;color:black;'>" + "" + Lantai + "/" + Nomor[i] + "</a>";
                c.BackColor = status("S/" + Lantai + "/" + Nomor[i]);
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
                Count = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM [NUP02]..CustomerNupDetail WHERE NoUnit= '" + unit + "'");
                int Count2 = 0;
                Count2 = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM [NUP02]..CustomerNUPDetail WHERE NoUnit='" + unit + "' ");
                if (Count == 1 || Count2 != 0)
                {
                    color = System.Drawing.ColorTranslator.FromHtml("#ff0000");
                }
                else
                {
                    if (Status == "A")
                    {
                        string Jenis = rs.Rows[0]["Jenis"].ToString();
                        color = System.Drawing.ColorTranslator.FromHtml("#3cc051");

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
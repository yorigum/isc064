using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LAUNCHING
{
    public partial class ShowPetaTV1 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            PrintKet();
            Print("UPPER GROUND");
            PrintKet2();
            Print2("GROUND");
            PrintKet3();

            fillLegend();
        }

        private void Print(string Lantai)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = new TableCell();
            c.Text = Lantai;
            c.Attributes["style"] = "font-size:8pt";
            c.CssClass = "lt";
            c.RowSpan = 2;
            r.Cells.Add(c);

            string[] nou = { "01", "02", "03", "05", "06", "07", "08", "09", "10", "11", "12", "15", "16", "17", "18", "19", "20", "21", "22", "25", "26", "27", "28", "29", "30", "31" };
            for (int i = 0; i < nou.Length; i++)
            {
                string nounit = nou[i];
                string lantai2 = "";

                c = new TableCell();
                if (Lantai == "UPPER GROUND")
                    lantai2 = "AUG";

                string x = href(lantai2 + "" + nounit.PadLeft(2, '0'));
                if (x != "")
                {
                    c.Text = "<a>" + lantai2 + "" + nounit.PadLeft(2, '0') + "</a>";
                }
                else
                {
                    c.Text = "";// nounit;
                }
                string noUnit = lantai2 + "" + nounit.PadLeft(2, '0');
                string unit = Db.SingleString("SELECT NoUnit FROM MS_UNIT WHERE NoUnit='" + noUnit + "'");

                c.BackColor = status(lantai2 + "" + nounit.PadLeft(2, '0'));
                c.ToolTip = lantai2 + "" + nounit.PadLeft(2, '0');
                r.Cells.Add(c);
            }

            tb.Rows.Add(r);
        }

        private void Print2(string Lantai)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = new TableCell();
            c.Text = Lantai;
            c.CssClass = "lt";
            c.RowSpan = 2;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "&nbsp;";
            c.CssClass = "lt";
            c.ColumnSpan = 14;
            c.RowSpan = 2;
            r.Cells.Add(c);

            string[] nou = { "01", "02", "03", "05", "06", "07", "08", "09", "10", "11", "12", "15" };
            for (int i = 0; i < nou.Length; i++)
            {
                string nounit = nou[i];
                string lantai2 = "";

                c = new TableCell();
                if (Lantai == "GROUND")
                    lantai2 = "AG";

                string x = href(lantai2 + "" + nounit.PadLeft(2, '0'));
                if (x != "")
                {
                    c.Text = "<a>" + lantai2 + "" + nounit.PadLeft(2, '0') + "</a>";
                }
                else
                {
                    c.Text = lantai2 + "" + nounit.PadLeft(2, '0');// nounit;
                }
                string noUnit = lantai2 + "" + nounit.PadLeft(2, '0');
                string unit = Db.SingleString("SELECT NoUnit FROM MS_UNIT WHERE NoUnit='" + noUnit + "'");

                c.BackColor = status(lantai2 + "" + nounit.PadLeft(2, '0'));
                c.ToolTip = lantai2 + "" + nounit.PadLeft(2, '0');
                r.Cells.Add(c);
            }

            tb.Rows.Add(r);
        }

        private void PrintKet()
        {
            TableRow r = new TableRow();
            TableCell c;

            c = new TableCell();
            c.Text = "Lantai / View";
            c.CssClass = "ket";
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "CITY";
            c.CssClass = "ket";
            c.ColumnSpan = 12;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "FACILITY";
            c.CssClass = "ket";
            c.ColumnSpan = 14;
            r.Cells.Add(c);

            tb.Rows.Add(r);
        }

        private void PrintKet2()
        {
            TableRow r = new TableRow();
            TableCell c;

            string[] a = { "13", "13", "13", "13", "10", "10", "11A", "11A", "13", "13", "11", "11", "11", "11", "13", "13", "13", "13", "13", "13", "13", "13", "13", "13" ,"11", "11"};

            for (int i = 0; i < a.Length; i++)
            {
                c = new TableCell();
                c.Text = a[i];
                c.CssClass = "ket";
                r.Cells.Add(c);
            }

            tb.Rows.Add(r);
        }

        private void PrintKet3()
        {
            TableRow r = new TableRow();
            TableCell c;

            string[] a = { "13", "13", "13", "13", "13", "13", "13", "13", "13", "13", "13", "13"};

            for (int i = 0; i < a.Length; i++)
            {
                c = new TableCell();
                c.Text = a[i];
                c.CssClass = "ket";
                r.Cells.Add(c);
            }

            tb.Rows.Add(r);
        }

        private string href(string unit)
        {
            string x = "";

            string strSql = "SELECT Status, NoStock FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT WHERE NoUnit = '" + unit + "'";
            DataTable rs = Db.Rs(strSql);
            if (rs.Rows.Count != 0)
            {
                if (rs.Rows[0]["Status"].ToString() == "B")
                {
                    int c = Db.SingleInteger("SELECT COUNT(*) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK"
                        + " WHERE Status = 'A' AND NoUnit = '" + unit + "'");
                    if (c != 0)
                    {
                        string NoKontrak = Db.SingleString("SELECT TOP 1 NoKontrak FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE Status = 'A' AND NoUnit = '" + unit + "'");

                        int NoTTS = 0;
                        DataTable tts = Db.Rs("SELECT TOP 1 NoTTS FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS WHERE Ref = '" + NoKontrak + "' AND Status <> 'VOID' ORDER BY NoTTS ASC");
                        if (tts.Rows.Count != 0)
                            NoTTS = Convert.ToInt32(tts.Rows[0][0]);

                        x = ""; //sold
                    }
                    else
                    {
                        if (unit == "A0828")
                        {
                            x = "";
                        }
                        else
                            x = ""; ; //hold internal
                    }
                }
                else
                {
                    int c = Db.SingleInteger("SELECT COUNT(*) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_RESERVASI"
                        + " WHERE Status = 'A' AND NoUnit = '" + unit + "'");
                    if (c != 0)
                        x = unit;//"KontrakDaftar2.aspx?NoStock=" + rs.Rows[0]["NoStock"]; //booked
                    else
                    {
                        if (Act.SecLevel == "AG")
                            x = unit;// "javascript:popUnit(\"" + rs.Rows[0]["NoStock"] + "\")"; //available
                        else
                            x = unit; //available
                    }
                }
            }

            return x;
        }

        private Color status(string unit)
        {
            string strSql = "SELECT Status"
                + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT "
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

        protected void fillLegend()
        {
            legend.Text = "<table><tr>";

            addLegendColor("Yellow", "Select Unit");
            addLegendColor("Red", "Sold");
            addLegendColor("White", "Available");

            legend.Text += "</tr><table>";
        }

        protected void addLegendColor(string code, string name)
        {

            bool isValidCode = true;

            code = code.Trim();
            name = name.Trim();

            if (isValidCode)
            {
                legend.Text += "<td style='width:20px; padding:0 0 0 5px; border:solid 1px black; background-color:" + code + ";'>&nbsp;</td><td>&nbsp;:&nbsp;</td><td style='padding:0 5px 0 0;'>" + name + "</td>";
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

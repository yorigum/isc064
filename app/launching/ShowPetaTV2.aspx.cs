using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LAUNCHING
{
    public partial class ShowPetaTV2 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            PrintHeader();
            PrintType();
            PrintKet();
            //Print("28");
            //Print("27");
            //Print("26");
            //Print("25");
            //Print("23");
            //Print("22");
            //Print("21");
            //Print("20");
            //Print("19");
            //Print("18");
            //Print("17");
            //Print("16");
            //Print("15");
            //Print("12");
            //Print("11");
            //Print("10");
            //Print("09");
            //Print("08");
            //Print("07");
            //Print("06");
            //Print("05");
            //Print("03");
            //Print("02");
            string[] lt = { "16", "15", "12", "11", "10", "09", "08", "07", "06", "05", "03", "02" };
            for (int i = 0; i < lt.Length; i++)
            {
                Print(lt[i]);
            }

            fillLegend();
        }

        private void PrintHeader()
        {
            TableRow r = new TableRow();
            TableCell c;

            c = new TableCell(); c.CssClass = "h"; c.Text = "No"; c.Width = 50; c.Height = 30;
            c.BackColor = Color.White; c.ForeColor = Color.Black;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "01"; c.Width = 50; c.Height = 30;
            c.BackColor = Color.White; c.ForeColor = Color.Black;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "02"; c.Width = 50;
            c.BackColor = Color.White; c.ForeColor = Color.Black;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "03"; c.Width = 50;
            c.BackColor = Color.White; c.ForeColor = Color.Black;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "05"; c.Width = 50;
            c.BackColor = Color.White; c.ForeColor = Color.Black;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "06"; c.Width = 50;
            c.BackColor = Color.White; c.ForeColor = Color.Black;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "07"; c.Width = 50;
            c.BackColor = Color.White; c.ForeColor = Color.Black;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "08"; c.Width = 50;
            c.BackColor = Color.White; c.ForeColor = Color.Black;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "09"; c.Width = 50;
            c.BackColor = Color.White; c.ForeColor = Color.Black;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "10"; c.Width = 50;
            c.BackColor = Color.White; c.ForeColor = Color.Black;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "11"; c.Width = 50;
            c.BackColor = Color.White; c.ForeColor = Color.Black;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "12"; c.Width = 50;
            c.BackColor = Color.White; c.ForeColor = Color.Black;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "15"; c.Width = 50;
            c.BackColor = Color.White; c.ForeColor = Color.Black;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "16"; c.Width = 50;
            c.BackColor = Color.White; c.ForeColor = Color.Black;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "17"; c.Width = 50;
            c.BackColor = Color.White; c.ForeColor = Color.Black;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "18"; c.Width = 50;
            c.BackColor = Color.White; c.ForeColor = Color.Black;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "19"; c.Width = 50;
            c.BackColor = Color.White; c.ForeColor = Color.Black;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "20"; c.Width = 50;
            c.BackColor = Color.White; c.ForeColor = Color.Black;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "21"; c.Width = 50;
            c.BackColor = Color.White; c.ForeColor = Color.Black;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "22"; c.Width = 50;
            c.BackColor = Color.White; c.ForeColor = Color.Black;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "23"; c.Width = 50;
            c.BackColor = Color.White; c.ForeColor = Color.Black;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "25"; c.Width = 50;
            c.BackColor = Color.White; c.ForeColor = Color.Black;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "26"; c.Width = 50;
            c.BackColor = Color.White; c.ForeColor = Color.Black;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "27"; c.Width = 50;
            c.BackColor = Color.White; c.ForeColor = Color.Black;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "28"; c.Width = 50;
            c.BackColor = Color.White; c.ForeColor = Color.Black;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "29"; c.Width = 50;
            c.BackColor = Color.White; c.ForeColor = Color.Black;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "30"; c.Width = 50;
            c.BackColor = Color.White; c.ForeColor = Color.Black;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "31"; c.Width = 50;
            c.BackColor = Color.White; c.ForeColor = Color.Black;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "32"; c.Width = 50;
            c.BackColor = Color.White; c.ForeColor = Color.Black;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "33"; c.Width = 50;
            c.BackColor = Color.White; c.ForeColor = Color.Black;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "35"; c.Width = 50;
            c.BackColor = Color.White; c.ForeColor = Color.Black;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "36"; c.Width = 50;
            c.BackColor = Color.White; c.ForeColor = Color.Black;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "37"; c.Width = 50;
            c.BackColor = Color.White; c.ForeColor = Color.Black;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "38"; c.Width = 50;
            c.BackColor = Color.White; c.ForeColor = Color.Black;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "39"; c.Width = 50;
            c.BackColor = Color.White; c.ForeColor = Color.Black;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "40"; c.Width = 50;
            c.BackColor = Color.White; c.ForeColor = Color.Black;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "41"; c.Width = 50;
            c.BackColor = Color.White; c.ForeColor = Color.Black;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "42"; c.Width = 50;
            c.BackColor = Color.White; c.ForeColor = Color.Black;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "43"; c.Width = 50;
            c.BackColor = Color.White; c.ForeColor = Color.Black;
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
            c.Height = 30;
            c.Width = 70;
            r.Cells.Add(c);

            for (int i = 1; i < tb.Rows[0].Cells.Count; i++)
            {
                string nounit = tb.Rows[0].Cells[i].Text;

                c = new TableCell();

                string x = tower + Lantai + nounit;//href(tower + Lantai + nounit);
                string Unit2 = tower + Lantai + nounit;
                if (x != "")
                {
                    
                        c.Text = "<a>" + tower + Lantai + nounit + "</a>";
                }
                else
                {
                    c.Text = "";// nounit;
                }
                string noUnit = tower + Lantai + nounit;
                string unit = Db.SingleString("SELECT NoUnit FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT WHERE NoUnit='" + noUnit + "'");

                c.BackColor = status(tower + Lantai + nounit);
                c.ToolTip = unit;
                c.Attributes["style"] = "1px dotted black";
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
            c.ColumnSpan = 10;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "Swimming Pool & Jakarta City";
            c.CssClass = "ket";
            c.ColumnSpan = 10;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "Swimming Pool";
            c.CssClass = "ket";
            c.BackColor = Color.SkyBlue;
            c.ColumnSpan = 10;
            r.Cells.Add(c);

            tb.Rows.Add(r);
        }
        private void PrintType()
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
            c.ColumnSpan = 5;
            c.CssClass = "ket2";
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
            c.CssClass = "ket2";
            c.ColumnSpan = 2;
            r.Cells.Add(c);

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

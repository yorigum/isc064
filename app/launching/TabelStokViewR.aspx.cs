using System;
using System.Drawing;
using System.IO;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.NUP
{
    public partial class TabelStokViewR : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {

            spanlokasi.InnerText = "RUKO";
            fillLegend();
            PrintHeader();
            PrintLantai();
        }

        private void PrintLantai()
        {
            DataTable rsUnit = Db.Rs("SELECT Distinct(Lantai) FROM MS_UNIT WHERE Lokasi='" + Lokasi + "' ORDER BY Lantai DESC");
            for (int i = 0; i < rsUnit.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                string NoUnit = rsUnit.Rows[i]["Lantai"].ToString();
                Print(NoUnit);
            }
        }

        private void PrintHeader()
        {
            TableRow r2 = new TableRow();
            TableCell c2;

            c2 = new TableCell(); c2.CssClass = "h"; c2.Text = "Type."; c2.Width = 50; c2.Height = 30;
            c2.BackColor = Color.Gray; c2.ForeColor = Color.Black;
            r2.Cells.Add(c2);

            DataTable rsUnit = Db.Rs("SELECT DISTINCT(NomorUnit) FROM MS_UNIT WHERE Lokasi='" + Lokasi + "'");
            for (int i = 0; i < rsUnit.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                string NomorUnit = rsUnit.Rows[i]["NomorUnit"].ToString();
                string Jenis = Db.SingleString("SELECT Jenis FROM MS_UNIT WHERE NomorUnit='" + NomorUnit + "' AND Lokasi='" + Lokasi + "'");
                c2 = new TableCell(); c2.CssClass = "h"; c2.Text = Jenis; c2.Width = 50; c2.Height = 30;
                c2.BackColor = Color.Gray; c2.ForeColor = Color.Black;
                r2.Cells.Add(c2);
            }
            tb.Rows.Add(r2);

            TableRow r = new TableRow();
            TableCell c;

            c = new TableCell(); c.CssClass = "h"; c.Text = "LT/No."; c.Width = 50; c.Height = 30;
            c.BackColor = Color.Gray; c.ForeColor = Color.Black;
            r.Cells.Add(c);

            rsUnit = Db.Rs("SELECT DISTINCT(NomorUnit) FROM MS_UNIT WHERE Lokasi='" + Lokasi + "'");
            for (int i = 0; i < rsUnit.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                string NoUnit = rsUnit.Rows[i]["NomorUnit"].ToString();

                c = new TableCell(); c.CssClass = "h"; c.Text = NoUnit; c.Width = 50; c.Height = 30;
                c.BackColor = Color.Gray; c.ForeColor = Color.Black;
                r.Cells.Add(c);
            }
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

            for (int i = 1; i < tb.Rows[1].Cells.Count; i++)
            {
                string nounit = tb.Rows[1].Cells[i].Text;

                c = new TableCell();
                string NomorUnit = Lokasi + "/" + Lantai + "/" + nounit;
                string x = href(NomorUnit);
                if (x != "")
                {
                    c.Text = "<a href='" + x + "'>" + NomorUnit + "</a>";
                    c.BackColor = status(NomorUnit);
                    c.CssClass = "lt";
                    c.ToolTip = NomorUnit;
                }
                else
                {
                    c.Text = "";// nounit;
                    c.BackColor = status(NomorUnit);
                    c.CssClass = "lt";
                }


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

                        x = "";
                    }
                    else
                        x = "";
                }
                else
                {
                    int c = Db.SingleInteger("SELECT COUNT(*) FROM MS_RESERVASI"
                        + " WHERE Status = 'A' AND NoUnit = '" + unit + "'");
                    if (c != 0)
                        x = "TabelStok2.aspx?NoUnit=" + unit;//"KontrakDaftar2.aspx?NoStock=" + rs.Rows[0]["NoStock"]; //booked
                    else
                    {
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
                        color = Color.Red; //sold
                    }
                    else
                        color = Color.Blue; //hold internal
                }
                else if (rs.Rows[0]["Status"].ToString() == "H")
                {
                    string seclevel = Db.SingleString("SELECT SecLevel FROM " + Mi.DbPrefix + "SECURITY..USERNAME WHERE UserID = '" + Act.UserID + "'");
                    if (seclevel == "SALES")
                        color = Color.Red; //reserved
                    else
                        color = Color.Blue; //reserved
                }
                else
                {
                    int c = Db.SingleInteger("SELECT COUNT(*) FROM MS_RESERVASI"
                        + " WHERE Status = 'A' AND NoUnit = '" + unit + "'");
                    if (c != 0)
                    {
                        string seclevel = Db.SingleString("SELECT SecLevel FROM " + Mi.DbPrefix + "SECURITY..USERNAME WHERE UserID = '" + Act.UserID + "'");
                        if (seclevel == "SALES")
                            color = Color.Red; //reserved
                        else
                            color = Color.Blue; //reserved
                    }
                    else
                        color = Color.Cyan; //available

                }

                return color;
            }
            else
                return Color.White;
        }

        private string Lokasi
        {
            get
            {
                return Cf.Pk(Request.QueryString["Lokasi"]);
            }
        }

        protected void fillLegend()
        {
            legend.Text = "<table><tr>";

            addLegendColor("White", "Available");
            addLegendColor("Yellow", "Being Selected");
            addLegendColor("Orange", "Booked");
            addLegendColor("Blue", "Payment");
            addLegendColor("Red", "Sold");

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
        private string NoNUP
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoNUP"]);
            }
        }
        private string NamaTower
        {
            get
            {
                return Cf.Pk(Request.QueryString["Nama"]);
            }
        }
    }

}

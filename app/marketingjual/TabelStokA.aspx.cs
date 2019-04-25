using System;
using System.Drawing;
using System.IO;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
	public partial class TabelStokA : System.Web.UI.Page
	{
        private static string Conn = "server=.; uid=batavianet;pwd=iNDigo100; database=ISC064_MARKETINGJUAL";
        protected void Page_Load(object sender, System.EventArgs e)
		{
            PrintHeader();
            Print("8");
            PrintHeader2();

            PrintHeader4(); //Kosong

            Print2("7");
            Print2("6");
            Print2("5");
            Print2("3");
            Print2("2");
            PrintHeader3();

            PrintHeader4(); //Kosong
            PrintHeader4(); //Kosong

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

            c = new TableCell(); c.CssClass = "h"; c.Text = "POOL VIEW"; c.ColumnSpan = 6;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "CITY VIEW"; c.ColumnSpan = 7;
            r.Cells.Add(c);


            tb.Rows.Add(r);
        }
        private void PrintHeader2()
        {
            TableRow r = new TableRow();
            TableCell c;

            c = new TableCell(); c.CssClass = "h"; c.Text = "&nbsp;";
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "C-2BR";
            c.Attributes["style"] += "background-color:#bdd7ee;";
            c.ColumnSpan = 2;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "LUX-5BR";
            c.Attributes["style"] += "background-color:#33cccc;";
            c.ColumnSpan = 4;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "D-1BR";
            c.Attributes["style"] += "background-color:#00b0f0;";
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "B-3BR";
            c.Attributes["style"] += "background-color:#9bc2e6;";
            c.ColumnSpan = 6;
            r.Cells.Add(c);


            tb.Rows.Add(r);
        }
        private void PrintHeader3()
        {
            TableRow r = new TableRow();
            TableCell c;

            c = new TableCell(); c.CssClass = "h"; c.Text = "&nbsp;";
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "1BR-A";
            c.Attributes["style"] += "background-color:#e2efda;";
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "2BR";
            c.Attributes["style"] += "background-color:#fff2cc;";
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "1BR-B";
            c.Attributes["style"] += "background-color:#a9d08e;";
            c.ColumnSpan = 2;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "3BR";
            c.Attributes["style"] += "background-color:#009999;";
            c.ColumnSpan = 2;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "1BR-A";
            c.Attributes["style"] += "background-color:#e2efda;";
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "2BR";
            c.Attributes["style"] += "background-color:#fff2cc;";
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "1BR-A";
            c.Attributes["style"] += "background-color:#e2efda;";
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "2BR";
            c.Attributes["style"] += "background-color:#fff2cc;";
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "1BR-A";
            c.Attributes["style"] += "background-color:#e2efda;";
            c.ColumnSpan = 2;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "2BR";
            c.Attributes["style"] += "background-color:#fff2cc;";
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
        private void PrintHeader6()
        {
            TableRow r = new TableRow();
            TableCell c;

            c = new TableCell(); c.CssClass = "h"; c.Text = "&nbsp;";
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "B1 - 3BR"; c.ColumnSpan = 6;
            c.Attributes["style"] += "background-color:#bfbfbf;";
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "C - 3BR";
            c.Attributes["style"] += "background-color:#339966;";
            c.ColumnSpan = 3;
            r.Cells.Add(c);

            c = new TableCell(); c.CssClass = "h"; c.Text = "A1 - 4BR";
            c.Attributes["style"] += "background-color:#808080;";
            c.ColumnSpan = 4;
            r.Cells.Add(c);

            tb.Rows.Add(r);
        }
        private void Print(string Lantai)
        {
            TableRow r = new TableRow();
            TableCell c;

            string[] Nomor = { "03", "05", "02", "01", "06" };

            c = new TableCell();
            c.Text = "PH";
            c.CssClass = "lt";
            r.Cells.Add(c);

            for (int i = 0; i < Nomor.Length; i++)
            {
                int Kolom = 0;
                if (Nomor[i] == "03")
                {
                    Kolom = 2;
                }
                else if (Nomor[i] == "05")
                {
                    Kolom = 4;
                }
                else if (Nomor[i] == "01" || Nomor[i] == "06")
                {
                    Kolom = 3;
                }
                else
                    Kolom = 0;

                c = new TableCell();
                string x = Href(NoStock("A/" + Lantai + "/" + Nomor[i]));
                c.Text = "<a href='" + x + "' style='text-decoration:none;color:black;'>" + "A/" + Lantai + "/" + Nomor[i] + "</a>";
                c.BackColor = status("A/" + Lantai + "/" + Nomor[i]);
                c.ColumnSpan = Kolom;
                r.Cells.Add(c);
            }

            tb.Rows.Add(r);
        }
        private void Print2(string Lantai)
        {
            TableRow r = new TableRow();
            TableCell c;

            string[] Nomor = { "06", "07", "08", "09", "10", "11", "05", "03", "02", "01", "16", "15", "12" };

            c = new TableCell();
            c.Text = Lantai;
            c.CssClass = "lt";
            r.Cells.Add(c);

            for (int i = 0; i < Nomor.Length; i++)
            {
                c = new TableCell();
                string x = Href(NoStock("A/" + Lantai + "/" + Nomor[i]));
                c.Text = "<a href='" + x + "' style='text-decoration:none;color:black;'>" + "A/" + Lantai + "/" + Nomor[i] + "</a>";
                c.BackColor = status("A/" + Lantai + "/" + Nomor[i]);
                r.Cells.Add(c);
            }

            tb.Rows.Add(r);
        }
        private void Print3(string Lantai)
        {
            TableRow r = new TableRow();
            TableCell c;

            string[] Nomor = { "01", "02", "03", "05" };

            c = new TableCell();
            c.Text = Lantai;
            c.CssClass = "lt";
            r.Cells.Add(c);

            for (int i = 0; i < Nomor.Length; i++)
            {
                int Kolom = 0;
                if (Nomor[i] == "05")
                {
                    Kolom = 5;
                }
                else
                {
                    Kolom = 3;
                }

                c = new TableCell();
                string x = Href(NoStock("A/" + Lantai + "/" + Nomor[i]));
                c.Text = "<a href='" + x + "' style='text-decoration:none;color:black;'>" + "A/" + Lantai + "/" + Nomor[i] + "</a>";
                c.BackColor = status("A/" + Lantai + "/" + Nomor[i]);
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
                Count2 = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM [NUP03]..CustomerNUPDetail WHERE NoUnit='" + unit + "'");
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
                        //Lantai 8
                        //Lantai 7-2
                        else if (Jenis == "1BR-A")
                            color = System.Drawing.ColorTranslator.FromHtml("#e2efda");
                        else if (Jenis == "2BR")
                            color = System.Drawing.ColorTranslator.FromHtml("#fff2cc");
                        else if (Jenis == "1BR-B")
                            color = System.Drawing.ColorTranslator.FromHtml("#a9d08e");
                        else if (Jenis == "3BR")
                            color = System.Drawing.ColorTranslator.FromHtml("#009999");
                        //Lantai 7-2
                        //Lantai 1
                        else if (Jenis == "B1-3BR")
                            color = System.Drawing.ColorTranslator.FromHtml("#bfbfbf");
                        else if (Jenis == "C-3BR")
                            color = System.Drawing.ColorTranslator.FromHtml("#339966");
                        else if (Jenis == "A1-4BR")
                            color = System.Drawing.ColorTranslator.FromHtml("#808080");
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

            legend.Text += "</tr><table>";
        }

        protected void addLegendColor(string code, string name)
        {

            bool isValidCode = true;

            code = code.Trim();
            name = name.Trim();

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
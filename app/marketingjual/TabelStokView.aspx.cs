using System;
using System.Drawing;
using System.IO;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
    public partial class TabelStokView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            spanlokasi.InnerText = NamaTower;
            fillLegend();
            PrintHeader();
            PrintLantai();

            Db.Execute("EXEC [spAutoExpHoldUnit]");
        }

        private void PrintLantai()
        {
            DataTable rsUnit = Db.Rs("SELECT Distinct(Lantai) FROM MS_UNIT WHERE Lokasi='" + Lokasi + "' AND Project= '" + Project + "' ORDER BY Lantai DESC");
            for (int i = 0; i < rsUnit.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                string NoUnit = rsUnit.Rows[i]["Lantai"].ToString();
                Print(NoUnit);
            }
        }

        private void PrintHeader()
        {

            TableRow r = new TableRow();
            TableCell c;

            c = new TableCell(); c.CssClass = "h"; c.Text = "LT./No."; c.Width = 50; c.Height = 30;
            c.BackColor = Color.Gray; c.ForeColor = Color.White;
            r.Cells.Add(c);

            DataTable rsUnit = Db.Rs("SELECT DISTINCT(Nomor) as Nomor FROM MS_UNIT WHERE Lokasi='" + Lokasi + "' AND Project= '" + Project + "' ORDER BY Nomor ");
            for (int i = 0; i < rsUnit.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                string NoUnit = rsUnit.Rows[i]["Nomor"].ToString();

                c = new TableCell(); c.CssClass = "h"; c.Text = NoUnit; c.Width = 50; c.Height = 30;
                c.BackColor = Color.Gray; c.ForeColor = Color.White;
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

            string ParamID = "FormatLantai" + Project;
            string ParamID2 = "FormatUnit" + Project;

            string strSql = Db.SingleString("SELECT Value FROM [ISC064_SECURITY].[dbo].[REF_PARAM] WHERE ParamID = '" + ParamID + "'");
            string strSql2 = Db.SingleString("SELECT Value FROM [ISC064_SECURITY].[dbo].[REF_PARAM] WHERE ParamID = '" + ParamID2 + "'");

            for (int i = 1; i < tb.Rows[0].Cells.Count; i++)
            {
                string nounit = tb.Rows[0].Cells[i].Text;

                c = new TableCell();
                string NomorUnit = Lokasi + strSql + Lantai + strSql2 + nounit;

                string NoStock = Db.SingleString("SELECT ISNULL(NoStock, '') FROM MS_UNIT WHERE NoUnit = '" + NomorUnit + "'");
                string type = Db.SingleString("SELECT Jenis FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");
                string x = TableStock.Href(NoStock);
                if (x != "")
                {
                    c.Text = "<a href='" + x + "'>" + nounit + "</a>";
                    c.BackColor = TableStock.WarnaUnit(NoStock);
                    c.CssClass = "lt";
                    c.Attributes.Add("id", NoStock);
                    c.ToolTip = type + "-" + NomorUnit;
                }
                else
                {
                    c.Text = "";// nounit;
                    c.BackColor = TableStock.WarnaUnit(NoStock);
                    c.CssClass = "lt";
                }


                r.Cells.Add(c);
            }

            tb.Rows.Add(r);
        }

        private string Lokasi
        {
            get
            {
                return Cf.Pk(Request.QueryString["Lokasi"]);
            }
        }
        private string Project
        {
            get
            {
                //return Db.SingleString("SELECT ISNULL(Project, '') FROM  REF_LOKASI WHERE Lokasi = '" + Lokasi + "'");
                return Cf.Pk(Request.QueryString["Project"]);
            }
        }
        protected void fillLegend()
        {
            string ParamID = "WarnaUnitJual" + Project;
            string ParamID2 = "WarnaUnitBooked" + Project;
            string ParamID3 = "WarnaUnitCancel" + Project;
            string ParamID4 = "WarnaUnitHold" + Project;
            string jual = Db.SingleString("SELECT ISNULL(MAX(Value), 'FFFFFF') FROM " + Mi.DbPrefix + "SECURITY..REF_PARAM WHERE ParamID = '" + ParamID + "' ");
            string booked = Db.SingleString("SELECT ISNULL(MAX(Value), 'FFFFFF') FROM " + Mi.DbPrefix + "SECURITY..REF_PARAM WHERE ParamID = '" + ParamID2 + "' ");
            string cancel = Db.SingleString("SELECT ISNULL(MAX(Value), 'FFFFFF') FROM " + Mi.DbPrefix + "SECURITY..REF_PARAM WHERE ParamID = '" + ParamID3 + "' ");
            string hold = Db.SingleString("SELECT ISNULL(MAX(Value), 'FFFFFF') FROM " + Mi.DbPrefix + "SECURITY..REF_PARAM WHERE ParamID = '" + ParamID4 + "' ");
            legend.Text = "<table><tr>";

            addLegendColor(jual, "Sold");
            addLegendColor(booked, "Reserved");
            addLegendColor(cancel, "Available");
            addLegendColor(hold, "Hold Internal");

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

        private string NamaTower
        {
            get
            {
                return Cf.Pk(Request.QueryString["Nama"]);
            }
        }
    }

}

using System;
using System.Drawing;
using System.IO;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.NUP
{
    public partial class TabelStokView2 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            spanlokasi.InnerText = NamaTower + " " + Lokasi;
            fillLegend();
            PrintHeader();
            PrintLantai();
        }

        private void PrintHeader()
        {
            TableRow r2 = new TableRow();
            TableCell c2;

            c2 = new TableCell(); c2.CssClass = "h"; c2.Text = "Type."; c2.Width = 50; c2.Height = 30;
            c2.BackColor = Color.Gray; c2.ForeColor = Color.Black;
            r2.Cells.Add(c2);

            DataTable rsUnit = Db.Rs("SELECT DISTINCT(Nomor) FROM MS_UNIT WHERE Lokasi='" + Lokasi + "' AND Project = '" + Project + "'");
            for (int i = 0; i < rsUnit.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                string NomorUnit = rsUnit.Rows[i]["Nomor"].ToString();
                rsUnit = Db.Rs("SELECT DISTINCT(Nomor) FROM MS_UNIT WHERE Lokasi='" + Lokasi + "' AND Project = '" + Project + "'");
                string Jenis = Db.SingleString("SELECT Jenis FROM MS_UNIT WHERE Nomor='" + NomorUnit + "' AND Lokasi='" + Lokasi + "' AND Project = '" + Project + "'");
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

            rsUnit = Db.Rs("SELECT DISTINCT(Nomor) FROM MS_UNIT WHERE Lokasi='" + Lokasi + "' AND Project = '" + Project + "'");
            for (int i = 0; i < rsUnit.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                string NoUnit = rsUnit.Rows[i]["Nomor"].ToString();

                c = new TableCell(); c.CssClass = "h"; c.Text = NoUnit; c.Width = 50; c.Height = 30;
                c.BackColor = Color.Gray; c.ForeColor = Color.Black;
                r.Cells.Add(c);
            }
            tb.Rows.Add(r);
        }
        private void PrintLantai()
        {
            DataTable rsUnit = Db.Rs("SELECT Distinct(Lantai) FROM MS_UNIT WHERE Lokasi='" + Lokasi + "' AND Project = '" + Project + "' ORDER BY Lantai DESC");
            for (int i = 0; i < rsUnit.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                string NoUnit = rsUnit.Rows[i]["Lantai"].ToString();
                Print(NoUnit);
            }
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
                string ParamID1 = "FormatLantai" + Project;
                string ParamID2 = "FormatUnit" + Project;
                string PemisahLantai = Db.SingleString("SELECT Value FROM " + Mi.DbPrefix + "SECURITY..REF_PARAM WHERE ParamID = '" + ParamID1 + "'");
                string PemisahUnit = Db.SingleString("SELECT Value FROM " + Mi.DbPrefix + "SECURITY..REF_PARAM WHERE ParamID = '" + ParamID2 + "'");
                string NomorUnit = Lokasi + PemisahLantai + Lantai + PemisahUnit + nounit;
                string NoStock = Db.SingleString("SELECT ISNULL(NoStock, '') FROM MS_UNIT WHERE NoUnit = '" + NomorUnit + "'");
                bool x = href(NomorUnit);// href(NomorUnit);\
                if (x == true)
                {
                    c.ID = NoStock;
                    c.Text = NomorUnit;//"<a href='" + x + "'>" + NomorUnit + "</a>";
                    c.BackColor = TableStock.WarnaUnit(NoStock);
                    c.CssClass = "lt";
                    c.ToolTip = NomorUnit;
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

        private bool href(string unit)
        {
            bool x = true;

            string strSql = "SELECT Status, NoStock FROM MS_UNIT WHERE NoUnit = '" + unit + "'";
            DataTable rs = Db.Rs(strSql);
            if (rs.Rows.Count != 0)
            {
                x = true;
            }
            else
            {
                x = false;
            }

            return x;
        }

        private Color status(string unit)
        {
            string strSql = "SELECT Status,NoStock"
                + " FROM MS_UNIT "
                + " WHERE NoUnit = '" + unit + "' AND Project = '" + Project + "'";
            DataTable rs = Db.Rs(strSql);
            if (rs.Rows.Count != 0)
            {
                Color color = new Color();
                string Status = rs.Rows[0]["Status"].ToString();
                string NoStock = rs.Rows[0]["NoStock"].ToString();
                if (Status == "B")
                {
                    var adaKontrak = Db.SingleInteger("Select count(*) from ms_kontrak where NoStock='" + NoStock + "' and Status='A'") > 0;
                    int adaPriority = Db.SingleInteger("Select count(*) from MS_NUP_PRIORITY where NoStock='" + NoStock + "'");
                    decimal Bayar = 0;
                    string NoNUPPriority = Db.SingleString("SELECT NoNUP FROM MS_NUP_PRIORITY WHERE NoStock='" + NoStock + "'");
                    string Tipe = Db.SingleString("SELECT Tipe FROM MS_NUP_PRIORITY WHERE NoStock='" + NoStock + "'");
                    decimal TotBayar = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiBayar),0) FROM MS_NUP_PELUNASAN WHERE NoNup = '" + NoNUPPriority + "' AND Tipe = '" + Tipe + "'");

                    if (adaKontrak)
                        return Color.Red; //sold //Merah
                    else if (adaPriority != 0)
                    {
                        if (Tipe == "RUSUNAMI")
                        {
                            Bayar = 2000000;
                        }
                        else
                            Bayar = 6000000;

                        if (Bayar == TotBayar)
                            return Color.Green; //Bayar BF Lunas //Hijau
                        else
                            return Color.Orange;//NUP sdh pilih unit //Orange

                    }
                    else
                        return Color.Orange; //NUP sdh pilih unit //Orange

                }
                else
                {
                    int c = Db.SingleInteger("SELECT COUNT(*) FROM MS_RESERVASI"
                        + " WHERE Status = 'A' AND NoUnit = '" + unit + "'");
                    if (c != 0)
                    {
                        color = Color.Blue; //reserved
                    }
                    else
                        color = Color.White; //available

                }

                return color;
            }
            else
                return Color.White;
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
                return Cf.Pk(Request.QueryString["Project"]);
            }
        }
    }

}

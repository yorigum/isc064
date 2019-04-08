using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.FINANCEAR
{
    public partial class VADownloadBRI : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            FillTag();
        }

        protected void FillTag()
        {
            DataTable rs = Db.Rs("SELECT b.NoKontrak, b.NoUnit, b.Status, c.Nama AS Cust, b.NoVA"
                + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK b"
                + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER c ON b.NoCustomer = c.NoCustomer"
                + " AND b.NoVA <> ''"
                + " ORDER BY b.NoUnit ASC"
                );

            if (rs.Rows.Count == 0)
                save.Enabled = false;

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                decimal Tagihan = Db.SingleDecimal("SELECT ISNULL(NilaiTagihan,0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN"
                    + " WHERE CONVERT(VARCHAR,TglJT,112) <= '" + Cf.Tgl112(Cf.AkhirBulan()) + "' AND CONVERT(VARCHAR,TglJT,112) >= '" + Cf.Tgl112(Cf.AwalBulan()) + "' "
                    + " AND NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "'");
                string namatagihan = Db.SingleString("SELECT NamaTagihan FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN"
                    + " WHERE CONVERT(VARCHAR,TglJT,112) <= '" + Cf.Tgl112(Cf.AkhirBulan()) + "' AND CONVERT(VARCHAR,TglJT,112) >= '" + Cf.Tgl112(Cf.AwalBulan()) + "' "
                    + " AND NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "'");
                decimal NoTagihan = Db.SingleDecimal("SELECT NoUrut FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN"
                   + " WHERE CONVERT(VARCHAR,TglJT,112) <= '" + Cf.Tgl112(Cf.AkhirBulan()) + "' AND CONVERT(VARCHAR,TglJT,112) >= '" + Cf.Tgl112(Cf.AwalBulan()) + "' "
                   + " AND NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "'");
                decimal Bayar = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "'"
                                               + " AND NOTAGIHAN IN (SELECT NOURUT FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN WHERE NoTagihan=" + NoTagihan
                                               + " AND NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "')");
                decimal Denda = Db.SingleDecimal("SELECT ISNULL(SUM(Denda-DendaReal-NilaiPutihDenda),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN"
                    + " WHERE NoUrut ="+ NoTagihan
                    + " AND NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "'");
                decimal Sisa = (Tagihan + Denda) - Bayar;

                HtmlTableRow tr;
                HtmlTableCell c;
                RadioButtonList rb;

                tr = new HtmlTableRow();
                list2.Controls.Add(tr);

                c = new HtmlTableCell();
                c.InnerHtml = (i + 1).ToString();
                c.ID = "pk2_" + i;
                c.Attributes["title"] = rs.Rows[i]["NoVA"] + ";" + Math.Round(Sisa) + ";" + rs.Rows[i]["Cust"] + ";" + namatagihan;
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["NoKontrak"].ToString();
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["NoUnit"].ToString();
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["Cust"].ToString();
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["NoVA"].ToString();
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = Cf.Num(Sisa);
                c.Align = "right";
                tr.Cells.Add(c);
            }
        }
        protected void ExportCSV(object sender, EventArgs e)
        {

            //Build the CSV file data as a Comma separated string.
            string csv = string.Empty;

            DataTable rs = Db.Rs("SELECT * FROM REF_ACC");

            //header
            csv += "Test";
            csv += "\r\n";
            for(int i = 0;i<rs.Rows.Count;i++)
            {
                //Add the Header row for CSV file.
                csv += rs.Rows[i]["Bank"].ToString() + ',' + rs.Rows[i]["SubID"].ToString() + ',';
                csv += "\r\n";
            }

           
            //Download the CSV file.
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=SqlExport.csv");
            Response.Charset = "";
            Response.ContentType = "application/text";
            Response.Output.Write(csv);
            Response.Flush();
            Response.End();

        }

    }
}

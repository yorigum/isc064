using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Globalization;
namespace ISC064.COLLECTION
{
    public partial class PemutihanDenda : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable rsTagihan = Db.Rs("SELECT * FROM ISC064_MARKETINGJUAL..MS_TAGIHAN WHERE NoUrut='" + NoUrut + "' AND NoKontrak = '" + NoKontrak + "'");
            decimal Sisa = Convert.ToDecimal(rsTagihan.Rows[0]["Denda"]) - Convert.ToDecimal(rsTagihan.Rows[0]["DendaReal"]) - Convert.ToDecimal(rsTagihan.Rows[0]["NilaiPutihDenda"]);

            //Response.Write("UPDATE ISC064_MARKETINGJUAL..MS_TAGIHAN SET  "
            //+ " Denda=0,NilaiPutihDenda = " + Convert.ToDecimal(Sisa, CultureInfo.CreateSpecificCulture("id-ID").NumberFormat) + " "
            //+ " WHERE NoKontrak = '" + NoKontrak + "' "
            //+ " and NoUrut = '" + NoUrut + "' ");
            Db.Execute("UPDATE ISC064_MARKETINGJUAL..MS_TAGIHAN SET  "
            + " NilaiPutihDenda = " + Convert.ToDecimal(Sisa, CultureInfo.CreateSpecificCulture("id-ID").NumberFormat) + " "
            + " ,PutihDenda = '1'"
            + " WHERE NoKontrak = '" + NoKontrak + "' "
            + " and NoUrut = '" + NoUrut + "' ");

            DataTable rsAft = Db.Rs("SELECT "
                + "CONVERT(VARCHAR,NoUrut) + '.   ' + NamaTagihan + ' ('+Tipe+')   ' + CONVERT(VARCHAR,TglJT,106) + '   ' + CONVERT(VARCHAR,NilaiTagihan,1) +'  '+CONVERT(VARCHAR,NilaiPutihDenda,1) "
                + "FROM ISC064_MARKETINGJUAL..MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' AND NoUrut = '" + NoUrut + "' ORDER BY NoUrut");


            DataTable rs = Db.Rs("SELECT"
                + " ISC064_MARKETINGJUAL..MS_KONTRAK.NoKontrak AS [No. Kontrak]"
                + ",ISC064_MARKETINGJUAL..MS_KONTRAK.NoUnit AS [Unit]"
                + ",ISC064_MARKETINGJUAL..MS_CUSTOMER.Nama AS [Customer]"
                + ",ISC064_MARKETINGJUAL..MS_KONTRAK.NilaiKontrak AS [Nilai Kontrak]"
                + ",ISC064_MARKETINGJUAL..MS_KONTRAK.Skema AS [Skema]"
                + " FROM ISC064_MARKETINGJUAL..MS_KONTRAK INNER JOIN ISC064_MARKETINGJUAL..MS_CUSTOMER"
                + " ON ISC064_MARKETINGJUAL..MS_KONTRAK.NoCustomer = ISC064_MARKETINGJUAL..MS_CUSTOMER.NoCustomer"
                + " WHERE ISC064_MARKETINGJUAL..MS_KONTRAK.NoKontrak = '" + NoKontrak + "'");

            string Ket = Cf.LogCapture(rs)
                + "<br>---PEMUTIHAN DENDA---<br>"
                + Cf.LogList(rsAft, "JADWAL TAGIHAN")
                ;

            Db.Execute("EXEC ISC064_MARKETINGJUAL..spLogPutihDenda "
                + " 'PD'"
                + ",'" + Act.UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'" + Ket + "'"
                + ",'" + NoKontrak + "'"
                );

            decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PUTIHDENDA_LOG ORDER BY LogID DESC");
            string Project = Db.SingleString("SELECT Project FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
            Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_PUTIHDENDA_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

            Response.Redirect("PemutihanDenda1.aspx?done=" + NoKontrak);
        }

        private string NoUrut
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoUrut"]);
            }
        }

        private string NoKontrak
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoKontrak"]);
            }
        }
    }
}
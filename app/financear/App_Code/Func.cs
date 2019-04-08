using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.FINANCEAR
{
	/// <summary>
	/// Summary description for Func.
	/// </summary>
	public class Func
	{
		public static decimal SaldoBank(string Acc, DateTime Tgl) 
		{
			decimal SaldoAwal = Db.SingleDecimal("SELECT SaldoAwal FROM REF_ACC WHERE Acc = '"+Acc+"'");
			
			decimal Masuk = Db.SingleDecimal("SELECT ISNULL(SUM(Nilai),0) FROM MS_KASMASUK "
				+ "WHERE Acc = '"+Acc+"' AND CONVERT(varchar,Tgl,112) < '"+Cf.Tgl112(Tgl)+"'");
			decimal Keluar = Db.SingleDecimal("SELECT ISNULL(SUM(Nilai),0) FROM MS_KASKELUAR "
				+ "WHERE Acc = '"+Acc+"' AND CONVERT(varchar,Tgl,112) < '"+Cf.Tgl112(Tgl)+"'");

			return SaldoAwal + Masuk - Keluar;
		}

		public static bool CekAkunting(string NoTTS)
		{
			bool isAkunting = false;
			int Akunting = Db.SingleInteger("SELECT Akunting FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS WHERE NoTTS = " + NoTTS);

			if(Akunting > 0)
				isAkunting = true;

			return isAkunting;
		}
        public static void KontrakHeader(string NoKontrak, Label nokontrakl, Label unit, Label customer, Label agent)
        {
            string strSql = "SELECT MS_KONTRAK.*"
                + ",MS_CUSTOMER.Nama AS Cs"
                + ",MS_AGENT.Nama + ' ' + MS_AGENT.Principal AS Ag"
                + " FROM ISC064_MARKETINGJUAL..MS_KONTRAK INNER JOIN ISC064_MARKETINGJUAL..MS_CUSTOMER ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
                + " INNER JOIN ISC064_MARKETINGJUAL..MS_AGENT ON MS_KONTRAK.NoAgent = MS_AGENT.NoAgent"
                + " WHERE MS_KONTRAK.NoKontrak = '" + NoKontrak + "'";

            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count == 0)
                System.Web.HttpContext.Current.Response.Redirect("/CustomError/Deleted.html");
            else
            {
                nokontrakl.Text = rs.Rows[0]["NoKontrak"] + "";
                unit.Text = rs.Rows[0]["NoStock"] + "/" + rs.Rows[0]["NoUnit"].ToString();
                customer.Text = rs.Rows[0]["Cs"].ToString();
                agent.Text = rs.Rows[0]["Ag"].ToString();
            }
        }
	}
}

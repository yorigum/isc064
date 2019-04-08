using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;

namespace ISC064.COLLECTION
{
    public partial class AutoPJT : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Db.Execute("exec spAutoPJT_JUAL");
            SendSMS();
        }

        private void SendSMS()
        {
            SMSLunas();
            SMSMinusTujuh();
            UlangTahun();
            Js.Close(this);
        }

        private void SMSLunas()
        {
            DataTable rw = Db.Rs("use ISC064_financear "
                                      + " select  "
                                      + "  a.NoTTS "
                                      + " ,a.Ref "
                                      + " ,a.NoBKM "
                                      + " ,a.Unit as NoUnit"
                                      + " ,a.Customer as Nama"
                                      + " ,a.Total "
                                      + " ,a.TglBKM "
                                      + " ,(select NoHP from ISC064_marketingjual..ms_customer where nocustomer=(Select NoCustomer From ISC064_marketingjual..ms_kontrak where nokontrak=a.ref)) as NoHP "
                                      + " ,(Select NoVa From ISC064_marketingjual..ms_kontrak where nokontrak=a.ref) as NOVa "
                                      + " ,(Select NamaTagihan from ISC064_marketingjual..ms_tagihan where nokontrak=a.ref and NoUrut=(Select top 1 NoTagihan from ISC064_marketingjual..ms_pelunasan where notts=a.Notts  order by NoUrut desc)) as NamaTagihan "
                                      + " ,(Select NilaiTagihan from ISC064_marketingjual..ms_tagihan where nokontrak=a.ref and NoUrut=(Select top 1 NoTagihan from ISC064_marketingjual..ms_pelunasan where notts=a.Notts  order by NoUrut desc)) as NilaiTagihan "
                                      + " from ms_tts a "
                                      + " where  "
                                      + " a.Status='POST'  "
                                      + " AND CONVERT(varchar,a.TglBKM,112) = '" + Cf.Tgl112(DateTime.Today.AddDays(0)) + "'"
                                      );

            for (int j = 0; j < rw.Rows.Count; j++)
            {

                var v = rw.Rows[j];

                string NamaTagihan = Db.SingleString("select b.namatagihan from ISC064_MARKETINGJUAL..MS_PELUNASAN a join ISC064_MARKETINGJUAL..MS_TAGIHAN b on a.nokontrak = b.Nokontrak and a.notagihan = b.nourut  where a.nokontrak ='" + v["Ref"] + "' and a.nobkm='" + v["NoBKM"] + "'");
                string LokasiUnit = Db.SingleString("SELECT Lokasi FROM ISC064_MARKETINGJUAL..MS_KONTRAK WHERE NOKONTRAK = '" + v["Ref"] + "'");
                string UnitApt = Db.SingleString("SELECT NoUnit FROM ISC064_MARKETINGJUAL..MS_KONTRAK WHERE NOKONTRAK = '" + v["Ref"] + "'");
                string[] ArrUnit = UnitApt.Split('/');

                string Pesan = "";
                Pesan = "Terima kasih atas pembayaran anda sebesar Rp. " + Cf.Num(v["Total"])
                    + " pada tanggal " + Cf.Day(v["TglBKM"]) + " untuk pembayaran " + NamaTagihan + " "
                   // + LokasiUnit + " lantai " + ArrUnit[1] + " no " + ArrUnit[2]
                    + " Westpoint"
                    ;
                Response.Write(Pesan + "<br />");
            }
        }

        private void SMSMinusTujuh()
        {

            string strSql = "SELECT a.*, c.NoHP, b.NoUnit, b.NoCustomer, b.NoVA, c.Nama "
                + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN a"
                + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK b ON a.NoKontrak = b.NoKontrak"
                + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER c ON b.NoCustomer = c.NoCustomer"
                + " WHERE 1=1 "
                + " AND CONVERT(varchar,a.TglJT,112) = '" + Cf.Tgl112(DateTime.Today.AddDays(7)) + "'"
                + " AND b.Status = 'A' AND a.KPR=0 "
                + " ORDER BY b.NoUnit";

            DataTable rs = Db.Rs(strSql);
            //Response.Write(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                decimal total = Db.SingleDecimal("use ISC064_marketingjual;Select (ISNULL(SUM(NilaiTagihan),0)) - (Select ISNULL(SUM(NilaiPelunasan),0) from ms_pelunasan where nokontrak ='" + rs.Rows[i]["NoKontrak"] + "' and Notagihan<=" + rs.Rows[i]["NoUrut"] + ") "
                                             + " from ms_tagihan a "
                                             + " where Nourut<=" + rs.Rows[i]["NoUrut"] + " and  a.nokontrak='" + rs.Rows[i]["NoKontrak"] + "'");

                string Pesan = "";
                Pesan = "Yth. Bapak/Ibu. " + rs.Rows[i]["Nama"].ToString() + " Pembayaran cicilan " + rs.Rows[i]["NamaTagihan"].ToString() + " pembelian unit apartemen sebesar Rp " + Cf.Num(rs.Rows[i]["NilaiTagihan"]) + " jatuh tempo pada " + Cf.Day(rs.Rows[i]["TglJT"])
                    + " . Mohon pembayaran dilakukan melalui virtual account " + rs.Rows[i]["NoVA"].ToString() + " Bank Mandiri."
                    + "  Abaikan pesan ini apabila pembayaran sudah dilakukan. Terima kasih"
                    ;

                Response.Write(Pesan + "<br />");

            }
        }

        private void UlangTahun()
        {
            string strSql = "SELECT A.Nama FROM ISC064_MARKETINGJUAL..MS_CUSTOMER A JOIN ISC064_MARKETINGJUAL..MS_KONTRAK B ON A.NOCUSTOMER = B.NOCUSTOMER WHERE B.STATUS = 'A' AND "
                + "(CONVERT(VARCHAR,DAY(TGLLAHIR)) = CONVERT(VARCHAR,DAY(GETDATE())) AND "
                + "CONVERT(VARCHAR,MONTH(TGLLAHIR)) = CONVERT(VARCHAR,MONTH(GETDATE())))"
                ;

            DataTable rs = Db.Rs(strSql);
            //Response.Write(strSql);

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;
                string Pesan = "";
                Pesan = "Westpoint mengucapkan Selamat ulang tahun kepada Yth. Bapak/Ibu " + rs.Rows[i]["Nama"].ToString() + ", semoga selalu diberikan rejeki, kesehatan dan kebahagiaan.";
                Response.Write(Pesan + "<br />");
            }

        }
    }
}

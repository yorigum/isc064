using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.Net;

namespace ISC064
{
    public partial class DailyReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MailMessage mail = new MailMessage();
            DataTable rs = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project IN (" + Act.ProjectListSql + ")");
            for (int i = 0; i < rs.Rows.Count; i++)
            {

                try
                {

                    string EmailFrom = Db.SingleString("SELECT ISNULL(Value, '') FROM " + Mi.DbPrefix + "SECURITY..REF_PARAM WHERE ParamID = 'EmailFrom" + rs.Rows[i]["Project"] + "'");
                    string Password = Db.SingleString("SELECT ISNULL(Value, '') FROM " + Mi.DbPrefix + "SECURITY..REF_PARAM WHERE ParamID = 'EmailPassword" + rs.Rows[i]["Project"] + "'");
                    string EmailDisplayName = Db.SingleString("SELECT ISNULL(Value, '') FROM " + Mi.DbPrefix + "SECURITY..REF_PARAM WHERE ParamID = 'EmailDisplayName" + rs.Rows[i]["Project"] + "'");
                    string EmailSMTP = Db.SingleString("SELECT ISNULL(Value, '') FROM " + Mi.DbPrefix + "SECURITY..REF_PARAM WHERE ParamID = 'EmailSMTP" + rs.Rows[i]["Project"] + "'");
                    string EmailSMTPPort = Db.SingleString("SELECT ISNULL(Value, '') FROM " + Mi.DbPrefix + "SECURITY..REF_PARAM WHERE ParamID = 'EmailSMTPPort" + rs.Rows[i]["Project"] + "'");

                    //Sender
                    mail.From = new MailAddress(EmailFrom   , EmailDisplayName);
                    DataTable Recipient = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "SECURITY..REF_EMAIL WHERE Project = '" + rs.Rows[i]["Project"] + "'");
                    for (int j = 0; j < Recipient.Rows.Count; j++)
                    {
                        //Recipient
                        mail.To.Add(new MailAddress(Recipient.Rows[j]["Email"].ToString()));

                    }

                    //Subject and Body
                    //string Body = BodyEmail();
                    mail.Subject = "Daily Report " + Cf.Day(DateTime.Now.AddDays(-1));
                    mail.Body = BodyEmail();
                    mail.IsBodyHtml = true;

                    var client = new SmtpClient(EmailSMTP, Convert.ToInt32(EmailSMTPPort));
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = true;
                    System.Net.NetworkCredential credentials =
                    new System.Net.NetworkCredential(EmailFrom, Password);
                    client.Credentials = credentials;
                    client.Send(mail);
                }
                catch(Exception err)
                {
                    Response.Write("Can't send email" + err + "<br>");
                }
            }
        }


        private string BodyEmail()
        {

            string Body = "", Header = "";
            string ContentMail = "";
            Header += "<table>";
            Header += "<tr><td style='text-align:right'>Project :</td><td>" + Mi.Pt + "</td></tr>";
            Header += "<tr><td style='text-align:right'>Date :</td><td>" + Cf.Day(DateTime.Today) + "</td></tr>";
            Header += "</table> <br />";

            //STOK
            Stok(ref ContentMail);

            //Summary
            Summary(ref ContentMail);

            //Aktivitas Customer
            AktivitasCustomer(ref ContentMail);

            //Finance
            Finance(ref ContentMail);


            Body = Header + ContentMail;// + FootNote;
            return Body;
        }

        private void Stok(ref string ContentMail)
        {

            ContentMail += "<b>Stock Unit : </b> <br />";
            DataTable rsLokasi = Db.Rs("SELECT DISTINCT Lokasi FROM ISC064_MARKETINGJUAL..MS_UNIT");
            ContentMail += "<table>";
            for (int i = 0; i < rsLokasi.Rows.Count; i++)
            {
                ContentMail += "<tr><td colspan='2'>" + rsLokasi.Rows[i]["Lokasi"].ToString() + "</td></tr>";

                int book = Db.SingleInteger("SELECT COUNT(*) FROM ISC064_MARKETINGJUAL..MS_UNIT WHERE Lokasi = '" + rsLokasi.Rows[i]["Lokasi"].ToString() + "'" +
                     "AND NoStock IN (SELECT NoStock FROM ISC064_MARKETINGJUAL..MS_RESERVASI WHERE Status = 'A')");

                int available = Db.SingleInteger("SELECT COUNT(*) FROM ISC064_MARKETINGJUAL..MS_UNIT WHERE Lokasi = '" + rsLokasi.Rows[i]["Lokasi"].ToString() + "' AND Status = 'A'");
                ContentMail += "<tr><td style='text-align:right;'>Available :</td><td>" + Cf.Num(available - book) + "</td></tr>";


                ContentMail += "<tr><td style='text-align:right;'>Booking :</td><td>" + Cf.Num(book) + "</td></tr>";

                int hold = Db.SingleInteger("SELECT COUNT(*) FROM ISC064_MARKETINGJUAL..MS_UNIT WHERE Lokasi = '" + rsLokasi.Rows[i]["Lokasi"].ToString() + "' AND Status = 'B'" +
                            "AND NoStock NOT IN (SELECT NoStock FROM ISC064_MARKETINGJUAL..MS_KONTRAK WHERE Lokasi = '" + rsLokasi.Rows[i]["Lokasi"].ToString() + "' AND Status = 'A')");
                ContentMail += "<tr><td style='text-align:right;'>Hold :</td><td>" + Cf.Num(hold) + "</td></tr>";

                int sold = Db.SingleInteger("SELECT COUNT(*) FROM ISC064_MARKETINGJUAL..MS_UNIT WHERE Lokasi = '" + rsLokasi.Rows[i]["Lokasi"].ToString() + "' AND Status = 'B'" +
                            "AND NoStock IN (SELECT NoStock FROM ISC064_MARKETINGJUAL..MS_KONTRAK WHERE Lokasi = '" + rsLokasi.Rows[i]["Lokasi"].ToString() + "' AND Status = 'A')");
                ContentMail += "<tr><td style='text-align:right;'>Sold :</td><td>" + Cf.Num(sold) + "</td></tr>";

            }
            ContentMail += "</table><br />";
        }

        private void Summary(ref string ContentMail)
        {
            decimal totalJenis = 0, totalUnit = 0;
            ContentMail += "<b>Sales :</b> <br />";
            DataTable rsJenis = Db.Rs("SELECT DISTINCT Jenis FROM ISC064_MARKETINGJUAL..MS_KONTRAK WHERE TglKontrak = '" + Cf.Tgl112(DateTime.Today.AddDays(-1)) + "' AND Status = 'A'");
            ContentMail += "<table>";
            ContentMail += "<tr><td style='width:100px;'>Tipe Unit</td><td style='width:100px;'>Jumlah Unit</td><td style='width:100px;'>Nilai Kontrak (Rp.)</td></tr>";
            for (int i = 0; i < rsJenis.Rows.Count; i++)
            {
                ContentMail += "<tr>";
                ContentMail += "<td>" + rsJenis.Rows[i]["Jenis"].ToString() + "</td>";

                int unitJenis = Db.SingleInteger("SELECT COUNT(*) FROM ISC064_MARKETINGJUAL..MS_KONTRAK WHERE TglKontrak = '" + Cf.Tgl112(DateTime.Today.AddDays(-1)) + "' AND Status = 'A' AND Jenis = '" + rsJenis.Rows[i]["Jenis"] + "'");
                totalUnit += unitJenis;
                ContentMail += "<td>" + Cf.Num(unitJenis) + "</td>";

                decimal nilaiJenis = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiKontrak), 0) FROM ISC064_MARKETINGJUAL..MS_KONTRAK WHERE TglKontrak = '" + Cf.Tgl112(DateTime.Today.AddDays(-1)) + "' AND Status = 'A' AND Jenis = '" + rsJenis.Rows[i]["Jenis"] + "'");
                totalJenis += nilaiJenis;
                ContentMail += "<td>" + Cf.Num(nilaiJenis) + "</td>";

                ContentMail += "</tr>";
            }
            ContentMail += "<tr><td>Total</td><td style='border-top:double black 1px;'>" + Cf.Num(totalUnit) + "</td><td style='border-top:double black 1px;'>" + Cf.Num(totalJenis) + "</td></tr>";
            ContentMail += "</table><br />";
        }

        private void AktivitasCustomer(ref string ContentMail)
        {
            ContentMail += "<b>Aktivitas Customer :</b> <br />";
            //Aktivitas Batal
            ContentMail += "<table>";
            DataTable rsBatal = Db.Rs("SELECT * FROM ISC064_MARKETINGJUAL..MS_KONTRAK WHERE TglBatal = '" + Cf.Tgl112(DateTime.Today.AddDays(-1)) + "' AND Status = 'B'");
            ContentMail += "<tr><td>Batal : " + Cf.Num(rsBatal.Rows.Count) + "</td></tr>";
            ContentMail += "</table>";

            //Aktivitas Pengalihan Hak
            ContentMail += "<table>";
            DataTable rsGantiNama = Db.Rs("SELECT * FROM ISC064_MARKETINGJUAL..MS_KONTRAK_LOG WHERE Tgl = '" + Cf.Tgl112(DateTime.Today.AddDays(-1)) + "' AND Aktivitas = 'GN'");
            ContentMail += "<tr><td>Pengalihan Hak : " + Cf.Num(rsGantiNama.Rows.Count) + "</td></tr>";
            ContentMail += "</table>";

            //Aktivitas Pindah Unit
            ContentMail += "<table>";
            DataTable rsGantiUnit = Db.Rs("SELECT * FROM ISC064_MARKETINGJUAL..MS_KONTRAK_LOG WHERE Tgl = '" + Cf.Tgl112(DateTime.Today.AddDays(-1)) + "' AND Aktivitas = 'GU'");
            ContentMail += "<tr><td>Pindah Unit : " + Cf.Num(rsGantiUnit.Rows.Count) + "</td></tr>";
            ContentMail += "</table><br />";
        }

        private void Finance(ref string ContentMail)
        {
            ContentMail += "<b>Finance :</b> <br />";
            decimal total = Db.SingleDecimal("SELECT ISNULL(SUM(Total),0) FROM ISC064_FINANCEAR..MS_TTS WHERE TglTTS = '" + Cf.Tgl112(DateTime.Today.AddDays(-1)) + "'");
            ContentMail += "<table>";
            ContentMail += "<tr><td></td><td colspan='2'>Payment Received (Rp) : " + Cf.Num(total) + "</td></tr>";

            decimal cash = Db.SingleDecimal("SELECT ISNULL(SUM(Total),0) FROM ISC064_FINANCEAR..MS_TTS WHERE TglTTS = '" + Cf.Tgl112(DateTime.Today.AddDays(-1)) + "' AND CaraBayar = 'TN'");
            ContentMail += "<tr><td colspan='2'></td><td style='text-align:right;'>Cash :</td><td>" + Cf.Num(cash) + "</td></tr>";

            decimal creditcard = Db.SingleDecimal("SELECT ISNULL(SUM(Total),0) FROM ISC064_FINANCEAR..MS_TTS WHERE TglTTS = '" + Cf.Tgl112(DateTime.Today.AddDays(-1)) + "' AND CaraBayar = 'KK'");
            ContentMail += "<tr><td colspan='2'></td><td style='text-align:right;'>Credit Card :</td><td>" + Cf.Num(creditcard) + "</td></tr>";

            decimal debitcard = Db.SingleDecimal("SELECT ISNULL(SUM(Total),0) FROM ISC064_FINANCEAR..MS_TTS WHERE TglTTS = '" + Cf.Tgl112(DateTime.Today.AddDays(-1)) + "' AND CaraBayar = 'KD'");
            ContentMail += "<tr><td colspan='2'></td><td style='text-align:right;'>Debit Card :</td><td>" + Cf.Num(debitcard) + "</td></tr>";

            decimal giro = Db.SingleDecimal("SELECT ISNULL(SUM(Total),0) FROM ISC064_FINANCEAR..MS_TTS WHERE TglTTS = '" + Cf.Tgl112(DateTime.Today.AddDays(-1)) + "' AND CaraBayar = 'BG'");
            ContentMail += "<tr><td colspan='2'></td><td style='text-align:right;'>Giro :</td><td>" + Cf.Num(giro) + "</td></tr>";

            decimal transfer = Db.SingleDecimal("SELECT ISNULL(SUM(Total),0) FROM ISC064_FINANCEAR..MS_TTS WHERE TglTTS = '" + Cf.Tgl112(DateTime.Today.AddDays(-1)) + "' AND CaraBayar = 'TR'");
            ContentMail += "<tr><td colspan='2'></td><td style='text-align:right;'>Transfer :</td><td>" + Cf.Num(transfer) + "</td></tr>";

            ContentMail += "</table><br /><br />";
        }

        private static string FootNote
        {
            get
            {
                return Environment.NewLine + Environment.NewLine
                + "----------------------------------------------------------------" + Environment.NewLine + "<br />"
                + "Email ini adalah email otomatis yang dihasilkan dari sistem Batavianet Web Application." + Environment.NewLine + "<br />"
                + "Mohon untuk tidak melakukan reply terhadap email ini karena mailbox ini tidak dimonitor.";
            }
        }
    }
}
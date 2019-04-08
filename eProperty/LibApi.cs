using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web.Script.Serialization;

namespace ISC064
{
    public class LibApi
    {
        //Report PDF
        public static void CreateFilePDFDailyReport(DateTime Tgl, string UserID, string Project, bool DefaultReport)
        {
            string Tipe = TipeReport(1);
            string nama = Tgl.Day + " " + NamaBln(Tgl.Month) + " " + Tgl.Year; //nama tampilan
            string pdffilename = "DR-" + Tgl.Day + NamaBln(Tgl.Month) + Tgl.Year + "-" + GetRandomPdfFileName() + ".pdf";
            string myHtml = Param.IPAddress + "LaporanApi/DailyReportApi.aspx?Tgl=" + LibApi.Tgl(Tgl) + "&UserID=" + UserID + "&Project=" + Project;
            LibApi.CreateFilePDF(pdffilename, myHtml, 1, Tipe, DefaultReport, nama);
        }
        public static void CreateFilePDFLapPenjualan(string UserID, string LokasiID, string TipeID, string AgentID,
            DateTime Dari, DateTime Sampai, string Project, bool DefaultReport)
        {
            string Tipe = TipeReport(2);
            string nama = TglNamaBln(Dari.Month, false) + " " + Dari.Year; //nama tampilan
            string pdffilename = "Penjualan-" + NamaBln(Dari.Month) + Dari.Year + "-" + GetRandomPdfFileName() + ".pdf";
            string myHtml = Param.IPAddress + "LaporanApi/LapPenjualanApi.aspx?tipe=" + TipeID + "&lokasi=" + LokasiID + "&agent=" + AgentID + "&titipjual=SEMUA&tipepro=SEMUA&userid=" + UserID + "&dari=" + LibApi.Tgl(Dari) + "&sampai=" + LibApi.Tgl(Sampai) + "&project=" + Project;
            //http://localhost:8030/LaporanApi/LapPenjualanApi.aspx?id=&tipe=SEMUA&lokasi=SEMUA&agent=SEMUA&titipjual=SEMUA&tipepro=SEMUA&userid=TES&dari=2018-02-01&sampai=2018-02-28
            LibApi.CreateFilePDF(pdffilename, myHtml, 0, Tipe, DefaultReport, nama);
        }
        public static void CreateFilePDFLapBatalKontrak(string UserID, string LokasiID, string TipeID, DateTime Dari, 
            DateTime Sampai, string Project, bool DefaultReport)
        {
            string Tipe = TipeReport(3);
            string nama = TglNamaBln(Dari.Month, false) + " " + Dari.Year; //nama tampilan
            string pdffilename = "BK-" + NamaBln(Dari.Month) + Dari.Year + "-" + GetRandomPdfFileName() + ".pdf";
            string myHtml = Param.IPAddress + "LaporanApi/LapBatalKontrakApi.aspx?tipe=" + TipeID + "&lokasi=" + LokasiID + "&userid=" + UserID + "&dari=" + LibApi.Tgl(Dari) + "&sampai=" + LibApi.Tgl(Sampai) + "&project=" + Project;
            LibApi.CreateFilePDF(pdffilename, myHtml, 0, Tipe, DefaultReport, nama);
        }
        public static void CreateFilePDFManagementReport(DateTime Tgl, string UserID, string Project, bool DefaultReport)
        {
            string Tipe = TipeReport(4);
            string nama = Tgl.Day + " " + NamaBln(Tgl.Month) + " " + Tgl.Year; //nama tampilan
            string pdffilename = "MR-" + Tgl.Day + NamaBln(Tgl.Month) + Tgl.Year + "-" + GetRandomPdfFileName() + ".pdf";
            string myHtml = Param.IPAddress + "LaporanApi/LapManagementReportApi.aspx?Tgl=" + LibApi.Tgl(Tgl) + "&UserID=" + UserID + "&Project=" + Project;
            LibApi.CreateFilePDF(pdffilename, myHtml, 1, Tipe, DefaultReport, nama);
        }
        public static void CreateFilePDF(string pdffilename, string myHtml, byte orientationpage, string Tipe, 
            bool DefaultReport, string nama)
        {

            string FolderPDF = Param.FolderPDF + Tipe;
            LibApi.CreateFolderPDF(FolderPDF);

            string save = FolderPDF + "\\" + pdffilename;
            string link = Param.IPAddress + "pdf/" + Tipe + "/" + pdffilename;

            Process p = new System.Diagnostics.Process();

            p.StartInfo.Arguments = "--orientation " + (orientationpage == 1 ? "portrait" : "landscape") + " --page-width 21cm --page-height 29.7cm --margin-left 1cm --margin-right 1cm --margin-top 1cm --margin-bottom 1cm " + myHtml + " " + save;
            p.StartInfo.FileName = Param.GeneratorPDF;
            p.Start();
            p.WaitForExit(60000);

            Db.Execute("INSERT INTO ISC064_SECURITY..REPORT "
                + " (Tipe, DefaultReport, Nama, Path, Link, TglGenerate) "
                + " VALUES ("
                + " '" + Tipe + "'"
                + ",'" + Convert.ToBoolean(DefaultReport) + "'"
                + ",'" + nama + "'"
                + ",'" + save + "'"
                + ",'" + link + "'"
                + ",'" + DateTime.Now + "'"
                + " )"
                );
        }
        public static void CreateFolderPDF(string FolderPDF)
        {
            bool exists = System.IO.Directory.Exists(FolderPDF);
            if (!exists)
            {
                System.IO.Directory.CreateDirectory(FolderPDF);
            }
        }
        public static void DeleteReport(string Tipe)
        {
            Db.Execute("DELETE FROM ISC064_SECURITY..REPORT WHERE Tipe = '" + Tipe + "'");
        }
        public static void DeleteFolderPDF(string FolderPDF)
        {
            bool exists = System.IO.Directory.Exists(FolderPDF);
            if (exists)
            {
                System.IO.Directory.Delete(FolderPDF, true);
            }
        }
        public static string GetRandomPdfFileName()
        {
            int maxSize = 6;
            char[] chars = new char[72];
            chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[1];

            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();

            crypto.GetNonZeroBytes(data);
            data = new byte[maxSize];
            crypto.GetNonZeroBytes(data);

            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }

            return result.ToString();
        }

        public static string NamaBln(int m)
        {
            string x = "";
            switch (m)
            {
                case 1: x = "Jan"; break;
                case 2: x = "Feb"; break;
                case 3: x = "Mar"; break;
                case 4: x = "Apr"; break;
                case 5: x = "Mei"; break;
                case 6: x = "Jun"; break;
                case 7: x = "Jul"; break;
                case 8: x = "Agt"; break;
                case 9: x = "Sep"; break;
                case 10: x = "Okt"; break;
                case 11: x = "Nov"; break;
                case 12: x = "Des"; break;
            }
            return x;
        }
        public static string Tgl(DateTime Input)
        {
            return Input.ToString("yyyy-MM-dd");
        }
        public static string TglNamaBln(int m, bool Roman)
        {
            string x = "";
            if (!Roman)
            {
                switch (m)
                {
                    case 1: x = "Januari"; break;
                    case 2: x = "Februari"; break;
                    case 3: x = "Maret"; break;
                    case 4: x = "April"; break;
                    case 5: x = "Mei"; break;
                    case 6: x = "Juni"; break;
                    case 7: x = "Juli"; break;
                    case 8: x = "Agustus"; break;
                    case 9: x = "September"; break;
                    case 10: x = "Oktober"; break;
                    case 11: x = "Nopember"; break;
                    case 12: x = "Desember"; break;
                }
            }
            else
            {
                switch (m)
                {
                    case 1: x = "I"; break;
                    case 2: x = "II"; break;
                    case 3: x = "III"; break;
                    case 4: x = "IV"; break;
                    case 5: x = "V"; break;
                    case 6: x = "VI"; break;
                    case 7: x = "VII"; break;
                    case 8: x = "VIII"; break;
                    case 9: x = "IX"; break;
                    case 10: x = "X"; break;
                    case 11: x = "XI"; break;
                    case 12: x = "XII"; break;
                }
            }
            return x;
        }

        public static string TipeReport(byte x)
        {
            string z = "";
            switch (x)
            {
                case 1: z = "DailyReport"; break;
                case 2: z = "Penjualan"; break;
                case 3: z = "BatalKontrak"; break;
                case 4: z = "ManagementReport"; break;
            }
            return z;
        }

        //Koneksi Db
        public static string CnnApi
        {
            get
            {
                System.Configuration.AppSettingsReader s = new System.Configuration.AppSettingsReader();
                string x = (string)s.GetValue("CnnApi", typeof(string));
                s = null;
                return x;
            }
        }
        public static void Execute(string strSql)
        {
            SqlConnection sqlCnn = new SqlConnection(CnnApi);
            SqlCommand sqlCmd = new SqlCommand(strSql, sqlCnn);
            sqlCnn.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCnn.Close();
        }
        public static DataTable Rs(string strSql)
        {
            SqlConnection sqlCnn = new SqlConnection(CnnApi);
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(strSql, sqlCnn);
            DataSet objDS = new DataSet();
            sqlAdapter.Fill(objDS, "data");
            sqlCnn.Close();

            DataTable rst = new DataTable();
            rst = objDS.Tables["data"];

            return rst;
        }
        public static string SingleString(string strSql)
        {
            SqlConnection sqlCnn = new SqlConnection(CnnApi);
            SqlCommand sqlCmd = new SqlCommand(strSql, sqlCnn);
            sqlCnn.Open();
            string x = "";
            x = (string)sqlCmd.ExecuteScalar();
            sqlCnn.Close();

            return x;
        }
        public static int SingleInteger(string strSql)
        {
            SqlConnection sqlCnn = new SqlConnection(CnnApi);
            SqlCommand sqlCmd = new SqlCommand(strSql, sqlCnn);
            sqlCnn.Open();
            int x = (int)sqlCmd.ExecuteScalar();
            sqlCnn.Close();

            return x;
        }
        public static string TokenFirebase
        {
            get
            {
                System.Configuration.AppSettingsReader s = new System.Configuration.AppSettingsReader();
                string x = (string)s.GetValue("TokenFirebase", typeof(string));
                s = null;
                return x;
            }
        }
        public static string IDPengirimFirebase
        {
            get
            {
                System.Configuration.AppSettingsReader s = new System.Configuration.AppSettingsReader();
                string x = (string)s.GetValue("IDPengirimFirebase", typeof(string));
                s = null;
                return x;
            }
        }

        //Notification
        public static void PushNotif(string Judul, string Ket, string UserID, string Ref, byte Klik)
        {
            try
            {
                int user = SingleInteger("SELECT COUNT(*) FROM SecUser WHERE UserID = '" + UserID + "'");
                if (user > 0)
                {
                    InsertSecNotif(Judul, Ket, UserID, Ref, Klik);

                    DataTable rsFirebase = Rs("SELECT * FROM SecFirebase WHERE UserID = '" + UserID + "'");
                    for (int i = 0; i < rsFirebase.Rows.Count; i++)
                    {
                        SendNotif(rsFirebase.Rows[i]["FirebaseID"].ToString(), Judul, Ket, Ref, Klik);
                    }
                }
            }
            catch { }
        }
        private static void InsertSecNotif(string Judul, string Ket, string UserID, string Ref, byte Klik)
        {
            Execute("EXEC InsertSecNotification2 "
                + " '" + Judul + "'"
                + ",'" + Ket + "'"
                + ",'" + UserID + "'"
                + ",''"
                + ",'" + Ref + "'"
                + "," + Klik);
        }
        private static void SendNotif(string DeviceID, string Judul, string Body, string Ref, byte Klik)
        {
            string ApplicationID = TokenFirebase;
            string SenderID = IDPengirimFirebase;

            try
            {
                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";
                var data = new
                {
                    to = DeviceID,
                    notification = new
                    {
                        title = "Mobile eProperty",
                        body = Body,
                        click_action = Judul + ";" + Ref + ";" + Klik,
                        sound = "default"
                    },
                    data = new
                    {
                        data1 = Body,
                        data2 = Ref
                    }
                };

                var serializer = new JavaScriptSerializer();
                var json = serializer.Serialize(data);
                Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                tRequest.Headers.Add(string.Format("Authorization:key=" + ApplicationID));
                tRequest.Headers.Add(string.Format("Sender:id=" + SenderID));
                tRequest.ContentLength = byteArray.Length;
                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                string str = sResponseFromServer;
                                //return str;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
                //return str;
            }
        }
    }
}

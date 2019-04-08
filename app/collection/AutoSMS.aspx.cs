//using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ISC064.COLLECTION
{

    public partial class AutoSMS : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SendSMS();
        }
        protected void SendSMS()
        {
            //SMSPJT();
            //SMSKuitansi();
            SMSUltah();
        }
        protected void SMSPJT()
        {
            DataTable rs = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE SMSID != ''");
            //var rs = Db.Rs("");
            //from p in db.SecProjects
            // where !p.Inaktif && p.SMSID != ""
            // select p;
            foreach (DataRow r in rs.Rows)
            {
                switch (r["SMSID"].ToString())
                {
                    case "ST":
                        //int  st = Db.SingleInteger("SELECT COUNT (*) FROM SmsSatuTitik WHERE Project IN (" + Act.ProjectListSql + ")"); //db.SmsSatuTitiks.SingleOrDefault(p => p.Project == r.Project);
                        DataTable st = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "SECURITY..SmsSatuTitik WHERE Project = '" + rs.Rows[0]["Project"] + "'");
                        if (st.Rows.Count > 0)
                        {
                            DataTable f = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "SECURITY..SmsFormat WHERE Project = '" + rs.Rows[0]["Project"] + "' AND Tipe = 'INVOICE' "); //db.SmsFormats.SingleOrDefault(p => p.Project == r.Project && p.Tipe == "INVOICE" && !p.Inaktif);
                            if (f.Rows.Count > 0)
                            {
                                string strSql = SMS.query("INVOICE", "", f.Rows[0]["SatuanWaktu"].ToString(), Convert.ToDecimal(f.Rows[0]["NilaiWaktu"]), f.Rows[0]["Project"].ToString());
                                DataTable rsa = Db.Rs(strSql);
                                for (int i = 0; i < rsa.Rows.Count; i++)
                                {
                                    if (!Response.IsClientConnected) break;

                                    string HP = rsa.Rows[i]["NomorHP"].ToString();

                                    string Pesan = "";
                                    if ((HP != "") && (HP != "-"))
                                    {
                                        string ip = "103.29.215.210"; //Param.IP;

                                        Pesan = SMS.Template2("INVOICE", rsa.Rows[i]["NoCustomer"].ToString(), f.Rows[0]["SatuanWaktu"].ToString(), Convert.ToDecimal(f.Rows[0]["NilaiWaktu"]), f.Rows[0]["Project"].ToString());

                                        //string url = string.Format("http://" + ip + "/api/send.php?uid=" + st.Rows[0]["Username"] + "&pwd=" + st.Rows[0]["Pass"] + "&mask=" + st.Rows[0]["Masking"] + "&msisdn=" + HP + "&sub=" + st.Rows[0]["Divisi"] + "&msg=" + Pesan + "");
                                        //using (WebClient client = new WebClient())
                                        //{
                                        //    string json = client.DownloadString(url);
                                        //    try
                                        //    {
                                        //        if (!String.IsNullOrEmpty(json))
                                        //        {
                                        //            SMSBlast smsblast = (new JavaScriptSerializer()).Deserialize<SMSBlast>(json);
                                        //        }
                                        //    }
                                        //    catch (Exception ex)
                                        //    {
                                        //        Response.Write("A + " + Pesan + "<br/>");
                                        //    }
                                        //}

                                        Response.Write(Pesan + "<br/>");
                                    }
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        protected void SMSKuitansi()
        {
            //var rs = from p in db.SecProjects
            //         where !p.Inaktif && p.SMSID != ""
            //         select p;
            DataTable rs = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE SMSID != ''");
            foreach (DataRow r in rs.Rows)
            {
                switch (r["SMSID"].ToString())
                {
                    case "ST":
                        //var st = db.SmsSatuTitiks.SingleOrDefault(p => p.Project == r.Project);
                        DataTable st = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "SECURITY..SmsSatuTitik WHERE Project = '" + rs.Rows[0]["Project"] + "'");
                        if (st.Rows.Count > 0)
                        {
                            //var f = db.SmsFormats.SingleOrDefault(p => p.Project == r.Project && p.Tipe == "KUITANSI" && !p.Inaktif);
                            DataTable f = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "SECURITY..SmsFormat WHERE Project = '" + rs.Rows[0]["Project"] + "' AND Tipe = 'KUITANSI' ");
                            if (f.Rows.Count > 0)
                            {
                                string strSql = SMS.query("KUITANSI", "", f.Rows[0]["SatuanWaktu"].ToString(), Convert.ToDecimal(f.Rows[0]["NilaiWaktu"]), f.Rows[0]["Project"].ToString());
                                DataTable rsa = Db.Rs(strSql);
                                for (int i = 0; i < rsa.Rows.Count; i++)
                                {
                                    if (!Response.IsClientConnected) break;

                                    string HP = rsa.Rows[i]["NomorHP"].ToString();

                                    string Pesan = "";
                                    if ((HP != "") && (HP != "-"))
                                    {
                                        string ip = "103.29.215.210"; //Param.IP;

                                        Pesan = SMS.Template2("KUITANSI", rsa.Rows[i]["NoTTS"].ToString(), f.Rows[0]["SatuanWaktu"].ToString(), Convert.ToDecimal(f.Rows[0]["NilaiWaktu"]), f.Rows[0]["Project"].ToString());

                                        //string url = string.Format("http://" + ip + "/api/send.php?uid=" + st.Rows[0]["Username"] + "&pwd=" + st.Rows[0]["Pass"] + "&mask=" + st.Rows[0]["Masking"] + "&msisdn=" + HP + "&sub=" + st.Rows[0]["Divisi"] + "&msg=" + Pesan + "");
                                        //using (WebClient client = new WebClient())
                                        //{
                                        //    string json = client.DownloadString(url);
                                        //    try
                                        //    {
                                        //        if (!String.IsNullOrEmpty(json))
                                        //        {
                                        //            SMSBlast smsblast = (new JavaScriptSerializer()).Deserialize<SMSBlast>(json);
                                        //        }
                                        //    }
                                        //    catch (Exception ex)
                                        //    {
                                        //        Response.Write("A + " + Pesan + "<br/>");
                                        //    }
                                        //}

                                        Response.Write(Pesan + "<br/>");
                                    }
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        protected void SMSUltah()
        {
            //var rs = from p in db.SecProjects
            //         where !p.Inaktif && p.SMSID != ""
            //         select p;
            DataTable rs = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE SMSID  != ''");
            foreach (DataRow r in rs.Rows)
            {
                switch (r["SMSID"].ToString())
                {
                    case "ST":
                        //var st = db.SmsSatuTitiks.SingleOrDefault(p => p.Project == r.Project);
                        DataTable st = Db.Rs("SELECT * FROM  " + Mi.DbPrefix + "SECURITY..SmsSatuTitik WHERE Project = '" + rs.Rows[0]["Project"] + "'");
                        if (st.Rows.Count > 0)
                        {
                            //var f = db.SmsFormats.SingleOrDefault(p => p.Project == r.Project && p.Tipe == "ULANGTAHUN" && !p.Inaktif);
                            DataTable f = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "SECURITY..SmsFormat WHERE Project = '" + rs.Rows[0]["Project"] + "' AND Tipe = 'ULANGTAHUN' ");
                            if (f.Rows.Count > 0)
                            {
                                string strSql = SMS.query("ULANGTAHUN", "", f.Rows[0]["SatuanWaktu"].ToString(), Convert.ToDecimal(f.Rows[0]["NilaiWaktu"]), f.Rows[0]["Project"].ToString());
                                DataTable rsa = Db.Rs(strSql);
                                for (int i = 0; i < rsa.Rows.Count; i++)
                                {
                                    if (!Response.IsClientConnected) break;

                                    string HP = rsa.Rows[i]["NomorHP"].ToString();

                                    string Pesan = "";
                                    if ((HP != "") && (HP != "-"))
                                    {
                                        string ip = "103.29.215.210"; //Param.IP;

                                        Pesan = SMS.Template2("ULANGTAHUN", rsa.Rows[i]["NoCustomer"].ToString(), f.Rows[0]["SatuanWaktu"].ToString(), Convert.ToDecimal(f.Rows[0]["NilaiWaktu"]), f.Rows[0]["Project"].ToString());

                                        string url = string.Format("http://" + ip + "/api/send.php?uid=" + st.Rows[0]["Username"] + "&pwd=" + st.Rows[0]["Pass"] + "&mask=" + st.Rows[0]["Masking"] + "&msisdn=" + HP + "&sub=" + st.Rows[0]["Divisi"] + "&msg=" + Pesan + "");
                                        using (WebClient client = new WebClient())
                                        {
                                            string json = client.DownloadString(url);
                                            try
                                            {
                                                if (!String.IsNullOrEmpty(json))
                                                {
                                                    SMSBlast smsblast = (new JavaScriptSerializer()).Deserialize<SMSBlast>(json);
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                Response.Write("A + " + Pesan + "<br/>");
                                            }
                                        }

                                        //Response.Write(Pesan + "<br/>");
                                    }
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        public class SMSBlast
        {
           // public City city { get; set; }
            //public List<List> list { get; set; }
            public string uid { get; set; }
            public string pwd { get; set; }
            public string mask { get; set; }
            public string msisdn { get; set; }
            public string sub { get; set; }
            public string msg { get; set; }
        }
    }
}
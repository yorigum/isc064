using System;
using System.Data;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI.WebControls;

namespace ISC064 {
    /// <summary>
    /// Security functions
    /// </summary>
    public class Act {
        #region public static void NoCache()
        public static void NoCache()
        {
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.CacheControl = "no-cache";
        }
        #endregion
        #region public static void CekInt(string Qstr)
        public static void CekInt(string Qstr)
        {
            //validasi harus integer
            try
            {
                int z = Convert.ToInt32(
                    HttpContext.Current.Request.QueryString[Qstr]
                    );
            }
            catch
            {
                HttpContext.Current.Response.Redirect("/CustomError/Deleted.html");
            }
        }
        #endregion
        #region private string CnnSSO
        public static string CnnSSO
        {
            get
            {
                return "server=.; uid=batavianet;pwd=iNDigo100; database=000SSO";
            }
        }
        #endregion

        #region public static void Pass()
        public static void Pass()
        {
            //Fungsi agar setiap kali terjadi build tidak perlu log-in ulang
            if (HttpContext.Current.Session["UserID"] == null && Mi.isDevelopment)
            {
                UserID = "W";
                SecLevel = Db.SingleString(
                    "SELECT SecLevel FROM " + Mi.DbPrefix + "SECURITY..USERNAME WHERE UserID = '" + UserID + "'");
            }

            if (HttpContext.Current.Session["UserID"] == null)
                HttpContext.Current.Response.Redirect("/CustomError/Restricted.html");
            else
            {
                CekLogin(); //cek tabel LOGIN
                CekSecLevel(); //cek security level
            }
        }
        #endregion
        public static string Foto(string UserID)
        {
            string foto = "";
            string rs = Db.SingleString("SELECT ISNULL(Foto, '')"
                + " FROM " + Mi.DbPrefix + "SECURITY..USERNAME"
                + " WHERE "
                + " UserID = '" + UserID + "'");

            if (rs == null || rs == "")
            {
                foto = "/Media/User.png";
            }
            else
            {
                foto = rs;
            }
            return foto;
        }

        #region public static bool Sec(string Halaman)
        public static bool Sec(string Halaman)
        {
            bool permit = false;

            System.Data.DataTable rs = Db.Rs("SELECT Sifat"
                + " FROM " + Mi.DbPrefix + "SECURITY..PAGEDENY"
                + " WHERE "
                + " UserID = '" + UserID + "'"
                + " AND Halaman = '" + Halaman + "'");

            if (rs.Rows.Count != 0)
            {
                //konfigurasi khusus
                if (!(bool)rs.Rows[0][0]) //1 = deny
                    permit = true; //granted
            }
            else
            {
                //konfigurasi normal mengikuti security level
                int c = Db.SingleInteger("SELECT COUNT(Kode)"
                    + " FROM " + Mi.DbPrefix + "SECURITY..PAGESEC "
                    + " WHERE "
                    + " Kode = '" + SecLevel + "'"
                    + " AND Halaman = '" + Halaman + "'");

                if (c != 0)
                    permit = true;
            }

            return permit;
        }
        #endregion

        #region private static void CekLogin()
        private static void CekLogin()
        {
            string uid = HttpContext.Current.Session["UserID"].ToString();
            string sid = HttpContext.Current.Session.SessionID;

            int c = Db.SingleInteger("SELECT COUNT(LogID)"
                + " FROM " + Mi.DbPrefix + "SECURITY..LOGIN "
                + " WHERE "
                + " UserID = '" + uid + "'"
                + " AND SessionID = '" + sid + "'"
                + " AND Status = 'A'"
                );

            if (c == 0)
                HttpContext.Current.Response.Redirect("/CustomError/Restricted.html");
            else
            {
                //time extention
                Db.Execute("UPDATE " + Mi.DbPrefix + "SECURITY..LOGIN"
                    + " SET TglExpire = DATEADD(mi,45,GETDATE())"
                    + " WHERE "
                    + " UserID = '" + uid + "'"
                    + " AND SessionID = '" + sid + "'"
                    + " AND Status = 'A'"
                    );
        
            }
        }
        #endregion
        #region private static void CekSecLevel()
        private static void CekSecLevel()
        {
            if (!Sec(HttpContext.Current.Request.PhysicalPath))
                HttpContext.Current.Response.Redirect("/CustomError/SecLevel.html");
        }
        #endregion

        #region public static string UserID
        public static string UserID
        {
            get
            {
                if (HttpContext.Current.Session["UserID"] != null)
                    return (string)HttpContext.Current.Session["UserID"];
                else
                    return "";
            }
            set { HttpContext.Current.Session["UserID"] = value; }
        }
        #endregion
        #region public static string Username
        public static string Username
        {
            get
            {
                if (HttpContext.Current.Session["Username"] != null)
                    return (string)HttpContext.Current.Session["Username"];
                else
                    return "";
            }
            set { HttpContext.Current.Session["Username"] = value; }
        }
        #endregion
        #region public static string SecLevel
        public static string SecLevel
        {
            get
            {
                if (HttpContext.Current.Session["SecLevel"] != null)
                    return (string)HttpContext.Current.Session["SecLevel"];
                else
                    return "";
            }
            set { HttpContext.Current.Session["SecLevel"] = value; }
        }
        #endregion
        #region public static string IP
        public static string IP
        {
            get { return HttpContext.Current.Request.UserHostAddress; }
        }
        #endregion
        //13-02-2018 stefanus dari ISC064 SentraLand
        public static string Hash(string pInput)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            byte[] data = System.Text.Encoding.ASCII.GetBytes(pInput);
            data = md5.ComputeHash(data);

            string x = "";
            for (int i = 0; i < data.Length; i++)

                x += data[i].ToString("x2");

            return x;
        }
        #region public static void UserProjectList(DropDownList d)
        public static void UserProjectList(DropDownList d)
        {
            string strSql = "SELECT * FROM ISC064_SECURITY..PROJECTSEC WHERE UserID='" + UserID + "' AND Granted = 1";

            DataTable rs = Db.Rs(strSql);
            if (rs.Rows.Count == 0)
            {
                d.Items.Add(new ListItem { Text = "Project :", Value = "Project" });
            }
            else
            {
                for (int i = 0; i < rs.Rows.Count; i++)
                {
                    string v = rs.Rows[i]["Project"].ToString();
                    string t = v + " - " + rs.Rows[i]["NamaProject"].ToString();
                    d.Items.Add(new ListItem(t, v));
                }
            }
        }
        #endregion
        #region public static void ProjectList(CheckBoxList ch)
        public static void ProjectList(CheckBoxList ch)
        {
            string strSql = "SELECT * FROM ISC064_SECURITY..REF_PROJECT";
            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string t = rs.Rows[i]["Nama"].ToString();
                string v = rs.Rows[i]["Project"].ToString();
                ch.Items.Add(new ListItem(t, v));
            }
        }
        #endregion
        #region public static void ProjectList(DropDownList d)
        public static void ProjectList(DropDownList d)
        {
            string strSql = "SELECT * FROM ISC064_SECURITY..PROJECTSEC A "
                            + "INNER JOIN ISC064_SECURITY..REF_PROJECT B ON A.Project = B.Project "
                            + " WHERE A.UserID='" + UserID + "' AND A.Granted = 1";

            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["Project"].ToString();
                string t = v + " - " + rs.Rows[i]["Nama"].ToString();
                d.Items.Add(new ListItem(t, v));
            }
        }

        #region public static void ProjectList(DropDownList d,string Pers)
        public static void ProjectList(DropDownList d, string Pers)
        {
            string strSql = "SELECT * FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT A "
                              + " WHERE A.PROJECT IN (SELECT PROJECT FROM " + Mi.DbPrefix + "SECURITY..PROJECTSEC WHERE UserID = '" + UserID + "' AND Granted = 1)"
                              + " AND A.Pers = '" + Pers + "'";

            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["Project"].ToString();
                string t = v + " - " + rs.Rows[i]["Nama"].ToString();
                d.Items.Add(new ListItem(t, v));
            }
        }
        #endregion
        public static void PersList(DropDownList d)
        {
            string strSql = "SELECT * FROM ISC064_SECURITY..PTSec A "
                            + "INNER JOIN ISC064_SECURITY..REF_PERS B ON A.Pers = B.Pers "
                            + " WHERE A.UserID='" + Act.UserID + "' AND A.Granted = 1";

            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string t = rs.Rows[i]["Nama"].ToString();
                string v = rs.Rows[i]["Pers"].ToString();
                d.Items.Add(new ListItem(t, v));
            }
        }

        #endregion
        #region public static void ProjectAccess(CheckBoxList ch)
        public static void ProjectAccess(CheckBoxList ch)
        {
            string strSql = "SELECT * FROM ISC064_SECURITY..PROJECTSEC WHERE UserID = '" + UserID + "' AND Granted = 1";
            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string t = rs.Rows[i]["NamaProject"].ToString();
                string v = rs.Rows[i]["Project"].ToString();
                ch.Items.Add(new ListItem(t, v));
            }
        }
        #endregion
        #region public static void ProjectAccess(DropDownList d)
        public static void ProjectAccess(DropDownList d)
        {
            string strSql = "SELECT * FROM ISC064_SECURITY..PROJECTSEC WHERE UserID = '" + UserID + "' AND Granted = 1";
            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string t = rs.Rows[i]["NamaProject"].ToString();
                string v = rs.Rows[i]["Project"].ToString();
                d.Items.Add(new ListItem(t, v));
            }
        }
        #endregion

        #region public static string NamaPT(string ProjectID)
        public static string NamaPT(string ProjectID)
        {
            return Db.SingleString("SELECT ISNULL(Pers, '') FROM ISC064_SECURITY..REF_PROJECT WHERE Project = '" + ProjectID + "'");
        }
        #endregion
        #region public static string ProjectListSql
        public static string ProjectListSql
        {
            get
            {
                DataTable rs = Db.Rs("SELECT A.PROJECT FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT A "
                                  + " WHERE A.PROJECT IN (SELECT PROJECT FROM " + Mi.DbPrefix + "SECURITY..PROJECTSEC WHERE UserID = '" + UserID + "' AND Granted = 1)");
                string s = "";

                if (rs.Rows.Count == 0)
                {
                    s = "''";
                }
                else
                {
                    for (int i = 0; i < rs.Rows.Count; i++)
                    {
                        if (s != "")
                            s += ",";
                        s += "'" + rs.Rows[i]["Project"] + "'";
                    }
                }
                return s;
            }
        }
        #endregion
        #region public static string ProjectList(string Pers)
        public static string ProjectList(string Pers)
        {
            DataTable rs = Db.Rs("SELECT A.PROJECT FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT A "
                              + " WHERE A.PROJECT IN (SELECT PROJECT FROM " + Mi.DbPrefix + "SECURITY..PROJECTSEC WHERE UserID = '" + UserID + "' AND Granted = 1)"
                              + " AND A.Pers = '" + Pers + "'"
                              );
            string s = "";
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (s != "")
                    s += ",";
                s += "'" + rs.Rows[i]["Project"] + "'";
            }

            return s;
        }
        #endregion
        #region public static bool AksesProject(string ProjectID)
        public static bool AksesProject(string ProjectID)
        {
            return Db.SingleInteger("SELECT COUNT(*) FROM ISC064_SECURITY..PROJECTSEC WHERE UserID = '" + UserID + "' AND Project = '" + ProjectID + "' AND Granted = 1") > 0;
        }
        #endregion
        #region public static bool AksesProject(string ProjectID, string User)
        public static bool AksesProject(string ProjectID, string User)
        {
            return Db.SingleInteger("SELECT COUNT(*) FROM ISC064_SECURITY..PROJECTSEC WHERE UserID = '" + User + "' AND Project = '" + ProjectID + "' AND Granted = 1") > 0;
        }
        #endregion
        #region public static bool AksesPers(string PersID)
        public static bool AksesPers(string PersID)
        {
            return Db.SingleInteger("SELECT COUNT(*) FROM ISC064_SECURITY..PTSEC WHERE UserID = '" + UserID + "' AND Pers = '" + PersID + "' AND Granted = 1") > 0;
        }
        #endregion
        #region public static bool AksesPers(string PersID, string User)
        public static bool AksesPers(string PersID, string User)
        {
            return Db.SingleInteger("SELECT COUNT(*) FROM ISC064_SECURITY..PTSEC WHERE UserID = '" + User + "' AND Pers = '" + PersID + "' AND Granted = 1") > 0;
        }
        #endregion
    }
}

using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.Configuration;

namespace ISC064
{
    /// <summary>
    /// Summary description for Param.
    /// </summary>
    public class Param
    {
        #region public static string AkuntingFilePath
        public static string AkuntingFilePath
        {
            get
            {
                System.Configuration.AppSettingsReader x = new System.Configuration.AppSettingsReader();
                string s = (string)x.GetValue("akuntingFilePath", typeof(string));
                return s;
            }
        }
        #endregion
        #region public static string Lantai
        public static string Lantai
        {
            get
            {
                System.Configuration.AppSettingsReader x = new System.Configuration.AppSettingsReader();
                string s = (string)x.GetValue("lantai", typeof(string));
                return s;
            }
        }
        #endregion
        #region public static string NoUnit
        public static string NoUnit
        {
            get
            {
                System.Configuration.AppSettingsReader x = new System.Configuration.AppSettingsReader();
                string s = (string)x.GetValue("nounit", typeof(string));
                return s;
            }
        }
        #endregion
        #region public static string BackupDB
        public static string FolderBackup
        {

            get
            {

                System.Configuration.AppSettingsReader x = new System.Configuration.AppSettingsReader();

                string s = (string)x.GetValue("FolderBackup", typeof(string));

                return s;

            }

        }
        #endregion
        public static bool Exist(string ParamID)
        {
            return !String.IsNullOrEmpty(GetParam(ParamID));
        }
        public static void InsertParam(string Value, string ParamID, string Keterangan)
        {
            Db.Execute("INSERT INTO REF_PARAM(ParamID, Keterangan, Value) VALUES('" + ParamID + "', '" + Keterangan + "', '" + Value + "')");
        }
        public static void UpdateParam(string Value, string ParamID)
        {
            Db.Execute("UPDATE REF_PARAM SET Value = '" + Value + "' WHERE ParamID = '" + ParamID + "'");
        }
        public static void InsertOrUpdate(string Value, string ParamID, string Keteragan)
        {
            if (!Exist(ParamID))
                InsertParam(Value, ParamID, Keteragan);
            else
                UpdateParam(Value, ParamID);
        }
        public static string PathFilePDF
        {
            get
            {
                return GetConfig("PathFilePDF");
            }
            set
            {
                SetConfig("PathFilePDF", value.ToString());
            }
        }
        public static string PathFilePDFMarketingJual
        {
            get
            {
                return GetConfig("PathFilePDFMarketingJual");
            }
            set
            {
                SetConfig("PathFilePDFMarketingJual", value.ToString());
            }
        }
        public static string PathFilePDFFinanceAR
        {
            get
            {
                return GetConfig("PathFilePDFFinanceAR");
            }
            set
            {
                SetConfig("PathFilePDFFinanceAR", value.ToString());
            }
        }
        public static string PathFilePDFCollection
        {
            get
            {
                return GetConfig("PathFilePDFCollection");
            }
            set
            {
                SetConfig("PathFilePDFCollection", value.ToString());
            }
        }
        public static string PathFilePDFLegal
        {
            get
            {
                return GetConfig("PathFilePDFLegal");
            }
            set
            {
                SetConfig("PathFilePDFLegal", value.ToString());
            }
        }
        public static string PathFilePDFKPA
        {
            get
            {
                return GetConfig("PathFilePDFKPA");
            }
            set
            {
                SetConfig("PathFilePDFKPA", value.ToString());
            }
        }
        public static string PathLinkFilePDF
        {
            get
            {
                return GetConfig("PathLinkFilePDF");
            }
            set
            {
                SetConfig("PathLinkFilePDF", value.ToString());
            }
        }
        public static string PathLinkFilePDFMarketingJual
        {
            get
            {
                return GetConfig("PathLinkFilePDFMarketingJual");
            }
            set
            {
                SetConfig("PathLinkFilePDFMarketingJual", value.ToString());
            }
        }
        public static string PathLinkFilePDFFinanceAR
        {
            get
            {
                return GetConfig("PathLinkFilePDFFinanceAR");
            }
            set
            {
                SetConfig("PathLinkFilePDFFinanceAR", value.ToString());
            }
        }
        public static string PathLinkFilePDFCollection
        {
            get
            {
                return GetConfig("PathLinkFilePDFCollection");
            }
            set
            {
                SetConfig("PathLinkFilePDFCollection", value.ToString());
            }
        }
        public static string PathLinkFilePDFLegal
        {
            get
            {
                return GetConfig("PathLinkFilePDFLegal");
            }
            set
            {
                SetConfig("PathLinkFilePDFLegal", value.ToString());
            }
        }
        public static string PathLinkFilePDFKPA
        {
            get
            {
                return GetConfig("PathLinkFilePDFKPA");
            }
            set
            {
                SetConfig("PathLinkFilePDFKPA", value.ToString());
            }
        }
        public static string PathWkhtml
        {
            get
            {
                return GetConfig("PathWkhtml");
            }
            set
            {
                SetConfig("PathWkhtml", value.ToString());
            }
        }

        #region public static string IPAddress
        public static string IPAddress
        {

            get
            {

                System.Configuration.AppSettingsReader x = new System.Configuration.AppSettingsReader();

                string s = (string)x.GetValue("IPAddress", typeof(string));

                return s;

            }

        }
        #endregion
        #region public static string FolderPDF
        public static string FolderPDF
        {

            get
            {

                System.Configuration.AppSettingsReader x = new System.Configuration.AppSettingsReader();

                string s = (string)x.GetValue("FolderPDF", typeof(string));

                return s;

            }

        }
        #endregion
        #region public static string GeneratorPDF
        public static string GeneratorPDF
        {

            get
            {

                System.Configuration.AppSettingsReader x = new System.Configuration.AppSettingsReader();

                string s = (string)x.GetValue("GeneratorPDF", typeof(string));

                return s;

            }

        }
        #endregion

        //Driver
        public static string GetParam(string ParamID)
        {
            return Db.SingleString("SELECT Value FROM REF_PARAM WHERE ParamID = '" + Cf.Str(ParamID) + "'");
        }
        private static void SetParam(string ParamID, string ParamVal)
        {
            Db.Execute("UPDATE REF_PARAM SET Value = '" + ParamVal + "'"
                    + " WHERE ParamID = '" + ParamID + "'");
        }
        private static string GetConfig(string key)
        {
            return new AppSettingsReader().GetValue(key, typeof(string)).ToString();
        }
        private static void SetConfig(string key, string value)
        {
            Configuration cfg = WebConfigurationManager.OpenWebConfiguration("~");
            AppSettingsSection x = (AppSettingsSection)cfg.GetSection("appSettings");
            x.Settings[key].Value = value;
            cfg.Save();
        }
    }
}

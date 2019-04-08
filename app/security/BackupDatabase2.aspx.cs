using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;

namespace ISC064.SECURITY
{
    public partial class BackupDatabase2 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            //Act.Pass();
            //Act.NoCache();
            tglbackup.Text = Cf.Day(TglBackup);

            if (System.IO.File.Exists(Request.PhysicalApplicationPath
                  + "BACKUP_DB\\ISC064_SECURITY_" + Cf.Tgl112(TglBackup) + ".BAK"))
                linksec.Text = "<a href=\"BACKUP_DB/ISC064_SECURITY_" + Cf.Tgl112(TglBackup) + ".bak\">Download</a>";

            if (System.IO.File.Exists(Request.PhysicalApplicationPath
                 + "BACKUP_DB\\ISC064_MARKETINGJUAL_" + Cf.Tgl112(TglBackup) + ".BAK"))
                linkmkt.Text = "<a href=\"BACKUP_DB/ISC064_MARKETINGJUAL_" + Cf.Tgl112(TglBackup) + ".bak\">Download</a>";

            if (System.IO.File.Exists(Request.PhysicalApplicationPath
              + "BACKUP_DB\\ISC064_FINANCEAR_" + Cf.Tgl112(TglBackup) + ".BAK"))
                linkfin.Text = "<a href=\"BACKUP_DB/ISC064_FINANCEAR_" + Cf.Tgl112(TglBackup) + ".bak\">Download</a>";

        }

        private DateTime TglBackup
        {
            get
            {
                return Convert.ToDateTime(Request.QueryString["TglBackup"]);
            }
        }

        public static string CnnBackup
        {
            get
            {
                System.Configuration.AppSettingsReader s = new System.Configuration.AppSettingsReader();
                string x = (string)s.GetValue("cnnBackup", typeof(string));
                s = null;
                return x;
            }
        }
        public static void ExecuteBackup(string strSql)
        {
            SqlConnection sqlCnn = new SqlConnection(CnnBackup);
            SqlCommand sqlCmd = new SqlCommand(strSql, sqlCnn);
            sqlCnn.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCnn.Close();
        }
        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion
    }
}

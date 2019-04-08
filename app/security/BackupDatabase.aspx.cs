using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Data.SqlClient;

namespace ISC064.SECURITY
{
    public partial class BackupDatabase : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            //Act.Pass();
            //Act.NoCache();
            if (!Page.IsPostBack)
            {
                tglbackup.Text = Cf.Day(DateTime.Today);
            }
            FeedBack();
            Js.Confirm(this, "Lanjutkan proses backup?\\n"
                + "");
        }

        private bool valid()
        {
            bool x = true;
            string s = "";


            if (!Cf.isTgl(tglbackup))
            {
                x = false;
                if (s == "") s = tglbackup.ID;
                tglbackupc.Text = "Tanggal";
            }
            else
                tglbackupc.Text = "";

            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Format Tanggal : Bulan / Tanggal / Tahun.\\n"
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );

            return x;
        }
        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "Backup Berhasil...";
            }
        }
        protected void backup_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                DateTime TglBackup = Convert.ToDateTime(tglbackup.Text);
                ExecuteBackup("Exec [spAutoBackup] '" + Param.FolderBackup + "','" + Cf.Tgl112(TglBackup) + "'");
                Response.Redirect("BackupDatabase2.aspx?TglBackup=" + TglBackup);
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

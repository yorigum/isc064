using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.FINANCEAR
{
    public partial class NoVAUpload : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();
            if (!Page.IsPostBack) Act.ProjectList(project);
            Bind();
            Js.Confirm(this, "Lanjutkan proses upload data no VA?");
            feed.Text = "";
        }

        protected void upload_Click(object sender, System.EventArgs e)
        {
            if (!file.PostedFile.FileName.EndsWith(".xls"))
            {
                Js.Alert(
                    this
                    , "Proses Upload Gagal.\\n"
                    + "File yang boleh di-upload adalah file dengan extension .xls saja."
                    , ""
                    );
            }
            else
            {
                string path = Request.PhysicalApplicationPath
                    + "Template\\NoVA_" + Session.SessionID + ".xls";

                Dfc.UploadFile(".xls", path, file);

                Cek(path);

                //Hapus file sementara tersebut dari hard-disk server
                Dfc.DeleteFile(path);
            }
        }

        private void Cek(string path)
        {
            string strSql = "SELECT * FROM [VA$]";
            DataTable rs = new DataTable();

            try
            {
                rs = Db.xls(strSql, path);
            }
            catch { }

            if (Rpt.ValidateXls(rs, rule, gagal))
                Save(path);
        }

        private void Save(string path)
        {
            int total = 0;

            string strSql = "SELECT * FROM [VA$]";
            DataTable rs = Db.xls(strSql, path);

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                if (Save(rs, i))
                    total++;
            }

            feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                + "Upload Berhasil  : " + total + " baris data";
        }

        private void Bind()
        {
            System.Text.StringBuilder x = new System.Text.StringBuilder();

            DataTable rs = Db.Rs("SELECT * FROM REF_ACC WHERE Project = '" + project.SelectedValue + "' ORDER BY Acc");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (x.Length != 0) x.Append("/");
                x.Append(rs.Rows[i]["Bank"].ToString());
            }

            TableCell c = rule.Rows[3].Cells[4];
            c.Text = x.ToString();
        }

        private bool Save(DataTable rs, int i)
        {
            string NoVA = Cf.Str(rs.Rows[i][0]);
            string NoUnit = Cf.Str(rs.Rows[i][1]);
            string Bank = Cf.Str(rs.Rows[i][2]);

            int nova = Db.SingleInteger("SELECT COUNT(*) FROM REF_VA WHERE NoVA = '" + NoVA + "'");
            int bank = Db.SingleInteger("SELECT COUNT(*) FROM REF_ACC WHERE Bank = '" + Bank + "'");
            int unit = Db.SingleInteger("SELECT COUNT(*) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT WHERE NoUnit = '" + NoUnit + "' AND Project = '" + project.SelectedValue + "'");

            if (nova == 0 && unit > 0 && bank > 0)
            {
                Db.Execute("EXEC spVARegis"
                    + " '" + NoVA + "'"
                    + ", '" + NoUnit + "'"
                    + ", '" + Bank + "'"
                    );

                //Project & NamaProject
                string NamaProject = Db.SingleString("SELECT Nama FROM ISC064_SECURITY..REF_PROJECT where Project ='" + project.SelectedValue + "'");

                //update project & namaproject
                Db.Execute("UPDATE REF_VA SET Project = '" + project.SelectedValue + "', NamaProject='" + NamaProject + "' WHERE NoVA ='" + NoVA + "'");

                DataTable rs2 = Db.Rs("SELECT "
                                + " NoVA AS [No. VA]"
                                + ",NoUnit AS [No. Unit]"
                                + ",Bank"
                                + ",Project"
                                + ",NamaProject"
                                + " FROM REF_VA "
                                + " WHERE NoVA  = '" + NoVA + "'");

                string KetLog = Cf.LogCapture(rs2);

                Db.Execute("EXEC spLogVA"
                    + " 'REGIS'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + KetLog + "'"
                    + ",'" + NoVA + "'"
                    );

                return true;
            }
            else
                return false;
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

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Data.OleDb;

namespace ISC064.FINANCEAR
{
    public partial class VAImporBNI : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!IsPostBack)
            {
                Act.ProjectList(project);
                fillAcc();
            }
        }
        private void fillAcc()
        {
            string Project = (project.SelectedIndex == 0) ? " AND Project IN (" + Act.ProjectListSql + ")" : " AND Project = '" + project.SelectedValue + "'";
            DataTable rs = Db.Rs("SELECT * FROM REF_ACC WHERE Rekening !=''" + Project);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["Acc"].ToString();
                string t = v + " : " + rs.Rows[i]["Bank"] + " " + rs.Rows[i]["Rekening"];
                bank.Items.Add(new ListItem(t, v));
            }
        }
        private bool valid()
        {
            string s = "";
            bool x = true;

            if (bank.SelectedIndex == 0)
            {
                x = false;
                if (s == "") s = bank.ID;
                bankc.Text = "Pilih";
            }
            else
                bankc.Text = "";

            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Rekening Bank harus dipilih.\\n"
                    , "document.getElementById('" + s + "').focus();"
                    );

            return x;
        }
        protected bool filevalid()
        {
            bool x = true;
            string path = Request.PhysicalApplicationPath
                + "VA\\VA_" + Session.SessionID + ".xls";

            if (!file.PostedFile.FileName.EndsWith(".xls")) x = false;

            if (!x)
                Js.Alert(
                    this
                    , "Proses Upload Gagal.\\n"
                    + "Aturan Proses :\\n"
                    + "1. File Teks hanya yang berekstensi \"xls\" saja.\\n"
                    + "2. File Teks sudah pernah diproses sebelumnya."
                    , ""
                    );

            return x;
        }
        protected void upload_Click(object sender, System.EventArgs e)
        {

            if (valid() && filevalid())
            {
                string path = Request.PhysicalApplicationPath
                    + "VA\\VA_" + Session.SessionID + ".xls";

                Dfc.UploadFile(".xls", path, file);

                Response.Redirect("VAImporBNI2.aspx?bank=" + bank.SelectedValue + "&path=" + path + "&project=" + project.SelectedValue);
            }
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

        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            bank.Items.Clear();
            bank.Items.Add(new ListItem("Bank : "));
            fillAcc();
        }
    }

}

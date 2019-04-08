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

namespace ISC064.financear
{
    public partial class VAUpload : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!IsPostBack)
            {
                fillAcc();
            }
        }
        private void fillAcc()
        {
            DataTable rs = Db.Rs("SELECT * FROM REF_ACC WHERE Project IN (" + Act.ProjectListSql + ")");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v2 = rs.Rows[i]["Acc"].ToString() + ";" + rs.Rows[i]["SubID"].ToString();
                string v = rs.Rows[i]["Acc"].ToString();
                string t = v + " : " + rs.Rows[i]["Bank"] + " " + rs.Rows[i]["Rekening"];
                bank.Items.Add(new ListItem(t, v2));
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
                + "VA\\VA_" + Session.SessionID + ".txt";

            if (!file.PostedFile.FileName.EndsWith(".txt")) x = false;

            if (!x)
                Js.Alert(
                    this
                    , "Proses Upload Gagal.\\n"
                    + "Aturan Proses :\\n"
                    + "1. File Teks hanya yang berekstensi \"txt\" saja.\\n"
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
                    + "VA\\VA_" + Session.SessionID + ".txt";

                Dfc.UploadFile(".txt", path, file);
                Response.Redirect("VAUpload2.aspx?bank=" + bank.SelectedValue + "&path=" + path);

                //Response.Redirect("VAUpload2.aspx?bank=" + bank.SelectedValue + "&path=" + path + "&project=" + project.SelectedValue);
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
    }

}

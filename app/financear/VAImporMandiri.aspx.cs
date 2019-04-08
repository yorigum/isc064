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

namespace ISC064.FINANCEAR
{
    public partial class VAImporMandiri : System.Web.UI.Page
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
            DataTable rs = Db.Rs("SELECT * FROM REF_ACC WHERE Rekening !='' AND Project IN (" + Act.ProjectListSql + ")");
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
                bankc.Text = "Choose";
            }
            else
                bankc.Text = "";

            if (!x)
                Js.Alert(
                    this
                    , "Invalid input.\\n\\n"
                    + "Process Rules :\\n"
                    + "1. Account Bank must be choosen.\\n"
                    , "document.getElementById('" + s + "').focus();"
                    );

            return x;
        }
        protected bool filevalid()
        {
            bool x = true;
            string path = Request.PhysicalApplicationPath
                + "VA\\VA_" + Session.SessionID + ".xls";
            
            if(File.Exists(path))
            {
                string strSql = "SELECT * FROM [VA$]";
                DataTable rs = Db.xls(strSql, path);

                for (int i = 0; i < rs.Rows.Count; i++)
                {
                    if (Cf.isTgl(rs.Rows[i][1]) == false)
                    {
                        x = false;
                        Js.Alert(
                            this
                            , "Process Upload Failed.\\n"
                            + "Process Rule :\\n"
                            + "\\n 1. Data VA or bank is not valid."
                            , ""
                            );
                    }
                }
            }

                    if (!file.PostedFile.FileName.EndsWith(".xls")) x = false;

            if (!x)
                Js.Alert(
                    this
                    , "Process Upload Failed.\\n"
                    + "Process Rule :\\n"
                    + "1. Files that can be uploaded is the file with extension .xls only."
                    + "2. Files has been processed before."
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

                Response.Redirect("VAImporMandiri2.aspx?bank=" + bank.SelectedValue + "&path=" + path);
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

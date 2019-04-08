using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace ISC064.FINANCEAR
{
    public partial class VAImporBRI : System.Web.UI.Page
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
                string v2 = rs.Rows[i]["Acc"].ToString() + ";" + rs.Rows[i]["SubID"].ToString();
				string v = rs.Rows[i]["Acc"].ToString();
				string t = v + " : " + rs.Rows[i]["Bank"] + " " + rs.Rows[i]["Rekening"];
				bank.Items.Add(new ListItem(t,v2));
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
                bankc.Text = "";
            }
            else
                bankc.Text = "";

            if (!x)
                Js.Alert(
                    this
                    , "Input tidak valid.\\n\\n"
                    + "Keterangan :\\n"
                    + "1. Bank belum dipilih.\\n"
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
                    , "Process Upload Failed.\\n"
                    + "Process Rule :\\n"
                    + "1. File yang bisa diupload hanya dengan ekstensi .xls saja."
                    + "2. File sudah diproses sebelumnya."
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

                Response.Redirect("VAImporBRI2.aspx?bank=" + bank.SelectedValue + "&path=" + path);
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
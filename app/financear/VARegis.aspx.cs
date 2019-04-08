using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.FINANCEAR
{
    public partial class VARegis : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                Act.ProjectList(project);
                fillAcc();
            }

            FeedBack();

            nounit.Attributes["ondblclick"] = "popDaftarUnit2('a');";
            btnpop2.Attributes.Add("modal-url", "DaftarUnit.aspx?va=1&project=" + project.SelectedValue);
        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "Pendaftaran Berhasil...";
                project.SelectedValue = Request.QueryString["project"];
                bank.Items.Clear();
                bank.Items.Add(new ListItem("Bank :"));
                fillAcc();
            }
        }

        private void fillAcc()
        {
            DataTable rs = Db.Rs("SELECT * FROM REF_ACC WHERE Rekening !='' AND Project = '" + project.SelectedValue + "'");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["Bank"].ToString();
                string t = v + " : " + rs.Rows[i]["Bank"] + " " + rs.Rows[i]["Rekening"];
                bank.Items.Add(new ListItem(t, v));
            }
        }

        private bool valid()
        {
            string s = "";
            bool x = true;

            string VA = Cf.Pk(start.Text);
            int c = Db.SingleInteger("SELECT COUNT(*) FROM REF_VA WHERE NoVA = '" + VA + "'");

            if (VA == "")
            {
                x = false;
                if (s == "") s = start.ID;
                startc.Text = "Kosong";
            }
            else
                startc.Text = "";

            if (c > 0)
            {
                x = false;
                if (s == "") s = start.ID;
                startc.Text = "Duplikat";
            }
            else
                startc.Text = "";

            if (bank.SelectedIndex == 0)
            {
                x = false;
                bankc.Text = "Pilih bank";
            }
            else
                bankc.Text = "";

            string unit = Cf.Pk(nounit.Text);
            int c2 = Db.SingleInteger("SELECT COUNT(*) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT WHERE NoUnit = '" + unit + "'");

            if (c2 == 0)
            {
                x = false;
                if (s == "") s = nounit.ID;
                nostockc.Text = "Unit tidak available";
            }
            else
                nostockc.Text = "";

            if (!x)
            {
                if (!x)
                    Js.Alert(
                        this
                        , "Input Tidak Valid.\\n\\n"
                        + "Aturan Proses :\\n"
                        + "1. No. VA tidak boleh kosong dan tidak boleh duplikat.\\n"
                        + "2. No. Unit harus available.\\n"
                        + "3. Bank tidak boleh kosong.\\n"
                        , "document.getElementById('" + s + "').focus();"
                        + "document.getElementById('" + s + "').select();"
                        );
            }

            return x;
        }

        protected void ok_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {

                string NoVA = Cf.Str(start.Text);
                string NoUnit = Cf.Str(nounit.Text);
                string Bank = Cf.Str(bank.SelectedValue);
                string Project = Db.SingleString("SELECT Project FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT WHERE NoUnit = '" + NoUnit + "'");
                string NamaProject = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + Project + "'");
                int x = Db.SingleInteger("SELECT COUNT(*) FROM REF_VA WHERE NoVA = '" + NoVA + "'");
                if (x == 0)
                {
                    Db.Execute("EXEC spVARegis"
                        + " '" + NoVA + "'"
                        + ", '" + NoUnit + "'"
                        + ", '" + Bank + "'"
                        );

                    Db.Execute("UPDATE REF_VA SET Project = '" + Project + "',NamaProject = '" + NamaProject + "' WHERE NoVA = '" + NoVA + "'");

                    DataTable rs = Db.Rs("SELECT "
                        + " NoVA AS [No. VA]"
                        + ",NoUnit AS [No. Unit]"
                        + ",Bank"
                        + " FROM REF_VA "
                        + " WHERE NoVA  = '" + NoVA + "'");

                    string KetLog = Cf.LogCapture(rs);

                    Db.Execute("EXEC spLogVA"
                        + " 'REGIS'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + KetLog + "'"
                        + ",'" + NoVA + "'"
                        );
                }

                Response.Redirect("VARegis.aspx?done=1&project=" + project.SelectedValue);
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
            bank.Items.Add(new ListItem("Bank :"));
            fillAcc();
        }
    }
}

using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.ADMINJUAL
{
    public partial class AgentLevelDaftar : System.Web.UI.Page
    {
        protected System.Web.UI.HtmlControls.HtmlInputButton cancel;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                Js.Focus(this, namaLevel);

            }
            Fill();
            FeedBack();
        }
        protected decimal TotalKomisi
        {
            get;set;
        }
        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "<a href=\"javascript:popEditJenis('" + Request.QueryString["done"] + "')\">"
                        + "Pendaftaran Berhasil..."
                        + "</a>";
            }
        }

        private bool valid()
        {
            string s = "";
            bool x = true;
            if (Cf.isEmpty(namaLevel)) {
                x = false;
                s = namaLevel.ID;
            }
            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Jenis Unit Properti tidak boleh kosong.\\n"
                    + "2. Nama Jenis Unit tidak boleh kosong.\\n"
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );

            return x;

        }


        void Fill() {

            inBaru.Items.Clear();
            DataTable rs = Db.Rs("Select * from MS_AGENT_LEVEL where tipe='Internal' order by LevelID desc");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["LevelID"].ToString();
                string x = rs.Rows[i]["Nama"].ToString();
                string t = x + " (" + rs.Rows[i]["LevelID"] + ")";

                inBaru.Items.Add(new ListItem(t, v));
            }
            if (rs.Rows.Count != 0)
            {
                inBaru.SelectedIndex = 0;
                inBaru.Attributes["ondblclick"] = "popEditAgentLevel("
                    + "this.options[this.selectedIndex].value)";
            }
            exBaru.Items.Clear();
            rs = Db.Rs("Select * from MS_AGENT_LEVEL where tipe='External' order by LevelID desc");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["LevelID"].ToString();
                string x = rs.Rows[i]["Nama"].ToString();
                string t = x + " (" + rs.Rows[i]["LevelID"] + ")";

                exBaru.Items.Add(new ListItem(t, v));
            }
            if (rs.Rows.Count != 0)
            {
                exBaru.SelectedIndex = 0;
                exBaru.Attributes["ondblclick"] = "popEditAgentLevel("
                    + "this.options[this.selectedIndex].value)";
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
        protected void save_Click(object sender, EventArgs e)
        {
            if (valid()) {
                Db.Execute("exec spAgentLevelDaftar '"+ namaLevel.Text +"','"+ tipe.SelectedValue +"'");
                Response.Redirect("AgentLevelDaftar.aspx?done=1");
            }
            
        }
}
}

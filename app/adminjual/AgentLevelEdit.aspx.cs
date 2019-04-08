using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.ADMINJUAL
{
    public partial class AgentLevelEdit : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Act.Sec("ED:" + Request.PhysicalPath))
            {
                ok.Enabled = false;
                save.Enabled = false;
            }

            if (!Page.IsPostBack)
            {
                //Bind(); //tanggal dan bulan
                Fill();
            }

            FeedBack();
            Js.Confirm(this, "Lanjutkan proses edit data agent level?\\n"
                + "");
        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "Edit Berhasil...";
            }
        }

      

        private void Fill()
        {
            DataTable rs = Db.Rs("select * from ms_agent_level where levelid='"+ Levelid +"'");
            LevelID.Text = rs.Rows[0][0].ToString();
            nama.Text = rs.Rows[0][1].ToString();
        }

        private bool valid()
        {
            string s = "";
            bool x = true;

            


            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Jenis Unit Properti tidak boleh kosong.\\n"
                    + "2. Nama Jenis Unit Properti tidak boleh kosong.\\n"
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );

            return x;
        }


        protected void ok_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                Db.Execute("update ms_agent_level set nama='" + nama.Text + "' where LevelID='" + Levelid + "'");
                Js.Close(this);
                Response.Redirect("AgentLevelDaftar.aspx?done=1");
            }
            Js.Close(this);
        }

        private void Page_Load()
        {
            throw new NotImplementedException();
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            if (valid()) {
                Db.Execute("update ms_agent_level set nama='"+ nama.Text +"' where LevelID='"+ Levelid +"'");
                
            }

        }

        private string Levelid
        {
            get
            {
                return Cf.Pk(Request.QueryString["LevelID"]);
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

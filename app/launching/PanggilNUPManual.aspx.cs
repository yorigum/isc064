using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LAUNCHING
{
    public partial class PanggilNUPManual : System.Web.UI.Page
    {
        private int CallID;
        private bool isNext = false;
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();
            if (!Page.IsPostBack)
            {
                //datang.Enabled = false;
                //next.Enabled = false;
                //tidakdatang.Enabled = false;
                //selesai.Enabled = false;
                isNext = true;
            }

            string query = "select ISNULL(MAX(ID),0) from MS_LAUNCHING_CALL Where NUPID='" + Cf.Pk(nomor.Text) + "'";

            CallID = Db.SingleInteger(query);
            //Response.Write(Act.UserID);
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


        protected void call_Click(object sender, EventArgs e)
        {
            Call();
        }
        void Call()
        {
            var ada = Db.SingleInteger("Select Count(*) from MS_LAUNCHING_CALL where NUPID='" + Cf.Pk(nomor.Text) + "' and CounterID !=" + CounterID) > 0;
            err.Text = "";
            if (ada)
            {
                err.Text = "Nomor telah dipanggil di counter lain.. silahkan klik nomor selanjutnya dan panggil.";
            }
            else
            {
                if (Cf.isNumerik(nomor.Text))
                {

                    Db.Execute("Exec spCall_NUP '" + Cf.Pk(nomor.Text) + "'," + CounterID);
                }else
                {
                    err.Text = "Nomor tidak valid";
                }
            }
        }

        private int CounterID
        {
            get
            {
                return Db.SingleInteger("Select ISNULL((Select top 1 ID from " + Mi.DbPrefix + "SECURITY..REF_ADMIN_LAUNCHING where USerID='"+ Act.UserID +"'),0)");
            }
        }


        protected void datang_Click(object sender, EventArgs e)
        {
            Db.Execute("update ms_launching_call set Status=1 where ID=" + CallID);
            Response.Redirect("ClosingNUP.aspx?id=" + nomor.Text);
            //Init();
        }

    

        protected void tidakdatang_Click(object sender, EventArgs e)
        {
            Db.Execute("update ms_launching_call set Status=2 where ID=" + CallID);
        }
    }
}

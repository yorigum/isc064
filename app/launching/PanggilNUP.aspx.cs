using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LAUNCHING
{
    public partial class PanggilNUP : System.Web.UI.Page
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

            Init();
            string query = "select ISNULL(MAX(ID),0) from MS_LAUNCHING_CALL Where NUPID='" + Cf.Pk(nomor.Text) + "'";

            CallID = Db.SingleInteger(query);
            //Response.Write(Act.UserID);
        }
        void Init()
        {
            string query = "Select top 1 * from MS_LAUNCHING_CALL where CounterID=(Select top 1 ID from " + Mi.DbPrefix + "SECURITY..REF_ADMIN_LAUNCHING where USerid='" + Act.UserID + "')  order by ID desc";

            var rs = Db.Rs(query);
            if (rs.Rows.Count > 0)
            {
                var r = rs.Rows[0];

                if(isNext)
                    nomor.Text = r["NUPID"].ToString();

                // nup sedang dipanggil
                if ((byte)r["status"] == 0)
                {
                    next.Enabled =
                    call.Enabled = false;

                    datang.Enabled =
                    tidakdatang.Enabled = true;

                }
                // sedang pilih unit
                else if ((byte)r["status"] == 1)
                {
                    next.Enabled =
                    tidakdatang.Enabled =
                    datang.Enabled =
                    call.Enabled = false;
                    next.Enabled = true;
                }
                else if ((byte)r["status"] == 2)
                {
                    tidakdatang.Enabled =
                    datang.Enabled = false;

                    call.Enabled =
                    next.Enabled = true;
                }
                else
                {
                    getNext();
                }
            } else
            {
                getNext();
            }

            //string 



        }
        void getNext()
        {

            isNext = true;
            string query = "Select ISNULL(( select top 1 NoNUP from ms_nup a where  Status=1 and "
                    + " (Select count(*) from MS_LAUNCHING_CALL where NUPID=a.NoNUP ) = 0 ),'-')";

            nomor.Text = Db.SingleString(query);
            next.Enabled =
            datang.Enabled =
            tidakdatang.Enabled = false;
            call.Enabled = true;
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
            isNext = false;
        }
        void Call()
        {
            var ada = Db.SingleInteger("Select Count(*) from MS_LAUNCHING_CALL where NUPID='" + Cf.Pk(nomor.Text) + "'  and CounterID !=" + CounterID) > 0;
            err.Text = "";
            if (ada)
            {
                err.Text = "Nomor telah dipanggil di counter lain.. silahkan klik nomor selanjutnya dan panggil.";
            }
            else
            {
                Db.Execute("Exec spCall_NUP '" + Cf.Pk(nomor.Text) + "'," + CounterID);
            }
            Init();
        }

        private int CounterID
        {
            get
            {
                return Db.SingleInteger("Select ISNULL((Select top 1 ID from " + Mi.DbPrefix + "SECURITY..REF_ADMIN_LAUNCHING where USerID='"+ Act.UserID +"'),0)");
            }
        }

        protected void next_Click(object sender, EventArgs e)
        {
            getNext();
        }

        protected void datang_Click(object sender, EventArgs e)
        {
            Db.Execute("update ms_launching_call set Status=1 where ID=" + CallID);
            Response.Redirect("ClosingNUP.aspx?id=" + nomor.Text);
            //Init();
        }

        protected void selesai_Click(object sender, EventArgs e)
        {
            next.Enabled = true;
            Db.Execute("update ms_launching_call set Status=3 where ID=" + CallID);
            Init();
        }

        protected void tidakdatang_Click(object sender, EventArgs e)
        {
            Db.Execute("update ms_launching_call set Status=2 where ID=" + CallID);
            Init();
        }
    }
}

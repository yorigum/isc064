using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LAUNCHING
{
    public partial class NUPDaftar2 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                Fill();
            }
        }

        private void Fill()
		{
            nonup.Text = NoNUP;
            nonup.Font.Size = 35;

            DataTable dtNUP = Db.Rs("SELECT * FROM MS_NUP WHERE NoNUP = " + NoNUP);

            if (dtNUP.Rows.Count > 0)
            {
                string namaCS = dtNUP.Rows[0]["NamaCustomer"].ToString();//Db.SingleString("SELECT ISNULL(Nama, '') FROM MS_CUSTOMER WHERE NoCustomer = " + Convert.ToInt32(dtNUP.Rows[0]["NoCustomer"]));
                string namaAG = dtNUP.Rows[0]["NamaAgent"].ToString();//Db.SingleString("SELECT ISNULL(Nama, '') FROM MS_AGENT WHERE NoAgent = " + Convert.ToInt32(dtNUP.Rows[0]["NoAgent"]));

                customer.Text = namaCS;
                agent.Text = namaAG;
                asp.HRef = "javascript:openPopUp('PrintNUP.aspx?NoNUP=" + NoNUP + "','920','650')";
                adaftar.HRef = "NUPDaftar.aspx";
            }
		}

        private string NoNUP
        {
            get
            {
                return Cf.Pk(Request.QueryString["No"]);
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

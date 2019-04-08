using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LAUNCHING
{
    public partial class RegisReception3 : System.Web.UI.Page
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
            customer.Font.Size = 35;

            DataTable dtNUP = Db.Rs("SELECT * FROM MS_NUP a INNER JOIN MS_CUSTOMER b ON a.NoCustomer = b.NoCustomer WHERE a.NoNUP = '" + NoNUP + "' AND a.Tipe = '" + Tipe + "' AND a.Project = '" + Project + "'");

            if (dtNUP.Rows.Count > 0)
            {
                string namaCS = dtNUP.Rows[0]["Nama"].ToString();

                customer.Text = namaCS;
            }


        }

        private string NoNUP
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoNUP"]);
            }
        }
        private string Tipe
        {
            get
            {
                return Cf.Pk(Request.QueryString["Tipe"]);
            }
        }

        private string Project
        {
            get
            {
                return Cf.Pk(Request.QueryString["project"]);
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

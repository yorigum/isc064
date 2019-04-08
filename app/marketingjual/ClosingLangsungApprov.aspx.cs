using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
    public partial class ClosingLangsungApprov : System.Web.UI.Page
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
            DataTable rs = Db.Rs("SELECT A.NoKontrak,A.TglKontrak,B.Nama AS NamaCS,C.Nama AS NamaAG,D.NoUnit,A.NoStock,A.NoCustomer,A.SN FROM MS_KONTRAK_APPROVAL A"
                + " INNER JOIN MS_CUSTOMER B ON A.NoCustomer = B.NoCustomer"
                + " INNER JOIN MS_AGENT C ON A.NoAgent = C.NoAgent"
                + " INNER JOIN MS_UNIT D ON A.NoStock = D.NoStock WHERE A.NoKontrak='" + NoKontrak + "'"
             );
            if (rs.Rows.Count != 0)
            {
                nokontrak.Text = rs.Rows[0]["NoKontrak"].ToString();
                unit.Text = rs.Rows[0]["NoUnit"].ToString();
                customer.Text = rs.Rows[0]["NamaCS"].ToString();
                agent.Text = rs.Rows[0]["NamaAG"].ToString();
                tglkontrak.Text = Cf.Day(Convert.ToDateTime(rs.Rows[0]["TglKontrak"]));
                
            }
        }

        private string NoKontrak
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoKontrak"]);
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

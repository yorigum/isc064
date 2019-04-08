using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
    public partial class HoldUnitDaftarDone : System.Web.UI.Page
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
            DataTable rs = Db.Rs("SELECT A.NoHold,A.TglHold,A.TglHoldExpired,B.Nama AS NamaCS,C.Nama AS NamaAG,D.NoUnit,A.NoStock,A.NoCustomer FROM MS_HOLD A"
                + " INNER JOIN MS_CUSTOMER B ON A.NoCustomer = B.NoCustomer"
                + " INNER JOIN MS_AGENT C ON A.NoAgent = C.NoAgent"
                + " INNER JOIN MS_UNIT D ON A.NoStock = D.NoStock WHERE A.NoHold='" + NoHold + "'"
             );
            if (rs.Rows.Count != 0)
            {
                nohold.Text = "<a href=\"javascript:popEditHold('" + rs.Rows[0]["NoHold"] + "')\">"
                    + rs.Rows[0]["NoHold"].ToString() + "</a>";

                unit.Text = rs.Rows[0]["NoUnit"].ToString();
                customer.Text = rs.Rows[0]["NamaCS"].ToString();
                agent.Text = rs.Rows[0]["NamaAG"].ToString();
                tglhold.Text = Cf.Day(Convert.ToDateTime(rs.Rows[0]["TglHold"]));
                tglholdexp.Text = Cf.Date(Convert.ToDateTime(rs.Rows[0]["TglHoldExpired"]));
                aclosing.HRef = "ClosingLangsungDaftar3.aspx?NoHold=" + NoHold + "&NoStock=" + rs.Rows[0]["NoStock"].ToString() + "&NoCustomer=" + rs.Rows[0]["NoCustomer"].ToString() + "";
            }
        }

        private string NoHold
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoHold"]);
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

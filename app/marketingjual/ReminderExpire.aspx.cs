using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
    public partial class ReminderExpire : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();
            Cf.SetGrid(tb);
            Fill();
            ok.HRef = "Reminder.aspx?project=" + Project;
        }

        private void Fill()
        {
            string nav = "'<a href=\"javascript:popEditReservasi('''+ CONVERT(varchar(50),NoReservasi)+''')\">' + FORMAT(NoReservasi,'0000#') + '</a>'";
            string strSql = "SELECT "
                + nav
                + " AS No"
                + ",MS_RESERVASI.NoUnit AS Unit"
                + ",CONVERT(VARCHAR,TglExpire,106) AS Expire"
                + ",MS_CUSTOMER.Nama  + '<br>' + MS_CUSTOMER.NoTelp AS Customer"                
                + ",MS_AGENT.Nama + ' ' + MS_AGENT.Principal AS Sales"                
                + " FROM MS_RESERVASI INNER JOIN MS_CUSTOMER ON MS_RESERVASI.NoCustomer = MS_CUSTOMER.NoCustomer"
                + " INNER JOIN MS_AGENT ON MS_RESERVASI.NoAgent = MS_AGENT.NoAgent"
                + " INNER JOIN MS_UNIT ON MS_RESERVASI.NoStock = MS_UNIT.NoStock"
                + " WHERE NoUrut = 1 AND MS_RESERVASI.Status = 'E'"
                + " AND MS_UNIT.Project = '" + Project + "'"
                + " ORDER BY TglExpire, MS_RESERVASI.TglInput";

            DataTable rs = Db.Rs(strSql);
            tb.DataSource = rs;
            tb.DataBind();
            if (tb.PageCount == 0) kosong.InnerText = "Reminder untuk topik diatas masih kosong.";

        }
        private string Project
        {
            get
            {
                return Cf.Str(Request.QueryString["Project"]);
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

        protected void tb_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            tb.PageIndex = e.NewPageIndex;
            Fill();
        }
    }
}

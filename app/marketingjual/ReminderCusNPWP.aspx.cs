using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
    public partial class ReminderCusNPWP : System.Web.UI.Page
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
            string nav = "'<a href=\"javascript:call('''+ CONVERT(VARCHAR(10),NoCustomer)+''')\">' + Nama + '</a>'";
            string strSql = "SELECT "
                + nav
                + " AS Nama"
                + ",NPWP"
                + ",NPWPAlamat1 + '<br>' + NPWPAlamat2 + '<br>' + NPWPAlamat3 AS Alamat"
                + " FROM MS_CUSTOMER WHERE Project = '" + Project + "'"
                + " AND (NPWP = '' OR "
                + "NPWPAlamat1 = '' OR NPWPAlamat2 = '' OR NPWPAlamat3 = '')"
                + " ORDER BY Nama";

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

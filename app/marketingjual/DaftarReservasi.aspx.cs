using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
    public partial class DaftarReservasi : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();
            Cf.SetGrid(tb);            

            if (!Page.IsPostBack)
            {
                if (Request.QueryString["status"] == null)
                    metode.SelectedIndex = 0;
                else if (Request.QueryString["status"] == "a")
                    metode.SelectedIndex = 1;
                else if (Request.QueryString["status"] == "e")
                    metode.SelectedIndex = 2;

                Act.ProjectList(project);
                if (metode.SelectedIndex != 0) metode.Enabled = false;                
            }
        }

        protected void search_Click(object sender, System.EventArgs e)
        {
            Cf.SetGrid(tb);
            Fill();
        }

        private void Fill()
        {
            string addq = "";
            if (metode.SelectedIndex == 1)
                addq = " AND MS_RESERVASI.Status = 'A'";
            else if (metode.SelectedIndex == 2)
                addq = " AND MS_RESERVASI.Status = 'E'";

            string nav = "'<a href =\"javascript:call('''+CONVERT(varchar(10), NoReservasi)+''')\">'"
                      + " + FORMAT(NoReservasi, '0000#') + "
                      + "'</a>'";

            string strSql = "SELECT"
                + nav
                + " AS No"
                + ",NoUrut AS NoUrut"
                + ",CONVERT(VARCHAR, Tgl, 106) AS Tgl"
                + ",CONVERT(VARCHAR, TglExpire, 113) AS BatasWaktu"
                + ",MS_RESERVASI.NoUnit AS Unit"
                + ",MS_CUSTOMER.Nama AS Customer"
                + ",(SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = MS_UNIT.Project) AS Project"
                + " FROM MS_RESERVASI INNER JOIN MS_CUSTOMER ON MS_RESERVASI.NoCustomer = MS_CUSTOMER.NoCustomer"
                + " INNER JOIN MS_AGENT ON MS_RESERVASI.NoAgent = MS_AGENT.NoAgent"
                + " INNER JOIN MS_UNIT ON MS_RESERVASI.NoStock = MS_UNIT.NoStock"
                + " WHERE CONVERT(varchar,NoReservasi) + MS_RESERVASI.NoUnit + MS_CUSTOMER.Nama + MS_AGENT.Nama + MS_AGENT.Principal "
                + " LIKE '%" + Cf.Str(keyword.Text) + "%'"
                + " AND MS_UNIT.Project = '" + project.SelectedValue + "'"
                + addq
                + " ORDER BY NoReservasi";

            DataTable rs = new DataTable();
            Db.Fill(rs, strSql);
            tb.DataSource = rs;
            tb.DataBind();
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

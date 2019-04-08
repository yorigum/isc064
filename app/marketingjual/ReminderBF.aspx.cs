using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
	public partial class ReminderBF : System.Web.UI.Page
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
            string nav = "'<a href=\"javascript:popJadwalTagihan('''+CONVERT(varchar(50),NoKontrak)+''');\">' + NoKontrak + '</a>' AS Kontrak";
            string strSql = "SELECT "
                + nav
                + ",NoUnit AS Unit"
                + ",MS_CUSTOMER.Nama AS Customer"
                + ",MS_AGENT.Nama + ' ' + MS_AGENT.Principal AS Sales"
                + ",CONVERT(VARCHAR(10),PersenLunas) + '%' AS Pelunasan"
                + " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
                + " INNER JOIN MS_AGENT"
                + " ON MS_KONTRAK.NoAgent = MS_AGENT.NoAgent"
                + " WHERE 1=1"
                + " AND ms_kontrak.Status = 'A'"
                + " AND "
                + "("
                + "SELECT COUNT(*) FROM MS_PELUNASAN"
                + " INNER JOIN MS_TAGIHAN ON MS_PELUNASAN.NoKontrak = MS_TAGIHAN.NoKontrak AND MS_PELUNASAN.NoTagihan = MS_TAGIHAN.NoUrut"
                + " WHERE MS_TAGIHAN.Tipe NOT IN ('BF')"
                + " AND MS_PELUNASAN.NoKontrak = MS_KONTRAK.NoKontrak"
                + ") = 0"
                + " AND MS_KONTRAK.Project = '" + Project + "'"
                ;

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

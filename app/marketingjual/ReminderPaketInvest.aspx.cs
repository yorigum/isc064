using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
    public partial class ReminderPaketInvest : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();
            Cf.SetGrid(tb);
			Fill();
            ok.HRef = "Reminder.aspx?project=" + Project;

            if (Request.QueryString["done"] != null)
            {
                feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                    + "<a show-modal='#ModalPopUp' modal-title='Kontrak Detil' modal-url='KontrakEdit.aspx?NoKontrak=" + Request.QueryString["done"] + "'>"
                    + "AJB Berhasil..."
                    + "</a>";
            }
        }

		private void Fill()
		{
            string nav = "'<a href=\"javascript:call('''+NoKontrak+''')\">' + NoKontrak + '</a>' AS Kontrak";
			string strSql = "SELECT "
                + nav                				
				+ ",NoUnit AS Unit"
                + ",CONVERT(VARCHAR,TglPaketInvestasi,106) AS Tgl"
				+ ",MS_CUSTOMER.Nama AS Customer"
				+ ",MS_AGENT.Nama + ' ' + MS_AGENT.Principal AS Sales"
				+ ",CONVERT(varchar(50),PersenLunas) + '%' AS Pelunasan"
				+ " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
				+ " INNER JOIN MS_AGENT"
				+ " ON MS_KONTRAK.NoAgent = MS_AGENT.NoAgent"
				+ " WHERE 1=1"
                + " AND MS_KONTRAK.PaketInvestasi = 1 AND MS_KONTRAK.TglPaketInvestasi <'" + Cf.Tgl112(DateTime.Today) + "' AND MS_KONTRAK.Status='A'"
                + " OR (MS_KONTRAK.PaketInvestasi = 1 AND MS_KONTRAK.TglPaketInvestasi <'" + Cf.Tgl112(DateTime.Today.AddDays(30)) + "' AND MS_KONTRAK.Status='A')"
                + " AND MS_KONTRAK.Project = '" + Project + "'"
                + " ORDER BY MS_KONTRAK.NoKontrak";

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

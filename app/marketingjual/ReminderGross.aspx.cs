using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
	public partial class ReminderGross : System.Web.UI.Page
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
                    + "<a href=\"javascript:popEditKontrak('" + Request.QueryString["done"] + "')\">"
                    + "Prosedur Diskon Berhasil..."
                    + "</a>";
            }
        }

        private void Fill()
        {
            string nav = "'<a href=\"KontrakDiskon.aspx?gross=1&NoKontrak='''+ CONVERT(varchar(50),NoKontrak) +'''\">' + NoKontrak + '</a>' AS Kontrak";
            string strSql = "SELECT "
                + nav                
                + ",NoUnit AS Unit"
                + ",Nama AS Customer"
                + ",FORMAT(Gross,'#,###') AS BlmDiskon"
                + ",FORMAT(DiskonRupiah,'#,###') AS Diskon"
                + ",CASE WHEN FlagGross = 1 THEN 'Edit Kontrak' WHEN FlagGross = 2 THEN 'Refresh Unit' WHEN FlagGross = 3 THEN 'Pindah Unit' WHEN FlagGross = 4 THEN 'Serah Terima' END AS Keterangan"
                + " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER"
                + " ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
                + " WHERE 1=1"
                + " AND MS_KONTRAK.FlagGross <> 0 AND MS_KONTRAK.Status = 'A'"
                + " AND MS_KONTRAK.Project ='" + Project + "'"
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

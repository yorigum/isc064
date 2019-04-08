using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.COLLECTION
{
	public partial class ReminderSTBaru : System.Web.UI.Page
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
            string nav = "'<a href=\"javascript:call('''+ CONVERT(VARCHAR(50),NoTunggakan) +''')\">' + ManualTunggakan + '</a>' AS ST";
            string strSql = "SELECT "
                + nav
                + ",CONVERT(VARCHAR,TglTunggakan,106) AS Tgl"
                + ",'<font style=font:bold 15pt>' + CONVERT(VARCHAR(50),a.LevelTunggakan) + '</font>' AS Level"
                + ",a.Customer + '<br>Telp: ' + a.NoTelp AS Cs"
                + ",a.Tipe + ' No. : ' + a.Ref + '<br>Unit : ' + a.Unit AS Keterangan"
                + ",FORMAT(a.Total,'#,###') AS Nilai"
                + " FROM MS_TUNGGAKAN a INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK b ON a.Ref = b.NoKontrak "
                + " WHERE a.PrintST = 0 "
                + " AND (SELECT COUNT(*) FROM MS_TUNGGAKAN_JURNAL WHERE NoTunggakan = a.NoTunggakan) = 0"
                + " AND b.Project = '" + Project + "'"
                + " ORDER BY NoTunggakan";

            DataTable rs = Db.Rs(strSql);
            tb.DataSource = rs;
            tb.DataBind();
            if (tb.PageCount == 0) kosong.InnerText = "Reminder untuk topik diatas masih kosong.";
        }

        private string Project
        {
            get
            {
                return Cf.Pk(Request.QueryString["Project"]);
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

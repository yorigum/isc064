using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;


namespace ISC064.KPA
{
    public partial class ReminderSP3K4   : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();
            Cf.SetGrid(tb);
            Fill();
            cancel.HRef = "Reminder.aspx?project=" + Project;
        }

        private void Fill()
        {
            string nav = "'<a href=\"javascript:popEditSP3K('''+CONVERT(varchar(50),NoKontrak)+''');\">'+ NoKontrak +'</a>' AS Kontrak";
            string strSql = "SELECT "
                    + nav
                    + ",CONVERT(VARCHAR,TglKontrak,106) AS Tgl"
                    + ",NoUnit AS Unit"
                    + ",MS_CUSTOMER.Nama AS Cs"
                    + ",MS_AGENT.Nama AS Agent"
                    + ",BankKPR AS Bank"
                    + ",CONVERT(VARCHAR,TglHasilSP3K,106) AS Pengajuan"
                    + ",HasilSP3K AS Hasil"
                    + " FROM MS_KONTRAK"
                    + " INNER JOIN MS_CUSTOMER ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
                    + " INNER JOIN MS_AGENT ON MS_KONTRAK.NoAgent = MS_AGENT.NoAgent"
                    + " WHERE MS_KONTRAK.Status = 'A'"
                    + " AND CaraBayar = 'KPR'"
                    + " AND StatusBerkas = 1"
                    + " AND StatusSP3K = 'SELESAI'"
                    + " AND HasilSP3K = 'TOLAK'"
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
                return Cf.Pk(Request.QueryString["project"]);
            }
        }


        protected void tb_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            tb.PageIndex = e.NewPageIndex;
            Fill();
        }
    }
}

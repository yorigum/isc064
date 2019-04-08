using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.FINANCEAR
{
    public partial class ReminderAnonimSolve : System.Web.UI.Page
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
            string nav = "'<a onclick=\"popEditTA('''+CONVERT(varchar(50),NoAnonim)+''')\">' + CONVERT(VARCHAR,Tgl,106) + '</a>' AS Tgl";

            string strSql = "SELECT "
                + nav
                + ",Bank"
                + ",FORMAT(Nilai,'#,###') AS Nilai"
                + ",Unit + '<br>' + Customer + '<br>' + Ket AS Ket"
                + " FROM MS_ANONIM"
                + " WHERE Status = 'ID' "
                + " AND Project = '" + Project + "'"
                + " ORDER BY NoAnonim";

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

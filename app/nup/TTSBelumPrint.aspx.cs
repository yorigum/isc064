using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.NUP
{
    public partial class TTSBelumPrint : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();
            Cf.SetGrid(tb);
            if (!Page.IsPostBack)
            {
                Act.ProjectList(project);
                if (Request.QueryString["project"] != null)
                {
                    project.SelectedValue = Request.QueryString["project"];
                }
            }

            Fill();

            if (Request.QueryString["done"] != null)
            {
                feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                    + "<a href=\"javascript:popEditKontrak('" + Request.QueryString["done"] + "')\">"
                    + "PPJB Berhasil..."
                    + "</a>";
            }
        }

        private void Fill()
        {
            string nav = "'<a href=\"javascript:call('''+A.NoNUP+''','''+A.Tipe+''','''+A.Project+''')\">' + A.NoNUP + '</a>'";
            string strSql = "SELECT "
                + nav
                + " AS NUP"
                + ",A.NoTTS AS TTS"
                + ",FORMAT(B.Total,'#,###') AS Nilai"
                + " FROM MS_NUP_PELUNASAN A"
                + " INNER JOIN " + Mi.DbPrefix + "FINANCEAR..MS_TTS B ON A.NoTTS = B.NoTTS AND A.Project = B.Project"
                + " WHERE B.PrintTTS = 0"
                + " AND (SELECT COUNT(NoNUP) FROM MS_NUP WHERE NoNUP = A.NoNUP AND Project = '" + project.SelectedValue + "') > 0";
            
            DataTable rs = Db.Rs(strSql);
            tb.DataSource = rs;
            tb.DataBind();
            if (tb.PageCount == 0)
            {
                kosong.InnerText = "Data tidak tersedia.";
            }
            else
            {
                kosong.InnerText = "";
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

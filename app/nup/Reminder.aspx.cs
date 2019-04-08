using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.NUP
{
    public partial class Reminder : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack) Act.ProjectList(project);

            countBelumBayar.Text = Db.SingleInteger("SELECT ISNULL(COUNT(*), 0) FROM MS_NUP WHERE NilaiBayar=0 AND Project = '"+project.SelectedValue+"'").ToString();

            countTTSBelumPrint.Text = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM MS_NUP_PELUNASAN A"
                          + " INNER JOIN ISC064_FINANCEAR..MS_TTS B ON A.NoTTS = B.NoTTS"
                          + " WHERE B.PrintTTS = 0"
                          + " AND (SELECT COUNT(NoNUP) FROM MS_NUP WHERE NoNUP = A.NoNUP AND Project = '" + project.SelectedValue + "') > 0"
                          ).ToString();
            
            tts.HRef = "TTSBelumPrint.aspx?project=" + project.SelectedValue;
            tts2.HRef = "TTSBelumPrint.aspx?project=" + project.SelectedValue;
            nup2.HRef = "NUPDaftarBayar.aspx?project=" + project.SelectedValue;
            nup.HRef = "NUPDaftarBayar.aspx?project=" + project.SelectedValue;
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
    }
}

using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LAUNCHING
{
    public partial class ClosingDone : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                Fill();
            }
        }

        private void Fill()
        {
            Func.KontrakHeader(NoKontrak, nokontrakl, unit, customer, agent);

            string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");

            asp.HRef = "javascript:openPopUp('/launching/PrintSP.aspx?NoKontrak=" + NoKontrak + "&project=" + Project + "','920','650')";
            ajp.HRef = "javascript:openPopUp('/launching/PrintJadwalTagihan.aspx?NoKontrak=" + NoKontrak + "','920','650')";

            int AdaNUP = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM MS_NUP_PRIORITY WHERE NoNUPHeader='" + NoNUPHeader + "' AND NoKontrak=''");
            if (AdaNUP > 0)
            {
                backclosing.HRef = "ClosingNUP3.aspx?NoNUP=" + NoNUPHeader;
            }
            else
            {
                backclosing.Visible = false;
            }

            if (NoTTS == "0")
                atts.Visible = false;
            else
                atts.Visible = true;
                atts.HRef = "javascript:openPopUp('PrintTTS.aspx?NoTTS=" + NoTTS + "','920','650')";
        }

        private string NoKontrak
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoKontrak"]);
            }
        }
        private string NoNUPHeader
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoNUPHeader"]);
            }
        }

        private string NoTTS
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoTTS"]);
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
    }
}

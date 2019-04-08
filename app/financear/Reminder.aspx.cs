using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.FINANCEAR
{
    public partial class Reminder : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                Act.ProjectList(project);
                if (Request.QueryString["project"] != null)
                {
                    project.SelectedValue = Request.QueryString["project"];
                }
            }
            countPosting.Text = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_TTS WHERE Status = 'BARU' AND Project = '" + project.SelectedValue + "'"
                ).ToString();

            countInkonsisten.Text = Db.SingleInteger(
                "SELECT COUNT(NoBG) FROM "
                + "(SELECT NoBG, COUNT(DISTINCT TglBG) AS Total FROM MS_TTS WHERE CaraBayar = 'BG' AND Project = '" + project.SelectedValue + "' GROUP BY NOBG) AS TableBG "
                + " WHERE Total > 1 "
                ).ToString();

            countBGJatuhTempo.Text = Db.SingleInteger(
                "SELECT COUNT(DISTINCT NoBG) FROM MS_TTS WHERE CaraBayar = 'BG' AND StatusBG = 'OK' AND Status = 'BARU'"
                + " AND CONVERT(varchar, TglJTBG, 112) <= '" + Cf.Tgl112(DateTime.Today) + "'"
                + " AND Project = '" + project.SelectedValue + "'"
                ).ToString();

            countBGBad.Text = Db.SingleInteger(
                "SELECT COUNT(DISTINCT NoBG) FROM MS_TTS WHERE CaraBayar = 'BG' AND StatusBG = 'BAD' AND Project = '" + project.SelectedValue + "'"
                ).ToString();

            countAnonimBaru.Text = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_ANONIM WHERE Status = 'BARU' AND Project = '" + project.SelectedValue + "'"
                ).ToString();

            countAnonimSolve.Text = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_ANONIM WHERE Status = 'ID' AND Project = '" + project.SelectedValue + "'"
                ).ToString();

            //unallocated
            int c = 0;
            string[] x = Sc.MktCatalog();
            for (int i = 0; i <= x.GetUpperBound(0); i++)
            {
                string[] xdetil = x[i].Split(';');

                if (xdetil[1] != "TENANT")
                    c = c + Db.SingleInteger("SELECT COUNT(*) FROM " + xdetil[0] + "..MS_PELUNASAN WHERE NoTagihan = 0");
            }
            countUnallocated.Text = c.ToString();

            tts.HRef = "ReminderTTSPosting.aspx?Project=" + project.SelectedValue;
            tts2.HRef = "ReminderTTSPosting.aspx?Project=" + project.SelectedValue;
            inkonsistengiro.HRef = "ReminderInkonsisten.aspx?Project=" + project.SelectedValue;
            inkonsistengiro2.HRef = "ReminderInkonsisten.aspx?Project=" + project.SelectedValue;
            jtgiro.HRef = "ReminderBGJatuhTempo.aspx?Project=" + project.SelectedValue;
            jtgiro2.HRef = "ReminderBGJatuhTempo.aspx?Project=" + project.SelectedValue;
            masalahgiro.HRef = "ReminderBGBad.aspx?Project=" + project.SelectedValue;
            masalahgiro2.HRef = "ReminderBGBad.aspx?Project=" + project.SelectedValue;
            unalo.HRef = "ReminderUnallocated.aspx?Project=" + project.SelectedValue;
            unalo2.HRef = "ReminderUnallocated.aspx?Project=" + project.SelectedValue;
            anobaru.HRef = "ReminderAnonimBaru.aspx?Project=" + project.SelectedValue;
            anobaru2.HRef = "ReminderAnonimBaru.aspx?Project=" + project.SelectedValue;
            anosolve.HRef = "ReminderAnonimSolve.aspx?Project=" + project.SelectedValue;
            anosolve2.HRef = "ReminderAnonimSolve.aspx?Project=" + project.SelectedValue;

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

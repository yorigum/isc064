using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.ADMINJUAL
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
            countPL1.Text = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_UNIT WHERE FlagSPL = 0 AND Status = 'A' AND Project = '" + project.SelectedValue + "'"
                ).ToString();

            countPL2.Text = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_UNIT WHERE FlagSPL = 2 AND Status = 'A' AND Project = '" + project.SelectedValue + "'"
                ).ToString();

            edit.HRef = "ReminderPLEdit.aspx?Project=" + project.SelectedValue;
            edit2.HRef = "ReminderPLEdit.aspx?Project=" + project.SelectedValue;
            pending.HRef = "ReminderPLPending.aspx?Project=" + project.SelectedValue;
            pending2.HRef = "ReminderPLPending.aspx?Project=" + project.SelectedValue;

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

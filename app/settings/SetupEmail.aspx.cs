using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ISC064.SETTINGS
{
    public partial class SetupEmail : System.Web.UI.Page
    {
        private string ParamEmailFrom { get { return "EmailFrom" + project.SelectedValue; } }
        private string ParamPassword { get { return "EmailPassword" + project.SelectedValue; } }
        private string ParamEmailDisplayName { get { return "EmailDisplayName" + project.SelectedValue; } }
        private string ParamSMTP { get { return "EmailSMTP" + project.SelectedValue; } }
        private string ParamSMTPPort { get { return "EmailSMTPPort" + project.SelectedValue; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                Act.ProjectList(project);
                fill();
            }                

            FeedBack();
        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["d"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "Edit berhasil..."
                        ;
                project.SelectedValue = Request.QueryString["project"];
                fill();
            }
        }

        protected void fill()
        {            

            string EmailFrom = Db.SingleString("SELECT ISNULL(Value, '') FROM REF_PARAM WHERE ParamID = '" + ParamEmailFrom + "'");
            string Password = Db.SingleString("SELECT ISNULL(Value, '') FROM REF_PARAM WHERE ParamID = '" + ParamPassword + "'");
            string EmailDisplayName = Db.SingleString("SELECT ISNULL(Value, '') FROM REF_PARAM WHERE ParamID = '" + ParamEmailDisplayName + "'");
            string EmailSMTP = Db.SingleString("SELECT ISNULL(Value, '') FROM REF_PARAM WHERE ParamID = '" + ParamSMTP + "'");
            string EmailSMTPPort = Db.SingleString("SELECT ISNULL(Value, '') FROM REF_PARAM WHERE ParamID = '" + ParamSMTPPort + "'");

            emailfrom.Text = EmailFrom;
            password.Text = Password;
            displayname.Text = EmailDisplayName;
            smtp.Text = EmailSMTP;
            port.Text = EmailSMTPPort;
        }

        protected void ok_Click(object sender, EventArgs e)
        {
            if (valid)
            {
                string Project = project.SelectedValue;

                if (!Param.Exist(ParamEmailFrom))
                    Param.InsertParam(emailfrom.Text, ParamEmailFrom, "Email From " + Project);
                else
                    Param.UpdateParam(emailfrom.Text, ParamEmailFrom);

                if (!Param.Exist(ParamPassword))
                    Param.InsertParam(password.Text, ParamPassword, "Password email " + Project);
                else
                    Param.UpdateParam(password.Text, ParamPassword);

                if (!Param.Exist(ParamEmailDisplayName))
                    Param.InsertParam(displayname.Text, ParamEmailDisplayName, "Email display name " + Project);
                else
                    Param.UpdateParam(displayname.Text, ParamEmailDisplayName);

                if (!Param.Exist(ParamSMTP))
                    Param.InsertParam(smtp.Text, ParamSMTP, "Email SMTP " + Project);
                else
                    Param.UpdateParam(smtp.Text, ParamSMTP);

                if (!Param.Exist(ParamSMTPPort))
                    Param.InsertParam(port.Text, ParamSMTPPort, "SMTP Port " + Project);
                else
                    Param.UpdateParam(port.Text, ParamSMTPPort);

                Response.Redirect("SetupEmail.aspx?d=1&project="+Project);
            }
        }

        private bool valid
        {
            get
            {
                bool x = true;

                if (Cf.isEmpty(emailfrom))
                {
                    x = false;
                    Cf.MarkError(emailfrom);
                }
                else
                    Cf.ClrError(emailfrom);

                if (Cf.isEmpty(password))
                {
                    x = false;
                    Cf.MarkError(password);
                }
                else
                    Cf.ClrError(password);

                if (Cf.isEmpty(displayname))
                {
                    x = false;
                    Cf.MarkError(displayname);
                }
                else
                    Cf.ClrError(displayname);

                if (Cf.isEmpty(smtp))
                {
                    x = false;
                    Cf.MarkError(smtp);
                }
                else
                    Cf.ClrError(smtp);

                if (!Cf.isMoney(port))
                {
                    x = false;
                    Cf.MarkError(port);
                }
                else
                    Cf.ClrError(port);

                if (!x)
                    Js.Alert(this, "", "Harap melengkapi data tersebut");

                return x;
            }
        }

        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            fill();
        }
    }
}
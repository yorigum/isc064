using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ISC064.SETTINGS
{
    public partial class PLIncludePPN : System.Web.UI.Page
    {
        protected string PL { get { return "PLIncludePPN" + project.SelectedValue; } }
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
                        + "Edit Berhasil..."
                        ;
                project.SelectedValue = Request.QueryString["project"];
                fill();
            }
        }

        protected void fill()
        {
            string ya = Db.SingleString("SELECT Value FROM REF_PARAM WHERE ParamID = '" + PL + "'");
            include.Checked = ya == "True";

            int count = Db.SingleInteger("SELECT COUNT(*) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE Project = '" + project.SelectedValue + "'");
            if(count > 0)
            {
                ok.Enabled = false;
                ok.ToolTip = "Sudah ada kontrak terdaftar.";
            }
        }

        protected void ok_Click(object sender, EventArgs e)
        {
            string ya = "";
            if (include.Checked)
                ya = "True";
            else
                ya = "False";

            string Project = project.SelectedValue;
            Param.InsertOrUpdate(ya, PL, "Price list include PPN untuk Project " + Project );
            //Db.Execute("UPDATE REF_PARAM SET Value = '" + ya + "' WHERE PARAMID = 'PLIncludePPN'");

            Response.Redirect("PLIncludePPN.aspx?d=1&project="+Project);
        }

        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            fill();
        }
    }
}
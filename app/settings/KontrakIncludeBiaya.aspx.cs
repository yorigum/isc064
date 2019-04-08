using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ISC064.SETTINGS
{
    public partial class KontrakIncludeBiaya : System.Web.UI.Page
    {
        protected string Biaya { get { return "KontrakIncludeBiaya" + project.SelectedValue; } }
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
            }
        }

        protected void fill()
        {
            string ya = Db.SingleString("SELECT Value FROM REF_PARAM WHERE ParamID = '" + Biaya + "'");
            include.Checked = ya == "True";
        }

        protected void ok_Click(object sender, EventArgs e)
        {
            string ya = "";
            if (include.Checked)
                ya = "True";
            else
                ya = "False";

            string Project = project.SelectedValue;
            Param.InsertOrUpdate(ya, Biaya, "Kontrak include Biaya untuk Project " + Project );
            //Db.Execute("UPDATE REF_PARAM SET Value = '" + ya + "' WHERE PARAMID = 'PLIncludePPN'");

            Response.Redirect("KontrakIncludeBiaya.aspx?d=1");
        }

        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            fill();
        }
    }
}
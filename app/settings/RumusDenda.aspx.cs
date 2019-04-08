using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ISC064.SETTINGS
{
    public partial class RumusDenda : System.Web.UI.Page
    {
        protected string RumusDenda1 { get { return "RumusDenda" + project.SelectedValue; } }
        protected string RumusDenda2 { get { return "RumusDenda2" + project.SelectedValue; } }
        protected string GracePeriod { get { return "GracePeriodDenda" + project.SelectedValue; } }
        protected string BerlakuDenda { get { return "BerlakuDenda" + project.SelectedValue; } }
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

        protected void fill()
        {
            Js.NumberFormat(grace);

            if (Param.Exist(RumusDenda1))
            {
                rumus1.Text = Param.GetParam(RumusDenda1);
            }
            if (Param.Exist(RumusDenda2))
            {
                rumus2.Text = Param.GetParam(RumusDenda2);
            }
            if (Param.Exist(GracePeriod))
            {
                grace.Text = Cf.Num(Convert.ToDecimal(Param.GetParam(GracePeriod)));
            }
            if (Param.Exist(BerlakuDenda))
            {
                berlaku.Text = Param.GetParam(BerlakuDenda);
            }
        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "Edit berhasil..."
                        ;
                project.SelectedValue = Request.QueryString["project"];
                fill();
            }
        }

        protected void ok_Click(object sender, EventArgs e)
        {
            string Project = project.SelectedValue;

            Param.InsertOrUpdate(rumus1.Text, RumusDenda1, "Rumus denda project " + Project);
            Param.InsertOrUpdate(rumus2.Text, RumusDenda2, "Rumus pembagi denda project " + Project);
            Param.InsertOrUpdate(grace.Text, GracePeriod, "Grace period denda project " + Project);
            Param.InsertOrUpdate(berlaku.Text, BerlakuDenda, "Berlaku denda dimulai sejak, untuk project " + Project);

            Response.Redirect("RumusDenda.aspx?done=1&project="+Project);
        }

        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            fill();
        }
    }
}
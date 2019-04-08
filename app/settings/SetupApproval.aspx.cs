using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ISC064.SETTINGS
{

    public partial class SetupApproval : System.Web.UI.Page
    {
        protected string GantiUnit { get { return "ApprovGantiUnit" + project.SelectedValue; } }
        protected string GantiNama { get { return "ApprovGantiNama" + project.SelectedValue; } }
        protected string Batal { get { return "ApprovBatal" + project.SelectedValue; } }
        protected string Adjustment { get { return "ApprovAdjustment" + project.SelectedValue; } }
        protected string Reschedule { get { return "ApprovReschedule" + project.SelectedValue; } }
        protected string CustomTagihan { get { return "ApprovCustomTagihan" + project.SelectedValue; } }
        protected string Diskon { get { return "ApprovDiskon" + project.SelectedValue; } }
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
            //Js.NumberFormat(grace);

            if (Param.Exist(GantiUnit))
            {
                //GU.Text = Param.GetParam(GantiUnit);
                string ya = Db.SingleString("SELECT Value FROM REF_PARAM WHERE ParamID = '" + GantiUnit + "'");
                GU.SelectedValue = ya;
            }
            if (Param.Exist(GantiNama))
            {
                string ya = Db.SingleString("SELECT Value FROM REF_PARAM WHERE ParamID = '" + GantiNama + "'");
                GN.SelectedValue = ya;
            }
            if (Param.Exist(Batal))
            {
                string ya = Db.SingleString("SELECT Value FROM REF_PARAM WHERE ParamID = '" + Batal + "'");
                batal.SelectedValue = ya;
            }
            if (Param.Exist(Adjustment))
            {
                string ya = Db.SingleString("SELECT Value FROM REF_PARAM WHERE ParamID = '" + Adjustment + "'");
                adjust.SelectedValue = ya;
            }
            if (Param.Exist(Reschedule))
            {
                string ya = Db.SingleString("SELECT Value FROM REF_PARAM WHERE ParamID = '" + Reschedule + "'");
                resc.SelectedValue = ya;
            }
            if (Param.Exist(CustomTagihan))
            {
                string ya = Db.SingleString("SELECT Value FROM REF_PARAM WHERE ParamID = '" + CustomTagihan + "'");
                custom.SelectedValue = ya;
            }
            if (Param.Exist(Diskon))
            {
                string ya = Db.SingleString("SELECT Value FROM REF_PARAM WHERE ParamID = '" + Diskon + "'");
                diskon.SelectedValue = ya;
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

            Param.InsertOrUpdate(GU.SelectedValue, GantiUnit, "Setting Approval Ganti Unit project ");
            Param.InsertOrUpdate(GN.SelectedValue, GantiNama, "Setting Approval Ganti Nama project ");
            Param.InsertOrUpdate(batal.SelectedValue, Batal, "Setting Approval Pembatalan Kontrak project ");
            Param.InsertOrUpdate(adjust.SelectedValue, Adjustment, "Setting Approval Adjustment Kontrak ");
            Param.InsertOrUpdate(resc.SelectedValue, Reschedule, "Setting Approval Reschedule Tagihan");
            Param.InsertOrUpdate(custom.SelectedValue, CustomTagihan, "Setting Approval Customize Tagihan");
            Param.InsertOrUpdate(diskon.SelectedValue, Diskon, "Setting Approval Diskon");

            Response.Redirect("SetupApproval.aspx?done=1&project="+project.SelectedValue);
        }

        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            fill();
        }
    }
}
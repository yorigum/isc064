using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.SETTINGS
{
    public partial class Approval : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();
            if (!IsPostBack)
            {
                Act.ProjectList(project);
                fill();
            }
        }
        protected void fill()
        {

            for (int i = 1; i <= 7; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                c = new TableCell();
                c.Text = "<a href=\"ApprovalDetail.aspx?project="+project.SelectedValue+"&tipe=" + i + "\">"
                       + LibControls.Bind.TipeApproval(Convert.ToByte(i))
                   + "</a>";
                r.Cells.Add(c);

                Rpt.Border(r);
                rpt.Rows.Add(r);

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

        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            fill();
        }
    }
}

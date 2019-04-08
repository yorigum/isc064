using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
    public partial class Customer : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            //abcd.HRef = "?k=abcd&project=" + project.SelectedValue;

            if (!Page.IsPostBack)
            {
                Act.ProjectList(project);
                Js.Focus(this, search);

                if (Request.QueryString["k"] != null)
                {
                    Fill(Request.QueryString["project"]);
                    project.SelectedValue = Request.QueryString["project"];
                }
                //else
                //{
                //    Fill(project.SelectedValue);
                //}
            }
            abcd.HRef = "?k=abcd&project=" + project.SelectedValue;
            efgh.HRef = "?k=efgh&project=" + project.SelectedValue;
            ijkl.HRef = "?k=ijkl&project=" + project.SelectedValue;
            mnop.HRef = "?k=mnop&project=" + project.SelectedValue;
            qrst.HRef = "?k=qrst&project=" + project.SelectedValue;
            uvwx.HRef = "?k=uvwx&project=" + project.SelectedValue;
            yz09.HRef = "?k=yz09&project=" + project.SelectedValue;

        }

        private void Fill(string Project)
        {
            abcd.HRef = "?k=abcd&project=" + Project;
            efgh.HRef = "?k=efgh&project=" + Project;
            ijkl.HRef = "?k=ijkl&project=" + Project;
            mnop.HRef = "?k=mnop&project=" + Project;
            qrst.HRef = "?k=qrst&project=" + Project;
            uvwx.HRef = "?k=uvwx&project=" + Project;
            yz09.HRef = "?k=yz09&project=" + Project;

            string aq = "";
            switch (Request.QueryString["k"])
            {
                case "abcd":
                    aq = " AND LEFT(Nama,1) IN ('a','b','c','d','A','B','C','D')";
                    break;
                case "efgh":
                    aq = " AND LEFT(Nama,1) IN ('e','f','g','h')";
                    break;
                case "ijkl":
                    aq = " AND LEFT(Nama,1) IN ('i','j','k','l')";
                    break;
                case "mnop":
                    aq = " AND LEFT(Nama,1) IN ('m','n','o','p')";
                    break;
                case "qrst":
                    aq = " AND LEFT(Nama,1) IN ('q','r','s','t')";
                    break;
                case "uvwx":
                    aq = " AND LEFT(Nama,1) IN ('u','v','w','x')";
                    break;
                case "yz09":
                    aq = " AND LEFT(Nama,1) IN ('y','z','0','1','2','3','4','5','6','7','8','9')";
                    break;
            }

            string customsec = "";
            if (Act.SecLevel == "AG")
                customsec = " AND AgentInput = '" + Cf.Str(Act.UserID) + "'";

            string strSql = "SELECT "
                + " NoCustomer"
                + ",Nama"
                + ",LEFT(Nama,1) AS Alfa "
                + " FROM MS_CUSTOMER "
                + " WHERE Status = 'A' " + aq + customsec
                + " AND Project = '" + Project + "'"
                + " ORDER BY Nama, NoCustomer";

            DataTable rs = Db.Rs(strSql);

            string p = "";
            System.Text.StringBuilder x = new System.Text.StringBuilder();

            Rpt.NoData(x, rs, "Tidak ada customer dengan nama yang berawalan : " + Request.QueryString["k"] + ".");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                string alfa = rs.Rows[i]["Alfa"].ToString().ToUpper();
                if (p != alfa)
                {
                    p = alfa;
                    x.Append("<br><h1>" + alfa + "</h1>");
                }

                x.Append("<p><a href=\"javascript:call('" + rs.Rows[i]["NoCustomer"] + "')\">"
                    + rs.Rows[i]["Nama"]
                    + " (" + rs.Rows[i]["NoCustomer"].ToString().PadLeft(5, '0') + ")"
                    + "</a></p>");
            }

            list.Text = x.ToString();
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
            list.Attributes.Clear();
            if(Request.QueryString["k"] != null)
            {
                Fill(project.SelectedValue);
            }
        }
    }
}

using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.KOMISI
{
    public partial class SkemaCF : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            //FillTable();
            if (!Page.IsPostBack)
            {
                Act.ProjectList(project);
            }
        }
        protected void display_Click(object sender, System.EventArgs e)
        {
            FillTable();
        }
        private void FillTable()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            //Isi skema aktif
            DataTable rs = Db.Rs("SELECT * FROM REF_SKOM_CF WHERE Inaktif = 0 AND Project = '" + project.SelectedValue + "'");
            Rpt.NoData(sb, rs, "<font style='font:8pt'>Tidak terdapat skema closing fee dengan status aktif.</font>");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                sb.Append("<li>"
                    + "<a href=\"javascript:popSkomCF('" + rs.Rows[i]["NoSkema"].ToString() + "')\">"
                    + rs.Rows[i]["Nama"] + " (" + rs.Rows[i]["NoSkema"].ToString().PadLeft(3, '0') + ")"
                    + "</a>"
                    + "</li>"
                    );
            }

            aktifIntern.InnerHtml = sb.ToString();

            //Isi skema inaktif
            sb = new System.Text.StringBuilder();
            rs = Db.Rs("SELECT * FROM REF_SKOM_CF WHERE Inaktif = 1  AND Project = '" + project.SelectedValue + "'");
            Rpt.NoData(sb, rs, "<font style='font:8pt'>Tidak terdapat skema closing fee dengan status inaktif.</font>");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                sb.Append("<li>"
                    + "<a href=\"javascript:popSkomCF('" + rs.Rows[i]["NoSkema"].ToString() + "')\">"
                    + rs.Rows[i]["Nama"].ToString() + " (" + rs.Rows[i]["NoSkema"].ToString().PadLeft(3, '0') + ")"
                    + "</a>"
                    + "</li>"
                    );
            }

            inaktifIntern.InnerHtml = sb.ToString();
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
        protected void RegSkemaIn_Click(object sender, EventArgs e)
        {
            Response.Redirect("SkemaCFRegis.aspx");
        }
    }
}

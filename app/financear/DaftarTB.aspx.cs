using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.FINANCEAR
{
    public partial class DaftarTB : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();
            Cf.SetGrid(tb);
            //Js.ConfirmKeyword(this, keyword);

            if (!Page.IsPostBack)
            {
                if (Request.QueryString["status"] == null)
                    metode.SelectedIndex = 0;
                //else if (Request.QueryString["status"] == "ok")
                //    metode.SelectedIndex = 1;
                //else if (Request.QueryString["status"] == "identifikasi")
                //    metode.SelectedIndex = 2;
                //else if (Request.QueryString["status"] == "solve")
                //    metode.SelectedIndex = 3;
                Act.ProjectList(project);
                if (metode.SelectedIndex != 0) metode.Enabled = false;
            }
        }

        protected void search_Click(object sender, System.EventArgs e)
        {
            Cf.SetGrid(tb);
            Fill();
        }

        private void Fill()
        {
            //string addq = "";
            //if (metode.SelectedIndex == 1)
            //    addq = " AND Status = 'BARU'";
            //else if (metode.SelectedIndex == 2)
            //    addq = " AND Status = 'ID'";
            //else if (metode.SelectedIndex == 3)
            //    addq = " AND Status = 'S'";

            string nav = "'<a href=\"javascript:call('''+ CONVERT(VARCHAR(50),a.Nocb) +''')\">' + FORMAT(a.Nocb,'000000#') + '</a><br><i></i>'";

            string strSql = "SELECT "
                + nav
                + " AS CB"
                + ",CONVERT(VARCHAR,a.TglPengembalian,106) AS Tgl"
                + ",FORMAT(a.SisaTagihan,'#,###') AS Sisa"
                + ",FORMAT(a.LebihBayar,'#,###') AS Lebih"
                + ",FORMAT(a.Cashback,'#,###') AS Cashback"
                + " FROM MS_CASHBACK a JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK b ON a.NoKontrak = b.NoKontrak "
                + " WHERE CONVERT(varchar,Nocb) + SisaTagihan + LebihBayar + Cashback "
                + " LIKE '%" + Cf.Str(keyword.Text) + "%'"
                + " AND b.Project = '" + project.SelectedValue + "'"
                //+ addq
                + " ORDER BY Nocb";

            DataTable rs = Db.Rs(strSql);
            tb.DataSource = rs;
            tb.DataBind();
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

        protected void tb_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            tb.PageIndex = e.NewPageIndex;
            Fill();
        }
    }
}

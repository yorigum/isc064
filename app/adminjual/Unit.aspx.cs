using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.ADMINJUAL
{
    public partial class Unit : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();
            Cf.SetGrid(tb);

            if (!Page.IsPostBack)
            {
                Act.ProjectList(project);
                Js.Focus(this, search);
                Bind();
            }
        }

        private void Bind()
        {
            DataTable rs;
            string strSql;

            strSql = "SELECT * FROM REF_JENIS WHERE Project = '" + project.SelectedValue + "' ORDER BY SN";
            rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["Jenis"].ToString();
                string t = v + " - " + rs.Rows[i]["Nama"].ToString();
                jenis.Items.Add(new ListItem(t, v));
            }

            strSql = "SELECT DISTINCT Lokasi FROM MS_UNIT WHERE Project = '" + project.SelectedValue + "' ORDER BY Lokasi";
            rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
                lokasi.Items.Add(new ListItem(rs.Rows[i][0].ToString()));
        }

        private void Fill()
        {
            string Jenis = "";
            if (jenis.SelectedIndex != 0)
                Jenis = " AND Jenis = '" + jenis.SelectedValue + "'";

            string Lokasi = "";
            if (lokasi.SelectedIndex != 0)
                Lokasi = " AND Lokasi = '" + Cf.Str(lokasi.SelectedValue) + "'";

            string Status = "";
            if (status.SelectedIndex == 1)
                Status = " AND Status = 'A'";
            else if (status.SelectedIndex == 2)
                Status = " AND Status = 'B'";
            else if (status.SelectedIndex == 3)
                Status = " AND Status = 'H'";

            string nav = "'<a href=\"javascript:call(''' + NoStock + ''')\">'"
                    + " + NoStock + "
                    + "'</a>'"
                    ;

            string Project = (project.SelectedIndex == 0) ? " AND Project IN (" + Act.ProjectListSql + ")" : " AND Project = '" + project.SelectedValue + "'";

            string strSql = "SELECT"
                + nav
                + " AS NoStock"
                + ",CASE WHEN Status = 'A' THEN 'Available' WHEN Status = 'B' THEN 'Sold' ELSE 'Hold' END AS Status"
                //+ ",CASE WHEN Status = 'A' THEN 'A' CASE WHEN Status = 'B' THEN 'S' CASE WHEN Status = 'H' THEN 'H' END AS Status"
                + ",NoUnit"
                + ",FORMAT (Luas,'#,#0') AS Luas"
                + ",FORMAT (LuasSG,'#,#0') AS LuasSG"
                + ",FORMAT (LuasNett,'#,#0') AS LuasNett"
                + ",Project"
                + ",CASE WHEN  DefaultPL = 2 THEN FORMAT(PricelistKavling, '#,###') ELSE FORMAT(PriceList, '#,###') END AS PL"
                + ",Jenis AS Keterangan"
                + " FROM MS_UNIT"
                + " WHERE 1=1"
                + Project
                + Jenis
                + Lokasi
                + Status
                + " ORDER BY NoStock";

            DataTable rs = new DataTable();
            Db.Fill(rs, strSql);
            tb.DataSource = rs;
            tb.DataBind();

        }
        protected void display_Click(object sender, System.EventArgs e)
        {
            Cf.SetGrid(tb);
            Fill();
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
            jenis.Items.Clear();
            jenis.Items.Add(new ListItem("Jenis : "));
            lokasi.Items.Clear();
            lokasi.Items.Add(new ListItem("Lokasi : "));
            Bind();
        }

        protected void tb_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            tb.PageIndex = e.NewPageIndex;
            Fill();
        }
    }
}

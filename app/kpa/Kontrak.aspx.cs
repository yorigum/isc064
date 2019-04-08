using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.KPA
{
    public partial class Kontrak : System.Web.UI.Page
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

            rs = Db.Rs("SELECT DISTINCT YEAR(TglKontrak), MONTH(TglKontrak) FROM MS_KONTRAK WHERE Project = '"+project.SelectedValue+"' AND CaraBayar='KPR'"
                + " ORDER BY YEAR(TglKontrak), MONTH(TglKontrak)");
            for (int i = 0; i < rs.Rows.Count; i++)
                thnKontrak.Items.Add(new ListItem(
                    Cf.MonthnameEnglish((int)rs.Rows[i][1]) + " " + rs.Rows[i][0].ToString()
                    , rs.Rows[i][0] + "," + rs.Rows[i][1]
                    ));

            thnKontrak.SelectedIndex = thnKontrak.Items.Count - 1;

            strSql = "SELECT * FROM REF_JENIS WHERE Project = '" + project.SelectedValue + "' ORDER BY SN";
            rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["Jenis"].ToString();
                string t = v + " - " + rs.Rows[i]["Nama"].ToString();
                jenis.Items.Add(new ListItem(t, v));
            }

            strSql = "SELECT DISTINCT Lokasi FROM MS_KONTRAK WHERE Project = '" + project.SelectedValue + "' ORDER BY Lokasi";
            rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
                lokasi.Items.Add(new ListItem(rs.Rows[i][0].ToString()));
        }

        private void Fill()
        {
            string Periode = "";
            if (thnKontrak.SelectedIndex != 0)
            {
                string[] z = thnKontrak.SelectedValue.Split(',');
                Periode = " AND YEAR(TglKontrak) = " + z[0]
                    + " AND MONTH(TglKontrak) = " + z[1];
            }

            string Jenis = "";
            if (jenis.SelectedIndex != 0)
                Jenis = " AND Jenis = '" + jenis.SelectedValue + "'";

            string Lokasi = "";
            if (lokasi.SelectedIndex != 0)
                Lokasi = " AND Lokasi = '" + Cf.Str(lokasi.SelectedValue) + "'";

            string NoKontrak = "";

            NoKontrak = "'<a href=\"javascript:call(''' + NoKontrak + ''')\" ' + CASE MS_KONTRAK.Status WHEN 'B' THEN 'style = ''text-decoration:line-through''' ELSE '' END + '>'"
                  + "+ NoKontrak + '</a><br>";

            NoKontrak += "  <font style=''font:8pt;color:' + CASE WHEN StatusBerkas = '1' THEN 'Black' ELSE 'Silver' END + ' ''>BERKAS</font>&nbsp;&nbsp;"
                         + "<font style=''font:8pt;color:' + CASE WHEN StatusWawancara = 'SELESAI' THEN 'Black' ELSE 'Silver' END + '''>WAWANCARA</font>&nbsp;&nbsp;"
                         + "<font style=''font:8pt;color:' + CASE WHEN StatusOTS = 'SELESAI' THEN 'Black' ELSE 'Silver' END + '''>OTS</font>&nbsp;&nbsp;"
                         + "<font style=''font:8pt;color:' + CASE WHEN StatusLPA = 'SELESAI' THEN 'Black' ELSE 'Silver' END + '''>LPA</font>&nbsp;&nbsp;"
                         + "<font style=''font:8pt;color:' + CASE WHEN StatusSP3K = 'SELESAI' THEN 'Black' ELSE 'Silver' END + '''>SP3K</font>&nbsp;&nbsp;"
                         + "<font style=''font:8pt;color:' + CASE WHEN StatusAkad = 'SELESAI' THEN 'Black' ELSE 'Silver' END + '''>AKAD</font>&nbsp;&nbsp;"
                         + "<font style=''font:8pt;color:' + CASE WHEN MS_KONTRAK.Status = 'B' THEN 'Red' ELSE 'Silver' END + '''>BATAL</font>"
                         + "' AS Kontrak"
                         ;

            string Project = (project.SelectedIndex == 0) ? " AND MS_KONTRAK.Project IN (" + Act.ProjectListSql + ")" : " AND MS_KONTRAK.Project = '" + project.SelectedValue + "'";

            string strSql = "SELECT "
                + NoKontrak
                + ",MS_KONTRAK.NoUnit AS Unit"
                + ",CONVERT(VARCHAR,MS_KONTRAK.TglKontrak,106) AS Tgl"
                + ",MS_CUSTOMER.Nama + '<Br>' + MS_AGENT.Nama + ' - ' + MS_AGENT.Principal AS Customer"
                + ",CaraBayar AS Keterangan"
                + ",MS_KONTRAK.NamaProject AS Project"
                + " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer "
                + " INNER JOIN MS_AGENT ON MS_KONTRAK.NoAgent = MS_AGENT.NoAgent"
                + " WHERE 1=1"
                + Project
                + Jenis
                + Lokasi
                + Periode
                + " AND CaraBayar='KPR'"
                + " ORDER BY NoKontrak";

            DataTable rs = Db.Rs(strSql);
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

        protected void tb_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            tb.PageIndex = e.NewPageIndex;
            Fill();
        }

        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            thnKontrak.Items.Clear();
            thnKontrak.Items.Add(new ListItem("Periode Kontrak :"));
            jenis.Items.Clear();
            jenis.Items.Add(new ListItem("Jenis :"));
            lokasi.Items.Clear();
            lokasi.Items.Add(new ListItem("Lokasi :"));
            Bind();
            Fill();
        }
    }
}

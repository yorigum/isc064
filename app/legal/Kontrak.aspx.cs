using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LEGAL
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

            rs = Db.Rs("SELECT DISTINCT YEAR(TglKontrak), MONTH(TglKontrak) FROM MS_KONTRAK WHERE Project = '" + project.SelectedValue + "'"
                + " ORDER BY YEAR(TglKontrak), MONTH(TglKontrak)");
            for (int i = 0; i < rs.Rows.Count; i++)
                thnKontrak.Items.Add(new ListItem(
                    Cf.Monthname((int)rs.Rows[i][1]) + " " + rs.Rows[i][0].ToString()
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

            string TitipJual = "";
            if (titipjual.SelectedIndex != 0)
                TitipJual = " AND TitipJual=" + titipjual.SelectedValue.ToString();

            string NoKontrak = "'<a href=\"javascript:call(''' + A.NoKontrak + ''')\">'"
                  + "+ A.NoKontrak + '</a><br>";

            NoKontrak += "  <font style=''font:8pt;color:' + CASE WHEN D.NoPPJB != '' AND (D.PPJB = 'D' OR D.PPJB = 'T') THEN 'Black' ELSE 'Silver' END + ' ''>PPJB</font>&nbsp;&nbsp;"
                         + "<font style=''font:8pt;color:' + CASE WHEN E.NoAJB != '' AND (E.AJB = 'D' OR E.AJB = 'T') THEN 'Black' ELSE 'Silver' END + '''>AJB</font>&nbsp;&nbsp;"
                         + "<font style=''font:8pt;color:' + CASE WHEN F.NoST != '' AND (F.ST = 'D' OR F.ST = 'T') THEN 'Black' ELSE 'Silver' END + '''>BAST</font>&nbsp;&nbsp;"
                         + "<font style=''font:8pt;color:' + CASE WHEN G.StatusIMB = '3' THEN 'Black' ELSE 'Silver' END + '''>IMB</font>&nbsp;&nbsp;"
                         + "<font style=''font:8pt;color:' + CASE WHEN H.StatusSertifikat = '3' THEN 'Black' ELSE 'Silver' END + '''>STT</font>&nbsp;&nbsp;"
                         + "' AS Kontrak"
                         ;

            string Project = (project.SelectedIndex == 0) ? " AND A.Project IN (" + Act.ProjectListSql + ")" : " AND A.Project = '" + project.SelectedValue + "'";

            string strSql = "SELECT "
                + NoKontrak
                + ",A.Status AS Status"
                + ",A.NoUnit AS Unit"
                + ",CONVERT(VARCHAR,A.TglKontrak,106) AS Tgl"
                + ",B.Nama + '<br>' + C.Nama + ' ' + C.Principal AS Customer"
                + ", '' AS Keterangan"
                + ",A.NamaProject AS Project"
                + " FROM MS_KONTRAK A INNER JOIN MS_CUSTOMER B ON A.NoCustomer = B.NoCustomer "
                + " INNER JOIN MS_AGENT C ON A.NoAgent = C.NoAgent"
                + " LEFT JOIN MS_PPJB D ON A.NoKontrak = D.NoKontrak"
                + " LEFT JOIN MS_AJB E ON A.NoKontrak = E.NoKontrak"
                + " LEFT JOIN MS_BAST F ON A.NoKontrak = F.NoKontrak"
                + " LEFT JOIN MS_IMB G ON A.NoKontrak = G.NoKontrak"
                + " LEFT JOIN MS_SERTIFIKAT H ON A.NoKontrak = H.NoKontrak"
                + " WHERE 1=1"
                + Project
                + Jenis
                + Lokasi
                + Periode
                + TitipJual
                + " ORDER BY A.NoKontrak";

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
            lokasi.Items.Clear();
            jenis.Items.Clear();
            thnKontrak.Items.Clear();
            lokasi.Items.Add(new ListItem("Lokasi : "));
            jenis.Items.Add(new ListItem("Jenis : "));
            thnKontrak.Items.Add(new ListItem("Periode Kontrak : "));
            Bind();
        }
    }
}

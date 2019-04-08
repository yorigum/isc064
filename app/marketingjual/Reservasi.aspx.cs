using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
    public partial class Reservasi : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();
            Cf.SetGrid(tb);

            if (!Page.IsPostBack)
            {
                Js.Focus(this, search);
                Bind();
                Act.ProjectList(project);
            }
        }

        private void Bind()
        {
            DataTable rs;
            string strSql;

            rs = Db.Rs("SELECT DISTINCT YEAR(Tgl), MONTH(Tgl) FROM MS_RESERVASI "
                + " ORDER BY YEAR(Tgl), MONTH(Tgl)");
            for (int i = 0; i < rs.Rows.Count; i++)
                thnReservasi.Items.Add(new ListItem(
                    Cf.Monthname((int)rs.Rows[i][1]) + " " + rs.Rows[i][0].ToString()
                    , rs.Rows[i][0] + "," + rs.Rows[i][1]
                    ));

            thnReservasi.SelectedIndex = thnReservasi.Items.Count - 1;

            strSql = "SELECT * FROM REF_JENIS WHERE Project = '" + project.SelectedValue + "' ORDER BY SN";
            rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["Jenis"].ToString();
                string t = v + " - " + rs.Rows[i]["Nama"].ToString();
                jenis.Items.Add(new ListItem(t, v));
            }

            strSql = "SELECT DISTINCT Lokasi FROM REF_LOKASI WHERE Project = '" + project.SelectedValue + "' ORDER BY Lokasi";
            rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
                lokasi.Items.Add(new ListItem(rs.Rows[i][0].ToString()));
        }

        private void Fill()
        {
            string Periode = "";
            if (thnReservasi.SelectedIndex != 0)
            {
                string[] z = thnReservasi.SelectedValue.Split(',');
                Periode = " AND YEAR(MS_RESERVASI.Tgl) = " + z[0]
                    + " AND MONTH(MS_RESERVASI.Tgl) = " + z[1];
            }

            string Jenis = "";
            if (jenis.SelectedIndex != 0)
                Jenis = " AND MS_UNIT.Jenis = '" + jenis.SelectedValue + "'";

            string Lokasi = "";
            if (lokasi.SelectedIndex != 0)
                Lokasi = " AND MS_UNIT.Lokasi = '" + Cf.Str(lokasi.SelectedValue) + "'";

            string NilPengikatan = "";
            if (denganttr.SelectedIndex != 0)
            {
                if(denganttr.SelectedIndex == 1)
                {
                    NilPengikatan = " AND MS_RESERVASI.Netto > 0";
                }
                else
                {
                    NilPengikatan = " AND MS_RESERVASI.Netto = 0";
                }
            }

            string CaraBayar = "";
            if (carabayar.SelectedIndex != 0)
            {
                CaraBayar = " AND CaraBayar = '" + carabayar.SelectedValue + "'";
            }

            string nav = "'<a href =\"javascript:call('''+CONVERT(varchar(10), NoReservasi)+''')\">'"
                      //+ " + FORMAT(NoReservasi, '00000#') + "
                      + " + NoReservasi2 + "
                      + "'</a>'";

            string Project = (project.SelectedIndex == 0) ? " AND MS_UNIT.Project IN (" + Act.ProjectListSql + ")" : " AND MS_UNIT.Project = '" + project.SelectedValue + "'";

            string strSql = "SELECT"
           + nav
           + " AS No"
           + ",'<b>'+CONVERT(varchar(10), NoUrut)+'</b>' AS NoUrut"
           + ",CONVERT(VARCHAR, Tgl, 106) AS Tgl"
           + ",CONVERT(VARCHAR, TglExpire, 113) AS BatasWaktu"
           + ",MS_RESERVASI.Status AS Status"
           + ",MS_RESERVASI.NoUnit + '<br/><i>' + MS_RESERVASI.Jenis + '</i>' AS Unit"
           + ",MS_CUSTOMER.Nama + '<br/>' +"
           + "MS_AGENT.Nama + ' ' +MS_AGENT.Principal AS Customer"
           + ",(SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = MS_UNIT.Project) AS Project"
           + " FROM MS_RESERVASI INNER JOIN MS_CUSTOMER ON MS_RESERVASI.NoCustomer = MS_CUSTOMER.NoCustomer"
           + " INNER JOIN MS_AGENT ON MS_RESERVASI.NoAgent = MS_AGENT.NoAgent"
           + " INNER JOIN MS_UNIT ON MS_RESERVASI.NoStock = MS_UNIT.NoStock"
           + " WHERE 1=1"
           + Project
           + Jenis
           + Lokasi
           + Periode
           + NilPengikatan
           + CaraBayar
           + " ORDER BY NoReservasi";

            DataTable rs = new DataTable();
            Db.Fill(rs, strSql);
            tb.DataSource = rs;
            tb.DataBind();

        }
        protected void display_Click(object sender, System.EventArgs e)
        {
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
            jenis.Items.Add(new ListItem("Jenis :"));
            lokasi.Items.Clear();
            lokasi.Items.Add(new ListItem("Lokasi :"));
            Bind();
        }

        protected void tb_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            tb.PageIndex = e.NewPageIndex;

        }
    }
}

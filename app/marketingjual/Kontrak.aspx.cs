using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
    public partial class Kontrak : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                Cf.SetGrid(tb);
                Act.ProjectList(project);
                Js.Focus(this, search);
                Bind();
            }
        }

        private void Bind()
        {
            DataTable rs;
            string strSql;

            rs = Db.Rs("SELECT DISTINCT YEAR(TglKontrak), MONTH(TglKontrak) FROM MS_KONTRAK "
                + " WHERE Project = '" + project.SelectedValue + "' ORDER BY YEAR(TglKontrak), MONTH(TglKontrak)");
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

        protected void Fill()
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

            string Status = "";
            if (status.SelectedIndex != 0)
                Status = " AND MS_KONTRAK.Status='" + status.SelectedValue + "'";

            string Project = (project.SelectedIndex == 0) ? " AND MS_KONTRAK.Project IN (" + Act.ProjectListSql + ")" : " AND MS_KONTRAK.Project = '" + project.SelectedValue + "'";

            string NoKontrak = "'<a href=\"javascript:call(''' + MS_KONTRAK.NoKontrak + ''')\" ' + CASE MS_KONTRAK.Status WHEN 'B' THEN 'style = ''text-decoration:line-through''' ELSE '' END + '>' + MS_KONTRAK.NoKontrak + '</a><br>";

            NoKontrak += "  <font style=''font:8pt;color:' + CASE WHEN(SELECT COUNT(NoUrut) FROM MS_TAGIHAN WHERE NoKontrak = MS_KONTRAK.NoKontrak) <> 0 THEN 'Black' ELSE 'Silver' END + ' ''>TAGIH</font>&nbsp;&nbsp;"
                         + "<font style=''font:8pt;color:' + CASE WHEN FlagKomisi = 1 THEN 'Black' ELSE 'Silver' END + '''>KOM</font>&nbsp;&nbsp;"
                         + "<font style=''font:8pt;color:' + CASE WHEN D.NoPPJB != '' AND (D.PPJB = 'D' OR D.PPJB = 'T') THEN 'Black' ELSE 'Silver' END + ' ''>PPJB</font>&nbsp;&nbsp;"
                         + "<font style=''font:8pt;color:' + CASE WHEN E.NoAJB != '' AND (E.AJB = 'D' OR E.AJB = 'T') THEN 'Black' ELSE 'Silver' END + '''>AJB</font>&nbsp;&nbsp;"
                         + "<font style=''font:8pt;color:' + CASE WHEN F.NoST != '' AND (F.ST = 'D' OR F.ST = 'T') THEN 'Black' ELSE 'Silver' END + '''>BAST</font>&nbsp;&nbsp;"
                         + "<font style=''font:8pt;color:' + CASE WHEN(SELECT COUNT(*) FROM MS_KONTRAK_GIMMICK WHERE NoKontrak = MS_KONTRAK.NoKontrak) <> 0 THEN 'Black' ELSE 'Silver' END + ' ''>GIMMICK</font>&nbsp;&nbsp;"
                         + "' AS NoKontrak"
                         ;

            string strSql = "SELECT"
                + NoKontrak
                + ",CONVERT(VARCHAR, TglKontrak, 106) AS Tanggal"
                + ",MS_KONTRAK.NoUnit"
                + ",MS_KONTRAK.Status AS Ket"
                + ",MS_CUSTOMER.Nama + '<br />' +"
                + "MS_AGENT.Nama + ' ' + MS_AGENT.Principal AS Customer"
                + ",MS_KONTRAK.NamaProject AS Project"
                + " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer "
                + " INNER JOIN MS_AGENT ON MS_KONTRAK.NoAgent = MS_AGENT.NoAgent"
                + " LEFT JOIN MS_PPJB D ON MS_KONTRAK.NoKontrak = D.NoKontrak"
                + " LEFT JOIN MS_AJB E ON MS_KONTRAK.NoKontrak = E.NoKontrak"
                + " LEFT JOIN MS_BAST F ON MS_KONTRAK.NoKontrak = F.NoKontrak"
                + " WHERE 1=1"
                + Project
                + Jenis
                + Lokasi
                + Periode
                + TitipJual
                + Status
                + " ORDER BY MS_KONTRAK.NoKontrak";

            DataTable rs = Db.Rs(strSql);

            //Response.Write(strSql);

            tb.DataSource = rs;
            tb.DataBind();
        }

        protected void display_Click(object sender, System.EventArgs e)
        {
            if (project.SelectedIndex > 0)
            {
                Cf.SetGrid(tb);
                Fill();
            }
            else
            {
                Js.Alert(this, "Pilih Project dahulu.", "");
            }
        }
        
        protected void tb_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            tb.PageIndex = e.NewPageIndex;
            Fill();
        }

        //protected void project_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    jenis.Items.Clear();
        //    jenis.Items.Add(new ListItem("Jenis :"));
        //    lokasi.Items.Clear();
        //    lokasi.Items.Add(new ListItem("Lokasi :"));
        //    Bind();
        //}

        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            jenis.Items.Clear();
            lokasi.Items.Clear();
            thnKontrak.Items.Clear();
            jenis.Items.Add(new ListItem("Jenis : "));
            lokasi.Items.Add(new ListItem("Lokasi : "));
            thnKontrak.Items.Add(new ListItem("Periode Kontrak : "));
            Bind();
        }
    }
}

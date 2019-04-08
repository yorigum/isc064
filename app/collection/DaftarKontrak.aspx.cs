using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.COLLECTION
{
    public partial class DaftarKontrak : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();
            Cf.SetGrid(tb);

            if (!Page.IsPostBack)
            {
                if (Request.QueryString["status"] == null)
                    metode.SelectedIndex = 0;
                else if (Request.QueryString["status"] == "a")
                    metode.SelectedIndex = 1;
                else if (Request.QueryString["status"] == "b")
                    metode.SelectedIndex = 2;
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
            string addq = "";
            if (metode.SelectedIndex == 1)
                addq = " AND ISC064_MARKETINGJUAL..MS_KONTRAK.Status = 'A'";
            else if (metode.SelectedIndex == 2)
                addq = " AND ISC064_MARKETINGJUAL..MS_KONTRAK.Status = 'B'";

            if (Request.QueryString["tag"] != null)
            {
                addq = addq + " AND (SELECT COUNT(NoUrut) FROM ISC064_MARKETINGJUAL..MS_TAGIHAN WHERE NoKontrak = ISC064_MARKETINGJUAL..MS_KONTRAK.NoKontrak) = 0";
                info.Text = "Kontrak dengan kondisi jadwal tagihan sudah dikeluarkan, tidak ditampilkan.";
            }
            if (Request.QueryString["kom"] != null)
            {
                addq = addq + " AND FlagKomisi = 0";
                info.Text = "Kontrak dengan kondisi jadwal komisi sudah dikeluarkan, tidak ditampilkan.";
            }
            if (Request.QueryString["st"] != null)
            {
                addq = addq + " AND ST <> 'D'";
                info.Text = "Kontrak dengan kondisi serah terima sudah dijalankan, tidak ditampilkan.";
            }
            if (Request.QueryString["ppjb"] != null)
            {
                addq = addq + " AND PPJB <> 'D'";
                info.Text = "Kontrak dengan kondisi PPJB sudah dijalankan, tidak ditampilkan.";
            }
            if (Request.QueryString["ajb"] != null)
            {
                addq = addq + " AND AJB <> 'D'";
                info.Text = "Kontrak dengan kondisi AJB sudah dijalankan, tidak ditampilkan.";
            }
            if (Request.QueryString["dd"] != null)
            {
                addq = " AND (SELECT COUNT(*) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN WHERE NoKontrak = " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK.NoKontrak AND Denda > 0) > 0";
                info.Text = "Kontrak dengan kondisi tidak ada denda, tidak ditampilkan.";
            }

            string NoKontrak = "";

            if (Request.QueryString["status"] == "dari" || Request.QueryString["status"] == "sampai")
            {
                NoKontrak = "'<a href=\"javascript:callSource(''' + NoKontrak + ''', ''" + Request.QueryString["status"] + "'')\">'"
                    + "+ NoKontrak + '</a><br>";

            }
            else
            {
                NoKontrak = "'<a href=\"javascript:call(''' + NoKontrak + ''')\" ' + CASE MS_KONTRAK.Status WHEN 'B' THEN 'style = ''text-decoration:line-through''' ELSE '' END + '>'"
                      + "+ NoKontrak + '</a><br>";
            }

            NoKontrak += "  <font style=''font:8pt;color:' + CASE WHEN(SELECT COUNT(NoUrut) FROM "+Mi.DbPrefix+"MARKETINGJUAL..MS_TAGIHAN WHERE NoKontrak = MS_KONTRAK.NoKontrak) <> 0 THEN 'Black' ELSE 'Silver' END + ' ''>TAGIH</font>&nbsp;&nbsp;"
                         + "<font style=''font:8pt;color:' + CASE WHEN FlagKomisi = 1 THEN 'Black' ELSE 'Silver' END + '''>KOM</font>&nbsp;&nbsp;"
                         + "<font style=''font:8pt;color:' + CASE WHEN NoPPJB <> '' THEN 'Black' ELSE 'Silver' END + '''>PPJB</font>&nbsp;&nbsp;"
                         + "<font style=''font:8pt;color:' + CASE WHEN NoAJB <> '' THEN 'Black' ELSE 'Silver' END + '''>AJB</font>&nbsp;&nbsp;"
                         + "<font style=''font:8pt;color:' + CASE WHEN ST = 'D' THEN 'Black' ELSE 'Silver' END + '''>BAST</font>"
                         + "' AS NoKontrak"
                         ;

            string strSql = "SELECT "
                + NoKontrak
                + ",NoUnit AS Unit"
                + ",CONVERT(VARCHAR,TglKontrak,106) AS Tanggal"
                + ",ISC064_MARKETINGJUAL..MS_CUSTOMER.Nama + '<br>' + ISC064_MARKETINGJUAL..MS_AGENT.Nama + ' ' + ISC064_MARKETINGJUAL..MS_AGENT.Principal AS Customer"
                + ",'' AS Keterangan"
                + ",ISC064_MARKETINGJUAL..MS_KONTRAK.NamaProject AS Project"
                + " FROM ISC064_MARKETINGJUAL..MS_KONTRAK INNER JOIN ISC064_MARKETINGJUAL..MS_CUSTOMER ON ISC064_MARKETINGJUAL..MS_KONTRAK.NoCustomer = ISC064_MARKETINGJUAL..MS_CUSTOMER.NoCustomer "
                + " INNER JOIN ISC064_MARKETINGJUAL..MS_AGENT ON ISC064_MARKETINGJUAL..MS_KONTRAK.NoAgent = ISC064_MARKETINGJUAL..MS_AGENT.NoAgent"
                + " WHERE NoKontrak + NoUnit + ISC064_MARKETINGJUAL..MS_CUSTOMER.Nama + ISC064_MARKETINGJUAL..MS_AGENT.Nama + ISC064_MARKETINGJUAL..MS_AGENT.Principal "
                + " LIKE '%" + Cf.Str(keyword.Text) + "%'"
                + " AND MS_KONTRAK.Project = '" + project.SelectedValue + "'"
                + addq
                + " ORDER BY NoKontrak";

            DataTable rs = new DataTable();
            Db.Fill(rs, strSql);
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

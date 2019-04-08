using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.FINANCEAR
{
    public partial class DaftarKontrak : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Cf.SetGrid(tb);
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                Act.ProjectList(project);

                if (Request.QueryString["status"] == null)
                    metode.SelectedIndex = 0;
                else if (Request.QueryString["status"] == "a")
                    metode.SelectedIndex = 1;
                else if (Request.QueryString["status"] == "b")
                    metode.SelectedIndex = 2;

                if (metode.SelectedIndex != 0) metode.Enabled = false;

                Fill();
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
                addq = " AND MS_KONTRAK.Status = 'A'";
            else if (metode.SelectedIndex == 2)
                addq = " AND MS_KONTRAK.Status = 'B'";

            if (Request.QueryString["tag"] != null)
            {
                addq = addq + " AND (SELECT COUNT(NoUrut) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN WHERE NoKontrak = MS_KONTRAK.NoKontrak) = 0";
                info.Text = "Kontrak dengan kondisi jadwal tagihan sudah dikeluarkan, tidak ditampilkan.";
            }
            if (Request.QueryString["kom"] != null)
            {
                addq = addq + " AND FlagKomisi = 0";
                info.Text = "Kontrak dengan kondisi jadwal komisi sudah dikeluarkan, tidak ditampilkan.";
            }
            if (Request.QueryString["st"] != null)
            {
                //				addq = addq + " AND ST <> 'D'";
                //				info.Text = "Kontrak dengan kondisi serah terima sudah dijalankan, tidak ditampilkan.";
            }
            if (Request.QueryString["ppjb"] != null)
            {
                //addq = addq + " AND PPJB <> 'D'";
                //info.Text = "Kontrak dengan kondisi PPJB sudah dijalankan, tidak ditampilkan.";
            }
            if (Request.QueryString["ajb"] != null)
            {
                //				addq = addq + " AND AJB <> 'D'";
                //				info.Text = "Kontrak dengan kondisi AJB sudah dijalankan, tidak ditampilkan.";
            }
            string NoKontrak = "";

            if (Request.QueryString["status"] == "dari" || Request.QueryString["status"] == "sampai")
            {
                NoKontrak = "'<a href=\"javascript:callSource(''' + MS_KONTRAK.NoKontrak + ''', ''" + Request.QueryString["status"] + "'')\">'"
                    + "+ MS_KONTRAK.NoKontrak + '</a><br>";

            }
            else
            {
                NoKontrak = "'<a href=\"javascript:call(''' + MS_KONTRAK.NoKontrak + ''')\" ' + CASE MS_KONTRAK.Status WHEN 'B' THEN 'style = ''text-decoration:line-through''' ELSE '' END + '>'"
                      + "+ MS_KONTRAK.NoKontrak + '</a><br>";
            }

            NoKontrak += "  <font style=''font:8pt;color:' + CASE WHEN(SELECT COUNT(NoUrut) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN WHERE NoKontrak = MS_KONTRAK.NoKontrak) <> 0 THEN 'Black' ELSE 'Silver' END + ' ''>TAGIH</font>&nbsp;&nbsp;"
                         + "<font style=''font:8pt;color:' + CASE WHEN FlagKomisi = 1 THEN 'Black' ELSE 'Silver' END + '''>KOM</font>&nbsp;&nbsp;"
                         + "<font style=''font:8pt;color:' + CASE WHEN D.NoPPJB != '' AND (D.PPJB = 'D' OR D.PPJB = 'T') THEN 'Black' ELSE 'Silver' END + ' ''>PPJB</font>&nbsp;&nbsp;"
                         + "<font style=''font:8pt;color:' + CASE WHEN E.NoAJB != '' AND (E.AJB = 'D' OR E.AJB = 'T') THEN 'Black' ELSE 'Silver' END + '''>AJB</font>&nbsp;&nbsp;"
                         + "<font style=''font:8pt;color:' + CASE WHEN F.NoST != '' AND (F.ST = 'D' OR F.ST = 'T') THEN 'Black' ELSE 'Silver' END + '''>BAST</font>&nbsp;&nbsp;"
                         + "' AS NoKontrak"
                         ;

            string strSql = "SELECT "
                + NoKontrak
                + ",CONVERT(VARCHAR, TglKontrak, 106) AS Tanggal"
                + ",ms_kontrak.Status AS Keterangan"
                + ",NoUnit AS Unit"
                + ",MS_CUSTOMER.Nama + '<br>'+ "
                + " MS_AGENT.Nama + ' ' + MS_AGENT.Principal AS Customer"
                + ",MS_KONTRAK.NamaProject AS Project"
                + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer "
                + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_AGENT ON MS_KONTRAK.NoAgent = MS_AGENT.NoAgent"
                + " LEFT JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_PPJB D ON MS_KONTRAK.NoKontrak = D.NoKontrak"
                + " LEFT JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_AJB E ON MS_KONTRAK.NoKontrak = E.NoKontrak"
                + " LEFT JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_BAST F ON MS_KONTRAK.NoKontrak = F.NoKontrak"
                + " WHERE MS_KONTRAK.NoKontrak + NoUnit + MS_CUSTOMER.Nama + MS_AGENT.Nama + MS_AGENT.Principal "
                + " LIKE '%" + Cf.Str(keyword.Text) + "%'"
                + " AND MS_KONTRAK.Project = '" + project.SelectedValue + "'"
                + addq
                + " ORDER BY MS_KONTRAK.NoKontrak";

            DataTable rs = new DataTable();
            rs = Db.Rs(strSql);

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

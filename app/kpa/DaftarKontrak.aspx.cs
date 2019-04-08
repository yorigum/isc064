using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.KPA
{
    public partial class DaftarKontrak : System.Web.UI.Page
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
                addq = " AND MS_KONTRAK.Status = 'A'";
            else if (metode.SelectedIndex == 2)
                addq = " AND MS_KONTRAK.Status = 'B'";

            if (Request.QueryString["tag"] != null)
            {
                addq = addq + " AND (SELECT COUNT(NoUrut) FROM MS_TAGIHAN WHERE NoKontrak = MS_KONTRAK.NoKontrak) = 0";
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
            if (Request.QueryString["kpr"] != null)
            {
                addq = addq + " AND CaraBayar = 'KPR'";
                info.Text = "Kontrak dengan carabayar selain KPR tidak ditampilkan.";
            }
            if (Request.QueryString["kpr"] != null)
            {
                addq = addq + " AND CaraBayar = 'KPR'";
                info.Text = "Kontrak dengan carabayar selain KPR tidak ditampilkan.";
            }
            if (Request.QueryString["ajb"] != null)
            {
                addq = addq + " AND AJB <> 'D'";
                info.Text = "Kontrak dengan kondisi AJB sudah dijalankan, tidak ditampilkan.";
            }
            if (Request.QueryString["pengajuan"] != null)
            {
                addq = addq + " AND CaraBayar = 'KPR'";
                info.Text = "Kontrak dengan carabayar selain KPR tidak ditampilkan.";
            }

            string NoKontrak = "";


            if (Request.QueryString["status"] == "dari" || Request.QueryString["status"] == "sampai")
            {
                NoKontrak = "'<a href=\"javascript:callSource(''' + NoKontrak + ''', ''" + Request.QueryString["status"] + "'')\">'"
                    + "+ NoKontrak + '</a><br>";

            }
            else if (Request.QueryString["pengajuan"] != null)
            {
                NoKontrak = "'<a href=\"javascript:callkontrakproject(''' + NoKontrak + ''', ''' + MS_KONTRAK.Project + ''')\" ' + CASE MS_KONTRAK.Status WHEN 'B' THEN 'style = ''text-decoration:line-through''' ELSE '' END + '>'"
                      + "+ NoKontrak + '</a><br>";
            }
            else
            {
                NoKontrak = "'<a href=\"javascript:call(''' + NoKontrak + ''')\" ' + CASE MS_KONTRAK.Status WHEN 'B' THEN 'style = ''text-decoration:line-through''' ELSE '' END + '>'"
                      + "+ NoKontrak + '</a><br>";
            }

            NoKontrak += "  <font style=''font:8pt;color:' + CASE WHEN StatusBerkas = '1' THEN 'Black' ELSE 'Silver' END + ' ''>BERKAS</font>&nbsp;&nbsp;"
                         + "<font style=''font:8pt;color:' + CASE WHEN StatusWawancara = 'SELESAI' THEN 'Black' ELSE 'Silver' END + '''>WAWANCARA</font>&nbsp;&nbsp;"
                         + "<font style=''font:8pt;color:' + CASE WHEN StatusOTS = 'SELESAI' THEN 'Black' ELSE 'Silver' END + '''>OTS</font>&nbsp;&nbsp;"
                         + "<font style=''font:8pt;color:' + CASE WHEN StatusLPA = 'SELESAI' THEN 'Black' ELSE 'Silver' END + '''>LPA</font>&nbsp;&nbsp;"
                         + "<font style=''font:8pt;color:' + CASE WHEN StatusSP3K = 'SELESAI' THEN 'Black' ELSE 'Silver' END + '''>SP3K</font>&nbsp;&nbsp;"
                         + "<font style=''font:8pt;color:' + CASE WHEN StatusAkad = 'SELESAI' THEN 'Black' ELSE 'Silver' END + '''>AKAD</font>&nbsp;&nbsp;"
                         + "<font style=''font:8pt;color:' + CASE WHEN MS_KONTRAK.Status = 'B' THEN 'Red' ELSE 'Silver' END + '''>BATAL</font>"
                         + "' AS Kontrak"
                         ;


            string strSql = "SELECT "
                + NoKontrak
                + ",NoUnit AS Unit"
                + ",CONVERT(VARCHAR,TglKontrak,106) AS Tgl"
                + ",MS_CUSTOMER.Nama + '<br>' + MS_AGENT.Nama + ' ' + MS_AGENT.Principal AS Customer"
                + ",CaraBayar AS Keterangan"
                + ",NamaProject AS Project"
                + " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer "
                + " INNER JOIN MS_AGENT ON MS_KONTRAK.NoAgent = MS_AGENT.NoAgent"
                + " WHERE NoKontrak + NoUnit + MS_CUSTOMER.Nama + MS_AGENT.Nama + MS_AGENT.Principal "
                + " LIKE '%" + Cf.Str(keyword.Text) + "%'"
                + " AND MS_KONTRAK.Project = '" + project.SelectedValue + "'"
                + addq
                + " AND CaraBayar='KPR'"
                + " ORDER BY NoKontrak";

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

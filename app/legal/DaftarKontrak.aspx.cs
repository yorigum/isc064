using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LEGAL
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
                if (Request.QueryString["A.status"] == null)
                    metode.SelectedIndex = 0;
                else if (Request.QueryString["A.status"] == "a")
                    metode.SelectedIndex = 1;
                else if (Request.QueryString["A.status"] == "b")
                    metode.SelectedIndex = 2;

                if (metode.SelectedIndex != 0) metode.Enabled = false;

                Act.ProjectList(project);
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
                addq = " AND A.Status = 'A'";
            else if (metode.SelectedIndex == 2)
                addq = " AND A.Status = 'B'";

            if (Request.QueryString["st"] != null)
            {
                //				addq = addq + " AND ST <> 'D'";
                //				info.Text = "Kontrak dengan kondisi serah terima sudah dijalankan, tidak ditampilkan.";
            }
            if (Request.QueryString["ppjb"] != null)
            {
                //addq = addq + " AND D.PPJB <> 'D'";
                //info.Text = "Kontrak dengan kondisi PPJB sudah dijalankan, tidak ditampilkan.";
            }
            if (Request.QueryString["ajb"] != null)
            {
                //				addq = addq + " AND AJB <> 'D'";
                //				info.Text = "Kontrak dengan kondisi AJB sudah dijalankan, tidak ditampilkan.";
            }
            if (Request.QueryString["StatusSertifikat"] != null)
            {
                //				addq = addq + " AND AJB <> 'D'";
                //				info.Text = "Kontrak dengan kondisi AJB sudah dijalankan, tidak ditampilkan.";
            }
            if (Request.QueryString["StatusIMB"] != null)
            {
                //				addq = addq + " AND AJB <> 'D'";
                //				info.Text = "Kontrak dengan kondisi AJB sudah dijalankan, tidak ditampilkan.";
            }

            string NoKontrak = "";
            if (Request.QueryString["status"] == "dari" || Request.QueryString["status"] == "sampai")
            {
                NoKontrak = "'<a href=\"javascript:callSource(''' + A.NoKontrak + ''' , '''+ A.Status+''')\" ' + CASE A.Status WHEN 'B' THEN 'style = ''text-decoration:line-through''' ELSE '' END + '>'"
                  + "+ A.NoKontrak + '</a><br>";
            }
            else
            {
                NoKontrak = "'<a href=\"javascript:call(''' + A.NoKontrak + ''')\" ' + CASE A.Status WHEN 'B' THEN 'style = ''text-decoration:line-through''' ELSE '' END + '>'"
                  + "+ A.NoKontrak + '</a><br>";
            }

            NoKontrak += "  <font style=''font:8pt;color:' + CASE WHEN D.NoPPJB != '' AND (D.PPJB = 'D' OR D.PPJB = 'T') THEN 'Black' ELSE 'Silver' END + ' ''>PPJB</font>&nbsp;&nbsp;"
                         + "<font style=''font:8pt;color:' + CASE WHEN E.NoAJB != '' AND (E.AJB = 'D' OR E.AJB = 'T') THEN 'Black' ELSE 'Silver' END + '''>AJB</font>&nbsp;&nbsp;"
                         + "<font style=''font:8pt;color:' + CASE WHEN F.NoST != '' AND (F.ST = 'D' OR F.ST = 'T') THEN 'Black' ELSE 'Silver' END + '''>BAST</font>&nbsp;&nbsp;"
                         + "<font style=''font:8pt;color:' + CASE WHEN G.StatusIMB = '3' THEN 'Black' ELSE 'Silver' END + '''>IMB</font>&nbsp;&nbsp;"
                         + "<font style=''font:8pt;color:' + CASE WHEN H.StatusSertifikat = '3' THEN 'Black' ELSE 'Silver' END + '''>STT</font>&nbsp;&nbsp;"                         
                         + "' AS Kontrak"
                         ;

            string strSql = "SELECT "
                + NoKontrak
                + ",A.NoUnit AS Unit"
                + ",CONVERT(VARCHAR,A.TglKontrak,106) AS Tgl"
                + ",B.Nama + '<br>' + C.Nama + ' ' + C.Principal AS Customer"
                + ",'' AS Keterangan"
                + ",A.NamaProject AS Project"
                + " FROM MS_KONTRAK A INNER JOIN MS_CUSTOMER B ON A.NoCustomer = B.NoCustomer "
                + " INNER JOIN MS_AGENT C ON A.NoAgent = C.NoAgent"
                + " LEFT JOIN MS_PPJB D ON A.NoKontrak = D.NoKontrak"
                + " LEFT JOIN MS_AJB E ON A.NoKontrak = E.NoKontrak"
                + " LEFT JOIN MS_BAST F ON A.NoKontrak = F.NoKontrak"
                + " LEFT JOIN MS_IMB G ON A.NoKontrak = G.NoKontrak"
                + " LEFT JOIN MS_SERTIFIKAT H ON A.NoKontrak = H.NoKontrak"
                + " WHERE A.NoKontrak + A.NoUnit + B.Nama + C.Nama + C.Principal "
                + " LIKE '%" + Cf.Str(keyword.Text) + "%'"
                + " AND A.Project = '" + project.SelectedValue + "'"
                + addq
                + " ORDER BY A.NoKontrak";

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

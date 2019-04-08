using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.COLLECTION
{
    public partial class LunasRegistrasi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Cf.SetGrid(tb);
            if (!Page.IsPostBack) Act.ProjectList(project);
            Js.Focus(this, keyword);
            Js.ConfirmKeyword(this, keyword);

            FeedBack();

        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "<a href=\"javascript:popEditSKL('" + Request.QueryString["done"] + "')\">"
                        + "Registrasi Surat Keterangan Lunas Berhasil..."
                        + "</a>";
            }
        }

        protected void search_Click(object sender, EventArgs e)
        {
            Cf.SetGrid(tb);
            string[] x = Sc.MktCatalog();
            for (int i = 0; i <= x.GetUpperBound(0); i++)
            {
                string[] xdetil = x[i].Split(';');
                Fill(xdetil[0], xdetil[1]);
            }
        }

        private void Fill(string Tb, string Ket)
        {
            string nav = "'<a href=\"javascript:call('''+ CONVERT(varchar(50),NoKontrak) +''')\">' + NoKontrak + '</a>'";

            string strSql = "SELECT "
                    + nav
                    + " AS Kontrak"
                    + ",MS_KONTRAK.Status AS Status"
                    + ",'" + Ket + "' AS Tipe"
                    + ",MS_KONTRAK.NoUnit AS Unit"
                    + ",Nama AS Customer"
                    + ",MS_KONTRAK.NamaProject AS Project"
                    + " FROM " + Tb + "..MS_KONTRAK AS MS_KONTRAK INNER JOIN " + Tb + "..MS_CUSTOMER AS MS_CUSTOMER ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer "
                    + " WHERE NoKontrak + NoUnit + Nama"
                    + " LIKE '%" + Cf.Str(keyword.Text) + "%'"
                    + " AND MS_KONTRAK.PersenLunas = 100"
                    + " AND MS_KONTRAK.Project = '" + project.SelectedValue + "'"
                    + " AND MS_KONTRAK.NoKontrak NOT IN (SELECT Ref FROM "+Mi.DbPrefix+"FINANCEAR..MS_SKL)"
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
            string[] x = Sc.MktCatalog();
            for (int i = 0; i <= x.GetUpperBound(0); i++)
            {
                string[] xdetil = x[i].Split(';');
                Fill(xdetil[0], xdetil[1]);
            }
        }
    }
}

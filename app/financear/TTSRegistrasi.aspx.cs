using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.FINANCEAR
{
    public partial class TTSRegistrasi : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Cf.SetGrid(tb);
            if (!Page.IsPostBack) Act.ProjectList(project);
            Js.Focus(this, keyword);
            //Js.ConfirmKeyword(this, keyword);

            FeedBack();
        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                {
                    if (Request.QueryString["from"] != null && Request.QueryString["to"] != null)
                    {
                        feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                            + "<a href=\"javascript:popPrintTTSMulti('" + Request.QueryString["from"] + "', '" + Request.QueryString["to"] + "');\">"
                            + "Registrasi Berhasil.."
                            + "</a>"
                            ;
                    }
                    else
                    {
                        feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                            + "<a href=\"javascript:popEditTTS('" + Request.QueryString["done"] + "')\">"
                            + "Registrasi Berhasil.."
                            + "</a>"
                            ;
                    }
                }
            }
        }

		protected void search_Click(object sender, System.EventArgs e)
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
            string strSql = "";

            if (Ket == "TENANT")
            {
                strSql = "SELECT "
                    + " NoPenghuni AS Ref"
                    + ",NoUnit"
                    + ",Nama AS Cs"
                    + ",(SELECT COUNT(*) FROM " + Tb + "..MS_TAGIHAN WHERE NoPenghuni = MS_PENGHUNI.NoPenghuni AND CaraBayar = '') AS count"
                    + ",MS_PENGHUNI.Status"
                    + " FROM " + Tb + "..MS_PENGHUNI AS MS_PENGHUNI "
                    + " WHERE NoPenghuni + NoUnit + Nama"
                    + " LIKE '%" + Cf.Str(keyword.Text) + "%'"
                    + " ORDER BY NoPenghuni";
            }
            else
            {
                string nav = "NoKontrak + '<br><a href=\"javascript:call('''+ CONVERT(varchar(50),NoKontrak) +''',''" + Ket + "'')\"> Next </a>'";

                strSql = "SELECT "
                    + nav
                    + " AS Ref"
                    + ",MS_KONTRAK.Status AS Status"
                    + ",'"+Ket+"' AS Tipe"                    
                    + ",NoUnit AS Unit"
                    + ",Nama AS Customer"
                    + ",(  SELECT COUNT(*) FROM " + Tb + "..MS_TAGIHAN AS MS_TAGIHAN WHERE NoKontrak = MS_KONTRAK.NoKontrak AND "
                    + "		  ( NilaiTagihan - "
                    + "				(	SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Tb + "..MS_PELUNASAN "
                    + "					WHERE NoTagihan = MS_TAGIHAN.NoUrut AND NoKontrak = MS_KONTRAK.NoKontrak"
                    + "				)"
                    + "		  ) > 0"
                    + " )  AS Tagihan"                    
                    + ",MS_KONTRAK.NamaProject AS Project"
                    + " FROM " + Tb + "..MS_KONTRAK AS MS_KONTRAK INNER JOIN " + Tb + "..MS_CUSTOMER AS MS_CUSTOMER ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer "
                    + " WHERE NoKontrak + NoUnit + Nama"
                    + " LIKE '%" + Cf.Str(keyword.Text) + "%'"
                    + " AND MS_KONTRAK.Status = 'A'"
                    + " AND MS_KONTRAK.Project = '" + project.SelectedValue + "'"
                    + " AND ( SELECT COUNT(*) FROM ISC064_MARKETINGJUAL..MS_TAGIHAN AS MS_TAGIHAN WHERE NoKontrak = MS_KONTRAK.NoKontrak AND ( NilaiTagihan - ( SELECT ISNULL(SUM(NilaiPelunasan),0) FROM ISC064_MARKETINGJUAL..MS_PELUNASAN WHERE NoTagihan = MS_TAGIHAN.NoUrut AND NoKontrak = MS_KONTRAK.NoKontrak ) ) > 0 ) > 0 "
                    + " ORDER BY NoKontrak";
            }            
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

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Drawing;

namespace ISC064.KPA
{
    public partial class KontrakRealisasi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
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
                {

                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "<a href=\"javascript:popEditRealisasi('" + Request.QueryString["done"] + "')\">"
                        + "Registrasi Berhasil.."
                        + "</a>"
                        ;

                }
            }
        }
        protected void search_Click(object sender, EventArgs e)
        {
            Cf.SetGrid(tb);
            Fill();
        }
        protected void Fill()
        {
            string nav = "'<a href=\"javascript:call2('''+ CONVERT(varchar(50),a.NoPengajuan)+''')\">Next...</a>'";
            string pengajuan = ",'<a href=\"javascript:call(''' + CONVERT(varchar(50),a.NoPengajuan) + ''')\" ' + CASE a.Status WHEN 'CANCEL' THEN 'style = ''text-decoration:line-through''' ELSE '' END + '>'"
                            + "+ FORMAT(a.NoPengajuan,'000000#') + '</a>'";

            string strSql = "SELECT "
                + nav
                + " AS Next"
                + pengajuan
                + " AS Pengajuan"
                + ",CONVERT(VARCHAR,a.TglFormulir,106) + '<br>' + a.UserID AS Tgl"
                + ",a.Keterangan AS Keterangan"
                + ",FORMAT(a.Total,'#,###') AS Total"
                + ",c.NamaProject AS Project"
                + " FROM " + Mi.DbPrefix + "FINANCEAR..MS_PENGAJUAN_KPA a"
                + " INNER JOIN " + Mi.DbPrefix + "FINANCEAR..MS_PENGAJUAN_KPA_DETIL b ON a.NoPengajuan = b.NoPengajuan"
                + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK c ON c.NoKontrak = b.NoKontrak"
                + " WHERE"
                + " b.NoKontrak + b.Nama + b.NoUnit + a.Keterangan + CONVERT(varchar(MAX), a.NoPengajuan)"
                + " LIKE '%" + Cf.Str(keyword.Text) + "%'"
                + " AND a.Status='BARU'"
                + " AND c.Project = '" + project.SelectedValue + "'"
                + " ORDER BY a.NoPengajuan ";
            
            DataTable rs = Db.Rs(strSql);
            tb.DataSource = rs;
            tb.DataBind();
            //Rpt.NoData(rpt, rs, "Tidak ada Pengajuan Tagihan KPA dengan kriteria seperti tersebut diatas.");

        }

        protected void tb_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            tb.PageIndex = e.NewPageIndex;
            Fill();
        }
    }
}
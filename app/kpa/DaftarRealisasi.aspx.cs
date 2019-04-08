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
    public partial class DaftarRealisasi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();
            Cf.SetGrid(tb);
            if (!Page.IsPostBack) Act.ProjectList(project);
            //Js.ConfirmKeyword(this, keyword);
        }
        protected void search_Click(object sender, System.EventArgs e)
        {
            Cf.SetGrid(tb);
            Fill();
        }

        private void Fill()
        {
            string nav = ",'<a href=\"javascript:call('''+CONVERT(varchar(50),a.NoReal)+''')\">' + FORMAT(a.NoReal,'000000#') + '</a>'";  
                      
            string strSql = "SELECT distinct(a.NoReal)"
                + " AS Realisasi"
                + nav
                + " AS NoRealisasi"
                + ",CONVERT(VARCHAR,a.TglReal,106) + '<br>' + a.UserID AS Tgl"
                + ",a.Ket + '<br>' + "
                + "replace(replace((select + '<b>' + n.NoTTS2 + '</b><bR>' + n.Ref + ' - ' + n.Unit + '<br>' + n.Customer + '<br>' "
                + " from ms_pelunasan_KPA m join ISC064_financear..ms_tts n on m.NoTTS = n.NoTTS where m.NoRealKPA = a.NoReal  FOR XML PATH('')),'&lt;','<'),'&gt;','>') AS Keterangan"
                + ",FORMAT(a.Total,'#,###') AS Total"
                + ",c.NamaProject AS Project"
                + " FROM " + Mi.DbPrefix + "FINANCEAR..MS_REAL_KPA a"
                + " INNER JOIN MS_PELUNASAN_KPA b ON a.NoReal = b.NoRealKPA"
                + " INNER JOIN MS_KONTRAK c ON c.NoKontrak = b.NoKontrak"
                + " INNER JOIN MS_CUSTOMER d ON d.NoCustomer = c.NoCustomer"
                + " WHERE"
                + " b.NoKontrak + d.Nama + c.NoUnit + a.Ket + CONVERT(varchar(MAX), a.NoPengajuan)"
                + " LIKE '%" + Cf.Str(keyword.Text) + "%'"
                + " AND c.Project = '" + project.SelectedValue + "'"
                + " ORDER BY a.NoReal";

            DataTable rs = Db.Rs(strSql);
            
            tb.DataSource = rs;
            tb.DataBind();
        }

        protected void tb_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            tb.PageIndex = e.NewPageIndex;
            Fill();
        }
    }
}

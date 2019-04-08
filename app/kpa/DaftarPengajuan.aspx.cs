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
using System.Linq;


namespace ISC064.KPA
{
    public partial class DaftarPengajuan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();
            Cf.SetGrid(tb);
            //Js.ConfirmKeyword(this, keyword);

            if (!Page.IsPostBack)
            {
                if (Request.QueryString["status"] == null)
                    metode.SelectedIndex = 0;
                else if (Request.QueryString["status"] == "BARU")
                    metode.SelectedIndex = 1;
                else if (Request.QueryString["status"] == "POST")
                    metode.SelectedIndex = 2;
                else if (Request.QueryString["status"] == "CANCEL")
                    metode.SelectedIndex = 3;

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
                addq = " AND a.Status = 'BARU'";
            else if (metode.SelectedIndex == 2)
                addq = " AND a.Status = 'POST'";
            else if (metode.SelectedIndex == 3)
                addq = " AND a.Status = 'CANCEL'";

            string nav = ",'<a href=\"javascript:call(''' + CONVERT(varchar(50),a.NoPengajuan) + ''')\" ' + CASE a.Status WHEN 'CANCEL' THEN 'style = ''text-decoration:line-through''' ELSE '' END + '>'"
                    + "+ FORMAT(a.NoPengajuan,'000000#') + '</a><br><i>' + a.Status +  '</i>'"; 

            string strSql = "SELECT distinct (a.NoPengajuan) "                
                + " AS Pengajuan"
                + nav
                + " AS NoPengajuan"
                + ",CONVERT(VARCHAR,a.TglFormulir,106) + '<br>' + a.UserID AS Tgl"
                + ",a.Keterangan + "
                + "'<br>' + replace(replace((SELECT + '<b>' + Nokontrak + '<br></b>' + Nama + ' - ' + NoUnit + '<br>' FROM " + Mi.DbPrefix+ "FINANCEAR..MS_PENGAJUAN_KPA_DETIL WHERE NoPengajuan = a.NoPengajuan FOR XML PATH('')),'&lt;','<'),'&gt;','>')  AS Keterangan"
                + ",FORMAT(a.Total,'#,###') AS Total"
                + ",c.NamaProject AS Project"                
                + " FROM " + Mi.DbPrefix + "FINANCEAR..MS_PENGAJUAN_KPA a"
                + " INNER JOIN " + Mi.DbPrefix + "FINANCEAR..MS_PENGAJUAN_KPA_DETIL b ON a.NoPengajuan = b.NoPengajuan"
                + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK c ON b.NoKontrak = c.NoKontrak"
                + " WHERE"
                + " b.NoKontrak + b.Nama + b.NoUnit + a.Keterangan + CONVERT(varchar(MAX), a.NoPengajuan)"
                + " LIKE '%" + Cf.Str(keyword.Text) + "%'"
                + " AND c.Project = '" + project.SelectedValue + "'"
                + addq
                + " ORDER BY a.NoPengajuan";

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

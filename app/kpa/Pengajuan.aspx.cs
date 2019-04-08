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
    public partial class Pengajuan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();
            Cf.SetGrid(tb);
            if (!Page.IsPostBack)
            {
                Act.ProjectList(project);
                init();
            }

            Js.Focus(this, search);

        }
        private void init()
        {
            dari.Text = Cf.Day(DateTime.Today);
            sampai.Text = Cf.Day(DateTime.Today);

            bindUser();

        }
        private void bindUser()
        {
            DataTable rs;

            rs = Db.Rs("SELECT DISTINCT UserID FROM " + Mi.DbPrefix + "FINANCEAR..MS_PENGAJUAN_KPA ORDER BY UserID");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["UserID"].ToString();
                user.Items.Add(new ListItem(v, v));
            }
        }
        private bool valid()
        {
            string s = "";
            bool x = true;

            if (!Cf.isTgl(dari))
            {
                daric.Text = "Tanggal";
                if (s == "") s = dari.ID;
                x = false;
            }
            else
                daric.Text = "";

            if (!Cf.isTgl(sampai))
            {
                sampaic.Text = "Tanggal";
                if (s == "") s = sampai.ID;
                x = false;
            }
            else
                sampaic.Text = "";

            if (!x)
                RegisterStartupScript("err"
                    , "<script type='text/javascript'>document.getElementById('" + s + "').select()</script>");

            return x;
        }

        protected void display_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                Cf.SetGrid(tb);
                Fill();
            }
        }

        private void Fill()
        {
            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);
            if (Dari > Sampai)
            {
                DateTime x = Sampai;
                Sampai = Dari;
                Dari = x;
            }

            string UserID = "";
            if (user.SelectedIndex != 0)
                UserID = " AND UserID = '" + user.SelectedValue + "'";


            string Status = "";
            if (statusB.Checked) Status = " AND a.Status = 'BARU'"; //baru
            if (statusR.Checked) Status = " AND a.Status = 'POST'"; //terealisasi

            string nav = ",'<a href=\"javascript:call(''' + CONVERT(varchar(50),a.NoPengajuan) + ''')\" ' + CASE a.Status WHEN 'CANCEL' THEN 'style = ''text-decoration:line-through''' ELSE '' END + '>'"
                        + "+ FORMAT(a.NoPengajuan,'000000#') + '</a><br><i>' + a.Status +  '</i>'";

            string Project = (project.SelectedIndex == 0) ? " AND d.Project IN (" + Act.ProjectListSql + ")" : " AND d.Project = '" + project.SelectedValue + "'";

            string strSql = "SELECT distinct(a.NoPengajuan) "
                + "AS Pengajuan"
                + nav
                + " AS NoPengajuan"
                + ",CONVERT(VARCHAR,a.TglFormulir,106) + '<br>' + a.UserID AS Tgl"
                + ",a.Keterangan + '<br>' + replace(replace((SELECT  + '<b>' + b.nokontrak + '<br></b>' + b.Nama + ' - ' + b.NoUnit + '<br>' FROM " + Mi.DbPrefix + "FINANCEAR..MS_PENGAJUAN_KPA_DETIL B WHERE a.NoPengajuan = B.nopengajuan FOR XML PATH('')),'&lt;','<'),'&gt;','>') AS Keterangan "
                + ",FORMAT(a.Total,'#,###') AS Total"
                + ",FORMAT((SELECT ISNULL(SUM(Total),0) FROM " + Mi.DbPrefix + "FINANCEAR..MS_REAL_KPA WHERE NoPengajuan=a.NoPengajuan),'#,###') AS Realisasi"
                + ",d.NamaProject AS Project"
                + " FROM " + Mi.DbPrefix + "FINANCEAR..MS_PENGAJUAN_KPA a"
                + " INNER JOIN " + Mi.DbPrefix + "FINANCEAR..MS_PENGAJUAN_KPA_DETIL c on c.NoPengajuan = a.NoPengajuan"
                + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_Kontrak d on d.NoKontrak = c.NoKontrak"
                + " WHERE 1=1 "
                + " AND CONVERT(varchar,TglFormulir,112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND CONVERT(varchar,TglFormulir,112) <= '" + Cf.Tgl112(Sampai) + "'"
                + Project
                + UserID
                + Status
                + " ORDER BY a.NoPengajuan";
            //Response.Write(strSql);
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

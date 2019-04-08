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
    public partial class Realisasi : System.Web.UI.Page
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
                    , "<script language='javascript'>document.getElementById('" + s + "').select()</script>");

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
                UserID = " AND a.UserID = '" + user.SelectedValue + "'";

            string nav = ",'<a href=\"javascript:call('''+CONVERT(varchar(50),a.NoReal)+''')\">' + FORMAT(a.NoReal,'000000#') + '</a><br><i>' + d.Status + '</i>'";

            string Project = (project.SelectedIndex == 0) ? " AND c.Project IN (" + Act.ProjectListSql + ")" : " AND c.Project = '" + project.SelectedValue + "'";

            string strSql = "SELECT distinct(a.NoReal) "
                + " AS Realisasi"
                + nav
                + " AS NoRealisasi"
                + ",CONVERT(VARCHAR,a.TglReal,106) + '<br>' + a.UserID AS Tgl"
                + ",a.Ket + '<br>' + "
                + "replace(replace((select + '<b>' + n.NoTTS2 + '</b><bR>' + n.Ref + ' - ' + n.Unit + '<br>' + n.Customer + '<br>' "
                + " from ms_pelunasan_KPA m join ISC064_financear..ms_tts n on m.NoTTS = n.NoTTS where m.NoRealKPA = a.NoReal  FOR XML PATH('')),'&lt;','<'),'&gt;','>') AS Keterangan"
                + ",FORMAT(a.Total,'#,###') AS Total"
                + ",c.NamaProject AS Project"
                + " FROM " + Mi.DbPrefix + "FINANCEAR..MS_REAL_KPA a JOIN " + Mi.DbPrefix + "FINANCEAR..MS_PENGAJUAN_KPA_DETIL b ON a.NoPengajuan = b.NoPengajuan"
                + " JOIN " + Mi.DbPrefix + "FINANCEAR..MS_PENGAJUAN_KPA d ON b.NoPengajuan = d.NoPengajuan"
                + " JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK c ON b.NoKontrak = c.NoKontrak"
                + " WHERE 1=1 "
                + " AND CONVERT(varchar,TglReal,112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND CONVERT(varchar,TglReal,112) <= '" + Cf.Tgl112(Sampai) + "'"
                + Project
                + UserID
                + " ORDER BY NoReal";

            DataTable rs = Db.Rs(strSql);
            tb.DataSource = rs;
            tb.DataBind();
            //Rpt.NoData(rpt, rs, "Tidak ada Realisasi Tagihan KPA dengan kriteria seperti tersebut diatas.");

        }

        protected void tb_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            tb.PageIndex = e.NewPageIndex;
            Fill();
        }
    }
}
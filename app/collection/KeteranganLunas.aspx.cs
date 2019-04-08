using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ISC064.COLLECTION
{
    public partial class KeteranganLunas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
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

            string nav = "'<a href=\"javascript:call('''+ CONVERT(varchar(50),NoSKL) +''')\">' + NoSKL + '</a>'";

            string Project = (project.SelectedIndex == 0) ? " AND b.Project IN(" + Act.ProjectListSql + ")" : " AND b.Project = '" + project.SelectedValue + "'";

            string strSql = "SELECT"
                          + nav
                          + " AS System"
                          + ",a.NoSKLManual AS Manual"
                          + ",CONVERT(VARCHAR,a.TglSKL,106) AS Tgl"
                          + ",a.Ref AS Kontrak"
                          + ",(SELECT Nama FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER WHERE NOCUSTOMER IN(SELECT NOCUSTOMER FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NOKONTRAK=a.Ref)) AS Customer"
                          + ",b.NamaProject AS Project"
                          + " FROM MS_SKL a INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK b ON a.Ref = b.NoKontrak"
                          + " WHERE CONVERT(varchar,TglSKL,112) >= '" + Cf.Tgl112(Dari) + "'"
                          + " AND CONVERT(varchar,TglSKL,112) <= '" + Cf.Tgl112(Sampai) + "'"
                          + Project
                          + " ORDER BY NoSKL";

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

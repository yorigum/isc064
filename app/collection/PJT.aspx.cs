using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.COLLECTION
{
    public partial class PJT : System.Web.UI.Page
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

            //tipe
            string[] x = Sc.MktCatalog();
            for (int i = 0; i <= x.GetUpperBound(0); i++)
            {
                string[] xdetil = x[i].Split(';');
                tipe.Items.Add(new ListItem(xdetil[2], xdetil[1]));
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

            string Tipe = "";
            if (tipe.SelectedIndex != 0)
                Tipe = " AND Tipe = '" + Cf.Str(tipe.SelectedValue) + "'";

            string nav = "'<a href=\"javascript:call('''+ CONVERT(varchar(50),a.NoPJT) +''')\">' + a.NoPJT + '</a>'";

            string Project = (project.SelectedIndex == 0) ? " AND b.Project IN(" + Act.ProjectListSql + ")" : " AND b.Project = '" + project.SelectedValue + "'";

            string strSql = "SELECT"
                + nav
                + " AS Invoice"
                + ",CONVERT(VARCHAR,TglPJT,106) AS Tgl"
                + ",a.Customer + '<br>Telp. ' + a.NoTelp AS Customer"
                + ",a.Tipe +'No. : '+ a.Ref + '<br>Unit : ' + a.Unit AS Keterangan"
                + ",FORMAT(a.Total,'#,###') AS Nilai "
                + ",(SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = b.Project) AS Project"
                + " FROM MS_PJT a INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK b ON a.Ref = b.NoKontrak"
                + " WHERE 1=1 "
                + Tipe
                + Project
                + " AND CONVERT(varchar,TglPJT,112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND CONVERT(varchar,TglPJT,112) <= '" + Cf.Tgl112(Sampai) + "'"                
                + " ORDER BY NoPJT";
            
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

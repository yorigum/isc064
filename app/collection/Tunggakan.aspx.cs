using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.COLLECTION
{
    public partial class Tunggakan : System.Web.UI.Page
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
                Js.Alert(this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Project Harus Dipilih.\\n"
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );
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

            string Tipe = "";
            if (tipe.SelectedIndex != 0)
                Tipe = " AND a.Tipe = '" + Cf.Str(tipe.SelectedValue) + "'";

            string Level = "";
            if (level.SelectedIndex != 0)
                Level = " AND a.LevelTunggakan = " + level.SelectedValue;

            string Status = "";
            if (status.SelectedIndex != 0)
                Status = " AND a.Status = '" + Cf.Str(status.SelectedValue) + "'";

            string nav = "'<a href=\"javascript:call('''+ CONVERT(varchar(50),NoTunggakan) +''')\">' + ManualTunggakan + '</a>'";

            string Project = (project.SelectedIndex == 0) ? " AND b.Project IN(" + Act.ProjectListSql + ")" : " AND b.Project = '" + project.SelectedValue + "'";

            string strSql = "SELECT"
                + nav
                + " AS ST"
                + ",CONVERT(VARCHAR,TglTunggakan,106) AS Tgl"
                + ",'<font style=font:bold 15pt>' + CONVERT(varchar(50),a.LevelTunggakan) + '</font><br><i>' +"
                + " CASE a.Status "
                + "		WHEN 'A' THEN 'AKTIF' "
                + "		WHEN 'S' THEN 'SETTLED' "
                + "		WHEN 'U' THEN 'UPGRADED' "
                + " END AS Status"
                + ",a.Customer + '<br>Telp. ' + a.NoTelp AS Customer"
                + ",a.Tipe + ' No. : ' + a.Ref + '<br>Unit : ' + a.Unit AS Keterangan"
                + ",FORMAT(a.Total,'#,###') AS Nilai"
                + ",b.NamaProject AS Project"
                + " FROM MS_TUNGGAKAN a INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK b"
                + " ON a.Ref = b.NoKontrak"
                + " WHERE 1=1 "
                + Project
                + Tipe
                + Level
                + Status
                + " AND CONVERT(varchar,TglTunggakan,112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND CONVERT(varchar,TglTunggakan,112) <= '" + Cf.Tgl112(Sampai) + "'"
                + " ORDER BY NoTunggakan";

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

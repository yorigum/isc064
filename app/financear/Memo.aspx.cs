using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.FINANCEAR
{
    public partial class Memo : System.Web.UI.Page
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

            bindUser();

            //tipe
            string[] x = Sc.MktCatalog();
            for (int i = 0; i <= x.GetUpperBound(0); i++)
            {
                string[] xdetil = x[i].Split(';');
                tipe.Items.Add(new ListItem(xdetil[2], xdetil[1]));
            }
        }

        private void bindUser()
        {
            DataTable rs;

            rs = Db.Rs("SELECT DISTINCT UserID FROM MS_MEMO ORDER BY UserID");
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
                ClientScript.RegisterStartupScript(
                    GetType()
                    , "err"
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
                UserID = " AND a.UserID = '" + user.SelectedValue + "'";

            string CaraBayar = "";
            if (carabayar.SelectedIndex != 0)
                CaraBayar = " AND a.CaraBayar = '" + carabayar.SelectedValue + "'";

            string Tipe = "";
            if (tipe.SelectedIndex != 0)
                Tipe = " AND a.Tipe = '" + Cf.Str(tipe.SelectedValue) + "'";

            string Status = "";
            if (statusP.Checked) Status = " AND a.Status = 'POST'";
            if (statusV.Checked) Status = " AND a.Status = 'VOID'";

            string nav = "'<a onclick=popEditMEMO('''+ CONVERT(VARCHAR(50),a.NoMemo)+''')>' + a.NoMEMO2 + '</a><br><i>' + a.Status + '</i>'";

            string Project = (project.SelectedIndex == 0) ? " AND b.Project IN (" + Act.ProjectListSql + ")" : " AND b.Project = '" + project.SelectedValue + "'";

            string strSql = "SELECT"
                + nav
                + " AS Memo"
                + ",CONVERT(VARCHAR,a.TglMEMO,106) + '<br>' + (SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..USERNAME WHERE UserID = a.UserID) AS Tgl"
                + ",a.Tipe + ' No : ' + a.Ref + '<br>' + a.Unit + '<br>' + a.Customer AS Customer"
                + ",a.Ket + CASE WHEN a.Titip != '' THEN '<br>Pengelola : ' + a.Titip ELSE '' END + CASE WHEN a.Tolak != '' THEN '<br>Tolakan : ' + a.Tolak ELSE '' END AS Keterangan"
                + ",CASE a.CaraBayar"
                + "		WHEN 'TN' THEN 'TUNAI'"
                + "		WHEN 'KK' THEN 'KARTU KREDIT'"
                + "		WHEN 'KD' THEN 'KARTU DEBIT'"
                + "		WHEN 'TR' THEN 'TRANSFER BANK'"
                + "		WHEN 'BG' THEN 'CEK GIRO'"
                + "		WHEN 'UJ' THEN 'UANG JAMINAN'"
                + "		WHEN 'MB' THEN 'MERCHANT BANKING'"
                + "		WHEN 'PP' THEN 'PENGHAPUSAN PIUTANG'"
                + "		WHEN 'DN' THEN 'DISKON'"
                + "		WHEN 'FM' THEN 'FINANCE MEMORIAL'"
                + "		WHEN 'TG' THEN 'TUKAR GULING'"
                + " END AS Tipe"
                + ",CASE WHEN a.Pph = '1' THEN + FORMAT(a.Total,'#,###') + '<br>PPH' ELSE FORMAT(a.Total,'#,###')  END AS Total"
                + ",b.NamaProject AS Project"
                + " FROM MS_MEMO a JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK b ON a.Ref = b.NoKontrak"
                + " WHERE 1=1 "
                + " AND CONVERT(varchar,TglMEMO,112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND CONVERT(varchar,TglMEMO,112) <= '" + Cf.Tgl112(Sampai) + "'"
                + Project
                + UserID
                + CaraBayar
                + Tipe
                + Status
                + " ORDER BY NoMEMO";

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

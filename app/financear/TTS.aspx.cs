using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.FINANCEAR
{
    public partial class TTS : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();
            Cf.SetGrid(tb);
            if (!Page.IsPostBack)
            {
                init();
            }

            Js.Focus(this, search);
        }

        private void init()
        {
            dari.Text = Cf.Day(DateTime.Today);
            sampai.Text = Cf.Day(DateTime.Today);

            bindUser();
            Act.ProjectList(project);
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

            rs = Db.Rs("SELECT DISTINCT UserID FROM MS_TTS ORDER BY UserID");
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

            if (project.SelectedIndex == 0)
            {
                x = false;

                Js.Alert(this,"Pilih Project :","");
            }

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
                UserID = " AND UserID = '" + user.SelectedValue + "'";

            string CaraBayar = "";
            if (carabayar.SelectedIndex != 0)
                CaraBayar = " AND CaraBayar = '" + carabayar.SelectedValue + "'";


            string Project = "";
            if (project.SelectedValue != "Project :")
            {
                Project = " AND MS_TTS.Project = '" + project.SelectedValue + "'";
            }
            string Tipe = "";
            if (tipe.SelectedIndex != 0)
                Tipe = " AND Tipe = '" + Cf.Str(tipe.SelectedValue) + "'";

            string Status = "";
            if (statusB.Checked) Status = " AND MS_TTS.Status = 'BARU'";
            if (statusP.Checked) Status = " AND MS_TTS.Status = 'POST'";
            if (statusV.Checked) Status = " AND MS_TTS.Status = 'VOID'";

            string nav = "'<a href=\"javascript:call(''' + CONVERT(varchar(50),NoTTS) + ''')\" ' + CASE MS_TTS.Status WHEN 'VOID' THEN 'style = ''text-decoration:line-through''' ELSE '' END + '>'"
                      + "+ CONVERT(varchar(50),NoTTS2) + '</a><br>TTS Manual : ' + MS_TTS.ManualTTS +'<br><i>'+ MS_TTS.Status + '</i>' + CASE MS_TTS.Status WHEN 'POST' THEN '<br>KWT Manual:' +MS_TTS.ManualBKM ELSE '' END";

            string strSql = "SELECT"
                + nav
                + " AS TTS"
                + ",CONVERT(VARCHAR,MS_TTS.TglTTS,106) + '<br>' + (SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..USERNAME WHERE UserID = MS_TTS.UserID) AS Tgl"
                + ",MS_TTS.Tipe + ' No : ' + MS_TTS.Ref + '<br>' + MS_TTS.Unit + '<br>' + MS_TTS.Customer AS Customer"
                + ",MS_TTS.Ket + CASE WHEN MS_TTS.Titip != '' THEN '<br>Pengelola : ' + MS_TTS.Titip ELSE '' END + CASE WHEN MS_TTS.Tolak != '' THEN '<br>Tolakan : ' + MS_TTS.Tolak ELSE '' END AS Keterangan"
                + ",CASE MS_TTS.CaraBayar"
                + "		WHEN 'TN' THEN 'TUNAI'"
                + "		WHEN 'KK' THEN 'KARTU KREDIT'"
                + "		WHEN 'KD' THEN 'KARTU DEBIT'"
                + "		WHEN 'TR' THEN 'TRANSFER BANK'"
                + "		WHEN 'BG' THEN 'CEK GIRO<br>' + MS_TTS.NoBG + '<br><font style=white-space:nowrap>Tgl. BG : ' + CONVERT(VARCHAR,MS_TTS.TglBG,106) + '</font>'"
                + "		WHEN 'UJ' THEN 'UANG JAMINAN'"
                + "		WHEN 'DN' THEN 'DISKON'"
                + " ELSE '' END AS CaraBayar"
                + ",CASE WHEN MS_TTS.Pph = '1' THEN + FORMAT(MS_TTS.Total,'#,###') + '<br>PPH' ELSE FORMAT(MS_TTS.Total,'#,###')  END AS Total"
                + ",MS_TTS.Project"
                + " FROM MS_TTS "
                + " WHERE 1=1 "
                + " AND CONVERT(varchar,TglTTS,112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND CONVERT(varchar,TglTTS,112) <= '" + Cf.Tgl112(Sampai) + "'"
                + Project
                + UserID
                + CaraBayar
                + Tipe
                + Status
                + " ORDER BY NoTTS";

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

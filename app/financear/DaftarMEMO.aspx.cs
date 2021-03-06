using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.FINANCEAR
{
	public partial class DaftarMEMO : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();
            Cf.SetGrid(tb);
			//Js.ConfirmKeyword(this,keyword);

            if (!Page.IsPostBack)
            {
                if (Request.QueryString["status"] == null)
                    metode.SelectedIndex = 0;
                else if (Request.QueryString["status"] == "ok")
                    metode.SelectedIndex = 1;
                else if (Request.QueryString["status"] == "post")
                    metode.SelectedIndex = 2;
                else if (Request.QueryString["status"] == "void")
                    metode.SelectedIndex = 3;
                Act.ProjectList(project);
                if (metode.SelectedIndex != 0) metode.Enabled = false;
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
                addq = " AND a.Status = 'VOID'";

            string nav = "";
            if (Request.QueryString["status"] == "tbDari" || Request.QueryString["status"] == "tbSampai")
            {
                nav = "'<a href=\"javascript:callSource(''' + CONVERT(varchar(50),NoMEMO) + ''' + '''+a.Status+''')\" ' + CASE a.Status WHEN 'VOID' THEN 'style = ''text-decoration:line-through''' ELSE '' END + '>'"
                      + "+ FORMAT(NoMEMO,'0000000#') + '</a><br><i>'+ a.Status + '</i>'";
            }
            else
            {
                nav = "'<a href=\"javascript:call(''' + CONVERT(varchar(50),NoTTS) + ''')\" ' + CASE a.Status WHEN 'VOID' THEN 'style = ''text-decoration:line-through''' ELSE '' END + '>'"
                      + "+ FORMAT(NoMEMO,'000000#') + '</a><br><i>'+ a.Status + '</i>'";
            }


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
                + "		WHEN 'BG' THEN 'CEK GIRO<br>' + a.NoBG + '<br><font style=white-space:nowrap>Tgl. BG : ' + CONVERT(VARCHAR,a.TglBG,106) + '</font>'"
                + "		WHEN 'UJ' THEN 'UANG JAMINAN'"
                + "		WHEN 'DN' THEN 'DISKON'"
                + "		WHEN 'TG' THEN 'TUKAR GULING'"
                + " ELSE '' END AS Tipe"
                + ",CASE WHEN a.Pph = '1' THEN + FORMAT(a.Total,'#,###') + '<br>PPH' ELSE FORMAT(a.Total,'#,###')  END AS Total"
                + ",b.NamaProject AS Project"
                + " FROM MS_MEMO a JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK b ON a.Ref = b.NoKontrak"
                + " WHERE CONVERT(varchar,NoMEMO) + Ref + Unit + Customer + Ket + NoBG "
                + " LIKE '%" + Cf.Str(keyword.Text) + "%'"
                + " AND b.Project = '" + project.SelectedValue + "'"
                + addq
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

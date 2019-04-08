using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.FINANCEAR
{
	public partial class ReminderTTSPosting : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();
            Cf.SetGrid(tb);
			Fill();
            ok.HRef = "Reminder.aspx?project=" + Project;
        }

		private void Fill()
		{
            string nav = "'<a href=\"javascript:call('''+CONVERT(varchar(50),NoTTS)+''')\">' + FORMAT(NoTTS,'000000#') + '</a>' AS TTS";
			string strSql = "SELECT "
                + nav
                + ",CONVERT(VARCHAR,TglTTS,106) + '<br>' + (SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..USERNAME WHERE UserID = MS_TTS.UserID) AS Tgl"
                + ",Tipe + ' No. ' + Ref + '<br>' + Unit + '<br>' + Customer AS Cs"
                + ",Ket + CASE WHEN Titip != '' THEN '<br>Pengelola : ' + Titip ELSE '' END + CASE WHEN Tolak != '' THEN '<br>Tolakan : ' + Tolak ELSE '' END AS Keterangan"
                + ",CASE CaraBayar"
                + "		WHEN 'TN' THEN 'TUNAI'"
                + "		WHEN 'KK' THEN 'KARTU KREDIT'"
                + "		WHEN 'KD' THEN 'KARTU DEBIT'"
                + "		WHEN 'TR' THEN 'TRANSFER BANK'"
                + "		WHEN 'BG' THEN 'CEK GIRO<br>' + NoBG + '<br><font style=white-space:nowrap>Tgl. BG : ' + CONVERT(VARCHAR,TglBG,106) + '</font>'"
                + "		WHEN 'UJ' THEN 'UANG JAMINAN'"
                + "		WHEN 'DN' THEN 'DISKON'"
                + " END AS Bayar"
                + ",FORMAT(Total,'#,###') AS Total"
                + " FROM MS_TTS WHERE "
				+ " Status = 'BARU' AND Project = '" + Project + "'"
                ;

            DataTable rs = Db.Rs(strSql);
            tb.DataSource = rs;
            tb.DataBind();
            if (tb.PageCount == 0) kosong.InnerText = "Reminder untuk topik diatas masih kosong.";
		}
        private string Project
        {
            get
            {
                return Cf.Pk(Request.QueryString["project"]);
            }
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

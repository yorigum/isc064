using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.FINANCEAR
{
	public partial class ReminderBGJatuhTempo : System.Web.UI.Page
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
            string nav2 = ",'<a show-modal=#ModalPopUp modal-title=Edit TTS modal-url=TTSEdit.aspx?NoTTS='''+CONVERT(varchar(50),NoTTS)+''' >' + FORMAT(NoTTS,'000000#') + '</a><br><i>' + Status + '</i><br>' + CONVERT(VARCHAR,TglTTS,106) AS TTS";
            string strSql = "SELECT "
                + "'<b>' + NoBG + '</b><br><font style=white-space:nowrap>Tgl. BG : ' + CONVERT(VARCHAR,TglBG,106) + '</font>' AS BG"
                + nav2
                + ",Tipe + ' No. ' + Ref + '<br>' + Unit + '<br>' + Customer AS Cs"
                + ",Ket + CASE WHEN Titip != '' THEN '<br>Pengelola : ' + Titip ELSE '' END + CASE WHEN Tolak != '' THEN '<br>Tolakan : ' + Tolak ELSE '' END AS Ket"
                + ",FORMAT(Total,'#,###') AS Nilai"
                + " FROM MS_TTS"
				+ " WHERE CaraBayar = 'BG' AND StatusBG = 'OK' AND Status = 'BARU' "
				+ " AND CONVERT(varchar, TglJTBG, 112) <= '"+Cf.Tgl112(DateTime.Today)+ "' AND Project = '" + Project + "'"
                + " ORDER BY TglBG, NoBG";

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

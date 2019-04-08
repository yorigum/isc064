using System;
using System.Drawing;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.FINANCEAR
{
    public partial class PrintBKMBatch1 : System.Web.UI.Page
    {
        protected string NoTTS
        {
            get
            {

                string query = "";
                for (int i = 0; i < TTS.GetUpperBound(0); i++)
                {
                    if (query != "")
                        query += ",";

                    query += "'" + TTS[i] + "'";
                }

                return query;
            }
        }
        protected string[] TTS
        {
            get
            {
                return Request.QueryString["id"].Split(';');
            }
        }
        protected string Project
        {
            get
            {
                return Request.QueryString["project"];
            }
        }
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Fill();
                Js.AutoPrint(this);
            }
        }

        private void Fill()
        {
            for (int i = 0; i < TTS.GetUpperBound(0); i++)
            {
                SetTemplate(TTS[i],Project);
            }
        }

        private void SetTemplate(string id,string project)
        {
            //increment
            Db.Execute("UPDATE MS_TTS SET PrintBKM = PrintBKM + 1 WHERE NoTTS = " + id);

            //Logfile
            DataTable rs = Db.Rs("SELECT "
                + " CONVERT(varchar, TglTTS, 106) AS [Tanggal]"
                + ",Tipe"
                + ",Ref AS [Ref.]"
                + ",Unit"
                + ",Customer"
                + ",CaraBayar AS [Cara Bayar]"
                + ",Ket AS [Keterangan]"
                + ",Total"
                + ",NoBG AS [No. BG]"
                + ",CONVERT(varchar, TglBG, 106) AS [Tanggal BG]"
                + ",NoBKM AS [No. BKM]"
                + ",CONVERT(varchar, TglBKM, 106) AS [Tanggal BKM]"
                + " FROM MS_TTS WHERE NoTTS = " + id);

            Db.Execute("EXEC spLogTTS"
                + " 'P-BKM'"
                + ",'" + Act.UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'" + Cf.LogCapture(rs) + "'"
                + ",'" + id.ToString().PadLeft(7, '0') + "'"
                );

            decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_TTS_LOG ORDER BY LogID DESC");            
            Db.Execute("UPDATE MS_TTS_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

            PrintBKMTemplate uc = (PrintBKMTemplate)Page.LoadControl("PrintBKMTemplate.ascx");
            uc.NoTTS = id;
            uc.Project = project;
            list.Controls.Add(uc);
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
    }
}

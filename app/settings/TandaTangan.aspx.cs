using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.SETTINGS
{
    public partial class TandaTangan : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                FillTable();
            }
        }

        private void FillTable()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            DataTable rs = Db.Rs("SELECT DISTINCT(Dokumen) FROM REF_SIGN ORDER BY Dokumen");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                sb.Append("<li>"
                    + "<a show-modal='#ModalPopUp' modal-title='Edit Tanda Tangan' modal-url='TandaTanganEdit.aspx?Dokumen="+ rs.Rows[i]["Dokumen"] + "'>"
                    + rs.Rows[i]["Dokumen"]
                    + "</a>"
                    + "</li>"
                    );
            }

            aktif.InnerHtml = sb.ToString();
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
        protected void dokumen_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LAUNCHING
{
    public partial class HitungNUP : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Fill();
            }
        }

        private void Fill()
        {
            int sudahaktivasi = Db.SingleInteger("SELECT COUNT(*) FROM MS_NUP WHERE Status = 1");
            int sudahpilihunit = Db.SingleInteger("SELECT COUNT(*) FROM MS_NUP WHERE Status = 3");
            int sudahclosing = Db.SingleInteger("SELECT COUNT(*) FROM MS_NUP WHERE Status = 4");

            aktivasi.Text = sudahaktivasi.ToString();
            pilih.Text = sudahpilihunit.ToString();
            closing.Text = sudahclosing.ToString();
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

using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace ISC064.LEGAL
{
    public partial class PrintPPJBTemplate1 : System.Web.UI.UserControl
    {

        //Passing parameter
        public string nomor;
        public string proj;
        public int index = 0, index2 = 0;
        private string Halaman { get { return "PPJB"; } }
        public string NoKontrak
        {
            set { nomor = value; }
        }
        public string Project
        {
            set { proj = value; }
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Fill();
        }

        private void Fill()
        {
            DataTable rsKontrak = Db.Rs("SELECT NoKontrak FROM MS_KONTRAK WHERE NoKontrak = '" + nomor + "'");
            if (rsKontrak.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");

            string Html = He.Template(Halaman, nomor, proj);

            content.InnerHtml = Html;
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
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion
    }
}

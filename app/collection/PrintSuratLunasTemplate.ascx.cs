using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Text;

namespace ISC064.COLLECTION
{
    public partial class PrintSuratLunasTemplate : System.Web.UI.UserControl
    {

        //Passing parameter
        public string nomor;
        public string proj;
        public int index = 0, index2 = 0;
        public string NoSKL
        {
            set { nomor = value; }
        }
        public string Project
        {
            set { proj = value; }
        }
        public string Halaman { get { return "SuratKeteranganLunas"; } }
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Fill();
        }

        private void Fill()
        {
            content.InnerHtml = He.Template(Halaman, nomor, proj);
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

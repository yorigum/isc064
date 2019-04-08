namespace ISC064.COLLECTION
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Web.UI.WebControls;
    using System.Text;

    public partial class PrintPJTTemplate : System.Web.UI.UserControl
    {

        //Passing parameter
        public string nomor;
        public string pro;
        public string NoPJT
        {
            set { nomor = value; }
        }
        public string Project
        {
            set { pro = value; }
        }
        private string Halaman { get { return "Invoice"; } }
        protected void Page_Load(object sender, System.EventArgs e)
        {
            content.InnerHtml = He.Template(Halaman, nomor,pro);
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

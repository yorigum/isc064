namespace ISC064.MARKETINGJUAL {
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Web.UI.WebControls;

    public partial class PrintSkemaTemplate : System.Web.UI.UserControl
    {
        protected System.Web.UI.WebControls.Label answer;

        //Passing parameter
        public string nomor;
        public string NoTunggakan
        {
            set { nomor = value; }
        }
        private string Halaman { get { return "SuratPeringatan"; } }
        protected void Page_Load(object sender, System.EventArgs e)
        {
            content.InnerHtml = He.Template(Halaman, nomor, "");
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

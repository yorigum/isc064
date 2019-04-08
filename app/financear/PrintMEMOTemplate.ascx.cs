namespace ISC064.FINANCEAR
{
	using System;
	using System.Data;
	using System.Data.SqlClient;
	using System.Web.UI.WebControls;

    public partial class PrintMEMOTemplate : System.Web.UI.UserControl
    {
        protected System.Web.UI.WebControls.Label lblblok;
        protected System.Web.UI.WebControls.Label lblunitno;
        protected System.Web.UI.WebControls.Label lblhrg;
        protected System.Web.UI.WebControls.Label cash;
        protected System.Web.UI.WebControls.Label ccard;
        protected System.Web.UI.WebControls.Label noccard;
        protected System.Web.UI.WebControls.Label cekgiro;
        protected System.Web.UI.WebControls.Label nocekgiro;
        protected System.Web.UI.WebControls.Label bankcekgiro;
        protected System.Web.UI.WebControls.Label transfer;
        protected System.Web.UI.WebControls.Label norek;
        protected System.Web.UI.WebControls.Label bankrek;
        protected System.Web.UI.WebControls.Label tgltr;
        protected System.Web.UI.WebControls.Label tglsrt;
        protected System.Web.UI.WebControls.Label jenis;

        //Passing parameter
        public string nomor;
        public string pro;
        string TipeUnit;
        string Lantai;
        string NoUnit;
        string Luas;

        public string NoMEMO
        {
            set { nomor = value; }
        }
        public string Project
        {
            set { pro = value; }
        }
        private string Halaman { get { return "Memo"; } }
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

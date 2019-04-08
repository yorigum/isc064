namespace ISC064.MARKETINGJUAL
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Web.UI.WebControls;

    public partial class PrintBFormTemplate : System.Web.UI.UserControl
    {


        //Passing parameter
        public string nomor;
        public string proj;
        public string NoReservasi
        {
            set { nomor = value; }
        }
        public string Project
        {
            set { proj = value; }
        }
        private string Halaman { get { return "BookingForm"; } }
        protected void Page_Load(object sender, System.EventArgs e)
        {
            content.InnerHtml = He.Template(Halaman, nomor, proj);
        }
    }
}
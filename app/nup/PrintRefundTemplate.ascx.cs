namespace ISC064.NUP
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Web.UI.WebControls;

    public partial class PrintRefundTemplate : System.Web.UI.UserControl
    {

        //Passing parameter
        public string nomor;
        public string Tipe;
        public string NoNUP
        {
            set { nomor = value; }
        }
        public string Tipeee
        {
            set { Tipe = value; }
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Fill();
        }

        private void Fill()
        {
            string strSql = "SELECT * FROM MS_NUP WHERE NoNUP = '" + nomor + "' AND Tipe = '" + Tipe + "'";

            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count != 0)
            {
                DateTime Tgl = Db.SingleTime("Select TglBayar From MS_NUP_PELUNASAN Where NoNUP = '" + nomor + "' AND Tipe = '" + Tipe + "'");
                string RB = Db.SingleString("Select RekBank From MS_NUP_PELUNASAN Where NoNUP = '" + nomor + "' AND Tipe = '" + Tipe + "'");

                no.Text = nup.Text = nomor;
                tglp.Text = Cf.DayIndo(rs.Rows[0]["TglDaftar"]);
                nama.Text = Cf.Str(rs.Rows[0]["NamaBfr"]);
                tglbf.Text = Cf.Day(Tgl);//Cf.DayIndo(rs.Rows[0]["TglBayar"]);
                jumlah.Text = "Rp. 1.000.000,00";//Cf.Num(rs.Rows[0]["NilaiBayar"]);
                string strSql2 = "SELECT * FROM MS_CUSTOMER WHERE NoCustomer = '" + rs.Rows[0]["NoCustomer"].ToString() + "'";

                DataTable rs2 = Db.Rs(strSql2);
                norek.Text = Cf.Str(rs2.Rows[0]["RekNo"]);
                narek.Text = Cf.Str(rs2.Rows[0]["RekNama"]);
                bank.Text = Cf.Str(rs2.Rows[0]["RekBank"]);
                tglnow.Text = Cf.Day(DateTime.Today);
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
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion
    }
}

namespace ISC064.NUP
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Web.UI.WebControls;
    using System.Text.RegularExpressions;

    public partial class PrintNUPTemplate : System.Web.UI.UserControl
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

        private bool validPrint()
        {
            bool x = true;

            DataTable dtNUPCS = Db.Rs("SELECT A.*,B.Nama,B.NoKTP,B.NoHP,B.NoTelp,B.RekNo,B.RekNama,B.RekBank,B.RekCabang"
                + " FROM MS_NUP A INNER JOIN MS_CUSTOMER B ON A.NoCustomer=B.NoCustomer WHERE a.NoNUP='" + nomor + "' AND a.Tipe = '" + Tipe + "'");
            
            if (dtNUPCS.Rows[0]["NoAgent"] == "0"
                || dtNUPCS.Rows[0]["NoKTP"] == ""
                || dtNUPCS.Rows[0]["NoHP"] == ""
                || dtNUPCS.Rows[0]["NoTelp"] == ""
                || dtNUPCS.Rows[0]["RekNo"] == ""
                || dtNUPCS.Rows[0]["RekNama"] == ""
                || dtNUPCS.Rows[0]["RekBank"] == ""
                || dtNUPCS.Rows[0]["RekCabang"] == ""
                )
                x = false;

            return x;
        }

        private void Fill()
        {
            if (validPrint())
            {
                divPrint.Visible = true;
                divInfo.Visible = false;

                string strSql = "SELECT * FROM MS_NUP INNER JOIN MS_CUSTOMER ON MS_NUP.NoCustomer = MS_CUSTOMER.NoCustomer WHERE MS_NUP.NoNUP= '" + nomor + "' AND MS_NUP.Tipe = '" + Tipe + "'";
                DataTable rs = Db.Rs(strSql);
                if (rs.Rows.Count != 0)
                {
                    DataTable dtAgent = Db.Rs("SELECT * FROM MS_AGENT WHERE NoAgent=" + Convert.ToInt32(rs.Rows[0]["NoAgent"]));
                    
                    string rev = "";
                    if (Convert.ToInt32(rs.Rows[0]["Revisi"].ToString()) > 0)
                        rev = "R";

                    nonup.Text = rs.Rows[0]["NoNUP"].ToString() + rev;
                    tgl.Text = Cf.Day(rs.Rows[0]["TglDaftar"].ToString());
                    nilainup.Text = "Rp " + Cf.Num(Db.SingleDecimal("SELECT ISNULL(SUM(NilaiBayar),0) FROM MS_NUP_PELUNASAN WHERE NoNUP = '" + nomor + "' AND Tipe = '" + Tipe + "'"));
                    nama.Text = cus.Text = rs.Rows[0]["Nama"].ToString();
                    alamat.Text = rs.Rows[0]["Alamat1"].ToString() + "<br />" + rs.Rows[0]["Alamat2"].ToString() + "<br />" + rs.Rows[0]["Alamat3"].ToString() + "<br />" + rs.Rows[0]["Alamat4"].ToString();
                    noktp.Text = rs.Rows[0]["NoKTP"].ToString();
                    alamatktp.Text = rs.Rows[0]["KTP1"].ToString() + "<br />" + rs.Rows[0]["KTP2"].ToString() + "<br />" + rs.Rows[0]["KTP3"].ToString() + "<br />" + rs.Rows[0]["KTP4"].ToString();
                    telp.Text = rs.Rows[0]["NoTelp"].ToString() + " / " + rs.Rows[0]["NoHP"].ToString();
                    jenispilihan1.Text = rs.Rows[0]["Tipe"].ToString();
                    tiperumah.Text = rs.Rows[0]["Tipe"].ToString();
                    namarek.Text = rs.Rows[0]["RekNama"].ToString();
                    bank.Text = rs.Rows[0]["RekBank"].ToString();
                    cabang.Text = rs.Rows[0]["RekCabang"].ToString();
                    namarek.Text = rs.Rows[0]["RekNama"].ToString();
                    norek.Text = rs.Rows[0]["RekNo"].ToString();
                    agent.Text = agent2.Text = dtAgent.Rows[0]["Nama"].ToString();
                    telpagent.Text = dtAgent.Rows[0]["Kontak"].ToString();

                    //increment
                    Db.Execute("UPDATE MS_NUP SET PrintNUP = PrintNUP + 1, TglPrintNUP = '" + Cf.Tgl112(DateTime.Now) + "' WHERE NoNUP = '" + nomor + "' AND Tipe = '" + Tipe + "'");
                }
            }
            else
            {
                divPrint.Visible = false;
                divInfo.Visible = true;
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

namespace ISC064.LAUNCHING
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
        public string NoNUP
        {
            set { nomor = value; }
        }
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Fill();
        }

        private bool validPrint()
        {
            bool x = true;

            //DataTable dtNUPCS = Db.Rs("SELECT A.*,B.Nama,B.NoKTP,B.NoHP,B.NoTelp,B.RekNo,B.RekNama,B.RekBank,B.RekCabang"
            //    + " FROM MS_NUP A INNER JOIN MS_CUSTOMER B ON A.NoCustomer=B.NoCustomer WHERE NoNUP='" + nomor + "'");

            //if (dtNUPCS.Rows[0]["NoAgent"] == "0"
            //    || dtNUPCS.Rows[0]["NoKTP"] == ""
            //    || dtNUPCS.Rows[0]["NoHP"] == ""
            //    || dtNUPCS.Rows[0]["NoTelp"] == ""
            //    || dtNUPCS.Rows[0]["RekNo"] == ""
            //    || dtNUPCS.Rows[0]["RekNama"] == ""
            //    || dtNUPCS.Rows[0]["RekBank"] == ""
            //    || dtNUPCS.Rows[0]["RekCabang"] == ""
            //    )
            //x = false;

            return x;
        }

        private void Fill()
        {
            if (validPrint())
            {
                divPrint.Visible = true;
                divInfo.Visible = false;

                string strSql = "SELECT * FROM MS_NUP WHERE MS_NUP.NoNUP= '" + nomor + "'";
                DataTable rs = Db.Rs(strSql);
                if (rs.Rows.Count != 0)
                {
                    decimal bayaran = 0;
                    string strSql2 = "SELECT * FROM MS_NUP_PELUNASAN WHERE NoNUP= '" + nomor + "'";
                    DataTable rs2 = Db.Rs(strSql2);
                    if (rs2.Rows.Count != 0)
                    {
                        bayaran = Convert.ToDecimal(rs2.Rows[0]["Total"]);

                        switch (rs2.Rows[0]["CaraBayar"].ToString())
                        {
                            case "TR":
                                cabarTrf.Text = "X";
                                cabarTunai.Text = cabarCC.Text = cabarLainnya.Text = "";
                                break;
                            case "TN":
                                cabarTunai.Text = "X";
                                cabarCC.Text =  cabarTrf.Text = cabarLainnya.Text = "";
                                break;
                            case "KK":
                                cabarCC.Text = "X";
                                cabarTunai.Text = cabarTrf.Text = cabarLainnya.Text = "";
                                break;
                            default:
                                cabarLainnya.Text = "X";
                                cabarTunai.Text = cabarCC.Text = cabarTrf.Text = "";
                                break;
                        }
                    }
                    QRImage.Src = "QR\\" + nomor + ".jpg";
                    QRImage.Width = 100;
                    QRImage.Height = 100;
                    QRImage.Attributes["style"] = "left:243";
                    nbayar.Text = Cf.Num(bayaran) + "&nbsp;";
                    nbayar.Attributes["style"] = "border-bottom:1px solid black";
                    if (bayaran != 0)
                        nterbilang.Text = Money.Str(bayaran) + " RUPIAH";
                    nterbilang.Attributes["style"] = "border-bottom:1px solid black";

                    pemesan.Text = pemesan2.Text = rs.Rows[0]["NamaCustomer"].ToString();
                    noktp.Text = rs.Rows[0]["NoKTP"].ToString();
                    npwp.Text = rs.Rows[0]["NoNPWP"].ToString();
                    email.Text = rs.Rows[0]["Email"].ToString();
                    korespon1.Text = rs.Rows[0]["Alamat1"].ToString() + " " + rs.Rows[0]["Alamat2"].ToString();
                    korespon2.Text = rs.Rows[0]["Alamat3"].ToString() + " " + rs.Rows[0]["Alamat4"].ToString();
                    notelp.Text = rs.Rows[0]["NoTelp"].ToString();
                    nohp.Text = rs.Rows[0]["NoHP"].ToString();
                    bank.Text = rs.Rows[0]["RekBank"].ToString();
                    norek.Text = rs.Rows[0]["RekNomor"].ToString();
                    sales.Text = rs.Rows[0]["NamaAgent"].ToString();
                    if (rs.Rows[0]["TglNUP"] is DBNull)
                        tglNUP.Text = "";
                    else
                        tglNUP.Text = Cf.Tgl113(Convert.ToDateTime(rs.Rows[0]["TglNUP"]));

                    string refEv = Db.SingleString("SELECT Event FROM REF_EVENT WHERE SN = " + rs.Rows[0]["SumberData"].ToString());
                    string nNup = refEv + "-" + rs.Rows[0]["NoNUP"].ToString();
                    nonup.Text = nNup;

                    Db.Execute("UPDATE MS_NUP SET PrintNUP = PrintNUP + 1, TglPrint = '" + Cf.Tgl112(DateTime.Now) + "' WHERE NoNUP = '" + nomor + "'");
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

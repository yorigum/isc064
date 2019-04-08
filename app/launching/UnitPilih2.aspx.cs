using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
namespace ISC064.LAUNCHING
{
    public partial class UnitPilih2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            string strSql = "SELECT A.NoNUP,D.Nama, C.NoUnit, C.Nomor, C.NamaJalan, C.LuasSG,C.LuasNett,A.Tipe,A.NomorSkema,A.Harga,C.Jenis FROM MS_NUP_PRIORITY A"
                + " INNER JOIN MS_NUP B ON A.NoNUP = B.NoNUP AND A.Tipe=B.Tipe"
                + " INNER JOIN MS_UNIT C ON A.NoStock = C.NoStock"
                + " INNER JOIN MS_CUSTOMER D ON B.NoCustomer = D.NoCustomer"
                + " WHERE A.NoNUP='" + NoNUP + "' AND  a.Tipe ='" + Tipe + "'";

            
            DataTable rs = Db.Rs(strSql);
            nounit.Text = rs.Rows[0]["NoUnit"].ToString();
            priority.Text = NoNUP;

            jalan.Text = rs.Rows[0]["NamaJalan"].ToString();
            nomorunit.Text = rs.Rows[0]["Nomor"].ToString();
            namacust.Text = rs.Rows[0]["Nama"].ToString();

            luast.Text = Cf.Num(rs.Rows[0]["LuasSG"]);
            luasb.Text = Cf.Num(rs.Rows[0]["LuasNett"]);
            CB.Text = Db.SingleString("SELECT Nama FROM REF_SKEMA WHERE Nomor = " + rs.Rows[0]["NomorSkema"]);
            PL.Text = Cf.Num(rs.Rows[0]["Harga"]);
            tepe.Text = rs.Rows[0]["Jenis"].ToString();
        }
        private string NoStock
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoStock"]);
            }
        }
        private string NoNUP
        {
            get
            {
                return Cf.Pk(Request.QueryString["No"]);
            }
        }

        private string Tipe
        {
            get
            {
                return Cf.Pk(Request.QueryString["Tipe"]);
            }
        }

        protected void closingcancel_Click(object sender, EventArgs e)
        {
            Db.Execute("UPDATE MS_UNIT SET Status = 'A' WHERE NoStock = '" + NoStock + "'");
            decimal NilaiKontrak = 0;
            decimal NilaiDiskon = 0;

            Db.Execute("UPDATE MS_NUP_PRIORITY SET NoStock ='" + NoStock + "', NomorSkema= '' WHERE NoNUP='" + NoNUP + "' AND TIpe ='" + Tipe + "'");

            DataTable rs = Db.Rs("SELECT "
                    + " NoNUP AS [No. NUP],"
                    + " (SELECT NoUnit FROM MS_UNIT WHERE NoStock =  MS_NUP_PRIORITY.NoStock) AS [No Unit],"
                    + " (Select Nama FROM REF_SKEMA WHERE Nomor = MS_NUP_PRIORITY.NomorSkema) AS [Cara Bayar]"
                    + " FROM MS_NUP_PRIORITY "
                    + " WHERE NoNUP = '" + NoNUP + "' AND Tipe ='" + Tipe + "'"
                    );


            Db.Execute("EXEC spLogNUP"
                + " 'P-Unit'"
                + ",'" + Act.UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'" + Cf.LogCapture(rs) + "'"
                + ",'" + NoNUP + "'"
                );
            Response.Redirect("ClosingNUP2.aspx?No=" + NoNUP + "&Tipe="+Tipe);
        }
    }
}
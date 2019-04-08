using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
namespace ISC064.LAUNCHING
{
    public partial class UnitPilihPrint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Js.AutoPrint(this);

            string strSql = "SELECT * FROM MS_PRIORITY A INNER JOIN MS_UNIT B ON A.NoStock = B.NoStock WHERE NoNUP = '" + NoNUP + "'";
            DataTable rs = Db.Rs(strSql);
            nounit.Text = rs.Rows[0]["NoUnit"].ToString();
            priority.Text = NoNUP;
            nomorunit.Text = rs.Rows[0]["NoUnit"].ToString();
            namacust.Text = Db.SingleString("SELECT Nama FROM MS_CUSTOMER WHERE NoNUP ='" + NoNUP + "'");
            tipe.Text = Db.SingleString("SELECT Nama FROM REF_JENIS WHERE Jenis = '" + rs.Rows[0]["Jenis"].ToString() + "'");
            luas.Text = Cf.Num(rs.Rows[0]["luas"]);
            harga.Text = Cf.Num(rs.Rows[0]["NilaiKontrak"]);


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
                return Cf.Pk(Request.QueryString["NoNUP"]);
            }
        }

}
}
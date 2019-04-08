using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.NUP
{
    public partial class NUPBatalPilih : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                //Bind(); //tanggal dan bulan
                Fill();
            }

            FeedBack();
        }
        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "Edit Berhasil...";
            }
        }

        private void Bind()
        {

        }

        private void Fill()
        {
            string strSql = "SELECT * FROM MS_UNIT WHERE NoStock = '" + NoStock + "'";
            DataTable rs = Db.Rs(strSql);

            priority.Text = NoPriority;
            nomorunit.Text = rs.Rows[0]["NoUnit"].ToString();
            namacust.Text = Db.SingleString("SELECT Nama FROM MS_NUP INNER JOIN MS_CUSTOMER ON MS_NUP.NoCustomer=MS_CUSTOMER.NoCustomer WHERE NoNUP ='" + NoPriority + "'");
            tipe.Text = rs.Rows[0]["Tipe"].ToString();
            luas.Text = Cf.Num(rs.Rows[0]["LuasSG"]);
            harga.Text = Cf.NumBulat(rs.Rows[0]["Pricelist"]);
            unitpilih.Text = rs.Rows[0]["NoUnit"].ToString();
        }

        private bool valid()
        {
            string s = "";

            bool x = true;


            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Unit Properti tidak boleh kosong.\\n"
                    + "2. Unit sudah dipilih.\\n"
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );

            return x;
        }

        private string NoStock
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoStock"]);
            }
        }

        private string NoPriority
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoNUP"]);
            }
        }
    }
}
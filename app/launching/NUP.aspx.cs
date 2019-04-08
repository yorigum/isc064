using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LAUNCHING
{
    public partial class Nup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {

            }


        }

        private bool valid()
        {
            bool x = true;

            //int c2 = Db.SingleInteger(
            //    "SELECT COUNT(*) FROM MS_CUSTOMER WHERE NoNUP = '" + NoNUP + "'");

            //int c = Db.SingleInteger(
            //    "SELECT COUNT(*) FROM MS_PRIORITY WHERE NoNUP = '" + NoNUP + "'");

            //if (c != 0 || c2 == 0)
            //    x = false;

            if (!x)
                Js.Alert(
                    this
                    , "NUP Tidak Valid.\\n\\n"
                    + "Kemungkinan Sebab :\\n"
                    + "1. NUP tersebut tidak terdaftar.\\n"
                    + "2. NUP tersebut sudah memilih unit.\\n"
                    , "document.getElementById('nonup').focus();"
                    + "document.getElementById('nonup').select();"
                    );

            return x;
        }

    }
}
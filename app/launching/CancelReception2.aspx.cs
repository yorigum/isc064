using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LAUNCHING
{
    public partial class CancelReception2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //FeedBack();
            Act.Pass();
            Act.NoCache();
            if (!Page.IsPostBack)
            {
                fillData();
            }

            Js.Confirm(this, "Apakah Anda yakin?");
        }

        private void fillData()
        {
            string strSql = "SELECT * FROM MS_NUP a INNER JOIN MS_CUSTOMER b ON a.NoCustomer = b.NoCustomer WHERE a.NoNUP = '" + NoNUP + "' AND a.Tipe = '" + Tipe + "'";
            DataTable rsNUP = Db.Rs(strSql);

            if (rsNUP.Rows.Count > 0)
            {
                nomorNUP.Text = Cf.Str(rsNUP.Rows[0]["NoNUP"]);
                if (rsNUP.Rows[0]["TglDaftar"] is DBNull)
                    Save.Enabled = false;
                else
                    tglNUP.Text = Cf.Day(Convert.ToDateTime(rsNUP.Rows[0]["TglDaftar"]));

                nama.Text = Cf.Str(rsNUP.Rows[0]["Nama"]);

                ctelp.Text = Cf.Str(rsNUP.Rows[0]["NoTelp"]);
                chp.Text = Cf.Str(rsNUP.Rows[0]["NoHP"]);
                noktp.Text = Cf.Str(rsNUP.Rows[0]["NoKTP"]);



            }
        }

        private bool isUnique()
        {
            int c = Db.SingleInteger(
                "SELECT COUNT(NoNUP) FROM MS_NUP WHERE NoNUP = '" + NoNUP + "' AND Tipe = '" + Tipe + "'");

            if (c == 0)
                return true;
            else
                return false;
        }
        protected void save_Click(object sender, System.EventArgs e)
        {
            Db.Execute("UPDATE MS_NUP SET Status=0,TglAktivasi=null WHERE NoNUP = '" + NoNUP + "' AND Tipe = '" + Tipe + "'");

            Response.Redirect("RegisReception.aspx");
        }

        //private string NoCustomer
        //{
        //    get
        //    {
        //        return Cf.Pk(nocustomer.Text);
        //    }
        //}
        private string NoNUP
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoNUP"]);
            }
        }
        private string Tipe
        {
            get
            {
                return Cf.Pk(Request.QueryString["Tipe"]);
            }
        }
    }
}

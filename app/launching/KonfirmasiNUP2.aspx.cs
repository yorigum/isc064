using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LAUNCHING
{
    public partial class KonfirmasiNUP2 : System.Web.UI.Page
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

        //private void FeedBack()
        //{
        //    feed.Text = "";
        //    if (!Page.IsPostBack)
        //    {
        //        if (Request.QueryString["done"] != null)
        //            feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
        //                + "<a href=\"javascript:popEditCustomer('" + Request.QueryString["done"] + "')\">"
        //                + "Registrasi Berhasil..."
        //                + "</a>";
        //    }
        //}

        private void fillData()
        {
            string strSql = "SELECT * FROM MS_NUP a INNER JOIN MS_CUSTOMER b ON a.NoCustomer = b.NoCustomer WHERE NoNUP = '" + NoNUP + "'";
            DataTable rsNUP = Db.Rs(strSql);

            if (rsNUP.Rows.Count > 0)
            {
                nomorNUP.Text = Cf.Str(rsNUP.Rows[0]["NoNUP"]);
                if (rsNUP.Rows[0]["TglDaftar"] is DBNull)
                    Save.Enabled = false;
                else
                    tglNUP.Text = Cf.Day(Convert.ToDateTime(rsNUP.Rows[0]["TglDaftar"]));

                //sumberdata.Text = Cf.Str(rsNUP.Rows[0]["SumberData"]) + " - " + Db.SingleString("SELECT Nama FROM REF_EVENT WHERE Event = '" + rsNUP.Rows[0]["SumberData"] + "'");
                nama.Text = Cf.Str(rsNUP.Rows[0]["Nama"]);
                noktp.Text = Cf.Str(rsNUP.Rows[0]["NoKTP"]);                
                ctelp.Text = Cf.Str(rsNUP.Rows[0]["NoTelp"]);
                chp.Text = Cf.Str(rsNUP.Rows[0]["NoHP"]);
                
                string a = "";
                DataTable bind = Db.Rs("SELECT * FROM MS_NUP WHERE NoNUPHeader = '" + NoNUP + "'");
                for (int i = 0; i < bind.Rows.Count; i++)
                {
                    a += bind.Rows[i]["NoNUP"].ToString() + "<br/>";
                }
                nuplain.Text = a;
            }
        }

        private bool isUnique()
        {
            int c = Db.SingleInteger(
                "SELECT COUNT(NoNUP) FROM MS_NUP WHERE NoNUP = '" + NoNUP + "'");

            if (c == 0)
                return true;
            else
                return false;
        }
        protected void save_Click(object sender, System.EventArgs e)
        {
            DataTable bind = Db.Rs("SELECT * FROM MS_NUP WHERE NoNUPHeader = '" + NoNUP + "'");
            for (int i = 0; i < bind.Rows.Count; i++)
            {
                Db.Execute("UPDATE MS_NUP SET FlagStatus = 3 WHERE NoNUP = '" + bind.Rows[i]["NoNUP"] + "'");

                Db.Execute("EXEC spLogNUP"
                    + " 'EDIT'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'KONFIRMASI NUP'"
                    + ",'" + bind.Rows[i]["NoNUP"] + "'"
                    + ",'" + bind.Rows[i]["Tipe"].ToString() + "'"
                    );

                //Db.Execute("INSERT INTO MS_NUP_PRIORITY(NoNUP) VALUES('" + NoNUP + "')");
            }
            Response.Redirect("ClosingNUP2.aspx?No=" + NoNUP);
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
    }
}

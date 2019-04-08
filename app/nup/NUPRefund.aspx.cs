using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.NUP
{
    public partial class NUPRefund : System.Web.UI.Page
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

            Js.Confirm(this, "Apakah Anda yakin untuk merefund NUP ini?");
        }

        private void fillData()
        {
            string strSql = "SELECT * FROM MS_NUP a INNER JOIN MS_CUSTOMER b ON a.NoCustomer = b.NoCustomer WHERE a.NoNUP = '" + NoNUP + "' AND a.Tipe = '" + Tipe + "' AND a.Project = '" + Project + "' ";
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

                //Rekening Refund
                bank.Text = Cf.Str(rsNUP.Rows[0]["RekBank"]);
                cabang.Text = Cf.Str(rsNUP.Rows[0]["RekCabang"]);
                norek.Text = Cf.Str(rsNUP.Rows[0]["RekNo"]);
                ats.Text = Cf.Str(rsNUP.Rows[0]["RekNama"]);
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
            int NoCust = Db.SingleInteger("SELECT NoCustomer FROM MS_NUP WHERE NoNUP = '" + NoNUP + "' AND Tipe = '" + Tipe + "' AND Project='" + Project + "'");

            DataTable rsBefNUP = Db.Rs("SELECT "
                + " MS_NUP.NoNUP AS [NUP]"
                + ",MS_AGENT.Nama AS [Nama Sales]"
                + ",MS_CUSTOMER.Nama AS [Nama Customer]"
                + ",MS_CUSTOMER.RekBank AS [Bank Refund]"
                + ",MS_CUSTOMER.RekCabang AS [Cabang Bank Refund]"
                + ",MS_CUSTOMER.RekNo AS [No. Rek Refund]"
                + ",MS_CUSTOMER.RekNama AS [Atas Nama Rek Refund]"
                + " FROM MS_NUP INNER JOIN MS_AGENT ON MS_NUP.NoAgent = MS_AGENT.NoAgent"
                + " INNER JOIN MS_CUSTOMER ON MS_NUP.NoCustomer = MS_CUSTOMER.NoCustomer"
                + " WHERE MS_NUP.NoNUP='" + NoNUP + "' AND MS_NUP.Tipe = '" + Tipe + "'"
                );

            Db.Execute("UPDATE MS_NUP SET"
                + " Status=5"
                + ",TglAktivasi=getdate()"
                + " WHERE NoNUP = '" + NoNUP + "' AND Tipe = '" + Tipe + "' AND Project = '" + Project + "'");

            Db.Execute("UPDATE MS_CUSTOMER SET"
                + " RekBank = '" + bank.Text + "'"
                + ",RekCabang = '" + cabang.Text + "'"
                + ",RekNo = '" + norek.Text + "'"
                + ",RekNama = '" + ats.Text + "'"
                + " WHERE NoNUP = '" + NoNUP + "' AND NoCustomer = '" + NoCust + "'");

            DataTable rsAftNUP = Db.Rs("SELECT "
                + " MS_NUP.NoNUP AS [NUP]"
                + ",MS_AGENT.Nama AS [Nama Sales]"
                + ",MS_CUSTOMER.Nama AS [Nama Customer]"
                + ",MS_CUSTOMER.RekBank AS [Bank Refund]"
                + ",MS_CUSTOMER.RekCabang AS [Cabang Bank Refund]"
                + ",MS_CUSTOMER.RekNo AS [No. Rek Refund]"
                + ",MS_CUSTOMER.RekNama AS [Atas Nama Rek Refund]"
                + " FROM MS_NUP INNER JOIN MS_AGENT ON MS_NUP.NoAgent = MS_AGENT.NoAgent"
                + " INNER JOIN MS_CUSTOMER ON MS_NUP.NoCustomer = MS_CUSTOMER.NoCustomer"
                + " WHERE MS_NUP.NoNUP='" + NoNUP + "' AND MS_NUP.Tipe = '" + Tipe + "' AND MS_NUP.Project = '" + Project + "'"
                );

            //Logfile
            DataTable rsLog = Db.Rs("SELECT "
                + " MS_NUP.NoNUP AS [NUP]"
                + ",MS_CUSTOMER.Nama AS [Customer]"
                + ",MS_CUSTOMER.Nama AS [Sales/Agent]"
                + ",MS_NUP.UserInputNama AS [Diinput Oleh]"
                + ",MS_NUP.Tipe AS [Tipe]"
                + " FROM MS_NUP INNER JOIN MS_CUSTOMER"
                + " ON MS_NUP.NoCustomer = MS_CUSTOMER.NoCustomer"
                + " INNER JOIN MS_AGENT ON MS_NUP.NoAgent = MS_AGENT.NoAgent"
                + " WHERE MS_NUP.NoNUP = '" + NoNUP + "' AND MS_NUP.Tipe = '" + Tipe + "' AND MS_NUP.Project = '" + Project + "'");

            //Logfile NUP
            string KetNUP = "Edit data NUP : " + NoNUP + " Tipe : " + Tipe + "<br>"
                + Cf.LogCompare(rsBefNUP, rsAftNUP);

            Db.Execute("EXEC spLogNUP"
            + " 'REFUND'"
            + ",'" + Act.UserID + "'"
            + ",'" + Act.IP + "'"
            + ",'" + KetNUP + "'"
            + ",'" + NoNUP + "'"
            );

            Response.Redirect("NUP.aspx");
        }

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
        private string Project
        {
            get
            {
                return Cf.Pk(Request.QueryString["project"]);
            }
        }
    }
}

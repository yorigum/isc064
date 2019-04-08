using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.NUP
{
    public partial class NUPRefundFin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                Fill();
            }
        }

        private void Fill()
        {
            string tampilNUP = "";
            DataTable dtNUP = Db.Rs("SELECT * FROM MS_NUP WHERE NoNUP = " + NoNUP + " AND Tipe ='" + Tipe + "'");

            if (dtNUP.Rows.Count > 0)
            {
                tampilNUP = dtNUP.Rows[0]["NoNUP"].ToString();
                int intrevisi = Convert.ToInt32(dtNUP.Rows[0]["Revisi"]);
                if (intrevisi > 0)
                    tampilNUP = tampilNUP + "R" + intrevisi.ToString();

                nonup.Text = tampilNUP;
                nonup.Font.Size = 35;

                string namaCS = Db.SingleString("SELECT ISNULL(Nama, '') FROM MS_CUSTOMER WHERE NoCustomer = " + Convert.ToInt32(dtNUP.Rows[0]["NoCustomer"]));
                string namaAG = Db.SingleString("SELECT ISNULL(Nama, '') FROM MS_AGENT WHERE NoAgent = " + Convert.ToInt32(dtNUP.Rows[0]["NoAgent"]));

                string notts = Db.SingleString("SELECT NoTTS FROM MS_NUP_PELUNASAN WHERE NoNUP='" + NoNUP + "' AND Tipe = '" + Tipe + "' AND PelunasanKe=2");

                customer.Text = namaCS;
                agent.Text = namaAG;
                asp.HRef = "javascript:openPopUp('/nup/PrintRefund.aspx?NoNUP=" + NoNUP + "&Tipe="+Tipe+"','920','650')";
            }


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
    }
}
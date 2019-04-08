using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LAUNCHING
{
    public partial class UnitPetaBooking1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Act.Pass();
            //Act.NoCache();

            if (!Page.IsPostBack)
            {
                Fill();
            }

            FeedBack();
            //Js.Confirm(this, "Apakah Anda yakin memilih unit " + nomorunit.Text);
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

        private void Fill()
        {
            string strSql = "SELECT * FROM MS_UNIT WHERE NoStock = '" + NoStock + "'";
            DataTable rs = Db.Rs(strSql);
            string strSql2 = "SELECT * FROM MS_NUP WHERE NoNUP='" + NoPilihan + "'";
            DataTable rs2 = Db.Rs(strSql2);
            int Count = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM MS_NUP WHERE NoNUP = '" + NoPilihan + "'");
            if (Count != 0)
                priority.Text = NoPilihan;
            if (rs.Rows.Count > 0)
                nomorunit.Text = rs.Rows[0]["NoUnit"].ToString();
            if (rs2.Rows.Count > 0)
                namacust.Text = rs2.Rows[0]["NamaCustomer"].ToString();

            string st = "AVAILABLE";
            if (rs.Rows[0]["Status"].ToString() != "A")
            {
                st = "NOT AVAILABLE";
                tdsave.Attributes["style"] = "display:none";
            }
            statusunit.Text = st;
        }

        private bool valid()
        {
            string s = "";

            bool x = true;
            string status = Db.SingleString("SELECT Status FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");
            if (status != "A")
            {
                x = false;
                if (s == "") s = nomorunit.ID;
                nomorunitc.Text = "Unit sudah dipilih.";
            }
            else
                nomorunitc.Text = "";

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

        private bool Save()
        {
            if (valid())
            {
                Db.Execute("UPDATE MS_UNIT SET Status = 'P' WHERE NoStock = '" + NoStock + "'");
                int Count = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM MS_NUP WHERE NoNUP = '" + NoPilihan + "'");
                string NoPilihanNew = NoPilihan;
                if (Count == 0)
                    NoPilihanNew = "T-" + NoPilihanNew;

                Db.Execute("INSERT INTO MS_NUP_PRIORITY(NoNUP, NoStock, NoNUPHeader) VALUES('" + NoPilihanNew + "', '" + NoStock + "', '" + NoNUP + "')");
                
                return true;
            }
            else
                return false;
        }

        protected void ok_Click(object sender, System.EventArgs e)
        {
            if (Save()) Js.Close(this);
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            if (Save())
            {
                Response.Redirect("ClosingNUP2.aspx?No=" + NoNUP);
            }
        }
        protected void cancel_Click(object sender, EventArgs e)
        {
            //http://localhost:8000/launching/UnitPetaDetil.aspx?f=GEM%20CITY&NoNUP=122&NoPil=012
            Response.Redirect("UnitPetaDetil.aspx?f=GEM CITY&NoPil=" + NoPilihan + "&NoNUP=" + NoNUP);
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
        private string NoPilihan
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoPil"]);
            }
        }
    }
}
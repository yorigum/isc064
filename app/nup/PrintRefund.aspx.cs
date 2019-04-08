using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.NUP
{
    public partial class PrintRefund : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            SetTemplate();

            if (!Page.IsPostBack)
            {
                Fill();
            }

            if (reprint.Visible)
                RegisterOnSubmitStatement(
                    "md5Script"
                    , "document.getElementById('pass').value=hex_md5(document.getElementById('pass').value);"
                    );
        }

        private void SetTemplate()
        {
            PrintRefundTemplate uc = (PrintRefundTemplate)Page.LoadControl("PrintRefundTemplate.ascx");
            uc.NoNUP = NoNUP;
            uc.Tipe = Tipe;
            list.Controls.Add(uc);
        }

        private void Fill()
        {
            cancel.Attributes["onclick"] = "location.href='NUPEdit.aspx?NoNUP=" + NoNUP + "' AND Tipe = '" + Tipe + "'";
            //cancel2.Attributes["onclick"] = "location.href='KontrakEdit.aspx?NoKontrak="+NoKontrak+"'";

            string strSql = "SELECT PrintRefund FROM MS_NUP WHERE NoNUP = '" + NoNUP + "' AND Tipe = '" + Tipe + "'";
            DataTable rs = Db.Rs(strSql);
            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/NoPrint.html");
            else
            {
                count.Text = rs.Rows[0]["PrintRefund"].ToString();
                if ((int)rs.Rows[0]["PrintRefund"] == 0)
                    Tampil(); //langsung tampil
                else
                {
                    //mekanisme reprint
                    list.Visible = false;
                    reprint.Visible = true;
                    Js.Focus(this, username);

                    if (Session["SalahPass"] == null)
                        Session["SalahPass"] = "0"; //Hitung password salah berapa kali
                    else
                    {
                        if (Session["SalahPass"].ToString() != "0")
                            salah.Text = Session["SalahPass"] + "x salah";
                    }
                }
            }
        }

        private void Tampil()
        {
            list.Visible = true;
            reprint.Visible = false;

            int c = Db.SingleInteger("SELECT PrintRefund FROM MS_NUP WHERE NoNUP='" + NoNUP + "' AND Tipe = '" + Tipe + "'");

            //Logfile
            DataTable rsLog = Db.Rs("SELECT "
                + " MS_NUP.NoNUP AS [NUP]"
                + ",MS_NUP.Tipe AS [NUP]"
                + ",MS_CUSTOMER.Nama AS [Customer]"
                + " FROM MS_NUP INNER JOIN MS_CUSTOMER"
                + " ON MS_NUP.NoCustomer = MS_CUSTOMER.NoCustomer"
                + " WHERE MS_NUP.NoNUP = '" + NoNUP + "' AND Tipe = '" + Tipe + "'");

            Db.Execute("EXEC spLogNUP"
            + " 'P-RFNUP'"
            + ",'" + Act.UserID + "'"
            + ",'" + Act.IP + "'"
            + ",'" + Cf.LogCapture(rsLog) + "'"
            + ",'" + NoNUP + "'"            
            );

            Js.AutoPrint(this);
        }

        protected void btn_Click(object sender, System.EventArgs e)
        {
            string pid = "RP:" + Request.PhysicalPath;
            string Username = Cf.Str(username.Text);
            int c = Db.SingleInteger(
                "SELECT COUNT(*) FROM " + Mi.DbPrefix + "SECURITY..USERNAME "
                + " WHERE UserID = '" + Username + "'"
                + " AND Pass = '" + pass.Text + "'"
                + " AND (SecLevel = 'PENGAWAS' OR SecLevel='SUP' OR SecLevel='DIR')"
                + " AND Status = 'A'"
                + " AND "
                + " (" //cek sec. level untuk reprint
                + "	SecLevel IN "
                + "		(SELECT Kode FROM " + Mi.DbPrefix + "SECURITY..PAGESEC WHERE Halaman = '" + pid + "')"
                + "	OR UserID IN "
                + "		(SELECT UserID FROM " + Mi.DbPrefix + "SECURITY..PAGEDENY WHERE Halaman = '" + pid + "' AND Sifat=0)"
                + " )"
                );

            if (c != 0)
                Valid(Username);
            else
                Invalid();
        }

        private void Valid(string Username)
        {
            Session["SalahPass"] = null;

            //Logfile
            DataTable rsLog = Db.Rs("SELECT "
                + " MS_NUP.NoNUP AS [NUP]"
                + ",MS_CUSTOMER.Nama AS [Customer]"
                + " FROM MS_NUP INNER JOIN MS_CUSTOMER"
                + " ON MS_NUP.NoCustomer = MS_CUSTOMER.NoCustomer"
                + " WHERE MS_NUP.NoNUP = '" + NoNUP + "'");

            Db.Execute("EXEC spLogNUP"
                + " 'R-RFNUP'"
                + ",'" + username + "'"
                + ",'" + Act.IP + "'"
                + ",'" + Cf.LogCapture(rsLog) + "'"
                + ",'" + NoNUP + "'"
                + ",'" + Tipe + "'"
                );

            Tampil();
        }

        private void Invalid()
        {
            //3x salah password akan mengakibatkan sign-out otomatis;
            int x = Convert.ToInt32(Session["SalahPass"]) + 1;
            salah.Text = x.ToString() + "x gagal";
            Session["SalahPass"] = x;

            if (x >= 3)
                Response.Redirect("SignOut.aspx?pass=1");

            Js.Alert(
                this
                , "Otorisasi Gagal " + x + "x.\\n"
                + "Username akan Sign-Out otomatis apabila salah 3x."
                , "document.getElementById('pass').focus();"
                );
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion
    }
}

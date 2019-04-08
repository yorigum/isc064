using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064
{
    public partial class _Default : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.NoCache();

            if (!Mi.Licensed())
                Response.Redirect("/CustomError/Licensed.html");

            RegisterOnSubmitStatement(
                "md5Script"
                , "document.getElementById('pass').value=hex_md5(document.getElementById('pass').value);"
                );

            if (!Page.IsPostBack)
            {
                comp.InnerHtml = Mi.Pt;
                Js.Focus(this, username);
            }
        }

        protected void btn_Click(object sender, System.EventArgs e)
        {
            string Username = Cf.Str(username.Text);

            if (IsExist(Username))
            {
                string strSql = "SELECT "
                    + " UserID, Pass, SecLevel, GantiPass "
                    + " FROM USERNAME WHERE UserID = '" + Username + "'";
                DataTable rs = Db.Rs(strSql);

                string UserID = rs.Rows[0]["UserID"].ToString();
                string Pass = rs.Rows[0]["Pass"].ToString();
                string SecLevel = rs.Rows[0]["SecLevel"].ToString();
                bool GantiPass = (bool)rs.Rows[0]["GantiPass"];

                if (rs.Rows[0]["Pass"].ToString() != pass.Text)
                {
                    SalahPass(UserID);
                }
                else
                {
                    //if (IsInside(UserID))
                    //{
                    //    //Double Login
                    //    Db.Execute("EXEC spAppDoubleLogin "
                    //        + " '" + UserID + "'"
                    //        + ",'" + Act.IP + "'"
                    //        );

                    //    Js.Alert(
                    //        this
                    //        , "Double Login.\\n"
                    //        + "Masih ada user lain di dalam aplikasi dengan username anda.\\n"
                    //        + "Silakan mencoba untuk login kembali 5 menit lagi."
                    //        , "document.getElementById('pass').focus();"
                    //        + "document.getElementById('pass').select();"
                    //        );
                    //}
                    //else
                    //{
                    LogIn(UserID, SecLevel, GantiPass);
                    //}
                }
            }
        }

        private bool IsExist(string Username)
        {
            //Username terdaftar dan tidak diblokir ?
            if (Db.SingleInteger(
                "SELECT COUNT(*) FROM USERNAME WHERE UserID = '" + Username + "' AND Status = 'A'") != 0)
            {
                return true;
            }
            else
            {
                Js.Alert(
                    this
                    , "Username Tidak Terdaftar atau Username Diblokir."
                    , "document.getElementById('username').focus();"
                    + "document.getElementById('username').select();"
                    );

                return false;
            }
        }

        private bool IsInside(string UserID)
        {
            //User masih ada di dalam program ?
            if (Db.SingleInteger(
                "SELECT COUNT(*) FROM LOGIN WHERE UserID = '" + UserID + "' AND Status = 'A'") != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void SalahPass(string UserID)
        {
            //Prosedur salah password
            Db.Execute("EXEC spSalahPass "
                + " '" + UserID + "'"
                + ",'" + Act.IP + "'"
                );

            int PassError = Db.SingleInteger(
                "SELECT PassError FROM USERNAME WHERE UserID = '" + UserID + "'");

            //3x salah password?
            if (PassError >= 3)
            {
                //Prosedur blokir
                Db.Execute("EXEC spUserBlokir "
                    + " '" + UserID + "'"
                    + ",'" + Act.IP + "'"
                    );

                Js.Alert(
                    this
                    , "Password Salah 3x.\\n"
                    + "Username Diblokir.\\n"
                    + "Harap menghubungi divisi IT untuk aktivasi kembali."
                    , "document.getElementById('username').focus();"
                    + "document.getElementById('username').select();"
                    );
            }
            else
            {
                Js.Alert(
                    this
                    , "Password Salah " + PassError + "x.\\n"
                    + "Username akan diblokir apabila salah 3x."
                    , "document.getElementById('pass').focus();"
                    + "document.getElementById('pass').select();"
                    );
            }
        }

        private void LogIn(string UserID, string SecLevel, bool GantiPass)
        {
            //Simpan dulu tanggal TERAKHIR LOGIN (tidak termasuk kejadian login detik ini)
            DateTime TglLogin = Db.SingleTime("SELECT TglLogin FROM USERNAME WHERE UserID = '" + UserID + "'");
            Session["LastLogin"] = Cf.IndoWeek(TglLogin) + ", " + Cf.Date(TglLogin);

            Db.Execute("EXEC spAppLogin "
                + " '" + UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'" + Session.SessionID + "'"
                );

            //Init Session
            Act.UserID = UserID;
            Act.SecLevel = SecLevel;

            if (GantiPass)
                Response.Redirect("GantiPass.aspx?login=1");
            else
                Response.Redirect("Gateway.aspx");
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

        protected void username_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

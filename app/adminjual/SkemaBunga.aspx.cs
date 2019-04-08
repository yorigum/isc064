using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace ISC064.ADMINJUAL
{
    public partial class SkemaBunga : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                string persen = Request.QueryString["t1"];
                string ket = Request.QueryString["t2"];

                if (persen != "" && ket != "")
                {
                    string[] p = persen.Split(' ');
                    string[] k = ket.Split(';');

                    try { persen1.Text = (Convert.ToDecimal(p[0]) * (decimal)1).ToString(); }
                    catch { }
                    try { persen2.Text = (Convert.ToDecimal(p[1]) * (decimal)1).ToString(); }
                    catch { }
                    try { persen3.Text = (Convert.ToDecimal(p[2]) * (decimal)1).ToString(); }
                    catch { }
                    try { persen4.Text = (Convert.ToDecimal(p[3]) * (decimal)1).ToString(); }
                    catch { }
                    try { persen5.Text = (Convert.ToDecimal(p[4]) * (decimal)1).ToString(); }
                    catch { }
                    try { persen6.Text = (Convert.ToDecimal(p[5]) * (decimal)1).ToString(); }
                    catch { }
                    try { persen7.Text = (Convert.ToDecimal(p[6]) * (decimal)1).ToString(); }
                    catch { }
                    try { persen8.Text = (Convert.ToDecimal(p[7]) * (decimal)1).ToString(); }
                    catch { }
                    try { persen9.Text = (Convert.ToDecimal(p[8]) * (decimal)1).ToString(); }
                    catch { }
                    try { persen10.Text = (Convert.ToDecimal(p[9]) * (decimal)1).ToString(); }
                    catch { }

                    try { ket1.Text = k[0]; }
                    catch { }
                    try { ket2.Text = k[1]; }
                    catch { }
                    try { ket3.Text = k[2]; }
                    catch { }
                    try { ket4.Text = k[3]; }
                    catch { }
                    try { ket5.Text = k[4]; }
                    catch { }
                    try { ket6.Text = k[5]; }
                    catch { }
                    try { ket7.Text = k[6]; }
                    catch { }
                    try { ket8.Text = k[7]; }
                    catch { }
                    try { ket9.Text = k[8]; }
                    catch { }
                    try { ket10.Text = k[9]; }
                    catch { }
                }
            }
        }

        private bool valid()
        {
            bool x = true;

            if ((ket1.Text != "" && persen1.Text == "") || (ket1.Text == "" && persen1.Text != "")) x = false;
            if ((ket2.Text != "" && persen2.Text == "") || (ket2.Text == "" && persen2.Text != "")) x = false;
            if ((ket3.Text != "" && persen3.Text == "") || (ket3.Text == "" && persen3.Text != "")) x = false;
            if ((ket4.Text != "" && persen4.Text == "") || (ket4.Text == "" && persen4.Text != "")) x = false;
            if ((ket5.Text != "" && persen5.Text == "") || (ket5.Text == "" && persen5.Text != "")) x = false;
            if ((ket6.Text != "" && persen6.Text == "") || (ket6.Text == "" && persen6.Text != "")) x = false;
            if ((ket7.Text != "" && persen7.Text == "") || (ket7.Text == "" && persen7.Text != "")) x = false;
            if ((ket8.Text != "" && persen8.Text == "") || (ket8.Text == "" && persen8.Text != "")) x = false;
            if ((ket9.Text != "" && persen9.Text == "") || (ket9.Text == "" && persen9.Text != "")) x = false;
            if ((ket10.Text != "" && persen10.Text == "") || (ket10.Text == "" && persen10.Text != "")) x = false;

            if (ket1.Text != "")
            {
                if (!Cf.isMoney(persen1))
                {
                    x = false;
                    Cf.MarkError(persen1);
                }
                else
                    persen1.Attributes["style"] = "bacground-color:white;";
            }
            else
                persen1.Attributes["style"] = "bacground-color:white;";

            if (ket2.Text != "")
            {
                if (!Cf.isMoney(persen2))
                {
                    x = false;
                    Cf.MarkError(persen2);
                }
                else
                    persen2.Attributes["style"] = "bacground-color:white;";
            }
            else
                persen2.Attributes["style"] = "bacground-color:white;";

            if (ket3.Text != "")
            {
                if (!Cf.isMoney(persen3))
                {
                    x = false;
                    Cf.MarkError(persen3);
                }
                else
                    persen3.Attributes["style"] = "bacground-color:white;";
            }
            else
                persen3.Attributes["style"] = "bacground-color:white;";

            if (ket4.Text != "")
            {
                if (!Cf.isMoney(persen4))
                {
                    x = false;
                    Cf.MarkError(persen4);
                }
                else
                    persen4.Attributes["style"] = "bacground-color:white;";
            }
            else
                persen4.Attributes["style"] = "bacground-color:white;";

            if (ket5.Text != "")
            {
                if (!Cf.isMoney(persen5))
                {
                    x = false;
                    Cf.MarkError(persen5);
                }
                else
                    persen5.Attributes["style"] = "bacground-color:white;";
            }
            else
                persen5.Attributes["style"] = "bacground-color:white;";

            if (ket6.Text != "")
            {
                if (!Cf.isMoney(persen6))
                {
                    x = false;
                    Cf.MarkError(persen6);
                }
                else
                    persen6.Attributes["style"] = "bacground-color:white;";
            }
            else
                persen6.Attributes["style"] = "bacground-color:white;";

            if (ket7.Text != "")
            {
                if (!Cf.isMoney(persen7))
                {
                    x = false;
                    Cf.MarkError(persen7);
                }
                else
                    persen7.Attributes["style"] = "bacground-color:white;";
            }
            else
                persen7.Attributes["style"] = "bacground-color:white;";

            if (ket8.Text != "")
            {
                if (!Cf.isMoney(persen8))
                {
                    x = false;
                    Cf.MarkError(persen8);
                }
                else
                    persen8.Attributes["style"] = "bacground-color:white;";
            }
            else
                persen8.Attributes["style"] = "bacground-color:white;";

            if (ket9.Text != "")
            {
                if (!Cf.isMoney(persen9))
                {
                    x = false;
                    Cf.MarkError(persen9);
                }
                else
                    persen9.Attributes["style"] = "bacground-color:white;";
            }
            else
                persen9.Attributes["style"] = "bacground-color:white;";

            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Rumus bunga harus lengkap.\\n"
                    , ""
                    );

            return x;
        }

        protected void ok_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                System.Text.StringBuilder persen = new System.Text.StringBuilder();

                if (persen1.Text != "") persen.Append("+" + Convert.ToDecimal(persen1.Text));
                if (persen2.Text != "") persen.Append("+" + Convert.ToDecimal(persen2.Text));
                if (persen3.Text != "") persen.Append("+" + Convert.ToDecimal(persen3.Text));
                if (persen4.Text != "") persen.Append("+" + Convert.ToDecimal(persen4.Text));
                if (persen5.Text != "") persen.Append("+" + Convert.ToDecimal(persen5.Text));
                if (persen6.Text != "") persen.Append("+" + Convert.ToDecimal(persen6.Text));
                if (persen7.Text != "") persen.Append("+" + Convert.ToDecimal(persen7.Text));
                if (persen8.Text != "") persen.Append("+" + Convert.ToDecimal(persen8.Text));
                if (persen9.Text != "") persen.Append("+" + Convert.ToDecimal(persen9.Text));
                if (persen10.Text != "") persen.Append("+" + Convert.ToDecimal(persen10.Text));
                
                if (persen.Length != 0)
                    persen = persen.Remove(0, 1);

                System.Text.StringBuilder ket = new System.Text.StringBuilder();
                if (ket1.Text != "") ket.Append(";" + ket1.Text.Replace(";", " ").Replace("'", "\\'"));
                if (ket2.Text != "") ket.Append(";" + ket2.Text.Replace(";", " ").Replace("'", "\\'"));
                if (ket3.Text != "") ket.Append(";" + ket3.Text.Replace(";", " ").Replace("'", "\\'"));
                if (ket4.Text != "") ket.Append(";" + ket4.Text.Replace(";", " ").Replace("'", "\\'"));
                if (ket5.Text != "") ket.Append(";" + ket5.Text.Replace(";", " ").Replace("'", "\\'"));
                if (ket6.Text != "") ket.Append(";" + ket6.Text.Replace(";", " ").Replace("'", "\\'"));
                if (ket7.Text != "") ket.Append(";" + ket7.Text.Replace(";", " ").Replace("'", "\\'"));
                if (ket8.Text != "") ket.Append(";" + ket8.Text.Replace(";", " ").Replace("'", "\\'"));
                if (ket9.Text != "") ket.Append(";" + ket9.Text.Replace(";", " ").Replace("'", "\\'"));
                if (ket10.Text != "") ket.Append(";" + ket10.Text.Replace(";", " ").Replace("'", "\\'"));
                if (ket.Length != 0)
                    ket = ket.Remove(0, 1);

                ClientScript.RegisterStartupScript(
                    GetType()
                    , "selectCal"
                    , "<script language='javascript'>"
                    + " window.opener.document.getElementById('" + Request.QueryString["d1"] + "').value='" + persen + "';"
                    + " window.opener.document.getElementById('" + Request.QueryString["d2"] + "').value='" + ket + "';"
                    + " window.close();"
                    + "</script>"
                    );
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

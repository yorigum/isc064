﻿namespace ISC064.KOMISI
{
    using System;
    using System.Drawing;
    using System.Data;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;

    public partial class HeadCF : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["p"] != null)
                    this.Page.RegisterStartupScript(
                        "focusScript"
                        , "<script type='text/javascript'>"
                        + " document.getElementById('" + this.ID + "_" + prev.ID + "').focus();"
                        + "</script>"
                        );
                else if (Request.QueryString["n"] != null)
                    this.Page.RegisterStartupScript(
                        "focusScript"
                        , "<script type='text/javascript'>"
                        + " document.getElementById('" + this.ID + "_" + next.ID + "').focus();"
                        + "</script>"
                        );

                string p = Db.SingleString("SELECT ISNULL((SELECT TOP 1 NoCF FROM MS_KOMISI_CF WHERE NoCF < '" + Nomor + "' ORDER BY NoCF DESC),'')");
                string n = Db.SingleString("SELECT ISNULL((SELECT TOP 1 NoCF FROM MS_KOMISI_CF WHERE NoCF > '" + Nomor + "' ORDER BY NoCF ASC),'')");
                if (p != "") prev.HRef = "?p=1&Nomor=" + p + "&project=" + Project; else prev.InnerHtml = "<img src='/Media/icon_prev_d.gif'>";
                if (n != "") next.HRef = "?n=1&Nomor=" + n + "&project=" + Project; else next.InnerHtml = "<img src='/Media/icon_next_d.gif'>";

                string strSql = "SELECT NoCF,NamaAgent FROM MS_KOMISI_CF WHERE NoCF = '" + Nomor + "' and Project = '" + Project + "'";
                DataTable rs = Db.Rs(strSql);

                if (rs.Rows.Count == 0)
                    Response.Redirect("/CustomError/Deleted.html");
                else
                {
                    nomor.Text = rs.Rows[0]["NoCF"].ToString();
                    nama.Text = rs.Rows[0]["NamaAgent"].ToString();
                }
            }
        }

        private string Nomor
        {
            get
            {
                return Cf.Pk(Request.QueryString["Nomor"]);
            }
        }

        private string Project
        {
            get
            {
                return Cf.Pk(Request.QueryString["project"]);
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
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion
    }
}

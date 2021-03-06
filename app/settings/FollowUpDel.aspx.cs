﻿using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.SETTINGS
{
    public partial class FollowUpDel : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                Js.Focus(this, ket);
                string Grouping = Db.SingleString("SELECT NamaGrouping FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_FOLLOWUP WHERE No='" + No + "'");
                Js.Confirm(this,
                    "Apakah anda ingin menghapus lokasi unit : " + Grouping + " ?\\n"
                    + "Perhatian bahwa data akan dihapus secara PERMANEN."
                    );
            }
        }

        protected void delbtn_Click(object sender, System.EventArgs e)
        {
            DataTable rs = Db.Rs(
                "SELECT * FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_FOLLOWUP WHERE No = '" + No + "'");

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else if (!Act.AksesProject(rs.Rows[0]["Project"].ToString()))
                Response.Redirect("/CustomError/SecLevel.html");
            else
            {
                string Ket = "***Alasan Delete :<br>" + Cf.Str(ket.Text)
                    + "<br><br>***Data Sebelum Delete :<br>"
                    + Cf.LogCapture(rs);

                int c = Db.SingleInteger(
                    "SELECT COUNT(*) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_FOLLOWUP WHERE NamaGrouping = '" + rs.Rows[0]["NamaGrouping"] + "'");

                if (c == 0)
                {
                    Db.Execute("EXEC " + Mi.DbPrefix + "MARKETINGJUAL..spLogGrouping"
                  + " 'DELETE'"
                  + ",'" + Act.UserID + "'"
                  + ",'" + Act.IP + "'"
                  + ",'" + Ket + "'"
                  + ",'" + No + "'"
                  );
                    Db.Execute("DELETE FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_FOLLOWUP WHERE No = '" + No + "'");
                    Js.Close(this);
                }
                else
                {
                    //Tidak bisa dihapus
                    frm.Visible = false;
                    nodel.Visible = true;

                }
            }
        }

        private string No
        {
            get
            {
                return Cf.Pk(Request.QueryString["No"]);
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

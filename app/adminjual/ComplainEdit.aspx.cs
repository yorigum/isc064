using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.ADMINJUAL
{
    public partial class ComplainEdit : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();


            if (!Page.IsPostBack)
            {
                //Bind(); //tanggal dan bulan
                Fill();
            }

            FeedBack();
            Js.Confirm(this, "Lanjutkan proses edit data jenis complain?\\n"
                + "");
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
            btndel.Attributes["onclick"] = "location.href='ComplainDel.aspx?NoComplain=" + NoComplain + "'";

            string strSql = "SELECT * FROM REF_COMPLAIN WHERE NoComplain = '" + NoComplain + "'";
            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
               judul.Text = rs.Rows[0]["Judul"].ToString();
               pic.Text = rs.Rows[0]["PIC"].ToString();
            }
        }

        private bool valid()
        {
            string s = "";
            bool x = true;

            if (Cf.isEmpty(judul))
            {
                x = false;
                if (s == "") s = judul.ID;
                judulc.Text = "Kosong";
            }
            else
                judulc.Text = "";


            if (Cf.isEmpty(pic))
            {
                x = false;
                if (s == "") s = pic.ID;
                picc.Text = "Kosong";
            }
            else
                picc.Text = "";


            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Judul Complain tidak boleh kosong.\\n"
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );

            return x;
        }


        protected void ok_Click(object sender, System.EventArgs e)
        {

            Db.Execute("UPDATE REF_COMPLAIN SET Judul='" + judul.Text + "', PIC='" + pic.Text + "' WHERE NoComplain = '" + NoComplain + "'");
                    //Response.Redirect("ComplainEdit.aspx?done=1&NoComplain=" + NoComplain);
                    Js.Close(this);
              

        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            Db.Execute("UPDATE REF_COMPLAIN SET Judul='" + judul.Text + "' , PIC='" + pic.Text + "' WHERE NoComplain = '" + NoComplain + "'");
            Response.Redirect("ComplainEdit.aspx?done=1&NoComplain=" + NoComplain);
            //Js.Close(this);
        }

        private string NoComplain
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoComplain"]);
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
        protected void del_Click(object sender, EventArgs e)
        {
            Db.Execute("DELETE FROM REF_COMPLAIN WHERE NoComplain=" + Convert.ToInt32(NoComplain));
            Js.Close(this);
        }
}
}

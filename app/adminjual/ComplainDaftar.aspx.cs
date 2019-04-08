using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.ADMINJUAL
{
    public partial class ComplainDaftar : System.Web.UI.Page
    {
        protected System.Web.UI.HtmlControls.HtmlInputButton cancel;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                Js.Focus(this, judul);

                Fill();
            }

            FeedBack();
        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "<a href=\"javascript:popEditComplain('" + Request.QueryString["done"] + "')\">"
                        + "Pendaftaran Berhasil..."
                        + "</a>";
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
                    + "1.Judul Complain tidak boleh kosong.\\n"
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );

            return x;

        }


        protected void save_Click(object sender, System.EventArgs e)
        {

            if (valid())
            {
              
                
                string Judul = judul.Text;
                string PIC = pic.Text;

                Db.Execute("INSERT INTO REF_COMPLAIN(Judul,PIC) VALUES('" + Judul + "', '"+ PIC +"')");
                Response.Redirect("ComplainDaftar.aspx?done=" + Db.SingleInteger("SELECT MAX(NoComplain) FROM REF_COMPLAIN").ToString());


            }
        }

        private void Fill()
        {
            string strSql = "SELECT TOP 25 * "
                + " FROM REF_COMPLAIN"
                + " ORDER BY NoComplain DESC";

            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["NoComplain"].ToString();
                string t = rs.Rows[i]["Judul"].ToString() + "(" + rs.Rows[i]["PIC"].ToString() + ")";

                baru.Items.Add(new ListItem(t, v));
            }

            if (rs.Rows.Count != 0)
            {
                baru.SelectedIndex = 0;
                baru.Attributes["ondblclick"] = "popEditComplain("
                    + "this.options[this.selectedIndex].value)";
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

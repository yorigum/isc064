using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
    public partial class MigratePembayaran : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();

            if (!Page.IsPostBack)
            {
                hasil.Visible = false;
                //FillTerbaru();
            }

            FeedBack();

            Js.Focus(this, nokontrak);
            Js.ConfirmKeyword(this, nokontrak);
        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "<a href=\"javascript:popEditKontrak('" + Request.QueryString["done"] + "')\">"
                        + "Pendaftaran Pembayaran Berhasil..."
                        + "</a>";
            }
        }

        protected void display_Click(object sender, System.EventArgs e)
        {
            Fill();
            hasil.Visible = true;
        }

        private void Fill()
        {
            string strSql = "SELECT"
                + " DISTINCT(NoKontrak)"
                + " FROM MIGRATE_PEMBAYARAN"
                + " WHERE NoKontrak LIKE '%" + Cf.Str(nokontrak.Text) + "%'"
                + " AND Approved = 0"
                + " ORDER BY NoKontrak";

            DataTable rs = Db.Rs(strSql);
            Rpt.NoData(rpt, rs, "Tidak terdapat kontrak dengan kriteria seperti tersebut diatas.");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                TableRow r = new TableRow();
                TableCell c;

                c = new TableCell();
                c.Text = "<a href='MigratePembayaran2.aspx?No=" + rs.Rows[i]["NoKontrak"] + "'>Next...</a>";
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoKontrak"].ToString();
                r.Cells.Add(c);

                Rpt.Border(r);
                rpt.Rows.Add(r);
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

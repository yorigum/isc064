using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
    public partial class MigrateJadwal : System.Web.UI.Page
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
                        + "<a href=\"javascript:popJadwalTagihan('" + Request.QueryString["done"] + "')\">"
                        + "Pendaftaran Jadwal Tagihan Berhasil..."
                        + "</a>";
            }
        }

        //private void FillTerbaru()
        //{
        //    string strSql = "SELECT TOP 10 NoKontrak, NoUnit "
        //        + " FROM MS_KONTRAK "
        //        + " ORDER BY TglInput DESC, NoKontrak DESC";

        //    DataTable rs = Db.Rs(strSql);
        //    for (int i = 0; i < rs.Rows.Count; i++)
        //    {
        //        string v = rs.Rows[i]["NoKontrak"].ToString();
        //        string t = v + " (" + rs.Rows[i]["NoUnit"] + ")";

        //        baru.Items.Add(new ListItem(t, v));
        //    }

        //    if (rs.Rows.Count != 0)
        //    {
        //        baru.SelectedIndex = 0;
        //        baru.Attributes["ondblclick"] = "location.href="
        //            + "'KontrakDaftar3.aspx?NoKontrak='+this.options[this.selectedIndex].value";
        //    }
        //}

        protected void display_Click(object sender, System.EventArgs e)
        {
            Fill();
            hasil.Visible = true;
        }

        private void Fill()
        {
            string strSql = "SELECT"
                + " DISTINCT(NoKontrak)"
                + " FROM MIGRATE_TAGIHAN"
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
                c.Text = "<a href='MigrateJadwal2.aspx?NoKontrak=" + rs.Rows[i]["NoKontrak"] + "'>Next...</a>";
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoKontrak"].ToString();
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "<a style='font:8pt' href=\"javascript:hapus('" + rs.Rows[i]["NoKontrak"] + "')\">Delete...</a>";
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

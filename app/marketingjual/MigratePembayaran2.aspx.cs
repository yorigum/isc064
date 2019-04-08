using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
    public partial class MigratePembayaran2 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();

            if (!Page.IsPostBack)
            {
                Fill();
                hasil.Visible = true;
            }
        }

        private void Fill()
        {
            string strSql = "SELECT"
                + " DISTINCT(NoTTS)"
                + " FROM MIGRATE_PEMBAYARAN"
                + " WHERE NoKontrak = '" + Request.QueryString["No"] + "'"
                + " AND Approved = 0"
                + " ORDER BY NoTTS";

            DataTable rs = Db.Rs(strSql);
            Rpt.NoData(rpt, rs, "Tidak terdapat tts dengan kriteria seperti tersebut diatas.");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                TableRow r = new TableRow();
                TableCell c;

                c = new TableCell();
                c.Text = "<a href='MigratePembayaran3.aspx?No=" + Request.QueryString["No"] + "&NoTTS=" + rs.Rows[i]["NoTTS"] + "'>Next...</a>";
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoTTS"].ToString();
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

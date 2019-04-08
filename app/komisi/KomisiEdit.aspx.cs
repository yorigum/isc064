using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;

namespace ISC064.KOMISI
{
    public partial class KomisiEdit : System.Web.UI.Page
    {
        protected DataTable rs;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Act.Sec("ED:" + Request.PhysicalPath))
            {
                ok.Enabled = false;
            }

            if (!Page.IsPostBack)
            {
                FillHeader();
            }

            if (!Page.IsPostBack)
            {
                FillTable();
            }
        }
        private void FillHeader()
        {
            btnlog.Attributes["onclick"] = "location.href='LogPk.aspx?Tb=MS_KOMISI_LOG&Pk=" + Nomor.PadLeft(5, '0') + "'";

            DataTable rsHeader = Db.Rs("SELECT * FROM MS_KOMISI WHERE NoKomisi = '" + Nomor + "'");
            if (rsHeader.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                skema.Text = rsHeader.Rows[0]["NamaSkema"] + "(" + rsHeader.Rows[0]["NoSkema"].ToString().PadLeft(3, '0') + ")";
                termin.Text = rsHeader.Rows[0]["NamaTermin"] + "(" + rsHeader.Rows[0]["NoTermin"].ToString().PadLeft(3, '0') + ")";
                nokontrak.Text = rsHeader.Rows[0]["NoKontrak"].ToString();
                nounit.Text = rsHeader.Rows[0]["NoUnit"].ToString();
                sales.Text = rsHeader.Rows[0]["NamaAgent"].ToString();
                cust.Text = rsHeader.Rows[0]["NamaCust"].ToString();
                tgl.Text = Cf.Day(rsHeader.Rows[0]["Tgl"]);
                project.Text = rsHeader.Rows[0]["Project"].ToString();
            }
        }

        private void FillTable()
        {
            DataTable rs = Db.Rs("SELECT * FROM MS_KOMISI_DETAIL WHERE NoKomisi = '" + Nomor + "'");
            if (rs.Rows.Count > 0)
            {
                for (int i = 0; i < rs.Rows.Count; i++)
                {
                    TableCell c;
                    TableRow tr;

                    tr = new TableRow();

                    c = new TableCell();
                    c.Text = (i + 1).ToString() + ".";
                    tr.Cells.Add(c);

                    c = new TableCell();
                    c.Text = rs.Rows[i]["NamaAgent"].ToString();
                    tr.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Cf.Num(rs.Rows[i]["Nilai"]);
                    c.CssClass = "right";
                    tr.Cells.Add(c);

                    tb.Rows.Add(tr);
                }
            }
        }
        protected void ok_Click(object sender, System.EventArgs e)
        {
            Js.Close(this);
        }
        private string Nomor
        {
            get
            {
                return Cf.Pk(Request.QueryString["Nomor"]);
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

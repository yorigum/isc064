using System;
using System.Drawing;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.APPROVAL
{

    public partial class KontrakApprovGU : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();
            Cf.SetGrid(tb);

            if (!Page.IsPostBack)
            {
                Act.ProjectList(project);
                Js.Focus(this, search);
                Bind();
            }

            FeedBack();
        }

        private bool validuser()
        {
            bool x = true;

            string ArrProject = "";
            string Projected = "";
            string strSql = "SELECT Project from " + Mi.DbPrefix + "SECURITY..REF_PROJECT";
            DataTable rs = Db.Rs(strSql);

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (i == (rs.Rows.Count - 1))
                {
                    ArrProject += "'" + rs.Rows[i]["Project"] + "'";
                }
                else
                {
                    ArrProject += "'" + rs.Rows[i]["Project"] + "',";
                }

            }

            if (project.SelectedIndex > 0)
            {
                Projected = "'" + project.SelectedValue + "'";
            }
            else
            {
                Projected = ArrProject;
            }


            if (Db.SingleInteger("SELECT COUNT(Lvl) FROM " + Mi.DbPrefix + "SECURITY..REF_APPROVAL WHERE Tipe = 2 AND Project IN (" + Projected + ") AND UserID = '" + Act.UserID + "'") == 0)
            {
                x = false;
            }

            if (!x)
            {
                Js.Alert(
                    this
                    , "Data Tidak Valid.\\n\\n"
                    + "Kemungkinan Sebab :\\n"
                    + "1. User dengan Level : " + Act.UserID + " belum diberikan hak Approve.\\n \\n"
                    + "Catatan : Hak approve bisa dilakukan pada modul Setting\\n"
                    , "document.getElementById('project').focus();"
                    + "document.getElementById('project').select();"
                    );
            }

            return x;

        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] == "1")
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "Approval Pindah Unit Kontrak Berhasil...";
                else if (Request.QueryString["done"] == "2")
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "Reject Pindah Unit Kontrak Berhasil...";
            }
        }

        private void Bind()
        {

            DataTable rs;
            string strSql;

            rs = Db.Rs("SELECT DISTINCT YEAR(a.TglKontrak), MONTH(a.TglKontrak) FROM MS_KONTRAK a WHERE a.Project = '" + project.SelectedValue + "'"
                + " ORDER BY YEAR(a.TglKontrak), MONTH(a.TglKontrak)");
            for (int i = 0; i < rs.Rows.Count; i++)
                thnKontrak.Items.Add(new ListItem(
                    Cf.Monthname((int)rs.Rows[i][1]) + " " + rs.Rows[i][0].ToString()
                    , rs.Rows[i][0] + "," + rs.Rows[i][1]
                    ));

            thnKontrak.SelectedIndex = thnKontrak.Items.Count - 1;

            strSql = "SELECT DISTINCT a.Lokasi FROM MS_KONTRAK a WHERE a.Project = '" + project.SelectedValue + "' ORDER BY a.Lokasi";
            rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
                lokasi.Items.Add(new ListItem(rs.Rows[i][0].ToString()));

        }

        protected void Fill()
        {
            string Periode = "";
            if (thnKontrak.SelectedIndex != 0)
            {
                string[] z = thnKontrak.SelectedValue.Split(',');
                Periode = " AND YEAR(a.TglKontrak) = " + z[0]
                    + " AND MONTH(a.TglKontrak) = " + z[1];
            }

            string Lokasi = "";
            if (lokasi.SelectedIndex != 0)
                Lokasi = " AND a.Lokasi = '" + Cf.Str(lokasi.SelectedValue) + "'";

            //cek level si user dalam approval
            DataTable rs2 = Db.Rs("SELECT Lvl FROM ISC064_SECURITY..REF_APPROVAL a WHERE Tipe = 2 AND Project = '" + project.SelectedValue + "' AND UserID = '" + Act.UserID + "'");
            if (rs2.Rows.Count != 0)
            {
                int lvl = (rs2.Rows.Count > 0 && Convert.ToInt16(rs2.Rows[0]["Lvl"]) == 1) ? Convert.ToInt16(rs2.Rows[0]["Lvl"]) : (Convert.ToInt16(rs2.Rows[0]["Lvl"]) - 1);

                //kalo dia bukan level pertama, cek di level sebelum nya udah ada yang approve atau belum
                string level = Convert.ToInt16(rs2.Rows[0]["Lvl"]) > 1 ? " AND (SELECT COUNT(*) FROM MS_APPROVAL_DETAIL WHERE NoApproval = e.NoApproval AND Lvl = " + lvl + " AND TglApproval IS NOT NULL) > 0" : "";

                string nav = "'<a href=KontrakApprovGU2.aspx?NoKontrak=''' + A.NoKontrak + '''&NoApproval=''' + e.NoApproval + '''&Level=" + Convert.ToInt16(rs2.Rows[0]["Lvl"]) + ">Next</a><br>'";

                string strSql = " SELECT "
                            + nav
                            + " AS Nav"
                            + ",e.SumberID AS Kontrak"
                            + ",CONVERT(VARCHAR,A.TglKontrak,106) AS Tgl, b.NoUnit AS Unit, c.Nama AS Customer, d.Nama AS Agent"
                            + ",(SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = a.Project) AS Project"
                            + " FROM MS_APPROVAL e"
                            + " INNER JOIN MS_KONTRAK a ON e.SumberID = a.NoKontrak"
                            + " INNER JOIN MS_UNIT b ON a.NoStock = b.NoStock"
                            + " INNER JOIN MS_CUSTOMER c ON a.NoCustomer = c.NoCustomer"
                            + " INNER JOIN MS_AGENT d ON a.NoAgent = d.NoAgent"
                            + " WHERE "
                            + " a.Status <> 'B'"
                            + " AND e.Sumber = '" + Str.Approval("2") + "'"
                            + " AND e.Project = '" + project.SelectedValue + "'"
                            + Periode
                            + Lokasi
                            + " AND e.Status <> 'DONE'"
                            + " AND (SELECT COUNT(*) FROM MS_APPROVAL_DETAIL WHERE NoApproval = e.NoApproval AND Lvl = " + Convert.ToInt16(rs2.Rows[0]["Lvl"]) + " AND TglApproval IS NOT NULL) = 0"
                            + level
                            ;

                DataTable rs = Db.Rs(strSql);
                tb.DataSource = rs;
                tb.DataBind();
            }
        }
        protected void display_Click(object sender, System.EventArgs e)
        {
            //if(validuser())
            //{ 
            Fill();
            //}
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

        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            lokasi.Items.Clear();
            lokasi.Items.Add(new ListItem("Lokasi :"));
            thnKontrak.Items.Clear();
            thnKontrak.Items.Add(new ListItem("Periode Kontrak :"));
            Bind();
        }

        protected void tb_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            tb.PageIndex = e.NewPageIndex;
            Fill();
        }
    }
}
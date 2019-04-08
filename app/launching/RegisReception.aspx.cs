using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LAUNCHING
{
    public partial class RegisReception : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();
            Cf.SetGrid(tb);
            if (!Page.IsPostBack)
            {
                Act.ProjectList(project);
                Bind();
                //Fill();
            }
        }

        protected void Bind()
        {
            jenis.Items.Clear();
            jenis.Items.Add(new ListItem("Pilih : "));
            DataTable rs = Db.Rs("SELECT * FROM REF_JENISPROPERTI WHERE Project = '" + project.SelectedValue + "'");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string a = rs.Rows[i]["SN"].ToString();
                string b = rs.Rows[i]["Nama"].ToString();
                jenis.Items.Add(new ListItem(b));
            }
        }

        private void Fill()
        {
            string JENIS = "";
            if (jenis.SelectedIndex > 0) JENIS = " AND a.Tipe = '" + jenis.SelectedValue + "'";
            string Level = Act.SecLevel;
            string warna = "' + CASE WHEN a.Status = 1 THEN 'style=''background-color:yellow'''"
                        + "       WHEN a.Status = 2 THEN 'style=''background-color:green'''"
                        + "       WHEN a.Status = 3 THEN 'style=''background-color:blue'''"
                        + "       WHEN a.Status = 4 THEN 'style=''background-color:red'''"
                        + "       WHEN a.Status = 5 THEN 'style=''background-color:darkorange'''"
                        + "       WHEN a.Status = 6 THEN 'style=''background-color:pink'''"
                        + " ELSE '' END + '";

            string nav = "'<a href=\"javascript:call('''+a.NoNUP+''','''+a.Tipe+''','''+a.Project+''')\">'  + a.NoNUP + '</a>'";

            string Query2 = "SELECT "
                        + nav
                        + " AS NUP"
                        + ",a.Tipe AS Tipe"
                        + ",b.Nama AS Customer"
                        + ",(SELECT Nama FROM MS_AGENT WHERE NoAgent = a.NoAgent) AS Agent"
                        + ",CASE WHEN a.Status = 0 THEN '<a href=\"RegisReception2.aspx?NoNUP='''+ a.NoNUP +'''&Tipe='''+ a.Tipe +'''&Project='''+a.Project+''' \">Aktivasi.. </a>'"
                        + "      WHEN a.Status = 1 THEN 'Sudah Aktivasi..'"
                        + "      WHEN a.Status = 2 THEN 'Pemanggilan NUP..'"
                        + "      WHEN a.Status = 3 THEN 'Sudah Pilih Unit..'"
                        + "      WHEN a.Status = 4 THEN 'Closing Unit..'"
                        + "      WHEN a.Status = 5 THEN 'Refund NUP..'"
                        + "      WHEN a.Status = 6 THEN 'Hold Aktivasi NUP..' END AS Act "
                        + ",CASE WHEN a.Status = 1 THEN '<a href=\"CancelReception2.aspx?NoNUP='''+a.NoNUP+'''&Tipe='''+a.Tipe+'''&Project='''+a.Project+'''\"> Cancel Aktivasi.. </a>'"
                        + "      ELSE '' END AS Act2"
                        + ",CASE WHEN a.Status = 0 AND '" + Level + "' = 'SUP' THEN '<a href=\"HoldReception2.aspx?NoNUP='''+a.NoNUP+'''&Tipe='''+a.Tipe+'''&Project='''+a.Project+'''\"> Hold Aktivasi.. </a>'"
                        + "      WHEN a.Status = 6 THEN '<a href=\"UnholdReception2.aspx?NoNUP='''+a.NoNUP+'''&Tipe='''+a.Tipe+'''\"> Unhold Aktivasi </a>'"
                        + "      ELSE '' END AS Act3"
                        + " FROM MS_NUP a "
                        + " INNER JOIN MS_CUSTOMER b ON a.NoCustomer = b.NoCustomer"
                        //+ " WHERE a.NoNUP = '" + rsNama.Rows[i]["NoNUP"].ToString() + "'"
                        + " WHERE a.Project = '" + project.SelectedValue + "'"
                        + " AND a.NoNUP IN (SELECT NoNUP FROM MS_NUP_PELUNASAN WHERE Tipe = a.Tipe AND Project = '" + project.SelectedValue + "')"
                        + JENIS
                        + " AND (b.Nama+a.NoNUP LIKE '%" + keyword.Text + "%') ORDER BY a.NoNUP";
            DataTable rsNUP = Db.Rs(Query2);
            tb.DataSource = rsNUP;
            tb.DataBind();
        }

        private bool CekPelunasanNUP(string NoNUPHeader, string Tipe)
        {
            bool x = true;
            int countJumlahNUP = Db.SingleInteger("SELECT COUNT(*) FROM MS_NUP WHERE NoNUP='" + NoNUPHeader + "' AND Tipe = '" + Tipe + "'");
            DataTable rs = Db.Rs("SELECT * FROM MS_NUP WHERE NoNUP='" + NoNUPHeader + "'");
            int countPelunasan = 0;
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                //cek di pelunasan NUP
                int cek = Db.SingleInteger("SELECT COUNT(*) FROM MS_NUP_PELUNASAN WHERE NoNUP='" + rs.Rows[i]["NoNUP"].ToString() + "' AND Tipe = '" + Tipe + "'");
                if (cek != 0)
                {
                    countPelunasan += 1;
                }
            }
            if (countJumlahNUP != countPelunasan)
            {
                x = false;
            }

            return x;
        }

        protected void display_Click(object sender, System.EventArgs e)
        {
            Cf.SetGrid(tb);
            Fill();
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
            jenis.Items.Clear();
            Bind();
        }

        protected void tb_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            tb.PageIndex = e.NewPageIndex;
            Fill();
        }
    }
}

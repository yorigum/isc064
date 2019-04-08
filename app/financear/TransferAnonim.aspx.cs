using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.FINANCEAR
{
    public partial class TransferAnonim : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();
            Cf.SetGrid(tb);
            if (!Page.IsPostBack)
            {
                Act.ProjectList(project);
                init();
            }

            Js.Focus(this, search);
        }

        private void init()
        {
            dari.Text = Cf.Day(DateTime.Today);
            sampai.Text = Cf.Day(DateTime.Today);

            bindRek();
        }

        private void bindRek()
        {
            DataTable rs;

            rs = Db.Rs("SELECT * FROM REF_ACC WHERE Project = '" + project.SelectedValue + "'");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["Acc"].ToString();
                string t = v + " : " + rs.Rows[i]["Bank"] + " " + rs.Rows[i]["Rekening"];
                rek.Items.Add(new ListItem(t, v));
            }
        }

        private bool valid()
        {
            string s = "";
            bool x = true;

            if (!Cf.isTgl(dari))
            {
                daric.Text = "Tanggal";
                if (s == "") s = dari.ID;
                x = false;
            }
            else
                daric.Text = "";

            if (!Cf.isTgl(sampai))
            {
                sampaic.Text = "Tanggal";
                if (s == "") s = sampai.ID;
                x = false;
            }
            else
                sampaic.Text = "";

            if (!x)
                ClientScript.RegisterStartupScript(GetType(), "err"
                    , "<script type='text/javascript'>document.getElementById('" + s + "').select()</script>");

            return x;
        }

        protected void display_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                Cf.SetGrid(tb);
                Fill();
            }
        }

        private void Fill()
        {
            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);
            if (Dari > Sampai)
            {
                DateTime x = Sampai;
                Sampai = Dari;
                Dari = x;
            }

            string Project = " AND Project IN (" + Act.ProjectListSql + ")";
            if (project.SelectedValue != "SEMUA")
            {
                Project = " AND Project = '" + project.SelectedValue + "'";
            }

            string Rek = "";
            if (rek.SelectedIndex != 0)
                Rek = " AND AccountID = '" + rek.SelectedValue + "'";

            string Status = "";
            if (statusB.Checked) Status = " AND Status = 'BARU'";
            if (statusID.Checked) Status = " AND Status = 'ID'";
            if (statusS.Checked) Status = " AND Status = 'S'";

            string nav = "'<a href=\"javascript:call(''' + CONVERT(varchar(50),NoAnonim) + ''')\">'"
                      + "+ FORMAT(NoAnonim,'0000000#') + '</a><br><i>'+ Status + '</i>'";

            string strSql = "SELECT"
                + nav
                + " AS Anonim"
                + ",CONVERT(VARCHAR,Tgl,106) AS Tgl"
                //+ ",Unit + '<br>' + Customer AS Customer"
                + ",Ket AS Keterangan"
                + ",(SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = MS_ANONIM.Project) AS Project"
                + ",FORMAT(Nilai,'#,###') AS Total"
                + " FROM MS_ANONIM "
                + " WHERE 1=1 "
                + " AND CONVERT(varchar,Tgl,112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND CONVERT(varchar,Tgl,112) <= '" + Cf.Tgl112(Sampai) + "'"
                + Rek
                + Project
                + Status
                + " ORDER BY NoAnonim";

            DataTable rs = Db.Rs(strSql);

            tb.DataSource = rs;
            tb.DataBind();
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

        protected void tb_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            tb.PageIndex = e.NewPageIndex;
            Fill();
        }

        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            rek.Items.Clear();
            rek.Items.Add(new ListItem("Rekening : "));
            bindRek();
        }
    }
}

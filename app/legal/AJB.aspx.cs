using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ISC064.LEGAL
{ 

public partial class AJB : System.Web.UI.Page
{
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();
            Cf.SetGrid(tb);
            if (!Page.IsPostBack)
            {
                init();
                Act.ProjectList(project);
            }

            Js.Focus(this, search);
        }

        private void init()
        {
            dari.Text = Cf.Day(DateTime.Today);
            sampai.Text = Cf.Day(DateTime.Today);

            if (Request.QueryString["status"] == null)
                status.SelectedIndex = 0;
            else if (Request.QueryString["status"] == "a")
                status.SelectedIndex = 1;
            else if (Request.QueryString["status"] == "b")
                status.SelectedIndex = 2;

            if (status.SelectedIndex != 0) status.Enabled = false;
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
                RegisterStartupScript("err"
                    , "<script language='javascript'>document.getElementById('" + s + "').select()</script>");

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

            string Status = "";
            if (status.SelectedIndex == 1)
                Status = " AND A.Status = 'A'";
            else if (status.SelectedIndex == 2)
                Status = " AND A.Status = 'B'";

            string NoKontrak = "'<a href=\"javascript:call(''' + A.NoKontrak + ''')\">'"
                 + "+ A.NoKontrak + '</a><br>";

            NoKontrak += "  <font style=''font:8pt;color:' + CASE WHEN D.NoPPJB != '' THEN 'Black' ELSE 'Silver' END + ' ''>PPJB</font>&nbsp;&nbsp;"
             + "<font style=''font:8pt;color:' + CASE WHEN E.NoAJB != '' THEN 'Black' ELSE 'Silver' END + '''>AJB</font>&nbsp;&nbsp;"
             + "<font style=''font:8pt;color:' + CASE WHEN F.ST = 'D' THEN 'Black' ELSE 'Silver' END + '''>BAST</font>&nbsp;&nbsp;"
             + "<font style=''font:8pt;color:' + CASE WHEN G.NoIMB = '' THEN 'Black' ELSE 'Silver' END + '''>IMB</font>&nbsp;&nbsp;"
             + "<font style=''font:8pt;color:' + CASE WHEN H.NoSertifikat = '' THEN 'Black' ELSE 'Silver' END + '''>STT</font>&nbsp;&nbsp;"
             + "' AS Kontrak"
             ;

            string Project = (project.SelectedIndex == 0) ? " AND A.Project IN (" + Act.ProjectListSql + ")" : " AND A.Project = '" + project.SelectedValue + "'";

            string strSql = "SELECT "
                + NoKontrak
                + ",A.NoUnit AS Unit"
                + ",B.Nama AS Customer"
                + ",'' AS Keterangan"
                + ",A.NamaProject AS Project"
                + " FROM MS_KONTRAK A INNER JOIN MS_CUSTOMER B ON A.NoCustomer = B.NoCustomer "
                + " INNER JOIN MS_AGENT C ON A.NoAgent = C.NoAgent"
                + " LEFT JOIN MS_PPJB D ON A.NoKontrak = D.NoKontrak"
                + " LEFT JOIN MS_AJB E ON A.NoKontrak = E.NoKontrak"
                + " LEFT JOIN MS_BAST F ON A.NoKontrak = F.NoKontrak"
                + " LEFT JOIN MS_IMB G ON A.NoKontrak = G.NoKontrak"
                + " LEFT JOIN MS_SERTIFIKAT H ON A.NoKontrak = H.NoKontrak"
                + " WHERE 1=1"
                + " AND CONVERT(VARCHAR, TglKontrak, 112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND CONVERT(VARCHAR, TglKontrak, 112) <= '" + Cf.Tgl112(Sampai) + "'"
                + Project
                + Status
                + " ORDER BY A.NoKontrak";

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
    }
}
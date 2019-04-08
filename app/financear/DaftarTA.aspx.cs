using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.FINANCEAR
{
    public partial class DaftarTA : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();
            Cf.SetGrid(tb);
            //Js.ConfirmKeyword(this, keyword);

            if (!Page.IsPostBack)
            {
                if (Request.QueryString["status"] == null)
                    metode.SelectedIndex = 0;
                //else if (Request.QueryString["status"] == "ok")
                //    metode.SelectedIndex = 1;
                //else if (Request.QueryString["status"] == "identifikasi")
                //    metode.SelectedIndex = 2;
                //else if (Request.QueryString["status"] == "solve")
                //    metode.SelectedIndex = 3;

                if (metode.SelectedIndex != 0) metode.Enabled = false;
            }
        }

        protected void search_Click(object sender, System.EventArgs e)
        {
            Cf.SetGrid(tb);
            Fill();
        }

        private void Fill()
        {
            string addq = "";
            if (metode.SelectedIndex == 1)
                addq = " AND Status = 'BARU'";
            else if (metode.SelectedIndex == 2)
                addq = " AND Status = 'ID'";
            else if (metode.SelectedIndex == 3)
                addq = " AND Status = 'S'";

            string nav = "";
            if (Request.QueryString["status"] == "tbDari" || Request.QueryString["status"] == "tbSampai")
            {
                nav = "'<a href=\"javascript:callSource(''' + CONVERT(varchar(50),NoAnonim) + ''' + '''+Status+''')\">'"
                      + "+ FORMAT(NoAnonim,'0000000#') + '</a><br><i>'+ Status + '</i>'";
            }
            else
            {
                nav = "'<a href=\"javascript:call(''' + CONVERT(varchar(50),NoAnonim) + ''')\">'"
                      + "+ FORMAT(NoAnonim,'0000000#') + '</a><br><i>'+ Status + '</i>'";
            }

            string strSql = "SELECT"
            + nav
            + " AS Anonim"
            + ",CONVERT(VARCHAR,Tgl,106) AS Tgl"
            + ",Unit + '<br>' + Customer AS Customer"
            + ",Ket AS Keterangan"
            + ",FORMAT(Nilai,'#,###') AS Total"
            + " FROM MS_ANONIM"
            + " WHERE CONVERT(varchar,NoAnonim) + Unit + Customer + Ket "
            + " LIKE '%" + Cf.Str(keyword.Text) + "%'"
            + addq
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
    }
}

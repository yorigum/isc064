using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.ADMINJUAL
{
    public partial class DaftarGimmick : System.Web.UI.Page
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
                else if (Request.QueryString["status"] == "1")
                    metode.SelectedIndex = 1;
                else if (Request.QueryString["status"] == "0")
                    metode.SelectedIndex = 2;

                Act.ProjectList(project);
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
                addq = " AND Status = '1'";
            else if (metode.SelectedIndex == 2)
                addq = " AND Status = '0'";

            //string nav = "'<a href=\"javascript:call(''' + CONVERT(varchar(50), ItemID) + ''')\">'"
            //        + " + Nama + (FORMAT(ItemID, ' (00000#)')) +"
            //        + "'</a>'"
            //        ;
            string nav = "'<a href=\"javascript:call(''' + ItemID + ''')\">' + Nama + '</a>' AS Nama";

            string strSql = "SELECT "
                + nav 
                + ",(select Nama from REF_TIPE_GIMMICK where REF_TIPE_GIMMICK.ID = MS_GIMMICK.Tipe) AS Tipe"
                + ",CONVERT(int, Stock) AS Stock"
                + ",Satuan AS Satuan"
                + " FROM MS_GIMMICK "
                + " WHERE Nama + Ket"
                + " LIKE '%" + Cf.Str(keyword.Text) + "%'"
                + " AND Project = '" + project.SelectedValue + "'"
                + addq
                + " ORDER BY Nama, ItemID";
            
            DataTable rs = new DataTable();
            Db.Fill(rs, strSql);    
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

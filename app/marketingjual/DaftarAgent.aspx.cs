using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
	public partial class DaftarAgent : System.Web.UI.Page
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			if(!Page.IsPostBack)
            {
                Cf.SetGrid(tb);

                if (Request.QueryString["status"]==null)
					metode.SelectedIndex = 0;
				else if(Request.QueryString["status"]=="a")
					metode.SelectedIndex = 1;
				else if(Request.QueryString["status"]=="i")
					metode.SelectedIndex = 2;

				if(metode.SelectedIndex!=0) metode.Enabled = false;
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
			if(metode.SelectedIndex==1)
				addq = " AND Status = 'A'";
			else if(metode.SelectedIndex==2)
				addq = " AND Status = 'I'";

            string Agent = "";
            if (Request.QueryString["status"] == "dari" || Request.QueryString["status"] == "sampai")
            {
                Agent = "'<a href=\"javascript:callSource(''' + CONVERT(VARCHAR(10), NoAgent) + ''', ''" + Request.QueryString["status"] + "'')\">"
                    + "' + Nama + '"
                    + " (' + CONVERT(VARCHAR(10), NoAgent) + ')"
                    + "</a>' AS Nama"
                    ;
            }
            else
            {
                Agent = "'<a href=\"javascript:call(''' + CONVERT(VARCHAR(10), NoAgent) + ''')\">"
                    + "' + Nama + '"
                    + " (' + CONVERT(VARCHAR(10), NoAgent) + ')"
                    + "</a>' AS Nama"
                    ;
            }

            string strSql = "SELECT "
                + Agent
                + ",Principal"
				+ " FROM MS_AGENT "
				+ " WHERE Nama + Principal"
				+ " LIKE '%" + Cf.Str(keyword.Text) +"%'"
				+ addq
				+ " ORDER BY Nama, NoAgent";

            DataTable rs = new DataTable();            

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

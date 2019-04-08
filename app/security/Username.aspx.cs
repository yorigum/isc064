using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.SECURITY
{
	public partial class Username : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();
            Cf.SetGrid(tb);
            if (!Page.IsPostBack)
			{
				Js.Focus(this,display);
				Bind();
			}
		}

		private void Bind()
		{
			string strSql = "SELECT * FROM SECLEVEL ORDER BY Kode";
			DataTable rs = Db.Rs(strSql);
			for(int i=0;i<rs.Rows.Count;i++)
			{
				string v = rs.Rows[i]["Kode"].ToString();
				string t = v + " - " + rs.Rows[i]["Nama"];
				seclevel.Items.Add(new ListItem(t,v));
			}
		}

        private void Fill()
        {
            string addq = "";
            if (seclevel.SelectedIndex != 0)
                addq = " AND SecLevel = '" + seclevel.SelectedValue + "'";

			string status = "";
			if(aktif.Checked) status = "A";
			if(blokir.Checked) status = "B";

			addq = addq + " AND Status = '" + status + "'";

            string nav = "'<a href=\"javascript:popEditUser('''+UserID+''')\">' +Nama+ '</a>'";
            string strSql = "SELECT "
                + nav
                + " AS Nama"
                + ",UserID AS Kode"
                + ",SecLevel"
                + ",CASE "
                + "	WHEN CONVERT(varchar,DATEADD(yy,1,TglLogin),112) <= CONVERT(varchar,getdate(),112) "
                + "		THEN CONVERT(varchar,TglLogin,106) + ' *' "
                + "		ELSE CONVERT(varchar,TglLogin,106)"
                + " END AS TglLogin"
                + ",CASE "
                + "	WHEN RotasiPass <> 0 AND CONVERT(varchar,DATEADD(m,RotasiPass,TglPass),112) <= CONVERT(varchar,getdate(),112) "
                + "		THEN CONVERT(varchar,TglPass,106) + ' **' "
                + "		ELSE CONVERT(varchar,TglPass,106)"
                + " END AS TglPass"
                + ",TglPass"
                + ",RotasiPass"
                + " FROM USERNAME WHERE 1=1"
                + addq
                + " ORDER BY Nama, UserID";

            DataTable rs = Db.Rs(strSql);
            tb.DataSource = rs;
            tb.DataBind();
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

        protected void tb_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            tb.PageIndex = e.NewPageIndex;
            Fill();
        }
    }
}

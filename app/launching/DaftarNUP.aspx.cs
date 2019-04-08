using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LAUNCHING
{
	public partial class DaftarNUP : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			Js.ConfirmKeyword(this,keyword);

			if(!Page.IsPostBack)
			{
			}
		}

		protected void search_Click(object sender, System.EventArgs e)
		{
			Fill();
		}

		private void Fill()
		{
			string addq = "";
            //if(metode.SelectedIndex==1)
            //    addq = " AND a.Status = 1";
            //else if(metode.SelectedIndex==2)
            //    addq = " AND a.Status = 0";

			string strSql = "SELECT "
				+ " a.*"
				+ ",b.Nama AS csNama"
                + " FROM MS_NUP a"
                + " INNER JOIN MS_CUSTOMER b ON a.NoCustomer = b.NoCustomer "
                + " WHERE b.Nama"
				+ " LIKE '%" + Cf.Str(keyword.Text) +"%'"
				+ addq
                + " ORDER BY a.NoNUP";

			DataTable rs = Db.Rs(strSql);
			Rpt.NoData(rpt, rs, "Tidak ditemukan data NUP dengan keyword diatas.");

			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;
				
				TableRow r = new TableRow();
				TableCell c;

                c = new TableCell();
                c.Text = rs.Rows[i]["NoNUP"].ToString();
                r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["csNama"].ToString();
				r.Cells.Add(c);

                string namaAgent = Db.SingleString("SELECT ISNULL(Nama,'') FROM MS_AGENT WHERE NoAgent=" + Convert.ToInt32(rs.Rows[i]["NoAgent"]));
                c = new TableCell();
                c.Text = namaAgent;
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

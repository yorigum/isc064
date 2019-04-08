using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL {
	public partial class DaftarVA : System.Web.UI.Page {

		protected void Page_Load(object sender, System.EventArgs e) {
			Act.Pass();
			Act.NoCache();

			Js.ConfirmKeyword(this, keyword);

			if (!Page.IsPostBack) {
			}
		}

		protected void search_Click(object sender, System.EventArgs e) {
			Fill();
		}

		private void Fill() {


			string strSql = "SELECT a.* FROM " + Mi.DbPrefix + "FINANCEAR..REF_VA a"
				+ " WHERE NoVA NOT IN (SELECT NoVA FROM MS_KONTRAK)"
                + " AND NoUnit = '" + NoStock + "'"
				+ " AND NoVA + Bank LIKE '%" + Cf.Str(keyword.Text) + "%'";

			DataTable rs = Db.Rs(strSql);
			Rpt.NoData(rpt, rs, "Tidak ditemukan data virtual account dengan keyword diatas.");

			for (int i = 0; i < rs.Rows.Count; i++) {
				if (!Response.IsClientConnected) break;

				TableRow r = new TableRow();
				TableCell c;

				c = new TableCell();
				c.Text = "<a href=\"javascript:call('" + rs.Rows[i]["NoVA"] + "')\">"
					+ rs.Rows[i]["NoVA"].ToString()
					+ "</a>"
					;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Bank"].ToString();
				r.Cells.Add(c);

				Rpt.Border(r);
				rpt.Rows.Add(r);
			}
		}

        private string NoStock
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoStock"]);
            }
        }

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e) {
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
		private void InitializeComponent() {

		}
		#endregion
	}
}

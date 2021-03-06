using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.ADMINJUAL
{
	public partial class PetaDetil : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			dasar.ImageUrl = "/marketingjual/FP/Base/"+f+".jpg";

			Fill();
		}

		private void Fill()
		{
			string strSql = "SELECT DISTINCT"
				+ " NoUnit"
				+ ",Koordinat"
				+ " FROM MS_UNIT"
				+ " WHERE Peta = '" + f + "'"
				+ " ORDER BY NoUnit";
			
			DataTable rs = Db.Rs(strSql);
			Rpt.NoData(rpt, rs, "Floor plan ini belum memiliki koordinat.");

			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				TableRow r = new TableRow();
				TableCell c;

				c = new TableCell();
				c.Text = rs.Rows[i]["NoUnit"].ToString();
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Cut(rs.Rows[i]["Koordinat"],100);
				r.Cells.Add(c);

				Rpt.Border(r);
				rpt.Rows.Add(r);
			}
		}

		private string f
		{
			get
			{
				return Cf.Pk(Request.QueryString["f"]);
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

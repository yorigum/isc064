using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.SECURITY
{
	public partial class SecLevelAkses : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			if(!Page.IsPostBack)
			{
				Fill();
			}
		}

		private void Fill()
		{
			DataTable rs = Db.Rs("SELECT PAGE.Modul, PAGE.Nama, PAGE.Halaman"
				+ " FROM "
				+ " PAGESEC INNER JOIN PAGE ON PAGESEC.Halaman = PAGE.Halaman "
				+ " WHERE Kode = '"+Kode+"'"
				+ " ORDER BY Modul, Nama"
				);

			Rpt.NoData(rpt, rs, "Hak akses untuk security level ini masih kosong.");

			string modul = "";
			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				TableRow r;
				TableCell c;

				if(modul!=rs.Rows[i]["Modul"].ToString())
				{
					r = new TableRow();
					
					c = new TableCell();
					c.Text = "<br>" + rs.Rows[i]["Modul"].ToString();
					c.Font.Bold = true;
					c.Font.Size = 10;
					c.ColumnSpan = 2;
					r.Cells.Add(c);
					
					Rpt.Border(r);
					rpt.Rows.Add(r);

					modul = rs.Rows[i]["Modul"].ToString();
				}

				r = new TableRow();

				c = new TableCell();
				c.Text = rs.Rows[i]["Nama"].ToString();
				r.Cells.Add(c);

				string namaFile = rs.Rows[i]["Halaman"].ToString();
				int index = namaFile.LastIndexOf("\\");
				if(index!=-1)
				{
					namaFile = namaFile.Substring(index+1,namaFile.Length-index-1);
				}

				c = new TableCell();
				c.Text = namaFile;
				c.Font.Size = 8;
				r.Cells.Add(c);
				
				Rpt.Border(r);
				r.Cells[0].Attributes["style"] = r.Cells[0].Attributes["style"]
					+ ";padding-left:30px";
				rpt.Rows.Add(r);
			}
		}

		private string Kode
		{
			get
			{
				return Cf.Pk(Request.QueryString["Kode"]);
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

//===========================================================================
// This file was modified as part of an ASP.NET 2.0 Web project conversion.
// The class name was changed and the class modified to inherit from the abstract base class 
//// in file 'App_Code\Migrated\Stub_Peta_aspx_cs.cs'.
// During runtime, this allows other classes in your web application to bind and access 
// the code-behind page using the abstract base class.
// The associated content page 'Peta.aspx' was also modified to refer to the new class name.
// For more information on this code pattern, please refer to http://go.microsoft.com/fwlink/?LinkId=46995 
//===========================================================================
using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.SETTINGS
{
	public partial class Peta : System.Web.UI.Page
    {
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			Fill();

			if(Request.QueryString["done"]!=null)
			{
				feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
					+ "Proses Berhasil...";
			}
		}

		private void Fill()
		{
			string path = Request.PhysicalApplicationPath.Replace("settings","marketing")
				+ "FP\\Base\\";
			string[] file = System.IO.Directory.GetFiles(path,"*.jpg");

			if(file.GetUpperBound(0)==-1)
			{
				TableRow r = new TableRow();
				TableCell c;
				c = new TableCell();
				c.Text = "Floor plan belum tersedia.";
				r.Cells.Add(c);
				rpt.Rows.Add(r);
			}

			for(int i=0;i<=file.GetUpperBound(0);i++)
			{
				if(!Response.IsClientConnected) break;

				string f = System.IO.Path.GetFileNameWithoutExtension(file[i]);

				TableRow r = new TableRow();
				TableCell c;

				c = new TableCell();
				c.Text = "<a href=\"javascript:call('"+f+"')\">" + f + "</a>";
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = "<a href=\"javascript:hapus('"+f+"')\">Delete...</a>";
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

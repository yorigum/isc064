using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ISC064.MARKETINGJUAL
{
	public partial class PetaBesar : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			string Src = Request.PhysicalApplicationPath + Request.QueryString["f"].Replace("/","\\");

			if(File.Exists(Src))
			{
				Response.Clear();
				Response.ContentType = "image/png";

				System.Drawing.Image original = System.Drawing.Image.FromFile(Src);

				int newHeight = original.Height;
				int newWidth = original.Width;

				Bitmap thumb = new Bitmap(newWidth, newHeight);
				MemoryStream stmMemory = new MemoryStream();

				Graphics gr = Graphics.FromImage(thumb);
				gr.DrawImage(original, 0, 0, newWidth, newHeight);

				thumb.Save(stmMemory, ImageFormat.Png);
				stmMemory.WriteTo(Response.OutputStream);

				gr.Dispose();
				thumb.Dispose();
				original.Dispose();

				Response.End();
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

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ISC064.MARKETINGJUAL
{
	public partial class PetaKecil : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			int maxsize = 300;
			try
			{
				maxsize = Convert.ToInt32(Request.QueryString["size"]);
			}
			catch{}
			string Src = Request.PhysicalApplicationPath + Request.QueryString["f"].Replace("/","\\");

			if(File.Exists(Src))
			{
				Response.Clear();
				Response.ContentType = "image/png";

				System.Drawing.Image original = System.Drawing.Image.FromFile(Src);

				int height = original.Height;
				int width = original.Width;

				int newHeight = height;
				int newWidth = width;
				
				if(height==width)
				{
					//kubus
					if(height>maxsize)
					{
						newWidth = maxsize;
						newHeight = maxsize;
					}
				}
				else if(height>width)
				{
					//portrait
					if(height>maxsize)
					{
						newHeight = maxsize;
						newWidth = Convert.ToInt32(
							(decimal)width * ((decimal)maxsize/Convert.ToDecimal(height)));
					}
				}
				else if(width>height)
				{
					//landscape
					if(width>maxsize)
					{
						newWidth = maxsize;
						newHeight = Convert.ToInt32(
							(decimal)height * ((decimal)maxsize/Convert.ToDecimal(width)));
					}
				}

				if(newWidth==0)newWidth=1;
				if(newHeight==0)newHeight=1;

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

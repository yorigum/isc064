using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.ADMINJUAL
{
	public partial class AgentFoto : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			Act.CekInt("NoAgent");

			if(!Page.IsPostBack)
			{
				Fill();
			}

			Js.Confirm(this, "Save file gambar baru?\\nPerhatian bahwa file lama akan di-overwrite.");
			FeedBack();
		}

		private void FeedBack()
		{
			feed.Text = "";
			if(!Page.IsPostBack)
			{
				if(Request.QueryString["done"]!=null)
					feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
						+ "Edit Foto Berhasil...";
			}
		}

		private void Fill()
		{
			if(System.IO.File.Exists(Request.PhysicalApplicationPath
				+ "Foto\\" + NoAgent.ToString().PadLeft(5,'0') + ".jpg"))
				foto.ImageUrl = "Foto/" + NoAgent.ToString().PadLeft(5,'0') + ".jpg";
			else
				foto.Visible = false;
		}

		protected void upload_Click(object sender, System.EventArgs e)
		{
			if(!file.PostedFile.FileName.EndsWith(".jpg"))
			{
				Js.Alert(
					this
					, "Proses Upload Gagal.\\n"
					+ "File yang boleh di-upload adalah file dengan extension .jpg saja."
					, ""
					);
			}
			else
			{
				string path = Request.PhysicalApplicationPath
					+ "Foto\\" + NoAgent.PadLeft(5,'0') + ".jpg";

				Dfc.UploadFile(".jpg",path,file);

				//Logfile
				DataTable rs = Db.Rs("SELECT "
					+ " NoAgent AS [No. Agent]"
					+ ",Nama AS [Nama]"
					+ " FROM MS_AGENT"
					+ " WHERE NoAgent = " + NoAgent
					);

				string Ket = Cf.LogCapture(rs);

				Db.Execute("EXEC spLogAgent"
					+ " 'FOTO'"
					+ ",'" + Act.UserID + "'"
					+ ",'" + Act.IP + "'"
					+ ",'" + Ket + "'"
					+ ",'" + NoAgent.PadLeft(5,'0') + "'"
					);

				Response.Redirect("AgentFoto.aspx?done=1&NoAgent="+NoAgent);
			}
		}

		private string NoAgent
		{
			get
			{
				return Cf.Pk(Request.QueryString["NoAgent"]);
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

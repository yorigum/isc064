using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.FINANCEAR
{
	public partial class BGUploadTitip : System.Web.UI.Page
	{
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			Js.Confirm(this,"Lanjutkan proses upload data pengelola cek giro?");
			feed.Text = "";
		}

		protected void upload_Click(object sender, System.EventArgs e)
		{
			if(!file.PostedFile.FileName.EndsWith(".xls"))
			{
				Js.Alert(
					this
					, "Proses Upload Gagal.\\n"
					+ "File yang boleh di-upload adalah file dengan extension .xls saja."
					, ""
					);
			}
			else
			{
				string path = Request.PhysicalApplicationPath
					+ "Template\\Titip_" + Session.SessionID + ".xls";
				
				Dfc.UploadFile(".xls",path,file);

				Cek(path);
				
				//Hapus file sementara tersebut dari hard-disk server
				Dfc.DeleteFile(path);
			}
		}

		private void Cek(string path)
		{
			string strSql = "SELECT * FROM [TitipBG$]";
			DataTable rs = new DataTable();
            
			try
			{
				rs = Db.xls(strSql,path);
			}
			catch {}


			if(Rpt.ValidateXls(rs, rule, gagal))
				Save(path);
		}
        
		private void Save(string path)
		{
			int total = 0;

			string strSql = "SELECT * FROM [TitipBG$]";
			DataTable rs = Db.xls(strSql,path);

			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				if(Save(rs,i))
					total++;
			}

			feed.Text = "<img src='/Media/db.gif' align=absmiddle>"
				+ "Upload Berhasil  : " + total + " baris data";
		}

		private bool Save(DataTable rs, int i)
		{
			string NoBG = Cf.Pk(rs.Rows[i][0]);
			string Titip = Cf.Str(rs.Rows[i][1]);
			
			//Jika data NA maka diisi dengan string kosong saja di dalam database
			if(Titip=="NA") Titip = "";

			//execute
			Db.Execute("UPDATE MS_TTS SET Titip = '"+Titip+"' WHERE NoBG = '"+NoBG+"'");
				
			DataTable rsLog = Db.Rs("SELECT NoTTS FROM MS_TTS WHERE NoBG = '"+NoBG+"'");
			for(int ix=0;ix<rsLog.Rows.Count;ix++)
			{
				int NoTTS = (int)rsLog.Rows[ix]["NoTTS"];

				DataTable rsHeader = Db.Rs("SELECT "
					+ " NoTTS AS [No. TTS]"
					+ ",Tipe"
					+ ",Ref AS [Ref.]"
					+ ",Unit"
					+ ",Customer"
					+ ",Total AS [Nilai TTS]"
					+ ",NoBG AS [No. BG]"
					+ ",Titip AS [Pengelola BG]"
					+ " FROM MS_TTS"
					+ " WHERE NoTTS = " + NoTTS
					);

				//Logfile
				string ketlog = Cf.LogCapture(rsHeader);

				Db.Execute("EXEC spLogTTS"
					+ " 'EDIT'"
					+ ",'" + Act.UserID + "'"
					+ ",'" + Act.IP + "'"
					+ ",'" + ketlog + "'"
					+ ",'" + NoTTS.ToString().PadLeft(7,'0') + "'"
					);

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_TTS_LOG ORDER BY LogID DESC");
                string Project = Db.SingleString("SELECT Project FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = (SELECT Ref FROM MS_TTS WHERE NoTTS = '" + NoTTS + "')");
                Db.Execute("UPDATE MS_TTS_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

            }

            return true;
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

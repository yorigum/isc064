using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.ADMINJUAL
{
	public partial class UnitUploadZone : System.Web.UI.Page
	{
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			Js.Confirm(this,"Lanjutkan proses upload data zoning unit properti?");
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
					+ "Template\\Zoning_" + Session.SessionID + ".xls";
				
				Dfc.UploadFile(".xls",path,file);

				Cek(path);
				
				//Hapus file sementara tersebut dari hard-disk server
				Dfc.DeleteFile(path);
				
			}
		}

		private void Cek(string path)
		{
			string strSql = "SELECT * FROM [Zoning$]";
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
			System.Text.StringBuilder log = new System.Text.StringBuilder();
			int total = 0;

			string strSql = "SELECT * FROM [Zoning$]";
			DataTable rs = Db.xls(strSql,path);

			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				if(Save(rs,i))
				{
					total++;
				}
			}

			feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
				+ "Upload Berhasil  : " + total + " baris data";
		}

		private bool Save(DataTable rs, int i)
		{
			string Unit = Cf.Str(rs.Rows[i][0]);

			if(Db.SingleInteger("SELECT COUNT(*) FROM MS_UNIT WHERE NoUnit = '" + Unit + "'")!=0)
			{
				string NoStock = Db.SingleString("SELECT NoStock FROM MS_UNIT WHERE NoUnit = '" + Unit + "'");
				string Zoning = Cf.Str(rs.Rows[i][1]);
				
				DataTable rsBef = Db.Rs("SELECT "
					+ " Zoning "
					+ " FROM MS_UNIT "
					+ " WHERE NoStock = '" + NoStock + "'");

				Db.Execute("UPDATE MS_UNIT "
					+ " SET Zoning = '" + Zoning + "'"
					+ " WHERE NoStock = '" + NoStock + "'");

				DataTable rsAft = Db.Rs("SELECT "
					+ " Zoning "
					+ " FROM MS_UNIT "
					+ " WHERE NoStock = '" + NoStock + "'");

				string Ket = "*** UPLOAD ZONING<br>Unit : " + Unit + "<br>"
					+ Cf.LogCompare(rsBef,rsAft);

				Db.Execute("EXEC spLogUnit"
					+ " 'EDIT'"
					+ ",'" + Act.UserID + "'"
					+ ",'" + Act.IP + "'"
					+ ",'" + Ket + "'"
					+ ",'" + NoStock + "'"
					);

				return true;
			}
			else
				return false;
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

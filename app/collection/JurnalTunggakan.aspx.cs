using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.COLLECTION
{
	public partial class JurnalTunggakan : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			Act.CekInt("NoTunggakan");

			Fill();
			FeedBack();

			Js.Confirm(this, "Simpan aktivitas baru ke dalam jurnal?");
		}

		private void FeedBack()
		{
			feed.Text = "";
			if(!Page.IsPostBack)
			{
				if(Request.QueryString["done"]!=null)
					feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
						+ "Simpan Jurnal Berhasil...";
			}
		}

		private void Fill()
		{
			string strSql = "SELECT *"
				+ ",(SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..USERNAME WHERE UserID = MS_TUNGGAKAN_JURNAL.UserID) AS Nama"
				+ " FROM MS_TUNGGAKAN_JURNAL"
				+ " WHERE NoTunggakan = " + NoTunggakan
				+ " ORDER BY JurnalID";
			
			DataTable rs = Db.Rs(strSql);
			Rpt.NoData(rpt, rs, "Jurnal untuk pemberitahuan jatuh tempo tersebut masih kosong.");

			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				TableRow r = new TableRow();
				TableCell c;

				c = new TableCell();
				c.Text = Cf.Day(rs.Rows[i]["Tgl"]);
				c.Wrap = false;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Time(rs.Rows[i]["Tgl"]);
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["UserID"].ToString();
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Nama"].ToString();
				c.Wrap = false;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Ket"].ToString();
				r.Cells.Add(c);

				c = new TableCell();
				if(System.IO.File.Exists(Request.PhysicalApplicationPath
					+ "JurnalTunggakan\\" + rs.Rows[i]["JurnalID"] + ".jpg"))
					c.Text = "<a href=\"javascript:popGambar('JurnalTunggakan/"+rs.Rows[i]["JurnalID"]+".jpg')\">View</a>";
				else
					c.Text = "&nbsp;";
				r.Cells.Add(c);

				Rpt.Border(r);
				rpt.Rows.Add(r);
			}
		}

		protected void save_Click(object sender, System.EventArgs e)
		{
			if(file.PostedFile.FileName.Length!=0
				&& !file.PostedFile.FileName.EndsWith(".jpg"))
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
				string Ket = Cf.Str(akt.SelectedValue) + "<br>" + Cf.Str(baru.Text);
			
				Db.Execute("EXEC spJurnalTunggakan "
					+ " '" + Act.UserID + "'"
					+ ",'" + NoTunggakan + "'"
					+ ",'" + Ket + "'"
					);

				if(file.PostedFile.FileName.Length!=0)
				{
					long JurnalID = Db.SingleLong("SELECT TOP 1 JurnalID FROM MS_TUNGGAKAN_JURNAL ORDER BY JurnalID DESC");
					string path = Request.PhysicalApplicationPath
						+ "JurnalTunggakan\\" + JurnalID + ".jpg";
					Dfc.UploadFile(".jpg",path,file);
				}

				Response.Redirect("JurnalTunggakan.aspx?done=1&NoTunggakan="+NoTunggakan);
			}
		}

		private string NoTunggakan
		{
			get
			{
				return Cf.Pk(Request.QueryString["NoTunggakan"]);
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

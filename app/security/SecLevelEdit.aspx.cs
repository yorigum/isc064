using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.SECURITY
{
	public partial class SecLevelEdit : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			if(!Act.Sec("ED:"+Request.PhysicalPath))
			{
				ok.Enabled = false;
				save.Enabled = false;
			}

			if(!Page.IsPostBack)
			{
				Fill();
			}

			FeedBack();
		}

		private void FeedBack()
		{
			feed.Text = "";
			if(!Page.IsPostBack)
			{
				if(Request.QueryString["done"]!=null)
					feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
						+ "Edit Berhasil...";
			}
		}

		private void Fill()
		{
			btnlog.Attributes["onclick"] = "location.href='LogPk.aspx?Tb=SECLEVEL_LOG&Pk="+Kode+"'";
			btndel.Attributes["onclick"] = "location.href='SecLevelDel.aspx?Kode="+Kode+"'";
			
			string strSql = "SELECT * FROM SECLEVEL WHERE Kode = '" + Kode + "'";
			DataTable rs = Db.Rs(strSql);

			if(rs.Rows.Count==0)
				Response.Redirect("/CustomError/Deleted.html");
			else
			{
				kode.Text = rs.Rows[0]["Kode"].ToString();
				nama.Text = rs.Rows[0]["Nama"].ToString();

				FillUser();
			}
		}

		private void FillUser()
		{
			System.Text.StringBuilder x = new System.Text.StringBuilder();

			DataTable rs = Db.Rs("SELECT Nama,UserID FROM USERNAME WHERE SecLevel = '"+Kode+"' ORDER BY Nama,UserID");
			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				x.Append("<li><a href='EditUser.aspx?UserID="+rs.Rows[i]["UserID"]+"'>"
					+ rs.Rows[i]["Nama"] + " ("+rs.Rows[i]["UserID"]+")</a></li>");
			}

			daftaruser.InnerHtml = x.ToString();
		}

		private bool unik()
		{
			bool x = true;

			int c = Db.SingleInteger("SELECT COUNT(*) FROM SECLEVEL WHERE"
				+ " Kode <> '" + Kode + "'"
				+ " AND Kode = '" + Cf.Pk(kode.Text) + "'"
				);

			if(c!=0)
				x = false;

			return x;
		}

		private bool valid()
		{
			string s = "";
			bool x = true;

			//kode
			kode.Text = Cf.Pk(kode.Text);

			if(Cf.isEmpty(kode))
			{
				x = false;
				if(s=="") s = kode.ID;
				kodec.Text = "Kosong";
			}
			else
			{
				if(!unik())
				{
					x = false;
					if(s=="") s = kode.ID;
					kodec.Text = "Duplikat";
				}
				else
					kodec.Text = "";
			}

			if(Cf.isEmpty(nama))
			{
				x = false;
				if(s=="") s = nama.ID;
				namac.Text = "Kosong";
			}
			else
				namac.Text = "";

			if(!x)
				Js.Alert(
					this
					, "Input Tidak Valid.\\n\\n"
					+ "Aturan Proses :\\n"
					+ "1. Kode harus diisi dan tidak boleh duplikat.\\n"
					+ "2. Nama harus diisi.\\n"
					, "document.getElementById('"+s+"').focus();"
					+ "document.getElementById('"+s+"').select();"
					);

			return x;
		}

		private bool Save()
		{
			if(valid())
			{
				string KodeBaru = Cf.Pk(kode.Text);
				string Nama = Cf.Str(nama.Text);

				DataTable rsBef = Db.Rs("SELECT "
					+ " Kode"
					+ ",Nama"
					+ " FROM SECLEVEL "
					+ " WHERE Kode = '" + Kode + "'");

				Db.Execute("EXEC spSecLevelEdit"
					+ " '" + Kode + "'"
					+ ",'" + KodeBaru + "'"
					+ ",'" + Nama + "'"
					);
				
				DataTable rsAft = Db.Rs("SELECT "
					+ " Kode"
					+ ",Nama"
					+ " FROM SECLEVEL "
					+ " WHERE Kode = '" + KodeBaru + "'");

				string KetLog = Cf.LogCompare(rsBef, rsAft);

				Db.Execute("EXEC spLogSeclevel"
					+ " 'EDIT'"
					+ ",'" + Act.UserID + "'"
					+ ",'" + Act.IP + "'"
					+ ",'" + KetLog + "'"
					+ ",'" + KodeBaru + "'"
					);

				return true;
			}
			else
				return false;
		}

		protected void ok_Click(object sender, System.EventArgs e)
		{
			if(Save()) Js.Close(this);
		}

		protected void save_Click(object sender, System.EventArgs e)
		{
			string KodeBaru = Cf.Pk(kode.Text);
			if(Save()) Response.Redirect("SecLevelEdit.aspx?Kode=" + KodeBaru + "&done=1");
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

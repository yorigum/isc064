using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.SETTINGS
{
	public partial class Aktivasi : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			if(!Page.IsPostBack)
			{
				Js.Focus(this,userid);
				userid.Attributes["ondblclick"] = "popDaftarBlokir();";
				frm.Visible = false;
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
						+ "<a href=\"javascript:popEditUser('"+Request.QueryString["done"]+"')\">"
						+ "Aktivasi Berhasil..."
						+ "</a>";
			}
		}

		private bool valid()
		{
			bool x = true;

			int c = Db.SingleInteger(
				"SELECT COUNT(*) FROM USERNAME WHERE UserID = '" + UserID + "' AND Status = 'B'");

			if(c==0)
				x = false;

			if(!x)
				Js.Alert(
					this
					, "Username Tidak Valid.\\n\\n"
					+ "Kemungkinan Sebab :\\n"
					+ "1. Username tersebut tidak terdaftar.\\n"
					+ "2. Username tersebut tidak diblokir.\\n"
					, "document.getElementById('userid').focus();"
					+ "document.getElementById('userid').select();"
					);

			return x;
		}

		protected void next_Click(object sender, System.EventArgs e)
		{
			if(valid())
			{
				pilih.Visible = false;
				frm.Visible = true;

				Fill();
			}
		}

		private void Fill()
		{
			string strSql = "SELECT * FROM USERNAME WHERE UserID = '" + UserID + "'";
			DataTable rs = Db.Rs(strSql);

			if(rs.Rows.Count==0)
				Response.Redirect("/CustomError/Deleted.html");
			else
			{
				useridl.Text = rs.Rows[0]["UserID"].ToString();
				nama.Text = rs.Rows[0]["Nama"].ToString();
				seclevel.Text = rs.Rows[0]["SecLevel"].ToString();
			}
		}

		protected void save_Click(object sender, System.EventArgs e)
		{
			Db.Execute("EXEC spUserAktivasi "
				+ " '" + UserID + "'"
				+ ",'" + Act.IP + "'"
				);

			DataTable rs = Db.Rs("SELECT "
				+ " UserID AS [Kode / Username]"
				+ ",Nama AS [Nama Lengkap]"
				+ ",SecLevel AS [Security Level]"
				+ " FROM USERNAME WHERE UserID = '" + UserID + "'");
			
			Db.Execute("EXEC spLogUsername "
				+ " 'AU'"
				+ ",'" + Act.UserID + "'"
				+ ",'" + Act.IP + "'"
				+ ",'" + Cf.LogCapture(rs) + "'"
				+ ",'" + UserID + "'"
				);

			Response.Redirect("Aktivasi.aspx?done="+UserID);
		}

		private string UserID
		{
			get
			{
				return Cf.Pk(userid.Text);
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

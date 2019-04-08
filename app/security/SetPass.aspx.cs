using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.SECURITY
{
	public partial class SetPass : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			if(!Page.IsPostBack)
			{
				Js.Focus(this,userid);
				userid.Attributes["ondblclick"] = "popDaftarAktif();";
				frm.Visible = false;
			}

			FeedBack();
			if(frm.Visible) md5();
		}

		private void md5()
		{
			RegisterOnSubmitStatement(
				"submitScript"
				, "document.getElementById('passMD5').value=hex_md5(document.getElementById('pass').value);"
				);
		}

		private void FeedBack()
		{
			feed.Text = "";
			if(!Page.IsPostBack)
			{
				if(Request.QueryString["done"]!=null)
					feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
						+ "<a href=\"javascript:popEditUser('"+Request.QueryString["done"]+"')\">"
						+ "Set Password Baru Berhasil..."
						+ "</a>";
			}
		}

		private bool valid()
		{
			bool x = true;

			int c = Db.SingleInteger(
				"SELECT COUNT(*) FROM USERNAME WHERE UserID = '" + UserID + "' AND Status = 'A'");

			if(c==0)
				x = false;

			if(!x)
				Js.Alert(
					this
					, "Username Tidak Valid.\\n\\n"
					+ "Kemungkinan Sebab :\\n"
					+ "1. Username tersebut tidak terdaftar.\\n"
					+ "2. Username tersebut telah diblokir.\\n"
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

				//set password baru
				Js.Focus(this,pass);
				pass.Text = Cf.Random(5);
				md5();
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

		private bool passvalid()
		{
			bool x = true;

			if(Cf.isEmpty(pass))
			{
				x = false;
				passc.Text = "Kosong";
			}
			else
				passc.Text = "";

            string Password = pass.Text;
            if (Password.Length < 8)
            {
                x = false;
                passc.Text = "Password baru harus tediri dari minimal 8 karakter.";
            }

			return x;
		}

		protected void save_Click(object sender, System.EventArgs e)
		{
			if(passvalid())
			{
				Db.Execute("EXEC spGantiPass "
					+ " '" + UserID + "'"
					+ ",'" + passMD5.Text + "'"
					+ ", " + Cf.BoolToSql(gantipass.Checked)
					);

				DataTable rs = Db.Rs("SELECT "
					+ " UserID AS [Kode / Username]"
					+ ",Nama AS [Nama Lengkap]"
					+ ",SecLevel AS [Security Level]"
					+ ",GantiPass AS [Rubah Password di Login Berikutnya]"
					+ " FROM USERNAME WHERE UserID = '" + UserID + "'");

				Db.Execute("EXEC spLogUsername "
					+ " 'SPB'"
					+ ",'" + Act.UserID + "'"
					+ ",'" + Act.IP + "'"
					+ ",'" + Cf.LogCapture(rs) + "'"
					+ ",'" + UserID + "'"
					);

				Response.Redirect("SetPass.aspx?done="+UserID);
			}
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

using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.SECURITY
{
	public partial class Pendaftaran : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			if(!Page.IsPostBack)
			{
				Js.Focus(this,userid);

				bindSecLevel();
                bindAgent();

				//Password awal
				pass.Text = Cf.Random(5);
				
				//Daftar 10 terbaru
				Fill();
			}

			FeedBack();

			ClientScript.RegisterOnSubmitStatement(
                GetType()
				,"submitScript"
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
						+ "Pendaftaran Berhasil..."
						+ "</a>";
			}
		}

		private void bindSecLevel()
		{
			string strSql = "SELECT * FROM SECLEVEL ORDER BY Kode";
			DataTable rs = Db.Rs(strSql);
			for(int i=0;i<rs.Rows.Count;i++)
			{
				string v = rs.Rows[i]["Kode"].ToString();
				string t = v + " - " + rs.Rows[i]["Nama"];
				seclevel.Items.Add(new ListItem(t,v));
			}
			seclevel.Rows = rs.Rows.Count;

			//supaya tidak terlalu panjang
			if(seclevel.Rows>10)
				seclevel.Rows = 10;

			seclevel.SelectedIndex = 0;
		}

        private void bindAgent()
        {
            DataTable rs = Db.Rs("SELECT Nama,Principal,NoAgent FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_AGENT WHERE Status = 'A'"
                + " ORDER BY Nama,NoAgent");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["NoAgent"].ToString();
                string t = rs.Rows[i]["Nama"].ToString();
                if (rs.Rows[i]["Principal"].ToString() != "")
                    t = t + " (" + rs.Rows[i]["Principal"] + ")";
                agent.Items.Add(new ListItem(t, v));
            }
        }

		private bool unik()
		{
			bool x = true;

			int c = Db.SingleInteger(
				"SELECT COUNT(*) FROM USERNAME WHERE UserID = '" + UserID + "'");

			if(c!=0)
				x = false;

			return x;
		}

		private bool valid()
		{
			string s = "";
			bool x = true;

			userid.Text = Cf.Pk(userid.Text);
			if(Cf.isEmpty(userid))
			{
				x = false;
				if(s=="") s = userid.ID;
				useridc.Text = "Kosong";
			}
			else
			{
				if(!unik())
				{
					x = false;
					if(s=="") s = userid.ID;
					useridc.Text = "Duplikat";
				}
				else
					useridc.Text = "";
			}
            
            if (Cf.isEmpty(nama))
			{
				x = false;
				if(s=="") s = nama.ID;
				namac.Text = "Kosong";
			}
			else
				namac.Text = "";

            if (!Cf.isEmail(email.Text))
            {
                x = false;
                if (s == "") s = email.ID;
                emailc.Text = "Format Email";
            }
            else
                emailc.Text = "";

            DataTable rs = Db.Rs("SELECT * FROM USERNAME WHERE UserID != '" + UserID + "' AND Email = '" + Cf.Str(email.Text) + "'");
            if (rs.Rows.Count > 0)
            {
                x = false;
                if (s == "") s = email.ID;
                emailc.Text = "Duplikat Email";
            }
            else
                emailc.Text = "";

            if (Cf.isEmpty(pass))
			{
				x = false;
				if(s=="") s = pass.ID;
				passc.Text = "Kosong";
			}
			else
				passc.Text = "";

			if(!Cf.isInt(rotasipass))
			{
				x = false;
				if(s=="") s = rotasipass.ID;
				rotasipassc.Text = "Angka Bulat";
			}
			else
				rotasipassc.Text = "";

			if(!x)
				Js.Alert(
					this
					, "Input Tidak Valid.\\n\\n"
					+ "Aturan Proses :\\n"
					+ "1. Kode tidak boleh kosong dan tidak boleh duplikat.\\n"
					+ "2. Nama Lengkap tidak boleh kosong.\\n"
					+ "3. Password Awal tidak boleh kosong.\\n"
					+ "4. Aturan Ganti Password harus berupa angka bulat saja.\\n"
					, "document.getElementById('"+s+"').focus();"
					+ "document.getElementById('"+s+"').select();"
					);

			return x;
		}
        protected bool filevalid()
        {
            bool x = true;
            string s = "";

            if (file.PostedFile.FileName.Length != 0
                && !file.PostedFile.FileName.EndsWith(".jpg"))
            {
                x = false;

                if (s == "")
                    s = file.ID;
            }

            if (!x)
            {
                Js.Alert(
                    this
                    , "Proses Upload Gagal.\\n"
                    + "File yang boleh di-upload adalah file dengan extension .jpg saja."
                    , "document.getElementById('" + s + "').focus();"
                    );
            }

            return x;
        }

        protected void save_Click(object sender, System.EventArgs e)
		{
            if (filevalid())
            {
                if (valid())
                {
                    if (pass.Text.Length < 8)
                    {
                        passc.Text = "Password baru harus tediri dari minimal 8 karakter.";
                        passc.ForeColor = Color.Red;
                    }
                    else
                    {
                        passc.Text = "";

                        string Nama = Cf.Str(nama.Text);
                        string SecLevel = seclevel.SelectedValue;
                        string Password = passMD5.Text;
                        string Email = email.Text;
                        int RotasiPass = Convert.ToInt32(rotasipass.Text);
                        int NoAgent = Convert.ToInt32(agent.SelectedValue);

                        Db.Execute("EXEC spUserDaftar"
                            + " '" + UserID + "'"
                            + ",'" + Password + "'"
                            + ",'" + Nama + "'"
                            + ",'" + SecLevel + "'"
                            + ", " + Cf.BoolToSql(gantipass.Checked)
                            + ", " + RotasiPass
                            );

                        if (file.PostedFile.FileName.Length != 0)
                        {
                            string path = Request.PhysicalApplicationPath + "Foto\\" + UserID + ".png";
                            Dfc.UploadFile(".jpg", path, file);
                            Db.Execute("UPDATE USERNAME SET Foto = '" + path + "' WHERE UserID = '" + UserID + "'");
                        }
                        Db.Execute("UPDATE USERNAME SET Email = '" + Email + "' WHERE UserID = '" + UserID + "'");

                        DataTable rs = Db.Rs("SELECT "
                            + " UserID AS [Kode / Username]"
                            + ",Nama AS [Nama Lengkap]"
                            + ",SecLevel AS [Security Level]"
                            + ",Email AS [Email]"
                            + ",Foto AS [Foto]"
                            + ",GantiPass AS [Rubah Password di Login Pertama]"
                            + ",RotasiPass AS [Frekuensi Rotasi Password (Bulanan)]"
                            + ",NoAgent AS [Kode Sales]"
                            + " FROM USERNAME WHERE UserID = '" + UserID + "'");

                        Db.Execute("EXEC spLogUsername"
                            + " 'PUB'"
                            + ",'" + Act.UserID + "'"
                            + ",'" + Act.IP + "'"
                            + ",'" + Cf.LogCapture(rs) + "'"
                            + ",'" + UserID + "'"
                            );

                        Response.Redirect("Pendaftaran.aspx?done=" + UserID);
                    }
                }
            }
		}

		private void Fill()
		{
			string strSql = "SELECT TOP 25 Nama, UserID"
				+ " FROM USERNAME "
				+ " ORDER BY TglInput DESC, Nama";

			DataTable rs = Db.Rs(strSql);
			for(int i=0;i<rs.Rows.Count;i++)
			{
				string v = rs.Rows[i]["UserID"].ToString();
				string t = rs.Rows[i]["Nama"] + " ("+v+")";

				baru.Items.Add(new ListItem(t,v));
			}

			if(rs.Rows.Count!=0)
			{
				baru.SelectedIndex = 0;
				baru.Attributes["ondblclick"] = "popEditUser("
					+"this.options[this.selectedIndex].value)";
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

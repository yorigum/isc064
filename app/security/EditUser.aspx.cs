using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.SECURITY
{
	public partial class EditUser : System.Web.UI.Page
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
				Bind(); //bind security level
                bindAgent(); //bind agent
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

		private void Bind()
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
			if(seclevel.Rows>10)
				seclevel.Rows = 10;
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

		private void Fill()
		{
			aKey.HRef = "javascript:openModal('EditKey.aspx?UserID=" + UserID + "','350','150')";
			btnlog.Attributes["onclick"] = "location.href='LogPk.aspx?Tb=USERNAME_LOG&Pk="+UserID+"'";
			btndel.Attributes["onclick"] = "location.href='DelUser.aspx?UserID="+UserID+"'";
			
			string strSql = "SELECT * FROM USERNAME WHERE UserID = '" + UserID + "'";
			DataTable rs = Db.Rs(strSql);

			if(rs.Rows.Count==0)
				Response.Redirect("/CustomError/Deleted.html");
			else
			{
				userid.Text = rs.Rows[0]["UserID"].ToString();
				nama.Text = rs.Rows[0]["Nama"].ToString();
                email.Text = rs.Rows[0]["Email"].ToString();

                seclevel.SelectedValue = rs.Rows[0]["SecLevel"].ToString();
                agent.SelectedValue = rs.Rows[0]["NoAgent"].ToString();

				gantipass.Checked = (bool)rs.Rows[0]["GantiPass"];
				rotasipass.Text = rs.Rows[0]["RotasiPass"].ToString();
				
				tglinput.Text = Cf.Date(rs.Rows[0]["TglInput"]);
				tgllogin.Text = Cf.Date(rs.Rows[0]["TglLogin"]);
				tglpass.Text = Cf.Date(rs.Rows[0]["TglPass"]);

				if(rs.Rows[0]["Status"].ToString()=="A")
				{
					status.ForeColor = Color.Green;
					status.Text = "Aktif";
				}
				else
				{
					status.ForeColor = Color.Red;
					status.Text = "Blokir";
				}
			}
		}

		private bool valid()
		{
			string s = "";
			bool x = true;

			if(Cf.isEmpty(nama))
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

            if (!Cf.isInt(rotasipass))
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
					+ "1. Nama Lengkap tidak boleh kosong.\\n"
					+ "2. Aturan Ganti Password harus berupa angka bulat saja.\\n"
                    + "3. Format email harus valid & unik.\\n"
                    , "document.getElementById('" +s+"').focus();"
					+ "document.getElementById('"+s+"').select();"
					);

			return x;
		}

		private bool Save()
		{
			if(valid())
			{
				string Nama = Cf.Str(nama.Text);
				int RotasiPass = Convert.ToInt32(rotasipass.Text);
                string Email = Cf.Str(email.Text);
                string SecLevel = seclevel.SelectedValue;
                int NoAgent = Convert.ToInt32(agent.SelectedValue);

				DataTable rsBef = Db.Rs("SELECT "
					+ " Nama AS [Nama Lengkap]"
				    + ",Email"
                    + ",SecLevel AS [Security Level]"
					+ ",GantiPass AS [Rubah Password di Login Pertama]"
					+ ",RotasiPass AS [Frekuensi Rotasi Password (Bulanan)]"
                    + ",NoAgent AS [Kode Sales]"
					+ " FROM USERNAME WHERE UserID = '" + UserID + "'");

				Db.Execute("EXEC spUserEdit "
					+ " '" + UserID + "'"
					+ ",'" + Nama + "'"
					+ ",'" + SecLevel + "'"
					+ ", " + Cf.BoolToSql(gantipass.Checked)
					+ ", " + RotasiPass
					);

                //Db.Execute("UPDATE USERNAME SET NoAgent = " + NoAgent + " WHERE UserID = '" + UserID + "'");                
                Db.Execute("UPDATE USERNAME SET Email = '" + Email + "' WHERE UserID = '" + UserID + "'");

                DataTable rsAft = Db.Rs("SELECT "
					+ " Nama AS [Nama Lengkap]"
                    + ",Email"
                    + ",SecLevel AS [Security Level]"
					+ ",GantiPass AS [Rubah Password di Login Pertama]"
					+ ",RotasiPass AS [Frekuensi Rotasi Password (Bulanan)]"
                    + ",NoAgent AS [Kode Sales]"
					+ " FROM USERNAME WHERE UserID = '" + UserID + "'");

				//Logfile
				string Ket = Cf.LogCompare(rsBef,rsAft);

				Db.Execute("EXEC spLogUsername "
					+ " 'EDU'"
					+ ",'" + Act.UserID + "'"
					+ ",'" + Act.IP + "'"
					+ ",'" + Ket + "'"
					+ ",'" + UserID + "'"
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
			if(Save()) Response.Redirect("EditUser.aspx?UserID="+UserID+"&done=1");
		}

		private string UserID
		{
			get
			{
				return Cf.Pk(Request.QueryString["UserID"]);
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

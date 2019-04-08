using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064
{
	public partial class Gateway : System.Web.UI.Page
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			RegisterOnSubmitStatement(
				"submitScript"
				, "document.getElementById('passold').value = hex_md5(document.getElementById('passold').value);"
				+ "document.getElementById('passbaru').value = hex_md5(document.getElementById('passbaru').value);"
				+ "document.getElementById('passconfirm').value = hex_md5(document.getElementById('passconfirm').value);"
				);

			if(!Page.IsPostBack)
			{
				comp.InnerHtml  = Mi.Pt;
				seclevel.Text = Act.SecLevel;

				nama.Text = Db.SingleString(
					"SELECT Nama FROM USERNAME WHERE UserID = '" + Act.UserID + "'");

				if(Session["LastLogin"]!=null)
					tglLogin.Text = Session["LastLogin"].ToString();

				Nav();

				if(Request.QueryString["login"]!=null)
				{
					Js.Focus(this, passold);
					cancel.Disabled = true;
					dariLogin.Checked = true;
				}

				namauser.Text = Db.SingleString(
					"SELECT Nama FROM USERNAME WHERE UserID = '" + Act.UserID + "'");

				if(Session["SalahPass"]==null)
					Session["SalahPass"] = "0"; //Hitung password salah berapa kali
				else
				{
					if(Session["SalahPass"].ToString()!="0")
						salah.Text = Session["SalahPass"] + "x salah";
				}

				//Disable password baru
				save.Enabled = false;
				passbaru.Enabled = false;
				passconfirm.Enabled = false;
				passbaru.Attributes["style"] = "background-color:'#ECE9D8'";
				passconfirm.Attributes["style"] = "background-color:'#ECE9D8'";

			}
			FeedBack();
		}

		private void Nav()
		{
            aSettings.HRef = "/settings/?UserID=" + Act.UserID + "&SID=" + Session.SessionID;
            aSecurity.HRef = "/security/?UserID="+Act.UserID+"&SID="+Session.SessionID;
			aAdminjual.HRef = "/adminjual/?UserID="+Act.UserID+"&SID="+Session.SessionID;
			aMarketingjual.HRef = "/marketingjual/?UserID="+Act.UserID+"&SID="+Session.SessionID;
			aCollection.HRef = "/collection/?UserID="+Act.UserID+"&SID="+Session.SessionID;
			aFinanceAR.HRef = "/financear/?UserID="+Act.UserID+"&SID="+Session.SessionID;
            aKpa.HRef = "/kpa/?UserID=" + Act.UserID + "&SID=" + Session.SessionID;
            aLegal.HRef = "/legal/?UserID=" + Act.UserID + "&SID=" + Session.SessionID;
            aKomisi.HRef = "/komisi/?UserID=" + Act.UserID + "&SID=" + Session.SessionID;
			aLaunching.HRef = "/launching/?UserID=" + Act.UserID + "&SID=" + Session.SessionID;
            aNup.HRef = "/nup/?UserID=" + Act.UserID + "&SID=" + Session.SessionID;
            aApp.HRef = "/approval/?UserID=" + Act.UserID + "&SID=" + Session.SessionID;
            ShowNav(settings, "settings");
            ShowNav(security, "security");
			ShowNav(adminjual, "adminjual");
			ShowNav(marketingjual, "marketingjual");
			ShowNav(collection, "collection");
			ShowNav(financear, "financear");
            ShowNav(approval, "approval");
            ShowNav(kpa, "kpa");
            ShowNav(legal, "legal");
			ShowNav(komisi, "komisi");
            ShowNav(launching, "launching");
            ShowNav(nup, "nup");
        } 

		private void ShowNav(HtmlControl a, string namafolder)
		{
			if(Db.SingleInteger("SELECT COUNT(*) "
				+ " FROM PAGEDENY INNER JOIN PAGE ON PAGEDENY.Halaman = PAGE.Halaman"
				+ " WHERE (Modul = '"+namafolder.ToUpper()+"' OR Modul = '"+namafolder.ToUpper()+"\\LAPORAN') AND Sifat = 0"
				+ " AND UserID = '"+Act.UserID+"'"
				)!=0)
			{
				a.Visible = true;
			}
			else
			{
				if(Db.SingleInteger("SELECT COUNT(*) "
					+ " FROM PAGESEC INNER JOIN PAGE ON PAGESEC.Halaman = PAGE.Halaman"
					+ " WHERE Modul = '"+namafolder.ToUpper()+"'"
					+ " AND Kode = '"+Act.SecLevel+"'"
					)==0)
				{
					a.Visible = false;
				}
			}
		}

		private void FeedBack()
		{
			feed.Text = "";
			if(!Page.IsPostBack)
			{
				if(Request.QueryString["pass"]=="1")
					feed.Text = "<img src='/Media/db.gif' align=absmiddle> Ganti Password Berhasil";
			}
		}

		protected void next_Click(object sender, System.EventArgs e)
		{
			string pass = Db.SingleString(
				"SELECT Pass FROM USERNAME WHERE UserID = '" + Act.UserID + "'");
			if(pass != passold.Text)
			{
				//3x salah password akan mengakibatkan sign-out otomatis;
				int x = Convert.ToInt32(Session["SalahPass"]) + 1;
				salah.Text = x.ToString() + "x salah";
				Session["SalahPass"] = x;
			
				if(x>=3)
					Response.Redirect("SignOut.aspx?pass=1");

				string scr = "";
				if(dariLogin.Checked)
					scr = "document.getElementById('passold').focus();";

				Js.Alert(
					this
					, "Password Salah "+x+"x.\\n"
					+ "Username akan Sign-Out otomatis apabila salah 3x."
					, scr
					);
			}
			else
			{
                
                //reset salah password
                Session["SalahPass"] = null;
                salah.Text = "";

                //Enable password baru
                save.Enabled = true;
                passbaru.Enabled = true;
                passconfirm.Enabled = true;
                passbaru.Attributes["style"] = "background-color:''";
                passconfirm.Attributes["style"] = "background-color:''";

                //Disable password lama
                next.Enabled = false;
                passold.Enabled = false;
                passold.Attributes["style"] = "background-color:'#ECE9D8'";

                //if (dariLogin.Checked)
                //    Js.Focus(this, passbaru);


                //GantiPass.Visible = true;
            }
		}

		protected void save_Click(object sender, System.EventArgs e)
		{
			if(valid())
			{
				Save();
				Selesai();
			}
		}

		private bool valid()
		{
			bool x = true;
			
			string pass = Db.SingleString(
				"SELECT Pass FROM USERNAME WHERE UserID = '" + Act.UserID + "'");

			if(passbaru.Text == Cf.NullMD5() || passbaru.Text != passconfirm.Text || passbaru.Text == pass)
			{
				x = false;
			}

			if(!x)
			{
				string scr = "";
				if(dariLogin.Checked)
					scr = "document.getElementById('passbaru').focus();";

				Js.Alert(
					this
					, "Input Tidak Valid.\\n\\n"
					+ "Aturan Pengisian :\\n"
					+ "1. Password Baru tidak boleh kosong.\\n"
					+ "2. Password Baru dan Confirm Password Baru harus sama.\\n"
					+ "3. Password Baru tidak boleh sama dengan Password Lama.\\n"
					, scr
					);
			}

			return x;
		}

		private void Save()
		{
			string UserID = Act.UserID;

			Db.Execute("EXEC spGantiPass "
				+ " '" + UserID + "'"
				+ ",'" + passbaru.Text + "'"
				+ ",0"
				);

			Db.Execute("EXEC spLogSecurity "
				+ " 'GP'"
				+ ",'" + UserID + "'"
				+ ",'" + Act.IP + "'"
				);
		}

		private void Selesai()
		{
			if(dariLogin.Checked)
				Response.Redirect("Gateway.aspx");
			else
				Response.Redirect("Gateway.aspx?pass=1");
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

using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.SETTINGS
{
	public partial class SecLevel : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			if(!Page.IsPostBack)
			{
				FillTable();
				Js.Focus(this, kode);
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
						+ "<a href=\"javascript:popSecLevel('"+Request.QueryString["done"]+"')\">"
						+ "Pendaftaran Berhasil..."
						+ "</a>";
			}
		}

		private void FillTable()
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			DataTable rs = Db.Rs("SELECT * FROM SECLEVEL ORDER BY Kode");
			
			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				sb.Append("<li>"
					+ "<a show-modal='#ModalPopUp' modal-title='Security Level Edit' modal-url='SecLevelEdit.aspx?Kode="+rs.Rows[i]["Kode"]+"'>"
					+ rs.Rows[i]["Nama"] + " ("+rs.Rows[i]["Kode"]+")"
					+ "</a>"
					+ "</li>"
					);

				string v = rs.Rows[i]["Kode"].ToString();
				string t = v + " - " + rs.Rows[i]["Nama"];
				copyconfig.Items.Add(new ListItem(t,v));
			}

			aktif.InnerHtml = sb.ToString();
		}

		private bool unik()
		{
			bool x = true;

			string Kode = Cf.Pk(kode.Text);
			int c = Db.SingleInteger("SELECT COUNT(*) FROM SECLEVEL WHERE"
				+ " Kode = '" + Kode + "'"
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

		protected void ok_Click(object sender, System.EventArgs e)
		{
			if(valid())
			{
				string Kode = Cf.Pk(kode.Text);
				string Nama = Cf.Str(nama.Text);

				Db.Execute("EXEC spSecLevelDaftar"
					+ " '" + Kode + "'"
					+ ",'" + Nama + "'"
					+ ",'" + copyconfig.SelectedValue + "'"
					);

				DataTable rs = Db.Rs("SELECT "
					+ " Kode"
					+ ",Nama"
					+ ",'"+copyconfig.SelectedValue+"' AS [Konfigurasi Copy]"
					+ " FROM SECLEVEL "
					+ " WHERE Kode = '" + Kode + "'");

				DataTable rsDetil = Db.Rs("SELECT PAGE.Modul + ' ' + PAGE.Nama + ' ' + PAGE.Halaman"
					+ " FROM PAGESEC INNER JOIN PAGE ON PAGESEC.Halaman = PAGE.Halaman "
					+ " WHERE Kode = '"+Kode+"' ORDER BY Modul,Nama");

				string KetLog = Cf.LogCapture(rs)
					+ Cf.LogList(rsDetil,"KONFIGURASI SECURITY");

				Db.Execute("EXEC spLogSeclevel"
					+ " 'DAFTAR'"
					+ ",'" + Act.UserID + "'"
					+ ",'" + Act.IP + "'"
					+ ",'" + KetLog + "'"
					+ ",'" + Kode + "'"
					);

				Response.Redirect("SecLevel.aspx?done=" + Kode);
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

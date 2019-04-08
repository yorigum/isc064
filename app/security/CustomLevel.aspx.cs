using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.SECURITY
{
	public partial class CustomLevel : System.Web.UI.Page
	{
		protected DataTable rs;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			if(!Page.IsPostBack)
			{
				BindModul(); //daftar modul
				Fill();
			}

			FeedBack();
			if(data.Visible) Bind();
		}

		private void BindModul()
		{
			DataTable rsList = Db.Rs("SELECT DISTINCT Modul FROM PAGE ORDER BY Modul");
			for(int i=0;i<rsList.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;
				daftarmodul.Items.Add(new ListItem(rsList.Rows[i][0].ToString()));
			}

			if(Request.QueryString["Modul"]!=null)
			{
				daftarmodul.SelectedValue = Request.QueryString["Modul"].ToString();
				data.Visible = true;
			}
			else
				data.Visible = false;
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
			display.Attributes["onclick"] = "gantiModul('"+UserID+"',document.getElementById('daftarmodul'))";
			daftarmodul.Attributes["ondblclick"] = "gantiModul('"+UserID+"',this)";
		}

		private void Bind()
		{
			string addq = "";
			if(Modul!="")
				addq = " AND Modul = '"+Modul+"'";

			string Kode = Db.SingleString(
				"SELECT SecLevel FROM USERNAME WHERE UserID = '"+UserID+"'");
			
			string strSql = "SELECT *"
				+ ",(SELECT COUNT(*) FROM PAGESEC WHERE Halaman = PAGE.Halaman AND Kode = '"+Kode+"') AS Status "
				+ ",(SELECT COUNT(*) FROM PAGEDENY WHERE Sifat = 0 AND Halaman = PAGE.Halaman AND UserID = '"+UserID+"') AS StatGrant "
				+ ",(SELECT COUNT(*) FROM PAGEDENY WHERE Sifat = 1 AND Halaman = PAGE.Halaman AND UserID = '"+UserID+"') AS StatDeny "
				+ " FROM PAGE WHERE 1=1 " + addq
				+ " ORDER BY Modul, Nama";
			rs = Db.Rs(strSql);
			Rpt.NoData(list, rs, "Mapping program belum dilakukan.");

			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				Label l;
				CheckBox r;
				RadioButton rb;

				//konfigurasi security khusus
				string style = "";
				if(Convert.ToInt32(rs.Rows[i]["StatGrant"])!=0)
					style = "style='background-color:lightgreen'";
				if(Convert.ToInt32(rs.Rows[i]["StatDeny"])!=0)
					style = "style='background-color:pink'";

				l = new Label();
				l.Text = "<tr valign='top' "+style+">"
					+ "<td style='border-bottom:1px dashed silver;white-space:nowrap'>";
				list.Controls.Add(l);
				
				r = new CheckBox();
				r.ID = "p_" + i;
				if(Convert.ToInt32(rs.Rows[i]["Status"])!=0)
					r.Checked = true;
				r.Enabled = false;
				list.Controls.Add(r);

				rb = new RadioButton();
				rb.ID = "no_" + i;
				rb.Text = "Normal";
				rb.Font.Size = 8;
				rb.GroupName = "rb_"+i;
				if(Convert.ToInt32(rs.Rows[i]["StatGrant"])==0
					&& Convert.ToInt32(rs.Rows[i]["StatDeny"])==0)
					rb.Checked = true;
                rb.CssClass = "igroup-radio";
				list.Controls.Add(rb);

				l = new Label();
				l.Text = "&nbsp;";
				list.Controls.Add(l);

				rb = new RadioButton();
				rb.ID = "grant_" + i;
				rb.Text = "Grant";
				rb.Font.Size = 8;
				rb.GroupName = "rb_"+i;
				if(Convert.ToInt32(rs.Rows[i]["StatGrant"])!=0)
					rb.Checked = true;
                rb.CssClass = "igroup-radio";
                list.Controls.Add(rb);

				l = new Label();
				l.Text = "&nbsp;";
				list.Controls.Add(l);

				rb = new RadioButton();
				rb.ID = "deny_" + i;
				rb.Text = "Deny";
				rb.Font.Size = 8;
				rb.GroupName = "rb_"+i;
				if(Convert.ToInt32(rs.Rows[i]["StatDeny"])!=0)
					rb.Checked = true;
                rb.CssClass = "igroup-radio";
                list.Controls.Add(rb);

				string namaFile = rs.Rows[i]["Halaman"].ToString();
				int index = namaFile.LastIndexOf("\\");
				if(index!=-1)
				{
					namaFile = namaFile.Substring(index+1,namaFile.Length-index-1);
				}

				l = new Label();
				l.Text = "</td>"
					+ "<td style='border-bottom:1px dashed silver;font-size:8pt'>"
					+ rs.Rows[i]["Modul"]
					+ "<br><font style='padding-left:20'>" + rs.Rows[i]["Nama"] + "</font>"
					+ "</td>"
					+ "<td style='border-bottom:1px dashed silver;font-size:8pt'>"
					+ namaFile
					+ "</td>";
				list.Controls.Add(l);

				l = new Label();
				l.Text = "</tr>";
				list.Controls.Add(l);
			}
		}

		protected void save_Click(object sender, System.EventArgs e)
		{
			if(Save()) Response.Redirect("CustomLevel.aspx?done=1&Modul="+Modul+"&UserID="+UserID);
		}

		protected void ok_Click(object sender, System.EventArgs e)
		{
			if(Save()) Js.Close(this);
		}

		private bool Save()
		{
			string addq = "";
			if(Modul!="")
				addq = " AND Halaman IN "
					+ "(SELECT Halaman FROM PAGE WHERE Modul = '"+Modul+"')";

			DataTable rsBefGrant = Db.Rs("SELECT PAGE.Modul + ' ' + PAGE.Nama + ' ' + PAGE.Halaman "
				+ " FROM PAGEDENY INNER JOIN PAGE ON PAGEDENY.Halaman = PAGE.Halaman "
				+ " WHERE Sifat = 0 AND UserID = '"+UserID+"' ORDER BY Modul,Nama");
			DataTable rsBefDeny = Db.Rs("SELECT PAGE.Modul + ' ' + PAGE.Nama + ' ' + PAGE.Halaman "
				+ " FROM PAGEDENY INNER JOIN PAGE ON PAGEDENY.Halaman = PAGE.Halaman "
				+ " WHERE Sifat = 1 AND UserID = '"+UserID+"' ORDER BY Modul,Nama");

			//reset dulu
			Db.Execute("DELETE FROM PAGEDENY WHERE UserID = '" + UserID + "'"
				+ addq);

			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				RadioButton grant = (RadioButton)list.FindControl("grant_"+i);
				RadioButton deny = (RadioButton)list.FindControl("deny_"+i);

				if(grant.Checked)
					Db.Execute("INSERT INTO PAGEDENY (UserID,Halaman,Sifat) "
						+ "VALUES ('"+UserID+"','"+rs.Rows[i]["Halaman"]+"',0)");
				
				if(deny.Checked)
					Db.Execute("INSERT INTO PAGEDENY (UserID,Halaman,Sifat) "
						+ "VALUES ('"+UserID+"','"+rs.Rows[i]["Halaman"]+"',1)");
			}
			
			DataTable rsAftGrant = Db.Rs("SELECT PAGE.Modul + ' ' + PAGE.Nama + ' ' + PAGE.Halaman "
				+ " FROM PAGEDENY INNER JOIN PAGE ON PAGEDENY.Halaman = PAGE.Halaman "
				+ " WHERE Sifat = 0 AND UserID = '"+UserID+"' ORDER BY Modul,Nama");
			DataTable rsAftDeny = Db.Rs("SELECT PAGE.Modul + ' ' + PAGE.Nama + ' ' + PAGE.Halaman "
				+ " FROM PAGEDENY INNER JOIN PAGE ON PAGEDENY.Halaman = PAGE.Halaman "
				+ " WHERE Sifat = 1 AND UserID = '"+UserID+"' ORDER BY Modul,Nama");

			DataTable rsDetail = Db.Rs("SELECT "
				+ " UserID AS [Kode / Username]"
				+ ",Nama AS [Nama Lengkap]"
				+ " FROM USERNAME WHERE UserID = '" + UserID + "'");

			string Ket = Cf.LogCapture(rsDetail)
				+ Cf.LogList(rsBefGrant,rsAftGrant,"GRANTED")
				+ Cf.LogList(rsBefDeny,rsAftDeny,"DENIED");

			//Log!
			Db.Execute("EXEC spLogUsername "
				+ " 'CL'"
				+ ",'" + Act.UserID + "'"
				+ ",'" + Act.IP + "'"
				+ ",'" + Ket + "'"
				+ ",'" + UserID + "'"
				);

			return true;
		}

		private string UserID
		{
			get
			{
				return Cf.Pk(Request.QueryString["UserID"]);
			}
		}

		private string Modul
		{
			get
			{
				try
				{
					return Cf.Pk(Request.QueryString["Modul"]);
				}
				catch
				{
					return "";
				}
			}
		}

		public void printIndex()
		{
			if(data.Visible)
				Response.Write(rs.Rows.Count);
			else
				Response.Write("1");
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

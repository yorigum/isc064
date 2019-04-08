using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.SECURITY
{
	public partial class SecLevelConfig : System.Web.UI.Page
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
			display.Attributes["onclick"] = "gantiModul('"+Kode+"',document.getElementById('daftarmodul'))";
			daftarmodul.Attributes["ondblclick"] = "gantiModul('"+Kode+"',this)";
		}

		private void Bind()
		{
			string addq = "";
			if(Modul!="")
				addq = " AND Modul = '"+Modul+"'";

			string strSql = "SELECT *,"
				+ " (SELECT COUNT(*) FROM PAGESEC WHERE Halaman = PAGE.Halaman AND Kode = '"+Kode+"') AS Status "
				+ " FROM PAGE WHERE 1=1 " + addq
				+ " ORDER BY Modul, Nama";
			rs = Db.Rs(strSql);
			Rpt.NoData(list, rs, "Mapping program belum dilakukan.");

			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				Label l;
				CheckBox r;

				l = new Label();
				l.Text = "<tr valign='top'>"
					+ "<td style='border-bottom:1px dashed silver'>";
				list.Controls.Add(l);
				
				r = new CheckBox();
				r.ID = "p_" + i;
				if(Convert.ToInt32(rs.Rows[i]["Status"])!=0)
					r.Checked = true;
				list.Controls.Add(r);

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

		private bool Save()
		{
			string addq = "";
			if(Modul!="")
				addq = " AND Halaman IN "
					+ "(SELECT Halaman FROM PAGE WHERE Modul = '"+Modul+"')";

			DataTable rsBef = Db.Rs("SELECT PAGE.Modul + ' ' + PAGE.Nama + ' ' + PAGE.Halaman"
				+ " FROM PAGESEC INNER JOIN PAGE ON PAGESEC.Halaman = PAGE.Halaman "
				+ " WHERE Kode = '"+Kode+"' ORDER BY Modul,Nama");

			Db.Execute("DELETE FROM PAGESEC WHERE Kode = '"+Kode+"'"
				+ addq);
			
			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;
				
				CheckBox cb = (CheckBox)list.FindControl("p_"+i);
				if(cb.Checked)
				{
					Db.Execute("INSERT INTO PAGESEC (Kode,Halaman) "
						+ "VALUES ('"+Kode+"','"+rs.Rows[i]["Halaman"]+"')");
				}
			}
				
			DataTable rsAft = Db.Rs("SELECT PAGE.Modul + ' ' + PAGE.Nama + ' ' + PAGE.Halaman"
				+ " FROM PAGESEC INNER JOIN PAGE ON PAGESEC.Halaman = PAGE.Halaman "
				+ " WHERE Kode = '"+Kode+"' ORDER BY Modul,Nama");

			string KetLog = Cf.LogList(rsBef, rsAft, "KONFIGURASI SECURITY");

			Db.Execute("EXEC spLogSeclevel"
				+ " 'EDIT'"
				+ ",'" + Act.UserID + "'"
				+ ",'" + Act.IP + "'"
				+ ",'" + KetLog + "'"
				+ ",'" + Kode + "'"
				);

			return true;
		}

		protected void ok_Click(object sender, System.EventArgs e)
		{
			if(Save()) Js.Close(this);
		}

		protected void save_Click(object sender, System.EventArgs e)
		{
			if(Save()) Response.Redirect("SecLevelConfig.aspx?Kode="+Kode+"&Modul="+Modul+"&done=1");
		}

		private string Kode
		{
			get
			{
				return Cf.Pk(Request.QueryString["Kode"]);
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

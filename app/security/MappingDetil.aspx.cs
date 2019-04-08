using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.SECURITY
{
	public partial class MappingDetil : System.Web.UI.Page
	{
		protected DataTable rs;
		protected DataTable rsKhusus;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			if(!Page.IsPostBack)
			{
				Fill();
			}

			Bind();
			BindKhusus();

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
			string strSql = "SELECT * FROM PAGE WHERE Halaman = '" + Halaman + "'";
			DataTable rsHeader = Db.Rs(strSql);

            if (rsHeader.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                halaman.Text = rsHeader.Rows[0]["Halaman"].ToString();
                modul.Text = rsHeader.Rows[0]["Modul"].ToString();
                nama.Text = rsHeader.Rows[0]["Nama"].ToString();
                tgl.Text = Cf.Day(rsHeader.Rows[0]["TglInput"]);
            }
        }

        private void Bind()
		{
			string strSql = "SELECT *,"
				+ " (SELECT COUNT(*) FROM PAGESEC WHERE Kode = SECLEVEL.Kode AND Halaman = '"+Halaman+"') AS Status "
				+ " FROM SECLEVEL ORDER BY Kode";
			rs = Db.Rs(strSql);
			
			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				string v = rs.Rows[i]["Kode"].ToString();
				string t = rs.Rows[i]["Nama"] + " ("+v+")";

				Label l;
				CheckBox r;

				l = new Label();
				l.Text = "<tr valign='middle'>"
					+ "<td style='border-bottom:1px dashed silver'>";
				list.Controls.Add(l);
				
				r = new CheckBox();
				r.ID = "sl_" + i;
				if(Convert.ToInt32(rs.Rows[i]["Status"])!=0)
					r.Checked = true;
				list.Controls.Add(r);

				l = new Label();
				l.Text = "</td>"
					+ "<td style='border-bottom:1px dashed silver;font-size:8pt'>"
					+ t
					+ "</td>";
				list.Controls.Add(l);

				l = new Label();
				l.Text = "</tr>";
				list.Controls.Add(l);
			}
		}

		
		private void BindKhusus()
		{
			string strSql = "SELECT *"
				+ ",(SELECT COUNT(*) FROM PAGEDENY WHERE UserID = USERNAME.UserID AND Halaman = '"+Halaman+"' AND Sifat = 1) AS StatDeny "
				+ ",(SELECT COUNT(*) FROM PAGEDENY WHERE UserID = USERNAME.UserID AND Halaman = '"+Halaman+"' AND Sifat = 0) AS StatGrant "
				+ " FROM USERNAME ORDER BY UserID";
			rsKhusus = Db.Rs(strSql);
			
			for(int i=0;i<rsKhusus.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				string v = rsKhusus.Rows[i]["UserID"].ToString();
				string t = rsKhusus.Rows[i]["Nama"] + " ("+v+")";

				Label l;
				RadioButton rb;

				l = new Label();
				l.Text = "<tr valign='middle'>"
					+ "<td style='border-bottom:1px dashed silver'>";
				list2.Controls.Add(l);
				
				rb = new RadioButton();
				rb.ID = "no_" + i;
				rb.Text = "Normal";
				rb.Font.Size = 8;
				rb.GroupName = "rb_"+i;
				if(Convert.ToInt32(rsKhusus.Rows[i]["StatGrant"])==0
					&& Convert.ToInt32(rsKhusus.Rows[i]["StatDeny"])==0)
					rb.Checked = true;
                rb.CssClass = "igroup-radio";
                list2.Controls.Add(rb);

				l = new Label();
				l.Text = "&nbsp;";
				list2.Controls.Add(l);

				rb = new RadioButton();
				rb.ID = "grant_" + i;
				rb.Text = "Grant";
				rb.Font.Size = 8;
				rb.GroupName = "rb_"+i;
				if(Convert.ToInt32(rsKhusus.Rows[i]["StatGrant"])!=0)
					rb.Checked = true;
                rb.CssClass = "igroup-radio";
                list2.Controls.Add(rb);

				l = new Label();
				l.Text = "&nbsp;";
				list2.Controls.Add(l);

				rb = new RadioButton();
				rb.ID = "deny_" + i;
				rb.Text = "Deny";
				rb.Font.Size = 8;
				rb.GroupName = "rb_"+i;
				if(Convert.ToInt32(rsKhusus.Rows[i]["StatDeny"])!=0)
					rb.Checked = true;
                rb.CssClass = "igroup-radio";
                list2.Controls.Add(rb);

				l = new Label();
				l.Text = "</td>"
					+ "<td style='border-bottom:1px dashed silver;font-size:8pt'>"
					+ t
					+ "</td>"
					+ "<td style='border-bottom:1px dashed silver;font-size:8pt'>"
					+ rsKhusus.Rows[i]["SecLevel"]
					+ "</td>"
					;
				list2.Controls.Add(l);

				l = new Label();
				l.Text = "</tr>";
				list2.Controls.Add(l);
			}
		}

		protected void ok_Click(object sender, System.EventArgs e)
		{
			if(Save()) Js.Close(this);
		}
		protected void save_Click(object sender, System.EventArgs e)
		{
			if(Save()) Response.Redirect("MappingDetil.aspx?Halaman="+Halaman+"&done=1");
		}

		private bool Save()
		{
			DataTable rsBef = Db.Rs(
				"SELECT Kode FROM PAGESEC WHERE Halaman = '"+Halaman+"' ORDER BY Kode");
			DataTable rsBefGrant = Db.Rs(
				"SELECT UserID FROM PAGEDENY WHERE Halaman = '"+Halaman+"' AND Sifat = 0 ORDER BY UserID");
			DataTable rsBefDeny = Db.Rs(
				"SELECT UserID FROM PAGEDENY WHERE Halaman = '"+Halaman+"' AND Sifat = 1 ORDER BY UserID");

			Db.Execute("DELETE FROM PAGESEC WHERE Halaman = '"+Halaman+"'");
			Db.Execute("DELETE FROM PAGEDENY WHERE Halaman = '"+Halaman+"'");

			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;
				
				CheckBox cb = (CheckBox)list.FindControl("sl_"+i);

				if(cb.Checked)
					Db.Execute("INSERT INTO PAGESEC (Kode,Halaman) "
						+ "VALUES ('"+rs.Rows[i]["Kode"]+"','"+Halaman+"')");
			}

			for(int i=0;i<rsKhusus.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;
				
				RadioButton grant = (RadioButton)list2.FindControl("grant_"+i);
				RadioButton deny = (RadioButton)list2.FindControl("deny_"+i);
				
				if(grant.Checked)
					Db.Execute("INSERT INTO PAGEDENY (UserID,Halaman,Sifat) "
						+ "VALUES ('"+rsKhusus.Rows[i]["UserID"]+"','"+Halaman+"',0)");
				
				if(deny.Checked)
					Db.Execute("INSERT INTO PAGEDENY (UserID,Halaman,Sifat) "
						+ "VALUES ('"+rsKhusus.Rows[i]["UserID"]+"','"+Halaman+"',1)");
			}
			
			DataTable rsAft = Db.Rs(
				"SELECT Kode FROM PAGESEC WHERE Halaman = '"+Halaman+"' ORDER BY Kode");
			DataTable rsAftGrant = Db.Rs(
				"SELECT UserID FROM PAGEDENY WHERE Halaman = '"+Halaman+"' AND Sifat = 0 ORDER BY UserID");
			DataTable rsAftDeny = Db.Rs(
				"SELECT UserID FROM PAGEDENY WHERE Halaman = '"+Halaman+"' AND Sifat = 1 ORDER BY UserID");

			DataTable rsHeader = Db.Rs("SELECT "
				+ " Modul"
				+ ",Nama AS Keterangan"
				+ ",Halaman "
				+ " FROM PAGE WHERE Halaman = '"+Halaman+"'");

			string KetLog = Cf.LogCapture(rsHeader)
				+ Cf.LogList(rsBef, rsAft, "KONFIGURASI SECURITY")
				+ Cf.LogList(rsBefGrant,rsAftGrant,"KONFIGURASI KHUSUS - GRANTED")
				+ Cf.LogList(rsBefDeny,rsAftDeny,"KONFIGURASI KHUSUS - DENIED");

			Db.Execute("EXEC spLogSeclevel"
				+ " 'EDIT'"
				+ ",'" + Act.UserID + "'"
				+ ",'" + Act.IP + "'"
				+ ",'" + KetLog + "'"
				+ ",''"
				);

			return true;
		}

		private string Halaman
		{
			get
			{
				return Request.QueryString["Halaman"];
			}
		}

		public void printIndex()
		{
			Response.Write(rs.Rows.Count);
		}
		public void printIndexKhusus()
		{
			Response.Write(rsKhusus.Rows.Count);
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

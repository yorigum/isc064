using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.SECURITY
{
	public partial class MKA : System.Web.UI.Page
	{
		protected DataTable rs;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			Head();
			Bind();

			//Initialize awal
			if(!Page.IsPostBack)
				Fill();
		}

		private void Head()
		{
			string strSql = "SELECT * FROM SECLEVEL ORDER BY Kode";
			DataTable rs = Db.Rs(strSql);
			for(int i=0;i<rs.Rows.Count;i++)
			{
				HtmlTableCell c = new HtmlTableCell("th");
				c.InnerHtml = rs.Rows[i]["Kode"].ToString();
				c.Width = "25";
				c.Attributes["onclick"] = "location.href='?SL="+rs.Rows[i]["Kode"]+"'";
				c.Attributes["onmouseover"] = "this.style.color='blue'";
				c.Attributes["onmouseout"] = "this.style.color=''";
				head.Cells.Add(c);
			}
		}

		private void Bind()
		{
			string addq = "";
			if(Request.QueryString["SL"]!=null)
				addq = " AND SecLevel = '"+Request.QueryString["SL"]+"'";

			string strSql = "SELECT "
				+ " UserID"
				+ ",Nama"
				+ ",SecLevel"
				+ " FROM USERNAME"
				+ " WHERE Status = 'A'"
				+ addq
				+ " ORDER BY Nama, UserID";

			rs = Db.Rs(strSql);
			Rpt.NoData(list, rs, "Tidak ada username dengan status AKTIF.");

			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				Label l;
				RadioButton r;

				l = new Label();
				l.Text = "<tr valign='top'>"
					+ "<td style='border-bottom:1px dashed silver'>"
					+ rs.Rows[i]["UserID"]
					+ "</td>"
					+ "<td style='border-bottom:1px dashed silver'>"
					+ "<a href=\"javascript:mka('"+rs.Rows[i]["UserID"]+"')\">"
					+ rs.Rows[i]["Nama"]
					+ "</a></td>";
				list.Controls.Add(l);

				for(int j=2;j<=head.Cells.Count-1;j++)
				{
					l = new Label();
					l.Text = "<td style='border-bottom:1px dashed silver'>";
					list.Controls.Add(l);

					string s = head.Cells[j].InnerHtml;

					r = new RadioButton();
					r.ID = s + i;
					r.GroupName = "sl_" + i;
                    list.Controls.Add(r);

					l = new Label();
					l.Text = "</td>";
					list.Controls.Add(l);
				}

				l = new Label();
				l.Text = "</tr>";
				list.Controls.Add(l);
			}
		}

		private void Fill()
		{
			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				string s = rs.Rows[i]["SecLevel"].ToString();
				if(s!="")
				{
					RadioButton r = (RadioButton) list.FindControl(s + i);
					r.Checked = true;
				}
			}
		}

		protected void save_Click(object sender, System.EventArgs e)
		{
			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				string seclevel = "";
				for(int j=2;j<=head.Cells.Count-1;j++)
				{
					string s = head.Cells[j].InnerHtml;
					RadioButton r = (RadioButton) list.FindControl(s + i);
					if(r.Checked) seclevel = s;
				}

				string UserID = rs.Rows[i]["UserID"].ToString();

				DataTable rsDetail = Db.Rs("SELECT "
					+ " UserID AS [Kode / Username]"
					+ ",Nama AS [Nama Lengkap]"
					+ " FROM USERNAME WHERE UserID = '" + UserID + "'");

				DataTable rsBef = Db.Rs("SELECT "
					+ " SecLevel AS [Security Level]"
					+ " FROM USERNAME WHERE UserID = '" + UserID + "'");

				Db.Execute("EXEC spUserAkses"
					+ " '" + UserID + "'"
					+ ",'" + seclevel + "'"
					);

				DataTable rsAft = Db.Rs("SELECT "
					+ " SecLevel AS [Security Level]"
					+ " FROM USERNAME WHERE UserID = '" + UserID + "'");

				if(seclevel!=rs.Rows[i]["SecLevel"].ToString())
				{
					string Ket = Cf.LogCapture(rsDetail)
						+ Cf.LogCompare(rsBef,rsAft);

					Db.Execute("EXEC spLogUsername "
						+ " 'MKA'"
						+ ",'" + Act.UserID + "'"
						+ ",'" + Act.IP + "'"
						+ ",'" + Ket + "'"
						+ ",'" + UserID + "'"
						);
				}
			}

			feed.Text = "<img src='/Media/db.gif' align=absmiddle> Edit MKA Berhasil...";
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

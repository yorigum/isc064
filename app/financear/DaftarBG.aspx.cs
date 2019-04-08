using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.FINANCEAR
{
	public partial class DaftarBG : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			Js.ConfirmKeyword(this,keyword);

			if(!Page.IsPostBack)
			{
				if(Request.QueryString["status"]==null)
					metode.SelectedIndex = 0;
				else if(Request.QueryString["status"]=="ok")
					metode.SelectedIndex = 1;
				else if(Request.QueryString["status"]=="okbc")
					metode.SelectedIndex = 2;
				else if(Request.QueryString["status"]=="oksc")
					metode.SelectedIndex = 3;
				else if(Request.QueryString["status"]=="bad")
					metode.SelectedIndex = 4;

				if(metode.SelectedIndex!=0) metode.Enabled = false;
			}
		}

		protected void search_Click(object sender, System.EventArgs e)
		{
			Fill();
		}

		private void Fill()
		{
			string addq = "";
			if(metode.SelectedIndex==1)
				addq = " AND StatusBG = 'OK'";
			else if(metode.SelectedIndex==2)
				addq = " AND StatusBG = 'OK' AND Status = 'BARU'";
			else if(metode.SelectedIndex==3)
				addq = " AND StatusBG = 'OK' AND Status = 'POST'";
			else if(metode.SelectedIndex==4)
				addq = " AND StatusBG = 'BAD'";

			string strSql = "SELECT *"
				+ " FROM MS_TTS"
				+ " WHERE CaraBayar = 'BG'"
				+ " AND Ref + Unit + Customer + Ket + NoBG LIKE '%" + Cf.Str(keyword.Text) +"%'"
				+ addq
				+ " ORDER BY NoBG";

			DataTable rs = Db.Rs(strSql);
			Rpt.NoData(rpt, rs, "Tidak ditemukan data cek giro dengan keyword diatas.");

			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;
				
				TableRow r = new TableRow();
				TableCell c;

				string status = rs.Rows[i]["Status"].ToString();

				c = new TableCell();
				c.Text = "<a href=\"javascript:call('"+rs.Rows[i]["NoBG"]+"','"+Cf.Day(rs.Rows[i]["TglBG"])+"')\">"
					+ rs.Rows[i]["NoBG"] + "</a>"
					+ "<br><font style='white-space:nowrap'>Tgl. BG : " + Cf.Day(rs.Rows[i]["TglBG"]) + "</font>";
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["NoTTS"].ToString().PadLeft(7,'0')
					+ "<br><i>"+status+"</i>"
					+ "<br>" + Cf.Day(rs.Rows[i]["TglTTS"]);
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Tipe"] + " No. " + rs.Rows[i]["Ref"]
					+ "<br>" + rs.Rows[i]["Unit"]
					+ "<br>" + rs.Rows[i]["Customer"];
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Ket"].ToString();
				if(rs.Rows[i]["Titip"].ToString()!="")
					c.Text = c.Text + "<br>Pengelola : " + rs.Rows[i]["Titip"];
				if(rs.Rows[i]["Tolak"].ToString()!="")
					c.Text = c.Text + "<br>Tolakan : " + rs.Rows[i]["Tolak"];
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Num(rs.Rows[i]["Total"]);
				c.HorizontalAlign = HorizontalAlign.Right;
				r.Cells.Add(c);

				Rpt.Border(r);
				rpt.Rows.Add(r);
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

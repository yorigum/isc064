using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.FINANCEAR
{
	public partial class DaftarTTS2 : System.Web.UI.Page
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
				else if(Request.QueryString["status"]=="post")
					metode.SelectedIndex = 2;
				else if(Request.QueryString["status"]=="void")
					metode.SelectedIndex = 3;

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
				addq = " AND Status = 'BARU'";
			else if(metode.SelectedIndex==2)
				addq = " AND Status = 'POST'";
			else if(metode.SelectedIndex==3)
				addq = " AND Status = 'VOID'";

			string strSql = "SELECT *"
				+ ",(SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..USERNAME WHERE UserID = MS_TTS.UserID) AS Nama"
				+ ",CASE CaraBayar"
				+ "		WHEN 'TN' THEN 'TUNAI'"
				+ "		WHEN 'KK' THEN 'KARTU KREDIT'"
				+ "		WHEN 'KD' THEN 'KARTU DEBIT'"
				+ "		WHEN 'TR' THEN 'TRANSFER BANK'"
				+ "		WHEN 'BG' THEN 'CEK GIRO'"
				+ "		WHEN 'UJ' THEN 'UANG JAMINAN'"
				+ "		WHEN 'DN' THEN 'DISKON'"
				+ " END AS CaraBayar2"
				+ " FROM MS_TTS"
				+ " WHERE CONVERT(varchar,NoTTS) + Ref + Unit + Customer + Ket + NoBG "
				+ " LIKE '%" + Cf.Str(keyword.Text) +"%'"
				+ addq
				+ " ORDER BY NoTTS";

			DataTable rs = Db.Rs(strSql);
			Rpt.NoData(rpt, rs, "Tidak ditemukan data tanda terima sementara dengan keyword diatas.");

			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;
				
				TableRow r = new TableRow();
				TableCell c;

				string status = rs.Rows[i]["Status"].ToString();
				string strike = "";
				if(status=="VOID")
				{
					r.ForeColor = Color.Silver;
					strike = "style='text-decoration:line-through'";
				}

				string bkm = "";
				if(status=="POST")
				{
                    bkm = "<br>BKM:" + rs.Rows[i]["NoBKM"].ToString().PadLeft(7, '0');
				}

				c = new TableCell();
				if(Request.QueryString["status"] == "tbDari" || Request.QueryString["status"] == "tbSampai")
				{
					c.Text = "<a href=\"javascript:callSource('" + rs.Rows[i]["NoTTS"] + "', '" + Request.QueryString["status"] + "')\" "+strike+">"
						+ rs.Rows[i]["NoTTS"].ToString().PadLeft(7,'0') + "</a>"
						+ "<br><i>"+status+"</i>"
						+ bkm;
				}
				else
				{
					c.Text = "<a href=\"javascript:call('"+rs.Rows[i]["NoTTS"]+"')\" "+strike+">"
						+ rs.Rows[i]["NoTTS"].ToString().PadLeft(7,'0') + "</a>"
						+ "<br><i>"+status+"</i>"
						+ bkm;
				}
				r.Cells.Add(c);

				string userid = "";
				if(rs.Rows[i]["Nama"].ToString()=="") userid = rs.Rows[i]["UserID"].ToString();

				c = new TableCell();
				c.Text = Cf.Day(rs.Rows[i]["TglTTS"])
					+ "<br>" + rs.Rows[i]["Nama"] + userid;
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
				c.Text = rs.Rows[i]["CaraBayar2"].ToString();
				if(rs.Rows[i]["CaraBayar"].ToString()=="BG")
					c.Text = c.Text
						+ "<br>" + rs.Rows[i]["NoBG"]
						+ "<br><font style='white-space:nowrap'>Tgl. BG : " + Cf.Day(rs.Rows[i]["TglBG"]) + "</font>";
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Num(rs.Rows[i]["Total"]);
				if((bool)rs.Rows[i]["Pph"])
					c.Text = c.Text + "<br>PPH";
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

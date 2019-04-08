using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.FINANCEAR
{
	public partial class CustomerTTS : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			if(!Page.IsPostBack)
			{
				FillTable();
			}
		}

		private void FillTable()
		{
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
				+ " WHERE Tipe = '"+Tipe+"' AND Ref = '"+Ref+"'";
			DataTable rs = Db.Rs(strSql);

			decimal t1 = 0;

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
					bkm = "<br>BKM:" + rs.Rows[i]["NoBKM2"];
				}

				c = new TableCell();
				c.Text = "<a href='TTSEdit.aspx?NoTTS="+rs.Rows[i]["NoTTS"]+"' "+strike+">"
                    + rs.Rows[i]["NoTTS2"] + "</a>"
					+ "<br><i>"+status+"</i>"
					+ bkm;
                c.Wrap = false;
				r.Cells.Add(c);

				string userid = "";
				if(rs.Rows[i]["Nama"].ToString()=="") userid = rs.Rows[i]["UserID"].ToString();

				c = new TableCell();
				c.Text = Cf.Day(rs.Rows[i]["TglTTS"])
					+ "<br>" + rs.Rows[i]["Nama"] + userid;
                c.Wrap = false;
                r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["Ket"].ToString();
				if(rs.Rows[i]["Titip"].ToString()!="")
					c.Text = c.Text + "<br>Pengelola : " + rs.Rows[i]["Titip"];
				if(rs.Rows[i]["Tolak"].ToString()!="")
					c.Text = c.Text + "<br>Tolakan : " + rs.Rows[i]["Tolak"];
                c.Wrap = false;
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
				c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

				Rpt.Border(r);
				rpt.Rows.Add(r);

				t1 = t1 + (decimal)rs.Rows[i]["Total"];
				
				if(i==rs.Rows.Count-1)
					SubTotal(t1);
			}
		}

		private void SubTotal(decimal t1)
		{
			TableRow r = new TableRow();
			TableCell c;

			c = new TableCell();
			c.ColumnSpan = 4;
			c.Text = "<b>GRAND TOTAL</b>";
			r.Cells.Add(c);

			c = new TableCell();
			c.Font.Bold = true;
			c.Text = Cf.Num(t1);
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			rpt.Rows.Add(r);
		}

		private string Tipe
		{
			get
			{
				return Cf.Pk(Request.QueryString["Tipe"]);
			}
		}

		private string Ref
		{
			get
			{
				return Cf.Pk(Request.QueryString["Ref"]);
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

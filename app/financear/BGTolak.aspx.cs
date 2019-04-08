using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.FINANCEAR
{
	public partial class BGTolak : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			if(!Page.IsPostBack)
			{
				nobg.Attributes["ondblclick"] = "popDaftarBG('ok');";
				Js.Focus(this,nobg);
				frm.Visible = false;
			}

			FeedBack();
			if(frm.Visible) Js.Confirm(this,"Lanjutkan proses tolakan cek giro?");
		}

		private void FeedBack()
		{
			feed.Text = "";
			if(!Page.IsPostBack)
			{
				if(Request.QueryString["done"]!=null)
					feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
						+ "Tolakan Cek Giro Berhasil...";
			}
		}

		private bool valid()
		{
			bool x = true;

			int c = Db.SingleInteger(
				"SELECT COUNT(*) FROM MS_TTS WHERE NoBG = '" + NoBG + "'");

			if(c==0)
				x = false;

			if(!x)
				Js.Alert(
					this
					, "Cek Giro Tidak Valid.\\n\\n"
					+ "Kemungkinan Sebab :\\n"
					+ "1. Nomor cek giro tersebut tidak terdaftar.\\n"
					, "document.getElementById('nobg').focus();"
					+ "document.getElementById('nobg').select();"
					);

			return x;
		}

		protected void next_Click(object sender, System.EventArgs e)
		{
			if(valid())
			{
				pilih.Visible = false;
				frm.Visible = true;

				Fill();

				Js.Confirm(this,"Lanjutkan proses tolakan cek giro?");
			}
		}

		private void Fill()
		{
			string strSql = "SELECT * "
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
				+ " FROM MS_TTS "
				+ " WHERE NoBG = '"+NoBG+"' "
				+ " ORDER BY NoTTS";

			DataTable rs = Db.Rs(strSql);
			Rpt.NoData(rpt, rs, "Tidak ada TTS dengan kriteria seperti tersebut diatas.");

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
					bkm = "<br>BKM:" + rs.Rows[i]["NoBKM"];
				}

				c = new TableCell();
				c.Text = "<a href=\"javascript:popEditTTS('"+rs.Rows[i]["NoTTS"]+"')\" "+strike+">"
					+ rs.Rows[i]["NoTTS"].ToString().PadLeft(7,'0') + "</a>"
					+ "<br><i>"+status+"</i>"
					+ bkm;
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
				c.HorizontalAlign = HorizontalAlign.Right;
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
			c.ColumnSpan = 5;
			c.Text = "<b>GRAND TOTAL</b>";
			r.Cells.Add(c);

			c = new TableCell();
			c.Font.Bold = true;
			c.Text = Cf.Num(t1);
			c.HorizontalAlign = HorizontalAlign.Right;
			r.Cells.Add(c);

			rpt.Rows.Add(r);
		}

		private bool datavalid()
		{
			bool x = true;

			return x;
		}

		protected void save_Click(object sender, System.EventArgs e)
		{
			if(datavalid())
			{
				Db.Execute("EXEC spTTSGiroTolak "
					+ " '" + NoBG + "'"
					+ ",'" + alasan.SelectedValue + "'"
					);

				DataTable rs = Db.Rs("SELECT NoTTS FROM MS_TTS WHERE NoBG = '"+NoBG+"'");
				for(int i=0;i<rs.Rows.Count;i++)
				{
					int NoTTS = (int)rs.Rows[i]["NoTTS"];
				
					DataTable rsLog = Db.Rs("SELECT "
						+ " NoTTS AS [No. TTS]"
						+ ",Tipe"
						+ ",Ref AS [Ref.]"
						+ ",Unit"
						+ ",Customer"
						+ ",Total AS [Nilai TTS]"
						+ ",NoBG AS [No. BG]"
						+ ",Tolak AS [Alasan Penolakan]"
						+ " FROM MS_TTS"
						+ " WHERE NoTTS = " + NoTTS
						);

					//Logfile
					string ketlog = Cf.LogCapture(rsLog);

					Db.Execute("EXEC spLogTTS"
						+ " 'TOLAK'"
						+ ",'" + Act.UserID + "'"
						+ ",'" + Act.IP + "'"
						+ ",'" + ketlog + "'"
						+ ",'" + NoTTS.ToString().PadLeft(7,'0') + "'"
						);

                    decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_TTS_LOG ORDER BY LogID DESC");
                    string Project = Db.SingleString("SELECT Project FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = (SELECT Ref FROM MS_TTS WHERE NoTTS = '" + NoTTS + "')");
                    Db.Execute("UPDATE MS_TTS_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                }

                Response.Redirect("BGTolak.aspx?done="+NoBG);
			}
		}

		private string NoBG
		{
			get
			{
				return Cf.Pk(nobg.Text);
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

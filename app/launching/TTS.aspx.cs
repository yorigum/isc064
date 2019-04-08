using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LAUNCHING
{
	public partial class TTS : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			if(!Page.IsPostBack)
			{
				init();
			}

			Js.Focus(this,search);
		}

		private void init()
		{
			dari.Text = Cf.Day(DateTime.Today);
			sampai.Text = Cf.Day(DateTime.Today);

			bindUser();

			//tipe
			string[] x = Sc.MktCatalog();
			for(int i=0;i<=x.GetUpperBound(0);i++)
			{
				string[] xdetil = x[i].Split(';');
				tipe.Items.Add(new ListItem(xdetil[2],xdetil[1]));
			}
		}

		private void bindUser()
		{
			DataTable rs;

			rs = Db.Rs("SELECT DISTINCT UserID FROM ISC064_FINANCEAR..MS_TTS ORDER BY UserID");
			for(int i=0;i<rs.Rows.Count;i++)
			{
				string v = rs.Rows[i]["UserID"].ToString();
				user.Items.Add(new ListItem(v,v));
			}
		}

		private bool valid()
		{
			string s = "";
			bool x = true;

			if(!Cf.isTgl(dari))
			{
				daric.Text = "Tanggal";
				if(s=="") s = dari.ID;
				x = false;
			}
			else
				daric.Text = "";

			if(!Cf.isTgl(sampai))
			{
				sampaic.Text = "Tanggal";
				if(s=="") s = sampai.ID;
				x = false;
			}
			else
				sampaic.Text = "";

			if(!x)
				RegisterStartupScript("err"
					,"<script language='javascript'>document.getElementById('"+s+"').select()</script>");
			
			return x;
		}

		protected void display_Click(object sender, System.EventArgs e)
		{
			if(valid())
			{
				Fill();
			}
		}

		private void Fill()
		{
			DateTime Dari = Convert.ToDateTime(dari.Text);
			DateTime Sampai = Convert.ToDateTime(sampai.Text);
			if(Dari>Sampai)
			{
				DateTime x = Sampai;
				Sampai = Dari;
				Dari = x;
			}

			string UserID = "";
			if(user.SelectedIndex!=0)
				UserID = " AND UserID = '"+user.SelectedValue+"'";

			string CaraBayar = "";
			if(carabayar.SelectedIndex!=0)
				CaraBayar = " AND CaraBayar = '"+carabayar.SelectedValue+"'";

			string Tipe = "";
			if(tipe.SelectedIndex != 0)
				Tipe = " AND Tipe = '" + Cf.Str(tipe.SelectedValue) + "'";

			string Status = "";
			if(statusB.Checked) Status = " AND Status = 'BARU'";
			if(statusP.Checked) Status = " AND Status = 'POST'";
			if(statusV.Checked) Status = " AND Status = 'VOID'";

			string strSql = "SELECT * "
                + ",(SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..USERNAME WHERE UserID = ISC064_FINANCEAR..MS_TTS.UserID) AS Nama"
				+ ",CASE CaraBayar"
				+ "		WHEN 'TN' THEN 'TUNAI'"
				+ "		WHEN 'KK' THEN 'KARTU KREDIT'"
				+ "		WHEN 'KD' THEN 'KARTU DEBIT'"
				+ "		WHEN 'TR' THEN 'TRANSFER BANK'"
				+ "		WHEN 'BG' THEN 'CEK GIRO'"
				+ "		WHEN 'UJ' THEN 'UANG JAMINAN'"
				+ "		WHEN 'DN' THEN 'DISKON'"
				+ " END AS CaraBayar2"
                + " FROM ISC064_FINANCEAR..MS_TTS "
				+ " WHERE 1=1 "
				+ " AND CONVERT(varchar,TglTTS,112) >= '" + Cf.Tgl112(Dari) + "'"
				+ " AND CONVERT(varchar,TglTTS,112) <= '" + Cf.Tgl112(Sampai) + "'"
				+ UserID
				+ CaraBayar
				+ Tipe
				+ Status
				+ " ORDER BY NoTTS";

			DataTable rs = Db.Rs(strSql);
			Rpt.NoData(rpt, rs, "Tidak ada TTS dengan kriteria seperti tersebut diatas.");

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
                    //bkm = "<br>BKM:" + rs.Rows[i]["NoBKM"].ToString().PadLeft(7,'0')
                        bkm = "<br>KWT Manual:" + rs.Rows[i]["ManualBKM"].ToString();
				}

				c = new TableCell();
                c.Text = "<a href=\"javascript:call('" + rs.Rows[i]["NoTTS"] + "')\" " + strike + ">"
                    + rs.Rows[i]["NoTTS"].ToString().PadLeft(7,'0')+ "</a>"
                    + "<br>TTS Manual : " + rs.Rows[i]["ManualTTS"].ToString()
                    + "<br><i>" + status + "</i>"
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

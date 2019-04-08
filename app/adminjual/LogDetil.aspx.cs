using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.ADMINJUAL
{
	public partial class LogDetil : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			DataTable rs = Db.Rs(
				"SELECT * FROM " + Tb + " WHERE LogID = " + LogID);

			if(rs.Rows.Count==0)
				Response.Redirect("/CustomError/Deleted.html");
			else
			{
				sumber.Text = Request.QueryString["sumber"];
				logid.Text = LogID;
				tgl.Text = Cf.Date(rs.Rows[0]["Tgl"]);

				user.Text = rs.Rows[0]["UserID"].ToString();
				ip.Text = rs.Rows[0]["IP"].ToString();

				BindAktivitas(rs.Rows[0]["Aktivitas"].ToString());

				pk.Text = rs.Rows[0]["Pk"].ToString();
				ket.Text = rs.Rows[0]["Ket"].ToString().Replace("<br>","\n");

				if(!Act.Sec("AL:"+Request.PhysicalPath)) ok.Enabled = false;
				if((string)rs.Rows[0]["Approve"]!="")
				{
					ok.Enabled = false;
					approveinfo.Text = (string)rs.Rows[0]["Approve"];
				}
			}

			BindButton();
		}

		private void BindButton()
		{
			string addq = "";
			string addh = "";
			if(Request.QueryString["pk"]!=null)
			{
				addq = " AND Pk = '"+Request.QueryString["pk"]+"'";
				addh = "&pk="+Request.QueryString["pk"];
			}

			int logid = Convert.ToInt32(LogID);
			long p = Db.SingleLong("SELECT ISNULL((SELECT TOP 1 LogID FROM "+Tb+" WHERE LogID < "+logid+addq+" ORDER BY LogID DESC),0)");
			long n = Db.SingleLong("SELECT ISNULL((SELECT TOP 1 LogID FROM "+Tb+" WHERE LogID > "+logid+addq+" ORDER BY LogID ASC),0)");
			if(p!=0) prev.HRef = "LogDetil.aspx?LogID="+p+"&tb="+Tb+"&sumber="+sumber.Text+addh; else prev.InnerHtml = "<img src='/Media/icon_prev_d.gif'>";
			if(n!=0) next.HRef = "LogDetil.aspx?LogID="+n+"&tb="+Tb+"&sumber="+sumber.Text+addh; else next.InnerHtml = "<img src='/Media/icon_next_d.gif'>";
		}

		private void BindAktivitas(string akt)
		{
			FillAkt(akt1 , "MS_UNIT_LOG");
			FillAkt(akt2 , "MS_AGENT_LOG");
			FillAkt(akt3 , "ISC064_MARKETINGJUAL..REF_SKEMA_LOG");
			FillAkt(akt4 , "ISC064_MARKETINGJUAL..REF_AGENT_TIPE_LOG");
            FillAkt(akt5 , "ISC064_MARKETINGJUAL..REF_AGENT_LEVEL_LOG");
            FillAkt(akt6 , "ISC064_MARKETINGJUAL..MS_GIMMICK_LOG");
            FillAkt(akt7 , "ISC064_MARKETINGJUAL..REF_TIPE_GIMMICK_LOG");

            try
			{
				aktivitas.SelectedValue = akt;
			}
			catch
			{
				aktivitas.Items.Add(new ListItem(akt));
				aktivitas.SelectedValue = akt;
			}
		}

		private void FillAkt(DropDownList akt, string table)
		{
			if(Tb==table)
			{
				for(int i=0;i<akt.Items.Count;i++)
					aktivitas.Items.Add(new ListItem(akt.Items[i].Text,akt.Items[i].Value));
			}
		}

		protected void ok_Click(object sender, System.EventArgs e)
		{
			ok.Enabled = false;
			
			string Approve = Act.UserID + ", " + Cf.Day(DateTime.Today);
			Db.Execute("UPDATE " + Tb
				+ " SET Approve = '" + Approve.Replace("'","''") +"'"
				+ " WHERE LogID = " + LogID
				);
		}

		private string Tb
		{
			get
			{
				return Cf.Pk(Request.QueryString["tb"]);
			}
		}

		private string LogID
		{
			get
			{
				return Cf.Pk(Request.QueryString["LogID"]);
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

using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
	public partial class KontrakIMBEdit : System.Web.UI.Page
	{
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			if(!IsPostBack)
				Fill();

			Js.Confirm(this, "Lanjutkan dengan proses edit IMB?");
			Js.Focus(this, ok);
		}

		protected void Fill()
		{
			cancel.Attributes["onclick"] = "location.href='KontrakProses.aspx?NoKontrak=" + NoKontrak + "'";

			
			string strSql = "SELECT "
				+ " MS_KONTRAK.*"
				+ ",MS_CUSTOMER.Nama AS Cs"
				+ " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
				+ " WHERE MS_KONTRAK.NoKontrak = '" + NoKontrak + "'";

			DataTable rsHeader = Db.Rs(strSql);

			if(rsHeader.Rows.Count==0)
				Response.Redirect("/CustomError/Deleted.html");
			else
			{
				nokontrak.Text = rsHeader.Rows[0]["NoKontrak"].ToString();
				unit.Text = rsHeader.Rows[0]["NoUnit"].ToString();
				customer.Text = rsHeader.Rows[0]["Cs"].ToString();

				tbTgl.Text = Cf.Day(rsHeader.Rows[0]["TglIMB"]);
				tbNoIMB.Text = rsHeader.Rows[0]["NoIMB"].ToString();
				keteranganimb.Text = rsHeader.Rows[0]["KetImb"].ToString();
		
				if(Convert.ToInt32(rsHeader.Rows[0]["StatusIMB"]) == 0)
				{
					rblStatus.SelectedIndex = 0;
					selesai.Visible = false;
				}
				else if(Convert.ToInt32(rsHeader.Rows[0]["StatusIMB"]) == 1)
				{
					rblStatus.SelectedIndex = 1;
					selesai.Visible = true;
				}
			}
		}

		
		private void Save()
		{
			string Status = "";
			//if(rblStatus.SelectedIndex != 0)
				Status = rblStatus.SelectedValue;
	
            string sql ="",sql1;
			sql1="SELECT "
				+ "StatusIMB AS [Status IMB]"
				+ ", TglIMB AS [Tgl. IMB]"
				+ ", NoIMB AS [No. IMB]"
				+ " FROM MS_KONTRAK"
				+ " WHERE NoKontrak = '" + NoKontrak + "'" ;

		
			DataTable rsBef = Db.Rs(sql1
				);
     
			sql = "UPDATE MS_KONTRAK"
				+ " SET StatusIMB = " + Status + ""
				+ ", NoImb = ''"
				+ ", KetImb = NULL"
				+ ", TglImb = NULL"
				+ " WHERE NoKontrak = '" + NoKontrak + "'";

			Db.Execute(sql);

			if(selesai.Visible)
			{
				DateTime Tgl = Convert.ToDateTime(tbTgl.Text);
				string NoIMB = Cf.Str(tbNoIMB.Text);
				//string keteranganimb = Cf.Str(keteranganimb.Text);

				Db.Execute("UPDATE MS_KONTRAK"
					+ " SET NoIMB = '" + NoIMB + "'"
					+ ", KetImb = '" + keteranganimb.Text + "'"
					+ ", TglIMB = '" + Tgl + "'"
					+ " WHERE NoKontrak = '" + NoKontrak + "'"
					);
			}

			DataTable rsAft = Db.Rs("SELECT "
				+ "StatusIMB AS [Status IMB]"
				+ ", TglIMB AS [Tgl. IMB]"
				+ ", NoIMB AS [No. IMB]"
				+ " FROM MS_KONTRAK"
				+ " WHERE NoKontrak = '" + NoKontrak + "'" 
				);

			//Logfile
			string Log = Cf.LogCompare(rsBef, rsAft);

			Db.Execute("EXEC spLogKontrak"
				+ " 'EDIT'"
				+ ",'" + Act.UserID + "'"
				+ ",'" + Act.IP + "'"
				+ ",'" + Log + "'"
				+ ",'" + NoKontrak + "'"
				);

			Response.Redirect("KontrakProses.aspx?NoKontrak=" + NoKontrak + "&done=8");
		}

		protected void ok_Click(object sender, System.EventArgs e)
		{
				Save();
		}

		protected void rblStatus_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(rblStatus.SelectedIndex == 1)
			{
				selesai.Visible = true;
				tbTgl.Text = Cf.Day(DateTime.Today);
			}
			else
			{
				selesai.Visible = false;
			}
		}

		private string NoKontrak
		{
			get
			{
				return Cf.Pk(Request.QueryString["NoKontrak"]);
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

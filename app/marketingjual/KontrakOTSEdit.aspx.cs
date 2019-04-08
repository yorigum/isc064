using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
	public partial class KontrakOTSEdit : System.Web.UI.Page
	{
		

		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			if(!IsPostBack)
				Fill();

			Js.Confirm(this, "Lanjutkan dengan proses edit OTS?");
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

				tgl.Text = Cf.Day(rsHeader.Rows[0]["TglOTS"]);
				target.Text = Cf.Day(rsHeader.Rows[0]["TargetOTS"]);
				ket.Text = rsHeader.Rows[0]["KetOTS"].ToString();
				
				if(rsHeader.Rows[0]["HasilOTS"].ToString() == "TOLAK")
					hasil.SelectedIndex = 0;
				else if(rsHeader.Rows[0]["HasilOTS"].ToString() == "SETUJU")
					hasil.SelectedIndex = 1;

				if(rsHeader.Rows[0]["StatusOTS"].ToString() == "")
				{
					status.SelectedIndex = 0;
					dijadwalkan.Visible = false;
					selesai.Visible = false;
				}
				else if(rsHeader.Rows[0]["StatusOTS"].ToString() == "TIDAK PERLU")
				{
					status.SelectedIndex = 1;
					dijadwalkan.Visible = false;
					selesai.Visible = false;
				}
				else if(rsHeader.Rows[0]["StatusOTS"].ToString() == "DIJADWALKAN")
				{
					status.SelectedIndex = 2;
					dijadwalkan.Visible = true;
					selesai.Visible = false;
				}
				else if(rsHeader.Rows[0]["StatusOTS"].ToString() == "SELESAI")
				{
					status.SelectedIndex = 3;
					dijadwalkan.Visible = true;
					selesai.Visible = true;
				}
			}
		}

		private bool Valid()
		{
			bool x = true;
			string s = "";

			if(dijadwalkan.Visible)
			{
				if(!Cf.isTgl(target))
				{
					x = false;

					if(s == "")
						s = target.ID;

					targetc.Text = "Tanggal";
				}
				else
					targetc.Text = "";
			}

			if(selesai.Visible)
			{
				if(!Cf.isTgl(tgl))
				{
					x = false;

					if(s == "")
						s = tgl.ID;

					tglc.Text = "Tanggal";
				}else
					tglc.Text = "";
			}

			if(!x)
			{
				this.RegisterStartupScript(
					"focusScript"
					, "<script language='javascript' type='text/javascript'>"
					+ "document.getElementById('" + s + "').focus();"
					+ "</script>"
					);
			}

			return x;
		}

		private void Save()
		{
			string Status = "";
			if(status.SelectedIndex != 0)
				Status = status.SelectedItem.Text;

			string Hasil = "", Ket = "";
			if(Status == "TIDAK PERLU")
				Hasil = Status;

			DataTable rsBef = Db.Rs("SELECT "
				+ "StatusOTS AS [Status OTS]"
				+ ", TargetOTS AS [Target OTS]"
				+ ", TglOTS AS [Tgl. OTS]"
				+ ", HasilOTS AS [Hasil OTS]"
				+ ", KetOTS AS [Keterangan OTS]"
				+ " FROM MS_KONTRAK"
				+ " WHERE NoKontrak = '" + NoKontrak + "'" 
				);

			Db.Execute("UPDATE MS_KONTRAK"
				+ " SET StatusOTS = '" + Status + "'"
				+ ", TargetOTS = NULL"
				+ ", TglOTS = NULL"
				+ ", HasilOTS = '" + Hasil + "'"
				+ ", KetOTS = ''"
				+ " WHERE NoKontrak = '" + NoKontrak + "'"
				);

			if(dijadwalkan.Visible)
			{
				DateTime Target = Convert.ToDateTime(target.Text);
				Hasil = "MENUNGGU";

				Db.Execute("UPDATE MS_KONTRAK"
					+ " SET TargetOTS = '" + Target + "'"
					+ ", TglOTS = NULL"
					+ ", HasilOTS = '" + Hasil + "'"
					+ ", KetOTS = ''"
					+ " WHERE NoKontrak = '" + NoKontrak + "'"
					);
			}

			if(selesai.Visible)
			{
				DateTime Tgl = Convert.ToDateTime(tgl.Text);
				Hasil = hasil.SelectedValue;
				Ket = Cf.Str(ket.Text);

				Db.Execute("UPDATE MS_KONTRAK"
					+ " SET TglOTS = '" + Tgl + "'"
					+ ", HasilOTS = '" + Hasil + "'"
					+ ", KetOTS = '" + Ket + "'"
					+ " WHERE NoKontrak = '" + NoKontrak + "'"
					);
			}

			DataTable rsAft = Db.Rs("SELECT "
				+ "StatusOTS AS [Status OTS]"
				+ ", TargetOTS AS [Target OTS]"
				+ ", TglOTS AS [Tgl. OTS]"
				+ ", HasilOTS AS [Hasil OTS]"
				+ ", KetOTS AS [Keterangan OTS]"
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

			Response.Redirect("KontrakProses.aspx?NoKontrak=" + NoKontrak + "&done=7");
		}

		protected void ok_Click(object sender, System.EventArgs e)
		{
			if(Valid())
				Save();
		}

		protected void status_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(status.SelectedItem.Text == "DIJADWALKAN")
			{
				dijadwalkan.Visible = true;
				selesai.Visible = false;
			}
			else if(status.SelectedItem.Text == "SELESAI")
			{
				dijadwalkan.Visible = true;
				selesai.Visible = true;
			}
			else
			{
				dijadwalkan.Visible = false;
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

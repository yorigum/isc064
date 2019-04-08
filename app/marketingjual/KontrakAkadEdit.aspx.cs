using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
	public partial class KontrakAkadEdit : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			if(!IsPostBack)
			{
				nilai.Attributes["onfocus"] = "tempnum=CalcFocus(this);tempx=this.value";
				nilai.Attributes["onkeyup"] = "CalcType(this,tempnum);";
				nilai.Attributes["onblur"] = "CalcBlur(this);";

				Fill();
			}

			Js.Confirm(this, "Lanjutkan dengan proses edit Akad?");
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

				tbTgl.Text = Cf.Day(rsHeader.Rows[0]["TglAkad"]);
				tbTarget.Text = Cf.Day(rsHeader.Rows[0]["TargetAkad"]);
				tbNoAkad.Text = rsHeader.Rows[0]["NoAkad"].ToString();
				tbKet.Text = rsHeader.Rows[0]["KetAkad"].ToString();
				nilai.Text = Cf.Num(rsHeader.Rows[0]["RealisasiAkad"]);

				if(rsHeader.Rows[0]["StatusAkad"].ToString() == "")
				{
					rblStatus.SelectedIndex = 0;
					dijadwalkan.Visible = false;
					selesai.Visible = false;
				}
				else if(rsHeader.Rows[0]["StatusAkad"].ToString() == "TIDAK PERLU")
				{
					rblStatus.SelectedIndex = 1;
					dijadwalkan.Visible = false;
					selesai.Visible = false;
				}
				else if(rsHeader.Rows[0]["StatusAkad"].ToString() == "DIJADWALKAN")
				{
					rblStatus.SelectedIndex = 2;
					dijadwalkan.Visible = true;
					selesai.Visible = false;
				}
				else if(rsHeader.Rows[0]["StatusAkad"].ToString() == "SELESAI")
				{
					rblStatus.SelectedIndex = 3;
					dijadwalkan.Visible = true;
					selesai.Visible = true;
				}

//				if(Convert.ToBoolean(rsHeader.Rows[0]["CadanganAkad"]))
//					cadangan.SelectedIndex = 1;
//				else
//					cadangan.SelectedIndex = 0;
			}
		}

		private bool Valid()
		{
			bool x = true;
			string s = "";

			if(dijadwalkan.Visible)
			{
				if(!Cf.isTgl(tbTarget))
				{
					x = false;

					if(s == "")
						s = tbTarget.ID;

					lblTarget.Text = "Tanggal";
				}
				else
					lblTarget.Text = "";
			}

			if(selesai.Visible)
			{
				if(!Cf.isTgl(tbTgl))
				{
					x = false;

					if(s == "")
						s = tbTgl.ID;

					lblTgl.Text = "Tanggal";
				}else
					lblTgl.Text = "";

				if(!Cf.isMoney(nilai))
				{
					x = false;

					if(s == "")
						s = nilai.ID;

					nilaic.Text = "Angka";
				}else
					nilaic.Text = "";
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
			if(rblStatus.SelectedIndex != 0)
				Status = rblStatus.SelectedItem.Text;

			DataTable rsBef = Db.Rs("SELECT "
				+ "StatusAkad AS [Status Akad]"
				+ ", TargetAkad AS [Target Akad]"
				+ ", TglAkad AS [Tgl. Akad]"
				+ ", NoAkad AS [No. Akad]"
				+ ", KetAkad AS [Keterangan Akad]"
				+ ", RealisasiAkad AS [Realisasi Akad]"
//				+ ", CadanganAkad AS [Cadangan Akad]"
				+ " FROM MS_KONTRAK"
				+ " WHERE NoKontrak = '" + NoKontrak + "'" 
				);

			Db.Execute("UPDATE MS_KONTRAK"
				+ " SET StatusAkad = '" + Status + "'"
				+ ", TargetAkad = NULL"
				+ ", NoAkad = ''"
				+ ", TglAkad = NULL"
				+ ", KetAkad = ''"
				+ " WHERE NoKontrak = '" + NoKontrak + "'"
				);

			if(dijadwalkan.Visible)
			{
				DateTime Target = Convert.ToDateTime(tbTarget.Text);

				Db.Execute("UPDATE MS_KONTRAK"
					+ " SET TargetAkad = '" + Target + "'"
					+ ", NoAkad = ''"
					+ ", TglAkad = NULL"
					+ ", KetAkad = ''"
					+ " WHERE NoKontrak = '" + NoKontrak + "'"
					);
			}

			if(selesai.Visible)
			{
				DateTime Tgl = Convert.ToDateTime(tbTgl.Text);
				string Ket = Cf.Str(tbKet.Text);
				string NoAkad = Cf.Str(tbNoAkad.Text);
				decimal Nilai = Convert.ToDecimal(nilai.Text);
//				string CadanganAkad = cadangan.SelectedValue;

				Db.Execute("UPDATE MS_KONTRAK"
					+ " SET NoAkad = '" + NoAkad + "'"
					+ ", TglAkad = '" + Tgl + "'"
					+ ", KetAkad = '" + Ket + "'"
					+ ", RealisasiAkad = " + Nilai
//					+ ", CadanganAkad = " + CadanganAkad
					+ " WHERE NoKontrak = '" + NoKontrak + "'"
					);

				//Settlement PAkad
				Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_PAKAD"
					+ " SET Status = 'S'"
					+ " WHERE Ref = '" + NoKontrak + "'"
					+ " AND Status = 'A'"
					);
			}

			DataTable rsAft = Db.Rs("SELECT "
				+ "StatusAkad AS [Status Akad]"
				+ ", TargetAkad AS [Target Akad]"
				+ ", TglAkad AS [Tgl. Akad]"
				+ ", NoAkad AS [No. Akad]"
				+ ", KetAkad AS [Keterangan Akad]"
				+ ", RealisasiAkad AS [Realisasi Akad]"
//				+ ", CadanganAkad AS [Cadangan Akad]"
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

			Response.Redirect("KontrakProses.aspx?NoKontrak=" + NoKontrak + "&done=5");
		}

		protected void ok_Click(object sender, System.EventArgs e)
		{
			if(Valid())
				Save();
		}

		protected void rblStatus_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(rblStatus.SelectedItem.Text == "DIJADWALKAN")
			{
				dijadwalkan.Visible = true;
				selesai.Visible = false;
			}
			else if(rblStatus.SelectedItem.Text == "SELESAI")
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

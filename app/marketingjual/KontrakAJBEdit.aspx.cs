using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
	public partial class KontrakAJBEdit : System.Web.UI.Page
	{
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			if(!IsPostBack)
				Fill();

			Js.Confirm(this, "Lanjutkan dengan proses edit AJB?");
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

				tbTgl.Text = Cf.Day(rsHeader.Rows[0]["TglAJB"]);
				tbNoAJB.Text = rsHeader.Rows[0]["NoAJB"].ToString();
				tbNamaNotaris.Text = rsHeader.Rows[0]["NamaNotaris"].ToString();
				tbKet.Text = rsHeader.Rows[0]["KetAJB"].ToString();
				noroya.Text = rsHeader.Rows[0]["NoRoya"].ToString();

				if(rsHeader.Rows[0]["StatusAJB"].ToString() == "")
				{
					rblStatus.SelectedIndex = 0;
					selesai.Visible = false;
				}
				else if(rsHeader.Rows[0]["StatusAJB"].ToString() == "SELESAI")
				{
					rblStatus.SelectedIndex = 1;
					selesai.Visible = true;
				}
			}
		}

		private bool Valid()
		{
			bool x = true;
			string s = "";

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
				+ "StatusAJB AS [Status AJB]"
				+ ", NamaNotaris AS [Nama Notaris]"
				+ ", TglAJB AS [Tgl. AJB]"
				+ ", NoAJB AS [No. AJB]"
				+ ", KetAJB AS [Keterangan AJB]"
				+ ", NoRoya AS [No. Roya]"
				+ " FROM MS_KONTRAK"
				+ " WHERE NoKontrak = '" + NoKontrak + "'" 
				);

			Db.Execute("UPDATE MS_KONTRAK"
				+ " SET StatusAJB = '" + Status + "'"
				+ ", NamaNotaris = ''"
				+ ", NoAJB = ''"
				+ ", TglAJB = NULL"
				+ ", KetAJB = ''"
				+ " WHERE NoKontrak = '" + NoKontrak + "'"
				);

			if(selesai.Visible)
			{
				DateTime Tgl = Convert.ToDateTime(tbTgl.Text);
				string Ket = Cf.Str(tbKet.Text);
				string NamaNotaris = Cf.Str(tbNamaNotaris.Text);
				string NoAJB = Cf.Str(tbNoAJB.Text);
				string NoRoya = Cf.Str(noroya.Text);

				Db.Execute("UPDATE MS_KONTRAK"
					+ " SET NoAJB = '" + NoAJB + "'"
					+ ", TglAJB = '" + Tgl + "'"
					+ ", NamaNotaris = '" + NamaNotaris + "'"
					+ ", KetAJB = '" + Ket + "'"
					+ ", NoRoya = '" + NoRoya + "'"
					+ " WHERE NoKontrak = '" + NoKontrak + "'"
					);
			}

			DataTable rsAft = Db.Rs("SELECT "
				+ "StatusAJB AS [Status AJB]"
				+ ", NamaNotaris AS [Nama Notaris]"
				+ ", TglAJB AS [Tgl. AJB]"
				+ ", NoAJB AS [No. AJB]"
				+ ", KetAJB AS [Keterangan AJB]"
				+ ", NoRoya AS [No. Roya]"
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
			if(rblStatus.SelectedItem.Text == "SELESAI")
				selesai.Visible = true;
			else
				selesai.Visible = false;
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

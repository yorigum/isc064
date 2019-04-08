using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
	public partial class KomisiDel : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			Act.CekInt("NoUrut");

			DataTable rs = Db.Rs(
				"SELECT * FROM MS_KOMISI WHERE NoKontrak = '" + NoKontrak + "'"
				+ " AND NoUrut = " + NoUrut);

			int totalkomisi = Db.SingleInteger(
				"SELECT COUNT(*) FROM MS_KOMISI WHERE NoKontrak = '" + NoKontrak + "'"
				);

			if(rs.Rows.Count==0)
				Response.Redirect("/CustomError/Deleted.html");
			else
			{
				DataTable rsBef = Db.Rs("SELECT "
					+ "CONVERT(VARCHAR,NoUrut) + '.  ' + NamaKomisi + ' ('+Tipe+')   CAIR:' + CONVERT(VARCHAR,TermCair,1) + '% (' + Jadwal + ')  ' + CONVERT(VARCHAR,NilaiKomisi,1) "
					+ "FROM MS_KOMISI WHERE NoKontrak = '" + NoKontrak + "' ORDER BY NoUrut");
			
				Db.Execute("EXEC spKomisiDel "
					+ " '" + NoKontrak + "'"
					+ ", " + NoUrut
					);

				int c = Db.SingleInteger(
					"SELECT COUNT(*) FROM MS_KOMISI WHERE NoKontrak = '" + NoKontrak + "'"
					);

				if(c!=totalkomisi)
				{
					//Log
					DataTable rsAft = Db.Rs("SELECT "
						+ "CONVERT(VARCHAR,NoUrut) + '.  ' + NamaKomisi + ' ('+Tipe+')   CAIR:' + CONVERT(VARCHAR,TermCair,1) + '% (' + Jadwal + ')  ' + CONVERT(VARCHAR,NilaiKomisi,1) "
						+ "FROM MS_KOMISI WHERE NoKontrak = '" + NoKontrak + "' ORDER BY NoUrut");

					DataTable rsDetail = Db.Rs("SELECT"
						+ " MS_KONTRAK.NoKontrak AS [No. Kontrak]"
						+ ",MS_KONTRAK.NoUnit AS [Unit]"
						+ ",MS_CUSTOMER.Nama AS [Customer]"
						+ ",MS_KONTRAK.NilaiKontrak AS [Nilai Kontrak]"
						+ ",MS_KONTRAK.SkemaKomisi AS [Skema Komisi]"
						+ " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER"
						+ " ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
						+ " WHERE MS_KONTRAK.NoKontrak = '" + NoKontrak + "'");

					string Ket = Cf.LogCapture(rsDetail)
						+ "<br>---DELETE KOMISI---<br>"
						+ Cf.LogList(rsBef, rsAft , "JADWAL KOMISI");
				
					Db.Execute("EXEC spLogKontrak"
						+ " 'EJK'"
						+ ",'" + Act.UserID + "'"
						+ ",'" + Act.IP + "'"
						+ ",'" + Ket + "'"
						+ ",'" + NoKontrak + "'"
						);
				
					Response.Redirect("KomisiEdit.aspx?NoKontrak="+NoKontrak+"&done=1");
				}
				else
				{
					//Tidak bisa dihapus
					nodel.Visible = true;
				}
			}
		}

		private string NoKontrak
		{
			get
			{
				return Cf.Pk(Request.QueryString["NoKontrak"]);
			}
		}

		private string NoUrut
		{
			get
			{
				return Cf.Pk(Request.QueryString["NoUrut"]);
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

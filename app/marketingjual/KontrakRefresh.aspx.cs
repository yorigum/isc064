using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
	public partial class KontrakRefresh : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			DataTable rs = Db.Rs(
				"SELECT * FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "' AND Status = 'A'");

			if(rs.Rows.Count==0)
			{
				nodel.Visible = true;
			}
			else
			{
				DataTable rsBef = Db.Rs("SELECT "
					+ " Luas AS [Luas]"
					+ ",Gross AS [Nilai Gross]"
					+ ",DiskonRupiah AS [Diskon dalam Rupiah]"
					+ ",DiskonPersen AS [Diskon dalam Persen]"
					+ ",NoUnit AS [No. Unit]"
					+ ",Jenis AS [Jenis]"
					+ ",Lokasi AS [Lokasi]"
					+ " FROM MS_KONTRAK "
					+ " WHERE NoKontrak = '" + NoKontrak + "'");

				Db.Execute("EXEC spKontrakRefresh '"+NoKontrak+"'");

				DataTable rsAft = Db.Rs("SELECT "
					+ " Luas AS [Luas]"
					+ ",Gross AS [Nilai Gross]"
					+ ",DiskonRupiah AS [Diskon dalam Rupiah]"
					+ ",DiskonPersen AS [Diskon dalam Persen]"
					+ ",NoUnit AS [No. Unit]"
					+ ",Jenis AS [Jenis]"
					+ ",Lokasi AS [Lokasi]"
					+ " FROM MS_KONTRAK "
					+ " WHERE NoKontrak = '" + NoKontrak + "'");

				DataTable rsDet = Db.Rs("SELECT "
					+ " NoKontrak AS [No. Kontrak]"
					+ ",NoUnit AS [Unit]"
					+ ",MS_CUSTOMER.Nama AS [Customer]"
					+ " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER"
					+ " ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
					+ " WHERE NoKontrak = '" + NoKontrak + "'");

				string Ket = Cf.LogCapture(rsDet)
					+ Cf.LogCompare(rsBef,rsAft)
					;

				Db.Execute("EXEC spLogKontrak "
					+ " 'REF'"
					+ ",'" + Act.UserID + "'"
					+ ",'" + Act.IP + "'"
					+ ",'" + Ket + "'"
					+ ",'" + NoKontrak + "'"
					);

				Response.Redirect("KontrakEdit.aspx?done=1&NoKontrak=" + NoKontrak);
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

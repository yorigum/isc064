using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.ADMINJUAL
{
	public partial class SkemaDel : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			Act.CekInt("Nomor");

			if(!Page.IsPostBack)
			{
				Js.Focus(this, ket);
				Js.Confirm(this,
					"Apakah anda ingin menghapus skema cara bayar : "+Nomor+" ?\\n"
					+ "Perhatian bahwa data akan dihapus secara PERMANEN."
					);
			}
		}

		protected void delbtn_Click(object sender, System.EventArgs e)
		{
			DataTable rs = Db.Rs(
				"SELECT * FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_SKEMA WHERE Nomor = " + Nomor);

			if(rs.Rows.Count==0)
				Response.Redirect("/CustomError/Deleted.html");
            else if (!Act.AksesProject(rs.Rows[0]["Project"].ToString()))
                Response.Redirect("/CustomError/SecLevel.html");
            else
			{
				DataTable rsHeader = Db.Rs("SELECT "
					+ " Nomor"
					+ ",Nama"
					+ ",Diskon"
					+ ",RThousand AS [Pembulatan Nilai]"
					+ ",Status"
					+ " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_SKEMA "
					+ " WHERE Nomor = " + Nomor);

				DataTable rsDetail = Db.Rs("SELECT "
					+ " CONVERT(VARCHAR, Baris) "
					+ " + '.  ' + Nama + ' (' + Tipe + ')  ' "
					+ " + TipeNominal + CONVERT(VARCHAR, Nominal, 1) + '  ' "
					+ " + TipeJadwal + '(' + CONVERT(VARCHAR, IntJadwal) + ')' + "
					+ " ISNULL(CONVERT(VARCHAR, TglFix, 106), 'NULL') + '  ' "
					+ " + 'REF:' + CONVERT(VARCHAR,RefJadwal) + '  ' "
					+ " + 'BF:' + CONVERT(VARCHAR, BF)"
					+ " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_SKEMA_DETAIL WHERE Nomor = " + Nomor);

                int cekdigunakan = Db.SingleInteger("select COUNT(distinct(RefSkema)) from " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK inner join " + Mi.DbPrefix + "MARKETINGJUAL..REF_SKEMA on " + Mi.DbPrefix + "MARKETINGJUAL..REF_SKEMA.Nomor = MS_KONTRAK.RefSkema"
	                + " where " + Mi.DbPrefix + "MARKETINGJUAL..REF_SKEMA.Nomor = " + Nomor);
                if (cekdigunakan == 0)
                {
                    Db.Execute("EXEC " + Mi.DbPrefix + "MARKETINGJUAL..spSkemaDel "
                        + Nomor
                        );

                    string Ket = "***Alasan Delete :<br>" + Cf.Str(ket.Text)
                        + "<br><br>***Data Sebelum Delete :<br>"
                        + Cf.LogCapture(rsHeader)
                        + Cf.LogList(rsDetail, "RUMUS");

                    int c = Db.SingleInteger(
                        "SELECT COUNT(*) FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_SKEMA WHERE Nomor = " + Nomor);

                    if (c == 0)
                    {
                        Db.Execute("EXEC " + Mi.DbPrefix + "MARKETINGJUAL..spLogSkema"
                            + " 'DELETE'"
                            + ",'" + Act.UserID + "'"
                            + ",'" + Act.IP + "'"
                            + ",'" + Ket + "'"
                            + ",'" + Nomor.PadLeft(3, '0') + "'"
                            );

                        decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM REF_SKEMA_LOG ORDER BY LogID DESC");
                        string Project = Db.SingleString("SELECT Project FROM REF_SKEMA WHERE Nomor = '" + Nomor + "'");
                        Db.Execute("UPDATE REF_SKEMA_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                        Js.Close(this);
                    }
                }
                else
                {
                    //Tidak bisa dihapus
                    frm.Visible = false;
                    nodel.Visible = true;
                }
                
			}
		}

		private string Nomor
		{
			get
			{
				return Cf.Pk(Request.QueryString["Nomor"]);
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

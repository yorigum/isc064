using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.ADMINJUAL
{
	public partial class UnitEditSpek : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			if(!Page.IsPostBack)
			{
				Fill();
			}

			FeedBack();
		}

		private void FeedBack()
		{
			feed.Text = "";
			if(!Page.IsPostBack)
			{
				if(Request.QueryString["done"]!=null)
					feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
						+ "Edit Berhasil...";
			}
		}

		private void Fill()
		{
			string strSql = "SELECT * FROM MS_UNIT WHERE NoStock = '" + NoStock + "'";
			DataTable rs = Db.Rs(strSql);

			if(rs.Rows.Count==0)
				Response.Redirect("/CustomError/Deleted.html");
			else
			{
				panjang.Text = Cf.Num(rs.Rows[0]["Panjang"]);
				lebar.Text = Cf.Num(rs.Rows[0]["Lebar"]);
				tinggi.Text = Cf.Num(rs.Rows[0]["Tinggi"]);
				luassg.Text = Cf.Num(rs.Rows[0]["LuasSG"]);
				luasnett.Text = Cf.Num(rs.Rows[0]["LuasNett"]);
				lebarjalan.Text = Cf.Num(rs.Rows[0]["LebarJalan"]);

                hargagimmick.Text = Cf.Num(rs.Rows[0]["TambahanHargaGimmick"]);
                hargalainlain.Text = Cf.Num(rs.Rows[0]["TambahanHargaLainLain"]);

				zoning.Text = rs.Rows[0]["Zoning"].ToString();
				arahhadap.Text = rs.Rows[0]["ArahHadap"].ToString();
				panorama.Text = rs.Rows[0]["Panorama"].ToString();

				HadapAtrium.Checked = (bool)rs.Rows[0]["HadapAtrium"];
				HadapEntrance.Checked = (bool)rs.Rows[0]["HadapEntrance"];
				HadapEskalator.Checked = (bool)rs.Rows[0]["HadapEskalator"];
				HadapLift.Checked = (bool)rs.Rows[0]["HadapLift"];
				HadapParkir.Checked = (bool)rs.Rows[0]["HadapParkir"];
				HadapAxis.Checked = (bool)rs.Rows[0]["HadapAxis"];
				Hook.Checked = (bool)rs.Rows[0]["Hook"];

                NJ.Text  = rs.Rows[0]["NamaJalan"].ToString();

                if ((bool)rs.Rows[0]["Outdoor"])
					outdoor.Checked = true;
				else
					indoor.Checked = true;
			}
		}

		private bool valid()
		{
			string s = "";
			bool x = true;

			if(!Cf.isMoney(panjang))
			{
				x = false;
				if(s=="") s = panjang.ID;
				panjangc.Text = "Angka";
			}
			else
				panjangc.Text = "";


            if (!Cf.isMoney(hargagimmick))
            {
                x = false;
                if (s == "") s = hargagimmick.ID;
                hargagimmickc.Text = "Angka";
            }
            else
                hargagimmickc.Text = "";

            if (!Cf.isMoney(hargalainlain))
            {
                x = false;
                if (s == "") s = hargalainlain.ID;
                hargalainlainc.Text = "Angka";
            }
            else
                hargalainlainc.Text = "";

			if(!Cf.isMoney(lebar))
			{
				x = false;
				if(s=="") s = lebar.ID;
				lebarc.Text = "Angka";
			}
			else
				lebarc.Text = "";

			if(!Cf.isMoney(tinggi))
			{
				x = false;
				if(s=="") s = tinggi.ID;
				tinggic.Text = "Angka";
			}
			else
				tinggic.Text = "";

			if(!Cf.isMoney(luassg))
			{
				x = false;
				if(s=="") s = luassg.ID;
				luassgc.Text = "Angka";
			}
			else
				luassgc.Text = "";

			if(!Cf.isMoney(luasnett))
			{
				x = false;
				if(s=="") s = luasnett.ID;
				luasnettc.Text = "Angka";
			}
			else
				luasnettc.Text = "";

			if(!Cf.isMoney(lebarjalan))
			{
				x = false;
				if(s=="") s = lebarjalan.ID;
				lebarjalanc.Text = "Angka";
			}
			else
				lebarjalanc.Text = "";

			if(!Cf.isMoney(panjang))
			{
				x = false;
				if(s=="") s = panjang.ID;
				panjangc.Text = "Angka";
			}
			else
				panjangc.Text = "";

			if(!Cf.isMoney(lebar))
			{
				x = false;
				if(s=="") s = lebar.ID;
				lebarc.Text = "Angka";
			}
			else
				lebarc.Text = "";

			if(!x)
				Js.Alert(
					this
					, "Input Tidak Valid."
					, "document.getElementById('"+s+"').focus();"
					+ "document.getElementById('"+s+"').select();"
					);

			return x;
		}

		private bool Save()
		{
			if(valid())
			{
				DataTable bef = Db.Rs("SELECT "
					+ " Zoning"
					+ ",ArahHadap AS [Arah Hadap]"
					+ ",Panorama"
					+ ",Panjang"
					+ ",Lebar"
					+ ",Tinggi"
					+ ",LuasSG AS [Luas Tanah]"
					+ ",LuasNett AS [Luas Bangunan]"
					+ ",HadapAtrium AS [Hadap Atrium/Void]"
					+ ",HadapEntrance AS [Hadap Entrance]"
					+ ",HadapEskalator AS [Hadap Eskalator]"
					+ ",HadapLift AS [Hadap Lift]"
					+ ",HadapParkir AS [Hadap Parkir]"
					+ ",HadapAxis AS [Hadap Axis]"
					+ ",Hook AS [Hook]"
					+ ",LebarJalan AS [Lebar Jalan]"
					+ ",Outdoor AS [Outdoor]"
                    + ",NamaJalan AS [Nama Jalan]"
                    + " FROM MS_UNIT "
					+ " WHERE NoStock = '"+NoStock+"'");

				string Zoning = Cf.Str(zoning.Text);
				string ArahHadap = Cf.Str(arahhadap.Text);
				string Panorama = Cf.Str(panorama.Text);
				decimal Panjang = Convert.ToDecimal(panjang.Text);
				decimal Lebar = Convert.ToDecimal(lebar.Text);
				decimal Tinggi = Convert.ToDecimal(tinggi.Text);
				decimal LuasSG = Convert.ToDecimal(luassg.Text);
				decimal LuasNett = Convert.ToDecimal(luasnett.Text);
				decimal LebarJalan = Convert.ToDecimal(lebarjalan.Text);

                decimal HargaGimmick = Convert.ToDecimal(hargagimmick.Text);
                decimal HargaLainLain = Convert.ToDecimal(hargalainlain.Text);
                string NamaJalan = NJ.Text;
                
				Db.Execute("EXEC spUnitEditSpek"
					+ " '" + NoStock + "'"
					+ ",'" + Zoning + "'"
					+ ", " + Panjang
					+ ", " + Lebar
					+ ", " + Tinggi
					+ ", " + LuasSG
					+ ", " + LuasNett
					+ ", " + Cf.BoolToSql(HadapAtrium.Checked)
					+ ", " + Cf.BoolToSql(HadapEntrance.Checked)
					+ ", " + Cf.BoolToSql(HadapEskalator.Checked)
					+ ", " + Cf.BoolToSql(HadapLift.Checked)
					+ ", " + Cf.BoolToSql(HadapParkir.Checked)
					+ ", " + Cf.BoolToSql(HadapAxis.Checked)
					+ ", " + Cf.BoolToSql(Hook.Checked)
					+ ", " + LebarJalan
					+ ", " + Cf.BoolToSql(outdoor.Checked)
					+ ",'" + ArahHadap + "'"
					+ ",'" + Panorama + "'"
					);

                Db.Execute("UPDATE MS_UNIT SET TambahanHargaGimmick = '"+HargaGimmick+"', TambahanHargaLainLain = '"+HargaLainLain+"',NamaJalan = '" + NamaJalan + "' WHERE NoStock = '"+NoStock+"'  ");

                DataTable kon = Db.Rs("SELECT * FROM MS_KONTRAK WHERE NoStock = '" + NoStock + "' AND Status = 'A'");
                if (kon.Rows.Count > 0)
                {
                    Db.Execute("UPDATE MS_KONTRAK SET Revisi = Revisi + 1 WHERE NoKontrak = '" + kon.Rows[0]["NoKontrak"].ToString() + "'");
                }

				DataTable aft = Db.Rs("SELECT "
					+ " Zoning"
					+ ",ArahHadap AS [Arah Hadap]"
					+ ",Panorama"
					+ ",Panjang"
					+ ",Lebar"
					+ ",Tinggi"
					+ ",LuasSG AS [Luas Tanah]"
					+ ",LuasNett AS [Luas Bangunan]"
					+ ",HadapAtrium AS [Hadap Atrium/Void]"
					+ ",HadapEntrance AS [Hadap Entrance]"
					+ ",HadapEskalator AS [Hadap Eskalator]"
					+ ",HadapLift AS [Hadap Lift]"
					+ ",HadapParkir AS [Hadap Parkir]"
					+ ",HadapAxis AS [Hadap Axis]"
					+ ",Hook AS [Hook]"
					+ ",LebarJalan AS [Lebar Jalan]"
					+ ",Outdoor AS [Outdoor]"
                    + ",NamaJalan AS [Nama Jalan]"
                    + " FROM MS_UNIT "
					+ " WHERE NoStock = '"+NoStock+"'");

				//Logfile
				string Ket = Cf.LogCompare(bef,aft);

				Db.Execute("EXEC spLogUnit"
					+ " 'EDIT'"
					+ ",'" + Act.UserID + "'"
					+ ",'" + Act.IP + "'"
					+ ",'" + Ket + "'"
					+ ",'" + NoStock + "'"
					);

				return true;
			}
			else
				return false;
		}

		protected void ok_Click(object sender, System.EventArgs e)
		{
			if(Save()) Js.Close(this);
		}

		protected void save_Click(object sender, System.EventArgs e)
		{
			if(Save()) Response.Redirect("UnitEditSpek.aspx?done=1&NoStock="+NoStock);
		}

		private string NoStock
		{
			get
			{
				return Cf.Pk(Request.QueryString["NoStock"]);
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

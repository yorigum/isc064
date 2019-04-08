using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
	public partial class KontrakDiskon : System.Web.UI.Page
	{
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			if(!Page.IsPostBack)
			{
				backbtn.Visible = false;
				nokontrak.Attributes["ondblclick"] = "popDaftarKontrak('a');";
				
				if(Request.QueryString["NoKontrak"]!=null)
				{
					//dari halaman pendaftaran
					dariDaftar.Checked = true;
					nokontrak.Text = Request.QueryString["NoKontrak"];
					LoadKontrak();

					InitForm();
					Fill();


					if(Request.QueryString["gross"]!=null)
						//dari halaman gross
						cancel.Attributes["onclick"] = "location.href='ReminderGross.aspx'";
					else
						cancel.Attributes["onclick"] = "location.href='KontrakDaftar3.aspx?NoKontrak="+NoKontrak+"'";
				}
				else
				{
					Js.Focus(this,nokontrak);
					frm.Visible = false;
				}
			}

			FeedBack();
			if(frm.Visible) Js.Confirm(this, "Lanjutkan proses diskon nilai kontrak?");
		}

		private void FeedBack()
		{
			feed.Text = "";
			if(!Page.IsPostBack)
			{
				if(Request.QueryString["done"]!=null)
					feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
						+ "<a href=\"javascript:popEditKontrak('"+Request.QueryString["done"]+"')\">"
						+ "Prosedur Diskon Berhasil..."
						+ "</a>";
			}
		}

		private bool valid()
		{
			bool x = true;

			int c = Db.SingleInteger(
				"SELECT COUNT(*) FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "' AND Status = 'A'");

			if(c==0)
				x = false; 

			
			if(!x)
				Js.Alert(
					this
					, "Kontrak Tidak Valid.\\n\\n"
					+ "Kemungkinan Sebab :\\n"
					+ "1. Kontrak tersebut tidak terdaftar.\\n"
					+ "2. Kontrak tersebut sudah dibatalkan.\\n"
					, "document.getElementById('nokontrak').focus();"
					+ "document.getElementById('nokontrak').select();"
					);

			return x;
		}

		private void LoadKontrak()
		{
			if(valid())
			{
				pilih.Visible = false;
				frm.Visible = true;

				InitForm();
				Fill();

				Js.Focus(this, disc);

				if(frm.Visible) Js.Confirm(this, "Lanjutkan proses diskon nilai kontrak?");
			}
			else
			{
				backbtn.Visible = true;
				Js.Focus(this,nokontrak);
				frm.Visible = false;
			}
		}

		protected void next_Click(object sender, System.EventArgs e)
		{
			if(valid())
			{
				pilih.Visible = false;
				frm.Visible = true;

				InitForm();
				Fill();

				Js.Focus(this, disc);

				if(frm.Visible) Js.Confirm(this, "Lanjutkan proses diskon nilai kontrak?");
			}
		}

		private void InitForm()
		{
			//NumberFormat.js
			gross.Attributes["style"] = nilai.Attributes["style"] = "border:0px;font:bold;";
			dpp.Attributes["style"] = "border:0px;font:bold;";

			disc.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
			disc.Attributes["onkeyup"] = "CalcType(this,tempnum);";

            diskontambahan.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            diskontambahan.Attributes["onkeyup"] = "CalcType(this,tempnum);";

		//	disc.Attributes["onblur"] = "CalcBlur(this); hitungPPN(gross, this, bunga); CalcBlur(ppn,tempnum);";
			
			bunga.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
			bunga.Attributes["onkeyup"] = "CalcType(this,tempnum);";
		//	bunga.Attributes["onblur"] = "CalcBlur(this); hitungPPN(gross, disc, this); CalcBlur(ppn,tempnum);";

            gimmick.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            gimmick.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            gimmick.Attributes["onblur"] = "kaliLuas(this, lainlain, gross)";

            lainlain.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            lainlain.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            lainlain.Attributes["onblur"] = "kaliLuas(gimmick, this, gross)";
		}

		private void Fill()
		{
			Func.KontrakHeader(NoKontrak, nokontrakl, unit, customer, agent);

			string strSql = "SELECT "
				+ " MS_UNIT.PriceListMin"
				+ ",MS_KONTRAK.Gross"
				+ ",MS_KONTRAK.NilaiKontrak"
				+ ",MS_KONTRAK.DiskonRupiah"
                + ",MS_KONTRAK.DiskonTambahan"
				+ ",MS_KONTRAK.FlagGross"
				+ ",MS_KONTRAK.OutBalance"
                + ",MS_KONTRAK.HargaLainLain"
                + ",MS_KONTRAK.HargaGimmick"
				+ ",(SELECT ISNULL(SUM(NilaiTagihan),0) FROM MS_TAGIHAN WHERE NoKontrak = MS_KONTRAK.NoKontrak) AS TotalTagihan"
				+ ",MS_KONTRAK.Skema"
				+ ",MS_KONTRAK.FlagKomisi"
				+ ", MS_KONTRAK.NilaiPPN"
				+ ", MS_KONTRAK.PPN"
				+ ", MS_KONTRAK.BungaNominal"
				+ " FROM MS_KONTRAK INNER JOIN MS_UNIT ON MS_KONTRAK.NoStock = MS_UNIT.NoStock"
				+ " WHERE MS_KONTRAK.NoKontrak = '" + NoKontrak + "'";
			DataTable rs = Db.Rs(strSql);

			if(rs.Rows.Count==0)
				Response.Redirect("/CustomError/Deleted.html");
			else
			{
				string ket = "";
				switch((int)rs.Rows[0]["FlagGross"])
				{
					case 1:
						ket = "Edit Kontrak";
						break;
					case 2:
						ket = "Refresh Unit";
						break;
					case 3:
						ket = "Pindah Unit";
						break;
					case 4:
						ket = "Serah Terima";
						break;
				}
				if(ket!="") discinfo.Text = "Gross berubah karena " + ket + "<br><br>";

				gross.Text = Cf.Num(rs.Rows[0]["Gross"]);
				disc.Text = Cf.Num(rs.Rows[0]["DiskonRupiah"]);
                gimmick.Text = Cf.Num(rs.Rows[0]["HargaGimmick"]);
                lainlain.Text = Cf.Num(rs.Rows[0]["HargaLainLain"]);
                totalharga.Text = Cf.Num(Convert.ToDecimal(gross.Text) + Convert.ToDecimal(gimmick.Text) + Convert.ToDecimal(lainlain.Text)); 
                diskontambahan.Text = Cf.Num(rs.Rows[0]["DiskonTambahan"]);
				decimal bng = Db.SingleDecimal("SELECT ISNULL(BungaNominal,0) FROM MS_KONTRAK WHERE NoKontrak = '"+ NoKontrak +"'");
				bunga.Text = Cf.Num(bng);//rs.Rows[0]["BungaNominal"]);
                ppnlabel.Text = Cf.Num(rs.Rows[0]["NilaiPPN"]);
			//	ppn.Text = Cf.Num(rs.Rows[0]["NilaiPPN"]);
				nilai.Text = Cf.Num(rs.Rows[0]["NilaiKontrak"]);

				if ((bool)rs.Rows[0]["PPN"])
				{
					rdInclude.Checked = true;
				}
				else if (!(bool)rs.Rows[0]["PPN"])
				{
					rdExclude.Checked = true; 
				}

					if (Convert.ToDecimal(rs.Rows[0]["NilaiPPN"]) == 0)
					{
						dpp.Text = Cf.Num( rs.Rows[0]["NilaiKontrak"] );
					}
					else	
					{
						dpp.Text = Cf.Num(Math.Round( Convert.ToDecimal(nilai.Text) / (decimal)1.1));
					}

				//Price list minimum
				decimal PriceListMin = Convert.ToDecimal(rs.Rows[0]["PriceListMin"]);
				pricemin.Text = Cf.Num(PriceListMin);
                netto.Text = Cf.Num(rs.Rows[0]["NilaiKontrak"]);
                totaltagihan.Text = Cf.Num(rs.Rows[0]["TotalTagihan"]);
				outofbalance.Text = "<a href=\"javascript:popJadwalTagihan('"+NoKontrak+"')\">"
					+ Cf.Num(rs.Rows[0]["OutBalance"])
					+ "</a>"
					;

				skema.Text = rs.Rows[0]["Skema"].ToString();

				if((int)rs.Rows[0]["FlagKomisi"]==0)
					warningkomisi.Visible = false;

			//	ppn.Text = Cf.Num(rs.Rows[0]["NilaiPPN"]);
			}
		}

		private bool datavalid()
		{
			bool x = true;
			
            //if(!Cf.isMoney(ppn))
            //{
            //    x = false;
            //    ppnc.Text = "Angka";
            //    //ppn.Text = "0";
            //}
            //else
            //    ppnc.Text = "";

			if(!Cf.isMoney(bunga))
			{
				x = false;
				errbunga.Text = "Angka";
				//ppn.Text = "0";
			}
            if (Cf.isEmpty(bunga))
            {
                x = false;
                errbunga.Text = "Kosong";
                //ppn.Text = "0";
            }
            else
				errbunga.Text = "";

            if (!Cf.isMoney(gimmick))
            {
                x = false;
                gimmickc.Text = "Angka";
                //ppn.Text = "0";
            }
            if (Cf.isEmpty(gimmick))
            {
                x = false;
                gimmickc.Text = "Kosong";
                //ppn.Text = "0";
            }
            else
                gimmickc.Text = "";

            if (!Cf.isMoney(lainlain))
            {
                x = false;
                lainlainc.Text = "Angka";
                //ppn.Text = "0";
            }
            if (Cf.isEmpty(lainlain))
            {
                x = false;
                lainlainc.Text = "Kosong";
                //ppn.Text = "0";
            }
            else
                lainlainc.Text = "";

			if(!Cf.isMoney(disc))
			{
				x = false;
				discc.Text = "Angka";
				//disc.Text = "0";
			}
            if (Cf.isEmpty(disc))
            {
                x = false;
                discc.Text = "Kosong";
                //ppn.Text = "0";
            }
            else
                discc.Text = "";

            if (!Cf.isMoney(diskontambahan))
            {
                x = false;
                diskontambahanc.Text = "Angka";
                //disc.Text = "0";
            }
            if (Cf.isEmpty(diskontambahan))
            {
                x = false;
                diskontambahanc.Text = "Kosong";
                //ppn.Text = "0";
            }
            else
                diskontambahanc.Text = "";

			if((rdInclude.Checked == false) && (rdExclude.Checked == false))  
			{
				x = false;
				InExc.Text = "Pilih Salah Satu";
			}
			else
				InExc.Text = "";

            if (x)
            {
                decimal Netto;
                Netto = Convert.ToDecimal(gross.Text) + Convert.ToDecimal(gimmick.Text) + Convert.ToDecimal(lainlain.Text) - Convert.ToDecimal(disc.Text) - Convert.ToDecimal(diskontambahan.Text) + Convert.ToDecimal(bunga.Text);

                if (Netto < Convert.ToDecimal(pricemin.Text))
                {
                    x = false;
                    nilaic.Text = "Batas Minimum";
                }
                else
                    nilaic.Text = "";
            }
            if (!x)
				Js.Alert(
					this
					, "Input Tidak Valid.\\n\\n"
					+ "Aturan Proses :\\n"
					+ "1. Nilai Kontrak harus berupa angka dan positif.\\n"
					+ "2. Nilai Kontrak harus lebih besar dari harga minimum.\\n"
					+ "3. Nilai Include dan Exclude PPN harus dipilih.\\n"
					, "document.getElementById('" + nilai.ID + "').focus();"
					+ "document.getElementById('" + nilai.ID + "').select();"
					);

			return x;
		}

		protected void save_Click(object sender, System.EventArgs e)
		{
            if (datavalid())
            {
                //string lvl = "0";
                int Lvl = 0;
                //int Lvl = Db.SingleInteger("SELECT TOP 1 Lvl FROM ISC064_SECURITY..REF_APPROVAL WHERE Tipe = 4 ORDER BY Lvl DESC");
                int MaxApp = 0;
                if (HakApp().Rows.Count > 0)
                    MaxApp = Db.SingleByte("SELECT TOP 1 Lvl FROM ISC064_SECURITY..REF_APPROVAL WHERE Tipe = 4 ORDER BY Lvl DESC");

                if (Lvl < MaxApp)
                {
                    DataTable rsBef = Db.Rs("SELECT "
                  + " DiskonRupiah AS [Diskon dalam Rupiah] "
                  + ", NilaiPPN AS [PPN] "
                  + ", PPN AS [PPN2] "
                  //+ ", BungaNominal AS [BungaNominal] "
                  + ",NilaiKontrak AS [Nilai Kontrak] "
                  + " FROM MS_KONTRAK "
                  + " WHERE NoKontrak = '" + NoKontrak + "' "
                  );

                    decimal Gross = Convert.ToDecimal(gross.Text);
                    decimal Disc = Convert.ToDecimal(disc.Text);
                    decimal DiskonTambahan = Convert.ToDecimal(diskontambahan.Text);
                    decimal bng = Convert.ToDecimal(bunga.Text);
                    decimal HargaGimmick = Convert.ToDecimal(gimmick.Text);
                    decimal HargaLainLain = Convert.ToDecimal(lainlain.Text);
                    //decimal NilaiKontrak = Gross + bng;

                    decimal PPN = 0;
                    //	decimal PPN = Convert.ToDecimal(ppn.Text);
                    decimal NilaiKontrak = Gross - Disc - DiskonTambahan + bng + HargaGimmick + HargaLainLain;

                    string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                    string ParamID = "PLIncludePPN" + Project;
                    bool includeppn = Db.SingleString("SELECT Value FROM " + Mi.DbPrefix + "SECURITY..REF_PARAM WHERE ParamID = '" + ParamID + "'") == "True";

                    decimal NilaiDPP = Math.Round(Convert.ToDecimal(NilaiKontrak));
                    if (includeppn) NilaiDPP = Math.Round(Convert.ToDecimal(Convert.ToDouble(NilaiKontrak) / 1.1));

                    decimal NilaiPPN = 0;
                    if (Convert.ToBoolean(rsBef.Rows[0]["PPN2"]) == true)
                    {
                        NilaiPPN = Math.Round((Decimal)0.1 * NilaiDPP);
                    }

                    string c = Db.SingleString("SELECT Value FROM " + Mi.DbPrefix + "SECURITY..REF_PARAM WHERE ParamID = 'ApprovDiskon" + Project + "'");
                    if (c == "True")
                    {

                        // Update 1
                        Db.Execute(" UPDATE MS_KONTRAK "
                        + " SET TempNilaiPPN = " + NilaiPPN
                        + ", TempGross = " + Gross
                        + ", TempNilaiKontrak = " + NilaiKontrak
                        + ", TempNilaiDPP = " + NilaiDPP
                        + ", TempBungaNominal = " + bng
                        + " WHERE NoKontrak = '" + NoKontrak + "'"
                        );

                        //Db.Execute("EXEC spKontrakDiskon"
                        //    + " '" + NoKontrak + "'"
                        //    + ", " + Gross
                        //    + ", " + NilaiKontrak
                        //    + ",0"
                        //    + ",''"
                        //    + ",''"
                        //    );

                        // Update 3
                        Db.Execute("UPDATE MS_KONTRAK"
                            + " SET "
                            + " TempDiskonRupiah = '" + Disc + "' "
                            + ", TempDiskonTambahan = '" + DiskonTambahan + "' "
                            + ", HargaGimmick = '" + HargaGimmick + "' "
                            + ", HargaLainLain = '" + HargaLainLain + "' "
                            + ", Revisi = Revisi + 1"
                            + ", FlagADJ = 1"
                            + ", FlagGross = 3"
                            + " WHERE NoKontrak = '" + NoKontrak + "' "
                            );

                        //Update manual
                        //Db.Execute(" UPDATE MS_KONTRAK "
                        //    + " SET DiskonRupiah = " + Disc
                        ////	+ ", NilaiPPN = " + PPN
                        //    + ", BungaNominal = '" + bng + "'"
                        //    + ", PPN = '" + statusPPN + "'"
                        //    + " WHERE NoKontrak = '" + NoKontrak + "'");

                        DataTable rsAft = Db.Rs("SELECT "
                                + " DiskonRupiah AS [Diskon dalam Rupiah]"
                                + ", NilaiPPN AS [PPN]"
                                + ", PPN AS [PPN2] "
                                + ", BungaNominal AS [BungaNominal]"
                                + ",NilaiKontrak AS [Nilai Kontrak]"
                                + " FROM MS_KONTRAK"
                                + " WHERE NoKontrak = '" + NoKontrak + "'"
                                );

                        DataTable rs = Db.Rs("SELECT "
                            + " MS_KONTRAK.NoKontrak AS [No. Kontrak]"
                            + ",MS_KONTRAK.NoUnit AS [Unit]"
                            + ",MS_CUSTOMER.Nama AS [Customer]"
                            + ",MS_KONTRAK.Gross AS [Nilai Gross]"
                            + " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER"
                            + " ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
                            + " WHERE MS_KONTRAK.NoKontrak = '" + NoKontrak + "'");

                        string Ket = Cf.LogCapture(rs)
                            + Cf.LogCompare(rsBef, rsAft)
                            ;

                        Db.Execute("EXEC spLogKontrak"
                            + " 'APR-DI'"
                            + ",'" + Act.UserID + "' "
                            + ",'" + Act.IP + "' "
                            + ",'" + Ket + "' "
                            + ",'" + NoKontrak + "' "
                            );

                        decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
                        Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                        Func.CekKomisi(NoKontrak);

                        if (dariDaftar.Checked)
                        {
                            if (Request.QueryString["gross"] != null)
                                Response.Redirect("ReminderGross.aspx?done=" + NoKontrak);
                            else
                                Response.Redirect("KontrakDaftar3.aspx?done=1&NoKontrak=" + NoKontrak);
                        }
                        else
                            Response.Redirect("KontrakDiskon.aspx?done=" + NoKontrak);
                    }
                    else
                    {
                        // DataTable rsBef = Db.Rs("SELECT "
                        //+ " DiskonRupiah AS [Diskon dalam Rupiah] "
                        //+ ", NilaiPPN AS [PPN] "
                        //+ ", PPN AS [PPN2] "
                        ////+ ", BungaNominal AS [BungaNominal] "
                        //+ ",NilaiKontrak AS [Nilai Kontrak] "
                        //+ " FROM MS_KONTRAK "
                        //+ " WHERE NoKontrak = '" + NoKontrak + "' "
                        //);

                        //decimal Gross = Convert.ToDecimal(gross.Text);
                        //decimal Disc = Convert.ToDecimal(disc.Text);
                        //decimal DiskonTambahan = Convert.ToDecimal(diskontambahan.Text);
                        //decimal bng = Convert.ToDecimal(bunga.Text);
                        //decimal HargaGimmick = Convert.ToDecimal(gimmick.Text);
                        //decimal HargaLainLain = Convert.ToDecimal(lainlain.Text);
                        ////	decimal PPN = Convert.ToDecimal(ppn.Text);
                        //decimal NilaiKontrak = Gross - Disc - DiskonTambahan + bng + HargaGimmick + HargaLainLain;

                        //string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                        //string ParamID = "PLIncludePPN" + Project;
                        //bool includeppn = Db.SingleString("SELECT Value FROM " + Mi.DbPrefix + "SECURITY..REF_PARAM WHERE ParamID = '" + ParamID + "'") == "True";

                        //decimal NilaiDPP = Math.Round(Convert.ToDecimal(NilaiKontrak));
                        //if (includeppn) NilaiDPP = Math.Round(Convert.ToDecimal(Convert.ToDouble(NilaiKontrak) / 1.1));

                        //decimal NilaiPPN = 0;
                        //if (Convert.ToBoolean(rsBef.Rows[0]["PPN2"]) == true)
                        //{
                        //    NilaiPPN = Math.Round((Decimal)0.1 * NilaiDPP);
                        //}


                        //string statusPPN = "";

                        // Update 1
                        Db.Execute(" UPDATE MS_KONTRAK "
                            + " SET NilaiPPN = " + NilaiPPN
                            + ", NilaiKontrak = " + NilaiKontrak
                            + ", NilaiDPP = " + NilaiDPP
                            + ", BungaNominal = " + bng
                            + ", FlagADJ = 0"
                            + " WHERE NoKontrak = '" + NoKontrak + "'"
                            );

                        Db.Execute("EXEC spKontrakDiskon"
                            + " '" + NoKontrak + "'"
                            + ", " + Gross
                            + ", " + NilaiKontrak
                            + ",0"
                            + ",''"
                            + ",''"
                            );

                        // Update 3
                        Db.Execute("UPDATE MS_KONTRAK"
                            + " SET "
                            + " DiskonRupiah = '" + Disc + "' "
                            + ", DiskonTambahan = '" + DiskonTambahan + "' "
                            + ", HargaGimmick = '" + HargaGimmick + "' "
                            + ", HargaLainLain = '" + HargaLainLain + "' "
                            + ", Revisi = Revisi + 1"
                            + " WHERE NoKontrak = '" + NoKontrak + "' "
                            );

                        //Update manual
                        //Db.Execute(" UPDATE MS_KONTRAK "
                        //    + " SET DiskonRupiah = " + Disc
                        ////	+ ", NilaiPPN = " + PPN
                        //    + ", BungaNominal = '" + bng + "'"
                        //    + ", PPN = '" + statusPPN + "'"
                        //    + " WHERE NoKontrak = '" + NoKontrak + "'");

                        DataTable rsAft = Db.Rs("SELECT "
                                + " DiskonRupiah AS [Diskon dalam Rupiah]"
                                + ", NilaiPPN AS [PPN]"
                                + ", PPN AS [PPN2] "
                                + ", BungaNominal AS [BungaNominal]"
                                + ",NilaiKontrak AS [Nilai Kontrak]"
                                + " FROM MS_KONTRAK"
                                + " WHERE NoKontrak = '" + NoKontrak + "'"
                                );

                        DataTable rs = Db.Rs("SELECT "
                            + " MS_KONTRAK.NoKontrak AS [No. Kontrak]"
                            + ",MS_KONTRAK.NoUnit AS [Unit]"
                            + ",MS_CUSTOMER.Nama AS [Customer]"
                            + ",MS_KONTRAK.Gross AS [Nilai Gross]"
                            + " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER"
                            + " ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
                            + " WHERE MS_KONTRAK.NoKontrak = '" + NoKontrak + "'");

                        string Ket = Cf.LogCapture(rs)
                            + Cf.LogCompare(rsBef, rsAft)
                            ;

                        Db.Execute("EXEC spLogKontrak"
                            + " 'DISKON'"
                            + ",'" + Act.UserID + "' "
                            + ",'" + Act.IP + "' "
                            + ",'" + Ket + "' "
                            + ",'" + NoKontrak + "' "
                            );

                        decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
                        Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                        Func.CekKomisi(NoKontrak);

                        //if (dariDaftar.Checked)
                        //{
                        //    if (Request.QueryString["gross"] != null)
                        //        Response.Redirect("ReminderGross.aspx?done=" + NoKontrak);
                        //    else
                        //        Response.Redirect("KontrakDaftar3.aspx?done=1&NoKontrak=" + NoKontrak);
                        //}
                        //else
                            Response.Redirect("KontrakDiskon.aspx?done=" + NoKontrak);
                    }
                }
            }
		}

		private string NoKontrak
		{
			get
			{
				return Cf.Pk(nokontrak.Text);
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

        private static DataTable HakApp()
        {
            DataTable hakapp = Db.Rs("SELECT * FROM ISC064_SECURITY..REF_APPROVAL WHERE Tipe = 4");

            return hakapp;
        }
    }
}

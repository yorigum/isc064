using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.FINANCEAR
{
	public partial class TTSRegistrasiPenghuni : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Table rpt;
		protected DataTable rsTagihan;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			if(!Page.IsPostBack)
			{
				nobg.Attributes["ondblclick"] = "popDaftarBG();";
				InitForm();
				
				Js.Focus(this, ket);

				detildiv.Visible = false;
				nilai.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
				nilai.Attributes["onkeyup"] = "CalcType(this,tempnum);";
				nilai.Attributes["onblur"] = "CalcBlur(this);";

				fillanonim();
				fillAcc();
			}

			if(detildiv.Visible)
				Js.Confirm(this, "Lanjutkan proses registrasi tanda terima sementara?");
			
			FillTb();
		}

		private void fillanonim() 
		{
			DataTable rs = Db.Rs("SELECT * FROM MS_ANONIM WHERE Status = 'ID'");
			for(int i=0;i<rs.Rows.Count;i++) 
			{
				string v = rs.Rows[i]["NoAnonim"].ToString();
				string t = Cf.Day(rs.Rows[i]["Tgl"])
					+ " (" + rs.Rows[i]["Bank"] + ") "
					+ Cf.Num(rs.Rows[i]["Nilai"]) 
					+ " Keterangan : "
					+ rs.Rows[i]["Unit"] + " "
					+ rs.Rows[i]["Customer"] + " "
					+ rs.Rows[i]["Ket"];
				anonim.Items.Add(new ListItem(t,v));
			}
		}

		private void fillAcc() 
		{
			DataTable rs = Db.Rs("SELECT * FROM REF_ACC");
			for(int i=0;i<rs.Rows.Count;i++) 
			{
				string v = rs.Rows[i]["Acc"].ToString();
				string t = v + " : " + rs.Rows[i]["Bank"] + " " + rs.Rows[i]["Rekening"];
				ddlAcc.Items.Add(new ListItem(t,v));
			}
		}

		private void InitForm()
		{
			if(Act.Sec("DiskonTTS"))
			{
				carabayar.RepeatColumns = 3;
				carabayar.Items.Add(new ListItem("DN = Diskon","DN"));
			}

			gt.Attributes["style"] = "border:0px;font:bold;";
			sisadeposit.Attributes["style"] = "border:0px;font:bold;";
			tgl.Text = Cf.Day(DateTime.Today);

			tipe.Text = Tipe;
			referensi.Text = Ref;
			
			unit.Text = Db.SingleString("SELECT NoUnit "
				+ " FROM "+Tb+"..MS_PENGHUNI "
				+ " WHERE NoPenghuni = '"+Ref+"'");
			
			customer.Text = Db.SingleString("SELECT Nama "
				+ " FROM "+Tb+"..MS_PENGHUNI "
				+ " WHERE NoPenghuni = '"+Ref+"'");

			sisadeposit.Text = Cf.Num(
				Db.SingleDecimal("SELECT SisaDeposit "
				+ " FROM "+Tb+"..MS_PENGHUNI "
				+ " WHERE NoPenghuni = '"+Ref+"'")
				);
		}

		private void FillTb()
		{
			string strSql = "SELECT * "
				+ ",NilaiTagihan AS SisaTagihan"
				+ " FROM "+Tb+"..MS_TAGIHAN AS MS_TAGIHAN WHERE NoPenghuni = '" + Ref + "'"
				+ " AND CaraBayar = ''"
				+ " ORDER BY TglJT, NoUrut";
			rsTagihan = Db.Rs(strSql);

			for(int i=0;i<rsTagihan.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				Label l;
				TextBox t;

				l = new Label();
				l.Text = "<tr valign=top>"
					+ "<td>" + rsTagihan.Rows[i]["Tipe"] + "." + rsTagihan.Rows[i]["NoUrut"] + "</td>"
					+ "<td>" + rsTagihan.Rows[i]["NamaTagihan"] + "</td>"
					+ "<td style='white-space:nowrap'>" + Cf.Day(rsTagihan.Rows[i]["TglJT"]) + "</td>"
					+ "<td align=right>" + Cf.Num(rsTagihan.Rows[i]["SisaTagihan"]) + "</td>"
					+ "<td>"
					;
				list.Controls.Add(l);

				t = new TextBox();
				t.ID = "lunas_"+i;
				t.Width = 100;
				t.CssClass = "txt_num";
				t.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
				t.Attributes["onkeyup"] = "CalcType(this,tempnum);";
				t.Attributes["onblur"] = "CalcBlur(this);hitunggt();";
				//t.ReadOnly = true;
				list.Controls.Add(t);

				l = new Label();
				l.Text = "</td>"
					+ "<td><input type='checkbox' onclick=\"tagihan('"+i+"','"+Cf.Num(rsTagihan.Rows[i]["SisaTagihan"])+"',this)\"></td>"
					+ "</tr>";
				list.Controls.Add(l);
			}
		}

		private bool datavalid()
		{
			if(carabayar.SelectedIndex==-1)
			{
				Js.Alert(
					this
					, "Cara Bayar Tidak Valid.\\n"
					+ "Silakan pilih salah satu cara bayar yang tersedia."
					, ""
					);

				return false;
			}
			else
			{
				string s = "";
				bool x = true;

				if(!Cf.isTgl(tgl))
				{
					x = false;
					if(s=="") s = tgl.ID;
					tglc.Text = "Tanggal";
				}
				else
					tglc.Text = "";

				if(ddlAcc.SelectedIndex == 0)
				{
					x = false;

					if(s == "")
						s = ddlAcc.ID;

					ddlAccErr.Text = "Harus dipilih";
				}
				else
					ddlAccErr.Text = "";

				if(carabayar.SelectedValue=="BG")
				{
					nobg.Text = Cf.Pk(nobg.Text);
					if(Cf.isEmpty(nobg))
					{
						x = false;
						if(s=="") s = nobg.ID;
						nobgc.Text = "Kosong";
					}
					else
						nobgc.Text = "";

					if(!Cf.isTgl(tglbg))
					{
						x = false;
						if(s=="") s = tglbg.ID;
						tglbgc.Text = "Tanggal";
					}
					else
						tglbgc.Text = "";
				}

				bool adasatu = false;
				for(int i=0;i<rsTagihan.Rows.Count;i++)
				{
					TextBox lunas = (TextBox) list.FindControl("lunas_" + i);
					if(lunas.Text!="")
					{
						adasatu = true;
						try
						{
							decimal z = Convert.ToDecimal(lunas.Text);
							if(z<Convert.ToDecimal(rsTagihan.Rows[i]["SisaTagihan"]))
							{
								x = false;
								if(s=="") s = lunas.ID;
							}
						}
						catch
						{
							x = false;
							if(s=="") s = lunas.ID;
						}
					}
				}

				if(!adasatu)
				{
					x = false;
					if(s=="") s = gt.ID;
					gtc.Attributes["style"] = "color:red";
				}
				else
					gtc.Attributes["style"] = "color:black";

				if(x
					&& carabayar.SelectedValue=="UJ"
					&& Convert.ToDecimal(gt.Text) > Convert.ToDecimal(sisadeposit.Text))
				{
					x = false;
					if(s=="") s = sisadeposit.ID;
					sisadepositc.Text = "Tidak Cukup";
				}
				else
					sisadepositc.Text = "";

				if(!x)
					Js.Alert(
						this
						, "Input Tidak Valid.\\n\\n"
						+ "Aturan Proses :\\n"
						+ "1. Format Tanggal : Bulan / Tanggal / Tahun.\\n"
						+ "2. Pembayaran harus berupa angka dan minimal untuk satu tagihan.\\n"
						+ "3. Khusus Cek Giro : No. BG tidak boleh kosong.\\n"
						+ "4. Khusus Uang Jaminan : Sisa Deposit tidak mencukupi.\\n"
						+ "5. Nilai pembayaran tidak boleh kurang dari nilai tagihan.\\n"
						+ "6. Rekening Bank harus dipilih.\\n"
						, "document.getElementById('"+s+"').focus();"
						+ "document.getElementById('"+s+"').select();"
						);

				return x;
			}
		}

		protected void save_Click(object sender, System.EventArgs e)
		{
			if(datavalid())
			{
				DateTime TglTTS = Convert.ToDateTime(tgl.Text);
				string Unit = Cf.Str(unit.Text);
				string Customer = Cf.Str(customer.Text);
				string CaraBayar = carabayar.SelectedValue;
				string Ket = Cf.Str(ket.Text);

				Db.Execute("EXEC spTTSRegistrasi"
					+ " '" + TglTTS + "'"
					+ ",'" + Act.UserID + "'"
					+ ",'" + Act.IP + "'"
					+ ",'" + Tipe + "'"
					+ ",'" + Ref + "'"
					+ ",'" + Unit + "'"
					+ ",'" + Customer + "'"
					+ ",'" + CaraBayar + "'"
					+ ",'" + Ket + "'"
					);

				int NoTTS = Db.SingleInteger("SELECT TOP 1 NoTTS FROM MS_TTS ORDER BY NoTTS DESC");

				Db.Execute("UPDATE MS_TTS SET Acc = '" + ddlAcc.SelectedValue + "' WHERE NoTTS = " + NoTTS);
				
				//khusus cek giro
				if(carabayar.SelectedValue=="BG")
				{
					string NoBG = Cf.Pk(nobg.Text);
					DateTime TglBG = Convert.ToDateTime(tglbg.Text);

					Db.Execute("EXEC spTTSRegistrasiBG"
						+ " '" + NoTTS + "'"
						+ ",'" + NoBG + "'"
						+ ",'" + TglBG + "'"
						);
				}

				if(anonim.SelectedIndex>0) 
				{
					Db.Execute("UPDATE MS_ANONIM SET Status = 'S' WHERE NoAnonim = "
						+anonim.SelectedValue);
				}

				System.Text.StringBuilder alokasi = new System.Text.StringBuilder();

				for(int i=0;i<rsTagihan.Rows.Count;i++)
				{
					TextBox lunas = (TextBox) list.FindControl("lunas_" + i);
					if(lunas.Text!="")
					{
						string NoTagihan = rsTagihan.Rows[i]["Tipe"] + "." + rsTagihan.Rows[i]["NoUrut"];
						string NamaTagihan = Cf.Str(rsTagihan.Rows[i]["NamaTagihan"])
							+ " ("+rsTagihan.Rows[i]["Tipe"]+")";
						decimal Nilai = Convert.ToDecimal(lunas.Text);

						Db.Execute("EXEC spTTSAlokasi "
							+ "  " + NoTTS
							+ ",'" + NoTagihan + "'"
							+ ", " + Nilai
							);

						alokasi.Append(NamaTagihan + "    " + Cf.Num(Nilai) + "<br>");
					}
				}

				DataTable rs = Db.Rs("SELECT "
					+ " CONVERT(varchar, TglTTS, 106) AS [Tanggal]"
					+ ",Tipe"
					+ ",Ref AS [Ref.]"
					+ ",Unit"
					+ ",Customer"
					+ ",CaraBayar AS [Cara Bayar]"
					+ ",Ket AS [Keterangan]"
					+ ",Total"
					+ ",NoBG AS [No. BG]"
					+ ",CONVERT(varchar, TglBG, 106) AS [Tanggal BG]"
					+ ", Acc AS [Rekening Bank]"
					+ " FROM MS_TTS WHERE NoTTS = " + NoTTS);

				string KetLog = Cf.LogCapture(rs)
					+ "<br>***ALOKASI PEMBAYARAN:<br>"
					+ alokasi.ToString();

				Db.Execute("EXEC spLogTTS"
					+ " 'REGIS'"
					+ ",'" + Act.UserID + "'"
					+ ",'" + Act.IP + "'"
					+ ",'" + KetLog + "'"
					+ ",'" + NoTTS.ToString().PadLeft(7,'0') + "'"
					);

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_TTS_LOG ORDER BY LogID DESC");
                string Project = Db.SingleString("SELECT Project FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = (SELECT Ref FROM MS_TTS WHERE NoTTS = '" + NoTTS + "')");
                Db.Execute("UPDATE MS_TTS_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                Response.Redirect("TTSRegistrasi.aspx?done="+NoTTS);
			}
		}

		protected void next_Click(object sender, System.EventArgs e)
		{
			if(Cf.isMoney(nilai))
			{
				detildiv.Visible = true;
				nilaitr.Visible = false;

				Alokasi(Convert.ToDecimal(nilai.Text));

				Js.Confirm(this, "Lanjutkan proses registrasi tanda terima sementara?");
			}
			else
				Js.Alert(
					this
					, "Input Tidak Valid.\\n\\n"
					+ "Aturan Proses :\\n"
					+ "1. Nilai harus berupa angka.\\n"
					, "document.getElementById('"+nilai.ID+"').focus();"
					+ "document.getElementById('"+nilai.ID+"').select();"
					);
		}
		private void Alokasi(decimal total)
		{
			decimal x = total;

			for(int i=0;i<rsTagihan.Rows.Count;i++)
			{
				TextBox lunas = (TextBox) list.FindControl("lunas_" + i);
				decimal SisaTagihan = (decimal)rsTagihan.Rows[i]["SisaTagihan"];

				if(i==rsTagihan.Rows.Count-1)
				{
					//last row
					lunas.Text = Cf.Num(x);
				}
				else
				{
					if(SisaTagihan>=x)
					{
						//break, soalnya total udah abis
						lunas.Text = Cf.Num(x);
						break;
					}
					else
					{
						lunas.Text = Cf.Num(SisaTagihan);
					}
				}
				x = x - SisaTagihan;
			}

			gt.Text = Cf.Num(total);
		}

		private string Tb
		{
			get
			{
				return Sc.MktTb(Tipe);
			}
		}

		private string Ref
		{
			get
			{
				return Cf.Pk(Request.QueryString["Ref"]);
			}
		}

		private string Tipe
		{
			get
			{
				return Cf.Pk(Request.QueryString["Tipe"]);
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

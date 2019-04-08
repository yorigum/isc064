using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.FINANCEAR
{
	public partial class TTSRegistrasiMulti : System.Web.UI.Page
	{
		private DataTable rsTagihan;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			if(!IsPostBack)
			{
				InitHeader();
			}

			InitForm();
			Js.Confirm(this, "Lanjutkan proses registrasi tanda terima sementara?");
		}

		private void InitHeader()
		{
			lblTipe.Text = Tipe;
			lblReferensi.Text = Ref;
			
			lblUnit.Text = Db.SingleString("SELECT NoUnit "
				+ " FROM " + Tb + "..MS_KONTRAK "
				+ " WHERE NoKontrak = '" + Ref + "'");
			
			lblCustomer.Text = Db.SingleString("SELECT Nama "
				+ " FROM "+Tb+"..MS_KONTRAK AS MS_KONTRAK "
				+ " INNER JOIN " + Tb + "..MS_CUSTOMER AS MS_CUSTOMER "
				+ " ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
				+ " WHERE NoKontrak = '" + Ref + "'");
		}

		private void InitForm()
		{
			string strSql = "SELECT * "
				+ ",(NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Tb + "..MS_PELUNASAN WHERE NoTagihan = MS_TAGIHAN.NoUrut AND NoKontrak = '" + Ref + "') ) AS SisaTagihan"
				+ " FROM " + Tb + "..MS_TAGIHAN AS MS_TAGIHAN WHERE NoKontrak = '" + Ref + "'"
				+ " AND (NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Tb + "..MS_PELUNASAN WHERE NoTagihan = MS_TAGIHAN.NoUrut AND NoKontrak = '" + Ref + "') ) > 0"
				+ " ORDER BY TglJT, NoUrut";
			rsTagihan = Db.Rs(strSql);

			for(int i = 0; i < rsTagihan.Rows.Count; i++)
			{
				if(!Response.IsClientConnected)
					break;

				Label l;
				DropDownList ddl;
				TextBox tb;
				HtmlInputButton btn;
				RadioButtonList rbl;

				l = new Label();
				l.Text = "<tr>";
				list.Controls.Add(l);

				/*No. Tagihan*/
				l = new Label();
				l.Text = "<td>" + rsTagihan.Rows[i]["NoKontrak"].ToString() + "." + rsTagihan.Rows[i]["NoUrut"].ToString() + "</td>";
				list.Controls.Add(l);
				/*************/

				/*Nama Tagihan*/
				l = new Label();
				l.Text = "<td>" + rsTagihan.Rows[i]["NamaTagihan"].ToString()+ "</td>";
				list.Controls.Add(l);
				/**************/

				/*Sisa Tagihan*/
				l = new Label();
				l.Text = "<td align='right'>" + Cf.Num(rsTagihan.Rows[i]["SisaTagihan"]) + "</td>";
				list.Controls.Add(l);
				/**************/

				/*Cara Bayar*/
				l = new Label();
				l.Text = "<td>";
				list.Controls.Add(l);

				ddl = new DropDownList();
				ddl.ID = "carabayar_" + i;
				ddl.CssClass = "ddl";
				ddl.Width = 150;
				ddl.Items.Add(new ListItem("- Pilih Cara Bayar -"));
				ddl.Items.Add(new ListItem("TN = TUNAI", "TN"));
				ddl.Items.Add(new ListItem("KK = KARTU KREDIT", "KK"));
				ddl.Items.Add(new ListItem("KD = KARTU DEBIT", "KD"));
				ddl.Items.Add(new ListItem("TR = TRANSFER BANK", "TR"));
				ddl.Items.Add(new ListItem("BG = CEK GIRO", "BG"));
				ddl.Items.Add(new ListItem("DN = DISKON", "DN"));
				list.Controls.Add(ddl);

				l = new Label();
				l.Text = "</td>";
				list.Controls.Add(l);
				/************/

				/*Tgl*/
				l = new Label();
				l.Text = "<td>";
				list.Controls.Add(l);

				tb = new TextBox();
				tb.ID = "tgl_" + i;
				tb.CssClass = "txt_center";
				tb.Width = 85;
				tb.Text = Cf.Day(DateTime.Now);
				list.Controls.Add(tb);

				btn = new HtmlInputButton();
				btn.Value = "...";
				btn.Attributes["class"] = "btn";
//				btn.Attributes["onclick"] = "openCalendar('tgl_" + i + "');";
				list.Controls.Add(btn);

				l = new Label();
				l.Text = "</td>";
				list.Controls.Add(l);
				/*****/

				/*Keterangan*/
				l = new Label();
				l.Text = "<td>";
				list.Controls.Add(l);

				tb = new TextBox();
				tb.ID = "ket_" + i;
				tb.CssClass = "txt";
				tb.Width = 175;
				tb.MaxLength = 200;
				list.Controls.Add(tb);

				l = new Label();
				l.Text = "</td>";
				list.Controls.Add(l);
				/************/

				/*Rekening Bank*/
				l = new Label();
				l.Text = "<td>";
				list.Controls.Add(l);

				ddl = new DropDownList();
				ddl.ID = "rekening_" + i;
				ddl.CssClass = "ddl";
				ddl.Width = 200;
				ddl.Items.Add(new ListItem("- Pilih Rekening Bank -"));
				fillacc(ddl);
				list.Controls.Add(ddl);

				l = new Label();
				l.Text = "</td>";
				list.Controls.Add(l);
				/***************/

				/*No. BG*/
				l = new Label();
				l.Text = "<td>";
				list.Controls.Add(l);

				tb = new TextBox();
				tb.ID = "nobg_" + i;
				tb.CssClass = "txt";
				tb.Width = 80;
				tb.MaxLength = 20;
				list.Controls.Add(tb);

				l = new Label();
				l.Text = "</td>";
				list.Controls.Add(l);
				/********/

				/*Tgl BG*/
				l = new Label();
				l.Text = "<td>";
				list.Controls.Add(l);

				tb = new TextBox();
				tb.ID = "tglbg_" + i;
				tb.CssClass = "txt_center";
				tb.Width = 85;
				list.Controls.Add(tb);

				btn = new HtmlInputButton();
				btn.ID = "btn_" + i;
				btn.Value = "...";
				btn.Attributes["class"] = "btn";
//				btn.Attributes["onclick"] = "openCalendar('tglbg_" + i + "');";
				list.Controls.Add(btn);

				l = new Label();
				l.Text = "</td>";
				list.Controls.Add(l);
				/********/

				/*Nilai*/
				l = new Label();
				l.Text = "<td>";
				list.Controls.Add(l);

				tb = new TextBox();
				tb.ID = "nilai_" + i;
				tb.CssClass = "txt_num";
				tb.Width = 120;
				tb.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
				tb.Attributes["onkeyup"] = "CalcType(this,tempnum);";
				tb.Attributes["onblur"] = "CalcBlur(this);";
				list.Controls.Add(tb);

				l = new Label();
				l.Text = "</td>";
				list.Controls.Add(l);
				/*******/

				/*Sumber Bayar*/
				l = new Label();
				l.Text = "<td>";
				list.Controls.Add(l);

				rbl = new RadioButtonList();
				rbl.ID = "sumberbayar_" + i;
				rbl.Items.Add(new ListItem("Dari Customer", "0"));
				rbl.Items.Add(new ListItem("Dari Bank", "1"));
				rbl.Items[0].Selected = true;
				list.Controls.Add(rbl);

				l = new Label();
				l.Text = "</td>";
				list.Controls.Add(l);

				l = new Label();
				l.Text = "</tr>";
				list.Controls.Add(l);
			}
		}

		private void fillacc(DropDownList ddlAcc) 
		{
			DataTable rs = Db.Rs("SELECT * FROM REF_ACC");
			for(int i=0;i<rs.Rows.Count;i++) 
			{
				string v = rs.Rows[i]["Acc"].ToString();
				string t = v + " : " + rs.Rows[i]["Bank"] + " " + rs.Rows[i]["Rekening"];
				ddlAcc.Items.Add(new ListItem(t,v));
			}
		}

		private bool Valid()
		{
			bool x = true;
			string s = "", strFocus = "";
			bool boolAdaSatu = false;

			for(int i = 0; i < rsTagihan.Rows.Count; i++)
			{
				DropDownList carabayar = (DropDownList)list.FindControl("carabayar_" + i);
				DropDownList rekening = (DropDownList)list.FindControl("rekening_" + i);
				TextBox nilai = (TextBox)list.FindControl("nilai_" + i);
				TextBox tgl = (TextBox)list.FindControl("tgl_" + i);

				if(carabayar.SelectedIndex != 0)
				{
					boolAdaSatu = true;

					if(!Cf.isTgl(tgl))
					{
						x = false;

						if(s == "")
							s = tgl.ID;

						break;
					}

					if(rekening.SelectedIndex == 0)
					{
						x = false;

						if(s == "")
							s = rekening.ID;

						break;
					}
					
					if(Cf.isEmpty(nilai))
					{
						x = false;

						if(s == "")
							s = nilai.ID;

						break;
					}

					if(carabayar.SelectedValue == "BG")
					{
						TextBox nobg = (TextBox)list.FindControl("nobg_" + i);
						TextBox tglbg = (TextBox)list.FindControl("tglbg_" + i);

						if(Cf.isEmpty(nobg))
						{
							x = false;

							if(s == "")
								s = nobg.ID;

							break;
						}

						if(!Cf.isTgl(tglbg))
						{
							x = false;

							if(s == "")
								s = tglbg.ID;

							break;
						}
					}
				}
			}

			if(!boolAdaSatu)
				x = false;

			if(s != "")
				strFocus = "document.getElementById('" + s + "').focus();";

			if(!x)
			{
				Js.Alert(
					this
					, "Input Tidak Valid.\\n\\n"
					+ "Aturan Proses :\\n"
					+ "1. Harap cek kembali kelengkapan data yang diinput.\\n"
					+ "2. Khusus untuk cara bayar BG (Cek Giro), No. BG dan Tgl BG harus diisi.\\n"
					+ "3. Registrasi TTS harus minimal untuk satu tagihan."
					, strFocus
					);
			}

			return x;
		}

		private void SaveTTS()
		{
			int intFrom = 0, intTo = 0;

			for(int i = 0; i < rsTagihan.Rows.Count; i++)
			{
				if(!Response.IsClientConnected)
					break;

				DropDownList carabayar = (DropDownList)list.FindControl("carabayar_" + i);

				if(carabayar.SelectedIndex != 0)
				{
					TextBox ket = (TextBox)list.FindControl("ket_" + i);
					TextBox nilai = (TextBox)list.FindControl("nilai_" + i);
					TextBox tgl = (TextBox)list.FindControl("tgl_" + i);
					DropDownList rekening = (DropDownList)list.FindControl("rekening_" + i);
					RadioButtonList sumberbayar = (RadioButtonList)list.FindControl("sumberbayar_" + i);

					DateTime TglTTS = Convert.ToDateTime(tgl.Text);
					string CaraBayar = Cf.Str(carabayar.SelectedValue);
					string Ket = Cf.Str(ket.Text);
					decimal Nilai = Convert.ToDecimal(nilai.Text);
					string Unit = Cf.Str(lblUnit.Text);
					string Customer = Cf.Str(lblCustomer.Text);
					int NoTagihan = Convert.ToInt32(rsTagihan.Rows[i]["NoUrut"]);
					string NamaTagihan = Cf.Str(rsTagihan.Rows[i]["NamaTagihan"])
						+ " (" + rsTagihan.Rows[i]["Tipe"] + ")"
						;
					string Rekening = rekening.SelectedValue;

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

					if(intFrom == 0)
						intFrom = NoTTS;

					if(carabayar.SelectedValue == "BG")
					{
						TextBox nobg = (TextBox)list.FindControl("nobg_" + i);
						TextBox tglbg = (TextBox)list.FindControl("tglbg_" + i);

						string NoBG = Cf.Pk(nobg.Text);
						DateTime TglBG = Convert.ToDateTime(tglbg.Text);

						Db.Execute("EXEC spTTSRegistrasiBG"
							+ " '" + NoTTS + "'"
							+ ",'" + NoBG + "'"
							+ ",'" + TglBG + "'"
							);
					}

					Db.Execute("EXEC spTTSAlokasi "
						+ "  " + NoTTS
						+ ", " + NoTagihan
						+ ", " + Nilai
						);

					Db.Execute("UPDATE MS_TTS"
						+ " SET Acc = '" + Rekening + "'"
						+ ", SumberBayar = " + sumberbayar.SelectedValue
						+ " WHERE NoTTS = " + NoTTS
						);

					string strAlokasi = NamaTagihan + "    " + Cf.Num(Nilai);

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
						+ strAlokasi;

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

                }
            }

			intTo = Db.SingleInteger("SELECT TOP 1 NoTTS FROM MS_TTS ORDER BY NoTTS DESC");

			Response.Redirect("TTSRegistrasi.aspx?done=1&from=" + intFrom + "&to=" + intTo);
		}

		protected void save_Click(object sender, System.EventArgs e)
		{	
			if(Valid())
			{
				SaveTTS();
			}
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

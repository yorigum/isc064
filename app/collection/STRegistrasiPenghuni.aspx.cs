using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.COLLECTION
{
	public partial class STRegistrasiPenghuni : System.Web.UI.Page
	{
		protected DataTable rsTagihan;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			if(!Page.IsPostBack)
			{
				InitForm();
				Js.Focus(this, tgl);
			}

			Js.Confirm(this, "Lanjutkan proses registrasi surat tunggakan?");
			FillTb();
		}

		private void InitForm()
		{
			tgl.Text = Cf.Day(DateTime.Today);
			gt.Attributes["style"] = "border:0px;font:bold;";

			tipe.Text = Tipe;
			referensi.Text = Ref;
			
			DataTable rs = Db.Rs("SELECT NoUnit, Nama, NoTelp, Alamat1, Alamat2, Alamat3 "
				+ " FROM "+Tb+"..MS_PENGHUNI "
				+ " WHERE NoPenghuni = '"+Ref+"'");
			if(rs.Rows.Count!=0)
			{
				unit.Text = rs.Rows[0]["NoUnit"].ToString();
				customer.Text = rs.Rows[0]["Nama"].ToString();
				notelp.Text = rs.Rows[0]["NoTelp"].ToString();
				alamat1.Text = rs.Rows[0]["Alamat1"].ToString();
				alamat2.Text = rs.Rows[0]["Alamat2"].ToString();
				alamat3.Text = rs.Rows[0]["Alamat3"].ToString();
			}
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
					+ "<td>";
				list.Controls.Add(l);

				t = new TextBox();
				t.ID = "lunas_"+i;
				t.Attributes["style"] = "display:none";
				list.Controls.Add(t);

				l = new Label();
				l.Text = "<input type='checkbox' onclick=\"tagihan('"+i+"','"+Cf.Num(rsTagihan.Rows[i]["SisaTagihan"])+"',this)\"></td>"
					+ "</tr>";
				list.Controls.Add(l);
			}
		}

		private bool datavalid()
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

			bool adasatu = false;
			for(int i=0;i<rsTagihan.Rows.Count;i++)
			{
				TextBox lunas = (TextBox) list.FindControl("lunas_" + i);
				if(lunas.Text!="")
				{
					adasatu = true;
					break;
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

			if(!x)
				Js.Alert(
					this
					, "Input Tidak Valid.\\n\\n"
					+ "Aturan Proses :\\n"
					+ "1. Format Tanggal : Bulan / Tanggal / Tahun.\\n"
					+ "2. Surat minimal untuk satu tagihan.\\n"
					, "document.getElementById('"+s+"').focus();"
					+ "document.getElementById('"+s+"').select();"
					);

			return x;
		}

		protected void save_Click(object sender, System.EventArgs e)
		{
			if(datavalid())
			{
				DateTime TglTunggakan = Convert.ToDateTime(tgl.Text);
				string Unit = Cf.Str(unit.Text);
				string Customer = Cf.Str(customer.Text);
				string NoTelp = Cf.Str(notelp.Text);
				string Alamat1 = Cf.Str(alamat1.Text);
				string Alamat2 = Cf.Str(alamat2.Text);
				string Alamat3 = Cf.Str(alamat3.Text);

				Db.Execute("EXEC spTunggakanRegistrasi"
					+ " '" + TglTunggakan + "'"
					+ ",'" + Tipe + "'"
					+ ",'" + Ref + "'"
					+ ",'" + Unit + "'"
					+ ",'" + Customer + "'"
					+ ",'" + NoTelp + "'"
					+ ",'" + Alamat1 + "'"
					+ ",'" + Alamat2 + "'"
					+ ",'" + Alamat3 + "'"
					);

				int NoTunggakan = Db.SingleInteger("SELECT TOP 1 NoTunggakan FROM MS_TUNGGAKAN ORDER BY NoTunggakan DESC");

				System.Text.StringBuilder alokasi = new System.Text.StringBuilder();

				for(int i=0;i<rsTagihan.Rows.Count;i++)
				{
					TextBox lunas = (TextBox) list.FindControl("lunas_" + i);
					if(lunas.Text!="")
					{
						int NoTagihan = (int)rsTagihan.Rows[i]["NoUrut"];
						string NamaTagihan = Cf.Str(rsTagihan.Rows[i]["NamaTagihan"])
							+ " ("+rsTagihan.Rows[i]["Tipe"]+")";
						decimal Nilai = Convert.ToDecimal(rsTagihan.Rows[i]["SisaTagihan"]);
						DateTime TglJT = Convert.ToDateTime(rsTagihan.Rows[i]["TglJT"]);

						Db.Execute("EXEC spTunggakanDetil "
							+ "  " + NoTunggakan
							+ ",'" + NoTagihan + "'"
							+ ",'" + NamaTagihan + "'"
							+ ", " + Nilai
							+ ",'" + TglJT + "'"
							);

						alokasi.Append(NamaTagihan + "    " + Cf.Num(Nilai) + "<br>");
					}
				}

				DataTable rs = Db.Rs("SELECT "
					+ " CONVERT(varchar, TglTunggakan, 106) AS [Tanggal]"
					+ ",Tipe"
					+ ",Ref AS [Ref.]"
					+ ",Unit"
					+ ",Customer"
					+ " FROM MS_TUNGGAKAN WHERE NoTunggakan = " + NoTunggakan);

				string KetLog = Cf.LogCapture(rs)
					+ "<br>***DETIL TAGIHAN:<br>"
					+ alokasi.ToString();

				Db.Execute("EXEC spLogTunggakan"
					+ " 'REGIS'"
					+ ",'" + Act.UserID + "'"
					+ ",'" + Act.IP + "'"
					+ ",'" + KetLog + "'"
					+ ",'" + NoTunggakan.ToString().PadLeft(7,'0') + "'"
					);

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_TUNGGAKAN_LOG ORDER BY LogID DESC");
                string Project = Db.SingleString("SELECT Project FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = (SELECT Ref FROM MS_TUNGGAKAN WHERE NoTunggakan = '" + NoTunggakan + "') ");
                Db.Execute("UPDATE MS_TUNGGAKAN_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                Response.Redirect("STRegistrasi.aspx?done="+NoTunggakan);
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

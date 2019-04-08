using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
	public partial class Alokasi : System.Web.UI.Page
	{
		protected DataTable rs;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			FillTable();
			Js.Confirm(this, "Lanjutkan proses edit alokasi pelunasan?");
		}

		private void BindTagihan(DropDownList ddl)
		{
			DataTable rsTagihan = Db.Rs(
				"SELECT NoUrut, NamaTagihan FROM MS_TAGIHAN AS MS_TAGIHAN WHERE NoKontrak = '"+NoKontrak+"' ORDER BY NoUrut"
				);

			ddl.Items.Add(new ListItem("*** Unallocated","0"));
			for(int i=0;i<rsTagihan.Rows.Count;i++)
			{
				ddl.Items.Add(new ListItem(rsTagihan.Rows[i]["NamaTagihan"].ToString()
					,rsTagihan.Rows[i]["NoUrut"].ToString()));
			}
		}

		private void FillTable()
		{
			list.Controls.Clear();
			
			rs = Db.Rs("SELECT * FROM MS_PELUNASAN WHERE NoKontrak = '" + NoKontrak + "' ORDER BY NoUrut");
			
			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				Label l;
				DropDownList ddl;

				l = new Label();
				l.Text = "<tr><td>";
				list.Controls.Add(l);

				ddl = new DropDownList();
				ddl.ID = "notagihan_" + i;
				BindTagihan(ddl);
				try
				{
					ddl.SelectedValue = rs.Rows[i]["NoTagihan"].ToString();
				}
				catch{}
				ddl.Width = 130;
				ddl.CssClass = "ddl";
				ddl.Attributes["Style"] = "font:8pt";
				list.Controls.Add(ddl);

				l = new Label();
				l.Text = "</td>";
				list.Controls.Add(l);

				l = new Label();
				string strAkunting = "";
				if(Func.CekAkunting(Convert.ToInt32(rs.Rows[i]["NoTTS"])))
					strAkunting = "<span style='color: red;'>Akunting</span>";
				l.Text = "<td>"+rs.Rows[i]["NoTTS"].ToString().PadLeft(7,'0')+"<br />" + strAkunting + "</td>"
					+ "<td>"+rs.Rows[i]["CaraBayar"]+"</td>"
					+ "<td>"+rs.Rows[i]["Ket"]+"</td>"
					+ "<td>"+Cf.Day(rs.Rows[i]["TglPelunasan"])+"</td>"
					+ "<td align=right>"+Cf.Num(rs.Rows[i]["NilaiPelunasan"])+"</td>"
					+ "</tr>";
				list.Controls.Add(l);
			}
		}

		private bool Save()
		{
			DataTable rsBef = Db.Rs("SELECT "
				+ " CONVERT(VARCHAR,NoUrut) + '.  ' + CaraBayar + ' ' + Ket + '    ' + CONVERT(VARCHAR,TglPelunasan,106) + '   ' + CONVERT(VARCHAR,NilaiPelunasan,1) + '  Tagihan : ' + CONVERT(VARCHAR,NoTagihan)"
				+ " FROM MS_PELUNASAN WHERE NoKontrak = '" + NoKontrak + "' ORDER BY NoUrut");
			
			for(int i=0;i<rs.Rows.Count;i++)
			{
				DropDownList notagihan = (DropDownList) list.FindControl("notagihan_" + i);
				
				int NoUrut = Convert.ToInt32(rs.Rows[i]["NoUrut"]);
				int NoTagihan = Convert.ToInt32(notagihan.SelectedValue);

				string strSql = "SELECT"
					+ " CaraBayar AS [Cara Bayar]"
					+ ", Ket AS [Keterangan]"
					+ ", TglPelunasan AS [Tgl. Pelunasan]"
					+ ", NilaiPelunasan AS [Nilai]"
					+ ", NoTagihan AS [Tagihan]"
					+ " FROM MS_PELUNASAN"
					+ " WHERE NoKontrak = '" + NoKontrak + "'"
					+ " AND NoUrut = " + NoUrut
					;
				DataTable AnomaliBef = Db.Rs(strSql);

				Db.Execute("EXEC spPelunasanEdit "
					+ " '" + NoKontrak + "'"
					+ ", " + NoUrut
					+ ", " + NoTagihan
					);

				if(NoTagihan != Convert.ToInt32(rs.Rows[i]["NoTagihan"]))
				{
					int Akunting = Db.SingleInteger("SELECT Akunting FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS WHERE NoTTS = " + Cf.Pk(rs.Rows[i]["NoTTS"]));

					if(Akunting == 1)
					{
						DataTable AnomaliAft = Db.Rs(strSql);
						string NoVoucher = Db.SingleString("SELECT NoVoucher FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS WHERE NoTTS = '" + Cf.Pk(rs.Rows[i]["NoTTS"]) + "'");

						Akun.InsertAnomali("TTS", Cf.Pk(rs.Rows[i]["NoTTS"]), Cf.LogCapture(AnomaliBef), Cf.LogCapture(AnomaliAft), "EDIT ALOKASI PELUNASAN", "", NoVoucher);
					}
				}
			}

			DataTable rsAft = Db.Rs("SELECT "
				+ " CONVERT(VARCHAR,NoUrut) + '.  ' + CaraBayar + ' ' + Ket + '    ' + CONVERT(VARCHAR,TglPelunasan,106) + '   ' + CONVERT(VARCHAR,NilaiPelunasan,1) + '  Tagihan : ' + CONVERT(VARCHAR,NoTagihan)"
				+ " FROM MS_PELUNASAN WHERE NoKontrak = '" + NoKontrak + "' ORDER BY NoUrut");

			DataTable rsDetail = Db.Rs("SELECT"
				+ " MS_KONTRAK.NoKontrak AS [No. Kontrak]"
				+ ",MS_KONTRAK.NoUnit AS [Unit]"
				+ ",MS_CUSTOMER.Nama AS [Customer]"
				+ " FROM MS_KONTRAK AS MS_KONTRAK INNER JOIN MS_CUSTOMER AS MS_CUSTOMER"
				+ " ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
				+ " WHERE MS_KONTRAK.NoKontrak = '" + NoKontrak + "'");

			string KetLog = Cf.LogCapture(rsDetail)
				+ Cf.LogList(rsBef, rsAft, "ALOKASI PELUNASAN");
				
			Db.Execute("EXEC spLogKontrak"
				+ " 'EAP'"
				+ ",'" + Act.UserID + "'"
				+ ",'" + Act.IP + "'"
				+ ",'" + KetLog + "'"
				+ ",'" + NoKontrak + "'"
				);

            decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
            string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
            Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

            return true;
		}

		protected void ok_Click(object sender, System.EventArgs e)
		{
			Save();
			
			if(Request.QueryString["reset"]!=null)
			{
				if(Request.QueryString["dd"]!=null)
					Response.Redirect("KontrakDaftar3.aspx?done=1&NoKontrak="+NoKontrak);
				else
					Response.Redirect("TagihanReset.aspx?done="+NoKontrak);
			}
			else
			{
				if(Request.QueryString["dd"]!=null)
					Response.Redirect("KontrakDaftar3.aspx?NoKontrak="+NoKontrak+"&done=1");
                else if (Request.QueryString["custom"] != null)
					Response.Redirect("TagihanCustom.aspx?done="+NoKontrak);
				else
					Response.Redirect("TagihanReschedule.aspx?done="+NoKontrak);
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

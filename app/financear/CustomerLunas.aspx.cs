using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.FINANCEAR
{
	public partial class CustomerLunas : System.Web.UI.Page
	{
		protected DataTable rs;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			if(!Act.Sec("ED:"+Request.PhysicalPath))
			{
				ok.Enabled = false;
				save.Enabled = false;
			}

			if(Tipe!="TENANT")
			{
				FillTable();
				tenant.Visible = false;
			}
			else
			{
				//tipe tenant
				tenant.Visible = true;
				frm.Visible = false;
			}

			FeedBack();
			Js.Confirm(this, "Lanjutkan proses edit alokasi pelunasan?");
		}

		private void FeedBack()
		{
			feed.Text = "";
			if(!Page.IsPostBack)
			{
				if(Request.QueryString["done"]!=null)
					feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
						+ "Edit Alokasi Pelunasan Berhasil...";
			}
		}
		
		private void BindTagihan(DropDownList ddl)
		{
			DataTable rsTagihan = Db.Rs(
				"SELECT NoUrut, NamaTagihan FROM "+Tb+"..MS_TAGIHAN AS MS_TAGIHAN WHERE NoKontrak = '"+Ref+"' ORDER BY NoUrut"
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

			rs = Db.Rs("SELECT * FROM "+Tb+"..MS_PELUNASAN WHERE NoKontrak = '" + Ref + "' ORDER BY NoUrut");
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
                if (Convert.ToInt32(rs.Rows[i]["NoTTS"]) == 0)
                {
                    l.Text = "<td><a href='MEMOEdit.aspx?NoMEMO=" + rs.Rows[i]["NoMEMO"] + "'>" + rs.Rows[i]["NoMEMO"].ToString().PadLeft(7, '0') + "</a></td>"
                        + "<td>" + rs.Rows[i]["CaraBayar"] + "</td>"
                        + "<td>" + rs.Rows[i]["Ket"] + "</td>"
                        + "<td>" + Cf.Day(rs.Rows[i]["TglPelunasan"]) + "</td>"
                        + "<td align=right>" + Cf.Num(rs.Rows[i]["NilaiPelunasan"]) + "</td>"
                        + "</tr>";
                    list.Controls.Add(l);
                }
                else
                {
                    l.Text = "<td><a href='TTSEdit.aspx?NoTTS=" + rs.Rows[i]["NoTTS"] + "'>" + rs.Rows[i]["NoTTS2"].ToString() + "</a></td>"
                        + "<td>" + rs.Rows[i]["CaraBayar"] + "</td>"
                        + "<td>" + rs.Rows[i]["Ket"] + "</td>"
                        + "<td>" + Cf.Day(rs.Rows[i]["TglPelunasan"]) + "</td>"
                        + "<td align=right>" + Cf.Num(rs.Rows[i]["NilaiPelunasan"]) + "</td>"
                        + "</tr>";
                    list.Controls.Add(l);
                }
			}
		}

		private bool Save()
		{
			DataTable rsBef = Db.Rs("SELECT "
				+ " CONVERT(VARCHAR,NoUrut) + '.  ' + CaraBayar + ' ' + Ket + '    ' + CONVERT(VARCHAR,TglPelunasan,106) + '   ' + CONVERT(VARCHAR,NilaiPelunasan,1) + '  Tagihan : ' + CONVERT(VARCHAR,NoTagihan)"
				+ " FROM "+Tb+"..MS_PELUNASAN WHERE NoKontrak = '" + Ref + "' ORDER BY NoUrut");
			
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
					+ " FROM " + Tb + "..MS_PELUNASAN"
					+ " WHERE NoKontrak = '" + Ref + "'"
					+ " AND NoUrut = " + NoUrut
					;
				DataTable AnomaliBef = Db.Rs(strSql);

				Db.Execute("EXEC "+Tb+"..spPelunasanEdit "
					+ " '" + Ref + "'"
					+ ", " + NoUrut
					+ ", " + NoTagihan
					);
                
			}

			DataTable rsAft = Db.Rs("SELECT "
				+ " CONVERT(VARCHAR,NoUrut) + '.  ' + CaraBayar + ' ' + Ket + '    ' + CONVERT(VARCHAR,TglPelunasan,106) + '   ' + CONVERT(VARCHAR,NilaiPelunasan,1) + '  Tagihan : ' + CONVERT(VARCHAR,NoTagihan)"
				+ " FROM "+Tb+"..MS_PELUNASAN WHERE NoKontrak = '" + Ref + "' ORDER BY NoUrut");

			DataTable rsDetail = Db.Rs("SELECT"
				+ " MS_KONTRAK.NoKontrak AS [No. Kontrak]"
				+ ",MS_KONTRAK.NoUnit AS [Unit]"
				+ ",MS_CUSTOMER.Nama AS [Customer]"
				+ " FROM "+Tb+"..MS_KONTRAK AS MS_KONTRAK INNER JOIN "+Tb+"..MS_CUSTOMER AS MS_CUSTOMER"
				+ " ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
				+ " WHERE MS_KONTRAK.NoKontrak = '" + Ref + "'");

			string KetLog = Cf.LogCapture(rsDetail)
				+ Cf.LogList(rsBef, rsAft, "ALOKASI PELUNASAN");
				
			Db.Execute("EXEC spLogTTS"
				+ " 'EAP'"
				+ ",'" + Act.UserID + "'"
				+ ",'" + Act.IP + "'"
				+ ",'" + KetLog + "'"
				+ ",''"
				);

            decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_TTS_LOG ORDER BY LogID DESC");
            string Project = Db.SingleString("SELECT Project FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = '" + Ref + "'");
            Db.Execute("UPDATE MS_TTS_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

            return true;
		}

		protected void ok_Click(object sender, System.EventArgs e)
		{
			if(Save()) Js.Close(this);
		}

		protected void save_Click(object sender, System.EventArgs e)
		{
			if(Save()) Response.Redirect("CustomerLunas.aspx?Tipe="+Tipe+"&Ref="+Ref+"&done=1");
		}

		private string Tb
		{
			get
			{
				return Sc.MktTb(Tipe);
			}
		}

		private string Tipe
		{
			get
			{
				return Cf.Pk(Request.QueryString["Tipe"]);
			}
		}

		private string Ref
		{
			get
			{
				return Cf.Pk(Request.QueryString["Ref"]);
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

using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
	public partial class KomisiBayar : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			if(!IsPostBack)
			{	
				Check();
				Fill();
			}

			Js.Confirm(this, "Lanjutkan dengan proses Pembayaran Komisi?");
		}

		private void Check()
		{
			if(Db.SingleBool("SELECT SudahBayar FROM MS_KOMISI_DETAIL WHERE NoKontrak = '" + NoKontrak + "' AND Baris = '" + NoUrut + "' AND BarisTermin = '" + Baris + "'"))
				cbStatus.Checked = true;
			else
				cbStatus.Checked = false;
		}

		private void Fill()
		{
			string strSql = "SELECT a.*, b.NoAgent, b.NoCustomer, b.NoUnit"
				+ " FROM MS_KOMISI_DETAIL a"
				+ " INNER JOIN MS_KONTRAK b ON a.NoKontrak = b.NoKontrak"
				+ " WHERE a.NoKontrak = '" + NoKontrak + "'"
				+ " AND a.Baris = '" + NoUrut + "' "
                + " AND a.BarisTermin = '" + Baris + "' "
				;
			DataTable rs = Db.Rs(strSql);

			if(rs.Rows.Count == 0)
				Response.Redirect("/CustomError/Deleted.html");
			else
			{
				lblNoKontrak.Text = rs.Rows[0]["NoKontrak"].ToString();
                lblAgent.Text = Db.SingleString("SELECT NamaPenerima FROM MS_KOMISI Where NoKontrak = '" + NoKontrak + "'");
				lblCustomer.Text = Db.SingleString("SELECT Nama FROM MS_CUSTOMER WHERE NoCustomer = '" + Cf.Pk(rs.Rows[0]["NoCustomer"]) + "' ");
				lblUnit.Text = rs.Rows[0]["NoUnit"].ToString();
                decimal NByr = Db.SingleDecimal("Select NilaiKomisi From MS_KOMISI_DETAIL Where NoKontrak ='" + NoKontrak + "' AND Baris ='" + NoUrut + "' AND BarisTermin ='" + Baris + "'");
				if(cbStatus.Checked)
				{
					tbNota.Text = rs.Rows[0]["NoNota"].ToString().PadLeft(7, '0');
					tbTglBayar.Text = Cf.Day(rs.Rows[0]["TglBayar"]);
					tbNilai.Text = Cf.Num(rs.Rows[0]["NilaiBayar"]);
				}
				else
				{
					int NoNota = Db.SingleInteger("SELECT ISNULL(MAX(NoNota), 0) FROM MS_KOMISI_DETAIL");
					NoNota += 1;
					tbNota.Text = NoNota.ToString().PadLeft(7, '0'); 
					tbTglBayar.Text = Cf.Day(DateTime.Now);
					tbNilai.Text = Cf.Num(Math.Round(NByr));
				}

				tbNilai.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
				tbNilai.Attributes["onkeyup"] = "CalcType(this,tempnum);";
				tbNilai.Attributes["onblur"] = "CalcBlur(this);";
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

		private bool Valid()
		{
			bool x = true;
			string s = "";

			if(Cf.isEmpty(tbNota))
			{
				x = false;

				if(s == "")
					s = tbNota.ID;

				lblNotac.Text = "Kosong";
			}else
				lblNotac.Text = "";

			if(!Cf.isInt(tbNota))
			{
				x = false;

				if(s == "")
					s = tbNota.ID;

				lblNotac.Text = "Format";
			}else
				lblNotac.Text = "";

			if(!Cf.isTgl(tbTglBayar))
			{
				x = false;

				if(s == "")
					s = tbTglBayar.ID;

				lblTglBayarc.Text = "Tanggal";
			}else
				lblTglBayarc.Text = "";

			if(Cf.isEmpty(tbNilai))
			{
				x = false;

				if(s == "")
					s = tbNilai.ID;

				lblNilaic.Text = "Kosong";
			}
			else
				lblNilaic.Text = "";

			if(!x)
			{
				this.RegisterStartupScript(
					"focusScript"
					, "<script language='javascript' type='text/javascript'>"
					+ "document.getElementById('" + s + "').focus();"
					+ "</script>"
					);
			}

			return x;
		}

		private bool isDuplikat()
		{
			bool x = false;
			string s = "";

            decimal komisi = Db.SingleDecimal("SELECT NilaiKomisi From MS_KOMISI_DETAIL WHERE NoKontrak ='" + NoKontrak + "' And Baris='" + NoUrut + "' And BarisTermin='" + Baris + "'");
            if (Convert.ToDecimal(tbNilai.Text) > Convert.ToDecimal(Math.Round(komisi)))
            {
                x = true;
            }

			int intNoNota = Db.SingleInteger("SELECT NoNota FROM MS_KOMISI_DETAIL WHERE NoKontrak = '" + NoKontrak + "' AND Baris = " + NoUrut + " AND BarisTermin = " + Baris);
			if(cbStatus.Checked && (Convert.ToInt32(tbNota.Text) != intNoNota))
			{
				if(Db.SingleInteger("SELECT COUNT(*) FROM MS_KOMISI_DETAIL WHERE NoNota = " + Convert.ToInt32(tbNota.Text)) > 0)
				{
					x = true;
					
					if(s == "")
						s = tbNota.ID;

					lblNotac.Text = "Duplikat";
				}
				else
					lblNotac.Text = "";
			}
			else if(!cbStatus.Checked)
			{
				if(Db.SingleInteger("SELECT COUNT(*) FROM MS_KOMISI_DETAIL WHERE NoNota = " + Convert.ToInt32(tbNota.Text)) > 0)
				{
					x = true;
					
					if(s == "")
						s = tbNota.ID;

					lblNotac.Text = "Duplikat";
				}
				else
					lblNotac.Text = "";

			}

            
            if (x)
            {
                Js.Alert(
                    this
                    , "1. Nilai harus diisi.\\n"
                    + "2. Nilai Tidak Boleh Melebihi Batas Komisi.\\n"
                    , " document.getElementById('" + s + "').focus();"
                    + " document.getElementById('" + s + "').select();"
                    );
            }
			return x;
		}

		private void Save()
		{
			int NoNota = Convert.ToInt32(tbNota.Text);
			DateTime TglBayar = Convert.ToDateTime(tbTglBayar.Text);
			decimal NilaiBayar = Convert.ToDecimal(tbNilai.Text);

			DataTable rsBef = Db.Rs("SELECT * FROM MS_KOMISI_DETAIL WHERE NoKontrak = '" + NoKontrak + "' AND Baris = '" + NoUrut + "' AND BarisTermin = '" + Baris + "'");

			string strSql = "UPDATE MS_KOMISI_DETAIL"
				+ " SET NoNota = " + NoNota
				+ ", TglBayar = '" + TglBayar + "'"
				+ ", NilaiBayar = " + NilaiBayar
				+ ", SudahBayar = 1"
				+ " WHERE NoKontrak = '" + NoKontrak + "'"
				+ " AND Baris = " + NoUrut
                + " AND BarisTermin = " + Baris
				;
			Db.Execute(strSql);
            
			DataTable rsAft = Db.Rs("SELECT * FROM MS_KOMISI_DETAIL WHERE NoKontrak = '" + NoKontrak + "' AND Baris = '" + NoUrut + "' AND BarisTermin = '" + Baris + "'");
			
			string Ket = "";

			if(!cbStatus.Checked)
			{
				Ket = "---BAYAR KOMISI---<br>"
                    + Cf.LogCapture(Db.Rs("SELECT * FROM MS_KOMISI_DETAIL WHERE NoKontrak = '" + NoKontrak + "' AND Baris = '" + NoUrut + "' AND BarisTermin = '" + Baris + "'"));
			    
            }
			else
			{
				Ket = "---EDIT KOMISI---<br>"
					+ Cf.LogCompare(rsBef, rsAft);
			}
				
			Db.Execute("EXEC spLogKontrak"
				+ " 'BAYAR KOMISI'"
				+ ",'" + Act.UserID + "'"
				+ ",'" + Act.IP + "'"
				+ ",'" + Ket + "'"
				+ ",'" + NoKontrak + "'"
				);

			this.RegisterStartupScript(
				"closeScript"
				, "<script language='javascript' type='text/javascript'>"
				+ "dialogArguments.location.href = 'KontrakJadwalKomisi.aspx?NoKontrak=" + NoKontrak + "&done=2';"
				+ "window.close();"
				+ "</script>"
				);

            Response.Redirect("KontrakJadwalKomisi.aspx?Nokontrak=" + NoKontrak);
		}

		protected void btnOK_Click(object sender, System.EventArgs e)
		{
			if(Valid())
			{
				if(!isDuplikat())
					Save();
                
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

        private string Baris
        {
            get
            {
                return Cf.Pk(Request.QueryString["Baris"]);
            }
        }
        protected void cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("KontrakJadwalKomisi.aspx?Nokontrak=" + NoKontrak);
        }
}
}

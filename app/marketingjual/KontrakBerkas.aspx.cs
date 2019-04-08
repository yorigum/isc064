using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
	
	public partial class KontrakBerkas : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			if(!IsPostBack)
				newFill();
				//Fill();

			Js.Confirm(this, "Lanjutkan dengan proses edit Checklist Berkas?");
		}

		protected void Fill()
		{
			string strSql = "SELECT "
				+ " MS_KONTRAK.*"
				+ ",MS_CUSTOMER.Nama AS Cs"
				+ " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
				+ " WHERE MS_KONTRAK.NoKontrak = '" + NoKontrak + "'";

			DataTable rs = Db.Rs(strSql);

			if(rs.Rows.Count==0)
				Response.Redirect("/CustomError/Deleted.html");
			else
			{
				kontrak.Text = rs.Rows[0]["NoKontrak"].ToString();
				unit.Text = rs.Rows[0]["NoUnit"].ToString();
				customer.Text = rs.Rows[0]["Cs"].ToString();
//				DataTable rstgl = Db.Rs("SELECT ISNULL(TglSelesaiBerkas,0) as tgl FROM MS_KONTRAK WHERE NoKontrak = '"+ rs.Rows[0]["NoKontrak"] +"'");
//				DateTime tglberkas = Convert.ToDateTime(rstgl.Rows[0]["tgl"]);
				if (rs.Rows[0]["TglSelesaiberkas"].Equals(System.DBNull.Value))
				{
					tgl.Text = "";	  
				}
				else
				{
					tgl.Text = Cf.Day(rs.Rows[0]["TglSelesaiBerkas"]);
				}
				
				if(Convert.ToBoolean(rs.Rows[0]["StatusBerkas"]) == true)
					status.Items[1].Selected = true;

				string[] x = rs.Rows[0]["CheckListDokumen"].ToString().Split(';');

				foreach(string y in x)
				{
					for(int i = 0; i < umum.Items.Count; i++)
					{
						if(y == umum.Items[i].Text)
							umum.Items[i].Selected = true;
					}

					for(int i = 0; i < pn.Items.Count; i++)
					{
						if(y == pn.Items[i].Text)
							pn.Items[i].Selected = true;
					}

					for(int i = 0; i < swasta.Items.Count; i++)
					{
						if(y == swasta.Items[i].Text)
							swasta.Items[i].Selected = true;
					}

					for(int i = 0; i < wira.Items.Count; i++)
					{
						if(y == wira.Items[i].Text)
							wira.Items[i].Selected = true;
					}

					for(int i = 0; i < lain.Items.Count; i++)
					{
						if(y == lain.Items[i].Text)
							lain.Items[i].Selected = true;
					}
				}
			}
		}

		protected bool Valid()
		{
			bool x = true;
			string s = "";
//
//			if(!Cf.isTgl(tgl))
//			{
//				x = false;
//
//				if(s == "")
//					s = tgl.ID;
//
//				tglc.Text = "Tanggal";
//			}else
//				tglc.Text = "";

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

		protected void ok_Click(object sender, System.EventArgs e)
		{
			if(Valid())
			{

				bool StatusBerkas = false;
				if(status.SelectedValue == "1")
					StatusBerkas = true;

				string CheckListDokumen = "";
				System.Text.StringBuilder x = new System.Text.StringBuilder();

				for(int i = 0; i < umum.Items.Count; i++)
				{
					if(umum.Items[i].Selected)
						x.Append(umum.Items[i].Text + ";");
				}

				for(int i = 0; i < pn.Items.Count; i++)
				{
					if(pn.Items[i].Selected)
						x.Append(pn.Items[i].Text + ";");
				}

				for(int i = 0; i < swasta.Items.Count; i++)
				{
					if(swasta.Items[i].Selected)
						x.Append(swasta.Items[i].Text + ";");
				}

				for(int i = 0; i < wira.Items.Count; i++)
				{
					if(wira.Items[i].Selected)
						x.Append(wira.Items[i].Text + ";");
				}

				for(int i = 0; i < lain.Items.Count; i++)
				{
					if(lain.Items[i].Selected)
						x.Append(lain.Items[i].Text + ";");
				}

				CheckListDokumen = x.ToString();

				DataTable rsBef = Db.Rs("SELECT "
					+ "CheckListDokumen AS [Check List Dokumen]"
					+ ", StatusBerkas AS [StatusBerkas]"
					//+ ", ISNULL(TglSelesaiBerkas,0) AS [Tgl. Selesai Berkas]"
					+ " FROM MS_KONTRAK"
					+ " WHERE NoKontrak = '" + NoKontrak + "'"
					);
			
				if(tgl.Text != "")
				{
					DateTime Tgl = Convert.ToDateTime(tgl.Text);
					Db.Execute("UPDATE MS_KONTRAK"
						+ " SET CheckListDokumen = '" + CheckListDokumen + "'"
						+ ", StatusBerkas = " + Cf.BoolToSql(StatusBerkas)
						+ ", TglSelesaiBerkas = '" + Tgl + "'"
						+ " WHERE NoKontrak = '" + NoKontrak + "'"
						);
				}
				else
				{
					Db.Execute("UPDATE MS_KONTRAK"
						+ " SET CheckListDokumen = '" + CheckListDokumen + "'"
						+ ", StatusBerkas = " + Cf.BoolToSql(StatusBerkas)
						//+ ", TglSelesaiBerkas = '" + null + "'"
						+ " WHERE NoKontrak = '" + NoKontrak + "'"
						);
				}

				DataTable rsAft = Db.Rs("SELECT "
					+ "CheckListDokumen AS [Check List Dokumen]"
					+ ", StatusBerkas AS [StatusBerkas]"
					//+ ", TglSelesaiBerkas AS [Tgl. Selesai Berkas]"
					+ " FROM MS_KONTRAK"
					+ " WHERE NoKontrak = '" + NoKontrak + "'"
					);
			
				string Ket = Cf.LogCompare(rsBef, rsAft);
				
				Db.Execute("EXEC spLogKontrak"
					+ " 'EDIT'"
					+ ",'" + Act.UserID + "'"
					+ ",'" + Act.IP + "'"
					+ ",'" + Ket + "'"
					+ ",'" + NoKontrak + "'"
					);

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
                string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                Js.Close(this);
			}
		}

		private void newFill()
		{
			string strSql = "SELECT a.NoKontrak"
							+", a.NoUnit"
							+", a.CheckListDokumen"
							+", a.StatusBerkas"
							+", a.TglSelesaiBerkas"
							+", b.Nama FROM MS_KONTRAK a INNER JOIN MS_CUSTOMER b ON a.NoCustomer = b.NoCustomer WHERE a.NoKontrak = '"+ nk +"'";
			DataTable rs = Db.Rs(strSql);
			
			if(rs.Rows.Count==0)
				Response.Redirect("/CustomError/Deleted.html");
			else
			{
				kontrak.Text = nk;
				unit.Text = rs.Rows[0]["NoUnit"].ToString();
				customer.Text = rs.Rows[0]["Nama"].ToString();

				string[] x = rs.Rows[0]["CheckListDokumen"].ToString().Split(';');

				foreach(string y in x)
				{
					for(int i = 0; i < umum.Items.Count; i++)
					{
						if(y == umum.Items[i].Text)
							umum.Items[i].Selected = true;
					}

					for(int i = 0; i < pn.Items.Count; i++)
					{
						if(y == pn.Items[i].Text)
							pn.Items[i].Selected = true;
					}

					for(int i = 0; i < swasta.Items.Count; i++)
					{
						if(y == swasta.Items[i].Text)
							swasta.Items[i].Selected = true;
					}

					for(int i = 0; i < wira.Items.Count; i++)
					{
						if(y == wira.Items[i].Text)
							wira.Items[i].Selected = true;
					}

					for(int i = 0; i < lain.Items.Count; i++)
					{
						if(y == lain.Items[i].Text)
							lain.Items[i].Selected = true;
					}
				}

				if(Convert.ToBoolean(rs.Rows[0]["StatusBerkas"]) == true)
					status.Items[1].Selected = true;

				if (rs.Rows[0]["TglSelesaiberkas"].Equals(System.DBNull.Value))
				{
					tgl.Text = "";	  
				}
				else
				{
					tgl.Text = Cf.Day(rs.Rows[0]["TglSelesaiBerkas"]);
				}
			}
		}

		protected string NoKontrak
		{
			get
			{
				return Cf.Pk(Request.QueryString["NoKontrak"]);
			}
		}

		protected string nk
		{
			get
			{
				return NoKontrak;
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

		protected void umum_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

	}
}

using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
	public partial class KontrakEditKey : System.Web.UI.Page
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			if(!Page.IsPostBack)
			{
				Fill();
			}
		}

		private void Fill()
		{
			string strSql = "SELECT * FROM MS_KONTRAK WHERE NoKontrak = '" + Lama + "'";
			DataTable rs = Db.Rs(strSql);
			if(rs.Rows.Count==0)
				Response.Redirect("/CustomError/Deleted.html");
			else
			{
				baru.Text = rs.Rows[0]["NoKontrak"].ToString();
				
				noppjb.Text = rs.Rows[0]["NoPPJB"].ToString();
				noajb.Text = rs.Rows[0]["NoAJB"].ToString();
				nobast.Text = rs.Rows[0]["NoST"].ToString();

				if(noppjb.Text=="") ppjb.Visible = false;
				if(noajb.Text=="") ajb.Visible = false;
				if(nobast.Text=="") st.Visible = false;
			}
		}

		private bool unik()
		{
			bool x = true;

			int c = Db.SingleInteger(
				"SELECT COUNT(*) FROM MS_KONTRAK WHERE"
				+ " NoKontrak = '" + Baru + "'"
				+ " AND NoKontrak <> '" + Lama + "'"
				);

			if(c!=0)
				x = false;

			return x;
		}

		private bool unikPPJB(string NoPPJB)
		{
			bool x = true;

			int c = Db.SingleInteger(
				"SELECT COUNT(*) FROM MS_KONTRAK WHERE"
				+ " NoPPJB = '" + NoPPJB + "'"
				+ " AND NoKontrak <> '" + Lama + "'"
				);

			if(c!=0)
				x = false;

			return x;
		}

		private bool unikAJB(string NoAJB)
		{
			bool x = true;

			int c = Db.SingleInteger(
				"SELECT COUNT(*) FROM MS_KONTRAK WHERE"
				+ " NoAJB = '" + NoAJB + "'"
				+ " AND NoKontrak <> '" + Lama + "'"
				);

			if(c!=0)
				x = false;

			return x;
		}

		private bool unikST(string NoST)
		{
			bool x = true;

			int c = Db.SingleInteger(
				"SELECT COUNT(*) FROM MS_KONTRAK WHERE"
				+ " NoST = '" + NoST + "'"
				+ " AND NoKontrak <> '" + Lama + "'"
				);

			if(c!=0)
				x = false;

			return x;
		}

		private bool valid()
		{
			bool x = true;

			baru.Text = Cf.Pk(baru.Text);
			if(Cf.isEmpty(baru))
			{
				x = false;
				baruc.Text = "Kosong";
			}
			else
			{
				if(!unik())
				{
					x = false;
					baruc.Text = "Duplikat";
				}
				else
					baruc.Text = "";
			}

			if(ppjb.Visible)
			{
				string NoPPJB = Cf.Pk(noppjb.Text);
				if(Cf.isEmpty(noppjb))
				{
					x = false;
					noppjbc.Text = "Kosong";
				}
				else
				{
					if(!unikPPJB(NoPPJB))
					{
						x = false;
						noppjbc.Text = "Duplikat";
					}
					else
						noppjbc.Text = "";
				}
			}

			if(ajb.Visible)
			{
				string NoAJB = Cf.Pk(noajb.Text);
				if(Cf.isEmpty(noajb))
				{
					x = false;
					noajbc.Text = "Kosong";
				}
				else
				{
					if(!unikAJB(NoAJB))
					{
						x = false;
						noajbc.Text = "Duplikat";
					}
					else
						noajbc.Text = "";
				}
			}

			if(st.Visible)
			{
				string NoST = Cf.Pk(nobast.Text);
				if(Cf.isEmpty(nobast))
				{
					x = false;
					nobastc.Text = "Kosong";
				}
				else
				{
					if(!unikST(NoST))
					{
						x = false;
						nobastc.Text = "Duplikat";
					}
					else
						nobastc.Text = "";
				}
			}

			if(!x)
				Js.Alert(
					this
					, "Input Tidak Valid.\\n\\n"
					+ "Aturan Proses :\\n"
					+ "1. No. Kontrak harus diisi dan tidak boleh duplikat.\\n"
					+ "2. No. PPJB harus diisi dan tidak boleh duplikat.\\n"
					+ "3. No. AJB harus diisi dan tidak boleh duplikat.\\n"
					+ "4. No. BAST harus diisi dan tidak boleh duplikat.\\n"
					, ""
					);

			return x;
		}

		protected void save_Click(object sender, System.EventArgs e)
		{
			if(valid())
			{
				if(Lama!=Baru)
				{
					Db.Execute("EXEC spKontrakGantiKey "
						+ " '" + Lama + "'"
						+ ",'" + Baru + "'"
						);

					//Logfile
					string Ket = "Ganti PK : " + Lama + " -> " + Baru;

					Db.Execute("EXEC spLogKontrak "
						+ " 'EDIT'"
						+ ",'" + Act.UserID + "'"
						+ ",'" + Act.IP + "'"
						+ ",'" + Ket + "'"
						+ ",'" + Baru + "'"
						);
				}

				SaveLegal();

				RegisterStartupScript("closerfr"
					,"<script language='javascript'>"
					+ " dialogArguments.location.href='KontrakEdit.aspx?done=1&NoKontrak="+Baru+"';"
					+ " window.close();"
					+ "</script>"
					);
			}
		}

		private void SaveLegal()
		{
			DataTable rsBef = Db.Rs("SELECT"
				+ " NoPPJB AS [No. PPJB]"
				+ ",NoAJB AS [No. AJB]"
				+ ",NoST AS [No. BAST]"
				+ " FROM MS_KONTRAK"
				+ " WHERE MS_KONTRAK.NoKontrak = '" + Baru + "'");

			string NoPPJB = Cf.Pk(noppjb.Text);
			string NoAJB = Cf.Pk(noajb.Text);
			string NoST = Cf.Pk(nobast.Text);

			Db.Execute("UPDATE MS_KONTRAK SET"
				+ " NoPPJB = '"+NoPPJB+"'"
				+ ",NoAJB = '"+NoAJB+"'"
				+ ",NoST = '"+NoST+"'"
				+ " WHERE NoKontrak = '"+Baru+"'"
				);

			DataTable rsAft = Db.Rs("SELECT"
				+ " NoPPJB AS [No. PPJB]"
				+ ",NoAJB AS [No. AJB]"
				+ ",NoST AS [No. BAST]"
				+ " FROM MS_KONTRAK"
				+ " WHERE MS_KONTRAK.NoKontrak = '" + Baru + "'");

			DataTable rs = Db.Rs("SELECT"
				+ " MS_KONTRAK.NoKontrak AS [No. Kontrak]"
				+ ",MS_KONTRAK.NoUnit AS [Unit]"
				+ ",MS_CUSTOMER.Nama AS [Customer]"
				+ " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER"
				+ " ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
				+ " WHERE MS_KONTRAK.NoKontrak = '" + Baru + "'");

			string ket = Cf.LogCapture(rs)
				+ Cf.LogCompare(rsBef,rsAft);

			Db.Execute("EXEC spLogKontrak "
				+ " 'EDIT'"
				+ ",'" + Act.UserID + "'"
				+ ",'" + Act.IP + "'"
				+ ",'" + ket + "'"
				+ ",'" + Baru + "'"
				);
		}

		private string Lama
		{
			get
			{
				return Cf.Pk(Request.QueryString["NoKontrak"]);
			}
		}

		private string Baru
		{
			get
			{
				return Cf.Pk(baru.Text);
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

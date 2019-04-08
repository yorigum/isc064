using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.COLLECTION
{
	public partial class PJTEdit : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

            //Act.CekInt("NoPJT");

			if(!Act.Sec("ED:"+Request.PhysicalPath))
			{
				ok.Enabled = false;
				save.Enabled = false;
			}

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
			btnlog.Attributes["onclick"] = "location.href='LogPk.aspx?Tb=MS_PJT_LOG&Pk="+NoPJT+"'";			
			
			string strSql = "SELECT * FROM MS_PJT WHERE NoPJT = '" + NoPJT + "'";
			DataTable rs = Db.Rs(strSql);

			if(rs.Rows.Count==0)
				Response.Redirect("/CustomError/Deleted.html");
			else
			{
				unit.Text = rs.Rows[0]["Unit"].ToString();
				customer.Text = rs.Rows[0]["Customer"].ToString();
				notelp.Text = rs.Rows[0]["NoTelp"].ToString();
				alamat1.Text = rs.Rows[0]["Alamat1"].ToString();
				alamat2.Text = rs.Rows[0]["Alamat2"].ToString();
				alamat3.Text = rs.Rows[0]["Alamat3"].ToString();
                hp.Text = Db.SingleString("SELECT b.NoHP FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK a INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER b on a.NoCustomer=b.NoCustomer WHERE a.NoKontrak = '" + rs.Rows[0]["Ref"].ToString() + "'");
				tgl.Text = Cf.Day(rs.Rows[0]["TglPJT"]);
                tgljt.Text = Cf.Day(rs.Rows[0]["TglJT"]);
				total.Text = Cf.Num(rs.Rows[0]["Total"]);

				printPJT.InnerHtml = printPJT.InnerHtml + " ("+rs.Rows[0]["PrintPJT"]+")";

				FillTb();
			}
            string Project = Db.SingleString("SELECT Project FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = '" + rs.Rows[0]["Ref"] + "'");
            printPJT.HRef = "PrintPJT.aspx?NoPJT=" + NoPJT + "&project=" + Project;
        }

		private void FillTb()
		{
			System.Text.StringBuilder x = new System.Text.StringBuilder();
			DataTable rs = Db.Rs("SELECT * FROM MS_PJT_DETIL WHERE NoPJT = '" + NoPJT + "'");
			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				x.Append("<li style='font:8pt; text-align:left'>"
					+ "<strong>" + rs.Rows[i]["NamaTagihan"] + "</strong>"
					+ "<br><span style='width:120;padding-left:20; margin-right:10px;'>" + Cf.Day(rs.Rows[i]["TglJT"]) + "</span>"
					+ "Nilai : " + Cf.Num(rs.Rows[i]["Nilai"]) + "</li>");
			}

			detil.InnerHtml = x.ToString();
		}

		private bool valid()
		{
			string s = "";
			bool x = true;

            if (!Cf.isTgl(tgl))
            {
                x = false;
                if (s == "") s = tgl.ID;
                tglc.Text = "Tanggal";
            }
            else
                tglc.Text = "";

			if(Cf.isEmpty(unit))
			{
				x = false;
				if(s=="") s = unit.ID;
				unitc.Text = "Kosong";
			}
			else
				unitc.Text = "";

			if(Cf.isEmpty(customer))
			{
				x = false;
				if(s=="") s = customer.ID;
				customerc.Text = "Kosong";
			}
			else
				customerc.Text = "";

			if(!x)
				Js.Alert(
					this
					, "Input Tidak Valid.\\n\\n"
					+ "Aturan Proses :\\n"
                    + "1. Format Tanggal : Bulan / Tanggal / Tahun.\\n"
					+ "2. Unit Properti tidak boleh kosong.\\n"
					+ "3. Customer tidak boleh kosong.\\n"
					, "document.getElementById('"+s+"').focus();"
					+ "document.getElementById('"+s+"').select();"
					);

			return x;
		}

		private bool Save()
		{
			if(valid())
			{
                DateTime TglPJT = Convert.ToDateTime(tgl.Text);
				string Unit = Cf.Str(unit.Text);
				string Customer = Cf.Str(customer.Text);
				string NoTelp = Cf.Str(notelp.Text);
                string NoHP = Cf.Str(hp.Text);
				string Alamat1 = Cf.Str(alamat1.Text);
				string Alamat2 = Cf.Str(alamat2.Text);
				string Alamat3 = Cf.Str(alamat3.Text);

				DataTable rs = Db.Rs("SELECT "
					+ " CONVERT(varchar, TglPJT, 106) AS [Tanggal]"
					+ ",Tipe"
					+ ",Ref AS [Ref.]"
					+ ",Total"
					+ " FROM MS_PJT"
					+ " WHERE NoPJT = '" + NoPJT + "'"
					);

				DataTable rsBef = Db.Rs("SELECT "
					+ " Unit"
					+ ",Customer"
					+ ",NoTelp AS [No. Telepon]"
					+ ",Alamat1 AS [Alamat #1]"
					+ ",Alamat2 AS [Alamat #2]"
					+ ",Alamat3 AS [Alamat #3]"
                    + ",TglPJT AS [Tgl]"
					+ " FROM MS_PJT"
					+ " WHERE NoPJT = '" + NoPJT + "'"
					);
                
				Db.Execute("EXEC spPJTEdit"
					+ " '" + NoPJT + "'"
					+ ",'" + Unit + "'"
					+ ",'" + Customer + "'"
					+ ",'" + NoTelp + "'"
					+ ",'" + Alamat1 + "'"
					+ ",'" + Alamat2 + "'"
					+ ",'" + Alamat3 + "'"
					);
                string REP = Db.SingleString("SELECT Ref FROM MS_PJT WHERE NoPJT = '" + NoPJT + "'");

                int NOCUS = Db.SingleInteger("SELECT NOCUSTOMER FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NOKONTRAK='" + REP + "'");
                Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER"
                    + " SET ALAMAT1 ='"+ Alamat1 + "'"
                    + ", ALAMAT2 ='" + Alamat2 + "'"
                    + ", ALAMAT3 ='" + Alamat3 + "'"
                    + ", NoTelp ='" + NoTelp + "'"
                    + ", NoHP ='"+ NoHP +"'"
                    + " WHERE NoCustomer="+ NOCUS 
                    );

                string strSql = "SELECT NoKontrak FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoCustomer = " + NOCUS;
                DataTable rs1 = Db.Rs(strSql);
                for (int i = 0; i < rs1.Rows.Count; i++)
                {
                    if (!Response.IsClientConnected) break;

                    Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_TUNGGAKAN"
                           + " SET ALAMAT1 ='" + Alamat1 + "'"
                           + ", ALAMAT2 ='" + Alamat2 + "'"
                           + ", ALAMAT3 ='" + Alamat3 + "'"
                           + ", NoTelp ='" + NoTelp + "'"
                           + " WHERE REF='" + rs1.Rows[i]["NoKontrak"] + "'"
                           );

                    Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_PJT"
                           + " SET ALAMAT1 ='" + Alamat1 + "'"
                           + ", ALAMAT2 ='" + Alamat2 + "'"
                           + ", ALAMAT3 ='" + Alamat3 + "'"
                           + ", NoTelp ='" + NoTelp + "'"
                           + " WHERE REF='" + rs1.Rows[i]["NoKontrak"] + "'"
                           );

                }

                Db.Execute("UPDATE MS_PJT SET TglPJT = '" + TglPJT + "' WHERE NoPJT = '" + NoPJT + "'");

				DataTable rsAft = Db.Rs("SELECT "
					+ " Unit"
					+ ",Customer"
					+ ",NoTelp AS [No. Telepon]"
					+ ",Alamat1 AS [Alamat #1]"
					+ ",Alamat2 AS [Alamat #2]"
					+ ",Alamat3 AS [Alamat #3]"
                    + ",TglPJT AS [Tgl]"
					+ " FROM MS_PJT"
					+ " WHERE NoPJT = '" + NoPJT + "'"
					);
				
				//Logfile
				string ketlog = Cf.LogCapture(rs)
					+ Cf.LogCompare(rsBef,rsAft);

				Db.Execute("EXEC spLogPJT"
					+ " 'EDIT'"
					+ ",'" + Act.UserID + "'"
					+ ",'" + Act.IP + "'"
					+ ",'" + ketlog + "'"
					+ ",'" + NoPJT.ToString() + "'"
					);

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_PJT_LOG ORDER BY LogID DESC");
                string Project = Db.SingleString("SELECT Project FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = (SELECT Ref FROM MS_PJT WHERE NoPJT = '" + NoPJT + "') ");
                Db.Execute("UPDATE MS_PJT_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

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
			if(Save()) Response.Redirect("PJTEdit.aspx?done=1&NoPJT="+NoPJT);
		}

		private string NoPJT
		{
			get
			{
				return Cf.Pk(Request.QueryString["NoPJT"]);
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

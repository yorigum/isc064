using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.ADMINJUAL
{
	public partial class UnitEditKey : System.Web.UI.Page
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
			string strSql = "SELECT * FROM MS_UNIT WHERE NoStock = '" + Lama + "'";
			DataTable rs = Db.Rs(strSql);
			if(rs.Rows.Count==0)
				Response.Redirect("/CustomError/Deleted.html");
			else
			{
				baru.Text = rs.Rows[0]["NoStock"].ToString();
			}
		}

		private bool unik()
		{
			bool x = true;

			int c = Db.SingleInteger(
				"SELECT COUNT(*) FROM MS_UNIT WHERE"
				+ " NoStock = '" + Baru + "'"
				+ " AND NoStock <> '" + Lama + "'"
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

			if(!x)
				Js.Alert(
					this
					, "Input Tidak Valid.\\n\\n"
					+ "Aturan Proses :\\n"
					+ "1. No. Stock harus diisi dan tidak boleh duplikat.\\n"
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
					Db.Execute("EXEC spUnitGantiKey "
						+ " '" + Lama + "'"
						+ ",'" + Baru + "'"
						);

					//Logfile
					string Ket = "Ganti PK : " + Lama + " -> " + Baru;

					Db.Execute("EXEC spLogUnit "
						+ " 'EDIT'"
						+ ",'" + Act.UserID + "'"
						+ ",'" + Act.IP + "'"
						+ ",'" + Ket + "'"
						+ ",'" + Baru + "'"
						);
				}

				RegisterStartupScript("closerfr"
					,"<script type='text/javascript'>"
					+ " dialogArguments.location.href='UnitEdit.aspx?done=1&NoStock="+Baru+"';"
					+ " window.close();"
					+ "</script>"
					);
			}
		}

		private string Lama
		{
			get
			{
				return Cf.Pk(Request.QueryString["NoStock"]);
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

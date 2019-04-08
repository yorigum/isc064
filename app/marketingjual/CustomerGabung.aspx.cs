using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
	public partial class CustomerGabung : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			if(!Page.IsPostBack)
			{
				pilihA.Checked = true;

				Js.Focus(this,del);
				
				del.Attributes["ondblclick"] = "popDaftarCustomer();";
				del.Attributes["onclick"] = "document.getElementById('pilihA').checked=true";
				simpan.Attributes["ondblclick"] = "popDaftarCustomer();";
				simpan.Attributes["onclick"] = "document.getElementById('pilihB').checked=true";
				
				frm.Visible = false;
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
						+ "<a href=\"javascript:popEditCustomer('"+Request.QueryString["done"]+"')\">"
						+ "Gabung Nomor Berhasil..."
						+ "</a>";
			}
		}

		private bool valid()
		{
			string s = "";
			bool x = true;
			
			int n1 = 0;
			try{n1 = Convert.ToInt32(del.Text);}
			catch
			{
				if(s=="") s = del.ID;
				x = false;
			}

			int n2 = 0;
			try{n2 = Convert.ToInt32(simpan.Text);}
			catch
			{
				if(s=="") s = simpan.ID;
				x = false;
			}

			if(n1!=0)
			{
				int c = Db.SingleInteger(
					"SELECT COUNT(*) FROM MS_CUSTOMER WHERE NoCustomer = " + n1);
				if(c==0)
				{
					if(s=="") s = del.ID;
					x = false;
				}
			}

			if(n2!=0)
			{
				int c = Db.SingleInteger(
					"SELECT COUNT(*) FROM MS_CUSTOMER WHERE NoCustomer = " + n2);
				if(c==0)
				{
					if(s=="") s = simpan.ID;
					x = false;
				}
			}

			//customer a dan b sama
			if(n1==n2)
			{
				if(s=="") s = del.ID;
				x = false;
			}

			if(!x)
				Js.Alert(
					this
					, "Customer Tidak Valid.\\n\\n"
					+ "Kemungkinan Sebab :\\n"
					+ "1. Customer tidak terdaftar.\\n"
					+ "2. Customer A dan Customer B harus berbeda.\\n"
					, "document.getElementById('"+s+"').focus();"
					+ "document.getElementById('"+s+"').select();"
					);

			return x;
		}

		protected void next_Click(object sender, System.EventArgs e)
		{
			if(valid())
			{
				pilih.Visible = false;
				frm.Visible = true;

				Fill();

				Js.Focus(this,save);
				Js.Confirm(this,"Lanjutkan proses gabung nomor customer?\\nPerhatian bahwa customer A akan dihapus PERMANEN.");
			}
		}

		private void Fill()
		{
			int n1 = Convert.ToInt32(del.Text);
			int n2 = Convert.ToInt32(simpan.Text);

			nama1.Text = Db.SingleString(
				"SELECT Nama FROM MS_CUSTOMER WHERE NoCustomer = " + n1)
				+ " ("+n1.ToString().PadLeft(5,'0')+")";
			
			nama2.Text = Db.SingleString(
				"SELECT Nama FROM MS_CUSTOMER WHERE NoCustomer = " + n2)
				+ " ("+n2.ToString().PadLeft(5,'0')+")";

			Fill(rpta, n1);
			Fill(rptb, n2);
		}

		protected void save_Click(object sender, System.EventArgs e)
		{
			int n1 = Convert.ToInt32(del.Text);
			int n2 = Convert.ToInt32(simpan.Text);

			string NamaHapus = nama1.Text.Replace("'","''");
			string NamaSimpan = nama2.Text.Replace("'","''");

            DataTable rsHapus = Db.Rs("SELECT * FROM MS_CUSTOMER WHERE NoCustomer = " + n1);
            DataTable kontrakHapus = Db.Rs("SELECT NoKontrak FROM MS_KONTRAK WHERE NoCustomer = " + n1 + " AND Project IN(" + Act.ProjectListSql + ")");

			//Hati-hati, n1 dan n2 terbalik di susunan stored procedure
			Db.Execute("EXEC spCustomerGabung "
				+ " " + n2
				+ "," + n1
				);

			string KetHapus = "Data dipindahkan ke customer : " + NamaSimpan
				+ "<br>"
				+ Cf.LogList(kontrakHapus, "DAFTAR KONTRAK")
				+ "<br>"
				+ Cf.LogCapture(rsHapus)
				;

			Db.Execute("EXEC spLogCustomer "
				+ " 'DELETE'"
				+ ",'" + Act.UserID + "'"
				+ ",'" + Act.IP + "'"
				+ ",'" + KetHapus + "'"
				+ ",'" + n1.ToString().PadLeft(5,'0') + "'"
				);

            DataTable kontrakSimpan = Db.Rs("SELECT NoKontrak FROM MS_KONTRAK WHERE NoCustomer = " + n2 + " AND Project IN(" + Act.ProjectListSql + ")");
            string Ket = "Gabungan data dari customer : " + NamaHapus
                + "<br>"
                + Cf.LogList(kontrakSimpan, "DAFTAR KONTRAK")
                ;
            Db.Execute("EXEC spLogCustomer "
                + " 'GABUNG'"
                + ",'" + Act.UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'" + Ket + "'"
                + ",'" + n2.ToString().PadLeft(5, '0') + "'"
                );

			Response.Redirect("CustomerGabung.aspx?done="+n2);
		}

        private void Fill(Table rpt, int myID)
        {
            string strSql = "SELECT "
                + " NoKontrak"
                + ",TglKontrak"
                + ",NoUnit"
                + ",NilaiKontrak"
                + " FROM MS_KONTRAK "
                + " WHERE NoCustomer = " + myID
                + " AND Project IN(" + Act.ProjectListSql + ")"
                + " ORDER BY NoKontrak";

            DataTable rs = Db.Rs(strSql);
            Rpt.NoData(rpt, rs, "Customer tidak memiliki histori kontrak.");

			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				TableRow r = new TableRow();
				TableCell c;

				c = new TableCell();
				c.Text = "<a href=\"javascript:popEditKontrak('"+rs.Rows[i]["NoKontrak"]+"')\">"
					+ rs.Rows[i]["NoKontrak"]
					+ "</a>";
                c.Wrap = false;
				r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Day(rs.Rows[i]["TglKontrak"]);
                c.Wrap = false;
                r.Cells.Add(c);

				c = new TableCell();
				c.Text = rs.Rows[i]["NoUnit"].ToString();
                c.Wrap = false;
                r.Cells.Add(c);

				c = new TableCell();
				c.Text = Cf.Num(rs.Rows[i]["NilaiKontrak"]);
				c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

				Rpt.Border(r);
				rpt.Rows.Add(r);
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

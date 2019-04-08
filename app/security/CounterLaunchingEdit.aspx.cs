using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Linq;
using System.Collections.Generic;
namespace ISC064.SECURITY
{
	public partial class EditUser : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();
			
			//if(!Act.Sec("ED:"+Request.PhysicalPath))
			//{
			//	ok.Enabled = false;
			//	save.Enabled = false;
			//}

			if(!Page.IsPostBack)
			{
                var rs = from DataRow r in Db.Rs("Select * from Username where Status='A'").Rows
                         select new ListItem
                         {
                             Value = r["UserID"].ToString(),
                             Text = r["Nama"].ToString()
                         };
                username.Items.AddRange(rs.ToArray());
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
			//aKey.HRef = "javascript:openModal('EditKey.aspx?UserID=" + UserID + "','350','150')";
			btndel.Attributes["onclick"] = "location.href='CounterLaunchingDel.aspx?id="+MyID+"'";
			
			string strSql = "SELECT * FROM REF_ADMIN_LAUNCHING WHERE ID = '" + MyID + "'";
            //DataTable rs = Db.Rs(strSql);
            var rs = from DataRow r in Db.Rs(strSql).Rows
                     select new {
                         Nama = r["Nama"].ToString(),
                         UserID = r["UserID"].ToString(),
                         Nomor = r["Nomor"].ToString()
                     }; 

            if (!rs.Any())
				Response.Redirect("/CustomError/Deleted.html");
			else
			{
                nama.Text = rs.First().Nama;
                username.SelectedValue = rs.First().UserID;
                nomor.Text = rs.First().Nomor;
            }
		}

		private bool valid()
		{
			string s = "";
			bool x = true;

            if (Cf.isEmpty(nama))
            {
                x = false;
                if (s == "") s = nama.ID;
                namac.Text = "Kosong";
            }
            else
                namac.Text = "";
            if (!Cf.isInt(nomor.Text))
            {
                x = false;
                if (s == "") s = nomor.ID;
                nomorc.Text = "angka";
            }
            else
                nomorc.Text = "";

            if (!x)
				Js.Alert(
					this
					, "Input Tidak Valid.\\n\\n"
					+ "Aturan Proses :\\n"
                    + "1. Nama tidak boleh kosong.\\n"
                    + "2. Nomor harus angka.\\n"
                    , "document.getElementById('" +s+"').focus();"
					+ "document.getElementById('"+s+"').select();"
					);

			return x;
		}

		private bool Save()
		{
			if(valid())
			{
                Db.Execute("UPdate REF_ADMIN_LAUNCHING set USERID='"+ Cf.Pk(username.SelectedValue) 
                    +"',Nama='"+ Cf.Pk(nama.Text) 
                    +"',Nomor='"+ nomor.Text 
                    +"' where id='"+ MyID +"'");

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
			if(Save()) Response.Redirect("CounterLaunchingEdit.aspx?id="+ MyID + "&done=1");
		}

		private string MyID
		{
			get
			{
				return Cf.Pk(Request.QueryString["id"]);
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

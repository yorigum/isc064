using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.SECURITY
{
	public partial class DelUser : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			if(!Page.IsPostBack)
			{
                Js.Focus(this, ket);
				Js.Confirm(delbtn,
					"Apakah anda ingin menghapus username : "+UserID+" ?\\n"
					+ "Perhatian bahwa data akan dihapus secara PERMANEN."
					);
			}
		}

		protected void delbtn_Click(object sender, System.EventArgs e)
		{
			DataTable rs = Db.Rs(
				"SELECT * FROM USERNAME WHERE UserID = '" + UserID + "'");

			if(rs.Rows.Count==0)
				Response.Redirect("/CustomError/Deleted.html");

			string Ket = "***Alasan Delete :<br>" + Cf.Str(ket.Text)
				+ "<br><br>***Data Sebelum Delete :<br>"
				+ Cf.LogCapture(rs);

			Db.Execute("EXEC spUserDel '"+UserID+"'");

			int c = Db.SingleInteger(
				"SELECT COUNT(*) FROM USERNAME WHERE UserID = '" + UserID + "'");

			if(c==0)
			{
				//Log
				Db.Execute("EXEC spLogUsername "
					+ " 'DEL'"
					+ ",'" + Act.UserID + "'"
					+ ",'" + Act.IP + "'"
					+ ",'" + Ket + "'"
					+ ",'" + UserID + "'"
					);

				Js.Close(this);
			}
			else
			{
				//Tidak bisa dihapus
				frm.Visible = false;
				nodel.Visible = true;
			}
		}

        protected void cancelbtn_Click(object sender, System.EventArgs e)
        {
            Response.Redirect("EditUser.aspx?UserID=" + UserID);
        }

        private string UserID
		{
			get
			{
				return Cf.Pk(Request.QueryString["UserID"]);
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

using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.SECURITY
{
	public partial class CounterLaunchingDel : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			if(!Page.IsPostBack)
			{
                Js.Focus(this, ket);
				Js.Confirm(delbtn,
					"Apakah anda ingin menghapus counter ini ?\\n"
					+ "Perhatian bahwa data akan dihapus secara PERMANEN."
					);
			}
		}

		protected void delbtn_Click(object sender, System.EventArgs e)
		{
			DataTable rs = Db.Rs("SELECT * FROM REF_ADMIN_LAUNCHING WHERE id = '" + MyID + "'");

			if(rs.Rows.Count==0)
				Response.Redirect("/CustomError/Deleted.html");


			Db.Execute("DELETE REF_ADMIN_LAUNCHING WHERE ID= '" + MyID+"'");

			int c = Db.SingleInteger(
                "SELECT COUNT(*) FROM REF_ADMIN_LAUNCHING WHERE ID = '" + MyID + "'");

			if(c==0)
			{
				
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
            Response.Redirect("CounterLaunchingEdit.aspx?id=" + MyID);
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

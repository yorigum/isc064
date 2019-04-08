using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
namespace ISC064.ADMINJUAL
{
	public partial class PetaHapus : System.Web.UI.Page
	{

        protected string Idx { get { return Cf.Pk(Request.QueryString["id"]); } }
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();
            var rs = Db.Rs("Select * from MS_siteplan where id='" + Idx + "'");
            if(rs.Rows.Count==0)
                Response.Redirect("/CustomError/Deleted.html");

            string FileName1 = Request.PhysicalApplicationPath.ToLower() + @"\marketingjual\FP\Base\" + Idx + ".jpg";
            string FileName2 = Request.PhysicalApplicationPath.ToLower() + @"\marketingjual\FP\Base\" + Idx + ".png";

            if (File.Exists(FileName1)) File.Delete(FileName1);
            if (File.Exists(FileName2)) File.Delete(FileName2);

            Db.Execute("Delete MS_siteplan where ID='"+ Idx +"'");

			//Logfile
			string Ket = "---DELETE FLOOR PLAN---<br>"
                        + rs.Rows[0]["Nama"]
                        ;

			Db.Execute("EXEC spLogUnit"
				+ " 'FP'"
				+ ",'" + Act.UserID + "'"
				+ ",'" + Act.IP + "'"
				+ ",'" + Ket + "'"
				+ ",''"
				);
            Js.Close(this);
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

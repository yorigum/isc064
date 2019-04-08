using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Diagnostics;

namespace ISC064.NUP
{
	public partial class PrintNUP1 : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{

			SetTemplate();
			
			if(!Page.IsPostBack)
			{
				Fill();
			}
		}

		private void SetTemplate()
		{
			PrintNUPTemplate2 uc = (PrintNUPTemplate2) Page.LoadControl("PrintNUPTemplate2.ascx"); 
			uc.NoNUP = NoNUP;
            uc.Project = Project;
            uc.Tipe = Tipe;
            list.Controls.Add(uc);
		}

		private void Fill()
		{
            //string strSql = "SELECT PrintSP FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'";
            //DataTable rs = Db.Rs(strSql);
            //if(rs.Rows.Count==0)
            //	Response.Redirect("/CustomError/NoPrint.html");
            Tampil();
		}

		private void Tampil()
		{
			list.Visible = true;
		}

		private string NoNUP
		{
			get
			{
				return Cf.Pk(Request.QueryString["NoNUP"]);
			}
		}
        private string Project
        {
            get
            {
                return Cf.Pk(Request.QueryString["project"]);
            }
        }
        private string Tipe
        {
            get
            {
                return Cf.Pk(Request.QueryString["tipe"]);
            }
        }
    }
}

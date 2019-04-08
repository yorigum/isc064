using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Diagnostics;

namespace ISC064.MARKETINGJUAL
{
	public partial class PrintGimmick1 : System.Web.UI.Page
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
            PrintGimmickTemplate uc = (PrintGimmickTemplate) Page.LoadControl("PrintGimmickTemplate.ascx"); 
			uc.NoKontrak = NoKontrak;
            uc.Project = Project;
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

		private string NoKontrak
		{
			get
			{
				return Cf.Pk(Request.QueryString["NoKontrak"]);
			}
		}
        private string Project
        {
            get
            {
                return Cf.Pk(Request.QueryString["project"]);
            }
        }
    }
}

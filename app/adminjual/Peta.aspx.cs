
using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;
namespace ISC064.ADMINJUAL
{
	public partial class Migrated_Peta : Peta
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

            if (!Page.IsPostBack)
            {
                Act.ProjectList(project);
                Fill();
            }
			
			if(Request.QueryString["done"]!=null)
			{
				feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
					+ "Proses Berhasil...";
			}
		}

		private void Fill()
		{

            var rs = Db.Rs("Select * from ms_siteplan where parentid=0 AND Project = '" + project.SelectedValue + "'");

            var ul = new HtmlGenericControl("ul");
            ul.Attributes.Add("class","listitem");
            HtmlGenericControl li;
            foreach (DataRow r in rs.Rows)
            {
                li = new HtmlGenericControl("li");
                li.InnerHtml = "<a href='javascript:Edit("+ r["id"] +");' class='link'>" + r["Nama"] + "</a>";
                ul.Controls.Add(li);
                list.Controls.Add(ul);
                Loop(r["ID"],ul);
                if ((bool)r["isparent"])
                {
                    var ul2 = new HtmlGenericControl("ul");
                    li = new HtmlGenericControl("li");
                    li.InnerHtml = "<a href='javascript:New(" + r["id"] + ");'> [+]</a>";
                    ul2.Controls.Add(li);
                    ul.Controls.Add(ul2);
                }
            }

            li = new HtmlGenericControl("li");
            li.InnerHtml = "<a href='javascript:New(0);'> [+]</a>";
            ul.Controls.Add(li);
            list.Controls.Add(ul);
        }
      
        void Loop(object ParentID,Control Ctrl)
        {
            var ul = new HtmlGenericControl("ul");
            ul.Attributes.Add("class", "listitem");
            HtmlGenericControl li;
            var rs = Db.Rs("Select * from ms_siteplan where parentid='"+ ParentID + "'");
            foreach (DataRow r in rs.Rows)
            {
                li = new HtmlGenericControl("li");
                li.InnerHtml = "<a href='javascript:Edit(" + r["id"] + ");' class='link'>" + r["Nama"] + "</a>";
                ul.Controls.Add(li);
                Ctrl.Controls.Add(ul);
                Loop(r["ID"], ul);
                if ((bool)r["isparent"])
                {
                    var ul2 = new HtmlGenericControl("ul");
                    li = new HtmlGenericControl("li");
                    li.InnerHtml = "<a href='javascript:New(" + r["id"] + ");'> [+]</a>";
                    ul2.Controls.Add(li);
                    ul.Controls.Add(ul2);
                }
            }
            
        }

        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fill();
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

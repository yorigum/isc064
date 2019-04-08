using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
namespace ISC064.MARKETINGJUAL
{
    public partial class UnitPetaList : System.Web.UI.Page
	{
	
        protected string ParentID
        {
            get
            {
                if(Request.QueryString["ParentID"] == null)
                {
                    return "0";
                }
                  
                
                if (Cf.isInt(Request.QueryString["ParentID"]))
                {
                    return Request.QueryString["ParentID"];
                }
                return "0";
            }
        }
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			if(!Page.IsPostBack)
			{
				Display();

                string utama = Request.PhysicalApplicationPath.ToLower().Replace("admin", "marketing")
                    + @"FP\PETA-UTAMA.jpg"; //file dasar
                if (File.Exists(utama))
                {
                    HtmlImage img = new HtmlImage();
                    img.Src = @"\marketingjual\FP\PETA-UTAMA.jpg";
                    img.Attributes.Add("class","img");
                    imgpanel.Controls.Add(img);
                }
            }
		}

		private void Display()
		{
            DataTable rs = Db.Rs("Select ID,Nama,isParent,PathGambarDasar From ms_siteplan where ParentID ='" + ParentID +"'");
            for (int i = 0; i < rs.Rows.Count; i++) {
                TableRow r = new TableRow();
                TableCell c = new TableCell();
                c.Attributes.Add("style","text-decoration:none;font-size:24px;");
                if ((bool)rs.Rows[i][2])
                {
                    c.Text = "<a href='UnitPetaList.aspx?ParentID=" + rs.Rows[i][0] + "'>" + rs.Rows[i][1] + "</a>";
                }else
                {
                    if (File.Exists(Server.MapPath(".").ToLower().Replace("marketingjual", "") + rs.Rows[i][3].ToString()))
                    {
                        c.Attributes["style"] = "text-decoration:none;font-size:24px;";
                        c.Text = "<a href='UnitPetaDetil.aspx?ID=" + rs.Rows[i][0] + "'>" + rs.Rows[i][1] + "</a>";
                    }else
                    {
                        c.Text = "<a href='#'>" + rs.Rows[i][1] + "</a>";
                    }
                }
                r.Cells.Add(c);
                tbList.Rows.Add(r);
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

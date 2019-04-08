
using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;
namespace ISC064.ADMINJUAL
{
	public partial class PetaDaftar : Peta
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

            if (!Page.IsPostBack)
            {
                Act.ProjectList(project);
            }

            Fill();

			if(Request.QueryString["done"]!=null)
			{
				feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
					+ "Proses Berhasil...";
			}
		}
        protected int ParentID{
            get
            {
                return Convert.ToInt32(Request.QueryString["ParentID"]);
            }
        }
		private void Fill()
		{
            //int nomor = Db.SingleInteger("SELECT ParentID FROM MS_SITEPLAN WHERE ParentID = '" + ParentID + "'");
            if (tipe.SelectedIndex == 0 && ParentID == 0)
            {
                proj.Visible = true;
                trpetadasar.Visible = false;
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

        protected void tipe_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int nomor = Db.SingleInteger("SELECT ParentID FROM MS_SITEPLAN WHERE ParentID = '" + ParentID + "'");
            if (tipe.SelectedIndex == 1 && ParentID > 0)
            {
                trpetadasar.Visible = true;
                proj.Visible = false;
                //trpetatransparent.Visible = true;
            }
            else if(tipe.SelectedIndex == 0 && ParentID > 0)
            {
                proj.Visible = false;
                trpetadasar.Visible = false;
            }
            else if (tipe.SelectedIndex == 1 && ParentID == 0)
            {
                proj.Visible = false;
                trpetadasar.Visible = false;
            }
            else if (tipe.SelectedIndex == 0 && ParentID == 0)
            {
                proj.Visible = true;
                trpetadasar.Visible = false;
            }
            else
            {
                proj.Visible = true;
                trpetadasar.Visible = false;
                //trpetatransparent.Visible = false;
            }
        }

        protected void save_Click(object sender, EventArgs e)
        {
            if (Valid)
            {
                byte aaa = 0;
                if (tipe.SelectedIndex == 0)
                {
                    aaa = 1;
                }
                Db.Execute("EXEC spSitePlanDaftar"
                    + " '" + ParentID + "'"
                    + ",'" + namapeta.Text + "'"
                    + ", " + aaa
                    + ",''"
                    + ",''"
                    );

                //ExecMkt.spSitePlanDaftar(ParentID, namapeta.Text,tipe.SelectedIndex == 0, "", "");
                var ID = Db.SingleInteger("Select ISNULL(Max(ID),0) from MS_Siteplan");

                Db.Execute("UPDATE MS_SITEPLAN SET Project='" + project.SelectedValue + "' WHERE ID = '" + ID + "'");

                var Project = Db.SingleString("Select Project from MS_Siteplan WHERE ParentID = '" + ParentID + "'");

                string NamaPeta = "PETA_" + ID;

                string pdasar = Request.PhysicalApplicationPath.ToLower().Replace("admin", "marketing")
                    + "FP\\Base\\" + NamaPeta + ".jpg"; //file dasar
                string ptransparent = Request.PhysicalApplicationPath.ToLower().Replace("admin", "marketing")
                    + "FP\\Base\\" + NamaPeta + ".png"; //file dasar
                string pstatus = Request.PhysicalApplicationPath.ToLower().Replace("admin", "marketing")
                    + "FP\\" + NamaPeta + ".jpg"; //file status

                string urlpdasar = @"\marketingjual\FP\Base\" + NamaPeta + ".jpg"; //file dasar
                string urlptransparent = @"\marketingjual\FP\Base\" + NamaPeta + ".png"; //file dasar
                string urlpstatus = @"\marketingjual\FP\" + NamaPeta + ".jpg"; //file status

                if (file1.HasFile)
                {
                    
                    Dfc.UploadFilePeta(".jpg", pdasar, file1);
                    Dfc.CopyFile(pdasar, pstatus);
                    Db.Execute("update ms_siteplan set PathGambarDasar='" + urlpdasar + "' where ID='" + ID + "'");
                    Db.Execute("update ms_siteplan set Project='" + Project + "' where ID='" + ID + "'");
                }
                //if (file2.HasFile)
                //{
                //    Dfc.UploadFile(".png", ptransparent, file2);
                //    Db.Execute("update ms_siteplan set PathGambarTransparent='" + urlptransparent + "' where ID='" + ID + "'");
                //}

               
                Js.Close(this);
            }
        }

        bool Valid
        {
            get
            {
                bool x = true;
                Cf.ClrError(namapeta);
                if (Cf.isEmpty(namapeta))
                {
                    x = false;
                    Cf.MarkError(namapeta);
                }
                if (file1.HasFile)
                {
                    if (file1.FileName.EndsWith(".jpg") || file1.FileName.EndsWith(".jpeg"))
                    {

                    }else
                    {
                        x = false;
                        file1c.Text = "file harus berextensi *.jpg";
                    }
                }
                //if (file2.HasFile)
                //{
                //    if (!file2.FileName.EndsWith(".png"))
                //    {
                //        x = false;
                //        file2c.Text = "file harus berextensi *.png";
                //    }
                //}
                if (!x) Js.Alert(this,"Data tidak valid","");
                return x;
            }
        }
    }
}

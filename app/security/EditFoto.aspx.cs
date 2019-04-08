using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.SECURITY
{
	public partial class EditFoto : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();
			
			if(!Act.Sec("ED:"+Request.PhysicalPath))
			{
				ok.Enabled = false;
			}

            foto.Src = "..//" + ISC064.Act.Foto(UserID);
        }

        protected void ok_Click(object sender, System.EventArgs e)
        {
            if (filevalid())
            {
                if (file.PostedFile.FileName.Length != 0)
                {
                    DataTable rsBef = Db.Rs("SELECT "
                                + " Foto"
                                + " FROM USERNAME WHERE UserID = '" + UserID + "'");

                    string path = Request.PhysicalApplicationPath + "Foto\\" + UserID + ".png";
                    string save = "security\\Foto\\" + UserID + ".png";
                    Dfc.UploadFile(".jpg", path, file);
                    Db.Execute("UPDATE USERNAME SET Foto ='" + save + "' WHERE UserID = '" + UserID + "'");

                    DataTable rsAft = Db.Rs("SELECT "
                                + " Foto"
                                + " FROM USERNAME WHERE UserID = '" + UserID + "'");

                    string Ket = Cf.LogCompare(rsBef, rsAft);

                    Db.Execute("EXEC spLogUsername "
                        + " 'EDU'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + Ket + "'"
                        + ",'" + UserID + "'"
                        );

                }
                Js.Close(this);
            }

        }
        protected bool filevalid()
        {
            bool x = true;
            string s = "";

            if (file.PostedFile.FileName.Length != 0
                && !file.PostedFile.FileName.EndsWith(".jpg"))
            {
                x = false;

                if (s == "")
                    s = file.ID;
            }

            if (!x)
            {
                Js.Alert(
                    this
                    , "Proses Upload Gagal.\\n"
                    + "File yang boleh di-upload adalah file dengan extension .jpg saja."
                    , "document.getElementById('" + s + "').focus();"
                    );
            }

            return x;
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

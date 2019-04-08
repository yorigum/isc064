using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064
{
	public partial class GantiFoto : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();
            //foto.Src = Act.Foto(Act.UserID);            
		}

        protected void ok_Click(object sender, EventArgs e)
        {
            if (filevalid())
            {
                if (file.PostedFile.FileName.Length != 0)
                {
                    string path = "D:\\ISC\\ISC064\\app\\security\\Foto\\" + Act.UserID + ".png";
                    string save = "security\\Foto\\" + Act.UserID + ".png";
                    Dfc.UploadFile(".jpg", path, file);
                    Db.Execute("UPDATE USERNAME SET Foto ='" + save + "' WHERE UserID = '" + Act.UserID + "'");
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

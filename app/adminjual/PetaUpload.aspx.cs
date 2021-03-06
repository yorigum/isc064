using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.ADMINJUAL
{
    public partial class PetaUpload : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            Js.Confirm(this, "Lanjutkan proses upload peta dasar?");

            Js.Focus(this, namapeta);
            feed.Text = "";
        }

        protected void upload_Click(object sender, System.EventArgs e)
        {
            namapeta.Text = Cf.FileSafe(namapeta.Text);

            if (namapeta.Text == ""
                || !file1.PostedFile.FileName.EndsWith(".jpg"))
            {
                Js.Alert(
                    this
                    , "Proses Upload Gagal.\\n"
                    + "1. Nama Peta tidak boleh kosong.\\n"
                    + "2. Extension file peta utama hanya boleh JPG.\\n"
                    //+ "3. Extension file peta nomor hanya boleh PNG\\n"
                    , "document.getElementById('namapeta').focus();"
                    + "document.getElementById('namapeta').select();"
                    );
            }
            else
            {
                string NamaPeta = Cf.FileSafe(namapeta.Text);

                string pdasar = Request.PhysicalApplicationPath.Replace("admin", "marketing")
                    + "FP\\Base\\" + Tower + "\\" + NamaPeta + ".jpg"; //file dasar
                string pstatus = Request.PhysicalApplicationPath.Replace("admin", "marketing")
                    + "FP\\" + NamaPeta + ".jpg"; //file status

                //string pnomor = Request.PhysicalApplicationPath.Replace("admin", "marketing")
                //    + "FP\\" + NamaPeta + ".png"; //file nomor

                Dfc.UploadFile(".jpg", pdasar, file1);
                //Dfc.UploadFile(".png", pnomor, file2);

                Dfc.CopyFile(pdasar, pstatus);//Copy dari base ke status

                //Logfile
                string Ket = "***UPLOAD FLOOR PLAN : " + NamaPeta;

                Db.Execute("EXEC spLogUnit"
                    + " 'FP'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + Ket + "'"
                    + ",''"
                    );

                feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                    + "Upload Berhasil...";
            }
        }
        private string Tower
        {
            get
            {
                return Cf.Pk(Request.QueryString["Tower"]);
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

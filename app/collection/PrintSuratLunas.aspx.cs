using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Diagnostics;

namespace ISC064.COLLECTION
{
    public partial class PrintSuratLunas : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            SetTemplate();

            if (!Page.IsPostBack)
            {
                Fill();
            }

            if (reprint.Visible)
                RegisterOnSubmitStatement(
                    "md5Script"
                    , "document.getElementById('pass').value=hex_md5(document.getElementById('pass').value);"
                    );
        }

        private void SetTemplate()
        {
            PrintSuratLunasTemplate uc = (PrintSuratLunasTemplate)Page.LoadControl("PrintSuratLunasTemplate.ascx");
            uc.NoSKL = NoSKL;
            uc.Project = Project;
            list.Controls.Add(uc);
        }

        private void Fill()
        {
            cancel.Attributes["onclick"] = "location.href='SKLEdit.aspx?NoSKL=" + NoSKL + "'";
            cancel2.Attributes["onclick"] = "location.href='SKLEdit.aspx?NoSKL=" + NoSKL + "'";

            string strSql = "SELECT PrintSKL FROM MS_SKL WHERE NoSKL = '" + NoSKL + "'";
            DataTable rs = Db.Rs(strSql);
            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/NoPrint.html");
            else
            {
                count.Text = rs.Rows[0]["PrintSKL"].ToString();
                if ((int)rs.Rows[0]["PrintSKL"] == 0)
                {
                    Tampil(); //langsung tampil
                    ConvertPdf();
                    Response.Redirect(Param.PathLinkFilePDFCollection + NoSKL.Replace("/", "_").Replace("\\", "_") + Project + "_SKL.pdf");
                }
                else
                {
                    //mekanisme reprint
                    list.Visible = false;
                    reprint.Visible = true;
                    Js.Focus(this, username);

                    if (Session["SalahPass"] == null)
                        Session["SalahPass"] = "0"; //Hitung password salah berapa kali
                    else
                    {
                        if (Session["SalahPass"].ToString() != "0")
                            salah.Text = Session["SalahPass"] + "x salah";
                    }
                }
            }
        }

        private void Tampil()
        {
            list.Visible = true;
            reprint.Visible = false;
            Js.AutoPrint(this);

            //increment
            Db.Execute("UPDATE MS_SKL SET PrintSKL = PrintSKL + 1 WHERE NoSKL = '" + NoSKL + "'");

            //Logfile
            DataTable rs = Db.Rs("SELECT "
                + " CONVERT(varchar, TglSKL, 106) AS [Tanggal]"
                + ",NoSKL"
                + ",Ref AS [Ref.]"
                + ",NoSKLManual"
                + ",Used AS [No.YgDigunakan]"
                + " FROM MS_SKL WHERE NoSKL = '" + NoSKL + "'");

            Db.Execute("EXEC spLogSKL"
                + " '"+DateTime.Now+"'"
                + " ,'P-SKL'"
                + ",'" + Act.UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'" + NoSKL.ToString() + "'"
                + ",'" + Cf.LogCapture(rs) + "'"
                );
        }

        private void ConvertPdf()
        {
            Process p = new System.Diagnostics.Process();

            string myHtml = "http://" + Request.Url.Host + ":" + Request.Url.Port + "/collection/PrintSuratLunas1.aspx?NoSKL=" + NoSKL + "&project=" + Project;

            string save = Param.PathFilePDFCollection + NoSKL.Replace("/", "_").Replace("\\", "_") + Project + "_SKL.pdf";
            string link = Param.PathLinkFilePDFCollection + NoSKL.Replace("/", "_").Replace("\\", "_") + Project + ".pdf";

            p.StartInfo.Arguments = "--orientation portrait --page-width 8.5in --page-height 11in --margin-left 2cm --margin-right 2cm --margin-top 1.25cm --margin-bottom 0 " + myHtml + " " + save;
            p.StartInfo.FileName = Mi.PathWkhtmlPDFReport;
            p.Start();
            p.WaitForExit(60000);
        }


        protected void btn_Click(object sender, System.EventArgs e)
        {
            string pid = "RP:" + Request.PhysicalPath;
            string Username = Cf.Str(username.Text);
            int c = Db.SingleInteger(
                "SELECT COUNT(*) FROM " + Mi.DbPrefix + "SECURITY..USERNAME "
                + " WHERE UserID = '" + Username + "'"
                + " AND Pass = '" + pass.Text + "'"
                + " AND Status = 'A'"
                + " AND "
                + " (" //cek sec. level untuk reprint
                + "	SecLevel IN "
                + "		(SELECT Kode FROM " + Mi.DbPrefix + "SECURITY..PAGESEC WHERE Halaman = '" + pid + "')"
                + "	OR UserID IN "
                + "		(SELECT UserID FROM " + Mi.DbPrefix + "SECURITY..PAGEDENY WHERE Halaman = '" + pid + "' AND Sifat=0)"
                + " )"
                );

            if (c != 0)
                Valid(Username);
            else
                Invalid();
        }

        private void Valid(string Username)
        {
            Session["SalahPass"] = null;

            //Logfile otorisasi
            DataTable rs = Db.Rs("SELECT "
                + " CONVERT(varchar, TglSKL, 106) AS [Tanggal]"
                + ",NoSKL"
                + ",Ref AS [Ref.]"
                + ",NoSKLManual"
                + ",Used AS [No.YgDigunakan]"
                + " FROM MS_SKL WHERE NoSKL = '" + NoSKL + "'");

            Db.Execute("EXEC spLogSKL"
                + " '" + DateTime.Now + "'"
                + " ,'R-SKL'"
                + ",'" + Act.UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'" + NoSKL.ToString() + "'"
                + ",'" + Cf.LogCapture(rs) + "'"
                );
            Tampil();
            string file = Param.PathFilePDFCollection + NoSKL.Replace("/", "_").Replace("\\", "_") + Project + "_SKL.pdf";
            bool exist = System.IO.File.Exists(file);
            if (exist)
            {
                System.IO.File.Delete(file);
            }
            ConvertPdf();
            Response.Redirect(Param.PathLinkFilePDFCollection + NoSKL.Replace("/", "_").Replace("\\", "_") + Project + "_SKL.pdf");
        }

        private void Invalid()
        {
            //3x salah password akan mengakibatkan sign-out otomatis;
            int x = Convert.ToInt32(Session["SalahPass"]) + 1;
            salah.Text = x.ToString() + "x salah";
            Session["SalahPass"] = x;

            if (x >= 3)
                Response.Redirect("SignOut.aspx?pass=1");

            Js.Alert(
                this
                , "Otorisasi Gagal " + x + "x.\\n"
                + "Username akan Sign-Out otomatis apabila salah 3x."
                , "document.getElementById('pass').focus();"
                );
        }

        private string NoSKL
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoSKL"]);
            }
        }

        private string Project
        {
            get
            {
                return Cf.Pk(Request.QueryString["project"]);
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

using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Diagnostics;
using System.Web;

namespace ISC064.MARKETINGJUAL
{
    public partial class PrintGimmick : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            //req panahome -- lunas booking fee
            decimal TagihanBF = Db.SingleDecimal("select NilaiTagihan from MS_TAGIHAN where NoKontrak = '" + NoKontrak + "' and Tipe = 'BF'");
            decimal PelunasanBF = Db.SingleDecimal("select NilaiPelunasan from MS_PELUNASAN where NoKontrak = '" + NoKontrak + "' and SudahCair != 0 and NoTagihan IN (select NoUrut from MS_TAGIHAN where Tipe = 'BF')");

            if (PelunasanBF < TagihanBF)
            {
                nokontrakbf.Text = NoKontrak.ToString();

                reprint.Visible = false;
                alertbf.Visible = true;
            }
            else
            {
                alertbf.Visible = false;
                SetTemplate();

                if (!Page.IsPostBack)
                {
                    Fill();
                }

                if (reprint.Visible)
                    ClientScript.RegisterOnSubmitStatement(
                        GetType()
                        , "md5Script"
                        , "document.getElementById('pass').value=hex_md5(document.getElementById('pass').value);"
                        );
            }
        }

        private void SetTemplate()
        {
            PrintGimmickTemplate uc = (PrintGimmickTemplate)Page.LoadControl("PrintGimmickTemplate.ascx");
            uc.NoKontrak = NoKontrak;
            uc.Project = Project;
            list.Controls.Add(uc);
        }

        private void SetTemplate2()
        {
            PrintGimmickTemplate2 uc = (PrintGimmickTemplate2)Page.LoadControl("PrintGimmickTemplate2.ascx");
            uc.NoKontrak = NoKontrak;
            uc.Project = Project;
            list.Controls.Add(uc);
        }

        private void Fill()
        {
            cancel.Attributes["onclick"] = "location.href='KontrakGimmick.aspx?NoKontrak=" + NoKontrak + "'";
            cancel2.Attributes["onclick"] = "location.href='KontrakGimmick.aspx?NoKontrak=" + NoKontrak + "'";

            string strSql = "SELECT TOP 1 PrintGimmick FROM MS_KONTRAK_GIMMICK WHERE NoKontrak = '" + NoKontrak + "'";
            DataTable rs = Db.Rs(strSql);
            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/NoPrint.html");
            else
            {
                count.Text = rs.Rows[0]["PrintGimmick"].ToString();
                if ((int)rs.Rows[0]["PrintGimmick"] == 0)
                {
                    Tampil(); //langsung tampil
                    ConvertPdf();
                    Response.Redirect(Param.PathLinkFilePDFMarketingJual + NoKontrak.Replace("/", "_").Replace("\\", "_") + Project + "_Gimmick.pdf");
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
            Db.Execute("UPDATE MS_KONTRAK_GIMMICK SET PrintGimmick = PrintGimmick + 1 WHERE NoKontrak = '" + NoKontrak + "'");

            //Logfile
            DataTable rs = Db.Rs("SELECT "
                + " a.NoKontrak AS [No. Kontrak]"
                + ",b.NoUnit AS [Unit]"
                + ",(Select Nama from MS_CUSTOMER c where c.NoCustomer = b.NoCustomer) AS Customer"
                + ",a.PrintGimmick AS [Print Gimmick Counter]"
                + " FROM MS_KONTRAK_GIMMICK a INNER JOIN MS_KONTRAK b"
                + " ON a.NoKontrak = b.NoKontrak"
                + " WHERE a.NoKontrak = '" + NoKontrak + "'");

            Db.Execute("EXEC spLogKontrak"
                + " 'P-GIM'"
                + ",'" + Act.UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'" + Cf.LogCapture(rs) + "'"
                + ",'" + NoKontrak.ToString() + "'"
                );
            decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
            string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
            Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

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
        private void ConvertPdf()
        {
            Process p = new System.Diagnostics.Process();

            string myHtml = "http://" + Request.Url.Host + ":" + Request.Url.Port + "/marketingjual/PrintGimmick1.aspx?NoKontrak=" + NoKontrak + "&project=" + Project;
            string save = Param.PathFilePDFMarketingJual + NoKontrak.Replace("/", "_").Replace("\\", "_") + Project + "_SP.pdf";
            string link = Param.PathLinkFilePDFMarketingJual + NoKontrak.Replace("/", "_").Replace("\\", "_") + Project + ".pdf";

            p.StartInfo.Arguments = "--orientation portrait --page-width 8.5in --page-height 11in --margin-left 0 --margin-right 0 --margin-top 1.25cm --margin-bottom 0 " + myHtml + " " + save;
            p.StartInfo.FileName = Mi.PathWkhtmlPDFReport;
            p.Start();
            p.WaitForExit(60000);
        }
        private void Valid(string Username)
        {
            Session["SalahPass"] = null;

            //Logfile otorisasi
            DataTable rs = Db.Rs("SELECT "
                + " a.NoKontrak AS [No. Kontrak]"
                + ",b.NoUnit AS [Unit]"
                + ",(Select Nama from MS_CUSTOMER c where c.NoCustomer = b.NoCustomer) AS Customer"
                + " FROM MS_KONTRAK_GIMMICK a INNER JOIN MS_KONTRAK b"
                + " ON a.NoKontrak = b.NoKontrak"
                + " WHERE a.NoKontrak = '" + NoKontrak + "'");

            Db.Execute("EXEC spLogKontrak"
                + " 'R-GIM'"
                + ",'" + Username + "'"
                + ",'" + Act.IP + "'"
                + ",'" + Cf.LogCapture(rs) + "'"
                + ",'" + NoKontrak.ToString() + "'"
                );

            decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
            string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
            Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

            Tampil();
            string file = Param.PathFilePDFMarketingJual + NoKontrak.Replace("/", "_").Replace("\\", "_") + Project + "_Gimmick.pdf";
            bool exist = System.IO.File.Exists(file);
            if (exist)
            {
                System.IO.File.Delete(file);
            }
            ConvertPdf();
            Response.Redirect("http://" + Request.Url.Host + ":" + Request.Url.Port
                + Param.PathLinkFilePDFMarketingJual + NoKontrak.Replace("/", "_").Replace("\\", "_") + Project + "_Gimmick.pdf");
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

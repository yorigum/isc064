using System;
using System.Data;
using System.Diagnostics;

namespace ISC064.MARKETINGJUAL
{
    public partial class PrintSP : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            //req panahome -- lunas booking fee
            decimal TagihanBF = 0;// Db.SingleDecimal("select ISNULL(SUM(NilaiTagihan), 0) from MS_TAGIHAN where NoKontrak = '" + NoKontrak + "' and Tipe = 'BF'");
            decimal PelunasanBF = 0;// Db.SingleDecimal("select ISNULL(SUM(NilaiPelunasan),0) from MS_PELUNASAN where NoKontrak = '" + NoKontrak + "' and SudahCair != 0 and NoTagihan IN (select NoUrut from MS_TAGIHAN where Tipe = 'BF')");

            if (PelunasanBF != TagihanBF)
            {
                nokontrakbf.Text = NoKontrak.ToString();

                reprint.Visible = false;
                alertbf.Visible = true;
            }
            else
            {
                reprint.Visible = false;
                alertbf.Visible = false;
                //string Lokasi = Db.SingleString("SELECT Lokasi FROM MS_KONTRAK WHERE NoKontrak='" + NoKontrak + "'");
                //if (Lokasi != "S")
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
            PrintSPTemplate uc = (PrintSPTemplate)Page.LoadControl("PrintSPTemplate.ascx");
            uc.NoKontrak = NoKontrak;
            uc.Project = Project;
            list.Controls.Add(uc);
        }

        private void SetTemplate2()
        {
            PrintSPTemplate2 uc = (PrintSPTemplate2)Page.LoadControl("PrintSPTemplate2.ascx");
            uc.NoKontrak = NoKontrak;
            uc.Project = Project;
            list.Controls.Add(uc);
        }

        private void Fill()
        {
            cancel.Attributes["onclick"] = "location.href='KontrakEdit.aspx?NoKontrak=" + NoKontrak + "'";
            cancel2.Attributes["onclick"] = "location.href='KontrakEdit.aspx?NoKontrak=" + NoKontrak + "'";

            string strSql = "SELECT PrintSP FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'";
            DataTable rs = Db.Rs(strSql);
            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/NoPrint.html");
            else
            {
                count.Text = rs.Rows[0]["PrintSP"].ToString();
                if ((int)rs.Rows[0]["PrintSP"] == 0)
                {
                    //if (Request.QueryString["Priview"].ToString() == "1")
                    //{
                    //    Tampil();

                    //    Db.Execute("UPDATE MS_KONTRAK SET PrintSP = (PrintSP - 1) WHERE NoKontrak = '" + NoKontrak + "'");
                    //    Response.Write("<style media='print'>body{display : none;visibility : hidden;}</style>");
                    //}
                    //else
                    //{
                    //    Tampil(); //langsung tampil
                    //              //ConvertPdf();
                    //              //Response.Redirect(Param.PathLinkFilePDFMarketingJual + NoKontrak.Replace("/", "_").Replace("\\", "_") + Project + "_SP.pdf");
                    //}
                }
                else
                {
                    if (Request.QueryString["Priview"].ToString() == "1")
                    {
                        Tampil();

                        Db.Execute("UPDATE MS_KONTRAK SET PrintSP = (PrintSP - 1) WHERE NoKontrak = '" + NoKontrak + "'");
                        Response.Write("<style media='print'>body{display : none;visibility : hidden;}</style>");
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
        }

        private void Tampil()
        {
            list.Visible = true;
            reprint.Visible = false;
            Js.AutoPrint(this);

            //increment
            Db.Execute("UPDATE MS_KONTRAK SET PrintSP = PrintSP + 1 WHERE NoKontrak = '" + NoKontrak + "'");

            //Logfile
            DataTable rs = Db.Rs("SELECT "
                + " MS_KONTRAK.NoKontrak AS [No. Kontrak]"
                + ",MS_KONTRAK.NoUnit AS [Unit]"
                + ",MS_CUSTOMER.Nama AS [Customer]"
                + ",PrintSP AS [Print Counter]"
                + " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER"
                + " ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
                + " WHERE MS_KONTRAK.NoKontrak = '" + NoKontrak + "'");

            Db.Execute("EXEC spLogKontrak"
                + " 'P-SP'"
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

            string myHtml = "http://" + Request.Url.Host + ":" + Request.Url.Port + "/marketingjual/PrintSP1.aspx?NoKontrak=" + NoKontrak + "&project=" + Project;
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
                + " MS_KONTRAK.NoKontrak AS [No. Kontrak]"
                + ",MS_KONTRAK.NoUnit AS [Unit]"
                + ",MS_CUSTOMER.Nama AS [Customer]"
                + " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER"
                + " ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
                + " WHERE MS_KONTRAK.NoKontrak = '" + NoKontrak + "'");

            Db.Execute("EXEC spLogKontrak"
                + " 'R-SP'"
                + ",'" + Username + "'"
                + ",'" + Act.IP + "'"
                + ",'" + Cf.LogCapture(rs) + "'"
                + ",'" + NoKontrak.ToString() + "'"
                );

            decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
            string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
            Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

            Tampil();
            //string file = Param.PathFilePDFMarketingJual + NoKontrak.Replace("/", "_").Replace("\\", "_") + Project + "_SP.pdf";
            //bool exist = System.IO.File.Exists(file);
            //if (exist)
            //{
            //    System.IO.File.Delete(file);
            //}
            //ConvertPdf();
            //Response.Redirect("http://" + Request.Url.Host + ":" + Request.Url.Port
            //    + Param.PathLinkFilePDFMarketingJual + NoKontrak.Replace("/", "_").Replace("\\", "_") + Project + "_SP.pdf");
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

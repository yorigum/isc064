﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Diagnostics;

namespace ISC064.KPA
{
    public partial class PrintPengajuan : System.Web.UI.Page
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
            PrintPengajuanTemplate uc = (PrintPengajuanTemplate)Page.LoadControl("PrintPengajuanTemplate.ascx");
            uc.NoPengajuan = NoPengajuan;
            list.Controls.Add(uc);
        }

        private void Fill()
        {
            cancel.Attributes["onclick"] = "location.href='PengajuanEdit.aspx?id=" + NoPengajuan + "'";
            cancel2.Attributes["onclick"] = "location.href='PengajuanEdit.aspx?id=" + NoPengajuan + "'";

            string strSql = "SELECT * FROM " + Mi.DbPrefix + "FINANCEAR..MS_PENGAJUAN_KPA WHERE NoPengajuan=" + NoPengajuan;
            DataTable rs = Db.Rs(strSql);
            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/NoPrint.html");
            else
            {
                count.Text = rs.Rows[0]["PrintPengajuan"].ToString();
                if ((int)rs.Rows[0]["PrintPengajuan"] == 0)
                {
                    Tampil(); //langsung tampil
                    ConvertPdf();
                    Response.Redirect(Param.PathLinkFilePDFKPA + NoPengajuan + "_PengajuanKPA.pdf");
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
            Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_PENGAJUAN_KPA SET PrintPengajuan = PrintPengajuan + 1 WHERE NoPengajuan=" + NoPengajuan);

            //Logfile
            DataTable rs = Db.Rs("SELECT "
                 + " CONVERT(varchar, TglInput, 106) AS [Tanggal]"
                 + ",NoPengajuan"
                 + ",CONVERT(varchar, TglFormulir, 106) AS [Tanggal Formulir]"
                 + ",CONVERT(varchar, TglRencanaCair, 106) AS [Tanggal Rencana Cair]"
                 + ",Total"
                 + ",Keterangan"
                 + " FROM ISC064_FINANCEAR..MS_PENGAJUAN_KPA WHERE NoPengajuan = " + NoPengajuan);


            Db.Execute("EXEC ISC064_FINANCEAR..spLogPengajuanKPA"
                + " 'P-PKPA'"
                + ",'" + Act.UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'" + Cf.LogCapture(rs) + "'"
                + ",'" + NoPengajuan + "'"
                );

            decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM " + Mi.DbPrefix + "FINANCEAR..MS_PENGAJUAN_KPA_LOG ORDER BY LogID DESC");
            string Project = Db.SingleString("SELECT Project FROM " + Mi.DbPrefix + "FINANCEAR..MS_PENGAJUAN_KPA WHERE NoPengajuan = '" + NoPengajuan + "'");
            Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_PENGAJUAN_KPA_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);


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
                + " CONVERT(varchar, TglInput, 106) AS [Tanggal]"
                + ",NoPengajuan"
                + ",CONVERT(varchar, TglFormulir, 106) AS [Tanggal Formulir]"
                + ",CONVERT(varchar, TglRencanaCair, 106) AS [Tanggal Rencana Cair]"
                + ",Total"
                + ",Keterangan"
                + " FROM ISC064_FINANCEAR..MS_PENGAJUAN_KPA WHERE NoPengajuan = " + NoPengajuan);


            Db.Execute("EXEC ISC064_FINANCEAR..spLogPengajuanKPA"
                + " 'R-PKPA'"
                + ",'" + Act.UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'" + Cf.LogCapture(rs) + "'"
                + ",'" + NoPengajuan + "'"
                );

            decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM " + Mi.DbPrefix + "FINANCEAR..MS_PENGAJUAN_KPA_LOG ORDER BY LogID DESC");
            string Project = Db.SingleString("SELECT Project FROM " + Mi.DbPrefix + "FINANCEAR..MS_PENGAJUAN_KPA WHERE NoPengajuan = '" + NoPengajuan + "'");
            Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_PENGAJUAN_KPA_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

            Tampil();
            string file = Param.PathFilePDFKPA + NoPengajuan + "_PengajuanKPA.pdf";
            bool exist = System.IO.File.Exists(file);
            if (exist)
            {
                System.IO.File.Delete(file);
            }
            ConvertPdf();
            Response.Redirect(Param.PathLinkFilePDFKPA + NoPengajuan + "_PengajuanKPA.pdf");
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

        private string NoPengajuan
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoPengajuan"]);
            }
        }

        private void ConvertPdf()
        {
            Process p = new System.Diagnostics.Process();

            string myHtml = "http://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port + "/kpa/PrintPengajuan1.aspx?NoPengajuan=" + NoPengajuan;

            string save = Param.PathFilePDFKPA + NoPengajuan + "_PengajuanKPA.pdf";
            string link = Param.PathLinkFilePDFKPA + NoPengajuan + ".pdf";

            p.StartInfo.Arguments = "--orientation portrait --page-width 8.5in --page-height 11in --margin-left 2cm --margin-right 2cm --margin-top 1.25cm --margin-bottom 0 " + myHtml + " " + save;
            p.StartInfo.FileName = Mi.PathWkhtmlPDFReport;
            p.Start();
            p.WaitForExit(60000);
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

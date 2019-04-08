﻿using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
    public partial class PrintFPS : System.Web.UI.Page
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
            PrintFPSTemplate uc = (PrintFPSTemplate)Page.LoadControl("PrintFPSTemplate.ascx");
            uc.NoKontrak = NoKontrak;
            list.Controls.Add(uc);
        }

        private void Fill()
        {
            cancel.Attributes["onclick"] = "location.href='KontrakEdit.aspx?NoKontrak=" + NoKontrak + "'";
            cancel2.Attributes["onclick"] = "location.href='KontrakEdit.aspx?NoKontrak=" + NoKontrak + "'";

            string strSql = "SELECT PrintFPS FROM MS_KONTRAK WHERE Status = 'A' AND NoKontrak = '" + NoKontrak + "'";
            DataTable rs = Db.Rs(strSql);
            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/NoPrint.html");
            else
            {
                count.Text = rs.Rows[0]["PrintFPS"].ToString();
                if ((int)rs.Rows[0]["PrintFPS"] == 0)
                    Tampil(); //langsung tampil
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
            Db.Execute("UPDATE MS_KONTRAK SET PrintFPS = PrintFPS + 1 WHERE NoKontrak = '" + NoKontrak+"'");

            //Logfile

            DataTable rs = Db.Rs("SELECT "
                + " CONVERT(varchar, TglKontrak, 106) AS [Tanggal]"
                + ",NoKontrak AS [No Kontrak]"
                + ",NoCustomer AS [No Customer]"
                + ",Gross"
                + ",NoUnit AS [No Unit]"
                + ",NoAJB AS [No AJB]"
                + ",CaraBayar AS [Cara Bayar]"
                + ",KetWawancara AS [Keterangan]"
                + ",NilaiKontrak AS [Nilai Kontrak]"
                + ",NoFPS AS [No. FPS]"
                + " FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");

            Db.Execute("EXEC spLogKontrak"
                + " 'P-FPS'"
                + ",'" + Act.UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'" + Cf.LogCapture(rs) + "'"
                + ",'" + NoKontrak.ToString().PadLeft(7, '0') + "'"
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

        private void Valid(string Username)
        {
            Session["SalahPass"] = null;

            //Logfile otorisasi
            DataTable rs = Db.Rs("SELECT "
                + " CONVERT(varchar, TglKontrak, 106) AS [Tanggal]"
                + ",NoKontrak AS [No Kontrak]"
                + ",NoCustomer AS [No Customer]"
                + ",Gross"
                + ",NoUnit AS [No Unit]"
                + ",NoAJB AS [No AJB]"
                + ",CaraBayar AS [Cara Bayar]"
                + ",KetWawancara AS [Keterangan]"
                + ",NilaiKontrak AS [Nilai Kontrak]"
                + ",NoFPS AS [No. FPS]"
                + " FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");

            Db.Execute("EXEC spLogKontrak"
                + " 'R-FPS'"
                + ",'" + Username + "'"
                + ",'" + Act.IP + "'"
                + ",'" + Cf.LogCapture(rs) + "'"
                + ",'" + NoKontrak.ToString().PadLeft(7, '0') + "'"
                );

            decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
            string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
            Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

            Tampil();
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
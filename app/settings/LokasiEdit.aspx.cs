﻿using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.ADMINJUAL
{
    public partial class LokasiEdit : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Act.Sec("ED:" + Request.PhysicalPath))
            {
                ok.Enabled = false;
                save.Enabled = false;
            }

            if (!Page.IsPostBack)
            {
                Act.ProjectList(project);
                //Bind(); //tanggal dan bulan
                Fill();
            }

            FeedBack();
            Js.Confirm(this, "Lanjutkan proses edit data lokasi unit properti?\\n"
                + "");
        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "Edit Berhasil...";
            }
        }

        private void Fill()
        {            

            btnlog.Attributes["onclick"] = "location.href='LogPk.aspx?tb=ISC064_MARKETINGJUAL..REF_LOKASI_LOG&Pk=" + NoLokasi + "'";
            btndel.Attributes["onclick"] = "location.href='LokasiDel.aspx?NoLokasi=" + NoLokasi + "'";

            string strSql = "SELECT * FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_LOKASI WHERE SN = '" + NoLokasi + "'";
            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else if (!Act.AksesProject(rs.Rows[0]["Project"].ToString()))
                Response.Redirect("/CustomError/SecLevel.html");
            else
            {
                nolokasi.Text = rs.Rows[0]["SN"].ToString();
                lokasi.Text = rs.Rows[0]["Lokasi"].ToString();
                namalokasi.Text = rs.Rows[0]["Nama"].ToString();
                project.SelectedValue = rs.Rows[0]["Project"].ToString();
            }
        }

        private bool valid()
        {
            string s = "";
            bool x = true;

            if (Cf.isEmpty(lokasi))
            {
                x = false;
                if (s == "") s = lokasi.ID;
                lokasic.Text = "Kosong";
            }
            else
                lokasic.Text = "";

            if (Cf.isEmpty(namalokasi))
            {
                x = false;
                if (s == "") s = namalokasi.ID;
                namalokasic.Text = "Kosong";
            }
            else
                namalokasic.Text = "";


            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Lokasi Unit Properti tidak boleh kosong.\\n"
                    + "2. Nama Lokasi Unit Properti tidak boleh kosong.\\n"
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );

            return x;
        }

        protected void Save(bool close)
        {
            if (valid())
            {
                int ada = Db.SingleInteger("SELECT COUNT(*) FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_LOKASI WHERE Lokasi='" + lokasi.Text + "' AND Project = '" + project.SelectedValue + "' AND SN != " + NoLokasi);
                if (ada > 0)
                {
                    string s = "";

                    Js.Alert(
                       this
                       , "Input Tidak Valid.\\n\\n"
                       + "Aturan Proses :\\n"
                       + "1. Lokasi Unit Properti tidak boleh kosong.\\n"
                       + "2. Nama Lokasi Unit tidak boleh kosong.\\n"
                       + "3. Lokasi Unit Duplikat.\\n"
                       , "document.getElementById('" + s + "').focus();"
                       + "document.getElementById('" + s + "').select();"
                       );
                }
                else
                {

                    string strSql = "SELECT * FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_LOKASI WHERE SN = '" + NoLokasi + "'";
                    DataTable rs = Db.Rs(strSql);
                    DataTable rsBef = Db.Rs("SELECT "
                                + " Lokasi"
                                + ",Nama"
                                + ",Project"
                                + ",SN"
                                + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_LOKASI "
                                + " WHERE SN = '" + NoLokasi + "'");

                    //relasi
                    Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT SET Lokasi='" + lokasi.Text + "' WHERE Lokasi='" + rs.Rows[0]["Lokasi"] + "'");
                    Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK SET Lokasi='" + lokasi.Text + "' WHERE Lokasi='" + rs.Rows[0]["Lokasi"] + "'");
                    Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_RESERVASI SET Lokasi='" + lokasi.Text + "' WHERE Lokasi='" + rs.Rows[0]["Lokasi"] + "'");
                    Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_RESERVASI_OBS SET Lokasi='" + lokasi.Text + "' WHERE Lokasi='" + rs.Rows[0]["Lokasi"] + "'");

                    Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..REF_LOKASI SET Lokasi='" + lokasi.Text + "',Nama='" + namalokasi.Text + "',Project='" + project.SelectedValue + "' WHERE SN = '" + NoLokasi + "'");

                    DataTable rsAft = Db.Rs("SELECT "
                                + " Lokasi"
                                + ",Nama"
                                + ",Project"
                                + ",SN"
                                + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_LOKASI "
                                + " WHERE SN = '" + NoLokasi + "'");

                    //Logfile
                    string Ket = "Lokasi: " + lokasi.Text + "<br>"
                        + Cf.LogCompare(rsBef, rsAft);

                    Db.Execute("EXEC " + Mi.DbPrefix + "MARKETINGJUAL..spLogLokasi"
                        + " 'EDIT'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + Ket + "'"
                        + ",'" + NoLokasi + "'"
                        );

                    decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_LOKASI_LOG ORDER BY LogID DESC");
                    Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..REF_LOKASI_LOG SET Project = '" + project.SelectedValue + "' WHERE LogID  = " + LogID);

                    if (close)
                        Js.CloseAndReload(this);
                    else
                        Response.Redirect("LokasiEdit.aspx?done=1&NoLokasi=" + NoLokasi);
                }
            }
        }


        protected void ok_Click(object sender, System.EventArgs e)
        {
            Save(true);
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            Save(false);
        }

        private string NoLokasi
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoLokasi"]);
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

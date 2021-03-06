﻿using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.ADMINJUAL
{

    public partial class TipeSalesEdit : System.Web.UI.Page
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
                Fill();
            }

            FeedBack();
            Js.Confirm(this, "Lanjutkan proses edit data Tipe unit properti?\\n"
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
            Act.ProjectList(project);

            btnlog.Attributes["onclick"] = "location.href='LogPk.aspx?tb=ISC064_MARKETINGJUAL..REF_AGENT_TIPE_LOG&Pk=" + NoTipe + "'";
            btndel.Attributes["onclick"] = "location.href='TipeSalesDel.aspx?NoTipe=" + NoTipe + "'";

            string strSql = "SELECT * FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_AGENT_TIPE WHERE ID = '" + NoTipe + "'";
            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else if (!Act.AksesProject(rs.Rows[0]["Project"].ToString()))
                Response.Redirect("/CustomError/SecLevel.html");
            else
            {
                notipe.Text = rs.Rows[0]["ID"].ToString();
                tipe.Text = rs.Rows[0]["Tipe"].ToString();
                Cf.SelectedValue(project, rs.Rows[0]["Project"].ToString());
            }
        }

        private bool valid()
        {
            string s = "";
            bool x = true;

            if (Cf.isEmpty(tipe))
            {
                x = false;
                if (s == "") s = tipe.ID;
                tipec.Text = "Kosong";
            }
            else
                tipec.Text = "";

            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Tipe Sales tidak boleh kosong.\\n"
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );

            return x;
        }


        protected void ok_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                int ada = Db.SingleInteger("SELECT COUNT(*) FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_AGENT_TIPE WHERE Tipe='" + tipe.Text + "' AND ID != " + NoTipe);
                if (ada > 0)
                {
                    string s = "";

                    Js.Alert(
                       this
                       , "Input Tidak Valid.\\n\\n"
                       + "Aturan Proses :\\n"
                       + "1. Tipe Sales tidak boleh kosong.\\n"
                       + "2. Tipe Sales Duplikat.\\n"
                       , "document.getElementById('" + s + "').focus();"
                       + "document.getElementById('" + s + "').select();"
                       );
                }
                else
                {
                    string strSql = "SELECT * FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_AGENT_TIPE WHERE ID = '" + NoTipe + "'";
                    DataTable rs = Db.Rs(strSql);
                    DataTable rsBef = Db.Rs("SELECT "
                                + " Tipe"
                                + ",Project"
                                + ",ID"
                                + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_AGENT_TIPE "
                                + " WHERE ID = '" + NoTipe + "'");

                    //relasi
                    //Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_RESERVASI SET Jenis='" + jenis.Text + "' WHERE Jenis='" + rs.Rows[0]["Jenis"] + "'");

                    Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..REF_AGENT_TIPE SET Tipe='" + tipe.Text + "',Project= '" + project.SelectedValue + "' WHERE ID = '" + NoTipe + "'");

                    DataTable rsAft = Db.Rs("SELECT "
                                + " Tipe"
                                + ",Project"
                                + ",ID"
                                + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_AGENT_TIPE "
                                + " WHERE ID = '" + NoTipe + "'");

                    //Logfile
                    string Ket = "Tipe: " + tipe.Text + "<br>"
                        + Cf.LogCompare(rsBef, rsAft);

                    Db.Execute("EXEC " + Mi.DbPrefix + "MARKETINGJUAL..spLogTipeSales"
                        + " 'EDIT'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + Ket + "'"
                        + ",'" + NoTipe + "'"
                        );

                    Js.Close(this);
                }
            }

        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            // if (Save()) Response.Redirect("JenisEdit.aspx?done=1&NoJenis=" + NoJenis);

            if (valid())
            {
                int ada = Db.SingleInteger("SELECT COUNT(*) FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_AGENT_TIPE WHERE Tipe ='" + tipe.Text + "' AND ID != " + NoTipe);
                if (ada > 0)
                {
                    string s = "";

                    Js.Alert(
                       this
                       , "Input Tidak Valid.\\n\\n"
                       + "Aturan Proses :\\n"
                       + "1. Tipe Sales tidak boleh kosong.\\n"
                       + "2. Tipe Sales Duplikat.\\n"
                       , "document.getElementById('" + s + "').focus();"
                       + "document.getElementById('" + s + "').select();"
                       );
                }
                else
                {
                    string strSql = "SELECT * FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_AGENT_TIPE WHERE ID = '" + NoTipe + "'";
                    DataTable rs = Db.Rs(strSql);
                    DataTable rsBef = Db.Rs("SELECT "
                                + " Tipe"
                                + ",Project"
                                + ",ID"
                                + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_AGENT_TIPE "
                                + " WHERE ID = '" + NoTipe + "'");

                    //relasi
                    //Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT SET Jenis='" + jenis.Text + "' WHERE Jenis='" + rs.Rows[0]["Jenis"] + "'");

                    Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..REF_AGENT_TIPE SET Tipe='" + tipe.Text + "',Project= '" + project.SelectedValue + "' WHERE ID = '" + NoTipe + "'");

                    DataTable rsAft = Db.Rs("SELECT "
                                + " Tipe"
                                + ",Project"
                                + ",ID"
                                + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_AGENT_TIPE "
                                + " WHERE ID = '" + NoTipe + "'");

                    //Logfile
                    string Ket = "Tipe: " + tipe.Text + "<br>"
                        + Cf.LogCompare(rsBef, rsAft);

                    Db.Execute("EXEC " + Mi.DbPrefix + "MARKETINGJUAL..spLogTipeSales"
                        + " 'EDIT'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + Ket + "'"
                        + ",'" + NoTipe + "'"
                        );

                    decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_AGENT_TIPE_LOG ORDER BY LogID DESC");
                    Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..REF_AGENT_TIPE_LOG SET Project = '" + project.SelectedValue + "' WHERE LogID  = " + LogID);

                    Response.Redirect("TipeSalesEdit.aspx?done=1&NoTipe=" + NoTipe);
                }
            }
        }

        private string NoTipe
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoTipe"]);
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
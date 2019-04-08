using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ISC064.ADMINJUAL
{
    public partial class TipeGimmickEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            Act.CekInt("ID");

            //if (!Act.Sec("ED:" + Request.PhysicalPath))
            //{
            //    ok.Enabled = false;
            //    save.Enabled = false;
            //}

            if (!Page.IsPostBack)
            {
                Act.ProjectList(project);

                Fill();
            }

            FeedBack();
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

        protected void Fill()
        {
            btnlog.Attributes["onclick"] = "location.href='LogPk.aspx?Tb=" + Mi.DbPrefix + "MARKETINGJUAL..REF_TIPE_GIMMICK_LOG&Pk=" + ID + "'";
            btndel.Attributes["onclick"] = "location.href='TipeGimmickDel.aspx?Nomor=" + ID + "'";

            DataTable rs = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_TIPE_GIMMICK WHERE ID = '" + ID + "'");
            if(rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                noid.Text = ID;
                Nama.Text = rs.Rows[0]["Nama"].ToString();
                project.SelectedValue = rs.Rows[0]["Project"].ToString();
            }
        }

        protected bool valid()
        {
            bool x = true;

            if (Cf.isEmpty(Nama))
            {
                x = false;
            }

            if (!x)
                Js.Alert(this
                    , "Proses Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Nama tidak boleh kosong.\\n"
                    + "2. Project harus pilih.\\n"
                    , "");

            return x;
        }

        private bool Save()
        {
            bool x = true;

            if (valid())
            {
                string NamaTipe = Cf.Str(Nama.Text);
                string Project = project.SelectedValue;

                DataTable rsBef = Db.Rs("SELECT "
                    + " Nama AS [Nama] "
                    + " ,Project as [Project] "
                    + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_TIPE_GIMMICK WHERE ID = " + ID);

                Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..REF_TIPE_GIMMICK SET Nama = '" + NamaTipe + "',Project = '" + Project + "' WHERE ID = '" + ID + "'");

                DataTable rsAft = Db.Rs("SELECT "
                    + " Nama AS [Nama] "
                    + " ,Project as [Project] "
                    + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_TIPE_GIMMICK WHERE ID = " + ID);

                string ket = Cf.LogCompare(rsBef, rsAft);

                Db.Execute("EXEC " + Mi.DbPrefix + "MARKETINGJUAL..spLogTipeGimmick"
                    + " 'EDIT'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + ket + "'"
                    + ",'" + ID + "'"
                    );

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_TIPE_GIMMICK_LOG ORDER BY LogID DESC");
                Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..REF_TIPE_GIMMICK_LOG SET Project = '" + project.Text + "' WHERE LogID  = " + LogID);

                Response.Redirect("TipeGimmickEdit.aspx?ID=" + ID + "&done=1");
            }

            return x;
        }

        protected void ok_Click(object sender, System.EventArgs e)
        {
            if (Save()) Js.Close(this);
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            if (Save()) Response.Redirect("TipeGimmickEdit.aspx?ID=" + ID + "&done=1");
        }

        private string ID
        {
            get
            {
                return Cf.Pk(Request.QueryString["ID"]);
            }
        }
    }
}
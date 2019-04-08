using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ISC064.ADMINJUAL
{
    public partial class GimmickEdit : System.Web.UI.Page
    {
        DataTable rs;
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

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
            btnlog.Attributes["onclick"] = "location.href='LogPk.aspx?Tb=" + Mi.DbPrefix + "MARKETINGJUAL..MS_GIMMICK_LOG&Pk=" + ID + "'";
            btndel.Attributes["onclick"] = "location.href='GimmickDel.aspx?Nomor=" + ID + "'";

            DataTable rk = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_GIMMICK WHERE ItemID = '" + ID + "'");
            if (rk.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                if (rk.Rows[0]["Status"].ToString() == "0")
                    inaktif.Checked = true;
                else if (rk.Rows[0]["Status"].ToString() == "1")
                    aktif.Checked = true;

                project.SelectedValue = rk.Rows[0]["project"].ToString();
                rs = Db.Rs("SELECT ID,Nama FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_TIPE_GIMMICK WHERE PRoject = '" + project.SelectedValue + "'");
                for (int i = 0; i < rs.Rows.Count; i++)
                {
                    string a = rs.Rows[i]["ID"].ToString();
                    string b = rs.Rows[i]["Nama"].ToString();
                    tipe.Items.Add(new ListItem(b, a));
                }
                tipe.SelectedValue = rk.Rows[0]["Tipe"].ToString();
                noid.Text = rk.Rows[0]["ItemID"].ToString();
                nama.Text = rk.Rows[0]["Nama"].ToString();
                satuan.Text = rk.Rows[0]["Satuan"].ToString();
                qty.Text = Cf.Num(rk.Rows[0]["Stock"]);
                hsb.Text = Cf.Num(rk.Rows[0]["HargaSatuan"]);
                htb.Text = Cf.Num(rk.Rows[0]["HargaTotal"]);
                htb2.Text = Cf.Num(htb.Text);
                ket.Text = rk.Rows[0]["ket"].ToString();

                htb.Enabled = false;
            }
        }

        private bool valid()
        {
            bool x = true;

            if (project.SelectedIndex == 0)
            {
                x = false;
            }

            if (tipe.SelectedIndex == 0)
            {
                x = false;
            }

            if (Cf.isEmpty(satuan))
            {
                x = false;
            }

            if (Cf.isEmpty(qty))
            {
                x = false;
            }
            else
            {
                if (!Cf.isMoney(qty))
                {
                    x = false;
                }
            }

            if (!Cf.isMoney(hsb))
            {
                x = false;
            }

            if (Cf.isEmpty(nama))
            {
                x = false;
            }
            else
            {
                if (Db.SingleInteger("SELECT COUNT(*) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_GIMMICK WHERE Nama = '" + nama.Text + "'") > 1)
                {
                    x = false;
                }
            }

            if (!x)
                Js.Alert(this,
                    "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Project harus di pilih.\\n"
                    + "2. Tipe Barang harus di pilih.\\n"
                    + "3. Satuan Barang tidak boleh kosong.\\n"
                    + "4. Qty Barang harus Angka.\\n"
                    + "5. Harga Satuan Barang harus Angka.\\n"
                    + "6. Nama harus di isi.\\n"
                    + "7. Nama Barang sudah ada.\\n"
                    , "");

            return x;
        }

        private bool Save()
        {
            if (valid())
            {
                string Project = project.SelectedValue;
                string TipeBarang = tipe.SelectedValue;
                string NamaBarang = Cf.Str(nama.Text);
                string Satuan = Cf.Str(satuan.Text);
                decimal Qty = Convert.ToDecimal(qty.Text);
                decimal Harga = Convert.ToDecimal(hsb.Text);
                decimal Total = Qty * Harga;
                int StatusAktif = 0;
                if (aktif.Checked)
                {
                    StatusAktif = 1;
                }

                DataTable rsBef = Db.Rs("SELECT "
                    + " Nama AS [Nama] "
                    + " ,HargaSatuan as [Harga Satuan] "
                    + " ,HargaTotal as [Harga Total] "
                    + " ,Stock as [Stock] "
                    + " ,Satuan as [Satuan] "
                    + " ,Project as [Project] "
                    + " ,Tipe as [Tipe] "
                    + " ,Status as [Status] "
                    + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_GIMMICK WHERE ItemID = " + ID);

                Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_GIMMICK"
                    + " SET Nama = '" + NamaBarang + "'"
                    + ",HargaSatuan = '" + Harga + "'"
                    + ",HargaTotal = '" + Total + "'"
                    + ",Stock = '" + Qty + "'"
                    + ",Satuan = '" + Satuan + "'"
                    + ",Project = '" + Project + "'"
                    + ",Tipe = '" + TipeBarang + "'"
                    + ",Status = '" + StatusAktif + "'"
                    + " WHERE ItemID = '" + ID + "'");

                DataTable rsAft = Db.Rs("SELECT "
                    + " Nama AS [Nama] "
                    + " ,HargaSatuan as [Harga Satuan] "
                    + " ,HargaTotal as [Harga Total] "
                    + " ,Stock as [Stock] "
                    + " ,Satuan as [Satuan] "
                    + " ,Project as [Project] "
                    + " ,Tipe as [Tipe] "
                    + " ,Status as [Status] "
                    + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_GIMMICK WHERE ItemID = " + ID);

                string ket = Cf.LogCompare(rsBef, rsAft);

                Db.Execute("EXEC " + Mi.DbPrefix + "MARKETINGJUAL..spLogGimmick"
                    + " 'EDIT'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + ket + "'"
                    + ",'" + ID + "'"
                    );

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_GIMMICK_LOG ORDER BY LogID DESC");
                Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_GIMMICK_LOG SET Project = '" + project.Text + "' WHERE LogID  = " + LogID);

                //Response.Redirect("TipeGimmickEdit.aspx?ID=" + ID + "&done=1");
                return true;
            }
            else
                return false;
        }

        protected void ok_Click(object sender, System.EventArgs e)
        {
            if (Save()) Js.Close(this);
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            if (Save()) Response.Redirect("GimmickEdit.aspx?ID=" + ID + "&done=1");
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
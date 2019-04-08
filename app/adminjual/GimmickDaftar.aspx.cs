using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

namespace ISC064.ADMINJUAL
{
    public partial class GimmickDaftar : System.Web.UI.Page
    {
        DataTable rs;
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                init();

                Js.Focus(this, save);
                Fill();
            }
            FeedBack();

            Js.NumberFormat(hrst);
        }
        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "<a href=\"javascript:popEditMGimmick('" + Request.QueryString["done"] + "')\">"
                        + "Pendaftaran Berhasil..."
                        + "</a>";
            }
        }
        protected void init()
        {
            Act.ProjectList(project);
        }

        private void Fill()
        {
            string strSql = "SELECT TOP 25 ItemID, Nama "
                + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_GIMMICK WHERE Project IN (" + Act.ProjectListSql + ")"
                + " ORDER BY ItemID DESC";

            rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["ItemID"].ToString();
                string t = rs.Rows[i]["Nama"].ToString();

                baru.Items.Add(new ListItem(t, v));
            }

            if (rs.Rows.Count != 0)
            {
                baru.SelectedIndex = 0;
                baru.Attributes["ondblclick"] = "popEditMGimmick("
                    + "this.options[this.selectedIndex].value)";
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

            if (!Cf.isMoney(hrst))
            {
                x = false;
            }

            if (!x)
                Js.Alert(this,
                    "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Project harus di pilih.\\n"
                    + "2. Tipe Barang harus di pilih.\\n"
                    + "3. Satuan Barang tidak boleh kosong.\\n"
                    + "4. Qty Barang harus Angka.\\n"
                    + "5. Harag Satuan Barang harus Angka.\\n"
                    + "6. Nama harus di isi.\\n"
                    , "");

            return x;
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                int ID = Db.SingleInteger("SELECT COUNT(*) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_GIMMICK WHERE Project = '" + project.SelectedValue + "'");
                if (ID == 0)
                {
                    ID = 1;
                }
                else
                {
                    ID = (ID + 1);
                }

                string Project = project.SelectedValue;
                string Tipe = Cf.Str(tipe.Text);
                string NamaBarang = Cf.Str(Nama.Text);
                string SatuanBarang = Cf.Str(satuan.Text);
                decimal Qty = Convert.ToDecimal(qty.Text);
                decimal HargaSatauan = Convert.ToDecimal(hrst.Text);
                string Ket = Cf.Str(ket.Text.Replace("/r/n", "<br/>"));

                decimal Tot = Math.Round(Qty * HargaSatauan);

                Db.Execute("INSERT INTO " + Mi.DbPrefix + "MARKETINGJUAL..MS_GIMMICK values ("
                    + ID
                    + ",1"
                    + ",'" +NamaBarang + "'"
                    + ",'" +Ket + "'"
                    + ",'" + Tipe + "'"
                    + ",'" + SatuanBarang + "'"
                    + ",getdate()"
                    + ",getdate()"
                    + "," + Qty
                    + "," + HargaSatauan
                    + "," + Tot
                    + ",'" + Project + "'"
                    + ")"
                    );

                //noid.Text = Db.SingleInteger("SELECT TOP 1 ItemID FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_GIMMICK WHERE Project = '" + project.SelectedValue + "'").ToString();

                DataTable rs = Db.Rs("SELECT "
                    + "Nama as [Nama Barang]"
                    + ",(select Nama from REF_TIPE_GIMMICK where REF_TIPE_GIMMICK.ID = MS_GIMMICK.Tipe) AS [Tipe Barang]"
                    + ",Satuan"
                    + ",TglInput as [Tangga Daftar]"
                    + ",Stock"
                    + ",HargaSatuan as [Harga Satuan Barang]"
                    + ",HargaTotal as [Harga Total Barang]"
                    + ",ket as [Keterangan]"
                    + ",Project"
                    + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_GIMMICK"
                    + " WHERE ItemID = " + ID
                    );

                Db.Execute("EXEC " + Mi.DbPrefix + "MARKETINGJUAL..spLogGimmick"
                    + " 'DAFTAR'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + Cf.LogCapture(rs) + "'"
                    + ",'" + ID + "'"
                    );

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_GIMMICK_LOG ORDER BY LogID DESC");
                Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_GIMMICK_LOG SET Project = '" + project.Text + "' WHERE LogID  = " + LogID);

                Response.Redirect("GimmickDaftar.aspx?done=" + ID);
            }
        }

        //private string ID
        //{
        //    get
        //    {
        //        return Cf.Pk(noid.Text);
        //    }
        //}

        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (project.SelectedIndex != 0)
            {
                rs = Db.Rs("SELECT ID,Nama FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_TIPE_GIMMICK WHERE PRoject = '" + project.SelectedValue + "'");
                for (int i = 0; i < rs.Rows.Count; i++)
                {
                    string a = rs.Rows[i]["ID"].ToString();
                    string b = rs.Rows[i]["Nama"].ToString();
                    tipe.Items.Add(new ListItem(b, a));
                }
            }
        }
    }
}
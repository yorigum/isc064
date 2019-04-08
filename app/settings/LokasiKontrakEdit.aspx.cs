using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.ADMINJUAL
{
    public partial class LokasiKontrakEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
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
            btnlog.Attributes["onclick"] = "location.href='LogPk.aspx?Tb="+Mi.DbPrefix+"MARKETINGJUAL..REF_LOKASI_KONTRAK_LOG&Pk=" + NoLokasi + "'";
            btndel.Attributes["onclick"] = "location.href='LokasiKontrakDel.aspx?NoLokasi=" + NoLokasi + "'";

            string strSql = "SELECT * FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_LOKASI_KONTRAK WHERE SN = '" + NoLokasi + "'";
            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                nolokasi.Text = rs.Rows[0]["SN"].ToString();
                lokasi.Text = rs.Rows[0]["Lokasi"].ToString();
                namalokasi.Text = rs.Rows[0]["Nama"].ToString();
                project.SelectedValue = rs.Rows[0]["Project"].ToString();
            }
        }

        private void SaveLok()
        {

            string strSql = "SELECT * FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_LOKASI_KONTRAK WHERE SN = '" + NoLokasi + "'";
            DataTable rs = Db.Rs(strSql);
            DataTable rsBef = Db.Rs("SELECT "
                        + " Lokasi"
                        + ",Nama"
                        + ",SN"
                        + " FROM "+Mi.DbPrefix+"MARKETINGJUAL..REF_LOKASI_KONTRAK "
                        + " WHERE SN = '" + NoLokasi + "'");

            Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..REF_LOKASI_KONTRAK SET Lokasi='" + lokasi.Text + "', Nama='" + namalokasi.Text + "' WHERE SN = '" + NoLokasi + "'");

            DataTable rsAft = Db.Rs("SELECT "
                        + " Lokasi"
                        + ",Nama"
                        + ",SN"
                        + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_LOKASI_KONTRAK "
                        + " WHERE SN = '" + NoLokasi + "'");

            //Logfile
            string Ket = "Lokasi: " + lokasi.Text + "<br>"
                + Cf.LogCompare(rsBef, rsAft);

            Db.Execute("EXEC " + Mi.DbPrefix + "MARKETINGJUAL..spLogLokasiKontrak"
                + " 'EDIT'"
                + ",'" + Act.UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'" + Ket + "'"
                + ",'" + NoLokasi + "'"
                );


            //}
        }

        protected void ok_Click(object sender, EventArgs e)
        {
            SaveLok();
            Js.Close(this);
        }

        protected void save_Click(object sender, EventArgs e)
        {
            SaveLok();
            Response.Redirect("LokasiKontrakEdit.aspx?done=1&NoLokasi=" + NoLokasi);
        }

        private string NoLokasi
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoLokasi"]);
            }
        }
    }
}
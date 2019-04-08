using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.SETTINGS
{
    public partial class MasterData : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

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
                project.SelectedValue = Request.QueryString["project"];
                Fill();
            }
        }

        private bool valid()
        {
            string s = "";
            bool x = true;

            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Kode tidak boleh kosong dan tidak boleh duplikat.\\n"
                    + "2. Nama Lengkap tidak boleh kosong.\\n"
                    + "3. Password Awal tidak boleh kosong.\\n"
                    + "4. Aturan Ganti Password harus berupa angka bulat saja.\\n"
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );

            return x;
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                int No = 0;
                No = Db.SingleInteger("SELECT ISNULL(MAX(No), 0) FROM REF_DATA WHERE Project = '" + project.SelectedValue + "'");

                if (No > 0)
                {
                    DataTable bef = Log();
                    //update
                    Db.Execute("UPDATE REF_DATA"
                        + " SET NPWP = '" + npwp.Text + "'"
                        + " ,NPWPNama = '" + npwpnama.Text + "'"
                        + " ,AlamatNPWP = '" + npwpalamat.Text + "'"
                        + " ,NoTelp = '" + notelp.Text + "'"
                        + " ,NoFax = '" + nofax.Text + "'"
                        + " ,AlamatPers = '" + alamat1.Text + "'"
                        + " ,AlamatProject = '" + alamat2.Text + "'"
                        + " ,RekBank = '" + namabank.Text + "'"
                        + " ,RekCabang = '" + cabang.Text + "'"
                        + " ,RekNo = '" + norek.Text + "'"
                        + " ,RekAN = '" + an.Text + "'"
                        + " ,RekAlamat = '" + alamatbank.Text + "'"
                        + " ,BlokNPWP = '" + BlokNPWP.Text + "'"
                        + " ,NomorNPWP = '" + NomorNPWP.Text + "'"
                        + " ,RTNPWP = '" + RTNPWP.Text + "'"
                        + " ,RWNPWP = '" + RWNPWP.Text + "'"
                        + " ,KecamatanNPWP = '" + KecamatanNPWP.Text + "'"
                        + " ,KabupatenNPWP = '" + KabupatenNPWP.Text + "'"
                        + " ,PropinsiNPWP = '" + PropinsiNPWP.Text + "'"
                        + " ,KodePosNPWP = '" + KodePosNPWP.Text + "'"
                        + " WHERE Project = '" + project.SelectedValue + "'"
                        );

                    string KetLog = Cf.LogCompare(bef, Log());

                    Db.Execute("EXEC spLogRefData"
                        + " 'EDIT'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + KetLog + "'"
                        + ",'" + No + "'"
                        );
                }
                else
                {
                    No = Db.SingleInteger("SELECT COUNT(No) + 1 FROM REF_DATA");

                    Db.Execute(
                          "INSERT INTO REF_DATA "
                        + "(No, NPWP, NPWPNama, AlamatNPWP, NoTelp, NoFax, AlamatPers, AlamatProject, RekBank, RekCabang, RekNo"
                        + ", RekAN, RekAlamat, BlokNPWP, NomorNPWP, RTNPWP, RWNPWP, KecamatanNPWP, KabupatenNPWP, PropinsiNPWP, KodePosNPWP, Project)"
                        + " VALUES(" + No + ",'" + npwp.Text + "', '" + npwpnama.Text + "', '" + npwpalamat.Text + "', '" + notelp.Text + "', '" + nofax.Text + "', '" + alamat1.Text + "'"
                        + ", '" + alamat2.Text + "', '" + namabank.Text + "', '" + cabang.Text + "', '" + norek.Text + "', '" + an.Text + "', '" + alamatbank.Text + "'"
                        + ", '" + BlokNPWP.Text + "', '" + NomorNPWP.Text + "', '" + RTNPWP.Text + "', '" + RWNPWP.Text + "', '" + KecamatanNPWP.Text + "', '" + KabupatenNPWP.Text + "'"
                        + ", '" + PropinsiNPWP.Text + "', '" + KodePosNPWP.Text + "', '" + project.SelectedValue + "'"
                        + ")"
                        );

                    string KetLog = Cf.LogCapture(Log());

                    Db.Execute("EXEC spLogRefData"
                        + " 'DAFTAR'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + KetLog + "'"
                        + ",'" + No + "'"
                        );

                }

                Response.Redirect("MasterData.aspx?done=1&project=" + project.SelectedValue);
            }
        }

        private void Fill()
        {
            namapt.Text = Mi.Pt;

            DataTable rs = Db.Rs("SELECT * FROM REF_DATA WHERE Project = '" + project.SelectedValue + "'");
            if (rs.Rows.Count > 0)
            {
                npwp.Text = rs.Rows[0]["NPWP"].ToString();
                npwpnama.Text = rs.Rows[0]["NPWPNama"].ToString();
                npwpalamat.Text = rs.Rows[0]["AlamatNPWP"].ToString();
                notelp.Text = rs.Rows[0]["NoTelp"].ToString();
                nofax.Text = rs.Rows[0]["NoFax"].ToString();
                alamat1.Text = rs.Rows[0]["AlamatPers"].ToString();
                alamat2.Text = rs.Rows[0]["AlamatProject"].ToString();
                namabank.Text = rs.Rows[0]["RekBank"].ToString();
                norek.Text = rs.Rows[0]["RekNo"].ToString();
                an.Text = rs.Rows[0]["RekAN"].ToString();
                alamatbank.Text = rs.Rows[0]["RekAlamat"].ToString();
                cabang.Text = rs.Rows[0]["RekCabang"].ToString();
                BlokNPWP.Text = rs.Rows[0]["BlokNPWP"].ToString();
                NomorNPWP.Text = rs.Rows[0]["NomorNPWP"].ToString();
                RTNPWP.Text = rs.Rows[0]["RTNPWP"].ToString();
                RWNPWP.Text = rs.Rows[0]["RWNPWP"].ToString();
                KecamatanNPWP.Text = rs.Rows[0]["KecamatanNPWP"].ToString();
                KabupatenNPWP.Text = rs.Rows[0]["KabupatenNPWP"].ToString();
                PropinsiNPWP.Text = rs.Rows[0]["PropinsiNPWP"].ToString();
                KodePosNPWP.Text = rs.Rows[0]["KodePosNPWP"].ToString();
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

        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fill();
        }

        protected DataTable Log()
        {
            return Db.Rs("SELECT "
                   + " Project"
                   + ",NPWP"
                   + ",NPWPNama AS [Nama NPWP]"
                   + ",AlamatNPWP AS [Alamat NPWP]"
                   + ",NoTelp AS [No. Telp]"
                   + ",NoFax AS [No. Fax]"
                   + ",AlamatPers AS [Alamat Perusahaan]"
                   + ",AlamatProject AS [Alamat Project]"
                   + ",RekBank AS [Nama Bank]"
                   + ",RekCabang AS [Cabang Bank]"
                   + ",RekNo AS [No. Rekening]"
                   + ",RekAN AS [Rek Atas Nama]"
                   + ",RekAlamat AS [Rek Alamat]"
                   + ",BlokNPWP AS [Blok NPWP]"
                   + ",NomorNPWP AS [Nomor NPWP]"
                   + ",RTNPWP AS [RT NPWP]"
                   + ",RWNPWP AS [RW NPWP]"
                   + ",KecamatanNPWP AS [Kecamatan NPWP]"
                   + ",KabupatenNPWP AS [Kabupaten NPWP]"
                   + ",PropinsiNPWP AS [Propinsi NPWP]"
                   + ",KodePosNPWP AS [Kode Pos NPWP]"
                   + " FROM REF_DATA "
                   + " WHERE Project = '" + project.SelectedValue + "'"
                   );
        }
    }
}

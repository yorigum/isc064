using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.KPA
{
    public partial class KontrakSP3KEdit : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!IsPostBack)
            {
                nokontrak.Attributes["ondblclick"] = "popDaftarKontrak('a&kpr=1');";

                if (Request.QueryString["NoKontrak"] != null)
                {
                    nokontrak.Text = Request.QueryString["NoKontrak"];
                    LoadKontrak();
                }
                else
                {
                    Js.Focus(this, nokontrak);
                    frm.Visible = false;
                }

                nilai.Attributes["onfocus"] = "tempnum=CalcFocus(this);tempx=this.value";
                nilai.Attributes["onkeyup"] = "CalcType(this,tempnum);";
                nilai.Attributes["onblur"] = "CalcBlur(this);";

                tambahum.Attributes["onfocus"] = "tempnum=CalcFocus(this);tempx=this.value";
                tambahum.Attributes["onkeyup"] = "CalcType(this,tempnum);";
                tambahum.Attributes["onblur"] = "CalcBlur(this);";

                tgljtum.Text = Cf.Day(DateTime.Today.AddDays(1));
            }

            FeedBack();
            if (frm.Visible) Js.Confirm(this, "Lanjutkan proses pencatatan aktivitas Edit SP3K?");

        }

        private void LoadKontrak()
        {
            if (valid())
            {
                pilih.Visible = false;
                frm.Visible = true;

                Fill();

                Js.Focus(this, ok);
                Js.Confirm(this, "Lanjutkan proses pencatatan aktivitas Edit SP3K ?");
            }
            else
            {
                Js.Focus(this, nokontrak);
                frm.Visible = false;
            }
        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "<a href=\"javascript:popEditSP3K('" + Request.QueryString["done"] + "')\">"
                        + "Input Berhasil..."
                        + "</a>";
            }
        }

        private bool valid()
        {
            bool x = true;

            int c = Db.SingleInteger("SELECT COUNT(*) FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "' AND Status = 'A' and CaraBayar = 'KPR'");

            if (c == 0)
                x = false;

            int a = Db.SingleInteger("SELECT COUNT(*) FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "' AND StatusWawancara = 'SELESAI'");

            if (a == 0)
                x = false;

            decimal Pengajuan = Db.SingleDecimal("SELECT NilaiPengajuan FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
            decimal Approv = 0;
            decimal TambahUm = 0;

            if (!Cf.isEmpty(nilai))
            {
                if (!Cf.isMoney(nilai))
                {
                    x = false;
                    nilaic.Text = "Format Angka";
                }
                else
                {
                    Approv = Convert.ToDecimal(nilai.Text);
                }
            }
            if (!Cf.isEmpty(tambahum))
            {
                if (!Cf.isMoney(tambahum))
                {
                    x = false;
                    tgljtumc.Text = "Format Angka";
                }
                else
                {
                    TambahUm = Convert.ToDecimal(tambahum.Text);
                }
            }


            if (!x)
                Js.Alert(
                    this
                    , "Kontrak Tidak Valid.\\n\\n"
                    + "Kemungkinan Sebab :\\n"
                    + "1. Kontrak tersebut tidak terdaftar.\\n"
                    + "2. Kontrak tersebut sudah dibatalkan.\\n"
                    + "3. Kontrak tersebut bukan KPR.\\n"
                    + "4. Kontrak tersebut belum melalui proses wawancara.\\n"
                    , "document.getElementById('nokontrak').focus();"
                    + "document.getElementById('nokontrak').select();"
                    );

            return x;
        }

        protected void next_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                pilih.Visible = false;
                frm.Visible = true;

                Fill();

                Js.Focus(this, nokontrak);
                Js.Confirm(this, "Lanjutkan proses pencatatan aktivitas Edit SP3K?");
            }
        }

        protected void Fill()
        {
            if (Request.QueryString["NoKontrak"] != null)
            {
                cancel.Attributes["onclick"] = "window.close()";
                pageof.Visible = false;
            }
            else
            {
                cancel.Attributes["onclick"] = "location.href='KontrakSP3KEdit.aspx?'";
            }

            string strSql = "SELECT "
                + " MS_KONTRAK.*"
                + ",MS_CUSTOMER.Nama AS Cs"
                + " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
                + " WHERE MS_KONTRAK.NoKontrak = '" + NoKontrak + "'";

            DataTable rsHeader = Db.Rs(strSql);

            if (rsHeader.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                kontrakno.Text = rsHeader.Rows[0]["NoKontrak"].ToString();
                unit.Text = rsHeader.Rows[0]["NoUnit"].ToString();
                customer.Text = rsHeader.Rows[0]["Cs"].ToString();

                tbTgl.Text = Cf.Day(rsHeader.Rows[0]["TglHasilSP3K"]);
                tbTarget.Text = Cf.Day(rsHeader.Rows[0]["TargetSP3K"]);
                tbPengajuan.Text = Cf.Day(rsHeader.Rows[0]["TglPengajuanSP3K"]);
                tbNoSP3K.Text = rsHeader.Rows[0]["NoSP3K"].ToString();
                tbKet.Text = rsHeader.Rows[0]["KetSP3K"].ToString();

                nilaipengajuan.Text = Cf.Num(rsHeader.Rows[0]["NilaiPengajuan"]);
                nilai.Text = Cf.Num(rsHeader.Rows[0]["ApprovalKPR"]);
                tambahum.Text = Cf.Num(rsHeader.Rows[0]["NilaiKelebihanKPA"]);

                if (rsHeader.Rows[0]["HasilSP3K"].ToString() == "TOLAK")
                    rblHasil.SelectedIndex = 0;
                else if (rsHeader.Rows[0]["HasilSP3K"].ToString() == "SETUJU")
                    rblHasil.SelectedIndex = 1;
                else if (rsHeader.Rows[0]["HasilSP3K"].ToString() == "SETUJU SEBAGIAN")
                    rblHasil.SelectedIndex = 2;

                if (rsHeader.Rows[0]["StatusSP3K"].ToString() == "")
                {
                    rblStatus.SelectedIndex = 0;
                    dijadwalkan.Visible = false;
                    diajukan.Visible = false;
                    selesai.Visible = false;
                }
                else if (rsHeader.Rows[0]["StatusSP3K"].ToString() == "DIJADWALKAN")
                {
                    rblStatus.SelectedIndex = 1;
                    dijadwalkan.Visible = true;
                    diajukan.Visible = false;
                    selesai.Visible = false;
                }
                else if (rsHeader.Rows[0]["StatusSP3K"].ToString() == "DIAJUKAN")
                {
                    rblStatus.SelectedIndex = 2;
                    dijadwalkan.Visible = true;
                    diajukan.Visible = true;
                    selesai.Visible = false;
                }
                else if (rsHeader.Rows[0]["StatusSP3K"].ToString() == "SELESAI")
                {
                    rblStatus.SelectedIndex = 3;
                    dijadwalkan.Visible = true;
                    diajukan.Visible = true;
                    selesai.Visible = true;
                }
            }
        }

        private bool Valid()
        {
            bool x = true;
            string s = "";

            if (dijadwalkan.Visible)
            {
                if (!Cf.isTgl(tbTarget))
                {
                    x = false;

                    if (s == "")
                        s = tbTarget.ID;

                    lblTarget.Text = "Tanggal";
                }
                else
                    lblTarget.Text = "";
            }

            if (diajukan.Visible)
            {
                if (!Cf.isTgl(tbPengajuan))
                {
                    x = false;

                    if (s == "")
                        s = tbPengajuan.ID;

                    lblPengajuan.Text = "Tanggal";
                }
                else
                    lblPengajuan.Text = "";
            }

            if (selesai.Visible)
            {
                if (!Cf.isTgl(tbTgl))
                {
                    x = false;

                    if (s == "")
                        s = tbTgl.ID;

                    lblTgl.Text = "Tanggal";
                }
                else
                    lblTgl.Text = "";

                if (!Cf.isMoney(nilai))
                {
                    x = false;

                    if (s == "")
                        s = nilai.ID;

                    nilaic.Text = "Angka";
                }
                else
                    nilaic.Text = "";

                if (!Cf.isMoney(tambahum))
                {
                    x = false;

                    if (s == "")
                        s = tambahum.ID;

                    tambahumc.Text = "Angka";
                }
                else
                    tambahumc.Text = "";

                if (!Cf.isTgl(tgljtum))
                {
                    x = false;

                    if (s == "")
                        s = tgljtum.ID;

                    tgljtumc.Text = "Tanggal";
                }
                else
                    tgljtumc.Text = "";

                if (Convert.ToDecimal(nilai.Text) > Convert.ToDecimal(nilaipengajuan.Text))
                {
                    x = false;
                    nilaic.Text = "Nilai Disetujui harus lebih kecil dari Nilai Pengajuan";
                }
                else
                {
                    nilaic.Text = "";
                }

                if (Convert.ToDecimal(tambahum.Text) > Convert.ToDecimal(nilaipengajuan.Text))
                {
                    x = false;
                    tambahumc.Text = "Tambahan Uang Muka harus lebih kecil dari Nilai Pengajuan";
                }
                else
                {
                    tambahumc.Text = "";
                }
            }

            //if (!x)
            //{
            //    this.RegisterStartupScript(
            //        "focusScript"
            //        , "<script language='javascript' type='text/javascript'>"
            //        + "document.getElementById('" + s + "').focus();"
            //        + "</script>"
            //        );
            //}

            return x;
        }

        private void Save()
        {
            if (valid())
            {
                string Status = "";
                if (rblStatus.SelectedIndex != 0)
                    Status = rblStatus.SelectedItem.Text;

                string Hasil = "";
                if (Status == "TIDAK PERLU")
                    Hasil = Status;

                DataTable rsBef = Db.Rs("SELECT "
                    + "StatusSP3K AS [Status SP3K]"
                    + ", TargetSP3K AS [Target SP3K]"
                    + ", TglPengajuanSP3K AS [Tgl. Pengajuan SP3K]"
                    + ", TglHasilSP3K AS [Tgl. Hasil SP3K]"
                    + ", NoSP3K AS [No. SP3K]"
                    + ", HasilSP3K AS [Hasil SP3K]"
                    + ", KetSP3K AS [Keterangan SP3K]"
                    + ", ApprovalKPR AS [Nilai KPR Disetujui]"
                    + ", NilaiKelebihanKPA AS [Tambah Uang Muka KPR]"
                    + " FROM MS_KONTRAK"
                    + " WHERE NoKontrak = '" + NoKontrak + "'"
                    );

                Db.Execute("UPDATE MS_KONTRAK"
                    + " SET StatusSP3K = '" + Status + "'"
                    + ", TargetSP3K = NULL"
                    + ", TglPengajuanSP3K = NULL"
                    + ", TglHasilSP3K = NULL"
                    + ", NoSP3K = ''"
                    + ", HasilSP3K = '" + Hasil + "'"
                    + ", KetSP3K = ''"
                    + " WHERE NoKontrak = '" + NoKontrak + "'"
                    );

                if (dijadwalkan.Visible)
                {
                    DateTime Target = Convert.ToDateTime(tbTarget.Text);
                    Hasil = "MENUNGGU";

                    Db.Execute("UPDATE MS_KONTRAK"
                        + " SET TargetSP3K = '" + Target + "'"
                        + ", TglPengajuanSP3K = NULL"
                        + ", TglHasilSP3K = NULL"
                        + ", NoSP3K = ''"
                        + ", HasilSP3K = '" + Hasil + "'"
                        + ", KetSP3K = ''"
                        + " WHERE NoKontrak = '" + NoKontrak + "'"
                        );
                }

                if (diajukan.Visible)
                {
                    DateTime TglPengajuan = Convert.ToDateTime(tbPengajuan.Text);
                    Hasil = "MENUNGGU";

                    Db.Execute("UPDATE MS_KONTRAK"
                        + " SET TglPengajuanSP3K = '" + TglPengajuan + "'"
                        + ", TglHasilSP3K = NULL"
                        + ", NoSP3K = ''"
                        + ", HasilSP3K = '" + Hasil + "'"
                        + ", KetSP3K = ''"
                        + " WHERE NoKontrak = '" + NoKontrak + "'"
                        );
                }

                if (selesai.Visible)
                {
                    DateTime Tgl = Convert.ToDateTime(tbTgl.Text);
                    string NoSP3K = Cf.Str(tbNoSP3K.Text);
                    Hasil = rblHasil.SelectedItem.Text;
                    string Ket = Cf.Str(tbKet.Text);
                    decimal Nilai = Convert.ToDecimal(nilai.Text);

                    Db.Execute("UPDATE MS_KONTRAK"
                        + " SET TglHasilSP3K = '" + Tgl + "'"
                        + ", NoSP3K = '" + NoSP3K + "'"
                        + ", HasilSP3K = '" + Hasil + "'"
                        + ", KetSP3K = '" + Ket + "'"
                        + ", ApprovalKPR = " + Nilai
                        + " WHERE NoKontrak = '" + NoKontrak + "'"
                        );

                    //// ---  dari pustaka 
                    decimal TambahUM = Db.SingleDecimal("SELECT NilaiPengajuan FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'") - Nilai;
                    DataTable rs = Db.Rs("SELECT TOP 1 * FROM MS_TAGIHAN WHERE Tipe = 'DP' AND NamaTagihan LIKE '%TAMBAHAN UANG MUKA%' AND NoKontrak = '" + NoKontrak + "'");

                    if (TambahUM > 0)
                    {
                        Db.Execute("EXEC spTagihanDaftar "
                            + " '" + NoKontrak + "'"
                            + ", 'TAMBAHAN UANG MUKA'"
                            + ", '" + Convert.ToDateTime(tgljtum.Text) + "'"
                            + ", " + TambahUM
                            + ", 'DP'"
                            );
                        Db.Execute("UPDATE MS_KONTRAK SET NilaiKelebihanKPA=0 WHERE NoKontrak='" + NoKontrak + "'");
                    }
                    else if (TambahUM < 0)
                    {
                        Db.Execute("UPDATE MS_KONTRAK SET NilaiKelebihanKPA=" + (TambahUM * -1) + " WHERE NoKontrak='" + NoKontrak + "'");
                    }
                    Db.Execute("UPDATE MS_TAGIHAN SET NilaiTagihan = " + Nilai + " FROM ISC064_MARKETINGJUAL..MS_TAGIHAN WHERE NoKontrak='" + NoKontrak + "' AND KPR=1");
                   
                }

                DataTable rsAft = Db.Rs("SELECT "
                    + "StatusSP3K AS [Status SP3K]"
                    + ", TargetSP3K AS [Target SP3K]"
                    + ", TglPengajuanSP3K AS [Tgl. Pengajuan SP3K]"
                    + ", TglHasilSP3K AS [Tgl. Hasil SP3K]"
                    + ", NoSP3K AS [No. SP3K]"
                    + ", HasilSP3K AS [Hasil SP3K]"
                    + ", KetSP3K AS [Keterangan SP3K]"
                    + ", ApprovalKPR AS [Nilai KPR Disetujui]"
                    + ", NilaiKelebihanKPA AS [Tambah Uang Muka KPR]"
                    + " FROM MS_KONTRAK"
                    + " WHERE NoKontrak = '" + NoKontrak + "'"
                    );

                //Logfile
                string Log = Cf.LogCompare(rsBef, rsAft);

                Db.Execute("EXEC spLogKontrak"
                    + " 'KPASP3'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + Log + "'"
                    + ",'" + NoKontrak + "'"
                    );

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
                string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);
                Db.Execute("Exec spTagihanBalance '" + NoKontrak + "'");
                if (Request.QueryString["NoKontrak"] != null)
                    this.RegisterStartupScript(
                    "focusScript"
                    , "<script language='javascript' type='text/javascript'>"
                    + "window.close();"
                    + "</script>"
                    );
                else
                    Response.Redirect("KontrakSP3KEdit.aspx?done=" + NoKontrak);
            }
        }
        protected void ok_Click(object sender, System.EventArgs e)
        {
            if (Valid())
                Save();
        }

        protected void rblStatus_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (rblStatus.SelectedItem.Text == "DIJADWALKAN")
            {
                dijadwalkan.Visible = true;
                diajukan.Visible = false;
                selesai.Visible = false;
            }
            else if (rblStatus.SelectedItem.Text == "DIAJUKAN")
            {
                dijadwalkan.Visible = true;
                diajukan.Visible = true;
                selesai.Visible = false;
            }
            else if (rblStatus.SelectedItem.Text == "SELESAI")
            {
                dijadwalkan.Visible = true;
                diajukan.Visible = true;
                selesai.Visible = true;
            }
            else
            {
                dijadwalkan.Visible = false;
                diajukan.Visible = false;
                selesai.Visible = false;
            }
        }

        private string NoKontrak
        {
            get
            {
                return Cf.Pk(nokontrak.Text);
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

        //protected void nilai_TextChanged(object sender, EventArgs e)
        //{

        //    string strSql = "SELECT "
        //        + " MS_KONTRAK.*"
        //        + ",MS_CUSTOMER.Nama AS Cs"
        //        + " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
        //        + " WHERE MS_KONTRAK.NoKontrak = '" + NoKontrak + "'";

        //    DataTable rsHeader = Db.Rs(strSql);

        //    if (rsHeader.Rows.Count == 0)
        //        Response.Redirect("/CustomError/Deleted.html");
        //    else
        //    {
        //        decimal NilaiPengajuan = (decimal)rsHeader.Rows[0]["NilaiPengajuan"];
        //        if (Cf.isMoney(nilai))
        //        {
        //            decimal Nilai = 0;
        //            Nilai = Convert.ToDecimal(nilai.Text);
        //            if (NilaiPengajuan > Nilai)
        //            {
        //                tambahum.Text = Cf.Num(NilaiPengajuan - Nilai);
        //            }
        //            else
        //            {
        //                tambahum.Text = Cf.Num(0);
        //            }
        //        }
        //    }
        //}
    }
}

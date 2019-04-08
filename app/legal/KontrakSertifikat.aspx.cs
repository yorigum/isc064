using System;
using System.Collections.Generic;
//using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ISC064.LEGAL
{
    public partial class KontrakSertifikat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            //Js.Confirm(this, "Lanjutkan dengan proses edit Sertifikat?");
            //Js.Focus(this, ok);

            if (!Page.IsPostBack)
            {
                backbtn.Visible = false;
                nokontrak.Attributes["ondblclick"] = "popDaftarKontrak('a&st=1');";
                //Fill();
                jangkawaktu.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
                jangkawaktu.Attributes["onkeyup"] = "CalcType(this,tempnum);";
                jangkawaktu.Attributes["onblur"] = "CalcBlur(this);";

                if (Request.QueryString["NoKontrak"] != null)
                {
                    dariReminder.Checked = true;
                    nokontrak.Text = Request.QueryString["NoKontrak"];
                    LoadKontrak();
                    cancel.Attributes["onclick"] = "location.href='ReminderST.aspx'";
                }
                else
                {
                    Js.Focus(this, nokontrak);
                    frm.Visible = false;
                }
            }
            //Js.Confirm(this, "Lanjutkan dengan proses edit Sertifikat?");
            //Js.Focus(this, save);
            FeedBack();
            if (frm.Visible) Js.Confirm(this, "Lanjutkan dengan proses edit Sertifikat?");
        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "<a href=\"javascript:popKontrakSertifikatEdit('" + Request.QueryString["done"] + "')\">"
                        + "Registrasi Sertifikat Berhasil..."
                        + "</a>";
            }
        }

        private bool validKontrak()
        {
            bool x = true;

            int c = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "' AND Status = 'A'");// AND ST <> 'D'");

            if (c == 0)
                x = false;

            int a = Db.SingleInteger(
                        "SELECT COUNT(*) FROM MS_SERTIFIKAT WHERE NoKontrak = '" + NoKontrak + "' AND StatusSertifikat = 'T'");
            if (a > 0)
                x = false;

            if (!x)
                Js.Alert(
                    this
                    , "Kontrak Tidak Valid.\\n\\n"
                    + "Kemungkinan Sebab :\\n"
                    + "1. Kontrak tersebut tidak terdaftar.\\n"
                    + "2. Kontrak tersebut sudah dibatalkan.\\n"
                    + "3. Prosedur serah terima sudah dijalankan.\\n"
                    , "document.getElementById('nokontrak').focus();"
                    + "document.getElementById('nokontrak').select();"
                    );

            return x;
        }

        private void LoadKontrak()
        {
            if (validKontrak())
            {
                pilih.Visible = false;
                frm.Visible = true;

                //InitForm();
                Fill();
                //Js.Focus(this, luas);
                //Js.Confirm(this, "Lanjutkan proses serah terima unit properti?");
            }
            else
            {
                backbtn.Visible = true;
                Js.Focus(this, nokontrak);
                frm.Visible = false;
            }
        }

        protected void next_Click(object sender, System.EventArgs e)
        {
            if (validKontrak())
            {
                pilih.Visible = false;
                frm.Visible = true;

                //InitForm();
                Fill();
                //Js.Focus(this, luas);
                //Js.Confirm(this, "Lanjutkan proses serah terima unit properti?");
                //Js.Confirm(this, "Lanjutkan dengan proses edit Sertifikat?");
                //Js.Focus(this, save);
            }
        }

        protected void Fill()
        {

            //cancel.Attributes["onclick"] = "location.href='KontrakProses.aspx?NoKontrak=" + NoKontrak + "'";


            //string strSql = "SELECT "
            //+ " A.NoKontrak"
            //+ ", A.NoUnit"
            //+ ", C.TglSertifikat"
            //+ ", C.KetSertifikat"
            //+ ", C.JangkaWaktu"
            //+ ", C.NoSertifikat"
            //+ ",B.Nama AS Cs"
            //+ " FROM MS_KONTRAK A INNER JOIN MS_CUSTOMER B ON A.NoCustomer = B.NoCustomer"
            //+ " LEFT JOIN MS_SERTIFIKAT C ON A.NoKontrak = C.NoKontrak"
            //+ " WHERE A.NoKontrak = '" + NoKontrak + "'";
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
                nokontrak2.Text = rsHeader.Rows[0]["NoKontrak"].ToString();
                unit.Text = rsHeader.Rows[0]["NoUnit"].ToString();
                customer.Text = rsHeader.Rows[0]["Cs"].ToString();
                //tbTgl.Text = Cf.Day(rsHeader.Rows[0]["TglSertifikat"]);
                //tglakhir.Text = Cf.Day(rsHeader.Rows[0]["TglAkhir"]);
                //statussertifikat.Text = rsHeader.Rows[0]["StatusHak"].ToString();
                //tbNoSertifikat.Text = rsHeader.Rows[0]["NoSertifikat"].ToString();
                //jangkawaktu.Text = Cf.Num(rsHeader.Rows[0]["JangkaWaktu"]).ToString();
                //tbKeterangan.Text = rsHeader.Rows[0]["KetSertifikat"].ToString();
            }

        }

        private bool valid()
        {

            string s = "";
            bool x = true;

            if (!Cf.isMoney(jangkawaktu))
            {
                x = false;
                if (s == "") s = jangkawaktu.ID;
                jangkawaktuc.Text = "Angka";
            }

            if (!Cf.isTgl(tbTgl))
            {
                x = false;

                if (s == "")
                    s = tbTgl.ID;

                lblTgl.Text = "Tanggal";
            }
            else
                lblTgl.Text = "";

            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Format Tanggal : Bulan / Tanggal / Tahun.\\n"
                    + "2. Nama tidak boleh kosong.\\n"
                    + "3. Luas Lama harus berupa angka.\\n"
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );

            return x;
        }

        private void Save()
        {
            int c = Db.SingleInteger("SELECT COUNT(*) FROM MS_SERTIFIKAT WHERE NoKontrak = '" + NoKontrak + "'");
            if (c == 0)
            {
                DataTable rsBef = Db.Rs("SELECT "
                + " StatusSertifikat AS [Status Sertifikat]"
                + ", TglSertifikat AS [Tgl. Sertifikat]"
                + ", TglAkhir AS [Tgl. Berakhir Sertifikat]"
                + ", StatusHak AS [Status Hak]"
                + ", NoSertifikat AS [No. Sertifikat]"
                + ", KetSertifikat AS [Keterangan Sertifikat]"
                + ", JangkaWaktu AS [Jangka Waktu]"
                + " FROM MS_SERTIFIKAT"
                + " WHERE NoKontrak = '" + NoKontrak + "'"
                );

                DateTime Tgl = Convert.ToDateTime(tbTgl.Text);
                DateTime TglAkhir = Convert.ToDateTime(tglakhir.Text);
                string NoSertifikat = Cf.Str(tbNoSertifikat.Text);
                string ket = Cf.Str(tbKeterangan.Text);
                string status = "T"; // B = Belum, S = Target, D = Proses, T = Sertifikat

                Db.Execute("INSERT INTO MS_SERTIFIKAT (NoKontrak,NoSertifikat,StatusSertifikat,KetSertifikat,JangkaWaktu,TglSertifikat, StatusHak, TglAkhir) VALUES ('" + NoKontrak + "','" + NoSertifikat + "','" + status + "','" + ket + "','" + Convert.ToInt32(jangkawaktu.Text) + "','" + Tgl + "','" + statussertifikat.SelectedValue + "','" + TglAkhir + "')");

                //Logfile
                DataTable rs = Db.Rs("SELECT"
                                        + " A.NoKontrak AS [No. Kontrak]"
                                        + ",B.NoUnit AS [Unit]"
                                        + ",C.Nama AS [Customer]"
                                        + ",CONVERT(varchar, A.TglTargetSertifikat, 106) AS [Tanggal Target Sertifikat]"
                                        + ",CONVERT(varchar, A.TglProsesSertifikat, 106) AS [Tanggal Proses Sertifikat]"
                                        + ",CONVERT(varchar, A.TglAkhir, 106) AS [Tanggal Berakhir Sertifikat]"
                                        + ", A.NoSertifikat AS [No. Sertifikat]"
                                        + ", A.JangkaWaktu AS [Jangka Waktu Sertifikat]"
                                        + ",PersenLunas AS [Prosentase Pelunasan]"
                                        + ", case when A.StatusSertifikat='S' then 'Target Sertifikat' when A.StatusSertifikat='D' then 'Proses Sertifikat' when A.StatusSertifikat='B' then 'Belum Sertifikat' else 'Registrasi Sertifikat' end as [Status Sertifikat]"
                                        + ", case when A.StatusHak='0' then 'HGB' else 'Hak Milik' end as [Status Sertifikat Tanah]"
                                        + " FROM MS_Sertifikat A INNER JOIN MS_KONTRAK B"
                                        + " ON A.NoKontrak = B.NoKontrak"
                                        + " INNER JOIN MS_CUSTOMER C ON B.NoCustomer = C.NoCustomer"
                                        + " WHERE A.NoKontrak = '" + NoKontrak + "'");

                string ketlog = Cf.LogCapture(rs)
                    ;

                Db.Execute("EXEC spLogKontrak "
                    + " 'STT'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + ketlog + "'"
                    + ",'" + NoKontrak + "'"
                    );

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
                string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

            }
            else
            {
                DataTable rsBef = Db.Rs("SELECT "
                + " StatusSertifikat AS [Status Sertifikat]"
                + ", TglSertifikat AS [Tgl. Sertifikat]"
                + ", TglAkhir AS [Tgl. Berakhir Sertifikat]"
                + ", StatusHak AS [Status Hak]"
                + ", NoSertifikat AS [No. Sertifikat]"
                + ", KetSertifikat AS [Keterangan Sertifikat]"
                + ", JangkaWaktu AS [Jangka Waktu]"
                + " FROM MS_SERTIFIKAT"
                + " WHERE NoKontrak = '" + NoKontrak + "'"
                );

                //DateTime Tgl = Convert.ToDateTime(tbTgl.Text);
                string NoSertifikat = Cf.Str(tbNoSertifikat.Text);
                string ket = Cf.Str(tbKeterangan.Text);
                string status = "T"; // B = Belum, S = Target, D = Proses, T = Sertifikat

                Db.Execute("UPDATE MS_SERTIFIKAT"
                + " SET NoSertifikat = '" + NoSertifikat + "'"
                + ", StatusSertifikat = '" + status + "'"
                + ", KetSertifikat = '" + Cf.Str(tbKeterangan.Text) + "'"
                + ", JangkaWaktu = '" + Convert.ToInt32(jangkawaktu.Text) + "'"
                + ", StatusHak = '" + statussertifikat.SelectedValue + "'"
                + " WHERE NoKontrak = '" + NoKontrak + "'"
                );

               
                if (tbTgl.Text != "")
                {
                    Db.Execute("UPDATE MS_SERTIFIKAT SET TglSertifikat='" + Convert.ToDateTime(tbTgl.Text) + "' WHERE NoKontrak = '" + NoKontrak + "'");
                }
                if (tglakhir.Text != "")
                {
                    Db.Execute("UPDATE MS_SERTIFIKAT SET TglAkhir='" + Convert.ToDateTime(tglakhir.Text) + "' WHERE NoKontrak = '" + NoKontrak + "'");
                }

                //Logfile
                DataTable rs = Db.Rs("SELECT"
                    + " A.NoKontrak AS [No. Kontrak]"
                    + ",B.NoUnit AS [Unit]"
                    + ",C.Nama AS [Customer]"
                    + ",CONVERT(varchar, A.TglTargetSertifikat, 106) AS [Tanggal Target Sertifikat]"
                    + ",CONVERT(varchar, A.TglProsesSertifikat, 106) AS [Tanggal Proses Sertifikat]"
                    + ",CONVERT(varchar, A.TglAkhir, 106) AS [Tanggal Berakhir Sertifikat]"
                    + ", A.NoSertifikat AS [No. Sertifikat]"
                    + ", A.JangkaWaktu AS [Jangka Waktu Sertifikat]"
                    + ",PersenLunas AS [Prosentase Pelunasan]"
                    + ", case when A.StatusSertifikat='S' then 'Target Sertifikat' when A.StatusSertifikat='D' then 'Proses Sertifikat' when A.StatusSertifikat='B' then 'Belum Sertifikat' else 'Registrasi Sertifikat' end as [Status Sertifikat]"
                    + ", case when A.StatusHak='0' then 'HGB' else 'Hak Milik' end as [Status Sertifikat Tanah]"
                    + " FROM MS_Sertifikat A INNER JOIN MS_KONTRAK B"
                    + " ON A.NoKontrak = B.NoKontrak"
                    + " INNER JOIN MS_CUSTOMER C ON B.NoCustomer = C.NoCustomer"
                    + " WHERE A.NoKontrak = '" + NoKontrak + "'");

                string ketlog = Cf.LogCapture(rs)
                    ;

                Db.Execute("EXEC spLogKontrak "
                    + " 'STT'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + ketlog + "'"
                    + ",'" + NoKontrak + "'"
                    );

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
                string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

            }
            Response.Redirect("KontrakSertifikat.aspx?done=" + NoKontrak);
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

        protected void save_Click(object sender, EventArgs e)
        {
            Save();
        }
    }
}
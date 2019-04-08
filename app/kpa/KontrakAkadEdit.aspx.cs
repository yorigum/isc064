using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.KPA
{
    public partial class KontrakAkadEdit : System.Web.UI.Page
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
            }
            FeedBack();
            if (frm.Visible) Js.Confirm(this, "Lanjutkan proses pencatatan aktivitas Edit Akad?");

        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "<a href=\"javascript:popEditAkad('" + Request.QueryString["done"] + "')\">"
                        + "Input Berhasil..."
                        + "</a>";
            }
        }

        private void LoadKontrak()
        {
            if (valid())
            {
                pilih.Visible = false;
                frm.Visible = true;

                Fill();

                Js.Focus(this, ok);
                Js.Confirm(this, "Lanjutkan proses pencatatan aktivitas Edit Akad?");
            }
            else
            {
                Js.Focus(this, nokontrak);
                frm.Visible = false;
            }
        }

        private bool valid()
        {
            bool x = true;

            int c = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "' AND Status = 'A' and CaraBayar = 'KPR'");

            if (c == 0)
                x = false;

            int a = Db.SingleInteger("SELECT COUNT(*) FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "' AND StatusSP3K = 'SELESAI'");

            if (a == 0)
                x = false;


            if (!x)
                Js.Alert(
                    this
                    , "Kontrak Tidak Valid.\\n\\n"
                    + "Kemungkinan Sebab :\\n"
                    + "1. Kontrak tersebut tidak terdaftar.\\n"
                    + "2. Kontrak tersebut sudah dibatalkan.\\n"
                    + "3. Kontrak tersebut bukan KPR.\\n"
                    + "4. Kontrak tersebut belum melalui proses SP3K sampai selesai.\\n"
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
                Js.Confirm(this, "Lanjutkan proses pencatatan aktivitas Edit Akad?");
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
                cancel.Attributes["onclick"] = "location.href='KontrakAkadEdit.aspx'";
            }


            DataTable rsRetensi = Db.Rs(
                "SELECT * FROM REF_RETENSI"
                );

            for (int i = 0; i < rsRetensi.Rows.Count; i++)
            {
                retensi.Items.Add(new ListItem(rsRetensi.Rows[i]["Kode"].ToString() + "-" + rsRetensi.Rows[i]["NamaKategori"].ToString()
                    , rsRetensi.Rows[i]["Kode"].ToString()));
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
                retensi.SelectedValue = (rsHeader.Rows[0]["RetensiKPA"].ToString() != "") ? rsHeader.Rows[0]["RetensiKPA"].ToString() : "0";
                tbTgl.Text = Cf.Day(rsHeader.Rows[0]["TglAkad"]);
                tbTarget.Text = Cf.Day(rsHeader.Rows[0]["TargetAkad"]);
                tbNoAkad.Text = rsHeader.Rows[0]["NoAkad"].ToString();
                tbKet.Text = rsHeader.Rows[0]["KetAkad"].ToString();

                string a = Db.SingleString("SELECT HasilSP3K FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");

                if (a == "SETUJU" || a == "SETUJU SEBAGIAN")
                {
                    nilai.Text = Cf.Num(rsHeader.Rows[0]["ApprovalKPR"]);
                }
                else
                {
                    nilai.Text = "0";
                }

                if (rsHeader.Rows[0]["StatusAkad"].ToString() == "")
                {
                    rblStatus.SelectedIndex = 0;
                    dijadwalkan.Visible = false;
                    selesai.Visible = false;
                }
                else if (rsHeader.Rows[0]["StatusAkad"].ToString() == "DIJADWALKAN")
                {
                    rblStatus.SelectedIndex = 1;
                    dijadwalkan.Visible = true;
                    selesai.Visible = false;
                }
                else if (rsHeader.Rows[0]["StatusAkad"].ToString() == "SELESAI")
                {
                    rblStatus.SelectedIndex = 2;
                    dijadwalkan.Visible = true;
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
            }

            if (!x)
            {
                this.RegisterStartupScript(
                    "focusScript"
                    , "<script language='javascript' type='text/javascript'>"
                    + "document.getElementById('" + s + "').focus();"
                    + "</script>"
                    );
            }

            return x;
        }

        private void Save()
        {
            string Status = "";
            if (rblStatus.SelectedIndex != 0)
                Status = rblStatus.SelectedItem.Text;

            DataTable rsBef = Db.Rs("SELECT "
                + "StatusAkad AS [Status Akad]"
                + ", TargetAkad AS [Target Akad]"
                + ", TglAkad AS [Tgl. Akad]"
                + ", NoAkad AS [No. Akad]"
                + ", KetAkad AS [Keterangan Akad]"
                + ", RealisasiAkad AS [Realisasi Akad]"
                + " FROM MS_KONTRAK"
                + " WHERE NoKontrak = '" + NoKontrak + "'"
                );

            Db.Execute("UPDATE MS_KONTRAK"
                + " SET StatusAkad = '" + Status + "'"
                + ", TargetAkad = NULL"
                + ", NoAkad = ''"
                + ", TglAkad = NULL"
                + ", KetAkad = ''"
                + ", RetensiKPA='" + retensi.SelectedValue.ToString() + "'"
                + " WHERE NoKontrak = '" + NoKontrak + "'"
                );

            if (dijadwalkan.Visible)
            {
                DateTime Target = Convert.ToDateTime(tbTarget.Text);

                Db.Execute("UPDATE MS_KONTRAK"
                    + " SET TargetAkad = '" + Target + "'"
                    + ", NoAkad = ''"
                    + ", TglAkad = NULL"
                    + ", KetAkad = ''"
                    + " WHERE NoKontrak = '" + NoKontrak + "'"
                    );
            }

            if (selesai.Visible)
            {
                DateTime Tgl = Convert.ToDateTime(tbTgl.Text);
                string Ket = Cf.Str(tbKet.Text);
                string NoAkad = Cf.Str(tbNoAkad.Text);
                decimal Nilai = Convert.ToDecimal(nilai.Text);

                Db.Execute("UPDATE MS_KONTRAK"
                    + " SET NoAkad = '" + NoAkad + "'"
                    + ", TglAkad = '" + Tgl + "'"
                    + ", KetAkad = '" + Ket + "'"
                    + ", RealisasiAkad = " + Nilai
                    + " WHERE NoKontrak = '" + NoKontrak + "'"
                    );

                //Settlement PAkad
                Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_PAKAD"
                    + " SET Status = 'S'"
                    + " WHERE Ref = '" + NoKontrak + "'"
                    + " AND Status = 'A'"
                    );

                //bentuk tagihan kpa buat di master kpa berdasarkan persentase retensi
                int cek = Db.SingleInteger("SELECT COUNT(*) FROM MS_TAGIHAN_KPA WHERE NoKontrak = '" + NoKontrak + "'");
                if (cek == 0)
                {
                    string Project2 = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                    string Bank = Db.SingleString("SELECT BankKPR FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                    DataTable rs2 = Db.Rs("SELECT * FROM REF_BANKKPA WHERE Project  = '" + Project2 + "' AND Bank = '" + Bank + "'");
                    for (int i = 0; i < rs2.Rows.Count; i++)
                    {
                        decimal nilai2 = Nilai / 100 * Convert.ToDecimal(rs2.Rows[i]["Nilai"].ToString());
                        Db.Execute("EXEC spTagihanDaftarKPA"
                        + " '" + NoKontrak + "'"
                        + ",'" + rs2.Rows[i]["Kode"].ToString() + "'"
                        + ",'" + Tgl + "'"
                        + "," + nilai2
                        + ",'" + rs2.Rows[i]["Kode"].ToString() + "'"
                        );
                    }
                }
            }

            DataTable rsAft = Db.Rs("SELECT "
                + "StatusAkad AS [Status Akad]"
                + ", TargetAkad AS [Target Akad]"
                + ", TglAkad AS [Tgl. Akad]"
                + ", NoAkad AS [No. Akad]"
                + ", KetAkad AS [Keterangan Akad]"
                + ", RealisasiAkad AS [Realisasi Akad]"
                + " FROM MS_KONTRAK"
                + " WHERE NoKontrak = '" + NoKontrak + "'"
                );

            //Logfile
            string Log = Cf.LogCompare(rsBef, rsAft);

            Db.Execute("EXEC spLogKontrak"
                + " 'KPAAKD'"
                + ",'" + Act.UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'" + Log + "'"
                + ",'" + NoKontrak + "'"
                );

            decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
            string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
            Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

            DataTable rsDetail = Db.Rs("SELECT"
                        + " MS_KONTRAK.NoKontrak AS [No. Kontrak]"
                        + ",MS_KONTRAK.NoUnit AS [Unit]"
                        + ",MS_CUSTOMER.Nama AS [Customer]"
                        + ",MS_KONTRAK.NilaiKontrak AS [Nilai Kontrak]"
                        + ",MS_KONTRAK.Skema AS [Skema]"
                        + " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER"
                        + " ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
                        + " WHERE MS_KONTRAK.NoKontrak = '" + NoKontrak + "'");

            DataTable rsTagihan = Db.Rs("SELECT "
                        + "CONVERT(VARCHAR,NoUrut) + '.   ' + NamaTagihan + ' ('+Tipe+')   ' + CONVERT(VARCHAR,TglJT,106) + '   ' + CONVERT(VARCHAR,NilaiTagihan,1) "
                        + "FROM MS_TAGIHAN_KPA WHERE NoKontrak = '" + NoKontrak + "' ORDER BY NoUrut");

            string Keterangan = Cf.LogCapture(rsDetail)
                + "<br>---REGIS TAGIHAN KPA---<br>"
                + Cf.LogList(rsTagihan, "JADWAL TAGIHAN KPA");

            Db.Execute("EXEC spLogKontrak"
                + " 'REGKPA'"
                + ",'" + Act.UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'" + Keterangan + "'"
                + ",'" + NoKontrak + "'"
                );

            if (Request.QueryString["NoKontrak"] != null)
                this.RegisterStartupScript(
                "focusScript"
                , "<script language='javascript' type='text/javascript'>"
                + "window.close();"
                + "</script>"
                );
            else
                Response.Redirect("KontrakAkadEdit.aspx?done=" + NoKontrak);
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
                selesai.Visible = false;
            }
            else if (rblStatus.SelectedItem.Text == "SELESAI")
            {
                dijadwalkan.Visible = true;
                selesai.Visible = true;
            }
            else
            {
                dijadwalkan.Visible = false;
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

    }
}

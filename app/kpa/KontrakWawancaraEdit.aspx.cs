using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.KPA
{
    public partial class KontrakWawancaraEdit : System.Web.UI.Page
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

                nilaipengajuan.Attributes["onfocus"] = "tempnum=CalcFocus(this);tempx=this.value";
                nilaipengajuan.Attributes["onkeyup"] = "CalcType(this,tempnum);";
                nilaipengajuan.Attributes["onblur"] = "CalcBlur(this);";

                Bind();
            }

            FeedBack();
            if (frm.Visible) Js.Confirm(this, "Lanjutkan proses pencatatan aktivitas Edit Wawancara?");
        }

        private void Bind()
        {
            string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
            DataTable rs = Db.Rs("SELECT * FROM REF_BANKKPA WHERE Project = '" + Project + "'");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["KodeBank"].ToString();
                string t = v + " : " + rs.Rows[i]["Bank"];
                ddlAcc.Items.Add(new ListItem(t, v));
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
                Js.Confirm(this, "Lanjutkan proses pencatatan aktivitas Edit Wawancara?");
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
                        + "<a href=\"javascript:popEditWawancara('" + Request.QueryString["done"] + "')\">"
                        + "Input Berhasil..."
                        + "</a>";
            }
        }

        private bool valid()
        {
            bool x = true;

            int c = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "' AND Status = 'A' and CaraBayar = 'KPR'");

            if (c == 0)
                x = false;


            if (!x)
                Js.Alert(
                    this
                    , "Kontrak Tidak Valid.\\n\\n"
                    + "Kemungkinan Sebab :\\n"
                    + "1. Kontrak tersebut tidak terdaftar.\\n"
                    + "2. Kontrak tersebut sudah dibatalkan.\\n"
                    + "3. Kontrak tersebut bukan KPR.\\n"
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
                Js.Confirm(this, "Lanjutkan proses pencatatan aktivitas Edit Wawancara?");
            }
        }

        protected void Fill()
        {
            if (Request.QueryString["NoKontrak"] != null)
            {
                cancel.Attributes["onclick"] = "window.close()";
                pageof.Visible = false;
                Js.Focus(this, ddlAcc);
            }
            else
            {
                ddlAcc.Items.Clear();
                ddlAcc.Items.Add(new ListItem("- Pilih Bank KPA -"));
                Bind();
                cancel.Attributes["onclick"] = "location.href='KontrakWawancaraEdit.aspx'";
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

                tbTgl.Text = Cf.Day(rsHeader.Rows[0]["TglWawancara"]);
                tbTarget.Text = Cf.Day(rsHeader.Rows[0]["TargetWawancara"]);

                //nilaipengajuan.Text = Cf.Num(rsHeader.Rows[0]["NilaiPengajuan"]);
                decimal NilaiPengajuan = 0;
                if (Convert.ToDecimal(rsHeader.Rows[0]["NilaiPengajuan"]) == 0)
                {
                    NilaiPengajuan = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiTagihan), 0) FROM MS_TAGIHAN"
                        + " WHERE NoKontrak = '" + NoKontrak + "' AND Tipe = 'ANG' AND KPR = 1"
                        );
                }
                else
                    NilaiPengajuan = Convert.ToDecimal(rsHeader.Rows[0]["NilaiPengajuan"]);
                nilaipengajuan.Text = Cf.Num(NilaiPengajuan);

                //if(rsHeader.Rows[0]["LokasiWawancara"].ToString() != "")
                //{
                //	if(rsHeader.Rows[0]["LokasiWawancara"].ToString() == "ON THE SPOT")
                //		rblLokasi.SelectedIndex = 0;
                //	else
                //		rblLokasi.SelectedIndex = 1;
                //}
                tbLokasi.Text = rsHeader.Rows[0]["LokasiWawancara"].ToString();
                tbKet.Text = rsHeader.Rows[0]["KetWawancara"].ToString();

                if (rsHeader.Rows[0]["StatusWawancara"].ToString() == "")
                {
                    rblStatus.SelectedIndex = 0;
                    dijadwalkan.Visible = false;
                    selesai.Visible = false;
                }
                else if (rsHeader.Rows[0]["StatusWawancara"].ToString() == "DIJADWALKAN")
                {
                    rblStatus.SelectedIndex = 1;
                    dijadwalkan.Visible = true;
                    selesai.Visible = false;
                }
                else if (rsHeader.Rows[0]["StatusWawancara"].ToString() == "SELESAI")
                {
                    rblStatus.SelectedIndex = 2;
                    dijadwalkan.Visible = true;
                    selesai.Visible = true;
                }
                if (rsHeader.Rows[0]["BankKPR"].ToString() != "")
                {
                    string Bank = Db.SingleString("SELECT Bank FROM REF_BANKKPA WHERE KodeBank='" + rsHeader.Rows[0]["BankKPR"] + "'");
                    ddlAcc.Items.Add(new ListItem("Tidak berubah: " + Bank, rsHeader.Rows[0]["BankKPR"].ToString()));
                    ddlAcc.SelectedIndex = ddlAcc.Items.Count - 1;
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


                if (!Cf.isMoney(nilaipengajuan))
                {
                    x = false;

                    if (s == "")
                        s = nilaipengajuan.ID;

                    nilaipengajuanc.Text = "Angka";
                }
                else
                    nilaipengajuanc.Text = "";
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
            }

            if (ddlAcc.SelectedIndex == 0)
            {
                x = false;
                lblBank.Text = "Pilih";
            }
            else
            {
                lblBank.Text = "";
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
            string Bank = "";
            if (ddlAcc.SelectedIndex != 0)
                Bank = ddlAcc.SelectedValue;


            DataTable rsBef = Db.Rs("SELECT "
                + "BankKPR AS [Bank KPA]"
                + ", StatusWawancara AS [Status Wawancara]"
                + ", TargetWawancara AS [Target Wawancara]"
                + ", TglWawancara AS [Tgl. Wawancara]"
                + ", LokasiWawancara AS [Lokasi Wawancara]"
                + ", KetWawancara AS [Keterangan Wawancara]"
                + ", NilaiPengajuan AS [Nilai Pengajuan]"
                + " FROM MS_KONTRAK"
                + " WHERE NoKontrak = '" + NoKontrak + "'"
                );

            Db.Execute("UPDATE MS_KONTRAK"
                + " SET StatusWawancara = '" + Status + "'"
                + ", TargetWawancara = NULL"
                + ", LokasiWawancara = ''"
                + ", TglWawancara = NULL"
                + ", KetWawancara = ''"
                + ", BankKPR = '" + Bank + "'"
                + " WHERE NoKontrak = '" + NoKontrak + "'"
                );

            if (dijadwalkan.Visible)
            {
                DateTime Target = Convert.ToDateTime(tbTarget.Text);
                //string Lokasi = rblLokasi.SelectedItem.Text;
                string Lokasi = tbLokasi.Text;
                decimal NilaiPengajuan = Convert.ToDecimal(nilaipengajuan.Text);

                Db.Execute("UPDATE MS_KONTRAK"
                    + " SET TargetWawancara = '" + Target + "'"
                    + ", LokasiWawancara = '" + Lokasi + "'"
                    + ", TglWawancara = NULL"
                    + ", KetWawancara = ''"
                    + ", NilaiPengajuan = " + NilaiPengajuan
                    + " WHERE NoKontrak = '" + NoKontrak + "'"
                    );
            }

            if (selesai.Visible)
            {
                DateTime Tgl = Convert.ToDateTime(tbTgl.Text);
                string Ket = Cf.Str(tbKet.Text);

                Db.Execute("UPDATE MS_KONTRAK"
                    + " SET TglWawancara = '" + Tgl + "'"
                    + ", KetWawancara = '" + Ket + "'"
                    + " WHERE NoKontrak = '" + NoKontrak + "'"
                    );

                //Settlement SPW
                Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_PWAWANCARA"
                    + " SET Status = 'S'"
                    + " WHERE Ref = '" + NoKontrak + "'"
                    + " AND Status = 'A'"
                    );
            }

            DataTable rsAft = Db.Rs("SELECT "
                + "BankKPR AS [Bank KPA]"
                + ", StatusWawancara AS [Status Wawancara]"
                + ", TargetWawancara AS [Target Wawancara]"
                + ", TglWawancara AS [Tgl. Wawancara]"
                + ", LokasiWawancara AS [Lokasi Wawancara]"
                + ", KetWawancara AS [Keterangan Wawancara]"
                + ", NilaiPengajuan AS [Nilai Pengajuan]"
                + " FROM MS_KONTRAK"
                + " WHERE NoKontrak = '" + NoKontrak + "'"
                );

            //Logfile
            string Log = Cf.LogCompare(rsBef, rsAft);

            Db.Execute("EXEC spLogKontrak"
                + " 'KPAWAN'"
                + ",'" + Act.UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'" + Log + "'"
                + ",'" + NoKontrak + "'"
                );

            decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
            string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
            Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

            if (Request.QueryString["NoKontrak"] != null)
                this.RegisterStartupScript(
                "focusScript"
                , "<script language='javascript' type='text/javascript'>"
                + "window.close();"
                + "</script>"
                );
            else
                Response.Redirect("KontrakWawancaraEdit.aspx?done=" + NoKontrak);

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

using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LEGAL
{
    public partial class KontrakAJB : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                backbtn.Visible = false;
                nokontrak.Attributes["ondblclick"] = "popDaftarKontrak('a&ajb=1');";

                if (Request.QueryString["NoKontrak"] != null)
                {
                    dariReminder.Checked = true;
                    nokontrak.Text = Request.QueryString["NoKontrak"].Replace("'", "");
                    LoadKontrak();

                    cancel.Attributes["onclick"] = "location.href='ReminderAJB.aspx'";
                }
                else
                {
                    Js.Focus(this, nokontrak);
                    frm.Visible = false;
                }
            }

            FeedBack();
            if (frm.Visible) Js.Confirm(this, "Lanjutkan proses pencatatan aktivitas AJB?");
        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "<a href=\"javascript:popKontrakAJBEdit('" + Request.QueryString["done"] + "')\">"
                        + "AJB Berhasil..."
                        + "</a>";
            }
        }

        private bool valid()
        {
            bool x = true;

            int c = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "' AND Status = 'A'");// AND AJB <> 'D'");

            if (c == 0)
                x = false;

            int a = Db.SingleInteger(
                     "SELECT COUNT(*) FROM MS_AJB WHERE NoKontrak = '" + NoKontrak + "' AND NoAJB !='' AND (AJB = 'D' OR AJB = 'T')");// AND ST <> 'D'");
            if (a > 0)
                x = false;

            if (!x)
                Js.Alert(
                    this
                    , "Kontrak Tidak Valid.\\n\\n"
                    + "Kemungkinan Sebab :\\n"
                    + "1. Kontrak tersebut tidak terdaftar.\\n"
                    + "2. Kontrak tersebut sudah dibatalkan.\\n"
                    + "3. Prosedur AJB sudah dijalankan.\\n"
                    , "document.getElementById('nokontrak').focus();"
                    + "document.getElementById('nokontrak').select();"
                    );

            return x;
        }

        private void LoadKontrak()
        {
            if (valid())
            {
                pilih.Visible = false;
                frm.Visible = true;

                Fill();

                Js.Focus(this, save);
                Js.Confirm(this, "Lanjutkan proses pencatatan aktivitas AJB?");
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
            if (valid())
            {
                pilih.Visible = false;
                frm.Visible = true;

                Fill();

                Js.Focus(this, save);
                Js.Confirm(this, "Lanjutkan proses pencatatan aktivitas AJB?");
            }
        }

        private void Fill()
        {
            nilaibiaya.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            nilaibiaya.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            nilaibiaya.Attributes["onblur"] = "CalcBlur(this);";

            Func.KontrakHeader(NoKontrak, nokontrakl, unit, customer, agent);

            string strSql = "SELECT * FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'";
            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                tglajb.Text = Cf.Day(DateTime.Today);
                persenlunas.Text = Cf.Num(rs.Rows[0]["PersenLunas"]);

                string ParamID = "FormatAJB" + rs.Rows[0]["Project"];
                string a = Db.SingleString("SELECT Value FROM [ISC064_SECURITY].[dbo].[REF_PARAM] WHERE ParamID = '" + ParamID + "'");
                decimal minajb = Convert.ToDecimal(a);
                if ((decimal)rs.Rows[0]["PersenLunas"] < minajb)
                    lunasinfo.Text = "PELUNASAN BELUM MENCAPAI " + minajb + "%";
            }
        }

        private bool datavalid()
        {
            string s = "";
            bool x = true;

            if (!Cf.isTgl(tglajb))
            {
                x = false;
                if (s == "") s = tglajb.ID;
                tglajbc.Text = "Tanggal";
            }
            else
                tglajbc.Text = "";


            if (!Cf.isMoney(nilaibiaya))
            {
                x = false;
                if (s == "") s = nilaibiaya.ID;
                nilaibiayac.Text = "Angka";
            }
            else
                nilaibiayac.Text = "";

            decimal lunas = Db.SingleDecimal("SELECT PersenLunas FROM MS_KONTRAK WHERE NoKontrak ='" + NoKontrak + "'");
            string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
            string ParamID = "FormatAJB" + Project;

            string a = Db.SingleString("SELECT Value FROM [ISC064_SECURITY].[dbo].[REF_PARAM] WHERE ParamID = '" + ParamID + "'");
            decimal minajb = Convert.ToDecimal(a);

            if (lunas < minajb)
            {
                x = false;
                lunasinfo.Text = "PELUNASAN BELUM MENCAPAI " + minajb + "%";
            }
            else
            {
                lunasinfo.Text = "";
            }

            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Format Tanggal : Bulan / Tanggal / Tahun.\\n"
                    + "2. Biaya Administrasi harus berupa angka.\\n"
                    + "3. Pelunasan Belum Mencapai " + minajb + "%"
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );

            return x;
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            if (datavalid())
            {
                int c = Db.SingleInteger("SELECT COUNT(*) FROM MS_AJB WHERE NoKontrak = '" + NoKontrak + "'");
                DateTime TglAJB = Convert.ToDateTime(tglajb.Text);
                string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                string status = "D"; // B = Belum, S = Target, D = AJB, T = Tanda Tangan

                if (c == 0)
                {
                    string NoAJB = Db.SingleString("SELECT NoAJB FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");

                    if (NoAJB == "")
                        NoAJB = Numerator.AJB(TglAJB.Month, TglAJB.Year, Project);

                    Db.Execute("EXEC spAJB "
                        + " '" + NoKontrak + "'"
                        + ",'" + NoAJB + "'"
                        + ",'" + TglAJB + "'"
                        );

                    Db.Execute("UPDATE MS_AJB SET Project = '" + Project + "' WHERE NoAJB = '" + NoAJB + "'");



                    Db.Execute("UPDATE MS_AJB SET"
                             + " AJBu=" + ajbused.SelectedValue
                             + " ,NoAJBm='" + noajbm.Text + "'"
                             + " ,AJB='" + status + "'"
                             + ",TglAJB='" + tglajb.Text + "'"
                             + ",NamaNotaris='" + notaris.Text + "'"
                             + ",KetAJB ='" + keterangan.Text + "'"
                             + ",Biaya ='" + nilaibiaya.Text + "'"
                             + " WHERE NoKontrak = '" + NoKontrak + "'"
                               );

                    Db.Execute("UPDATE MS_KONTRAK SET AJB = 'D',TglAJB='" + tglajb.Text + "' WHERE NoKontrak = '" + NoKontrak + "'");
                    decimal NilaiBiaya = Convert.ToDecimal(nilaibiaya.Text);
                    if (NilaiBiaya != 0)
                    {
                        Db.Execute("EXEC spTagihanDaftar "
                            + " '" + NoKontrak + "'"
                            + ",'BIAYA ADM. AJB'"
                            + ",'" + TglAJB + "'"
                            + ", " + NilaiBiaya
                            + ",'ADM'"
                            );

                        int NoUrut = Db.SingleInteger("SELECT TOP 1 NoUrut FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' ORDER BY NoUrut DESC");
                        Db.Execute("UPDATE MS_TAGIHAN SET Jenis = 'AJB' WHERE NoKontrak = '" + NoKontrak + "' AND NoUrut = " + NoUrut);

                    }
                    DataTable rs = Db.Rs("SELECT"
                           + " A.NoKontrak AS [No. Kontrak]"
                           + ",B.NoUnit AS [Unit]"
                           + ",C.Nama AS [Customer]"
                           + ",CONVERT(varchar, A.TglTargetAJB, 106) AS [Tanggal Target AJB]"
                           + ",CONVERT(varchar, A.TglAJB, 106) AS [Tanggal AJB]"
                           + ",PersenLunas AS [Prosentase Pelunasan]"
                           + ",A.Biaya"
                           + ",A.KetAJB"
                           + ", case when A.AJB='S' then 'Target AJB' when A.AJB='D' then 'AJB' when A.AJB='B' then 'Belum AJB' else 'Tanda Tangan AJB' end as [Status AJB]"
                           + " FROM MS_AJB A INNER JOIN MS_KONTRAK B"
                           + " ON A.NoKontrak = B.NoKontrak"
                           + " INNER JOIN MS_CUSTOMER C ON B.NoCustomer = C.NoCustomer"
                           + " WHERE A.NoKontrak = '" + NoKontrak + "'");
                                    
                    string ket = Cf.LogCapture(rs)
                        + "<br>Biaya Administrasi : " + Cf.Num(NilaiBiaya)
                        ;

                    Db.Execute("EXEC spLogKontrak "
                         + " 'AJB'"
                         + ",'" + Act.UserID + "'"
                         + ",'" + Act.IP + "'"
                         + ",'" + ket + "'"
                         + ",'" + NoKontrak + "'"
                         );
                    decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
                    Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                }
                else
                {                

                    string NoAJB = Db.SingleString("SELECT NoAJB FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                    if (NoAJB == "")
                        NoAJB = Numerator.AJB(TglAJB.Month, TglAJB.Year, Project);

                    Db.Execute("UPDATE MS_AJB SET"
                             + " AJBu=" + ajbused.SelectedValue
                             + ",NoAJB= '" + NoAJB + "'"
                             + ",NoAJBm='" + noajbm.Text + "'"
                             + ",TglAJB='" + tglajb.Text + "'"
                             + ",AJB='" + status + "'"
                             + ",NamaNotaris='" + notaris.Text + "'"
                             + ",KetAJB ='" + keterangan.Text + "'"
                             + ",Biaya =" + Convert.ToDecimal(nilaibiaya.Text)
                             + ",Project = '" + Project + "'"
                             + " WHERE NoKontrak = '" + NoKontrak + "'"
                               );

                    decimal NilaiBiaya = Convert.ToDecimal(nilaibiaya.Text);
                    if (NilaiBiaya != 0)
                    {
                        Db.Execute("EXEC spTagihanDaftar "
                            + " '" + NoKontrak + "'"
                            + ",'BIAYA ADM. AJB'"
                            + ",'" + TglAJB + "'"
                            + ", " + NilaiBiaya
                            + ",'ADM'"
                            );

                        int NoUrut = Db.SingleInteger("SELECT TOP 1 NoUrut FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' ORDER BY NoUrut DESC");
                        Db.Execute("UPDATE MS_TAGIHAN SET Jenis = 'AJB' WHERE NoKontrak = '" + NoKontrak + "' AND NoUrut = " + NoUrut);

                    }
                    Db.Execute("UPDATE MS_KONTRAK SET AJB = 'D',TglAJB='" + tglajb.Text + "' WHERE NoKontrak = '" + NoKontrak + "'");
                    DataTable rs = Db.Rs("SELECT"
                             + " A.NoKontrak AS [No. Kontrak]"
                             + ",B.NoUnit AS [Unit]"
                             + ",C.Nama AS [Customer]"
                             + ",CONVERT(varchar, A.TglTargetAJB, 106) AS [Tanggal Target AJB]"
                             + ",CONVERT(varchar, A.TglAJB, 106) AS [Tanggal AJB]"
                             + ",PersenLunas AS [Prosentase Pelunasan]"
                             + ",A.Biaya"
                             + ",A.KetAJB"
                             + ", case when A.AJB='S' then 'Target AJB' when A.AJB='D' then 'AJB' when A.AJB='B' then 'Belum AJB' else 'Tanda Tangan AJB' end as [Status AJB]"
                             + " FROM MS_AJB A INNER JOIN MS_KONTRAK B"
                             + " ON A.NoKontrak = B.NoKontrak"
                             + " INNER JOIN MS_CUSTOMER C ON B.NoCustomer = C.NoCustomer"
                             + " WHERE A.NoKontrak = '" + NoKontrak + "'");

                    string ket = Cf.LogCapture(rs)
                        + "<br>Biaya Administrasi : " + Cf.Num(NilaiBiaya)
                        ;

                    Db.Execute("EXEC spLogKontrak "
                         + " 'AJB'"
                         + ",'" + Act.UserID + "'"
                         + ",'" + Act.IP + "'"
                         + ",'" + ket + "'"
                         + ",'" + NoKontrak + "'"
                         );
                    decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
                    Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                }
                if (dariReminder.Checked)
                    Response.Redirect("ReminderAJB.aspx?done=" + NoKontrak + "&project=" + Project);
                else
                    Response.Redirect("KontrakAJB.aspx?done=" + NoKontrak);
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

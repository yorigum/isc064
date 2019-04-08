using System;
using System.Collections.Generic;
//using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ISC064.LEGAL
{
    public partial class KontrakSertifikatProses : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Act.Pass();
        Act.NoCache();

        if (!Page.IsPostBack)
        {
            backbtn.Visible = false;
            nokontrak.Attributes["ondblclick"] = "popDaftarKontrak('a&st=1');";

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
                    + "Proses Sertifikat Berhasil..."
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
                        "SELECT COUNT(*) FROM MS_SERTIFIKAT WHERE NoKontrak = '" + NoKontrak + "' AND (StatusSertifikat='D' OR StatusSertifikat='T')");
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


            string strSql = "SELECT "
                + " A.NoKontrak"
                + ", A.NoUnit"
                + ", C.TglProsesSertifikat"
                + ", C.NoSertifikat"
                + ", C.StatusHak"
                + ", C.JangkaWaktu"
                + ", C.TglAkhir"
                + ",B.Nama AS Cs"
                + " FROM MS_KONTRAK A INNER JOIN MS_CUSTOMER B ON A.NoCustomer = B.NoCustomer"
                + " LEFT JOIN MS_SERTIFIKAT C ON A.NoKontrak = C.NoKontrak"
                + " WHERE A.NoKontrak = '" + NoKontrak + "'";


            DataTable rsHeader = Db.Rs(strSql);

            if (rsHeader.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                nokontrak2.Text = rsHeader.Rows[0]["NoKontrak"].ToString();
                unit.Text = rsHeader.Rows[0]["NoUnit"].ToString();
                customer.Text = rsHeader.Rows[0]["Cs"].ToString();
                try
                {
                    tbTgl.Text = Cf.Day(rsHeader.Rows[0]["TglProsesSertifikat"]);
                }
                catch { }
        }
        }

    private bool valid()
    {

        string s = "";
        bool x = true;

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
            string status = "D"; // B = Belum, S = Target, D = Proses, T = Sertifikat
            if (c == 0)
            {

                DateTime Tgl = Convert.ToDateTime(tbTgl.Text);

                Db.Execute("INSERT INTO MS_SERTIFIKAT (NoKontrak,NoSertifikat,StatusSertifikat, TglProsesSertifikat)"
                    + "VALUES ('" + NoKontrak + "','','" + "','" + status + "','" + Tgl + "')");

                //Logfile
                DataTable rs = Db.Rs("SELECT"
                                       + " A.NoKontrak AS [No. Kontrak]"
                                       + ",B.NoUnit AS [Unit]"
                                       + ",C.Nama AS [Customer]"
                                       + ",CONVERT(varchar, A.TglTargetSertifikat, 106) AS [Tanggal Target Sertifikat]"
                                       + ",CONVERT(varchar, A.TglProsesSertifikat, 106) AS [Tanggal Proses Sertifikat]"
                                       + ", A.NoSertifikat AS [No. Sertifikat]"
                                       + ",PersenLunas AS [Prosentase Pelunasan]"
                                       + ", case when A.StatusSertifikat='S' then 'Target Sertifikat' when A.StatusSertifikat='D' then 'Proses Sertifikat' when A.StatusSertifikat='B' then 'Belum Sertifikat' else 'Registrasi Sertifikat' end as [Status Sertifikat]"
                                       + " FROM MS_Sertifikat A INNER JOIN MS_KONTRAK B"
                                       + " ON A.NoKontrak = B.NoKontrak"
                                       + " INNER JOIN MS_CUSTOMER C ON B.NoCustomer = C.NoCustomer"
                                       + " WHERE A.NoKontrak = '" + NoKontrak + "'");

                string ket = Cf.LogCapture(rs)
                    ;

                Db.Execute("EXEC spLogKontrak "
                    + " 'PRO-STT'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + ket + "'"
                    + ",'" + NoKontrak + "'"
                    );
                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
                string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

            }
            else
            {
                Db.Execute("UPDATE MS_SERTIFIKAT"
                + " SET StatusSertifikat = '" + status + "'"
                + " WHERE NoKontrak = '" + NoKontrak + "'"
                );

                if (tbTgl.Text != "")
                {
                    Db.Execute("UPDATE MS_SERTIFIKAT SET TglProsesSertifikat ='" + Convert.ToDateTime(tbTgl.Text) + "' WHERE NoKontrak = '" + NoKontrak + "'");
                }
                //Logfile
                DataTable rs = Db.Rs("SELECT"
                                        + " A.NoKontrak AS [No. Kontrak]"
                                        + ",B.NoUnit AS [Unit]"
                                        + ",C.Nama AS [Customer]"
                                        + ",CONVERT(varchar, A.TglTargetSertifikat, 106) AS [Tanggal Target Sertifikat]"
                                        + ",CONVERT(varchar, A.TglProsesSertifikat, 106) AS [Tanggal Proses Sertifikat]"
                                        + ", A.NoSertifikat AS [No. Sertifikat]"
                                        + ",PersenLunas AS [Prosentase Pelunasan]"
                                        + ", case when A.StatusSertifikat='S' then 'Target Sertifikat' when A.StatusSertifikat='D' then 'Proses Sertifikat' when A.StatusSertifikat='B' then 'Belum Sertifikat' else 'Registrasi Sertifikat' end as [Status Sertifikat]"
                                        + " FROM MS_Sertifikat A INNER JOIN MS_KONTRAK B"
                                        + " ON A.NoKontrak = B.NoKontrak"
                                        + " INNER JOIN MS_CUSTOMER C ON B.NoCustomer = C.NoCustomer"
                                        + " WHERE A.NoKontrak = '" + NoKontrak + "'");

                string ket = Cf.LogCapture(rs)
                    ;

                Db.Execute("EXEC spLogKontrak "
                    + " 'PRO-STT'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + ket + "'"
                    + ",'" + NoKontrak + "'"
                    );
                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
                string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

            }

            Response.Redirect("KontrakSertifikatProses.aspx?done=" + NoKontrak);
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
using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LEGAL
{

    public partial class KontrakAJBTarget : System.Web.UI.Page
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
                    nokontrak.Text = Request.QueryString["NoKontrak"];
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
                     "SELECT COUNT(*) FROM MS_AJB WHERE NoKontrak = '" + NoKontrak + "' AND (AJB='S' or AJB='D' or AJB='T')");// AND ST <> 'D'");
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
            Func.KontrakHeader(NoKontrak, nokontrakl, unit, customer, agent);

            string strSql = "SELECT A.NoKontrak,A.PersenLunas,B.*"
            + " FROM MS_KONTRAK A LEFT JOIN MS_AJB B ON A.NoKontrak = B.NoKontrak "
            + "WHERE A.NoKontrak = '" + NoKontrak + "'";
            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                tgltarget.Text = Cf.Day(rs.Rows[0]["TglTargetAJB"]);

                persenlunas.Text = Cf.Num(rs.Rows[0]["PersenLunas"]);
                if ((decimal)rs.Rows[0]["PersenLunas"] < 100)
                    lunasinfo.Text = "PELUNASAN BELUM MENCAPAI 100%";
            }
        }

        private bool datavalid()
        {
            string s = "";
            bool x = true;

            if (!Cf.isTgl(tgltarget))
            {
                x = false;
                if (s == "") s = tgltarget.ID;
                tgltargetc.Text = "Tanggal";
            }
            else
                tgltargetc.Text = "";

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
                    + "2. Biaya Administrasi harus berupa angka."
                    + "3. Pelunasan Belum Mencapai " + minajb + "%"
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );

            return x;
        }

        private bool isUnique(string kodebaru)
        {
            bool x = true;

            int c = Db.SingleInteger("SELECT COUNT(*) FROM MS_AJB WHERE NoAJB = '" + kodebaru + "'");
            if (c != 0)
                x = false;

            return x;
        }

        private string AutoID()
        {
            string x = "";
            int c = Db.SingleInteger("SELECT COUNT(NoAJB) FROM MS_AJB "
                + " WHERE AJB = 'D'"
                );

            bool hasfound = false;
            while (!hasfound)
            {
                if (!Response.IsClientConnected) break;

                c++;
                x = "AJB/" + c.ToString().PadLeft(7, '0');

                if (isUnique(x)) hasfound = true;
            }

            return x;
        }

        private bool isUnique2(string kodebaru)
        {
            bool x = true;

            int d = Db.SingleInteger("SELECT COUNT(*) FROM MS_KONTRAK WHERE NoFPS = '" + kodebaru + "'");
            if (d != 0)
                x = false;

            return x;
        }

        private string AutoIDFPS()
        {
            string x = "";
            int d = Db.SingleInteger("SELECT COUNT(NoFPS) FROM MS_KONTRAK "
                + " WHERE AJB = 'D'"
                );
            d = d - 1;

            string status0 = "010";
            string tahun = DateTime.Today.Year.ToString().Substring(2, 2);

            bool hasfound = false;
            while (!hasfound)
            {
                if (!Response.IsClientConnected) break;

                d++;
                x = d.ToString().PadLeft(8, '0');

                if (isUnique2(x)) hasfound = true;
            }
            //default normal=> 0 0 0. 0 0 0 – 0 0 .0 0 0 0 0 0 0 0
            x = status0 + ".000" + "-" + tahun + "." + x;

            return x;
        }



        protected void save_Click(object sender, System.EventArgs e)
        {
            if (datavalid())
            {
                int c = Db.SingleInteger("SELECT COUNT(*) FROM MS_AJB WHERE NoKontrak = '" + NoKontrak + "'");
                string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                DateTime TglAJB = Convert.ToDateTime(tgltarget.Text);
                string status = "S"; // B = Belum, S = Target, D = AJB, T = Tanda Tangan

                if (c == 0)
                {
                    string NoAJB = Db.SingleString("SELECT NoAJB FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                    if (NoAJB == "")
                        NoAJB = Numerator.AJB(TglAJB.Month, TglAJB.Year, Project);

                    Db.Execute("EXEC spAJB "
                        + " '" + NoKontrak + "'"
                        + ",'" + NoAJB + "'"
                        + ",''"
                        );

                    Db.Execute("UPDATE MS_AJB SET Project = '" + Project + "' WHERE NoAJB = '" + NoAJB + "'");                   

                    Db.Execute("UPDATE MS_AJB SET"
                             //+ " AJBu=" + ajbused.SelectedValue
                             + " AJB='" + status + "'"
                             + " WHERE NoKontrak = '" + NoKontrak + "'"
                               );

                    if (tgltarget.Text != "")
                    {
                        Db.Execute("UPDATE MS_AJB SET TglTargetAJB='" + Convert.ToDateTime(tgltarget.Text) + "' WHERE NoKontrak = '" + NoKontrak + "'");
                    }
                    DataTable rs = Db.Rs("SELECT"
                      + " A.NoKontrak AS [No. Kontrak]"
                      + ",B.NoUnit AS [Unit]"
                      + ",C.Nama AS [Customer]"
                      + ",CONVERT(varchar, A.TglTargetAJB, 106) AS [Tanggal Target AJB]"
                      + ",PersenLunas AS [Prosentase Pelunasan]"
                      + ", case when A.AJB='S' then 'Target AJB' when A.AJB='D' then 'AJB' when A.AJB='B' then 'Belum AJB' else 'Tanda Tangan AJB' end as [Status AJB]"
                      + " FROM MS_AJB A INNER JOIN MS_KONTRAK B"
                      + " ON A.NoKontrak = B.NoKontrak"
                      + " INNER JOIN MS_CUSTOMER C ON B.NoCustomer = C.NoCustomer"
                      + " WHERE A.NoKontrak = '" + NoKontrak + "'");

                    string ket = Cf.LogCapture(rs)
                        ;

                    Db.Execute("EXEC spLogKontrak "
                        + " 'T-AJB'"
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
                    Db.Execute("UPDATE MS_AJB SET"
                             + " AJB='" + status + "'"
                             + " WHERE NoKontrak = '" + NoKontrak + "'"
                               );

                    if (tgltarget.Text != "")
                    {
                        Db.Execute("UPDATE MS_AJB SET TglTargetAJB='" + Convert.ToDateTime(tgltarget.Text) + "' WHERE NoKontrak = '" + NoKontrak + "'");
                    }


                    DataTable rs = Db.Rs("SELECT"
                     + " A.NoKontrak AS [No. Kontrak]"
                     + ",B.NoUnit AS [Unit]"
                     + ",C.Nama AS [Customer]"
                     + ",CONVERT(varchar, A.TglTargetAJB, 106) AS [Tanggal Target AJB]"
                     + ",PersenLunas AS [Prosentase Pelunasan]"
                     + ", case when A.AJB='S' then 'Target AJB' when A.AJB='D' then 'AJB' when A.AJB='B' then 'Belum AJB' else 'Tanda Tangan AJB' end as [Status AJB]"
                     + " FROM MS_AJB A INNER JOIN MS_KONTRAK B"
                     + " ON A.NoKontrak = B.NoKontrak"
                     + " INNER JOIN MS_CUSTOMER C ON B.NoCustomer = C.NoCustomer"
                     + " WHERE A.NoKontrak = '" + NoKontrak + "'");

                    string ket = Cf.LogCapture(rs)
                        ;

                    Db.Execute("EXEC spLogKontrak "
                        + " 'T-AJB'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + ket + "'"
                        + ",'" + NoKontrak + "'"
                        );
                    decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
                    Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                }
                if (dariReminder.Checked)
                    Response.Redirect("ReminderAJB.aspx?done=" + NoKontrak);
                else
                    Response.Redirect("KontrakAJBTarget.aspx?done=" + NoKontrak);
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
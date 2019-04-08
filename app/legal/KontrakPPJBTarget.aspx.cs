using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
//using System.Web.Script.Serialization;
using System.Net;
using System.Net.Mail;
namespace ISC064.LEGAL
{

    public partial class KontrakPPJBTarget : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                backbtn.Visible = false;
                nokontrak.Attributes["ondblclick"] = "popDaftarKontrak('a&ppjb=1');";

                if (Request.QueryString["NoKontrak"] != null)
                {
                    dariReminder.Checked = true;
                    nokontrak.Text = Request.QueryString["NoKontrak"];
                    LoadKontrak();

                    cancel.Attributes["onclick"] = "location.href='ReminderPPJB.aspx'";
                }
                else
                {
                    Js.Focus(this, nokontrak);
                    frm.Visible = false;
                }
            }

            FeedBack();
            if (frm.Visible) Js.Confirm(this, "Lanjutkan proses pencatatan aktivitas PPJB?");
        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "<a href=\"javascript:popKontrakPPJBEdit('" + Request.QueryString["done"] + "')\">"
                        + "PPJB Berhasil..."
                        + "</a>";
            }
        }

        private bool valid()
        {
            bool x = true;

            int c = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "' AND Status = 'A'");// AND PPJB <> 'D'");

            if (c == 0)
                x = false;

            int a = Db.SingleInteger(
                     "SELECT COUNT(*) FROM MS_PPJB WHERE NoKontrak = '" + NoKontrak + "' AND (PPJB='T' or PPJB='D' or PPJB='S')");// AND ST <> 'D'");
            if (a > 0)
                x = false;

            if (!x)
                Js.Alert(
                    this
                    , "Kontrak Tidak Valid.\\n\\n"
                    + "Kemungkinan Sebab :\\n"
                    + "1. Kontrak tersebut tidak terdaftar.\\n"
                    + "2. Kontrak tersebut sudah dibatalkan.\\n"
                    + "3. Prosedur PPJB sudah dijalankan.\\n"
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
                Js.Confirm(this, "Lanjutkan proses pencatatan aktivitas PPJB?");
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
                Js.Confirm(this, "Lanjutkan proses pencatatan aktivitas PPJB?");
            }
        }

        private void Fill()
        {
            Func.KontrakHeader(NoKontrak, nokontrakl, unit, customer, agent);

            //string strSql = "SELECT * FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'";
            string strSql = "SELECT A.NoKontrak,A.PersenLunas, A.Project, B.*"
                + " FROM MS_KONTRAK A LEFT JOIN MS_PPJB B ON A.NoKontrak = B.NoKontrak "
                + "WHERE A.NoKontrak = '" + NoKontrak + "'";
            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                tgltarget.Text = Cf.Day(rs.Rows[0]["TglTargetPPJB"]);

                persenlunas.Text = Cf.Num(rs.Rows[0]["PersenLunas"]);

                string ParamID = "FormatPPJB" + rs.Rows[0]["Project"];

                string minppjb = Db.SingleString("SELECT ISNULL(Value,'') FROM [ISC064_SECURITY].[dbo].[REF_PARAM] WHERE ParamID = '" + ParamID + "'");

                if ((decimal)rs.Rows[0]["PersenLunas"] < Convert.ToDecimal(minppjb))
                    lunasinfo.Text = "PELUNASAN BELUM MENCAPAI " + minppjb.ToString() + "%";
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
            string project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
            string ParamID = "FormatPPJB" + project;

            string a = Db.SingleString("SELECT Value FROM [ISC064_SECURITY].[dbo].[REF_PARAM] WHERE ParamID = '" + ParamID + "'");
            decimal minppjb = Convert.ToDecimal(a);

            if (lunas <= minppjb)
            {
                x = false;
                lunasinfo.Text = "PELUNASAN BELUM MENCAPAI " + minppjb + "%";
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
                    + "3. Pelunasan Belum Mencapai " + minppjb + "%."
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );

            return x;
        }

        private bool isUnique(string kodebaru)
        {
            bool x = true;

            int c = Db.SingleInteger("SELECT COUNT(*) FROM MS_PPJB WHERE NoPPJB = '" + kodebaru + "'");
            if (c != 0)
                x = false;

            return x;
        }

        private string AutoID()
        {
            string x = "";
            int c = Db.SingleInteger("SELECT COUNT(NoPPJB) FROM MS_PPJB "
                + " WHERE PPJB = 'D'"
                );

            bool hasfound = false;
            while (!hasfound)
            {
                if (!Response.IsClientConnected) break;

                c++;
                //d++;
                x = "PPJB/" + c.ToString().PadLeft(5, '0'); /*+ "/DU/" + Act.UserID + "/AMT/PPJB." + d.ToString() + "/" + Cf.Roman(TglPPJB.Month) + "/" + TglPPJB.Year.ToString();*/

                if (isUnique(x)) hasfound = true;
            }

            return x;
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            if (datavalid())
            {
                DateTime TglPPJB = Convert.ToDateTime(tgltarget.Text);
                string status = "S"; // B = Belum, S = Target, D = PPJB, T = Tanda Tangan
                string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");

                int c = Db.SingleInteger("SELECT COUNT(*) FROM MS_PPJB WHERE NoKontrak = '" + NoKontrak + "'");
                if (c == 0)
                {
                    string NoPPJB = Db.SingleString("SELECT NoPPJB FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");

                    if (NoPPJB == "")
                        NoPPJB = Numerator.PPJB(TglPPJB.Month, TglPPJB.Year, Project);

                    Db.Execute("EXEC spPPJB "
                        + " '" + NoKontrak + "'"
                        + ",'" + NoPPJB + "'"
                        + ",''"
                        );

                    Db.Execute("UPDATE MS_PPJB SET Project = '" + Project + "' WHERE NoPPJB = '" + NoPPJB + "'");
                    
                    Db.Execute("UPDATE MS_PPJB"
                        + " SET PPJB = '" + status + "'"
                        + " WHERE NoKontrak = '" + NoKontrak + "'"
                        );

                    if (tgltarget.Text != "")
                    {
                        Db.Execute("UPDATE MS_PPJB SET TglTargetPPJB ='" + Convert.ToDateTime(tgltarget.Text) + "' WHERE NoKontrak = '" + NoKontrak + "'");
                    }
                    DataTable rs = Db.Rs("SELECT"
                       + " A.NoKontrak AS [No. Kontrak]"
                       + ",B.NoUnit AS [Unit]"
                       + ",C.Nama AS [Customer]"
                       + ",CONVERT(varchar, A.TglTargetPPJB, 106) AS [Tanggal Target PPJB]"
                       + ",PersenLunas AS [Prosentase Pelunasan]"
                       + ", case when A.PPJB='S' then 'Target PPJB' when A.PPJB='D' then 'PPJB' when A.PPJB='B' then 'Belum PPJB' else 'Tanda Tangan PPJB' end as [Status PPJB]"
                       + " FROM MS_PPJB A INNER JOIN MS_KONTRAK B"
                       + " ON A.NoKontrak = B.NoKontrak"
                       + " INNER JOIN MS_CUSTOMER C ON B.NoCustomer = C.NoCustomer"
                       + " WHERE A.NoKontrak = '" + NoKontrak + "'");

                    string ket = Cf.LogCapture(rs)
                        ;

                    Db.Execute("EXEC spLogKontrak "
                        + " 'T-PPJB'"
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
                    Db.Execute("UPDATE MS_PPJB SET"
                    + " PPJB='" + status + "'"
                    + " WHERE NoKontrak = '" + NoKontrak + "'"
                    );

                    if (tgltarget.Text != "")
                    {
                        Db.Execute("UPDATE MS_PPJB SET TglTargetPPJB ='" + Convert.ToDateTime(tgltarget.Text) + "' WHERE NoKontrak = '" + NoKontrak + "'");
                    }
                    DataTable rs = Db.Rs("SELECT"
                      + " A.NoKontrak AS [No. Kontrak]"
                      + ",B.NoUnit AS [Unit]"
                      + ",C.Nama AS [Customer]"
                      + ",CONVERT(varchar, A.TglTargetPPJB, 106) AS [Tanggal Target PPJB]"
                      + ",PersenLunas AS [Prosentase Pelunasan]"
                     + ", case when A.PPJB='S' then 'Target PPJB' when A.PPJB='D' then 'PPJB' when A.PPJB='B' then 'Belum PPJB' else 'Tanda Tangan PPJB' end as [Status PPJB]"
                      + " FROM MS_PPJB A INNER JOIN MS_KONTRAK B"
                      + " ON A.NoKontrak = B.NoKontrak"
                      + " INNER JOIN MS_CUSTOMER C ON B.NoCustomer = C.NoCustomer"
                      + " WHERE A.NoKontrak = '" + NoKontrak + "'");
                    string ket = Cf.LogCapture(rs)
                        ;

                    Db.Execute("EXEC spLogKontrak "
                      + " 'T-PPJB'"
                      + ",'" + Act.UserID + "'"
                      + ",'" + Act.IP + "'"
                      + ",'" + ket + "'"
                      + ",'" + NoKontrak + "'"
                      );
                    decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
                    Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                }

                if (dariReminder.Checked)
                    Response.Redirect("ReminderPPJB.aspx?done=" + NoKontrak);
                else
                    Response.Redirect("KontrakPPJBTarget.aspx?done=" + NoKontrak);
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
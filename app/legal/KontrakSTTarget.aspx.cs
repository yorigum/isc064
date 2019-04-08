using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LEGAL
{
    public partial class KontrakSTTarget : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
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
            if (frm.Visible) Js.Confirm(this, "Lanjutkan proses serah terima unit properti?");
        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "<a href=\"javascript:popKontrakSTEdit('" + Request.QueryString["done"] + "')\">"
                        + "Serah Terima Berhasil..."
                        + "</a>";
            }
        }

        private bool valid()
        {
            bool x = true;

            int c = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "' AND Status = 'A'");// AND ST <> 'D'");

            if (c == 0)
                x = false;

            int a = Db.SingleInteger(
                         "SELECT COUNT(*) FROM MS_BAST WHERE NoKontrak = '" + NoKontrak + "' AND (ST='S' OR ST='D' OR ST='T')");// AND ST <> 'D'");
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
            if (valid())
            {
                pilih.Visible = false;
                frm.Visible = true;

                InitForm();
                Fill();

                //Js.Focus(this, luas);
                Js.Confirm(this, "Lanjutkan proses serah terima unit properti?");
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

                InitForm();
                Fill();

                //Js.Focus(this, luas);
                Js.Confirm(this, "Lanjutkan proses serah terima unit properti?");
            }
        }

        private void InitForm()
        {
            //kalkulator
            //luas.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            //luas.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            //luas.Attributes["onblur"] = "CalcBlur(this);";

            //nilaibiaya.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            //nilaibiaya.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            //nilaibiaya.Attributes["onblur"] = "CalcBlur(this);";
        }

        private void Fill()
        {
            Func.KontrakHeader(NoKontrak, nokontrakl, unit, customer, agent);

            //string strSql = "SELECT *"
            //    + " FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'";
            string strSql = "SELECT A.NoKontrak,A.PersenLunas,B.*"
                    + " FROM MS_KONTRAK A LEFT JOIN MS_BAST B ON A.NoKontrak = B.NoKontrak "
                    + "WHERE A.NoKontrak = '" + NoKontrak + "'";
            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                //luas.Text = Cf.Num(rs.Rows[0]["Luas"]);
                tgltarget.Text = Cf.Day(rs.Rows[0]["TargetST"]);

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

            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Format Tanggal : Bulan / Tanggal / Tahun.\\n"
                    + "2. Luas harus berupa angka.\\n"
                    + "3. Biaya Administrasi harus berupa angka."
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );

            return x;
        }

        private bool isUnique(string kodebaru)
        {
            bool x = true;

            int c = Db.SingleInteger("SELECT COUNT(*) FROM MS_BAST WHERE NoST = '" + kodebaru + "'");
            if (c != 0)
                x = false;

            return x;
        }

        private string AutoID()
        {
            string x = "";
            int c = Db.SingleInteger("SELECT COUNT(NoST) FROM MS_BAST "
                + " WHERE ST = 'D'"
                );

            bool hasfound = false;
            while (!hasfound)
            {
                if (!Response.IsClientConnected) break;

                c++;
                x = c.ToString().PadLeft(7, '0');

                if (isUnique(x)) hasfound = true;
            }

            return x;
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            if (datavalid())
            {
                DateTime TglST = Convert.ToDateTime(tgltarget.Text);
                DateTime TargetST = Db.SingleTime("SELECT TargetST FROM MS_KONTRAK WHERE NoKontrak ='" + NoKontrak + "'");
                string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                int c = Db.SingleInteger("SELECT COUNT(*) FROM MS_BAST WHERE NoKontrak = '" + NoKontrak + "'");
                string status = "S"; // B = Belum, S = Target, D = Serah Terima, T = Tanda Tangan
                if (c == 0)
                {

                    string NoST = Db.SingleString("SELECT NoST FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                    if (NoST == "")
                        NoST = Numerator.BAST(TglST.Month, TglST.Year, Project);

                    Db.Execute("EXEC spST "
                        + " '" + NoKontrak + "'"
                        + ",'" + NoST + "'"
                        + ",''"
                        );

                    Db.Execute("UPDATE MS_BAST SET Project = '" + Project + "' WHERE NoST = '" + NoST + "'");

                    Db.Execute("UPDATE MS_BAST SET"
                             + " ST='" + status + "'"
                             + " WHERE NoKontrak = '" + NoKontrak + "'"
                               );

                    if (tgltarget.Text != "")
                    {
                        Db.Execute("UPDATE MS_BAST SET TargetST='" + Convert.ToDateTime(tgltarget.Text) + "' WHERE NoKontrak = '" + NoKontrak + "'");
                    }

                    DataTable rs = Db.Rs("SELECT"
                      + " A.NoKontrak AS [No. Kontrak]"
                      + ",B.NoUnit AS [Unit]"
                      + ",C.Nama AS [Customer]"
                      + ",CONVERT(varchar, A.TargetST, 106) AS [Tanggal Target BAST]"
                      + ",PersenLunas AS [Prosentase Pelunasan]"
                      + ", case when A.ST='S' then 'Target BAST' when A.ST='D' then 'BAST' when A.ST='B' then 'Belum BAST' else 'Tanda Tangan BAST' end as [Status BAST]"
                      + " FROM MS_BAST A INNER JOIN MS_KONTRAK B"
                      + " ON A.NoKontrak = B.NoKontrak"
                      + " INNER JOIN MS_CUSTOMER C ON B.NoCustomer = C.NoCustomer"
                      + " WHERE A.NoKontrak = '" + NoKontrak + "'");

                    string ket = Cf.LogCapture(rs)
                        ;

                    Db.Execute("EXEC spLogKontrak "
                        + " 'T-BAST'"
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
                    Db.Execute("UPDATE MS_BAST SET"
                       + " ST='" + status + "'"
                       + " WHERE NoKontrak = '" + NoKontrak + "'"
                         );

                    if (tgltarget.Text != "")
                    {
                        Db.Execute("UPDATE MS_BAST SET TargetST='" + Convert.ToDateTime(tgltarget.Text) + "' WHERE NoKontrak = '" + NoKontrak + "'");
                    }

                    DataTable rs = Db.Rs("SELECT"
                      + " A.NoKontrak AS [No. Kontrak]"
                      + ",B.NoUnit AS [Unit]"
                      + ",C.Nama AS [Customer]"
                      + ",CONVERT(varchar, A.TargetST, 106) AS [Tanggal Target BAST]"
                      + ",PersenLunas AS [Prosentase Pelunasan]"
                      + ", case when A.ST='S' then 'Target BAST' when A.ST='D' then 'BAST' when A.ST='B' then 'Belum BAST' else 'Tanda Tangan BAST' end as [Status BAST]"
                      + " FROM MS_BAST A INNER JOIN MS_KONTRAK B"
                      + " ON A.NoKontrak = B.NoKontrak"
                      + " INNER JOIN MS_CUSTOMER C ON B.NoCustomer = C.NoCustomer"
                      + " WHERE A.NoKontrak = '" + NoKontrak + "'");

                    string ket = Cf.LogCapture(rs)
                        ;

                    Db.Execute("EXEC spLogKontrak "
                        + " 'T-BAST'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + ket + "'"
                        + ",'" + NoKontrak + "'"
                        );
                    decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
                    Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                }
                if (dariReminder.Checked)
                    Response.Redirect("ReminderST.aspx?done=" + NoKontrak);
                else
                    Response.Redirect("KontrakSTTarget.aspx?done=" + NoKontrak);
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
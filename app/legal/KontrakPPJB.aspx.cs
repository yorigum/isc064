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
    public partial class KontrakPPJB : System.Web.UI.Page
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
                    nokontrak.Text = Request.QueryString["NoKontrak"].Replace("'", "");
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
                 "SELECT COUNT(*) FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "' AND NoPPJB != '' AND (PPJB = 'D' OR PPJB = 'T') ");// AND ST <> 'D'");
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
            nilaibiaya.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            nilaibiaya.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            nilaibiaya.Attributes["onblur"] = "CalcBlur(this);";

            nilaikpa.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            nilaikpa.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            nilaikpa.Attributes["onblur"] = "CalcBlur(this);";

            Func.KontrakHeader(NoKontrak, nokontrakl, unit, customer, agent);

            string strSql = "SELECT * FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'";
            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                tglppjb.Text = Cf.Day(DateTime.Today);

                persenlunas.Text = Cf.Num(rs.Rows[0]["PersenLunas"]);

                string ParamID = "FormatPPJB" + rs.Rows[0]["Project"];
                string a = Db.SingleString("SELECT Value FROM [ISC064_SECURITY].[dbo].[REF_PARAM] WHERE ParamID = '" + ParamID + "'");
                decimal minppjb = Convert.ToDecimal(a);
                if ((decimal)rs.Rows[0]["PersenLunas"] < minppjb)
                    lunasinfo.Text = "PELUNASAN BELUM MENCAPAI " + minppjb + "%";
            }
        }

        private bool datavalid()
        {
            string s = "";
            bool x = true;

            if (!Cf.isTgl(tglppjb))
            {
                x = false;
                if (s == "") s = tglppjb.ID;
                tglppjbc.Text = "Tanggal";
            }
            else
                tglppjbc.Text = "";

            if (!Cf.isMoney(nilaibiaya))
            {
                x = false;
                if (s == "") s = nilaibiaya.ID;
                nilaibiayac.Text = "Angka";
            }
            else
                nilaibiayac.Text = "";

            if (!Cf.isMoney(nilaikpa))
            {
                x = false;
                if (s == "") s = nilaikpa.ID;
                nilaikpac.Text = "Angka";
            }
            else
                nilaikpac.Text = "";

            decimal lunas = Db.SingleDecimal("SELECT PersenLunas FROM MS_KONTRAK WHERE NoKontrak ='" + NoKontrak + "'");
            string project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
            string ParamID = "FormatPPJB" + project;

            string a = Db.SingleString("SELECT Value FROM [ISC064_SECURITY].[dbo].[REF_PARAM] WHERE ParamID = '" + ParamID + "'");
            decimal minppjb = Convert.ToDecimal(a);

            if (lunas < minppjb)
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
                    + "3. Pelunasan Belum Mencapai " + minppjb + "%"
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );

            return x;
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            if (datavalid())
            {
                DateTime TglPPJB = Convert.ToDateTime(tglppjb.Text);
                decimal Lunas = Convert.ToDecimal(persenlunas.Text);
                decimal NilaiBiaya = Convert.ToDecimal(nilaibiaya.Text);
                string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                string status = "D"; // B = Belum, S = Target, D = PPJB, T = Tanda Tangan

                int c = Db.SingleInteger("SELECT COUNT(*) FROM MS_PPJB WHERE NoKontrak = '" + NoKontrak + "'");
                if (c == 0)
                {
                    string NoPPJB = Db.SingleString("SELECT NoPPJB FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");

                    if (NoPPJB == "")
                        NoPPJB = Numerator.PPJB(TglPPJB.Month, TglPPJB.Year, Project);

                    Db.Execute("EXEC spPPJB "
                        + " '" + NoKontrak + "'"
                        + ",'" + NoPPJB + "'"
                        + ",'" + TglPPJB + "'"
                        );

                    Db.Execute("UPDATE MS_PPJB SET Project = '" + Project + "' WHERE NoPPJB = '" + NoPPJB + "'");

                    Db.Execute("UPDATE MS_PPJB"
                        //+ " SET NilaiRealisasiKPR = " + Convert.ToDecimal(nilaikpa.Text)
                        //+ ", RekeningCairKPR = '" + rekcair.SelectedValue + "'"
                        + " SET NoPPJBm = '" + noppjbm.Text + "'"
                        + " ,PPJB = '" + status + "'"
                        + " ,PPJBu= '" + ppjbused.SelectedValue + "'"
                        + " ,NoKontrak = '" + nokontrak.Text + "'"
                        + " ,Biaya = '" + NilaiBiaya + "'"
                        + ", KetPPJB= '" + keterangan.Text + "'"
                        + " ,TglPPJB= '" + tglppjb.Text + "'"
                        + " WHERE NoKontrak = '" + NoKontrak + "'"
                        );

                    Db.Execute("UPDATE MS_KONTRAK SET PPJB = '" + status + "',TglPPJB = '" + TglPPJB + "', NoPPJB = '" + NoPPJB + "' WHERE NoKontrak = '" + NoKontrak + "'");

                    if (NilaiBiaya != 0)
                    {
                        Db.Execute("EXEC spTagihanDaftar "
                            + " '" + NoKontrak + "'"
                            + ",'BIAYA ADM. PPJB'"
                            + ",'" + TglPPJB + "'"
                            + ", " + NilaiBiaya
                            + ",'ADM'"
                            );
                    }
                    DataTable rs = Db.Rs("SELECT"
                     + " A.NoKontrak AS [No. Kontrak]"
                     + ",B.NoUnit AS [Unit]"
                     + ",C.Nama AS [Customer]"
                     + ",A.NoPPJB AS [No. PPJB]"
                     + ",A.KetPPJB AS [Keterangan PPJB]"
                     + ",A.Biaya AS [Biaya PPJB]"
                     + ",CONVERT(varchar, A.TglTargetPPJB, 106) AS [Tanggal Target PPJB]"
                     + ",CONVERT(varchar, A.TglPPJB, 106) AS [Tanggal PPJB]"
                     + ", case when A.PPJB='S' then 'Target PPJB' when A.PPJB='D' then 'PPJB' when A.PPJB='B' then 'Belum PPJB' else 'Tanda Tangan PPJB' end as [Status PPJB]"
                     + ",PersenLunas AS [Prosentase Pelunasan]"
                     + " FROM MS_PPJB A INNER JOIN MS_KONTRAK B"
                     + " ON A.NoKontrak = B.NoKontrak"
                     + " INNER JOIN MS_CUSTOMER C ON B.NoCustomer = C.NoCustomer"
                     + " WHERE A.NoKontrak = '" + NoKontrak + "'");
                    string ket = Cf.LogCapture(rs)
                        + "<br><br>Biaya Administrasi : " + Cf.Num(NilaiBiaya)
                        ;

                    Db.Execute("EXEC spLogKontrak "
                           + " 'PPJB'"
                           + ",'" + Act.UserID + "'"
                           + ",'" + Act.IP + "'"
                           + ",'" + ket + "'"
                           + ",'" + NoKontrak + "'"
                           );

                    decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
                    Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                    string ParamID = "FormatPPJB";
                    string a = Db.SingleString("SELECT Value FROM [ISC064_SECURITY].[dbo].[REF_PARAM] WHERE ParamID = '" + ParamID + "'");
                    decimal minppjb = Convert.ToDecimal(a);
                    if (Lunas >= minppjb)
                    {
                        ////Send Email -- kalo dipake koment-nya dibuka.
                        //Email(NoKontrak, NoPPJB);
                    }
                }
                else
                {
                    string NoPPJB = Db.SingleString("SELECT NoPPJB FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                    if (NoPPJB == "")
                        NoPPJB = Numerator.PPJB(TglPPJB.Month, TglPPJB.Year, Project);

                    Db.Execute("UPDATE MS_PPJB"
                        + " SET NoPPJBm = '" + noppjbm.Text + "'"
                        + " ,NoPPJB = '" + NoPPJB + "'"
                        + " ,PPJB = '" + status + "'"
                        + " ,PPJBu= '" + ppjbused.SelectedValue + "'"
                        //+ " ,NoKontrak = '" + nokontrak.Text + "'"
                        + " ,Biaya = '" + NilaiBiaya + "'"
                        + ", KetPPJB= '" + keterangan.Text + "'"
                        + " ,TglPPJB= '" + tglppjb.Text + "'"
                        + ", Project = '" + Project + "'"
                        + " WHERE NoKontrak = '" + NoKontrak + "'"
                        );

                    Db.Execute("UPDATE MS_KONTRAK SET PPJB = '" + status + "',TglPPJB = '" + TglPPJB + "', NoPPJB = '" + NoPPJB + "' WHERE NoKontrak = '" + NoKontrak + "'");

                    if (NilaiBiaya != 0)
                    {
                        Db.Execute("EXEC spTagihanDaftar "
                            + " '" + NoKontrak + "'"
                            + ",'BIAYA ADM. PPJB'"
                            + ",'" + TglPPJB + "'"
                            + ", " + NilaiBiaya
                            + ",'ADM'"
                            );
                    }

                    DataTable rs = Db.Rs("SELECT"
                        + " A.NoKontrak AS [No. Kontrak]"
                        + ",B.NoUnit AS [Unit]"
                        + ",C.Nama AS [Customer]"
                        + ",A.NoPPJB AS [No. PPJB]"
                        + ",A.KetPPJB AS [Keterangan PPJB]"
                        + ",A.Biaya AS [Biaya PPJB]"
                        + ",CONVERT(varchar, A.TglTargetPPJB, 106) AS [Tanggal Target PPJB]"
                        + ",CONVERT(varchar, A.TglPPJB, 106) AS [Tanggal PPJB]"
                        + ", case when A.PPJB='S' then 'Target PPJB' when A.PPJB='D' then 'PPJB' when A.PPJB='B' then 'Belum PPJB' else 'Tanda Tangan PPJB' end as [Status PPJB]"
                        + ",PersenLunas AS [Prosentase Pelunasan]"
                        + " FROM MS_PPJB A INNER JOIN MS_KONTRAK B"
                        + " ON A.NoKontrak = B.NoKontrak"
                        + " INNER JOIN MS_CUSTOMER C ON B.NoCustomer = C.NoCustomer"
                        + " WHERE A.NoKontrak = '" + NoKontrak + "'");
                    string ket = Cf.LogCapture(rs)
                        + "<br>Biaya Administrasi : " + Cf.Num(NilaiBiaya)
                        ;

                    Db.Execute("EXEC spLogKontrak "
                            + " 'PPJB'"
                            + ",'" + Act.UserID + "'"
                            + ",'" + Act.IP + "'"
                            + ",'" + ket + "'"
                            + ",'" + NoKontrak + "'"
                            );

                    decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
                    Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                    //string NoPPJB = Db.SingleString("SELECT NoPPJB FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                    string ParamID = "FormatPPJB";
                    string a = Db.SingleString("SELECT Value FROM [ISC064_SECURITY].[dbo].[REF_PARAM] WHERE ParamID = '" + ParamID + "'");
                    decimal minppjb = Convert.ToDecimal(a);
                    if (Lunas >= minppjb)
                    {
                        ////Send Email -- kalo dipake koment-nya dibuka.
                        //Email(NoKontrak, NoPPJB);
                    }
                }
                if (dariReminder.Checked)
                    Response.Redirect("ReminderPPJB.aspx?done=" + NoKontrak + "&project=" + Project);
                else
                    Response.Redirect("KontrakPPJB.aspx?done=" + NoKontrak);
            }
        }

        protected void Email(string NoKontrak, string NoPPJB)
        {
            string Nama = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER A JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK B ON A.NOCUSTOMER = B.NOCUSTOMER WHERE B.NOKONTRAK = '" + NoKontrak + "'");

            string Email = Db.SingleString("SELECT Email FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER A JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK B ON A.NOCUSTOMER = B.NOCUSTOMER WHERE B.NOKONTRAK = '" + NoKontrak + "'");

            // Isi SMS Blast 
            string Pesan = "<p>Mengundang Bapak/Ibu. " + Nama + " untuk datang tanda tangan PPJB di hari kerja senin - jumat "
                + "di kantor Marketing SAVASA, Cikarang, info lebih lanjut hub kantor marketing kami di 021 3110 7666 bag adm.</p>";


            MailMessage mail = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();

            try
            {
                //mail.From = new MailAddress("App.eProperty.batavianet@gmail.com", "eProperty Application");
                mail.From = new MailAddress("App.eProperty@batavianet.com", "eProperty Application");
                mail.To.Add(new MailAddress(Email));
                //mail.Bcc.Add(new MailAddress("dbatavianet@gmail.com"));

                mail.Subject = "PPJB";
                mail.Body = "No. PPJB: " + NoPPJB + "<br />" + Pesan;
                mail.IsBodyHtml = true;

                //smtpClient.Host = "smtp.gmail.com";
                //smtpClient.Port = 587;
                //smtpClient.EnableSsl = true;
                //smtpClient.UseDefaultCredentials = true;
                //smtpClient.Credentials = new System.Net.NetworkCredential("App.eProperty.batavianet@gmail.com", "batav123");

                smtpClient.Host = "mail.batavianet.com";
                smtpClient.Port = 25;
                smtpClient.Credentials = new System.Net.NetworkCredential("App.eProperty@batavianet.com", "batav123");
                smtpClient.Timeout = 100000;

                smtpClient.Send(mail);
            }
            catch (Exception ex)
            {
                Response.Write("Can't send email. Reason : " + ex);
            }

        }
        //public class SMSBlast
        //{
        //    //public City city { get; set; }
        //    //public List<List> list { get; set; }
        //}

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

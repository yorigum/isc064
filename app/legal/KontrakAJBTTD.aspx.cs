using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LEGAL
{
    public partial class KontrakAJBTTD : System.Web.UI.Page
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
                     "SELECT COUNT(*) FROM MS_AJB WHERE NoKontrak = '" + NoKontrak + "' AND AJB = 'D'");// AND ST <> 'D'");
            if (a == 0)
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

            //string strSql = "SELECT * FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'";
            string strSql = "SELECT A.NoKontrak,A.PersenLunas,B.*"
                    + " FROM MS_KONTRAK A LEFT JOIN MS_AJB B ON A.NoKontrak = B.NoKontrak "
                    + "WHERE A.NoKontrak = '" + NoKontrak + "'";
            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                //tglajb.Text = Cf.Day(DateTime.Today);
                tglttd.Text = Cf.Day(rs.Rows[0]["TglTTDAJB"]);
                noajb.Text = rs.Rows[0]["NoAJB"].ToString();
                noajbm.Text = rs.Rows[0]["NoAJBm"].ToString();
                tglajb.Text = Cf.Day(rs.Rows[0]["TglAJB"]);
                nilaibiaya.Text = Cf.Num(rs.Rows[0]["Biaya"]);
                //tglttd.Text = Cf.Day(rs.Rows[0]["TglTTDAJB"]);
                //ajbused.SelectedValue = rs.Rows[0]["AJBu"].ToString();
                notaris.Text = rs.Rows[0]["NamaNotaris"].ToString();
                keterangan.Text = rs.Rows[0]["KetAJB"].ToString();

                persenlunas.Text = Cf.Num(rs.Rows[0]["PersenLunas"]);
                if ((decimal)rs.Rows[0]["PersenLunas"] < 100)
                    lunasinfo.Text = "PELUNASAN BELUM MENCAPAI 100%";
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

            if (!Cf.isTgl(tglttd))
            {
                x = false;
                if (s == "") s = tglttd.ID;
                tglttdc.Text = "Tanggal";
            }
            else
                tglttdc.Text = "";


            if (!Cf.isMoney(nilaibiaya))
            {
                x = false;
                if (s == "") s = nilaibiaya.ID;
                nilaibiayac.Text = "Angka";
            }
            else
                nilaibiayac.Text = "";

            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Format Tanggal : Bulan / Tanggal / Tahun.\\n"
                    + "2. Biaya Administrasi harus berupa angka."
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
                x = "AJB/" + c.ToString().PadLeft(5, '0');

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
                DateTime TglAJB = Convert.ToDateTime(tglajb.Text);
                string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                string status = "T"; // B = Belum, S = Target, D = AJB, T = Tanda Tangan
                int c = Db.SingleInteger("SELECT COUNT(*) FROM MS_AJB WHERE NoKontrak = '" + NoKontrak + "'");
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
                 + ",NamaNotaris='" + notaris.Text + "'"
                 + ",KetAJB ='" + keterangan.Text + "'"
                 + ",Biaya ='" + nilaibiaya.Text + "'"
                 + " WHERE NoKontrak = '" + NoKontrak + "'"
                   );

                    if (tglttd.Text != "")
                    {
                        Db.Execute("UPDATE MS_AJB SET TglTTDAJB='" + Convert.ToDateTime(tglttd.Text) + "' WHERE NoKontrak = '" + NoKontrak + "'");
                    }
                    if (tglajb.Text != "")
                    {
                        Db.Execute("UPDATE MS_AJB SET TglAJB='" + Convert.ToDateTime(tglajb.Text) + "' WHERE NoKontrak = '" + NoKontrak + "'");
                    }

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
                    }
                    //Kode dan Nomor Seri FPS
                    //string NoFPS = Db.SingleString("SELECT NoFPS FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                    //if (NoFPS == "")
                    //    NoFPS = AutoIDFPS();

                    //Db.Execute("UPDATE MS_KONTRAK SET NoFPS= '" + NoFPS + "' WHERE NoKontrak = '" + NoKontrak + "' ");

                    DataTable rs = Db.Rs("SELECT"
                              + " A.NoKontrak AS [No. Kontrak]"
                              + ",B.NoUnit AS [Unit]"
                              + ",C.Nama AS [Customer]"
                              + ",CONVERT(varchar, A.TglTargetAJB, 106) AS [Tanggal Target AJB]"
                              + ",CONVERT(varchar, A.TglAJB, 106) AS [Tanggal AJB]"
                              + ",CONVERT(varchar, A.TglTTDAJB, 106) AS [Tanggal TTD AJB]"
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
                         + " 'TTD-AJB'"
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
                         + " AJBu=" + ajbused.SelectedValue
                         + " ,NoAJBm='" + noajbm.Text + "'"
                         + " ,AJB='" + status + "'"
                         + ",NamaNotaris='" + notaris.Text + "'"
                         + ",KetAJB ='" + keterangan.Text + "'"
                         + ",Biaya ='" + nilaibiaya.Text + "'"
                         + " WHERE NoKontrak = '" + NoKontrak + "'"
                           );

                    if (tglttd.Text != "")
                    {
                        Db.Execute("UPDATE MS_AJB SET TglTTDAJB='" + Convert.ToDateTime(tglttd.Text) + "' WHERE NoKontrak = '" + NoKontrak + "'");
                    }
                    if (tglajb.Text != "")
                    {
                        Db.Execute("UPDATE MS_AJB SET TglAJB='" + Convert.ToDateTime(tglajb.Text) + "' WHERE NoKontrak = '" + NoKontrak + "'");
                    }

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
                    }
                    //Kode dan Nomor Seri FPS
                    //string NoFPS = Db.SingleString("SELECT NoFPS FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                    //if (NoFPS == "")
                    //    NoFPS = AutoIDFPS();

                    //Db.Execute("UPDATE MS_KONTRAK SET NoFPS= '" + NoFPS + "' WHERE NoKontrak = '" + NoKontrak + "' ");

                    DataTable rs = Db.Rs("SELECT"
                            + " A.NoKontrak AS [No. Kontrak]"
                            + ",B.NoUnit AS [Unit]"
                            + ",C.Nama AS [Customer]"
                            + ",CONVERT(varchar, A.TglTargetAJB, 106) AS [Tanggal Target AJB]"
                            + ",CONVERT(varchar, A.TglAJB, 106) AS [Tanggal AJB]"
                            + ",CONVERT(varchar, A.TglTTDAJB, 106) AS [Tanggal TTD AJB]"
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
                         + " 'TTD-AJB'"
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
                    Response.Redirect("KontrakAJBTTD.aspx?done=" + NoKontrak);
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
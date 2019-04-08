using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LEGAL
{
    public partial class KontrakST : System.Web.UI.Page
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
                    string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                    cancel.Attributes["onclick"] = "location.href='ReminderST.aspx?Project=" + Project + "'";
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
                     "SELECT COUNT(*) FROM MS_BAST WHERE NoKontrak = '" + NoKontrak + "' AND (ST = 'D' OR ST = 'T') ");// AND NoST <> 'D'");
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

                Js.Focus(this, luas);
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

                Js.Focus(this, luas);
                Js.Confirm(this, "Lanjutkan proses serah terima unit properti?");
            }
        }

        private void InitForm()
        {
            //kalkulator
            luas.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            luas.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            luas.Attributes["onblur"] = "CalcBlur(this);";

            luasnett.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            luasnett.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            luasnett.Attributes["onblur"] = "CalcBlur(this);";

            nilaibiaya.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            nilaibiaya.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            nilaibiaya.Attributes["onblur"] = "CalcBlur(this);";

            lebihbayar.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            lebihbayar.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            lebihbayar.Attributes["onblur"] = "CalcBlur(this);";

            masagaransi.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            masagaransi.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            masagaransi.Attributes["onblur"] = "CalcBlur(this);";
        }

        private void Fill()
        {
            Func.KontrakHeader(NoKontrak, nokontrakl, unit, customer, agent);

            //string strSql = "SELECT *"
            //	+ " FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'";
            string strSql = "SELECT A.NoKontrak,A.PersenLunas,A.TargetST,A.NoStock,B.*"
                    + " FROM MS_KONTRAK A LEFT JOIN MS_BAST B ON A.NoKontrak = B.NoKontrak "
                    + "WHERE A.NoKontrak = '" + NoKontrak + "'";
            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                if (rs.Rows[0]["TglST"] is DBNull)
                {
                    tglst.Text = Cf.Day(rs.Rows[0]["TargetST"]);
                }
                else
                {
                    tglst.Text = Cf.Day(DateTime.Today);
                }

                persenlunas.Text = Cf.Num(rs.Rows[0]["PersenLunas"]);
                DataTable rs2 = Db.Rs("SELECT * FROM MS_UNIT WHERE NoStock = '" + rs.Rows[0]["NoStock"] + "'");
                luas.Text = Cf.Num(rs2.Rows[0]["Luas"]);
                luasnett.Text = Cf.Num(rs2.Rows[0]["LuasNett"]);
                if ((decimal)rs.Rows[0]["PersenLunas"] < 100)
                    lunasinfo.Text = "PELUNASAN BELUM MENCAPAI 100%";
            }
        }

        private bool datavalid()
        {
            string s = "";
            bool x = true;

            if (!Cf.isMoney(masagaransi))
            {
                x = false;
                if (s == "") s = masagaransi.ID;
                masagaransic.Text = "Angka";
            }

            if (!Cf.isTgl(tglst))
            {
                x = false;
                if (s == "") s = tglst.ID;
                tglstc.Text = "Tanggal";
            }
            else
                tglstc.Text = "";

            if (!Cf.isMoney(luas))
            {
                x = false;
                if (s == "") s = luas.ID;
                luasc.Text = "Angka";
            }
            else
                luasc.Text = "";

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

        protected void save_Click(object sender, System.EventArgs e)
        {
            if (datavalid())
            {
                DateTime TglST = Convert.ToDateTime(tglst.Text);
                DateTime TargetST = Db.SingleTime("SELECT TargetST FROM MS_KONTRAK WHERE NoKontrak ='" + NoKontrak + "'");
                decimal NilaiKontrak = Db.SingleDecimal("SELECT NilaiKontrak FROM MS_KONTRAK WHERE NoKontrak ='" + NoKontrak + "'");
                decimal Luas = Convert.ToDecimal(luas.Text);
                decimal LuasNett = Convert.ToDecimal(luasnett.Text);
                decimal Biaya = Convert.ToDecimal(nilaibiaya.Text);
                decimal LebihBayar = Convert.ToDecimal(lebihbayar.Text);
                decimal MasaGaransi = Convert.ToDecimal(masagaransi.Text);
                string status = "D"; // B = Belum, S = Target, D = Serah Terima, T = Tanda Tangan
                string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");

                if (TglST > TargetST)
                {
                    TimeSpan ts = new TimeSpan();
                    ts = TglST.Subtract(TargetST);
                    int Telat = ts.Days;
                    decimal dendaST = (decimal)0.001 * NilaiKontrak * Telat;

                    Db.Execute(" UPDATE MS_KONTRAK SET DendaST ='" + dendaST + "' WHERE NoKontrak='" + NoKontrak + "'");
                }

                int c = Db.SingleInteger("SELECT COUNT(*) FROM MS_BAST WHERE NoKontrak = '" + NoKontrak + "'");
                if (c == 0)
                {

                    string NoST = Db.SingleString("SELECT NoST FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                    if (NoST == "")
                        NoST = Numerator.BAST(TglST.Month, TglST.Year, Project);

                    Db.Execute("EXEC spST "
                        + " '" + NoKontrak + "'"
                        + ",'" + NoST + "'"
                        + ",'" + TglST + "'"
                        );

                    Db.Execute("UPDATE MS_BAST SET Project = '" + Project + "' WHERE NoST = '" + NoST + "'");

                    Db.Execute("UPDATE MS_BAST SET"
                             + " STu=" + stused.SelectedValue
                             + " ,NoSTm='" + nostm.Text + "'"
                             + " ,ST='" + status + "'"
                             + " ,LuasGross= '" + Luas + "'"
                             + " ,LuasNett= '" + LuasNett + "'"
                             + " ,Biaya= '" + Biaya + "'"
                             + " ,LebihBayar= '" + LebihBayar + "'"
                             + " ,MasaGaransi= '" + MasaGaransi + "'"
                             + " ,KetST ='" + keterangan.Text + "'"
                             + " WHERE NoKontrak = '" + NoKontrak + "'"
                               );

                    if (tglst.Text != "")
                    {
                        Db.Execute("UPDATE MS_BAST SET TglST='" + Convert.ToDateTime(tglst.Text) + "' WHERE NoKontrak = '" + NoKontrak + "'");
                    }

                    Db.Execute("UPDATE MS_KONTRAK SET ST = '" + status + "',TglST = '" + Convert.ToDateTime(tglst.Text) + "' WHERE NoKontrak = '" + NoKontrak + "'");

                    decimal NilaiBiaya = Convert.ToDecimal(nilaibiaya.Text);
                    if (NilaiBiaya != 0)
                    {
                        Db.Execute("EXEC spTagihanDaftar "
                            + " '" + NoKontrak + "'"
                            + ",'BIAYA ADM. SERAH TERIMA'"
                            + ",'" + TglST + "'"
                            + ", " + NilaiBiaya
                            + ",'ADM'"
                            );
                    }
                    DataTable rs = Db.Rs("SELECT"
                    + " A.NoKontrak AS [No. Kontrak]"
                    + ",B.NoUnit AS [Unit]"
                    + ",C.Nama AS [Customer]"
                    + ",A.NoST AS [No. BAST]"
                    + ",A.LuasGross AS [Luas Tanah]"
                    + ",A.LuasNett AS [Luas Bangunan]"
                    + ",CONVERT(varchar, A.TargetST, 106) AS [Tanggal Target BAST]"
                    + ",CONVERT(varchar, A.TglST, 106) AS [Tanggal BAST]"
                    + ",PersenLunas AS [Prosentase Pelunasan]"
                    + ",A.MasaGaransi"
                    + ", case when A.ST='S' then 'Target BAST' when A.ST='D' then 'BAST' when A.ST='B' then 'Belum BAST' else 'Tanda Tangan BAST' end as [Status BAST]"
                    + " FROM MS_BAST A INNER JOIN MS_KONTRAK B"
                    + " ON A.NoKontrak = B.NoKontrak"
                    + " INNER JOIN MS_CUSTOMER C ON B.NoCustomer = C.NoCustomer"
                    + " WHERE A.NoKontrak = '" + NoKontrak + "'");

                    string ket = Cf.LogCapture(rs)
                      //+ Cf.LogCompare(rsBef,rsAft)
                      + "<br>Biaya Administrasi : " + Cf.Num(NilaiBiaya)
                      ;

                    Db.Execute("EXEC spLogKontrak "
                           + " 'BAST'"
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

                    string NoST = Db.SingleString("SELECT NoST FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                    if (NoST == "")
                        NoST = Numerator.BAST(TglST.Month, TglST.Year, Project);

                    Db.Execute("UPDATE MS_BAST SET"
                             + " STu=" + stused.SelectedValue
                             + " ,NoST = '" + NoST + "'"
                             + " ,NoSTm='" + nostm.Text + "'"
                             + " ,ST='" + status + "'"
                             + " ,LuasGross= '" + Luas + "'"
                             + " ,LuasNett= '" + LuasNett + "'"
                             + " ,Biaya= '" + Biaya + "'"
                             + " ,LebihBayar= '" + LebihBayar + "'"
                             + " ,MasaGaransi= '" + MasaGaransi + "'"
                             + " ,KetST ='" + keterangan.Text + "'"
                             + ", Project = '" + Project + "'"
                             + " WHERE NoKontrak = '" + NoKontrak + "'"
                               );

                    if (tglst.Text != "")
                    {
                        Db.Execute("UPDATE MS_BAST SET TglST='" + Convert.ToDateTime(tglst.Text) + "' WHERE NoKontrak = '" + NoKontrak + "'");
                    }

                    decimal NilaiBiaya = Convert.ToDecimal(nilaibiaya.Text);
                    if (NilaiBiaya != 0)
                    {
                        Db.Execute("EXEC spTagihanDaftar "
                            + " '" + NoKontrak + "'"
                            + ",'BIAYA ADM. SERAH TERIMA'"
                            + ",'" + TglST + "'"
                            + ", " + NilaiBiaya
                            + ",'ADM'"
                            );
                    }
                    Db.Execute("UPDATE MS_KONTRAK SET ST = '" + status + "',TglST = '" + Convert.ToDateTime(tglst.Text) + "' WHERE NoKontrak = '" + NoKontrak + "'");
                    DataTable rs = Db.Rs("SELECT"
                   + " A.NoKontrak AS [No. Kontrak]"
                   + ",B.NoUnit AS [Unit]"
                   + ",C.Nama AS [Customer]"
                   + ",A.NoST AS [No. BAST]"
                   + ",A.LuasGross AS [Luas Tanah]"
                   + ",A.LuasNett AS [Luas Bangunan]"
                   + ",CONVERT(varchar, A.TargetST, 106) AS [Tanggal Target BAST]"
                   + ",CONVERT(varchar, A.TglST, 106) AS [Tanggal BAST]"
                   + ",PersenLunas AS [Prosentase Pelunasan]"
                   + ",A.MasaGaransi"
                   + ", case when A.ST='S' then 'Target BAST' when A.ST='D' then 'BAST' when A.ST='B' then 'Belum BAST' else 'Tanda Tangan BAST' end as [Status BAST]"
                   + " FROM MS_BAST A INNER JOIN MS_KONTRAK B"
                   + " ON A.NoKontrak = B.NoKontrak"
                   + " INNER JOIN MS_CUSTOMER C ON B.NoCustomer = C.NoCustomer"
                   + " WHERE A.NoKontrak = '" + NoKontrak + "'");

                    string ket = Cf.LogCapture(rs)
                      //+ Cf.LogCompare(rsBef,rsAft)
                      + "<br>Biaya Administrasi : " + Cf.Num(NilaiBiaya)
                      ;

                    Db.Execute("EXEC spLogKontrak "
                        + " 'BAST'"
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
                    Response.Redirect("KontrakST.aspx?done=" + NoKontrak);
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

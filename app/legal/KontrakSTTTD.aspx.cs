using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LEGAL
{

    public partial class KontrakSTTTD : System.Web.UI.Page
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
                     "SELECT COUNT(*) FROM MS_BAST WHERE NoKontrak = '" + NoKontrak + "' AND ST = 'D'");// AND ST <> 'D'");
            if (a == 0)
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

            nilaibiaya.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            nilaibiaya.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            nilaibiaya.Attributes["onblur"] = "CalcBlur(this);";

            luasnett.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            luasnett.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            luasnett.Attributes["onblur"] = "CalcBlur(this);";

            lebihbayar.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            lebihbayar.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            lebihbayar.Attributes["onblur"] = "CalcBlur(this);";
        }

        private void Fill()
        {
            Func.KontrakHeader(NoKontrak, nokontrakl, unit, customer, agent);

            string strSql = "SELECT A.NoKontrak,A.PersenLunas,B.*"
                    + " FROM MS_KONTRAK A LEFT JOIN MS_BAST B ON A.NoKontrak = B.NoKontrak "
                    + "WHERE A.NoKontrak = '" + NoKontrak + "'";
            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                //luas.Text = Cf.Num(rs.Rows[0]["Luas"]);
                //targetst.Text = Cf.Day(rs.Rows[0]["TargetST"]);
                nost.Text = rs.Rows[0]["NoST"].ToString();
                nostm.Text = rs.Rows[0]["NoSTm"].ToString();
                tglst.Text = Cf.Day(rs.Rows[0]["TglST"]);
                luas.Text = Cf.Num(rs.Rows[0]["LuasGross"]);
                luasnett.Text = Cf.Num(rs.Rows[0]["LuasNett"]);
                nilaibiaya.Text = Cf.Num(rs.Rows[0]["Biaya"]);
                lebihbayar.Text = Cf.Num(rs.Rows[0]["LebihBayar"]);
                tglttd.Text = Cf.Day(rs.Rows[0]["TglTTDST"]);
                //stused.SelectedValue = rs.Rows[0]["STu"].ToString();
                keterangan.Text = rs.Rows[0]["KetST"].ToString();

                persenlunas.Text = Cf.Num(rs.Rows[0]["PersenLunas"]);
                if ((decimal)rs.Rows[0]["PersenLunas"] < 100)
                    lunasinfo.Text = "PELUNASAN BELUM MENCAPAI 100%";
            }
        }

        private bool datavalid()
        {
            string s = "";
            bool x = true;

            if (!Cf.isTgl(tglst))
            {
                x = false;
                if (s == "") s = tglst.ID;
                tglstc.Text = "Tanggal";
            }
            else
                tglstc.Text = "";
            if (!Cf.isTgl(tglttd))
            {
                x = false;
                if (s == "") s = tglttd.ID;
                tglttdc.Text = "Tanggal";
            }
            else
                tglttdc.Text = "";

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
                x = "ST/" + c.ToString().PadLeft(5, '0');

                if (isUnique(x)) hasfound = true;
            }

            return x;
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            if (datavalid())
            {
                DateTime TglST = Convert.ToDateTime(tglst.Text);
                decimal Luas = Convert.ToDecimal(luas.Text);
                decimal LuasNett = Convert.ToDecimal(luasnett.Text);
                decimal Biaya = Convert.ToDecimal(nilaibiaya.Text);
                decimal LebihBayar = Convert.ToDecimal(lebihbayar.Text);
                string status = "T";

                int c = Db.SingleInteger("SELECT COUNT(*) FROM MS_BAST WHERE NoKontrak = '" + NoKontrak + "'");
                if (c == 0)
                {
                    string NoST = Db.SingleString("SELECT NoST FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                    if (NoST == "")
                        NoST = AutoID();

                    Db.Execute("EXEC spST "
                        + " '" + NoKontrak + "'"
                        + ",'" + NoST + "'"
                        + ",'" + TglST + "'"
                        );

                    Db.Execute("UPDATE MS_BAST SET"
                             + " STu=" + stused.SelectedValue
                             + " ,NoSTm='" + nostm.Text + "'"
                             + " ,ST='" + status + "'"
                             + " ,LuasGross= '" + Luas + "'"
                             + " ,LuasNett= '" + LuasNett + "'"
                             + " ,Biaya= '" + Biaya + "'"
                             + " ,LebihBayar= '" + LebihBayar + "'"
                             + " ,KetST ='" + keterangan.Text + "'"
                             + " WHERE NoKontrak = '" + NoKontrak + "'"
                               );

                    if (tglst.Text != "")
                    {
                        Db.Execute("UPDATE MS_BAST SET TglST='" + Convert.ToDateTime(tglst.Text) + "' WHERE NoKontrak = '" + NoKontrak + "'");
                    }
                    if (tglttd.Text != "")
                    {
                        Db.Execute("UPDATE MS_BAST SET TglTTDST='" + Convert.ToDateTime(tglttd.Text) + "' WHERE NoKontrak = '" + NoKontrak + "'");
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
                    DataTable rs = Db.Rs("SELECT"
                                       + " A.NoKontrak AS [No. Kontrak]"
                                       + ",B.NoUnit AS [Unit]"
                                       + ",C.Nama AS [Customer]"
                                       + ",A.NoST AS [No. BAST]"
                                       + ",A.LuasGross AS [Luas Tanah]"
                                       + ",A.LuasNett AS [Luas Bangunan]"
                                       + ",CONVERT(varchar, A.TargetST, 106) AS [Tanggal Target BAST]"
                                       + ",CONVERT(varchar, A.TglST, 106) AS [Tanggal BAST]"
                                       + ",CONVERT(varchar, A.TglTTDST, 106) AS [Tanggal TTD BAST]"
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
                           + " 'TTD-BAST'"
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

                    Db.Execute("UPDATE MS_BAST SET"
                             + " STu=" + stused.SelectedValue
                             + " ,NoSTm='" + nostm.Text + "'"
                             + " ,ST='" + status + "'"
                             + " ,LuasGross= '" + Luas + "'"
                             + " ,LuasNett= '" + LuasNett + "'"
                             + " ,Biaya= '" + Biaya + "'"
                             + " ,LebihBayar= '" + LebihBayar + "'"
                             + " ,KetST ='" + keterangan.Text + "'"
                             + " WHERE NoKontrak = '" + NoKontrak + "'"
                               );

                    if (tglst.Text != "")
                    {
                        Db.Execute("UPDATE MS_BAST SET TglST='" + Convert.ToDateTime(tglst.Text) + "' WHERE NoKontrak = '" + NoKontrak + "'");
                    }
                    if (tglttd.Text != "")
                    {
                        Db.Execute("UPDATE MS_BAST SET TglTTDST='" + Convert.ToDateTime(tglttd.Text) + "' WHERE NoKontrak = '" + NoKontrak + "'");
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
                    DataTable rs = Db.Rs("SELECT"
                            + " A.NoKontrak AS [No. Kontrak]"
                            + ",B.NoUnit AS [Unit]"
                            + ",C.Nama AS [Customer]"
                            + ",A.NoST AS [No. BAST]"
                            + ",A.LuasGross AS [Luas Tanah]"
                            + ",A.LuasNett AS [Luas Bangunan]"
                            + ",CONVERT(varchar, A.TargetST, 106) AS [Tanggal Target BAST]"
                            + ",CONVERT(varchar, A.TglST, 106) AS [Tanggal BAST]"
                            + ",CONVERT(varchar, A.TglTTDST, 106) AS [Tanggal TTD BAST]"
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
                           + " 'TTD-BAST'"
                           + ",'" + Act.UserID + "'"
                           + ",'" + Act.IP + "'"
                           + ",'" + ket + "'"
                           + ",'" + NoKontrak + "'"
                           );


                    decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
                    string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                    Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                }
                if (dariReminder.Checked)
                    Response.Redirect("ReminderST.aspx?done=" + NoKontrak);
                else
                    Response.Redirect("KontrakSTTTD.aspx?done=" + NoKontrak);
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
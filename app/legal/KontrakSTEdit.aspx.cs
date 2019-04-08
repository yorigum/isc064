using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LEGAL
{

    public partial class KontrakSTEdit : System.Web.UI.Page
    {
        string foCheck, focounterCheck;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Act.Sec("ED:" + Request.PhysicalPath))
            {
                ok.Enabled = true;
                save.Enabled = true;
            }

            if (!Page.IsPostBack)
            {
                //btnpop.Attributes["onclick"] = "popDaftarVA('" + NoUnit + "')";
                //nova.Attributes.Add("readonly", "readonly");
                validNilai(); //validasi input nilai
                Fill();
            }

            FeedBack();
            Js.Confirm(this, "Lanjutkan proses edit serah terima?\\n");
        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "Edit Berhasil...";
            }
        }

        private void validNilai()
        {
            nilaibiaya.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            nilaibiaya.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            nilaibiaya.Attributes["onblur"] = "CalcBlur(this);";

            nilaibiaya1.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            nilaibiaya1.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            nilaibiaya1.Attributes["onblur"] = "CalcBlur(this);";

            nilaibiaya2.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            nilaibiaya2.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            nilaibiaya2.Attributes["onblur"] = "CalcBlur(this);";

            luas.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            luas.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            luas.Attributes["onblur"] = "CalcBlur(this);";

            luas1.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            luas1.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            luas1.Attributes["onblur"] = "CalcBlur(this);";

            luas2.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            luas2.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            luas2.Attributes["onblur"] = "CalcBlur(this);";

            luasnett.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            luasnett.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            luasnett.Attributes["onblur"] = "CalcBlur(this);";

            luasnett1.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            luasnett1.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            luasnett1.Attributes["onblur"] = "CalcBlur(this);";

            luasnett2.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            luasnett2.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            luasnett2.Attributes["onblur"] = "CalcBlur(this);";

            lebihbayar.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            lebihbayar.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            lebihbayar.Attributes["onblur"] = "CalcBlur(this);";

            lebihbayar1.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            lebihbayar1.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            lebihbayar1.Attributes["onblur"] = "CalcBlur(this);";

            lebihbayar2.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            lebihbayar2.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            lebihbayar2.Attributes["onblur"] = "CalcBlur(this);";
        }

        private void Fill()
        {
            //aKey.HRef = "javascript:openModal('KontrakEditKey.aspx?NoKontrak=" + NoKontrak + "','350','220')";
            //aStatus.HRef = "javascript:openModal('KontrakStatus.aspx?NoKontrak=" + NoKontrak + "','500','500')";
            printBAST.HRef = "PrintBAST.aspx?NoKontrak=" + NoKontrak;

            btnlog.Attributes["onclick"] = "location.href='LogPk.aspx?Tb=MS_KONTRAK_LOG&Pk=" + NoKontrak + "'";
            //btndel.Attributes["onclick"] = "location.href='KontrakDel.aspx?NoKontrak=" + NoKontrak + "'";
            aStatus.HRef = "javascript:openModal('KontrakStatus.aspx?NoKontrak=" + NoKontrak + "','500','500')";
            refresh.Attributes["onclick"] = "if(confirm('"
                + "Apakah anda ingin mengambil ulang data unit ?\\n"
                + "Perhatian bahwa nilai GROSS dan DISKON bisa berubah."
                + "'))"
                + "{location.href='KontrakRefresh.aspx?NoKontrak=" + NoKontrak + "'}";

            //string strSql = "SELECT *"
            //    + " FROM MS_BAST WHERE NoKontrak = '" + NoKontrak + "'";
            string strSql = "SELECT A.NoKontrak,A.TargetST AS Target,B.*"
                    + " FROM MS_KONTRAK A LEFT JOIN MS_BAST B ON A.NoKontrak = B.NoKontrak "
                    + "WHERE A.NoKontrak = '" + NoKontrak + "'";

            DataTable rs = Db.Rs(strSql);
            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                //    B = BELUM
                //    D = SUDAH REGIS
                //    T = PROSES TTD
                //    S = SELESAI
                stat.SelectedValue = rs.Rows[0]["ST"].ToString();
                if (rs.Rows[0]["ST"].ToString() == "S")
                {
                    tgltarget.Text = Cf.Day(rs.Rows[0]["TargetST"]);
                }
                if (rs.Rows[0]["ST"].ToString() == "T")
                {
                    tgltarget.Text = Cf.Day(rs.Rows[0]["Target"]);
                }
                if (rs.Rows[0]["ST"].ToString() == "D" || rs.Rows[0]["ST"].ToString() == "T")
                {
                    nost.Text = rs.Rows[0]["NoST"].ToString();
                    nost1.Text = rs.Rows[0]["NoST"].ToString();
                    nost2.Text = rs.Rows[0]["NoST"].ToString();
                    nostm.Text = rs.Rows[0]["NoSTm"].ToString();
                    nostm1.Text = rs.Rows[0]["NoSTm"].ToString();
                    nostm2.Text = rs.Rows[0]["NoSTm"].ToString();
                    tgltarget.Text = Cf.Day(rs.Rows[0]["Target"]);
                    tglst.Text = Cf.Day(rs.Rows[0]["TglST"]);
                    tglst1.Text = Cf.Day(rs.Rows[0]["TglST"]);
                    tglst2.Text = Cf.Day(rs.Rows[0]["TglST"]);
                }
                luas.Text = Cf.Num(rs.Rows[0]["LuasGross"]);
                luas1.Text = Cf.Num(rs.Rows[0]["LuasGross"]);
                luas2.Text = Cf.Num(rs.Rows[0]["LuasGross"]);
                luasnett.Text = Cf.Num(rs.Rows[0]["LuasNett"]);
                luasnett1.Text = Cf.Num(rs.Rows[0]["LuasNett"]);
                luasnett2.Text = Cf.Num(rs.Rows[0]["LuasNett"]);
                nilaibiaya.Text = Cf.Num(rs.Rows[0]["Biaya"]);
                nilaibiaya1.Text = Cf.Num(rs.Rows[0]["Biaya"]);
                nilaibiaya2.Text = Cf.Num(rs.Rows[0]["Biaya"]);
                lebihbayar.Text = Cf.Num(rs.Rows[0]["LebihBayar"]);
                lebihbayar1.Text = Cf.Num(rs.Rows[0]["LebihBayar"]);
                lebihbayar2.Text = Cf.Num(rs.Rows[0]["LebihBayar"]);
                tglttd.Text = Cf.Day(rs.Rows[0]["TglTTDST"]);
                tglttd2.Text = Cf.Day(rs.Rows[0]["TglTTDST"]);
                stused.SelectedValue = rs.Rows[0]["STu"].ToString();
                keterangan.Text = rs.Rows[0]["KetST"].ToString();
                keterangan1.Text = rs.Rows[0]["KetST"].ToString();
                keterangan2.Text = rs.Rows[0]["KetST"].ToString();

                ubah();
                //luas.Text = Cf.Num(rs.Rows[0]["Luas"]);
                if (rs.Rows[0]["TglST"] is DBNull)
                {
                    tglst.Text = Cf.Day(rs.Rows[0]["Target"]);
                }

                // B = Belum, S = Target, D = Serah Terima, T = Tanda Tangan
           }
        }

        private bool valid()
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

            if (stat.SelectedValue == "")
            {
                x = false;
            }

            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Format Tanggal : Bulan / Tanggal / Tahun.\\n"
                    + "2. Harap memilih kondisi dari tiap tiap kelengkapan data"
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );

            return x;
        }

        private bool Save()
        {
            if (valid())
            {
                if (belum.Visible)
                {
                    DateTime TglST = Convert.ToDateTime(tglst.Text);
                    decimal Luas = Convert.ToDecimal(luas.Text);
                    decimal LuasNett = Convert.ToDecimal(luasnett.Text);

                    DataTable rs = Db.Rs("SELECT"
                        + " A.NoKontrak AS [No. Kontrak]"
                        + ",B.NoUnit AS [Unit]"
                        + ",C.Nama AS [Customer]"
                        + ",CONVERT(varchar, A.TargetST, 106) AS [Jadwal Serah Terima]"
                        + ",A.NoST AS [No. BAST]"
                        + ",B.Luas AS [Luas]"
                        + ",B.TargetST AS [TglTarget]"
                        + ",A.Biaya"
                        + ",A.LebihBayar"
                        + ",A.LuasGross"
                        + ",A.LuasNett"
                        + ",A.KetST"
                        + ",CONVERT(varchar, A.TglST, 106) AS [Tanggal BAST]"
                        + ",PersenLunas AS [Prosentase Pelunasan]"
                        + " FROM MS_BAST A INNER JOIN MS_KONTRAK B"
                        + " ON A.NoKontrak = B.NoKontrak"
                        + " INNER JOIN MS_CUSTOMER C ON B.NoCustomer = C.NoCustomer"
                        + " WHERE A.NoKontrak = '" + NoKontrak + "'");

                    Db.Execute("UPDATE MS_BAST SET"
                             + " STu=" + stused.SelectedValue
                             + ",ST='" + stat.SelectedValue + "'"
                             + ",NoSTm='" + nostm.Text + "'"
                             + ",LuasGross ='" + luas.Text + "'"
                             + ",LuasNett ='" + luasnett.Text + "'"
                             + ",Biaya ='" + nilaibiaya.Text + "'"
                             + ",LebihBayar ='" + lebihbayar.Text + "'"
                             + ",KetST ='" + keterangan.Text + "'"
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

                    string ket = Cf.LogCapture(rs)
                        //+ Cf.LogCompare(rsBef, rsAft)
                        //+ "<br>Biaya Administrasi : " + Cf.Num(NilaiBiaya)
                        ;

                    Db.Execute("EXEC spLogKontrak "
                        + " 'ST'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + ket + "'"
                        + ",'" + NoKontrak + "'"
                        );

                    decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
                    string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                    Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                }
                else if (target.Visible)
                {
                    DateTime TglST = Convert.ToDateTime(tglst.Text);
                    decimal Luas = Convert.ToDecimal(luas.Text);
                    decimal LuasNett = Convert.ToDecimal(luasnett.Text);

                    DataTable rs = Db.Rs("SELECT"
                        + " A.NoKontrak AS [No. Kontrak]"
                        + ",B.NoUnit AS [Unit]"
                        + ",C.Nama AS [Customer]"
                        + ",CONVERT(varchar, A.TargetST, 106) AS [Jadwal Serah Terima]"
                        + ",A.NoST AS [No. BAST]"
                        + ",B.Luas AS [Luas]"
                        + ",B.TargetST AS [TglTarget]"
                        + ",A.Biaya"
                        + ",A.LebihBayar"
                        + ",A.LuasGross"
                        + ",A.LuasNett"
                        + ",A.KetST"
                        + ",CONVERT(varchar, A.TglST, 106) AS [Tanggal BAST]"
                        + ",PersenLunas AS [Prosentase Pelunasan]"
                        + " FROM MS_BAST A INNER JOIN MS_KONTRAK B"
                        + " ON A.NoKontrak = B.NoKontrak"
                        + " INNER JOIN MS_CUSTOMER C ON B.NoCustomer = C.NoCustomer"
                        + " WHERE A.NoKontrak = '" + NoKontrak + "'");

                    Db.Execute("UPDATE MS_BAST SET"
                             + " STu=" + stused.SelectedValue
                             + ",ST='" + stat.SelectedValue + "'"
                             + " WHERE NoKontrak = '" + NoKontrak + "'"
                               );

                    tgltarget.Text = Cf.Day(rs.Rows[0]["TglTarget"]);

                    if (tgltarget.Text != "")
                    {
                        Db.Execute("UPDATE MS_BAST SET TglST='" + Convert.ToDateTime(tgltarget.Text) + "' WHERE NoKontrak = '" + NoKontrak + "'");
                    }

                    //decimal NilaiBiaya = Convert.ToDecimal(nilaibiaya.Text);
                    //if (NilaiBiaya != 0)
                    //{
                    //    Db.Execute("EXEC spTagihanDaftar "
                    //        + " '" + NoKontrak + "'"
                    //        + ",'BIAYA ADM. SERAH TERIMA'"
                    //        + ",'" + TglST + "'"
                    //        + ", " + NilaiBiaya
                    //        + ",'ADM'"
                    //        );
                    //}

                    string ket = Cf.LogCapture(rs)
                        //+ Cf.LogCompare(rsBef, rsAft)
                        //+ "<br>Biaya Administrasi : " + Cf.Num(NilaiBiaya)
                        ;

                    Db.Execute("EXEC spLogKontrak "
                        + " 'ST'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + ket + "'"
                        + ",'" + NoKontrak + "'"
                        );

                    decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
                    string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                    Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                }
                else if (bast.Visible)
                {
                    DateTime TglST = Convert.ToDateTime(tglst.Text);
                    decimal Luas = Convert.ToDecimal(luas.Text);
                    decimal LuasNett = Convert.ToDecimal(luasnett.Text);

                    DataTable rs = Db.Rs("SELECT"
                        + " A.NoKontrak AS [No. Kontrak]"
                        + ",B.NoUnit AS [Unit]"
                        + ",C.Nama AS [Customer]"
                        + ",CONVERT(varchar, A.TargetST, 106) AS [Jadwal Serah Terima]"
                        + ",A.NoST AS [No. BAST]"
                        + ",B.Luas AS [Luas]"
                        + ",B.TargetST AS [TglTarget]"
                        + ",A.Biaya"
                        + ",A.LebihBayar"
                        + ",A.LuasGross"
                        + ",A.LuasNett"
                        + ",A.KetST"
                        + ",CONVERT(varchar, A.TglST, 106) AS [Tanggal BAST]"
                        + ",PersenLunas AS [Prosentase Pelunasan]"
                        + " FROM MS_BAST A INNER JOIN MS_KONTRAK B"
                        + " ON A.NoKontrak = B.NoKontrak"
                        + " INNER JOIN MS_CUSTOMER C ON B.NoCustomer = C.NoCustomer"
                        + " WHERE A.NoKontrak = '" + NoKontrak + "'");

                    Db.Execute("UPDATE MS_BAST SET"
                             + " STu=" + stused1.SelectedValue
                             + ",ST='" + stat.SelectedValue + "'"
                             + ",NoSTm='" + nostm1.Text + "'"
                             + ",LuasGross ='" + luas1.Text + "'"
                             + ",LuasNett ='" + luasnett1.Text + "'"
                             + ",Biaya ='" + nilaibiaya1.Text + "'"
                             + ",LebihBayar ='" + lebihbayar1.Text + "'"
                             + ",KetST ='" + keterangan1.Text + "'"
                             + " WHERE NoKontrak = '" + NoKontrak + "'"
                               );

                    if (tglst1.Text != "")
                    {
                        Db.Execute("UPDATE MS_BAST SET TglST='" + Convert.ToDateTime(tglst1.Text) + "' WHERE NoKontrak = '" + NoKontrak + "'");
                    }

                    decimal NilaiBiaya = Convert.ToDecimal(nilaibiaya1.Text);
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

                    string ket = Cf.LogCapture(rs)
                        //+ Cf.LogCompare(rsBef, rsAft)
                        //+ "<br>Biaya Administrasi : " + Cf.Num(NilaiBiaya)
                        ;

                    Db.Execute("EXEC spLogKontrak "
                        + " 'ST'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + ket + "'"
                        + ",'" + NoKontrak + "'"
                        );

                    decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
                    string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                    Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                }
                else if (ttd.Visible)
                {
                    DateTime TglST = Convert.ToDateTime(tglst2.Text);
                    decimal Luas = Convert.ToDecimal(luas2.Text);
                    decimal LuasNett = Convert.ToDecimal(luasnett2.Text);

                    DataTable rs = Db.Rs("SELECT"
                        + " A.NoKontrak AS [No. Kontrak]"
                        + ",B.NoUnit AS [Unit]"
                        + ",C.Nama AS [Customer]"
                        + ",CONVERT(varchar, A.TargetST, 106) AS [Jadwal Serah Terima]"
                        + ",A.NoST AS [No. BAST]"
                        + ",B.Luas AS [Luas]"
                        + ",B.TargetST AS [TglTarget]"
                        + ",A.Biaya"
                        + ",A.LebihBayar"
                        + ",A.LuasGross"
                        + ",A.LuasNett"
                        + ",A.KetST"
                        + ",CONVERT(varchar, A.TglST, 106) AS [Tanggal BAST]"
                        + ",PersenLunas AS [Prosentase Pelunasan]"
                        + " FROM MS_BAST A INNER JOIN MS_KONTRAK B"
                        + " ON A.NoKontrak = B.NoKontrak"
                        + " INNER JOIN MS_CUSTOMER C ON B.NoCustomer = C.NoCustomer"
                        + " WHERE A.NoKontrak = '" + NoKontrak + "'");

                    Db.Execute("UPDATE MS_BAST SET"
                             + " STu=" + stused2.SelectedValue
                             + ",ST='" + stat.SelectedValue + "'"
                             + ",NoSTm='" + nostm2.Text + "'"
                             + ",LuasGross ='" + luas2.Text + "'"
                             + ",LuasNett ='" + luasnett2.Text + "'"
                             + ",Biaya ='" + nilaibiaya2.Text + "'"
                             + ",LebihBayar ='" + lebihbayar2.Text + "'"
                             + ",KetST ='" + keterangan2.Text + "'"
                             + " WHERE NoKontrak = '" + NoKontrak + "'"
                               );

                    if (tglst2.Text != "")
                    {
                        Db.Execute("UPDATE MS_BAST SET TglST='" + Convert.ToDateTime(tglst2.Text) + "' WHERE NoKontrak = '" + NoKontrak + "'");
                    }
                    if (tglttd2.Text != "")
                    {
                        Db.Execute("UPDATE MS_BAST SET TglTTDST='" + Convert.ToDateTime(tglttd2.Text) + "' WHERE NoKontrak = '" + NoKontrak + "'");
                    }

                    decimal NilaiBiaya = Convert.ToDecimal(nilaibiaya2.Text);
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

                    string ket = Cf.LogCapture(rs)
                        //+ Cf.LogCompare(rsBef, rsAft)
                        //+ "<br>Biaya Administrasi : " + Cf.Num(NilaiBiaya)
                        ;

                    Db.Execute("EXEC spLogKontrak "
                        + " 'ST'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + ket + "'"
                        + ",'" + NoKontrak + "'"
                        );

                    decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
                    string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                    Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                }
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void stat_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            ubah();
        }

        protected void ubah()
        {       
            if (stat.SelectedValue == "B")
            {
                belum.Visible = true;
                target.Visible = false;
                bast.Visible = false;
                ttd.Visible = false;
                //tbTgl.Text = Cf.Day(DateTime.Today);
            }
            else if (stat.SelectedValue == "S")
            {
                target.Visible = true;
                bast.Visible = false;
                ttd.Visible = false;
                belum.Visible = false;
                //tbTglTarget.Text = Cf.Day(DateTime.Today);
            }
            else if (stat.SelectedValue == "D")
            {
                bast.Visible = true;
                ttd.Visible = false;
                target.Visible = false;
                belum.Visible = false;
                //tbTglProses.Text= Cf.Day(DateTime.Today);
            }
            else if (stat.SelectedValue == "T")
            {
                ttd.Visible = true;
                target.Visible = false;
                bast.Visible = false;
                belum.Visible = false;
                //tbTgl.Text = Cf.Day(DateTime.Today);
            }
        }

        protected void ok_Click(object sender, System.EventArgs e)
        {
            if (Save()) Js.Close(this);
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            if (Save()) Response.Redirect("KontrakSTEdit.aspx?done=1&NoKontrak=" + NoKontrak);
            //Save(); 
        }

        private string NoKontrak
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoKontrak"]);
            }
        }

        private decimal checkNull(object obj)
        {
            if (obj == null)
                return 0;
            else
                return Convert.ToDecimal(obj);
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
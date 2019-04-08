using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LEGAL
{
	public partial class KontrakAJBEdit : System.Web.UI.Page
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
            Js.Confirm(this, "Lanjutkan proses edit kontrak?\\n");
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
        }

        private void Fill()
        {
            //aKey.HRef = "javascript:openModal('KontrakEditKey.aspx?NoKontrak=" + NoKontrak + "','350','220')";
            //aStatus.HRef = "javascript:openModal('KontrakStatus.aspx?NoKontrak=" + NoKontrak + "','500','500')";


            printAJB.HRef = "PrintAJB.aspx?NoKontrak=" + NoKontrak;

            btnlog.Attributes["onclick"] = "location.href='LogPk.aspx?Tb=MS_KONTRAK_LOG&Pk=" + NoKontrak + "'";
            //btndel.Attributes["onclick"] = "location.href='KontrakDel.aspx?NoKontrak=" + NoKontrak + "'";
            aStatus.HRef = "javascript:openModal('KontrakStatus.aspx?NoKontrak=" + NoKontrak + "','500','500')";
            refresh.Attributes["onclick"] = "if(confirm('"
                + "Apakah anda ingin mengambil ulang data unit ?\\n"
                + "Perhatian bahwa nilai GROSS dan DISKON bisa berubah."
                + "'))"
                + "{location.href='KontrakRefresh.aspx?NoKontrak=" + NoKontrak + "'}";

            //string strSql = "SELECT *"
            //    + " FROM MS_AJB WHERE NoKontrak = '" + NoKontrak + "'";
            //string strSql = "SELECT* FROM MS_KONTRAK WHERE NoKontrak NOT IN(SELECT NoKontrak FROM MS_PPJB)";
            string strSql = "SELECT A.NoKontrak,B.*"
                + " FROM MS_KONTRAK A LEFT JOIN MS_AJB B ON A.NoKontrak = B.NoKontrak "
                //+ " LEFT JOIN MS_PPJB C ON A.NoKontrak = C.NoKontrak"
                + "WHERE A.NoKontrak = '" + NoKontrak + "'";
            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                //    B = BELUM
                //    S = TARGET
                //    D = AJB
                //    T = PROSES TTD
                stat.SelectedValue = rs.Rows[0]["AJB"].ToString();
                tgltarget.Text = "";
                tglajb.Text = "";
                tglajb1.Text = "";
                tglajb2.Text = "";
                printAJB.InnerHtml = printAJB.InnerHtml + " (" + rs.Rows[0]["PrintAJB"] + ")";


                if (rs.Rows[0]["AJB"].ToString() == "D" || rs.Rows[0]["AJB"].ToString() == "T")
                {
                    noajb.Text = rs.Rows[0]["NoAJB"].ToString();
                    noajb1.Text = rs.Rows[0]["NoAJB"].ToString();
                    noajb2.Text = rs.Rows[0]["NoAJB"].ToString();
                    noajbm.Text = rs.Rows[0]["NoAJBm"].ToString();
                    noajbm1.Text = rs.Rows[0]["NoAJBm"].ToString();
                    noajbm2.Text = rs.Rows[0]["NoAJBm"].ToString();
                    tgltarget.Text = Cf.Day(rs.Rows[0]["TglTargetAJB"]);
                    tglajb.Text = Cf.Day(rs.Rows[0]["TglAJB"]);
                    tglajb1.Text = Cf.Day(rs.Rows[0]["TglAJB"]);
                    tglajb2.Text = Cf.Day(rs.Rows[0]["TglAJB"]);
                    tglttd2.Text = Cf.Day(rs.Rows[0]["TglTTDAJB"]);
                }

                if (Convert.ToString(rs.Rows[0]["AJB"]) == "S")
                {
                    tgltarget.Text = Cf.Day(rs.Rows[0]["TglTargetAJB"]);
                }
                ajbused.SelectedValue = rs.Rows[0]["AJBu"].ToString();
                ajbused1.SelectedValue = rs.Rows[0]["AJBu"].ToString();
                ajbused2.SelectedValue = rs.Rows[0]["AJBu"].ToString();
                notaris.Text = rs.Rows[0]["NamaNotaris"].ToString();
                notaris1.Text = rs.Rows[0]["NamaNotaris"].ToString();
                notaris2.Text = rs.Rows[0]["NamaNotaris"].ToString();
                keterangan.Text = rs.Rows[0]["KetAJB"].ToString();
                keterangan1.Text = rs.Rows[0]["KetAJB"].ToString();
                keterangan2.Text = rs.Rows[0]["KetAJB"].ToString();
                nilaibiaya.Text = Cf.Num(rs.Rows[0]["Biaya"]);
                nilaibiaya1.Text = Cf.Num(rs.Rows[0]["Biaya"]);
                nilaibiaya2.Text = Cf.Num(rs.Rows[0]["Biaya"]);

                if (stat.SelectedValue == "T")
                {
                    divTTD.Visible = true;
                }

                ubah();
            }
        }

        private bool valid()
        {
            string s = "";
            bool x = true;

            if (!Cf.isTgl(tglttd))
            {
                x = false;
                if (s == "") s = tglttd.ID;
                tglttdc.Text = "Tanggal";
            }
            else
            {
                tglttdc.Text = "";
            }

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
                //DataTable rsBef = Db.Rs("SELECT "
                //    + " NoKontrak AS [No. Kontrak]"
                //    + ", NoAJB"
                //    + ", NoAJBm"
                //    + ", AJB"
                //    + ", AJBu"
                //    + ", TglAJB"
                //    + ", TglTTDAJB"
                //    + ", Biaya"
                //    + " FROM MS_AJB"
                //    + " WHERE NoKontrak = '" + NoKontrak + "'"
                //    );
                if (belum.Visible)
                {
                    Db.Execute("UPDATE MS_AJB SET"
                         + " AJB ='" + stat.SelectedValue + "'"
                         + " ,NoAJBm='" + noajbm.Text + "'"
                         + ", AJBu='" + ajbused.SelectedValue + "'"
                         + ",NamaNotaris='" + notaris.Text + "'"
                         + ",KetAJB ='" + keterangan.Text + "'"
                         + " WHERE NoKontrak = '" + NoKontrak + "'"
                           );

                    if (tglttd.Text != "")
                    {
                        Db.Execute("UPDATE MS_AJB SET TglTTDAJB='" + Convert.ToDateTime(tglttd.Text) + "' WHERE NoKontrak = '" + NoKontrak + "'");
                    }
                    if (tglajb.Text != "")
                    {
                        Db.Execute("UPDATE MS_AJB SET TglAJB ='" + Convert.ToDateTime(tglajb.Text) + "' WHERE NoKontrak = '" + NoKontrak + "'");
                    }


                    DataTable rsAft = Db.Rs("SELECT "
                        + " NoKontrak AS [No. Kontrak]"
                        + ",CONVERT(varchar,TglAJB,106) AS [Tanggal AJB]"
                        + ", NoAJB"
                        + ", NoAJBm"
                        + ", AJB"
                        + ", AJBu"
                        + ", TglAJB"
                        + ", TglTTDAJB"
                        + ", Biaya"
                        + " FROM MS_AJB"
                        + " WHERE NoKontrak = '" + NoKontrak + "'"
                        );
                    //Logfile
                    string Ket = Cf.LogCapture(rsAft);

                    Db.Execute("EXEC spLogKontrak"
                        + " 'EDIT'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + Ket + "'"
                        + ",'" + NoKontrak + "'"
                        );

                    decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
                    string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                    Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                }
                else if (target.Visible)
                {
                    Db.Execute("UPDATE MS_AJB SET"
                         + " AJB ='" + stat.SelectedValue + "'"
                         + " WHERE NoKontrak = '" + NoKontrak + "'"
                           );

                    if (tgltarget.Text != "")
                    {
                        Db.Execute("UPDATE MS_AJB SET TglTargetAJB='" + Convert.ToDateTime(tgltarget.Text) + "' WHERE NoKontrak = '" + NoKontrak + "'");
                    }

                    DataTable rsAft = Db.Rs("SELECT "
                        + " NoKontrak AS [No. Kontrak]"
                        + ",CONVERT(varchar,TglAJB,106) AS [Tanggal AJB]"
                        + ", NoAJB"
                        + ", NoAJBm"
                        + ", AJB"
                        + ", AJBu"
                        + ", TglAJB"
                        + ", TglTTDAJB"
                        + ", TglTargetAJB"
                        + ", Biaya"
                        + " FROM MS_AJB"
                        + " WHERE NoKontrak = '" + NoKontrak + "'"
                        );
                    //Logfile
                    string Ket = Cf.LogCapture(rsAft);

                    Db.Execute("EXEC spLogKontrak"
                        + " 'EDIT'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + Ket + "'"
                        + ",'" + NoKontrak + "'"
                        );

                    decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
                    string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                    Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                }
                else if (ajb.Visible)
                {
                    Db.Execute("UPDATE MS_AJB SET"
                         + " AJB ='" + stat.SelectedValue + "'"
                         + " ,NoAJBm='" + noajbm1.Text + "'"
                         + ", AJBu='" + ajbused1.SelectedValue + "'"
                         + ",NamaNotaris='" + notaris1.Text + "'"
                         + ",KetAJB ='" + keterangan1.Text + "'"
                         + " WHERE NoKontrak = '" + NoKontrak + "'"
                           );

                    if (tglajb1.Text != "")
                    {
                        Db.Execute("UPDATE MS_AJB SET TglAJB='" + Convert.ToDateTime(tglajb1.Text) + "' WHERE NoKontrak = '" + NoKontrak + "'");
                    }

                    DataTable rsAft = Db.Rs("SELECT "
                        + " NoKontrak AS [No. Kontrak]"
                        + ",CONVERT(varchar,TglAJB,106) AS [Tanggal AJB]"
                        + ", NoAJB"
                        + ", NoAJBm"
                        + ", AJB"
                        + ", AJBu"
                        + ", TglAJB"
                        + ", TglTTDAJB"
                        + ", Biaya"
                        + " FROM MS_AJB"
                        + " WHERE NoKontrak = '" + NoKontrak + "'"
                        );
                    //Logfile
                    string Ket = Cf.LogCapture(rsAft);

                    Db.Execute("EXEC spLogKontrak"
                        + " 'EDIT'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + Ket + "'"
                        + ",'" + NoKontrak + "'"
                        );
                    decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
                    string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                    Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                }
                else if (ttd.Visible)
                {
                    Db.Execute("UPDATE MS_AJB SET"
                         + " AJB ='" + stat.SelectedValue + "'"
                         + " ,NoAJBm='" + noajbm2.Text + "'"
                         + ", AJBu='" + ajbused2.SelectedValue + "'"
                         + ",NamaNotaris='" + notaris2.Text + "'"
                         + ",KetAJB ='" + keterangan2.Text + "'"
                         + " WHERE NoKontrak = '" + NoKontrak + "'"
                           );

                    if (tglttd2.Text != "")
                    {
                        Db.Execute("UPDATE MS_AJB SET TglTTDAJB='" + Convert.ToDateTime(tglttd2.Text) + "' WHERE NoKontrak = '" + NoKontrak + "'");
                    }
                    if (tglajb2.Text != "")
                    {
                        Db.Execute("UPDATE MS_AJB SET TglAJB ='" + Convert.ToDateTime(tglajb2.Text) + "' WHERE NoKontrak = '" + NoKontrak + "'");
                    }

                    DataTable rsAft = Db.Rs("SELECT "
                        + " NoKontrak AS [No. Kontrak]"
                        + ",CONVERT(varchar,TglAJB,106) AS [Tanggal AJB]"
                        + ", NoAJB"
                        + ", NoAJBm"
                        + ", AJB"
                        + ", AJBu"
                        + ", TglAJB"
                        + ", TglTTDAJB"
                        + ", Biaya"
                        + " FROM MS_AJB"
                        + " WHERE NoKontrak = '" + NoKontrak + "'"
                        );
                    //Logfile
                    string Ket = Cf.LogCapture(rsAft);

                    Db.Execute("EXEC spLogKontrak"
                        + " 'EDIT'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + Ket + "'"
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
                ajb.Visible = false;
                ttd.Visible = false;
                //tbTgl.Text = Cf.Day(DateTime.Today);
            }
            else if (stat.SelectedValue == "S")
            {
                target.Visible = true;
                ajb.Visible = false;
                ttd.Visible = false;
                belum.Visible = false;
                //tbTglTarget.Text = Cf.Day(DateTime.Today);
            }
            else if (stat.SelectedValue == "D")
            {
                ajb.Visible = true;
                ttd.Visible = false;
                target.Visible = false;
                belum.Visible = false;
                //tbTglProses.Text= Cf.Day(DateTime.Today);
            }
            else if (stat.SelectedValue == "T")
            {
                ttd.Visible = true;
                target.Visible = false;
                ajb.Visible = false;
                belum.Visible = false;
                //tbTgl.Text = Cf.Day(DateTime.Today);
            }
            else
            {
                belum.Visible = true;
                target.Visible = false;
                ajb.Visible = false;
                ttd.Visible = false;
            }

        }

        protected void ok_Click(object sender, System.EventArgs e)
        {
            if (Save()) Js.Close(this);
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            if (Save()) Response.Redirect("KontrakAJBEdit.aspx?done=1&NoKontrak=" + NoKontrak);
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
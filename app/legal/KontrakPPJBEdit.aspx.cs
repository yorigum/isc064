using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LEGAL
{

    public partial class KontrakPPJBEdit : System.Web.UI.Page
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
                Bind();
                validNilai();
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
                        + "Edit PPJB Berhasil...";
            }
        }

        private void validNilai()
        {
            //nilaibiaya.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            //nilaibiaya.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            //nilaibiaya.Attributes["onblur"] = "CalcBlur(this);";

            nilaibiaya1.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            nilaibiaya1.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            nilaibiaya1.Attributes["onblur"] = "CalcBlur(this);";

            nilaibiaya2.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            nilaibiaya2.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            nilaibiaya2.Attributes["onblur"] = "CalcBlur(this);";
        }

        private void Bind()
        {
            string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
            string strSql = "SELECT DISTINCT"
                + " A.Nama"
                + ",A.SN"                
                + " FROM REF_BERKAS_PPJB A WHERE Project = '" + Project + "'";

            DataTable rs = Db.Rs(strSql);
            Rpt.NoData(list, rs, "Daftar unit untuk kondisi price list yang dipilih tidak ada.");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                Label l;
                TextBox t;
                TextBox tgl;
                CheckBox cb;
                HtmlInputButton btn;

                l = new Label();
                l.Text = "<tr>"
                    + "<td>" + rs.Rows[i]["Nama"] + "</td>"
                    + "<td>:</td>";
                list.Controls.Add(l);

                l = new Label();
                l.Text = "<td>";
                list.Controls.Add(l);

                RadioButtonList r1 = new RadioButtonList();
                r1.ID = "rb_" + i;
                r1.Enabled = false;
                r1.Items.Add(new ListItem("Tidak Ada", "T"));
                r1.Items.Add(new ListItem("Ada", "A"));
                r1.RepeatDirection = RepeatDirection.Horizontal;
                r1.CellPadding = 10;
                if (NoKontrak != "") r1.SelectedIndex = Db.SingleBool("Select ISNULL((SELECT CAST(ISNULL (Value,0) AS bit) From MS_BERKAS_PPJB WHERE NoKontrak =  '" + NoKontrak + "' AND NoBerkas='" + (i + 1) + "'),0)") ? 1 : 0;
                list.Controls.Add(r1);

                l = new Label();
                l.Text = "</td></tr>";
                list.Controls.Add(l);

                DataTable a = Db.Rs("Select DISTINCT(TglLengkap) From MS_BERKAS_PPJB WHERE NoKontrak =  '" + NoKontrak + "'");                
                tglkp.Text = (a.Rows.Count == 0) ? "" : Cf.Day(a.Rows[0]["TglLengkap"]);

            }
        }

        private void Fill()
        {

            

            btnlog.Attributes["onclick"] = "location.href='LogPk.aspx?Tb=MS_KONTRAK_LOG&Pk=" + NoKontrak + "'";
         
            aStatus.HRef = "javascript:openModal('KontrakStatus.aspx?NoKontrak=" + NoKontrak + "','500','500')";
            refresh.Attributes["onclick"] = "if(confirm('"
                + "Apakah anda ingin mengambil ulang data unit ?\\n"
                + "Perhatian bahwa nilai GROSS dan DISKON bisa berubah"
                + "'))"
                + "{location.href='KontrakRefresh.aspx?NoKontrak=" + NoKontrak + "'}";

            string strSql = "SELECT A.NoKontrak,B.*"
                + " FROM MS_KONTRAK A LEFT JOIN MS_PPJB B ON A.NoKontrak = B.NoKontrak "
                + "WHERE A.NoKontrak = '" + NoKontrak + "'";

            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                //    B = BELUM
                //    S = TARGET
                //    D = PPJB
                //    T = PROSES TTD
                stat.SelectedValue = rs.Rows[0]["PPJB"].ToString();
                noppjb1.Text = "";
                noppjb2.Text = "";
                noppjbm1.Text = "";
                noppjbm2.Text = "";

                if (Convert.ToString(rs.Rows[0]["PPJB"]) == "D" || Convert.ToString(rs.Rows[0]["PPJB"]) == "T")
                {
                    noppjb1.Text = rs.Rows[0]["NoPPJB"].ToString();
                    noppjb2.Text = rs.Rows[0]["NoPPJB"].ToString();
                    noppjbm1.Text = rs.Rows[0]["NoPPJBm"].ToString();
                    noppjbm2.Text = rs.Rows[0]["NoPPJBm"].ToString();
                }
                //tglppjb1.Text = Cf.Day(rs.Rows[0]["TglPPJB"]);
                //tglppjb2.Text = Cf.Day(rs.Rows[0]["TglPPJB"]);
                //tglttd2.Text = Cf.Day(rs.Rows[0]["TglTTDPPJB"]);
                //tglkp.Text = Cf.Day(rs.Rows[0]["TglLengkapPPJB"]);
                keterangan1.Text = Cf.Str(rs.Rows[0]["KetPPJB"]);
                keterangan2.Text = Cf.Str(rs.Rows[0]["KetPPJB"]);
                nilaibiaya1.Text = Cf.Num(rs.Rows[0]["Biaya"]);
                nilaibiaya2.Text = Cf.Num(rs.Rows[0]["Biaya"]);
                ppjbused1.SelectedValue = rs.Rows[0]["PPJBu"].ToString();
                ppjbused2.SelectedValue = rs.Rows[0]["PPJBu"].ToString();

                ubah();
                //if (Convert.ToString(rs.Rows[0]["PPJB"]) == "B")
                //{
                //    tglkp.Text = Cf.Day(rs.Rows[0]["TglLengkapPPJB"]);
                //}
                if (Convert.ToString(rs.Rows[0]["PPJB"]) == "S")
                {
                    tbTglTarget.Text = Cf.Day(rs.Rows[0]["TglTargetPPJB"]);
                    tglppjb1.Text = ""; 
                    tglppjb2.Text = "";
                    tglkp.Text = Cf.Day(rs.Rows[0]["TglLengkapPPJB"]);
                }
                else if (Convert.ToString(rs.Rows[0]["PPJB"]) == "D")
                {
                    tbTglTarget.Text = Cf.Day(rs.Rows[0]["TglTargetPPJB"]);
                    tglppjb1.Text = Cf.Day(rs.Rows[0]["TglPPJB"]);
                    tglppjb2.Text = Cf.Day(rs.Rows[0]["TglPPJB"]);
                    tglkp.Text = Cf.Day(rs.Rows[0]["TglLengkapPPJB"]);
                }
                else if (Convert.ToString(rs.Rows[0]["PPJB"]) == "T")
                {
                    tbTglTarget.Text = Cf.Day(rs.Rows[0]["TglTargetPPJB"]);
                    tglppjb1.Text = Cf.Day(rs.Rows[0]["TglPPJB"]);
                    tglppjb2.Text = Cf.Day(rs.Rows[0]["TglPPJB"]);
                    tglttd2.Text = Cf.Day(rs.Rows[0]["TglTTDPPJB"]);
                    tglkp.Text = Cf.Day(rs.Rows[0]["TglLengkapPPJB"]);
                }

                printPPJB.HRef = "PrintPPJB.aspx?NoKontrak=" + NoKontrak + "&project=" + rs.Rows[0]["Project"];

            }
        }

        private bool valid()
        {
            string s = "";
            bool x = true;

            if (!Cf.isTgl(tglkp))
            {
                x = false;
                if (s == "") s = tglkp.ID;
                tglkpc.Text = "Tanggal";
            }
            else
            {
                tglkpc.Text = "";
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
                string Status = "";
                Status = stat.SelectedValue;
                if (belum.Visible)
                {
                    Db.Execute("UPDATE MS_PPJB SET"
                        + " PPJB ='" + stat.SelectedValue + "'"
                        + " WHERE NoKontrak = '" + NoKontrak + "'"
                          );

                    if (tglkp.Text != "")
                    {
                        Db.Execute("UPDATE MS_PPJB SET TglLengkapPPJB='" + Convert.ToDateTime(tglkp.Text) + "' WHERE NoKontrak = '" + NoKontrak + "'");
                    }
                    DataTable rsAft = Db.Rs("SELECT "
                   + " NoKontrak AS [No. Kontrak]"
                   //+ ",CONVERT(varchar,TglKontrak,106) AS [Tanggal Kontrak]"
                   + ", NoPPJB"
                   + ", NoPPJBm"
                   + ", PPJB"
                   + ", PPJBu"
                   + ", TglPPJB"
                   + ", TglCetakPPJB"
                   + ", TglTTDPPJB"
                   + ", TglLengkapPPJB"
                   + ", KTPMilik"
                   + ", KTPIstri"
                   + ", KK"
                   + ", SNKH"
                   + ", SKK"
                   + ", RK"
                   + ", BT"
                   + ", KW"
                   + ", DU"
                   + ", DL"
                   + ", SM"
                   + " FROM MS_PPJB"
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
                    if (tbTglTarget.Text != "")
                    {
                        Db.Execute("UPDATE MS_PPJB SET TglTargetPPJB='" + Convert.ToDateTime(tbTglTarget.Text) + "' WHERE NoKontrak = '" + NoKontrak + "'");
                    }
                    DataTable rsAft = Db.Rs("SELECT "
                   + " NoKontrak AS [No. Kontrak]"
                   //+ ",CONVERT(varchar,TglKontrak,106) AS [Tanggal Kontrak]"
                   + ", NoPPJB"
                   + ", NoPPJBm"
                   + ", PPJB"
                   + ", PPJBu"
                   + ", TglPPJB"
                   + ", TglCetakPPJB"
                   + ", TglTTDPPJB"
                   + ", TglLengkapPPJB"
                   + ", KTPMilik"
                   + ", KTPIstri"
                   + ", KK"
                   + ", SNKH"
                   + ", SKK"
                   + ", RK"
                   + ", BT"
                   + ", KW"
                   + ", DU"
                   + ", DL"
                   + ", SM"
                   + " FROM MS_PPJB"
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
                else if (selesai.Visible)
                {
                    Db.Execute("UPDATE MS_PPJB SET"
                        + " PPJB ='" + stat.SelectedValue + "'"
                        + ", PPJBu=" + ppjbused1.SelectedValue
                        + ",Biaya='" + nilaibiaya1.Text + "'"
                        + ",KetPPJB='" + keterangan1.Text + "'"
                        + " WHERE NoKontrak = '" + NoKontrak + "'"
                          );

                    if (tglppjb1.Text != "")
                    {
                        Db.Execute("UPDATE MS_PPJB SET TglPPJB='" + Convert.ToDateTime(tglppjb1.Text) + "' WHERE NoKontrak = '" + NoKontrak + "'");
                    }
                    DataTable rsAft = Db.Rs("SELECT "
                   + " NoKontrak AS [No. Kontrak]"
                   //+ ",CONVERT(varchar,TglKontrak,106) AS [Tanggal Kontrak]"
                   + ", NoPPJB"
                   + ", NoPPJBm"
                   + ", PPJB"
                   + ", PPJBu"
                   + ", TglPPJB"
                   + ", TglCetakPPJB"
                   + ", TglTTDPPJB"
                   + ", TglLengkapPPJB"
                   + ", KTPMilik"
                   + ", KTPIstri"
                   + ", KK"
                   + ", SNKH"
                   + ", SKK"
                   + ", RK"
                   + ", BT"
                   + ", KW"
                   + ", DU"
                   + ", DL"
                   + ", SM"
                   + " FROM MS_PPJB"
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
                    Db.Execute("UPDATE MS_PPJB SET"
                        + " PPJB ='" + stat.SelectedValue + "'"
                        + ", PPJBu=" + ppjbused2.SelectedValue
                        + ",Biaya='" + nilaibiaya2.Text + "'"
                        + ",KetPPJB='" + keterangan2.Text + "'"
                        + " WHERE NoKontrak = '" + NoKontrak + "'"
                          );

                    if (tglttd2.Text != "")
                    {
                        Db.Execute("UPDATE MS_PPJB SET TglTTDPPJB='" + Convert.ToDateTime(tglttd2.Text) + "' WHERE NoKontrak = '" + NoKontrak + "'");
                    }
                    if (tglppjb2.Text != "")
                    {
                        Db.Execute("UPDATE MS_PPJB SET TglPPJB='" + Convert.ToDateTime(tglppjb2.Text) + "' WHERE NoKontrak = '" + NoKontrak + "'");
                    }
                    DataTable rsAft = Db.Rs("SELECT "
                   + " NoKontrak AS [No. Kontrak]"
                   //+ ",CONVERT(varchar,TglKontrak,106) AS [Tanggal Kontrak]"
                   + ", NoPPJB"
                   + ", NoPPJBm"
                   + ", PPJB"
                   + ", PPJBu"
                   + ", TglPPJB"
                   + ", TglCetakPPJB"
                   + ", TglTTDPPJB"
                   + ", TglLengkapPPJB"
                   + ", KTPMilik"
                   + ", KTPIstri"
                   + ", KK"
                   + ", SNKH"
                   + ", SKK"
                   + ", RK"
                   + ", BT"
                   + ", KW"
                   + ", DU"
                   + ", DL"
                   + ", SM"
                   + " FROM MS_PPJB"
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
                selesai.Visible = false;
                ttd.Visible = false;
                list.Controls.Clear();
                Bind();
                //tbTgl.Text = Cf.Day(DateTime.Today);
            }
            else if (stat.SelectedValue == "S")
            {
                target.Visible = true;
                selesai.Visible = false;
                ttd.Visible = false;
                belum.Visible = false;
                //tbTglTarget.Text = Cf.Day(DateTime.Today);
            }
            else if (stat.SelectedValue == "D")
            {
                selesai.Visible = true;
                ttd.Visible = false;
                target.Visible = false;
                belum.Visible = false;
                //tbTglProses.Text= Cf.Day(DateTime.Today);
            }
            else if (stat.SelectedValue == "T")
            {
                ttd.Visible = true;
                target.Visible = false;
                selesai.Visible = false;
                belum.Visible = false;
                //tbTgl.Text = Cf.Day(DateTime.Today);
            }
            else
            {
                belum.Visible = true;
                target.Visible = false;
                selesai.Visible = false;
                ttd.Visible = false;
            }
        }


        protected void ok_Click(object sender, System.EventArgs e)
        {
            if (Save()) Js.Close(this);
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            if (Save()) Response.Redirect("KontrakPPJBEdit.aspx?done=1&NoKontrak=" + NoKontrak);
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
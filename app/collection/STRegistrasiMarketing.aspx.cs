using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.COLLECTION
{
    public partial class STRegistrasiMarketing : System.Web.UI.Page
    {
        protected DataTable rsTagihan;
        protected DataTable rs1;
        protected DataTable rs2;
        protected DataTable rs3;
        protected DataTable rs4;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                InitForm();
                Js.Focus(this, tgl);
            }

            Js.Confirm(this, "Lanjutkan proses registrasi surat tunggakan?");
            FillTb();
            FillSts();
        }

        private void InitForm()
        {
            tgl.Text = Cf.Day(DateTime.Today);
            gt.Attributes["style"] = "border:0px;font:bold;";

            tipe.Text = Tipe;
            referensi.Text = Ref;
            tglKuasa.Text = Cf.Day(DateTime.Today);

            unit.Text = Db.SingleString("SELECT NoUnit "
                + " FROM " + Tb + "..MS_KONTRAK "
                + " WHERE NoKontrak = '" + Ref + "'");

            DataTable rs = Db.Rs("SELECT Nama, NoTelp, Alamat1, Alamat2, Alamat3 "
                + " FROM " + Tb + "..MS_KONTRAK AS MS_KONTRAK "
                + " INNER JOIN " + Tb + "..MS_CUSTOMER AS MS_CUSTOMER "
                + " ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
                + " WHERE NoKontrak = '" + Ref + "'");
            if (rs.Rows.Count != 0)
            {
                customer.Text = rs.Rows[0]["Nama"].ToString();
                notelp.Text = rs.Rows[0]["NoTelp"].ToString();
                alamat1.Text = rs.Rows[0]["Alamat1"].ToString();
                alamat2.Text = rs.Rows[0]["Alamat2"].ToString();
                alamat3.Text = rs.Rows[0]["Alamat3"].ToString();
            }
        }

        private void FillTb()
        {
            string strSql = "SELECT * "
                + ",(NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Tb + "..MS_PELUNASAN WHERE NoTagihan = MS_TAGIHAN.NoUrut AND NoKontrak = '" + Ref + "') ) AS SisaTagihan,Denda"
                + " FROM " + Tb + "..MS_TAGIHAN AS MS_TAGIHAN WHERE NoKontrak = '" + Ref + "'"
                + " AND (NilaiTagihan - (SELECT ISNULL(SUM(NilaiPelunasan),0) FROM " + Tb + "..MS_PELUNASAN WHERE NoTagihan = MS_TAGIHAN.NoUrut AND NoKontrak = '" + Ref + "') ) > 0"
                + " AND CONVERT(VARCHAR,TglJT,112) < '" + Cf.Tgl112(DateTime.Today) + "'"
                + " ORDER BY TglJT, NoUrut";
            rsTagihan = Db.Rs(strSql);
            
            for (int i = 0; i < rsTagihan.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                Label l;
                TextBox t;

                l = new Label();
                l.Text = "<tr valign=top>"
                    + "<td>" + rsTagihan.Rows[i]["NoKontrak"] + "." + rsTagihan.Rows[i]["NoUrut"] + "</td>"
                    + "<td>" + rsTagihan.Rows[i]["NamaTagihan"] + "</td>"
                    + "<td>" + rsTagihan.Rows[i]["Tipe"] + "</td>"
                    + "<td style='white-space:nowrap'>" + Cf.Day(rsTagihan.Rows[i]["TglJT"]) + "</td>"
                    + "<td align=right>" + Cf.Num(rsTagihan.Rows[i]["SisaTagihan"]) + "</td>"
                    + "<td>";
                list.Controls.Add(l);

                t = new TextBox();
                t.ID = "lunas_" + i;
                t.Attributes["style"] = "display:none";
                list.Controls.Add(t);

                l = new Label();
                l.Text = "<input type='checkbox' onclick=\"tagihan('" + i + "','" + Cf.Num(rsTagihan.Rows[i]["SisaTagihan"]) + "',this)\"></td>"
                    + "</tr>";
                list.Controls.Add(l);
            }
        }

        private void FillSts()
        {
            level_1.Enabled = true;
            level_2.Enabled = true;
            level_3.Enabled = true;
            level_4.Enabled = true;

            trTglSuratKuasa.Visible = false;

            string stsSql_1 = "SELECT TOP 1 NoTunggakan, TglTunggakan FROM MS_TUNGGAKAN WHERE Ref = '" + Ref + "' AND LevelTunggakan = 1 ORDER BY TglTunggakan DESC";
            rs1 = Db.Rs(stsSql_1);
            string stsSql_2 = "SELECT TOP 1 NoTunggakan, TglTunggakan FROM MS_TUNGGAKAN WHERE Ref = '" + Ref + "' AND LevelTunggakan = 2 ORDER BY TglTunggakan DESC";
            rs2 = Db.Rs(stsSql_2);
            string stsSql_3 = "SELECT TOP 1 NoTunggakan, TglTunggakan FROM MS_TUNGGAKAN WHERE Ref = '" + Ref + "' AND LevelTunggakan = 3 ORDER BY TglTunggakan DESC";
            rs3 = Db.Rs(stsSql_3);
            string stsSql_4 = "SELECT TOP 1 NoTunggakan, TglTunggakan FROM MS_TUNGGAKAN WHERE Ref = '" + Ref + "' AND LevelTunggakan = 4 ORDER BY TglTunggakan DESC";
            rs4 = Db.Rs(stsSql_4);

            if (rs1.Rows.Count > 0)
            {
                string stat = Db.SingleString("SELECT TOP 1 Status FROM MS_TUNGGAKAN WHERE Ref = '" + Ref + "' AND LevelTunggakan = 1 ORDER BY TglTunggakan DESC");
                if (stat == "S")
                {
                    level_1.Enabled = true;
                }
                else
                {
                    level_1.Enabled = false;
                    stsLevel_1.Text = "<a href=\"javascript:call('" + rs1.Rows[0]["NoTunggakan"] + "')\">("
                                    + Cf.Day(rs1.Rows[0]["TglTunggakan"]) + ")</a>";
                }
            }

            if (rs2.Rows.Count > 0)
            {
                string stat = Db.SingleString("SELECT TOP 1 Status FROM MS_TUNGGAKAN WHERE Ref = '" + Ref + "' AND LevelTunggakan = 2 ORDER BY TglTunggakan DESC");
                if (stat == "S")
                {
                    level_2.Enabled = true;
                }
                else
                {
                    level_1.Enabled = false;
                    stsLevel_1.Text = "<a href=\"javascript:call('" + rs1.Rows[0]["NoTunggakan"] + "')\">("
                                    + Cf.Day(rs1.Rows[0]["TglTunggakan"]) + ")</a>";

                    level_2.Enabled = false;
                    stsLevel_2.Text = "<a href=\"javascript:call('" + rs2.Rows[0]["NoTunggakan"] + "')\">("
                                    + Cf.Day(rs2.Rows[0]["TglTunggakan"]) + ")</a>";
                }
            }

            if (rs3.Rows.Count > 0)
            {
                string stat = Db.SingleString("SELECT TOP 1 Status FROM MS_TUNGGAKAN WHERE Ref = '" + Ref + "' AND LevelTunggakan = 3 ORDER BY TglTunggakan DESC");
                if (stat == "S")
                {
                    level_3.Enabled = true;
                }
                else
                {
                    level_1.Enabled = false;
                    stsLevel_1.Text = "<a href=\"javascript:call('" + rs1.Rows[0]["NoTunggakan"] + "')\">("
                                    + Cf.Day(rs1.Rows[0]["TglTunggakan"]) + ")</a>";

                    level_2.Enabled = false;
                    stsLevel_2.Text = "<a href=\"javascript:call('" + rs2.Rows[0]["NoTunggakan"] + "')\">("
                                    + Cf.Day(rs2.Rows[0]["TglTunggakan"]) + ")</a>";

                    level_3.Enabled = false;
                    stsLevel_3.Text = "<a href=\"javascript:call('" + rs3.Rows[0]["NoTunggakan"] + "')\">("
                                    + Cf.Day(rs3.Rows[0]["TglTunggakan"]) + ")</a>";
                }
            }

            if (rs4.Rows.Count > 0)
            {
                string stat = Db.SingleString("SELECT TOP 1 Status FROM MS_TUNGGAKAN WHERE Ref = '" + Ref + "' AND LevelTunggakan = 4 ORDER BY TglTunggakan DESC");
                if (stat == "S")
                {
                    level_4.Enabled = true;
                }
                else
                {
                    level_4.Enabled = false;
                    stsLevel_4.Text = "<a href=\"javascript:call('" + rs4.Rows[0]["NoTunggakan"] + "')\">("
                                    + Cf.Day(rs4.Rows[0]["TglTunggakan"]) + ")</a>";
                }
            }

            if (level_1.Enabled == true)
            {
                level_1.Enabled = true;
                level_2.Enabled = false;
                level_3.Enabled = false;
                level_4.Enabled = false;

                level_1.Checked = true;
            }
            else if (level_2.Enabled == true)
            {
                level_1.Enabled = false;
                level_2.Enabled = true;
                level_3.Enabled = false;
                level_4.Enabled = false;

                level_2.Checked = true;
            }
            else if (level_3.Enabled == true)
            {
                level_1.Enabled = false;
                level_2.Enabled = false;
                level_3.Enabled = true;
                level_4.Enabled = false;

                level_3.Checked = true;
            }
            else if (level_4.Enabled == true)
            {
                trTglSuratKuasa.Visible = true;

                level_1.Enabled = false;
                level_2.Enabled = false;
                level_3.Enabled = false;
                level_4.Enabled = true;

                level_4.Checked = true;
            }

            if (level_1.Enabled == false && level_2.Enabled == false && level_3.Enabled == false && level_4.Enabled == false)
            {
                lvlc.Text = "Semua Surat Level Tunggakan sudah pernah diterbitkan";
                divInsert.Visible = false;
                save.Enabled = false;
                save.Visible = false;
            }
        }

        private bool datavalid()
        {
            string s = "";
            bool x = true;

            if (!Cf.isTgl(tgl))
            {
                x = false;
                if (s == "") s = tgl.ID;
                tglc.Text = "Tanggal";
            }
            else
                tglc.Text = "";

            bool adasatu = false;
            for (int i = 0; i < rsTagihan.Rows.Count; i++)
            {
                TextBox lunas = (TextBox)list.FindControl("lunas_" + i);
                if (lunas.Text != "")
                {
                    adasatu = true;
                    break;
                }
            }

            if (!adasatu)
            {
                x = false;
                if (s == "") s = gt.ID;
                gtc.Attributes["style"] = "color:red";
            }
            else
                gtc.Attributes["style"] = "color:black";

            if (!levelTunggakanValid())
            {
                x = false;
                lvlc.Text = "Pilih Level Tunggakan";
            }
            else if (!kuasaSomasiValid())
            {
                x = false;
                lvlc.Text = "Isi Tanggal Kuasa Somasi";
                tglKuasac.Text = "Tanggal";
            }
            else
                lvlc.Text = "";

            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Format Tanggal : Bulan / Tanggal / Tahun.\\n"
                    + "2. Surat minimal untuk satu tagihan.\\n"
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );

            return x;
        }

        protected bool levelTunggakanValid()
        {
            bool status = false;

            if (level_1.Checked == true) { status = true; }
            else if (level_2.Checked == true) { status = true; }
            else if (level_3.Checked == true) { status = true; }
            else if (level_4.Checked == true) { status = true; }

            return status;
        }

        protected bool kuasaSomasiValid()
        {
            bool status = true;

            if (level_4.Checked == true)
            {
                if (!Cf.isTgl(tglKuasa))
                {
                    status = false;
                }
            }

            return status;
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            if (datavalid())
            {
                DateTime TglTunggakan = Convert.ToDateTime(tgl.Text);
                string Unit = Cf.Str(unit.Text);
                string Customer = Cf.Str(customer.Text);
                string NoTelp = Cf.Str(notelp.Text);
                string Alamat1 = Cf.Str(alamat1.Text);
                string Alamat2 = Cf.Str(alamat2.Text);
                string Alamat3 = Cf.Str(alamat3.Text);

                Db.Execute("EXEC spTunggakanRegistrasi"
                    + " '" + TglTunggakan + "'"
                    + ",'" + Tipe + "'"
                    + ",'" + Ref + "'"
                    + ",'" + Unit + "'"
                    + ",'" + Customer + "'"
                    + ",'" + NoTelp + "'"
                    + ",'" + Alamat1 + "'"
                    + ",'" + Alamat2 + "'"
                    + ",'" + Alamat3 + "'"
                    );

                int NoTunggakan = Db.SingleInteger("SELECT TOP 1 NoTunggakan FROM MS_TUNGGAKAN ORDER BY NoTunggakan DESC");



                System.Text.StringBuilder alokasi = new System.Text.StringBuilder();

                for (int i = 0; i < rsTagihan.Rows.Count; i++)
                {
                    TextBox lunas = (TextBox)list.FindControl("lunas_" + i);
                    if (lunas.Text != "")
                    {
                        int NoTagihan = (int)rsTagihan.Rows[i]["NoUrut"];
                        string NamaTagihan = Cf.Str(rsTagihan.Rows[i]["NamaTagihan"]);
                        decimal Nilai = Convert.ToDecimal(rsTagihan.Rows[i]["SisaTagihan"]);
                        DateTime TglJT = Convert.ToDateTime(rsTagihan.Rows[i]["TglJT"]);
                        decimal denda = Convert.ToDecimal(rsTagihan.Rows[i]["Denda"]);

                        Db.Execute("EXEC spTunggakanDetil "
                            + "  " + NoTunggakan
                            + ",'" + NoTagihan + "'"
                            + ",'" + NamaTagihan + "'"
                            + ", " + Nilai
                            + ",'" + TglJT + "'"
                            + ", " + denda
                            );

                        alokasi.Append(NamaTagihan + "    " + Cf.Num(Nilai) + "<br>");
                    }
                }
                int LevelTunggakanSelected = 0;
                // Update Manual Level Tunggakan
                if (levelTunggakanValid())
                {

                    if (level_1.Checked == true) { LevelTunggakanSelected = 1; }
                    else if (level_2.Checked == true) { LevelTunggakanSelected = 2; }
                    else if (level_3.Checked == true) { LevelTunggakanSelected = 3; }
                    else if (level_4.Checked == true) { LevelTunggakanSelected = 4; }

                    if (LevelTunggakanSelected != 0)
                    {
                        Db.Execute("UPDATE MS_TUNGGAKAN SET LevelTunggakan = " + LevelTunggakanSelected + " WHERE NoTunggakan = " + NoTunggakan);
                    }

                    if (LevelTunggakanSelected == 4)
                    {
                        DateTime TanggalKuasa = Convert.ToDateTime(tglKuasa.Text);

                        Db.Execute("UPDATE MS_TUNGGAKAN SET TglKuasaSomasi = '" + TanggalKuasa + "' WHERE NoTunggakan = " + NoTunggakan);
                    }

                    /*UPDATE STATUS*/
                    //cek jumlah ST untuk NoKontrak tertentu
                    int count = Db.SingleInteger("SELECT COUNT(*) FROM MS_TUNGGAKAN WHERE Ref='" + Ref + "'");
                    if (LevelTunggakanSelected == 4)
                    {
                        if (count > 0)
                        {
                            //Update status
                            Db.Execute("UPDATE MS_TUNGGAKAN SET Status='A' WHERE Ref='" + Ref + "' AND LevelTunggakan < '" + LevelTunggakanSelected + "'");
                        }
                    }
                    else
                    {
                        if (count > 0)
                        {
                            //Update status
                            Db.Execute("UPDATE MS_TUNGGAKAN SET Status='S' WHERE Ref='" + Ref + "' AND LevelTunggakan < '" + LevelTunggakanSelected + "'");
                        }
                    }
                }
                string Project = Db.SingleString("SELECT Project FROM "+Mi.DbPrefix+"MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = '" + Ref + "'");
                //Numerator
                Db.Execute("UPDATE MS_TUNGGAKAN SET ManualTunggakan = '" + Numerator.ST(TglTunggakan.Month, TglTunggakan.Year, Project) + "',Project = '" + Project + "' WHERE NoTunggakan = " + NoTunggakan);
                DataTable rs = Db.Rs("SELECT "
                    + " CONVERT(varchar, TglTunggakan, 106) AS [Tanggal]"
                    + ",Tipe"
                    + ",Ref AS [Ref.]"
                    + ",Unit"
                    + ",Customer"
                    + " FROM MS_TUNGGAKAN WHERE NoTunggakan = " + NoTunggakan);

                string KetLog = Cf.LogCapture(rs)
                    + "<br>***DETIL TAGIHAN:<br>"
                    + alokasi.ToString();

                Db.Execute("EXEC spLogTunggakan"
                    + " 'REGIS'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + KetLog + "'"
                    + ",'" + NoTunggakan.ToString().PadLeft(7, '0') + "'"
                    );

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_TUNGGAKAN_LOG ORDER BY LogID DESC");                
                Db.Execute("UPDATE MS_TUNGGAKAN_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                Response.Redirect("STRegistrasi.aspx?done=" + NoTunggakan);
            }
        }

        private string Tb
        {
            get
            {
                return Sc.MktTb(Tipe);
            }
        }

        private string Ref
        {
            get
            {
                return Cf.Pk(Request.QueryString["Ref"]);
            }
        }

        private string Tipe
        {
            get
            {
                return Cf.Pk(Request.QueryString["Tipe"]);
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

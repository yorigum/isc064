using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.ADMINJUAL
{
    public partial class SkemaEdit : System.Web.UI.Page
    {
        protected DataTable rs;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            Act.CekInt("Nomor");

            if (!Act.Sec("ED:" + Request.PhysicalPath))
            {
                ok.Enabled = false;
                save.Enabled = false;
            }

            if (!Page.IsPostBack)
            {
                diskon.Attributes["style"] = "display: none;";
                FillHeader();
            }

            FillTable();
            FeedBack();
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

        public void UnitList(DropDownList d)
        {
            string strSql = "SELECT * FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_LOKASI WHERE Project = 'MARC'";
            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["Lokasi"].ToString();
                string t = v + " - " + rs.Rows[i]["Nama"].ToString();
                d.Items.Add(new ListItem(t, v));
            }
        }

        private void FillHeader()
        {
            Js.NumberFormat(barunominal);
            Act.ProjectList(project);
            UnitList(lokasi);
            diskon.Attributes["onfocus"] = "tempnum=CalcFocus(this);tempdisc=this.value;";
            diskon.Attributes["onblur"] = "if(this.value!=tempdisc){"
                + "recaldisc(document.getElementById('diskon'));"
                + "}";

            btnlog.Attributes["onclick"] = "location.href='LogPk.aspx?Tb=" + Mi.DbPrefix + "MARKETINGJUAL..REF_SKEMA_LOG&Pk=" + Nomor.PadLeft(3, '0') + "'";
            btndel.Attributes["onclick"] = "location.href='SkemaDel.aspx?Nomor=" + Nomor + "'";

            DataTable rsHeader = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_SKEMA WHERE Nomor = " + Nomor);
            if (rsHeader.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else if (!Act.AksesProject(rsHeader.Rows[0]["Project"].ToString()))
                Response.Redirect("/CustomError/SecLevel.html");
            else
            {
                nama.Text = rsHeader.Rows[0]["Nama"].ToString();
                diskon.Text = rsHeader.Rows[0]["Diskon"].ToString();
                diskonket.Text = rsHeader.Rows[0]["DiskonKet"].ToString();
                bunga2.Text = rsHeader.Rows[0]["Bunga"].ToString();
                bungaket.Text = rsHeader.Rows[0]["BungaKet"].ToString();

                jenis.SelectedValue = rsHeader.Rows[0]["Jenis"].ToString();
                Cf.SelectedValue(project, rsHeader.Rows[0]["Project"].ToString());
                Cf.SelectedValue(lokasi, rsHeader.Rows[0]["TipeUnit"].ToString());
                if (rsHeader.Rows[0]["Status"].ToString() == "A")
                {
                    aktif.Checked = true;
                    inaktif.Checked = false;
                }
                else
                {
                    aktif.Checked = false;
                    inaktif.Checked = true;
                }

                round.Checked = (bool)rsHeader.Rows[0]["RThousand"];
            }
        }

        private void FillTable()
        {
            rs = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_SKEMA_DETAIL WHERE Nomor = " + Nomor);

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                Label l;
                RadioButton rb;
                TextBox bx;
                HtmlInputButton btn;
                CheckBox cb;

                //No.
                l = new Label();
                l.Text = "<tr><td>" + rs.Rows[i]["Baris"].ToString() + ".</td>";
                list.Controls.Add(l);

                //Tipe
                l = new Label();
                l.Text = "<td>";
                list.Controls.Add(l);

                rb = new RadioButton();
                rb.ID = "bf_" + i;
                rb.Text = "BF";
                rb.GroupName = "tipe_" + i;
                if (rs.Rows[i]["Tipe"].ToString() == "BF")
                    rb.Checked = true;
                else
                    rb.Checked = false;
                list.Controls.Add(rb);

                l = new Label();
                l.Text = "&nbsp;&nbsp;";
                list.Controls.Add(l);

                rb = new RadioButton();
                rb.ID = "dp_" + i;
                rb.Text = "DP";
                rb.GroupName = "tipe_" + i;
                if (rs.Rows[i]["Tipe"].ToString() == "DP")
                    rb.Checked = true;
                else
                    rb.Checked = false;
                list.Controls.Add(rb);

                l = new Label();
                l.Text = "&nbsp;&nbsp;";
                list.Controls.Add(l);

                rb = new RadioButton();
                rb.ID = "ang_" + i;
                rb.Text = "ANG";
                rb.GroupName = "tipe_" + i;
                if (rs.Rows[i]["Tipe"].ToString() == "ANG")
                    rb.Checked = true;
                else
                    rb.Checked = false;
                list.Controls.Add(rb);

                //Nama
                l = new Label();
                l.Text = "</td><td>";
                list.Controls.Add(l);

                bx = new TextBox();
                bx.ID = "nama_" + i;
                bx.Text = rs.Rows[i]["Nama"].ToString();
                bx.CssClass = "txt";
                bx.MaxLength = 50;
                list.Controls.Add(bx);

                //Tipe Nominal
                l = new Label();
                l.Text = "</td><td>";
                list.Controls.Add(l);

                rb = new RadioButton();
                rb.ID = "persen_" + i;
                rb.GroupName = "tipenominal_" + i;
                rb.Text = "%";
                if (rs.Rows[i]["TipeNominal"].ToString() == "%")
                    rb.Checked = true;
                else
                    rb.Checked = false;
                list.Controls.Add(rb);

                l = new Label();
                l.Text = "&nbsp;&nbsp;";
                list.Controls.Add(l);

                rb = new RadioButton();
                rb.ID = "rupiah_" + i;
                rb.GroupName = "tipenominal_" + i;
                rb.Text = "F";
                if (rs.Rows[i]["TipeNominal"].ToString() == "F")
                    rb.Checked = true;
                else
                    rb.Checked = false;
                list.Controls.Add(rb);

                l = new Label();
                l.Text = "&nbsp;&nbsp;";
                list.Controls.Add(l);

                bx = new TextBox();
                bx.ID = "nominal_" + i;
                bx.CssClass = "txt_num";
                bx.Text = Cf.Num(rs.Rows[i]["Nominal"]).ToString();
                bx.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
                //bx.Attributes["onkeyup"] = "CalcType(this,tempnum);";
                bx.Attributes["onblur"] = "CalcBlur(this);";
                list.Controls.Add(bx);

                //Jadwal
                l = new Label();
                l.Text = "</td><td>";
                list.Controls.Add(l);

                rb = new RadioButton();
                rb.ID = "bln_" + i;
                rb.GroupName = "tipejadwal_" + i;
                rb.Text = "M";
                if (rs.Rows[i]["TipeJadwal"].ToString() == "M")
                    rb.Checked = true;
                else
                    rb.Checked = false;
                list.Controls.Add(rb);

                l = new Label();
                l.Text = "&nbsp;&nbsp;";
                list.Controls.Add(l);

                rb = new RadioButton();
                rb.ID = "hr_" + i;
                rb.GroupName = "tipejadwal_" + i;
                rb.Text = "D";
                if (rs.Rows[i]["TipeJadwal"].ToString() == "D")
                    rb.Checked = true;
                else
                    rb.Checked = false;
                list.Controls.Add(rb);

                l = new Label();
                l.Text = "&nbsp;&nbsp;";
                list.Controls.Add(l);

                rb = new RadioButton();
                rb.ID = "fix_" + i;
                rb.GroupName = "tipejadwal_" + i;
                rb.Text = "F";
                if (rs.Rows[i]["TglFix"].ToString() != "")
                    rb.Checked = true;
                else
                    rb.Checked = false;
                list.Controls.Add(rb);

                l = new Label();
                l.Text = "&nbsp;&nbsp;";
                list.Controls.Add(l);

                bx = new TextBox();
                bx.ID = "lama_" + i;
                bx.Width = 85;
                if (rs.Rows[i]["TglFix"].ToString() != "")
                    bx.Text = Cf.Day(rs.Rows[i]["TglFix"]);
                else
                    bx.Text = rs.Rows[i]["IntJadwal"].ToString();
                bx.CssClass = "txt_center";
                list.Controls.Add(bx);

                l = new Label();
                l.Text = "&nbsp;";
                list.Controls.Add(l);

                btn = new HtmlInputButton();
                btn.Value = "...";
                btn.Attributes["class"] = "btn";
                btn.Attributes["onclick"] = "javascript:openCalendar('lama_" + i + "')";
                list.Controls.Add(btn);

                l = new Label();
                l.Text = "</td><td>";
                list.Controls.Add(l);

                bx = new TextBox();
                bx.ID = "ref_" + i;
                bx.Width = 40;
                bx.Text = rs.Rows[i]["RefJadwal"].ToString();
                bx.CssClass = "txt_center";
                list.Controls.Add(bx);

                l = new Label();
                l.Text = "</td><td>";
                list.Controls.Add(l);

                cb = new CheckBox();
                cb.ID = "potong_" + i;
                if ((bool)rs.Rows[i]["BF"])
                    cb.Checked = true;
                else
                    cb.Checked = false;
                list.Controls.Add(cb);

                l = new Label();
                l.Text = "</td><td>";
                list.Controls.Add(l);

                cb = new CheckBox();
                cb.ID = "kpr_" + i;
                cb.Checked = (bool)rs.Rows[i]["KPR"];
                list.Controls.Add(cb);

                //link Delete
                l = new Label();
                l.Text = "</td><td>"
                    + "<a href=\"javascript:hapusbaris('" + Nomor + "', '" + rs.Rows[i]["Baris"].ToString() + "')\">Delete...</a>"
                    + "</td></tr>";
                list.Controls.Add(l);
            }
        }

        private bool valid()
        {
            bool x = true;
            string s = "";

            //nama
            if (Cf.isEmpty(nama))
            {
                x = false;
                if (s == "") s = nama.ID;
                namac.Text = "Kosong";
            }
            else
                namac.Text = "";

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TextBox namatagihan = (TextBox)list.FindControl("nama_" + i);
                TextBox nominal = (TextBox)list.FindControl("nominal_" + i);
                TextBox lama = (TextBox)list.FindControl("lama_" + i);
                TextBox referensi = (TextBox)list.FindControl("ref_" + i);
                RadioButton bln = (RadioButton)list.FindControl("bln_" + i);
                RadioButton hr = (RadioButton)list.FindControl("hr_" + i);
                RadioButton fix = (RadioButton)list.FindControl("fix_" + i);

                if (Cf.isEmpty(namatagihan))
                {
                    x = false;
                    if (s == "") s = namatagihan.ID;
                }
                else if (!Cf.isMoney(nominal))
                {
                    x = false;
                    if (s == "") s = nominal.ID;
                }
                else if ((bln.Checked || hr.Checked) && !Cf.isInt(lama))
                {
                    x = false;
                    if (s == "") s = lama.ID;
                }
                else if (fix.Checked && !Cf.isTgl(lama))
                {
                    x = false;
                    if (s == "") s = lama.ID;
                }
                else if (!Cf.isInt(referensi))
                {
                    x = false;
                    if (s == "") s = referensi.ID;
                }
                else if (Convert.ToInt32(referensi.Text) < 0)
                {
                    x = false;
                    if (s == "") s = referensi.ID;
                }
            }

            if (barunama.Text != "" || barunominal.Text != "" || barulama.Text != "" || barureferensi.Text != "")
            {
                if (Cf.isEmpty(barunama))
                {
                    x = false;
                    if (s == "") s = barunama.ID;
                }
                else if (!Cf.isMoney(barunominal))
                {
                    x = false;
                    if (s == "") s = barunominal.ID;
                }
                else if ((barubln.Checked || baruhr.Checked) && !Cf.isInt(barulama))
                {
                    x = false;
                    if (s == "") s = barulama.ID;
                }
                else if (barufix.Checked && !Cf.isTgl(barulama))
                {
                    x = false;
                    if (s == "") s = barulama.ID;
                }
                else if (!Cf.isInt(barureferensi))
                {
                    x = false;
                    if (s == "") s = barureferensi.ID;
                }
                else if (Convert.ToInt32(barureferensi.Text) < 0)
                {
                    x = false;
                    if (s == "") s = barureferensi.ID;
                }
            }

            if (!x)
            {
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Nama tidak boleh kosong.\\n"
                    + "2. Nilai harus berupa angka.\\n"
                    + "3. Jadwal (FIX), Format tanggal : Bulan / Tanggal / Tahun.\\n"
                    + "4. Jadwal (M/D), Interval harus berupa angka.\\n"
                    + "5. Jadwal Setelah harus diisi dengan angka positif."
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );
            }

            return x;
        }

        private bool Save()
        {
            if (valid())
            {
                DataTable rsHeaderBef = Db.Rs("SELECT "
                    + " Nomor"
                    + ",Nama"
                    + ",Project"
                    + ",Diskon"
                    + ",DiskonKet AS [Keterangan Diskon]"
                    + ",Bunga"
                    + ",BungaKet AS [Keterangan Bunga]"
                    + ",RThousand AS [Pembulatan Nilai]"
                    + ",Status"
                    + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_SKEMA "
                    + " WHERE Nomor = " + Nomor);

                DataTable rsBef = Db.Rs("SELECT "
                    + " CONVERT(VARCHAR, Baris) "
                    + " + '.  ' + Nama + ' (' + Tipe + ')  ' "
                    + " + TipeNominal + CONVERT(VARCHAR, Nominal, 1) + '  ' "
                    + " + TipeJadwal + '(' + CONVERT(VARCHAR, IntJadwal) + ')' + "
                    + " ISNULL(CONVERT(VARCHAR, TglFix, 106), 'NULL') + '  ' "
                    + " + 'REF:' + CONVERT(VARCHAR,RefJadwal) + '  ' "
                    + " + 'BF:' + CONVERT(VARCHAR, BF)"
                    + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_SKEMA_DETAIL WHERE Nomor = " + Nomor);

                UpdateSkema();
                UpdateSkemaDetail();
                UpdateSkemaBaru();
                
                //Response.Write("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..REF_SKEMA SET  Jenis = '" + jenis.SelectedValue + "' WHERE Nomor = '" + Nomor + "'  ");
                Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..REF_SKEMA SET Jenis = '" + jenis.SelectedValue + "' WHERE Nomor = '" + Nomor + "'  ");
                Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..REF_SKEMA SET TipeUnit = '" + lokasi.SelectedValue + "' WHERE Nomor = '" + Nomor + "'  ");

                DataTable rsHeaderAft = Db.Rs("SELECT "
                    + " Nomor"
                    + ",Nama"
                    + ",Project"
                    + ",Diskon"
                    + ",DiskonKet AS [Keterangan Diskon]"
                    + ",Bunga"
                    + ",BungaKet AS [Keterangan Bunga]"
                    + ",RThousand AS [Pembulatan Nilai]"
                    + ",Status"
                    + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_SKEMA "
                    + " WHERE Nomor = " + Nomor);

                DataTable rsAft = Db.Rs("SELECT "
                    + " CONVERT(VARCHAR, Baris) "
                    + " + '.  ' + Nama + ' (' + Tipe + ')  ' "
                    + " + TipeNominal + CONVERT(VARCHAR, Nominal, 1) + '  ' "
                    + " + TipeJadwal + '(' + CONVERT(VARCHAR, IntJadwal) + ')' + "
                    + " ISNULL(CONVERT(VARCHAR, TglFix, 106), 'NULL') + '  ' "
                    + " + 'REF:' + CONVERT(VARCHAR,RefJadwal) + '  ' "
                    + " + 'BF:' + CONVERT(VARCHAR, BF)"
                    + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_SKEMA_DETAIL WHERE Nomor = " + Nomor);

                string Ket = "HEADER :<br>" + Cf.LogCompare(rsHeaderBef, rsHeaderAft)
                    + "<br>"
                    + Cf.LogList(rsBef, rsAft, "RUMUS");

                Db.Execute("EXEC " + Mi.DbPrefix + "MARKETINGJUAL..spLogSkema"
                    + " 'EDIT'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + Ket + "'"
                    + ",'" + Nomor.PadLeft(3, '0') + "'"
                    );

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM REF_SKEMA_LOG ORDER BY LogID DESC");
                Db.Execute("UPDATE REF_SKEMA_LOG SET Project = '" + project.Text + "' WHERE LogID  = " + LogID);

                return true;
            }
            else
                return false;
        }

        protected void ok_Click(object sender, System.EventArgs e)
        {
            if (Save()) Js.Close(this);
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            if (Save()) /*Response.Redirect("SkemaEdit.aspx?Nomor=" + Nomor + "&done=1"*/;
        }

        private void UpdateSkema()
        {
            string Nama = Cf.Str(nama.Text);
            string Diskon = Cf.Str(diskon.Text);
            string DiskonKet = Cf.Str(diskonket.Text);
            string Bunga = Cf.Str(bunga2.Text);

            string BungaKet = Cf.Str(bungaket.Text);
            string Project = Cf.Pk(project.SelectedValue);

            string Status = "";
            if (aktif.Checked)
                Status = "A";
            else if (inaktif.Checked)
                Status = "I";

            Db.Execute("EXEC " + Mi.DbPrefix + "MARKETINGJUAL..spSkemaEdit"
                + "  " + Nomor
                + ",'" + Nama + "'"
                + ",'" + Diskon + "'"
                + ",'" + DiskonKet + "'"
                + ",'" + Bunga + "'"
                + ",'" + BungaKet + "'"
                + ", " + Cf.BoolToSql(round.Checked)
                + ",'" + Status + "'"
                + ",'" + Project + "'"
                );
        }

        private void UpdateSkemaDetail()
        {
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                RadioButton bf = (RadioButton)list.FindControl("bf_" + i);
                RadioButton dp = (RadioButton)list.FindControl("dp_" + i);
                RadioButton ang = (RadioButton)list.FindControl("ang_" + i);
                TextBox nama = (TextBox)list.FindControl("nama_" + i);
                RadioButton persen = (RadioButton)list.FindControl("persen_" + i);
                RadioButton rupiah = (RadioButton)list.FindControl("rupiah_" + i);
                TextBox nominal = (TextBox)list.FindControl("nominal_" + i);
                RadioButton bln = (RadioButton)list.FindControl("bln_" + i);
                RadioButton hr = (RadioButton)list.FindControl("hr_" + i);
                RadioButton fix = (RadioButton)list.FindControl("fix_" + i);
                TextBox lama = (TextBox)list.FindControl("lama_" + i);
                TextBox referensi = (TextBox)list.FindControl("ref_" + i);
                CheckBox potong = (CheckBox)list.FindControl("potong_" + i);
                CheckBox kpr = (CheckBox)list.FindControl("kpr_" + i);

                string Nama = Cf.Str(nama.Text);

                string Tipe = "";
                if (bf.Checked)
                    Tipe = "BF";
                else if (dp.Checked)
                    Tipe = "DP";
                else if (ang.Checked)
                    Tipe = "ANG";

                decimal Nominal = Convert.ToDecimal(nominal.Text);
                string TipeNominal = "";
                if (persen.Checked)
                    TipeNominal = "%";
                else
                    TipeNominal = "F";

                string TipeJadwal = "";
                if (bln.Checked)
                    TipeJadwal = "M";
                else if (hr.Checked)
                    TipeJadwal = "D";
                else
                    TipeJadwal = "F";

                string TglFix = "NULL";
                int IntJadwal = 0;
                if (TipeJadwal == "F")
                    TglFix = "'" + Cf.Str(Convert.ToDateTime(lama.Text)) + "'";
                else
                    IntJadwal = Convert.ToInt32(lama.Text);

                int RefJadwal = Convert.ToInt32(referensi.Text);

                Db.Execute("EXEC " + Mi.DbPrefix + "MARKETINGJUAL..spSkemaEditBaris"
                    + "  " + Nomor
                    + ", " + rs.Rows[i]["Baris"].ToString()
                    + ",'" + Tipe + "'"
                    + ",'" + Nama + "'"
                    + ", " + Nominal
                    + ",'" + TipeNominal + "'"
                    + ", " + TglFix
                    + ",'" + TipeJadwal + "'"
                    + ", " + IntJadwal
                    + ", " + RefJadwal
                    + ", " + Cf.BoolToSql(potong.Checked)
                    // + ", '" + Nominal + "'"
                    );

                Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..REF_SKEMA_DETAIL"
                    + " SET KPR = " + Cf.BoolToSql(kpr.Checked)
                    + " WHERE Nomor = " + Nomor
                    + " AND Baris = " + rs.Rows[i]["Baris"]
                    );
            }
        }

        private void UpdateSkemaBaru()
        {
            if (barunama.Text != "")
            {
                string Nama = Cf.Str(barunama.Text);

                string Tipe = "";
                if (barubf.Checked)
                    Tipe = "BF";
                else if (barudp.Checked)
                    Tipe = "DP";
                else if (baruang.Checked)
                    Tipe = "ANG";

                decimal Nominal = Convert.ToDecimal(barunominal.Text);
                string TipeNominal = "";
                if (barupersen.Checked)
                    TipeNominal = "%";
                else
                    TipeNominal = "F";

                string TipeJadwal = "";
                if (barubln.Checked)
                    TipeJadwal = "M";
                else if (baruhr.Checked)
                    TipeJadwal = "D";
                else
                    TipeJadwal = "F";

                string TglFix = "NULL";
                int IntJadwal = 0;
                if (TipeJadwal == "F")
                    TglFix = "'" + Cf.Str(Convert.ToDateTime(barulama.Text)) + "'";
                else
                    IntJadwal = Convert.ToInt32(barulama.Text);

                int RefJadwal = Convert.ToInt32(barureferensi.Text);

                Db.Execute("EXEC " + Mi.DbPrefix + "MARKETINGJUAL..spSkemaTambah"
                    + "  " + Nomor
                    + ",'" + Tipe + "'"
                    + ",'" + Nama + "'"
                    + ", " + Nominal
                    + ",'" + TipeNominal + "'"
                    + ", " + TglFix
                    + ",'" + TipeJadwal + "'"
                    + ", " + IntJadwal
                    + ", " + RefJadwal
                    + ", " + Cf.BoolToSql(barupotong.Checked)
                    + ", '" + Nominal + "'"
                    //+ ", " + Cf.BoolToSql(kpr.Checked)
                    );


            }
        }

        private string Nomor
        {
            get
            {
                return Cf.Pk(Request.QueryString["Nomor"]);
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

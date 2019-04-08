using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.SETTINGS
{
    public partial class Skema : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                Bind();
                FillTable();
            }

            FeedBack();
        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "<a href=\"javascript:popSkema('" + Request.QueryString["done"] + "')\">"
                        + "Pendaftaran Berhasil..."
                        + "</a>";
            }
        }

        private void FillTable()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            //Isi skema aktif
            DataTable rs = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_SKEMA WHERE Status = 'A' AND Project IN (" + Act.ProjectListSql + ")");
            Rpt.NoData(sb, rs, "<font style='font:8pt'>Tidak terdapat skema cara bayar dengan status aktif.</font>");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                sb.Append("<li>"
                    + "<a href=\"javascript:popSkema('" + rs.Rows[i]["Nomor"].ToString() + "')\">"
                    + rs.Rows[i]["Nama"] + " (" + rs.Rows[i]["Nomor"].ToString().PadLeft(3, '0') + ")"
                    + "</a>"
                    + "</li>"
                    );
            }

            aktif.InnerHtml = sb.ToString();

            //Isi skema inaktif
            sb = new System.Text.StringBuilder();

            rs = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_SKEMA WHERE Status = 'I' AND Project IN (" + Act.ProjectListSql + ")");
            Rpt.NoData(sb, rs, "<font style='font:8pt'>Tidak terdapat skema cara bayar dengan status inaktif.</font>");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                sb.Append("<li>"
                    + "<a href=\"javascript:popSkema('" + rs.Rows[i]["Nomor"].ToString() + "')\">"
                    + rs.Rows[i]["Nama"].ToString() + " (" + rs.Rows[i]["Nomor"].ToString().PadLeft(3, '0') + ")"
                    + "</a>"
                    + "</li>"
                    );
            }

            inaktif.InnerHtml = sb.ToString();
        }

        private void Bind()
        {
            Act.ProjectList(project);

            Js.Focus(this, nama);

            Js.NumberFormat(bfjumlah);
            Js.NumberFormat(dpjumlah);
            Js.NumberFormat(angjumlah);
            Js.NumberFormat(diskon);
            diskon.Attributes["onblur"] = "if(this.value!=tempdisc){"
                + "recaldisc(document.getElementById('diskon'));"
                + "}";

            diskon.Attributes["style"] = "display: none;";
        }

        private bool valid()
        {
            string s = "";
            bool x = true;

            //nama
            if (Cf.isEmpty(nama))
            {
                x = false;
                if (s == "") s = nama.ID;
                namac.Text = "Kosong";
            }
            else
                namac.Text = "";

            if (jenis.SelectedIndex == -1)
            {
                x = false;
                if (s == "") s = jenis.ID;
                carabayarc.Text = "Pilih salah satu jenis";
            }
            else
                carabayarc.Text = "";

            //bf nominal
            if (!Cf.isInt(bfkali))
            {
                x = false;
                if (s == "") s = bfkali.ID;
                bfc.Text = "Angka";
            }
            else if (Convert.ToInt32(bfkali.Text) < 0)
            {
                x = false;
                if (s == "") s = bfkali.ID;
                bfc.Text = "Positif";
            }
            else if (!Cf.isMoney(bfjumlah))
            {
                x = false;
                if (s == "") s = bfjumlah.ID;
                bfc.Text = "Angka";
            }
            else
                bfc.Text = "";

            //dp nominal
            if (!Cf.isInt(dpkali))
            {
                x = false;
                if (s == "") s = dpkali.ID;
                dpc.Text = "Angka";
            }
            else if (Convert.ToInt32(dpkali.Text) < 0)
            {
                x = false;
                if (s == "") s = dpkali.ID;
                dpc.Text = "Positif";
            }
            else if (!Cf.isMoney(dpjumlah))
            {
                x = false;
                if (s == "") s = dpjumlah.ID;
                dpc.Text = "Angka";
            }
            else
                dpc.Text = "";

            //ang nominal
            if (!Cf.isInt(angkali))
            {
                x = false;
                if (s == "") s = angkali.ID;
                angc.Text = "Angka";
            }
            else if (Convert.ToInt32(angkali.Text) < 0)
            {
                x = false;
                if (s == "") s = angkali.ID;
                angc.Text = "Positif";
            }
            else if (!Cf.isMoney(angjumlah))
            {
                x = false;
                if (s == "") s = angjumlah.ID;
                angc.Text = "Angka";
            }
            else
                angc.Text = "";

            //bf jadwal
            if (!Cf.isInt(bflama1))
            {
                x = false;
                if (s == "") s = bflama1.ID;
                bf2c.Text = "Angka";
            }
            else if (Convert.ToInt32(bflama1.Text) < 0)
            {
                x = false;
                if (s == "") s = bflama1.ID;
                bf2c.Text = "Positif";
            }
            else if (!Cf.isInt(bflama2))
            {
                x = false;
                if (s == "") s = bflama2.ID;
                bf2c.Text = "Angka";
            }
            else if (Convert.ToInt32(bflama2.Text) < 0)
            {
                x = false;
                if (s == "") s = bflama2.ID;
                bf2c.Text = "Positif";
            }
            else
                bf2c.Text = "";

            //dp jadwal
            if (!Cf.isInt(dplama1))
            {
                x = false;
                if (s == "") s = dplama1.ID;
                dp2c.Text = "Angka";
            }
            else if (Convert.ToInt32(dplama1.Text) < 0)
            {
                x = false;
                if (s == "") s = dplama1.ID;
                dp2c.Text = "Positif";
            }
            else if (!Cf.isInt(dplama2))
            {
                x = false;
                if (s == "") s = dplama2.ID;
                dp2c.Text = "Angka";
            }
            else if (Convert.ToInt32(dplama2.Text) < 0)
            {
                x = false;
                if (s == "") s = dplama2.ID;
                dp2c.Text = "Positif";
            }
            else
                dp2c.Text = "";

            //ang jadwal
            if (!Cf.isInt(anglama1))
            {
                x = false;
                if (s == "") s = anglama1.ID;
                ang2c.Text = "Angka";
            }
            else if (Convert.ToInt32(anglama1.Text) < 0)
            {
                x = false;
                if (s == "") s = anglama1.ID;
                ang2c.Text = "Positif";
            }
            else if (!Cf.isInt(anglama2))
            {
                x = false;
                if (s == "") s = anglama2.ID;
                ang2c.Text = "Angka";
            }
            else if (Convert.ToInt32(anglama2.Text) < 0)
            {
                x = false;
                if (s == "") s = anglama2.ID;
                ang2c.Text = "Positif";
            }
            else
                ang2c.Text = "";

            if (x)
            {
                if ((dp1potong.Checked || dpspotong.Checked) && (Convert.ToInt32(dpkali.Text) == 0))
                {
                    x = false;
                    if (s == "") s = dpkali.ID;
                    cc.Text = "DP 0 kali";
                }
                else if ((ang1potong.Checked || angspotong.Checked) && (Convert.ToInt32(angkali.Text) == 0))
                {
                    x = false;
                    if (s == "") s = angkali.ID;
                    cc.Text = "Angsuran 0 kali";
                }
                else
                    cc.Text = "";
            }

            if (!x)
            {
                ClientScript.RegisterStartupScript(
                    GetType()
                    , "focusScript"
                    , "<script type='text/javascript'>"
                    + " document.getElementById('" + s + "').focus();"
                    + " document.getElementById('" + s + "').select();"
                    + "</script>"
                    );
            }

            return x;
        }

        protected void ok_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                string Nama = Cf.Str(nama.Text);
                string Diskon = Cf.Str(diskon.Text);
                string DiskonKet = Cf.Str(diskonket.Text);
                string Bunga = Cf.Str(bunga2.Text);
                string BungaKet = Cf.Str(bungaket.Text);
                bool RThousand = round.Checked;
                string Project = project.SelectedValue;

                Db.Execute("EXEC " + Mi.DbPrefix + "MARKETINGJUAL..spSkemaBaru"
                    + " '" + Nama + "'"
                    + ",'" + Diskon + "'"
                    + ",'" + DiskonKet + "'"
                    + ", " + Cf.BoolToSql(RThousand)
                    + ",'" + Bunga + "'"
                    + ",'" + BungaKet + "'"
                    + ",'" + Project + "'"
                    );

                //nomor skema terbaru
                int Nomor = Db.SingleInteger("SELECT TOP 1 Nomor FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_SKEMA"
                    + " ORDER BY Nomor DESC"
                    );

                Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..REF_SKEMA SET Jenis = '" + jenis.SelectedValue + "' WHERE Nomor = '" + Nomor + "'  ");

                SaveBaris(Nomor, "BF", "BOOKING FEE", bfkali, bfrupiah, bfjumlah, bfbln1, bfbln2, bflama1, bflama2);
                SaveBaris(Nomor, "DP", "DP", dpkali, dprupiah, dpjumlah, dpbln1, dpbln2, dplama1, dplama2);
                SaveBaris(Nomor, "ANG", "ANGSURAN", angkali, angrupiah, angjumlah, angbln1, angbln2, anglama1, anglama2);

                SaveLog(Nomor);

                Response.Redirect("Skema.aspx?done=" + Nomor);
            }
        }

        private void SaveBaris(int Nomor, string Tipe, string Nama, TextBox kali, CheckBox rp, TextBox nom, CheckBox bln1, CheckBox bln2, TextBox interval1, TextBox interval2)
        {
            int count = Convert.ToInt32(kali.Text);
            int index = Db.SingleInteger("SELECT ISNULL(MAX(Baris),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_SKEMA_DETAIL"
                + " WHERE Nomor = " + Nomor);
            int topref = index;

            for (int i = 0; i < count; i++)
            {
                if (!Response.IsClientConnected) break;

                index++;

                decimal Nominal = Convert.ToDecimal(nom.Text) / count;
                string TipeNominal = "%";
                if (rp.Checked) TipeNominal = "F";

                string TipeJadwal = "";
                int IntJadwal = 0;
                int RefJadwal = 0;
                if (i != 0)
                {
                    //interval
                    if (bln1.Checked) TipeJadwal = "M";
                    else TipeJadwal = "D";
                    IntJadwal = Convert.ToInt32(interval1.Text) * i;
                    RefJadwal = topref + 1;
                }
                else
                {
                    //pertama
                    if (bln2.Checked) TipeJadwal = "M";
                    else TipeJadwal = "D";
                    IntJadwal = Convert.ToInt32(interval2.Text);
                    RefJadwal = topref;
                }

                bool BF = false;
                if (Tipe == "DP")
                {
                    if (dp1potong.Checked && i == 0) BF = true;
                    if (dpspotong.Checked) BF = true;
                }
                if (Tipe == "ANG")
                {
                    if (ang1potong.Checked && i == 0) BF = true;
                    if (angspotong.Checked) BF = true;
                }

                if ((Tipe == "ANG") && (i == count - 1))
                {
                    Db.Execute("EXEC " + Mi.DbPrefix + "MARKETINGJUAL..spSkemaTambah "
                        + Nomor
                        + ",'" + Tipe + "'"
                        + ",'PELUNASAN'"
                        + ", " + Nominal
                        + ",'" + TipeNominal + "'"
                        + ",NULL"
                        + ",'" + TipeJadwal + "'"
                        + ", " + IntJadwal
                        + ", " + RefJadwal
                        + ", " + Cf.BoolToSql(BF)
                        );
                    string jenis = Db.SingleString("SELECT JENIS FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_SKEMA WHERE NOMOR=" + Nomor);
                    if (jenis == "KPR")
                    {
                        Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..REF_SKEMA_DETAIL SET KPR = 1 WHERE NOMOR=" + Nomor + " AND NAMA='PELUNASAN'");
                    }

                }
                else
                {
                    Db.Execute("EXEC " + Mi.DbPrefix + "MARKETINGJUAL..spSkemaTambah "
                        + Nomor
                        + ",'" + Tipe + "'"
                        + ",'" + Nama + " " + (i + 1) + "'"
                        + ", " + Nominal
                        + ",'" + TipeNominal + "'"
                        + ",NULL"
                        + ",'" + TipeJadwal + "'"
                        + ", " + IntJadwal
                        + ", " + RefJadwal
                        + ", " + Cf.BoolToSql(BF)
                        );
                }
            }
        }

        private void SaveLog(int Nomor)
        {
            DataTable rsHeader = Db.Rs("SELECT "
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

            DataTable rsDetail = Db.Rs("SELECT "
                + " CONVERT(VARCHAR, Baris) "
                + " + '.  ' + Nama + ' (' + Tipe + ')  ' "
                + " + TipeNominal + CONVERT(VARCHAR, Nominal, 1) + '  ' "
                + " + TipeJadwal + '(' + CONVERT(VARCHAR, IntJadwal) + ')' + "
                + " ISNULL(CONVERT(VARCHAR, TglFix, 106), 'NULL') + '  ' "
                + " + 'REF:' + CONVERT(VARCHAR,RefJadwal) + '  ' "
                + " + 'BF:' + CONVERT(VARCHAR, BF)"
                + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_SKEMA_DETAIL WHERE Nomor = " + Nomor);

            string Ket = Cf.LogCapture(rsHeader)
                + Cf.LogList(rsDetail, "RUMUS");

            Db.Execute("EXEC " + Mi.DbPrefix + "MARKETINGJUAL..spLogSkema"
                + " 'BARU'"
                + ",'" + Act.UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'" + Ket + "'"
                + ",'" + Nomor.ToString().PadLeft(3, '0') + "'"
                );
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

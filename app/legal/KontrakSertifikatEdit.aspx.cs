using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LEGAL
{
	public partial class KontrakSertifikatEdit : System.Web.UI.Page
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
                Bind(); //tanggal dan bulan
                Fill();
            }

            FeedBack();
            Js.Confirm(this, "Lanjutkan proses edit Sertifikat?\\n");
        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "Edit Sertifikat Berhasil...";
                //feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                //    + "<a href=\"javascript:popKontrakSertifikatEdit('" + Request.QueryString["done"] + "')\">"
                //    + "Sertifikat Berhasil..."
                //    + "</a>";
            }
        }

        private void Bind()
        {

        }

        private void Fill()
        {
            //aKey.HRef = "javascript:openModal('KontrakEditKey.aspx?NoKontrak=" + NoKontrak + "','350','220')";
            //aStatus.HRef = "javascript:openModal('KontrakStatus.aspx?NoKontrak=" + NoKontrak + "','500','500')";

            btnlog.Attributes["onclick"] = "location.href='LogPk.aspx?Tb=MS_KONTRAK_LOG&Pk=" + NoKontrak + "'";
            //btndel.Attributes["onclick"] = "location.href='KontrakDel.aspx?NoKontrak=" + NoKontrak + "'";
            aStatus.HRef = "javascript:openModal('KontrakStatus.aspx?NoKontrak=" + NoKontrak + "','500','500')";
            refresh.Attributes["onclick"] = "if(confirm('"
                + "Apakah anda ingin mengambil ulang data unit ?\\n"
                + "Perhatian bahwa nilai GROSS dan DISKON bisa berubah."
                + "'))"
                + "{location.href='KontrakRefresh.aspx?NoKontrak=" + NoKontrak + "'}";

            //string strSql = "SELECT *"
            //    + " FROM MS_SERTIFIKAT WHERE NoKontrak = '" + NoKontrak + "'";
            string strSql = "SELECT A.NoKontrak , B.*"
                + " FROM MS_KONTRAK A LEFT JOIN MS_SERTIFIKAT B ON A.NoKontrak = B.NoKontrak "
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
                stat.SelectedValue = rs.Rows[0]["StatusSertifikat"].ToString();
                
                tbTglTarget.Text = Cf.Day(rs.Rows[0]["TglTargetSertifikat"]);
                tbTglTarget2.Text = Cf.Day(rs.Rows[0]["TglTargetSertifikat"]);
                tbTglProses.Text = Cf.Day(rs.Rows[0]["TglProsesSertifikat"]);
                tbTglProses2.Text = Cf.Day(rs.Rows[0]["TglProsesSertifikat"]);
                tglakhir.Text = Cf.Day(rs.Rows[0]["TglAkhir"]);
                //tbKeterangan.Text = rs.Rows[0]["KetSertifikat"].ToString();
                //tbKeterangan1.Text = rs.Rows[0]["KetSertifikat"].ToString();
                tbKeterangan2.Text = rs.Rows[0]["KetSertifikat"].ToString();
                //tbKeterangan3.Text = rs.Rows[0]["KetSertifikat"].ToString();
                jangkawaktu.Text = Cf.Num(rs.Rows[0]["JangkaWaktu"]).ToString();
                tbTgl.Text = Cf.Day(rs.Rows[0]["TglSertifikat"]);
                tbTgl2.Text = Cf.Day(rs.Rows[0]["TglSertifikat"]);
                tbNoSertifikat.Text = Cf.Str(rs.Rows[0]["NoSertifikat"]).ToString();
                tbNoSertifikat2.Text = Cf.Str(rs.Rows[0]["NoSertifikat"]).ToString();

                if (!(rs.Rows[0]["StatusSertifikat"] is DBNull))
                {
                    if (rs.Rows[0]["StatusSertifikat"].ToString() == "B")
                    {
                        stat.SelectedIndex = 0;
                        belum.Visible = true;
                        target.Visible = false;
                        proses.Visible = false;
                        selesai.Visible = false;
                        tbTglTarget2.Text = Cf.Day(rs.Rows[0]["TglTargetSertifikat"]);
                        tbTglProses2.Text = Cf.Day(rs.Rows[0]["TglProsesSertifikat"]);
                        tbTgl2.Text = Cf.Day(rs.Rows[0]["TglSertifikat"]);
                        tbKeterangan2.Text = rs.Rows[0]["KetSertifikat"].ToString();
                    }
                    else if (rs.Rows[0]["StatusSertifikat"].ToString() == "S")
                    {
                        stat.SelectedIndex = 1;
                        target.Visible = true;
                        proses.Visible = false;
                        selesai.Visible = false;
                        belum.Visible = false;
                        tbTglTarget.Text = Cf.Day(rs.Rows[0]["TglTargetSertifikat"]);
                        //tbKeterangan3.Text = rs.Rows[0]["KetSertifikat"].ToString();
                    }
                    else if (rs.Rows[0]["StatusSertifikat"].ToString() == "D")
                    {
                        stat.SelectedIndex = 2;
                        proses.Visible = true;
                        target.Visible = false;
                        selesai.Visible = false;
                        belum.Visible = false;
                        tbTglProses.Text = Cf.Day(rs.Rows[0]["TglProsesSertifikat"]);
                        //tbKeterangan1.Text = rs.Rows[0]["KetSertifikat"].ToString();
                    }
                    else if (rs.Rows[0]["StatusSertifikat"].ToString() == "T")
                    {
                        stat.SelectedIndex = 3;
                        selesai.Visible = true;
                        target.Visible = false;
                        proses.Visible = false;
                        belum.Visible = false;
                        tbTgl.Text = Cf.Day(rs.Rows[0]["TglSertifikat"]);
                        //tbKeterangan.Text = rs.Rows[0]["KetSertifikat"].ToString();
                    }
                    if (statussertifikat.SelectedIndex == 0)
                    {
                        sertifikat1.Visible = true;
                        sertifikat2.Visible = true;
                    }
                    else
                    {
                        sertifikat1.Visible = false;
                        sertifikat2.Visible = false;
                    }
                }
            }
        }

        private bool valid()
        {
            string s = "";
            bool x = true;

            if (!Cf.isTgl(tbTgl) && tbTgl.Text != "")
            {
                x = false;
                if (s == "") s = tbTgl.ID;
            }
            if (!Cf.isTgl(tbTglProses) && tbTglProses.Text != "")
            {
                x = false;
                if (s == "") s = tbTglProses.ID;
            }
            if (!Cf.isTgl(tbTglTarget) && tbTglTarget.Text != "")
            {
                x = false;
                if (s == "") s = tbTglTarget.ID;
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

        private void Save()
        {
            string Status = "";
            //if(rblStatus.SelectedIndex != 0)
            Status = stat.SelectedValue;

            if (belum.Visible)
            {
                DateTime TglSertifikat2 = Convert.ToDateTime(tbTgl2.Text);
                DateTime TglTarget2 = Convert.ToDateTime(tbTglTarget2.Text);
                DateTime TglProses2 = Convert.ToDateTime(tbTglProses2.Text);
                string NoSertifikat2 = Cf.Str(tbNoSertifikat2.Text);
                string keterangan2 = Cf.Str(tbKeterangan2.Text);

                Db.Execute("UPDATE MS_SERTIFIKAT"
                    + " SET NoSertifikat = '" + NoSertifikat2 + "'"
                    + ", StatusSertifikat= '"+stat.SelectedValue+"'"
                    + ", KetSertifikat = '" + keterangan2 + "'"
                    + ", TglSertifikat = '" + TglSertifikat2 + "'"
                    + ", TglTargetSertifikat = '" + TglTarget2 + "'"
                    + ", TglProsesSertifikat = '" + TglProses2 + "'"
                    + " WHERE NoKontrak = '" + NoKontrak + "'"
                    );
            }
            if (selesai.Visible)
            {
                DateTime TglSertifikat = Convert.ToDateTime(tbTgl.Text);
                string NoSertifikat = Cf.Str(tbNoSertifikat.Text);
                //string keterangan = Cf.Str(tbKeterangan.Text);
                //string jangkawaktu = Cf.Str(jangkawaktu.Text);

                Db.Execute("UPDATE MS_SERTIFIKAT"
                    + " SET NoSertifikat = '" + NoSertifikat + "'"
                    + ", StatusSertifikat= '" + stat.SelectedValue + "'"
                    //+ ", KetSertifikat = '" + keterangan + "'"
                    + ", TglSertifikat = '" + TglSertifikat + "'"
                    + ", JangkaWaktu = '" +jangkawaktu.Text + "'"
                    + " WHERE NoKontrak = '" + NoKontrak + "'"
                    );
            }
            if (proses.Visible)
            {
                DateTime TglProses = Convert.ToDateTime(tbTglProses.Text);
                //string keterangan1 = Cf.Str(tbKeterangan1.Text);

                Db.Execute("UPDATE MS_SERTIFIKAT"
                    + " SET StatusSertifikat= '" + stat.SelectedValue + "'"
                    + ", TglProsesSertifikat = '" + TglProses + "'"
                    + " WHERE NoKontrak = '" + NoKontrak + "'"
                    );
            }
            if (target.Visible)
            {
                DateTime TglTarget = Convert.ToDateTime(tbTglTarget.Text);
                //string keterangan3 = Cf.Str(tbKeterangan3.Text);

                Db.Execute("UPDATE MS_SERTIFIKAT"
                    + " SET TglTargetSertifikat = '" + TglTarget + "'"
                    + ", StatusSertifikat= '" + stat.SelectedValue + "'"
                    + " WHERE NoKontrak = '" + NoKontrak + "'"
                    );
            }

            DataTable rsAft = Db.Rs("SELECT "
                + "StatusSertifikat AS [Status Sertifikat]"
                + ", TglSertifikat AS [Tgl. Sertifikat]"
                + ", NoSertifikat AS [No. Sertifikat]"
                + ", TglProsesSertifikat AS [Tgl. Proses]"
                + ", TglTargetSertifikat AS [Tgl. Target]"
                + ", KetSertifikat AS [Keterangan Sertifikat]"
                + " FROM MS_SERTIFIKAT"
                + " WHERE NoKontrak = '" + NoKontrak + "'"
                );

            //Logfile
            //string Log = Cf.LogCompare(rsBef, rsAft);
            string Log = Cf.LogCapture(rsAft);

            Db.Execute("EXEC spLogKontrak"
                + " 'EDIT'"
                + ",'" + Act.UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'" + Log + "'"
                + ",'" + NoKontrak + "'"
                );
            decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
            string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
            Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

            Response.Redirect("KontrakSertifikatEdit.aspx?done=1&NoKontrak=" + NoKontrak);
        }

        protected void statussertifikat_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (statussertifikat.SelectedIndex == 0)
            {
                sertifikat1.Visible = true;
                sertifikat2.Visible = true;
            }
            else
            {
                sertifikat1.Visible = false;
                sertifikat2.Visible = false;
            }
        }

        protected void stat_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (stat.SelectedIndex == 0)
            {
                belum.Visible = true;
                target.Visible = false;
                proses.Visible = false;
                selesai.Visible = false;
                //tbTgl.Text = Cf.Day(DateTime.Today);
            }
            else if (stat.SelectedIndex == 1)
            {
                target.Visible = true;
                proses.Visible = false;
                selesai.Visible = false;
                belum.Visible = false;
                //tbTglTarget.Text = Cf.Day(DateTime.Today);
            }
            else if (stat.SelectedIndex == 2)
            {
                proses.Visible = true;
                target.Visible = false;
                selesai.Visible = false;
                belum.Visible = false;
                //tbTglProses.Text = Cf.Day(DateTime.Today);
            }
            else if (stat.SelectedIndex == 3)
            {
                selesai.Visible = true;
                target.Visible = false;
                proses.Visible = false;
                belum.Visible = false;
                //tbTgl.Text = Cf.Day(DateTime.Today);
            }
            else
            {
                belum.Visible = true;
                target.Visible = false;
                proses.Visible = false;
                selesai.Visible = false;
            }
        }

        protected void ok_Click(object sender, System.EventArgs e)
        {
            Save();
            Js.Close(this);
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            Save();
        }

        private string NoKontrak
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoKontrak"]);
                //return Cf.Pk(noko.Text);
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

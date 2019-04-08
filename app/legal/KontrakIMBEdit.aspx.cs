using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LEGAL
{
	public partial class KontrakIMBEdit : System.Web.UI.Page
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
            Js.Confirm(this, "Lanjutkan proses edit IMB?\\n");
        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    //feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                    //    + "Edit Berhasil...";
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "Edit IMB Berhasil...";
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
            //    + " FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'";
            string strSql = "SELECT A.NoKontrak,B.*"
                + " FROM MS_KONTRAK A LEFT JOIN MS_IMB B ON A.NoKontrak = B.NoKontrak "
                + "WHERE A.NoKontrak = '" + NoKontrak + "'";

            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                stat.SelectedValue = rs.Rows[0]["StatusIMB"].ToString();
                tbTglTarget.Text = Cf.Day(rs.Rows[0]["TglTargetIMB"]);
                tbTglTarget2.Text = Cf.Day(rs.Rows[0]["TglTargetIMB"]);
                tbTglProses2.Text = Cf.Day(rs.Rows[0]["TglProsesIMB"]);
                tbTgl2.Text = Cf.Day(rs.Rows[0]["TglIMB"]);
                tbKeteranganIMB2.Text = rs.Rows[0]["KetImb"].ToString();
                tbTglProses.Text = Cf.Day(rs.Rows[0]["TglProsesIMB"]);
                tbKeterangan.Text = rs.Rows[0]["KetImb"].ToString();
                tbTgl.Text = Cf.Day(rs.Rows[0]["TglIMB"]);
                tbKeteranganIMB.Text = rs.Rows[0]["KetImb"].ToString();
                tbNoIMB.Text = rs.Rows[0]["NoIMB"].ToString();
                tbNoIMB2.Text = rs.Rows[0]["NoIMB"].ToString();

            if (!(rs.Rows[0]["StatusIMB"] is DBNull))
            {
                if (rs.Rows[0]["StatusIMB"].ToString() == "B")
                {
                    stat.SelectedIndex = 0;
                    belum.Visible = true;
                    target.Visible = false;
                    proses.Visible = false;
                    selesai.Visible = false;
                    tbTglTarget2.Text = Cf.Day(rs.Rows[0]["TglTargetIMB"]);
                    tbTglProses2.Text = Cf.Day(rs.Rows[0]["TglProsesIMB"]);
                    tbTgl2.Text = Cf.Day(rs.Rows[0]["TglIMB"]);
                }
                else if (rs.Rows[0]["StatusIMB"].ToString() == "T")
                {
                    stat.SelectedIndex = 1;
                    target.Visible = true;
                    proses.Visible = false;
                    selesai.Visible = false;
                    belum.Visible = false;
                    tbTglTarget.Text = Cf.Day(rs.Rows[0]["TglTargetIMB"]);
                }
                else if (rs.Rows[0]["StatusIMB"].ToString() == "D")
                {
                    stat.SelectedIndex = 2;
                    proses.Visible = true;
                    target.Visible = false;
                    selesai.Visible = false;
                    belum.Visible = false;
                    tbTglProses.Text = Cf.Day(rs.Rows[0]["TglProsesIMB"]);
                    tbKeterangan.Text = rs.Rows[0]["KetImb"].ToString();
                }
                else if (rs.Rows[0]["StatusIMB"].ToString() == "T")
                {
                    stat.SelectedIndex = 3;
                    selesai.Visible = true;
                    target.Visible = false;
                    proses.Visible = false;
                    belum.Visible = false;
                    tbTgl.Text = Cf.Day(rs.Rows[0]["TglIMB"]);
                    tbKeteranganIMB.Text = rs.Rows[0]["KetImb"].ToString();
                }
                    else
                    {
                        belum.Visible = true;
                        target.Visible = false;
                        proses.Visible = false;
                        selesai.Visible = false;
                    }
                }
                else
                {
                    belum.Visible = true;
                    target.Visible = false;
                    proses.Visible = false;
                    selesai.Visible = false;
                }
            }
        }

        private bool valid()
        {
            string s = "";
            bool x = true;

            //if (!Cf.isTgl(tglcetak) && tglcetak.Text != "")
            //{
            //    x = false;
            //    if (s == "") s = tglcetak.ID;
            //}

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

            //string sql = "", sql1;
            //sql1 = "SELECT "
            //    + "StatusIMB AS [Status IMB]"
            //    + ", TglIMB AS [Tgl. IMB]"
            //    + ", TglProsesIMB AS [Tgl. Proses]"
            //    + ", TglTargetIMB AS [Tgl. Target]"
            //    + ", NoIMB AS [No. IMB]"
            //    + " FROM MS_KONTRAK"
            //    + " WHERE NoKontrak = '" + NoKontrak + "'";


            //DataTable rsBef = Db.Rs(sql1
            //    );

            //sql = "UPDATE MS_KONTRAK"
            //    + " SET StatusIMB = " + Status + ""
            //    + ", NoImb = ''"
            //    + ", KetImb = ''"
            //    + ", TglProsesIMB = NULL"
            //    + ", TglTargetIMB = NULL"
            //    + ", TglImb = NULL"
            //    + " WHERE NoKontrak = '" + NoKontrak + "'";

            //Db.Execute(sql);

            if (belum.Visible)
            {
                DateTime TglIMB2 = Convert.ToDateTime(tbTgl2.Text);
                DateTime TglTarget2 = Convert.ToDateTime(tbTglTarget2.Text);
                DateTime TglProses2 = Convert.ToDateTime(tbTglProses2.Text);
                string NoIMB2 = Cf.Str(tbNoIMB.Text);
                string keteranganimb2 = Cf.Str(tbKeteranganIMB2.Text);

                Db.Execute("UPDATE MS_IMB"
                    + " SET NoIMB = '" + NoIMB2 + "'"
                    + ", StatusIMB = '" + stat.SelectedValue + "'"
                    + ", KetIMB = '" + keteranganimb2 + "'"
                    + ", TglIMB = '" + TglIMB2 + "'"
                    + ", TglTargetIMB = '" + TglTarget2 + "'"
                    + ", TglProsesIMB = '" + TglProses2 + "'"
                    + " WHERE NoKontrak = '" + NoKontrak + "'"
                    );
            }
            if (selesai.Visible)
            {
                DateTime TglIMB = Convert.ToDateTime(tbTgl.Text);
                string NoIMB = Cf.Str(tbNoIMB.Text);
                string keteranganimb = Cf.Str(tbKeteranganIMB.Text);

                Db.Execute("UPDATE MS_IMB"
                    + " SET NoIMB = '" + NoIMB + "'"
                    + ", StatusIMB = '" + stat.SelectedValue + "'"
                    + ", KetIMB = '" + keteranganimb + "'"
                    + ", TglIMB = '" + TglIMB + "'"
                    + " WHERE NoKontrak = '" + NoKontrak + "'"
                    );
            }
            if (proses.Visible)
            {
                DateTime TglProses = Convert.ToDateTime(tbTglProses.Text);
                string keterangan = Cf.Str(tbKeterangan.Text);

                Db.Execute("UPDATE MS_IMB"
                    + " SET KetIMB = '" + keterangan + "'"
                    + ", StatusIMB = '" + stat.SelectedValue + "'"
                    + ", TglProsesIMB = '" + TglProses + "'"
                    + " WHERE NoKontrak = '" + NoKontrak + "'"
                    );
            }
            if(target.Visible)
            {
                DateTime TglTarget = Convert.ToDateTime(tbTglTarget.Text);

                Db.Execute("UPDATE MS_IMB"
                    + " SET TglTargetIMB = '" + TglTarget + "'"
                    + ", StatusIMB = '" + stat.SelectedValue + "'"
                    + " WHERE NoKontrak = '" + NoKontrak + "'"
                    );
            }

            DataTable rsAft = Db.Rs("SELECT "
                + "StatusIMB AS [Status IMB]"
                + ", TglIMB AS [Tgl. IMB]"
                + ", NoIMB AS [No. IMB]"
                + ", TglProsesIMB AS [Tgl. Proses]"
                + ", TglTargetIMB AS [Tgl. Target]"
                + ", KetIMB AS [Keterangan IMB]"
                + " FROM MS_IMB"
                + " WHERE NoKontrak = '" + NoKontrak + "'"
                );

            //Logfile
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

            Response.Redirect("KontrakIMBEdit.aspx?done=1&NoKontrak=" + NoKontrak);
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
            else if(stat.SelectedIndex == 2)
            {
                proses.Visible = true;
                target.Visible = false;
                selesai.Visible = false;
                belum.Visible = false;
                //tbTglProses.Text= Cf.Day(DateTime.Today);
            }
            else if (stat.SelectedIndex == 3)
            {
                selesai.Visible = true;
                target.Visible = false;
                proses.Visible = false;
                belum.Visible = false;
                //tbTgl.Text = Cf.Day(DateTime.Today);
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

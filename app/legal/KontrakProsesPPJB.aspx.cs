using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
    public partial class KontrakProsesPPJB : System.Web.UI.Page
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
                Fill();
            }

            FeedBack();
            Js.Confirm(this, "Lanjutkan proses edit proses ppjb?\\n");

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
        protected void Fill()
        {
            string strSql = "SELECT *"
				+ " FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'";
            
			DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                //    B = BELUM
                //    D = SUDAH REGIS
                //    T = PROSES TTD
                //    S = SELESAI
                stat.SelectedValue = rs.Rows[0]["PPJB"].ToString();
                noppjb.Text = rs.Rows[0]["NoPPJB"].ToString();
                noppjbm.Text = rs.Rows[0]["NoPPJBm"].ToString();
                TglPPJB.Text = Cf.Day(rs.Rows[0]["TglPPJB"]);
                tglcetak.Text = Cf.Day(rs.Rows[0]["TglCetakPPJB"]);
                tglttd.Text = Cf.Day(rs.Rows[0]["TglTTDPPJB"]);
                tglkp.Text = Cf.Day(rs.Rows[0]["TglLengkapPPJB"]);
                ppjbused.SelectedValue = rs.Rows[0]["PPJBu"].ToString();
                ktp.Text = Db.SingleString("SELECT NoKTP FROM MS_CUSTOMER WHERE NoCustomer='"+ rs.Rows[0]["NoCustomer"].ToString() +"'");
                statktp.SelectedValue = rs.Rows[0]["KTPMilik"].ToString();
                statktp2.SelectedValue = rs.Rows[0]["KTPIstri"].ToString();
                kk.SelectedValue = rs.Rows[0]["KK"].ToString();
                nikah.SelectedValue = rs.Rows[0]["SNKH"].ToString();
                skk.SelectedValue = rs.Rows[0]["SKK"].ToString();
                rk.SelectedValue = rs.Rows[0]["RK"].ToString();
                bt.SelectedValue = rs.Rows[0]["BT"].ToString();
                kw.SelectedValue = rs.Rows[0]["KW"].ToString();
                du.SelectedValue = rs.Rows[0]["DU"].ToString();
                dl.SelectedValue = rs.Rows[0]["DL"].ToString();
                sm.SelectedValue = rs.Rows[0]["SM"].ToString();

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
        protected void ok_Click(object sender, System.EventArgs e)
        {
            if (Save()) Js.Close(this);
        }

        private bool Save()
        {
            if (valid())
            {
                DataTable rsBef = Db.Rs("SELECT "
                    + " NoKontrak AS [No. Kontrak]"
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
                    + " FROM MS_KONTRAK"
                    + " WHERE NoKontrak = '" + NoKontrak + "'"
                    );
               
               Db.Execute("UPDATE MS_KONTRAK SET"
                        + " PPJB ='"+ stat.SelectedValue +"'"
                        + ", PPJBu="+ ppjbused.SelectedValue
                        + ", KTPMilik="+ statktp.SelectedValue
                        + ", KTPIstri="+ statktp2.SelectedValue
                        + ", KK="+ kk.SelectedValue
                        + ", SNKH="+ nikah.SelectedValue
                        + ", SKK=" + skk.SelectedValue
                        + ", RK=" + rk.SelectedValue
                        + ", BT=" + bt.SelectedValue
                        + ", KW=" + kw.SelectedValue
                        + ", DU=" + du.SelectedValue
                        + ", DL=" + dl.SelectedValue
                        + ", SM=" + sm.SelectedValue
                        + " WHERE NoKontrak = '" + NoKontrak + "'"
                          );
               if (tglcetak.Text != "")
               {
                   Db.Execute("UPDATE MS_KONTRAK SET TglCetakPPJB='" + Convert.ToDateTime(tglcetak.Text) + "' WHERE NoKontrak = '" + NoKontrak + "'");
               }

               if (tglttd.Text != "")
               {
                   Db.Execute("UPDATE MS_KONTRAK SET TglTTDPPJB='" + Convert.ToDateTime(tglttd.Text) + "' WHERE NoKontrak = '" + NoKontrak + "'");
               }

               if (tglkp.Text != "")
               {
                   Db.Execute("UPDATE MS_KONTRAK SET TglLengkapPPJB='" + Convert.ToDateTime(tglkp.Text) + "' WHERE NoKontrak = '" + NoKontrak + "'");
               }
                

                DataTable rsAft = Db.Rs("SELECT "
                   + " NoKontrak AS [No. Kontrak]"
                   + ",CONVERT(varchar,TglKontrak,106) AS [Tanggal Kontrak]"
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
                   + " FROM MS_KONTRAK"
                   + " WHERE NoKontrak = '" + NoKontrak + "'"
                   );
                //Logfile
                string Ket = Cf.LogCompare(rsBef, rsAft);

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

                return true;
            }
            else
            {
                return false;
            }

        }
        private bool valid()
        {
            string s = "";
            bool x = true;

            if (!Cf.isTgl(tglcetak) && tglcetak.Text != "")
            {
                x = false;
                if (s == "") s = tglcetak.ID;
            }

            if (!Cf.isTgl(tglttd) && tglttd.Text != "")
            {
                x = false;
                if (s == "") s = tglttd.ID;
            }

            if (!Cf.isTgl(tglkp) && tglkp.Text != "")
            {
                x = false;
                if (s == "") s = tglkp.ID;
            }
            if (stat.SelectedValue == "")
            {
                x = false;
            }
            if (statktp.SelectedValue == "")
            {
                x = false;
            }
            if (nikah.SelectedValue == "")
            {
                x = false;
            }
            if (kk.SelectedValue == "")
            {
                x = false;
            }
            if (skk.SelectedValue == "")
            {
                x = false;
            }
            if (rk.SelectedValue == "")
            {
                x = false;
            }
            if (bt.SelectedValue == "")
            {
                x = false;
            }
            if (kw.SelectedValue == "")
            {
                x = false;
            }
            if (du.SelectedValue == "")
            {
                x = false;
            }
            if (dl.SelectedValue == "")
            {
                x = false;
            }
            if (sm.SelectedValue == "")
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

        protected void save_Click(object sender, System.EventArgs e)
        {
            if (Save()) Response.Redirect("KontrakProsesPPJB.aspx?done=1&NoKontrak=" + NoKontrak);
        }

        private string NoKontrak
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoKontrak"]);
            }
        }
}
}

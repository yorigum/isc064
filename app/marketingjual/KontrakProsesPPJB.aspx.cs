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
                        
            if (!Page.IsPostBack)
            {
                Bind();
                Fill();
            }
            
            Js.Confirm(this, "Lanjutkan proses edit proses ppjb?\\n");

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

            }
        }

        protected void Fill()
        {
            string strSql = "SELECT b.* FROM MS_KONTRAK a LEFT JOIN MS_PPJB b ON a.NoKontrak = b.NoKontrak WHERE a.NoKontrak = '" + NoKontrak + "'";
            //string strSql = "SELECT * FROM MS_PPJB WHERE NoKontrak = '" + NoKontrak + "'";            
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
                ppjbused.SelectedValue = rs.Rows[0]["PPJBu"].ToString();

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

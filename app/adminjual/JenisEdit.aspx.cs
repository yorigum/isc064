using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.ADMINJUAL
{
    public partial class JenisEdit : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Act.Sec("ED:" + Request.PhysicalPath))
            {
                ok.Enabled = false;
                save.Enabled = false;
            }

            if (!Page.IsPostBack)
            {
                //Bind(); //tanggal dan bulan
                Fill();
            }

            FeedBack();
            Js.Confirm(this, "Lanjutkan proses edit data Tipe unit properti?\\n"
                + "");
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

        //private void Bind()
        //{
        //    //NumberFormat.js
        //    luas.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
        //    luas.Attributes["onkeyup"] = "CalcType(this,tempnum);";
        //    luas.Attributes["onblur"] = "CalcBlur(this);";

        //    DataTable rs;
        //    string strSql;


        //    strSql = "SELECT * FROM REF_JENIS ORDER BY SN";
        //    rs = Db.Rs(strSql);
        //    for(int i=0;i<rs.Rows.Count;i++)
        //    {
        //        string v = rs.Rows[i]["Jenis"].ToString();
        //        string t = v + " - " + rs.Rows[i]["Nama"].ToString();
        //        jenis.Items.Add(new ListItem(t,v));
        //    }

        //}

        private void Fill()
        {
            //aKey.HRef = "javascript:openModal('UnitEditKey.aspx?NoJenis=" + NoJenis + "','350','150')";
            //aStatus.HRef = "javascript:openModal('UnitStatus.aspx?NoJenis=" + NoJenis + "','350','220')";
            btnlog.Attributes["onclick"] = "location.href='LogPk.aspx?Tb=REF_JENIS_LOG&Pk=" + NoJenis + "'";
            btndel.Attributes["onclick"] = "location.href='JenisDel.aspx?NoJenis=" + NoJenis + "'";

            string strSql = "SELECT * FROM REF_JENIS WHERE SN = '" + NoJenis + "'";
            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                nojenis.Text = rs.Rows[0]["SN"].ToString();
                jenis.Text = rs.Rows[0]["Jenis"].ToString();
                namajenis.Text = rs.Rows[0]["Nama"].ToString();
            }
        }

        private bool valid()
        {
            string s = "";
            bool x = true;

            if (Cf.isEmpty(jenis))
            {
                x = false;
                if (s == "") s = jenis.ID;
                jenisc.Text = "Kosong";
            }
            else
                jenisc.Text = "";

            if (Cf.isEmpty(namajenis))
            {
                x = false;
                if (s == "") s = namajenis.ID;
                namajenisc.Text = "Kosong";
            }
            else
                namajenisc.Text = "";


            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Tipe Unit Properti tidak boleh kosong.\\n"
                    + "2. Nama Tipe Unit Properti tidak boleh kosong.\\n"
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );

            return x;
        }


        protected void ok_Click(object sender, System.EventArgs e)
        {
            //if (Save()) Js.Close(this);
            if (valid())
            {
                int ada = Db.SingleInteger("SELECT COUNT(*) FROM REF_JENIS WHERE Jenis='" + jenis.Text + "' AND SN != " + NoJenis);
                if (ada > 0)
                {
                    string s = "";

                    Js.Alert(
                       this
                       , "Input Tidak Valid.\\n\\n"
                       + "Aturan Proses :\\n"
                       + "1. Tipe Unit Properti tidak boleh kosong.\\n"
                       + "2. Nama Tipe Unit tidak boleh kosong.\\n"
                       + "3. Tipe Unit Duplikat.\\n"
                       , "document.getElementById('" + s + "').focus();"
                       + "document.getElementById('" + s + "').select();"
                       );
                }
                else
                {
                    string strSql = "SELECT * FROM REF_JENIS WHERE SN = '" + NoJenis + "'";
                    DataTable rs = Db.Rs(strSql);
                    DataTable rsBef = Db.Rs("SELECT "
                                + " Jenis"
                                + ",Nama"
                                + ",SN"
                                + " FROM REF_JENIS "
                                + " WHERE SN = '" + NoJenis + "'");

                    //relasi
                    Db.Execute("UPDATE MS_UNIT SET Jenis='" + jenis.Text + "' WHERE Jenis='" + rs.Rows[0]["Jenis"] + "'");
                    Db.Execute("UPDATE MS_KONTRAK SET Jenis='" + jenis.Text + "' WHERE Jenis='" + rs.Rows[0]["Jenis"] + "'");
                    Db.Execute("UPDATE MS_RESERVASI SET Jenis='" + jenis.Text + "' WHERE Jenis='" + rs.Rows[0]["Jenis"] + "'");
                    Db.Execute("UPDATE MS_RESERVASI_OBS SET Jenis='" + jenis.Text + "' WHERE Jenis='" + rs.Rows[0]["Jenis"] + "'");

                    Db.Execute("UPDATE REF_JENIS SET Jenis='" + jenis.Text + "',Nama='" + namajenis.Text + "' WHERE SN = '" + NoJenis + "'");

                    DataTable rsAft = Db.Rs("SELECT "
                                + " Jenis"
                                + ",Nama"
                                + ",SN"
                                + " FROM REF_JENIS "
                                + " WHERE SN = '" + NoJenis + "'");

                    //Logfile
                    string Ket = "Jenis: " + jenis.Text + "<br>"
                        + Cf.LogCompare(rsBef, rsAft);

                    Db.Execute("EXEC spLogJenis"
                        + " 'EDIT'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + Ket + "'"
                        + ",'" + NoJenis + "'"
                        );

                    Js.Close(this);
                }
            }

        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            // if (Save()) Response.Redirect("JenisEdit.aspx?done=1&NoJenis=" + NoJenis);

            if (valid())
            {
                int ada = Db.SingleInteger("SELECT COUNT(*) FROM REF_JENIS WHERE Jenis='" + jenis.Text + "' AND SN != " + NoJenis);
                if (ada > 0)
                {
                    string s = "";

                    Js.Alert(
                       this
                       , "Input Tidak Valid.\\n\\n"
                       + "Aturan Proses :\\n"
                       + "1. Tipe Unit Properti tidak boleh kosong.\\n"
                       + "2. Nama Tipe Unit tidak boleh kosong.\\n"
                       + "3. Tipe Unit Duplikat.\\n"
                       , "document.getElementById('" + s + "').focus();"
                       + "document.getElementById('" + s + "').select();"
                       );
                }
                else
                {
                    string strSql = "SELECT * FROM REF_JENIS WHERE SN = '" + NoJenis + "'";
                    DataTable rs = Db.Rs(strSql);
                    DataTable rsBef = Db.Rs("SELECT "
                                + " Jenis"
                                + ",Nama"
                                + ",SN"
                                + " FROM REF_JENIS "
                                + " WHERE SN = '" + NoJenis + "'");

                    //relasi
                    Db.Execute("UPDATE MS_UNIT SET Jenis='" + jenis.Text + "' WHERE Jenis='" + rs.Rows[0]["Jenis"] + "'");
                    Db.Execute("UPDATE MS_KONTRAK SET Jenis='" + jenis.Text + "' WHERE Jenis='" + rs.Rows[0]["Jenis"] + "'");
                    Db.Execute("UPDATE MS_RESERVASI SET Jenis='" + jenis.Text + "' WHERE Jenis='" + rs.Rows[0]["Jenis"] + "'");
                    Db.Execute("UPDATE MS_RESERVASI_OBS SET Jenis='" + jenis.Text + "' WHERE Jenis='" + rs.Rows[0]["Jenis"] + "'");

                    Db.Execute("UPDATE REF_JENIS SET Jenis='" + jenis.Text + "',Nama='" + namajenis.Text + "' WHERE SN = '" + NoJenis + "'");

                    DataTable rsAft = Db.Rs("SELECT "
                                + " Jenis"
                                + ",Nama"
                                + ",SN"
                                + " FROM REF_JENIS "
                                + " WHERE SN = '" + NoJenis + "'");

                    //Logfile
                    string Ket = "Jenis: " + jenis.Text + "<br>"
                        + Cf.LogCompare(rsBef, rsAft);

                    Db.Execute("EXEC spLogJenis"
                        + " 'EDIT'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + Ket + "'"
                        + ",'" + NoJenis + "'"
                        );

                    Response.Redirect("JenisEdit.aspx?done=1&NoJenis=" + NoJenis);
                }
            }
        }

        private string NoJenis
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoJenis"]);
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

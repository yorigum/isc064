using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace ISC064.ADMINJUAL
{
    public partial class KomisiCFEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            Act.CekInt("INT");

            if (!Act.Sec("ED:" + Request.PhysicalPath))
            {
                ok.Enabled = false;
                save.Enabled = false;
            }

            if (!Page.IsPostBack)
            {
                Fill();
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
                        + "Edit Berhasil...";
            }
        }

       
        private void Fill()
        {
            btnlog.Attributes["onclick"] = "location.href='LogPk.aspx?Tb=REF_KOMISI_CF_LOG&Pk=" + Lvl + "'";
            btndel.Attributes["onclick"] = "location.href='KomisiCFDel.aspx?Lvl=" + Lvl + "'";


            atas.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            atas.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            atas.Attributes["onblur"] = "CalcBlur(this);";

            bawah.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            bawah.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            bawah.Attributes["onblur"] = "CalcBlur(this);";

            gm.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            gm.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            gm.Attributes["onblur"] = "CalcBlur(this);";

            sm.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            sm.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            sm.Attributes["onblur"] = "CalcBlur(this);";

            manager.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            manager.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            manager.Attributes["onblur"] = "CalcBlur(this);";

            string strSql = "SELECT * FROM REF_KOMISI_CF WHERE Lvl ='" + Lvl + "'";
            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                ket.Text = rs.Rows[0]["Keterangan"].ToString();
                atas.Text = Cf.Num(rs.Rows[0]["NilaiAtas"]);
                bawah.Text = Cf.Num(rs.Rows[0]["NilaiBawah"]);
                gm.Text = Cf.Num(rs.Rows[0]["NilaiGM"]);
                sm.Text = Cf.Num(rs.Rows[0]["NilaiSM"]);
                manager.Text = Cf.Num(rs.Rows[0]["NilaiM"]);
             
                tglInput.Text = Cf.Date(rs.Rows[0]["TglInput"]);
                tglEdit.Text = Cf.Date(rs.Rows[0]["TglEdit"]);
                
            }
        }

        private bool valid()
        {
            string s = "";
            bool x = true;


            if (Cf.isEmpty(ket))
            {
                x = false;
                if (s == "") s = ket.ID;
                ketc.Text = "Kosong";
            }
            else
                ketc.Text = "";

            
            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Level tidak boleh kosong.\\n"
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );

            return x;
        }

        private bool Save()
        {
            if (valid())
            {
                string Ket = ket.Text;
                decimal Atas = (atas.Text != "") ? Convert.ToDecimal(atas.Text) : 0;
                decimal Bawah = (bawah.Text != "") ? Convert.ToDecimal(bawah.Text) : 0;
                decimal GM = (gm.Text != "") ? Convert.ToDecimal(gm.Text) : 0 ;
                decimal SM = (sm.Text != "") ? Convert.ToDecimal(sm.Text) : 0;
                decimal Manager = (manager.Text != "") ? Convert.ToDecimal(manager.Text) : 0;

                DataTable rsBef = Db.Rs("SELECT "
                    + " Lvl AS [ID]"
                    + ",Keterangan AS Level"
                    + ",NilaiAtas AS [Nilai Batas Atas]"
                    + ",NilaiBawah AS [Nilai Batas Bawah]"
                    + ",NilaiGM AS [Nilai General Manager]"
                    + ",NilaiSM AS [Nilai Sales Manager]"
                    + ",NilaiM AS [Nilai Marketing]"
                    + " FROM REF_KOMISI_CF"
                    + " WHERE Lvl = '" + Lvl + "'"
                    );

                Db.Execute("EXEC spKomisiCFEdit"
                    + "  " + Lvl
                    + ",'" + Ket + "'"
                    + "," + Atas
                    + "," + Bawah
                    + "," + GM
                    + "," + SM
                    + "," + Manager
                    );

                DataTable rsAft = Db.Rs("SELECT "
                    + " Lvl AS [ID]"
                    + ",Keterangan AS Level"
                    + ",NilaiAtas AS [Nilai Batas Atas]"
                    + ",NilaiBawah AS [Nilai Batas Bawah]"
                    + ",NilaiGM AS [Nilai General Manager]"
                    + ",NilaiSM AS [Nilai Sales Manager]"
                    + ",NilaiM AS [Nilai Marketing]"
                    + " FROM REF_KOMISI_CF"
                    + " WHERE Lvl = '" + Lvl + "'"
                    );

                //Logfile
                string Keterangan = "Level : " + Ket + "<br>"
                    + Cf.LogCompare(rsBef, rsAft);

                Db.Execute("EXEC spLogKomisiCF"
                    + " 'EDIT'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + Keterangan + "'"
                    + ",'" + Lvl + "'"
                    );

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
            if (Save()) Response.Redirect("KomisiCFEdit.aspx?done=1&Lvl=" + Lvl);
        }

        private string Lvl
        {
            get
            {
                return Cf.Pk(Request.QueryString["Lvl"]);
            }
        }

    }
}

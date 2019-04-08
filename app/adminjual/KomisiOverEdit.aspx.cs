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
    public partial class KomisiOverEdit : System.Web.UI.Page
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
            btnlog.Attributes["onclick"] = "location.href='LogPk.aspx?Tb=REF_KOMISI_OVER_LOG&Pk=" + SN + "'";
            btndel.Attributes["onclick"] = "location.href='KomisiOverDel.aspx?SN=" + SN+ "'";


            overpro.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            overpro.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            overpro.Attributes["onblur"] = "CalcBlur(this);";

            overcross.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            overcross.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            overcross.Attributes["onblur"] = "CalcBlur(this);";


            string strSql = "SELECT * FROM REF_KOMISI_OVER WHERE SN = " + SN;
            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                jabatan.Text = rs.Rows[0]["Jabatan"].ToString();
                overpro.Text = Cf.Num(rs.Rows[0]["Project"]);
                overcross.Text = Cf.Num(rs.Rows[0]["CrossSelling"]);
             
                tglInput.Text = Cf.Date(rs.Rows[0]["TglInput"]);
                tglEdit.Text = Cf.Date(rs.Rows[0]["TglEdit"]);
                
            }
        }

        private bool valid()
        {
            string s = "";
            bool x = true;

            if (Cf.isEmpty(jabatan))
            {
                x = false;
                if (s == "") s = jabatan.ID;
                jabatanc.Text = "Kosong";
            }
            else
                jabatanc.Text = "";

            
            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Nama dan jabatan tidak boleh kosong.\\n"
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );

            return x;
        }

        private bool Save()
        {
            if (valid())
            {
                string Jabatan = jabatan.Text;
                decimal Project = (overpro.Text != "") ? Convert.ToDecimal(overpro.Text) : 0 ;
                decimal Cross = (overcross.Text != "") ? Convert.ToDecimal(overcross.Text) : 0;

                DataTable rsBef = Db.Rs("SELECT "
                    + " SN AS [No. Urut]"
                    + ",Jabatan"
                    + ",Project AS [Nilai Overriding Project]"
                    + ",CrossSelling AS [Nilai Overriding Cross Selling]"
                    + " FROM REF_KOMISI_OVER"
                    + " WHERE SN = " + SN
                    );

                Db.Execute("EXEC spKomisiOverEdit"
                    + "  " + SN
                    + ",'" + Jabatan + "'"
                    + "," + Project
                    + "," + Cross
                    );

                DataTable rsAft = Db.Rs("SELECT "
                    + " SN AS [No. Urut]"
                    + ",Jabatan"
                    + ",Project AS [Nilai Overriding Project]"
                    + ",CrossSelling AS [Nilai Overriding Cross Selling]"
                    + " FROM REF_KOMISI_OVER"
                    + " WHERE SN = " + SN
                    );

                //Logfile
                string Ket = "Jabatan : " + Jabatan + "<br>"
                    + Cf.LogCompare(rsBef, rsAft);

                Db.Execute("EXEC spLogKomisiOver"
                    + " 'EDIT'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + Ket + "'"
                    + ",'" + SN + "'"
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
            if (Save()) Response.Redirect("KomisiOverEdit.aspx?done=1&SN=" + SN);
        }

        private string SN
        {
            get
            {
                return Cf.Pk(Request.QueryString["SN"]);
            }
        }

    }
}

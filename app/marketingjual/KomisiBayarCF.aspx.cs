using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
    public partial class KomisiBayarCF : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!IsPostBack)
            {
                Fill();
            }

            Js.Confirm(this, "Lanjutkan dengan proses Pembayaran Closing Fee?");
        }

        private bool valid()
        {
            bool x = false;
            string s = "";

            int Nourut = Db.SingleInteger("Select NoUrut From MS_KOMISI where nokontrak ='" + NoKontrak + "' ");
            decimal CF = Db.SingleDecimal("SELECT NilaiOverriding From REF_SKOM_DETAIL WHERE Baris ='" + Baris + "' And Nomor ='" + Nourut + "'");
            if (Convert.ToDecimal(tbNilai.Text) > Convert.ToDecimal(Math.Round(CF)))
            {
                x = true;
                if (s == "") s = tbNilai.ID;
            }

            if (x)
            {
                Js.Alert(
                    this
                    , "1. Nilai harus diisi.\\n"
                    + "2. Nilai Tidak Boleh Melebihi Batas Komisi.\\n"
                    , " document.getElementById('" + s + "').focus();"
                    + " document.getElementById('" + s + "').select();"
                    );
            }
            return x;
        }

        private void Fill()
        {
            string strSql = "SELECT *"
                + " FROM MS_KOMISI"
                + " WHERE NoKontrak = '" + NoKontrak + "'";

            DataTable rr = Db.Rs(strSql);
            for (int j = 0; j < rr.Rows.Count; j++)
            {

                string strSql1 = "SELECT *"
                    + " FROM REF_SKOM_DETAIL"
                    + " Where Nomor = " + rr.Rows[0]["NoUrut"].ToString()
                    + " AND NilaiOverriding > 0";

                DataTable rs = Db.Rs(strSql1);

                for (int i = 0; i < rs.Rows.Count; i++)
                {
                    if (rs.Rows.Count == 0)
                        Response.Redirect("/CustomError/Deleted.html");
                    else
                    {
                        int NotaCF = Db.SingleInteger("SELECT ISNULL(MAX(NotaCF), 0) FROM MS_KOMISI_DETAIL");
                        NotaCF += 1;
                        notacf1.Text = NotaCF.ToString().PadLeft(7, '0');
                        lblNoKontrak.Text = NoKontrak;
                        lblAgent.Text = Db.SingleString("SELECT NamaPenerima FROM MS_KOMISI Where NoKontrak = '" + NoKontrak + "'");
                        
                        tbTglBayarClosing.Text = Cf.Day(DateTime.Now);

                        decimal cf = Db.SingleDecimal("SELECT NilaiOverriding FROM REF_SKOM_DETAIL Where Nomor = '" + rr.Rows[0]["NoUrut"].ToString() + "' AND Baris ='" + Baris + "'");
                        tbNilai.Text = Cf.Num(cf);
                        tbNilai.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
                        tbNilai.Attributes["onkeyup"] = "CalcType(this,tempnum);";
                        tbNilai.Attributes["onblur"] = "CalcBlur(this);";
                    }
                }
            }
            
               
        }

        private void save()
        {
            int NotaCF = Convert.ToInt32(notacf1.Text);
            DateTime TglBayar = Convert.ToDateTime(tbTglBayarClosing.Text);
            decimal NilaiCF = Convert.ToDecimal(tbNilai.Text);

            //DataTable rsBef = Db.Rs("SELECT NotaCF, TglBayarClosingFee, ClosingFee, FlagClosingFee FROM MS_KOMISI_DETAIL WHERE NoKontrak = '" + NoKontrak + "' AND BarisTermin = '" + Baris + "'");

            string strSql = "UPDATE MS_KOMISI_DETAIL "
                + " SET NotaCF = " + NotaCF
                + ", TglBayarClosingFee = '" + TglBayar + "'"
                + ", NilaiBayarCF = " + NilaiCF
                + ", FlagClosingFee = 1 "
                + " WHERE NoKontrak = '" + NoKontrak + "'"
                + " AND BarisTermin = " + Baris ;
            Db.Execute(strSql);

            string ket = "---BAYAR Closing Fee (KOMISI)---<br>"
                          +  Cf.LogCapture(Db.Rs("SELECT NotaCF, TglBayarClosingFee, ClosingFee, FlagClosingFee FROM MS_KOMISI_DETAIL WHERE NoKontrak = '" + NoKontrak + "' AND BarisTermin = '" + Baris + "'"));
            
            Db.Execute("EXEC spLogKontrak"
                + " 'BAYAR KOMISI (Closing Fee)'"
                + ",'" + Act.UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'" + ket + "'"
                + ",'" + NoKontrak + "'"
                );

            Response.Redirect("KontrakJadwalKomisi.aspx?Nokontrak=" + NoKontrak);
        }

        private string NoKontrak
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoKontrak"]);
            }
        }

        //private string NoUrut
        //{
        //    get
        //    {
        //        return Cf.Pk(Request.QueryString["NoUrut"]);
        //    }
        //}

        private string Baris
        {
            get
            {
                return Cf.Pk(Request.QueryString["Baris"]);
            }
        }
        protected void cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("KontrakJadwalKomisi.aspx?Nokontrak=" + NoKontrak);
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            
            if (!valid())
                save();
           
        }
}

}
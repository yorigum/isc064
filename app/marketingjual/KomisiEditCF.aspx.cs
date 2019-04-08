using System;
using System.Drawing;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
    public partial class KomisiEditCF : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!IsPostBack)
            {
                Fill();
            }

            Js.Confirm(this, "Lanjutkan dengan proses Edit Closing Fee?");
        }

        private bool isDuplikat()
        {
            bool x = false;
            string s = "";

            int Nourut = Db.SingleInteger("Select NoUrut From MS_KOMISI where nokontrak ='" + NoKontrak + "' ");
            decimal CF = Db.SingleDecimal("SELECT NilaiOverriding From REF_SKOM_DETAIL WHERE Nomor ='"+ Nourut +"' And Baris ='" + Baris + "'");
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
                        lblNoKontrak.Text = NoKontrak;
                        lblAgent.Text = Db.SingleString("SELECT NamaPenerima FROM MS_KOMISI Where NoKontrak = '" + NoKontrak + "'");

                        tbTglBayarClosing.Text = Cf.Day(DateTime.Now);
                        decimal cf = Db.SingleDecimal("SELECT CLosingFee FROM MS_KOMISI_DETAIL Where Nokontrak = '" + NoKontrak + "' AND BarisTermin ='" + Baris + "'");
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
            DateTime TglBayar = Convert.ToDateTime(tbTglBayarClosing.Text);
            decimal NilaiCF = Convert.ToDecimal(tbNilai.Text);

            DataTable rsBef = Db.Rs("SELECT Max(ClosingFee) FROM MS_KOMISI_DETAIL WHERE NoKontrak = '" + NoKontrak + "' AND BarisTermin = '" + Baris + "'");

            string strSql = "UPDATE MS_KOMISI_DETAIL "
                + " SET TglBayarClosingFee = '" + TglBayar + "'"
                + ", NilaiBayarCF = " + NilaiCF
                + ", FlagClosingFee = 1 "
                + " WHERE NoKontrak = '" + NoKontrak + "'"
                + " AND BarisTermin = " + Baris;
            Db.Execute(strSql);

            DataTable rsAft = Db.Rs("SELECT Max(ClosingFee) FROM MS_KOMISI_DETAIL WHERE NoKontrak = '" + NoKontrak + "' AND BarisTermin = '" + Baris + "'");

            DataTable rsDetail = Db.Rs("SELECT"
					+ " MS_KONTRAK.NoKontrak AS [No. Kontrak]"
					+ ",MS_KONTRAK.NoUnit AS [Unit]"
					+ ",MS_CUSTOMER.Nama AS [Customer]"
					+ ",MS_KONTRAK.NilaiKontrak AS [Nilai Kontrak]"
					+ ",MS_KONTRAK.SkemaKomisi AS [Skema Komisi]"
					+ " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER"
					+ " ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
					+ " WHERE MS_KONTRAK.NoKontrak = '" + NoKontrak + "'");

			string Ket = Cf.LogCapture(rsDetail)
					+ "<br>---EDIT KOMISI (Closing Fee)---<br>"
					+ Cf.LogList(rsBef, rsAft, "KOMISI (Closing Fee)");

            Db.Execute("EXEC spLogKontrak"
                + " 'EDIT KOMISI (Closing Fee)'"
                + ",'" + Act.UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'" + Ket + "'"
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

            if (!isDuplikat())
            {
                save();
            }
        }

    }
}
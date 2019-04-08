using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace ISC064.MARKETINGJUAL
{
    public partial class KomisiGeneratePeriode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            FeedBack();

            
            if(valid())
            {
                Fill();
            }

            if(!IsPostBack)
            {
                komisi.Items.Clear();
                DataTable rs = Db.Rs("SELECT * FROM REF_SKOM");
                for (int i = 0; i < rs.Rows.Count; i++)
                {
                    string a = rs.Rows[i]["Nama"].ToString();
                    string b = rs.Rows[i]["Nomor"].ToString();
                    komisi.Items.Add(new ListItem(a, b));

                }

            }
            
        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "Generate Komisi Berhasil...";
            }
        }

        protected void next_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                //pilih.Visible = false;
                frm.Visible = true;

                DateTime Dari = Convert.ToDateTime(dari.Text);
                DateTime Sampai = Convert.ToDateTime(sampai.Text);
                FillTop();
                MidTB();
                Fill();
                
            }

        }

        private bool valid()
        {

            bool x = true;
            string s = "";
            bool a = true;
            string b = "";
            bool f = true;
            string j = "";

            if (!Cf.isTgl(dari))
            {
                a = false;
                if (b == "") b = dari.ID;
                
            }
            else
                daric.Text = "";

            if (!Cf.isTgl(sampai))
            {
                a = false;
                if (b == "") b = sampai.ID;
                
            }
            else
                sampaic.Text = "";

            //decimal nilaibayar = Db.SingleDecimal("Select Count(b.NilaiCF) From MS_KOMISI a join ms_komisi_detail b on b.nokontrak=a.nokontrak Where a.Nama = '(SELECT Nama from ms_agent where noagent = '"+noagent.Text+"')' ");
            //if(nilaibayar > 0)
            //{
            //    f = false;
            //    if (j == "") j = errclear.ID;
            //}

            if(!a)
            {
                daric.Text = "Tanggal Harus Di isi ...";
                sampaic.Text = "Tanggal Harus Di isi ...";
            }
            if (!x)
            {
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Tanggal tidak boleh kosong.\\n"
                    + "2. Jadwal (FIX), Format tanggal : Bulan / Tanggal / Tahun.\\n"
                    + "3. Jadwal (M/D), Interval harus berupa angka.\\n"
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );
            }

            return a;
            return x;
        }

        protected void Fill()
        {
            list.Controls.Clear();

            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);

            string NoAgent = noagent.Text;
            string strSql = "SELECT * FROM MS_KONTRAK WHERE TglKontrak >= '" + Dari+ "' and TglKontrak <= '" + Sampai+ "' AND NoAgent='" + NoAgent + "' and Status='A' and FlagKomisi=0 ";
            DataTable rs = Db.Rs(strSql);
            int index = 0;
                for (int i = 0; i < rs.Rows.Count; i++)
                {
                    string NoKontrak = rs.Rows[i]["NoKontrak"].ToString();
                    string customer = Db.SingleString(
                        "SELECT Nama FROM MS_CUSTOMER WHERE NoCustomer = " + rs.Rows[i]["NoCustomer"]);
                    string Unit = rs.Rows[i]["NoUnit"].ToString();
                    FillTb(Convert.ToInt32(rs.Rows[i]["NoAgent"]), rs.Rows[i]["Refferal"].ToString(), Convert.ToDecimal(rs.Rows[i]["NilaiDPP"]), NoKontrak, customer, Unit,Convert.ToDateTime(rs.Rows[i]["TglKontrak"]), rs.Rows[i]["CaraBayar"].ToString(), index);
                    index++;
                }

        }

        protected void FillTop()
        {
            HtmlTableCell c;
            HtmlTableRow tr;
            tr = new HtmlTableRow();

            c = new HtmlTableCell();
            c.Attributes["style"] = "Background:lightblue;";
            c.RowSpan = 2;
            c.InnerHtml = "#";
            tr.Cells.Add(c);

            c = new HtmlTableCell();
            c.Attributes["style"] = "Background:lightblue;";
            c.RowSpan = 2;
            c.InnerHtml = "No Kontrak";
            tr.Cells.Add(c);

            c = new HtmlTableCell();
            c.Attributes["style"] = "Background:lightblue;";
            c.RowSpan = 2;
            c.InnerHtml = "Tgl Kontrak";
            tr.Cells.Add(c);

            c = new HtmlTableCell();
            c.Attributes["style"] = "Background:lightblue;";
            c.RowSpan = 2;
            c.InnerHtml = "Customer";
            tr.Cells.Add(c);

            c = new HtmlTableCell();
            c.Attributes["style"] = "Background:lightblue;";
            c.RowSpan = 2;
            c.InnerHtml = "Unit";
            tr.Cells.Add(c);

            c = new HtmlTableCell();
            c.Attributes["style"] = "Background:lightblue;";
            c.RowSpan = 2;
            c.InnerHtml = "Nilai DPP";
            tr.Cells.Add(c);

            c = new HtmlTableCell();
            c.Attributes["style"] = "Background:lightblue;";
            c.RowSpan = 2;
            c.InnerHtml = "Cara Bayar";
            tr.Cells.Add(c);

            c = new HtmlTableCell();
            c.Attributes["style"] = "Background:lightblue;";
            c.RowSpan = 2;
            c.InnerHtml = "Jenis Komisi";
            tr.Cells.Add(c);

            c = new HtmlTableCell();
            c.Attributes["style"] = "Background:lightblue;";
            c.RowSpan = 2;
            c.InnerHtml = "Penerima";
            tr.Cells.Add(c);

            //c = new HtmlTableCell();
            //c.Attributes["style"] = "Background:lightblue;";
            //c.RowSpan = 2;
            //c.InnerHtml = "Persentase";
            //tr.Cells.Add(c);

            c = new HtmlTableCell();
            c.Attributes["style"] = "Background:lightblue;";
            c.RowSpan = 2;
            c.InnerHtml = "Nilai Closing Fee";
            tr.Cells.Add(c);

            c = new HtmlTableCell();
            c.Attributes["style"] = "Background:skyblue;";
            c.RowSpan = 2;
            c.InnerHtml = "Termin";
            tr.Cells.Add(c);

            string hasil = "SELECT Nama FROM REF_SKOM_DETAIL Where Nomor = '" + komisi.SelectedValue + "'";
            DataTable hr = Db.Rs(hasil);
            if (hr.Rows.Count > 0)
            {
                c = new HtmlTableCell();
                c.Attributes["style"] = "Background:skyblue;text-align:center";
                c.Align = "Right";
                c.ColSpan = hr.Rows.Count;
                c.InnerText = "Nilai Komisi";
                tr.Cells.Add(c);

            }

            TopList.Controls.Add(tr);
        }

        protected void MidTB()
        {
            HtmlTableCell c;
            HtmlTableRow tr;
            tr = new HtmlTableRow();

            string hasil = "SELECT Nama FROM REF_SKOM_DETAIL Where Nomor = '" + komisi.SelectedValue + "'";
            DataTable hr = Db.Rs(hasil);
            if (hr.Rows.Count > 0)
            {
                int d = 0;
                for (int j = 0; j < hr.Rows.Count; j++)
                {
                    c = new HtmlTableCell();
                    c.Attributes["style"] = "Background:skyblue;";
                    c.Align = "Right";
                    c.InnerText = hr.Rows[j]["Nama"].ToString();
                    tr.Cells.Add(c);

                }
            }
            MiddleList.Controls.Add(tr);
        }
        protected void FillTb(int NoAgent, string Refferal, decimal DPP, string NoKontrak, string Customer, string Unit, DateTime TglKontrak, string CaraBayar, int index)
        {
            DataTable rs = Db.Rs("SELECT * FROM MS_AGENT WHERE NoAgent=" + NoAgent);
            if (rs.Rows.Count <= 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                HtmlTableCell c;
                HtmlTableRow tr;
                CheckBox cb;
                Label l;

                DataTable kj = Db.Rs("Select Nomor from ref_skom_term where Nomor = '" + komisi.SelectedValue+ "'");

                tr = new HtmlTableRow();

                cb = new CheckBox();
                cb.ToolTip = "proses_" + index;
                cb.ID = "proses_" + index;
                c = new HtmlTableCell();
                c.Controls.Add(cb);
                c.RowSpan = kj.Rows.Count;
                tr.Cells.Add(c);


                c = new HtmlTableCell();
                c.InnerHtml = NoKontrak;
                c.ID = "kontrak_" + index;
                c.RowSpan = kj.Rows.Count;
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = Cf.Day(TglKontrak);
                c.RowSpan = kj.Rows.Count;
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerText = Customer;
                c.RowSpan = kj.Rows.Count;
                c.ID = "cust_" + index;
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerText = Unit;
                c.RowSpan = kj.Rows.Count;
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerText = Cf.Num(Math.Round(DPP));
                c.RowSpan = kj.Rows.Count;
                c.Align = "Right";
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerText = CaraBayar;
                c.RowSpan = kj.Rows.Count;
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                string nama1 = Db.SingleString("SELECT Nama FROM REF_SKOM WHERE Nomor = '" + komisi.SelectedValue + "'");
                c.InnerText = nama1;
                c.ID = "komisi_" + index;
                c.RowSpan = kj.Rows.Count;
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerText = rs.Rows[0]["Nama"].ToString();
                c.ID = "nama_" + index;
                c.RowSpan = kj.Rows.Count;
                tr.Cells.Add(c);

                decimal persen1 = Db.SingleDecimal("Select NilaiKomisi from ref_skom where nomor = '" + komisi.SelectedValue + "'");

                decimal nilai1 = Db.SingleDecimal("Select NilaiOverriding from ref_skom_detail where nomor = '" + komisi.SelectedValue + "'");
               
                decimal NilaiKomisi = DPP * persen1 / 100;
                c = new HtmlTableCell();
                c.Align = "Right";
                c.InnerText = Cf.Num(Math.Round(NilaiKomisi));
                c.ID = "NKomisi_" + index;
                c.RowSpan = kj.Rows.Count;
                tr.Cells.Add(c);

                list.Controls.Add(tr);

                termin(tr,c,Convert.ToInt32(komisi.SelectedValue), DPP,index);
            }
        }

        protected void termin(HtmlTableRow tr, HtmlTableCell c, int nomor, decimal DPP, int index)
        {
            System.Text.StringBuilder ArrNilaikomisi = new System.Text.StringBuilder();
            string nc = "select Nominal from ref_skom_detail where nomor = '" + nomor + "'";
                        DataTable nk = Db.Rs(nc);
                        for (int g = 0; g < nk.Rows.Count; g++)
                        {
                            ArrNilaikomisi.Append(Cf.Num(Convert.ToDecimal(nk.Rows[g]["Nominal"])) + ";");
                        }
            
            string hasil = "SELECT * FROM REF_SKOM_TERM Where Nomor = '" + nomor + "'";

            DataTable hr = Db.Rs(hasil);    
            if (hr.Rows.Count > 0)
            {
                for (int j = 0; j < hr.Rows.Count; j++)
                {
                    if (j > 0)
                    {
                        tr = new HtmlTableRow();
                    }

                    c = new HtmlTableCell();
                    c.Align = "Right";
                    c.InnerText = hr.Rows[j]["Nama"].ToString();
                    tr.Cells.Add(c);
                    
                    string[] Arrpersens = hr.Rows[j]["PersenLv"].ToString().Split(';');
                    int hj = Arrpersens.Length-1;

                    for (int a = 0; a < hj; a++)
                    {
                        string hw = "SELECT a.Nama,a.PersenLv,b.Nama,b.Nominal FROM REF_SKOM_TERM a join REF_SKOM_DETAIL b on a.Nomor = b.Nomor  Where a.Nomor = '" + nomor + "'";
                        DataTable hs = Db.Rs(hw);

                        string[] Arrnilai = ArrNilaikomisi.ToString().Split(';');
                        
                            c = new HtmlTableCell();
                            c.Align = "Right";
                            decimal persent = Convert.ToDecimal(Arrnilai[a]);
                            decimal komision = (DPP * persent) / 100;
                            decimal persens = Convert.ToDecimal(Arrpersens[a]);
                            decimal Hasil = (komision * persens) / 100;
                            c.InnerText = Cf.Num(Math.Round(Hasil));
                            tr.Cells.Add(c);
                    }
                    list.Controls.Add(tr);
                }
            }
        }
        protected void save_Click(object sender, EventArgs e)
        {
            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);

            string strSql = "SELECT * FROM MS_KONTRAK WHERE TglKontrak >= '" + Dari + "' and TglKontrak <= '" + Sampai + "' and Status='A' and NoAgent='" + noagent.Text + "' and FlagKomisi=0";
            DataTable rs = Db.Rs(strSql);
          
            for (int r = 0; r < rs.Rows.Count; r++ )
            {
                CheckBox cb = (CheckBox)list.FindControl("proses_" + r);
                HtmlTableCell nk = (HtmlTableCell)list.FindControl("kontrak_" + r);
                HtmlTableCell nilai = (HtmlTableCell)list.FindControl("NKomisi_" + r);
                HtmlTableCell namakomisi = (HtmlTableCell)list.FindControl("komisi_" + r);
               
                if (cb.Checked)
                {
                    Save(Convert.ToInt32(rs.Rows[0]["NoAgent"]), Convert.ToInt32(komisi.SelectedValue), (nk.InnerHtml).ToString(), (komisi.SelectedItem).ToString(), Convert.ToDecimal(nilai.InnerHtml));
                }
            }
            Response.Redirect("KomisiGeneratePeriode.aspx?done=1");
        }

        protected void Save(int NoAgent, int TipeKomisi, string NoKontrak, string namakomisi, decimal Nilaikomisi)
        {
            DataTable rs = Db.Rs("SELECT * FROM MS_AGENT WHERE NoAgent=" + NoAgent);
            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                if (Nilaikomisi > 0)
                {
                    //Response.Write("=> " + NoKontrak + "-" + NoAgent + "-" +TipeKomisi+ "<br/>");

                    Db.Execute("EXEC spKomisiDaftar "
                        + "'" + NoKontrak + "'"
                        + ", '" + namakomisi + "'"
                        + ", " + Nilaikomisi
                        + ", '" + rs.Rows[0]["Nama"].ToString() + "'"
                        + ", " + TipeKomisi
                        + ", " + NoAgent
                        );
                    
                    SaveDetail(NoKontrak,Convert.ToInt32(komisi.SelectedValue),NoAgent);
                }
            }
        }

        protected void SaveDetail(string Nokontrak, int nomor, int NoAgent)
        {
            System.Text.StringBuilder ArrNilaikomisi = new System.Text.StringBuilder();
            System.Text.StringBuilder ArrNilaiCF = new System.Text.StringBuilder();
            System.Text.StringBuilder ArrKom = new System.Text.StringBuilder();
            decimal DPP = Db.SingleDecimal("Select NilaiDPP from MS_KONTRAK Where NoKontrak = '" + Nokontrak + "'");

            string nc = "select Nominal, NilaiOverriding from ref_skom_detail where nomor = '" + nomor + "'";
            DataTable nk = Db.Rs(nc);
            for (int g = 0; g < nk.Rows.Count; g++)
            {
                ArrNilaikomisi.Append(Cf.Num(Convert.ToDecimal(nk.Rows[g]["Nominal"])) + ";");
                ArrNilaiCF.Append(Cf.Num(Convert.ToDecimal(nk.Rows[g]["NilaiOverriding"])) + ";");
            }

            string hasil = "SELECT * FROM REF_SKOM_TERM Where Nomor = '" + nomor + "'";
            DataTable hr = Db.Rs(hasil);
            if (hr.Rows.Count > 0)
            {
                for (int j = 0; j < hr.Rows.Count; j++)
                {
                    string[] Arrpersens = hr.Rows[j]["PersenLv"].ToString().Split(';');
                    int hj = Arrpersens.Length - 1;

                    string cnn1 = "";
                    string cnn2 = "";
                    for (int a = 0; a < hj; a++)
                    {
                        string hw = "SELECT a.Nama as NM,a.PersenLv,b.Nama as NMSkema,b.Nominal FROM REF_SKOM_TERM a join REF_SKOM_DETAIL b on a.Nomor = b.Nomor  Where a.Nomor = '" + nomor + "' Order by a.Nama asc";
                        DataTable hs = Db.Rs(hw);

                        string[] Arrnilai = ArrNilaikomisi.ToString().Split(';');
                        string[] ArrCF = ArrNilaiCF.ToString().Split(';');

                        decimal NilaiCF = Convert.ToDecimal(ArrCF[a]);
                        decimal persent = Convert.ToDecimal(Arrnilai[a]);
                        decimal komision = (DPP * persent) / 100;
                        decimal persens = Convert.ToDecimal(Arrpersens[a]);
                        decimal Hasil = (komision * persens) / 100;
                        ArrKom.Append(Hasil + ";");
                        string[] cnn = ArrKom.ToString().Split(';');
                        cnn1 = cnn[0];
                        cnn2 = cnn[1];

                            //Response.Write(nomor + " / " + Nokontrak + "-" + (j + 1) + "-" + (a + 1) + "-" + hc.Rows[a][""].ToString() + "-" + hr.Rows[j]["nama"].ToString() + "-" + hs.Rows[a]["NMSkema"].ToString() + "-" + Cf.Num(Math.Round(Hasil)) + " - " + Cf.Num(Math.Round(NilaiCF)) + "<br/>");

                            Db.Execute("EXEC spKomisiDetail "
                                + "'" + Nokontrak + "'"
                                + ", " + (j + 1)
                                + ", '" + hr.Rows[j]["nama"].ToString() + "'"
                                + ", '" + hs.Rows[a]["NMSkema"].ToString() + "'"
                                + ", '" + Cf.Num(Hasil) + "'"
                                + ", '" + Cf.Num(NilaiCF) + "'"
                                + ", " + (a + 1)
                                );
                        
                    }
                }

            }
        }

        //DataTable rsKomisi = Db.Rs("SELECT CONVERT(VARCHAR,NoUrut) + '.  ' + NamaKomisi + ' - '+ NamaPenerima + ' ('+Tipe+')   CAIR:' + CONVERT(VARCHAR,TermCair,1) + '% ' + CONVERT(VARCHAR,NilaiKomisi,1) "
        //  + "FROM MS_KOMISI_DETAIL WHERE NoKontrak = '" + NoKontrak + "' ORDER BY NoUrut");

        //string Ket = Cf.LogList(rsKomisi, "JADWAL KOMISI");

        //Db.Execute("EXEC spLogKontrak"
        //    + " 'KOMISI'"
        //    + ",'" + Act.UserID + "'"
        //    + ",'" + Act.IP + "'"
        //    + ",'" + Ket + "'"
        //    + ",'" + NoKontrak + "'"
        //    );

        protected void genfalse_Click(object sender, EventArgs e)
        {
            if (valid())
            {
                frm.Visible = true;

                Response.Redirect("KomisiClear.aspx?Dari=" + Convert.ToDateTime(dari.Text) + "&Sampai=" + Convert.ToDateTime(sampai.Text) + "&NoAgent=" + noagent.Text + "");
            }
        }
}
}


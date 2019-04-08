using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;


namespace ISC064.ADMINJUAL
{
    public partial class AgentKomisiGen : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            Act.CekInt("NoAgent");

            if (!Page.IsPostBack)
            {
                //backbtn.Visible = false;
                dari.Text = Cf.Day(Cf.AwalBulan());
                sampai.Text = Cf.Day(Cf.AkhirBulan());
                frm.Visible=false;

            }
            FeedBack();
            if (frm.Visible) Js.Confirm(this, "Lanjutkan proses generate jadwal komisi?");

        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        //+ "<a href=\"javascript:popJadwalKomisi('" + Request.QueryString["done"] + "')\">"
                    + "Generate Komisi Berhasil...";
                    //+ "</a>";
            }
        }

        private bool valid()
        {
            bool x = true;
            string s = "";

            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);
            string thnDari = Dari.Year.ToString();
            string thnSampai = Sampai.Year.ToString();


            if (x)
            {

                if (!Cf.isTgl(dari))
                {
                    x = false;
                    if (s == "") s = dari.ID;
                    daric.Text = "Tanggal";
                }
                else
                    daric.Text = "";

                if (!Cf.isTgl(sampai))
                {
                    x = false;
                    if (s == "") s = sampai.ID;
                    sampaic.Text = "Tanggal";
                }
                else
                    sampaic.Text = "";
            }

            if (!x && s != "")
            {
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Pilih periode yang diinginkan.\\n"
                    + "Format Tanggal : Tanggal/ Bulan / Tahun.\\n"
                    //+ "Kemungkinan Sebab :\\n"
                    //+ "1. Kontrak tersebut tidak terdaftar.\\n"
                    //+ "2. Kontrak tersebut sudah dibatalkan.\\n"
                    //+ "3. Jadwal komisi sudah pernah dilakukan."
                    , "document.getElementById('nokontrak').focus();"
                    + "document.getElementById('nokontrak').select();"
                    );
             }


            return x;
        }

        protected void next_Click(object sender, EventArgs e)
        {
            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);
            string thnDari = Dari.Year.ToString();
            string thnSampai = Sampai.Year.ToString();
            if (thnDari.Length == 3 || thnSampai.Length == 3)
            {
                Js.Alert(
                this
                , "Format Tahun Tidak Valid.\\n\\n"
                + "Format Tahun : 2 atau 4 digit.\\n"
                , "document.getElementById('nokontrak').focus();"
                + "document.getElementById('nokontrak').select();"
                );
            }
            else
            {
                    if (valid())
                    {
                        pilih.Visible = false;
                        pilih2.Visible = false;
                        frm.Visible = true;

                        FillTb(NoAgent);

                        Js.Confirm(this, "Lanjutkan proses generate jadwal komisi?");
                    }
            }
        }

        private void FillTb(string NoAgent)
        {
            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);

            lbldari.Text = Cf.Day(dari.Text);
            lblsampai.Text = Cf.Day(sampai.Text);
            agent.Text = Db.SingleString("SELECT Nama FROM MS_AGENT WHERE NoAgent='"+NoAgent+"'");

            if (Dari > Sampai)
            {
                DateTime x = Sampai;
                Sampai = Dari;
                Dari = x;
            }

            string strSql = "SELECT "
                + " NoKontrak"
                + ",NoUnit"
                + ",NilaiKontrak"
                + ",TglKontrak"
                + ",Nama AS Cs"
                + ",Gross"
                + ",DiskonRupiah"
                + ",BungaNominal"
                + ",NilaiKontrak"
                + " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
                + " WHERE NoAgent = '" + NoAgent+"'"
                + " AND MS_KONTRAK.Status  = 'A'"
                + " AND MS_KONTRAK.FlagKomisi = 0"
                + " AND MS_KONTRAK.TglKontrak >= '" + Dari + "'"
                + " AND MS_KONTRAK.TglKontrak <= '" + Sampai + "'"
                + " ORDER BY NoKontrak";

            DataTable rs = Db.Rs(strSql);


            if (rs.Rows.Count == 0)
            {
                Rpt.NoData(rpt, rs, "Agen ini belum pernah melakukan penjualan / komisi untuk periode ini sudah digenerate.");
                save.Enabled = false;
            }

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;
                
                TableRow r = new TableRow();
                TableCell c;

                c = new TableCell();
                c.Text = rs.Rows[i]["NoKontrak"].ToString();
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglKontrak"]);
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoUnit"].ToString();
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Cs"].ToString();
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["Gross"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["NilaiKontrak"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                r.Cells.Add(c);

                Rpt.Border(r);
                rpt.Rows.Add(r);
            }
        }


        protected void save_Click(object sender, EventArgs e)
        {
            //Generate komisi

            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);           

            decimal PersenKomisi = 0;
            decimal komisiCF = 0;
            decimal akumulasiKomisi = 0;

            string strSql = "SELECT "
                        + " NoKontrak"
                        + ",NoUnit"
                        + ",NilaiKontrak"
                        + ",TglKontrak"
                        + ",Jenis"
                        + ",Nama AS Cs"
                        + ",Gross"
                        + ",CaraBayar"
                        + ",DiskonRupiah"
                        + ",CaraBayar"
                        + ",BungaNominal"
                        + " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
                        + " WHERE NoAgent = '" + NoAgent + "'"
                        + " AND MS_KONTRAK.Status  = 'A'"
                        + " AND MS_KONTRAK.FlagKomisi = 0"
                        + " AND MS_KONTRAK.TglKontrak >= '" + Dari + "'"
                        + " AND MS_KONTRAK.TglKontrak <= '" + Sampai + "'"
                        + " ORDER BY NoKontrak";

            DataTable rs = Db.Rs(strSql);
            if (rs.Rows.Count > 0)
            {
                //kontrak per agent
                int jumUnit = rs.Rows.Count;
                string tgl = null;
                string periode = null;
                decimal komisi40=0;
                decimal komisi60=0;
                decimal bunga = 0;

                for (int i = 0; i < jumUnit; i++)
                {
                    if (rs.Rows[i]["BungaNominal"] == null)
                        bunga = 0;
                    else
                        bunga = Convert.ToDecimal(rs.Rows[i]["BungaNominal"]);
                    decimal afterDiskon = Convert.ToDecimal(rs.Rows[i]["Gross"]) - Convert.ToDecimal(rs.Rows[i]["DiskonRupiah"]) + bunga;
                    DateTime tgl2 = Convert.ToDateTime(rs.Rows[i]["TglKontrak"]);
                    string month = Cf.Monthname(tgl2.Month);
                    tgl = Cf.Day(rs.Rows[i]["TglKontrak"].ToString());
                    periode = month +" "+ tgl.Substring(7, 4);

                    if (jumUnit == 1)
                    {
                        PersenKomisi = (decimal)0.008;
                        decimal nilaiKomisi = PersenKomisi * afterDiskon;
                        if (rs.Rows[i]["CaraBayar"].ToString() == "CASH KERAS")
                        {
                            akumulasiKomisi = Math.Round(nilaiKomisi + ((decimal)0.015 * nilaiKomisi));
                            komisi40 = (decimal)0.4 * akumulasiKomisi;
                            komisi60 = (decimal)0.6 * akumulasiKomisi;

                        }
                        else
                        {
                            akumulasiKomisi = Math.Round(nilaiKomisi);
                            komisi40 = (decimal)0.4 * akumulasiKomisi;
                            komisi60 = (decimal)0.6 * akumulasiKomisi;
                        }
                    }
                    else if (jumUnit >= 2 && jumUnit <= 4)
                    {
                        PersenKomisi = (decimal)0.01;
                        decimal nilaiKomisi = PersenKomisi * afterDiskon;
                        if (rs.Rows[i]["CaraBayar"].ToString() == "CASH KERAS")
                        {
                            akumulasiKomisi = Math.Round(nilaiKomisi + ((decimal)0.015 * nilaiKomisi));
                            komisi40 = (decimal)0.4 * akumulasiKomisi;
                            komisi60 = (decimal)0.6 * akumulasiKomisi;
                        }
                        else
                        {
                            akumulasiKomisi = Math.Round(nilaiKomisi);
                            komisi40 = (decimal)0.4 * akumulasiKomisi;
                            komisi60 = (decimal)0.6 * akumulasiKomisi;
                        }
                    }
                    else if (jumUnit >= 5)
                    {
                        PersenKomisi = (decimal)0.015;
                        decimal nilaiKomisi = PersenKomisi * afterDiskon;
                        if (rs.Rows[i]["CaraBayar"].ToString() == "CASH KERAS")
                        {
                            akumulasiKomisi = Math.Round(nilaiKomisi + (decimal)0.015 * nilaiKomisi);
                            komisi40 = (decimal)0.4 * akumulasiKomisi;
                            komisi60 = (decimal)0.6 * akumulasiKomisi;
                        }
                        else
                        {
                            akumulasiKomisi = Math.Round(nilaiKomisi);
                            komisi40 = (decimal)0.4 * akumulasiKomisi;
                            komisi60 = (decimal)0.6 * akumulasiKomisi;
                        }
                    }

                    if (jumUnit >= 1 && jumUnit <= 4)
                    {
                        komisiCF = (decimal)200000;
                    }
                    else if (jumUnit >= 5)
                    {
                        komisiCF = (decimal)300000;
                    }

                    decimal NoUrut = Db.SingleDecimal("SELECT ISNULL(MAX(NoUrut),0) + 1 FROM MS_KOMISI WHERE NoAgent= '" + NoAgent + "' ");
                    Db.Execute("INSERT INTO MS_KOMISI (NoKontrak, NoUrut, NilaiKomisi, NamaKomisi, NoAgent, PeriodeKomisi, ClosingFee, Komisi40, Komisi60, CaraBayar)"
                    + " VALUES('" + rs.Rows[i]["NoKontrak"] + "', '" + NoUrut + "', '" + akumulasiKomisi + "', 'Komisi Periode " + periode + "', '" + NoAgent + "', '" + periode + "', '" + komisiCF + "', '" + komisi40 + "', '" + komisi60 + "','"+rs.Rows[i]["CaraBayar"]+"')");

                    Db.Execute("UPDATE MS_KONTRAK"
                    + " SET FlagKomisi = 1 WHERE NoKontrak='"+rs.Rows[i]["NoKontrak"].ToString()+"' AND NoAgent = '" + NoAgent + "'");
                }              
            }

            Response.Redirect("AgentKomisiGen.aspx?done=1&NoAgent=" + NoAgent);
        }



        private string NoAgent
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoAgent"]);
            }
        }

   }
}
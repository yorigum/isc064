using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL.Laporan
{
    public partial class MasterKomisi : System.Web.UI.Page
    {
        private string StatusS { get { return (Request.QueryString["status_s"]); } }
        private string StatusA { get { return (Request.QueryString["status_a"]); } }
        private string StatusB { get { return (Request.QueryString["status_b"]); } }
        private string UserID { get { return (Request.QueryString["userid"]); } }
        private string Agent { get { return (Request.QueryString["sales"]); } }
        private string Project { get { return (Request.QueryString["project"]); } }
        private string Perusahaan { get { return (Request.QueryString["pers"]); } }
        private string AttachmentID { get { return (Request.QueryString["id"]); } }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Report();
        }

        protected int UserAgent()
        {
            int a = Db.SingleInteger("SELECT NoAgent FROM " + Mi.DbPrefix + "SECURITY..USERNAME WHERE UserID = '" + UserID + "'");

            return a;
        }

        private void Report()
        {
            rpt.Visible = true;

            Header();
            MenuAtas();
            Fill();
        }

        private void Header()
        {
            System.Text.StringBuilder x = new System.Text.StringBuilder();

            Rpt.Judul(x, comp, judul);

            if (StatusA != "")
                Rpt.SubJudul(x, "Status : " + StatusA);
            else if (StatusB != "")
                Rpt.SubJudul(x, "Status : " + StatusB);
            else
                Rpt.SubJudul(x, "Status : " + StatusS);

            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID=" + AttachmentID + "");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID=" + AttachmentID + "");

            Rpt.SubJudul(x
                , "Tanggal Kontrak" + " : " + Cf.Day(Dari) + " s/d " + Cf.Day(Sampai)
                );

            Rpt.SubJudul(x
                , "Sales : " + Agent
                );

            //Rpt.SubJudul(x
            //    , "Tipe : " + Rpt.inSql(tipe).Replace("'","")
            //    );

            //Rpt.Header(rpt, x);
            string legend = "Status: A = Aktif / B = Batal.< br /> ";
            //Rpt.HeaderReport(headReport, legend, x);
        }

        private void MenuAtas()
        {
            TableRow r = new TableRow();
            TableRow r2 = new TableRow();
            TableRow r3 = new TableRow();
            TableRow r4 = new TableRow();
            TableCell c = new TableCell();
            TableCell c2 = new TableCell();
            TableCell c3 = new TableCell();
            TableCell c4 = new TableCell();

            c = new TableCell();
            c.Text = "No";
            c.ForeColor = Color.White;
            c.Attributes["style"] = "background-color:#1E90FF";
            c.HorizontalAlign = HorizontalAlign.Left;
            c.RowSpan = 3;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "Nama";
            c.ForeColor = Color.White;
            c.Attributes["style"] = "background-color:#1E90FF";
            c.HorizontalAlign = HorizontalAlign.Left;
            c.RowSpan = 3;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "No Kontrak";
            c.ForeColor = Color.White;
            c.Attributes["style"] = "background-color:#1E90FF";
            c.HorizontalAlign = HorizontalAlign.Left;
            c.RowSpan = 3;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "Tgl Kontrak";
            c.ForeColor = Color.White;
            c.Attributes["style"] = "background-color:#1E90FF";
            c.HorizontalAlign = HorizontalAlign.Left;
            c.RowSpan = 3;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "No Unit";
            c.ForeColor = Color.White;
            c.Attributes["style"] = "background-color:#1E90FF";
            c.HorizontalAlign = HorizontalAlign.Left;
            c.RowSpan = 3;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "Nama Sales";
            c.ForeColor = Color.White;
            c.Attributes["style"] = "background-color:#1E90FF";
            c.HorizontalAlign = HorizontalAlign.Left;
            c.RowSpan = 3;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "Cara Bayar";
            c.ForeColor = Color.White;
            c.Attributes["style"] = "background-color:#1E90FF";
            c.HorizontalAlign = HorizontalAlign.Left;
            c.RowSpan = 3;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "Nilai Kontrak";
            c.ForeColor = Color.White;
            c.Attributes["style"] = "background-color:#1E90FF";
            c.HorizontalAlign = HorizontalAlign.Left;
            c.RowSpan = 3;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "DPP";
            c.ForeColor = Color.White;
            c.Attributes["style"] = "background-color:#1E90FF";
            c.HorizontalAlign = HorizontalAlign.Left;
            c.RowSpan = 3;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "Skema Komisi";
            c.ForeColor = Color.White;
            c.Attributes["style"] = "background-color:#1E90FF";
            c.HorizontalAlign = HorizontalAlign.Left;
            c.RowSpan = 3;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "Total Komisi";
            c.ForeColor = Color.White;
            c.Attributes["style"] = "background-color:#1E90FF";
            c.HorizontalAlign = HorizontalAlign.Left;
            c.RowSpan = 3;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "Termin";
            c.ForeColor = Color.White;
            c.Attributes["style"] = "background-color:#1E90FF";
            c.HorizontalAlign = HorizontalAlign.Left;
            c.RowSpan = 3;
            r.Cells.Add(c);

            int no = Db.SingleInteger("Select Max(nomor) from ref_skom_detail");
            string hasil = "SELECT Nama FROM REF_SKOM_DETAIL Where Nomor = '" + no + "'";
            DataTable hr = Db.Rs(hasil);

            if (hr.Rows.Count > 0)
            {
                c = new TableCell();
                c.Text = "Komisi";
                c.ForeColor = Color.White;
                c.Attributes["style"] = "background-color:#1E90FF";
                c.HorizontalAlign = HorizontalAlign.Center;
                c.RowSpan = 1;
                c.ColumnSpan = hr.Rows.Count * 3; //jumlah rows tipe sales
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "Closing Fee";
                c.ForeColor = Color.White;
                c.Attributes["style"] = "background-color:#1E90FF";
                c.HorizontalAlign = HorizontalAlign.Center;
                c.RowSpan = 1;
                c.ColumnSpan = hr.Rows.Count * 3; //jumlah rows tipe sales
                r.Cells.Add(c);

                for (int j = 0; j < hr.Rows.Count; j++)
                {
                    c2 = new TableCell();
                    c2.Text = hr.Rows[j]["Nama"].ToString();
                    c2.ForeColor = Color.White;
                    c2.Attributes["style"] = "background-color:#1E90FF";
                    c2.HorizontalAlign = HorizontalAlign.Center;
                    c2.ColumnSpan = 3;
                    r2.Cells.Add(c2);


                    c3 = new TableCell();
                    c3.Text = "Nilai Komisi";
                    c3.ForeColor = Color.White;
                    c3.Attributes["style"] = "background-color:#1E90FF";
                    r3.Cells.Add(c3);

                    c3 = new TableCell();
                    c3.Text = "Nilai Bayar";
                    c3.ForeColor = Color.White;
                    c3.Attributes["style"] = "background-color:#1E90FF";
                    r3.Cells.Add(c3);

                    c3 = new TableCell();
                    c3.Text = "Tanggal Bayar";
                    c3.ForeColor = Color.White;
                    c3.Attributes["style"] = "background-color:#1E90FF";
                    r3.Cells.Add(c3);
                }

                for (int g = 0; g < hr.Rows.Count; g++)
                {
                    c2 = new TableCell();
                    c2.Text = hr.Rows[g]["Nama"].ToString();
                    c2.ForeColor = Color.White;
                    c2.Attributes["style"] = "background-color:#1E90FF";
                    c2.HorizontalAlign = HorizontalAlign.Center;
                    c2.ColumnSpan = 3;
                    r2.Cells.Add(c2);

                    c3 = new TableCell();
                    c3.Text = "Nilai Closing Fee";
                    c3.ForeColor = Color.White;
                    c3.Attributes["style"] = "background-color:#1E90FF";
                    r3.Cells.Add(c3);

                    c3 = new TableCell();
                    c3.Text = "Nilai Bayar";
                    c3.ForeColor = Color.White;
                    c3.Attributes["style"] = "background-color:#1E90FF";
                    r3.Cells.Add(c3);

                    c3 = new TableCell();
                    c3.Text = "Tanggal Bayar";
                    c3.ForeColor = Color.White;
                    c3.Attributes["style"] = "background-color:#1E90FF";
                    r3.Cells.Add(c3);

                }
            }


            rpt.Rows.Add(r);
            rpt.Rows.Add(r2);
            rpt.Rows.Add(r3);
        }

        private void Fill()
        {
            string Status = "";
            if (StatusA != "") Status = " AND A.Status = 'A'";
            if (StatusB != "") Status = " AND A.Status = 'B'";

            string tgl = "";
            string order = "";

            tgl = "A.TglKontrak";
            order = ",A.NoKontrak";


            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");

            string nProject = "";
            if (Project != "SEMUA") nProject = " AND A.Project IN ('" + Project.Replace(",", "','") + "')";
            string nPerusahaan = "";
            if (Perusahaan != "SEMUA") nPerusahaan = " AND A.Pers = '" + Perusahaan + "'";

            string nAgent = "", nAgent2 = "";
            if (Agent != "SEMUA")
            {
                nAgent = " AND B.NoAgent = '" + Agent + "'";
                nAgent2 = " AND A.NoAgent = '" + Agent + "'";
            }
            else
            {
                if (UserAgent() > 0)
                    nAgent = " AND B.NoAgent = " + UserAgent();
            }

            int index = 1;

            int no = 1;

            string sql = "SELECT DISTINCT (NoAgent) from MS_KONTRAK A"
                + " where (select ISNULL(count(*),0) from MS_KOMISI where NoKontrak = A.NoKontrak) > 0" + nAgent2 + nProject + nPerusahaan;
            DataTable sr = Db.Rs(sql);
            decimal t1 = 0, t2 = 0;
            for (int g = 0; g < sr.Rows.Count; g++)
            {
                if (!Response.IsClientConnected) break;

                string strSql = "SELECT "
                    + "A.NoKontrak"
                    + ",A.TglKontrak"
                    + ",A.NilaiDPP"
                    + ",A.NoUnit"
                    + ",A.Skema"
                    + ",A.NilaiKontrak"
                    + ",B.Nama AS Ag"
                    + ",B.Principal"
                    + ",B.NPWP"
                    + ",B.Rekening"
                    + ",A.Status"
                    + ",A.PersenLunas"
                    + ",C.Nama as Customer"
                    + ",A.NoAgent"
                    + ",A.NoStock"
                    + " FROM MS_KONTRAK A INNER JOIN MS_AGENT B ON A.NoAgent = B.NoAgent"
                    + " INNER JOIN MS_CUSTOMER C ON A.NoCustomer = C.NoCustomer"
                    + " WHERE A.NoAgent= '" + sr.Rows[g]["NoAgent"] + "'"
                    + " AND A.FlagKomisi = '1'"
                    + Status
                    + " AND CONVERT(varchar,A.TglKontrak,112) >= '" + Cf.Tgl112(Dari) + "'"
                    + " AND CONVERT(varchar,A.TglKontrak,112) <= '" + Cf.Tgl112(Sampai) + "'"
                    + nAgent
                    + nProject
                    + nPerusahaan
                    + " ORDER BY B.Nama"
                    + order;



                DataTable rs = Db.Rs(strSql);
                for (int i = 0; i < rs.Rows.Count; i++)
                {
                    if (!Response.IsClientConnected) break;

                    TableRow r = new TableRow();
                    TableCell c;
                    TableRow r2a;
                    TableHeaderCell th2;
                    Table tb;

                    r.VerticalAlign = VerticalAlign.Top;
                    r.Attributes["ondblclick"] = "popJadwalKomisi('" + rs.Rows[i]["NoKontrak"] + "')";

                    //nambah no default
                    c = new TableCell();
                    c.Text = (no).ToString();
                    c.RowSpan = 4;
                    c.HorizontalAlign = HorizontalAlign.Left;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = rs.Rows[i]["Customer"].ToString();
                    c.RowSpan = 4;
                    c.HorizontalAlign = HorizontalAlign.Left;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = rs.Rows[i]["NoKontrak"].ToString();
                    string NoKontrak = rs.Rows[i]["NoKontrak"].ToString();
                    c.RowSpan = 4;
                    c.HorizontalAlign = HorizontalAlign.Left;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Cf.Day(rs.Rows[i]["TglKontrak"]);
                    c.RowSpan = 4;
                    c.HorizontalAlign = HorizontalAlign.Left;
                    r.Cells.Add(c);


                    c = new TableCell();
                    c.Text = rs.Rows[i]["NoUnit"].ToString();
                    c.RowSpan = 4;
                    c.HorizontalAlign = HorizontalAlign.Left;
                    r.Cells.Add(c);

                    string agen = Db.SingleString("Select Nama from ms_agent where NoAgent ='" + rs.Rows[i]["NoAgent"] + "'");
                    c = new TableCell();
                    c.Text = agen;
                    c.RowSpan = 4;
                    c.HorizontalAlign = HorizontalAlign.Left;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = rs.Rows[i]["Skema"].ToString();
                    c.RowSpan = 4;
                    c.HorizontalAlign = HorizontalAlign.Left;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Cf.Num(Convert.ToDecimal(rs.Rows[i]["NilaiKontrak"]));
                    c.RowSpan = 4;
                    c.HorizontalAlign = HorizontalAlign.Left;
                    r.Cells.Add(c);

                    //komisi marketing
                    c = new TableCell();
                    c.Text = Cf.Num(Math.Round(Convert.ToDecimal(rs.Rows[i]["NilaiDPP"])));
                    c.RowSpan = 4;
                    c.HorizontalAlign = HorizontalAlign.Right;
                    r.Cells.Add(c);

                    c = new TableCell();
                    string Skom = Db.SingleString("Select NamaKomisi From MS_KOMISI Where NoKontrak ='" + rs.Rows[i]["NoKontrak"].ToString() + "'");
                    c.Text = Skom;
                    c.RowSpan = 4;
                    c.HorizontalAlign = HorizontalAlign.Right;
                    r.Cells.Add(c);

                    c = new TableCell();
                    decimal NKom = Db.SingleDecimal("Select NilaiKomisi From MS_KOMISI Where NoKontrak ='" + rs.Rows[i]["NoKontrak"].ToString() + "'");
                    c.Text = Cf.Num(Math.Round(NKom));
                    c.RowSpan = 4;
                    c.HorizontalAlign = HorizontalAlign.Right;
                    r.Cells.Add(c);

                    rpt.Rows.Add(r);
                    no++;

                    t1 += NKom;
                    termin(NoKontrak);
                }

            }
            SubTotal("GRAND TOTAL", t1);

        }

        private void termin(string Nokontrak)
        {
            TableRow r2 = new TableRow();
            TableRow r3 = new TableRow();
            TableRow r4 = new TableRow();
            TableCell c2 = new TableCell();
            TableCell c3 = new TableCell();
            TableCell c4 = new TableCell();

            System.Text.StringBuilder ArrTermin = new System.Text.StringBuilder();

            int term = Db.SingleInteger("select count(distinct termin) from ms_komisi_detail where Nokontrak ='" + Nokontrak + "'");
            DataTable rj = Db.Rs("Select * from ms_komisi_detail where nokontrak ='" + Nokontrak + "' And Baris = 1");
            DataTable rj2 = Db.Rs("Select * from ms_komisi_detail where nokontrak ='" + Nokontrak + "' And Baris = 2");


            if (term == 1)
            {
                c2 = new TableCell();
                c2.Text = "TERMIN 1";
                r2.Cells.Add(c2);

                for (int g = 0; g < rj.Rows.Count; g++)
                {
                    c2 = new TableCell();
                    c2.Text = Cf.Num(rj.Rows[g]["NilaiKomisi"]);
                    r2.Cells.Add(c2);

                    c2 = new TableCell();
                    c2.Text = Cf.Num(Math.Round(Convert.ToDecimal(rj.Rows[g]["NilaiBayar"])));
                    r2.Cells.Add(c2);

                    c2 = new TableCell();
                    c2.Text = Cf.Day(rj.Rows[g]["TglBayar"]);
                    r2.Cells.Add(c2);

                }

                for (int h = 0; h < rj.Rows.Count; h++)
                {
                    c2 = new TableCell();
                    c2.Text = Cf.Num(rj.Rows[h]["ClosingFee"]);
                    r2.Cells.Add(c2);

                    c2 = new TableCell();
                    c2.Text = Cf.Num(Math.Round(Convert.ToDecimal(rj.Rows[h]["NilaiBayarCF"])));
                    r2.Cells.Add(c2);

                    c2 = new TableCell();
                    c2.Text = Cf.Day(rj.Rows[h]["TglBayarClosingfee"]);
                    r2.Cells.Add(c2);
                }
            }

            else if (term == 2)
            {
                c2 = new TableCell();
                c2.Text = "TERMIN 1";
                r2.Cells.Add(c2);

                c3 = new TableCell();
                c3.Text = "TERMIN 2";
                r3.Cells.Add(c3);


                for (int f = 0; f < rj.Rows.Count; f++)
                {
                    c2 = new TableCell();
                    c2.Text = Cf.Num(rj.Rows[f]["NilaiKomisi"]);
                    r2.Cells.Add(c2);

                    c2 = new TableCell();
                    c2.Text = Cf.Num(Math.Round(Convert.ToDecimal(rj.Rows[f]["NilaiBayar"])));
                    r2.Cells.Add(c2);

                    c2 = new TableCell();
                    c2.Text = Cf.Day(rj.Rows[f]["TglBayar"]);
                    r2.Cells.Add(c2);

                }

                //closingfee
                for (int h = 0; h < rj.Rows.Count; h++)
                {
                    c2 = new TableCell();
                    c2.Text = Cf.Num(rj.Rows[h]["ClosingFee"]);
                    c2.RowSpan = 2;
                    r2.Cells.Add(c2);

                    c2 = new TableCell();
                    c2.Text = Cf.Num(Math.Round(Convert.ToDecimal(rj.Rows[h]["NilaiBayarCF"])));
                    c2.RowSpan = 2;
                    r2.Cells.Add(c2);

                    c2 = new TableCell();
                    c2.Text = Cf.Day(rj.Rows[h]["TglBayarClosingfee"]);
                    c2.RowSpan = 2;
                    r2.Cells.Add(c2);
                }

                for (int f2 = 0; f2 < rj2.Rows.Count; f2++)
                {
                    c3 = new TableCell();
                    c3.Text = Cf.Num(rj2.Rows[f2]["NilaiKomisi"]);
                    r3.Cells.Add(c3);

                    c3 = new TableCell();
                    c3.Text = Cf.Num(Math.Round(Convert.ToDecimal(rj2.Rows[f2]["NilaiBayar"])));
                    r3.Cells.Add(c3);

                    c3 = new TableCell();
                    c3.Text = Cf.Day(rj2.Rows[f2]["TglBayar"]);
                    r3.Cells.Add(c3);

                }

            }
            else if (term == 3)
            {
                DataTable rj3 = Db.Rs("Select * from ms_komisi_detail where nokontrak ='" + Nokontrak + "' And Baris = 3");

                c2 = new TableCell();
                c2.Text = "TERMIN 1";
                r2.Cells.Add(c2);

                c3 = new TableCell();
                c3.Text = "TERMIN 2";
                r3.Cells.Add(c3);

                c4 = new TableCell();
                c4.Text = "TERMIN 3";
                r4.Cells.Add(c4);

                for (int j = 0; j < rj.Rows.Count; j++)
                {
                    c2 = new TableCell();
                    c2.Text = Cf.Num(rj.Rows[j]["NilaiKomisi"]);
                    r2.Cells.Add(c2);

                    c2 = new TableCell();
                    c2.Text = Cf.Num(Math.Round(Convert.ToDecimal(rj.Rows[j]["NilaiBayar"])));
                    r2.Cells.Add(c2);

                    c2 = new TableCell();
                    c2.Text = Cf.Day(rj.Rows[j]["TglBayar"]);
                    r2.Cells.Add(c2);
                }

                //closingfee
                for (int h = 0; h < rj.Rows.Count; h++)
                {
                    c2 = new TableCell();
                    c2.Text = Cf.Num(rj.Rows[h]["ClosingFee"]);
                    c2.RowSpan = 3;
                    r2.Cells.Add(c2);

                    c2 = new TableCell();
                    c2.Text = Cf.Num(Math.Round(Convert.ToDecimal(rj.Rows[h]["NilaiBayarCF"])));
                    c2.RowSpan = 3;
                    r2.Cells.Add(c2);

                    c2 = new TableCell();
                    c2.Text = Cf.Day(rj.Rows[h]["TglBayarClosingfee"]);
                    c2.RowSpan = 3;
                    r2.Cells.Add(c2);
                }

                for (int j2 = 0; j2 < rj2.Rows.Count; j2++)
                {
                    c3 = new TableCell();
                    c3.Text = Cf.Num(rj2.Rows[j2]["NilaiKomisi"]);
                    r3.Cells.Add(c3);

                    c3 = new TableCell();
                    c3.Text = Cf.Num(Math.Round(Convert.ToDecimal(rj2.Rows[j2]["NilaiBayar"])));
                    r3.Cells.Add(c3);

                    c3 = new TableCell();
                    c3.Text = Cf.Day(rj2.Rows[j2]["TglBayar"]);
                    r3.Cells.Add(c3);
                }

                for (int j3 = 0; j3 < rj3.Rows.Count; j3++)
                {
                    c4 = new TableCell();
                    c4.Text = Cf.Num(rj3.Rows[j3]["NilaiKomisi"]);
                    r4.Cells.Add(c4);

                    c4 = new TableCell();
                    c4.Text = Cf.Num(Math.Round(Convert.ToDecimal(rj3.Rows[j3]["NilaiBayar"])));
                    r4.Cells.Add(c4);

                    c4 = new TableCell();
                    c4.Text = Cf.Day(rj3.Rows[j3]["TglBayar"]);
                    r4.Cells.Add(c4);
                }
            }

            rpt.Rows.Add(r2);
            rpt.Rows.Add(r3);
            rpt.Rows.Add(r4);
        }
        private void SubTotal(string txt, decimal t1)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = Rpt.Foot();
            c.Text = txt;
            c.ColumnSpan = 8;
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t1);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            rpt.Rows.Add(r);
        }

        protected string TipeReff(string NoKontrak, string RefEm, string RefCust)
        {
            string Tipe = "";
            if (RefEm != "" && RefCust == "")
            {
                Tipe = "EMPLOYEE";
            }
            else if (RefEm == "" && RefCust != "")
            {
                Tipe = "BUYER";
            }

            return Tipe;
        }

        protected string NamaReff(string RefEm, string RefCust)
        {
            string Nama = "";
            if (RefEm != "" && RefCust == "")
            {
                Nama = Db.SingleString("SELECT Nama FROM MS_AGENT WHERE NoAgent = '" + RefEm + "'");
            }
            else if (RefEm == "" && RefCust != "")
            {
                Nama = Db.SingleString("SELECT Nama FROM MS_CUSTOMER WHERE NoCustomer = '" + RefCust + "'");
            }

            return Nama;
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

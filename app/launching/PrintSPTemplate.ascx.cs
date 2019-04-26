namespace ISC064.LAUNCHING
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Web.UI.WebControls;
    using System.Text.RegularExpressions;

    public partial class PrintSPTemplate : System.Web.UI.UserControl
    {
        //Passing parameter
        public string proj;
        public string nomor;

        public string NoKontrak
        {
            set { nomor = value; }
        }
        public string Project
        {
            set { proj = value; }
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Fill();
        }

        private void Fill()
        {
            string strSql = "SELECT * FROM MS_KONTRAK "
                + " WHERE NoKontrak = '" + nomor + "' and Project = '" + proj + "'"
                ;
            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count > 0)
            {
                nokontrak.Text = rs.Rows[0]["NoKontrak"].ToString();
                tglkontrak.Text = Cf.DayIndo(rs.Rows[0]["TglKontrak"]);
               // namaproject.Text = rs.Rows[0]["NamaProject"].ToString();
                namapers.Text = rs.Rows[0]["NamaPers"].ToString();
                nilaikontrak.Text = Cf.NumBulat(rs.Rows[0]["NilaiKontrak"]);
                skema.Text = Db.SingleString("select ISNULL(Nama, '') from REF_SKEMA where Nomor = '" + rs.Rows[0]["Refskema"] + "'");
                //ag.Text = Db.SingleString("SELECT NAMA FROM MS_AGENT WHERE NoAgent = " + rs.Rows[0]["NoAgent"].ToString());

                //fill data customer
                int CountCus = Db.SingleInteger("select count(*) from MS_CUSTOMER where NoCustomer = '" + rs.Rows[0]["NoCustomer"] + "'");
                if (CountCus != 0)
                {
                    string strSqlCus = "";
                    strSqlCus = "SELECT * FROM MS_CUSTOMER WHERE NoCustomer = '" + rs.Rows[0]["NoCustomer"] + "'";
                    DataTable rsCus = Db.Rs(strSqlCus);
                    for (int j = 0; j < rsCus.Rows.Count; j++)
                    {
                        namacs.Text = namacs2.Text = rsCus.Rows[j]["Nama"].ToString();
                        noktp.Text = rsCus.Rows[j]["NoKTP"].ToString();
                        npwp.Text = rsCus.Rows[j]["NPWP"].ToString();

                        alamatktp1.Text = rsCus.Rows[j]["KTP1"].ToString() + " " + rsCus.Rows[j]["KTP2"].ToString() + " " + rsCus.Rows[j]["KTP3"].ToString();
                        alamatktp2.Text = rsCus.Rows[j]["KTP4"].ToString() + " " + rsCus.Rows[j]["KTP5"].ToString();

                        alamatsekarang1.Text = rsCus.Rows[j]["Alamat1"].ToString() + " " + rsCus.Rows[j]["Alamat2"].ToString() + " " + rsCus.Rows[j]["Alamat3"].ToString();
                        alamatsekarang2.Text = rsCus.Rows[j]["Alamat4"].ToString() + " " + rsCus.Rows[j]["Alamat5"].ToString();

                        hp1.Text = rsCus.Rows[j]["NoHP"].ToString();
                        hp2.Text = rsCus.Rows[j]["NoHP2"].ToString();
                       // email.Text = rsCus.Rows[j]["Email"].ToString();
                    }
                }

                //fill data unit
                int CountUnit = Db.SingleInteger("select count(*) from MS_UNIT where NoStock = '" + rs.Rows[0]["NoStock"] + "'");
                if (CountUnit != 0)
                {
                    string strSqlUnit = "";
                    strSqlUnit = "SELECT * FROM MS_UNIT WHERE NoStock = '" + rs.Rows[0]["NoStock"] + "'";
                    DataTable rsUnit = Db.Rs(strSqlUnit);
                    for (int k = 0; k < rsUnit.Rows.Count; k++)
                    {
                     //   jenisproperti.Text = rsUnit.Rows[k]["JenisProperti"].ToString();
                        namajalan.Text = rsUnit.Rows[k]["NamaJalan"].ToString();
                        nounit.Text = rsUnit.Rows[k]["Nomor"].ToString().PadLeft(2, '0');
                        jenis.Text = rsUnit.Rows[k]["Jenis"].ToString();
                        luasnett.Text = Cf.Num(rsUnit.Rows[k]["LuasNett"]);
                        luassg.Text = Cf.Num(rsUnit.Rows[k]["LuasSG"]);
                        lokasi.Text = rsUnit.Rows[k]["Lokasi"].ToString();
                    }
                }

                //gimmick
                int CountGimmick = Db.SingleInteger("select count(*) from MS_KONTRAK_GIMMICK where NoKontrak = '" + nomor + "'");
                if (CountGimmick != 0)
                {
                  //  gimmicktr.Visible = true;
                    FillTb();
                }
                else
                {
                    //gimmicktr.Visible = false;
                }
            }
        }

        protected void FillTb()
        {
            string strSql = "SELECT * "
                + " FROM MS_KONTRAK_GIMMICK"
                + " WHERE NoKontrak = '" + nomor + "'";

            DataTable rs = Db.Rs(strSql);
           // Rpt.NoData(rpt, rs, "Daftar tagihan untuk kontrak tersebut masih kosong.");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;
                
                c = new TableCell();
                c.Text = "- " + rs.Rows[i]["Nama"].ToString() + " " + Cf.Num(rs.Rows[i]["Stock"]) + " " + rs.Rows[i]["Satuan"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Attributes["style"] = "font-size:11pt;font-family:'Times New Roman', Times, serif;";
                r.Cells.Add(c);

                Rpt.BorderNoList(r);
             //   rpt.Rows.Add(r);
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
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion
    }
}

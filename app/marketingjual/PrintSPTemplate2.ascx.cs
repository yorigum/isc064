namespace ISC064.MARKETINGJUAL
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Web.UI.WebControls;
    using System.Text.RegularExpressions;

    public partial class PrintSPTemplate2 : System.Web.UI.UserControl
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
                NoKontrak1.Text = rs.Rows[0]["NoKontrak"].ToString();

                hargajual1.Text = Cf.NumBulat(rs.Rows[0]["Gross"]);
                hargapengikat1.Text = Cf.NumBulat(rs.Rows[0]["NilaiKontrak"]);
                ppn1.Text = Cf.NumBulat(rs.Rows[0]["NilaiPPN"]);
                terbilang.Text = Money.Str(Convert.ToDecimal(rs.Rows[0]["NilaiKontrak"])) + " RUPIAH";
                skema.Text = Db.SingleString("select ISNULL(Nama, '') from REF_SKEMA where Nomor = '" + rs.Rows[0]["Refskema"] + "'");
                sales.Text = Db.SingleString("SELECT NAMA FROM MS_AGENT WHERE NoAgent = " + rs.Rows[0]["NoAgent"].ToString());
                //vabca.Text = rs.Rows[0]["NoVA"].ToString();
                string Nova = rs.Rows[0]["NoVA"].ToString();
                string KodeProVA = Nova.Substring(0, 5);
                string TahunBulanVA = Nova.Substring(5, 4);
                string TipeVA = Nova.Substring(9, 3);
                string NomorVA = Nova.Substring(12, 4);
                vabca.Text = KodeProVA + "-" + TahunBulanVA + "-" + TipeVA + "-" + NomorVA;
                tglsp.Text = Cf.Day(Convert.ToDateTime(rs.Rows[0]["TglKontrak"]));

                DataTable rs1 = Db.Rs("SELECT * FROM MS_UNIT WHERE NoUnit='" + rs.Rows[0]["NoUnit"].ToString() + "'");
                string Lokasi = "";
                if (rs1.Rows.Count > 0)
                {
                    string tipe1 = Db.SingleString("select jenis from ms_unit where nounit = '" + rs.Rows[0]["NoUnit"].ToString() + "'");
                    string tipe2 = Db.SingleString("select Nama from Ref_jenis where jenis = '" + tipe1 + "'");
                    tipeunit.Text = tipe2;
                    luassg.Text = Cf.Num(rs1.Rows[0]["LuasSG"]) + " M<sup>2</sup>";
                    luasnett.Text = Cf.Num(rs1.Rows[0]["LuasNett"]) + " M<sup>2</sup>";
                    lantai.Text = rs1.Rows[0]["Lantai"].ToString();
                    nounit.Text = rs1.Rows[0]["Nomor"].ToString();

                }
                tower.Text = "Paul Marc";// rs.Rows[0]["Lokasi"].ToString();


                ////fill data customer
                int CountCus = Db.SingleInteger("select count(*) from MS_CUSTOMER where NoCustomer = '" + rs.Rows[0]["NoCustomer"] + "'");
                if (CountCus != 0)
                {
                    string strSqlCus = "";
                    strSqlCus = "SELECT * FROM MS_CUSTOMER WHERE NoCustomer = '" + rs.Rows[0]["NoCustomer"] + "'";
                    DataTable rsCus = Db.Rs(strSqlCus);
                    for (int j = 0; j < rsCus.Rows.Count; j++)
                    {
                        namacs.Text = cs2.Text = rsCus.Rows[j]["Nama"].ToString();
                        noktp.Text = rsCus.Rows[j]["NoKTP"].ToString();
                        nonpwp.Text = rsCus.Rows[j]["NPWP"].ToString();

                        Almt.Text = rsCus.Rows[j]["KTP1"].ToString();
                        Almt2.Text = rsCus.Rows[j]["KTP2"].ToString();
                        Almt3.Text = rsCus.Rows[j]["KTP3"].ToString();
                        Almt4.Text = rsCus.Rows[j]["KTP4"].ToString() + "," + rsCus.Rows[j]["KTP5"].ToString();

                        AlmtSrt.Text = rsCus.Rows[j]["Alamat1"].ToString();
                        AlmtSrt2.Text = rsCus.Rows[j]["Alamat2"].ToString();
                        AlmtSrt3.Text = rsCus.Rows[j]["Alamat3"].ToString();
                        AlmtSrt4.Text = rsCus.Rows[j]["Alamat4"].ToString() + " " + rsCus.Rows[j]["Alamat5"].ToString();

                        hp.Text = rsCus.Rows[j]["NoHP"].ToString();
                        hpkerabat.Text = rsCus.Rows[j]["NoHPKerabat"].ToString();
                        email.Text = rsCus.Rows[j]["Email"].ToString();
                        telp.Text = rsCus.Rows[j]["NoTelp"].ToString();
                        NoKantor1.Text = rsCus.Rows[j]["NoKantor"].ToString();

                    }
                }

                ////fill data unit
                //int CountUnit = Db.SingleInteger("select count(*) from MS_UNIT where NoStock = '" + rs.Rows[0]["NoStock"] + "'");
                //if (CountUnit != 0)
                //{
                //    string strSqlUnit = "";
                //    strSqlUnit = "SELECT * FROM MS_UNIT WHERE NoStock = '" + rs.Rows[0]["NoStock"] + "'";
                //    DataTable rsUnit = Db.Rs(strSqlUnit);
                //    for (int k = 0; k < rsUnit.Rows.Count; k++)
                //    {
                //        jenisproperti.Text = rsUnit.Rows[k]["JenisProperti"].ToString();
                //        namajalan.Text = rsUnit.Rows[k]["NamaJalan"].ToString();
                //        nounit.Text = rsUnit.Rows[k]["Nomor"].ToString().PadLeft(2, '0');
                //        jenis.Text = rsUnit.Rows[k]["Jenis"].ToString();
                //        luasnett.Text = Cf.Num(rsUnit.Rows[k]["LuasNett"]);
                //        luassg.Text = Cf.Num(rsUnit.Rows[k]["LuasSG"]);
                //        lokasi.Text = rsUnit.Rows[k]["Lokasi"].ToString();
                //    }
                //}

                ////gimmick
                //int CountGimmick = Db.SingleInteger("select count(*) from MS_KONTRAK_GIMMICK where NoKontrak = '" + nomor + "'");
                //if (CountGimmick != 0)
                //{
                //    gimmicktr.Visible = true;
                //    FillTb();
                //}
                //else
                //{
                //    gimmicktr.Visible = false;
                //}
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

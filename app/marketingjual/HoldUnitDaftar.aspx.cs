//===========================================================================
// This file was modified as part of an ASP.NET 2.0 Web project conversion.
// The class name was changed and the class modified to inherit from the abstract base class 
//// in file 'App_Code\Migrated\Stub_TabelStok2_aspx_cs.cs'.
// During runtime, this allows other classes in your web application to bind and access 
// the code-behind page using the abstract base class.
// The associated content page 'TabelStok2.aspx' was also modified to refer to the new class name.
// For more information on this code pattern, please refer to http://go.microsoft.com/fwlink/?LinkId=46995 
//===========================================================================
using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
    public partial class HoldUnitDaftar : System.Web.UI.Page
    {
        string JenisProperti;
        protected void Page_Load(object sender, System.EventArgs e)
        {

            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {

                InitForm();
                if (Request.QueryString["NoUnit"] != null)
                {
                    unit.Text = Request.QueryString["NoUnit"];
                    nostock.Text = NoStock;
                }
                unit.Text = Db.SingleString(
                            "SELECT NoUnit FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");

                tglhold.Text = Cf.Day(DateTime.Today);
                tglholdexp.Text = Cf.Date(DateTime.Now.AddMonths(1));
            }

        }


        private void InitForm()
        {
            //Sales
            DataTable rs = Db.Rs("SELECT Nama,Principal,NoAgent FROM MS_AGENT WHERE Status = 'A'"
                + " ORDER BY Nama,NoAgent");
            if (rs.Rows.Count == 0)
            {
                ok.Enabled = false;
                agentc.Text = "AGENT TIDAK TERDAFTAR";
            }
            else
            {
                for (int i = 0; i < rs.Rows.Count; i++)
                {
                    string v = rs.Rows[i]["NoAgent"].ToString();
                    string t = rs.Rows[i]["Nama"].ToString();
                    if (rs.Rows[i]["Principal"].ToString() != "")
                        t = t + " (" + rs.Rows[i]["Principal"] + ")";
                    agent.Items.Add(new ListItem(t, v));
                }
                agent.Enabled = true;
            }

            //Mengisi Lokasi
            rs = Db.Rs("SELECT * FROM REF_LOKASI_KONTRAK ORDER BY Lokasi ASC");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["SN"].ToString();
                string t = rs.Rows[i]["Nama"].ToString();
                lokasikontrak.Items.Add(new ListItem(t, v));
            }

        }

        private bool unitvalid()
        {
            if (NoStock == "")
                return false;
            else
            {
                string Status = Db.SingleString("SELECT Status FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");
                if (Status != "A")
                    return false;
                else
                    return true;
            }
        }

        private bool valid()
        {
            bool x = true;
            string s = "";

            if (Cf.isEmpty(nama))
            {
                x = false;
                if (s == "") s = nama.ID;
                namac.Text = "Kosong";
            }
            else
                namac.Text = "";

            if (Cf.isEmpty(hp))
            {
                x = false;
                if (s == "") s = hp.ID;
                telpc.Text = "Kosong";
            }
            else
                telpc.Text = "";

            if (!unitvalid())
            {
                x = false;
                if (s == "") s = unit.ID;
                unitc.Text = "Tidak Available";
            }
            else
                unitc.Text = "";


            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Format Tanggal : Bulan / Tanggal / Tahun.\\n"
                    + "2. Nama tidak boleh kosong.\\n"
                    + "3. No. Telpon dan HP tidak boleh kosong.\\n"
                    + "4. Unit yang dipesan harus available dan tidak boleh kosong.\\n"
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );

            return x;
        }


        private void AutoID()
        {
            DateTime TglHold = Convert.ToDateTime(tglhold.Text);
            int c = Db.SingleInteger("SELECT COUNT(NoHold) FROM MS_HOLD");

            bool hasfound = false;
            while (!hasfound)
            {
                if (!Response.IsClientConnected) break;

                c++;
                nohold.Text = c.ToString().PadLeft(5, '0');

                if (isUnique()) hasfound = true;
            }
        }

        private bool isUnique()
        {
            int c = Db.SingleInteger(
                "SELECT COUNT(NoHold) FROM MS_HOLD WHERE NoHold = '" + NoHOLD + "'");

            if (c == 0)
                return true;
            else
                return false;
        }

        protected void ok_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                AutoID();

                string LokKontrak = lokasikontrak.SelectedValue;

                int NoAgent = Convert.ToInt16(agent.SelectedValue);
                DateTime TglHold = Convert.ToDateTime(tglhold.Text);
                DateTime TglHoldExp = Convert.ToDateTime(tglholdexp.Text);
                string Nama = Cf.Str(nama.Text);
                string KTP1 = Cf.Str(ktp1.Text);
                string KTP2 = Cf.Str(ktp2.Text);
                string KTP3 = Cf.Str(ktp3.Text);
                string KTP4 = Cf.Str(ktp4.Text);
                string NoKTP = Cf.Str(noktp.Text);
                string NoTelp = Cf.Str(telp.Text);
                string NoHp = Cf.Str(hp.Text);

                string SumberData = Cf.Str(sumberdata.SelectedValue);

                string TipeCs = "";
                if (perorangan.Checked) TipeCs = "PERORANGAN";
                if (badanhukum.Checked) TipeCs = "BADAN HUKUM";

                string wn = "";
                if (wni.Checked) wn = "WNI";
                if (wna.Checked) wn = "WNA";
                if (kori.Checked) wn = "KORPORASI INDONESIA";
                if (kora.Checked) wn = "KORPORASI ASING";

                Db.Execute("UPDATE MS_UNIT SET Status='H' WHERE NoStock='" + NoStock + "'");


                //InsertCustomer willy
                Db.Execute("EXEC spCustomerDaftarHOLD"
                      + " '" + Nama + "'"
                      + ",'" + NoTelp + "'"
                      + ",'" + NoHp + "'"
                      + ",'" + NoKTP + "'"
                      + ",'" + KTP1 + "'"
                      + ",'" + KTP2 + "'"
                      + ",'" + KTP3 + "'"
                      + ",'" + KTP4 + "'"
                      + ",'" + TipeCs + "'"
                      + ",'" + wn + "'"
                      + ",'A'"
              );

                int NoCustomer = Db.SingleInteger(
                "SELECT TOP 1 NoCustomer FROM MS_CUSTOMER ORDER BY NoCustomer DESC");

                LogCs(NoCustomer);

                Db.Execute("EXEC spHoldDaftar"
                    + " '" + NoHOLD + "'"
                    + ",'" + TglHold + "'"
                    + ",'" + TglHoldExp + "'"
                    + ",'" + NoStock + "'"
                    + ",'" + NoCustomer + "'"
                    + ",'" + NoAgent + "'"
                    );

                Db.Execute("UPDATE MS_HOLD SET LokasiKontrak = '" + LokKontrak + "' WHERE NoHold='" + NoHOLD + "'");

                DataTable rs = Db.Rs("SELECT A.NoHold,A.TglHold,A.TglHoldExpired,B.Nama,C.Nama,D.NoUnit FROM MS_HOLD A"
                        + " INNER JOIN MS_CUSTOMER B ON A.NoCustomer = B.NoCustomer"
                        + " INNER JOIN MS_AGENT C ON A.NoAgent = C.NoAgent"
                        + " INNER JOIN MS_UNIT D ON A.NoStock = D.NoStock WHERE A.NoHold='" + NoHOLD + "'"
                    );

                string Ket = Cf.LogCapture(rs);

                Db.Execute("EXEC spLogHold"
                    + " 'DAFTAR'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + Ket + "'"
                    + ",'" + NoHOLD + "'"
                    );

                Response.Redirect("HoldUnitDaftarDone.aspx?NoHold=" + NoHOLD);
            }
        }

        private void LogCs(int NoCustomer)
        {

            DataTable rs = Db.Rs("SELECT "
                + " NoCustomer AS [No. Customer]"
                + ",TipeCs AS [Tipe]"
                + ",Nama AS [Nama Lengkap]"
                + ",NoHP AS [No. HP]"
                + ",NoTelp AS [No. Telp]"
                + ",NoKTP AS [No. KTP]"
                + ",KTP1 AS [KTP Alamat]"
                + ",KTP2 AS [KTP RT/RW]"
                + ",KTP3 AS [KTP Kecamatan]"
                + ",KTP4 AS [KTP Kotamadya]"
                + " FROM MS_CUSTOMER"
                + " WHERE NoCustomer = " + NoCustomer
                );

            Db.Execute("EXEC spLogCustomer"
                + " 'DAFTAR'"
                + ",'" + Act.UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'" + Cf.LogCapture(rs) + "'"
                + ",'" + NoCustomer.ToString().PadLeft(5, '0') + "'"
                );
        }



        private string NoHOLD
        {
            get
            {
                return Cf.Pk(nohold.Text);
            }
        }


        private string NoStock
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoStock"]);
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


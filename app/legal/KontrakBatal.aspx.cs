using System;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LEGAL
{
    public partial class KontrakBatal : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                Js.Focus(this, nokontrak);
                nokontrak.Attributes["ondblclick"] = "popDaftarKontrak('a');";
                frm.Visible = false;

                nodel.Visible = false;
            }

            FeedBack();
            if (frm.Visible) Js.Confirm(this, "Lanjutkan proses pembatalan kontrak?\\nStatus kontrak tersebut akan menjadi BATAL.");
        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "<a href=\"javascript:popEditKontrak('" + Request.QueryString["done"] + "')\">"
                        + "Pembatalan Berhasil..."
                        + "</a>";

                //if(Request.QueryString["klaim"] != null)
                //{
                //    this.RegisterStartupScript(
                //        "klaimScript"
                //        , "<script language='javascript' type='text/javascript'>"
                //        + "openPopUp('/marketingjual/PrintFBatal.aspx?NoKontrak=" + Cf.Pk(Request.QueryString["klaim"]) + "', '920', '650')"
                //        + "</script>"
                //        );
                //}
            }
        }

        private bool valid()
        {
            bool x = true;

            int c = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "' AND Status = 'A'");

            if (c == 0)
                x = false;

            
            if (!x)
                Js.Alert(
                    this
                    , "Kontrak Tidak Valid.\\n\\n"
                    + "Kemungkinan Sebab :\\n"
                    + "1. Kontrak tersebut tidak terdaftar.\\n"
                    + "2. Kontrak tersebut sudah dibatalkan.\\n"
                    , "document.getElementById('nokontrak').focus();"
                    + "document.getElementById('nokontrak').select();"
                    );

            return x;
        }

        protected void next_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                pilih.Visible = false;
                frm.Visible = true;

                Fill();

                Js.Confirm(this, "Lanjutkan proses pembatalan kontrak?\\nStatus kontrak tersebut akan menjadi BATAL.");
            }
        }

        private void Fill()
        {
            nilaibiaya.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            nilaibiaya.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            nilaibiaya.Attributes["onblur"] = "CalcBlur(this);";

            nilaiklaim.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            nilaiklaim.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            nilaiklaim.Attributes["onblur"] = "CalcBlur(this);";

            totalPelunasan.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            totalPelunasan.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            totalPelunasan.Attributes["onblur"] = "CalcBlur(this);";

            Func.KontrakHeader(NoKontrak, nokontrakl, unit, customer, agent);

            DataTable rs = Db.Rs("SELECT * FROM ISC064_FINANCEAR..REF_ACC");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["Acc"].ToString();
                string t = v + " : " + rs.Rows[i]["Bank"] + " " + rs.Rows[i]["Rekening"];
                acc.Items.Add(new ListItem(t, v));
            }

            if (Func.CekAkunting(NoKontrak))
                warning.Text = "Transaksi sudah pernah diposting ke Akunting";
            else
                warning.Text = "";

            decimal TotalLunas = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan), 0)"
                       + " FROM MS_PELUNASAN"
                       + " WHERE NoKontrak = '" + NoKontrak + "'"
                       + " AND SudahCair = 1"
                       );

            int BF = Db.SingleInteger("SELECT ISNULL(SUM(NoUrut), 0)"
                       + " FROM MS_PELUNASAN"
                       + " WHERE NoKontrak = '" + NoKontrak + "'"
                       + " AND SudahCair = 1"
                       );


            totalPelunasan.Text = Cf.Num(TotalLunas);
            if (BF == 1) { nilaiklaim.Text = Cf.Num(TotalLunas); }
            else { nilaiklaim.Text = Cf.Num(0); }
        }

        private bool datavalid()
        {
            bool x = true;
            string s = "";

            if (!Cf.isTgl(tgl))
            {
                x = false;
                if (s == "") s = tgl.ID;
                tglc.Text = "Tanggal";
            }
            else
                tglc.Text = "";

            if (!Cf.isTgl(tglkembali))
            {
                x = false;
                if (s == "") s = tglkembali.ID;
                tglkembalic.Text = "Tanggal";
            }
            else
                tglkembalic.Text = "";

            //if(!Cf.isMoney(nilaibiaya))
            //{
            //    x = false;
            //    if(s=="") s = nilaibiaya.ID;
            //    nilaibiayac.Text = "Angka";
            //}
            //else
            //    nilaibiayac.Text = "";
            if (Convert.ToDecimal(totalPengembalian.Text) != Convert.ToDecimal(totalPelunasan.Text))
            {
                if (Convert.ToDecimal(nilaiklaim.Text) == Convert.ToDecimal(0))
                    x = false;
            }


            if (!Cf.isMoney(nilaiklaim))
            {
                x = false;
                if (s == "") s = nilaiklaim.ID;
                nilaiklaimc.Text = "Angka";
            }
            else
                nilaiklaimc.Text = "";

            if (!Cf.isMoney(totalPengembalian))
            {
                x = false;
                if (s == "") s = totalPengembalian.ID;
                totalpengembalianc.Text = "Angka";
            }
            else
                totalpengembalianc.Text = "";

            //if(acc.SelectedIndex == 0)
            //{
            //    x = false;

            //    if(s == "")
            //        s = acc.ID;

            //    accerr.Text = "Harus dipilih";
            //}
            //else
            //    accerr.Text = "";

            //if (!Cf.isTgl(tglKuasa))
            //{
            //    x = false;
            //    tglKuasac.Text = "Tanggal";
            //}
            //else
            //{
            //    tglKuasac.Text = "";
            //}


            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Kemungkinan Sebab :\\n"
                    + "1. Biaya Administrasi harus berupa angka.\\n"
                    + "2. Nilai Klaim harus berupa angka.\\n"
                    //+ "3. Rekening Pembatalan harus dipilih."
                    + "3. Tanggal Pengembalian salah format / kosong.\\n"
                    , "document.getElementById('" + s + "').focus();"
                    );

            return x;
        }

        protected void totalklaim(object sender, EventArgs e)
        {
            try
            {
                decimal Klaim = Convert.ToDecimal(totalPelunasan.Text) - Convert.ToDecimal(totalPengembalian.Text);
                nilaiklaim.Text = Cf.Num(Klaim);
                totalPengembalian.Text = Cf.Num(Convert.ToDecimal(totalPengembalian.Text));
            }
            catch
            {

            }
        }

        protected void save_Click(object sender, System.EventArgs e)
		{
			if(datavalid())
			{
                string AlasanBatal = Cf.Str(alasan.SelectedItem.Text);

				// Update Manual 1
                decimal NilaiKlaim = Convert.ToDecimal(nilaiklaim.Text);
                decimal NilaiPengembalian = Convert.ToDecimal(totalPengembalian.Text);
                decimal Biaya = Convert.ToDecimal(nilaibiaya.Text);

				decimal TotalLunas = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiPelunasan), 0)"
					+ " FROM MS_PELUNASAN"
					+ " WHERE NoKontrak = '" + NoKontrak + "'"
					+ " AND SudahCair = 1"
					);
				
				string tanggal;
				if (tgl.Text != "")
					tanggal = ", TglBatal = '" + Convert.ToDateTime(tgl.Text) + "'";
				else
					tanggal = ", TglBatal = '" + DateTime.Today + "'";

                DateTime TglKembali = Convert.ToDateTime(tglkembali.Text);

				Db.Execute("UPDATE MS_KONTRAK"
					+ " SET AlasanBatal ='" + AlasanBatal + "'"
                    + ", BiayaBatal = " + Biaya
                    + ", NilaiKlaim = " + NilaiKlaim
					+ tanggal
					+ ", TotalLunasBatal = " + TotalLunas
                    + ", TglKembali = '" + TglKembali + "'"
					+ ", NilaiPulang = " + NilaiPengembalian
					+ ", AccBatal = '" + acc.SelectedValue + "'"
                    + ", ApprovalBatal = 1"
                    + ", KetAlasanBatal = '" + ketalasan.Text + "'"
					+ " WHERE NoKontrak = '" + NoKontrak + "'"
					);

                Db.Execute("EXEC spLogKontrak "
                        + " 'BATAL'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",''"
                        + ",'" + NoKontrak + "'"
                        );

                

                Response.Redirect("KontrakBatal.aspx?done=" + NoKontrak + "&klaim=" + NoKontrak);
			}
		}

        private string NoKontrak
        {
            get
            {
                return Cf.Pk(nokontrak.Text);
            }
        }

        //Common Driver
        protected void Execute(string strSql, string Cnn)
        {
            SqlConnection sqlCnn = new SqlConnection(Cnn);
            SqlCommand sqlCmd = new SqlCommand(strSql, sqlCnn);
            sqlCnn.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCnn.Close();
        }
        protected DataTable Rs(string strSql, string Cnn)
        {
            SqlConnection sqlCnn = new SqlConnection(Cnn);
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(strSql, sqlCnn);
            DataSet objDS = new DataSet();
            sqlAdapter.Fill(objDS, "data");
            sqlCnn.Close();

            DataTable rs = new DataTable();
            rs = objDS.Tables["data"];

            return rs;
        }
        protected string SingleString(string strSql, string Cnn)
        {
            SqlConnection sqlCnn = new SqlConnection(Cnn);
            SqlCommand sqlCmd = new SqlCommand(strSql, sqlCnn);
            sqlCnn.Open();
            string x = "";
            x = (string)sqlCmd.ExecuteScalar();
            sqlCnn.Close();

            return x;
        }
        protected int SingleInteger(string strSql, string Cnn)
        {
            SqlConnection sqlCnn = new SqlConnection(Cnn);
            SqlCommand sqlCmd = new SqlCommand(strSql, sqlCnn);
            sqlCnn.Open();
            int x = (int)sqlCmd.ExecuteScalar();
            sqlCnn.Close();

            return x;
        }
        protected long SingleLong(string strSql, string Cnn)
        {
            SqlConnection sqlCnn = new SqlConnection(Cnn);
            SqlCommand sqlCmd = new SqlCommand(strSql, sqlCnn);
            sqlCnn.Open();
            long x = Convert.ToInt64(sqlCmd.ExecuteScalar());
            sqlCnn.Close();

            return x;
        }
        protected decimal SingleDecimal(string strSql, string Cnn)
        {
            SqlConnection sqlCnn = new SqlConnection(Cnn);
            SqlCommand sqlCmd = new SqlCommand(strSql, sqlCnn);
            sqlCnn.Open();
            decimal x = Convert.ToDecimal(sqlCmd.ExecuteScalar());
            sqlCnn.Close();

            return x;
        }
        protected bool SingleBool(string strSql, string Cnn)
        {
            SqlConnection sqlCnn = new SqlConnection(Cnn);
            SqlCommand sqlCmd = new SqlCommand(strSql, sqlCnn);
            sqlCnn.Open();
            bool x = (bool)sqlCmd.ExecuteScalar();
            sqlCnn.Close();

            return x;
        }
        protected byte SingleByte(string strSql, string Cnn)
        {
            SqlConnection sqlCnn = new SqlConnection(Cnn);
            SqlCommand sqlCmd = new SqlCommand(strSql, sqlCnn);
            sqlCnn.Open();
            byte x = (byte)sqlCmd.ExecuteScalar();
            sqlCnn.Close();

            return x;
        }
        protected DateTime SingleTime(string strSql, string Cnn)
        {
            SqlConnection sqlCnn = new SqlConnection(Cnn);
            SqlCommand sqlCmd = new SqlCommand(strSql, sqlCnn);
            sqlCnn.Open();
            DateTime x = (DateTime)sqlCmd.ExecuteScalar();
            sqlCnn.Close();

            return x;
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
        protected void alasan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (alasan.SelectedItem.ToString() == "LAINNYA")
            {
                lain.Visible = true;
            }
            else
            {
                lain.Visible = false;
            }
        }
}
}

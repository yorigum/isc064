using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.NUP
{
    public partial class NUPDaftarCustomer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FeedBack();
            if (!Page.IsPostBack)
            {
                Act.ProjectList(project);
                InitForm();
            }
            btnpop.Attributes.Add("modal-url", "DaftarCustomer2.aspx?status=a&project=" + project.SelectedValue);
            Js.Confirm(this, "Apakah Anda yakin?");
        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "<a href=\"javascript:popEditCustomer('" + Request.QueryString["done"] + "')\">"
                        + "Registrasi Berhasil..."
                        + "</a>";
            }
        }

        private bool valid()
        {
            string s = "";
            bool x = true;

            x = Cf.ValidMandatory(this, "Customer", project.SelectedValue);

            //bank
            if (Cf.isEmpty(bank))
            {
                x = false;
                if (s == "") s = bank.ID;
                bankc.Text = "Kosong";
            }
            else
                bankc.Text = "";

            //cabang
            if (Cf.isEmpty(cabang))
            {
                x = false;
                if (s == "") s = cabang.ID;
                cabangc.Text = "Kosong";
            }
            else
                cabangc.Text = "";

            //no rek
            if (Cf.isEmpty(rek))
            {
                x = false;
                if (s == "") s = rek.ID;
                rekc.Text = "Kosong";
            }
            else
                rekc.Text = "";

            //nama rek
            if (Cf.isEmpty(reknama))
            {
                x = false;
                if (s == "") s = reknama.ID;
                reknamac.Text = "Kosong";
            }
            else
                reknamac.Text = "";

            if (npwp.Text.Length < 15)
            {
                x = false;
                if (s == "") s = npwp.ID;
                npwpc.Text = "Harus 15 Digit";
            }
            else
                npwpc.Text = "";

            //telp
            if (Cf.isEmpty(ctelp))
            {
                x = false;
                if (s == "") s = ctelp.ID;
                ctelpc.Text = "Harus isi";
            }
            else
                ctelpc.Text = "";

            //hp
            if (Cf.isEmpty(chp))
            {
                x = false;
                if (s == "") s = chp.ID;
                chpc.Text = "Harus isi";
            }
            else
                chpc.Text = "";

            //agent
            if (agent.SelectedValue == "0")
            {
                x = false;
                if (s == "") s = agent.ID;
                agentc.Text = "Pilih Agent";
            }
            else
                agentc.Text = "";

            if (jenisproperti.SelectedIndex == 0)
            {
                x = false;
                if (s == "") s = jenisproperti.ID;
                jenispropertic.Text = "Pilih Jenis Properti";
            }
            else
                jenispropertic.Text = "";

            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Masukkan data yang diperlukan.\\n"
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );

            return x;
        }

        private void InitForm()
        {
            //willy
            //biar gak milih2.

            project.SelectedValue = "SVS";
            
            //Sales
            DataTable rs = Db.Rs("SELECT Nama,Principal,NoAgent FROM MS_AGENT WHERE Status = 'A' AND Project = '" + project.SelectedValue + "'"
                + " ORDER BY Nama,NoAgent");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["NoAgent"].ToString();
                string t = rs.Rows[i]["Nama"].ToString();
                //if (rs.Rows[i]["Principal"].ToString() != "")
                //    t = t + " (" + rs.Rows[i]["Principal"] + ")";
                agent.Items.Add(new ListItem(t, v));
            }
            DataTable rsJenis = Db.Rs("SELECT * FROM REF_JENISPROPERTI WHERE Project = '" + project.SelectedValue + "'");
            for (int a = 0; a < rsJenis.Rows.Count; a++)
            {
                string t = rsJenis.Rows[a]["Nama"].ToString();
                jenisproperti.Items.Add(new ListItem(t));
            }

        }

        private string AutoID()
        {
            string strLastNo = Db.SingleString("SELECT TOP 1 NoNUP FROM MS_NUP WHERE Tipe = '" + jenisproperti.SelectedValue + "' AND Project = '" + project.SelectedValue + "' ORDER BY NoNUP DESC");

            int intLastNo = Convert.ToInt32(strLastNo);

            int noBaru = intLastNo + 1;
            string NUP = noBaru.ToString().PadLeft(4, '0');

            return NUP;
        }

        protected void save_Click(object sender, System.EventArgs e)
        {

            if (valid())
            {
                string Nama = Cf.Str(nama.Text);
                string NoHP = Cf.Str(chp.Text);
                string NoTelp = Cf.Str(ctelp.Text);
                string NoKTP = Cf.Str(noktp.Text);
                string KTP1 = Cf.Str(ktp1.Text);
                string KTP2 = Cf.Str(ktp2.Text);
                string KTP3 = Cf.Str(ktp3.Text);
                string KTP4 = Cf.Str(ktp4.Text);
                string Kor1 = Cf.Str(Korespon1.Text);
                string Kor2 = Cf.Str(Korespon2.Text);
                string Kor3 = Cf.Str(Korespon3.Text);
                string Kor4 = Cf.Str(Korespon4.Text);
                string Email = Cf.Str(email.Text);

                string rekN = Cf.Str(rek.Text);
                string rekNam = Cf.Str(reknama.Text);
                string rekB = Cf.Str(bank.Text);
                string rekC = Cf.Str(cabang.Text);
                int NoAgent = Convert.ToInt32(agent.SelectedValue);

                int noCust = Db.SingleInteger("SELECT NoCustomer FROM MS_CUSTOMER ORDER BY NoCustomer DESC");
                noCust++;

                if (NoCustomer == "#AUTO#")
                {
                    Db.Execute("EXEC spCustomerDaftarBSA"
                    + " " + noCust
                    + ",'" + Nama + "'"
                    + ",'" + NoHP + "'"
                    + ",'" + NoTelp + "'"
                    + ",'" + NoKTP + "'"
                    + ",'" + KTP1 + "'"
                    + ",'" + KTP2 + "'"
                    + ",'" + KTP3 + "'"
                    + ",'" + KTP4 + "'"
                    + ",'" + Kor1 + "'"
                    + ",'" + Kor2 + "'"
                    + ",'" + Kor3 + "'"
                    + ",'" + Kor4 + "'"
                    + ",'" + rekNam + "'"
                    + ",'" + rekB + "'"
                    + ",'" + rekC + "'"
                    + ",'" + rekN + "'"
                    );

                    //get nomor customer terbaru
                    noCust = Db.SingleInteger("SELECT TOP 1 NoCustomer FROM MS_CUSTOMER ORDER BY NoCustomer DESC");
                }
                else
                    noCust = Convert.ToInt32(NoCustomer);
                
                string noBaruNUP = AutoID();
                DateTime TglDaftar = DateTime.Now.Date;

                Db.Execute("UPDATE MS_CUSTOMER SET Email = '" + email.Text + "', Project = '" + project.SelectedValue + "', NoNUP = '" + noBaruNUP +"' WHERE NoCustomer = '" + noCust + "'");

                //Insert ke MS_NUP
                Db.Execute("EXEC spNUPDaftar "
                    + "'" + noBaruNUP + "'"
                    + "," + noCust
                    + "," + NoAgent
                    + ",'" + jenisproperti.SelectedValue + "'"
                    + ",'" + project.SelectedValue + "'"
                    );

                Db.Execute("UPDATE MS_NUP SET "
                    + " TglDaftar = '" + TglDaftar + "'"
                    + ", UserInputID = '" + Act.UserID + "'"
                    + ", UserInputNama = '" + Db.SingleString("SELECT Nama FROM ISC064_SECURITY..USERNAME WHERE UserID = '" + Act.UserID + "'") + "'"
                    + " WHERE NoNUP='" + noBaruNUP + "' AND Tipe = '" + jenisproperti.SelectedValue + "' AND Project = '" + project.SelectedValue + "'");

                //Logfile
                DataTable rsLog = Db.Rs("SELECT "
                    + " MS_NUP.NoNUP AS [NUP]"
                    + ",MS_CUSTOMER.Nama AS [Customer]"
                    + ",MS_CUSTOMER.Nama AS [Sales/Agent]"
                    + ",MS_NUP.UserInputNama AS [Diinput Oleh]"
                    + ",MS_NUP.Tipe AS [Jenis Properti]"
                    + ",MS_NUP.Project AS [Project]"
                    + " FROM MS_NUP INNER JOIN MS_CUSTOMER"
                    + " ON MS_NUP.NoCustomer = MS_CUSTOMER.NoCustomer"
                    + " INNER JOIN MS_AGENT ON MS_NUP.NoAgent = MS_AGENT.NoAgent"
                    + " WHERE MS_NUP.NoNUP = '" + noBaruNUP + "' AND MS_NUP.Tipe = '" + jenisproperti.SelectedValue + "' AND MS_NUP.Project = '" + project.SelectedValue + "'");

                Db.Execute("EXEC spLogNUP"
                + " 'REGIS'"
                + ",'" + Act.UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'" + Cf.LogCapture(rsLog) + "'"
                + ",'" + noBaruNUP + "'"
                + ",'" + project.SelectedValue + "'"
                + ",'" + jenisproperti.SelectedValue + "'"
                );

                DataTable rs = Db.Rs("SELECT "
                + " NoCustomer AS [No. Customer]"
                + ",Nama AS [Nama Lengkap]"
                + ",NoTelp AS [No. Telepon]"
                + ",NoHp AS [No. HP]"
                + ",NoKTP AS [No. KTP]"
                + ",KTP1 AS [KTP Alamat]"
                + ",KTP2 AS [KTP RT/RW]"
                + ",KTP5 AS [KTP Kelurahan]"
                + ",KTP3 AS [KTP Kecamatan]"
                + ",KTP4 AS [KTP Kotamadya]"
                + ",Alamat1 AS [Koresponden Alamat]"
                + ",Alamat2 AS [Koresponden RT/RW]"
                + ",Alamat3 AS [Koresponden Keluarahan]"
                + ",Alamat4 AS [Koresponden Kecamatan]"
                + ",Alamat5 AS [Koresponden Kotamadya]"
                + " FROM MS_CUSTOMER"
                + " WHERE NoCustomer = " + noCust
                );

                Db.Execute("EXEC spLogCustomer"
                    + " 'REGIS'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + Cf.LogCapture(rs) + "'"
                    + ",'" + noCust.ToString().PadLeft(5, '0') + "'"
                    );

                //balikin ke halaman daftar
                Response.Redirect("NUPDaftarCustomerFin.aspx?No=" + noBaruNUP + "&Tipe=" + jenisproperti.SelectedValue + "&Project=" + project.SelectedValue);

            }
        }

        private string NoCustomer
        {
            get
            {
                return Cf.Pk(nocustomer.Text);
            }
        }

        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            nama.Text = "";
            ctelp.Text = "";
            chp.Text = "";
            noktp.Text = "";
            ktp1.Text = "";
            ktp2.Text = "";
            ktp3.Text = "";
            ktp4.Text = "";
            ktp5.Text = "";
            Korespon1.Text = "";
            Korespon2.Text = "";
            Korespon3.Text = "";
            Korespon4.Text = "";
            Korespon5.Text = "";
            bank.Text = "";
            cabang.Text = "";
            rek.Text = "";
            reknama.Text = "";
            agent.Items.Clear();
            agent.Items.Add(new ListItem("Pilih Agent : ", "0"));
            jenisproperti.Items.Clear();
            jenisproperti.Items.Add(new ListItem("Pilih Jenis Properti : "));
            InitForm();
        }
    }
}

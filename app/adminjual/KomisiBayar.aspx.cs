using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ISC064.ADMINJUAL
{
    public partial class KomisiBayar : System.Web.UI.Page
    {
        decimal komisi40;
        decimal komisi60;
        decimal sisaKomisi;
        string btnid;
        decimal NilaiBayar;
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!IsPostBack)
            {
                Check();
                Bind();
                Fill();
            }

            Js.Confirm(this, "Lanjutkan dengan proses Pencairan Komisi?");
        }

        private void Check()
        {
            if (Db.SingleBool("SELECT SudahBayar FROM MS_KOMISI WHERE NoKontrak = '" + NoKontrak + "' AND NoAgent = " + NoAgent))
                cbStatus.Checked = true;
            else
                cbStatus.Checked = false;
        }

        protected void Bind()
        {
            DataTable rs = Db.Rs("SELECT * FROM ISC064_FINANCEAR..REF_ACC");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["Acc"].ToString();
                string t = v + " : " + rs.Rows[i]["Bank"] + " " + rs.Rows[i]["Rekening"];
                //acc.Items.Add(new ListItem(t, v));
            }
        }

        private void Fill()
        {
            string strSql = "SELECT a.*, b.NoAgent, b.NoCustomer, b.NoUnit, b.CaraBayar"
                + " FROM MS_KOMISI a"
                + " INNER JOIN MS_KONTRAK b ON a.NoKontrak = b.NoKontrak"
                + " WHERE a.NoKontrak = '" + NoKontrak + "'"
                + " AND a.NoAgent = " + NoAgent
                ;
            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                lblNoKontrak.Text = rs.Rows[0]["NoKontrak"].ToString();
                lblAgent.Text = Db.SingleString("SELECT Nama FROM MS_AGENT WHERE NoAgent = " + Cf.Pk(rs.Rows[0]["NoAgent"]));
                lblPrincipal.Text = Db.SingleString("SELECT Principal FROM MS_AGENT WHERE NoAgent = " + Cf.Pk(rs.Rows[0]["NoAgent"]));
                lblCustomer.Text = Db.SingleString("SELECT Nama FROM MS_CUSTOMER WHERE NoCustomer = " + Cf.Pk(rs.Rows[0]["NoCustomer"]));
                lblUnit.Text = rs.Rows[0]["NoUnit"].ToString();

                if (cbStatus.Checked)
                {
                    //tbNota.Text = rs.Rows[0]["NoNota"].ToString().PadLeft(7, '0');
                    tbTglBayar.Text = Cf.Day(rs.Rows[0]["TglBayar"]);
                    tbNilai.Text = Cf.Num(rs.Rows[0]["NilaiBayar"]);
                    //acc.Items.Add(new ListItem("Tidak berubah: " + rs.Rows[0]["AccKomisi"], rs.Rows[0]["AccKomisi"].ToString()));
                   // acc.SelectedIndex = acc.Items.Count - 1;
                }
                else
                {
                    int NoNota = Db.SingleInteger("SELECT ISNULL(MAX(NoNota), 0) FROM MS_KOMISI");
                    NoNota += 1;
                    //tbNota.Text = NoNota.ToString().PadLeft(7, '0');
                    tbTglBayar.Text = Cf.Day(DateTime.Now);
                    //tbNilai.Text = Cf.Num(rs.Rows[0]["NilaiKomisi"]);
                    decimal nilaiKomisi = Convert.ToDecimal(rs.Rows[0]["NilaiKomisi"]);
                    decimal closingFee = Convert.ToDecimal(rs.Rows[0]["ClosingFee"]);
                    komisi40 = Convert.ToDecimal(rs.Rows[0]["Komisi40"]);
                    komisi60 = Convert.ToDecimal(rs.Rows[0]["Komisi60"]);
                    sisaKomisi = Convert.ToDecimal(rs.Rows[0]["SisaKomisi"]);
                    for (int i = 0; i < rs.Rows.Count; i++)
                    {
                        btnid = BtnID.Substring(0, 6);
                        if (btnid == "CAIR40")
                        {
                            if (Convert.ToDecimal(rs.Rows[i]["NilaiBayarKomisi40"]) == 0)
                            {
                                tbNilai.Text = Cf.Num(komisi40);
                            }
                            else
                            {
                                decimal nilaiCair = Convert.ToDecimal(rs.Rows[i]["Komisi40"]) - Convert.ToDecimal(rs.Rows[i]["NilaiBayarKomisi40"]);
                                tbNilai.Text = Cf.Num(nilaiCair);
                            }
                        }
                        else if (btnid == "CAIR60")
                        {
                            if (Convert.ToDecimal(rs.Rows[i]["NilaiBayarKomisi60"]) == 0)
                            {
                                tbNilai.Text = Cf.Num(komisi60);
                            }
                            else
                            {
                                decimal nilaiCair = Convert.ToDecimal(rs.Rows[i]["Komisi60"]) - Convert.ToDecimal(rs.Rows[i]["NilaiBayarKomisi60"]);
                                tbNilai.Text = Cf.Num(nilaiCair);
                            }
                        }
                        else if (btnid == "CAIRCF")
                        {
                            if (Convert.ToDecimal(rs.Rows[i]["NilaiBayarCF"]) == 0)
                            {
                                tbNilai.Text = Cf.Num(closingFee);
                            }
                            else
                            {
                                decimal nilaiCair = Convert.ToDecimal(rs.Rows[i]["ClosingFee"]) - Convert.ToDecimal(rs.Rows[i]["NilaiBayarCF"]);
                                tbNilai.Text = Cf.Num(nilaiCair);
                            }
                        }
                        else
                        {
                            if (Convert.ToDecimal(rs.Rows[i]["NilaiBayarSisaKomisi"]) == 0)
                            {
                                tbNilai.Text = Cf.Num(sisaKomisi);
                            }
                            else
                            {
                                decimal nilaiCair = Convert.ToDecimal(rs.Rows[i]["SisaKomisi"]) - Convert.ToDecimal(rs.Rows[i]["NilaiBayarSisaKomisi"]);
                                tbNilai.Text = Cf.Num(nilaiCair);
                            }
                        }
                    }
                    
                }

                tbNilai.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
                tbNilai.Attributes["onkeyup"] = "CalcType(this,tempnum);";
                tbNilai.Attributes["onblur"] = "CalcBlur(this);";

                if (Convert.ToBoolean(rs.Rows[0]["FOBOKomisi"]))
                    btnOK.Enabled = false;
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

        private bool Valid()
        {
            bool x = true;
            string s = "";

            //cekNilai();

            //if (Cf.isEmpty(tbNota))
            //{
            //    x = false;

            //    if (s == "")
            //        s = tbNota.ID;

            //    lblNotac.Text = "Kosong";
            //}
            //else
            //    lblNotac.Text = "";

            //if (!Cf.isInt(tbNota))
            //{
            //    x = false;

            //    if (s == "")
            //        s = tbNota.ID;

            //    lblNotac.Text = "Format";
            //}
            //else
            //    lblNotac.Text = "";

            if (!Cf.isTgl(tbTglBayar))
            {
                x = false;

                if (s == "")
                    s = tbTglBayar.ID;

                lblTglBayarc.Text = "Tanggal";
            }
            else
                lblTglBayarc.Text = "";

            if (Cf.isEmpty(tbNilai))
            {
                x = false;

                if (s == "")
                    s = tbNilai.ID;

                lblNilaic.Text = "Kosong";
            }
            else
                lblNilaic.Text = "";

            if (!x)
            {
                this.RegisterStartupScript(
                    "focusScript"
                    , "<script type='text/javascript'>"
                    + "document.getElementById('" + s + "').focus();"
                    + "</script>"
                    );
            }

            return x;

        }

        //private void cekNilai()
        //{
            
        //}

        //private bool isDuplikat()
        //{
        //    bool x = false;
        //    string s = "";

        //    int intNoNota = Db.SingleInteger("SELECT NoNota FROM MS_KOMISI WHERE NoKontrak = '" + NoKontrak + "' AND NoAgent = " + NoAgent);
            //if (cbStatus.Checked && (Convert.ToInt32(tbNota.Text) != intNoNota))
            //{
            //    if (Db.SingleInteger("SELECT COUNT(*) FROM MS_KOMISI WHERE NoNota = " + Convert.ToInt32(tbNota.Text)) > 0)
            //    {
            //        x = true;

            //        if (s == "")
            //            s = tbNota.ID;

            //        lblNotac.Text = "Duplikat";
            //    }
            //    else
            //        lblNotac.Text = "";
            //}
            //else if (!cbStatus.Checked)
            //{
            //    if (Db.SingleInteger("SELECT COUNT(*) FROM MS_KOMISI WHERE NoNota = " + Convert.ToInt32(tbNota.Text)) > 0)
            //    {
            //        x = true;

            //        if (s == "")
            //            s = tbNota.ID;

            //        lblNotac.Text = "Duplikat";
            //    }
            //    else
            //        lblNotac.Text = "";
            //}

        //    if (x)
        //    {
        //        this.RegisterStartupScript(
        //            "focusScript2"
        //            , "<script language='javascript' type='text/javascript'>"
        //            + "document.getElementById('" + s + "').focus();"
        //            + "</script>"
        //            );
        //    }

        //    return x;
        //}

        private void Save()
        {

            DataTable rs = Db.Rs("SELECT "
            + " NoKontrak AS [No. Kontrak]"
            + ", NoUrut AS [No. Urut]"
            + ", Tipe"
            + ", NamaKomisi AS [Nama Komisi]"
            + ", Jadwal"
            + ", CaraBayar"
            + ", NilaiKomisi"
            + ", Komisi40"
            + ", Komisi60"
            + ", ClosingFee"
            + ", FlagCF"
            + ", NilaiBayarCF"
            + ", NilaiBayarKomisi40"
            + ", NilaiBayarKomisi60"
            + ", Kompensasi"
            + ", TermCair AS [Term Cair]"
            + ", NilaiKomisi AS [Nilai Komisi]"
            + ", CONVERT(VARCHAR, TglBolehBayar, 106) AS [Tgl. Boleh Bayar]"
            + " FROM MS_KOMISI"
            + " WHERE NoKontrak = '" + NoKontrak + "' AND NoAgent = " + NoAgent);

            //int NoNota = Convert.ToInt32(tbNota.Text);
            DateTime TglBayar;
            TglBayar = Convert.ToDateTime(tbTglBayar.Text);
            NilaiBayar = Convert.ToDecimal(tbNilai.Text);
            btnid = BtnID.Substring(0, 6);
            string strtgl, strBayarKomisi="";
            decimal nilaiBayarKomisi= 0;


            if (btnid == "CAIR40")
            {
                strtgl = " TglBayar =";
                strBayarKomisi = " NilaiBayarKomisi40 =";
                nilaiBayarKomisi = Db.SingleDecimal("SELECT NilaiBayarKomisi40 FROM MS_KOMISI WHERE NoKontrak = '" + NoKontrak + "' AND NoAgent = '" + NoAgent + "'");
                nilaiBayarKomisi += NilaiBayar;
            }
            else if (btnid == "CAIR60")
            {
                strtgl = " TglBayarKomisi60 =";
                strBayarKomisi = " NilaiBayarKomisi60 =";
                nilaiBayarKomisi = Db.SingleDecimal("SELECT NilaiBayarKomisi60 FROM MS_KOMISI WHERE NoKontrak = '" + NoKontrak + "' AND NoAgent = '" + NoAgent + "'");
                nilaiBayarKomisi += NilaiBayar;
            }
            else if (btnid == "CAIRCF")
            {
                strtgl = " TglBayarCF =";
                strBayarKomisi = " NilaiBayarCF =";
                nilaiBayarKomisi = Db.SingleDecimal("SELECT NilaiBayarCF FROM MS_KOMISI WHERE NoKontrak = '" + NoKontrak + "' AND NoAgent = '" + NoAgent + "'");
                nilaiBayarKomisi += NilaiBayar;
            }
            else
            {
                strtgl = " TglBayarSisaKomisi =";
                strBayarKomisi = " NilaiBayarSisaKomisi =";
                nilaiBayarKomisi = Db.SingleDecimal("SELECT NilaiBayarSisaKomisi FROM MS_KOMISI WHERE NoKontrak = '" + NoKontrak + "' AND NoAgent = '" + NoAgent + "'");
                nilaiBayarKomisi += NilaiBayar;
            }


            DataTable rsBef = Db.Rs("SELECT "
                + " NoNota AS [No. Nota]"
                + ", CONVERT(VARCHAR, TglBayar, 106) AS [Tgl. Bayar]"
                + ", NilaiBayar AS [Nilai Bayar]"
                + ", AccKomisi AS [Rekening Bank]"
                + " FROM MS_KOMISI"
                + " WHERE NoKontrak = '" + NoKontrak + "' AND NoAgent = " + NoAgent);

            string strSql = "UPDATE MS_KOMISI"
                //+ " SET NoNota = " + NoNota
                + " SET " + strtgl + "'" + TglBayar + "'"
                + ", " + strBayarKomisi + "'" + nilaiBayarKomisi + "'"
                //+ ", NilaiBayar = " + akumulasiNilaiBayar
                //+ ", SudahBayar = 1"
                //+ ", AccKomisi = '" + acc.SelectedValue + "'"
                + " WHERE NoKontrak = '" + NoKontrak + "'"
                + " AND NoAgent = " + NoAgent
                ;
            Db.Execute(strSql);
            
            //Update FlagKomisi
            DataTable rs2 = Db.Rs("SELECT * FROM MS_KOMISI WHERE NoKontrak='"+NoKontrak+"' AND NoAgent='"+NoAgent+"'");
            if (Convert.ToDecimal(rs2.Rows[0]["NilaiBayarKomisi40"]) == Convert.ToDecimal(rs2.Rows[0]["Komisi40"]))
            {
                Db.Execute("UPDATE MS_KOMISI SET FlagKomisi40=1 WHERE NoKontrak='" + NoKontrak + "' AND NoAgent='" + NoAgent + "'");
            }

            if (Convert.ToDecimal(rs2.Rows[0]["NilaiBayarKomisi60"]) == Convert.ToDecimal(rs2.Rows[0]["Komisi60"]))
            {
                Db.Execute("UPDATE MS_KOMISI SET FlagKomisi60=1 WHERE NoKontrak='" + NoKontrak + "' AND NoAgent='" + NoAgent + "'");
            }

            if (Convert.ToDecimal(rs2.Rows[0]["NilaiBayarCF"]) == Convert.ToDecimal(rs2.Rows[0]["ClosingFee"]))
            {
                Db.Execute("UPDATE MS_KOMISI SET FlagCF=1 WHERE NoKontrak='" + NoKontrak + "' AND NoAgent='" + NoAgent + "'");
            }

            if (Convert.ToDecimal(rs2.Rows[0]["NilaiBayarSisaKomisi"]) == Convert.ToDecimal(rs2.Rows[0]["SisaKomisi"]))
            {
                Db.Execute("UPDATE MS_KOMISI SET FlagSisaKomisi=1 WHERE NoKontrak='" + NoKontrak + "' AND NoAgent='" + NoAgent + "'");
            }

            DataTable rsAft = Db.Rs("SELECT "
                + " NoNota AS [No. Nota]"
                + ", CONVERT(VARCHAR, TglBayar, 106) AS [Tgl. Bayar]"
                + ", NilaiBayar AS [Nilai Bayar]"
                + ", AccKomisi AS [Rekening Bank]"
                + " FROM MS_KOMISI"
                + " WHERE NoKontrak = '" + NoKontrak + "' AND NoAgent = " + NoAgent);

            //Cek Cara Bayar
            //string caraBayar = rs.Rows[0]["CaraBayar"].ToString();
            //string caraBayar2= Db.SingleString("SELECT CaraBayar FROM MS_KONTRAK WHERE NoKontrak='" + NoKontrak + "' AND NoAgent='" + NoAgent + "'");

            //if (Convert.ToInt32(rs.Rows[0]["Kompensasi"]) == 0 )
            //{
            //    if (caraBayar == "CASH KERAS")
            //    {
            //        if (caraBayar != caraBayar2)
            //        {
            //            if (caraBayar2 != "CASH KERAS")
            //            {
            //                decimal nilaikomisi = Convert.ToDecimal(rs.Rows[0]["NilaiKomisi"]);
            //                decimal kompensasi = (decimal)0.015 * nilaikomisi;
            //                Db.Execute("UPDATE MS_KOMISI SET Kompensasi='" + kompensasi + "' WHERE NoKontrak='" + NoKontrak + "' AND NoAgent='" + NoAgent + "'");
            //            }
            //        }
            //    }
            //}

            string Ket = "";
            if (!cbStatus.Checked)
            {
                Ket = Cf.LogCapture(rs)
                    + "<br>---BAYAR KOMISI---<br>"
                    + Cf.LogCapture(rsAft)
                    ;
            }
            else
            {
                Ket = Cf.LogCapture(rs)
                    + "<br>---EDIT KOMISI---<br>"
                    + Cf.LogCompare(rsBef, rsAft)
                    ;
            }

            Db.Execute("EXEC spLogKontrak"
                + " 'EJK'"
                + ",'" + Act.UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'" + Ket + "'"
                + ",'" + NoKontrak + "'"
                );

            decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
            string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
            Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

            this.RegisterStartupScript(
                "closeScript"
                , "<script type='text/javascript'>"
                + "dialogArguments.location.href = 'AgentJadwalKomisi.aspx?NoAgent=" + NoAgent+"';"
                + "window.close();"
                + "</script>"
                );
        }

        protected void btnOK_Click(object sender, System.EventArgs e)
        {

            if (Valid())
            {
                string btnid = BtnID.Substring(0, 6);
                decimal nilaiBayar = Convert.ToDecimal(tbNilai.Text);
                decimal nilaiBelumCair;
                DataTable rs2 = Db.Rs("SELECT * FROM MS_KOMISI WHERE NoKontrak='" + NoKontrak + "' AND NoAgent='" + NoAgent + "'");

                if (btnid == "CAIR40")
                {
                    if (nilaiBayar > (Convert.ToDecimal(rs2.Rows[0]["Komisi40"]) - Convert.ToDecimal(rs2.Rows[0]["NilaiBayarKomisi40"])) || nilaiBayar < (decimal)0)
                    {
                        if (Convert.ToDecimal(rs2.Rows[0]["NilaiBayarKomisi40"]) == 0)
                        {
                            Js.Alert(
                            this
                            , "Input Tidak Valid.\\n\\n"
                            + "Nilai cair kurang / melebihi nilai komisi.\\n"
                            , "document.getElementById('"+tbNilai.ClientID+"').focus();"
                            );
                            tbNilai.Text = Cf.Num(Convert.ToDecimal(rs2.Rows[0]["Komisi40"]));
                        }
                        else
                        {
                            Js.Alert(
                            this
                            , "Input Tidak Valid.\\n\\n"
                            + "Nilai cair kurang / melebihi nilai komisi.\\n"
                            , "document.getElementById('" + tbNilai.ClientID + "').focus();"
                            );
                            nilaiBelumCair = Convert.ToDecimal(rs2.Rows[0]["Komisi40"]) - Convert.ToDecimal(rs2.Rows[0]["NilaiBayarKomisi40"]);
                            tbNilai.Text = Cf.Num(nilaiBelumCair);
                        }

                    }
                    else
                    {
                        Save();
                    }
                }
                else if (btnid == "CAIR60")
                {
                    if (nilaiBayar > (Convert.ToDecimal(rs2.Rows[0]["Komisi60"]) - Convert.ToDecimal(rs2.Rows[0]["NilaiBayarKomisi60"])) || nilaiBayar < (decimal)0)
                    {
                        if (Convert.ToDecimal(rs2.Rows[0]["NilaiBayarKomisi60"]) == 0)
                        {
                            Js.Alert(
                            this
                            , "Input Tidak Valid.\\n\\n"
                            + "Nilai cair kurang / melebihi nilai komisi.\\n"
                            , "document.getElementById('" + tbNilai.ClientID + "').focus();"
                            );
                            tbNilai.Text = Cf.Num(Convert.ToDecimal(rs2.Rows[0]["Komisi60"]));
                        }
                        else
                        {
                            Js.Alert(
                            this
                            , "Input Tidak Valid.\\n\\n"
                            + "Nilai cair kurang / melebihi nilai komisi.\\n"
                            , "document.getElementById('" + tbNilai.ClientID + "').focus();"
                            );
                            nilaiBelumCair = Convert.ToDecimal(rs2.Rows[0]["Komisi60"]) - Convert.ToDecimal(rs2.Rows[0]["NilaiBayarKomisi60"]);
                            tbNilai.Text = Cf.Num(nilaiBelumCair);
                        }

                    }
                    else
                    {
                         Save();
                    }
                }
                else if (btnid == "CAIRCF")
                {
                    if (nilaiBayar > (Convert.ToDecimal(rs2.Rows[0]["ClosingFee"]) - Convert.ToDecimal(rs2.Rows[0]["NilaiBayarCF"])) || nilaiBayar < (decimal)0)
                    {
                        if (Convert.ToDecimal(rs2.Rows[0]["NilaiBayarCF"]) == 0)
                        {
                            Js.Alert(
                            this
                            , "Input Tidak Valid.\\n\\n"
                            + "Nilai cair kurang / melebihi nilai komisi.\\n"
                            , "document.getElementById('" + tbNilai.ClientID + "').focus();"
                            );
                            tbNilai.Text = Cf.Num(Convert.ToDecimal(rs2.Rows[0]["ClosingFee"]));
                        }
                        else
                        {
                            Js.Alert(
                            this
                            , "Input Tidak Valid.\\n\\n"
                            + "Nilai cair kurang / melebihi nilai komisi.\\n"
                            , "document.getElementById('" + tbNilai.ClientID + "').focus();"
                            );
                            nilaiBelumCair = Convert.ToDecimal(rs2.Rows[0]["ClosingFee"]) - Convert.ToDecimal(rs2.Rows[0]["NilaiBayarCF"]);
                            tbNilai.Text = Cf.Num(nilaiBelumCair);
                        }
                    }
                    else
                    {
                        Save();
                    }
                }
                else
                {
                    if (nilaiBayar > (Convert.ToDecimal(rs2.Rows[0]["SisaKomisi"]) - Convert.ToDecimal(rs2.Rows[0]["NilaiBayarSisaKomisi"])) || nilaiBayar < (decimal)0)
                    {
                        if (Convert.ToDecimal(rs2.Rows[0]["NilaiBayarSisaKomisi"]) == 0)
                        {
                            Js.Alert(
                            this
                            , "Input Tidak Valid.\\n\\n"
                            + "Nilai cair kurang / melebihi nilai komisi.\\n"
                            , "document.getElementById('" + tbNilai.ClientID + "').focus();"
                            );
                            tbNilai.Text = Cf.Num(Convert.ToDecimal(rs2.Rows[0]["SisaKomisi"]));
                        }
                        else
                        {
                            Js.Alert(
                            this
                            , "Input Tidak Valid.\\n\\n"
                            + "Nilai cair kurang / melebihi nilai komisi.\\n"
                            , "document.getElementById('" + tbNilai.ClientID + "').focus();"
                            );
                            nilaiBelumCair = Convert.ToDecimal(rs2.Rows[0]["SisaKomisi"]) - Convert.ToDecimal(rs2.Rows[0]["NilaiBayarSisaKomisi"]);
                            tbNilai.Text = Cf.Num(nilaiBelumCair);
                        }
                    }
                    else
                    {
                        Save();
                    }
                }
            }
        }

        private string NoKontrak
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoKontrak"]);
            }
        }

        private string NoAgent
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoAgent"]);
            }
        }

        private string BtnID
        {
            get
            {
                return Cf.Pk(Request.QueryString["id"]);
            }
        }

        //private string Periode
        //{
        //    get
        //    {
        //        return Cf.Pk(Request.QueryString["Periode"]);
        //    }
        //}
    }
}
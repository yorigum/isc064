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
    public partial class KomisiGenerate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            FeedBack();

        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "<a href=\"javascript:popEditKontrak('" + Request.QueryString["done"] + "')\">"
                        + "Generate Komisi Berhasil..."
                        + "</a>";
            }
        }

        protected void next_Click(object sender, System.EventArgs e)
        {
            //if (valid())
            //{
            pilih.Visible = false;
            frm.Visible = true;

            //InitForm();
            Fill();

            Js.Confirm(this, "Lanjutkan proses generate jadwal komisi?");
            //}
        }
        private string NoKontrak
        {
            get
            {
                return Cf.Pk(nokontrak.Text);
            }
        }

        protected void Fill()
        {
            string strSql = "SELECT * FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'";
            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                customer.Text = Db.SingleString(
                    "SELECT Nama FROM MS_CUSTOMER WHERE NoCustomer = " + rs.Rows[0]["NoCustomer"]);
                no.Text = NoKontrak;
                unit.Text = rs.Rows[0]["NoUnit"].ToString();
                dpp.Text = Cf.Num(rs.Rows[0]["NilaiDPP"]);
                if (rs.Rows[0]["FlagKomisi"].ToString() != "1")
                {
                    FillTb(Convert.ToInt32(rs.Rows[0]["NoAgent"]), rs.Rows[0]["Refferal"].ToString(), Convert.ToDecimal(rs.Rows[0]["NilaiDPP"]));
                }
                else
                {
                    save.Enabled = false;
                }
            }
        }
        protected void FillTb(int NoAgent, string Refferal, decimal DPP)
        {
            DataTable rs = Db.Rs("SELECT * FROM MS_AGENT WHERE NoAgent=" + NoAgent);
            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                HtmlTableCell c;
                HtmlTableRow tr;
                HtmlTableRow tr2;
                HtmlTableRow tr3;
                Label l;

                decimal baris = 1;
                //Pokok

                if (Refferal == "")
                {
                    decimal Persen = Db.SingleDecimal("SELECT Nilai FROM REF_KOMISI WHERE Kategori='Agent'");
                    decimal Nilai = DPP * Persen / 100;
                    tr = new HtmlTableRow();

                    c = new HtmlTableCell();
                    l = new Label();
                    l.Text = "Komisi Pokok Agent";
                    c.Controls.Add(l);
                    tr.Cells.Add(c);

                    c = new HtmlTableCell();
                    l = new Label();
                    l.Text = rs.Rows[0]["Nama"].ToString();
                    c.Controls.Add(l);
                    tr.Cells.Add(c);

                    c = new HtmlTableCell();
                    c.Align = "Right";
                    l = new Label();
                    l.Text = Cf.Num(Nilai);
                    c.Controls.Add(l);
                    tr.Cells.Add(c);

                    list.Controls.Add(tr);
                }
                else
                {
                    decimal Persen = Db.SingleDecimal("SELECT Nilai FROM REF_KOMISI WHERE Kategori='AgentReff'");
                    decimal Nilai = DPP * Persen / 100;
                    tr = new HtmlTableRow();

                    c = new HtmlTableCell();
                    l = new Label();
                    l.Text = "Komisi Pokok Agent";
                    c.Controls.Add(l);
                    tr.Cells.Add(c);

                    c = new HtmlTableCell();
                    l = new Label();
                    l.Text = rs.Rows[0]["Nama"].ToString();
                    c.Controls.Add(l);
                    tr.Cells.Add(c);

                    c = new HtmlTableCell();
                    l = new Label();
                    l.Text = Cf.Num(Nilai);
                    c.Align = "Right";
                    c.Controls.Add(l);
                    tr.Cells.Add(c);

                    list.Controls.Add(tr);

                    decimal Persen2 = Db.SingleDecimal("SELECT Nilai FROM REF_KOMISI WHERE Kategori='Refferal'");
                    decimal Nilai2 = DPP * Persen2 / 100;
                    tr2 = new HtmlTableRow();

                    c = new HtmlTableCell();
                    l = new Label();
                    l.Text = "Komisi Pokok Refferal";
                    c.Controls.Add(l);
                    tr2.Cells.Add(c);

                    c = new HtmlTableCell();
                    l = new Label();
                    l.Text = Refferal;
                    c.Controls.Add(l);
                    tr2.Cells.Add(c);

                    c = new HtmlTableCell();
                    l = new Label();
                    l.Text = Cf.Num(Nilai2);
                    c.Align = "Right";
                    c.Controls.Add(l);
                    tr2.Cells.Add(c);

                    list.Controls.Add(tr2);
                }

                //Closing Fee
                if (rs.Rows[0]["Tipe"].ToString().Contains("INHOUSE"))
                {
                    DataTable CF = Db.Rs("SELECT * FROM REF_KOMISI_CF");
                    for (int i = 0; i < CF.Rows.Count; i++)
                    {
                        if (DPP >= Convert.ToDecimal(CF.Rows[i]["NilaiBawah"]) && DPP <= Convert.ToDecimal(CF.Rows[i]["NilaiAtas"]))
                        {
                            tr = new HtmlTableRow();

                            c = new HtmlTableCell();
                            l = new Label();
                            l.Text = "Closing Fee GM";
                            c.Controls.Add(l);
                            tr.Cells.Add(c);

                            c = new HtmlTableCell();
                            l = new Label();
                            l.Text = Db.SingleString("SELECT ISNULL(GeneralManager,'') FROM MS_KOMISI_OVER WHERE NoKontrak='" + NoKontrak + "'");
                            c.Controls.Add(l);
                            tr.Cells.Add(c);

                            c = new HtmlTableCell();
                            l = new Label();
                            l.Text = Cf.Num(CF.Rows[i]["NilaiGM"]);
                            c.Align = "Right";
                            c.Controls.Add(l);
                            tr.Cells.Add(c);
                            list.Controls.Add(tr);


                            tr2 = new HtmlTableRow();

                            c = new HtmlTableCell();
                            l = new Label();
                            l.Text = "Closing Fee SM";
                            c.Controls.Add(l);
                            tr2.Cells.Add(c);

                            c = new HtmlTableCell();
                            l = new Label();
                            l.Text = Db.SingleString("SELECT ISNULL(SalesManager,'') FROM MS_KOMISI_OVER WHERE NoKontrak='" + NoKontrak + "'");
                            c.Controls.Add(l);
                            tr2.Cells.Add(c);

                            c = new HtmlTableCell();
                            l = new Label();
                            l.Text = Cf.Num(CF.Rows[i]["NilaiSM"]);
                            c.Align = "Right";
                            c.Controls.Add(l);
                            tr2.Cells.Add(c);
                            list.Controls.Add(tr2);

                            tr3 = new HtmlTableRow();

                            c = new HtmlTableCell();
                            l = new Label();
                            l.Text = "Closing Fee M";
                            c.Controls.Add(l);
                            tr3.Cells.Add(c);

                            c = new HtmlTableCell();
                            l = new Label();
                            l.Text = Db.SingleString("SELECT ISNULL(SalesManager,'') FROM MS_KOMISI_OVER WHERE NoKontrak='" + NoKontrak + "'");
                            c.Controls.Add(l);
                            tr3.Cells.Add(c);

                            c = new HtmlTableCell();
                            l = new Label();
                            l.Text = Cf.Num(CF.Rows[i]["NilaiM"]);
                            c.Align = "Right";
                            c.Controls.Add(l);
                            tr3.Cells.Add(c);
                            list.Controls.Add(tr3);
                        }
                    }
                }

                //overriding
                DataTable Over = Db.Rs("SELECT * FROM MS_KOMISI_OVER WHERE NoKontrak='" + NoKontrak + "'");

                if (Over.Rows.Count != 0)
                {
                    DataTable OveRef = Db.Rs("SELECT * FROM REF_KOMISI_OVER");
                    for (int i = 0; i < OveRef.Rows.Count; i++)
                    {
                        if (Db.SingleString("SELECT ISNULL(" + OveRef.Rows[i]["ID"] + ",'') FROM MS_KOMISI_OVER WHERE NoKontrak='" + NoKontrak + "'") != "")
                        {
                            tr = new HtmlTableRow();

                            c = new HtmlTableCell();
                            l = new Label();
                            l.Text = OveRef.Rows[i]["Jabatan"].ToString();
                            c.Controls.Add(l);
                            tr.Cells.Add(c);

                            c = new HtmlTableCell();
                            l = new Label();
                            l.Text = Db.SingleString("SELECT ISNULL(" + OveRef.Rows[i]["ID"] + ",'') FROM MS_KOMISI_OVER WHERE NoKontrak='" + NoKontrak + "'");
                            c.Controls.Add(l);
                            tr.Cells.Add(c);

                            c = new HtmlTableCell();
                            l = new Label();
                            decimal Nilai = (Convert.ToBoolean(OveRef.Rows[i]["CrossSelling"])) ? DPP * Convert.ToDecimal(OveRef.Rows[i]["CrossSelling"]) / 100 : DPP * Convert.ToDecimal(OveRef.Rows[i]["Project"]) / 100;
                            l.Text = Cf.Num(Nilai);
                            c.Controls.Add(l);
                            tr.Cells.Add(c);
                            list.Controls.Add(tr);
                        }
                    }

                }
            }
        }

        protected void save_Click(object sender, EventArgs e)
        {
            string strSql = "SELECT * FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'";
            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                Save(Convert.ToInt32(rs.Rows[0]["NoAgent"]), rs.Rows[0]["Refferal"].ToString(), Convert.ToDecimal(rs.Rows[0]["NilaiDPP"]));
            }
        }
        protected void Save(int NoAgent, string Refferal, decimal DPP)
        {
            DataTable rs = Db.Rs("SELECT * FROM MS_AGENT WHERE NoAgent=" + NoAgent);
            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                if (Refferal == "")
                {
                    decimal Persen = Db.SingleDecimal("SELECT Nilai FROM REF_KOMISI WHERE Kategori='Agent'");
                    decimal Nilai = DPP * Persen / 100;


                    Db.Execute("EXEC spKomisiDaftar "
                        + "'" + NoKontrak + "'"
                        + ", 'PK'"
                        + ", 'Komisi Pokok Agent'"
                        + ", " + Nilai
                        + ", '" + rs.Rows[0]["Nama"].ToString() + "'");

                }
                else
                {
                    decimal Persen = Db.SingleDecimal("SELECT Nilai FROM REF_KOMISI WHERE Kategori='AgentReff'");
                    decimal Nilai = DPP * Persen / 100;

                    Db.Execute("EXEC spKomisiDaftar "
                        + "'" + NoKontrak + "'"
                        + ", 'PK'"
                        + ", 'Komisi Pokok Agent'"
                        + ", " + Nilai
                        + ", '" + rs.Rows[0]["Nama"].ToString() + "'");

                    decimal Persen2 = Db.SingleDecimal("SELECT Nilai FROM REF_KOMISI WHERE Kategori='Refferal'");
                    decimal Nilai2 = DPP * Persen2 / 100;

                    Db.Execute("EXEC spKomisiDaftar "
                        + "'" + NoKontrak + "'"
                        + ", 'PK'"
                        + ", 'Komisi Pokok Refferal'"
                        + ", " + Nilai
                        + ", '" + Refferal + "'");

                }

                //Closing Fee
                if (rs.Rows[0]["Tipe"].ToString().Contains("INHOUSE"))
                {
                    DataTable CF = Db.Rs("SELECT * FROM REF_KOMISI_CF");
                    for (int i = 0; i < CF.Rows.Count; i++)
                    {
                        if (DPP >= Convert.ToDecimal(CF.Rows[i]["NilaiBawah"]) && DPP <= Convert.ToDecimal(CF.Rows[i]["NilaiAtas"]))
                        {
                            Db.Execute("EXEC spKomisiDaftar "
                                    + "'" + NoKontrak + "'"
                                    + ", 'CF'"
                                    + ", 'Closing Fee GM'"
                                    + ", " + Convert.ToDecimal(CF.Rows[i]["NilaiGM"])
                                    + ", '" + Db.SingleString("SELECT ISNULL(GeneralManager,'') FROM MS_KOMISI_OVER WHERE NoKontrak='" + NoKontrak + "'") + "'");

                            Db.Execute("EXEC spKomisiDaftar "
                                    + "'" + NoKontrak + "'"
                                    + ", 'CF'"
                                    + ", 'Closing Fee SM'"
                                    + ", " + Convert.ToDecimal(CF.Rows[i]["NilaiSM"])
                                    + ", '" + Db.SingleString("SELECT ISNULL(SalesManager,'') FROM MS_KOMISI_OVER WHERE NoKontrak='" + NoKontrak + "'") + "'");

                            Db.Execute("EXEC spKomisiDaftar "
                                    + "'" + NoKontrak + "'"
                                    + ", 'CF'"
                                    + ", 'Closing Fee M'"
                                    + ", " + Convert.ToDecimal(CF.Rows[i]["NilaiM"])
                                    + ", '" + Db.SingleString("SELECT ISNULL(ProjectManager,'') FROM MS_KOMISI_OVER WHERE NoKontrak='" + NoKontrak + "'") + "'");

                        }
                    }
                }

                //overriding
                DataTable Over = Db.Rs("SELECT * FROM MS_KOMISI_OVER WHERE NoKontrak='" + NoKontrak + "'");

                if (Over.Rows.Count != 0)
                {
                    DataTable OveRef = Db.Rs("SELECT * FROM REF_KOMISI_OVER");
                    for (int i = 0; i < OveRef.Rows.Count; i++)
                    {
                        if (Db.SingleString("SELECT ISNULL(" + OveRef.Rows[i]["ID"] + ",'') FROM MS_KOMISI_OVER WHERE NoKontrak='" + NoKontrak + "'") != "")
                        {
                            decimal Nilai = (Convert.ToBoolean(OveRef.Rows[i]["CrossSelling"])) ? DPP * Convert.ToDecimal(OveRef.Rows[i]["CrossSelling"]) / 100 : DPP * Convert.ToDecimal(OveRef.Rows[i]["Project"]) / 100;

                            Db.Execute("EXEC spKomisiDaftar "
                                + "'" + NoKontrak + "'"
                                + ", 'OV'"
                                + ", 'Overriding " + OveRef.Rows[i]["Jabatan"].ToString() + "'"
                                + ", " + Nilai
                                + ", '" + Db.SingleString("SELECT ISNULL(" + OveRef.Rows[i]["ID"] + ",'') FROM MS_KOMISI_OVER WHERE NoKontrak='" + NoKontrak + "'") + "'");

                        }
                    }

                }
            }

            Response.Redirect("KomisiGenerate.aspx?done="+ NoKontrak +"");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ISC064.COLLECTION
{
    public partial class RealisasiDenda2 : System.Web.UI.Page
    {
        protected DataTable rsTagihan;
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            FillTable();
            //	FeedBack();
            if (frm.Visible)
                Js.Confirm(this, "Lanjutkan proses Realisasi Denda?");
        }

        private bool valid()
        {
            bool x = true;

            int c = Db.SingleInteger(
                "SELECT COUNT(*) FROM ISC064_MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "' AND Status = 'A'");

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

        private void next_Click(object sender, System.EventArgs e)
        {
            //FillTable();
            if (valid())
            {
                //pilih.Visible = false;
                frm.Visible = true;

                FillTable();

                Js.Confirm(this, "Lanjutkan proses Realisasi Denda?");
            }
        }

        private void FillTable()
        {
            //Func.KontrakHeader(NoKontrak, nokontrakl, unit, customer, agent);
            DataTable rsk = Db.Rs("SELECT * FROM ISC064_MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
            nokontrakl.Text = rsk.Rows[0]["NoKontrak"].ToString();
            unit.Text = rsk.Rows[0]["NoUnit"].ToString();
            customer.Text = Db.SingleString("SELECT Nama FROM ISC064_MARKETINGJUAL..MS_CUSTOMER WHERE NoCustomer = '" + rsk.Rows[0]["NoCustomer"].ToString() + "'");
            agent.Text = Db.SingleString("SELECT Nama FROM ISC064_MARKETINGJUAL..MS_AGENT WHERE NoaGENT = '" + rsk.Rows[0]["NoAgent"].ToString() + "'");
            benefit.Text = Cf.Num(RoundUp(Db.SingleDecimal("SELECT ISNULL(SUM(Benefit-BenefitReal),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "'")));
            cek.Enabled = Convert.ToDecimal(benefit.Text) > 0 ? true : false;

            list.Controls.Clear();
            rsTagihan = Db.Rs("SELECT * FROM ISC064_MARKETINGJUAL..MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' "
                + " AND Denda > 0 AND (Denda - DendaReal) !=0 AND KPR<>1 AND Tipe <> 'ADM'");
            //			Rpt.NoData(list, rs, "Tidak ada tagihan untuk kontrak tersebut.");

            int nomer = 0;
            for (int i = 0; i < rsTagihan.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                //No
                Label l;
                TextBox bx;
                CheckBox cb;

                nomer++;
                l = new Label();
                l.Text = "<tr>"
                    + "<td>" + nomer + ".</td>";
                list.Controls.Add(l);

                l = new Label();
                l.Text = ""
                    + "<td>" + rsTagihan.Rows[i]["NamaTagihan"] + "</td>";
                list.Controls.Add(l);

                l = new Label();
                l.Text = ""
                    + "<td>" + rsTagihan.Rows[i]["Tipe"] + "</td>";
                list.Controls.Add(l);

                l = new Label();
                l.Text = ""
                    + "<td>" + Cf.Day(rsTagihan.Rows[i]["TglJT"]) + "</td>";
                list.Controls.Add(l);

                l = new Label();
                l.Text = ""
                    + "<td>" + Cf.Num(rsTagihan.Rows[i]["NilaiTagihan"]) + "</td>";
                list.Controls.Add(l);

                l = new Label();
                l.Text = ""
                     + "<td>" + Cf.Num(RoundUp(Convert.ToDecimal(rsTagihan.Rows[i]["Denda"]))) + "</td>";
                list.Controls.Add(l);

                l = new Label();
                l.Text = "<td>";
                list.Controls.Add(l);

                //Nilai
                bx = new TextBox();
                bx.ID = "real_" + i;
                bx.CssClass = "txt_num";
                bx.Text = Cf.Num(0);
                bx.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
                bx.Attributes["onkeyup"] = "CalcType(this,tempnum);";
                bx.Attributes["onblur"] = "CalcBlur(this);";
                bx.Width = 90;
                bx.Attributes["style"] = "font:8pt";
                list.Controls.Add(bx);

                l = new Label();
                l.Text = "</td>";
                list.Controls.Add(l);

                l = new Label();
                decimal Sisa = Convert.ToDecimal(rsTagihan.Rows[i]["Denda"]) - Convert.ToDecimal(rsTagihan.Rows[i]["DendaReal"]) - Convert.ToDecimal(rsTagihan.Rows[i]["NilaiPutihDenda"]) - Convert.ToDecimal(rsTagihan.Rows[i]["AlokasiBenefit"]);
                l.Text = ""
                    + "<td>" + Cf.Num(RoundUp(Sisa)) + "</td>";
                list.Controls.Add(l);

                cb = new CheckBox();
                cb.ID = "ben_" + i;
                cb.Visible = false;
                list.Controls.Add(cb);

                l = new Label();
                l.Text = "</td>";
                list.Controls.Add(l);

                l = new Label();
                l.Text = "</tr>";
                list.Controls.Add(l);
            }
        }

        protected void SubTotal(decimal t1, decimal t2, decimal t3)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = new TableCell();
            c.Text = "<strong>Grand Total</strong>";
            c.HorizontalAlign = HorizontalAlign.Left;
            c.ColumnSpan = 5;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "<strong>" + Cf.Num(t1) + "</strong>";
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "<strong>" + Cf.Num(t2) + "</strong>";
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "<strong>" + Cf.Num(t3) + "</strong>";
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            //rpt.Rows.Add(r);
        }
        protected void gantirealisasi(object sender, EventArgs e)
        {
            if (cek.Checked)
            {
                decimal x = Convert.ToDecimal(benefit.Text);

                for (int i = 0; i < rsTagihan.Rows.Count; i++)
                {
                    TextBox lunas = (TextBox)list.FindControl("real_" + i);
                    CheckBox cb = (CheckBox)list.FindControl("ben_" + i);

                    decimal SisaDenda = Convert.ToDecimal(rsTagihan.Rows[i]["Denda"]) - Convert.ToDecimal(rsTagihan.Rows[i]["DendaReal"]) - Convert.ToDecimal(rsTagihan.Rows[i]["NilaiPutihDenda"]) - Convert.ToDecimal(rsTagihan.Rows[i]["AlokasiBenefit"]);
                    lunas.ReadOnly = true;
                    cb.Checked = true;

                    if (i == rsTagihan.Rows.Count - 1)
                    {
                        //last row
                        if (SisaDenda < x)
                        {
                            lunas.Text = Cf.Num(RoundUp(SisaDenda));
                        }
                        else
                        {
                            lunas.Text = Cf.Num(RoundUp(x));
                        }
                    }
                    else
                    {
                        if (SisaDenda >= x)
                        {
                            //break, soalnya total udah abis
                            lunas.Text = Cf.Num(RoundUp(x));
                            break;
                        }
                        else
                        {
                            lunas.Text = Cf.Num(RoundUp(SisaDenda));
                        }
                    }

                    x = x - SisaDenda;
                }
            }
            else
            {
                for (int i = 0; i < rsTagihan.Rows.Count; i++)
                {
                    TextBox lunas = (TextBox)list.FindControl("real_" + i);
                    CheckBox cb = (CheckBox)list.FindControl("ben_" + i);

                    lunas.Text = "0";
                    lunas.ReadOnly = false;
                    cb.Checked = false;
                }
            }
        }

        protected bool ValidNilai()
        {
            bool x = true;

            for(int i=0; i<rsTagihan.Rows.Count; i++)
            {
                TextBox Lunas = (TextBox)list.FindControl("real_" + i);
                if(Lunas.Text != "")
                {
                    decimal NilaiSisaDenda = RoundUp(Convert.ToDecimal(rsTagihan.Rows[i]["Denda"])) - RoundUp(Convert.ToDecimal(rsTagihan.Rows[i]["DendaReal"])) - RoundUp(Convert.ToDecimal(rsTagihan.Rows[i]["PutihDenda"]));
                    decimal NilaiRealisasi = Convert.ToDecimal(Lunas.Text);

                    if(NilaiRealisasi > NilaiSisaDenda)
                    {
                        x = false;
                        Lunas.ForeColor = Color.Red;
                    }
                    else
                    {
                        Lunas.ForeColor = Color.Black;
                    }
                }
            }
            if (!x)
            {
                Js.Alert(this, "Nilai Realisasi Melebihin Sisa Denda","");
            }
            return x;
        }

        private bool datavalid()
        {
            bool x = true;
            string s = "";

            if (!cek.Checked)
            {
            if (!Cf.isTgl(tgl))
            {
                x = false;

                if (s == "")
                    s = tgl.ID;

                tglc.Text = "Tanggal";
            }
            else
                tglc.Text = "";
            }

            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Kemungkinan Sebab :\\n"
                    + "1. Tagihan Denda harus berupa angka.\\n"
                    + "2. Format Tanggal : Bulan / Tanggal / Tahun.\\n"
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );

            return x;
        }

        protected bool filevalid()
        {
            bool x = true;
            string s = "";

            if (file.PostedFile.FileName.Length != 0
                && !file.PostedFile.FileName.EndsWith(".jpg"))
            {
                x = false;

                if (s == "")
                    s = file.ID;
            }

            if (!x)
            {
                Js.Alert(
                    this
                    , "Proses Upload Gagal.\\n"
                    + "File yang boleh di-upload adalah file dengan extension .jpg saja."
                    , "document.getElementById('" + s + "').focus();"
                    );
            }

            return x;
        }

        protected void save_Click(object sender, EventArgs e)
        {
            if (datavalid() && ValidNilai())
            {
                if (filevalid())
                {
                    decimal Nilai = 0;
                    string NoUrut2 = "";

                    DataTable rsBef2 = Db.Rs("SELECT * FROM ISC064_MARKETINGJUAL..MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' "
                        + " AND Denda > 0 AND (Denda - DendaReal) !=0 AND KPR<>1 AND Tipe <> 'ADM'");

                    for (int i = 0; i < rsBef2.Rows.Count; i++)
                    {
                        CheckBox cb = (CheckBox)list.FindControl("ben_" + i);
                        if (cb.Checked)
                        {
                        TextBox Realisasi = (TextBox)list.FindControl("real_" + i);

                        int NoUrut = Convert.ToInt32(rsBef2.Rows[i]["NoUrut"]);

                        decimal A = Db.SingleDecimal("SELECT AlokasiBenefit FROM ISC064_MARKETINGJUAL..MS_TAGIHAN WHERE NoUrut = '" + NoUrut + "' and NoKontrak = '" + NoKontrak + "' ");
                        decimal AlokasiBenefit = Convert.ToDecimal(Realisasi.Text);
                        decimal C = A + AlokasiBenefit;
                        decimal X = AlokasiBenefit;
                            DataTable be = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN WHERE Benefit > BenefitReal AND NoKontrak = '" + NoKontrak + "'");
                            for (int j = 0; j < be.Rows.Count; j++)
                            {
                                decimal Benefit = Convert.ToDecimal(be.Rows[j]["Benefit"]);
                                decimal BenefitReal = Convert.ToDecimal(be.Rows[j]["BenefitReal"]);
                                decimal Sisa = Benefit - BenefitReal;

                                decimal D = Sisa < AlokasiBenefit ? Sisa : AlokasiBenefit;
                                decimal E = D + BenefitReal;

                                Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN SET BenefitReal = '" + E + "' WHERE NoKontrak = '" + be.Rows[j]["NoKontrak"] + "' AND NoUrut = '" + be.Rows[j]["NoUrut"] + "'");

                                X -= D;
                            }

                            Db.Execute("UPDATE ISC064_MARKETINGJUAL..MS_TAGIHAN Set AlokasiBenefit = '" + C + "' WHERE NoKontrak = '" + NoKontrak + "' and NoUrut = '" + NoUrut + "' ");
                        }
                        else
                        {
                            TextBox Realisasi = (TextBox)list.FindControl("real_" + i);

                            int NoUrut = Convert.ToInt32(rsBef2.Rows[i]["NoUrut"]);
                            decimal A = Db.SingleDecimal("SELECT DendaReal FROM ISC064_MARKETINGJUAL..MS_TAGIHAN WHERE NoUrut = '" + NoUrut + "' and NoKontrak = '" + NoKontrak + "' ");
                            decimal DendaReal = Convert.ToDecimal(Realisasi.Text);
                            if (DendaReal > 0)
                            {
                                NoUrut2 += NoUrut.ToString() + ";";
                            }
                            decimal C = A + DendaReal;
                        Nilai += DendaReal;

                            Db.Execute("UPDATE ISC064_MARKETINGJUAL..MS_TAGIHAN Set DendaReal = '" + C + "' WHERE NoKontrak = '" + NoKontrak + "' and NoUrut = '" + NoUrut + "' ");
                        }
                    }

                    if (Nilai > 0)
                    {
                            DateTime Tgl = Convert.ToDateTime(tgl.Text);

                            Db.Execute("EXEC ISC064_MARKETINGJUAL..spTagihanDaftar"
                            + " '" + NoKontrak + "'"
                            + ", 'BIAYA DENDA'"
                            + ", '" + Tgl + "'"
                            + ", " + Nilai
                            + ", 'ADM'"
                            );

                        int NoUrut1 = Db.SingleInteger("SELECT TOP 1 NoUrut FROM ISC064_MARKETINGJUAL..MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' order by NoUrut desc");
                        Db.Execute("UPDATE ISC064_MARKETINGJUAL..MS_TAGIHAN SET Jenis = 'DO', NoUrut2 = '" + NoUrut2 + "' WHERE NoKontrak = '" + NoKontrak + "' AND NoUrut= " + NoUrut1 + " ");
                    }

                    //realisasi = putihkan langsung
                    //Db.Execute("UPDATE MS_TAGIHAN SET PutihDenda = 1"
                    //        + " WHERE NoKontrak = '" + NoKontrak + "' AND NoUrut = '" +  + "'"
                    //    );
                    int NoUrut3 = Db.SingleInteger("SELECT TOP 1 NoUrut FROM ISC064_MARKETINGJUAL..MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' order by NoUrut desc");

                    DataTable rsAft = Db.Rs("SELECT "
                        + "CONVERT(VARCHAR,NoUrut) + '.   ' + NamaTagihan + ' ('+Tipe+')   ' + CONVERT(VARCHAR,TglJT,106) + '   ' + CONVERT(VARCHAR,NilaiTagihan,1) "
                        + "FROM ISC064_MARKETINGJUAL..MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' AND NoUrut = '"+NoUrut3+"' ORDER BY NoUrut");

                    DataTable rs = Db.Rs("SELECT"
                        + " ISC064_MARKETINGJUAL..MS_KONTRAK.NoKontrak AS [No. Kontrak]"
                        + ",ISC064_MARKETINGJUAL..MS_KONTRAK.NoUnit AS [Unit]"
                        + ",ISC064_MARKETINGJUAL..MS_CUSTOMER.Nama AS [Customer]"
                        + ",ISC064_MARKETINGJUAL..MS_KONTRAK.NilaiKontrak AS [Nilai Kontrak]"
                        + ",ISC064_MARKETINGJUAL..MS_KONTRAK.Skema AS [Skema]"
                        + " FROM ISC064_MARKETINGJUAL..MS_KONTRAK INNER JOIN ISC064_MARKETINGJUAL..MS_CUSTOMER"
                        + " ON ISC064_MARKETINGJUAL..MS_KONTRAK.NoCustomer = ISC064_MARKETINGJUAL..MS_CUSTOMER.NoCustomer"
                        + " WHERE ISC064_MARKETINGJUAL..MS_KONTRAK.NoKontrak = '" + NoKontrak + "'");

                    string Ket = Cf.LogCapture(rs)
                        + "<br>---REALISASI DENDA---<br>"
                        + Cf.LogList(rsAft, "JADWAL TAGIHAN")
                        ;

                    Db.Execute("EXEC ISC064_MARKETINGJUAL..spLogKontrak "
                        + " 'RD'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + Ket + "'"
                        + ",'" + NoKontrak + "'"
                        );

                    decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM "+Mi.DbPrefix+"MARKETINGJUAL..MS_KONTRAK_LOG ORDER BY LogID DESC");
                    string Project = Db.SingleString("SELECT Project FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                    Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                    Db.Execute("EXEC ISC064_MARKETINGJUAL..spLogRealisasiDenda "
                        + " 'RD'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + Ket + "'"
                        + ",'" + NoKontrak + "'"
                        );

                    decimal LogID2 = Db.SingleDecimal("SELECT TOP 1 LogID FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_REALISASIDENDA_LOG ORDER BY LogID DESC");                    
                    Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_REALISASIDENDA_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID2);

                    /*Insert jurnal kontrak*/
                    string strKetJurnal = "KONTRAK REALISASI DENDA<br />" + Cf.Str(baru.Text);

                    Db.Execute("EXEC ISC064_MARKETINGJUAL..spJurnalKontrak "
                        + " '" + Act.UserID + "'"
                        + ",'" + NoKontrak + "'"
                        + ",'" + strKetJurnal + "'"
                        );

                    if (file.PostedFile.FileName.Length != 0)
                    {
                        long JurnalID = Db.SingleLong("SELECT TOP 1 JurnalID FROM ISC064_MARKETINGJUAL..MS_KONTRAK_JURNAL ORDER BY JurnalID DESC");
                        string path = Request.PhysicalApplicationPath
                            + "JurnalKontrak\\" + JurnalID + ".jpg";
                        Dfc.UploadFile(".jpg", path, file);
                    }
                    Response.Redirect("RealisasiDenda.aspx?done=" + NoKontrak);
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

        private static decimal RoundUp(decimal input)
        {
            string x = input.ToString();
            string[] arr = x.Split(new char[] { '.' });

            if (arr.Length > 1)
            {
                if (decimal.Parse(arr[1]) > 0)
                {
                    decimal dc = decimal.Parse(arr[0]) + 1;
                    return dc;
                }
                else
                {
                    return decimal.Parse(arr[0]);
                }
            }
            else
            {
                return input;
            }
        }
    }
}
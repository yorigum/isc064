using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.ADMINJUAL.Laporan
{
    public partial class LaporanMasterKomisi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();

            int excel = Convert.ToInt16(Request.QueryString["Excel"]);
            if (excel == 1)
            {
                Report();
                rp.Controls.Add(header);
                rp.Controls.Add(report);
                Rpt.ToExcel(this, rp);
            }
            else
            {
                Report();
            }

            if (!Page.IsPostBack)
            {
                lblPT.Text = Mi.Pt;
                FillHeader();
                header.Visible = false;
                //Report();
            }  
        }

        private void FillHeader()
        {
            DataTable rs = Db.Rs("SELECT * FROM MS_AGENT WHERE NoAgent='" + NoAgent + "'");
            lblAgent.Text = rs.Rows[0]["Nama"].ToString();
            lblNoAgent.Text = rs.Rows[0]["NoAgent"].ToString();
            lblPrincipal.Text = rs.Rows[0]["Principal"].ToString();
            lblStatus.Text = rs.Rows[0]["Status"].ToString();
            lblAlamat.Text = rs.Rows[0]["Alamat"].ToString();
            lblPeriodeKomisi.Text = PeriodeKomisi;
        }

        private void Report()
        {
            Fill(NoAgent);
        }

        private void Fill(string NoAgent)
        {
            string strPeriodeKomisi = PeriodeKomisi;
            string periodekomisi= "";
            if (strPeriodeKomisi == "SEMUA")
            {
                periodekomisi = "";
            }
            else
            {
                periodekomisi = " AND PeriodeKomisi = '"+PeriodeKomisi+"'";
            }
            string strSql = "SELECT MS_KOMISI.*, MS_KONTRAK.NoUnit As unit"
            + " FROM MS_KOMISI"
            + " INNER JOIN MS_KONTRAK ON MS_KOMISI.NoKontrak = MS_KONTRAK.NoKontrak"
            + " WHERE MS_KOMISI.NoAgent = '"+ NoAgent +"'"
            + periodekomisi
            + " ORDER BY PeriodeKomisi";
            DataTable rs = Db.Rs(strSql);

            DataTable rs2 = Db.Rs("SELECT * FROM MS_AGENT WHERE NoAgent='" + NoAgent + "'");

                Label l;
                

                //Header
                l = new Label();
                l.Text = "<table cellpadding='4'>";
                header.Controls.Add(l);

                l = new Label();
                l.Text = "<tr><th colspan='3' align='left'><h3>Pancoran Riverside</h3></th></tr>";
                header.Controls.Add(l);

                l = new Label();
                l.Text = "<tr><td><h3>Laporan Master Komisi</h3></td>";
                header.Controls.Add(l);

                l = new Label();
                l.Text = "<td>&nbsp;</td>";
                header.Controls.Add(l);

                l = new Label();
                l.Text = "<td>&nbsp;</td></tr>";
                header.Controls.Add(l);

                l = new Label();
                l.Text = "<tr><td><b>Periode Komisi</b></td>";
                header.Controls.Add(l);

                l = new Label();
                l.Text = "<td><b>:</b></td>";
                header.Controls.Add(l);

                l = new Label();
                l.Text = "<td><b>" + PeriodeKomisi  + "</b></td></tr>";
                header.Controls.Add(l);

                l = new Label();
                l.Text = "<tr><td><b>Nama Sales</b></td>";
                header.Controls.Add(l);

                l = new Label();
                l.Text = "<td><b>:</b></td>";
                header.Controls.Add(l);

                l = new Label();
                l.Text = "<td><b>" + rs2.Rows[0]["Nama"].ToString() + "</td></b></tr>";
                header.Controls.Add(l);

                l = new Label();
                l.Text = "<tr><td><b>No Sales</b></td>";
                header.Controls.Add(l);

                l = new Label();
                l.Text = "<td><b>:</b></td>";
                header.Controls.Add(l);

                l = new Label();
                l.Text = "<td align='left'><b>" + rs2.Rows[0]["NoAgent"].ToString() + "</b></td></tr>";
                header.Controls.Add(l);

                l = new Label();
                l.Text = "<tr><td><b>Principal</b></td>";
                header.Controls.Add(l);

                l = new Label();
                l.Text = "<td><b>:</b></td>";
                header.Controls.Add(l);

                l = new Label();
                l.Text = "<td><b>" + rs2.Rows[0]["Principal"].ToString() + "</b></td></tr>";
                header.Controls.Add(l);


                l = new Label();
                l.Text = "<tr><td><b>Status</b></td>";
                header.Controls.Add(l);

                l = new Label();
                l.Text = "<td><b>:</b></td>";
                header.Controls.Add(l);

                l = new Label();
                l.Text = "<td><b>" + rs2.Rows[0]["Status"].ToString() + "</b></td></tr>";
                header.Controls.Add(l);

                l = new Label();
                l.Text = "<tr><td valign='top'><b>Alamat</b></td>";
                header.Controls.Add(l);

                l = new Label();
                l.Text = "<td valign='top'><b>:<b></td>";
                header.Controls.Add(l);

                l = new Label();
                l.Text = "<td valign='top'><b>" + rs2.Rows[0]["Alamat"].ToString() + "</b></td></tr>";
                header.Controls.Add(l);

                l = new Label();
                l.Text = "<tr><td valign='top'>&nbsp;</td></tr>";
                header.Controls.Add(l);

                l = new Label();
                l.Text = "</table>";
                header.Controls.Add(l);

                //Laporan
                l = new Label();
                l.Text = "<table width='800px' cellpadding='4'>";
                report.Controls.Add(l);

                l = new Label();
                l.Text = "<tr><th style='background-color: gray; color: white;' align='left'>No. Kontrak</th>";
                report.Controls.Add(l);

                l = new Label();
                l.Text = "<th style='background-color: gray; color: white;' align='left'>Unit</th>";
                report.Controls.Add(l);

                l = new Label();
                l.Text = "<th style='background-color: gray; color: white;' align='left'>Periode Komisi</th>";
                report.Controls.Add(l);

                l = new Label();
                l.Text = "<th style='background-color: gray; color: white;' align='left'>Status</th>";
                report.Controls.Add(l);

                l = new Label();
                l.Text = "<th style='background-color: gray; color: white;' align='left'>Nilai Komisi</th>";
                report.Controls.Add(l);

                l = new Label();
                l.Text = "<th style='background-color: gray; color: white;' align='left'>Tanggal Cair</th>";
                report.Controls.Add(l);

                l = new Label();
                l.Text = "<th style='background-color: gray; color: white;' align='left'>Nilai Bayar</th></tr>";
                report.Controls.Add(l);

                decimal t = 0; decimal t2 = 0; decimal t3 = 0; decimal nilaiBayarTot = 0;
                for (int i = 0; i < rs.Rows.Count; i++)
                {
                    if (!Response.IsClientConnected)
                    {
                        break;
                    }    

                    l = new Label();
                    l.Text = "<td rowspan='2' valign='top'>" + rs.Rows[i]["NoKontrak"].ToString()+ "</td>";
                    report.Controls.Add(l);
                    
                    

                    l = new Label();
                    l.Text = "<td rowspan='2' valign='top'>" + rs.Rows[i]["unit"].ToString() + "</td>";
                    report.Controls.Add(l);

                    l = new Label();
                    l.Text = "<td rowspan='2' valign='top'>" + rs.Rows[i]["PeriodeKomisi"].ToString() + "</td>";
                    report.Controls.Add(l);

                    string status40 = "";
                    string status60 = "";
                    string statusCF = "";
                    string statusSK = "";
                    bool stat40 = Convert.ToBoolean(rs.Rows[i]["FlagKomisi40"]);
                    bool stat60 = Convert.ToBoolean(rs.Rows[i]["FlagKomisi60"]);
                    bool statCF = Convert.ToBoolean(rs.Rows[i]["FlagCF"]);
                    bool statSK = Convert.ToBoolean(rs.Rows[i]["FlagSisaKomisi"]);
                    decimal SisaKomisi = Convert.ToDecimal(rs.Rows[i]["SisaKomisi"]);
                    decimal NilaiKomisi2 = Convert.ToDecimal(rs.Rows[i]["NilaiKomisi2"]);
                    decimal persencair40 = 0;
                    decimal persencair60 = 0;
                    decimal persencairCF = 0;
                    decimal persencairSK = 0;

                    if (Convert.ToDecimal(rs.Rows[i]["NilaiBayarKomisi40"]) > 0)
                    {
                        persencair40 = (Convert.ToDecimal(rs.Rows[i]["NilaiBayarKomisi40"]) / Convert.ToDecimal(rs.Rows[i]["Komisi40"])) * 100;
                        status40 = "Sudah Cair ("+ Math.Round(persencair40, 2) +"%)";
                    }
                    else
                    {
                        status40 = "Belum Cair";

                    }

                    if (Convert.ToDecimal(rs.Rows[i]["NilaiBayarKomisi60"]) > 0)
                    {
                        persencair60 = (Convert.ToDecimal(rs.Rows[i]["NilaiBayarKomisi60"]) / Convert.ToDecimal(rs.Rows[i]["Komisi60"])) * 100;
                        status60 = "Sudah Cair (" + Math.Round(persencair60, 2) + "%)";
                    }
                    else
                    {
                        status60 = "Belum Cair";
                    }

                    if (Convert.ToDecimal(rs.Rows[i]["NilaiBayarCF"]) > 0)
                    {
                        persencairCF = (Convert.ToDecimal(rs.Rows[i]["NilaiBayarCF"]) / Convert.ToDecimal(rs.Rows[i]["ClosingFee"])) * 100;
                        statusCF = "Sudah Cair (" + Math.Round(persencairCF, 2) + "%)";
                    }
                    else
                    {
                        statusCF = "Belum Cair";
                    }

                    if (Convert.ToDecimal(rs.Rows[i]["NilaiBayarSisaKomisi"]) > 0)
                    {
                        persencairSK = (Convert.ToDecimal(rs.Rows[i]["NilaiBayarSisaKomisi"]) / Convert.ToDecimal(rs.Rows[i]["SisaKomisi"])) * 100;
                        statusSK = "Sudah Cair (" + Math.Round(persencairSK, 2) + "%)";
                    }
                    else
                    {
                        statusSK = "Belum Cair";
                    }

                    l = new Label();
                    l.Text = "<td>"+ status40 +"</td>";
                    report.Controls.Add(l);

                    if (SisaKomisi == 0)
                    {
                        l = new Label();
                        l.Text = "<td  align='right'>" + Cf.Num(rs.Rows[i]["Komisi40"]) + "</td>";
                        report.Controls.Add(l);
                    }
                    else
                    {
                        l = new Label();
                        l.Text = "<td  align='right'>" + Cf.Num((decimal) 0.4 * NilaiKomisi2) + "</td>";
                        report.Controls.Add(l);
                    }

                    l = new Label();
                    l.Text = "<td align='right'>" + Cf.Day(rs.Rows[i]["TglBayar"]) + "</td>";
                    report.Controls.Add(l);

                    l = new Label();
                    l.Text = "<td align='right'>" + Cf.Num(rs.Rows[i]["NilaiBayarKomisi40"]) + "</td></tr>";
                    report.Controls.Add(l);
         
                    l = new Label();
                    l.Text = "<tr><td>" + status60 + "</td>";
                    report.Controls.Add(l);


                    if (SisaKomisi == 0)
                    {
                        l = new Label();
                        l.Text = "<td  align='right'>" + Cf.Num(rs.Rows[i]["Komisi60"]) + "</td>";
                        report.Controls.Add(l);
                    }
                    else
                    {
                        l = new Label();
                        l.Text = "<td  align='right'>" + Cf.Num((decimal)0.6 * NilaiKomisi2) + "</td>";
                        report.Controls.Add(l);
                    }

                    l = new Label();
                    l.Text = "<td align='right'>" + Cf.Day(rs.Rows[i]["TglBayarKomisi60"]) + "</td>";
                    report.Controls.Add(l);

                    l = new Label();
                    l.Text = "<td align='right'>" + Cf.Num(rs.Rows[i]["NilaiBayarKomisi60"]) + "</td></tr>";
                    report.Controls.Add(l);

                    l = new Label();
                    l.Text = "<tr><td colspan='3'>Closing Fee</td>";
                    report.Controls.Add(l);

                    l = new Label();
                    l.Text = "<td>" + statusCF + "</td>";
                    report.Controls.Add(l);

                    decimal CF = Convert.ToDecimal(rs.Rows[i]["ClosingFee"]);

                    l = new Label();
                    l.Text = "<td align='right'>"+Cf.Num(CF)+"</td>";
                    report.Controls.Add(l);

                    l = new Label();
                    l.Text = "<td align='right'>" + Cf.Day(rs.Rows[i]["TglBayarCF"]) + "</td>";
                    report.Controls.Add(l);

                    decimal nilaiBayarCF = Convert.ToDecimal(rs.Rows[i]["NilaiBayarCF"]);
                    l = new Label();
                    l.Text = "<td align='right'>" + Cf.Num(nilaiBayarCF) + "</td></tr>";
                    report.Controls.Add(l);

                    //decimal kompensasi = Convert.ToDecimal(rs.Rows[i]["Kompensasi"]);
                    
                    if (SisaKomisi != 0)
                    {
                        if (SisaKomisi > 0)
                        {
                            l = new Label();
                            l.Text = "<tr><td colspan='3'>Sisa Komisi</td>";
                            report.Controls.Add(l);

                            l = new Label();
                            l.Text = "<td>" + statusCF + "</td>";
                            report.Controls.Add(l);

                            l = new Label();
                            l.Text = "<td align='right'>&nbsp;</td>";
                            report.Controls.Add(l);

                            l = new Label();
                            l.Text = "<td align='right'>" + Cf.Day(rs.Rows[i]["TglBayarSisaKomisi"]) + "</td>";
                            report.Controls.Add(l);

                            l = new Label();
                            l.Text = "<td align='right'>" + Cf.Num(Convert.ToDecimal(rs.Rows[i]["NilaiBayarSisaKomisi"])) + "</td></tr>";
                            report.Controls.Add(l);
                        }
                        else
                        {
                            l = new Label();
                            l.Text = "<tr><td colspan='3'>Pengurangan Komisi</td>";
                            report.Controls.Add(l);

                            l = new Label();
                            l.Text = "<td>&nbsp;</td>";
                            report.Controls.Add(l);

                            l = new Label();
                            l.Text = "<td align='right'>&nbsp;</td>";
                            report.Controls.Add(l);

                            l = new Label();
                            l.Text = "<td align='right'>&nbsp;</td>";
                            report.Controls.Add(l);

                            l = new Label();
                            l.Text = "<td align='right'>" + Cf.Num(SisaKomisi) + "</td></tr>";
                            report.Controls.Add(l);
                        }

                    }

                    l = new Label();
                    l.Text = "<tr><td colspan='4' style='border-top:1px solid black; border-bottom:1px dashed gray;'><b>SUB TOTAL</b></td>";
                    report.Controls.Add(l);

                    decimal nilaiKomisi = Convert.ToDecimal(rs.Rows[i]["Komisi40"]) + Convert.ToDecimal(rs.Rows[i]["Komisi60"]);
                    decimal nilaBayar40 = Convert.ToDecimal(rs.Rows[i]["NilaiBayarKomisi40"]);
                    decimal nilaBayar60 = Convert.ToDecimal(rs.Rows[i]["NilaiBayarKomisi60"]);
                    decimal nilaiBayarSK = Convert.ToDecimal(rs.Rows[i]["NilaiBayarSisaKomisi"]);

                    if(SisaKomisi< 0)
                        t3 = nilaBayar40 + nilaBayar60 + nilaiBayarCF + SisaKomisi;
                    else
                        t3 = nilaBayar40 + nilaBayar60 + nilaiBayarCF + nilaiBayarSK;
                    t = CF + nilaiKomisi + SisaKomisi; 

                    l = new Label();
                    l.Text = "<td align='right' style='border-top:1px solid black; border-bottom:1px dashed gray;'><b>" + Cf.Num(t) + "</b></td>";
                    report.Controls.Add(l);

                    l = new Label();
                    l.Text = "<td style='border-top:1px solid black; border-bottom:1px dashed gray;'>&nbsp;</td>";
                    report.Controls.Add(l);

                    l = new Label();
                    l.Text = "<td align='right' style='border-top:1px solid black; border-bottom:1px dashed gray;'><b>" + Cf.Num(t3) + "</b></td></tr>";
                    report.Controls.Add(l);

                    t2 += t;//sub total Nilai Komisi
                    nilaiBayarTot += t3;//sub total nilai bayar

                }

                    l = new Label();
                    l.Text = "<tr><td colspan='4' style='border-bottom:2px solid black; border-top:2px solid black;'><b>GRAND TOTAL</b></td>";
                    report.Controls.Add(l);
                    
                    
                    l = new Label();
                    l.Text = "<td align= 'right' style='border-bottom:2px solid black; border-top:2px solid black;'><b>" + Cf.Num(t2) + "</b></td>";
                    report.Controls.Add(l);

                    l = new Label();
                    l.Text = "<td style='border-bottom:2px solid black; border-top:2px solid black;'>&nbsp;</td>";
                    report.Controls.Add(l);

                    l = new Label();
                    l.Text = "<td align= 'right' style='border-bottom:2px solid black; border-top:2px solid black;'><b>" + Cf.Num(nilaiBayarTot) + "</b></td></tr>";
                    report.Controls.Add(l);

                    l = new Label();
                    l.Text = "</table>";
                    report.Controls.Add(l);

        }

        private string NoAgent
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoAgent"]);
            }
        }

        private string PeriodeKomisi
        {
            get
            {
                return Cf.Pk(Request.QueryString["PeriodeKomisi"]);
            }
        }

    }
}

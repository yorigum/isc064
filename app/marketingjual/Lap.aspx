<%@ Page Language="c#" Inherits="ISC064.MARKETINGJUAL.Lap" CodeFile="Lap.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Laporan</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan">
    <style type="text/css">
        .list td {
            width: 220px;
            padding-left: 50px;
        }

        h2 {
            font: bold 10pt;
        }

        .left50 {
            margin-left: 50px;
        }
    </style>
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <div style="display: none">
            <h1>Laporan</h1>
            <br />
            <table class="list">
                <tr>
                    <td>
                        <a href="javascript:openLaporan('Laporan/ExecutiveSummary.aspx')">Management Report</a>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <a href="javascript:openLaporan('Laporan/MasterCustomer.aspx')">Master Customer</a>
                    </td>
                </tr>
                <tr>
                    <td>
                        <a href="javascript:openLaporan('Laporan/MasterReservasi.aspx')">Master Reservasi</a>
                    </td>
                </tr>
                <tr>
                    <td>
                        <a href="javascript:openLaporan('Laporan/MasterKontrak.aspx')">Master Kontrak</a>
                    </td>
                </tr>
                <tr>
                    <td>
                        <a href="javascript:openLaporan('Laporan/MasterTagihan.aspx')">Master Tagihan</a>
                    </td>
                </tr>
                <tr>
                    <td>
                        <a href="javascript:openLaporan('Laporan/MasterKomisi.aspx')">Master Komisi</a>
                    </td>
                </tr>
                <tr>
                    <td>
                        <a href="javascript:openLaporan('Laporan/MasterKomisiDetail.aspx')">Master Komisi Per-kontrak</a>
                    </td>
                </tr>   
                <!--
					<tr>
						<td>
							<a href="javascript:openLaporan('Laporan/MasterStock.aspx')">Master Stock</a>
						</td>
					</tr>
					-->
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <a href="javascript:openLaporan('Laporan/OutstandingTagihan.aspx')">Laporan Outstanding
                        Tagihan</a>
                    </td>
                </tr>
                <tr>
                    <td>
                        <a href="javascript:openLaporan('Laporan/LaporanPenjualan2.aspx')">Laporan Penjualan</a>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <a href="javascript:openLaporan('Laporan/LaporanGantiNama.aspx')">Laporan Pengalihan
                        Hak</a>
                    </td>
                </tr>
                <tr>
                    <td>
                        <a href="javascript:openLaporan('Laporan/LaporanGantiUnit.aspx')">Laporan Pindah Unit</a>
                    </td>
                </tr>
                <tr>
                    <td>
                        <a href="javascript:openLaporan('Laporan/LaporanBatal.aspx')">Laporan Customer Batal</a>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <a href="javascript:openLaporan('Laporan/LaporanPenjualan.aspx')">Laporan Kartu Piutang</a>
                    </td>
                </tr>
                <tr>
                    <td>
                        <a href="javascript:openLaporan('Laporan/LaporanAgingPiutang.aspx');">Laporan Aging
                        Piutang</a>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <a href="javascript:openLaporan('Laporan/LaporanPenjualanBF.aspx')">Laporan Penjualan
                        Baru Bayar Sampai BF</a>
                    </td>
                </tr>
                <tr>
                    <td>
                        <a href="javascript:openLaporan('Laporan/LaporanBatal.aspx');">Laporan Formulir Pembatalan</a>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <a href="javascript:openLaporan('Laporan/LaporanProyeksiTagihan.aspx');">Laporan Proyeksi
                        Tagihan</a>
                    </td>
                </tr>
            </table>
        </div>
        <h1 class="title title-line">Laporan
        </h1>
        <br />
        <div>
            <div class="box-list">
                <a href="javascript:openLaporan('Laporan/ExecutiveSummary.aspx')">
                    <div class="box-content">
                        <span>Laporan</span>
                        <p>Management<br>
                            Report</p>
                    </div>
                </a>
            </div>
            <div class="box-list">
                <a href="javascript:openLaporan('Laporan/LaporanPenjualan2.aspx')">
                    <div class="box-content">
                        <span>Laporan</span>
                        <p>Penjualan</p>
                    </div>
                </a>
            </div>
            <div class="box-list">
                <a href="javascript:openLaporan('Laporan/LaporanPenjualan3.aspx')">
                    <div class="box-content">
                        <span>Laporan</span>
                        <p>Penjualan Tahunan</p>
                    </div>
                </a>
            </div>
            <div class="box-list">
                <a href="javascript:openLaporan('Laporan/LaporanSalesPerformance.aspx')">
                    <div class="box-content">
                        <span>Laporan</span>
                        <p>Sales Performance</p>
                    </div>
                </a>
            </div>
            <div class="box-list">
                <a href="javascript:openLaporan('Laporan/LaporanGantiNama.aspx')">
                    <div class="box-content">
                        <span>Laporan</span>
                        <p>Pengalihan Hak</p>
                    </div>
                </a>
            </div>
            <div class="box-list">
                <a href="javascript:openLaporan('Laporan/LaporanGantiUnit.aspx')">
                    <div class="box-content">
                        <span>Laporan</span>
                        <p>Pindah Unit</p>
                    </div>
                </a>
            </div>
            <div class="box-list">
                <a href="javascript:openLaporan('Laporan/LaporanBatal.aspx')">
                    <div class="box-content">
                        <span>Laporan</span>
                        <p>Pembatalan</p>
                    </div>
                </a>
            </div>
            <div class="box-list">
                <a href="javascript:openLaporan('Laporan/LaporanAktivitasCustomer.aspx')">
                    <div class="box-content">
                        <span>Laporan</span>
                        <p>Aktivitas Customer</p>
                    </div>
                </a>
            </div>
            <div class="box-list">
                <a href="javascript:openLaporan('Laporan/MasterStock.aspx')">
                    <div class="box-content">
                        <span>Laporan</span>
                        <p>Master Stock</p>
                    </div>
                </a>
            </div>
            <div class="box-list">
                <a href="javascript:openLaporan('Laporan/MasterKontrak.aspx')">
                    <div class="box-content">
                        <span>Laporan</span>
                        <p>Master Kontrak</p>
                    </div>
                </a>
            </div>
            <div class="box-list">
                <a href="javascript:openLaporan('Laporan/MasterCustomer.aspx')">
                    <div class="box-content">
                        <span>Laporan</span>
                        <p>Master Customer</p>
                    </div>
                </a>
            </div>
            <div class="box-list"  style="display:none">
                <a href="javascript:openLaporan('Laporan/MasterKomisi.aspx')">
                    <div class="box-content">
                        <span>Laporan</span>
                        <p>Master Komisi</p>
                    </div>
                </a>
            </div>
            <div class="box-list"  style="display:none">
                <a href="javascript:openLaporan('Laporan/MasterKomisiDetail.aspx')">
                    <div class="box-content">
                        <span>Laporan</span>
                        <p>Master Komisi Per-kontrak</p>
                    </div>
                </a>
            </div>
            <div class="box-list" style="display:none">
                <a href="javascript:openLaporan('Laporan/MasterKomisiNonSales.aspx')">
                    <div class="box-content">
                        <span>Laporan</span>
                        <p>Master Komisi Non Sales</p>
                    </div>
                </a>
            </div>
            <div class="box-list"  style="display:none">
                <a href="javascript:openLaporan('Laporan/MasterKomisiBayar.aspx')">
                    <div class="box-content">
                        <span>Laporan</span>
                        <p>Komisi dalam Proses Pembayaran</p>
                    </div>
                </a>
            </div>
            <div class="box-list">
                <a href="javascript:openLaporan('Laporan/SummaryPenjualan.aspx')">
                    <div class="box-content">
                        <span>Laporan</span>
                        <p>Rekapitulasi Kontrak dan Pembayaran</p>
                    </div>
                </a>
            </div>
            <div class="box-list" style="display: none;">
                <a href="javascript:openLaporan('Laporan/LaporanWawancara.aspx')">
                    <div class="box-content">
                        <span>Laporan</span>
                        <p>Wawancara</p>
                    </div>
                </a>
            </div>
            <div class="box-list">
                <a href="javascript:openLaporan('Laporan/LaporanSP3k.aspx')">
                    <div class="box-content">
                        <span>Laporan</span>
                        <p>SP3K</p>
                    </div>
                </a>
            </div>
            <div class="box-list" style="display:none">
                <a href="javascript:openLaporan('Laporan/LaporanAkad.aspx')">
                    <div class="box-content">
                        <span>Laporan</span>
                        <p>Akad</p>
                    </div>
                </a>
            </div>
           <%-- <div class="box-list">
                <a href="javascript:openLaporan('Laporan/LaporanAJB.aspx')">
                    <div class="box-content">
                        <span>Laporan</span>
                        <p>AJB</p>
                    </div>
                </a>
            </div>--%>
            <div class="box-list" visible="false">
                <a href="javascript:openLaporan('Laporan/LaporanPPJB.aspx')">
                    <div class="box-content">
                        <span>Laporan</span>
                        <p>PPJB</p>
                    </div>
                </a>
            </div>
            <div class="box-list" visible="false">
                <a href="javascript:openLaporan('Laporan/LaporanComplain.aspx')">
                    <div class="box-content">
                        <span>Laporan</span>
                        <p>Complain</p>
                    </div>
                </a>
            </div>
            <div class="box-list">
                <a href="javascript:openLaporan('Laporan/LaporanMasterStock.aspx')">
                    <div class="box-content">
                        <span>Laporan</span>
                        <p>Master Stock Per Tipe Property</p>
                    </div>
                </a>
            </div>
            <div class="box-list">
                <a href="javascript:openLaporan('Laporan/MasterReservasi.aspx')">
                    <div class="box-content">
                        <span>Laporan</span>
                        <p>Master Reservasi</p>
                    </div>
                </a>
            </div>
            <div class="box-list" style="display: none;">
                <a href="javascript:openLaporan('Laporan/LaporanMasterStockDetil.aspx')">
                    <div class="box-content">
                        <span>Laporan</span>
                        <p>Master Stock Per Tipe Property (Detail)</p>
                    </div>
                </a>
            </div>
        </div>
    </form>
</body>
</html>

<%@ Page Language="c#" Inherits="ISC064.MARKETINGJUAL.Nav" CodeFile="Nav.aspx.cs" %>

<!DOCTYPE html>
<html>
<head>
    <title>Navigasi</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="0">
    <meta name="sec" content="Navigasi">
</head>
<body onunload="browserClose()" style="width: 97%">
    <form id="Form1" method="post" runat="server">
        <script type="text/javascript" src="/Js/Common.js"></script>
        <div class="sidebar">
            <div class="sidebar-header" style="background: #f09635">
                <p id="comp" runat="server" style="border-bottom: solid white 2px; margin-bottom: 5px;"></p>
                <div class="sidebar-logo">
                    <img src="/Media/icon_sales.png">
                    <p>SALES</p>
                </div>
            </div>
            <div class="sidebar-content">
                <ul class="sidebar-menu">
                    <li>
                        <i class="fa fa-bell-o sb-ico"></i>
                        <input value="Reminder" onclick="go(this, 'Reminder.aspx')" class="navmenu" type="button">
                    </li>
                    <li>
                        <i class="fa fa-bar-chart sb-ico"></i>
                        <input value="Dashboard" onclick="go(this, 'DashboardSales.aspx')" class="navmenu" type="button">
                    </li>
                    <li>
                        <i class="fa fa-pie-chart sb-ico"></i>
                        <input value="Laporan" onclick="go(this, 'Lap.aspx')" class="navmenu" type="button">
                    </li>
                    <%--<li>
                        <i class="fa fa-money sb-ico"></i>
                        <input value="Komisi" onclick="go(this, 'KomisiGeneratePeriode.aspx')" class="navmenu" type="button">
                        <input value="Komisi" onclick="opensub(this, 'subKomisi')" class="navmenu" type="button">
                        <span class="fa fa-plus-square addons" onclick="opensub(this, 'subKomisi')"></span>--%>
                    <%--<ul class="nav-submenu" style="display: none;" id='subKomisi'>
                            <li>
                                <input value="Generate Komisi" onclick="go(this, 'KomisiGeneratePeriode.aspx')" class="navmenu" type="button"></li>
                            <li>
                                <input value="Clear False Komisi" onclick="go(this, 'KomisiClearancePeriode.aspx')" class="navmenu" type="button"></li>
                            <li>
                                <input value="Pengajuan Komisi" onclick="go(this, 'KomisiPengajuan.aspx')" class="navmenu" type="button"></li>
                            <li>
                                <input value="Realisasi Komisi" onclick="go(this, 'KomisiRealisasi.aspx')" class="navmenu" type="button"></li>
                        </ul>
                    </li>--%>
                    <li>
                        <i class="fa fa-calculator sb-ico"></i>
                        <input value="Kalkulator" class="navmenu" type="button" onclick="opensub(this, 'subKalkulator')"><span class="fa fa-plus-square addons" onclick="opensub(this, 'subKalkulator')"></span>
                        <ul class="nav-submenu" style="display: none" id="subKalkulator">
                            <li>
                                <input value="Kalkulator KPR" onclick="go(this, 'KalkulatorKPR.aspx')" class="navmenu" type="button"></li>
                            <li>
                                <input value="Kalkulator Cara Bayar" onclick="go(this, 'KalkulatorSkema.aspx')" class="navmenu" type="button"></li>
                        </ul>
                    </li>
                    <li>
                        <i class="fa fa-users sb-ico"></i>
                        <input value="Customer" onclick="go(this, 'Customer.aspx'); opensub(this, 'subCustomer')" class="navmenu" type="button"><span class="fa fa-plus-square addons" onclick="opensub(this, 'subCustomer')"></span>
                        <ul class="nav-submenu" style="display: none" id="subCustomer">
                            <li>
                                <input value="Pendaftaran" onclick="go(this, 'CustomerDaftar.aspx')" class="navmenu" type="button"></li>
                            <%--<li>
                                <input value="Gabung Nomor" onclick="go(this, 'CustomerGabung.aspx')" class="navmenu" type="button"></li>--%>
                            <li>
                                <input value="Upload" onclick="go(this, 'CustomerUpload.aspx')" class="navmenu" type="button"></li>
                        </ul>
                    </li>
                    <li>
                        <i class="fa fa-building-o sb-ico"></i>
                        <input value="Unit" onclick="go(this, 'Unit.aspx'); opensub(this, 'subUnit')" class="navmenu" type="button"><span class="fa fa-plus-square addons" onclick="opensub(this, 'subUnit')"></span>
                        <ul class="nav-submenu" style="display: none" id="subUnit">
                             <li>
                                <input value="Closing" onclick="go(this, 'Closing.aspx')" class="navmenu" type="button"></li>
                             <li>
                                <input value="Closing NUP" onclick="go(this, 'ClosingNUP.aspx')" class="navmenu" type="button"></li>
                            <li>
                                <input value="Tabel Stok" onclick="go(this, 'TabelStok.aspx')" class="navmenu" type="button"></li>
                            <%--<li>
                                <input value="Floor Plan" onclick="go(this, 'UnitPeta.aspx')" class="navmenu" type="button" s></li>--%>
                            <li>
                                <input value="Site Plan" onclick="go(this, 'UnitPetaList.aspx')" class="navmenu" type="button"></li>
                        </ul>
                    </li>
                    <li>
                        <i class="fa fa-calendar-check-o sb-ico"></i>
                        <input value="Reservasi" onclick="go(this, 'Reservasi.aspx'); opensub(this, 'subReservasi')" class="navmenu" type="button"><span class="fa fa-plus-square addons" onclick="opensub(this, 'subReservasi')"></span>
                        <ul class="nav-submenu" style="display: none" id="subReservasi">
                            <li>
                                <input value="Pendaftaran" onclick="go(this, 'ReservasiDaftar.aspx')" class="navmenu" type="button"></li>
                            <%--<li>
                                <input value="Master TTR" onclick="go(this, 'TTR.aspx')" class="navmenu" type="button"></li>--%>
                            <li>
                                <input value="TTS Belum Di Print" onclick="go(this, 'PrintTTSBelumPrint.aspx')" class="navmenu" type="button"></li>
                        </ul>
                    </li>
                    <li>
                        <i class="fa fa-file-text-o sb-ico"></i>
                        <input value="Kontrak" onclick="go(this, 'Kontrak.aspx'); opensub(this, 'subKontrak')" class="navmenu" type="button"><span class="fa fa-plus-square addons" onclick="opensub(this, 'subKontrak')"></span>
                        <ul class="nav-submenu" style="display: none" id="subKontrak">
                            <li>
                                <input value="Surat Pesanan" onclick="go(this, 'KontrakDaftar.aspx')" class="navmenu" type="button"></li>
                            <li>
                                <input value="Adjustment Kontrak" onclick="go(this, 'AdjustmentKontrak.aspx')" class="navmenu" type="button"></li>
                            <li>
                                <input value="Customize Tagihan" onclick="go(this, 'TagihanCustom.aspx')" class="navmenu" type="button"></li>
                            <li>
                                <input value="Reschedule Tagihan" onclick="go(this, 'TagihanReschedule.aspx')" class="navmenu" type="button"></li>
                            <%--<li>
                                <input value="PPJB" onclick="go(this, 'KontrakPPJB.aspx')" class="navmenu" type="button"></li>
                            <li>
                                <input value="AJB" onclick="go(this, 'KontrakAJB.aspx')" class="navmenu" type="button"></li>
                            <li>
                                <input value="Serah Terima" onclick="go(this, 'KontrakST.aspx')" class="navmenu" type="button"></li>
                            --%><li>
                                <input value="Pengalihan Hak" onclick="go(this, 'KontrakGantiNama.aspx')" class="navmenu" type="button"></li>
                            <li>
                                <input value="Pindah Unit" onclick="go(this, 'KontrakGantiUnit.aspx')" class="navmenu" type="button"></li>
                            <li>
                                <input value="Pembatalan Kontrak" onclick="go(this, 'KontrakBatal.aspx')" class="navmenu" type="button"></li>
                            <%--<li>
                                <input value="Realisasi Denda BAST" onclick="go(this, 'KontrakDendaBASTRealisasi.aspx')" class="navmenu" type="button"></li>
                            <li>
                                <input value="Pemutihan Denda BAST" onclick="go(this, 'KontrakDendaBASTPutih.aspx')" class="navmenu" type="button"></li>--%>
                        </ul>
                    </li>
                    <%--<li>
                        <i class="fa fa-file-text-o sb-ico"></i>
                        <input value="Migrate" onclick="opensub(this, 'subMigrate')" class="navmenu" type="button"><span class="fa fa-plus-square addons" onclick="opensub(this, 'subMigrate')"></span>
                        <ul class="nav-submenu" style="display: none" id="subMigrate">
                            <li>
                                <input value="Upload Kontrak" onclick="go(this, 'UploadKontrak.aspx')" class="navmenu" type="button"></li>
                            <li>
                                <input value="Approval Kontrak" onclick="go(this, 'MigrateKontrak.aspx')" style="display: none;" class="navmenu" type="button"></li>
                            <li>
                                <input value="Upload Jadwal Tagihan" onclick="go(this, 'UploadJadwal.aspx')" class="navmenu" type="button"></li>
                            <li>
                                <input value="Approval Jadwal Tagihan" onclick="go(this, 'MigrateJadwal.aspx')" style="display: none;" class="navmenu" type="button"></li>
                            <li>
                                <input value="Upload Pembayaran" onclick="go(this, 'UploadPembayaran.aspx')" class="navmenu" type="button"></li>
                            <li>
                                <input value="Approval Pembayaran" onclick="go(this, 'MigratePembayaran.aspx')" style="display: none;" class="navmenu" type="button"></li>
                        </ul>
                    </li>--%>
                    <li>
                        <i class="fa fa-file-o sb-ico"></i>
                        <input value="Log File" onclick="go(this, 'Log.aspx')" class="navmenu" type="button">
                    </li>
                </ul>
            </div>
        </div>
        <div class="pad" style="display: none">
            <p style="font: bold 8pt; color: gray; padding-left: 5; padding-bottom: 3">
                Sales
            </p>
            <table>
                <tr id="d8" style="display: none">
                    <%--    <td align="right">
							<input value="Generate Komisi..." onclick="go(this,'KomisiGen.aspx')" class="navmenu" type="button"
								onmouseover="over(this)" onmouseout="out(this)">
						</td>--%>
                </tr>
                <tr>
                    <td align="right"></td>
                </tr>
                <tr>
                    <td align="right">
                        <table cellspacing="0">
                            <tr>
                                <td>
                                    <img src="/Media/icon_out.gif">
                                </td>
                                <td>
                                    <input type="button" value="Sign-Out" class="nav" onclick="if (confirm('Apakah anda ingin melakukan sign-out?\nProgram dan absensi aktif anda akan ditutup.')) { top.location.href = 'SignOut.aspx' }"
                                        style="width: 65" onmouseover="over(this)" onmouseout="out(this)">
                                </td>
                                <td width="100%"></td>
                                <td>
                                    <img src="/Media/icon_gateway.gif">
                                </td>
                                <td>
                                    <input type="button" value="Gateway" class="nav" onclick="top.location.href = '/Gateway.aspx'"
                                        style="width: 65" onmouseover="over(this)" onmouseout="out(this)">
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <hr size="1" noshade style="color: #DCDCDC">
                        <table>
                            <tr>
                                <td style="font: 8pt">User
                                </td>
                                <td>:
                                </td>
                                <td style="font: 8pt">
                                    <% Response.Write(ISC064.Act.UserID); %>
                                </td>
                            </tr>
                            <tr>
                                <td style="font: 8pt">Sec. Level
                                </td>
                                <td>:
                                </td>
                                <td style="font: 8pt">
                                    <% Response.Write(ISC064.Act.SecLevel); %>
                                </td>
                            </tr>
                            <tr>
                                <td style="font: 8pt">IP Addr.
                                </td>
                                <td>:
                                </td>
                                <td style="font: 8pt">
                                    <% Response.Write(ISC064.Act.IP); %>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>

        <script type="text/javascript">
            function opensub(sender, id) {
                // console.log(id);
                var sub = document.getElementById(id);
                var parent = sender.parentElement.getElementsByTagName('span')[0];
                // console.log(parent);
                if (sub.style.display === 'none') {
                    sub.style.display = "block";
                    parent.className = "";
                    parent.className = "fa fa-minus-square addons"
                } else {
                    sub.style.display = "none";
                    parent.className = "";
                    parent.className = "fa fa-plus-square addons";
                }
            }
            function go(foo, href) {
                bold(foo);
                top.content.location.href = href;
            }
            function bold(foo) {
                for (i = 1; i < document.Form1.elements.length - 2; i++) {
                    document.Form1.elements[i].style.fontWeight = 'normal';
                }
                foo.style.fontWeight = 'bold';
            }
        </script>

    </form>
</body>
</html>

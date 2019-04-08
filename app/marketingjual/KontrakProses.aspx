<%@ Register TagPrefix="uc1" TagName="HeadKontrak" Src="HeadKontrak.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavKontrak" Src="NavKontrak.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.MARKETINGJUAL.KontrakProses" CodeFile="KontrakProses.aspx.cs" %>

<!DOCTYPE html>
<html>
<head>
    <title>Proses KPR</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Kontrak - Proses KPR">
    <script src="/Js/Common.js" type="text/javascript"></script>
</head>
<body onkeyup="if(event.keyCode==27) window.close();">
    <form id="Form1" method="post" runat="server">
        <div class="content-header">
            <uc1:NavKontrak ID="NavKontrak1" runat="server" Aktif="4"></uc1:NavKontrak>
        </div>
        <div class="tabdata">
            <div class="pad">
                <input style="display: none">
                <uc1:HeadKontrak ID="HeadKontrak1" runat="server"></uc1:HeadKontrak>
                <div style="float: left; width: 350px">
                    <table cellspacing="5">
                        <tr>
                            <td style="padding-left: 10px">
                                <p class="feed">
                                    <asp:Label ID="feed" runat="server"></asp:Label></p>
                            </td>
                        </tr>
                    </table>
                    <table cellspacing="5">
                        <tr>
                            <td>Bank</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="bank" runat="server" Width="250" CssClass="ddl" Enabled="false">
                                    <asp:ListItem>MANDIRI</asp:ListItem>
                                    <asp:ListItem>MANDIRI SYA</asp:ListItem>
                                    <asp:ListItem>NISP</asp:ListItem>
                                    <asp:ListItem>BCA</asp:ListItem>
                                    <asp:ListItem>BCA SYA</asp:ListItem>
                                    <asp:ListItem>BRI</asp:ListItem>
                                    <asp:ListItem>BRI SYA</asp:ListItem>
                                    <asp:ListItem>NIAGA</asp:ListItem>
                                    <asp:ListItem>BII</asp:ListItem>
                                    <asp:ListItem>BNI</asp:ListItem>
                                    <asp:ListItem>BNI SYA</asp:ListItem>
                                    <asp:ListItem>BAG</asp:ListItem>
                                    <asp:ListItem>PERMATA</asp:ListItem>
                                    <asp:ListItem>BUMI PUTERA</asp:ListItem>
                                    <asp:ListItem>TUNAI</asp:ListItem>
                                    <asp:ListItem>MEGA</asp:ListItem>
                                    <asp:ListItem>MEGA SYA</asp:ListItem>
                                    <asp:ListItem>BTN</asp:ListItem>
                                    <asp:ListItem>BTN SYA</asp:ListItem>
                                </asp:DropDownList></td>
                        </tr>
                    </table>
                </div>
                <div style="float: left">
                    <h2 class="title">WAWANCARA</h2>
                    <table cellspacing="5">
                        <tr>
                            <td>Status</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblStatusWawancara" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>Target Wawancara</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblTargetWawancara" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>Tgl. Wawancara</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblTglWawancara" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>Lokasi Wawancara</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblLokasiWawancara" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td valign="top">Keterangan Wawancara</td>
                            <td valign="top">:</td>
                            <td>
                                <asp:Label ID="lblKetWawancara" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>Nilai Pengajuan</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="nilaipengajuan" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                    <hr color="silver" noshade size="1">
                    <h2 class="title">OTS</h2>
                    <table cellspacing="5">
                        <tr>
                            <td>Status</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="statusots" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>Target OTS</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="targetots" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>Tgl. OTS</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="tglots" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>Hasil OTS</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="hasilots" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td valign="top">Keterangan OTS</td>
                            <td valign="top">:</td>
                            <td>
                                <asp:Label ID="ketots" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                    <hr color="silver" noshade size="1">
                    <h2 class="title">LPA</h2>
                    <table cellspacing="5">
                        <tr>
                            <td>Status</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblStatusLPA" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>Target LPA</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblTargetLPA" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>Tgl. LPA</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblTglLPA" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>No. LPA</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblNoLPA" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td valign="top">Keterangan LPA</td>
                            <td valign="top">:</td>
                            <td>
                                <asp:Label ID="lblKetLPA" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                    <hr color="silver" noshade size="1">
                    <h2 class="title">SP3K</h2>
                    <table cellspacing="5">
                        <tr>
                            <td>Status</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblStatusSP3K" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>Target SP3K</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblTargetSP3K" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>Tgl. Pengajuan SP3K</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblTglPengajuanSP3K" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>Tgl. Hasil SP3K</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblTglHasilSP3K" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>No. SP3K</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblNoSP3K" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>Hasil SP3K</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblHasilSP3K" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td valign="top">Keterangan SP3K</td>
                            <td valign="top">:</td>
                            <td>
                                <asp:Label ID="lblKetSP3K" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td valign="top">Nilai KPR Disetujui</td>
                            <td valign="top">:</td>
                            <td>
                                <asp:Label ID="approvalkpr" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td valign="top">Tambahan Uang Muka</td>
                            <td valign="top">:</td>
                            <td>
                                <asp:Label ID="tambahum" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                    <hr color="silver" noshade size="1">
                    <h2 class="title">AKAD</h2>
                    <table cellspacing="5">
                        <tr>
                            <td>Status</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblStatusAkad" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>Target Akad</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblTargetAkad" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>Tgl. Akad</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblTglAkad" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>No. Akad</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblNoAkad" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td valign="top">Keterangan Akad</td>
                            <td valign="top">:</td>
                            <td>
                                <asp:Label ID="lblKetAkad" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>Realisasi Akad</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="realisasiakad" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                    <hr color="silver" noshade size="1">
                    <h2 class="title" style="display: none">AJB</h2>
                    <table cellspacing="5" style="display: none">
                        <tr>
                            <td>Status</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblStatusAJB" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>Tgl. AJB</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblTglAJB" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>No. AJB</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblNoAJB" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>Notaris</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblNamaNotaris" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td valign="top">Keterangan AJB</td>
                            <td valign="top">:</td>
                            <td>
                                <asp:Label ID="lblKetAJB" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                    <h2 class="title">SERTIFIKAT</h2>
                    <table cellspacing="5">
                        <tr>
                            <td>Status</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblStatusSertifikat" runat="server"></asp:Label></td>
                        </tr>
                        <tr id="atasnama" runat="server">
                            <td>Atas Nama</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="namaperusahaan" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>Tgl. Sertifikat</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblTglSertifikat" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>No. Sertifikat</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblNoSertifikat" runat="server"></asp:Label></td>
                        </tr>
                        <tr id="sedangproses1" runat="server">
                            <td>Nomor Ukur</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="nomorukur" runat="server"></asp:Label></td>
                        </tr>
                        <tr id="sedangproses2" runat="server">
                            <td>Tanggal Pengukuran</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="tanggalukur" runat="server"></asp:Label></td>
                        </tr>
                        <tr id="sedangproses3" runat="server">
                            <td>No Surat Ukur</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="nosuratukur" runat="server"></asp:Label></td>
                        </tr>
                        <tr id="sedangproses4" runat="server">
                            <td>Tanggal Surat Ukur</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="tanggalsuratukur" runat="server"></asp:Label></td>
                        </tr>
                        <tr id="sedangproses5" runat="server">
                            <td>Jumlah Bidang</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="jumlahbidang" runat="server"></asp:Label></td>
                        </tr>
                        <tr id="sertifikat3" runat="server">
                            <td>Status Sertifikat</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="statushak" runat="server"></asp:Label></td>
                        </tr>
                        <tr id="sertifikat2" runat="server">
                            <td>Jangka Waktu</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="jangkawaktu" runat="server" CssClass="txt"></asp:Label>tahun</td>
                        </tr>
                        <tr id="sertifikat1" runat="server">
                            <td>Tgl. Berakhir Sertifikat</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="tglakhir" runat="server" CssClass="txt"></asp:Label></td>
                        </tr>
                    </table>
                    <hr color="silver" noshade size="1">
                    <h2 class="title">IMB</h2>
                    <table cellspacing="5">
                        <tr>
                            <td>Status</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="statusimb" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>Tgl. IMB</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="tglimb" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>No. IMB</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="noimb" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>Keterangan</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="keteranganimb" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                <br />
                <br />
                <br />
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript">
        function popDaftarBerkas(nomor) {
            openModal('/marketingjual/KontrakBerkas.aspx?NoKontrak=' + nomor, '770', '500');
        }

        function popFiksi(nomor) {
            openModal('/marketingjual/KontrakFiksi.aspx?NoKontrak=' + nomor, '770', '500');
        }
    </script>
</body>
</html>

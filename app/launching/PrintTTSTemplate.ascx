<%@ Control Language="c#" Inherits="ISC064.LAUNCHING.PrintTTSTemplate" CodeFile="PrintTTSTemplate.ascx.cs" %>
<div style="width: 100%; display: block;">
    <table width="100%" cellspacing="0" style="font-family: Calibri;">
        <tr>
            <td width="25%" style="font-size: 8pt; padding-left: 35px;">
                PT. AKR SURABAYA LAND CORPORINDO<br />
                <b>GEM City</b> Jl. Raya Gubeng No.44<br />
                Gubeng - Surabaya 60281<br />
                Tel : +6231 5012 999
            </td>
            <td width="25%" align="center" valign="middle" style="font-size: 12pt;">
                <b><u>TANDA TERIMA SEMENTARA</u></b>
            </td>
            <td valign="middle" style="width: 30%; font-size: 8pt; text-align: left;">
                No.
                <asp:Label ID="notts" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="font-size: 1pt; border-bottom: 2px solid black;">
                &nbsp;
            </td>
        </tr>
    </table>
    <table width="100%" cellspacing="0" style="font-family: Calibri;">
        <tr>
            <td colspan="4" style="font-size: 3pt;">
                &nbsp;
            </td>
            <td style="border-bottom: 1px solid black; border-left: 2px solid black; font-size: 3pt;">
            </td>
        </tr>
        <tr>
            <td width="20%" style="font-size: 8pt; padding-left: 20px;">
                <u>Sudah Terima Dari</u><br />
                <i>Received From</i>
            </td>
            <td width="5%" valign="middle" style="font-size: 8pt;">
                :
            </td>
            <td colspan="2" style="font-size: 8pt;">
                <b>
                    <asp:Label ID="nama" runat="server"></asp:Label></b>
            </td>
            <td width="40%" style="border-left: 2px solid black;">
                <table width="100%" cellspacing="0" style="font-size: 8pt;">
                    <tr>
                        <td width="50%" style="font-size: 8pt; padding-left: 5px;">
                            Tanggal Efektif Uang Diterima
                        </td>
                        <td width="1%" style="font-size: 8pt;">
                            :
                        </td>
                        <td width="49%" style="font-size: 8pt;">
                            <asp:Label ID="tgljt" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="50%" style="font-size: 8pt; padding-left: 5px;">
                            Tanggal Cetak kwitansi
                        </td>
                        <td width="1%" style="font-size: 8pt;">
                            :
                        </td>
                        <td width="49%" style="font-size: 8pt;">
                            <asp:Label ID="tglbkm" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                &nbsp;
            </td>
            <td style="border-left: 2px solid black;">
                <table width="100%" cellspacing="0" style="font-size: 8pt;">
                    <tr>
                        <td width="50%" style="font-size: 8pt; padding-left: 5px;">
                            Cluster
                        </td>
                        <td width="1%" style="font-size: 8pt;">
                            :
                        </td>
                        <td width="49%" style="font-size: 8pt;">
                            <asp:Label ID="Cluster" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="50%" style="font-size: 8pt; padding-left: 5px;">
                            Unit
                        </td>
                        <td width="1%" style="font-size: 8pt;">
                            :
                        </td>
                        <td width="49%" style="font-size: 8pt;">
                            <asp:Label ID="nounit" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td width="20%" style="font-size: 8pt; padding-left: 20px;">
                <u>Banyaknya Uang</u><br />
                <i>Amount Received</i>
            </td>
            <td width="5%" valign="middle" style="font-size: 8pt;">
                :
            </td>
            <td colspan="2" style="font-size: 8pt;">
                <b>
                    <asp:Label ID="nilai" runat="server"></asp:Label></b>
            </td>
            <td width="40%" style="border-left: 2px solid black;">
                <table width="100%" cellspacing="0" style="font-size: 8pt;">
                    <tr>
                        <td width="50%" style="font-size: 8pt; padding-left: 5px;">
                            No. Faktur Pajak
                        </td>
                        <td width="1%" style="font-size: 8pt;">
                            :
                        </td>
                        <td width="49%" style="font-size: 8pt;">
                            <asp:Label ID="nofp" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="50%" style="font-size: 8pt; padding-left: 5px;">
                            <u>Pembayaran via</u><br />
                            <i>Payment via</i>
                        </td>
                        <td width="1%" style="font-size: 8pt;">
                            :
                        </td>
                        <td width="49%" style="font-size: 8pt;">
                            <asp:Label ID="cb" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="font-size: 1pt; border-bottom: 2px solid black;">
                &nbsp;
            </td>
            <td style="font-size: 1pt; border-left: 2px solid black; border-bottom: 2px solid black;">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td width="20%" style="font-size: 8pt; padding-left: 20px;">
                <u>Terbilang</u><br />
                <i>Amounted</i>
            </td>
            <td width="5%" valign="middle" style="font-size: 8pt;">
                :
            </td>
            <td colspan="3" style="font-size: 8pt;">
                <b>
                    <asp:Label ID="terbilang" runat="server"></asp:Label></b>
            </td>
        </tr>
        <tr>
            <td colspan="5" style="font-size: 1pt; border-bottom: 2px solid black;">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="5" style="font-size: 8pt;">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="4" style="font-size: 30pt;">
                &nbsp;
            </td>
            <td align="center" style="font-size: 8pt; vertical-align: top;">
                Penerima Pesanan
            </td>
        </tr>
        <tr>
            <td colspan="4">
                &nbsp;
            </td>
            <td align="center" style="font-size: 8pt;">
                <u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="sign" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>
            </td>
        </tr>
        <tr>
            <td colspan="5" style="font-size: 7pt">
                <table width="100%" cellspacing="0" style="font-size: 8pt;">
                    <tr>
                        <td rowspan="4" width="5%" style="vertical-align: top; font-size: 8pt;">
                            Catatan :
                        </td>
                        <td width="1%" style="font-size: 8pt; vertical-align: top;">
                            *
                        </td>
                        <td width="87%" style="font-size: 8pt;">
                            Pembayaran dianggap sah jika uang yang di transfer masuk ke Rekening PT. AKR Surabaya
                            Land Corporindo
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: 8pt; vertical-align: top;">
                            *
                        </td>
                        <td width="87%" style="font-size: 8pt;">
                            Pemesan tunduk dan terkait pada syarat dan ketentuan umum atas pemesanan unit.
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: 8pt; vertical-align: top;">
                            *
                        </td>
                        <td width="87%" style="font-size: 8pt;">
                            Semua dana yang telah diterima tidak dapat dikembalikan dengan alasan apapun, termasuk
                            bila permohonan KPA tidak disetujui.
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: 8pt; vertical-align: top;">
                            *
                        </td>
                        <td width="87%" style="font-size: 8pt; text-align: justify;">
                            Pemesan wajib menandatangani SPU / SPPKT dalam 7 (tujuh) hari diterimanya dana,
                            bila belum ditandatangani dalam 7 (tujuh) hari kalender maka Penerima pesanan menganggap
                            pihak pemesan membatalkan pesanannya dan penerima pesanan dapat menjual kembali
                            unit tersebut kepada pihak lain. Dana Booking Fee yang sudah disetorkan menjadi
                            tidak dapat dikembalikan dan menjadi milik sepenuhnya PT. AKR Land Corporindo
                        </td>
                    </tr>
                </table>
                Lembar Putih:Pemesan &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Lembar Merah:Finance &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                Lembar Kuning:Tax &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Lembar Hijau:SAD
            </td>
        </tr>
    </table>
</div>

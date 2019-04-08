<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PrintBFormTemplate2.ascx.cs"
    Inherits="ISC064.MARKETINGJUAL.PrintBFormTemplate2" %>
<style type="text/css">
    table, td, u, span
    {
        font-size: 12px;
        font-family: Calibri;
    }
</style>
<table width="100%" align="center">
    <tr>
        <td align="right">
             <img src="/Media/logo_svs_tts.jpg" style="width:200px; height:150px" />
        </td>
    </tr>
</table>
<table width="100%" align="center">
    <tr>
        <td style="font-size:18;text-align:left;" colspan="3"><b>FORM RESERVED</b></td>
    </tr>
    <tr>
        <td style="font-size:14;" colspan="3"><b><asp:Label ID="pers" runat="server"></asp:Label></b></td>
    </tr>
    <tr>
        <td style="height:20px">&nbsp;</td>
    </tr>
    <tr>
        <td style="width:12%">Tanggal</td>
        <td style="width:1%">:</td>
        <td style="border-bottom:1px dotted black;width:auto;">
            <asp:Label ID="tgl" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="height:20px">&nbsp;</td>
    </tr>
</table>
<table width="100%" align="center">
    <tr>
        <td style="font-size:18;text-align:left;" colspan="3"><b>DATA KONSUMEN</b></td>
    </tr>
    <tr>
        <td style="height:20px">&nbsp;</td>
    </tr>
    <tr>
        <td style="width:12%">Nama Customer</td>
        <td style="width:1%">:</td>
        <td style="border-bottom:1px dotted black;width:auto;">
            <asp:Label runat="server" ID="namacs"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="width:12%">Alamat</td>
        <td style="width:1%">:</td>
        <td style="border-bottom:1px dotted black;width:auto;">
            <asp:Label runat="server" ID="alamatktp"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="width:12%"></td>
        <td style="width:1%"></td>
        <td style="border-bottom:1px dotted black;width:auto;">&nbsp;</td>
    </tr>
    <tr>
        <td style="width:12%">No.Tlp/Handphone</td>
        <td style="width:1%">:</td>
        <td style="border-bottom:1px dotted black;width:auto;">
            <asp:Label runat="server" ID="telphpfax"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="width:13%">Reservasi/Hold Unit</td>
        <td style="width:1%">:</td>
        <td style="width:auto;">
            <table>
                <tr>
                    <td>Jalan &nbsp;</td>
                    <td style="border-bottom:1px dotted black;width:auto;">
                        <asp:Label ID="jalan" runat="server" />
                    </td>
                    <td>No Unit &nbsp;</td>
                    <td style="border-bottom:1px dotted black;width:auto;">
                        <asp:Label ID="nounit1" runat="server" />
                    </td>
                    <td>Jenis &nbsp;</td>
                    <td style="border-bottom:1px dotted black;width:auto;">
                        <asp:Label ID="jenis" runat="server" />
                    </td>

                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td style="width:12%">Alasan</td>
        <td style="width:1%">:</td>
        <td style="border-bottom:1px dotted black;width:auto;">
            <asp:Label ID="alasan1" runat="server" />
        </td>
    </tr>
    <tr>
        <td style="width:13%">Periode Reservasi</td>
        <td style="width:1%">:</td>
        <td style="width:auto;">
            <table align="left">
                <tr>
                    <td style="border-bottom:1px dotted black;">
                        <asp:Label ID="tglres" runat="server" />
                    </td>
                    <td>&nbsp; s/d &nbsp;</td>
                    <td style="border-bottom:1px dotted black;">
                        <asp:Label ID="tgltarget" runat="server" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td style="width:12%">Uang Reserve</td>
        <td style="width:1%">:</td>
        <td style="border-bottom:1px dotted black;width:auto;">Rp.&nbsp;
            <asp:Label runat="server" ID="harganett"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="width:12%;height:20px">Pembayaran ke </td>
        <td style="width:1%">:</td>
        <td></td>
    </tr>
    <tr>
        <td style="width:12%;height:20px;padding-left:25px"><li/>Bank</td>
        <td style="width:1%">:</td>
        <td><asp:Label ID="bankacc" runat="server"></asp:Label></td>
    </tr>
    <%--<tr>
        <td style="width:12%;height:20px;padding-left:25px"><li/>No Rek.</td>
        <td style="width:1%">:</td>
        <td>477 0150502</td>
    </tr>--%>
</table>
<br />
<table width="100%" cellpadding="3" cellspacing="0" align="center">
    <tr>
        <td style="text-align:center;border:1px solid black">Pemohon,</td>
        <td style="text-align:center;border:1px solid black">Mengetahui,</td>
        <td style="text-align:center;border:1px solid black">Menyetujui,</td>
    </tr>
    <tr>
        <td style="text-align:center;border:1px solid black;height:60px;vertical-align:bottom">(......................)</td>
        <td style="text-align:center;border:1px solid black;vertical-align:bottom">
            <table width="100%">
                <tr>
                    <td style="width:50%;text-align:center">(Keuangan)</td>
                    <td style="width:50%;text-align:center">(Sales Admin)</td>
                </tr>
            </table>
        </td>
        <td style="text-align:center;border:1px solid black;vertical-align:bottom">
            <table width="100%">
                <tr>
                    <td style="width:50%;text-align:center">(Sales Manager)</td>
                    <td style="width:50%;text-align:center">(GM Sales)</td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<br />
<table width="100%" align="center">
    <tr>
        <td style="width:25px">Catatan</td>
        <td style="width:1px">:</td>
        <td><div style="font-size:12px">
            <i>1. Harus dilengkapi dengan bukti tanda pembayaran uang reserve ke rekening perusahaan.</i>
            </div>
        </td>
    </tr>
    <tr>
        <td></td>
        <td></td>
        <td><div style="font-size:12px">
            <i>2. Uang Reserved dapat dikembalikan apabila tidak terjadi transaksi pembelian.</i>
            </div>
        </td>
    </tr>
    <tr>
        <td></td>
        <td></td>
        <td><div style="font-size:12px">
            <i>3. Reserved unit hanyaberlaku 3 hari.</i>
            </div>
        </td>
    </tr>
</table>
<%--end of table--%>

<%--<table style="width: 100%;">
    <tr>
        <td style="width: 10%; font-size: 11px;">
            Keterangan
        </td>
        <td style="width: 20%; font-size: 11px;">
            Lembar 1 (Putih) : Pembeli
        </td>
        <td style="width: 20%; font-size: 11px;">
            Lembar 2 (Merah) : Marketing File
        </td>
        <td style="width: 30%; font-size: 11px;">
            Lembar 3 (Kuning) : Administration &amp; Finance
        </td>
        <td style="width: 20%; font-size: 11px;">
            Lembar 4 (Hijau) : Arsip
        </td>
    </tr>
</table>--%>

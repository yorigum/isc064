<%@ Control Language="c#" Inherits="ISC064.FINANCEAR.PrintTTSTemplate" CodeFile="PrintTTSTemplate.ascx.cs" %>

<style>
    .fontheader
    {
        font-size:18pt;
        font-family:'Times New Roman', Times, serif;
    }

    .fontisi
    {
        font-size:11pt;
        font-family:'Times New Roman', Times, serif;
    }
</style>

<div style="width: 100%">
    
    <table style="width:100%">
        <tr>
            <td style="text-align:left"><img src="/Media/logo_svs_tts.jpg" style="width:200px; height:150px" /></td>
            <td class="fontheader" style="text-align:center"><b>TANDA TERIMA SEMENTARA</b></td>
        </tr>
    </table>
    
    <table style="width: 100%">
        <tr>
            <td colspan="3" class="fontisi"><span>Terima dari :</span></td>
            <td class="fontisi" style="width: 20%">&nbsp;</td>
            <td class="fontisi" style="width: 5%">Nomor</td>
            <td class="fontisi" style="width: 2%">:</td>
            <td class="fontisi">
                <asp:Label ID="nobkm" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td class="fontisi" colspan="4">
                <asp:Label ID="namacs" runat="server"></asp:Label></td>
            <td class="fontisi">Tanggal</td>
            <td class="fontisi">:</td>
            <td class="fontisi">
                <asp:Label ID="tglbkm" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td class="fontisi" colspan="4">
                <asp:Label ID="alamat1" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td class="fontisi" colspan="4">
                <asp:Label ID="alamat2" runat="server"></asp:Label></td>
            <td class="fontisi">No. SP</td>
            <td class="fontisi">:</td>
            <td class="fontisi">
                <asp:Label ID="nosp" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td class="fontisi" colspan="4">
                <asp:Label ID="alamat3" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td class="fontisi" colspan="3">&nbsp;</td>
        </tr>
        <tr>
            <td class="fontisi" style="width: 13%">Uang Sejumlah</td>
            <td class="fontisi" style="width: 2%">:</td>
            <td class="fontisi" style="width: 30%">
                Rp. <asp:Label ID="nilainup" runat="server"></asp:Label> ,-</td>
        </tr>
        <tr>
            <td class="fontisi">Terbilang</td>
            <td class="fontisi">:</td>
            <td class="fontisi">
                <asp:Label ID="terbilangnilainup" runat="server"></asp:Label></td>
        </tr>
    </table>

    <hr />

    <table style="width: 100%">
        <tr>
            <td class="fontisi" colspan="6">Untuk Pembayaran : 
                <asp:Label ID="baya" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="fontisi" colspan="3"><b>Uang Tanda Jadi</b></td>
            <td style="width: 30%">&nbsp;</td>
            <td class="fontisi" style="width: 7%">Rp.</td>
            <td class="fontisi" style="text-align:right;">
                <asp:Label ID="dppnup" runat="server"></asp:Label></td>
        </tr>
        <tr id="hide1" runat="server">
            <td class="fontisi" style="width: 13%">Nomor Unit</td>
            <td class="fontisi" style="width: 2%">:</td>
            <td class="fontisi" style="width: 30%">
                <asp:Label ID="nounit" runat="server"></asp:Label></td>
        </tr>
        <tr id="hide2" runat="server">
            <td class="fontisi">Jalan</td>
            <td class="fontisi">:</td>
            <td class="fontisi">
                <asp:Label ID="jalan" runat="server"></asp:Label></td>
        </tr>
        <tr id="hide3" runat="server">
            <td class="fontisi">Cluster</td>
            <td class="fontisi">:</td>
            <td class="fontisi">
                <asp:Label ID="cluster" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td class="fontisi" colspan="3"><b>PPN</b></td>
            <td>&nbsp;</td>
            <td class="fontisi">Rp.</td>
            <td class="fontisi" style="text-align:right;">
                <asp:Label ID="ppnnup" runat="server"></asp:Label></td>
        </tr>
    </table>

    <hr />

    <table style="width: 100%">
        <tr>
            <td class="fontisi" style="width: 13%">Via</td>
            <td class="fontisi" style="width: 2%">:</td>
            <td class="fontisi" style="width: 55%">
                <asp:Label ID="bankacc" runat="server"></asp:Label></td>
            <td class="fontisi">Cikarang,
                <asp:Label ID="tglttd" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td class="fontisi">Tanggal</td>
            <td class="fontisi">:</td>
            <td class="fontisi" colspan="2">
                <asp:Label ID="tglbankacc" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td colspan="4" style="height:90px;">&nbsp;</td>
        </tr>
        <tr>
            <td colspan="3">&nbsp;</td>
            <td class="fontisi">.............................................</td>
        </tr>
    </table>
</div>
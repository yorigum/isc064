<%@ Control Language="c#" Inherits="ISC064.MARKETINGJUAL.PrintJadwalTagihanTemplate" CodeFile="PrintJadwalTagihanTemplate.ascx.cs" %>
<table cellspacing="0" width="100%">
    <tr>
        <td width="35%">
            <img src="/Media/LogoTriniti2.png" width="100px" height="100px" style="padding-left: 5px" /></td>
        <td align="center">
            <h3>LAMPIRAN<br />
                JADWAL PEMBAYARAN</h3>
        </td>
        <td width="35%">
            <img src="/Media/Final-01.jpg" width="160px" height="80px" align="right" style="padding-right: 5px" /></td>
    </tr>
    <tr>
        <td colspan="3" style="border-bottom: 3px solid black">&nbsp;</td>
    </tr>
</table>
<div style="padding-left: 10px;">

    <table cellspacing="5">
        <tr>
            <td>No. SP</td>
            <td>:</td>
            <td width="450" colspan="4">
                <asp:Label ID="NoKontrak1" runat="server" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>Nama</td>
            <td>:</td>
            <td width="450" colspan="4">
                <asp:Label ID="nama1" runat="server" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>Tower / Lantai / Unit</td>
            <td>:</td>
            <td width="450" colspan="4">
                <asp:Label ID="unit" runat="server" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>Harga Pengikatan</td>
            <td>:</td>
            <td width="450" colspan="4">
                <asp:Label ID="nilai" runat="server" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>Cara Pembayaran</td>
            <td>:</td>
            <td width="450" colspan="4">
                <asp:Label ID="carabayar" runat="server" Font-Bold="True"></asp:Label>
            </td>
        </tr>
    </table>
</div>
<div style="padding-left: 10px;">
    <asp:Table ID="rpt" runat="server" CssClass="blue-skin tb" CellSpacing="0" Width="100%">
        <asp:TableRow HorizontalAlign="Left">
            <asp:TableHeaderCell Width="100" Style="font-size:12pt"><b>No.</b></asp:TableHeaderCell>
            <asp:TableHeaderCell Width="200" Style="font-size:12pt"><b>Tagihan</b></asp:TableHeaderCell>
            <asp:TableHeaderCell Width="100" Style="font-size:12pt"><b>Jatuh Tempo</b></asp:TableHeaderCell>
            <asp:TableHeaderCell HorizontalAlign="Right" Style="font-size:12pt"><b>Nilai</b></asp:TableHeaderCell>
        </asp:TableRow>
    </asp:Table>
</div>
<br />
<div>
    <br />
    <div style="padding-left: 10px;">
        <table style="width: 90%;" cellpadding="0" cellspacing="0">
            <tr style="display: none;">
                <td style="font-size: 8pt; text-align: left">Depok,&nbsp;<asp:Label ID="tglnow" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td style="font-size: 8pt; width: 2%; text-align: center;">
                    &nbsp;
                </td>
                <td style="font-size: 8pt; width:30%; text-align: center;">Dibuat Oleh,</td>
                <td style="font-size: 8pt; width: 38%; text-align: center;">&nbsp;</td>
                <td style="font-size: 8pt; width: 30%; text-align: center;">Disetujui,</td>
            </tr>
            <tr style="height: 100px;"></tr>
            <tr>
                <td style="font-size: 8pt; width: 2%; text-align: center;">
                    &nbsp;
                </td>
                <td style="font-size: 8pt; width: 30%; text-align: center;border-top:solid 1px black">
                    Finance<%--<asp:Label ID="fin" runat="server"></asp:Label>--%>
                </td>
                <td style="font-size: 8pt; width: 38%; text-align: center;">
                    &nbsp;
                </td>
                <td style="font-size: 8pt; width: 30%; text-align: center;border-top:solid 1px black">
                    <asp:Label ID="pemesan" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
</div>

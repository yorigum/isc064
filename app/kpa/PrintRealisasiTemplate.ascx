<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PrintRealisasiTemplate.ascx.cs"
    Inherits="ISC064.KPA.PrintRealisasiTemplate" %>
<table width="100%">
    <tr>
        <td>
            <img src="/Media/logo.png" width="130px" />
        </td>
        <td>
            &nbsp;
        </td>
        <td width="40%">
            <table>
                <tr>
                    <td>
                        Nomor Realisasi
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:Label ID="no" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<center>
    <h3>
        Realisasi Tagihan KPA</h3>
</center>
<table width="100%">
    <tr>
        <td align="left">
            <table>
                <tr>
                    <td valign="top">
                        Tgl Realisasi
                    </td>
                    <td valign="top">
                        :
                    </td>
                    <td valign="top">
                        <asp:Label ID="tgl" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        Nilai
                    </td>
                    <td valign="top">
                        :
                    </td>
                    <td valign="top">
                        <asp:Label ID="nilai" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        Keterangan
                    </td>
                    <td valign="top">
                        :
                    </td>
                    <td valign="top">
                        <asp:Label ID="ket" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td>
            <asp:Table ID="rpt" CssClass="datatb" runat="server" Width="100%">
                <asp:TableHeaderRow>
                    <asp:TableHeaderCell>No. TTS</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Status</asp:TableHeaderCell>
                    <asp:TableHeaderCell>No. Kontrak</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Customer</asp:TableHeaderCell>
                    <asp:TableHeaderCell>No. Unit</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Nilai Realisasi</asp:TableHeaderCell>
                </asp:TableHeaderRow>
            </asp:Table>
        </td>
    </tr>
</table>

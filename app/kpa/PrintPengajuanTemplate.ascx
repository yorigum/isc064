<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PrintPengajuanTemplate.ascx.cs"
    Inherits="ISC064.KPA.PrintPengajuanTemplate" %>
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
                        Nomor Pengajuan
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
        Pengajuan Tagihan KPR</h3>
</center>
<table width="100%">
    <tr>
        <td align="left">
            <table>
                <tr>
                    <td valign="top">
                        Nomor Surat
                    </td>
                    <td valign="top">
                        :
                    </td>
                    <td valign="top">
                        <asp:Label ID="nosurat" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        Tgl Formulir
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
                        Tgl Rencana Cair
                    </td>
                    <td valign="top">
                        :
                    </td>
                    <td valign="top">
                        <asp:Label ID="tglcair" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        Status
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:Label ID="status" runat="server"></asp:Label>
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
                    <asp:TableHeaderCell>No. Kontrak</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Customer</asp:TableHeaderCell>
                    <asp:TableHeaderCell>No. Unit</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Nilai Pengajuan</asp:TableHeaderCell>
                </asp:TableHeaderRow>
            </asp:Table>
        </td>
    </tr>
</table>

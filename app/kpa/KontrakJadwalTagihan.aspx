<%@ Page Language="c#" Inherits="ISC064.KPA.KontrakJadwalTagihan" CodeFile="KontrakJadwalTagihan.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="HeadKontrak" Src="HeadKontrak.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavKontrakEdit" Src="NavKontrakEdit.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Jadwal Tagihan</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Kontrak - Jadwal Tagihan">
</head>
<body onkeyup="if(event.keyCode==27) window.close()">
    <form id="Form1" method="post" runat="server">
            <div class="content-header">
                <uc1:NavKontrakEdit ID="NavKontrak1" runat="server" Aktif="3"></uc1:NavKontrakEdit>
            </div>
    <div class="tabdata">
        <div class="pad">
            <uc1:HeadKontrak ID="HeadKontrak1" runat="server"></uc1:HeadKontrak>
            <table cellspacing="5">
                <tr>
                    <td>
                        <input type="button" id="edit" runat="server" value="Edit Tagihan" class="btn btn-blue" style="width: 100px"
                            name="edit" accesskey="e">
                    </td>
                    <td style="padding-left: 10px">
                        <p class="feed">
                            <asp:Label ID="feed" runat="server"></asp:Label>
                        </p>
                    </td>
                </tr>
            </table>
            <table cellspacing="5">
                <tr>
                    <td colspan="3">
                        Skema :
                        <asp:Label ID="skema" runat="server" Font-Bold="True" Font-Size="14"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="150">
                        Nilai Kontrak KPA
                    </td>
                    <td>
                        :
                    </td>
                    <td width="150" align="right">
                        <asp:Label ID="nilai" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="150">
                        Nilai Kelebihan Pencairan KPA
                    </td>
                    <td valign="top">
                        :
                    </td>
                    <td width="150" align="right" valign="top">
                        <asp:Label ID="nilailebih" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        Tagihan KPA
                    </td>
                    <td>
                        :
                    </td>
                    <td align="right">
                        <asp:Label ID="tagihankpa" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr id="outtr" runat="server">
                    <td align="right" colspan="3">
                        <font style="font-weight: normal; font-size: 8pt; line-height: normal; font-style: normal;
                            font-variant: normal">Selisih Kontrak KPA dengan Tagihan KPA (out of balance)</font>
                        <br>
                        <asp:Label ID="outofbalance" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr valign="top">
                    <td>
                        Realisasi (<asp:Label ID="persenlunas" runat="server" Font-Bold="True"></asp:Label>%)
                        <br>
                        <font style="font-weight: normal; font-size: 8pt; line-height: normal; font-style: normal;
                            font-variant: normal">Pembayaran yang sudah terealisasi</font>
                    </td>
                    <td>
                        :
                    </td>
                    <td align="right">
                        <asp:Label ID="pelunasan" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
            <br>
            <p style="padding-right: 3px; padding-left: 3px; font-weight: normal; font-size: 8pt;
                padding-bottom: 3px; line-height: normal; padding-top: 3px; font-style: normal;
                font-variant: normal">
                <asp:Label ID="tipe" runat="server"></asp:Label>
            </p>
            <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="0">
                <asp:TableRow HorizontalAlign="Left">
                    <asp:TableHeaderCell Width="100">No.</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Tipe</asp:TableHeaderCell>
                    <asp:TableHeaderCell Width="200">Tagihan</asp:TableHeaderCell>
                    <asp:TableHeaderCell Width="75">Jatuh Tempo</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Right">Nilai</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Right">Pelunasan</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Right">Sisa</asp:TableHeaderCell>
                </asp:TableRow>
            </asp:Table>
        </div>
    </div>
    </form>
</body>
</html>

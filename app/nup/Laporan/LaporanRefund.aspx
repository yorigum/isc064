<%@ Page Language="c#" Inherits="ISC064.NUP.Laporan.LaporanRefund" CodeFile="LaporanRefund.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <title>Laporan Refund NUP</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Refund NUP">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27&&confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
    <form id="Form1" method="post" runat="server">
        <div style="display: none">
            <uc1:Head ID="Head1" runat="server"></uc1:Head>
        </div>
        <table id="param" runat="server" width="100%" cellspacing="3">
            <tr>
                <td>
                    <div class="underline">
                        <p class="comp" id="comp" runat="server"></p>
                        <h1 id="judul" runat="server" class="title">Laporan Refund NUP
                        </h1>
                    </div>
                    <%--<div class="form-model">
                        <div class="form-inline col">
                            <div class="pparam">
                                <p class="lbl">Status :</p>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:RadioButton ID="statusS" Text="SEMUA" runat="server" GroupName="status"></asp:RadioButton>
                                            </td>
                                            <td>
                                                <asp:RadioButton ID="statusA" Text="AKTIF" runat="server" GroupName="status" Checked="True"></asp:RadioButton>
                                            </td>
                                            <td>
                                                <asp:RadioButton ID="statusI" Text="INAKTIF" runat="server" GroupName="status"></asp:RadioButton>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>--%>
                    <br>
                    <div class="form-inline col pparam sub">
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="scr" runat="server" Text="Screen Preview" CssClass="btn btn-blue" AccessKey="s" OnClick="scr_Click"></asp:Button>
                                    <asp:Button ID="xls" runat="server" Text="Download Excel" CssClass="btn btn-green" AccessKey="e" OnClick="xls_Click"></asp:Button>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
        <div id="headReport" runat="server">
        </div>
        <br />
        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1">
            <asp:TableRow VerticalAlign="Bottom">
                <asp:TableHeaderCell HorizontalAlign="Left">No.</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">No. NUP</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Nama</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">No. KTP</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Alamat</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Kota</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">No. HP</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Nilai Refund</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Bank</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">No. Rek</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Atas Nama</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Cabang</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>

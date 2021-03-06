<%@ Reference Page="~/Unit.aspx" %>

<%@ Page Language="c#" Inherits="ISC064.ADMINJUAL.UnitUploadZone" CodeFile="UnitUploadZone.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <title>Upload Zoning</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Unit - Upload Zoning">
    <style type="text/css">
        .sm {
            font: 8pt;
        }
    </style>
</head>
<body>
    <form id="Form1" method="post" runat="server" class="cnt">
        <input type="text" style="display: none">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1>Upload Zoning</h1>
        <br>
        <h2>Standard Pengisian</h2>
        <asp:Table ID="rule" runat="server" CssClass="blue-skin tb" CellSpacing="0">
            <asp:TableRow HorizontalAlign="Left">
                <asp:TableHeaderCell>No.</asp:TableHeaderCell>
                <asp:TableHeaderCell Width="150">Kolom</asp:TableHeaderCell>
                <asp:TableHeaderCell Width="75">Format</asp:TableHeaderCell>
                <asp:TableHeaderCell>Panjang</asp:TableHeaderCell>
                <asp:TableHeaderCell Width="350">Keterangan</asp:TableHeaderCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>1.</asp:TableCell>
                <asp:TableCell>No. Unit</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>20</asp:TableCell>
                <asp:TableCell CssClass="sm">Unit yang tidak terdaftar akan diabaikan</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>2.</asp:TableCell>
                <asp:TableCell>Zoning</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>100</asp:TableCell>
                <asp:TableCell CssClass="sm">Peruntukan usaha</asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <p style="border-bottom: 1px dashed gray; padding-bottom: 10px">
            <a href="Template\Zoning.xls">Download Template...</a>
        </p>
        <br>
        <table cellspacing="5">
            <tr>
                <td>File Excel</td>
                <td>:</td>
                <td>
                    <input type="file" id="file" runat="server" class="txt" style="width: 600px" name="file">
                </td>
            </tr>
        </table>
        <table style="height: 50px;">
            <tr>
                <td>
                    <asp:Button ID="upload" runat="server" CssClass="btn btn-blue" Width="75" Text="OK" OnClick="upload_Click"></asp:Button>
                </td>
                <td style="padding-left: 10px">
                    <p class="feed">
                        <asp:Label ID="feed" runat="server"></asp:Label>
                    </p>
                </td>
            </tr>
        </table>
        <br>
        <h2 style="border-top: 1px dashed gray; padding-top: 10px">Gagal Upload :</h2>
        <asp:Table ID="gagal" runat="server"></asp:Table>
    </form>
</body>
</html>

<%@ Reference Page="~/Peta.aspx" %>
<%@ Reference Page="~/Unit.aspx" %>

<%@ Page Language="c#" Inherits="ISC064.ADMINJUAL.PetaUploadKoord" CodeFile="PetaUploadKoord.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Upload Koordinat</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Setup Peta Floor Plan - Upload Koordinat">
    <style type="text/css">
        .sm {
            font: 8pt;
        }
    </style>
</head>
<body class="default-content">
    <form id="Form1" method="post" runat="server">
        <input type="text" style="display: none">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Upload Koordinat</h1>
        <br>
        <asp:dropdownlist runat="server" ID="project" AutoPostBack="true"></asp:dropdownlist>
        <h2>Standard Pengisian</h2>
        <asp:Table ID="rule" runat="server" CssClass="tb blue-skin" CellSpacing="1">
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
                <asp:TableCell>Peta</asp:TableCell>
                <asp:TableCell>RANGE</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>3.</asp:TableCell>
                <asp:TableCell>Koordinat</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>255</asp:TableCell>
                <asp:TableCell CssClass="sm">Lokasi unit tidak di-generate apabila format koordinat tidak sesuai</asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <p style="border-bottom: 1px dashed gray; padding-bottom: 10">
            <a href="Template\Koordinat.xls">Download Template...</a>
        </p>
        <br>
        <table cellspacing="5">
            <tr>
                <td>File Excel</td>
                <td>:</td>
                <td>
                    <input type="file" id="file" runat="server" class="txt" style="width: 600" name="file">
                </td>
            </tr>
        </table>
        <table height="50">
            <tr>
                <td>
                    <asp:LinkButton ID="upload" runat="server" CssClass="btn btn-blue" Width="75" OnClick="upload_Click">
                        <i class="fa fa-share"></i> OK
                    </asp:LinkButton>
                </td>
                <td style="padding-left: 10">
                    <p class="feed">
                        <asp:Label ID="feed" runat="server"></asp:Label>
                    </p>
                    <p class="feed2">
                        <asp:Label ID="feed2" runat="server"></asp:Label>
                    </p>
                </td>
            </tr>
        </table>
        <br>
        <h2 style="border-top: 1px dashed gray; padding-top: 10">Gagal Upload :</h2>
        <asp:Table ID="gagal" runat="server"></asp:Table>
    </form>
</body>
</html>

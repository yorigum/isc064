<%@ Reference Page="~/Unit.aspx" %>

<%@ Page Language="c#" Inherits="ISC064.ADMINJUAL.UnitUploadPL" CodeFile="UnitUploadPL.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <title>Upload Price List</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Unit - Upload Price List">
    <style type="text/css">
        .sm {
            font: 8pt;
        }
    </style>
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <input type="text" style="display: none">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Upload Price List</h1>
        <br>
        <asp:dropdownlist runat="server" ID="project"></asp:dropdownlist>
        <h2 class="sm">Standard Pengisian</h2>
        <asp:Table ID="rule" runat="server" CssClass="blue-skin tb" CellSpacing="1">
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
                <asp:TableCell>Price List Minimum</asp:TableCell>
                <asp:TableCell>ANGKA</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm">Batas bawah negosiasi marketing</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>3.</asp:TableCell>
                <asp:TableCell>Price List Rumah</asp:TableCell>
                <asp:TableCell>ANGKA</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>4.</asp:TableCell>
                <asp:TableCell>Price List Kavling</asp:TableCell>
                <asp:TableCell>ANGKA</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>5.</asp:TableCell>
                <asp:TableCell>Biaya BPHTB</asp:TableCell>
                <asp:TableCell>ANGKA</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>6.</asp:TableCell>
                <asp:TableCell>Biaya Surat</asp:TableCell>
                <asp:TableCell>ANGKA</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>7.</asp:TableCell>
                <asp:TableCell>Biaya Proses</asp:TableCell>
                <asp:TableCell>ANGKA</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>8.</asp:TableCell>
                <asp:TableCell>Biaya Lain-Lain</asp:TableCell>
                <asp:TableCell>ANGKA</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>9.</asp:TableCell>
                <asp:TableCell>Periode</asp:TableCell>
                <asp:TableCell>DATE</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <p style="border-bottom: 1px solid gray; padding-bottom: 10px">
            <a href="Template\PL1.xls">Download Template...</a>
        </p>
        <br>
        <table cellspacing="5">
            <tr>
                <td>File Excel</td>
                <td>:</td>
                <td>
                    <input type="file" id="file" runat="server" class="btn " style="width: 600px; text-align: left;" name="file">
                </td>
            </tr>
        </table>
        <table style="height: 50px;">
            <tr>
                <td>
                    <asp:LinkButton ID="upload" runat="server" CssClass="btn btn-blue" Width="75" OnClick="upload_Click">
                        <i class="fa fa-share"></i> OK
                    </asp:LinkButton>
                </td>
                <td style="padding-left: 10px">
                    <p class="feed">
                        <asp:Label ID="feed" runat="server"></asp:Label>
                    </p>
                </td>
            </tr>
        </table>
        <br>
        <h2 style="border-top: 1px solid gray; padding-top: 10px">Gagal Upload :</h2>
        <asp:Table ID="gagal" runat="server"></asp:Table>
    </form>
</body>
</html>

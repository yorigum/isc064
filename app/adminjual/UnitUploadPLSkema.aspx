<%@ Reference Page="~/Unit.aspx" %>

<%@ Page Language="c#" Inherits="ISC064.ADMINJUAL.UnitUploadPLSkema" CodeFile="UnitUploadPLSkema.aspx.cs" %>

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
    <meta name="sec" content="Unit - Upload Price List Skema">
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
        <h1 class="title title-line">Upload Price List per Skema</h1>
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
            <asp:tablerow>
					<asp:tablecell>1.</asp:tablecell>
					<asp:tablecell>No. Unit</asp:tablecell>
					<asp:tablecell>TEKS</asp:tablecell>
					<asp:tablecell>20</asp:tablecell>
					<asp:tablecell cssclass="sm">Unit yang tidak terdaftar akan diabaikan</asp:tablecell>
				</asp:tablerow>
				<asp:tablerow>
					<asp:tablecell>2.</asp:tablecell>
					<asp:tablecell>No. Skema</asp:tablecell>
					<asp:tablecell>ANGKA</asp:tablecell>
					<asp:tablecell></asp:tablecell>
					<asp:tablecell cssclass="sm">Nomor Skema yang sudah didaftarkan</asp:tablecell>
				</asp:tablerow>
				<asp:tablerow>
					<asp:tablecell>3.</asp:tablecell>
					<asp:tablecell>Price List Minimum</asp:tablecell>
					<asp:tablecell>ANGKA</asp:tablecell>
					<asp:tablecell></asp:tablecell>
					<asp:tablecell cssclass="sm">Harga Minimal Unit</asp:tablecell>
				</asp:tablerow>
				<asp:tablerow>
					<asp:tablecell>4.</asp:tablecell>
					<asp:tablecell>Price List</asp:tablecell>
					<asp:tablecell>ANGKA</asp:tablecell>
					<asp:tablecell></asp:tablecell>
					<asp:tablecell cssclass="sm">Harga Unit untuk setiap skema yang mengikat</asp:tablecell>
				</asp:tablerow>
        </asp:Table>
        <p style="border-bottom: 1px solid gray; padding-bottom: 10px">
            <a href="Template\PL2.xls">Download Template...</a>
        </p>
        <br>
        <table cellspacing="5">
            <tr>
                <td>File Excel</td>
                <td>:</td>
                <td>
                    <input type="file" id="file" runat="server" class="btn " style="width: 600px; text-align:left;" name="file">
                </td>
            </tr>
        </table>
        <table style="height:50px;">
            <tr>
                <td>
                    <asp:Button ID="upload" runat="server" CssClass="btn btn-blue" Width="75" Text="OK" OnClick="upload_Click"></asp:Button>
                </td>
                <td style="padding-left: 10px">
                    <p class="feed">
                        <asp:Label ID="feed" runat="server"></asp:Label>
                    </p>
                    <p class="feed">
                        <asp:Label ID="feed2" runat="server"></asp:Label>
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

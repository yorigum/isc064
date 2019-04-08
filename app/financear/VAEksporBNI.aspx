<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VAEksporBNI.aspx.cs" Inherits="ISC064.FINANCEAR.VAEksporBNI" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Virtual Account </title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Virtual Account - Export Data BNI">
</head>
<body class="body-padding">
    <form id="form1" runat="server">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Ekspor Data Virtual Account BNI</h1>
        <br />
        <div>
            <asp:Table ID="rpt" runat="server" CellSpacing="1" CssClass="tb blue-skin">
                <asp:TableRow HorizontalAlign="Left" VerticalAlign="Bottom">
                    <asp:TableHeaderCell>No.</asp:TableHeaderCell>
                    <asp:TableHeaderCell Width="110">NAMA CUSTOMER</asp:TableHeaderCell>
                    <asp:TableHeaderCell Width="100">KODE CUSTOMER</asp:TableHeaderCell>
                    <asp:TableHeaderCell Width="100">NO BNI VA</asp:TableHeaderCell>
                    <asp:TableHeaderCell Width="130">UNIT</asp:TableHeaderCell>
                </asp:TableRow>
            </asp:Table>
            <br />
            <asp:Button ID="excel" CssClass="btn btn-green" runat="server" Text="DOWNLOAD EXCEL" OnClick="excel_Click" />
        </div>
    </form>
</body>
</html>

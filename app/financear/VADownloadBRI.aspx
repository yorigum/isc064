<%@ Page Language="c#" Inherits="ISC064.FINANCEAR.VADownloadBRI" CodeFile="VADownloadBRI.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Virtual Account</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Virtual Account BRI - Export Data">
</head>
<body>
    <form id="form1" runat="server">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1>Export Data Virtual Account BRI</h1>
        <br />
        <div>
            <table class="datatb" border="1">
                <tr>
                    <th colspan="7">DATA TAGIHAN</th>
                </tr>
                <tr>
                    <th>No.</th>
                    <th>Tgl. Transaksi</th>
                    <th>Deskripsi</th>
                    <th>Keterangan</th>
                    <th>Amount</th>
                    <th>User Pembuku Transaksi</th>
                </tr>
                <asp:PlaceHolder ID="list2" runat="server"></asp:PlaceHolder>
            </table>
            <br />
            <asp:Button ID="save" Text="Export" OnClick="ExportCSV" runat="server" />

        </div>
    </form>
</body>
</html>

<%@ Page Language="c#" Inherits="ISC064.KOMISI.KomisiRegis2" CodeFile="KomisiRegis2.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Generate Komisi</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Komisi - Generate Komisi (Step 2)">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Generate Komisi</h1>
        <br>
        <table class="tb blue-skin" cellspacing="1">
            <tr align="left" valign="bottom">
                <th>No. Kontrak</th>
                <th>Unit</th>
                <th>Customer</th>
                <th>Sales</th>
                <th class="right">Nilai Komisi</th>
                <th class="right">Nilai CF</th>
            </tr>
            <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>
            <tr>
                <td colspan="6">
                    <asp:LinkButton ID="save" runat="server" CssClass="btn btn-blue" Width="75" OnClick="save_Click"><i class="fa fa-share"></i> OK
                    </asp:LinkButton>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

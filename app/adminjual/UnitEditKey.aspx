<%@ Page Language="c#" Inherits="ISC064.ADMINJUAL.UnitEditKey" CodeFile="UnitEditKey.aspx.cs" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>Edit Unit (Nomor Stock)</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Unit - Edit Unit (Nomor Stock)">
    <meta http-equiv="pragma" content="no-cache">
    <base target="_self">
</head>
<body class="pop" onkeyup="if(event.keyCode==27)window.close();"
    onload="document.getElementById('baru').select()">
    <form id="Form1" method="post" runat="server">
        <table cellspacing="5">
            <tr>
                <td>No. Stock</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="baru" runat="server" CssClass="txt" Width="150" MaxLength="20"></asp:TextBox>
                    <asp:Label ID="baruc" runat="server" CssClass="err"></asp:Label>
                </td>
            </tr>
        </table>
        <table style="height: 40px;">
            <tr>
                <td>
                    <asp:Button ID="save" runat="server" CssClass="btn btn-blue" Text="OK" Width="75" OnClick="save_Click"></asp:Button>
                </td>
                <td>
                    <input id="cancel" type="button" class="btn btn-red" value="Cancel" style="width: 75px" onclick="window.close()">
                </td>
            </tr>
        </table>
        <input type="text" style="display: none">
    </form>
</body>
</html>

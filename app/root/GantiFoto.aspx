<%@ Page Language="c#" Inherits="ISC064.GantiFoto" CodeFile="GantiFoto.aspx.cs" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>Ganti Gambar</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="2">
    <meta name="sec" content="Ganti Gambar">
    <meta http-equiv="pragma" content="no-cache">
    <base target="_self">
</head>
<body class="body body-padding pop" onkeyup="if(!document.getElementById('dariLogin').checked){if(event.keyCode==27)window.close()}">
    <script type="text/javascript" src="/Js/MD5.js"></script>
    <form id="Form1" method="post" runat="server">
        <h1 class="title title-line">Edit Foto</h1>
        <%--            <img id="foto" runat="server" style="width:200px;height:200px;border-radius:50%" src="" />  --%>
        <br />
        <br />
        <asp:Label runat="server">Upload Gambar : </asp:Label>
        <input type="file" id="file" class="txt" runat="server" style="width: 300px" name="file" />

        <%--        <table border="1" cellpadding="10">
            <tr>
                <td style="width: 500px"><asp:Label runat="server" style="width: 568px">Upload Gambar : </asp:Label></td>
                <td>:</td>
                <td>
                    <input type="file" id="file" class="txt" runat="server" style="width: 568px" name="file" />
                </td>
            </tr>
        </table>--%>
        <table style="height: 50px;">
            <tr>
                <td>
                    <asp:LinkButton ID="ok" runat="server" CssClass="btn btn-blue" Width="75" OnClick="ok_Click"><i class="fa fa-share"></i> OK</asp:LinkButton>
                </td>
                <td>
                    <input id="cancel" type="button" onclick="window.close()" runat="server" class="btn btn-red" value="Cancel" style="width: 75px">
                </td>
                <td style="padding-left: 10px">
                    <p class="feed">
                        <asp:Label ID="Label1" runat="server"></asp:Label>
                    </p>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

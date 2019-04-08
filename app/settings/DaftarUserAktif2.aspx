<%@ Page Language="c#" Inherits="ISC064.SETTINGS.DaftarUserAktif2" CodeFile="DaftarUserAktif2.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>List Active User</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="(Pop-Up) List Active User">
    <meta http-equiv="pragma" content="no-cache">
    <base target="_self">
</head>
<body class="pop body-padding" onkeyup="if(event.keyCode==27)window.close()">
    <form id="Form1" method="post" runat="server">
        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1">
            <asp:TableRow HorizontalAlign="Left">
                <asp:TableHeaderCell Width="250">Name</asp:TableHeaderCell>
                <asp:TableHeaderCell Width="100">ID</asp:TableHeaderCell>
                <asp:TableHeaderCell>Sec. Level</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
        <script language="javascript" type="text/javascript">
            function call(x, x2, ctrl1, ctrl2) {
                //window.returnValue = x + ";" + x2;
                window.opener.call(x, x2, ctrl1, ctrl2);
                window.close();
            }

        </script>
    </form>
</body>
</html>

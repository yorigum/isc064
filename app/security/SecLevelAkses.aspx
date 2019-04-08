<%@ Register TagPrefix="uc1" TagName="NavSecLevel" Src="NavSecLevel.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeadSecLevel" Src="HeadSecLevel.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.SECURITY.SecLevelAkses" CodeFile="SecLevelAkses.aspx.cs" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>Tabel Hak Akses</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Setup Security Level - Tabel Hak Akses">
</head>
<body onkeyup="if(event.keyCode==27) window.close()">
    <form id="Form1" method="post" runat="server">
        <div class="content-header">
            <uc1:NavSecLevel ID="NavSecLevel1" runat="server" Aktif="3"></uc1:NavSecLevel>
        </div>
        <div class="tabdata">
            <div class="pad">
                <uc1:HeadSecLevel ID="HeadSecLevel1" runat="server"></uc1:HeadSecLevel>
                <br>
                <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin">
                    <asp:TableRow HorizontalAlign="Left">
                        <asp:TableHeaderCell Width="400">Keterangan</asp:TableHeaderCell>
                        <asp:TableHeaderCell Width="160">Halaman</asp:TableHeaderCell>
                    </asp:TableRow>
                </asp:Table>
            </div>
        </div>
    </form>
</body>
</html>

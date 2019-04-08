<%@ Page Language="c#" Inherits="ISC064.KOMISI.KomisiEditTerm" CodeFile="KomisiEditTerm.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="HeadKom" Src="HeadKom.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavKom" Src="NavKom.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Edit Komisi (Termin)</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="5">
    <meta name="sec" content="Komisi - Edit Komisi (Termin)">
</head>
<body onkeyup="if(event.keyCode==27) window.close()">
    <form id="Form1" method="post" runat="server">
        <div class="content-header">
            <uc1:NavKom ID="NavKom" runat="server" Aktif="2"></uc1:NavKom>
        </div>
        <div class="tabdata">
            <div class="pad">
                <uc1:HeadKom ID="HeadKom" runat="server"></uc1:HeadKom>
                <asp:Table ID="tb" runat="server" CssClass="tb blue-skin" CellSpacing="1">
                    <asp:TableRow>
                        <asp:TableHeaderCell>No.</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Agent</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Nama</asp:TableHeaderCell>
                        <asp:TableHeaderCell CssClass="right">Nilai</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Status</asp:TableHeaderCell>
                        <asp:TableHeaderCell>No. Referensi</asp:TableHeaderCell>
                    </asp:TableRow>
                </asp:Table>
            </div>
        </div>
    </form>
</body>
</html>

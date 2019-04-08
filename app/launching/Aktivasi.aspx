<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Aktivasi.aspx.cs" Inherits="ISC064.LAUNCHING.Aktivasi" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Show Aktivation</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta http-equiv="Refresh" content="5">
    <meta name="ctrl" content="1">
    <meta name="sec" content="NUP - NNUP Aktivation">
</head>
<body onkeyup="if(event.keyCode==27) history.back(-1)">
    <uc1:Head ID="Head1" runat="server"></uc1:Head>
    <form id="form1" runat="server">
        <div style="width: 100%;" align="center">
            <h1 style="text-align:center">
                NUP
            </h1>
            <asp:Table ID="rpt" runat="server" CellPadding="5">
                <asp:TableFooterRow>
                    <asp:TableHeaderCell style="font-size:20pt;">No NUP</asp:TableHeaderCell>
                    <asp:TableHeaderCell style="font-size:20pt;">Keterangan</asp:TableHeaderCell>
                    <asp:TableHeaderCell style="font-size:20pt;">Tgl Aktivasi</asp:TableHeaderCell>
                </asp:TableFooterRow>
            </asp:Table>
        </div>
    </form>
</body>
</html>

<%@ Page Language="c#" Inherits="ISC064.ADMINJUAL.UnitHistori" CodeFile="UnitHistori.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="HeadUnit" Src="HeadUnit.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavUnit" Src="NavUnit.ascx" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <title>Histori Kontrak</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Unit - Histori Kontrak">
</head>
<body onkeyup="if(event.keyCode==27) window.close()">
    <form id="Form1" method="post" runat="server">
        <div class="content-header">
            <uc1:NavUnit ID="NavUnit1" runat="server" Aktif="3"></uc1:NavUnit>
        </div>
        <div class="tabdata">
            <div class="pad">
                <uc1:HeadUnit ID="HeadUnit1" runat="server"></uc1:HeadUnit>
                <div style="padding: 5px">
                    <p style="font: 8pt; padding-left: 3px">
                        Status : A = Aktif / B = Batal / E = Expire
                    </p>
                    <asp:Table ID="rpt" runat="server" CssClass="blue-skin tb" CellSpacing="1">
                        <asp:TableRow HorizontalAlign="Left">
                            <asp:TableHeaderCell Width="100px" ColumnSpan="2">No.</asp:TableHeaderCell>
                            <asp:TableHeaderCell Width="75px">Tgl.</asp:TableHeaderCell>
                            <asp:TableHeaderCell Width="150px">Customer</asp:TableHeaderCell>
                            <asp:TableHeaderCell Width="150px">Sales</asp:TableHeaderCell>
                            <asp:TableHeaderCell Width="120px">Keterangan</asp:TableHeaderCell>
                        </asp:TableRow>
                    </asp:Table>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

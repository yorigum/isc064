<%@ Page Language="c#" Inherits="ISC064.FINANCEAR.CustomerMEMO" CodeFile="CustomerMEMO.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="HeadCIF" Src="HeadCIF.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavCIF" Src="NavCIF.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Customer Information File (Memo Pelunasan)</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Customer Information File - Memo Pelunasan">
</head>
<body onkeyup="if(event.keyCode==27) window.close()">
    <form id="Form1" method="post" runat="server">
        <div class="content-header">
            <uc1:NavCIF ID="NavCIF1" runat="server" Aktif="4"></uc1:NavCIF>
        </div>
        <div class="tabdata">
            <div class="pad">
                <uc1:HeadCIF ID="HeadCIF1" runat="server"></uc1:HeadCIF>
                <br />
                <p style="padding: 3px; font: 8pt">
                    Tipe Memo : PP = Penghapusan Piutang / DN = Diskon / TG = Tukar Guling
                </p>
                <asp:Table ID="rpt" runat="server" CssClass="tb" CellSpacing="3">
                    <asp:TableRow HorizontalAlign="Left">
                        <asp:TableHeaderCell Width="60">No. Memo</asp:TableHeaderCell>
                        <asp:TableHeaderCell Width="110">Tgl. / Kasir</asp:TableHeaderCell>
                        <asp:TableHeaderCell Width="200">Keterangan</asp:TableHeaderCell>
                        <asp:TableHeaderCell Width="130">Tipe Memo</asp:TableHeaderCell>
                        <asp:TableHeaderCell Width="90" HorizontalAlign="Right">Total</asp:TableHeaderCell>
                    </asp:TableRow>
                </asp:Table>
            </div>
        </div>
    </form>
</body>
</html>

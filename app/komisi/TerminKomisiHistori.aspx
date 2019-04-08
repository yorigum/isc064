<%@ Page Language="c#" Inherits="ISC064.KOMISI.TerminKomisiHistori" CodeFile="TerminKomisiHistori.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="HeadSkomTermin" Src="HeadSkomTermin.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavSkomTermin" Src="NavSkomTermin.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>Histori Termin Komisi</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Termin - Histori Termin">
</head>
<body onkeyup="if(event.keyCode==27) window.close()">
    <form id="Form1" method="post" runat="server">
        <div class="content-header">
            <uc1:NavSkomTermin ID="NavSkomTermin" runat="server" Aktif="2"></uc1:NavSkomTermin>
        </div>
        <div class="tabdata">
            <div class="pad">
                <uc1:Head ID="Head" runat="server"></uc1:Head>
                <uc1:HeadSkomTermin ID="HeadSkomTermin" runat="server"></uc1:HeadSkomTermin>
                <br />
                <div style="padding: 5px">
                    <p style="font: 8pt; padding-left: 3px">
                        Status : A = Aktif / B = Batal / E = Expire
                    </p>
                    <asp:Table ID="rpt" runat="server" CssClass="blue-skin tb" CellSpacing="1">
                        <asp:TableRow HorizontalAlign="Left">
                            <asp:TableHeaderCell>No. Skema</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Nama</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Status</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Dari</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Sampai</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Rumus</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Dasar Perhitungan</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Cara Bayar</asp:TableHeaderCell>
                        </asp:TableRow>
                    </asp:Table>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

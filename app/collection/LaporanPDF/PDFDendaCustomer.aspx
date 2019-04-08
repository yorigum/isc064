<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.COLLECTION.Laporan.DendaCustomer" CodeFile="PDFDendaCustomer.aspx.cs" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>Laporan Denda Customer</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Denda Customer">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
    <form id="Form1" method="post" runat="server">
        <div style="display: none">
            <uc1:Head ID="Head1" runat="server"></uc1:Head>
        </div>
        <table id="param" cellspacing="3" width="100%" runat="server">
            <tr>
                <td>
                    <p class="comp" id="comp" runat="server"></p>
                    <h1 id="judul" runat="server" class="title title-line">Laporan Denda Customer
                    </h1>
                </td>
            </tr>
        </table>
        <div id="headReport" runat="server">
        </div>
        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1">
            <asp:TableRow VerticalAlign="Middle">
                <asp:TableHeaderCell HorizontalAlign="center" Wrap="false" BackColor="#1E90FF" ForeColor="White">No.</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" Wrap="false" BackColor="#1E90FF" ForeColor="White">No. Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" Wrap="false" BackColor="#1E90FF" ForeColor="White">Customer</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" Wrap="false" BackColor="#1E90FF" ForeColor="White">Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" Wrap="false" BackColor="#1E90FF" ForeColor="White">Nilai Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" Wrap="false" BackColor="#1E90FF" ForeColor="White">Tagihan</asp:TableHeaderCell>                
                <asp:TableHeaderCell HorizontalAlign="center" Wrap="false" BackColor="#1E90FF" ForeColor="White">Telat</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" Wrap="false" BackColor="#1E90FF" ForeColor="White">Denda</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" Wrap="false" BackColor="#1E90FF" ForeColor="White">Benefit</asp:TableHeaderCell>                
                <asp:TableHeaderCell HorizontalAlign="center" Wrap="false" BackColor="#1E90FF" ForeColor="White">Realisasi Benefit</asp:TableHeaderCell>                  
                <asp:TableHeaderCell HorizontalAlign="center" Wrap="false" BackColor="#1E90FF" ForeColor="White">Realisasi Denda</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" Wrap="false" BackColor="#1E90FF" ForeColor="White">Putih Denda</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" Wrap="false" BackColor="#1E90FF" ForeColor="White">Saldo Denda</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>

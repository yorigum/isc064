<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PDFNUPBelumBayar.aspx.cs" Inherits="ISC064.NUP.Laporan.NUPBelumBayar" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>

<!DOCTYPE html>
<html>
<head>
    <title>Laporan NUP Belum Bayar</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan NUP Belum Bayar">
</head>
<body onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
    <form id="Form1" method="post" runat="server">
      
        <table id="param" runat="server" width="100%" cellspacing="3">
            <tr>
                <td>
                    <p class="comp" id="comp" runat="server"></p>
                    <h1 id="judul" class="title title-line" runat="server">Laporan NUP Belum Bayar
                    </h1>

                </td>
            </tr>
        </table>
        <div id="headReport" runat="server"></div>
        <br />
        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1">
            <asp:TableRow VerticalAlign="Bottom">
                <asp:TableHeaderCell HorizontalAlign="center"  BackColor="#1E90FF" ForeColor="White">No.</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center"  BackColor="#1E90FF" ForeColor="White">Tgl NUP</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center"  BackColor="#1E90FF" ForeColor="White">Nomor NUP & Revisi</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center"  BackColor="#1E90FF" ForeColor="White">Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center"  BackColor="#1E90FF" ForeColor="White">Nama Pemesan</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center"  BackColor="#1E90FF" ForeColor="White">Alamat Pemesan</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center"  BackColor="#1E90FF" ForeColor="White">No Telp</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center"  BackColor="#1E90FF" ForeColor="White">Nama Sales/Agent</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center"  BackColor="#1E90FF" ForeColor="White">HP Sales/Agent</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center"  BackColor="#1E90FF" ForeColor="White">Type</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center"  BackColor="#1E90FF" ForeColor="White">Nama & NoRek Refund</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>
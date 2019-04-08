<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.KPA.Laporan.MasterPiutangBank" CodeFile="PDFMasterPiutangBank.aspx.cs" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>Laporan Master Piutang Bank</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Master Piutang Bank">
</head>
<body style="padding: 10px;" onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
    <form id="Form1" method="post" runat="server">
        <div style="display: none">
            <uc1:Head ID="Head1" runat="server"></uc1:Head>
        </div>
        <table id="param" runat="server" width="100%" cellspacing="3">
            <tr>
                <td>
                    <p class="comp" id="comp" runat="server"></p>
                    <h1 id="judul" runat="server" class="title title-line">Laporan Master Piutang Bank
                    </h1>
                </td>
            </tr>
        </table>
        <div id="headReport" runat="server">
        </div>
        <br />
        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1">
            <asp:TableRow>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">No. Tagihan</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">Status</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">Tgl. Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">Customer</asp:TableHeaderCell>
                <%--<asp:TableHeaderCell HorizontalAlign="Left" Wrap="false">No.Telp</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false">No.Hp</asp:TableHeaderCell>--%>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">Tipe</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">Tagihan</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">Jatuh Tempo</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Wrap="false" BackColor="#1E90FF" ForeColor="White">Nilai Tagihan</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Wrap="false" BackColor="#1E90FF" ForeColor="White">Pengajuan</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="left" Wrap="false" BackColor="#1E90FF" ForeColor="White">Pencairan</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Wrap="false" BackColor="#1E90FF" ForeColor="White">Sisa Tagihan</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>

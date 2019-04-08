<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.COLLECTION.Laporan.DaftarTagihan" CodeFile="PDFDaftarTagihan.aspx.cs" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>Laporan Daftar Piutang</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Daftar Piutang">
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
                    <h1 id="judul" runat="server" class="title title-line">Laporan Daftar Piutang
                    </h1>
                </td>
            </tr>
        </table>
        <div id="headReport" runat="server">
        </div>
			<asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1">
				<asp:tablerow verticalalign="Middle">
					<asp:tableheadercell horizontalalign="Center" backcolor="#1E90FF" forecolor="white" rowspan="2">No.</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Center" backcolor="#1E90FF" forecolor="white" rowspan="2">Nama</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Center" backcolor="#1E90FF" forecolor="white" rowspan="2">No. Unit</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Center" backcolor="#1E90FF" forecolor="white" rowspan="2">Harga Kesepakatan</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Center" backcolor="#1E90FF" forecolor="white" rowspan="2">PPN</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Center" backcolor="#1E90FF" forecolor="white" rowspan="2">Total Harga</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Center" backcolor="#1E90FF" forecolor="white" rowspan="2">Pembayaran</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Center" backcolor="#1E90FF" forecolor="white" rowspan="2">Piutang</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Center" backcolor="#1E90FF" forecolor="white" rowspan="2" ID="tradmin" runat="server" Visible="false">Biaya Admin</asp:tableheadercell>
					<asp:tableheadercell backcolor="#1E90FF" forecolor="white" columnspan="7">KELOMPOK UMUR TAGIHAN</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Center" backcolor="#1E90FF" forecolor="white" rowspan="2">Jangka Pendek</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Center" backcolor="#1E90FF" forecolor="white" rowspan="2">Jangka Panjang</asp:tableheadercell>
				</asp:tablerow>
				<asp:tablerow verticalalign="Middle">
				    <asp:tableheadercell backcolor="#1E90FF" forecolor="white">Belum Jatuh Tempo</asp:tableheadercell>
					<asp:tableheadercell backcolor="#1E90FF" forecolor="white">0 - 30 hari</asp:tableheadercell>
					<asp:tableheadercell backcolor="#1E90FF" forecolor="white">31 - 60 hari</asp:tableheadercell>
					<asp:tableheadercell backcolor="#1E90FF" forecolor="white">61 - 90 hari</asp:tableheadercell>
					<asp:tableheadercell backcolor="#1E90FF" forecolor="white">91 - 120 hari</asp:tableheadercell>
					<asp:tableheadercell backcolor="#1E90FF" forecolor="white">121 - 180 hari</asp:tableheadercell>
					<asp:tableheadercell backcolor="#1E90FF" forecolor="white">>181 hari</asp:tableheadercell>
				</asp:tablerow>
				
			</asp:table>
    </form>
</body>
</html>

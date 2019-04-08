<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<%@ Page language="c#" Inherits="ISC064.FINANCEAR.Laporan.LaporanKasir" CodeFile="PDFLaporanKasir.aspx.cs" %>
<!DOCTYPE html>
<HTML>
	<HEAD>
		<title>Laporan Kasir</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Report.css" type="text/css" rel="stylesheet">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Laporan Kasir">
	</HEAD>
	<body class="body-padding" onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
		<form id="Form1" method="post" runat="server">
			<div style="DISPLAY:none"><uc1:head id="Head1" runat="server"></uc1:head></div>
			<table id="param" runat="server" width="100%" cellspacing="3">
				<tr>
					<td>
                        <div class="underline">
						    <p class="comp" id="comp" runat="server"></p>
						    <h1 id="judul" runat="server" class="title">
							    Laporan Kasir
						    </h1>
                        </div>
					</td>
				</tr>
			</table>
            <div id="headReport" runat="server"></div>
            <br />
			<asp:table id="rpt" runat="server" cssclass="tb blue-skin" cellspacing="1">
				<asp:tablerow verticalalign="Bottom" cssclass="tb blue-skin">
					<asp:tableheadercell horizontalalign="Left" Wrap="false">No. TTS</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left" Wrap="false">Tgl. TTS</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left" Wrap="false">No. KWT</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left" Wrap="false">Tgl. KWT</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left" Wrap="false">No Kontrak</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left" Wrap="false">Unit</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left" Wrap="false">Customer</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left" Wrap="false">Cara Bayar</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left" Wrap="false">Keterangan</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left" Wrap="false">No. BG</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left" Wrap="false">Tgl. BG</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left" Wrap="false">Rekening</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left" Wrap="false">Bank</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Right" Wrap="false">Pelunsan Piutang</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Right" Wrap="false">Pembulatan</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Right" Wrap="false">Total Pembayaran</asp:tableheadercell>
				</asp:tablerow>
			</asp:table>
		</form>
	</body>
</HTML>

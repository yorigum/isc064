<%@ Reference Page="~/Unit.aspx" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.Laporan.SummaryPenjualanPerLantai" CodeFile="SummaryPenjualanPerLantai.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Laporan Summary Penjualan Per Tower</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Report.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Laporan Summary Penjualan Per Tower">
	</head>
	<body onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
		<form id="Form1" method="post" runat="server">
			<div style="DISPLAY:none"><uc1:head id="Head1" runat="server"></uc1:head></div>
			<table id="param" runat="server" width="100%" cellspacing="3">
				<tr>
					<td>
						<p class="comp" id="comp" runat="server"></p>
						<h1 id="judul" runat="server">
							Laporan Summary Penjualan Per Tower
						</h1>
						<p class="pparam">
							<asp:radiobutton id="tglkontrak" runat="server" text="Tanggal Kontrak" font-bold="True" font-size="10"
								groupname="tgl" checked="True"></asp:radiobutton>
							:
						</p>
						<table>
							<tr>
								<td>dari</td>
								<td>
									<asp:textbox id="dari" runat="server" width="85" cssclass="txt_center"></asp:textbox>
									<label for="dari" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
								</td>
								<td rowspan="2">&nbsp;&nbsp;</td>
								<td>sampai</td>
								<td>
									<asp:textbox id="sampai" runat="server" width="85" cssclass="txt_center"></asp:textbox>
									<label for="sampai" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
								</td>
							</tr>
							<tr>
								<td colspan="3"><asp:label id="daric" runat="server" cssclass="err"></asp:label></td>
								<td colspan="3"><asp:label id="sampaic" runat="server" cssclass="err"></asp:label></td>
							</tr>
						</table>
						<br />
						<div class="ins">
							<table>
								<tr>
									<td>
										<asp:button id="scr" accesskey="s" runat="server" text="Screen Preview" width="100" cssclass="btn" onclick="scr_Click"></asp:button>
										<asp:button id="xls" accesskey="e" runat="server" text="Download Excel" width="100" cssclass="btn" onclick="xls_Click"></asp:button>
									</td>
								</tr>
							</table>
						</div>
					</td>
				</tr>
			</table>
			<asp:table id="rpt" runat="server" cssclass="tb" cellspacing="3">
				<asp:tablerow>
					<asp:tablecell columnspan="8" font-size="8pt">
						Luas dalam meter persegi.
					</asp:tablecell>
				</asp:tablerow>
				<asp:tablerow verticalalign="Bottom">
					<asp:tableheadercell horizontalalign="Left">Tower</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Right">Total Unit Terjual</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Right">Nilai Transaksi</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Right">Total Luas</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Right">Total Diskon</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Right">Sudah TTS</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Right">Sudah Cair</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Right">Sisa Outstanding</asp:tableheadercell>
				</asp:tablerow>
			</asp:table>
		</form>
	</body>
</html>

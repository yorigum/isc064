<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.Laporan.LaporanCollection" CodeFile="LaporanCollection.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Laporan Collection</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Report.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Laporan Collection">
	</head>
	<body onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
		<form id="Form1" method="post" runat="server">
			<div style="DISPLAY:none"><uc1:head id="Head1" runat="server"></uc1:head></div>
			<table id="param" runat="server" width="100%" cellspacing="3">
				<tr>
					<td>
						<p class="comp" id="comp" runat="server"></p>
						<h1 id="judul" runat="server">
							Laporan Hasil Collection
						</h1>
						<table cellspacing="0" cellpadding="0">
							<tr valign="top">
								<td>
									<p class="pparam">
										<b>Lokasi :</b>
										<br>
										<asp:listbox id="lokasi" runat="server" width="200" cssclass="ddl" rows="10">
											<asp:listitem>SEMUA</asp:listitem>
										</asp:listbox>
									</p>
								</td>
								<td width="20"></td>
								<td>
									<p class="pparam">
										<b>Status :</b>
										<asp:radiobutton id="statusS" runat="server" groupname="status" font-size="14" text="SEMUA"></asp:radiobutton>
										<asp:radiobutton id="statusA" runat="server" groupname="status" font-size="14" text="AKTIF" checked="True"></asp:radiobutton>
										<asp:radiobutton id="statusB" runat="server" groupname="status" font-size="14" text="BATAL"></asp:radiobutton>
									</p>
									<p class="pparam">
										As of:
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
								</td>
							</tr>
						</table>
						<br>
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
					<asp:tablecell columnspan="12" font-size="8pt">
						Status : A = Aktif / B = Batal.<br>
						Luas dalam meter persegi. Gross adalah harga sebelum diskon.
					</asp:tablecell>
				</asp:tablerow>
				<asp:tablerow verticalalign="Bottom">
					<asp:tableheadercell horizontalalign="Left" rowspan="2">No. Surat Pesanan</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left" rowspan="2">Tgl Surat Pesanan</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left" rowspan="2">Unit</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left" rowspan="2">Customer</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left" rowspan="2">Sales</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left" rowspan="2">Nett Price</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left" rowspan="2">Tagihan</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left" rowspan="2">Tgl Jatuh Tempo</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Center" columnspan="3">Billing</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Right" rowspan="2">Tgl Lunas</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Center" columnspan="2">Settlement</asp:tableheadercell>
				</asp:tablerow>
			</asp:table>
			<asp:label id="total1" runat="server" visible="False">0</asp:label>
			<asp:label id="total2" runat="server" visible="False">0</asp:label>
			<asp:label id="total3" runat="server" visible="False">0</asp:label>
			<asp:label id="total4" runat="server" visible="False">0</asp:label>
			<asp:label id="total5" runat="server" visible="False">0</asp:label>
		</form>
	</body>
</html>

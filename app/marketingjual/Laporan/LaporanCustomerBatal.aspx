<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.Laporan.LaporanCustomerBatal" CodeFile="LaporanCustomerBatal.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Laporan Data Customer Batal Unit</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Report.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Laporan Data Customer Batal Unit">
	</head>
	<body onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
		<form id="Form1" method="post" runat="server">
			<div style="DISPLAY:none"><uc1:head id="Head1" runat="server"></uc1:head></div>
			<table id="param" runat="server" width="100%" cellspacing="3">
				<tr>
					<td>
						<p class="comp" id="comp" runat="server"></p>
						<h1 id="judul" runat="server" class="title title-line">
							Laporan Pembatalan Unit
						</h1>
						<table cellspacing="0" cellpadding="0">
							<tr valign="top">
								<td>
								</td>
								<td>
									<p class="pparam">
										<strong>Tgl. Batal:</strong>
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
									</p>
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
            <div id="headReport" runat="server"></div>
            <br />
			<asp:table id="rpt" runat="server" cssclass="tb" cellspacing="3">
				<asp:tablerow verticalalign="Bottom">
					<asp:tableheadercell horizontalalign="Center">No.</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Tgl. Batal</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">No. Surat</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Nama</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Unit Batal</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Sales</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Alasan Batal</asp:tableheadercell>
				</asp:tablerow>
			</asp:table>
		</form>
	</body>
</html>

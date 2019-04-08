<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.Laporan.LaporanPPNUangMuka" CodeFile="LaporanPPNUangMuka.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Laporan Penerimaan PPN Uang Muka</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Report.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Laporan Penerimaan PPN Uang Muka">
	</HEAD>
	<body onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
		<form id="Form1" method="post" runat="server">
			<div style="DISPLAY:none"><uc1:head id="Head1" runat="server"></uc1:head></div>
			<table id="param" runat="server" width="100%" cellspacing="3">
				<tr>
					<td>
						<p class="comp" id="comp" runat="server"></p>
						<h1 id="judul" runat="server">
							Laporan Penerimaan PPN Uang Muka
						</h1>
						<table cellpadding="0" cellspacing="0">
							<tr valign="top">
								<td>
									<p class="pparam">
										<strong>Tanggal:</strong>
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
								<td width="20"></td>
							</tr>
							<tr>
								<td>
									<p class="pparam">
										<strong>PPN Ditanggung:</strong>
										<br>
										<asp:radiobuttonlist id="JenisPPN" runat="server">
											<asp:listitem selected="True">PEMERINTAH</asp:listitem>
											<asp:listitem>KONSUMEN</asp:listitem>
										</asp:radiobuttonlist>
									</p>
								</td>
							</tr>
						</table>
						<br>
						<div class="ins">
							<table>
								<tr>
									<td>
										<asp:button id="scr" runat="server" text="Screen Preview" cssclass="btn" width="100" accesskey="s" onclick="scr_Click"></asp:button>
										<asp:button id="xls" runat="server" text="Download Excel" cssclass="btn" width="100" accesskey="e" onclick="xls_Click"></asp:button>
									</td>
								</tr>
							</table>
						</div>
					</td>
				</tr>
			</table>
			<asp:table id="rpt" runat="server" cssclass="tb" cellspacing="3">
				<asp:tablerow>
					<asp:tablecell columnspan="10" font-size="8pt">
						&nbsp;
					</asp:tablecell>
				</asp:tablerow>
				<asp:tablerow verticalalign="Bottom">
					<asp:tableheadercell horizontalalign="Center">No.</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Tgl</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Kwitansi</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">No. Unit</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Nama Tagihan</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Right">Jumlah Penerimaan</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Right">PPN Bangunan</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Right">Jumlah PPN</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Right">Penerimaan Bersih</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">No. Faktur</asp:tableheadercell>
				</asp:tablerow>
			</asp:table>
		</form>
	</body>
</HTML>

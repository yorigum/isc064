<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.Laporan.LaporanPenjualan" CodeFile="LaporanPenjualan.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Laporan Kartu Piutang</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Report.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Laporan Sales">
	</HEAD>
	<body onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
		<form id="Form1" method="post" runat="server">
			<div style="DISPLAY:none"><uc1:head id="Head1" runat="server"></uc1:head></div>
			<table id="param" runat="server" width="100%" cellspacing="3">
				<tr>
					<td>
						<p class="comp" id="comp" runat="server"></p>
						<h1 id="judul" runat="server">
							Laporan Kartu Piutang
						</h1>
						<table cellpadding="0" cellspacing="0">
							<tr valign="top">
								<td>
									<p class="pparam">
										<strong>No. Kontrak</strong>
										<br>
										Dari
										<asp:textbox id="dari" runat="server" width="85" cssclass="txt_center" readonly="True"></asp:textbox><input type="button" value="..." onclick="popDaftarKontrak('dari');" class="btn">
										<asp:label id="daric" runat="server" cssclass="err"></asp:label>
										&nbsp;&nbsp; Sampai
										<asp:textbox id="sampai" runat="server" width="85" cssclass="txt_center" readonly="True"></asp:textbox><input type="button" value="..." onclick="popDaftarKontrak('sampai');" class="btn">
										<asp:label id="sampaic" runat="server" cssclass="err"></asp:label>
									</p>
								</td>
								<td width="20"></td>
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
			<asp:placeholder id="report" runat="server"></asp:placeholder>
			<asp:table id="rpt" runat="server"></asp:table>
			<script language="javascript" type="text/javascript">
			function callSource(nokontrak, source)
			{
				document.getElementById(source).value = nokontrak;
			}
			</script>
		</form>
	</body>
</HTML>

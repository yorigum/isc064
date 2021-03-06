﻿<%@ Page language="c#" Inherits="ISC064.LEGAL.DaftarIMB" CodeFile="DaftarIMB.aspx.cs" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Daftar IMB</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="(Pop-Up) Daftar IMB">
		<meta http-equiv="pragma" content="no-cache">
		<base target="_self">
	</head>
	<body class="body-padding pop" onkeyup="if(event.keyCode==27){window.close()}"
		onload="document.getElementById('keyword').select()">
		<form id="Form1" method="post" runat="server">
			<div class="pad">
				<table>
					<tr>
						<td>
							<asp:textbox id="keyword" runat="server" cssclass="txt" width="400"></asp:textbox>
						</td>
						<td>
							<asp:dropdownlist id="metode" runat="server" cssclass="txt">
								<asp:listitem>Semua</asp:listitem>
								<asp:listitem>Kontrak Aktif</asp:listitem>
								<asp:listitem>Kontrak Batal</asp:listitem>
							</asp:dropdownlist>
						</td>
						<td>
							<asp:button id="search" runat="server" cssclass="btn btn-blue" text="Search" accesskey="s" onclick="search_Click"></asp:button>
						</td>
					</tr>
				</table>
			</div>
			<br>
			<asp:table id="rpt" runat="server" cssclass="tb blue-skin" cellspacing="1">
				<asp:tablerow horizontalalign="Left" verticalalign="Bottom">
					<asp:tableheadercell width="160">No. Kontrak</asp:tableheadercell>
					<asp:tableheadercell width="100">Unit</asp:tableheadercell>
					<asp:tableheadercell width="75">Tgl.</asp:tableheadercell>
					<asp:tableheadercell width="200">Customer / Sales</asp:tableheadercell>
					<asp:tableheadercell width="50">Keterangan</asp:tableheadercell>
				</asp:tablerow>
			</asp:table>
			<input type="text" style="DISPLAY:none">
			<script type="text/javascript">
			    function call(nomor)
			    {
			        window.parent.call(nomor);
			        window.parent.globalclose();
			    }
			
			function callSource(nomor, source)
			{
			    window.close();
				window.opener.callSource(nomor, source);
			}
			</script>
		</form>
	</body>
</html>

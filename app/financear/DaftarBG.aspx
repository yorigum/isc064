<%@ Page language="c#" Inherits="ISC064.FINANCEAR.DaftarBG" CodeFile="DaftarBG.aspx.cs" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Daftar Cek Giro</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
    	<link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
		<meta name="ctrl" content="1">
		<meta name="sec" content="(Pop-Up) Daftar Cek Giro">
		<meta http-equiv="pragma" content="no-cache">
		<base target="_self">
	</head>
	<body class="pop" onkeyup="if(event.keyCode==27){window.close()}"
		onload="document.getElementById('keyword').select()">
		<form id="Form1" method="post" runat="server">
			<div class="pad">
				<table>
					<tr>
						<td>
							<asp:textbox id="keyword" runat="server" cssclass="txt" width="400"></asp:textbox>
						</td>
						<td>
							<asp:dropdownlist id="metode" runat="server" cssclass="ddl form-control">
								<asp:listitem>Semua</asp:listitem>
								<asp:listitem>BG Normal</asp:listitem>
								<asp:listitem>BG Normal, Belum Cair</asp:listitem>
								<asp:listitem>BG Normal, Sudah Cair</asp:listitem>
								<asp:listitem>BG Bermasalah</asp:listitem>
							</asp:dropdownlist>
						</td>
						<td>
							<asp:button id="search" runat="server" cssclass="btn" text="Search" accesskey="s" onclick="search_Click"></asp:button>
						</td>
					</tr>
				</table>
			</div>
			<br>
			<asp:table id="rpt" runat="server" cssclass="tb" cellspacing="3">
				<asp:tablerow horizontalalign="Left" verticalalign="Bottom">
					<asp:tableheadercell width="130">BG</asp:tableheadercell>
					<asp:tableheadercell width="75">TTS</asp:tableheadercell>
					<asp:tableheadercell width="200">Customer</asp:tableheadercell>
					<asp:tableheadercell width="200">Keterangan</asp:tableheadercell>
					<asp:tableheadercell width="90" horizontalalign="Right">Nilai</asp:tableheadercell>
				</asp:tablerow>
			</asp:table>
			<input type="text" style="display:none;">
			<script type="text/javascript">
			function call(nomor,tglcekgiro)
			{
			    window.opener.call(nomor, tglcekgiro);
			    window.close();
			}
			</script>
		</form>
	</body>
</html>

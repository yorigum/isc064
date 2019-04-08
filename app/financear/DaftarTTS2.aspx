<%@ Page language="c#" Inherits="ISC064.FINANCEAR.DaftarTTS2" CodeFile="DaftarTTS2.aspx.cs" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Daftar TTS</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
    	<link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
		<meta name="ctrl" content="1">
		<meta name="sec" content="(Pop-Up) Daftar TTS">
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
								<asp:listitem>TTS Baru</asp:listitem>
								<asp:listitem>TTS Post</asp:listitem>
								<asp:listitem>TTS Void</asp:listitem>
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
					<asp:tableheadercell width="60">No. TTS</asp:tableheadercell>
					<asp:tableheadercell width="110">Tgl. / Kasir</asp:tableheadercell>
					<asp:tableheadercell width="200">Customer</asp:tableheadercell>
					<asp:tableheadercell width="200">Keterangan</asp:tableheadercell>
					<asp:tableheadercell width="130">Cara Bayar</asp:tableheadercell>
					<asp:tableheadercell width="90" horizontalalign="Right">Total</asp:tableheadercell>
				</asp:tablerow>
			</asp:table>
			<input type="text" style="display:none;">
			<script type="text/javascript">
			function call(nomor)
			{
				dialogArguments.call(nomor);
				window.close();
			}
			
			function callSource(nomor, source)
			{
				dialogArguments.callSource(nomor, source);
				window.close();
			}
			</script>
		</form>
	</body>
</html>

<%@ Page language="c#" Inherits="ISC064.LAUNCHING.DaftarNUP" CodeFile="DaftarNUP.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Daftar NUP</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="(Pop-Up) Daftar NUP">
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
						<%--<td>
							<asp:dropdownlist id="metode" runat="server" cssclass="ddl">
								<asp:listitem>Semua</asp:listitem>
								<asp:listitem>Aktif</asp:listitem>
								<asp:listitem>Inaktif</asp:listitem>
							</asp:dropdownlist>
						</td>--%>
						<td>
							<asp:button id="search" runat="server" cssclass="btn" text="Search" accesskey="s" onclick="search_Click"></asp:button>
						</td>
					</tr>
				</table>
			</div>
			<br>
			<p style="font:8pt;padding:3">
				Menampilkan data NUP yang terdaftar
			</p>
			<asp:table id="rpt" runat="server" cssclass="tb" cellspacing="3">
				<asp:tablerow horizontalalign="Left">
					<asp:tableheadercell width="75">NUP</asp:tableheadercell>
					<asp:tableheadercell width="100">Customer</asp:tableheadercell>
					<asp:tableheadercell width="200">Agent</asp:tableheadercell>
				</asp:tablerow>
			</asp:table>
			<input type="text" style="display:none;">
			<script language="javascript">
			function call(nomor)
			{
			    window.close();
			    dialogArguments.call(nomor);
			}
			</script>
		</form>
	</body>
</html>

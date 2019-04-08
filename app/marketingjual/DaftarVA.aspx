<%@ Page Language="c#" Inherits="ISC064.MARKETINGJUAL.DaftarVA" CodeFile="DaftarVA.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
	<title>Daftar Virtual Account</title>
	<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
	<meta name="CODE_LANGUAGE" content="C#">
	<meta name="vs_defaultClientScript" content="JavaScript">
	<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	<link href="/Media/Style.css" type="text/css" rel="stylesheet">
	<meta name="ctrl" content="1">
	<meta name="sec" content="(Pop-Up) Daftar Virtual Account">
	<meta http-equiv="pragma" content="no-cache">
	<base target="_self">
</head>
<body class="pop" onkeyup="if(event.keyCode==27){window.close()}" onload="document.getElementById('keyword').select()">
	<form id="Form1" method="post" runat="server">
	<div class="pad">
		<table>
			<tr>
				<td>
					<asp:TextBox ID="keyword" runat="server" CssClass="txt" Width="400"></asp:TextBox>
				</td>
				<td>
					<asp:Button ID="search" runat="server" CssClass="btn" Text="Search" AccessKey="s"
						OnClick="search_Click"></asp:Button>
				</td>
			</tr>
		</table>
	</div>
	<br>
	<asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="3">
		<asp:TableRow HorizontalAlign="Left">
			<asp:TableHeaderCell Width="100">No. VA</asp:TableHeaderCell>
			<asp:TableHeaderCell Width="150">Bank</asp:TableHeaderCell>
		</asp:TableRow>
	</asp:Table>
	<input type="text" style="display: none;" />

	<script language="javascript" type="text/javascript">
		function call(nomor) {
			window.opener.call(nomor);
			window.close();
		}
	</script>

	</form>
</body>
</html>

<%@ Page Language="c#" Inherits="ISC064.MARKETINGJUAL.DaftarReff" CodeFile="DaftarReff.aspx.cs" %>

<!DOCTYPE html>

<html>
<head>
	<title>Daftar Refferator</title>
	<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
	<meta name="CODE_LANGUAGE" content="C#">
	<meta name="vs_defaultClientScript" content="JavaScript">
	<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	<link href="/Media/Style.css" type="text/css" rel="stylesheet">
	<meta name="ctrl" content="1">
	<meta name="sec" content="(Pop-Up) Daftar Refferator">
	<meta http-equiv="pragma" content="no-cache">
	<base target="_self">
</head>
<body class="body-padding pop" onkeyup="if(event.keyCode==27){window.close()}" onload="document.getElementById('keyword').select()">
	<form id="Form1" method="post" runat="server">
	<div class="pad">
		<table>
			<tr>
				<td>
					<asp:TextBox ID="keyword" runat="server" CssClass="txt" Width="400"></asp:TextBox>
				</td>
				<td>
					<asp:Button ID="search" runat="server" CssClass="btn btn-blue" Text="Search" AccessKey="s"
						OnClick="search_Click"></asp:Button>
				</td>
			</tr>
		</table>
	</div>
	<br>
	<asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1">
		<asp:TableRow HorizontalAlign="Left">
			<asp:TableHeaderCell Width="100">No. Agent</asp:TableHeaderCell>
			<asp:TableHeaderCell Width="150">Nama</asp:TableHeaderCell>	
		</asp:TableRow>
	</asp:Table>
	<asp:Table ID="rptx" runat="server" CssClass="tb blue-skin" CellSpacing="1">
		<asp:TableRow HorizontalAlign="Left">
			<asp:TableHeaderCell Width="100">No. Customer</asp:TableHeaderCell>
			<asp:TableHeaderCell Width="150">Nama</asp:TableHeaderCell>
		</asp:TableRow>
	</asp:Table>
	<input type="text" style="display: none;" />

	<script type="text/javascript">
	    function call2(reff, tipea) {
			dialogArguments.call(reff, tipea);
			window.close();
		}
		
	</script>

	</form>
</body>
</html>
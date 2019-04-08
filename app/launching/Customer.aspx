<%@ Page language="c#" Inherits="ISC064.LAUNCHING.Customer" CodeFile="Customer.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Customer Information File</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Customer Information File">
	</head>
	<body>
		<form id="Form1" method="post" runat="server" class="cnt">
			<uc1:head id="Head1" runat="server"></uc1:head>
			<input type="text" style="display:none">
			<h1>Customer Information File</h1>
			<br>
			<table style="border:1px solid #DCDCDC" cellspacing="5">
				<tr>
					<td style="font:8pt">Customer / Unit / Dokumen :</td>
					<td>
						<asp:textbox id="keyword" runat="server" cssclass="txt" width="300"></asp:textbox>
					</td>
					<td>
						<asp:button id="search" runat="server" cssclass="btn" text="Search" accesskey="s" onclick="search_Click"></asp:button>
					</td>
				</tr>
			</table>
			<br>
			<asp:table id="rpt" runat="server" cssclass="tb" cellspacing="3">
				<asp:tablerow horizontalalign="Left" verticalalign="Bottom">
					<asp:tableheadercell width="120">Ref.</asp:tableheadercell>
					<asp:tableheadercell>Status</asp:tableheadercell>
					<asp:tableheadercell width="50">Tipe</asp:tableheadercell>
					<asp:tableheadercell width="120">Unit</asp:tableheadercell>
					<asp:tableheadercell width="280">Customer</asp:tableheadercell>
				</asp:tablerow>
			</asp:table>
			<script language="javascript">
			function call(ref,tipe)
			{
				popCIF(ref,tipe)
			}
			</script>
		</form>
	</body>
</html>

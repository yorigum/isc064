<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.MigratePembayaran2" CodeFile="MigratePembayaran2.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Approval Pembayaran</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Migrate - Approval Pembayaran (Hal. 2)">
	</head>
	<body>
		<form id="Form1" method="post" runat="server" class="cnt">
			<uc1:head id="Head1" runat="server"></uc1:head>
			<input type="text" style="display:none">
			<h1>Approval Pembayaran</h1>
			<%--<p>Halaman 1 dari 4</p>--%>
			<br />
			<div id="hasil" runat="server">
				<%--<p style="font:8pt;padding-left:3">
					Harga price list adalah dalam rupiah per meter persegi per bulan.
					<br>
					Luas adalah dalam meter persegi.
				</p>--%>
				<asp:table id="rpt" runat="server" cssclass="tb" cellspacing="3">
					<asp:tablerow>
						<asp:tableheadercell>&nbsp;</asp:tableheadercell>
						<asp:tableheadercell width="100" horizontalalign="Left">TTS</asp:tableheadercell>
					</asp:tablerow>
				</asp:table>
			</div>
		</form>
	</body>
</html>

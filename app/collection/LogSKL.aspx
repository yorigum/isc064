<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LogSKL.aspx.cs" Inherits="ISC064.COLLECTION.LogSKL" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>

<html>
	<head>
		<title>Log File</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Log File SKL">
	</head>
	<body class="body-padding" onkeyup="if(event.keyCode==27)history.back(-1)">
		<form id="Form1" method="post" runat="server" class="cnt">
			<div style="display:none"><uc1:head id="Head1" runat="server"></uc1:head></div>
			<h1 class="title title-line">Log File</h1>
			<br>
			<asp:table id="rpt" runat="server" cssclass="tb blue-skin" cellspacing="1">
				<asp:tablerow horizontalalign="Left">
					<asp:tableheadercell width="65">Tgl</asp:tableheadercell>
					<asp:tableheadercell width="45">Jam</asp:tableheadercell>
					<asp:tableheadercell width="200" columnspan="2">User</asp:tableheadercell>
					<asp:tableheadercell width="50">Aktivitas</asp:tableheadercell>
					<asp:tableheadercell width="120">Referensi</asp:tableheadercell>
				</asp:tablerow>
			</asp:table>
			<table height="50">
				<tr>
					<td>
                        <a onclick="history.back(-1)" class="btn btn-blue t-white" style="width:75px">
							<i class="fa fa-share"></i> OK
						</a>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>

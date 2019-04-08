<%@ Page language="c#" Inherits="ISC064.NUP.LogPk" CodeFile="LogPk.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html lang="en">
	<head>
		<title>Log File</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
    	<link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Log File Detil per Objek Data">
	</head>
	<body class="body-padding" onkeyup="if(event.keyCode==27)history.back(-1)">
		<form id="Form1" method="post" runat="server" class="cnt">
			<div style="display:none"><uc1:head id="Head1" runat="server"></uc1:head></div>
			<h1 class="title title-line">Log File</h1>
			<asp:radiobuttonlist id="tb" runat="server" visible="False">
<%--				<asp:listitem value="USERNAME_LOG">USERNAME</asp:listitem>
				<asp:listitem value="SECLEVEL_LOG">SEC.LEVEL</asp:listitem>--%>
                <asp:listitem value="MS_NUP_LOG">NUP</asp:listitem>
			</asp:radiobuttonlist>
			<br>
			<asp:table id="rpt" runat="server" cssclass="tb blue-skin" cellspacing="1">
				<asp:tablerow horizontalalign="Left">
					<asp:tableheadercell width="65px">Tgl</asp:tableheadercell>
					<asp:tableheadercell width="45px">Jam</asp:tableheadercell>
					<asp:tableheadercell width="200px" columnspan="2">User</asp:tableheadercell>
					<asp:tableheadercell width="50px">Aktivitas</asp:tableheadercell>
					<asp:tableheadercell width="120px">Referensi</asp:tableheadercell>
					<asp:tableheadercell width="120px">Approval</asp:tableheadercell>
				</asp:tablerow>
			</asp:table>
			<table style="height:50px">
				<tr>
					<td>
						<input id="cancel" onclick="history.back(-1)" type="button" class="btn btn-blue" value="OK" style="width:75px">
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>

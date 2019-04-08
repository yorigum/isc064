<%@ Reference Control="~/Head.ascx" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MKP.aspx.cs" Inherits="ISC064.SECURITY.MKP" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Matrikulasi Akses Project</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Matrikulasi Akses Project">
	</head>
	<body class="body-padding">
		<form id="Form1" method="post" runat="server" class="cnt">
			<uc1:head id="Head1" runat="server"></uc1:head>
			<h1 class="title title-line">Matrikulasi Akses Project</h1>
			<p class="feed">
				<asp:label id="feed" runat="server"></asp:label>
			</p>
			<table class="tb blue-skin" cellspacing="3">
				<tr align="left" id="head" runat="server">
					<th onmouseover="this.style.color='blue'" onmouseout="this.style.color=''" onclick="location.href='?'">
						Kode / ID</th>
					<th width="250">
						Nama</th>
				</tr>
				<asp:placeholder id="list" runat="server" enableviewstate="True"></asp:placeholder>
			</table>
			<table height="50">
				<tr>
					<td>
						<asp:LinkButton id="save" runat="server" cssclass="btn btn-blue" width="75" onclick="save_Click"><i class="fa fa-share"></i> OK</asp:LinkButton>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>

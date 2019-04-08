<%@ Page language="c#" Inherits="ISC064.NUP.Log" CodeFile="Log.aspx.cs" %>
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
		<meta name="sec" content="Log File">
	</head>
	<body class="body-padding">
		<form id="Form1" method="post" runat="server" class="cnt">
			<uc1:head id="Head1" runat="server"></uc1:head>
			<input type="text" style="display:none">
			<h1 class="title title-line">Log File</h1>
			<br>
			<table style="border:1px solid #DCDCDC" cellspacing="5">
				<tr>
					<td>User</td>
					<td>:</td>
					<td>
						<asp:dropdownlist id="user" runat="server" cssclass="ddl igroup" width="180">
							<asp:listitem value="">Semua</asp:listitem>
						</asp:dropdownlist>
					</td>
					<td>Dari</td>
					<td>
						<asp:textbox id="dari" runat="server" cssclass="txt_center igroup" width="85"></asp:textbox>
						<input type="button" value="&#xf073;" style="font-family: 'fontawesome'" class="btn" onclick="openCalendar('dari');">
						<asp:label id="daric" runat="server" cssclass="err"></asp:label>
					</td>
					<td>Sampai</td>
					<td>
						<asp:textbox id="sampai" runat="server" cssclass="txt_center igroup" width="85"></asp:textbox>
						<input type="button" value="&#xf073;" style="font-family: 'fontawesome'" class="btn" onclick="openCalendar('sampai');">
						<asp:label id="sampaic" runat="server" cssclass="err"></asp:label>
					</td>
					<td>
						<asp:button id="display" runat="server" cssclass="btn btn-blue" text="Display" onclick="display_Click"></asp:button>
						<asp:button id="xls" accesskey="e" runat="server" text="Excel" cssclass="btn btn-green" onclick="xls_Click"></asp:button></td>
				</tr>
				<tr>
					<td>Keyword</td>
					<td>:</td>
					<td colspan="6">
						<asp:textbox id="keyword" runat="server" cssclass="txt igroup" width="250"></asp:textbox>
					</td>
				</tr>
				<tr>
					<td colspan="8">
						<asp:radiobuttonlist id="tb" runat="server" repeatdirection="Horizontal" repeatcolumns="5" font-bold="True">
                            <asp:listitem value="MS_NUP_LOG" class="igroup-radio">NUP</asp:listitem>
						</asp:radiobuttonlist>
						<asp:dropdownlist id="href" runat="server" visible="False">
							<asp:listitem value="MS_NUP_LOG">popNUP('%pk%')</asp:listitem>
						</asp:dropdownlist>
					</td>
				</tr>
			</table>
			<br>
			<asp:table id="rpt" runat="server" cssclass="tb blue-skin" cellspacing="1">
				<asp:tablerow horizontalalign="Left">
					<asp:tableheadercell width="65">Tgl</asp:tableheadercell>
					<asp:tableheadercell width="45">Jam</asp:tableheadercell>
					<asp:tableheadercell width="200" columnspan="2">User</asp:tableheadercell>
					<asp:tableheadercell width="50">Aktivitas</asp:tableheadercell>
					<asp:tableheadercell width="120">Referensi</asp:tableheadercell>
					<asp:tableheadercell width="120">Approval</asp:tableheadercell>
				</asp:tablerow>
			</asp:table>
		</form>
	</body>
</html>

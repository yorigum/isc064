<%@ Page language="c#" Inherits="ISC064.Calendar" CodeFile="Calendar.aspx.cs" %>
<!DOCTYPE html>
<html lang="en">
	<head>
		<title>Calendar</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="0">
		<meta name="sec" content="(Pop-Up) Calendar">
		<meta http-equiv="pragma" content="no-cache">
		<base target="_self">
	</head>
	<body class="pop" onkeyup="if(event.keyCode==27)window.close()">
		<form id="Form1" method="post" runat="server">
			<div class="pad" style="text-align:center;">
				<table>
					<tr>
						<td>
							<asp:button id="hariini" runat="server" cssclass="btn btn-blue" text="Hari ini..." onclick="hariini_Click"></asp:button>
						</td>
						<td width="100%"></td>
						<td>
							<asp:dropdownlist id="tahun" runat="server" autopostback="True" cssclass="ddl" onselectedindexchanged="tahun_SelectedIndexChanged"></asp:dropdownlist>
						</td>
						<td>
							<asp:dropdownlist id="bulan" runat="server" autopostback="True" cssclass="ddl" onselectedindexchanged="bulan_SelectedIndexChanged">
								<asp:listitem value="1">Januari</asp:listitem>
								<asp:listitem value="2">Februari</asp:listitem>
								<asp:listitem value="3">Maret</asp:listitem>
								<asp:listitem value="4">April</asp:listitem>
								<asp:listitem value="5">Mei</asp:listitem>
								<asp:listitem value="6">Juni</asp:listitem>
								<asp:listitem value="7">Juli</asp:listitem>
								<asp:listitem value="8">Agustus</asp:listitem>
								<asp:listitem value="9">September</asp:listitem>
								<asp:listitem value="10">Oktober</asp:listitem>
								<asp:listitem value="11">November</asp:listitem>
								<asp:listitem value="12">Desember</asp:listitem>
							</asp:dropdownlist>
						</td>
					</tr>
				</table>
			</div>
			<br>
			<div align="center">
				<asp:calendar id="cal" runat="server" weekenddaystyle-forecolor="red" todaydaystyle-backcolor="lightblue"
					dayheaderstyle-font-bold="True" onselectionchanged="cal_SelectionChanged"></asp:calendar>
			</div>
		</form>
	</body>
</html>

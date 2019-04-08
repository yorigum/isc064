<%@ Page language="c#" Inherits="ISC064.SECURITY.SecurityLogUser" CodeFile="SecurityLogUser.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="HeadUser" Src="HeadUser.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavUser" Src="NavUser.ascx" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Security Log per User</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
    	<link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Username - Tabel Security Log per User">
	</head>
	<body onkeyup="if(event.keyCode==27)window.close()">
		<form id="Form1" method="post" runat="server">
			<div class="content-header">
				<uc1:navuser id="NavUser1" runat="server" aktif="2"></uc1:navuser>
			</div>
			<div class="tabdata">
				<div class="pad">
					<uc1:headuser id="HeadUser1" runat="server"></uc1:headuser>
					<table>
						<tr>
							<td>
								<asp:dropdownlist id="tahun" runat="server" cssclass="ddl igroup" width="100"></asp:dropdownlist>
							</td>
							<td>
								<asp:dropdownlist id="bulan" runat="server" cssclass="ddl igroup" width="200"></asp:dropdownlist>
							</td>
							<td>
								<asp:button id="display" runat="server" text="Display" cssclass="btn btn-blue" onclick="display_Click"></asp:button>
							</td>
						</tr>
					</table>
					<br>
					<asp:table id="rpt" runat="server" cssclass="tb blue-skin" cellspacing="1">
						<asp:tablerow horizontalalign="Left">
							<asp:tableheadercell width="40">Tgl</asp:tableheadercell>
							<asp:tableheadercell width="60">Jam</asp:tableheadercell>
							<asp:tableheadercell width="150">Aktivitas</asp:tableheadercell>
							<asp:tableheadercell width="100">IP Address</asp:tableheadercell>
						</asp:tablerow>
					</asp:table>
				</div>
			</div>
		</form>
	</body>
</html>

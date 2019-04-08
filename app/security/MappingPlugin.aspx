<%@ Page language="c#" Inherits="ISC064.SECURITY.MappingPlugin" CodeFile="MappingPlugin.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html lang="en">
	<head>
		<title>Mapping Plugin</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
    	<link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Setup Mapping Plugin">
	</head>
	<body class="body-padding">
		<form id="Form1" method="post" runat="server" class="cnt">
			<uc1:head id="Head1" runat="server"></uc1:head>
			<h1 class="title title-line">Mapping Plugin</h1>
			<br>
			<table style="border:1px solid #DCDCDC" cellspacing="5">
				<tr>
					<td>
						<input type="button" onclick="if(confirm('Lanjutkan proses map ulang?')){location.href='MappingPluginUlang.aspx'}"
							class="btn" value="Map Ulang">
					</td>
					<td>
						<asp:dropdownlist id="modul" runat="server" cssclass="ddl form-control" width="250">
							<asp:listitem value="">Modul :</asp:listitem>
						</asp:dropdownlist>
					</td>
					<td>
						<asp:button id="display" runat="server" cssclass="btn btn-blue" text="Display" onclick="display_Click"></asp:button>
					</td>
				</tr>
			</table>
			<p class="feed">
				<asp:label id="feed" runat="server"></asp:label>
			</p>
			<asp:table id="rpt" runat="server" cssclass="tb blue-skin" CellSpacing="1">
				<asp:tablerow horizontalalign="Left">
					<asp:tableheadercell width="400">Keterangan</asp:tableheadercell>
					<asp:tableheadercell width="160">Halaman</asp:tableheadercell>
					<asp:tableheadercell width="160">Konfigurasi Security</asp:tableheadercell>
				</asp:tablerow>
			</asp:table>
		</form>
        <script type="text/javascript">
            function popPlugin(hal) {
                popHalaman3(hal);
            }
        </script>
	</body>
</html>

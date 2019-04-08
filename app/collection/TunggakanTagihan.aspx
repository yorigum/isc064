<%@ Page language="c#" Inherits="ISC064.COLLECTION.TunggakanTagihan" CodeFile="TunggakanTagihan.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="HeadTunggakan" Src="HeadTunggakan.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavTunggakan" Src="NavTunggakan.ascx" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Detil Tagihan</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Surat Peringatan - Detil Tagihan">
	</head>
	<body onkeyup="if(event.keyCode==27) window.close()">
		<form id="Form1" method="post" runat="server">
			<uc1:navtunggakan id="NavTunggakan1" runat="server" aktif="2"></uc1:navtunggakan>
			<div class="tabdata">
				<div class="pad">
					<uc1:headtunggakan id="HeadTunggakan1" runat="server"></uc1:headtunggakan>
					<br>
					<asp:table id="rpt" runat="server" cssclass="tb blue-skin" cellspacing="0">
						<asp:tablerow>
							<asp:tableheadercell horizontalalign="Left" width="100">Jatuh Tempo</asp:tableheadercell>
							<asp:tableheadercell horizontalalign="Left" width="50">Telat</asp:tableheadercell>
							<asp:tableheadercell horizontalalign="Left" width="300">Tagihan</asp:tableheadercell>
							<asp:tableheadercell horizontalalign="Right" width="100">Nilai</asp:tableheadercell>
						</asp:tablerow>
					</asp:table>
				</div>
			</div>
		</form>
	</body>
</html>

<%@ Page language="c#" Inherits="ISC064.COLLECTION.CustomerTunggakan" CodeFile="CustomerTunggakan.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="HeadCIF" Src="HeadCIF.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavCIF" Src="NavCIF.ascx" %>
<!DOCTYPE html >
<html>
	<head>
		<title>Customer Information File (Surat Peringatan)</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Customer Information File - Surat Peringatan">
	</head>
	<body onkeyup="if(event.keyCode==27) window.close()">
		<form id="Form1" method="post" runat="server">
            <div class="content-header">
			    <uc1:navcif id="NavCIF1" runat="server" aktif="3"></uc1:navcif>
			</div>
            <div class="tabdata">
				<div class="pad">
					<uc1:headcif id="HeadCIF1" runat="server"></uc1:headcif>
					<br>
					<asp:table id="rpt" runat="server" cssclass="tb blue-skin" cellspacing="1">
						<asp:tablerow horizontalalign="Left">
							<asp:tableheadercell width="75">No.</asp:tableheadercell>
							<asp:tableheadercell width="85">Tgl.</asp:tableheadercell>
							<asp:tableheadercell width="85">Status</asp:tableheadercell>
							<asp:tableheadercell width="400">Keterangan</asp:tableheadercell>
							<asp:tableheadercell horizontalalign="Right" width="80">Total</asp:tableheadercell>
						</asp:tablerow>
					</asp:table>
				</div>
			</div>
		</form>
	</body>
</html>

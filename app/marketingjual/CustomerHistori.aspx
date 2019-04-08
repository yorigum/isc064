<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.CustomerHistori" CodeFile="CustomerHistori.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="HeadCustomer" Src="HeadCustomer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavCustomer" Src="NavCustomer.ascx" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Histori Kontrak</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Customer - Histori Kontrak">
	</head>
	<body onkeyup="if(event.keyCode==27) window.close()">
		<form id="Form1" method="post" runat="server">
			<div class="content-header">
				<uc1:navcustomer id="NavCustomer1" runat="server" aktif="2"></uc1:navcustomer>			
			</div>
			<div class="tabdata">
				<div class="pad">
					<uc1:headcustomer id="HeadCustomer1" runat="server"></uc1:headcustomer>
					<p style="font:8pt;padding-left:3">
						Status : A = Aktif / B = Batal / E = Expire
					</p>
					<asp:table id="rpt" runat="server" cssclass="tb blue-skin" cellspacing="1">
						<asp:tablerow horizontalalign="Left">
							<asp:tableheadercell width="100" columnspan="2">No.</asp:tableheadercell>
							<asp:tableheadercell width="75">Tgl.</asp:tableheadercell>
							<asp:tableheadercell width="100">Unit</asp:tableheadercell>
							<asp:tableheadercell width="150">Sales</asp:tableheadercell>
							<asp:tableheadercell width="120">Keterangan</asp:tableheadercell>
							<asp:tableheadercell horizontalalign="Right">Nilai</asp:tableheadercell>
						</asp:tablerow>
					</asp:table>
				</div>
			</div>
		</form>
	</body>
</html>

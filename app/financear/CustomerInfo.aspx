<%@ Page language="c#" Inherits="ISC064.FINANCEAR.CustomerInfo" CodeFile="CustomerInfo.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="HeadCIF" Src="HeadCIF.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavCIF" Src="NavCIF.ascx" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Customer Information File (Tagihan dan Pelunasan)</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
    	<link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Customer Information File - Tagihan dan Pelunasan">
	</head>
	<body onkeyup="if(event.keyCode==27) window.close()">
		<form id="Form1" method="post" runat="server">
            <div class="content-header">
			    <uc1:navcif id="NavCIF1" runat="server" aktif="1"></uc1:navcif>
            </div>
			<div class="tabdata">
				<div class="pad">
					<uc1:headcif id="HeadCIF1" runat="server"></uc1:headcif>
					<br>
					<asp:table id="rpt" runat="server" cssclass="tb blue-skin" Width="100%" cellspacing="1">
						<asp:tablerow horizontalalign="Left">
							<asp:tableheadercell>No.</asp:tableheadercell>
							<asp:tableheadercell>Tagihan</asp:tableheadercell>
							<asp:tableheadercell>Tgl.</asp:tableheadercell>
							<asp:tableheadercell horizontalalign="Right">Pokok</asp:tableheadercell>
							<asp:tableheadercell horizontalalign="Right">Pelunasan</asp:tableheadercell>
							<asp:tableheadercell horizontalalign="Right">Sisa</asp:tableheadercell>
							<asp:tableheadercell horizontalalign="Right">Denda</asp:tableheadercell>
							<asp:tableheadercell horizontalalign="Right" Wrap="false">Realisasi Denda</asp:tableheadercell>
							<asp:tableheadercell horizontalalign="Right" Wrap="false">Putih Denda</asp:tableheadercell>
						</asp:tablerow>
					</asp:table>
				</div>
			</div>
		</form>
	</body>
</html>

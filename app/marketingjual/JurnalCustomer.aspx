<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.JurnalCustomer" CodeFile="JurnalCustomer.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="HeadCustomer" Src="HeadCustomer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavCustomer" Src="NavCustomer.ascx" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Jurnal Customer</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Jurnal Customer">
	</head>
	<body onkeyup="if(event.keyCode==27)document.getElementById('cancel').click()">
		<form id="Form1" method="post" runat="server">
			<div class="content-header">
				<uc1:navcustomer id="NavCustomer1" runat="server" aktif="4"></uc1:navcustomer>				
			</div>
			<div class="tabdata">
				<div class="pad">
					<uc1:headcustomer id="HeadCustomer1" runat="server"></uc1:headcustomer>
					<input type="text" style="display:none">
					<table cellspacing="5">
						<tr>
							<td colspan="3">
								<asp:radiobuttonlist id="akt" runat="server" repeatcolumns="3" repeatdirection="Vertical">
									<asp:listitem selected="True">DRAFTING PENAWARAN</asp:listitem>
									<asp:listitem>KIRIM PENAWARAN</asp:listitem>
									<asp:listitem>PHONE MARKETING</asp:listitem>
									<asp:listitem>VISIT CUSTOMER</asp:listitem>
									<asp:listitem>VISIT LOKASI</asp:listitem>
									<asp:listitem>ENTERTAIN</asp:listitem>
									<asp:listitem>LAINNYA</asp:listitem>
								</asp:radiobuttonlist>
							</td>
						</tr>
						<tr>
							<td>Keterangan Tambahan</td>
							<td>:</td>
							<td>
								<asp:textbox id="baru" runat="server" cssclass="txt" width="500"></asp:textbox>
							</td>
						</tr>
					</table>
					<table height="50">
						<tr>
							<td>
								<asp:LinkButton id="save" runat="server" cssclass="btn btn-blue" width="75" onclick="save_Click">
									<i class="fa fa-share"></i> OK
								</asp:LinkButton>
							</td>
							<td>
								<input id="cancel" type="button" class="btn btn-red" value="Cancel" name="cancel"
									onclick="window.close()">
							</td>
							<td style="padding-left:10px">
								<p class="feed">
									<asp:label id="feed" runat="server"></asp:label>
								</p>
							</td>
						</tr>
					</table>
					<asp:table id="rpt" runat="server" cssclass="tb blue-skin" cellspacing="1">
						<asp:tablerow horizontalalign="Left">
							<asp:tableheadercell width="65">Tgl</asp:tableheadercell>
							<asp:tableheadercell width="45">Jam</asp:tableheadercell>
							<asp:tableheadercell width="200" columnspan="2">User</asp:tableheadercell>
							<asp:tableheadercell width="340">Keterangan</asp:tableheadercell>
						</asp:tablerow>
					</asp:table>
				</div>
			</div>
		</form>
	</body>
</html>

<%@ Page Language="C#" Inherits="ISC064.ADMINJUAL.KomisiBayar" CodeFile="KomisiBayar.aspx.cs" %>
<!DOCTYPE html >
<html lang="en">
	<head>
		<title>Pencairan Komisi</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
    	<link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Sales - Bayar Komisi">
		<script type="text/javascript" src="/Js/NumberFormat.js"></script>
		<script type="text/javascript" src="/Js/Common.js"></script>
		<meta http-equiv="pragma" content="no-cache">
		<base target="_self">
	</head>
	<body onkeyup="if(event.keyCode==27) {window.close()}">
		<form id="Form1" method="post" runat="server">
			<div class="pad">
				<table cellpadding="5">
					<tr>
						<td>No. Kontrak</td>
						<td>:</td>
						<td><asp:label id="lblNoKontrak" runat="server" font-bold="True" font-size="14pt"></asp:label></td>
					</tr>
					<tr>
						<td>Sales</td>
						<td>:</td>
						<td><asp:label id="lblAgent" runat="server"></asp:label></td>
					</tr>
					<tr>
						<td>Principal</td>
						<td>:</td>
						<td><asp:label id="lblPrincipal" runat="server"></asp:label></td>
					</tr>
					<tr>
						<td>Customer</td>
						<td>:</td>
						<td><asp:label id="lblCustomer" runat="server"></asp:label></td>
					</tr>
					<tr>
						<td>Unit</td>
						<td>:</td>
						<td><asp:label id="lblUnit" runat="server"></asp:label></td>
					</tr>
				</table>
				<hr style="BORDER-BOTTOM: silver 1px dashed; MARGIN: 0px; COLOR: silver" noshade size="1">
				<table cellpadding="5">
<%--					<tr>
						<td>No. Nota</td>
						<td>:</td>
						<td><asp:textbox id="tbNota" runat="server" cssclass="txt"></asp:textbox><asp:label id="lblNotac" runat="server" cssclass="err"></asp:label></td>
					</tr>--%>
					<tr>
						<td>Tgl. Cair</td>
						<td>:</td>
						<td><asp:textbox id="tbTglBayar" runat="server" cssclass="txt_center" width="85"></asp:textbox><label for="tbTglBayar" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
							<asp:label id="lblTglBayarc" runat="server" cssclass="err"></asp:label></td>
					</tr>
					<tr>
						<td>Nilai Cair</td>
						<td>:</td>
						<td>
							<asp:textbox id="tbNilai" runat="server" cssclass="txt_num"></asp:textbox>
							<asp:label id="lblNilaic" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>
<%--					<tr>
						<td>Rekening Bank</td>
						<td>:</td>
						<td>
							<asp:dropdownlist id="acc" runat="server" cssclass="ddl" width="300"></asp:dropdownlist>
							<asp:label id="accerr" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>--%>
				</table>
				<table style="height:50px;">
					<tr>
						<td><asp:button id="btnOK" runat="server" cssclass="btn btn-blue" width="75" text="OK" onclick="btnOK_Click"></asp:button></td>
						<td><input class="btn" style="WIDTH: 75px" type="button" value="Cancel" name="cancel" onclick="window.close();" />
						</td>
					</tr>
				</table>
			</div>
			<asp:checkbox id="cbStatus" runat="server" visible="False"></asp:checkbox></form>
	</body>
</html>

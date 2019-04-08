<%@ Reference Page="~/Acc.aspx" %>
<%@ Page language="c#" Inherits="ISC064.FINANCEAR.KasKeluar" CodeFile="KasKeluar.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Kas Keluar</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
    	<link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Kas Bank - Kas Keluar">
	</head>
	<body>
		<form id="Form1" method="post" runat="server" class="cnt">
			<uc1:head id="Head1" runat="server"></uc1:head>
			<input type="text" style="display:none">
			<h1>Kas Keluar</h1>
			<br>
			<table cellspacing="5">
				<tr>
					<td>No. Voucher</td>
					<td>:</td>
					<td style="font-weight:bold">#AUTO</td>
				</tr>
				<tr>
					<td>Tanggal</td>
					<td>:</td>
					<td>
						<asp:textbox id="tgl" runat="server" cssclass="txt_center" width="85"></asp:textbox>
						<label for="tgl" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
						<asp:label id="tglc" runat="server" cssclass="err"></asp:label>
					</td>
				</tr>
				<tr>
					<td>Rekening Bank</td>
					<td>:</td>
					<td>
						<asp:dropdownlist id="acc" runat="server" cssclass="ddl" width="300"></asp:dropdownlist>
						<asp:label id="accc" runat="server" cssclass="err"></asp:label>
					</td>
				</tr>
				<tr>
					<td colspan="3">
						<br>
					</td>
				</tr>
				<tr>
					<td>Cara Bayar</td>
					<td>:</td>
					<td>
						<asp:radiobuttonlist id="carabayar" runat="server" repeatdirection="Horizontal">
							<asp:listitem value="TN" selected="True">Tunai</asp:listitem>
							<asp:listitem value="BG">Cek Giro</asp:listitem>
						</asp:radiobuttonlist>
					</td>
				</tr>
				<tr>
					<td>Alat Bayar</td>
					<td>:</td>
					<td>
						<asp:textbox id="alatbayar" runat="server" cssclass="txt" width="100" maxlength="50"></asp:textbox>
					</td>
				</tr>
				<tr>
					<td colspan="3">
						<br>
					</td>
				</tr>
				<tr>
					<td>Dibayar Kepada</td>
					<td>:</td>
					<td>
						<asp:textbox id="dibayarkepada" runat="server" cssclass="txt" width="200" maxlength="50"></asp:textbox>
					</td>
				</tr>
				<tr>
					<td>Keterangan</td>
					<td>:</td>
					<td>
						<asp:textbox id="keterangan" runat="server" cssclass="txt" width="400" maxlength="200"></asp:textbox>
					</td>
				</tr>
				<tr>
					<td colspan="3">
						<br>
					</td>
				</tr>
				<tr>
					<td>Nilai</td>
					<td>:</td>
					<td>
						<asp:textbox id="nilai" runat="server" cssclass="txt_num"></asp:textbox>
						<asp:label id="nilaic" runat="server" cssclass="err"></asp:label>
					</td>
				</tr>
			</table>
			<table style="height:50px">
				<tr>
					<td>
						<asp:button id="ok" runat="server" cssclass="btn" text="OK" width="75" onclick="ok_Click"></asp:button>
						<asp:label id="noacc" runat="server" cssclass="err"></asp:label>
					</td>
					<td style="padding-left:10">
						<p class="feed">
							<asp:label id="feed" runat="server"></asp:label>
						</p>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>

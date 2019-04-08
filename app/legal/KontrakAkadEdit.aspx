<%@ Reference Page="~/Log.aspx" %>
<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.KontrakAkadEdit" CodeFile="KontrakAkadEdit.aspx.cs" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Edit Akad</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Kontrak - Edit Akad">
		<script type="text/javascript" src="/Js/NumberFormat.js"></script>
	</head>
	<body class="body-padding" onkeyup="if(event.keyCode==27&amp;&amp;confirm('Kembali ke halaman proses KPR?')) document.getElementById('cancel').click()">
		<script type="text/javascript" src="/Js/Pop.js"></script>
		<script type="text/javascript" src="/Js/NumberFormat.js"></script>
		<form id="Form1" method="post" runat="server" class="cnt">
			<input style="DISPLAY: none">
			<h1 class="title title-line">
				Edit Akad
			</h1>
			<table cellspacing="5">
				<tr>
					<td>No. Kontrak</td>
					<td>:</td>
					<td><asp:label id="nokontrak" runat="server" font-bold="True"></asp:label></td>
				</tr>
				<tr>
					<td>Unit</td>
					<td>:</td>
					<td><asp:label id="unit" runat="server" font-bold="True"></asp:label></td>
				</tr>
				<tr>
					<td>Customer</td>
					<td>:</td>
					<td><asp:label id="customer" runat="server" font-bold="True"></asp:label></td>
				</tr>
			</table>
			<br>
			<table cellspacing="5">
				<tr>
					<td>Status</td>
					<td>:</td>
					<td>
						<asp:radiobuttonlist id="rblStatus" runat="server" repeatdirection="Horizontal" autopostback="True" onselectedindexchanged="rblStatus_SelectedIndexChanged">
							<asp:listitem>BELUM DITENTUKAN</asp:listitem>
							<asp:listitem>TIDAK PERLU</asp:listitem>
							<asp:listitem>DIJADWALKAN</asp:listitem>
							<asp:listitem>SELESAI</asp:listitem>
						</asp:radiobuttonlist>
					</td>
				</tr>
			</table>
			<table id="dijadwalkan" runat="server" cellspacing="5">
				<tr>
					<td>
						Target Akad</td>
					<td>:</td>
					<td>
						<asp:textbox id="tbTarget" runat="server" cssclass="txt_center" width="75" font-size="8"></asp:textbox>
						<input type="button" value="..." class="btn" onclick="openCalendar('tbTarget')">
						<asp:label id="lblTarget" runat="server" forecolor="Red"></asp:label>
					</td>
				</tr>
			</table>
			<table id="selesai" runat="server" cellspacing="5">
				<tr>
					<td>Tgl. Akad</td>
					<td>:</td>
					<td>
						<asp:textbox id="tbTgl" runat="server" cssclass="txt_center" width="75" font-size="8"></asp:textbox>
						<input type="button" value="..." class="btn" onclick="openCalendar('tbTgl')">
						<asp:label id="lblTgl" runat="server" forecolor="Red"></asp:label>
					</td>
				</tr>
				<tr>
					<td>No. Akad</td>
					<td>:</td>
					<td>
						<asp:textbox id="tbNoAkad" runat="server" cssclass="txt" maxlength="20" width="100"></asp:textbox>
					</td>
				</tr>
				<tr>
					<td valign="top">Keterangan</td>
					<td valign="top">:</td>
					<td><asp:textbox id="tbKet" runat="server" cssclass="txt" textmode="MultiLine" rows="5" columns="40"></asp:textbox></td>
				</tr>
				<tr>
					<td>Realisasi Akad</td>
					<td>:</td>
					<td>
						<asp:textbox id="nilai" runat="server" cssclass="txt_num"></asp:textbox>
						<asp:label id="nilaic" runat="server" cssclass="err"></asp:label>
					</td>
				</tr>
				<tr>
					<td>Cadangan</td>
					<td>:</td>
					<td>
						<asp:radiobuttonlist id="cadangan" runat="server" repeatdirection="Horizontal">
							<asp:listitem value="0">TIDAK</asp:listitem>
							<asp:listitem value="1">YA</asp:listitem>
						</asp:radiobuttonlist>
					</td>
				</tr>
			</table>
			<table height="50">
				<tr>
					<td>
						<asp:button id="ok" runat="server" cssclass="btn btn-blue" text="OK" width="75" onclick="ok_Click"></asp:button>
					</td>
					<td>
						<input class="btn btn-red" id="cancel" style="width: 75px" type="button" value="Cancel" name="cancel"
							runat="server">
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>

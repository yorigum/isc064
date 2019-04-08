<%@ Reference Page="~/Log.aspx" %>
<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.KontrakSP3KEdit" CodeFile="KontrakSP3KEdit.aspx.cs" %>
<!DOCTYPE html>
<HTML>
	<HEAD>
		<title>Edit SP3K</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Kontrak - Edit SP3K">
	</HEAD>
	<body class="body-padding" onkeyup="if(event.keyCode==27&amp;&amp;confirm('Kembali ke halaman proses KPR?')) document.getElementById('cancel').click()">
		<script type="text/javascript" src="/Js/Common.js"></script>
		<script type="text/javascript" src="/Js/NumberFormat.js"></script>
		<form class="cnt" id="Form1" method="post" runat="server">
			<input style="DISPLAY: none">
			<h1 class="title title-line">Edit 
				SP3K
			</h1>
			<table cellSpacing="5">
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
			<table cellSpacing="5">
				<tr>
					<td>Status</td>
					<td>:</td>
					<td><asp:radiobuttonlist id="rblStatus" runat="server" autopostback="True" repeatdirection="Horizontal" onselectedindexchanged="rblStatus_SelectedIndexChanged">
							<asp:listitem>BELUM DITENTUKAN</asp:listitem>
							<asp:listitem>TIDAK PERLU</asp:listitem>
							<asp:listitem>DIJADWALKAN</asp:listitem>
							<asp:listitem>DIAJUKAN</asp:listitem>
							<asp:listitem>SELESAI</asp:listitem>
						</asp:radiobuttonlist></td>
				</tr>
			</table>
			<table id="dijadwalkan" cellSpacing="5" runat="server">
				<tr>
					<td>Target SP3K</td>
					<td>:</td>
					<td><asp:textbox id="tbTarget" runat="server" font-size="8" width="75" cssclass="txt_center"></asp:textbox>
						<label for="tbTarget" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
						<asp:label id="lblTarget" runat="server" forecolor="Red"></asp:label></td>
				</tr>
			</table>
			<table id="diajukan" cellSpacing="5" runat="server">
				<tr>
					<td>Tgl. Pengajuan SP3K</td>
					<td>:</td>
					<td><asp:textbox id="tbPengajuan" runat="server" font-size="8" width="75" cssclass="txt_center"></asp:textbox>
						<label for="tbPengajuan" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
						<asp:label id="lblPengajuan" runat="server" forecolor="Red"></asp:label></td>
				</tr>
			</table>
			<table id="selesai" cellSpacing="5" runat="server">
				<tr>
					<td>Tgl. Hasil SP3K</td>
					<td>:</td>
					<td><asp:textbox id="tbTgl" runat="server" font-size="8" width="75" cssclass="txt_center"></asp:textbox>
						<label for="tbTgl" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
						<asp:label id="lblTgl" runat="server" forecolor="Red"></asp:label></td>
				</tr>
				<tr>
					<td>No. SP3K</td>
					<td>:</td>
					<td><asp:textbox id="tbNoSP3K" runat="server" width="100" cssclass="txt" maxlength="20"></asp:textbox></td>
				</tr>
				<tr>
					<td>Hasil SP3K</td>
					<td>:</td>
					<td><asp:radiobuttonlist id="rblHasil" runat="server" repeatdirection="Horizontal">
							<asp:listitem selected="True">TOLAK</asp:listitem>
							<asp:listitem>SETUJU</asp:listitem>
							<asp:listitem>SETUJU SEBAGIAN</asp:listitem>
						</asp:radiobuttonlist></td>
				</tr>
				<tr>
					<td vAlign="top">Keterangan</td>
					<td vAlign="top">:</td>
					<td><asp:textbox id="tbKet" runat="server" cssclass="txt" columns="40" rows="5" textmode="MultiLine"></asp:textbox></td>
				</tr>
				<tr>
					<td>Nilai KPR Disetujui</td>
					<td>:</td>
					<td><asp:textbox id="nilai" runat="server" width="150" cssclass="txt_num"></asp:textbox><asp:label id="nilaic" runat="server" cssclass="err"></asp:label></td>
				</tr>
				<tr>
					<td>Tambahan Uang Muka</td>
					<td>:</td>
					<td><asp:textbox id="tambahum" runat="server" width="150" cssclass="txt_num" readonly="True"></asp:textbox><asp:textbox id="tgljtum" runat="server" font-size="8" width="75" cssclass="txt_center"></asp:textbox>
						<label for="tgljtum" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
						<asp:label id="tgljtumc" runat="server" cssclass="err"></asp:label></td>
				</tr>
			</table>
			<table height="50">
				<tr>
					<td><asp:button id="ok" runat="server" width="75" cssclass="btn btn-blue" text="OK" onclick="ok_Click"></asp:button></td>
					<td><input class="btn btn-red" id="cancel" style="width: 75px" type="button" value="Cancel" name="cancel"
							runat="server">
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

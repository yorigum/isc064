<%@ Reference Page="~/Log.aspx" %>
<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.KontrakSertifikatEdit" CodeFile="KontrakSertifikatEdit.aspx.cs" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Edit Sertifikat</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link rel="stylesheet" type="text/css" href="/Media/Style.css">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Kontrak - Edit Sertifikat">
		<script type="text/javascript">
			function ValidateName(control, e)
			{
				if(control.value.length == 0 || !control.value.match(/[^\s]/))
				{
					alert(control + " harus diisi.");
					control.focus();
					
					if(window.event)
					{
						window.event.returnValue = false;
					}
					else
					{
						e.preventDefault();
					}
				}
			}
		</script>
	</head>
	<body class="body-padding" onkeyup="if(event.keyCode==27&amp;&amp;confirm('Kembali ke halaman proses KPR?')) document.getElementById('cancel').click()">
		<script type="text/javascript" src="/Js/Common.js"></script>
		<script type="text/javascript" src="/Js/NumberFormat.js"></script>
		<form id="Form1" class="cnt" method="post" runat="server">
			<input style="DISPLAY: none">
			<h1 class="title title-line">Edit 
				Sertifikat
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
					<td><asp:radiobuttonlist id="rblStatus" runat="server" repeatdirection="Horizontal" autopostback="True" onselectedindexchanged="rblStatus_SelectedIndexChanged">
							<asp:listitem value="0">BELUM DIKELUARKAN</asp:listitem>
							<asp:listitem value="2">SEDANG PROSES</asp:listitem>
							<asp:listitem value="3">BELUM BALIK NAMA</asp:listitem>
							<asp:listitem value="1">SELESAI</asp:listitem>
						</asp:radiobuttonlist></td>
				</tr>
			</table>
			<table id="selesai" cellspacing="5" runat="server">
				<tr id="atasnama" runat="server">
					<td width="150">Atas Nama
					</td>
					<td>:</td>
					<td><asp:textbox id="namaperusahaan" runat="server" maxlength="20" cssclass="txt" width="100"></asp:textbox></td>
				</tr>
				<tr>
					<td width="150">Tgl. Sertifikat</td>
					<td>:</td>
					<td><asp:textbox id="tbTgl" runat="server" cssclass="txt_center" width="75" font-size="8"></asp:textbox>
						<label for="tbTgl" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
						<asp:label id="lblTgl" runat="server" forecolor="Red"></asp:label></td>
				</tr>
				<tr>
					<td width="150">No. Sertifikat</td>
					<td>:</td>
					<td><asp:textbox id="tbNoSertifikat" runat="server" maxlength="20" cssclass="txt" width="100"></asp:textbox></td>
				</tr>
				<tr>
					<td width="150">Status Sertifikat</td>
					<td>:</td>
					<td><asp:radiobuttonlist id="statussertifikat" runat="server" repeatdirection="Horizontal" autopostback="True" onselectedindexchanged="statussertifikat_SelectedIndexChanged">
							<asp:listitem value="0" selected="True">HGB</asp:listitem>
							<asp:listitem value="1">Hak Milik</asp:listitem>
						</asp:radiobuttonlist></td>
				</tr>
				<tr id="sertifikat1" runat="server">
					<td>Jangka Waktu</td>
					<td>:</td>
					<td><asp:textbox id="jangkawaktu" runat="server" cssclass="txt_num" width="50"></asp:textbox>tahun<asp:label id="jangkawaktuc" runat="server" cssclass="err"></asp:label></td>
				</tr>
				<tr id="sertifikat2" runat="server">
					<td>Tgl. Berakhir Sertifikat</td>
					<td>:</td>
					<td><asp:textbox id="tglakhir" runat="server" cssclass="txt_center" width="75" font-size="8"></asp:textbox>
						<label for="tglakhir" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
						<asp:label id="tglakhirc" runat="server" forecolor="Red"></asp:label></td>
				</tr>
			</table>
			<table id="sedangproses" cellspacing="5" runat="server">
				<tr>
					<td width="150">No. Pengukuran Bidang</td>
					<td>:</td>
					<td><asp:textbox id="nomorukur" runat="server" maxlength="20" cssclass="txt" width="100"></asp:textbox></td>
				</tr>
				<tr>
					<td width="150">Tgl Ukur</td>
					<td>:</td>
					<td><asp:textbox id="tbTgl1" runat="server" cssclass="txt_center" width="75" font-size="8"></asp:textbox>
						<label for="tbTgl1" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
						<asp:label id="lblTgl1" runat="server" forecolor="Red"></asp:label></td>
				</tr>
				<tr>
					<td width="150">No. Surat Ukur</td>
					<td>:</td>
					<td><asp:textbox id="nomorsuratukur" runat="server" maxlength="20" cssclass="txt" width="100"></asp:textbox></td>
				</tr>
				<tr>
					<td width="150">Tgl Surat Ukur</td>
					<td>:</td>
					<td><asp:textbox id="tbTgl2" runat="server" cssclass="txt_center" width="75" font-size="8"></asp:textbox>
						<label for="tbTgl2" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
						<asp:label id="lblTgl2" runat="server" forecolor="Red"></asp:label></td>
				</tr>
				<tr>
					<td width="150">Jumlah Bidang</td>
					<td>:</td>
					<td><asp:textbox id="jumlahbidang" runat="server" maxlength="20" cssclass="txt" width="100"></asp:textbox></td>
				</tr>
			</table>
			<table height="50">
				<tr>
					<td><asp:button id="ok" runat="server" cssclass="btn btn-blue" width="75" text="OK" onclick="ok_Click"></asp:button></td>
					<td><input style="WIDTH: 75px" id="cancel" class="btn btn-red" value="Cancel" type="button" name="cancel"
							runat="server">
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>

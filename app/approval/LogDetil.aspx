<%@ Page language="c#" Inherits="ISC064.APPROVAL.LogDetil" CodeFile="LogDetil.aspx.cs" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Log File</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="4">
		<meta name="sec" content="Log File Detil">
		<meta http-equiv="pragma" content="no-cache">
		<base target="_self">
	</head>
	<body class="body-padding pop" onkeyup="if(event.keyCode==27)window.close()"
		onload="document.getElementById('ket').focus();">
		<form id="Form1" method="post" runat="server">
			<asp:dropdownlist id="akt1" runat="server" visible="False">
				<asp:listitem value="DAFTAR">DAFTAR = Pendaftaran Customer</asp:listitem>
				<asp:listitem value="EDIT">EDIT = Edit Customer</asp:listitem>
				<asp:listitem value="FOTO">FOTO = Edit Foto</asp:listitem>
				<asp:listitem value="DELETE">DELETE = Delete Customer (Permanen)</asp:listitem>
				<asp:listitem value="GABUNG">GABUNG = Prosedur Gabung Nomor</asp:listitem>
			</asp:dropdownlist>
			<asp:dropdownlist id="akt2" runat="server" visible="False">
				<asp:listitem value="DAFTAR">DAFTAR = Pendaftaran Reservasi</asp:listitem>
				<asp:listitem value="P-WL">P-WL = Print Kartu Waiting List</asp:listitem>
				<asp:listitem value="R-WL">R-WL = Otorisasi Reprint Kartu Waiting List</asp:listitem>
				<asp:listitem value="EDIT">EDIT = Edit Reservasi</asp:listitem>
				<asp:listitem value="PWL">PWL = Promote Waiting List</asp:listitem>
				<asp:listitem value="DELETE">DELETE = Delete Reservasi (Permanen)</asp:listitem>
			</asp:dropdownlist>
			<asp:dropdownlist id="akt3" runat="server" visible="False">
				<asp:listitem value="DAFTAR">DAFTAR = Pendaftaran Surat Pesanan</asp:listitem>
				<asp:listitem value="P-SP">P-SP = Print Surat Pesanan</asp:listitem>
				<asp:listitem value="R-SP">R-SP = Otorisasi Reprint Surat Pesanan</asp:listitem>
				<asp:listitem value="DISKON">DISKON = Prosedur Diskon</asp:listitem>
				<asp:listitem value="RT">RT = Reset Tagihan</asp:listitem>
				<asp:listitem value="CUSTOM">CUSTOM = Customize Tagihan</asp:listitem>
				<asp:listitem value="RESCHE">RESCHE = Reschedule Tagihan</asp:listitem>
				<asp:listitem value="KOMISI">KOMISI = Generate Komisi</asp:listitem>
				<asp:listitem value="RK">RK = Reset Komisi</asp:listitem>
				<asp:listitem value="P-RKOM">P-RKOM = Print Rencana Komisi</asp:listitem>
				<asp:listitem value="R-RKOM">R-RKOM = Otorisasi Reprint Rencana Komisi</asp:listitem>
				<asp:listitem value="PPJB">PPJB = Perjanjian Pengikatan Jual Beli</asp:listitem>
				<asp:listitem value="P-PPJB">P-PPJB = Print PPJB</asp:listitem>
				<asp:listitem value="R-PPJB">R-PPJB = Otorisasi Reprint PPJB</asp:listitem>
				<asp:listitem value="AJB">AJB = Akte Jual Beli</asp:listitem>
				<asp:listitem value="P-AJB">P-AJB = Print AJB</asp:listitem>
				<asp:listitem value="R-AJB">R-AJB = Otorisasi Reprint AJB</asp:listitem>
				<asp:listitem value="ST">ST = Serah Terima</asp:listitem>
				<asp:listitem value="P-BAST">P-BAST = Print BAST</asp:listitem>
				<asp:listitem value="R-BAST">R-BAST = Otorisasi Reprint BAST</asp:listitem>
				<asp:listitem value="GN">GN = Pengalihan Hak</asp:listitem>
				<asp:listitem value="GU">GU = Pindah Unit</asp:listitem>
				<asp:listitem value="BATAL">BATAL = Pembatalan Kontrak</asp:listitem>
				<asp:listitem value="EDIT">EDIT = Edit Kontrak</asp:listitem>
				<asp:listitem value="STATUS">STATUS = Edit Status Kontrak</asp:listitem>
				<asp:listitem value="EJT">EJT = Edit Jadwal Tagihan</asp:listitem>
				<asp:listitem value="EJK">EJK = Edit Jadwal Komisi</asp:listitem>
				<asp:listitem value="EAP">EAP = Edit Alokasi Pelunasan</asp:listitem>
				<asp:listitem value="REF">REF = Refresh Data Unit</asp:listitem>
				<asp:listitem value="DELETE">DELETE = Delete Kontrak (Permanen)</asp:listitem>
				<asp:listitem value="SK">SK = Solve Komisi</asp:listitem>
				<asp:listitem value="RD">RD = Realisasi Denda</asp:listitem>
			</asp:dropdownlist>
			<asp:dropdownlist id="akt4" runat="server" visible="False">
				<asp:listitem value="REGIS">REGIS = Registrasi Tanda Terima Reservasi</asp:listitem>
				<asp:listitem value="POST">POST = Posting Tanda Terima Reservasi</asp:listitem>
				<asp:listitem value="VOID">VOID = Void Tanda Terima Reservasi</asp:listitem>
				<asp:listitem value="P-TTR">P-TTR = Print Tanda Terima Reservasi</asp:listitem>
				<asp:listitem value="R-TTR">R-TTR = Otorisasi Reprint Tanda Terima Reservasi</asp:listitem>
				<asp:listitem value="EDIT">EDIT = Edit Tanda Terima Reservasi</asp:listitem>
			</asp:dropdownlist>
			<asp:dropdownlist id="akt5" runat="server" visible="False"></asp:dropdownlist>
			<script type="text/javascript">
			preva = MM_preloadImages('/Media/icon_prev_a.gif');
			prevo = MM_preloadImages('/Media/icon_prev_o.gif');
			prevc = MM_preloadImages('/Media/icon_prev_c.gif');
			nexta = MM_preloadImages('/Media/icon_next_a.gif');
			nexto = MM_preloadImages('/Media/icon_next_o.gif');
			nextc = MM_preloadImages('/Media/icon_next_c.gif');
			function MM_preloadImages() {
				x = new Image;
				x.src = MM_preloadImages.arguments[0];
				return x
			}
			function sc(foo,imgnew) {
				if (document.images) {foo.src=eval(imgnew + ".src");}
			}
			</script>
			<table cellspacing="5">
				<tr>
					<td><a id="prev" runat="server" class="abtn"><i class="fa fa-long-arrow-left"></i> <b>Prev</b></a></td>
					<td><a id="next" runat="server" class="abtn"><b>Next</b> <i class="fa fa-long-arrow-right"></i></a></td>
					<td><asp:button id="ok" runat="server" cssclass="btn btn-green" text="Approve" accesskey="a" onclick="ok_Click"></asp:button></td>
					<td>Approval :
						<asp:label id="approveinfo" runat="server"></asp:label>
					</td>
				</tr>
			</table>
			<table cellspacing="5">
				<tr>
					<td>Sumber</td>
					<td>:</td>
					<td>
						<asp:textbox id="sumber" runat="server" cssclass="txt" readonly="True" width="190"></asp:textbox>
					</td>
					<td width="10" rowspan="3"></td>
					<td>User</td>
					<td>:</td>
					<td>
						<asp:textbox id="user" runat="server" cssclass="txt" readonly="True" width="65"></asp:textbox>
					</td>
				</tr>
				<tr>
					<td>Nomor</td>
					<td>:</td>
					<td>
						<asp:textbox id="logid" runat="server" cssclass="txt" readonly="True" width="75"></asp:textbox>
					</td>
					<td>IP Addr.</td>
					<td>:</td>
					<td>
						<asp:textbox id="ip" runat="server" cssclass="txt" readonly="True"></asp:textbox>
					</td>
				</tr>
				<tr>
					<td>
						Referensi
					</td>
					<td>:</td>
					<td>
						<asp:textbox id="pk" runat="server" cssclass="txt" readonly="True" width="150"></asp:textbox>
					</td>
					<td>Tanggal</td>
					<td>:</td>
					<td>
						<asp:textbox id="tgl" runat="server" cssclass="txt" readonly="True" width="150"></asp:textbox>
					</td>
				</tr>
				<tr>
					<td>
						Aktivitas
					</td>
					<td>:</td>
					<td colspan="5">
						<asp:dropdownlist id="aktivitas" runat="server" cssclass="ddl" width="480"></asp:dropdownlist>
					</td>
				</tr>
			</table>
			<table cellspacing="5">
				<tr>
					<td>
						Deskripsi :
						<asp:textbox readonly="True" id="ket" runat="server" textmode="MultiLine" width="550" height="300"
							cssclass="txt"></asp:textbox>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>

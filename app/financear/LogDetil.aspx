<%@ Page language="c#" Inherits="ISC064.FINANCEAR.LogDetil" CodeFile="LogDetil.aspx.cs" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Log File</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
    	<link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
		<meta name="ctrl" content="4">
		<meta name="sec" content="Log File Detil">
		<meta http-equiv="pragma" content="no-cache">
		<base target="_self">
	</head>
	<body class="body-padding pop" onkeyup="if(event.keyCode==27)window.close()"
		onload="document.getElementById('ket').focus();">
		<form id="Form1" method="post" runat="server">
			<asp:dropdownlist id="akt1" runat="server" visible="False">
				<asp:listitem value="REGIS">REGIS = Registrasi Tanda Terima Sementara</asp:listitem>
				<asp:listitem value="POST">POST = Posting Tanda Terima Sementara</asp:listitem>
				<asp:listitem value="VOID">VOID = Void Tanda Terima Sementara</asp:listitem>
				<asp:listitem value="P-TTS">P-TTS = Print Tanda Terima Sementara</asp:listitem>
				<asp:listitem value="R-TTS">R-TTS = Otorisasi Reprint Tanda Terima Sementara</asp:listitem>
				<asp:listitem value="P-BKM">P-BKM = Print Bukti Kas Masuk</asp:listitem>
				<asp:listitem value="R-BKM">R-BKM = Otorisasi Reprint Bukti Kas Masuk</asp:listitem>
				<asp:listitem value="TOLAK">TOLAK = Tolakan Cek Giro</asp:listitem>
				<asp:listitem value="GANTI">GANTI = Penggantian Cek Giro</asp:listitem>
				<asp:listitem value="EAP">EAP = Edit Alokasi Pelunasan</asp:listitem>
				<asp:listitem value="EDIT">EDIT = Edit Tanda Terima Sementara</asp:listitem>
			</asp:dropdownlist>
			<asp:dropdownlist id="akt2" runat="server" visible="False">
				<asp:listitem value="REGIS">REGIS = Registrasi Transfer Anonim</asp:listitem>
				<asp:listitem value="EDIT">EDIT = Edit Transfer Anonim</asp:listitem>
				<asp:listitem value="DELETE">DELETE = Delete Transfer Anonim</asp:listitem>
			</asp:dropdownlist>
			<asp:dropdownlist id="akt3" runat="server" visible="False">
				<asp:listitem value="REGIS">REGIS = Registrasi Account</asp:listitem>
				<asp:listitem value="EDIT">EDIT = Edit Account</asp:listitem>
				<asp:listitem value="DELETE">DELETE = Delete Account</asp:listitem>
			</asp:dropdownlist>
			<asp:dropdownlist id="akt4" runat="server" visible="False">
				<asp:listitem value="REGIS">REGIS = Registrasi Kas Masuk</asp:listitem>
				<asp:listitem value="EDIT">EDIT = Edit Kas Masuk</asp:listitem>
				<asp:listitem value="P-KM">P-KM = Print Kas Masuk</asp:listitem>
				<asp:listitem value="R-KM">R-KM = Otorisasi Reprint Kas Masuk</asp:listitem>
			</asp:dropdownlist>
			<asp:dropdownlist id="akt5" runat="server" visible="False">
				<asp:listitem value="REGIS">REGIS = Registrasi Kas Keluar</asp:listitem>
				<asp:listitem value="EDIT">EDIT = Edit Kas Keluar</asp:listitem>
				<asp:listitem value="P-KK">P-KK = Print Kas Keluar</asp:listitem>
				<asp:listitem value="R-KK">R-KK = Otorisasi Reprint Kas Keluar</asp:listitem>
			</asp:dropdownlist>
            <asp:dropdownlist id="akt6" runat="server" visible="False">
				<asp:listitem value="REGIS">REGIS = Registrasi Memo</asp:listitem>
				<asp:listitem value="EDIT">EDIT = Edit Memo</asp:listitem>
				<asp:listitem value="P-MEMO">P-KK = Print Memo</asp:listitem>
				<asp:listitem value="R-MEMO">R-KK = Otorisasi Reprint Memo</asp:listitem>
			</asp:dropdownlist>
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
					<td><a id="prev" runat="server"><img src="/Media/icon_prev_a.gif" onmouseover="sc(this,'prevo')" onmousedown="sc(this,'prevc')"
								onmouseout="sc(this,'preva')" border="0"></a></td>
					<td><a id="next" runat="server"><img src="/Media/icon_next_a.gif" onmouseover="sc(this,'nexto')" onmousedown="sc(this,'nextc')"
								onmouseout="sc(this,'nexta')" border="0"></a></td>
					<td><asp:button id="ok" runat="server" cssclass="btn btn-blue" text="Approve" accesskey="a" onclick="ok_Click"></asp:button></td>
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
					<td style="vertical-align:top">
						Deskripsi
					</td>
					<td style="vertical-align:top">:</td>
                    <td>
						<asp:textbox readonly="True" id="ket" runat="server" textmode="MultiLine" width="550" height="300"
							cssclass="txt"></asp:textbox>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>

<%@ Page language="c#" Inherits="ISC064.SECURITY.LogDetil" CodeFile="LogDetil.aspx.cs" %>
<!DOCTYPE html>
<html lang="en">
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
				<asp:listitem value="PUB">PUB = Pendaftaran Username Baru</asp:listitem>
				<asp:listitem value="EDU">EDU = Edit User</asp:listitem>
				<asp:listitem value="SPB">SPB = Set Password Baru</asp:listitem>
				<asp:listitem value="AU">AU = Aktivasi Username</asp:listitem>
				<asp:listitem value="BU">BU = Blokir Username</asp:listitem>
				<asp:listitem value="MKA">MKA = Matrikulasi Kode Akses</asp:listitem>
				<asp:listitem value="CL">CL = Customize Security Level</asp:listitem>
				<asp:listitem value="DEL">DEL = Delete Username (Permanen)</asp:listitem>
			</asp:dropdownlist>
			<asp:dropdownlist id="akt2" runat="server" visible="False">
				<asp:listitem value="DAFTAR">DAFTAR = Pendaftaran Security Level Baru</asp:listitem>
				<asp:listitem value="EDIT">EDIT = Edit Security Level</asp:listitem>
				<asp:listitem value="DEL">DEL = Delete Security Level (Permanen)</asp:listitem>
			</asp:dropdownlist>
			<asp:dropdownlist id="akt3" runat="server" visible="False">
				<asp:listitem value="EDIT">EDIT = Edit Master Data</asp:listitem>
			</asp:dropdownlist>
			<asp:dropdownlist id="akt4" runat="server" visible="False"></asp:dropdownlist>
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
						<asp:dropdownlist id="aktivitas" runat="server" cssclass="ddl form-control" width="480"></asp:dropdownlist>
					</td>
				</tr>
				<tr>
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

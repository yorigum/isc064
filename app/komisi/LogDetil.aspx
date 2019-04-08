<%@ Page language="c#" Inherits="ISC064.KOMISI.LogDetil" CodeFile="LogDetil.aspx.cs" %>
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
				<asp:listitem value="REGIS">REGIS = Registrasi Skema Komisi</asp:listitem>
				<asp:listitem value="EDIT">EDIT = Edit Skema Komisi</asp:listitem>
				<asp:listitem value="DELETE">DELETE = Delete Skema Komisi</asp:listitem>
			</asp:dropdownlist>
            <asp:dropdownlist id="akt2" runat="server" visible="False">
				<asp:listitem value="REGIS">REGIS = Registrasi Termin Komisi</asp:listitem>
				<asp:listitem value="EDIT">EDIT = Edit Termin Komisi</asp:listitem>
				<asp:listitem value="DELETE">DELETE = Delete Termin Komisi</asp:listitem>
			</asp:dropdownlist>
			<asp:dropdownlist id="akt3" runat="server" visible="False">
				<asp:listitem value="REGIS">REGIS = Registrasi Skema Closing Fee</asp:listitem>
				<asp:listitem value="EDIT">EDIT = Edit Skema Skema Closing Fee</asp:listitem>
				<asp:listitem value="DELETE">DELETE = Delete Skema Skema Closing Fee</asp:listitem>
			</asp:dropdownlist>
            <asp:dropdownlist id="akt4" runat="server" visible="False">
				<asp:listitem value="REGIS">REGIS = Registrasi Skema Reward</asp:listitem>
				<asp:listitem value="EDIT">EDIT = Edit Skema Reward</asp:listitem>
				<asp:listitem value="DELETE">DELETE = Delete Skema Reward</asp:listitem>
			</asp:dropdownlist>  
            <asp:dropdownlist id="akt5" runat="server" visible="False">
				<asp:listitem value="REGIS">REGIS = Registrasi Komisi</asp:listitem>
				<asp:listitem value="DELETE">DELETE = Delete Komisi</asp:listitem>
			</asp:dropdownlist>          
            <asp:dropdownlist id="akt6" runat="server" visible="False">
				<asp:listitem value="REGIS">REGIS = Registrasi Pengajuan Komisi</asp:listitem>
				<asp:listitem value="EDIT">EDIT = Edit Pengajuan Komisi</asp:listitem>
				<asp:listitem value="DELETE">DELETE = Delete Pengajuan Komisi</asp:listitem>
			</asp:dropdownlist>  
            <asp:dropdownlist id="akt7" runat="server" visible="False">
				<asp:listitem value="REGIS">REGIS = Registrasi Realisasi Komisi</asp:listitem>
				<asp:listitem value="EDIT">EDIT = Edit Realisasi Komisi</asp:listitem>
				<asp:listitem value="DELETE">DELETE = Delete Realisasi Komisi</asp:listitem>
			</asp:dropdownlist>  
            <asp:dropdownlist id="akt8" runat="server" visible="False">
				<asp:listitem value="REGIS">REGIS = Registrasi Closing Fee</asp:listitem>
				<asp:listitem value="DELETE">DELETE = Delete Closing Fee</asp:listitem>
			</asp:dropdownlist>          
            <asp:dropdownlist id="akt9" runat="server" visible="False">
				<asp:listitem value="REGIS">REGIS = Registrasi Pengajuan Closing Fee</asp:listitem>
				<asp:listitem value="EDIT">EDIT = Edit Pengajuan Closing Fee</asp:listitem>
				<asp:listitem value="DELETE">DELETE = Delete Pengajuan Closing Fee</asp:listitem>
			</asp:dropdownlist>  
            <asp:dropdownlist id="akt10" runat="server" visible="False">
				<asp:listitem value="REGIS">REGIS = Registrasi Realisasi Closing Fee</asp:listitem>
				<asp:listitem value="EDIT">EDIT = Edit Realisasi Closing Fee</asp:listitem>
				<asp:listitem value="DELETE">DELETE = Delete Realisasi Closing Fee</asp:listitem>
			</asp:dropdownlist>  
            <asp:dropdownlist id="akt11" runat="server" visible="False">
				<asp:listitem value="REGIS">REGIS = Registrasi Reward</asp:listitem>
				<asp:listitem value="DELETE">DELETE = Delete Reward</asp:listitem>
			</asp:dropdownlist>          
            <asp:dropdownlist id="akt12" runat="server" visible="False">
				<asp:listitem value="REGIS">REGIS = Registrasi Pengajuan Reward</asp:listitem>
				<asp:listitem value="EDIT">EDIT = Edit Pengajuan Reward</asp:listitem>
				<asp:listitem value="DELETE">DELETE = Delete Pengajuan Reward</asp:listitem>
			</asp:dropdownlist>  
            <asp:dropdownlist id="akt13" runat="server" visible="False">
				<asp:listitem value="REGIS">REGIS = Registrasi Realisasi Reward</asp:listitem>
				<asp:listitem value="EDIT">EDIT = Edit Realisasi Reward</asp:listitem>
				<asp:listitem value="DELETE">DELETE = Delete Realisasi Reward</asp:listitem>
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
					<td><asp:button id="ok" runat="server" cssclass="btn" text="Approve" accesskey="a" onclick="ok_Click"></asp:button></td>
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

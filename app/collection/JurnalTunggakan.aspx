<%@ Page language="c#" Inherits="ISC064.COLLECTION.JurnalTunggakan" CodeFile="JurnalTunggakan.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="HeadTunggakan" Src="HeadTunggakan.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavTunggakan" Src="NavTunggakan.ascx" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Jurnal Surat Peringatan</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Jurnal Surat Peringatan">
	</head>
	<body onkeyup="if(event.keyCode==27)window.close()">
		<form id="Form1" method="post" runat="server">
            <div class="content-header">
			    <uc1:navtunggakan id="NavTunggakan1" runat="server" aktif="2"></uc1:navtunggakan>
            </div>
			<div class="tabdata">
				<div class="pad">
					<uc1:headtunggakan id="HeadTunggakan1" runat="server"></uc1:headtunggakan>
					<input type="text" style="display:none">
					<table cellspacing="5">
						<tr>
							<td colspan="3">
								<asp:radiobuttonlist id="akt" runat="server" repeatcolumns="3" repeatdirection="Vertical">
									<asp:listitem selected="True">TELEPON CUSTOMER</asp:listitem>
									<asp:listitem>KIRIM SURAT</asp:listitem>
									<asp:listitem>DEBT COLLECTOR</asp:listitem>
									<asp:listitem>PENGADILAN</asp:listitem>
									<asp:listitem>PEMBATALAN</asp:listitem>
									<asp:listitem>SETTLEMENT</asp:listitem>
									<asp:listitem>LAINNYA</asp:listitem>
								</asp:radiobuttonlist>
							</td>
						</tr>
						<tr>
							<td>Keterangan Tambahan</td>
							<td>:</td>
							<td>
								<asp:textbox id="baru" runat="server" cssclass="input-text" width="500"></asp:textbox>
							</td>
						</tr>
						<tr>
							<td>File Hasil Scan</td>
							<td>:</td>
							<td>
								<input type="file" id="file" runat="server" class="input-text" style="width:568px" name="file">
							</td>
						</tr>
					</table>
					<table height="50">
						<tr>
							<td>
								<asp:LinkButton id="save" runat="server" cssclass="btn btn-blue" width="75" onclick="save_Click"><i class="fa fa-share"></i> OK</asp:LinkButton>
							</td>
							<td>
								<input id="cancel" type="button" class="btn btn-red" value="Cancel" name="cancel"
									runat="server">
							</td>
							<td style="padding-left:10px">
								<p class="feed">
									<asp:label id="feed" runat="server"></asp:label>
								</p>
							</td>
						</tr>
					</table>
					<asp:table id="rpt" runat="server" cssclass="tb blue-skin" cellspacing="1" style="min-width:80%">
						<asp:tablerow horizontalalign="Left">
							<asp:tableheadercell width="65">Tgl</asp:tableheadercell>
							<asp:tableheadercell width="45">Jam</asp:tableheadercell>
							<asp:tableheadercell width="200" columnspan="2">User</asp:tableheadercell>
							<asp:tableheadercell width="340">Keterangan</asp:tableheadercell>
							<asp:tableheadercell>File</asp:tableheadercell>
						</asp:tablerow>
					</asp:table>
				</div>
			</div>
			<script type="text/javascript">
			function popGambar(foo){
				openPopUp(foo,700,500);
			}
			</script>
		</form>
	</body>
</html>

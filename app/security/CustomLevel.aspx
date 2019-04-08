<%@ Page language="c#" Inherits="ISC064.SECURITY.CustomLevel" CodeFile="CustomLevel.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="HeadUser" Src="HeadUser.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavUser" Src="NavUser.ascx" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Customize Security Level</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
    	<link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Username - Customize Security Level">
	</head>
	<body onkeyup="if(event.keyCode==27&&confirm('Apakah anda ingin menutup layar edit ini?')) document.getElementById('cancel').click()">
		<form id="Form1" method="post" runat="server">
			<div class="content-header">
				<uc1:navuser id="NavUser1" runat="server" aktif="3"></uc1:navuser>
			</div>
			<div class="tabdata">
				<div class="pad">
					<uc1:headuser id="HeadUser1" runat="server"></uc1:headuser>
					<p style="padding:7px">
						<asp:listbox id="daftarmodul" runat="server" cssclass="ddl" width="500" rows="10">
							<asp:listitem value="">Semua Modul...</asp:listitem>
						</asp:listbox>
						<br>
						<input id="display" runat="server" type="button" class="btn btn-blue" value="Display" style="WIDTH:75px"
							name="display"> <input id="cancel" type="button" onclick="window.close()" class="btn btn-red" value="Cancel" style="WIDTH:70px">
					</p>
					<div id="data" runat="server">
						<div style="padding-left:3px">
							<p class="feed">
								<asp:label id="feed" runat="server"></asp:label>
							</p>
						</div>
						<table class="tb blue-skin" cellspacing="1">
							<tr align="left">
								<th>
									Konfigurasi Khusus</th>
								<th width="200">
									Keterangan</th>
								<th>
									Halaman</th>
							</tr>
							<tr>
								<td colspan="3">
									<a href="javascript:if(confirm('Reset semua konfigurasi khusus?')){resetnormal()}">reset 
										ke normal...</a>
								</td>
							</tr>
							<asp:placeholder id="list" runat="server"></asp:placeholder>
						</table>
						<table style="height:50px">
							<tr>
								<td>
									<asp:LinkButton id="ok" runat="server" cssclass="btn btn-blue" width="75" onclick="ok_Click"><i class="fa fa-share"></i> OK</asp:LinkButton>
								</td>
								<td>
									<asp:Linkbutton id="save" runat="server" cssclass="btn btn-orange" width="75" accesskey="a" onclick="save_Click"><i class="fa fa-check"></i> Apply </asp:Linkbutton>
								</td>
							</tr>
						</table>
					</div>
				</div>
			</div>
			<script type="text/javascript">
			function resetnormal() {
				for(i=0;i<<%printIndex();%>;i++)
					document.getElementById("no_"+i).checked=true;
				
				document.getElementById('save').click();
			}
			function gantiModul(userid,foo) {
				if(foo.selectedIndex!=-1)
					location.href='CustomLevel.aspx?UserID='+userid+'&Modul='+foo.options[foo.selectedIndex].value;
			}
			</script>
		</form>
	</body>
</html>

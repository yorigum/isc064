<%@ Page language="c#" Inherits="ISC064.SECURITY.TabelAbsensi" CodeFile="TabelAbsensi.aspx.cs" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Tabel Absensi</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
    	<link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
		<meta name="ctrl" content="1">
		<script type="text/javascript" src="/Js/JQuery.min.js"></script>
		<link href="/plugins/bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css" rel="stylesheet" />
		<link href="/plugins/bootstrap-datepicker/dist/css/datetime.css" rel="stylesheet" />
		<script src="/plugins/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"></script>
		<meta name="sec" content="Absensi - Tabel Absensi">
	</head>
	<body class="body-padding" onkeyup="if(event.keyCode==27){window.close()}">
		<script type="text/javascript" src="/Js/Pop.js"></script>
		<form id="Form1" method="post" runat="server">
			<input type="text" style="display:none">
			<div class="pad">
				<table>
					<tr>
						<td><b>Tanggal :</b></td>
						<td>
							<asp:textbox id="date" runat="server" cssclass="txt_center igroup" width="85"></asp:textbox>
							<label type="button" class="btn btn-cal" for="date">
								<i class="fa fa-calendar"></i>
							</label>
						</td>
						<td>
							&nbsp;&nbsp;
						</td>
						<td>
							<asp:button id="display" runat="server" text="Display" cssclass="btn btn-blue" onclick="display_Click"></asp:button>
						</td>
						<td>
							&nbsp;&nbsp;
						</td>
						<td>
							<asp:label id="datec" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>
				</table>
			</div>
			<br />
			<asp:table id="rpt" runat="server" cssclass="tb blue-skin" cellspacing="1">
				<asp:tablerow>
					<asp:tablecell columnspan="6">Diurutkan atas <u>Jam Masuk</u></asp:tablecell>
				</asp:tablerow>
				<asp:tablerow horizontalalign="Left">
					<asp:tableheadercell width="75">Jam<br>Masuk</asp:tableheadercell>
					<asp:tableheadercell width="75">Jam<br>Keluar</asp:tableheadercell>
					<asp:tableheadercell width="75">Kode<br>User</asp:tableheadercell>
					<asp:tableheadercell width="200">Nama<br>User</asp:tableheadercell>
					<asp:tableheadercell width="80">Security<br>Level</asp:tableheadercell>
					<asp:tableheadercell width="80">IP<br>Address</asp:tableheadercell>
				</asp:tablerow>
			</asp:table>
		</form>
		<script type="text/javascript">
			$(document).ready(function(){
				$('#date').datepicker({
		            autoclose: true,
		            format: 'dd M yyyy',
		            language: 'id'
		        });
			})
		</script>
	</body>
</html>

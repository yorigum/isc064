<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.UnitWL" CodeFile="UnitWL.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="HeadUnit" Src="HeadUnit.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavUnit" Src="NavUnit.ascx" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Waiting List</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Unit - Waiting List">
	</head>
	<body onkeyup="if(event.keyCode==27) window.close();">
		<form id="Form1" method="post" runat="server">
            <div class="content-header">
			    <uc1:navunit id="NavUnit1" runat="server" aktif="2"></uc1:navunit>
            </div>
			<div class="tabdata">
				<div class="pad">
					<uc1:headunit id="HeadUnit1" runat="server"></uc1:headunit>
					<table cellspacing="5">
						<tr>
							<td>
								Kandidat Pemenang :
								<br>
								<asp:label id="win" runat="server" font-bold="True" font-size="14"></asp:label>
							</td>
						</tr>
					</table>
					<br>
					<asp:table id="rpt" runat="server" cellspacing="1" cssclass="tb blue-skin">
						<asp:tablerow horizontalalign="Left">
							<asp:tableheadercell columnspan="2">No. Urut</asp:tableheadercell>
							<asp:tableheadercell width="50">No.</asp:tableheadercell>
							<asp:tableheadercell width="75">Tgl.</asp:tableheadercell>
							<asp:tableheadercell width="120">Batas Waktu</asp:tableheadercell>
							<asp:tableheadercell width="170">Customer / Sales</asp:tableheadercell>
							<asp:tableheadercell width="170">Keterangan</asp:tableheadercell>
						</asp:tablerow>
					</asp:table>
				</div>
			</div>
			<script type="text/javascript">
				function promote(nomor)
				{
					if(confirm('Lanjutkan proses perubahan urutan waiting list?'))
					{
						location.href='ReservasiPromote.aspx?NoReservasi='+nomor;
					}
				}
			</script>
		</form>
	</body>
</html>

<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.DaftarReservasi" CodeFile="DaftarReservasi.aspx.cs" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Daftar Reservasi</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="(Pop-Up) Daftar Reservasi">
		<meta http-equiv="pragma" content="no-cache">
		<base target="_self">
	</head>
	<body class="body-padding pop" onkeyup="if(event.keyCode==27){window.close()}"
		onload="document.getElementById('keyword').select()">
		<form id="Form1" method="post" runat="server">
			<div class="pad">
				<table>
					<tr>
						<td>
							<asp:textbox id="keyword" runat="server" cssclass="txt" width="400"></asp:textbox>
						</td>
						<td>
							<asp:dropdownlist id="metode" runat="server" cssclass="ddl form-control" style='width: 100%'>
								<asp:listitem>Semua</asp:listitem>
								<asp:listitem>Reservasi Aktif</asp:listitem>
								<asp:listitem>Reservasi Expire</asp:listitem>
							</asp:dropdownlist>
						</td>
						<td>
							<asp:dropdownlist id="project" runat="server" cssclass="ddl form-control" style='width: 100%'>
							</asp:dropdownlist>
						</td>
						<td>
							<asp:button id="search" runat="server" cssclass="btn btn-blue" text="Search" accesskey="s" onclick="search_Click"></asp:button>
						</td>
					</tr>
				</table>
			</div>
			<br>
            <asp:GridView ID="tb" runat="server" SkinID="pager" OnPageIndexChanging="tb_PageIndexChanging">
                <Columns>
                    <asp:BoundField HeaderText="No." DataField="No" />
                    <asp:BoundField HeaderText="Unit" DataField="Unit" />
                    <asp:BoundField HeaderText="No. Urut" DataField="NoUrut" />
                    <asp:BoundField HeaderText="Tgl" DataField="Tgl" />
                    <asp:BoundField HeaderText="Customer" DataField="Customer" />
                    <asp:BoundField HeaderText="Batas Waktu" DataField="BatasWaktu" />
                    <asp:BoundField HeaderText="Project" DataField="Project" />
                </Columns>
            </asp:GridView>

			<input type="text" style="DISPLAY:none">
			<script type="text/javascript">
			function call(nomor)
			{
			    window.parent.call(nomor);
			    window.parent.globalclose();
			}
			</script>
		</form>
	</body>
</html>

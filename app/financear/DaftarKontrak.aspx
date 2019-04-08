<%@ Page language="c#" Inherits="ISC064.FINANCEAR.DaftarKontrak" CodeFile="DaftarKontrak.aspx.cs" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Daftar Kontrak</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="(Pop-Up) Daftar Kontrak">
		<meta http-equiv="pragma" content="no-cache">
		<base target="_self">
		<style type="text/css">
		#dip
		{
			border: 2px outset;
	        padding: 5px;
	        height: 100%;
		}
		</style>
	</head>
	<body class="pop" onkeyup="if(event.keyCode==27){window.close()}"
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
								<asp:listitem>Kontrak Aktif</asp:listitem>
								<asp:listitem>Kontrak Batal</asp:listitem>
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
			<p><asp:label id="info" runat="server"><br>
				</asp:label></p>
            <asp:GridView ID="tb" runat="server" SkinID="pager" OnPageIndexChanging="tb_PageIndexChanging">
                <Columns>
                    <asp:BoundField HeaderText="No. Kontrak" DataField="NoKontrak" />
                    <asp:BoundField HeaderText="Unit" DataField="Unit" />
                    <asp:BoundField HeaderText="Tgl." DataField="Tanggal" />
                    <asp:BoundField HeaderText="Customer / Sales" DataField="Customer" />
                    <asp:BoundField HeaderText="Keterangan" DataField="Keterangan" />
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
			function callSource(nomor, source)
			{
			    window.parent.callSource(nomor, source);
			    window.parent.globalclose();
			}
			</script>
		</form>
	</body>
</html>

<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.DaftarUnit" CodeFile="DaftarUnit.aspx.cs" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Daftar Unit</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="(Pop-Up) Daftar Unit">
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
							<asp:dropdownlist id="metode" runat="server" cssclass="ddl form-control">
								<asp:listitem>Semua</asp:listitem>
								<asp:listitem>Unit Aktif</asp:listitem>
								<asp:listitem>Unit Blokir</asp:listitem>
							</asp:dropdownlist>
						</td>
						<td>
							<asp:dropdownlist id="project" runat="server" cssclass="ddl form-control">
							</asp:dropdownlist>
						</td>
						<td>
							<asp:button id="search" runat="server" cssclass="btn btn-blue" text="Search" accesskey="s" onclick="search_Click"></asp:button>
						</td>
					</tr>
				</table>
			</div>
			<br>
			<p style="font:8pt;padding:3">
				Harga price list adalah dalam rupiah.
			</p>
            <asp:GridView ID="tb" runat="server" SkinID="pager" OnPageIndexChanging="tb_PageIndexChanging">
                <Columns>
                    <asp:BoundField HeaderText="No. Stock" DataField="NoStock" />
                    <asp:BoundField HeaderText="Unit" DataField="NoUnit" />
                    <asp:BoundField HeaderText="Keterangan" DataField="Keterangan" />
                    <asp:BoundField HeaderText="Luas Semi Gross" DataField="LuasSG" />
                    <asp:BoundField HeaderText="Luas Nett" DataField="LuasNett" />
                    <asp:BoundField HeaderText="Price List" DataField="PL" />
                    <asp:BoundField HeaderText="Project" DataField="Project" />
                </Columns>
            </asp:GridView>
			<input type="text" style="display:none;">
			<script type="text/javascript">
			function call(nomor)
			{
			    window.parent.call(nomor);
			    window.parent.globalclose();
				
			}
			function call2(nomor,nounit)
			{
			    window.parent.call(nomor, nounit);
			    window.parent.globalclose();
				
			}
			function call3(nomor, nounit, price) {
			    window.parent.call3(nomor, nounit, price);
			    window.parent.globalclose();
			}
			
			</script>
		</form>
	</body>
</html>

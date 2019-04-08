<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.DaftarCustomer" CodeFile="DaftarCustomer.aspx.cs" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Daftar Customer</title>
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="(Pop-Up) Daftar Customer">
		<meta http-equiv="pragma" content="no-cache">
		<base target="_self">
	</head>
	<body class="body-padding pop" onkeyup="if(event.keyCode==27){window.close()}"
		onload="document.getElementById('keyword').select()">
		<form id="Form1" method="post" runat="server">
        <asp:ScriptManager runat="server" ID="script" EnablePartialRendering="true"></asp:ScriptManager>
        <asp:UpdatePanel runat="server" ID="update" UpdateMode="Conditional">
            <ContentTemplate>
			<div class="pad">
				<table>
					<tr>
						<td>
							<asp:textbox id="keyword" runat="server" cssclass="txt" width="400"></asp:textbox>
						</td>
						<td>
							<asp:dropdownlist id="metode" runat="server" cssclass="ddl form-control" style='width: 100%'>
								<asp:listitem>Semua</asp:listitem>
								<asp:listitem>Customer Aktif</asp:listitem>
								<asp:listitem>Customer Inaktif</asp:listitem>
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
                <p>
                    <label runat="server" id="info"></label>
                </p>
            <asp:GridView ID="tb" runat="server" PageSize="5" OnPageIndexChanging="tb_PageIndexChanging" SkinID="pager">
                <Columns>
                    <asp:BoundField DataField="Nama" HeaderText="Nama" ItemStyle-Width="350" />
                    <asp:BoundField DataField="Alamat" HeaderText="Alamat" ItemStyle-Width="350" />                    
                </Columns>
            </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
        <input type="text" style="display: none;">
        <script type="text/javascript">
            function call(nomor,nama) {
                window.parent.call(nomor,nama);
                window.parent.globalclose();
            }
        </script>
    </form>
</body>
</html>

<%@ Page language="c#" Inherits="ISC064.LEGAL.DaftarKontrak" CodeFile="DaftarKontrak.aspx.cs" %>
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
        #dip {
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
                        <asp:TextBox ID="keyword" runat="server" CssClass="txt" Width="400"></asp:TextBox>
                    </td>
                    <td>
                        <asp:DropDownList ID="metode" runat="server" CssClass="txt">
                            <asp:ListItem>Semua</asp:ListItem>
                            <asp:ListItem>Kontrak Aktif</asp:ListItem>
                            <asp:ListItem>Kontrak Batal</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="project" runat="server" CssClass="txt">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="search" runat="server" CssClass="btn btn-blue" Text="Search" AccessKey="s" OnClick="search_Click"></asp:Button>
                    </td>
                </tr>
            </table>
        </div>
        <br>
        <p>
            <asp:Label ID="info" runat="server"><br>
            </asp:Label>
        </p>
        <div class="pad">
            <asp:GridView ID="tb" runat="server" SkinID="pager" OnPageIndexChanging="tb_PageIndexChanging">
                <Columns>
                    <asp:BoundField HeaderText="No. Kontrak" DataField="Kontrak" />
                    <asp:BoundField HeaderText="Unit " DataField="Unit" />
                    <asp:BoundField HeaderText="Tgl." DataField="Tgl" />
                    <asp:BoundField HeaderText="Customer / Agent" DataField="Customer" />
                    <asp:BoundField HeaderText="Keterangan" DataField="Keterangan" />
                    <asp:BoundField HeaderText="Project" DataField="Project" />
                </Columns>
            </asp:GridView>
        </div>
        <input type="text" style="display: none">
        <script type="text/javascript">
            function call(nomor) {
                window.parent.call(nomor);
                window.parent.globalclose();
			}
			
			function callSource(nomor, source)
			{
			    window.close();
				window.opener.callSource(nomor, source);
			}
			</script>
		</form>
	</body>
</html>

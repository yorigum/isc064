<%@ Page Language="c#" Inherits="ISC064.MARKETINGJUAL.Unit" CodeFile="Unit.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Master Unit</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Unit">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Master Unit</h1>
        <br>
        <table style="border: 1px solid #DCDCDC" cellspacing="10">
            <tr>
                <td colspan="3">
                    <input type="button" class="btn btn-blue" value="Search" show-modal='#ModalPopUp' modal-title='Daftar Unit' modal-url='DaftarUnit.aspx' id="search"
                        runat="server" name="search" accesskey="s">
                </td>
            </tr>
            <tr>
                <td><b>View by</b></td>
                <td>
                    <asp:DropDownList ID="jenis" runat="server" Width="200">
                        <asp:ListItem Value="">Tipe Unit :</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="lokasi" runat="server" Width="200">
                        <asp:ListItem Value="">Lokasi :</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="status" runat="server" Width="200">
                        <asp:ListItem>Status :</asp:ListItem>
                        <asp:ListItem>Unit Aktif</asp:ListItem>
                        <asp:ListItem>Unit Blokir</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="project" runat="server" Width="200" AutoPostBack="true" OnSelectedIndexChanged="project_SelectedIndexChanged">
                        <asp:ListItem>Project :</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="display" runat="server" CssClass="btn btn-blue" Text="Display" OnClick="display_Click"></asp:Button>
                </td>
            </tr>
        </table>
        <br />
        <div class="peach">
            Price list dalam rupiah.
        </div>
        <asp:GridView ID="tb" runat="server" SkinID="pager" OnPageIndexChanging="tb_PageIndexChanging">
            <Columns>
                <asp:BoundField HeaderText="No. Stock" DataField="NoStock" ItemStyle-Width="150" />
                <asp:BoundField HeaderText="Status" DataField="Status" />
                <asp:BoundField HeaderText="Unit" DataField="NoUnit" ItemStyle-Width="100" />
                <asp:BoundField HeaderText="Tipe Unit" DataField="Tipe" ItemStyle-Width="200" />
                <asp:BoundField HeaderText="Luas Tanah" DataField="LuasSG" ItemStyle-Width="250" />
                <asp:BoundField HeaderText="Luas Bangunan" DataField="LuasNett" ItemStyle-Width="200" />
                <asp:BoundField HeaderText="Price List" DataField="PL" ItemStyle-Width="100" />
				<asp:BoundField HeaderText="Project" DataField="Project" ItemStyle-Width="100" />
                <asp:BoundField DataField="Nav" />
            </Columns>
        </asp:GridView>
        <script type="text/javascript">
            function call(nomor) {
                popUnit(nomor);
            }
        </script>
    </form>
</body>
</html>

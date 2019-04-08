<%@ Page Language="c#" Inherits="ISC064.ADMINJUAL.Unit" CodeFile="Unit.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <title>Master Unit</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Unit">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Master Unit</h1>
        <br>
        <asp:ScriptManager ID="scriptmanager1" runat="server" EnablePartialRendering="true"></asp:ScriptManager>
        <asp:UpdatePanel ID="updPanel" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
        <table style="border: 1px solid #DCDCDC" cellspacing="5">
            <tr>
                <td>
                    <input type="button" class="btn btn-blue" value="Search" show-modal="#ModalPopUp" modal-url="DaftarUnit.aspx" modal-title="List Unit" id="search"
                        runat="server" name="search" accesskey="s">
                </td>
                <td>
                    <asp:DropDownList ID="jenis" runat="server" CssClass="ddl form-control" Width="230">
                        <asp:ListItem Value="">Jenis :</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="lokasi" runat="server" CssClass="ddl form-control" Width="170">
                        <asp:ListItem Value="">Lokasi :</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="status" runat="server" CssClass="ddl form-control">
                        <asp:ListItem>Status :</asp:ListItem>
                        <asp:ListItem>Available</asp:ListItem>
                        <asp:ListItem>Sold</asp:ListItem>
                        <asp:ListItem>Hold</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="project" runat="server" CssClass="ddl form-control" OnSelectedIndexChanged="project_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem>Project : </asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="display" runat="server" CssClass="btn btn-blue" Text="Display" OnClick="display_Click"></asp:Button>
                </td>
            </tr>
        </table>
        <br />
        <div class="peach">
            Status : A = Available / S = Sold / H = Hold Internal.
			<br />
            Price list dalam rupiah.
            <br />
        </div>
        <br />
        <asp:GridView ID="tb" runat="server" SkinID="pager" OnPageIndexChanging="tb_PageIndexChanging">
            <Columns>
                <asp:BoundField HeaderText="No. Stock" DataField="NoStock" />
                <asp:BoundField HeaderText="Status" DataField="Status" />
                <asp:BoundField HeaderText="Unit" DataField="NoUnit" />
                <asp:BoundField HeaderText="Keterangan" DataField="Keterangan" />
                <asp:BoundField HeaderText="Luas" DataField="Luas" />
                <asp:BoundField HeaderText="Luas Tanah" DataField="LuasSG" />
                <asp:BoundField HeaderText="Luas Bangunan" DataField="LuasNett" />
                <asp:BoundField HeaderText="Price List" DataField="PL" />                
                <asp:BoundField HeaderText="Project" DataField="Project" />  
            </Columns>
        </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
        <script type="text/javascript">
            function call(nomor) {
                popEditUnit(nomor);
            }
        </script>
    </form>
</body>
</html>

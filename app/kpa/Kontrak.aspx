<%@ Page Language="c#" Inherits="ISC064.KPA.Kontrak" CodeFile="Kontrak.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Master Kontrak</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Kontrak">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Master Kontrak KPR</h1>
        <br>
        <table style="border: 1px solid #DCDCDC" cellspacing="5">
            <tr>
                <td>
                    <input type="button" class="btn btn-blue" value="Search" show-modal='#ModalPopUp' modal-title='Daftar Kontrak' modal-url='DaftarKontrak.aspx' id="search"
                        runat="server" name="search" accesskey="s">
                </td>
                <td>
                    <asp:DropDownList ID="thnKontrak" runat="server" CssClass="select-dropdown" Width="150">
                        <asp:ListItem Value="">Periode Kontrak :</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="jenis" runat="server" CssClass="select-dropdown" Width="230">
                        <asp:ListItem Value="">Jenis :</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="lokasi" runat="server" CssClass="select-dropdown" Width="170">
                        <asp:ListItem Value="">Lokasi :</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="project" OnSelectedIndexChanged="project_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Value="">Project : </asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="display" runat="server" CssClass="btn btn-blue" Text="Display" OnClick="display_Click"></asp:Button>
                </td>
            </tr>
        </table>
        <br />
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
        <script type="text/javascript">
            function call(nomor) {
                popEditKontrakKPA(nomor);
            }
        </script>

    </form>
</body>
</html>

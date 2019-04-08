<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Gimmick.aspx.cs" Inherits="ISC064.ADMINJUAL.Gimmick" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Gimmick</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Gimmick">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <div>
            <h1 class="title title-line">Master Gimmick</h1>
            <table style="border: 1px solid #DCDCDC" cellspacing="5">
                <tr>
                    <td><b>Filter :</b></td>
                    <td>
                        <asp:DropDownList ID="tipe" runat="server" Width="200">
                            <asp:ListItem Value="">Pilih Tipe :</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="status" runat="server" Width="200">
                            <asp:ListItem Value="">Pilih Status :</asp:ListItem>
                            <asp:ListItem Value="0">INAKTIF</asp:ListItem>
                            <asp:ListItem Value="1">AKTIF</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="project" runat="server" Width="200">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <input type="button" class="btn btn-blue" value="Search" show-modal='#ModalPopUp' modal-title='Daftar Gimmick' modal-url='DaftarGimmick.aspx' id="search"
                            runat="server" name="search" accesskey="s">
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="display" runat="server" CssClass="btn btn-blue" Text="Display" OnClick="display_Click"></asp:Button>
                    </td>
                </tr>
            </table>
            <br>
            <asp:GridView ID="tb" runat="server" SkinID="pager" OnPageIndexChanging="tb_PageIndexChanging">
                <Columns>
                    <asp:BoundField HeaderText="Nama" DataField="Nama"/>
                    <asp:BoundField HeaderText="Tipe" DataField="Tipe" />
                    <asp:BoundField HeaderText="Stock" DataField="Stock" />
                    <asp:BoundField HeaderText="Satuan" DataField="Satuan" />
                </Columns>
            </asp:GridView>
            <script type="text/javascript">
                function call(nomor) {
                    popEditMGimmick(nomor);
                }
            </script>
        </div>
    </form>
</body>
</html>

<%@ Page Language="c#" Inherits="ISC064.COLLECTION.Tunggakan" CodeFile="Tunggakan.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Surat Peringatan</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Surat Peringatan">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <input type="text" style="display: none">
        <h1 class="title title-line">Surat Peringatan</h1>
        <br />
        <table style="border: 1px solid #DCDCDC" cellspacing="5">
            <tr>
                <td style="">
                    <input type="button" class="btn btn-blue" value="Search" show-modal='#ModalPopUp' modal-title='Daftar Tunggakan' modal-url='DaftarTunggakan.aspx' id="search" runat="server" name="search" accesskey="s">
                </td>
                <td style="">
                    <asp:DropDownList ID="tipe" runat="server" CssClass="" Width="230">
                        <asp:ListItem Value="">Tipe :</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="project" runat="server" CssClass="" Width="110">
                        <asp:ListItem Value="">Project :</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:DropDownList ID="level" runat="server" CssClass="" Width="90">
                        <asp:ListItem Value="">Level :</asp:ListItem>
                        <asp:ListItem Value="1">Level 1</asp:ListItem>
                        <asp:ListItem Value="2">Level 2</asp:ListItem>
                        <asp:ListItem Value="3">Level 3</asp:ListItem>
                        <asp:ListItem Value="4">Level 4</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="status" runat="server" CssClass="" Width="110">
                        <asp:ListItem Value="">Status :</asp:ListItem>
                        <asp:ListItem Value="A">AKTIF</asp:ListItem>
                        <asp:ListItem Value="S">SETTLED</asp:ListItem>
                        <asp:ListItem Value="U">UPGRADED</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;"><b>Dari</b></td>
                <td>
                    <asp:TextBox ID="dari" runat="server" CssClass="txt_center" Width="135"></asp:TextBox>
                    <label for="dari" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                    <asp:Label ID="daric" runat="server" CssClass="err"></asp:Label>
                </td>
                <td><b style="">Sampai</b></td>
                <td>
                    <asp:TextBox ID="sampai" runat="server" CssClass="txt_center" Width="135"></asp:TextBox>
                    <label for="sampai" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                    <asp:Label ID="sampaic" runat="server" CssClass="err"></asp:Label>
                </td>
                <td style="">
                    <asp:Button ID="display" runat="server" CssClass="btn btn-blue" Text="Display" OnClick="display_Click"></asp:Button>

                </td>
            </tr>
        </table>
        <br>
        <asp:GridView ID="tb" runat="server" SkinID="pager" OnPageIndexChanging="tb_PageIndexChanging">
            <Columns>
                <asp:BoundField HeaderText="No. ST" DataField="ST" />
                <asp:BoundField HeaderText="Tgl. " DataField="Tgl" />
                <asp:BoundField HeaderText="Status" DataField="Status" />
                <asp:BoundField HeaderText="Customer" DataField="Customer" />
                <asp:BoundField HeaderText="Keterangan" DataField="Keterangan" />
                <asp:BoundField HeaderText="Nilai" DataField="Nilai" />
                <asp:BoundField HeaderText="Project" DataField="Project" />
            </Columns>
        </asp:GridView>
        <script type="text/javascript">
            function call(nomor) {
                popEditTunggakan(nomor);
            }
        </script>
    </form>
</body>
</html>

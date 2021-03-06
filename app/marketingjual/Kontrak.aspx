<%@ Page Language="c#" Inherits="ISC064.MARKETINGJUAL.Kontrak" CodeFile="Kontrak.aspx.cs" %>

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
        <h1 class="title title-line">Master Kontrak</h1>
        <br>
        <table style="border: 1px solid #DCDCDC" cellspacing="5">
            <tr>
                <td></td>
                <td>
                    <input type="button" class="btn btn-blue" value="Search" show-modal='#ModalPopUp' modal-title='Daftar Kontrak' modal-url='DaftarKontrak.aspx' id="search"
                        runat="server" name="search" accesskey="s">
                </td>
            </tr>
            <tr>
                <td><b>View by</b></td>
                <td>
                    <asp:DropDownList ID="thnKontrak" runat="server" Width="200">
                        <asp:ListItem Value="">Periode Kontrak :</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="jenis" runat="server" Width="200">
                        <asp:ListItem Value="">Jenis :</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="lokasi" runat="server" Width="200">
                        <asp:ListItem Value="">Lokasi :</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:DropDownList ID="titipjual" runat="server" Width="200">
                        <asp:ListItem Value="">Status Titip Jual :</asp:ListItem>
                        <asp:ListItem Value="1">Titip Jual</asp:ListItem>
                        <asp:ListItem Value="0">Non Titip Jual</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="status" runat="server" Width="200">
                        <asp:ListItem Value="">Status Kontrak :</asp:ListItem>
                        <asp:ListItem Value="A">Aktif</asp:ListItem>
                        <asp:ListItem Value="B">Batal</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="project" runat="server" Width="200" AutoPostBack="true" OnSelectedIndexChanged="project_SelectedIndexChanged">
                        <asp:ListItem Value="">Project :</asp:ListItem>
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
        <br>
        <div class="peach">
            Status : A = Aktif / B = Batal
        </div>
        <asp:GridView ID="tb" runat="server" SkinID="pager" OnPageIndexChanging="tb_PageIndexChanging">
            <Columns>
                <asp:BoundField HeaderText="No. Kontrak" DataField="NoKontrak" ItemStyle-Width="180" />
                <asp:BoundField HeaderText="Unit" DataField="NoUnit" ItemStyle-Width="100" />
                <asp:BoundField HeaderText="Tgl." DataField="Tanggal" ItemStyle-Width="75" />
                <asp:BoundField HeaderText="Customer/ Sales" DataField="Customer" ItemStyle-Width="200" />
                <asp:BoundField HeaderText="Keterangan" DataField="Ket" />
                <asp:BoundField HeaderText="Project" DataField="Project" />
            </Columns>
        </asp:GridView>
        <script type="text/javascript">
            function call(nomor) {
                popEditKontrak(nomor);
            }
        </script>
    </form>
</body>
</html>

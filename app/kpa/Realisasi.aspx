<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Realisasi.aspx.cs" Inherits="ISC064.KPA.Realisasi" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Realisasi Tagihan KPR</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Realisasi Tagihan KPR">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <input type="text" style="display: none">
        <h1 class="title title-line">Realisasi Tagihan KPR</h1>
        <br>
        <table style="border: 1px solid #DCDCDC" cellspacing="5">
            <tr>
                <td>
                    <input type="button" class="btn btn-blue" value="Search" show-modal='#ModalPopUp' modal-title='Daftar Realisasi' modal-url='DaftarRealisasi.aspx?status=undefined' id="search" runat="server"
                        name="search" accesskey="s">
                </td>
                <td>
                    <asp:DropDownList ID="user" runat="server" CssClass="ddl form-control" Width="180">
                        <asp:ListItem Value="">Kasir :</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="project" runat="server" CssClass="ddl form-control" Width="180">
                        <asp:ListItem Value="">Project :</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>Dari</td>
                <td>
                    <asp:TextBox ID="dari" runat="server" CssClass="txt_center tgl" Width="85"></asp:TextBox>
                    <label for="dari" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                    <asp:Label ID="daric" runat="server" CssClass="err"></asp:Label>
                </td>
                <td>Sampai</td>
                <td>
                    <asp:TextBox ID="sampai" runat="server" CssClass="txt_center tgl" Width="85"></asp:TextBox>
                    <label for="sampai" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                    <asp:Label ID="sampaic" runat="server" CssClass="err"></asp:Label>
                </td>
                <td>
                    <asp:Button ID="display" runat="server" CssClass="btn btn-blue" Text="Display" OnClick="display_Click"></asp:Button>
                </td>
            </tr>
        </table>
        <br />
        <asp:GridView ID="tb" runat="server" SkinID="pager" OnPageIndexChanging="tb_PageIndexChanging">
            <Columns>
                <asp:BoundField HeaderText="No. Realisasi" DataField="Realisasi" Visible="false" />
                <asp:BoundField HeaderText="No. Realisasi" DataField="NoRealisasi" />
                <asp:BoundField HeaderText="Tgl. / Kasir" DataField="Tgl" />
                <asp:BoundField HeaderText="Keterangan" DataField="Keterangan" />
                <asp:BoundField HeaderText="Total Realisasi" DataField="Total" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField HeaderText="Project" DataField="Project" />
            </Columns>
        </asp:GridView>
        <script type="text/javascript">
            function call(nomor) {
                popEditRealisasi(nomor);
            }
        </script>
    </form>
</body>
</html>

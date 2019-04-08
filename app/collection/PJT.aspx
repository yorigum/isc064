<%@ Page Language="c#" Inherits="ISC064.COLLECTION.PJT" CodeFile="PJT.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Pemberitahuan Jatuh Tempo</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="P. Jatuh Tempo">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <input type="text" style="display: none">
        <h1 class="title title-line">Pemberitahuan Jatuh Tempo</h1>
        <br>
        <table style="border: solid silver 1px;" cellspacing="5">
            <tr>
                <td style="">
                    <input type="button" class="btn btn-blue" value="Search" show-modal='#ModalPopUp' modal-title='Daftar PJT' modal-url='DaftarPJT.aspx' id="search" runat="server" name="search" accesskey="s">
                </td>
                <td>
                    <asp:DropDownList ID="tipe" runat="server" CssClass="select-dropdown" style="margin-left:0px;">
                        <asp:ListItem Value="">Tipe :</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td colspan ="2">
                    <asp:DropDownList ID="project" runat="server" CssClass="select-dropdown" style="margin-left:0px;">
                        <asp:ListItem Value="">Project :</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align:right"><b>Dari</b></td>
                <td>
                    <asp:TextBox ID="dari" runat="server" type="text" CssClass="txt_center"></asp:TextBox>
                    <label for="dari" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                    <asp:Label ID="daric" runat="server" CssClass="err"></asp:Label>
                </td>
                <td style=""><b>Sampai</b></td>
                <td>
                    <asp:TextBox ID="sampai" runat="server" type="text" CssClass="txt_center"></asp:TextBox>
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
                <asp:BoundField HeaderText="No. Invoice" DataField="Invoice" />
                <asp:BoundField HeaderText="Tgl. " DataField="Tgl" />
                <asp:BoundField HeaderText="Customer" DataField="Customer" />
                <asp:BoundField HeaderText="Keterangan" DataField="Keterangan" />
                <asp:BoundField HeaderText="Nilai" DataField="Nilai" />
                <asp:BoundField HeaderText="Project" DataField="Project" />
            </Columns>
        </asp:GridView>
        <script type="text/javascript">
            function call(nomor) {
                popEditPJT(nomor);
            }
        </script>
    </form>
</body>
</html>

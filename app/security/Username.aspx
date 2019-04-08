<%@ Page language="c#" Inherits="ISC064.SECURITY.Username" CodeFile="Username.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Username</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Username">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Username</h1>
        <br>
        <table style="border: 1px solid #DCDCDC" cellspacing="5">
            <tr>
                <td>Status :</td>
                <td>
                    <asp:RadioButton ID="aktif" class="igroup-radio" runat="server" Text="Aktif" GroupName="status" Font-Bold="True" Checked="True"></asp:RadioButton>
                    <asp:RadioButton ID="blokir" class="igroup-radio" runat="server" Text="Blokir" GroupName="status" Font-Bold="True"></asp:RadioButton>
                </td>
                <td>&nbsp;</td>
                <td>
                    <asp:DropDownList ID="seclevel" runat="server" CssClass="ddl form-control" Width="250">
                        <asp:ListItem Value="">Security Level :</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:LinkButton ID="display" runat="server" CssClass="btn btn-blue" Text="Display" OnClick="display_Click">Display</asp:LinkButton>
                </td>
            </tr>
        </table>
        <br>
        <p style="padding: 3">
            * : Log-In terakhir sudah melebihi 1 tahun
				<br>
            ** : Password sudah harus diganti
        </p>
        <asp:GridView ID="tb" runat="server" SkinID="pager" OnPageIndexChanging="tb_PageIndexChanging">
            <Columns>
                <asp:BoundField HeaderText="Nama" DataField="Nama" />
                <asp:BoundField HeaderText="Kode " DataField="Kode" />
                <asp:BoundField HeaderText="Sec. Level " DataField="SecLevel" />
                <asp:BoundField HeaderText="Tgl Log - In " DataField="TglLogin" />
                <asp:BoundField HeaderText="Tgl Pass " DataField="TglPass" />
                <asp:BoundField HeaderText="Rotasi " DataField="RotasiPass" />                
            </Columns>
        </asp:GridView>
    </form>
</body>
</html>

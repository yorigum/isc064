<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CounterLaunching.aspx.cs" Inherits="ISC064.SECURITY.CounterLaunching" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <script type="text/javascript" src="/Js/JQuery.min.js"></script>
    <meta name="ctrl" content="1">
    <meta name="sec" content="Launching - Pendaftaran Counter Pilih Unit">
</head>
<body>
    <form id="form1" runat="server">
    <uc1:Head ID="Head1" runat="server"></uc1:Head>
    <div>
    <h1 class="title title-line">Pendaftaran Counter Pilih Unit</h1>
        <br>
        <table cellspacing="0" cellspacing="0">
            <tr valign="top">
                <td style="padding-right: 10">
                    <p><b>Terbaru :</b></p>
                    <asp:ListBox ID="baru" Rows="25" runat="server" Width="200" CssClass="ddl" ondblclick="popEditCounterLaunching(this.options[this.selectedIndex].value)"></asp:ListBox>
                    <p class="feed">
                        <asp:Label ID="feed" runat="server"></asp:Label>
                    </p>
                </td>
                <td style="padding: 5 10 0 15">
                    <img src="/Media/line_vert.gif"></td>
                <td>
                    <table cellspacing="5">
                        <tr>
                            <td >Nama </td>

                            <td>
                                <asp:TextBox ID="nama" runat="server" CssClass="txt igroup" Width="300" MaxLength="50"></asp:TextBox>
                                <asp:Label ID="namac" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td >Nomor </td>

                            <td>
                                <asp:TextBox ID="nomor" runat="server" CssClass="txt igroup" Width="100" MaxLength="50"></asp:TextBox>
                                <asp:Label ID="nomorc" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>User</td>
                            <td>
                                <asp:DropDownList runat="server" ID="username"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:LinkButton ID="save" runat="server" CssClass="btn btn-blue" Width="75" OnClick="save_Click"><i class="fa fa-share"></i> OK</asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>

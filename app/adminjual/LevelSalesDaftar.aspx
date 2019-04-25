<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.ADMINJUAL.LevelSalesDaftar" CodeFile="LevelSalesDaftar.aspx.cs" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>Pendaftaran Level Sales Baru</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Jenis Unit - Pendaftaran Level Sales Baru">
</head>
<body class="body-padding">
    <form class="cnt" id="Form1" method="post" runat="server">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Pendaftaran Level Sales Baru</h1>
        <br />
        <table cellspacing="0">
            <tr valign="top">
                <td style="padding-right: 10px">
                    <p>
                        <b>Terbaru :</b>
                    </p>
                    <asp:ListBox ID="baru" Rows="25" Enabled="false" runat="server" Width="200" CssClass="ddl"></asp:ListBox>
                    <p class="feed">
                        <asp:Label ID="feed" runat="server"></asp:Label>
                    </p>
                </td>
                <td style="padding-right: 10px; padding-left: 15px; padding-bottom: 0px; padding-top: 5px">
                    <img src="/Media/line_vert.gif">
                </td>
                <td>
                    <asp:Label ID="noreflevel" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                    <table cellspacing="5">
                        <tr>
                            <td colspan="3">
                                <p>
                                    <b>Dokumen</b>
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td>No. Level
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="nolevel" runat="server" CssClass="txt" Width="65" Font-Bold="True"
                                    Text="#AUTO#" ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Project</td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="project" runat="server" AutoPostBack="true" OnSelectedIndexChanged="project_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Nama 
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="nama" runat="server" CssClass="txt" Width="150" MaxLength="50"></asp:TextBox>
                                <asp:Label ID="namac" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Tipe 
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:DropDownList ID="tipe" runat="server" Width="200" AutoPostBack="true" OnSelectedIndexChanged="tipe_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:Label ID="tipec" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Parent 
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:DropDownList ID="parent" runat="server" Width="200" AutoPostBack="true">
                                    <%--<asp:ListItem Value="0">Pilih Parent</asp:ListItem>--%>
                                </asp:DropDownList>
                                <asp:Label ID="parentc" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table style="height: 50px">
                        <tr>
                            <td>
                                <asp:LinkButton ID="save" runat="server" CssClass="btn btn-blue" Width="75" AccessKey="a" OnClick="save_Click"><i class="fa fa-share"></i> OK </asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>



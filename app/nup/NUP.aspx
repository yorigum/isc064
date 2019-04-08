<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NUP.aspx.cs" Inherits="ISC064.NUP.NUP" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Master NUP</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="NUP - Master NUP">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Master NUP</h1>
        <br>
        <table style="border: 1px solid #DCDCDC" cellspacing="5">
            <tr>
                <%--<td style="display: none">
                    <input type="button" class="btn" value="Search" onclick="popDaftarNUP();" id="search"
                        runat="server" name="search" accesskey="s">
                </td>--%>
                <td>Display Nama / NUP</td>
                <td>
                    <asp:TextBox ID="keyword" runat="server" CssClass="txt" Width="150"></asp:TextBox>
                </td>
                <td>
                    <asp:DropDownList ID="project" runat="server" AutoPostBack="true" OnSelectedIndexChanged="project_SelectedIndexChanged">
                        <asp:ListItem>Project : </asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="jenis" runat="server"></asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="display" runat="server" CssClass="btn" Text="Display" OnClick="display_Click"></asp:Button>
                </td>
            </tr>
        </table>
        <br>
        <asp:GridView ID="tb" runat="server" SkinID="pager" OnPageIndexChanging="tb_PageIndexChanging">
            <Columns>
                <asp:BoundField HeaderText="NUP" DataField="NUP" />
                <asp:BoundField HeaderText="Status NUP " DataField="Status" />
                <asp:BoundField HeaderText="Customer" DataField="Customer" />
                <asp:BoundField HeaderText="Agent" DataField="Agent" />
                <asp:BoundField HeaderText="Jenis Properti" DataField="Jenis" />
                <asp:BoundField HeaderText="Project" DataField="Project" />
                <asp:BoundField HeaderText="" DataField="Revisi" />
                <asp:BoundField HeaderText="" DataField="Refund" />
            </Columns>
        </asp:GridView>
        <script language="javascript">
            function call(nomor,Tipe,Project) {
                popNUP(nomor, Tipe, Project);
            }
        </script>
    </form>
</body>
</html>

﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DaftarSKL.aspx.cs" Inherits="ISC064.COLLECTION.DaftarSKL" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html lang="en">
<html>
<head>
    <title>Daftar Surat Keterangan Lunas</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="(Pop-Up) Daftar Surat Keterangan Lunas">
    <meta http-equiv="pragma" content="no-cache">
    <base target="_self">
</head>
<body class="body-padding pop" onkeyup="if(event.keyCode==27){window.close()}"
    onload="document.getElementById('keyword').select()">
    <form id="Form1" method="post" runat="server">
        <div class="pad">
            <table>
                <tr>
                    <td>
                        <asp:TextBox ID="keyword" runat="server" CssClass="input-text" Width="400"></asp:TextBox>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="project"></asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="search" runat="server" CssClass="btn btn-blue" Text="Search" AccessKey="s" OnClick="search_Click"></asp:Button>
                    </td>
                </tr>
            </table>
        </div>
        <br>
        <asp:GridView ID="tb" runat="server" SkinID="pager" OnPageIndexChanging="tb_PageIndexChanging">
            <Columns>
                <asp:BoundField HeaderText="No. System" DataField="System" />
                <asp:BoundField HeaderText="No. Manual" DataField="Manual" />
                <asp:BoundField HeaderText="Tgl." DataField="Tgl" />
                <asp:BoundField HeaderText="No. Kontrak" DataField="Kontrak" />
                <asp:BoundField HeaderText="Customer" DataField="Customer" />
                <asp:BoundField HeaderText="Project" DataField="Project" />
            </Columns>
        </asp:GridView>
        <input type="text" style="display: none;">
        <script type="text/javascript">
            function call(nomor) {
                window.parent.call(nomor);
                location.href = "SKLEdit.aspx?NoSKL='" + nomor + "'";
                window.parent.globalclose();
            }
        </script>
    </form>
</body>
</html>
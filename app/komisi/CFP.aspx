﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CFP.aspx.cs" Inherits="ISC064.KOMISI.CFP" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <title>Pengajuan Pencairan Closing Fee</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Pengajuan Pencairan Closing Fee">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <input type="text" style="display: none">
        <h1 class="title title-line">Pengajuan Pencairan Closing Fee</h1>
        <br />
        <asp:ScriptManager runat="server" ID="script" EnablePartialRendering="true"></asp:ScriptManager>
        <asp:UpdatePanel runat="server" ID="update" UpdateMode="Conditional">
            <ContentTemplate>
                <table style="border: 1px solid #DCDCDC" cellspacing="5">
                    <tr>
                        <td>
                            <input type="button" style="" class="btn btn-blue" value="Search" show-modal='#ModalPopUp' modal-title='Daftar Pengajuan Pencairan Closing Fee' modal-url='DaftarCFP.aspx' id="search" runat="server" name="search" accesskey="s">
                        </td>
                        <td colspan="2">
                            <asp:DropDownList ID="project" runat="server" Width="250" AutoPostBack="true" OnSelectedIndexChanged="project_SelectedIndexChanged">
                                    <asp:ListItem>Pilih Project :</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td colspan="2">
                            <asp:DropDownList ID="tipesales" runat="server" Width="250">
                                <asp:ListItem Value="0">Tipe Marketing :</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td><b>Tgl. Pengajuan</b></td>
                        <td style="text-align:right"><b>: &nbsp; Dari</b></td>
                        <td>
                            <asp:TextBox ID="dari" runat="server" CssClass="txt_center" Width="135"></asp:TextBox>
                            <label for="dari" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                            <asp:Label ID="daric" runat="server" CssClass="err"></asp:Label>
                        </td>
                        <td><b style="padding-left: 10px;">Sampai</b></td>
                        <td>
                            <asp:TextBox ID="sampai" runat="server" CssClass="txt_center" Width="135"></asp:TextBox>
                            <label for="sampai" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                            <asp:Label ID="sampaic" runat="server" CssClass="err"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="">
                            <asp:Button ID="display" runat="server" CssClass="btn btn-blue" Text="Display" OnClick="display_Click"></asp:Button>
                        </td>
                        <td colspan="4">
                            <asp:Label ID="alertc" runat="server" CssClass="err"></asp:Label>
                        </td>
                    </tr>
                </table>

                <br />
                <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" Style="min-width: 80%" CellSpacing="1">
                    <asp:TableRow HorizontalAlign="Left">
                        <asp:TableHeaderCell Width="150">No. Pengajuan</asp:TableHeaderCell>
                        <asp:TableHeaderCell Width="100">Tgl. Pengajuan</asp:TableHeaderCell>
                        <asp:TableHeaderCell Width="75">Tipe</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Penerima</asp:TableHeaderCell>
                    </asp:TableRow>
                </asp:Table>
            </ContentTemplate>
        </asp:UpdatePanel>

        <script type="text/javascript">
            function call(nomor,project) {
                popEditCFP(nomor, project);
            }
        </script>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KomisiCFEdit.aspx.cs" Inherits="ISC064.ADMINJUAL.KomisiCFEdit" %>

<%@ Register TagPrefix="uc1" TagName="HeadCF" Src="HeadCF.ascx" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <title>Edit Closing Fee</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="5">
    <meta name="sec" content="Komisi - Edit Closing Fee">
</head>
<body onkeyup="if(event.keyCode==27) window.close()">
    <form id="Form1" method="post" runat="server">
        <div class="tabdata">
            <div class="pad">
                <uc1:HeadCF ID="HeadCF" runat="server"></uc1:HeadCF>
                <table cellspacing="5">
                    <tr>
                        <td width="100%"></td>
                        <td>
                            <label class="ibtn ibtn-file">
                                <input type="button" class="btn btn-blue btn-ico" value="Log" id="btnlog" runat="server" name="btnlog" accesskey="l">
                            </label>
                        </td>
                        <td style="display: none;">
                            <input type="button" class="btn btn-red" value="Delete" id="btndel" runat="server" name="btndel"
                                accesskey="d">
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td class="stamp">Input :
                        <asp:Label ID="tglInput" runat="server"></asp:Label>
                        </td>
                        <td class="stamp">Edit :
                        <asp:Label ID="tglEdit" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table cellspacing="5">
                    <tr>
                        <td colspan="3">
                            <p>
                                <b>Data</b>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td>Level
                        </td>
                        <td>:
                        </td>
                        <td>
                            <asp:TextBox ID="ket" runat="server" CssClass="txt" Width="200" Font-Bold="True"></asp:TextBox>
                            <asp:Label ID="ketc" runat="server" CssClass="err"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Nilai Atas
                        </td>
                        <td>:
                        </td>
                        <td>
                            <asp:TextBox ID="atas" runat="server" CssClass="txt" Width="100" Style="text-align: right;"></asp:TextBox>
                            IDR
                        </td>
                    </tr>
                    <tr>
                        <td>Nilai Bawah
                        </td>
                        <td>:
                        </td>
                        <td>
                            <asp:TextBox ID="bawah" runat="server" CssClass="txt" Width="100" Style="text-align: right;"></asp:TextBox>
                            IDR
                        </td>
                    </tr>
                    <tr>
                        <td>Nilai General Manager
                        </td>
                        <td>:
                        </td>
                        <td>
                            <asp:TextBox ID="gm" runat="server" CssClass="txt" Width="100" Style="text-align: right;"></asp:TextBox>
                            IDR
                        </td>
                    </tr>
                    <tr>
                        <td>Nilai Sales Manager
                        </td>
                        <td>:
                        </td>
                        <td>
                            <asp:TextBox ID="sm" runat="server" CssClass="txt" Width="100" Style="text-align: right;"></asp:TextBox>
                            IDR
                        </td>
                    </tr>
                    <tr>
                        <td>Nilai Marketing
                        </td>
                        <td>:
                        </td>
                        <td>
                            <asp:TextBox ID="manager" runat="server" CssClass="txt" Width="100" Style="text-align: right;"></asp:TextBox>
                            IDR
                        </td>
                    </tr>
                </table>
                <table style="height: 50px">
                    <tr>
                        <td>
                            <asp:LinkButton ID="ok" runat="server" CssClass="btn btn-blue" Width="75" OnClick="ok_Click"><i class="fa fa-share"></i> OK</asp:LinkButton>
                        </td>
                        <td>
                            <input id="cancel" type="button" onclick="window.close()" class="btn btn-red" value="Cancel"
                                style="width: 75px">
                        </td>
                        <td>
                            <asp:LinkButton ID="save" runat="server" CssClass="btn btn-orange" Width="75" AccessKey="a" OnClick="save_Click"><i class="fa fa-check"></i> Apply </asp:LinkButton>
                        </td>
                        <td style="padding-left: 10px">
                            <p class="feed">
                                <asp:Label ID="feed" runat="server"></asp:Label>
                            </p>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>

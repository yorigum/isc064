<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KontrakApproveDiskon2.aspx.cs" Inherits="ISC064.APPROVAL.KontrakApproveDiskon2" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Approval Kontrak Diskon</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Kontrak - Approval Kontrak Diskon">
    <style type="text/css">
        tr, td {
            vertical-align: top;
        }
    </style>
</head>
<body class="body-padding" onkeyup="if(document.getElementById('cancel')){if(event.keyCode==27)document.getElementById('cancel').click()}">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <div>
            <h1 class="title title-line">Approval Kontrak Diskon</h1>
            <br>
            <table width="100%">
                <tr>
                    <td style="width: 10%">
                        <table>
                            <tr>
                                <td>
                                    <label class="ibtn ibtn-file" style="display:none">
                                        <input type="button" class="btn btn-blue btn-ico" value="  Log  " id="btnlog" runat="server" name="btnlog"
                                            accesskey="l">
                                    </label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table cellspacing="5">
                <tr>
                    <td>No. Approval
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:Label ID="noapprov1" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Unit
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:Label ID="unit" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Customer
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:Label ID="customer" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Sales
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:Label ID="agent" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3">&nbsp;</td>
                </tr>
                <tr style="display:none">
                    <td>User Name
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:Label ID="NamaDiskon" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="3"></td>
                    <td></td>
                </tr>
                <tr>
                    <td style="vertical-align: top">Tanggal Pengajuan</td>
                    <td style="vertical-align: top">:</td>
                    <td>
                        <asp:Label ID="tglpengajuan" runat="server" Font-Bold="True"></asp:Label></td>
                </tr>
                <tr>
                    <td>Nilai Kontrak Sebelum Diskon
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:Label ID="nkontrakbef" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>

                <tr>
                    <td>Nilai Diskon
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:Label ID="nilaidiskon" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="3">
                        <hr noshade size="1">
                    </td>
                    <td>-
                    </td>
                </tr>
                <tr>
                    <td>Nilai Kontrak Setelah Diskon
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:Label ID="nkontrakaft" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>                
                <tr>
                    <td style="vertical-align: top">Note</td>
                    <td style="vertical-align: top">:</td>
                    <td>
                        <asp:TextBox ID="note" runat="server" TextMode="MultiLine" Height="70px" Width="300px"></asp:TextBox></td>
                </tr>
            </table><br />
            <asp:LinkButton ID="save" runat="server" Width="75" OnClick="save_Click1" CssClass="btn btn-blue" AccessKey="s"><i class="fa fa-share"></i> Approve</asp:LinkButton>
            <asp:LinkButton ID="reject" runat="server" Width="75" OnClick="reject_Click" CssClass="btn btn-red" AccessKey="d"><i class="fa fa-share"></i> Reject</asp:LinkButton>
    </form>
</body>
</html>

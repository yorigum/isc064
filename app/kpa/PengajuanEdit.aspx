<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PengajuanEdit.aspx.cs" Inherits="ISC064.KPA.PengajuanEdit" %>

<!DOCTYPE html>
<%@ Register TagPrefix="uc2" TagName="Head" Src="Head.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeadPengajuan" Src="HeadPengajuan.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Edit Pengajuan Tagihan KPR</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="5">
    <meta name="sec" content="Pengajuan Tagihan KPR - Edit Pengajuan">
    <style type="text/css">
        .style1 {
            height: 24px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <uc2:Head ID="Head1" runat="server"></uc2:Head>
        <div class="">
            <div class="pad">
                <uc1:HeadPengajuan ID="HeadPengajuan1" runat="server"></uc1:HeadPengajuan>
                <table cellspacing="5">
                    <tr valign="top">
                        <td>Print
                        </td>
                        <td>:</td>
                        <td class="printhref">
                            <p>
                                <a id="printPengajuan" runat="server"><b>Pengajuan</b></a>
                            </p>
                        </td>
                        <td width="80%"></td>
                        <td>
                            <label class="ibtn ibtn-file">
                                <input type="button" class="btn btn-blue btn-ico" value="  Log  " id="btnlog" runat="server" name="btnlog"
                                    accesskey="l">
                            </label>
                        </td>
                        <td>
                            <asp:Button ID="batal" runat="server" Text="Batal" OnClick="batal_Click" CssClass="btn btn-red"></asp:Button>
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0" style="margin-left: 10px;">
                    <tr valign="top">
                        <td>
                            <div style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px; padding-top: 5px">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <p>
                                                <b>Status</b>
                                            </p>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr valign="top">
                                        <td width="300">
                                            <p>
                                                <asp:Label ID="status" runat="server" Font-Bold="True" Font-Size="20"></asp:Label>&nbsp;
                                            </p>
                                            <p>
                                                &nbsp;
                                            </p>
                                        </td>
                                        <td></td>
                                    </tr>
                                </table>
                            </div>
                            <table cellspacing="5">
                                <tr>
                                    <td colspan="3" class="style1">
                                        <p>
                                            <b>Data Pengajuan Tagihan KPR</b>
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td>No. Surat
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="nosurat" Width="200" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Tgl. Formulir
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tglform" runat="server" CssClass="txt_center" Width="85"></asp:TextBox>
                                        <label for="tglform" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                                        <asp:Label ID="tglformc" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Tgl. Rencana Cair
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tglcair" runat="server" CssClass="txt_center" Width="85"></asp:TextBox>
                                        <label for="tglcair" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                                        <asp:Label ID="tglcairc" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">Keterangan
                                    </td>
                                    <td valign="top">:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ket" runat="server" Width="300" CssClass="txt" Height="70" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <b>
                                            <br />
                                            Detil </b>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="50"></td>
                        <td>
                            <table>
                                <tr>
                                    <td>Nilai:
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="nilai" runat="server" Font-Bold="True" Font-Size="18"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>

                <table>
                    <tr>
                        <td colspan="3">
                            <asp:Table ID="rpt" CssClass="tb blue-skin" runat="server" CellSpacing="1">
                                <asp:TableHeaderRow>
                                    <asp:TableHeaderCell>No. Kontrak</asp:TableHeaderCell>
                                    <asp:TableHeaderCell>Customer</asp:TableHeaderCell>
                                    <asp:TableHeaderCell>No. Unit</asp:TableHeaderCell>
                                    <asp:TableHeaderCell Wrap="false">Nilai Pengajuan</asp:TableHeaderCell>
                                    <asp:TableHeaderCell Wrap="false">Nilai Realisasi</asp:TableHeaderCell>
                                    <asp:TableHeaderCell Wrap="false">No Realisasi</asp:TableHeaderCell>
                                </asp:TableHeaderRow>
                            </asp:Table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">&nbsp;
                        </td>
                    </tr>
                </table>
                <table id="tblOK" height="50">
                    <tr>
                        <td>
                            <asp:LinkButton ID="ok" runat="server" CssClass="btn btn-blue" Width="75" OnClick="ok_Click"><i class="fa fa-share"></i> OK
                            </asp:LinkButton>
                        </td>
                        <td>
                            <asp:LinkButton ID="save" runat="server" CssClass="btn btn-orange" Width="75" AccessKey="a"
                                OnClick="save_Click"><i class="fa fa-check"></i>Apply</asp:LinkButton>
                        </td>
                        <td>
                            <input id="cancel" type="button" onclick="window.close()" class="btn btn-red" value="Cancel"
                                style="width: 100px">
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

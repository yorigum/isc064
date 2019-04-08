<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RealisasiEdit.aspx.cs" Inherits="ISC064.KPA.RealisasiEdit" %>

<!DOCTYPE html>
<%@ Register TagPrefix="uc2" TagName="Head" Src="Head.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeadRealisasi" Src="HeadRealisasi.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Edit Realisasi Tagihan KPR</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="5">
    <meta name="sec" content="Realisasi Tagihan KPR - Edit Realisasi">
    <style type="text/css">
        .style1 {
            height: 24px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <uc2:Head ID="Head2" runat="server"></uc2:Head>
        <div class="">
            <div class="pad">
                <uc1:HeadRealisasi ID="HeadRealisasi" runat="server"></uc1:HeadRealisasi>
                <table cellspacing="5">
                    <tr valign="top">
                        <td>Print
                        </td>
                        <td>:</td>
                        <td class="printhref">
                            <p>
                                <a id="printrealisasi" runat="server"><b>Realisasi</b></a>
                            </p>
                        </td>
                        <td width="80%"></td>
                        <td>
                            <label class="ibtn ibtn-file">
                                <input type="button" class="btn btn-blue btn-ico" value="  Log  " id="btnlog" runat="server" name="btnlog"
                                    accesskey="l"/>
                            </label>
                        </td>
                        <td>
                            <p>
                                <%--<input type="button" class="btn" value="Void" id="btnvoid" runat="server" name="btnvoid"/>--%>
                                <asp:Button ID="btnvoid" runat="server" Text="Void" OnClick="btnvoid_Click" CssClass="btn btn-red"></asp:Button>
                            </p>
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
                                            <b>Data Realisasi Tagihan KPR</b>
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Tgl. Realisasi
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tglform" runat="server" CssClass="tgl txt_center" Width="85"></asp:TextBox>
                                        <label for="tglform" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                                        <asp:Label ID="tglformc" runat="server" CssClass="err"></asp:Label>
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
                            </table>
                        </td>
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
                        <td>
                            <b>
                                <br />
                                Detil </b>
                            <asp:Table ID="rpt" CssClass="tb blue-skin" runat="server" Width="100%">
                                <asp:TableHeaderRow>
                                    <asp:TableHeaderCell>No. TTS</asp:TableHeaderCell>
                                    <asp:TableHeaderCell>Status</asp:TableHeaderCell>
                                    <asp:TableHeaderCell>No. Kontrak</asp:TableHeaderCell>
                                    <asp:TableHeaderCell>Customer</asp:TableHeaderCell>
                                    <asp:TableHeaderCell>No. Unit</asp:TableHeaderCell>
                                    <asp:TableHeaderCell Wrap="false">Nilai Realisasi</asp:TableHeaderCell>
                                </asp:TableHeaderRow>
                            </asp:Table>
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

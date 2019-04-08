<%@ Reference Page="~/Customer.aspx" %>

<%@ Page Language="c#" Inherits="ISC064.FINANCEAR.MEMOEdit" CodeFile="MEMOEdit.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Headmemo" Src="Headmemo.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Navmemo" Src="Navmemo.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Edit Memo Pelunasan</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="5">
    <meta name="sec" content="Memo Pelunasan - Edit MEMO">
</head>
<body onkeyup="if(event.keyCode==27)window.close()">
    <form id="Form1" method="post" runat="server">
        <div class="content-header">
            <uc1:Navmemo ID="NavMEMO1" runat="server" Aktif="1"></uc1:Navmemo>
        </div>
        <div class="tabdata">
            <div class="pad">
                <uc1:Headmemo ID="HeadMEMO1" runat="server"></uc1:Headmemo>
                <table cellspacing="5">
                    <tr valign="top">
                        <td>Print </td>
                        <td class="printhref">
                            <p><a id="printMEMO" runat="server"><b>Memo Pelunasan</b></a></p>
                        </td>
                        <td width="100%"></td>
                        <td>&nbsp;</td>
                        <td>&nbsp;&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;&nbsp;</td>
                        <td>
                            <label class="ibtn ibtn-file">
                                <input type="button" class="btn btn-blue btn-ico" value="  Log  " id="btnlog" runat="server" name="btnlog"
                                    accesskey="l" />
                            </label>
                        </td>
                        <td>&nbsp;&nbsp;</td>
                        <td>
                            <label class="ibtn ibtn-remove">
                                <input type="button" class="btn btn-red btn-ico" value="Void" id="btnvoid" runat="server" name="btnvoid">
                            </label>
                            <p>&nbsp;</p>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td class="stamp">Kasir :
								<asp:Label ID="kasir" runat="server"></asp:Label>,
								<asp:Label ID="ip" runat="server"></asp:Label>
                            &nbsp;<asp:Label ID="tglInput" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0">
                    <tr valign="top">
                        <td>
                            <div style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px; padding-top: 5px">
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td>
                                            <p><b>Status</b></p>
                                        </td>
                                        <td>
                                            <p><b>Tipe Memo</b></p>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td width="300">
                                            <p>
                                                <asp:Label ID="status" runat="server" Font-Bold="True" Font-Size="20"></asp:Label>
                                            </p>
                                            <table width="100%">
                                                <tr>
                                                    <td>No. Memo</td>
                                                    <td>:</td>
                                                    <td>
                                                        <asp:Label ID="memoinfo" runat="server" Font-Bold="True"></asp:Label></td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            <p>
                                                <asp:Label ID="tipememo" runat="server" Font-Bold="True" Font-Size="20"></asp:Label>
                                            </p>
                                            <p>&nbsp;</p>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <table cellspacing="5">
                                <tr>
                                    <td colspan="3">
                                        <p><b>Data Memo Pelunasan</b></p>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Tgl. Memo</td>
                                    <td>:</td>
                                    <td>
                                        <asp:TextBox ID="tglmemo" runat="server" CssClass="txt_center" Width="85"></asp:TextBox>
                                        <label for="tglmemo" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                                        <asp:Label ID="tglmemoc" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Keterangan</td>
                                    <td>:</td>
                                    <td>
                                        <asp:TextBox ID="ket" runat="server" Width="425" CssClass="txt" MaxLength="200"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <p><b>Identitas Customer</b></p>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Unit</td>
                                    <td>:</td>
                                    <td>
                                        <asp:TextBox ID="unit" runat="server" Width="200" CssClass="txt" MaxLength="100"></asp:TextBox>
                                        <asp:Label ID="unitc" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Customer</td>
                                    <td>:</td>
                                    <td>
                                        <asp:TextBox ID="customer" runat="server" Width="300" CssClass="txt" MaxLength="100"></asp:TextBox>
                                        <asp:Label ID="customerc" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <table id="tblOK" style="height: 50px">
                                <tr>
                                    <td colspan="3">
                                        <asp:Label ID="alasan" runat="server" Style="font-size: 18px; color: red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="ok" runat="server" CssClass="btn btn-blue" Width="75" OnClick="ok_Click"><i class="fa fa-share"></i> OK</asp:LinkButton>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="save" runat="server" CssClass="btn btn-orange" Width="75" AccessKey="a" OnClick="save_Click"><i class="fa fa-check"></i> Apply </asp:LinkButton>
                                    </td>
                                    <td>
                                        <input id="cancel" type="button" onclick="window.close()" class="btn btn-red" value="Cancel" style="width: 75px" />
                                    </td>
                                    <td style="padding-left: 10px">
                                        <p class="feed">
                                            <asp:Label ID="feed" runat="server"></asp:Label>
                                        </p>
                                    </td>
                                    <td style="padding-left: 10px">
                                        <p class="fobo">
                                            <asp:Label ID="statusFOBO" runat="server"></asp:Label>
                                        </p>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 20px"></td>
                        <td>
                            <b>Nilai </b>
                            <p>
                                <asp:Label ID="nilai" runat="server" Font-Bold="True" Font-Size="18"></asp:Label>
                            </p>
                            <p>
                                <asp:CheckBox ID="pph" runat="server" Text="PPH" Visible="false"></asp:CheckBox>
                            </p>
                            <br />
                            <p id="alokasi" runat="server"><b>Alokasi Pelunasan</b></p>
                            <ul id="detil" runat="server" class="plike" style="text-align:left;">
                            </ul>
                            <br />
                            <asp:Label ID="lblAkunting" runat="server" Font-Bold="True" Font-Size="12pt" ForeColor="red"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
